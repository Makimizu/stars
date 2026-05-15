using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Web.Services.Description;
using System.IO;

namespace AGR
{
    public partial class AGENT_DEDUCTION : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                LB_ID.Text = Request.QueryString["ID"].ToString();

                if(LB_ID.Text != "")
                {
                    DEDUC_DOC.Visible = false;
                }

                LoadRecord(LB_ID.Text);
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PARAM_REMUN_DEDUCTION where CODE not in ('CLAWBACK','TAX') order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, BANK = KODE + ' - ' + BANK from V_LINK_FINANCE_PARAM_TBL_BANK where isnull(KODE , '') <> '' and len(KODE) = 3 order by KODE";
            conn.ExecuteQuery();
            DDL_ACCBANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCBANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadRecord(string ID)
        {
            if (ID == "")
                return;

            conn.QueryString = "select " +
                                "DEDUCTION_ID, " +
                                "AGENT_CODE, " +
                                "AGENT_NAME, " +
                                "DEDUCTION_TYPE, " +
                                "REMARK, " +
                                "ACCNO, " +
                                "ACCBANK, " +
                                "ACCNAME, " +
                                "DEDUCTION_AMOUNT		= replace(convert(varchar(100), convert(money, DEDUCTION_AMOUNT), 1), '.00', ''), " +
                                "DEDUCTION_PAYMENT		= replace(convert(varchar(100), convert(money, DEDUCTION_PAYMENT), 1), '.00', ''), " +
                                "DEDUCTION_OUTSTANDING	= replace(convert(varchar(100), convert(money, DEDUCTION_OUTSTANDING), 1), '.00', ''), " +
                                "DEDUCTION_SETTLE_DATE	= convert(varchar(20), DEDUCTION_SETTLE_DATE, 106), " +
                                "DEDUCTION_SETTLE_DATE	= convert(varchar(20), DEDUCTION_SETTLE_DATE, 106), " +
                                "REQUESTBY				= REQUESTBY + ' - ' + convert(varchar(100), REQUESTDATE), " +
                                "APPROVEBY				= APPROVEBY + ' - ' + convert(varchar(100), APPROVEDATE) " +
                                "from		V_M_AGENTS_DEDUCTION a " +
                                "where DEDUCTION_ID = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            LB_AGENT_CODE.Text = conn.GetFieldValue("AGENT_CODE").ToString();
            LB_AGENT_NAME.Text = conn.GetFieldValue("AGENT_NAME").ToString() + "&nbsp;&nbsp;";
            DDL_TYPE.SelectedValue = conn.GetFieldValue("DEDUCTION_TYPE").ToString();
            TXT_AMOUNT.Text = conn.GetFieldValue("DEDUCTION_AMOUNT").ToString();
            TXT_REMARK.Text = conn.GetFieldValue("REMARK").ToString();

            TBL_ACC.Visible = true;

            TXT_ACCNO.Text = conn.GetFieldValue("ACCNO").ToString();
            TXT_ACCNAME.Text = conn.GetFieldValue("ACCNAME").ToString();
            DDL_ACCBANK.SelectedValue = conn.GetFieldValue("ACCBANK").ToString();

            TBL_PAYMENT.Visible = true;
            FillDGRSource();
            FillDGRTerm();
        }

