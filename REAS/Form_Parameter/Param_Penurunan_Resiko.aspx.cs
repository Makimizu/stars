using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DMS.DBConnection;
using System.IO;
using System.Data.OleDb;

namespace REAS.Form_Parameter
{
    public partial class Param_Penurunan_Resiko : System.Web.UI.Page
    {
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string _query = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select CODE, DESCR from PARAM_RESIKO_MASTER where DESCR like '%" + TXT_SEARCH.Text.Trim() + "%'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            gvLists.DataSource = dt;
            gvLists.DataBind();
        }

        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void gvLists_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "selectx") 
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvLists.Rows[rowIndex];
                lblCodeRisk.Text = (row.FindControl("lblCode") as LinkButton).Text;
                txtName.Text = (row.FindControl("lblDescr") as Label).Text;
                GetDataYear(lblCodeRisk.Text,"Y");
                GetDataMonth(lblCodeRisk.Text, "M");
                if (gvListTahunan.Rows.Count > 0 && gvListBulanan.Rows.Count > 0) 
                {
                    btnClear.Visible = true;
                }
            }
            else if (e.CommandName == "deletex") 
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvLists.Rows[rowIndex];
                string code = (row.FindControl("lblCode") as LinkButton).Text;
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    DeleteResikoMaster(code);
                    FillDGR();
                    txtName.Text = "";
                    lblCodeRisk.Text = "";
                    DGRNULL();
                }
            }
        }

        private void DeleteResikoMaster(string code) 
        {
            _query = "USP_DELETE_RESIKO_MASTER '" + @code + "'";
            conn.QueryString = _query;
            conn.ExecuteNonQuery();
        }

        private void DeleteResikoDetail(string code) 
        {
            _query = "USP_DELETE_RESIKO_DETAIL '" + @code + "'";
            conn.QueryString = _query;
            conn.ExecuteNonQuery();
        }

        private void GetDataYear(string code, string flag) 
        {
            try
            {
                _query = "USP_GET_RESIKO_TAHUNAN_PVT '" + @code + "', '" + flag + "'";
                conn.QueryString = _query;
                conn.ExecuteQuery();

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();

                if (dt.Rows.Count > 0)
                {
                    gvListTahunan.DataSource = dt;
                    gvListTahunan.DataBind();
                    lblStatus.Text = "TAHUNAN";
                    lblStatus.Visible = true;
                }
                else 
                {
                    DGRNULL();
                }
            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
            }
            
        }

        private void GetDataMonth(string code, string flag)
        {
            try
            {
                _query = "USP_GET_RESIKO_BULANAN_PVT '" + @code + "', '" + flag + "'";
                conn.QueryString = _query;
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                if (dt.Rows.Count > 0) 
                {
                    gvListBulanan.DataSource = dt;
                    gvListBulanan.DataBind();
                    lblStatus2.Text = "BULANAN";
                    lblStatus2.Visible = true;
                }
                
            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
            }
            

            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Nama dan File harus terisi!');", true);
            }
            else if (!fuTblBulanan.HasFile || !fuTblTahunan.HasFile)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage('Nama dan File harus terisi!');", true);
            }
            else
            {
                SaveResiko();
            }
        }

        private void SaveResiko() 
        {
            try
            {
                if (txtName.Text.Trim() == "")
                    return;

                string ID = "null";
                if (lblCodeRisk.Text != "")
                    ID = "'" + lblCodeRisk.Text + "'";

                conn.QueryString = "exec USP_INSERT_PARAM_RESIKO_MASTER " +
                                ID + "," +
                                "'" + txtName.Text.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                string codeID = conn.GetFieldValue("CODE").ToString();
                if (codeID != "")
                {
                   CekIDTblDetail(codeID);
                   lblCodeRisk.Text = codeID;
                   UploadTahunan(codeID);
                   UploadBulanan(codeID);
                   GetDataYear(codeID, "Y");
                   GetDataMonth(codeID, "M");
                }
            }
            catch (Exception ex) 
            {
                lblError.Text = ex.Message.ToString();
            }

            FillDGR();
        }

        private void CekIDTblDetail(string code) 
        {
           conn.QueryString = "SELECT * FROM PARAM_RESIKO_DETAIL WHERE CODE = '" + code + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                if (dt.Rows.Count > 0)
                {
                    conn.QueryString = "DELETE FROM PARAM_RESIKO_DETAIL WHERE CODE = '" + code + "'";
                    conn.ExecuteNonQuery();
                }
        }

        private void UploadTahunan(string code)
        {
            string filenameTahunan = Path.GetFileName(fuTblTahunan.FileName);
            string fullpathTahunan = Server.MapPath("~/Upload/") + Session["s"] + filenameTahunan;
            if (File.Exists(fullpathTahunan))
            {
                File.Delete(fullpathTahunan);
            }

            fuTblTahunan.SaveAs(fullpathTahunan);

            string strFileType = Path.GetExtension(fuTblTahunan.FileName).ToLower();
            string connstr = string.Empty;

            if (strFileType.Trim() == ".xls")
            {
                connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpathTahunan + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=YES;TypeGuessRows=0;ImportMixedTypes=Text""";

            }
            else if (strFileType.Trim() == ".xlsx")
            {
                connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullpathTahunan + ";Extended Properties=Excel 12.0;";
            }

            OleDbConnection con = new OleDbConnection(connstr);
            con.Open();

            ////string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpathTahunan + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            //string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullpathTahunan + ";Extended Properties=Excel 12.0;";
            //OleDbConnection con = new OleDbConnection(connStr);
            //con.Open();

            DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            foreach (DataRow myRow in dt.Rows)
            {
                conn.QueryString = "insert into PARAM_RESIKO_DETAIL select " +
                                        "'" + code + "'," + //CODE
                                        "'" + myRow[1].ToString().Trim() + "'," + //FLAG
                                        "'" + myRow[2].ToString().Trim() + "'," + //TAHUN POLIS
                                        "'" + myRow[3].ToString().Trim() + "'," + //MASA ASURANSI
                                        "'" + myRow[4].ToString().Trim() + "'," + //VALUE
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()";
                conn.ExecuteNonQuery();
            }
        }

        private void UploadBulanan(string code)
        {
            string fileNameBulanan = Path.GetFileName(fuTblBulanan.FileName);
            string fullPathBulanan = Server.MapPath("~/Upload/") + Session["s"] + fileNameBulanan;
            if (File.Exists(fullPathBulanan))
            {
                File.Delete(fullPathBulanan);
            }

            fuTblBulanan.SaveAs(fullPathBulanan);

            string strFileType = Path.GetExtension(fuTblBulanan.FileName).ToLower();
            string connstr = string.Empty;

            if (strFileType.Trim() == ".xls")
            {
                connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullPathBulanan + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=YES;TypeGuessRows=0;ImportMixedTypes=Text""";

            }
            else if (strFileType.Trim() == ".xlsx")
            {
                 connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullPathBulanan + ";Extended Properties=Excel 12.0;";
            }

            OleDbConnection con = new OleDbConnection(connstr);
            con.Open();

            ////string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullPathBulanan + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            //string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullPathBulanan + ";Extended Properties=Excel 12.0;";
            //OleDbConnection con = new OleDbConnection(connStr);
            //con.Open();

            DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();

            foreach (DataRow myRow in dt.Rows)
            {
                conn.QueryString = "insert into PARAM_RESIKO_DETAIL select " +
                                        "'" + code + "'," + //CODE
                                        "'" + myRow[1].ToString().Trim() + "'," + //FLAG
                                        "'" + myRow[2].ToString().Trim() + "'," + //TAHUN POLIS
                                        "'" + myRow[3].ToString().Trim() + "'," + //MASA ASURANSI
                                        "'" + myRow[4].ToString().Trim() + "'," + //VALUE
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE()";
                conn.ExecuteNonQuery();
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                string code = string.Empty;
                code = lblCodeRisk.Text.Trim();
                DeleteResikoDetail(code);
                GetDataYear(code, "Y");
                GetDataMonth(code, "M");
                btnClear.Visible = false;
            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
            }
        }

        private void DGRNULL()
        {
            lblStatus.Visible = false;
            lblStatus2.Visible = false;
            gvListTahunan.DataSource = null;
            gvListTahunan.DataBind();
            gvListBulanan.DataSource = null;
            gvListBulanan.DataBind();

        }
    }
}