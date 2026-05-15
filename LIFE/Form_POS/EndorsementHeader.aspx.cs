using DMS.DBConnection;
 using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks; 
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LIFE.Form_POS
{
    public partial class EndorsementHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected int track;
        #endregion
         
        protected void Page_Load(object sender, EventArgs e) 
        {
            if (!IsPostBack) 
            {
                SetMode(Request.QueryString["ID"].ToString());
                LoadRecord();
                ShowSubmission();
                track = GetTrack();
                ShowTrack();

                conn.QueryString = "select ENABLE = dbo.UFN_PENDING_ENABLE('" + LB_REGNO.Text + "','" + LB_SEQ.Text + "','POS')";
                conn.ExecuteQuery();
                if (conn.GetFieldValue("ENABLE").ToString() == "0" || track == 3)
                {
                    BT3.Enabled = false;
                }
                if (track == 3 || track == 2 || track == 1) //Disabled Button Hsitory
                { 
                    BT7.Visible = false; 
                }
                //DV_LOADING.Visible = false;
            }
        }
        protected void SetMode(string ID)
        {
            conn.QueryString = "select " +
                                "REGNO, " +
                                "SEQ " +
                                "from V_APPLICATION_ENDORSEMENT_PARENT " +
                                "where " +
                                "REGNO + '-' + Convert(varchar(10), SEQ) = '" + ID + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                LB_REGNO.Text = conn.GetFieldValue("REGNO").ToString();
                LB_SEQ.Text = conn.GetFieldValue("SEQ").ToString();
            }
            else
            {
                string input = ID;

                string[] parts = input.Split('-');

                LB_REGNO.Text = parts[0];
                LB_SEQ.Text = parts[1];
            }
            //LB_REGNO.Text = conn.GetFieldValue("REGNO").ToString();
            //LB_SEQ.Text = conn.GetFieldValue("SEQ").ToString();

            conn.QueryString = "select " +
                                "a.REGNO " +
                                "from        APPLICATION_ENDORSEMENT_MASTER a " +
                                "inner join  V_LINK_UB_PARAM_ENDORSEMENT b on a.ENDORSEMENT_TYPE = b.CODE collate database_default and b.UW_VERIFICATION = 1 " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and a.SEQ = " + LB_SEQ.Text;
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                BT_QUO.Visible = true;
            }
        }

        protected int GetTrack()
        {
            conn.QueryString = "select SEQ = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'POS', '" + LB_SEQ.Text + "')";
            conn.ExecuteQuery();

            return int.Parse(conn.GetFieldValue("SEQ").ToString());
        }

        protected void ShowTrack()
        {
            conn.QueryString = "exec SP_APPLICATION_TRACK_BUTTON '" + LB_REGNO.Text + "','POS', '" + LB_SEQ.Text + "'";
            conn.ExecuteQuery();
            DGR_TRACK.DataSource = conn.GetDataTable().Copy();
            DGR_TRACK.DataBind();

            for (int i = 0; i < DGR_TRACK.Items.Count; i++)
            {
                Button bt = (Button)DGR_TRACK.Items[i].FindControl("BT_TRACK");
                TextBox txt = (TextBox)DGR_TRACK.Items[i].FindControl("TXT_TRACK");
                Label lb = (Label)DGR_TRACK.Items[i].FindControl("LB_TRACK");
                //LB_BLACKLIST.Text = conn.GetFieldValue("BLACKLIST_PP").ToString() + conn.GetFieldValue("BLACKLIST_PU").ToString();

                if (DGR_TRACK.Items[i].Cells[3].Text == "1")
                {
                    bt.Visible = false;
                    txt.Visible = false;
                    lb.Text = DGR_TRACK.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                }
                else
                {
                    bt.Text = DGR_TRACK.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                    bt.Attributes.Add("onclick", "if(!confirm('Are you sure to go to " + bt.Text + " ?')){return false;};");
                    if (DGR_TRACK.Items[i].Cells[2].Text == "0")
                        txt.Visible = false;
                }
            }
        }


        protected void LoadRecord()
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_AGE.Text = conn.GetFieldValue("START_AGE").ToString();
            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_FULLNAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_INSPERIOD.Text = conn.GetFieldValue("INSURANCE_PERIOD").ToString();
            LB_PAYMENTPERIOD.Text = conn.GetFieldValue("PAYMENT_PERIOD").ToString();
            LB_PREMIUM.Text = conn.GetFieldValue("BASICPREMIUM").ToString();
            LB_PRODUCTGROUP.Text = conn.GetFieldValue("PRODUCT_GROUP_NAME").ToString();
            LB_PRODUCTNAME.Text = conn.GetFieldValue("PRODUCT_NAME").ToString();
            LB_SUMINS.Text = conn.GetFieldValue("SUMINS").ToString();
            LB_UWCODE.Text = conn.GetFieldValue("UW_DESCR").ToString();
            LB_VACC.Text = conn.GetFieldValue("VACC").ToString();

            string status1 = conn.GetFieldValue("MAIN_INSURED_BLOCK_STATUS").ToString();
            string status2 = conn.GetFieldValue("POLICY_HOLDER_BLOCK_STATUS").ToString();
            string status3 = conn.GetFieldValue("RESIKO_TINGGI2").ToString();
            string status4 = conn.GetFieldValue("RESIKO_TINGGI2").ToString();
            string status5 = conn.GetFieldValue("RISK_COUNTRY").ToString();
            string status6 = conn.GetFieldValue("RISK_JOB").ToString();
            string warning = ShowBlacklist();


            conn.QueryString = "select " +
                                "REGNO  " +
                                "from APPLICATION_ENDORSEMENT_MASTER a " +
                                "inner join UWBOX.dbo.PARAM_ENDORSEMENT b on a.ENDORSEMENT_TYPE = b.CODE and b.GROUP_CODE = 'ALN' " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and a.SEQ = " + LB_SEQ.Text;
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
                BT_QUO.Visible = true;

            //if (!string.IsNullOrEmpty(status1) && !string.IsNullOrWhiteSpace(status1))
            //{
            //    warning = status1;
            //}

            //if (!string.IsNullOrEmpty(status2) && !string.IsNullOrWhiteSpace(status2))
            //{
            //    if (warning == "")
            //    {
            //        warning = status2;
            //    }
            //    else
            //    {
            //        warning += "<br>" + status2;
            //    }
            //}

            //if (!string.IsNullOrEmpty(status3) && !string.IsNullOrWhiteSpace(status3))
            //{
            //    if (warning == "")
            //    {
            //        warning = status3;
            //    }
            //    else
            //    {
            //        warning += "<br>" + status3;
            //    }
            //}
            //else if (!string.IsNullOrEmpty(status4) && !string.IsNullOrWhiteSpace(status4))
            //{
            //    if (warning == "")
            //    {
            //        warning = status4;
            //    }
            //    else
            //    {
            //        warning += "<br>" + status4;
            //    }
            //}

            //if (!string.IsNullOrEmpty(status5) && !string.IsNullOrWhiteSpace(status5))
            //{
            //    if (warning == "")
            //    {
            //        warning = status5;
            //    }
            //    else
            //    {
            //        warning += "<br>" + status5;
            //    }
            //}

            //if (!string.IsNullOrEmpty(status6) && !string.IsNullOrWhiteSpace(status6))
            //{
            //    if (warning == "")
            //    {
            //        warning = status6;
            //    }
            //    else
            //    {
            //        warning += "<br>" + status6;
            //    }
            //}



            if (warning != "" && warning != "&nbsp;")
            {
                TD_WARNING.Visible = true;
                TD_WARNING.Style.Value = "padding: 12px;border: 2px solid red;";
                LB_WARNING.Text = warning;
                LB_WARNING.CssClass = "alert";
            }
            else
            {
                TD_WARNING.Visible = false;
                TD_WARNING.Style.Value = "";
                LB_WARNING.Text = "";
            }


        }

        protected void ShowSubmission()
        {
            LB_TITLE.Text = BT1.Text;

            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) < 4)
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementSubmissionFrame.aspx?regno=" + LB_REGNO.Text + "&seq=" + LB_SEQ.Text + "';</script>");
            else
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementReportFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowSubmission();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_App/ApplicationPremiumTermFrame.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementDocFrame.aspx?regno=" + LB_REGNO.Text + "&seq=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementRemarkFrame.aspx?regno=" + LB_REGNO.Text + "&seq=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementPendingFrame.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_App/ApplicationLoadingShare.aspx?ID=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT7_Click(object sender, EventArgs e)
        {           
            LB_TITLE.Text = ((Button)sender).Text;
            conn.QueryString = "SELECT TOP 1 a.SEQ,a.ENDORSEMENT_TYPE FROM    APPLICATION_ENDORSEMENT_MASTER a INNER JOIN  V_APPLICATION_master b ON a.regno = b.regno WHERE  a.regno = '" + LB_REGNO.Text + "' ORDER BY  a.SEQ DESC";
            conn.ExecuteQuery();

            string strENDORSEMENT_TYPE = conn.GetFieldValue("ENDORSEMENT_TYPE").ToString();
            string strSEQ = conn.GetFieldValue("SEQ").ToString();

            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER '" + LB_REGNO.Text + "'," + int.Parse(strSEQ);
            conn.ExecuteQuery();            
            string urlDetail = conn.GetFieldValue("URL").ToString(); 


            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_POS/EndorsementPayOutFrame.aspx?regno=" + LB_REGNO.Text + "&SEQ=8&TYPE=MBR01&REMARK=1';</script>");
            //ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_POS/EndorsementPayOutFrame.aspx?regno=" + LB_REGNO.Text + "&SEQ=" + strSEQ + "&TYPE=" + strENDORSEMENT_TYPE + "&REMARK=1';</script>");
            
            if (LB_TITLE.Text == "HISTORY" &&  !string.IsNullOrEmpty (urlDetail)) // Tampilkan Detail sesuai Endorsment seperti Submission
            {
                ClientScript.RegisterStartupScript(
                    GetType(),
                    "",
                    "<script>parent.appbody.location.href = '../Form_POS/" + urlDetail + "';</script>"
                );


            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_POS/EndorsementPayOutFrame.aspx?regno=" + LB_REGNO.Text + "&SEQ=" + strSEQ + "&TYPE=" + strENDORSEMENT_TYPE + "&REMARK=1';</script>");

            }


        }

        protected  void DGR_TRACK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Track")
            {

                TextBox txt = (TextBox)e.Item.FindControl("TXT_TRACK");
                string remark = "";

                if (VerificationBlacklist(LB_REGNO.Text))
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>alert('Verifkasi tidak dapat dilanjutkan karena member terdeteksi blacklist/fraud.')</script>");
                    return;
                }

                if (!txt.Visible)
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_VALIDATION_LOG " +
                                        "'" + LB_REGNO.Text + "'," +
                                        LB_SEQ.Text + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery();

                    if (conn.GetRowCount() > 0)
                    {
                        LB_TITLE.Text = "VALIDATION";
                        ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementValidationLog.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
                        return;
                    }
                }
                else
                {
                    if (txt.Text.Trim().Replace("'", "`") == "")
                        return;
                    remark = txt.Text.Trim().Replace("'", "`");
                }

                //await SetNextTrack(e.Item.Cells[0].Text, remark, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
                Task.Run(() => SetNextTrack(e.Item.Cells[0].Text, remark, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID")));


                string URL = "../Standard/default.html";
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = '" + URL + "';</script>");
            }
        }
        protected void SetNextTrack(string track, string remark, string userby)
        {
            int IntLAST_TRACK = 0;
            int GoIntLAST_TRACK = 0;
            int IntUNITIZE = 0;
            string strENDORSEMENT_TYPE_DESCR = "";

            string regno = LB_REGNO.Text.Replace("'", "''");
            string seq = LB_SEQ.Text;
            try
            {
                //conn.QueryString = "select LAST_TRACK,pg.UNITIZE,ENDORSEMENT_TYPE_DESCR " +
                //   "from V_APPLICATION_ENDORSEMENT_PARENT a " +
                //   "inner join UWBOX.dbo.PARAM_PRODUCT_GROUP pg on a.PRODUCT_GROUP_CODE = pg.CODE " +
                //   "where a.regno = '" + regno + "'";

                conn.QueryString = @"
                                    SELECT TOP 1 
                                        pe.DESCR AS ENDORSEMENT_TYPE_DESCR,
                                        pg.UNITIZE,
                                        CASE
                                            WHEN TRACK1_DATE IS NOT NULL 
                                                 AND TRACK2_DATE IS NULL 
                                                 AND TRACK3_DATE IS NULL 
                                                 AND TRACK4_DATE IS NULL THEN 1

                                            WHEN TRACK1_DATE IS NOT NULL 
                                                 AND TRACK2_DATE IS NOT NULL 
                                                 AND TRACK3_DATE IS NULL 
                                                 AND TRACK4_DATE IS NULL THEN 2

                                            WHEN TRACK1_DATE IS NOT NULL 
                                                 AND TRACK2_DATE IS NOT NULL 
                                                 AND TRACK3_DATE IS NOT NULL 
                                                 AND TRACK4_DATE IS NULL THEN 3

                                            WHEN TRACK1_DATE IS NOT NULL 
                                                 AND TRACK2_DATE IS NULL 
                                                 AND TRACK3_DATE IS NOT NULL 
                                                 AND TRACK4_DATE IS NULL THEN 3
                                        END AS LAST_TRACK
                                    FROM APPLICATION_TRACK a
                                    INNER JOIN APPLICATION_MASTER b 
                                        ON a.REGNO = b.REGNO

                                    INNER JOIN APPLICATION_ENDORSEMENT_MASTER c 
                                        ON a.REGNO = c.REGNO
                                        AND a.PARAM_VALUE = c.SEQ

                                    INNER JOIN UWBOX.dbo.PARAM_ENDORSEMENT pe 
                                        ON c.ENDORSEMENT_TYPE = pe.CODE

                                    INNER JOIN UWBOX.dbo.V_PARAM_PRODUCT_MASTER bb 
                                        ON b.PRODUCT_CODE = bb.PRODUCT_CODE

                                    INNER JOIN UWBOX.dbo.PARAM_PRODUCT_GROUP pg 
                                        ON bb.PRODUCT_GROUP = pg.CODE

                                    WHERE a.REGNO = '" + regno + @"'
                                    AND a.TRACK_TYPE = 'POS'

                                    ORDER BY a.PARAM_VALUE DESC";

                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    IntLAST_TRACK = Convert.ToInt16(conn.GetFieldValue("LAST_TRACK"));
                    IntUNITIZE = Convert.ToInt16(conn.GetFieldValue("UNITIZE"));
                    strENDORSEMENT_TYPE_DESCR = conn.GetFieldValue("ENDORSEMENT_TYPE_DESCR").ToString();
                }

                if (IntUNITIZE == 0) //Non Unitized
                {
                    if (IntLAST_TRACK == 1)
                    {
                        GoIntLAST_TRACK = IntLAST_TRACK + 2;
                    }
                    else if (IntLAST_TRACK == 2)
                    {
                        GoIntLAST_TRACK = IntLAST_TRACK + 1;
                    }
                    else if (IntLAST_TRACK == 3)
                    {
                        GoIntLAST_TRACK = IntLAST_TRACK + 1;
                    }




                    // 🔥 EKSEKUSI SP SEKALI SAJA
                    string spQuery = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK_ALN " +
                                     "'" + regno + "'," +
                                     "'" + seq + "'," +
                                     GoIntLAST_TRACK + "," +
                                     "'" + remark + "'," +
                                     "'" + userby + "'";

                    conn.QueryString = spQuery;
                    conn.ExecuteQuery();


                    // 🔥 KHUSUS TRACK 3 → EMAIL    
                    if (IntLAST_TRACK == 3)
                    {
                        conn.QueryString = "select top 1 * " +
                                             "from APPLICATION_TRACK a " +
                                             "inner join V_APPLICATION_MASTER b ON a.REGNO = b.REGNO " +
                                             "inner join APPLICATION_ENDORSEMENT_MASTER c ON b.REGNO = c.REGNO " +
                                             "JOIN UWBOX.dbo.PARAM_PRODUCT_GROUP pg ON b.PRODUCT_GROUP_CODE = pg.CODE " +
                                             "where pg.UNITIZE = 0 " +
                                             "and c.ENDORSEMENT_TYPE = 'MBR13' " +
                                             "and a.REGNO = '" + regno + "' " +
                                             "and a.TRACK_TYPE = 'POS' " +
                                             "order by a.PARAM_VALUE desc";

                        conn.ExecuteQuery();


                        if (conn.GetRowCount() > 0)
                        {
                            sendEmailToCrecon(IntUNITIZE);
                        }

                    }


                }
                else
                {
                    if (IntUNITIZE == 1)
                    {
                        if (IntLAST_TRACK == 1)
                        {
                            conn.QueryString = "SELECT TOP 1 * FROM V_APPLICATION_MASTER a " +
                                               "INNER JOIN APPLICATION_TRACK b ON a.REGNO = b.REGNO " +
                                               "WHERE a.REGNO = '" + regno + "' AND a.PRODUCT_GROUP_CODE= 'IUL' " +
                                               "AND b.TRACK1_DATE IS NOT NULL AND b.TRACK2_DATE IS NULL " +
                                               "ORDER BY b.PARAM_VALUE DESC";
                            conn.ExecuteQuery();

                            if (conn.GetRowCount() > 0 &&
                                (strENDORSEMENT_TYPE_DESCR == "FREELOOK"
                                    || strENDORSEMENT_TYPE_DESCR == "SURRENDER"
                                    || strENDORSEMENT_TYPE_DESCR == "MATURIY"
                                    || strENDORSEMENT_TYPE_DESCR == "WITHDRAWAL")
                                    )
                            {
                                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK_IUL " +
                                                   "'" + regno + "','" + seq + "'," + IntLAST_TRACK +
                                                   ",'" + remark + "','" + userby + "'"
;
                                conn.ExecuteQuery();
                            }
                            else
                            {
                                IntLAST_TRACK++;
                                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK_ALN " +
                                                   "'" + regno + "','" + seq + "'," + IntLAST_TRACK +
                                                   ",'" + remark + "','" + userby + "'";
                                conn.ExecuteQuery();
                            }
                        }
                        else if (IntLAST_TRACK == 3 &&
                                (strENDORSEMENT_TYPE_DESCR == "FREELOOK"
                                    || strENDORSEMENT_TYPE_DESCR == "SURRENDER"
                                    || strENDORSEMENT_TYPE_DESCR == "MATURIY"
                                    || strENDORSEMENT_TYPE_DESCR == "WITHDRAWAL")
                                    )
                        {
                            conn.QueryString = "SELECT TOP 1 * FROM V_APPLICATION_MASTER a " +
                                                   "INNER JOIN APPLICATION_TRACK b ON a.REGNO = b.REGNO " +
                                                   "WHERE a.REGNO = '" + regno + "' AND a.PRODUCT_GROUP_CODE= 'IUL' " +
                                                   "AND b.TRACK4_DATE IS NULL ORDER BY b.PARAM_VALUE DESC";
                            conn.ExecuteQuery();

                            if (conn.GetRowCount() > 0)
                            {
                                conn.QueryString = "update APPLICATION_TRACK set TRACK4_DATE=getdate(), " +
                                                   "TRACK4_BY ='" + userby + "' " +
                                                   "where TRACK_TYPE='POS' and regno= '" + regno + "'";
                                conn.ExecuteQuery();
                            }
                            else
                            {
                                string spQuery = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK_ALN " +
                                        "'" + regno + "'," +
                                        "'" + seq + "'," +
                                        IntLAST_TRACK + "," +
                                        "'" + remark + "'," +
                                        "'" + userby + "'";
                                conn.QueryString = spQuery;
                                conn.ExecuteQuery();

                                conn.QueryString = "select top 1 * " +
                                                     "from APPLICATION_TRACK a " +
                                                     "inner join V_APPLICATION_MASTER b ON a.REGNO = b.REGNO " +
                                                     "inner join APPLICATION_ENDORSEMENT_MASTER c ON b.REGNO = c.REGNO " +
                                                     "JOIN UWBOX.dbo.PARAM_PRODUCT_GROUP pg ON b.PRODUCT_GROUP_CODE = pg.CODE " +
                                                     "where pg.UNITIZE = 1 " +
                                                     "and c.ENDORSEMENT_TYPE = 'MBR13' " +
                                                     "and a.REGNO = '" + regno + "' " +
                                                     "and a.TRACK_TYPE = 'POS' " +
                                                     "order by a.PARAM_VALUE desc";

                                conn.ExecuteQuery();


                                //if (conn.GetRowCount() > 0)
                                //{
                                //    sendEmailToCrecon(IntUNITIZE);
                                //}

                                //IntLAST_TRACK++;

                                //if (IntLAST_TRACK == 4 &&
                                //    (strENDORSEMENT_TYPE_DESCR == "FREELOOK"
                                //        || strENDORSEMENT_TYPE_DESCR == "SURRENDER"
                                //        || strENDORSEMENT_TYPE_DESCR == "MATURIY"
                                //        || strENDORSEMENT_TYPE_DESCR == "WITHDRAWAL")
                                //        )
                                //{
                                //    conn.QueryString = "SELECT TOP 1 * FROM V_APPLICATION_MASTER a " +
                                //                       "INNER JOIN APPLICATION_TRACK b ON a.REGNO = b.REGNO " +
                                //                       "WHERE a.REGNO = '" + regno + "' AND a.PRODUCT_GROUP_CODE= 'IUL' " +
                                //                       "AND b.TRACK4_DATE IS NULL ORDER BY b.PARAM_VALUE DESC";
                                //    conn.ExecuteQuery();

                                //    if (conn.GetRowCount() > 0)

                                //    {
                                //        conn.QueryString = "update APPLICATION_TRACK set TRACK4_DATE=getdate(), " +
                                //                           "TRACK4_BY ='" + userby + "' " +
                                //                           "where TRACK_TYPE='POS' and regno= '" + regno + "'";
                                //        conn.ExecuteQuery();

                                //        //conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_NOTIFICATION_EMAIL " +
                                //        //                   "'" + regno + "','" + int.Parse(seq) + "','" + userby + "'";
                                //        //conn.ExecuteQuery();

                                //        // 🔥 EMAIL TANPA BLOCK
                                //        //sendEmailToCrecon(IntUNITIZE);
                                //    }
                                //    else
                                //    {
                                //        string spQuery = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK_ALN " +
                                //        "'" + regno + "'," +
                                //        "'" + seq + "'," +
                                //        IntLAST_TRACK + "," +
                                //        "'" + remark + "'," +
                                //        "'" + userby + "'";
                                //        conn.QueryString = spQuery;
                                //        conn.ExecuteQuery();

                                //        conn.QueryString = "select top 1 * " +
                                //                             "from APPLICATION_TRACK a " +
                                //                             "inner join V_APPLICATION_MASTER b ON a.REGNO = b.REGNO " +
                                //                             "inner join APPLICATION_ENDORSEMENT_MASTER c ON b.REGNO = c.REGNO " +
                                //                             "JOIN UWBOX.dbo.PARAM_PRODUCT_GROUP pg ON b.PRODUCT_GROUP_CODE = pg.CODE " +
                                //                             "where pg.UNITIZE = 1 " +
                                //                             "and c.ENDORSEMENT_TYPE = 'MBR13' " +
                                //                             "and a.REGNO = '" + regno + "' " +
                                //                             "and a.TRACK_TYPE = 'POS' " +
                                //                             "order by a.PARAM_VALUE desc";

                                //        conn.ExecuteQuery();


                                //        if (conn.GetRowCount() > 0)
                                //        {
                                //            sendEmailToCrecon(IntUNITIZE);
                                //        }


                                //    }
                                //}
                                ////else if (conn.GetRowCount() > 0 && strENDORSEMENT_TYPE_DESCR == "TOPUP IRREGULER")
                                ////{
                                ////    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK_TOPUP_LINK " +
                                ////                       "'" + regno + "','" + seq + "'," + IntLAST_TRACK +
                                ////                       ",'" + remark + "','" + userby + "'";
                                ////    conn.ExecuteQuery();

                                ////    sendEmailToCrecon(IntUNITIZE);
                                ////}
                                //else
                                //{
                                //    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK_ALN  " +
                                //                       "'" + regno + "','" + seq + "'," + IntLAST_TRACK +
                                //                       ",'" + remark + "','" + userby + "'";
                                //    conn.ExecuteQuery();

                                //}
                            }
                        }
                        else
                        {
                            IntLAST_TRACK++;
                            string spQuery = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK_ALN " +
                                    "'" + regno + "'," +
                                    "'" + seq + "'," +
                                    IntLAST_TRACK + "," +
                                    "'" + remark + "'," +
                                    "'" + userby + "'";
                            conn.QueryString = spQuery;
                            conn.ExecuteQuery();

                            conn.QueryString = "select top 1 * " +
                                                 "from APPLICATION_TRACK a " +
                                                 "inner join V_APPLICATION_MASTER b ON a.REGNO = b.REGNO " +
                                                 "inner join APPLICATION_ENDORSEMENT_MASTER c ON b.REGNO = c.REGNO " +
                                                 "JOIN UWBOX.dbo.PARAM_PRODUCT_GROUP pg ON b.PRODUCT_GROUP_CODE = pg.CODE " +
                                                 "where pg.UNITIZE = 1 " +
                                                 "and c.ENDORSEMENT_TYPE = 'MBR13' " +
                                                 "and a.REGNO = '" + regno + "' " +
                                                 "and a.TRACK_TYPE = 'POS' " +
                                                 "order by a.PARAM_VALUE desc";

                            conn.ExecuteQuery();
                        }
                    }
                }

                conn.QueryString = "exec SP_GetNextRunningNumber";
                conn.ExecuteQuery();

                string RunningNumber = conn.GetFieldValue("RunningNumber").ToString();

                conn.QueryString = "insert into DOKUMENNO (RegNo,RunningNumber) VALUES " +
                                   "('" + regno + "','" + RunningNumber + "')";
                conn.ExecuteQuery();
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }

        }

        private void sendEmailToCrecon(int intUnitize)
        {
            string logFolder = @"C:\Temp";
            string logFile = Path.Combine(logFolder, "email_debug.txt");
            string tempFolder = @"C:\Temp\EmailAttachment\";

            string regno = "";
            int seq = 0;
            string productCode = "";
            string tipeEndorsement = "";
            string noPolis = "";
            string namaPemegang = "";
            string totalTransaksi = "0";
            string fullPath = "";
            string tujuan = "";

            try
            {
                // =========================
                // 0. PERSIAPAN LOG & FOLDER
                // =========================
                if (!Directory.Exists(logFolder))
                    Directory.CreateDirectory(logFolder);

                if (!Directory.Exists(tempFolder))
                    Directory.CreateDirectory(tempFolder);

                File.AppendAllText(logFile, "\n==============================\n");
                File.AppendAllText(logFile, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n");
                File.AppendAllText(logFile, "START sendEmailToCrecon\n");

                regno = LB_REGNO.Text.Replace("'", "''");
                seq = Convert.ToInt32(LB_SEQ.Text);

                File.AppendAllText(logFile, "REGNO: " + regno + "\n");
                File.AppendAllText(logFile, "SEQ: " + seq + "\n");
                File.AppendAllText(logFile, "INTUNITIZE: " + intUnitize + "\n");

                // =========================
                // 1. VALIDASI TOP UP IRREGULAR
                // =========================

                conn.QueryString = "SELECT TOP 1 c.ENDORSEMENT_TYPE AS ENDORSEMENT_TYPE " +
                    "FROM APPLICATION_TRACK a " +
                    "INNER JOIN V_APPLICATION_MASTER b ON a.REGNO = b.REGNO " +
                    "INNER JOIN UWBOX.dbo.PARAM_PRODUCT_GROUP pg ON b.PRODUCT_GROUP_CODE = pg.CODE " +
                    "INNER JOIN APPLICATION_ENDORSEMENT_MASTER c ON a.REGNO = c.REGNO " +
                    "WHERE pg.UNITIZE = " + intUnitize + " " +
                    "AND c.ENDORSEMENT_TYPE = 'MBR13' " +
                    "AND a.REGNO = '" + regno + "' " +
                    "AND a.TRACK_TYPE = 'POS' " +
                    "ORDER BY a.PARAM_VALUE DESC";
                conn.ExecuteQuery();
                tipeEndorsement = conn.GetFieldValue("ENDORSEMENT_TYPE");
                // Kalau bukan TopUp Irregular, stop
                if (tipeEndorsement != "MBR13")
                {
                    File.AppendAllText(logFile, "STOP: Bukan TopUp Irregular.\n");
                    return;
                }

                // =========================
                // 2. TOTAL TOPUP
                // =========================
                conn.QueryString = "SELECT SUM(CAST(REPLACE(NEW_VAL,',','') AS DECIMAL(18,0))) totalTopup " +
                                   "FROM APPLICATION_ALTERATION_DETAIL " +
                                   "WHERE REGNO = '" + regno + "' " +
                                   "AND SEQ = " + seq + " " +
                                   "AND ENDORSEMENT_TYPE = 'MBR13'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0 && !string.IsNullOrEmpty(conn.GetFieldValue("totalTopup")))
                    totalTransaksi = conn.GetFieldValue("totalTopup").ToString();

                File.AppendAllText(logFile, "TOTAL TOPUP: " + totalTransaksi + "\n");

                // =========================
                // 3. GENERATE FILE ATTACHMENT DARI DB
                // =========================
                try
                {
                    conn.QueryString = "SELECT TOP 1 * FROM ARCHIEVE.dbo.LF_ARSIP " +
                                       "WHERE REMARK = 'Bukti Bayar' AND OWNER1 = '" + regno + "'";
                    conn.ExecuteQuery();

                    File.AppendAllText(logFile, "LF_ARSIP RowCount: " + conn.GetRowCount() + "\n");

                    if (conn.GetRowCount() > 0)
                    {
                        string namaFile = conn.GetFieldValue("NAMAFILE").ToString().Trim();
                        fullPath = Path.Combine(tempFolder, namaFile);

                        File.AppendAllText(logFile, "NamaFile DB: " + namaFile + "\n");
                        File.AppendAllText(logFile, "FullPath: " + fullPath + "\n");

                        // Hapus file lama kalau ada
                        if (File.Exists(fullPath))
                        {
                            File.Delete(fullPath);
                            File.AppendAllText(logFile, "File lama dihapus.\n");
                        }

                        // Generate file fisik dari THEFILE (VARBINARY)
                        GlobalUse.SQLToFilePath(fullPath.Trim(),
                            "SELECT THEFILE FROM ARCHIEVE.dbo.LF_ARSIP WHERE REMARK = 'Bukti Bayar' AND OWNER1='" + regno + "'");

                        File.AppendAllText(logFile, "File Exists After SQLToFilePath: " + File.Exists(fullPath) + "\n");

                        if (File.Exists(fullPath))
                        {
                            FileInfo fi = new FileInfo(fullPath);
                            File.AppendAllText(logFile, "File Size: " + fi.Length + "\n");
                        }
                    }
                    else
                    {
                        File.AppendAllText(logFile, "Tidak ada data attachment di LF_ARSIP.\n");
                    }
                }
                catch (Exception exAttach)
                {
                    File.AppendAllText(logFile, "ERROR ATTACHMENT: " + exAttach.ToString() + "\n");
                }

                // =========================
                // 4. DATA POLIS / MEMBER
                // =========================
                conn.QueryString = "exec SP_APPLICATION_MASTER '" + regno + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    noPolis = conn.GetFieldValue("POLICY_NO").ToString();
                    namaPemegang = conn.GetFieldValue("FULLNAME").ToString();
                }

                File.AppendAllText(logFile, "POLICY_NO: " + noPolis + "\n");
                File.AppendAllText(logFile, "FULLNAME: " + namaPemegang + "\n");

                string circleDate = DateTime.Now.ToString("dd MMM yyyy");

                // =========================
                // 5. BODY EMAIL
                // =========================
                string emailBody = @"
                                <html>
                                <body style='font-family:Tahoma; font-size:12px;'>
                                Kepada Yth: <b>Bagian Credit Control PT Asuransi Takaful Keluarga</b><br/>
                                Dari: <b>Bagian Policy Owner Service</b><br/>
                                Hal: <b>Informasi Top Up Irregular Polis " + noPolis + @" - " + namaPemegang + @"</b><br/><br/>

                                <p>Assalamu’alaikum Wr Wb,</p>
                                <p>Sehubungan dengan adanya pengajuan proses Top Up Irregular dengan data sebagai berikut:</p>

                                <table style='border-collapse:collapse;'>
                                <tr><td style='width:120px;'>No Polis</td><td>: " + noPolis + @"</td></tr>
                                <tr><td>Nominal</td><td>: Rp " + totalTransaksi + @"</td></tr>
                                <tr><td>Cycle Date</td><td>: " + circleDate + @"</td></tr>
                                </table>

                                <br/>
                                <p>Maka dengan ini mohon agar dapat diproses lebih lanjut input kontribusi Top Up Irregular tersebut dengan kelengkapan berkas terlampir.</p>

                                <p>Demikian disampaikan, atas perhatian dan kerjasama yang baik kami ucapkan terima kasih.</p>

                                <p>Wassalamu’alaikum Wr Wb</p>

                                <b>Policy Owner Service</b>
                                </body>
                                </html>";

                // =========================
                // 6. RECIPIENT
                // =========================
                conn.QueryString = "SELECT * FROM EMAIL_RECIPIENT_POS WHERE IS_ACTIVE = 1 AND EMAIL NOT IN ('cs_atk@takaful.com','')";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    DataTable dt = conn.GetDataTable().Copy();

                    var emails = dt.AsEnumerable()
                                   .Select(dr => dr["EMAIL"] == null ? "" : dr["EMAIL"].ToString().Trim())
                                   .Where(email => !string.IsNullOrEmpty(email));

                    tujuan = string.Join(";", emails);
                }

                File.AppendAllText(logFile, "RECIPIENTS: " + tujuan + "\n");

                if (string.IsNullOrEmpty(tujuan))
                {
                    File.AppendAllText(logFile, "STOP: RECIPIENTS kosong.\n");
                    return;
                }

                // =========================
                // 7. SMTP CONFIG
                // =========================
                string dari = "no-reply@takaful.com";
                string password = "@Tk2017";
                string smtpServer = "smtpatk.takaful.com";
                int smtpPort = 587;

                File.AppendAllText(logFile, "SMTP SERVER: " + smtpServer + "\n");
                File.AppendAllText(logFile, "SMTP PORT: " + smtpPort + "\n");

                // =========================
                // 8. SEND EMAIL
                // =========================
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(dari);

                    foreach (var email in tujuan.Split(';'))
                    {
                        if (!string.IsNullOrWhiteSpace(email))
                            mail.To.Add(email.Trim());
                    }

                    mail.Subject = "TOP UP IRREGULAR - " + noPolis + " (" + namaPemegang + ")";
                    mail.Body = emailBody;
                    mail.IsBodyHtml = true;

                    // ATTACH FILE kalau ada
                    if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
                    {
                        File.AppendAllText(logFile, "ATTACHMENT FOUND, adding...\n");
                        mail.Attachments.Add(new Attachment(fullPath));
                    }
                    else
                    {
                        File.AppendAllText(logFile, "ATTACHMENT NOT FOUND / EMPTY\n");
                    }

                    using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(dari, password);
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Timeout = 30000;

                        try
                        {
                            File.AppendAllText(logFile, "BEFORE SMTP SEND...\n");
                            System.Net.ServicePointManager.SecurityProtocol =
                                SecurityProtocolType.Tls12 |
                                SecurityProtocolType.Tls11 |
                                SecurityProtocolType.Tls;

                            smtp.Send(mail);
                            File.AppendAllText(logFile, "EMAIL BERHASIL DIKIRIM.\n");
                        }
                        catch (Exception exMail)
                        {
                            File.AppendAllText(logFile, "ERROR SMTP SEND: " + exMail.ToString() + "\n");
                            throw;
                        }
                    }
                }

                // =========================
                // 9. DELETE FILE TEMP
                // =========================
                if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
                {
                    try
                    {
                        File.Delete(fullPath);
                        File.AppendAllText(logFile, "FILE TEMP DELETED.\n");
                    }
                    catch (Exception exDelete)
                    {
                        File.AppendAllText(logFile, "ERROR DELETE FILE TEMP: " + exDelete.ToString() + "\n");
                    }
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (!Directory.Exists(logFolder))
                        Directory.CreateDirectory(logFolder);

                    File.AppendAllText(logFile, "FATAL ERROR: " + ex.ToString() + "\n");
                }
                catch { }

                ScriptManager.RegisterStartupScript(this, GetType(), "swalMail",
                    "Swal.fire({ position: 'center', icon: 'warning', title: 'Email tidak terkirim', text: '" + ex.Message.Replace("'", "\\'") + "', showConfirmButton: true });", true);
            }
        }
       
        //private void sendEmailToCrecon(int intUnitize)
        //{
        //    //Added By Endi -> untuk mengirimkan e-mail ke credit control jika endorsement type = Topup Irregular
        //    string productCode = "";
        //    string tipeEndorsement = "";
        //    if (intUnitize == 0)
        //    {
        //        conn.QueryString = "select * from V_APPLICATION_ENDORSEMENT_PARENT where LAST_TRACK = 3 and REGNO = '" + LB_REGNO.Text + "' AND SEQ = " + LB_SEQ.Text;
        //        conn.ExecuteQuery();
        //        productCode = conn.GetFieldValue("PRODUCT_CODE").ToString().Trim();
        //    }
        //    else
        //    {
        //        conn.QueryString = "select * from V_APPLICATION_ENDORSEMENT_PARENT_FAST where LAST_TRACK = 4 and REGNO = '" + LB_REGNO.Text + "' AND SEQ = " + LB_SEQ.Text;
        //        conn.ExecuteQuery();
        //        tipeEndorsement = conn.GetFieldValue("ENDORSEMENT_TYPE_DESCR").ToString().Trim();
        //    }

        //    if (productCode == "117" || tipeEndorsement == "TopUp Irreguler")
        //    {
        //        conn.QueryString = "select SUM(CAST(REPLACE(NEW_VAL,',','') AS DECIMAL(18,0))) totalTopup from APPLICATION_ALTERATION_DETAIL where REGNO = '" +
        //                            LB_REGNO.Text + "' and SEQ = " + LB_SEQ.Text + " and ENDORSEMENT_TYPE = 'MBR13'";
        //        conn.ExecuteQuery();
        //        string totalTransaksi = conn.GetFieldValue("totalTopup").ToString();

        //        // penyiapan file Bukti Bayar untuk di-attach dalam mail : By Endi O
        //        string fullPath = "";
        //        conn.QueryString = "SELECT * FROM ARCHIEVE.dbo.LF_ARSIP where REMARK = 'Bukti Bayar' AND OWNER1 = '" + LB_REGNO.Text + "'";
        //        conn.ExecuteQuery();
        //        if (conn.GetRowCount() > 0)
        //        {
        //            fullPath = Server.MapPath("~/Upload/") + conn.GetFieldValue("NAMAFILE");
        //            GlobalUse.SQLToFilePath(fullPath.Trim(),
        //                "SELECT THEFILE FROM ARCHIEVE.dbo.LF_ARSIP WHERE REMARK = 'Bukti Bayar' AND OWNER1='" + LB_REGNO.Text + "'");
        //        }
        //        string noPolis, namaPemegang;
        //        try
        //        {
        //            conn.QueryString = "exec SP_APPLICATION_MASTER '" + LB_REGNO.Text + "'";
        //            conn.ExecuteQuery();

        //            noPolis = conn.GetFieldValue("POLICY_NO").ToString();
        //            namaPemegang = conn.GetFieldValue("FULLNAME").ToString();

        //            string circleDate = DateTime.Now.ToString("dd MMM yyyy");

        //            // isi body HTML surat
        //            string emailBody = @"
        //                                                    <html>
        //                                                    <body style='font-family:Tahoma; font-size:12px;'>
        //                                                    Kepada Yth: <b>Bagian Credit Control PT Asuransi Takaful Keluarga</b><br/>
        //                                                    Dari: <b>Bagian Policy Owner Service</b><br/>
        //                                                    Hal: <b>Informasi Top Up Irregular Polis " + noPolis + @" - " + namaPemegang + @"</b><br/><br/>
        //                                                    <p>Assalamu’alaikum Wr Wb,</p>
        //                                                    <p>Sehubungan dengan adanya pengajuan proses Top Up Irregular dengan data sebagai berikut:</p>

        //                                                    <table style='border-collapse:collapse;'>
        //                                                    <tr><td style='width:120px;'>No Polis</td><td>: " + noPolis + @"</td></tr>
        //                                                    <tr><td>Nominal</td><td>: Rp " + totalTransaksi + @"</td></tr>
        //                                                    <tr><td>Cycle Date</td><td>: " + circleDate + @"</td></tr>
        //                                                    </table>

        //                                                    <br/>
        //                                                    <p>Maka dengan ini mohon agar dapat diproses lebih lanjut input kontribusi Top Up Irregular tersebut dengan kelengkapan berkas terlampir.</p>

        //                                                    <p>Demikian disampaikan, atas perhatian dan kerjasama yang baik kami ucapkan terima kasih.</p>

        //                                                    <p>Wassalamu’alaikum Wr Wb</p>

        //                                                    <b>Policy Owner Service</b>
        //                                                    </body>
        //                                                    </html>";

        //            string dari = "no-reply@takaful.com";
        //            string password = "@Tk2017";
        //            conn.QueryString = "select * from EMAIL_RECIPIENT_POS where is_active = 1";
        //            conn.ExecuteQuery();

        //            string tujuan = "";

        //            if (conn.GetRowCount() > 0)
        //            {
        //                DataTable dt = conn.GetDataTable().Copy();

        //                var emails = dt.AsEnumerable()
        //                               .Select(dr => dr["EMAIL"]?.ToString().Trim())
        //                               .Where(email => !string.IsNullOrEmpty(email));

        //                tujuan = string.Join(";", emails);
        //            }
        //            //conn.QueryString = "select * from EMAIL_RECIPIENT_POS where is_active = 1";
        //            //conn.ExecuteQuery();
        //            //string tujuan = "";
        //            //if (conn.GetRowCount() > 0)
        //            //{
        //            //    DataTable dt = new DataTable();
        //            //    dt = conn.GetDataTable().Copy();
        //            //    foreach (DataRow dr in  dt.Rows)
        //            //    {
        //            //        if (!tujuan.Equals("")) tujuan += ",";
        //            //        tujuan += dr["EMAIL"].ToString().Trim();
        //            //    }
        //            //}
        //            ////string tujuan = "endi.octaviano@mitra.takaful.com,ferdiansyah.pratama@mitra.takaful.com,pos-atk@takaful.com";
        //            string pathFile = fullPath;
        //            string smtpServer = "smtpatk.takaful.com";
        //            int smtpPort = 587;
        //            using (MailMessage mail = new MailMessage())
        //            {
        //                // FROM
        //                mail.From = new MailAddress(dari);

        //                // TO (multiple email dari string "email1;email2;...")
        //                foreach (var email in tujuan.Split(';'))
        //                {
        //                    if (!string.IsNullOrWhiteSpace(email))
        //                        mail.To.Add(email.Trim());
        //                }

        //                // SUBJECT & BODY
        //                mail.Subject = "TOP UP IRREGULAR - " + noPolis + " (" + namaPemegang + ")";
        //                mail.Body = emailBody;
        //                mail.IsBodyHtml = true;

        //                // ATTACHMENT
        //                if (!string.IsNullOrEmpty(pathFile))
        //                {
        //                    if (File.Exists(pathFile)) // biar aman
        //                    {
        //                        Attachment att = new Attachment(pathFile);
        //                        mail.Attachments.Add(att);
        //                    }
        //                }

        //                using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
        //                {
        //                    smtp.Credentials = new NetworkCredential(dari, password);
        //                    smtp.EnableSsl = true;
        //                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //                    smtp.Timeout = 20000;

        //                    try
        //                    {
        //                        smtp.Send(mail);
        //                        Console.WriteLine("Email berhasil dikirim.");
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine("Gagal mengirim email: " + ex.ToString());
        //                    }
        //                }
        //            }

        //            //using (MailMessage mail = new MailMessage(dari, tujuan))
        //            //{
        //            //    mail.Subject = "TOP UP IRREGULAR - " + noPolis + " (" + namaPemegang + ")";
        //            //    mail.Body = emailBody;
        //            //    mail.IsBodyHtml = true;
        //            //    //mail.CC.Add(new MailAddress("blabla"));

        //            //    if (!string.IsNullOrEmpty(pathFile))
        //            //    {
        //            //        Attachment att = new Attachment(pathFile);
        //            //        mail.Attachments.Add(att);
        //            //    }

        //            //    using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
        //            //    {
        //            //        smtp.Credentials = new NetworkCredential(dari, password);
        //            //        smtp.EnableSsl = true;

        //            //        try
        //            //        {
        //            //            smtp.Send(mail);
        //            //            Console.WriteLine("Email berhasil dikirim.");
        //            //        }
        //            //        catch (Exception ex)
        //            //        {
        //            //            Console.WriteLine("Gagal mengirim email: " + ex.Message);
        //            //        }
        //            //    }
        //            //}
        //            if (File.Exists(fullPath))
        //                File.Delete(fullPath);
        //        }
        //        catch (Exception exMail)
        //        {
        //            ScriptManager.RegisterStartupScript(this, GetType(), "swalMail",
        //                "Swal.fire({ position: 'center', icon: 'warning', title: 'Email tidak terkirim', text: '" + exMail.Message.Replace("'", "\\'") + "', showConfirmButton: true });", true);
        //        }
        //    }
        //}

        protected void BT_QUO_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'EndorsementUW.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
        }

        private bool VerificationBlacklist(string regno)
        {
            bool result = false;
            string warning = "";
            conn.QueryString = "exec SP_APPLICATION_MASTER '" + regno + "'";
            conn.ExecuteQuery();

            string MAIN_INSURED_BLOCK_STATUS = conn.GetFieldValue("MAIN_INSURED_BLOCK_STATUS").ToString();
            string POLICY_HOLDER_BLOCK_STATUS = conn.GetFieldValue("POLICY_HOLDER_BLOCK_STATUS").ToString();
            string flag = conn.GetFieldValue("FLAG").ToString().ToLower();
            string source = conn.GetFieldValue("SOURCE").ToString().ToLower();

            if (!string.IsNullOrEmpty(flag) && flag == "black" || !string.IsNullOrEmpty(source) && source == "fraud")
            {
                result = true;
                if (!string.IsNullOrEmpty(MAIN_INSURED_BLOCK_STATUS) && !string.IsNullOrWhiteSpace(MAIN_INSURED_BLOCK_STATUS))
                {
                    if (warning == "")
                    {
                        warning = MAIN_INSURED_BLOCK_STATUS;
                    }
                    else
                    {
                        warning += MAIN_INSURED_BLOCK_STATUS + "<br>";
                    }
                }

                if (!string.IsNullOrEmpty(POLICY_HOLDER_BLOCK_STATUS) && !string.IsNullOrWhiteSpace(POLICY_HOLDER_BLOCK_STATUS))
                {
                    if (warning == "")
                    {
                        warning = POLICY_HOLDER_BLOCK_STATUS;
                    }
                    else
                    {
                        warning += POLICY_HOLDER_BLOCK_STATUS + "<br>";
                    }
                }
            }

            return result;
        }
        protected string ShowBlacklist()
        {
            string warning = "";

            conn.QueryString = "exec SP_MEMBER_BLACKLIST '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            string BENEFICIARY_BLACKLISTED = conn.GetFieldValue("BENEFICIARY_BLACKLISTED").ToString();
            string MAIN_INSURED_BLACKLISTED = conn.GetFieldValue("MAIN_INSURED_BLACKLISTED").ToString();
            string POLICY_HOLDER_BLACKLISTED = conn.GetFieldValue("POLICY_HOLDER_BLACKLISTED").ToString();
            string RISKY_CUSTOMER = conn.GetFieldValue("RISKY_CUSTOMER").ToString();
            string FLAG = conn.GetFieldValue("FLAG").ToString();
            string SOURCE = conn.GetFieldValue("SOURCE").ToString();

            if (!string.IsNullOrEmpty(BENEFICIARY_BLACKLISTED) && !string.IsNullOrWhiteSpace(BENEFICIARY_BLACKLISTED))
            {
                warning += BENEFICIARY_BLACKLISTED + "<br/>";
            }

            if (!string.IsNullOrEmpty(MAIN_INSURED_BLACKLISTED) && !string.IsNullOrWhiteSpace(MAIN_INSURED_BLACKLISTED))
            {
                if (warning == "")
                {
                    warning = MAIN_INSURED_BLACKLISTED + "<br/>";
                }
                else
                {
                    warning += MAIN_INSURED_BLACKLISTED + "<br/>";
                }
            }

            if (!string.IsNullOrEmpty(POLICY_HOLDER_BLACKLISTED) && !string.IsNullOrWhiteSpace(POLICY_HOLDER_BLACKLISTED))
            {
                if (warning == "")
                {
                    warning = POLICY_HOLDER_BLACKLISTED + "<br/>";
                }
                else
                {
                    warning += POLICY_HOLDER_BLACKLISTED + "<br/>";
                }
            }

            if (!string.IsNullOrEmpty(RISKY_CUSTOMER) && !string.IsNullOrWhiteSpace(RISKY_CUSTOMER))
            {
                if (warning == "")
                {
                    warning = RISKY_CUSTOMER + "<br/>";
                }
                else
                {
                    warning += RISKY_CUSTOMER + "<br/>";
                }
            }

          
            return warning;
        }
    }
}