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
    public partial class Invoice_Adjustment : System.Web.UI.Page
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
                                "from INVOICE_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (TXT_NO.Text.Trim() != "")
                where = where + " and a.INVOICENO='" + TXT_NO.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.CUSTOMER_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_NOPOL.Text.Trim() != "")
                where = where + " and a.CUSTOMER_CODE like '%" + TXT_NOPOL.Text.Trim() + "%' ";
            if (TXT_INVDATE.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_INVDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.INVOICE_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_INVDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_ARDATE.Text.Trim() != "")
                where = where + " and convert(date,a.AR_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_ARDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_ARDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.AR_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_ARDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_AGE1.Text.Trim() != "")
                where = where + " and a.AGING >= '" + TXT_AGE1.Text.Trim() + "' ";

            if (TXT_AGE2.Text.Trim() != "")
                where = where + " and a.AGING <= '" + TXT_AGE2.Text.Trim() + "' ";

            conn.QueryString = "select " +
                                "INVOICENO, " +
                                "CUSTOMER_CODE, " +
                                "CUSTOMER_NAME, " +
                                "INVOICE_TYPE_DESCR, " +
                                "INVOICE_DATE = convert(varchar(20),INVOICE_DATE,106), " +
                                "AR_DATE = convert(varchar(20),AR_DATE,106), " +
                                "AGING, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                "PAYMENT_AMOUNT = replace(convert(varchar(100),convert(money,PAYMENT_AMOUNT),1),'.00',''), " +
                                "WRITEOFF = replace(convert(varchar(100),convert(money,WRITEOFF),1),'.00',''), " +
                                "DISCOUNT = replace(convert(varchar(100),convert(money,DISCOUNT),1),'.00',''), " +
                                "KOMISI = replace(convert(varchar(100),convert(money,KOMISI),1),'.00',''), " +
                                "PPN = replace(convert(varchar(100),convert(money,PPN),1),'.00',''), " +
                                "PPH = replace(convert(varchar(100),convert(money,PPH),1),'.00',''), " +
                                "ADJ_C = replace(convert(varchar(100),convert(money,ADJ_C),1),'.00',''), " +
                                "POLICYFEE = replace(convert(varchar(100),convert(money,POLICYFEE),1),'.00',''), " +
                                "STAMPFEE = replace(convert(varchar(100),convert(money,STAMPFEE),1),'.00',''), " +
                                "ADJ_D = replace(convert(varchar(100),convert(money,ADJ_D),1),'.00',''), " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                                "a.REPORT_URL " +
                                "from V_INVOICE_MASTER_ADJUSTMENT a " +
                                "where " +
                                "a.APP_ID = '" + DDL_APP.SelectedValue + "' " + where + " " +
                                "order by " +
                                "a.CUSTOMER_CODE, " +
                                "a.INVOICE_DATE";

            conn.ExecuteQuery();
            string result = "<TABLE style='border-spacing:0px;'><TR><TD>Total</TD><TD>:</TD><TD style='width:120px; text-align:right;'>" + conn.GetRowCount().ToString() + " Records</TD>";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select " +
                                "BILLED = replace(convert(varchar(100),convert(money,SUM(AMOUNT)),1),'.00',''), " +
                                "OUTSTANDING = replace(convert(varchar(100),convert(money,SUM(OUTSTANDING)),1),'.00','') " +
                                "from V_INVOICE_MASTER_ADJUSTMENT a " +
                                "where " +
                                "a.APP_ID = '" + DDL_APP.SelectedValue + "' " + where;
            conn.ExecuteQuery();

            result = result +
                        "<TR><TD>Billed</TD><TD>:</TD><TD style='text-align:right;'>" + conn.GetFieldValue(0, 0).ToString() + "</TD></TR>" +
                        "<TR><TD>Outstanding</TD><TD>:</TD><TD style='text-align:right;'>" + conn.GetFieldValue(0, 1).ToString() + "</TD></TR>" +
                        "</TABLE>";
            LB_RESULT.Text = result;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbINVOICENO = (LinkButton)DGR.Items[i].FindControl("LB_INVOICENO");
                TextBox txtDISC = (TextBox)DGR.Items[i].FindControl("TXT_DISC");
                TextBox txtCOMM = (TextBox)DGR.Items[i].FindControl("TXT_COMM");
                TextBox txtPPN = (TextBox)DGR.Items[i].FindControl("TXT_PPN");
                TextBox txtPPH = (TextBox)DGR.Items[i].FindControl("TXT_PPH");
                TextBox txtADJC = (TextBox)DGR.Items[i].FindControl("TXT_ADJMIN");
                TextBox txtPOLICYFEE = (TextBox)DGR.Items[i].FindControl("TXT_POLFEE");
                TextBox txtSTAMPFEE = (TextBox)DGR.Items[i].FindControl("TXT_STAMPFEE");
                TextBox txtADJD = (TextBox)DGR.Items[i].FindControl("TXT_ADJPLUS");
                Button btHST = (Button)DGR.Items[i].FindControl("BT_HST");
                Button btWO = (Button)DGR.Items[i].FindControl("BT_WO");

                

                lbINVOICENO.Text = DGR.Items[i].Cells[1].Text;
                lbINVOICENO.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") + "','INVOICE','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                btHST.Attributes.Add("onclick", "window.open('Invoice_History.aspx?INVOICENO=" + DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "','INVOICE','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[28].Text.Trim().Replace("&nbsp;", "") != "0")
                {
                    btWO.Visible = true;
                    btWO.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk WRITE OFF ?')){return false;};");
                }

                txtDISC.Text = DGR.Items[i].Cells[11].Text;
                txtCOMM.Text = DGR.Items[i].Cells[12].Text;
                txtPPN.Text = DGR.Items[i].Cells[13].Text;
                txtPPH.Text = DGR.Items[i].Cells[14].Text;
                txtADJC.Text = DGR.Items[i].Cells[15].Text;
                txtPOLICYFEE.Text = DGR.Items[i].Cells[16].Text;
                txtSTAMPFEE.Text = DGR.Items[i].Cells[17].Text;
                txtADJD.Text = DGR.Items[i].Cells[18].Text;
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtDISC = (TextBox)DGR.Items[i].FindControl("TXT_DISC");
                    TextBox txtCOMM = (TextBox)DGR.Items[i].FindControl("TXT_COMM");
                    TextBox txtPPN = (TextBox)DGR.Items[i].FindControl("TXT_PPN");
                    TextBox txtPPH = (TextBox)DGR.Items[i].FindControl("TXT_PPH");
                    TextBox txtADJC = (TextBox)DGR.Items[i].FindControl("TXT_ADJMIN");
                    TextBox txtPOLICYFEE = (TextBox)DGR.Items[i].FindControl("TXT_POLFEE");
                    TextBox txtSTAMPFEE = (TextBox)DGR.Items[i].FindControl("TXT_STAMPFEE");
                    TextBox txtADJD = (TextBox)DGR.Items[i].FindControl("TXT_ADJPLUS");
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                    if (cb.Checked)
                    {
                        //try
                        //{
                            conn.QueryString = "exec SP_INVOICE_MASTER_ADJUSTMENT " +
                                                "'" + DGR.Items[i].Cells[1].Text + "'," +
                                                "'" + txtDISC.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtCOMM.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtPPN.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtPPH.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtADJC.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtPOLICYFEE.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtSTAMPFEE.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtADJD.Text.Trim().Replace(",", "") + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteNonQuery();
                        //}
                        //catch { }
                    }
                }

                DGR.CurrentPageIndex = 0;
                FillDGR();
            }


            if (e.CommandName == "WriteOff")
            {
                try
                {
                    conn.QueryString = "exec SP_INVOICE_WRITEOFF " +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }

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

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }
    }
}