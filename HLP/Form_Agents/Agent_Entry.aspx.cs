using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Agents
{
    public partial class Agent_Entry : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ERR.Text = "";
                Setup();
                try
                {
                    LB_CODE.Text = Request.QueryString["code"];
                    LoadRecord();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = ex.Message;
                }
            }
        }

        protected void Setup()
        {
            DDL_CD.Items.Clear();
            conn.QueryString = "select CODE,DESCR from PR_CHANNEL_DISTRIBUTION";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
            FillDDLSubCD();            

            DDL_BRANCH.Items.Clear();
            conn.QueryString = "select CODE, NAME = UPPER(NAME) from V_LINK_SECURITY_M_BRANCH order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BRANCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDDLUpliner()
        {
            DDL_UPLINER.Items.Clear();
            DDL_UPLINER.Items.Add(new ListItem("", ""));
            conn.QueryString = "select CODE, FRONT_NAME+' '+LAST_NAME from V_M_AGENTS where CD='" + DDL_CD.SelectedValue + "' and TRACK='2' order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_UPLINER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDDLSubCD()
        {
            DDL_SUBCD.Items.Clear();
            conn.QueryString = "select SUB_CODE,DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where CD_CODE='" + DDL_CD.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_SUBCD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLUpliner();
        }

        protected void LoadRecord()
        {
            TXT_CODE.Text = LB_CODE.Text;
            if (LB_CODE.Text != "")
            {
                TXT_CODE.ReadOnly = true;
                BT_CLEAR.Visible = true;
                TR_PHOTO.Visible = true;
                TR_ACC.Visible = true;
            }
            else
                return;

            conn.QueryString = "select " +
                                "CODE, " +
                                "CD, " +
                                "SUBCD, " +
                                "FRONT_NAME, " +
                                "LAST_NAME, " +
                                "MID_NAME, " +
                                "DOB = convert(varchar(20),DOB,103), " +
                                "ID_EMPLOYEE, " +
                                "EMAIL, " +
                                "UPLINER, " +
                                "BRANCH_CODE, " +
                                "POSITION_TITLE, " +
                                "PHOTO, " +
                                "READ_ONLY, " +
                                "TRACK_DESCR " +
                                "from V_M_AGENTS where CODE = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("READ_ONLY").ToString() == "1")
            {
                BT_SAVE.Visible = false;
                BT_UPLOAD.Visible = false;
                BT_CLEAR.Visible = false;
                FILEUPLOAD.Visible = false;
            }
            
            try
            {
                DDL_BRANCH.SelectedValue = conn.GetFieldValue("BRANCH_CODE").ToString();
            }
            catch { }

            TXT_NMFRONT.Text = conn.GetFieldValue("FRONT_NAME").ToString();
            TXT_NMLAST.Text = conn.GetFieldValue("LAST_NAME").ToString();
            TXT_NMMID.Text = conn.GetFieldValue("MID_NAME").ToString();
            TXT_DOB.Text = conn.GetFieldValue("DOB").ToString();
            TXT_EMPID.Text = conn.GetFieldValue("ID_EMPLOYEE").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();
            TXT_TITLE.Text = conn.GetFieldValue("POSITION_TITLE").ToString();
            LB_STATUS.Text = conn.GetFieldValue("TRACK_DESCR").ToString();

            string subcd = conn.GetFieldValue("SUBCD").ToString();
            string upliner = conn.GetFieldValue("UPLINER").ToString();

            try
            {
                DDL_CD.SelectedValue = conn.GetFieldValue("CD").ToString();                                
            }
            catch { }

            try
            {
                FillDDLSubCD();
                DDL_SUBCD.SelectedValue = subcd;                
            }
            catch { }

            try
            {
                FillDDLUpliner();
                DDL_UPLINER.SelectedValue = upliner;
            }
            catch { }

            IMG_PHOTO.ImageUrl = GlobalUse.GetStringImageURL(conn, "select PHOTO from V_M_AGENTS where CODE = '" + LB_CODE.Text + "'", "PHOTO");
            IMG_PHOTO.Height = 100;

            FIllDGRRekening();
        }

        protected void DDL_CD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLSubCD();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            try
            {
                conn.QueryString = "exec SP_M_AGENTS_NEW_VALIDATION " +
                                    "'" + TXT_CODE.Text.Trim() + "'";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("RESULT").ToString() != "")
                {
                    LB_ERR.Text = conn.GetFieldValue("RESULT").ToString();
                    return;
                }

                string upliner = "null";
                if (DDL_UPLINER.SelectedValue != "")
                    upliner = "'" + DDL_UPLINER.SelectedValue + "'";
                conn.QueryString = "exec SP_M_AGENTS_UPSERT " +
                                    "'" + TXT_CODE.Text.Trim() + "'," +
                                    "'" + DDL_SUBCD.SelectedValue + "'," +
                                    "'" + TXT_NMFRONT.Text.Trim() + "'," +
                                    "'" + TXT_NMLAST.Text.Trim() + "'," +
                                    "'" + TXT_NMMID.Text.Trim() + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DOB.Text, "d/M/yyyy") + "'," +
                                    "'" + TXT_EMPID.Text.Trim() + "'," +
                                    "'" + TXT_EMAIL.Text.Trim() + "'," +
                                    upliner + "," +
                                    "'" + DDL_BRANCH.SelectedValue + "'," +
                                    "'" + TXT_TITLE.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                conn.ExecuteQuery();

                LB_CODE.Text = conn.GetFieldValue("CODE").ToString();

                SaveRek();
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message + "<BR>";
                return;
            }

            Response.Write("<script language='javascript'>parent.pageheader.location.href = parent.pageheader.location.href;</script>");
            Response.Write("<script language='javascript'>parent.pagebody.location.href = 'Agent_Entry.aspx?code=" + LB_CODE.Text + "';</script>");
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            string filename;
            string fullpath;
            FileUpload fup = (FileUpload) FILEUPLOAD;
            if (fup.HasFile)
            {
                try
                {
                    filename = Path.GetFileName(fup.FileName);
                    fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                    fup.SaveAs(fullpath);

                    string SQL = "update M_AGENTS set PHOTO=@File where CODE='" + LB_CODE.Text + "'";
                    GlobalUse.FileToSQL(fullpath, SQL);                    

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }

                    LoadRecord();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = ex.Message + "<BR>";
                }
            }
        }

        protected void BT_CLEAR_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "update M_AGENTS set PHOTO=null where CODE='" + LB_CODE.Text + "'";
                conn.ExecuteNonQuery();
                LoadRecord();
            }
            catch { }
        }

        protected void DDL_SUBCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLUpliner();
        }

        protected void FIllDGRRekening()
        {
            conn.QueryString = "exec SP_M_AGENTS_ACCOUNT '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REK.DataSource = dt;
            DGR_REK.DataBind();

            conn.QueryString = "select CODE,BANK from V_LINK_FINANCE_PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_REK.Items.Count; i++)
            {
                TextBox txtACC = (TextBox)DGR_REK.Items[i].FindControl("TXT_ACC");
                DropDownList ddlBANK = (DropDownList)DGR_REK.Items[i].FindControl("DDL_BANK");
                TextBox txtNAMA = (TextBox)DGR_REK.Items[i].FindControl("TXT_ACCNAMA");
                CheckBox cbACC = (CheckBox)DGR_REK.Items[i].FindControl("CB_ACC");
                CheckBox cbVA = (CheckBox)DGR_REK.Items[i].FindControl("CB_VA");

                txtACC.Text = DGR_REK.Items[i].Cells[0].Text.Replace("&nbsp;", "");                
                txtNAMA.Text = DGR_REK.Items[i].Cells[2].Text.Replace("&nbsp;", "");

                ddlBANK.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddlBANK.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddlBANK.SelectedValue = DGR_REK.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                }
                catch { }

                if (DGR_REK.Items[i].Cells[3].Text.Replace("&nbsp;", "") == "1")
                    cbACC.Checked = true;

                if (DGR_REK.Items[i].Cells[4].Text.Replace("&nbsp;", "") == "1")
                    cbVA.Checked = true;
            }
        }

        protected void SaveRek()
        {
            try
            {
                conn.QueryString = "delete from M_AGENTS_ACCOUNT where CODE = '" + LB_CODE.Text + "'";
                conn.ExecuteNonQuery();
            }
            catch { }

            for (int i = 0; i < DGR_REK.Items.Count; i++)
            {
                TextBox txtACC = (TextBox)DGR_REK.Items[i].FindControl("TXT_ACC");
                DropDownList ddlBANK = (DropDownList)DGR_REK.Items[i].FindControl("DDL_BANK");
                TextBox txtNAMA = (TextBox)DGR_REK.Items[i].FindControl("TXT_ACCNAMA");
                CheckBox cbACC = (CheckBox)DGR_REK.Items[i].FindControl("CB_ACC");
                CheckBox cbVA = (CheckBox)DGR_REK.Items[i].FindControl("CB_VA");
                                
                if (txtACC.Text.Trim() != "" && ddlBANK.SelectedValue != "" && txtNAMA.Text.Trim() != "")
                {
                    string active = "0";
                    string va = "0";
                    if (cbACC.Checked)
                        active = "1";
                    if (cbVA.Checked)
                        va = "1";

                    try
                    {
                        conn.QueryString = "insert into M_AGENTS_ACCOUNT select " +
                                            "'" + LB_CODE.Text + "'," +
                                            "'" + txtACC.Text.Trim() + "'," +
                                            "'" + ddlBANK.SelectedValue + "'," +
                                            "'" + txtNAMA.Text.Trim() + "'," +
                                            "'" + va + "'," +
                                            "'" + active + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "GETDATE()," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "GETDATE()";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }
        }
    }
}