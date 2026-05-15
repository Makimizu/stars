using System;
using System.IO;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data.OleDb;

namespace LIFE.Form_Finance
{
    public partial class UWSurplus : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("FN"));
        private string _fullpath, _path;
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

                Setup();
                DGR.CurrentPageIndex = 0;
                
            }
        }

        protected void Setup()
        {
            
        }

        


        protected void FillDGR()
        {
            BT_XLS.Visible = false;
            LB_RESULT.Text = "";

            conn.QueryString = "EXEC RPT_UNDERWRITING_SURPLUS '" + TXT_BATCHID.Text + "'";
            conn.ExecuteQuery(1000);

            if (conn.GetRowCount() > 0)
                BT_XLS.Visible = true;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = conn.GetRowCount().ToString();

            LB_RESULT.Text = "<TABLE style='border-spacing:0px;'>" +
                                "<TR><TD style='width:100px;'>Total Records</TD><TD>:</TD><TD>" + LB_RESULT.Text + "</TD></TR>" +
                                "</TABLE>";
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            
            conn.QueryString = "EXEC RPT_UNDERWRITING_SURPLUS '" + TXT_BATCHID.Text + "'";
            conn.ExecuteQuery(1000);

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            bool v = GlobalUse.ExportToExcel(dt, this, "RETUR", true);

            if (v)
            {
                FillDGR();
            }
            else { FillDGR(); }
        }

        //UPLOAD SUW START
        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            if (TXT_BATCHID.Text == "")
            {
                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = "Kolom BATCH ID harus diisi";
                return;
            }

            FillDGR();
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            if (TXT_BATCHID.Text == "")
            {
                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = "Kolom BATCH ID harus diisi";
                return;
            }

            try
            {
                conn.QueryString = "exec SP_SUW_INQUIRY_REK " +
                            "'" + TXT_BATCHID.Text + "', " +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery(1000);

                FillDGR();
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
                LB_ERR.Text = "Submit success";
            }
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            if (TXT_FILE_UPLOAD.Value == "")
                return;

            conn.QueryString = "select BATCH_ID = replace(convert(varchar(30),GETDATE(),112) + convert(varchar(30),GETDATE(),114),':','')";
            conn.ExecuteQuery();

            string BATCH_ID = conn.GetFieldValue("BATCH_ID").ToString();

            try
            {
                UploadFile(BATCH_ID);
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
                LB_ERR.Text = "Upload success, BATCH_ID = " + BATCH_ID;
            }
        }

        private void UploadFile(string BATCH_ID)
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
                ProcessSpecialTemplate(filename, _fullpath, BATCH_ID);
                return;
                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);
            }
        }

        protected void ProcessSpecialTemplate(string filename, string FullPath, string BATCH_ID)
        {
            string extension = "";

            var dataSplit = filename.Split(new string[] { "." }, StringSplitOptions.None);
            extension = dataSplit[dataSplit.Length - 1];

            string connstr = "";

            if (extension == "xlsx")
            {
                connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FullPath + @";Extended Properties=""Excel 12.0 Xml;HDR=YES""";
            }
            else
            {
                connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            }

            OleDbConnection con = new OleDbConnection(connstr);
            con.Open();

            DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = "";

            if (extension == "xlsx")
            {
                foreach (DataRow row in sheets.Rows)
                {
                    if (row["TABLE_NAME"].ToString().EndsWith("$"))
                    {
                        SheetName = row["TABLE_NAME"].ToString();
                        break;
                    }
                }

                if (SheetName == "")
                {
                    SheetName = sheets.Rows[0]["TABLE_NAME"].ToString();
                }
            }
            else
            {
                SheetName = sheets.Rows[0]["TABLE_NAME"].ToString();

            }

            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + SheetName + "]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);


            try
            {
                int IDX = 0;
                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    string sql = "insert into UNDERWRITING_SURPLUS_RAW select " +
                                    "'" + BATCH_ID + "'," +
                                    "'" + BATCH_ID + IDX.ToString() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE(),";

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

                con.Close();
            }
            catch (System.Exception ex)
            {
                con.Close();
                LB_ERR.Text = ex.Message + "<BR><BR>";

            }

        }
        //UPLOAD SUW FINISH
    }
}