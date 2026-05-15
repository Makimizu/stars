using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class Note_List : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from INVOICE_NOTA a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLNote();
        }

        protected void FillDDLNote()
        {
            conn.QueryString = "select TIPE_NOTA,DESCR from PARAM_NOTA_TYPE where APP_ID = '" + DDL_APP.SelectedValue + "'";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            string where = "";

            if (TXT_NO.Text.Trim() != "")
                where = where + " and a.NOTA_NO='" + TXT_NO.Text.Trim() + "' ";

            if (TXT_INVOICENO.Text.Trim() != "")
                where = where + " and a.INVOICENO='" + TXT_INVOICENO.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.CUSTOMER_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_NOPOL.Text.Trim() != "")
                where = where + " and a.CUSTOMER_CODE like '%" + TXT_NOPOL.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and a.NOTA_TYPE='" + DDL_TIPE.SelectedValue + "' ";

            if (TXT_NOTEDATE.Text.Trim() != "")
                where = where + " and convert(date,a.NOTA_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_NOTEDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_NOTEDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.NOTA_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_NOTEDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_INVDATE.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_INVDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "NOTA_NO, " +
                                "DC_DESCR, " +
                                "NOTA_TYPE_DESCR, " +
                                "INVOICENO, " +
                                "CUSTOMER_CODE, " +
                                "CUSTOMER_NAME, " +
                                "NOTA_DATE = convert(varchar(20),NOTA_DATE,106), " +
                                "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,106), " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                "REPORT_URL, " +
                                "REPORT_URL_ENG " +
                                "from V_NOTA_MASTER a " +   
                                "where " +
                                "a.DC='" +DDL_CD.SelectedValue+ "' " +
                                "and a.APP_ID = '" + DDL_APP.SelectedValue + "' " +
                                where + " order by a.NOTA_DATE desc";
            conn.ExecuteQuery();

            LB_RESULT.Text = "<TABLE style='border-spacing:0px;'><TR><TD>Total</TD><TD>:</TD><TD style='width:120px; text-align:right;'>" + conn.GetRowCount().ToString() + " Records</TD></TR></TABLE>";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbNOTENO = (LinkButton)DGR.Items[i].FindControl("LB_NOTENO");

                lbNOTENO.Text = DGR.Items[i].Cells[1].Text;
                lbNOTENO.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "") + "','INVOICE','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[2].Text == "CREDIT")
                {
                    DGR.Items[i].Cells[2].ForeColor = System.Drawing.Color.Red;
                    DGR.Items[i].Cells[9].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    DGR.Items[i].Cells[2].ForeColor = System.Drawing.Color.Green;
                    DGR.Items[i].Cells[9].ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Show")
            {


            }
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLNote();
        }
    }
}