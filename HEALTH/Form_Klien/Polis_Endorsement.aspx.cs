using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Endorsement : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {

        }

        protected void FillDGR()
        {
            string where = "";

            conn.QueryString = "select " +
                                "ID, " +
                                "SEQ, " +
                                "POLICY_NO = '<a href=''Polis_Endorsement_Frame.aspx?ID=' +a.ID+ '&SEQ=' +convert(varchar(10),a.SEQ)+ '''>' + POLICY_NO + '</a>', " +
                                "COMPANY_NAME, " +
                                "PERIOD = convert(varchar(20),START_DATE,106) + ' - ' + convert(varchar(20),END_DATE,106), " +
                                "CREATEBY, " +
                                "CREATEDATE " +
                                "from V_ALTER_POLICY_PERIOD a " +
                                "where APPROVEBY is null " + where +
                                "order by " +
                                "COMPANY_NAME, ID, SEQ";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                LB_ERROR.Text = "";

                try
                {
                    conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_ROLLBACK '" + e.Item.Cells[0].Text + "'," + e.Item.Cells[2].Text;
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                    return;
                }

                try
                {
                    FillDGR();
                }
                catch
                {
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
            }
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            TR1.Visible = false;
            TR2.Visible = false;
            TR3.Visible = true;
            FillDDLPolis();
            
            //Response.Redirect("Polis_Endorsement_Header.aspx?ID=&SEQ=");
        }

        protected void FillDDLPolis()
        {
            conn.QueryString = "select ID, POLICY = POLICY_NO + ' - ' + COMPANY_NAME from V_POLICY where (POLICY_NO + ' - ' + COMPANY_NAME) like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' order by COMPANY_NAME";
            conn.ExecuteQuery();
            DDL_POLICY.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_POLICY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLPeriod();
        }

        protected void FillDDLPeriod()
        {
            conn.QueryString = "select ID, PERIOD = convert(varchar(20),START_DATE,106) + ' - ' + convert(varchar(20),END_DATE,106) from POLICY_PERIOD where POLICY_ID = " + DDL_POLICY.SelectedValue;
            conn.ExecuteQuery();
            DDL_PERIOD.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_POLICY_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLPeriod();
        }

        protected void BT_POLICYSEARCH_Click(object sender, EventArgs e)
        {
            FillDDLPolis();
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_ERRORSUBMIT.Text = "";

            try
            {
                conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_INSERT " +
                                    "'" + DDL_PERIOD.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                string seq = conn.GetFieldValue("SEQ").ToString();
                Response.Redirect("Polis_Endorsement_Frame.aspx?ID=" + DDL_PERIOD.SelectedValue + "&SEQ=" + seq);
            }
            catch(System.Exception ex)
            {
                LB_ERRORSUBMIT.Text = ex.Message;
                return;
            }
        }
    }
}