using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Quot
{
    public partial class QuotationTC : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_QUOTNO.Text = Request.QueryString["code"];
                LB_VER.Text = Request.QueryString["ver"];
                Setup();
                FillDGR();
            }            
        }

        protected void Setup()
        {
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                BT_SAVE.Visible = false;
            }

            conn.QueryString = "select distinct " +
                                "a.BENEFIT_ID,  " +
                                "DESCR = isnull(b.DESCR, 'GENERAL'), " +
                                "SEQ = isnull(c.SEQ,0) " +
                                "from QUOTATION_VERSION_TC a " +
                                "left join PR_BENEFIT b on a.BENEFIT_ID=b.CODE " +
                                "left join PARAM_BENEFIT_SEQ c on a.BENEFIT_ID=c.CODE " +
                                "where " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                "and VERNO = " + LB_VER.Text + " " +
                                "order by 3";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_TC '" + LB_QUOTNO.Text + "'," + LB_VER.Text + ",'" + DDL_BENEFIT.SelectedValue + "'";
                conn.ExecuteQuery();
            }
            catch
            {
                return;
            }

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    ddl.Enabled = false;
                }

                conn.QueryString = DGR.Items[i].Cells[5].Text;
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                try
                {
                    ddl.SelectedValue = DGR.Items[i].Cells[4].Text;
                }
                catch { }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                conn.QueryString = "exec SP_QUOTATION_VERSION_TC_UPDATE " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text + "," +
                                    "'" + DGR.Items[i].Cells[0].Text + "'," +
                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                    ddl.SelectedValue + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                try 
                {
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = "<BR>" + ex.Message;
                }
            }

            FillDGR();
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}
