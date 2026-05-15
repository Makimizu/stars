using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Policy
{
    public partial class PolicyReinsurance : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                Setup();                
                FillDGR();
            }
        }

        protected void Setup()
        {
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                TBL_REINS.Visible = false;
            }
            else
            {
                conn.QueryString = "select ID, DESCR from V_LINK_REINS_TC_MASTER where TYPE='001' order by DESCR";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_TC.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            LoadLastTC();
        }

        protected void LoadLastTC()
        {
            conn.QueryString = "select top 1 REINS_TC_ID from POLICY_REINS " +
                                "where " +
                                "POLICY_ID = " + LB_ID.Text + " " +
                                "order by START_DATE desc";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
                LoadTC(conn.GetFieldValue("REINS_TC_ID").ToString());
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "CODE = a.REINS_TC_ID, " +
                                "START_DATE = convert(varchar(20),a.START_DATE,106), " +
                                "DESCR = a.REINS_TC_ID + ' - ' + b.DESCR " +
                                "from POLICY_REINS a " +
                                "left join V_LINK_REINS_TC_MASTER b on a.REINS_TC_ID = b.ID " +
                                "where " +
                                "a.POLICY_ID = " + LB_ID.Text + " " +
                                "order by " +
                                "a.START_DATE desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR.Items[i].FindControl("LBT_DATE");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                lbt.Text = DGR.Items[i].Cells[2].Text;
                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }

            if (Request.QueryString["readonly"].ToString() == "1")
            {
                DGR.Columns[DGR.Columns.Count - 1].Visible = false;
            }
        }

        protected void LoadTC(string code)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policyReinsbody.location.href = '../Form_Parameter/ReinsTC.aspx?CODE=" + code + "';</script>");
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LoadTC(e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from POLICY_REINS where " +
                                        "POLICY_ID = " + LB_ID.Text + " " +
                                        "and START_DATE = '" + e.Item.Cells[2].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                    LoadLastTC();
                }
                catch { }
            }
        }

        protected void BT_ADD_Click(object sender, EventArgs e)
        {
            if (TXT_DATE.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "exec SP_POLICY_REINS_INSERT " +
                                    LB_ID.Text + "," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + DDL_TC.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                                    
                conn.ExecuteNonQuery();
                FillDGR();
                LoadLastTC();
            }
            catch { }
        }
    }
    
}