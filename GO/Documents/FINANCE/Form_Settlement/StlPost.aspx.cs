using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Settlement
{
    public partial class StlPost : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DEFAULTDAY = convert(varchar(20),dateadd(day,-1,GETDATE()),103)";
            conn.ExecuteQuery();
            TXT_DATE1.Text = conn.GetFieldValue("DEFAULTDAY").ToString();
            TXT_DATE2.Text = conn.GetFieldValue("DEFAULTDAY").ToString();

            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            DDL_APP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select distinct " +
                                "a.NOREK_SOURCE, " +
                                "b.BANK " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join REKENING_MASTER b on a.NOREK_SOURCE=b.NOREK " +
                                "where a.NOREK_SOURCE is not null " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCSOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLTipe();


            BT_DETAILCLOSE.Attributes.Add("onclick", "document.getElementById('pnlpopup').style.display = 'none';");
        }

        protected void FillDDLTipe()
        {
            DDL_TIPE.Items.Clear();
            conn.QueryString = "select CODE,DESCR from PARAM_TIPE_SETTLEMENT where APP_ID = '" + DDL_APP.SelectedValue + "' order by 2";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
            DGR.CurrentPageIndex = 0;
            FillDGR();
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

            if (DDL_APP.SelectedValue != "")
            {
                where = where + " and a.APP_ID='" + DDL_APP.SelectedValue + "'";
            }

            if (DDL_ACCSOURCE.SelectedValue != "")
            {
                where = where + " and a.ACC_SOURCE='" + DDL_ACCSOURCE.SelectedValue + "'";
            }

            if (DDL_BANKCONN.SelectedValue != "")
            {
                where = where + " " + DDL_BANKCONN.SelectedValue + " ";
            }

            if (DDL_TIPE.SelectedValue != "")
            {
                where = where + " and a.TIPE_SETTLEMENT='" + DDL_TIPE.SelectedValue + "'";
            }

            if (TXT_ID.Text.Trim() != "")
            {
                where = where + " and a.REKAPID='" + TXT_ID.Text.Trim() + "'";
            }

            if (TXT_DATE1.Text.Trim() != "")
            {
                where = where + " and convert(date,a.APPROVALDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                where = where + " and convert(date,a.APPROVALDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_BENEF.Text.Trim() != "")
            {
                where = where + " and a.BENEFICIARY like '%" + TXT_BENEF.Text.Trim() + "%'";
            }

            conn.QueryString = "select " +
                                "a.REKAPID, " +
                                "a.ACC_NO, " +
                                "a.ACC_BANK, " +
                                "a.ACC_NAME, " +
                                "a.BENEFICIARY, " +
                                "a.TIPE_SETTLEMENT_DESCR, " +
                                "APPROVALBY = UPPER(a.APPROVALBY), " +
                                "APPROVALDATE = convert(varchar(20),a.APPROVALDATE,106), " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
                                "CHARGE = replace(convert(varchar(100),convert(money,isnull(b.CHARGE,0)),1),'.00',''), " +
                                "TOTAL = replace(convert(varchar(100),convert(money,a.AMOUNT + isnull(b.CHARGE,0)),1),'.00',''), " +
                                "a.CNT, " +
                                "b.PAYMENT_METHOD " +
                                "from V_SETTLEMENT_MASTER_REKAP a " +
                                "left join SETTLEMENT_MASTER_BANK_CHARGE b on a.REKAPID = b.REKAPID and a.ACC_NO = b.ACC_NO and a.ACC_NAME = b.ACC_NAME collate database_default and a.ACC_BANK_CODE = b.ACC_BANK collate database_default " +
                                "where " +
                                "ACC_SOURCE is not null " + where +
                                "order by " +
                                "a.APPROVALDATE, " +
                                "a.REKAPID";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = conn.GetRowCount().ToString();

            conn.QueryString = "select " +
                                "TOTAL = replace(convert(varchar(100),convert(money,SUM(a.AMOUNT + isnull(b.CHARGE,0))),1),'.00','') " +
                                "from V_SETTLEMENT_MASTER_REKAP a " +
                                "left join SETTLEMENT_MASTER_BANK_CHARGE b on a.REKAPID = b.REKAPID and a.ACC_NO = b.ACC_NO and a.ACC_NAME = b.ACC_NAME collate database_default and a.ACC_BANK_CODE = b.ACC_BANK collate database_default " +
                                "where " +
                                "ACC_SOURCE is not null " + where;
            conn.ExecuteQuery();

            LB_RESULT.Text = "<TABLE style='border-spacing:0px;'>" +
                                "<TR><TD style='width:100px;'>Total Records</TD><TD>:</TD><TD>" + LB_RESULT.Text + "</TD></TR>" +
                                "<TR><TD>Total Amount</TD><TD>:</TD><TD>" + conn.GetFieldValue("TOTAL").ToString() + "</TD></TR>" +
                                "</TABLE>";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");
                Button btIB = (Button)DGR.Items[i].FindControl("BT_FILE");
                Button btTL = (Button)DGR.Items[i].FindControl("BT_SLIP");
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                TextBox txtCHARGE = (TextBox)DGR.Items[i].FindControl("TXT_CHARGE");

                lbREKAPID.Text = DGR.Items[i].Cells[2].Text;
                txtCHARGE.Text = DGR.Items[i].Cells[4].Text;

                btTL.Visible = false;
                if (DGR.Items[i].Cells[3].Text == "TL")
                {
                    cb.Visible = false;
                    btTL.Visible = true;
                }

                lbREKAPID.Attributes.Add("onclick", "displayFloatingDiv('divDETAIL', 'DETAIL', 400, 200, 100, 50);");
                //lbREKAPID.Attributes.Add("onclick", "window.open('StlPostDetail.aspx','DETAIL','height=300px,width=500px,left=0,top=0,border=0,resizable=no,status=no,toolbar=no,scrollbars=no,fullscreen = { yes | no | 1 | 0 },menubar=no,location=no,dependent=yes');");
                //lbREKAPID.Attributes.Add("onclick", "<script language='javascript'>window.showModalDialog('StlPostDetail.aspx',dialogWidth:400px;dialogHeight: 200px;center: yes;resize: no;status: no;help:no');</Script>");
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DataGrid dgrDETAIL = (DataGrid)e.Item.FindControl("DGR_DETAIL");
            Button btDETAIL = (Button)e.Item.FindControl("BT_DETAIL");
            Button btFILE = (Button)e.Item.FindControl("BT_FILE");
            Button btSLIP = (Button)e.Item.FindControl("BT_SLIP");


            if (e.CommandName == "Settle")
            {
                Response.Redirect("Payment_Settle.aspx?MODE=PAY&CODE=" + e.Item.Cells[2].Text);
            }

            if (e.CommandName == "IBALL")
            {
                ToCSVALL();
            }

            if (e.CommandName == "IB")
            {
                /*
                if (e.Item.Cells[15].Text.Replace("&nbsp;", "") != "")
                    Response.Redirect(e.Item.Cells[15].Text.Replace("&nbsp;", ""));
                else
                    ToCSV(e.Item.Cells[2].Text);
                */
            }

            if (e.CommandName == "TL")
            {

            }

            if (e.CommandName == "Detail")
            {
                ShowDetail(e.Item.Cells[2].Text, e.Item.Cells[6].Text, e.Item.Cells[7].Text, e.Item.Cells[8].Text, e.Item.Cells[10].Text, 0);
            }

            if (e.CommandName == "SaveCharge")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtCHARGE = (TextBox)DGR.Items[i].FindControl("TXT_CHARGE");

                    try
                    {
                        conn.QueryString = "update SETTLEMENT_MASTER_BANK_CHARGE set " +
                                            "CHARGE = " + txtCHARGE.Text.Trim().Replace(",", "") + " " +
                                            "where " +
                                            "REKAPID = '" + DGR.Items[i].Cells[2].Text + "' " +
                                            "and ACC_NO = '" + DGR.Items[i].Cells[6].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }                    
                }
                FillDGR();
            }
        }

        protected void ShowDetail(string rekapid, string accno, string accname, string accbank, string type, int pageindex)
        {
            LB_DOCNO.Text = rekapid;
            LB_ACCNO.Text = accno;
            LB_ACCNAME.Text = accname;
            LB_ACCBANK.Text = accbank;
            LB_TRANSTYPE.Text = type;

            DGR_DETAIL.CurrentPageIndex = pageindex;
            FillDGRDetail();

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void FillDGRDetail()
        {
            conn.QueryString = "select " +
                                    "NBR = ROW_NUMBER() over (order by a.USERDATE), " +
                                    "a.DOCNO, " +
                                    "a.RESERVED_PARAM1, " +
                                    "a.DESCR, " +
                                    "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00','') " +
                                    "from SETTLEMENT_DETAIL a " +
                                    "where " +
                                    "a.REKAPID = '" + LB_DOCNO.Text + "' " +
                                    "and a.ACC_NO = '" + LB_ACCNO.Text + "' " +
                                    "order by a.USERDATE";
            conn.ExecuteQuery();
            DGR_DETAIL.DataSource = conn.GetDataTable().Copy();
            DGR_DETAIL.DataBind();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_ACCSOURCE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_BANKCONN_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_BANKCONN_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_TIPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Visible)
                    cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DGR_DETAIL_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ShowDetail(LB_DOCNO.Text, LB_ACCNO.Text, LB_ACCNAME.Text, LB_ACCBANK.Text, LB_TRANSTYPE.Text, e.NewPageIndex);
        }

        protected void ToCSVALL()
        {
            string 
            REKAPID = "", 
            ACC_SOURCE = DDL_ACCSOURCE.SelectedValue;

            bool bFound = false;

            var result = new StringBuilder();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    REKAPID = REKAPID + DGR.Items[i].Cells[2].Text + DGR.Items[i].Cells[6].Text + ",";
                    bFound = true;
                }
            }

            if (!bFound)
                return;

            conn.QueryString = "exec RPT_INTERNET_BANKING_HEX '" + REKAPID + "','" + ACC_SOURCE + "'";
            conn.ExecuteQuery();

            foreach (DataRow row in conn.GetDataTable().Copy().Rows)
            {
                for (int i = 0; i < conn.GetDataTable().Copy().Columns.Count; i++)
                {
                    var rowValue = row[i].ToString();
                    result.Append(rowValue.Replace("\\r\\n", "\r\n"));
                    result.Append(i == conn.GetDataTable().Copy().Columns.Count - 1 ? "\r\n" : ",");
                }
            }

            for (int j = 0; j < DGR.Items.Count; j++)
            {
                CheckBox cb = (CheckBox)DGR.Items[j].FindControl("CB");
                if (cb.Checked)
                {
                    conn.QueryString = "exec RPT_INTERNET_BANKING_HDEX '" + DGR.Items[j].Cells[2].Text + DGR.Items[j].Cells[6].Text + "','" + DDL_ACCSOURCE.SelectedValue + "','D'";
                    conn.ExecuteQuery();

                    foreach (DataRow row in conn.GetDataTable().Copy().Rows)
                    {
                        for (int i = 0; i < conn.GetDataTable().Copy().Columns.Count; i++)
                        {
                            var rowValue = row[i].ToString();
                            result.Append(rowValue);
                            result.Append(i == conn.GetDataTable().Copy().Columns.Count - 1 ? "\r\n" : ",");
                        }
                    }
                }
            }

            if (result != null)
            {
                conn.QueryString = "select convert(varchar(20),GETDATE(),112)";
                conn.ExecuteQuery();

                this.Response.Clear();
                this.Response.Buffer = true;

                this.Response.AddHeader("content-disposition", "attachment;filename=" + DDL_ACCSOURCE.SelectedItem.Text.Replace(" ", "") + "_" + conn.GetFieldValue(0, 0).ToString() + "_" + DDL_BANKCONN.SelectedItem.Text + ".csv");
                this.Response.Charset = "";
                this.Response.ContentType = "application/text";
                this.Response.Output.Write(result.ToString());
                this.Response.Flush();
                this.Response.End();
            }
        }
    }
}