        protected void FillDGRSource()
        {
            conn.QueryString = "exec SP_M_AGENT_DEDUCTION_PAYMENT_SOURCE '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PAYMENT_SOURCE.DataSource = dt;
            DGR_PAYMENT_SOURCE.DataBind();

            for (int i = 0; i < DGR_PAYMENT_SOURCE.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PAYMENT_SOURCE.Items[i].FindControl("CB");
                if (DGR_PAYMENT_SOURCE.Items[i].Cells[0].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void FillDGRTerm()
        {
            conn.QueryString = "exec SP_M_AGENT_DEDUCTION_PAYMENT_TERM '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PAYMENT_TERM.DataSource = dt;
            DGR_PAYMENT_TERM.DataBind();

            for (int i = 0; i < DGR_PAYMENT_TERM.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_PAYMENT_TERM.Items[i].FindControl("TXT_AMOUNT");
                Button btD = (Button)DGR_PAYMENT_TERM.Items[i].FindControl("BT_D");
                Button btX = (Button)DGR_PAYMENT_TERM.Items[i].FindControl("BT_X");

                txt.Text = DGR_PAYMENT_TERM.Items[i].Cells[2].Text;

                if (DGR_PAYMENT_TERM.Items[i].Cells[4].Text != "0")
                {
                    btX.Enabled = false;
                }
                else
                {
                    btD.Enabled = false;
                }
            }

            BT_ADD_TERM.Attributes.Add("onclick", "ShowProgress();");
        }

        protected void BT_SAVE_TERM_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);

            double TotalAmt= 0;
            double AmountFix = TXT_AMOUNT.Text.Trim() != "" ? double.Parse(TXT_AMOUNT.Text.Trim()) : 0;
            foreach (DataGridItem item in DGR_PAYMENT_TERM.Items)
            {
               // Get value from TemplateColumn TextBox (AMOUNT)
                TextBox txtAmount = (TextBox)item.FindControl("TXT_AMOUNT");
                string amount = txtAmount != null ? txtAmount.Text.Trim() : "0";
                double amtDuduction = double.Parse(amount);
                TotalAmt += amtDuduction;
            }

            if (TotalAmt > AmountFix) {
                string message = "Total payment term > dari amount yang diajukan";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ShowAlert('" + message + "');", true);
                return;
            }

            try
            {
                string usr = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
                //string qryString = $"SP_M_AGENT_DEDUCTION_PAYMENT_TERM_UPSERT '{LB_ID.Text}','{GlobalUse.GlobalDateFormat(DateTime.UtcNow.ToString().Trim(), "d/M/yyyy")}','0','{usr}','2'";
                //fixing interpolated string
                string qryString = string.Format(
                                                "SP_M_AGENT_DEDUCTION_PAYMENT_TERM_UPSERT '{0}','{1}','0','{2}','2'",
                                                LB_ID.Text,
                                                GlobalUse.GlobalDateFormat(DateTime.UtcNow.ToString().Trim(), "d/M/yyyy"),
                                                usr
                                            );

                conn.QueryString = qryString;
                conn.ExecuteQuery(100000);

                foreach (DataGridItem item in DGR_PAYMENT_TERM.Items)
                {
                    // Get values from BoundColumn (use Cells index, starting from 0)
                    string dueDate = item.Cells[0].Text;
                    string invoiceNo = item.Cells[1].Text;
                    string payment = item.Cells[3].Text; // Assuming PAYMENT is at index 3
                    string outstanding = item.Cells[4].Text; // Assuming OUTSTANDING is at index 4

                    // Get value from TemplateColumn TextBox (AMOUNT)
                    TextBox txtAmount = (TextBox)item.FindControl("TXT_AMOUNT");
                    string amount = txtAmount != null ? txtAmount.Text.Trim() : "0";   

                    conn.QueryString = "exec SP_M_AGENT_DEDUCTION_PAYMENT_TERM_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + GlobalUse.GlobalDateFormat(dueDate.ToString().Trim(), "d/M/yyyy") + "'," +
                                        "'" + amount.Replace(",", "") + "'," +
                                        "'" + usr + "'";
                    conn.ExecuteQuery(100000);
                }

                string message = "Payment term behasil disimpan";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ShowAlert('" + message + "');", true);
            }
            catch (Exception ex)
            {
                System.Threading.Thread.Sleep(3000);
                string message = "Save payment term failed : " + ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ShowAlert('" + message + "');", true);
            }            

            FillDGRTerm();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_AMOUNT.Text.Trim() == "" || TXT_REMARK.Text.Trim() == "" || LB_AGENT_CODE.Text == "")
                return;

            try
            {
                string ID, ACCNO, ACCNAME, ACCBANK;
                ACCNO = ACCBANK = ACCNAME = ID = "null";

                if (LB_ID.Text != "")
                {
                    ID = "'" + LB_ID.Text + "'";
                    ACCNO = "'" + TXT_ACCNO.Text.Trim() + "'";
                    ACCNAME = "'" + TXT_ACCNAME.Text.Trim() + "'";

                    if (DDL_ACCBANK.SelectedValue != "")
                        ACCBANK = "'" + DDL_ACCBANK.SelectedValue + "'";
                }

                conn.QueryString = "exec SP_M_AGENT_DEDUCTION_UPSERT " +
                                    ID + "," +
                                    "'" + LB_AGENT_CODE.Text + "'," +
                                    "'" + DDL_TYPE.SelectedValue + "'," +
                                    TXT_AMOUNT.Text.Trim().Replace(",", "") + "," +
                                    "'" + TXT_REMARK.Text.Trim().Replace("'", "`") + "'," +
                                    ACCNO + "," +
                                    ACCBANK + "," +
                                    ACCNAME + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                string deductionid = conn.GetFieldValue("DEDUCTION_ID").ToString();

                //add file upload deduction
                if (DEDUC_DOC.HasFile)
                {
                    try
                    {
                        // Optional: Validate file type or size
                        string fileName = Path.GetFileName(DEDUC_DOC.FileName);
                        string fullpath = Server.MapPath("~/Upload/") + Session["s"] + fileName;

                        if (File.Exists(fullpath))
                        {
                            File.Delete(fullpath);
                        }

                        // Save the file to the server
                        DEDUC_DOC.SaveAs(fullpath);

                        conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
                        conn.ExecuteQuery();
                        string code = conn.GetFieldValue(0, 0).ToString();
                        string owner2 = "007";
                        string remark = "DOCUMENT DEDUCTION";

                        string SQL = "delete from ARCHIEVE.dbo.AGR_ARSIP where OWNER1 = '" + deductionid + "' and OWNER2 = '" + owner2 + "' and TIPE='AGR_3' " +
                                    "insert into ARCHIEVE.dbo.AGR_ARSIP values (" +
                                    "'" + code + "'," +
                                    "'AGR_3'," +
                                    "'" + deductionid + "'," +
                                    "'" + owner2 + "'," +
                                    "null," +
                                    "'" + remark + "'," +
                                    "'" + fileName + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                                    "@File)";
                        GlobalUse.FileToSQL(fullpath, SQL);

                        if (File.Exists(fullpath))
                            File.Delete(fullpath);
                    }
                    catch (Exception ex)
                    {
                        // Handle the error (log it or show to user)
                        // Example: lblMessage.Text = "Error: " + ex.Message;
                    }
                }
                else
                {
                    // Example: lblMessage.Text = "Please select a file.";
                }

                //Response.Redirect("AGENT_DEDUCTION.aspx?ID=" + conn.GetFieldValue("DEDUCTION_ID").ToString());
                Response.Redirect("AGENT_DEDUCTION.aspx?ID=" + deductionid);
            }
            catch { }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            ShowAgentSearch();
        }

