using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Tools
{
    public partial class SearchEmployeCompany : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB1.Text = Request.QueryString["company_code"];
                LB2.Text = Request.QueryString["parent"];
                LB4.Text = Request.QueryString["target"];
                callbak.Text = Request.QueryString["callback"];
                Setup();
            }
        }

        protected void Setup()
        {
            if (LB1.Text != "")
                TXT_COMPANY.Enabled = false;

            conn.QueryString = "select CODE,DESCR from PR_FAMILY_GROUP order by 1";
            conn.ExecuteQuery();
            DDL_FAMILY.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_FAMILY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillGrid()
        {
            LB_COUNT.Text = "";

            string company = "b.COMPANY_CODE";
            if (LB1.Text != "")
                company = "'" + LB1.Text + "'";

            string sql = "select " +
                            "REGNO = a.REGNO,  " +
                            "NAMA = a.NAMA, " +
                            "[TGL LAHIR] = convert(varchar(20),a.DOB,106), " +
                            "[E/C/S] = c.DESCR " +
                            "from PESERTA_MASTER a " +
                            "inner join BRANCH b on a.BRANCH_CODE=b.BRANCH_CODE " +
                            "inner join PR_FAMILY_GROUP c on a.FAMILY_GROUP=c.CODE " +
                            "inner join COMPANY d on b.COMPANY_CODE=d.COMPANY_CODE " +
                            "where b.COMPANY_CODE=" + company + " ";

            if (TXT_NAMA.Text.Trim() != "")
                sql = sql + " and a.NAMA like '%" + TXT_NAMA.Text.Trim() + "%' ";
            if (TXT_COMPANY.Text.Trim() != "")
                sql = sql + " and d.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";
            if (TXT_CABANG.Text.Trim() != "")
                sql = sql + " and b.NAMA_CABANG like '%" + TXT_CABANG.Text.Trim() + "%' ";
            if (TXT_DOB.Text.Trim() != "")
                sql = sql + " and datediff(day,a.DOB,'" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "')=0 ";
            if (TXT_REGNO.Text.Trim() != "")
                sql = sql + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";
            if (DDL_FAMILY.SelectedValue != "")
                sql = sql + " and a.FAMILY_GROUP='" + DDL_FAMILY.SelectedValue + "' ";
            if (DDL_STAT.SelectedValue != "")
                sql = sql + " and a.STAT='" + DDL_STAT.SelectedValue + "' ";

            try
            {
                conn.QueryString = sql;
                conn.ExecuteQuery();

                LB_COUNT.Text = " " + conn.GetRowCount().ToString() + " Records";

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR1.DataSource = dt;
                DGR1.DataBind();
            }
            catch { }
        }

        protected void DGR1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string mode = "";
                switch (LB2.Text)
                {
                    case "0": mode = "forms[0]"; break;
                    case "1": mode = "form1"; break;
                }

                //string script = "<script language='javascript'>window.opener.document." + mode + "." + LB4.Text + ".value = '" + e.Item.Cells[1].Text + "'; window.close(); </script>";
                string callback = "";
                if (callbak.Text != "")
                    callback = "window.opener.document." + mode + "." + callbak.Text + ";";

                string script = "<script language='javascript'> " +
                                    "window.opener.document." + mode + "." + LB4.Text + ".value = '" + e.Item.Cells[1].Text + "'; " +
                                    callback +
                                    "window.close(); " +
                                "</script>";
                Response.Write(script);

                Response.Write(script);
            }
        }

        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            DGR1.CurrentPageIndex = 0;
            FillGrid();
        }


        protected void DGR1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR1.CurrentPageIndex = e.NewPageIndex;
            FillGrid();
        }
    }
}