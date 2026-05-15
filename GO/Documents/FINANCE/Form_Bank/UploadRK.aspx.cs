using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Runtime.InteropServices; 
//using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace FINANCE.Form_Bank
{
    public partial class UploadRK : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        private string _fullpath, _path;
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
            DDL_NOREK.Items.Clear();
            conn.QueryString = "select NOREK, BANK from REKENING_MASTER order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_NOREK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            ShowAccount();
            ToggleTemplateXLS();
        }

        protected void ShowAccount()
        {
            DDL_FORMAT.Items.Clear();
            conn.QueryString = "exec SP_PARAM_TBL_BANK_TEMPLATE_LIST '" + DDL_NOREK.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FORMAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select " +
                                "BATCH_ID," +
                                "NOREK," +
                                "BOOK_NAME," +
                                "BANK_NAME," +
                                "FILENAME," +
                                "RECORDS," +
                                "USERBY," +
                                "USERDATE " +
                                "from V_REKENING_JURNAL_BATCH where NOREK = '" + DDL_NOREK.SelectedValue + "' order by USERDATE desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDel = (Button)DGR.Items[i].FindControl("BT_DEL");
                btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");
            }
        }

        protected void DDL_NOREK_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAccount();
            ToggleTemplateXLS();
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            if (TXT_FILE_UPLOAD.Value == "")
                return;

            try
            {
                UploadFile();
                ShowAccount();
            }
            catch (Exception er)
            {
                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = er.Message + "<BR><BR>";
                return;
            }

            if (LB_ERR.Text == "")
            {
                LB_ERR.ForeColor = System.Drawing.Color.Black;
                LB_ERR.Text = "Upload success";
            }
        }

        private void UploadFile()
        {
            _path = Request.PhysicalApplicationPath + "Upload/";
            string filename;

            HttpFileCollection uploadedFiles = Request.Files;
            HttpPostedFile userPostedFile = uploadedFiles[0];

            if (userPostedFile.ContentLength > 0)
            {
                filename = Path.GetFileName(userPostedFile.FileName);
                _fullpath = _path + filename;

                userPostedFile.SaveAs(_fullpath);
                //if(DDL_FORMAT.SelectedValue != "99")
                    ProcessSpecialTemplate(filename, _fullpath);

                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);
            }
        }

        protected void ProcessSpecialTemplate(string filename, string FullPath)
        {
            conn.QueryString = "select BATCH_ID from REKENING_JURNAL_BATCH " +
                                "where " +
                                "NOREK = '" + DDL_NOREK.SelectedValue + "' " +
                                "and filename = '" + filename + "' " +
                                "and datediff(day,USERDATE,GETDATE()) = 0";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                LB_ERR.Text = "filename " + filename + " was already uploaded on batch number : " + conn.GetFieldValue("BATCH_ID").ToString() + "<BR><BR>";
                return;
            }

            conn.QueryString = "select BATCH_ID = replace(convert(varchar(30),GETDATE(),112) + convert(varchar(30),GETDATE(),114),':','')";
            conn.ExecuteQuery();

            string BATCH_ID = conn.GetFieldValue("BATCH_ID").ToString();
            conn.QueryString = "insert into REKENING_JURNAL_BATCH select " +
                                "'" + BATCH_ID + "'," +
                                "'" + DDL_NOREK.SelectedValue + "'," +
                                "'" + filename.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE()";
            conn.ExecuteNonQuery();

            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);
            con.Open();

            DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //LB_ERR.Text = sheets.Rows[0]["TABLE_NAME"].ToString();
            //OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$]", con);
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            

            /*
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(FullPath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            */
            try
            {
                /*
                for (int i = 1; i <= xlRange.Rows.Count; i++)
                {
                    string sql = "insert into REKENING_JURNAL_RAW select " +
                                    "'" + BATCH_ID + "'," +
                                    "'" + BATCH_ID + i.ToString() + "',";

                    for (int j = 1; j < 20; j++)
                    {
                        string val = "";
                        try
                        {
                            if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                                val = xlRange.Cells[i, j].Value2.ToString().Trim().Replace("'", "`");
                        }
                        catch (System.Exception ex)
                        {
                            string err = ex.Message;
                        }

                        sql = sql + "'" + val + "',";
                    }
                    
                    conn.QueryString = sql + "''";
                    conn.ExecuteNonQuery();
                }
                */
               

                

                
                int IDX = 0;
                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    string sql = "insert into REKENING_JURNAL_RAW select " +
                                    "'" + BATCH_ID + "'," +
                                    "'" + BATCH_ID + IDX.ToString() + "',";

                    for (int i = 0; i < 49; i++)
                    {
                        string val = "";
                        try
                        {
                            val = myRow[i].ToString().Trim().Replace("'", "`");
                        }
                        catch { }

                        sql = sql + "'" + val + "',";
                    }
                    conn.QueryString = sql + "''";
                    conn.ExecuteNonQuery();
                }


                conn.QueryString = "exec SP_REKENING_JURNAL_RAW_EXPORT " +
                                "'" + BATCH_ID + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                con.Close();
            }
            catch(System.Exception ex)
            {
                con.Close();
                LB_ERR.Text = ex.Message + "<BR><BR>";

                conn.QueryString = "exec SP_REKENING_JURNAL_BATCH_ROLLBACK '" + BATCH_ID + "'";
                conn.ExecuteNonQuery();

                //return;
            }

            /*
            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
            //con.Close();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            */
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERR.Text = "";

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_REKENING_JURNAL_BATCH_ROLLBACK '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    ShowAccount();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.ForeColor = System.Drawing.Color.Red;
                    LB_ERR.Text = ex.Message + "<BR><BR>";
                    return;
                }
            }
        }

        protected void DDL_FORMAT_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToggleTemplateXLS();
        }

        protected void ToggleTemplateXLS()
        {
            TR_XLS.Visible = false;
            if (DDL_FORMAT.SelectedValue == "99")
                TR_XLS.Visible = true;
        }
    }
}