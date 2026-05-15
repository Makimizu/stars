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
    public partial class PARAMETER_MASTER_LIST : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["mode"].ToString();
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR from PR_REMUN_TYPE where CODE = '" + LB_MODE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = "SCHEME : " + conn.GetFieldValue("DESCR").ToString();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";


            if (TXT_DESCRIPTION.Text.Trim() != "")
                where = where + " and DESCR like '%" + TXT_DESCRIPTION.Text.Trim() + "%' ";

            if (TXT_START_DATE.Text.Trim() != "")
                where = where + " and START_DATE like '%" + TXT_START_DATE.Text.Trim() + "%' ";

            if (TXT_END_DATE.Text.Trim() != "")
                where = where + " and END_DATE like '%" + TXT_END_DATE.Text.Trim() + "%' ";

           
            conn.QueryString = "select " +
                                "ID, " +
                                "DESCR, " +
                                "START_DATE = convert(varchar(20), START_DATE, 106), " +
                                "END_DATE = convert(varchar(20), END_DATE, 106), " +
                                "URL, " +
                                "RECORDS, " +
                                "PRODUCTS " +
                                "from V_PARAM_REMUN_MASTER " +
                                "where " +
                                "TYPE ='" + LB_MODE.Text + "' " + where;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbNAME = (LinkButton)DGR.Items[i].FindControl("LB_NAME");
                lbNAME.Text = DGR.Items[i].Cells[1].Text;
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
            if (e.CommandName == "Select")
            {
                Response.Redirect(e.Item.Cells[2].Text);

            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PARAM_REMUN_MASTER where ID = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }
    }
}