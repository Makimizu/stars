using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Settlement
{
    public partial class InvoiceComission : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string s = Session["s"].ToString();
            }
            catch
            {
                Response.Redirect("../Standard/FailedSession.aspx");
            }

        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (DDL_DONE.SelectedValue == "0")
                where = where + " and a.TRXID is null ";
            else
                where = where + " and a.TRXID is not null ";

            if (DDL_OUTS.SelectedValue != "")
                where = where + " and a.OUTSTANDING" + DDL_OUTS.SelectedValue + " ";

            if (TXT_DESCR.Text.Trim() != "")
                where = where + " a.DESCR like '%" + TXT_DESCR.Text.Trim() + "%' ";

            if (TXT_BENEF.Text.Trim() != "")
                where = where + " a.BENEFICIARY like '%" + TXT_BENEF.Text.Trim() + "%' ";

            if (TXT_DATE1.Text.Trim() != "")
            {
                where = where + " and convert(date,a.USERDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                where = where + " and convert(date,a.USERDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')";
            }

            conn.QueryString = "select " +
                                "INVOICENO		= INVOICENO, " +
                                "OUTSTANDING	= replace(convert(varchar(100),convert(money,OUTSTANDING),1),'.00',''), " +
                                "AMOUNT			= replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                "DESCR			= DESCR, " +
                                "BENEFICIARY	= BENEFICIARY, " +
                                "ACC_NO			= ACC_NO, " +
                                "ACC_BANK		= ACC_BANK, " +
                                "ACC_NAME		= ACC_NAME, " +
                                "REQUEST		= USERBY + '<BR>(' + convert(varchar(50),USERDATE) + ')', " +
                                "REKAPID        = a.REKAPID, " +
                                "SEQ            = a.SEQ, " +
                                "TRXID          = a.TRXID " +
                                "from V_INVOICE_COMISSION a " +
                                "where 1=1 " +
                                where +
                                "order by a.USERDATE";
            conn.ExecuteQuery();

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            conn.QueryString = "select CODE,BANK = LEFT(BANK,25) from PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btBENSAVE = (Button)DGR.Items[i].FindControl("BT_BENSAVE");
                Button btPROCEED = (Button)DGR.Items[i].FindControl("BT_PROSES");
                Label lbINVOICE = (Label)DGR.Items[i].FindControl("LB_INVOICENO");
                Label lbOUTSTANDING = (Label)DGR.Items[i].FindControl("LB_OUTS");
                TextBox txtBENEF = (TextBox)DGR.Items[i].FindControl("TXT_BENEF");
                TextBox txtACCNO = (TextBox)DGR.Items[i].FindControl("TXT_ACCNO");
                TextBox txtACCNAME = (TextBox)DGR.Items[i].FindControl("TXT_ACCNAME");
                DropDownList ddlACCBANK = (DropDownList)DGR.Items[i].FindControl("DDL_ACCBANK");

                btPROCEED.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk PROSES ?')){return false;};");

                lbINVOICE.Text = DGR.Items[i].Cells[1].Text;
                lbOUTSTANDING.Text = DGR.Items[i].Cells[2].Text;

                txtBENEF.Text = DGR.Items[i].Cells[6].Text;
                txtACCNO.Text = DGR.Items[i].Cells[7].Text;
                txtACCNAME.Text = DGR.Items[i].Cells[9].Text;


                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlACCBANK.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                try
                {
                    ddlACCBANK.SelectedValue = DGR.Items[i].Cells[8].Text;
                }
                catch { }

                if (DGR.Items[i].Cells[14].Text.Replace("&nbsp;", "") != "")
                {
                    btPROCEED.Visible = false;
                    btBENSAVE.Visible = false;
                }
                else
                {
                    if (DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "0")
                        btPROCEED.Visible = false;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            TextBox txtBENEF = (TextBox)e.Item.FindControl("TXT_BENEF");
            TextBox txtACCNO = (TextBox)e.Item.FindControl("TXT_ACCNO");
            TextBox txtACCNAME = (TextBox)e.Item.FindControl("TXT_ACCNAME");
            DropDownList ddlACCBANK = (DropDownList)e.Item.FindControl("DDL_ACCBANK");

            if (e.CommandName == "SaveBenef")
            {
                try
                {
                    conn.QueryString = "update INVOICE_COMISSION set " +
                                        "BENEFICIARY = '" + txtBENEF.Text.Trim() + "'," +
                                        "ACC_NO = '" + txtACCNO.Text.Trim() + "'," +
                                        "ACC_BANK = '" + ddlACCBANK.SelectedValue + "'," +
                                        "ACC_NAME = '" + txtACCNAME.Text.Trim() + "' " +
                                        "where " +
                                        "INVOICENO = '" + e.Item.Cells[1].Text + "' " +
                                        "and SEQ = " + e.Item.Cells[13].Text;
                    conn.ExecuteNonQuery();

                    int CurrPage = DGR.CurrentPageIndex;                    
                    FillDGR();
                    DGR.CurrentPageIndex = CurrPage;
                }
                catch { }
            }


            if (e.CommandName == "Proceed")
            {
                try
                {
                    conn.QueryString = "exec SP_COMISSION_SETTLEMENT " +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + e.Item.Cells[13].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
                catch { }
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }
    }
}