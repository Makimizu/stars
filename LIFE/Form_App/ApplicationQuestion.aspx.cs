using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_App
{
    public partial class ApplicationQuestion : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LQ"));
        protected Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_GROUP.Text = Request.QueryString["GROUP"].ToString();
                LB_MEMBERID.Text = Request.QueryString["MEMBERID"].ToString();

                if (Request.QueryString["URL"].ToString() != "")
                    LB_URL.Text = Crypto.DecryptStringAES(Request.QueryString["URL"].ToString());

                Setup();
                LoadQuestion();
                CheckTrack();
            }
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "UW", "") > 2)
            {
                DGR_QUESTION.Enabled = false;
                DGR_QUESTION_CHILD.Enabled = false;
                BT_SAVE.Visible = false;
                BT_SAVE_SUB.Visible = false;
            }
        }

        protected void Setup()
        {
            DDL_MEMBER.Items.Clear();
            conn.QueryString = "exec SP_APPLICATION_QUESTION_MEMBER " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_GROUP.Text + "'," +
                                    "'" + LB_MEMBERID.Text + "'";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MEMBER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                if (conn.GetFieldValue(i, 2).ToString() == "1")
                {
                    TR_MEMBER.Visible = true;
                }
                else
                {
                    TR_MEMBER.Visible = false;
                }
            }
            /*
            if (LB_MEMBERID.Text == "")
            {
                TR_MEMBER.Visible = true;
                conn.QueryString = "select distinct " +
                                    "b.ID, " +
                                    "b.FULLNAME " +
                                    "from		APPLICATION_BENEFIT a " +
                                    "inner join	V_LINK_CB_MEMBER_MASTER b on a.MEMBER_ID = b.ID " +
                                    "where " +
                                    "a.REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_MEMBER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
            else
            {
                TR_MEMBER.Visible = false;
                conn.QueryString = "select distinct " +
                                    "b.ID, " +
                                    "b.FULLNAME " +
                                    "from		APPLICATION_MASTER a " +
                                    "inner join	V_LINK_CB_MEMBER_MASTER b on a.MEMBER_ID = b.ID " +
                                    "where " +
                                    "a.REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_MEMBER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
            */
        }

        protected void LoadQuestion()
        {
            conn.QueryString = "select DESCR = UPPER(DESCR) from UWBOX.dbo.PR_QUESTION_MODULAR_GROUP where CODE = '" + LB_GROUP.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();



            conn.QueryString = "exec SP_APPLICATION_QUESTION " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + DDL_MEMBER.SelectedValue + "'," +
                                "'" + LB_GROUP.Text + "'";
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
                Button btQ = (Button)DGR_QUESTION.Items[j].FindControl("BT_Q");

                lbDESCR.Text = DGR_QUESTION.Items[j].Cells[1].Text.Replace("&nbsp;", "");

                switch (DGR_QUESTION.Items[j].Cells[7].Text.Replace("&nbsp;", ""))
                {
                    case "-1": btQ.Visible = true; btQ.BackColor = System.Drawing.Color.Red; break;
                    case "1": btQ.Visible = true; btQ.BackColor = System.Drawing.Color.Blue; break;
                }

                if (DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "") == "")
                {
                    ddl.BackColor = System.Drawing.Color.LightYellow;
                    txtVAL.BackColor = System.Drawing.Color.LightYellow;
                }

                if (DGR_QUESTION.Items[j].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    trQUESTION.Visible = true;
                    lbQUESTION.Visible = true;
                    txtNEXTVAL.Visible = true;
                    lbQUESTION.Text = DGR_QUESTION.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                    txtNEXTVAL.Text = DGR_QUESTION.Items[j].Cells[6].Text.Replace("&nbsp;", "");
                }

                if (DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    ddl.Visible = true;
                    conn.QueryString = DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    for (int k = 0; k < conn.GetRowCount(); k++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                    try
                    {
                        ddl.SelectedValue = DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
                else
                {
                    txtVAL.Visible = true;
                    switch (DGR_QUESTION.Items[j].Cells[3].Text)
                    {
                        case "STR": txtVAL.Text = DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                            break;
                        case "INT": txtVAL.Text = DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                            txtVAL.Style["text-align"] = "center";
                            txtVAL.Width = 50;
                            break;
                        case "FLO": try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                txtVAL.Style["text-align"] = "right";
                            }
                            catch { }
                            break;
                        case "BIT": txtVAL.Visible = false;
                            ddl.Visible = true;
                            ddl.Items.Add(new ListItem("NO", "0"));
                            ddl.Items.Add(new ListItem("YES", "1"));
                            try
                            {
                                ddl.SelectedValue = DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                            break;

                    }
                }

                //bool bCompleted = true;
                //if (DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "") == "")
                //    bCompleted = false;
                ////if (DGR_QUESTION.Items[j].Cells[3].Text.Replace("&nbsp;", "") != "" && DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "") == "")
                ////    bCompleted = false;
                //if (!bCompleted)
                //{
                //    lbUNCHECKED.Visible = true;
                //    DGR_QUESTION.Items[j].Cells[DGR_QUESTION.Columns.Count - 1].BackColor = System.Drawing.Color.Pink;
                //}
            }
        }

        protected void DDL_MEMBER_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadQuestion();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from APPLICATION_QUESTION where REGNO = '" + LB_REGNO.Text + "' and QUESTION_GROUP = '" + LB_GROUP.Text + "' and MEMBER_ID = '" + DDL_MEMBER.SelectedValue + "'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_QUESTION.Items.Count; i++)
            {
                try
                {
                    DropDownList ddl = (DropDownList)DGR_QUESTION.Items[i].FindControl("DDL_REFF");
                    TextBox txtVAL = (TextBox)DGR_QUESTION.Items[i].FindControl("TXT_VAL");
                    TextBox txtNEXTVAL = (TextBox)DGR_QUESTION.Items[i].FindControl("TXT_NEXTVAL");

                    string val = txtVAL.Text.Trim().Replace("'", "`");
                    if (DGR_QUESTION.Items[i].Cells[1].Text == "FLO")
                        val = val.Replace(",", "");
                    if (ddl.Visible)
                        val = ddl.SelectedValue;

                    if (val.Trim().Length > 0)
                    {
                        conn.QueryString = "insert into APPLICATION_QUESTION select " +
                                            "'" + LB_REGNO.Text + "'," +
                                            "'" + DDL_MEMBER.SelectedValue + "'," +
                                            "'" + LB_GROUP.Text + "'," +
                                            "'" + DGR_QUESTION.Items[i].Cells[0].Text + "'," +
                                            "'" + val + "'," +
                                            "'" + txtNEXTVAL.Text.Trim() + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
                        conn.ExecuteNonQuery();
                    }

                }
                catch { }
            }

            LoadQuestion();
        }

        protected void LB_BACK_Click(object sender, EventArgs e)
        {
            TBL_PARENT.Visible = true;
            TBL_CHILD.Visible = false;
            LoadQuestion();
        }

        protected void DGR_QUESTION_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Child")
            {
                TBL_PARENT.Visible = false;
                TBL_CHILD.Visible = true;
                LB_CHILD_TITLE.Text = e.Item.Cells[1].Text.Replace("?", "").Trim().ToUpper();

                ShowButton(e.Item.Cells[0].Text);
            }
        }

        protected void ShowButton(string groupseq)
        {
            LB_QUESTION_GROUP_SEQ.Text = groupseq;

            conn.QueryString = "exec SP_APPLICATION_QUESTION_MODULAR " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + DDL_MEMBER.SelectedValue + "'," +
                                "'" + LB_GROUP.Text + "'," +
                                "'" + groupseq + "'";
            conn.ExecuteQuery();
            DGR_BUTTON.DataSource = conn.GetDataTable().Copy();
            DGR_BUTTON.DataBind();

            for (int i = 0; i < DGR_BUTTON.Items.Count; i++)
            {
                Button bt = (Button)DGR_BUTTON.Items[i].FindControl("BT_MODULAR");
                bt.Text = DGR_BUTTON.Items[i].Cells[1].Text;
            }

            FillDGRSub(DGR_BUTTON.Items[0].Cells[0].Text, DGR_BUTTON.Items[0].Cells[1].Text);
        }

        protected void FillDGRSub(string modularcode, string modulardescr)
        {
            LB_MODULARCODE.Text = modularcode;
            LB_MODULARDESCR.Text = modulardescr;

            conn.QueryString = "exec SP_APPLICATION_QUESTION_SUB " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + DDL_MEMBER.SelectedValue + "'," +
                                "'" + LB_GROUP.Text + "'," +
                                "'" + LB_QUESTION_GROUP_SEQ.Text + "'," +
                                "'" + modularcode + "'";
            conn.ExecuteQuery();
            DGR_QUESTION_CHILD.DataSource = conn.GetDataTable().Copy();
            DGR_QUESTION_CHILD.DataBind();

            for (int j = 0; j < DGR_QUESTION_CHILD.Items.Count; j++)
            {
                Label lbDESCR = (Label)DGR_QUESTION_CHILD.Items[j].FindControl("LB_DESCR");
                DropDownList ddl = (DropDownList)DGR_QUESTION_CHILD.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR_QUESTION_CHILD.Items[j].FindControl("TXT_VAL");
                Label lbQUESTION = (Label)DGR_QUESTION_CHILD.Items[j].FindControl("LB_QUESTION");
                TextBox txtNEXTVAL = (TextBox)DGR_QUESTION_CHILD.Items[j].FindControl("TXT_NEXTVAL");
                Label lbUNCHECKED = (Label)DGR_QUESTION_CHILD.Items[j].FindControl("LB_UNCHECKED");
                System.Web.UI.HtmlControls.HtmlTableRow trQUESTION = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_QUESTION_CHILD.Items[j].FindControl("TR_NEXTQUESTION");

                lbDESCR.Text = DGR_QUESTION_CHILD.Items[j].Cells[1].Text.Replace("&nbsp;", "");

                if (DGR_QUESTION_CHILD.Items[j].Cells[5].Text.Replace("&nbsp;", "") == "")
                {
                    ddl.BackColor = System.Drawing.Color.LightYellow;
                    txtVAL.BackColor = System.Drawing.Color.LightYellow;
                }

                if (DGR_QUESTION_CHILD.Items[j].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    trQUESTION.Visible = true;
                    lbQUESTION.Visible = true;
                    txtNEXTVAL.Visible = true;
                    lbQUESTION.Text = DGR_QUESTION_CHILD.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                    txtNEXTVAL.Text = DGR_QUESTION_CHILD.Items[j].Cells[6].Text.Replace("&nbsp;", "");
                }

                if (DGR_QUESTION_CHILD.Items[j].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    ddl.Visible = true;
                    conn.QueryString = DGR_QUESTION_CHILD.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    for (int k = 0; k < conn.GetRowCount(); k++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                    try
                    {
                        ddl.SelectedValue = DGR_QUESTION_CHILD.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
                else
                {
                    txtVAL.Visible = true;
                    switch (DGR_QUESTION_CHILD.Items[j].Cells[3].Text)
                    {
                        case "STR": txtVAL.Text = DGR_QUESTION_CHILD.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                            break;
                        case "INT": txtVAL.Text = DGR_QUESTION_CHILD.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                            txtVAL.Style["text-align"] = "center";
                            txtVAL.Width = 50;
                            break;
                        case "FLO": try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_QUESTION_CHILD.Items[j].Cells[5].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                txtVAL.Style["text-align"] = "right";
                            }
                            catch { }
                            break;
                        case "BIT": txtVAL.Visible = false;
                            ddl.Visible = true;
                            ddl.Items.Add(new ListItem("NO", "0"));
                            ddl.Items.Add(new ListItem("YES", "1"));
                            try
                            {
                                ddl.SelectedValue = DGR_QUESTION_CHILD.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                            break;

                    }
                }
            }
        }

        protected void DGR_BUTTON_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Modular")
            {
                LB_MODULARCODE.Text = e.Item.Cells[0].Text;
                FillDGRSub(e.Item.Cells[0].Text, e.Item.Cells[1].Text);
            }
        }

        protected void BT_SAVE_SUB_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from APPLICATION_QUESTION_SUB where REGNO = '" + LB_REGNO.Text + "' and QUESTION_GROUP = '" + LB_GROUP.Text + "' and QUESTION_GROUP_SEQ = '" + LB_QUESTION_GROUP_SEQ.Text + "' and MODULAR_CODE = '" + LB_MODULARCODE.Text + "' and MEMBER_ID = '" + DDL_MEMBER.SelectedValue + "'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_QUESTION_CHILD.Items.Count; i++)
            {
                try
                {
                    DropDownList ddl = (DropDownList)DGR_QUESTION_CHILD.Items[i].FindControl("DDL_REFF");
                    TextBox txtVAL = (TextBox)DGR_QUESTION_CHILD.Items[i].FindControl("TXT_VAL");
                    TextBox txtNEXTVAL = (TextBox)DGR_QUESTION_CHILD.Items[i].FindControl("TXT_NEXTVAL");

                    string val = txtVAL.Text.Trim().Replace("'", "`");
                    if (DGR_QUESTION_CHILD.Items[i].Cells[1].Text == "FLO")
                        val = val.Replace(",", "");
                    if (ddl.Visible)
                        val = ddl.SelectedValue;

                    if (val.Trim().Length > 0)
                    {
                        conn.QueryString = "insert into APPLICATION_QUESTION_SUB select " +
                                            "'" + LB_REGNO.Text + "'," +
                                            "'" + DDL_MEMBER.SelectedValue + "'," +
                                            "'" + LB_GROUP.Text + "'," +
                                            "'" + LB_QUESTION_GROUP_SEQ.Text + "'," +
                                            "'" + LB_MODULARCODE.Text + "'," +
                                            "'" + DGR_QUESTION_CHILD.Items[i].Cells[0].Text + "'," +
                                            "'" + val + "'," +
                                            "'" + txtNEXTVAL.Text.Trim() + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
                        conn.ExecuteNonQuery();
                    }

                }
                catch { }
            }

            FillDGRSub(LB_MODULARCODE.Text, LB_MODULARDESCR.Text);
        }
    }
}