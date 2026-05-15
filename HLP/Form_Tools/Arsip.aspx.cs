using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HLP.Form_Tools
{
    public partial class Arsip : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

        private string _dbip, _fullpath, _path, _ftprootpath, _ftpuid, _ftppwd;
        private int _port;
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
            LB_FILE_TIPE.Text = Request.QueryString["tipe"];
            LB_OWNER.Text = Request.QueryString["owner"];
            FillDGRArsip();
        }

        private void FillDGRArsip()
        {
            conn.QueryString = "select CODE, REMARK, NAMAFILE from PARAM_ARSIP_DETAIL where OWNER1 = '" + LB_OWNER.Text + "' and TIPE='" + LB_FILE_TIPE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ARSIP.DataSource = dt;
            DGR_ARSIP.DataBind();
        }


        protected void DGR_ARSIP_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string filename = e.Item.Cells[2].Text.Replace(" ", "");
                if (filename.Length > 20)
                {
                    filename = filename.Substring(filename.Length - 20, 20);
                    //filename = filename.Substring(filename.Length - 50);
                }
                GlobalUse.SQLToFile(filename.Trim(),
                                "select THEFILE from PARAM_ARSIP_DETAIL where CODE='" + e.Item.Cells[0].Text + "'",
                                Page);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    string code = e.Item.Cells[0].Text;
                    conn.QueryString = "delete from PARAM_ARSIP_DETAIL where CODE='" + code + "'";
                    conn.ExecuteQuery();
                }
                catch
                {
                    GlobalTools.popMessage(this, "Delete file gagal");
                    return;
                }

                FillDGRArsip();
            }
        }

        private void uploadfile(string owner, string remark)
        {




            conn.QueryString = "select PATH from PARAM_ARSIP_TIPE where CODE = '" + LB_FILE_TIPE.Text + "' ";
            conn.ExecuteQuery();

            _path = Request.PhysicalApplicationPath + conn.GetFieldValue(0, 0).ToString();
            string filename;

            HttpFileCollection uploadedFiles = Request.Files;
            HttpPostedFile userPostedFile = uploadedFiles[0];

            if (userPostedFile.ContentLength > 0)
            {
                conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
                conn.ExecuteQuery();

                string code = conn.GetFieldValue(0, 0).ToString();

                filename = code + "_" + Path.GetFileName(userPostedFile.FileName);
                _fullpath = _path + filename;

                userPostedFile.SaveAs(_fullpath);

                string SQL = "insert into PARAM_ARSIP_DETAIL values (" +
                                    "'" + code + "'," +
                                    "'" + LB_FILE_TIPE.Text + "'," +
                                    "'" + owner + "'," +
                                    "null," +
                                    "null," +
                                    "'" + remark + "'," +
                                    "'" + filename + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                                    "@File)";

                GlobalUse.FileToSQL(_fullpath, SQL);

                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);
            }
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            if (TXT_FILE_UPLOAD.Value == "")
                return;

            Label1.Text = "";

            if (TXT_UPLOAD_REMARK.Text == "")
                return;

            try
            {
                uploadfile(LB_OWNER.Text, TXT_UPLOAD_REMARK.Text);
            }
            catch (Exception er)
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = er.Message;
                return;
            }

            Label1.ForeColor = System.Drawing.Color.Black;
            Label1.Text = "Upload success";
            FillDGRArsip();
        }
    }
}