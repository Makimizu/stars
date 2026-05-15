using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Bank
{
    public partial class FlagRK_Split : System.Web.UI.Page
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

                LB_TRXID.Text = Request.QueryString["TRXID"];
                Setup();
            }
        }

        protected void FillDGRInfo()
        {
            conn.QueryString = "SELECT " +
                                "NOREK, " +
                                "BOOK_NAME, " +
                                "DESCR, " +
                                "POST_DATE = convert(varchar(20),a.POST_DATE,106), " +
                                "FLAG_DATE = convert(varchar(20),a.FLAG_DATE,106), " +
                                "DEBET = replace(convert(varchar(100),convert(money,a.DEBET),1),'.00',''), " +
                                "CREDIT = replace(convert(varchar(100),convert(money,a.CREDIT),1),'.00','') " +
                                "FROM V_REKENING_JURNAL a " +
                                "WHERE TRXID = '" + LB_TRXID.Text + "'";
            conn.ExecuteQuery();

            LB_NOREK.Text = conn.GetFieldValue("NOREK").ToString();
            LB_BOOK_NAME.Text = conn.GetFieldValue("BOOK_NAME").ToString();
            LB_POST_DATE.Text = conn.GetFieldValue("POST_DATE").ToString();
            LB_FLAG_DATE.Text = conn.GetFieldValue("FLAG_DATE").ToString();
            LB_DEBET.Text = conn.GetFieldValue("DEBET").ToString();
            LB_CREDIT.Text = conn.GetFieldValue("CREDIT").ToString();
            LB_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
        }

        protected void Setup()
        {
            FillDGRInfo();
            FillDDLCode();
            FillDDLCOA();
            FillDDLT00();
            JournalDetailSubmit();
            FillDGRDETAIL();

        }

        protected void FillDDLCode()
        {
            conn.QueryString = "SELECT B.CODE, " +
                               "C.DESCR " +
                               "FROM REKENING_JURNAL A " +
                               "INNER JOIN PARAM_GL_JOURNAL_BREAKDOWN_USAGE B ON A.NOREK = B.NOREK AND " +
                                                 "(CASE WHEN A.DEBET > 0 THEN 'D' ELSE 'C' END) = B.DC " +
                               "INNER JOIN dbo.PARAM_GL_JOURNAL C ON B.CODE = C.CODE " +
                               "AND A.TRXID = '" + LB_TRXID.Text + "'";
            conn.ExecuteQuery();
            DDL_CODE.Items.Clear();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CODE.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString() + " - " + conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDDLCOA()
        {
            conn.QueryString = "SELECT " +
                               "A.COA, " +
                               "B.DESCR " +
                               "FROM dbo.PARAM_GL_JOURNAL_DETAIL A " +
                               "INNER JOIN dbo.PARAM_GL_COA B ON B.COA = A.COA " +
                               "WHERE A.CODE = '" + DDL_CODE.SelectedValue + "' " +
                               "AND A.DC = '" + DDL_DC.SelectedValue + "'";
            conn.ExecuteQuery();
            DDL_COA.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_COA.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString() + " - " + conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDDLT00()
        {
            conn.QueryString = "SELECT '' AS CODE,'' AS DESCR " +
                               "UNION ALL " +
                               "SELECT CODE, DESCR FROM PR_TCODE_T00 " +
                               "ORDER BY 2";
            conn.ExecuteQuery();
            DDL_T00.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_T00.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString() + " - " + conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGRDETAIL()
        {
            conn.QueryString = "select " +
                               "a.TRXID, " +
                               "a.CODE, " +
                               "a.COA, " +
                               "a.DC, " +
                               "DESCR, " +
                               "USERDATE = convert(varchar(20),a.USERDATE,106), " +
                               "DEBET = replace(convert(varchar(100),convert(money,a.DEBET),1),'.00',''), " +
                               "CREDIT = replace(convert(varchar(100),convert(money,a.CREDIT),1),'.00',''), " +
                               "IS_CUSTOMIZE, " +
                               "T00 " +
                               "from dbo.V_REKENING_JURNAL_FLAG_BREAKDOWN a " +
                               "WHERE a.TRXID='" + LB_TRXID.Text + "' " +
                               "ORDER BY a.DC DESC, a.USERDATE";
            conn.ExecuteQuery();

            string records = conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DETAIL.DataSource = dt;

            conn.QueryString = "select " +
                               "TOTAL_DEBET = replace(convert(varchar(100),convert(money,sum(a.DEBET)),1),'.00',''), " +
                               "TOTAL_CREDIT = replace(convert(varchar(100),convert(money,sum(a.CREDIT)),1),'.00','') " +
                               "from dbo.V_REKENING_JURNAL_FLAG_BREAKDOWN a " +
                               "WHERE a.TRXID='" + LB_TRXID.Text + "'";
            conn.ExecuteQuery();

            DGR_DETAIL.Columns[4].FooterText = conn.GetFieldValue("TOTAL_DEBET").ToString();
            DGR_DETAIL.Columns[5].FooterText = conn.GetFieldValue("TOTAL_CREDIT").ToString();
            DGR_DETAIL.DataBind();

            for (int i = 0; i < DGR_DETAIL.Items.Count; i++)
            {
                Button btDEL = (Button)DGR_DETAIL.Items[i].FindControl("BT_DEL");
                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");

                TextBox txtDebet = (TextBox)DGR_DETAIL.Items[i].FindControl("TXT_DEBET");
                TextBox txtCredit = (TextBox)DGR_DETAIL.Items[i].FindControl("TXT_CREDIT");

                if (dt.Rows[i]["DC"].ToString() == "D")
                {
                    txtDebet.Text = dt.Rows[i]["DEBET"].ToString();
                    txtDebet.Enabled = true;
                    txtCredit.Visible = false;
                }
                if (dt.Rows[i]["DC"].ToString() == "C")
                {
                    txtCredit.Text = dt.Rows[i]["CREDIT"].ToString();
                    txtCredit.Enabled = true;
                    txtDebet.Visible = false;
                }

                if (dt.Rows[i]["IS_CUSTOMIZE"].ToString() == "0")
                {
                    btDEL.Visible = false;
                    BT_SUBMIT.Visible = false;
                } else
                {
                    btDEL.Visible = true;
                    BT_SUBMIT.Visible = true;
                }
            }
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                conn.QueryString = "exec SP_REKENING_JURNAL_FLAG_BREAKDOWN_INSERT " +
                                    "'" + LB_TRXID.Text + "'," +
                                    "'" + DDL_CODE.SelectedValue + "'," +
                                    "'" + DDL_COA.SelectedValue + "'," +
                                    "'" + DDL_T00.SelectedValue + "'," +
                                    "'" + DDL_DC.SelectedValue + "'," +
                                    "'" + TXT_AMOUNT.Text.Replace(",", "") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                TXT_AMOUNT.Text = "";

                FillDGRInfo();
                FillDGRDETAIL();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = LB_ERROR.Text + ex.Message + "<BR>";
            }
        }

        protected void BT_SUBMIT_DETAIL_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            try
            {
                for (int i = 0; i < DGR_DETAIL.Items.Count; i++)
                {
                    TextBox txtDebet = (TextBox)DGR_DETAIL.Items[i].FindControl("TXT_DEBET");
                    TextBox txtCredit = (TextBox)DGR_DETAIL.Items[i].FindControl("TXT_CREDIT");

                    conn.QueryString = "UPDATE REKENING_JURNAL_FLAG_BREAKDOWN " +
                                        "SET AMOUNT = '" + txtDebet.Text.Replace(",", "") + "' " +
                                        "WHERE " +
                                        "TRXID = '" + LB_TRXID.Text + "' " +
                                        "and CODE = '" + DGR_DETAIL.Items[i].Cells[0].Text + "' " +
                                        "and DC = 'D' " +
                                        "and COA = '" + DGR_DETAIL.Items[i].Cells[2].Text + "' " +
                                        "and T00 = '" + DGR_DETAIL.Items[i].Cells[7].Text + "' ";
                    conn.ExecuteNonQuery();

                    conn.QueryString = "UPDATE REKENING_JURNAL_FLAG_BREAKDOWN " +
                                        "SET AMOUNT = '" + txtCredit.Text.Replace(",", "") + "' " +
                                        "WHERE " +
                                        "TRXID = '" + LB_TRXID.Text + "' " +
                                        "and CODE = '" + DGR_DETAIL.Items[i].Cells[0].Text + "' " +
                                        "and DC = 'C' " +
                                        "and COA = '" + DGR_DETAIL.Items[i].Cells[2].Text + "' " +
                                        "and T00 = '" + DGR_DETAIL.Items[i].Cells[7].Text + "' ";
                    conn.ExecuteNonQuery();
                }

                FillDGRDETAIL();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = LB_ERROR.Text + ex.Message + "<BR>";
            }
        }

        protected void DGR_DETAIL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            resetLabel();

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "DELETE FROM REKENING_JURNAL_FLAG_BREAKDOWN " +
                                        "where " +
                                        "TRXID = '" + LB_TRXID.Text + "' " +
                                        "and CODE = '" + e.Item.Cells[0].Text + "' " +
                                        "and DC = '" + e.Item.Cells[1].Text + "' " +
                                        "and COA = '" + e.Item.Cells[2].Text + "' " +
                                        "and T00 = '" + e.Item.Cells[7].Text + "' ";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = LB_ERROR.Text + ex.Message + "<BR>";
                }

                FillDGRDETAIL();
            }

            if (e.CommandName == "SaveDetail")
            {
                try
                {
                    TextBox txtDebet = (TextBox)e.Item.FindControl("TXT_DEBET");
                    TextBox txtCredit = (TextBox)e.Item.FindControl("TXT_CREDIT");

                    conn.QueryString =  "UPDATE REKENING_JURNAL_FLAG_BREAKDOWN " +
                                        "SET AMOUNT = '" + txtDebet.Text.Replace(",", "") + "' "+
                                        "WHERE " +
                                        "TRXID = '" + LB_TRXID.Text + "' " +
                                        "and CODE = '" + e.Item.Cells[0].Text + "' " +
                                        "and DC = 'D' " +
                                        "and COA = '" + e.Item.Cells[2].Text + "' " +
                                        "and T00 = '" + e.Item.Cells[7].Text + "' ";
                    conn.ExecuteNonQuery();

                    conn.QueryString = "UPDATE REKENING_JURNAL_FLAG_BREAKDOWN " +
                                        "SET AMOUNT = '" + txtCredit.Text.Replace(",", "") + "' " +
                                        "WHERE " +
                                        "TRXID = '" + LB_TRXID.Text + "' " +
                                        "and CODE = '" + e.Item.Cells[0].Text + "' " +
                                        "and DC = 'C' " +
                                        "and COA = '" + e.Item.Cells[2].Text + "' " +
                                        "and T00 = '" + e.Item.Cells[7].Text + "' ";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = LB_ERROR.Text + ex.Message + "<BR>";
                }

                FillDGRDETAIL();
            }
        }

        protected void DDL_CODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            JournalDetailSubmit();
            FillDDLCOA();
        }

        protected void DDL_DC_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLCOA();
        }

        protected void BT_POST_Click(object sender, EventArgs e)
        {
            resetLabel();
            try
            {
                conn.QueryString = "EXEC SP_REKENING_JURNAL_FLAG_BREAKDOWN_POSTED " +
                                   "'" + LB_TRXID.Text + "' ";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("SUCCESS").ToString() == "0")
                {
                    LB_ERROR_2.Text = conn.GetFieldValue("MESSAGE").ToString();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data has been posted.')", true);
                }
            }
            catch (System.Exception ex)
            {
                LB_ERROR_2.Text = LB_ERROR_2.Text + ex.Message + "<BR>";
            }
        }

        protected void BT_CANCEL_Click(object sender, EventArgs e)
        {
            try
            {
                resetLabel();

                conn.QueryString = "DELETE FROM REKENING_JURNAL_FLAG_BREAKDOWN " +
                                   "WHERE TRXID = '" + LB_TRXID.Text + "' ";
                conn.ExecuteNonQuery();

                FillDGRDETAIL();
            }
            catch (System.Exception ex)
            {
                LB_ERROR_2.Text = LB_ERROR_2.Text + ex.Message + "<BR>";
            }
        }

        protected void resetLabel()
        {
            LB_ERROR.Text = "";
            LB_ERROR_2.Text = "";
        }

        protected void BT_BACK_Click(object sender, EventArgs e)
        {
            Response.Redirect("FlagRK.aspx?menucode=992");
        }

        protected void JournalDetailSubmit()
        {
            try
            {
                conn.QueryString = "EXEC SP_REKENING_JURNAL_FLAG_BREAKDOWN_INSERT_BY_CODE " +
                                   " '" + DDL_CODE.SelectedValue + "', " +
                                   " '" + LB_NOREK.Text + "', " +
                                   " '" + LB_TRXID.Text + "', " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("IS_CUSTOMIZE").ToString() == "0")
                {
                    BT_SUBMIT.Visible = false;
                }
                else
                {
                    BT_SUBMIT.Visible = true;
                }
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = LB_ERROR.Text + ex.Message + "<BR>";
            }
        }
    }
}