        protected void BT_AGENT_SEARCH_Click(object sender, EventArgs e)
        {
            ShowAgentSearch();
        }

        protected void ShowAgentSearch()
        {
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            DGR_AGENT.CurrentPageIndex = 0;
            FillDGRAgent();
        }

        protected void FillDGRAgent()
        {
            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.FULLNAME, " +
                                "a.SUBCD_DESCR, " +
                                "a.AGENCY_NAME " +
                                "from		V_M_AGENTS a " +
                                "where " +
                                "a.FULLNAME like '%" + TXT_AGENTNAME.Text.Trim() + "%' " +
                                "and a.SUBCD_DESCR like '%" + TXT_LEVEL.Text.Trim() + "%' " +
                                "and isnull(a.AGENCY_NAME, '') like '%" + TXT_AGENCY.Text.Trim() + "%' " +
                                "order by " +
                                "2,3";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AGENT.DataSource = dt;
            DGR_AGENT.DataBind();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR_AGENT.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_AGENT.Items[i].FindControl("LB_AGENT_CODE");
                lb.Text = DGR_AGENT.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_AGENT_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            DGR_AGENT.CurrentPageIndex = e.NewPageIndex;
            FillDGRAgent();
        }

        protected void DGR_AGENT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LB_AGENT_CODE.Text = e.Item.Cells[1].Text;
                LB_AGENT_NAME.Text = e.Item.Cells[2].Text + "&nbsp;&nbsp;";
            }
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PAYMENT_SOURCE.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PAYMENT_SOURCE.Items[i].FindControl("CB");
                if (cb == (CheckBox)sender)
                {
                    string taken = "0";
                    if (cb.Checked)
                        taken = "1";
                    conn.QueryString = "exec SP_M_AGENT_DEDUCTION_PAYMENT_SOURCE_INSDEL " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DGR_PAYMENT_SOURCE.Items[i].Cells[1].Text + "'," +
                                        taken + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    FillDGRSource();
                    return;
                }
            }


        }

        protected void BT_ADD_TERM_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            try
            {
                conn.QueryString = "exec SP_M_AGENT_DEDUCTION_PAYMENT_TERM_UPSERT " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DUEDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + TXT_AMOUNT_INSERT.Text.Trim().Replace(",", "") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery(100000);
            }
            catch (Exception ex)
            {
                //MessageBox(this, ex.Message);
                System.Threading.Thread.Sleep(3000);
                string message = ex.Message.Contains("Total payment term > dari amount yang diajukan") ? "Total payment term > dari amount yang diajukan" : ex.Message;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ShowAlert('" + message + "');", true);
            }

            FillDGRTerm();
        }

        public void MessageBox(System.Web.UI.Page page, string strMsg)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);

        }

        protected void DGR_PAYMENT_TERM_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    string usr = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
                    //string qryString = $"SP_M_AGENT_DEDUCTION_PAYMENT_TERM_UPSERT '{LB_ID.Text}','{e.Item.Cells[0].Text}','0','{usr}','1'";
                    //fixing interpolated string in visual studio 2012
                    string qryString = string.Format(
                                                    "SP_M_AGENT_DEDUCTION_PAYMENT_TERM_UPSERT '{0}','{1}','0','{2}','1'",
                                                    LB_ID.Text,
                                                    e.Item.Cells[0].Text,
                                                    usr
                                                );

                    conn.QueryString = qryString;
                    //conn.QueryString = "exec SP_M_AGENT_DEDUCTION_PAYMENT_TERM_UPSERT " +
                    //                    "'" + LB_ID.Text + "'," +
                    //                    "'" + e.Item.Cells[0].Text + "'," +
                    //                    "0," +
                    //                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(100000);
                }
                catch { }

                FillDGRTerm();
            }
        }
    }
}