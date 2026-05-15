using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE_PROPOSAL
{
    public partial class QuotationREGNO : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");
                Setup();
                LoadQuotation();
            }
        }

        protected void Setup()
        {
            LB_REGNO.Text = Request.QueryString["REGNO"].ToString();

            conn.QueryString = "select " +
                                "c.CODE, " +
                                "FULLNAME = UPPER(LTRIM(isnull(c.FRONT_NAME,'')) + RTRIM(' ' + isnull(c.MID_NAME,'')) + RTRIM(' ' + isnull(c.LAST_NAME,''))) + ' - ' + c.code " +
                                "from QUOTATION_MASTER a " +
                                "inner join V_LINK_GLIFE_POLICY_CHANNEL_DISTRIBUTION b on a.POLICY_ID = b.ID " +
                                "inner join V_LINK_MARKETING_M_AGENTS c on b.SUBCD = c.SUBCD " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_AGENT.Items.Clear();
            DDL_AGENT.Items.Add(new ListItem("", ""));

            // test
            //if (GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "99" || GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") == "25")
            //{
            //    DDL_AGENT.Items.Add(new ListItem(GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"), GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID")));
            //}
            //else
            //{
            //    //if (DDL_BRANCH.SelectedValue != null)
            //    //{
            //    //    try
            //    //    {
            //    //        DDL_AGENT.SelectedValue = conn.GetFieldValue("USERBY").ToString();
            //    //    }
            //    //    catch { }
            //    //}
            //    //else
            //    //{
            //    //    DDL_AGENT.Items.Add(new ListItem("", ""));
            //    //}
            //    DDL_AGENT.Items.Add(new ListItem("", ""));
            //}

            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_AGENT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            //RoleCheck();
        }

        //protected void RoleCheck()
        //{
        //    string role = GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles");

        //    switch (role)
        //    {
        //        case "99": DDL_AGENT.Enabled = false; break;
        //    }
        //}


        protected void CheckConfirmed()
        {
            conn.QueryString = "select REGNO from V_LINK_GLIFE_APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                DDL_BRANCH.Enabled = false;
                DDL_AGENT.Enabled = false;

                BT_MAINSAVE.Visible = false;
                DDL_DEFINE.Visible = false;
                TXT_SUMINS.Enabled = false;
                TXT_STARTDATE.Enabled = false;
                DDL_TY.Enabled = false;
                DDL_TM.Enabled = false;
                DDL_TD.Enabled = false;
                TR_ENDDATE.Visible = true;
                TXT_ENDDATE.Enabled = false;

                TXT_REMARK.Visible = false;
                BT_REMARK_SAVE.Visible = false;

                for (int i = 0; i < DGR_BENEFIT.Columns.Count; i++)
                {
                    DGR_BENEFIT.Columns[i].Visible = false;
                    switch (i)
                    {
                        case 1: DGR_BENEFIT.Columns[i].Visible = true; break;
                        case 2: DGR_BENEFIT.Columns[i].Visible = true; break;
                        case 3: DGR_BENEFIT.Columns[i].Visible = true; break;
                    }
                }
            }
            else
            {
                if (LB_TRACK.Text != "1")
                {
                    conn.QueryString = "select DESCR from PARAM_TRACK where TIPE_CODE='NB' and SEQ = " + LB_TRACK.Text + "-1";
                    conn.ExecuteQuery();
                    BT_BACK.Text = "RETURN TO " + conn.GetFieldValue("DESCR").ToString();
                    BT_BACK.Visible = true;
                }
            }
        }

        protected void LoadQuotation()
        {
            DV_ENTRY.Visible = true;

            FillDDLBranch();

            conn.QueryString = "select " +
                                "REGNO, " +
                                "FULLNAME, " +
                                "DOB = convert(varchar(20),DOB,106), " +
                                "SEX = (case when SEX='M' then 'MALE' else 'FEMALE' end), " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "TC_DESCR, " +
                                "BRANCH_CODE, " +
                                "USERBY " +
                                "from V_QUOTATION_MASTER " +
                                "where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_GENDER.Text = conn.GetFieldValue("SEX").ToString();
            LB_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("TC_DESCR").ToString();

            try
            {
                DDL_BRANCH.SelectedValue = conn.GetFieldValue("BRANCH_CODE").ToString();
            }
            catch { }

            try
            {
                DDL_AGENT.SelectedValue = conn.GetFieldValue("USERBY").ToString();
            }
            catch { }

            LoadMainInfo();
            CheckConfirmed();
        }

        protected void LoadJoinLife()
        {
            DDL_QUESTION.Items.Clear();
            DDL_QUESTION.Items.Add(new ListItem(LB_NAME.Text, "0"));

            conn.QueryString = "select a.REGNO " +
                                "from QUOTATION_MASTER a " +
                                "inner join V_LINK_UW_TC_ITEMS b on b.TC_ITEM = '22' and b.TC_CODE = a.TC_ID and b.VAL = '1' " +
                                "where a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;


            DV_JOIN.Visible = true;
            conn.QueryString = "exec SP_APPLICATION_JOIN_ACCOUNT '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_JOIN.DataSource = dt;
            DGR_JOIN.DataBind();

            conn.QueryString = "select CODE,DESCR from PR_MEMBER_RELATIONSHIP order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < DGR_JOIN.Items.Count; i++)
            {
                TextBox txtNAME = (TextBox)DGR_JOIN.Items[i].FindControl("TXT_JOINFULLNAME");
                TextBox txtDOB = (TextBox)DGR_JOIN.Items[i].FindControl("TXT_JOINDOB");
                DropDownList ddlSEX = (DropDownList)DGR_JOIN.Items[i].FindControl("DDL_JOINSEX");
                DropDownList ddlRELATION = (DropDownList)DGR_JOIN.Items[i].FindControl("DDL_JOINRELATION");
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlRELATION.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                txtNAME.Text = DGR_JOIN.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtDOB.Text = DGR_JOIN.Items[i].Cells[4].Text.Replace("&nbsp;", "");

                try
                {
                    ddlSEX.SelectedValue = DGR_JOIN.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                }
                catch { }

                try
                {
                    ddlRELATION.SelectedValue = DGR_JOIN.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                }
                catch { }

                if (txtNAME.Text.Trim() != "")
                {
                    DDL_QUESTION.Items.Add(new ListItem(txtNAME.Text.Trim(), DGR_JOIN.Items[i].Cells[0].Text));
                }
            }

            if (DDL_QUESTION.Items.Count > 1)
                TBL_QUESTION.Visible = true;

        }

        protected void FillDDLBranch()
        {
            conn.QueryString = "select " +
                                "c.BRANCH_CODE, " +
                                "c.NAMA_CABANG " +
                                "from QUOTATION_MASTER a " +
                                "inner join V_LINK_GLIFE_POLICY b on a.POLICY_ID = b.ID " +
                                "inner join V_LINK_CB_BRANCH c on b.COMPANY_CODE = c.COMPANY_CODE " +
                                "where a.REGNO = '" + LB_REGNO.Text + "'";


            conn.ExecuteQuery();

            DDL_BRANCH.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BRANCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadMainInfo()
        {
            conn.QueryString = "select " +
                                "SUMINS = replace(convert(varchar(100), convert(money,SUMINS),1), '.00',''), " +
                                "START_AGE, " +
                                "START_DATE = convert(varchar(20),START_DATE,103), " +
                                "END_DATE = convert(varchar(20),END_DATE,103), " +
                                "UW_CODE = (case when UW_CODE='AC' then 'AUTOMATIC COVER' when UW_CODE='NM' then 'NON MEDICAL' else 'MEDICAL - '+UW_CODE end), " +
                                "PREMIUM = replace(convert(varchar(100), convert(money,isnull(PREMIUM,0)),1), '.00',''), " +
                                "a.LAST_TRACK " +
                                "from V_QUOTATION_MASTER a " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetFieldValue("SUMINS").ToString() != "")
            {
                LB_TRACK.Text = conn.GetFieldValue("LAST_TRACK").ToString();
                TXT_SUMINS.Text = conn.GetFieldValue("SUMINS").ToString();
                TXT_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
                TXT_ENDDATE.Text = conn.GetFieldValue("END_DATE").ToString();
                LB_AGE.Text = conn.GetFieldValue("START_AGE").ToString();
                LB_UWCODE.Text = conn.GetFieldValue("UW_CODE").ToString();
                LB_PREMIUM.Text = conn.GetFieldValue("PREMIUM").ToString();

                LoadBenefit();
                LoadAccumulation();
                LoadRemark();
                LoadDocument();
                LoadTenor();
                LoadJoinLife();
                LoadQuestion();
            }
        }

        protected void LoadQuestion()
        {
            DV_QUESTIONS.Visible = true;

            conn.QueryString = "exec SP_APPLICATION_TC_QUESTIONS '" + LB_REGNO.Text + "'," + DDL_QUESTION.SelectedValue;
            conn.ExecuteQuery();
            DGR_QUESTION.DataSource = conn.GetDataTable().Copy();
            DGR_QUESTION.DataBind();

            for (int j = 0; j < DGR_QUESTION.Items.Count; j++)
            {
                Label lbDESCR = (Label)DGR_QUESTION.Items[j].FindControl("LB_DESCR");
                DropDownList ddl = (DropDownList)DGR_QUESTION.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR_QUESTION.Items[j].FindControl("TXT_VAL");
                Label lbQUESTION = (Label)DGR_QUESTION.Items[j].FindControl("LB_QUESTION");
                TextBox txtNEXTVAL = (TextBox)DGR_QUESTION.Items[j].FindControl("TXT_NEXTVAL");
                Label lbUNCHECKED = (Label)DGR_QUESTION.Items[j].FindControl("LB_UNCHECKED");
                System.Web.UI.HtmlControls.HtmlTableRow trQUESTION = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_QUESTION.Items[j].FindControl("TR_NEXTQUESTION");

                lbDESCR.Text = DGR_QUESTION.Items[j].Cells[6].Text.Replace("&nbsp;", "");

                if (DGR_QUESTION.Items[j].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    trQUESTION.Visible = true;
                    lbQUESTION.Visible = true;
                    txtNEXTVAL.Visible = true;
                    lbQUESTION.Text = DGR_QUESTION.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                    txtNEXTVAL.Text = DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                }

                if (DGR_QUESTION.Items[j].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    ddl.Visible = true;
                    conn.QueryString = DGR_QUESTION.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    for (int k = 0; k < conn.GetRowCount(); k++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                    try
                    {
                        ddl.SelectedValue = DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
                else
                {
                    txtVAL.Visible = true;
                    switch (DGR_QUESTION.Items[j].Cells[1].Text)
                    {
                        case "STR": txtVAL.Text = DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                            break;
                        case "INT": txtVAL.Text = DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                            txtVAL.Style["text-align"] = "center";
                            txtVAL.Width = 50;
                            break;
                        case "FLO": try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                txtVAL.Style["text-align"] = "right";
                            }
                            catch { }
                            break;
                        case "BIT": txtVAL.Visible = false;
                            ddl.Visible = true;
                            ddl.Items.Add(new ListItem("YES", "1"));
                            ddl.Items.Add(new ListItem("NO", "0"));
                            try
                            {
                                ddl.SelectedValue = DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                            break;

                    }
                }

                bool bCompleted = true;
                if (DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "") == "")
                    bCompleted = false;
                //if (DGR_QUESTION.Items[j].Cells[3].Text.Replace("&nbsp;", "") != "" && DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "") == "")
                //    bCompleted = false;
                if (!bCompleted)
                {
                    lbUNCHECKED.Visible = true;
                    DGR_QUESTION.Items[j].Cells[DGR_QUESTION.Columns.Count - 1].BackColor = System.Drawing.Color.Pink;
                }
            }
        }

        protected void LoadBenefit()
        {
            conn.QueryString = "exec SP_APPLICATION_BENEFIT '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();

            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                TextBox txtSUMINS = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_BENEFITSUMINS");
                DropDownList ddlSUMINS = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_BENEFITSUMINS");
                TextBox txtRATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_BENEFITRATE");
                TextBox txtENDDATE = (TextBox)DGR_BENEFIT.Items[i].FindControl("TXT_BENEFITENDDATE");

                txtSUMINS.Text = DGR_BENEFIT.Items[i].Cells[2].Text;
                txtENDDATE.Text = DGR_BENEFIT.Items[i].Cells[3].Text;
                txtRATE.Text = DGR_BENEFIT.Items[i].Cells[4].Text;

                if (DGR_BENEFIT.Items[i].Cells[DGR_BENEFIT.Columns.Count - 1].Text.Replace("&nbsp;", "") != "")
                {
                    txtSUMINS.Visible = false;
                    ddlSUMINS.Visible = true;

                    //try
                    //{
                    conn.QueryString = DGR_BENEFIT.Items[i].Cells[DGR_BENEFIT.Columns.Count - 1].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    ddlSUMINS.Items.Clear();
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        ddlSUMINS.Items.Add(new ListItem(conn.GetFieldValue(j, 0).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    }

                    ddlSUMINS.SelectedValue = DGR_BENEFIT.Items[i].Cells[2].Text;
                    //}
                    //catch { }
                }
            }

            switch (LB_TRACK.Text)
            {
                case "1": DGR_BENEFIT.Columns[6].Visible = false; break;
            }
        }

        protected void LoadAccumulation()
        {
            conn.QueryString = "exec SP_APPLICATION_SUMINS_ACCUMULATION '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ACCU.DataSource = dt;
            DGR_ACCU.DataBind();

            for (int i = 0; i < DGR_ACCU.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR_ACCU.Items[i].FindControl("LBT_REGNO");
                lbt.Text = DGR_ACCU.Items[i].Cells[1].Text;
            }
        }

        protected void LoadRemark()
        {
            DV_REMARK.Visible = true;

            conn.QueryString = "select " +
                                "a.SEQ, " +
                                "REMARK = '<B>' + UPPER(a.CREATEBY) + ' - [' + convert(varchar(50),a.CREATEDATE) + ']</B><BR>' + a.REMARK " +
                                "from APPLICATION_REMARK a " +
                                "where a.REGNO = '" + LB_REGNO.Text + "' order by a.CREATEDATE desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REMARK.DataSource = dt;
            DGR_REMARK.DataBind();

            for (int i = 0; i < DGR_REMARK.Items.Count; i++)
            {
                //TextBox txtREMARK = (TextBox)DGR_REMARK.Items[i].FindControl("TXT_REMARK");
                //txtREMARK.Text = DGR_REMARK.Items[i].Cells[1].Text.Trim().Replace("&nbsp;", "");
            }
        }

        protected void LoadDocument()
        {
            conn.QueryString = "select " +
                                "a.DOCTYPE, " +
                                "aa.DESCR, " +
                                "RECEIVE_DATE = b.CREATEDATE, " +
                                "b.CODE, " +
                                "b.NAMAFILE " +
                                "from APPLICATION_MEDICAL_DOCUMENT a " +
                                "inner join V_LINK_UW_PR_UW_REQUIRED_DOCUMENT aa on a.DOCTYPE = aa.CODE " +
                                "left join V_LINK_ARCHIEVE b on a.REGNO = b.OWNER1 and a.DOCTYPE = b.OWNER2 " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            //if (conn.GetRowCount() == 0)
            //    return;

            DV_DOCS.Visible = true;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DOCS.DataSource = dt;
            DGR_DOCS.DataBind();

            for (int i = 0; i < DGR_DOCS.Items.Count; i++)
            {
                Label lbDESCR = (Label)DGR_DOCS.Items[i].FindControl("LB_DESCR");
                LinkButton lbtDESCR = (LinkButton)DGR_DOCS.Items[i].FindControl("LBT_DESCR");
                LinkButton lbtDELETE = (LinkButton)DGR_DOCS.Items[i].FindControl("LBT_DELETE");

                if (DGR_DOCS.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lbtDESCR.Visible = true;
                    lbtDESCR.Text = DGR_DOCS.Items[i].Cells[1].Text + "<BR>" +
                                    "<table style='border-spacing:0px;width:100%;'>" +
                                    "<tr><td>Filename<td>:</td></td><td>" + DGR_DOCS.Items[i].Cells[3].Text + "</td></tr>" +
                                    "<tr><td>Upload date</td><td>:</td><td>" + DGR_DOCS.Items[i].Cells[4].Text + "</td></tr>" +
                                    "</table>";
                }
                else
                {
                    lbtDELETE.Visible = false;
                    lbDESCR.Visible = true;
                    lbDESCR.Text = DGR_DOCS.Items[i].Cells[1].Text;
                }
            }


            TXT_ARCHIEVE.Text = "";
            conn.QueryString = "select CODE, REMARK, NAMAFILE from " +
                                "ARCHIEVE.dbo.GL_ARSIP " +
                                "where " +
                                "OWNER1 = '" + LB_REGNO.Text + "' " +
                                "and TIPE='GL_03'";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ARCHIEVE.DataSource = dt;
            DGR_ARCHIEVE.DataBind();

            for (int i = 0; i < DGR_ARCHIEVE.Items.Count; i++)
            {
                LinkButton lbtDESCR = (LinkButton)DGR_ARCHIEVE.Items[i].FindControl("LBT_DOWNLOAD");
                lbtDESCR.Text = DGR_ARCHIEVE.Items[i].Cells[2].Text;
            }
        }



        protected void BT_MAINSAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            string enddate = GlobalUse.GlobalDateFormat(TXT_ENDDATE.Text.Trim(), "d/M/yyyy");
            if (DDL_DEFINE.SelectedValue == "T")
            {
                conn.QueryString = "exec SSP_ENDDATE_YMD " +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    DDL_TY.SelectedValue + "," +
                                    DDL_TM.SelectedValue + "," +
                                    DDL_TD.SelectedValue;
                conn.ExecuteQuery();

                enddate = conn.GetFieldValue("END_DATE").ToString();
            }

            try
            {
                conn.QueryString = "exec SP_APPLICATION_MAIN_INFO_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + TXT_SUMINS.Text.Trim().Replace(",", "") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + enddate + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                //SendEmailCQ();
                Response.Redirect("Quotation.aspx?MEMBERID=&ID=" + LB_REGNO.Text);
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }

        protected void DGR_DOCS_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from ARCHIEVE.dbo.GL_ARSIP where CODE='" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                LoadDocument();
            }

            if (e.CommandName == "Download")
            {
                string filename = e.Item.Cells[3].Text.Replace(" ", "");
                GlobalUse.SQLToFile(filename.Trim(),
                                    "select THEFILE from ARCHIEVE.dbo.GL_ARSIP where CODE='" + e.Item.Cells[2].Text + "'",
                                    Page);
            }

            if (e.CommandName == "Upload")
            {
                FileUpload fu = (FileUpload)e.Item.FindControl("FU_DOC");
                if (fu.HasFile)
                {

                    string filename = Path.GetFileName(fu.FileName);
                    string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }

                    fu.SaveAs(fullpath);
                    conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
                    conn.ExecuteQuery();
                    string code = conn.GetFieldValue(0, 0).ToString();
                    string SQL = "delete from ARCHIEVE.dbo.GL_ARSIP where OWNER1 = '" + LB_REGNO.Text + "' and OWNER2 = '" + e.Item.Cells[0].Text + "' " +
                                    "insert into ARCHIEVE.dbo.GL_ARSIP values (" +
                                    "'" + code + "'," +
                                    "'GL_01'," +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "null," +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "'" + filename + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                                    "@File)";
                    GlobalUse.FileToSQL(fullpath, SQL);

                    if (File.Exists(fullpath))
                        File.Delete(fullpath);

                    LoadDocument();
                }
            }
        }

        protected void DGR_REMARK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from APPLICATION_REMARK where " +
                                        "REGNO = '" + LB_REGNO.Text + "' " +
                                        "and SEQ=" + e.Item.Cells[0].Text + " " +
                                        "and CREATEBY='" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    LoadRemark();
                }
                catch { }
            }
        }

        protected void DDL_DEFINE_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DDL_DEFINE.SelectedValue)
            {
                case "T": TR_TENOR.Visible = true;
                    TR_ENDDATE.Visible = false;
                    LoadTenor();
                    break;
                case "E": TR_TENOR.Visible = false;
                    TR_ENDDATE.Visible = true;
                    break;
            }
        }

        protected void LoadTenor()
        {
            if (LB_REGNO.Text == "")
                return;

            DDL_TY.SelectedIndex = 0;
            DDL_TM.SelectedIndex = 0;
            DDL_TD.SelectedIndex = 0;

            try
            {
                conn.QueryString = "exec SP_APPLICATION_MAIN_TENOR '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                DDL_TY.SelectedValue = conn.GetFieldValue("Y").ToString();
                DDL_TM.SelectedValue = conn.GetFieldValue("M").ToString();
                DDL_TD.SelectedValue = conn.GetFieldValue("D").ToString();
            }
            catch { }
        }

        protected void BT_REMARK_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                if (TXT_REMARK.Text.Trim() == "")
                    return;

                conn.QueryString = "exec SP_APPLICATION_REMARK_INSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + TXT_REMARK.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                LoadRemark();
                TXT_REMARK.Text = "";
            }
            catch { }
        }

        protected void DGR_ACCU_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("Quotation.aspx?ID=" + e.Item.Cells[0].Text + "&MEMBERID=");
            }
        }

        protected void BT_JOINSAVE_Click(object sender, EventArgs e)
        {
            SaveJoinLife();
            LoadJoinLife();
        }

        protected void SaveJoinLife()
        {
            conn.QueryString = "delete from APPLICATION_JOIN_ACCOUNT where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_JOIN.Items.Count; i++)
            {
                TextBox txtNAME = (TextBox)DGR_JOIN.Items[i].FindControl("TXT_JOINFULLNAME");
                TextBox txtDOB = (TextBox)DGR_JOIN.Items[i].FindControl("TXT_JOINDOB");
                DropDownList ddlSEX = (DropDownList)DGR_JOIN.Items[i].FindControl("DDL_JOINSEX");
                DropDownList ddlRELATION = (DropDownList)DGR_JOIN.Items[i].FindControl("DDL_JOINRELATION");

                if (txtNAME.Text.Trim() != "" && txtDOB.Text.Trim() != "")
                {
                    try
                    {
                        conn.QueryString = "exec SP_APPLICATION_JOIN_ACCOUNT_INSERT " +
                                            "'" + LB_REGNO.Text + "'," +
                                            "'" + txtNAME.Text.Trim() + "'," +
                                            "'" + ddlRELATION.SelectedValue + "'," +
                                            "'" + ddlSEX.SelectedValue + "'," +
                                            "'" + GlobalUse.GlobalDateFormat(txtDOB.Text.Trim(), "d/M/yyyy") + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }
        }

        protected void DGR_JOIN_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                TextBox txtNAME = (TextBox)e.Item.FindControl("TXT_JOINFULLNAME");
                txtNAME.Text = "";
                SaveJoinLife();
                LoadJoinLife();
            }
        }

        protected void DGR_ACCU_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "select " +
                                    "ACCUMULATION = replace(convert(varchar(100),convert(money,SUM(a.SUMINS)),1),'.00','') " +
                                    "from APPLICATION_SUMINS_ACCUMULATION a " +
                                    "where a.REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[2].Text = "TOTAL ACCUMULATION";
                e.Item.Cells[6].Text = conn.GetFieldValue("ACCUMULATION").ToString();
            }
        }

        protected void BT_QUESTIONSAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from APPLICATION_TC_QUESTIONS where REGNO = '" + LB_REGNO.Text + "' and SEQ = " + DDL_QUESTION.SelectedValue;
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_QUESTION.Items.Count; i++)
            {
                try
                {
                    DropDownList ddl = (DropDownList)DGR_QUESTION.Items[i].FindControl("DDL_REFF");
                    TextBox txtVAL = (TextBox)DGR_QUESTION.Items[i].FindControl("TXT_VAL");
                    TextBox txtNEXTVAL = (TextBox)DGR_QUESTION.Items[i].FindControl("TXT_NEXTVAL");

                    string val = txtVAL.Text.Trim();
                    if (DGR_QUESTION.Items[i].Cells[1].Text == "FLO")
                        val = val.Replace(",", "");
                    if (ddl.Visible)
                        val = ddl.SelectedValue;

                    conn.QueryString = "insert into APPLICATION_TC_QUESTIONS select " +
                                        "'" + LB_REGNO.Text + "'," +
                                        DDL_QUESTION.SelectedValue + "," +
                                        "'" + DGR_QUESTION.Items[i].Cells[0].Text + "'," +
                                        "'" + val + "'," +
                                        "'" + txtNEXTVAL.Text.Trim() + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            LoadQuestion();
        }

        protected void DDL_AGENT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_AGENT.SelectedValue == "")
                return;

            conn.QueryString = "update QUOTATION_MASTER set USERBY = '" + DDL_AGENT.SelectedValue + "' where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();
        }

        protected void DDL_BRANCH_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_BRANCH.SelectedValue == "")
                return;

            conn.QueryString = "update QUOTATION_MASTER set BRANCH_CODE = '" + DDL_BRANCH.SelectedValue + "' where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();
        }

        protected void DGR_ARCHIEVE_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from ARCHIEVE.dbo.GL_ARSIP where CODE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                LoadDocument();
            }

            if (e.CommandName == "Download")
            {
                string filename = e.Item.Cells[1].Text.Replace(" ", "");
                GlobalUse.SQLToFile(filename.Trim(),
                                    "select THEFILE from ARCHIEVE.dbo.GL_ARSIP where CODE='" + e.Item.Cells[0].Text + "'",
                                    Page);
            }
        }

        protected void LBT_ARCHIEVE_Click(object sender, EventArgs e)
        {
            if (TXT_ARCHIEVE.Text.Trim() == "")
                return;

            if (!FU_ARCHIEVE.HasFile)
                return;

            string filename = Path.GetFileName(FU_ARCHIEVE.FileName);
            string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            FU_ARCHIEVE.SaveAs(fullpath);
            conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
            conn.ExecuteQuery();
            string code = conn.GetFieldValue(0, 0).ToString();
            string SQL = "insert into ARCHIEVE.dbo.GL_ARSIP select " +
                            "ARCHIEVE.dbo.UFN_GET_NEWID()," +
                            "'GL_03'," +
                            "'" + LB_REGNO.Text + "'," +
                            "null," +
                            "null," +
                            "'" + TXT_ARCHIEVE.Text.Trim().Replace("'", "`") + "'," +
                            "'" + filename + "'," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                            "@File";
            GlobalUse.FileToSQL(fullpath, SQL);

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            LoadDocument();
        }

        protected void DGR_BENEFIT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                TextBox txtSUMINS = (TextBox)e.Item.FindControl("TXT_BENEFITSUMINS");
                DropDownList ddlSUMINS = (DropDownList)e.Item.FindControl("DDL_BENEFITSUMINS");
                TextBox txtRATE = (TextBox)e.Item.FindControl("TXT_BENEFITRATE");
                TextBox txtENDDATE = (TextBox)e.Item.FindControl("TXT_BENEFITENDDATE");

                string rate = txtRATE.Text.Trim().Replace(",", "");
                if (!DGR_BENEFIT.Columns[6].Visible)
                    rate = e.Item.Cells[4].Text.Trim().Replace(",", "");


                string sumins = txtSUMINS.Text.Replace(",", "");
                if (ddlSUMINS.Visible)
                    sumins = ddlSUMINS.SelectedValue.Replace(",", "");

                //try
                //{
                conn.QueryString = "exec SP_APPLICATION_BENEFIT_UPDATE " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + sumins + "'," +
                                    "'" + rate + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(txtENDDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                //}
                //catch { }

                LoadMainInfo();
            }
        }

        protected void BT_BACK_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_QUOTATION_MASTER_BACKTRACK '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            Response.Redirect("QuotationListVerify.aspx");
        }

        protected void DDL_QUESTION_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadQuestion();
        }

        protected void BT_SENDEMAIL_Click(object sender, EventArgs e)
        {
            SendEmailCQ();
            BT_SENDEMAIL.Visible = false;
        }

        protected void SendEmailCQ()
        {

            conn.QueryString = "select * from V_QUOTATION_MASTER a where a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();


            string uwcode = conn.GetFieldValue("UW_CODE").ToString();
            string fullname = conn.GetFieldValue("FULLNAME").ToString();
            string policyno = conn.GetFieldValue("POLICY_NO").ToString();
            string regono = conn.GetFieldValue("REGNO").ToString();
            string userby = conn.GetFieldValue("USERBY").ToString();

            conn.QueryString = "select * from V_LINK_MARKETING_M_AGENTS where CODE = '" + userby + "'";
            conn.ExecuteQuery();

            string email = conn.GetFieldValue("EMAIL").ToString();

            string sender = "no-reply@takaful.com";
            string recipient = "bancas.newbusiness@takaful.com, sakti.indrajaya@takaful.com, syahidrabbani@takaful.com, donny.hadisaputra@takaful.com, dea.habibie@takaful.com, nur.fadillah@takaful.com, aswin.adi@takaful.com";//conn.GetFieldValue("RECIPIENT").ToString();

            string body = " <p>Assalamu'alaikum Warahmatullahi Wabarakatuh&nbsp;</p> " +
                           " <p>Dear All&nbsp;</p> " +
                           " <p>Mohon bantuannya untuk memproses pengajuan berikut :</p> " +
                           " <table style='height: 112px; width: 386px; background-color: #808080; color: #fff'> " +
                           " <tbody> " +
                           " <tr> " +
                           " <td style='width: 183px;'>Regno</td> " +
                           " <td style='width: 10px;'>:</td> " +
                           " <td style='width: 171px;'>" + regono + "</td> " +
                           " </tr> " +
                           " <tr> " +
                           " <td style='width: 183px;'>Nama</td> " +
                           " <td style='width: 10px;'>:</td> " +
                           " <td style='width: 171px;'>" + fullname + "</td> " +
                           " </tr> " +
                           " <tr> " +
                           " <td style='width: 183px;'>Uang Pertanggungan</td> " +
                           " <td style='width: 10px;'>:</td> " +
                           " <td style='width: 171px;'>" + TXT_SUMINS.Text + "</td> " +
                           " </tr> " +
                           " <tr> " +
                           " <td style='width: 183px;'>U/W Code</td> " +
                           " <td style='width: 10px;'>:</td> " +
                           " <td style='width: 171px;'>" + uwcode + "</td> " +
                           " </tr> " +
                           " </tbody> " +
                           " </table> " +
                           " <p>Demikian, Terima Kasih.</p> " +
                           " <p>Wassalamu&rsquo;alaikum Warrahmatullahi Wabarakatuh</p>";

            string subject = "DATA INPUT BRO " + "_" + uwcode + "_" + DDL_AGENT.SelectedItem + "_" + policyno + "_" + fullname + "_" + regono;

            string CC = email;

            string BCC = "";

            string[] attachment = new string[0];
            GlobalUse.SendEmailCQ(sender, recipient, CC, BCC, subject, body, attachment);

        }
    }
}