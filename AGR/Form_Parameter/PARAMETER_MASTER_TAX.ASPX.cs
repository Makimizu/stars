using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR
{
    public partial class PARAMETER_MASTER_TAX : System.Web.UI.Page
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
            conn.QueryString = "select " +
                                "DESCR		= a.DESCRIPTION, " +
                                "TYPE		= c.DESCR, " +
                                "COMPANY	= b.COMPANY_NAME, " +
                                "STARTDATE	= convert(varchar(20), a.STARTDATE, 106), " +
                                "ENDDATE	= convert(varchar(20), a.ENDDATE, 106) " +
                                "from		PARAM_REMUN_MASTER a " +
                                "inner join	V_LINK_CB_COMPANY b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                "inner join	PR_REMUN_TYPE c on a.TYPE = c.CODE " +
                                "where " +
                                "ID = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY").ToString();
            LB_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
            LB_ENDDATE.Text = conn.GetFieldValue("ENDDATE").ToString();
            LB_STARTDATE.Text = conn.GetFieldValue("STARTDATE").ToString();
            LB_TYPE.Text = conn.GetFieldValue("TYPE").ToString();

            // conn.QueryString = "select CODE, DESCR from V_LINK_UB_PRODUCT_MASTER";
            conn.QueryString = "select " +
                                "PRODUCT_CODE,  " +
                                "PRODUCT_DESCR = PRODUCT_DESCR +' ('+ PRODUCT_CODE + ')'  " +
                                "from V_LINK_UB_PARAM_PRODUCT_MASTER " +
                                "order by PRODUCT_DESCR, PRODUCT_CODE ";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from PR_CHANNEL_DISTRIBUTION";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDL_Level();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void FillDGR()
        {
            string where = "";
            conn.QueryString = "select * " +
                               "from V_PARAM_REMUN_COMMISION " +
                                "where " +
                                "ID ='" + LB_ID.Text + "' " + where;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

        }
        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PARAM_REMUN_COMMISION where ID = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }


        protected void BT1_SAVE_Click(object sender, EventArgs e)
        {
            //[SP_PARAM_REMUN_COMMISSION_UPSERT]

            conn.QueryString = "exec SP_PARAM_REMUN_COMMISSION_UPSERT " +
                            "@ID = '" + LB_ID.Text + "', " +
                            "@PRODUCT_CODE =  '" + DDL_PRODUCT.SelectedValue + "'," +
                            "@CHANNEL =  '" + DDL_CHANNEL.SelectedValue + "'," +
                            "@LEVEL =  '" + DDL_LEVEL.SelectedValue + "'," +
                            "@YEAR = '" + TXT_YEAR.Text.Trim() + "'," +
                            "@MIN_AMOUNT = '" + TXT_MIN.Text.Trim() + "'," +
                            "@MAX_AMOUNT = '" + TXT_MAX.Text.Trim() + "'," +
                            "@DESCRIPTION = '" + TXT_DESCRIPTION.Text.Trim() + "'," +
                            "@NETT = '" + DDL_NETT.SelectedValue + "'," +
                            "@COMMPCT = '" + TXT_COMMPCT.Text.Trim() + "'," +
                            "@TRANS_TYPE = '" + TXT_TRANS_TYPE.Text.Trim() + "'," +
                            "@OR1 = '" + TXT_OR1.Text.Trim() + "'," +
                            "@OR2 = '" + TXT_OR2.Text.Trim() + "'," +
                            "@OR3 = '" + TXT_OR3.Text.Trim() + "'," +
                            "@OR4 = '" + TXT_OR4.Text.Trim() + "'," +
                            "@OR5 = '" + TXT_OR5.Text.Trim() + "'," +
                            "@OR6 = '" + TXT_OR6.Text.Trim() + "'," +
                            "@OR7 = '" + TXT_OR7.Text.Trim() + "'," +
                            "@OR8 = '" + TXT_OR8.Text.Trim() + "'," +
                            "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

            conn.ExecuteQuery();
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDL_Level();
        }

        protected void FillDDL_Level()
        {
            DDL_LEVEL.Items.Clear();

            conn.QueryString = "select SUB_CODE, DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where CD_CODE = '" + DDL_CHANNEL.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }
    }
}