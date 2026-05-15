using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class GPA_ApprovalList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Setup()
        {
            BT_APPROVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk APPROVE ?')){return false;};");

            conn.QueryString = "select CODE,DESCR from PR_TIPE_ENDORSEMENT";
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

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "All")
                {
                    for (int i = 0; i < DGR.Items.Count; i++)
                    {
                        CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                        cb.Checked = true;
                    }
                }

                if (e.CommandName == "GOTO")
                {
                    DropDownList DDL_GOTO = (DropDownList)e.Item.FindControl("DDL_GOTO");
                    TextBox TXT_REMARK = (TextBox)e.Item.FindControl("TXT_REMARK");
                    conn.QueryString = "exec SP_GPA_TRACK_GOTO_PROSES " +
                                        "@BATCH_ID='" + e.Item.Cells[1].Text + "'," +
                                        "@NEXT_TRACK= '" + DDL_GOTO.SelectedValue + "', " +
                                        "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery();

                    conn.QueryString = "insert into REMARK select " +
                                        "NEWID()," +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'GPA'," +
                                        "'000'," +
                                        "'" + TXT_REMARK.Text.Trim().Replace("'", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()";
                    conn.ExecuteQuery();
                    FillDGR();
                }
            }
            catch (Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void FillDGR()
        {
            BT_APPROVE.Visible = false;
            string where = "";

            if (TXT_ID.Text.Trim() != "")
                where = where + "and BATCH_ID='" + TXT_ID.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + "and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_DOCNO.Text.Trim() != "")
                where = where + "and DOCNO like '%" + TXT_DOCNO.Text.Trim() + "%' ";

            if (TXT_POLICY_NO.Text.Trim() != "")
                where = where + "and POLICY_NO like '%" + TXT_POLICY_NO.Text.Trim() + "%' ";

            if (DDL_TIPE.SelectedValue != "")
                where = where + "and TIPE_ENDORS='" + DDL_TIPE.SelectedValue + "' ";

            if (TXT_DATE1.Text.Trim() != "" || TXT_DATE2.Text.Trim() != "")
            {
                string date1 = "1 jan 1980";
                string date2 = "31 dec 2030";
                if (TXT_DATE1.Text.Trim() != "")
                    date1 = GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy");
                if (TXT_DATE2.Text.Trim() != "")
                    date2 = GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy");

                where = where + "and (convert(date,PROS_END) between convert(date,'" + date1 + "') and convert(date,'" + date2 + "')) ";
            }

            conn.QueryString = "select  " +
                                "BATCH_ID, " +
                                "TIPE_ENDORS, " +
                                "TIPE_ENDORS_DESCR, " +
                                "POLICY_NO, " +
                                "DOCNO, " +
                                "DOC_SOURCE_DESCR, " +
                                "COMPANY_NAME, " +
                                "VER_END, " +
                                "REPORT_URL, " +
                                "POLICY_PERIOD_ID " +
                                "from V_GPA_ENDORSEMENT_BATCH " +
                                "where " +
                                "VER_END is not null and APRV_START is null " +
                                where +
                                "order by PROS_END";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
                BT_APPROVE.Visible = true;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button BT_PREVIEW = (Button)DGR.Items[i].FindControl("BT_SELECT");
                Button BT_GOTO = (Button)DGR.Items[i].FindControl("BT_GOTO");
                DropDownList DDL_GOTO = (DropDownList)DGR.Items[i].FindControl("DDL_GOTO");

                CheckBox CB = (CheckBox)DGR.Items[i].FindControl("CB");
                Label lbBATCHID = (Label)DGR.Items[i].FindControl("LB_BATCH_ID");
                Label lbTIPE = (Label)DGR.Items[i].FindControl("LB_TIPE");
                Label lbTGLVER = (Label)DGR.Items[i].FindControl("LB_TGLVER");
                Label lbDOCNO = (Label)DGR.Items[i].FindControl("LB_DOCNO");
                Label lbDOCSRC = (Label)DGR.Items[i].FindControl("LB_DOCSRC");
                Label lbPOLNO = (Label)DGR.Items[i].FindControl("LB_POLNO");
                Label lbCOMPANY = (Label)DGR.Items[i].FindControl("LB_COMPANY");
                TextBox TXT_REMARK = (TextBox)DGR.Items[i].FindControl("TXT_REMARK");

                BT_PREVIEW.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[9].Text + "','GPA_ENDORSEMENT','height=450,width=900,left=0,top=0,status=no,toolbar=no,scrollbars=auto,titlebar=no,menubar=no,location=no,dependent=yes');");
                BT_GOTO.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk PROSES ?')){return false;};");

                conn.QueryString = "exec SP_GPA_TRACK_GOTO_LIST @BATCH_ID = '" + DGR.Items[i].Cells[1].Text + "' ";
                conn.ExecuteQuery();
                for (int ii = 0; ii < conn.GetRowCount(); ii++)
                    DDL_GOTO.Items.Add(new ListItem(conn.GetFieldValue(ii, 1).ToString(), conn.GetFieldValue(ii, 0).ToString()));


                lbBATCHID.Text = DGR.Items[i].Cells[1].Text;
                lbTIPE.Text = DGR.Items[i].Cells[3].Text;
                lbTGLVER.Text = DGR.Items[i].Cells[8].Text;
                lbDOCNO.Text = DGR.Items[i].Cells[4].Text;
                lbDOCSRC.Text = DGR.Items[i].Cells[5].Text;
                lbPOLNO.Text = DGR.Items[i].Cells[6].Text;
                lbCOMPANY.Text = DGR.Items[i].Cells[7].Text;

                if (DGR.Items[i].Cells[2].Text == "CAN")
                {
                    conn.QueryString = "exec SP_CHECK_PAYMENT_OUTSTANDING @BATCH_ID = '" + DGR.Items[i].Cells[1].Text + "' ";
                    conn.ExecuteQuery(1200);

                    if (conn.GetRowCount() > 0)
                    {
                        TXT_REMARK.Text = conn.GetFieldValue(0, 0).ToString();
                        CB.Enabled = false;
                    }
                }

                if (DGR.Items[i].Cells[2].Text == "DEL")
                {
                    conn.QueryString = "select REGNO from PESERTA_KELUAR_TEMP where BATCH_ID = '" + DGR.Items[i].Cells[1].Text + "' and TGL_KELUAR > GETDATE()";
                    conn.ExecuteQuery(1200);

                    if (conn.GetRowCount() > 0)
                    {
                        TXT_REMARK.Text = "BATCH BELUM BISA DI-APPROVE KARENA ADA REGNO YANG MEMILIKI TGL KELUAR LEBIH BESAR DARI HARI INI";
                        CB.Enabled = false;
                        DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                    }
                }

                if (DGR.Items[i].Cells[2].Text == "DEL" || DGR.Items[i].Cells[2].Text == "CAN" || DGR.Items[i].Cells[2].Text == "CHG")
                {
                    conn.QueryString = "select a.POLICY_NO from V_PESERTA_ENDORSEMENT a " +
                                       "inner join BRANCH_BANK_ACCOUNT b on a.BRANCH_CODE = b.BRANCH_CODE and b.TIPE = 2 " +
                                       "where a.BATCH_ID = '" + DGR.Items[i].Cells[1].Text + "'";
                    conn.ExecuteQuery(1200);

                    if (conn.GetRowCount() == 0)
                    {
                        TXT_REMARK.Text = "BATCH BELUM BISA DI-APPROVE KARENA PERUSAHAAN BELUM MEMILIKI NO REKENING REFUND";
                        CB.Enabled = false;
                        DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                    }
                }
            }
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            BT_APPROVE.Visible = false;
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "exec SP_GPA_APPROVE '" + DGR.Items[i].Cells[1].Text + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteQuery(1200);

                        //yulia added 2023/05/10 for send email cancelation
                        if (DGR.Items[i].Cells[2].Text == "CAN")
                        {
                            conn.QueryString = "SELECT " +
                                                "APPROVE_DATE, " +
                                                //"EMAIL_PIC " +
                                                "COMPANY_EMAIL " +
                                                "FROM V_GPA_ENDORSEMENT_BATCH_CANCELATION " +
                                                "WHERE BATCH_ID = '" + DGR.Items[i].Cells[1].Text + "' " +
                                                "AND TOTAL_AMOUNT_NOTA_CREDIT > 0";
                            conn.ExecuteQuery();
                            int sa = conn.GetRowCount();
                            if (conn.GetRowCount() > 0)
                            {
                                string ApproveDate = GlobalUse.GlobalDateFormat(conn.GetFieldValue(0, 0).ToString(), "yyyy-M-d");
                                string EmailPic = conn.GetFieldValue(0, 1).ToString();

                                if (EmailPic == "")
                                    return;

                                LB_ERROR.Text = GlobalUse.SendEmailSQL(System.Configuration.ConfigurationManager.AppSettings["appid"], "27", DGR.Items[i].Cells[1].Text, ApproveDate, EmailPic);

                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERROR.Text = LB_ERROR.Text + "- " + ex.Message + "<BR>";
                    }
                }
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}