using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace Archieve
{
    public partial class ArchieveDetail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_APPID.Text = Request.QueryString["APPID"].ToString();
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();

                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR = UPPER(DESCR) from ARCHIEVE.dbo.PARAM_ARSIP_TIPE where CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";

            string startdate, enddate;
            startdate = enddate = "null";

            if (TXT_DATE1.Text.Trim() != "")
                startdate = "'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "dd/MM/yyyy") + "'";
            if (TXT_DATE2.Text.Trim() != "")
                enddate = "'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "dd/MM/yyyy") + "'";

            conn.QueryString = "exec ARCHIEVE.dbo.SP_ARCHIEVE_DETAIL " +
                                "'" + LB_APPID.Text + "'," +
                                "'" + LB_TYPE.Text + "'," +
                                "'" + TXT_OWNER.Text.Trim() + "'," +
                                "'" + TXT_NAME.Text.Trim() + "'," +
                                "'" + TXT_REMARK.Text.Trim() + "'," +
                                startdate + "," +
                                enddate;
            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();
            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton bt = (LinkButton)DGR.Items[i].FindControl("BT_SELECT");
                bt.Text = DGR.Items[i].Cells[1].Text.ToUpper();

                if (DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") == "")
                {
                    bt.Enabled = false;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string filename = e.Item.Cells[1].Text.Replace(" ", "");
                GlobalUse.SQLToFile(filename.Trim(), e.Item.Cells[2].Text, Page);
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

    }
}