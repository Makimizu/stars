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
    public partial class StlDone : System.Web.UI.Page
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
                    LB_APP.Text = Request.QueryString["APPID"];
                }
                catch { }
                try
                {
                    LB_TIPE.Text = Request.QueryString["TIPE"];
                }
                catch { }

                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
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
            //DDL_ACCSOURCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCSOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (LB_APP.Text != "")
            {
                DDL_APP.SelectedValue = LB_APP.Text;
                DDL_APP.Enabled = false;
            }

            FillDDLTipe();

            if (LB_TIPE.Text != "")
            {
                DDL_TIPE.SelectedValue = LB_TIPE.Text;
                DDL_TIPE.Enabled = false;
            }
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
                where = where + " and a.NOREK_SOURCE='" + DDL_ACCSOURCE.SelectedValue + "'";
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

            if (TXT_DESCR.Text.Trim() != "")
            {
                where = where + " and a.DESCR like '%" + TXT_DESCR.Text.Trim() + "%'";
            }

            if (TXT_DATE1.Text.Trim() != "")
            {
                where = where + " and convert(date,a.PROCESSDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                where = where + " and convert(date,a.PROCESSDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')";
            }

            conn.QueryString = "select " +
                                "a.REKAPID, " +
                                "DESCR, " +
                                "NOREK_SOURCE_DESCR, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                "CHARGE = replace(convert(varchar(100),convert(money,CHARGE),1),'.00',''), " +
                                "TOTAL = replace(convert(varchar(100),convert(money,AMOUNT + CHARGE),1),'.00',''), " +
                                "CNT," +
                                "IB," +
                                "TL," +
                                "BANK_SOURCE_CODE, " +
                                "POSTFILE_URL, " +
                                "DESTINATION_BANK_DESCR, " +                                
                                "PROCESS = PROCESSBY +  ' (' + convert(varchar(50),PROCESSDATE) + ')', " +
                                "URL_DONE " +
                                "from V_SETTLEMENT_MASTER a " +
                                "inner join (select a.REKAPID from SETTLEMENT_MASTER_BANK_CHARGE a " +
			                    "           left join REKENING_JURNAL_CREDIT d on a.REKAPID=d.REKAPID and a.ACC_NO=d.ACC_NO and a.ACC_BANK=d.ACC_BANK and a.ACC_NAME=d.ACC_NAME " +
                                "           where d.REKAPID is null group by a.REKAPID) b on a.REKAPID=b.REKAPID " +
                                "where " +
                                "a.PROCESSBY is not null " + where +
                                "order by a.PROCESSDATE desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = conn.GetRowCount().ToString();

            conn.QueryString = "select " +
                                "TOTAL = replace(convert(varchar(100),convert(money,SUM(AMOUNT + CHARGE)),1),'.00','') " +
                                "from V_SETTLEMENT_MASTER a " +
                                "inner join (select a.REKAPID from SETTLEMENT_MASTER_BANK_CHARGE a " +
                                "           left join REKENING_JURNAL_CREDIT d on a.REKAPID=d.REKAPID and a.ACC_NO=d.ACC_NO and a.ACC_BANK=d.ACC_BANK and a.ACC_NAME=d.ACC_NAME " +
                                "           where d.REKAPID is null group by a.REKAPID) b on a.REKAPID=b.REKAPID " +
                                "where " +
                                "a.PROCESSBY is not null " + where;
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

                lbREKAPID.Text = DGR.Items[i].Cells[2].Text;
                lbREKAPID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[11].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=850px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[12].Text == "0")
                    btIB.Visible = false;
                if (DGR.Items[i].Cells[13].Text == "0")
                    btTL.Visible = false;
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
                if (e.Item.Cells[15].Text.Replace("&nbsp;", "") != "")
                    Response.Redirect(e.Item.Cells[15].Text.Replace("&nbsp;", ""));
                else
                    ToCSV(e.Item.Cells[2].Text);

            }

            if (e.CommandName == "TL")
            {
                
            }

            if (e.CommandName == "Detail")
            {   

                if (!dgrDETAIL.Visible)
                {
                    btDETAIL.Text = "SAVE";
                    btFILE.Visible = false;
                    btSLIP.Visible = false;

                    conn.QueryString = "select " +
                                        "ACC_NO, " +
                                        "BANK, " +
                                        "ACC_NAME, " +
                                        "CHARGE = replace(convert(varchar(100),convert(money,CHARGE),1),'.00',''), " +
                                        "PAYMENT_METHOD " +
                                        "from V_INTERNET_BANKING " +
                                        "where " +
                                        "REKAPID = '" + e.Item.Cells[2].Text + "'";

                    /*
                    conn.QueryString = "select " +
                                        "a.ACC_NO, " +
                                        "b.BANK, " +
                                        "a.ACC_NAME, " +
                                        "CHARGE = replace(convert(varchar(100),convert(money,a.CHARGE),1),'.00',''), " +
                                        "a.PAYMENT_METHOD " +
                                        "from SETTLEMENT_MASTER_BANK_CHARGE a " +
                                        "inner join PARAM_TBL_BANK b on a.ACC_BANK=b.CODE collate database_default " +
                                        "left join REKENING_JURNAL_CREDIT d on a.REKAPID=d.REKAPID and a.ACC_NO=d.ACC_NO and a.ACC_BANK=d.ACC_BANK and a.ACC_NAME=d.ACC_NAME " +
                                        "where a.REKAPID = '" + e.Item.Cells[2].Text + "' and d.REKAPID is null " +
                                        "order by b.BANK, a.ACC_NAME";
                    */
                    conn.ExecuteQuery();                    

                    if (conn.GetRowCount() > 0)
                    {
                        dgrDETAIL.Visible = true;
                        DataTable dt;
                        dt = new DataTable();
                        dt = conn.GetDataTable().Copy();
                        dgrDETAIL.DataSource = dt;
                        dgrDETAIL.DataBind();

                        conn.QueryString = "select CODE,DESCR from PR_PAYMENT_METHOD";
                        conn.ExecuteQuery();

                        for (int i = 0; i < dgrDETAIL.Items.Count; i++)
                        {
                            DropDownList ddl = (DropDownList)dgrDETAIL.Items[i].FindControl("DDL_MODE");
                            TextBox txt = (TextBox)dgrDETAIL.Items[i].FindControl("TXT_CHARGE");
                            for (int j = 0; j < conn.GetRowCount(); j++)
                            {
                                ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                                try
                                {
                                    ddl.SelectedValue = dgrDETAIL.Items[i].Cells[3].Text;
                                }
                                catch { }

                                txt.Text = dgrDETAIL.Items[i].Cells[4].Text.Trim().Replace("&nbsp;","");
                            }
                        }
                    }
                }
                else
                {
                    SaveDGRDetail(e.Item.Cells[2].Text, dgrDETAIL);
                    FillDGR();
                }
            }
        }

        protected void SaveDGRDetail(string REKAPID, DataGrid dgr)
        {
            for (int i = 0; i < dgr.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)dgr.Items[i].FindControl("DDL_MODE");
                TextBox txt = (TextBox)dgr.Items[i].FindControl("TXT_CHARGE");

                string charge = txt.Text.Trim().Replace(",", "");
                if (charge == "")
                    charge = "0";

                conn.QueryString = "exec SP_SETTLEMENT_MASTER_BANK_CHARGE_UPDATE " +
                                    "'" + REKAPID + "'," +
                                    "'" + dgr.Items[i].Cells[0].Text + "'," +
                                    "'" + dgr.Items[i].Cells[1].Text + "'," +
                                    "'" + ddl.SelectedValue + "'," +
                                    "'" + charge + "'";
                conn.ExecuteNonQuery();
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void ToCSV(string REKAPID)
        {
            var result = new StringBuilder();

            conn.QueryString = "exec RPT_INTERNET_BANKING_HD '" + REKAPID + "','H'";
            conn.ExecuteQuery();

            foreach (DataRow row in conn.GetDataTable().Copy().Rows)
            {
                for (int i = 0; i < conn.GetDataTable().Copy().Columns.Count; i++)
                {
                    //var rowValue = row[i].ToString().Contains(",") ? string.Format("\"{0}\"", row[i].ToString()) : row[i].ToString();
                    var rowValue = row[i].ToString();
                    result.Append(rowValue.Replace("\\r\\n","\r\n"));
                    result.Append(i == conn.GetDataTable().Copy().Columns.Count - 1 ? "\r\n" : ",");
                }
            }

            conn.QueryString = "exec RPT_INTERNET_BANKING_HD '" + REKAPID + "','D'";
            conn.ExecuteQuery();

            foreach (DataRow row in conn.GetDataTable().Copy().Rows)
            {
                for (int i = 0; i < conn.GetDataTable().Copy().Columns.Count; i++)
                {
                    //var rowValue = row[i].ToString().Contains(",") ? string.Format("\"{0}\"", row[i].ToString()) : row[i].ToString();
                    var rowValue = row[i].ToString();
                    result.Append(rowValue);
                    result.Append(i == conn.GetDataTable().Copy().Columns.Count - 1 ? "\r\n" : ",");
                }
            }

            if (result != null)
            {
                this.Response.Clear();
                this.Response.Buffer = true;

                this.Response.AddHeader("content-disposition", "attachment;filename=" + REKAPID + ".csv");
                this.Response.Charset = "";
                this.Response.ContentType = "application/text";
                this.Response.Output.Write(result.ToString());
                this.Response.Flush();
                this.Response.End();
            }
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

        protected void ToCSVALL()
        {
            string REKAPID = "", BANKCODE = "";
            bool bFound = false;
            
            var result = new StringBuilder();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                BANKCODE = DGR.Items[i].Cells[14].Text;

                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    REKAPID = REKAPID + DGR.Items[i].Cells[2].Text + ",";
                    bFound = true;
                }
            }

            if (!bFound)
                return;

            conn.QueryString = "exec RPT_INTERNET_BANKING_H '" + REKAPID + "','" +BANKCODE+ "'";
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
                    conn.QueryString = "exec RPT_INTERNET_BANKING_HD '" + DGR.Items[j].Cells[2].Text + "','D'";
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

                this.Response.AddHeader("content-disposition", "attachment;filename=" + DDL_ACCSOURCE.SelectedItem.Text.Replace(" ","") + "_" + conn.GetFieldValue(0,0).ToString() + "_" + DDL_BANKCONN.SelectedItem.Text + ".csv");
                this.Response.Charset = "";
                this.Response.ContentType = "application/text";
                this.Response.Output.Write(result.ToString());
                this.Response.Flush();
                this.Response.End();
            }
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
    }
}