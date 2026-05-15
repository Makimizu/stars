using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace GO
{
    public partial class UserList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillGrid();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select TOP (200) CODE, DESCR = UPPER(DESCR) from M_ROLES order by 2"; //"select CODE, DESCR = UPPER(DESCR) from M_ROLES order by 2";
            conn.ExecuteQuery();
            DDL_ROLE.Items.Clear();
            DDL_USERROLE.Items.Clear();
            DDL_ROLE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ROLE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_USERROLE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select TOP (200) CODE, NAME = CODE + ' - ' + NAME from M_BRANCH where COMPANY_CODE = '1' order by 1"; //"select CODE, NAME = CODE + ' - ' + NAME from M_BRANCH where COMPANY_CODE = '1' order by 1";
            conn.ExecuteQuery();
            DDL_BRANCH.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BRANCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            ShowUpliner("");
        }

        protected void FillGrid()
        {
            string where = "";
            LB_ERROR.Text = "";

            if (TXT_SEARCH.Text.Trim() != "")
                where = where + " and (CODE like '%" + TXT_SEARCH.Text.Trim() + "%' or isnull(FRONT_NAME,'')+isnull(MID_NAME,'')+isnull(LAST_NAME,'') like '%" + TXT_SEARCH.Text.Trim() + "%') ";

            if (DDL_ROLE.SelectedValue != "")
                where = where + " and ROLE_CODE = '" + DDL_ROLE.SelectedValue + "' ";

            if (DDL_LOCKED.SelectedValue != "")
                where = where + " and convert(int, LOCKED) = " + DDL_LOCKED.SelectedValue + " ";

            if (DDL_STATUS.SelectedValue != "")
                where = where + " and convert(int, ACTIVE) = " + DDL_STATUS.SelectedValue + " ";


            conn.QueryString = "select " +
                                "TOP (200)" +
                                "CODE, " +
                                "FULLNAME = LEFT(UPPER(LTRIM(isnull(FRONT_NAME,'')) + RTRIM(' ' + isnull(MID_NAME,'')) + RTRIM(' ' + isnull(LAST_NAME,''))), 50), " +
                                "ACTIVE = convert(int, ACTIVE), " +
                                "LOCKED = convert(int, LOCKED) " +
                                "from M_USERS " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by 2";
            conn.ExecuteQuery();
            DGR_LIST.DataSource = conn.GetDataTable().Copy();
            DGR_LIST.DataBind();

            LB_CNT.Text = "Records : " + conn.GetRowCount().ToString();

            for (int i = 0; i < DGR_LIST.Items.Count; i++)
            {
                LinkButton lbID = (LinkButton)DGR_LIST.Items[i].FindControl("LB_ID");
                LinkButton lbNAME = (LinkButton)DGR_LIST.Items[i].FindControl("LB_NAME");
                CheckBox rdSTAT = (CheckBox)DGR_LIST.Items[i].FindControl("RD_STAT");
                CheckBox rdLOCKED = (CheckBox)DGR_LIST.Items[i].FindControl("RD_LOCKED");


                lbID.Text = DGR_LIST.Items[i].Cells[0].Text;
                lbNAME.Text = DGR_LIST.Items[i].Cells[1].Text;

                if (DGR_LIST.Items[i].Cells[2].Text == "1")
                    rdSTAT.Checked = true;

                if (DGR_LIST.Items[i].Cells[3].Text == "1")
                    rdLOCKED.Checked = true;
            }
        }

        protected void RD_STAT_Change(object source, EventArgs e)
        {
            for (int i = 0; i < DGR_LIST.Items.Count; i++)
            {
                CheckBox rdSTAT = (CheckBox)DGR_LIST.Items[i].FindControl("RD_STAT");
                if (rdSTAT == (CheckBox)source)
                {
                    string stat = "1";
                    if (!rdSTAT.Checked)
                        stat = "0";

                    conn.QueryString = "update M_USERS set " +
                        "ACTIVE = " + stat + ", " +
                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                        "LASTCHANGEDATE = GETDATE() " +
                        "where CODE='" + DGR_LIST.Items[i].Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillGrid();
                    return;
                }
            }
        }

        protected void RD_LOCKED_Change(object source, EventArgs e)
        {
            for (int i = 0; i < DGR_LIST.Items.Count; i++)
            {
                CheckBox rdLOCKED = (CheckBox)DGR_LIST.Items[i].FindControl("RD_LOCKED");
                if (rdLOCKED == (CheckBox)source)
                {
                    string stat = "1";
                    if (!rdLOCKED.Checked)
                        stat = "0";

                    conn.QueryString = "update M_USERS set " +
                        "LOCKED = " + stat + ", " +
                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                        "LASTCHANGEDATE = GETDATE() " +
                        "where CODE='" + DGR_LIST.Items[i].Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillGrid();
                    return;
                }
            }
        }

        protected void DGR_LIST_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                ShowUser(e.Item.Cells[0].Text);
            }
        }

        protected void ShowUpliner(string code)
        {
            conn.QueryString = "select " +
                                "CODE, " +
                                "FULLNAME = UPPER(LTRIM(isnull(FRONT_NAME,'')) + RTRIM(' ' + isnull(MID_NAME,'')) + RTRIM(' ' + isnull(LAST_NAME,''))) " +
                                "from M_USERS " +
                                "where " +
                                "code <> '" + code + "' " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_UPLINER.Items.Clear();
            DDL_UPLINER.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_UPLINER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void ClearFields()
        {
            LB_ERROR.Text = "";
            TXT_CODE.Text = "";
            TXT_PASSWORD.Text = "";
            TXT_FRONTNAME.Text = "";
            TXT_LASTNAME.Text = "";
            TXT_MIDNAME.Text = "";
            TXT_DOB.Text = "";
            TXT_IDEMPLOYEE.Text = "";
            TXT_EMAIL.Text = "";
            TXT_TITLE.Text = "";

            TXT_CODE.ReadOnly = false;
            TBL_BINARY.Visible = false;
            DDL_ROLE.SelectedIndex = 0;
            DDL_BRANCH.SelectedIndex = 0;
            DDL_UPLINER.SelectedIndex = 0;
        }

        protected void ShowBinary(string code)
        {
            TBL_BINARY.Visible = true;
            conn.QueryString = "select " +
                                "SIGNATURE = isnull(a.SIGNATURE, b.DEFAULT_PROFILE_PIC), " +
                                "PHOTO = isnull(a.PHOTO, b.DEFAULT_PROFILE_PIC) " +
                                "from M_USERS a " +
                                "inner join SC_COMPANY b on b.CODE = '1' " +
                                "where " +
                                "a.CODE = '" + code + "'";

            try
            {
                IMG_PHOTO.ImageUrl = GlobalUse.GetStringImageURL(conn.QueryString, "PHOTO");
            }
            catch { }
            try
            {
                IMG_SIGNATURE.ImageUrl = GlobalUse.GetStringImageURL(conn.QueryString, "SIGNATURE");
            }
            catch { }
        }

        protected void ShowUser(string code)
        {
            TXT_CODE.ReadOnly = false;

            ClearFields();
            ShowUpliner(code);

            if (code.Trim() != "")
            {
                TXT_CODE.ReadOnly = true;
                ShowBinary(code);
            }

            conn.QueryString = "select " +
                                "a.CODE, " +
                                "PASSWORD, " +
                                "ROLE_CODE, " +
                                "FRONT_NAME = UPPER(FRONT_NAME), " +
                                "LAST_NAME = UPPER(LAST_NAME), " +
                                "MID_NAME = UPPER(MID_NAME), " +
                                "DOB = convert(varchar(20), BOD, 103), " +
                                "ID_EMPLOYEE, " +
                                "a.EMAIL, " +
                                "UPLINER, " +
                                "BRANCH_CODE, " +
                                "POSITION_TITLE, " +
                                "LOCKED = convert(int, LOCKED), " +
                                "ACTIVE = convert(int, ACTIVE) " +
                                "from M_USERS a " +
                                "where " +
                                "a.CODE = '" + code + "'";
            conn.ExecuteQuery();

            TXT_CODE.Text = conn.GetFieldValue("CODE").ToString();
            TXT_PASSWORD.Text = Crypto.DecryptStringAES(conn.GetFieldValue("PASSWORD").ToString());
            TXT_FRONTNAME.Text = conn.GetFieldValue("FRONT_NAME").ToString();
            TXT_LASTNAME.Text = conn.GetFieldValue("LAST_NAME").ToString();
            TXT_MIDNAME.Text = conn.GetFieldValue("MID_NAME").ToString();
            TXT_DOB.Text = conn.GetFieldValue("DOB").ToString();
            TXT_IDEMPLOYEE.Text = conn.GetFieldValue("ID_EMPLOYEE").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();
            TXT_TITLE.Text = conn.GetFieldValue("POSITION_TITLE").ToString();

            try
            {
                DDL_USERROLE.SelectedValue = conn.GetFieldValue("ROLE_CODE").ToString();
            }
            catch { }

            try
            {
                DDL_UPLINER.SelectedValue = conn.GetFieldValue("UPLINER").ToString();
            }
            catch { }

            try
            {
                DDL_BRANCH.SelectedValue = conn.GetFieldValue("BRANCH_CODE").ToString();
            }
            catch { }

        }

        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void DDL_ROLE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void DDL_STATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void DDL_LOCKED_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            string pwd = "";

            if (!Validate())
            {
                LB_ERROR.Text = "Some mandatory data has not been completed yet";
                return;
            }

            if (TXT_PASSWORD.Text.Trim() != "")
            {
                if (!ValidatePassword(TXT_PASSWORD.Text.Trim()))
                {
                    LB_ERROR.Text = "New Passwords are not matched criteria.<BR>" +
                                "Password Length should be minimum 8 characters.<BR>" +
                                "Password should be consisted by Alphanumeric (A-Z, a-z, 0-9) and Special Characters.";
                    return;
                }

                pwd = "PASSWORD = '" + Crypto.EncryptStringAES(TXT_PASSWORD.Text.Trim()) + "', ";
            }


            conn.QueryString = "select CODE from M_USERS where CODE = '" + TXT_CODE.Text.Trim() + "'";
            conn.ExecuteQuery();

            string upliner = "null";
            if (DDL_UPLINER.SelectedValue != "")
                upliner = "'" + DDL_UPLINER.SelectedValue + "'";

            if (conn.GetRowCount() == 0)
            {
                try
                {
                    conn.QueryString = "insert into M_USERS select " +
                                        "CODE = '" + TXT_CODE.Text.Trim() + "'," +
                                        "PASSWORD = '" + Crypto.EncryptStringAES(TXT_PASSWORD.Text.Trim()) + "'," +
                                        "ROLE_CODE = '" + DDL_USERROLE.SelectedValue + "'," +
                                        "FRONT_NAME = '" + TXT_FRONTNAME.Text.Trim() + "'," +
                                        "LAST_NAME = '" + TXT_LASTNAME.Text.Trim() + "'," +
                                        "MID_NAME = '" + TXT_MIDNAME.Text.Trim() + "'," +
                                        "BOD = '" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "'," +
                                        "ID_EMPLOYEE = '" + TXT_IDEMPLOYEE.Text.Trim() + "'," +
                                        "EMAIL = '" + TXT_EMAIL.Text.Trim() + "'," +
                                        "UPLINER = " + upliner + "," +
                                        "BRANCH_CODE = '" + DDL_BRANCH.SelectedValue + "'," +
                                        "POSITION_TITLE = '" + TXT_TITLE.Text.Trim() + "'," +
                                        "SIGNATURE = null," +
                                        "PHOTO = null," +
                                        "LOCKED = 0," +
                                        "ACTIVE = 1," +
                                        "START_ACT = null," +
                                        "LAST_ACT = null," +
                                        "LASTCHANGEPWD = null," +
                                        "CREATEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "CREATEDATE = GETDATE()," +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE()";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                    return;
                }
            }
            else
            {
                try
                {
                    conn.QueryString = "update M_USERS set " +
                                        pwd +
                                            "ROLE_CODE = '" + DDL_USERROLE.SelectedValue + "'," +
                                            "FRONT_NAME = '" + TXT_FRONTNAME.Text.Trim() + "'," +
                                            "LAST_NAME = '" + TXT_LASTNAME.Text.Trim() + "'," +
                                            "MID_NAME = '" + TXT_MIDNAME.Text.Trim() + "'," +
                                            "BOD = '" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "'," +
                                            "ID_EMPLOYEE = '" + TXT_IDEMPLOYEE.Text.Trim() + "'," +
                                            "EMAIL = '" + TXT_EMAIL.Text.Trim() + "'," +
                                            "UPLINER = " + upliner + "," +
                                            "BRANCH_CODE = '" + DDL_BRANCH.SelectedValue + "'," +
                                            "POSITION_TITLE = '" + TXT_TITLE.Text.Trim() + "'," +
                                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "LASTCHANGEDATE = GETDATE() " +
                                        "where CODE = '" + TXT_CODE.Text.Trim() + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                    return;
                }
            }

            FillGrid();
            ShowUser(TXT_CODE.Text.Trim());
        }

        protected bool ValidatePassword(string pwd)
        {
            bool result = true;

            conn.QueryString = "select RESULT=dbo.UFN_CHECK_PASSWORD_REGULATION('" + pwd + "')";
            conn.ExecuteQuery();
            if (conn.GetFieldValue("RESULT").ToString() == "0")
                return false;

            return result;
        }

        protected bool Validate()
        {
            bool result = true;

            if (TXT_CODE.Text.Trim() == "")
                result = false;

            if (TXT_FRONTNAME.Text.Trim() == "")
                result = false;

            if (TXT_LASTNAME.Text.Trim() == "")
                result = false;

            if (TXT_DOB.Text.Trim() == "")
                result = false;

            conn.QueryString = "select CODE from M_USERS where CODE = '" + TXT_CODE.Text.Trim() + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0 && TXT_PASSWORD.Text.Trim() == "")
                result = false;

            return result;
        }

        protected void BT_PHOTOCLEAR_Click(object sender, EventArgs e)
        {
            conn.QueryString = "update M_USERS set PHOTO = null where CODE = '" + TXT_CODE.Text.Trim() + "'";
            conn.ExecuteNonQuery();
            ShowUser(TXT_CODE.Text.Trim());
        }

        protected void BT_SIGNATURECLEAR_Click(object sender, EventArgs e)
        {
            conn.QueryString = "update M_USERS set SIGNATURE = null where CODE = '" + TXT_CODE.Text.Trim() + "'";
            conn.ExecuteNonQuery();
            ShowUser(TXT_CODE.Text.Trim());
        }

        protected void BT_PHOTOUPLOAD_Click(object sender, EventArgs e)
        {
            FileUpload fup = PHOTOUPLOAD;
            if (PHOTOUPLOAD.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fup.FileName);
                    string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                    fup.SaveAs(fullpath);

                    string SQL = "update M_USERS set PHOTO=@File where CODE='" + TXT_CODE.Text.Trim() + "'";
                    GlobalUse.FileToSQL(fullpath, SQL);
                    ShowBinary(TXT_CODE.Text.Trim());

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }
                catch { }
            }

            ShowBinary(TXT_CODE.Text.Trim());
        }

        protected void BT_SIGNATUREUPLOAD_Click(object sender, EventArgs e)
        {
            FileUpload fup = SIGNATUREUPLOAD;
            if (SIGNATUREUPLOAD.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(fup.FileName);
                    string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                    fup.SaveAs(fullpath);

                    string SQL = "update M_USERS set SIGNATURE=@File where CODE='" + TXT_CODE.Text.Trim() + "'";
                    GlobalUse.FileToSQL(fullpath, SQL);
                    ShowBinary(TXT_CODE.Text.Trim());

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                }
                catch { }
            }

            ShowBinary(TXT_CODE.Text.Trim());
        }

    }
}