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
    public partial class ChangePix : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        protected Connection conn2 = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ID.Text = Request.QueryString["userid"];
                ShowPix(ID.Text);
            }
        }

        protected void ShowPix(string UserID)
        {
            string sql = "select PHOTO from M_USERS where CODE = '" + UserID + "'";
            IMG1.ImageUrl = GlobalUse.GetStringImageURL(sql, "PHOTO");
            IMG1.Height = 100;
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            if (FILEUPLOAD.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(FILEUPLOAD.FileName);
                    string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }
                    FILEUPLOAD.SaveAs(fullpath);

                    string SQL = "update M_USERS set PHOTO = @File where CODE='" + ID.Text + "'";
                    GlobalUse.FileToSQL(fullpath, SQL);

                    if (File.Exists(fullpath))
                    {
                        File.Delete(fullpath);
                    }

                    string frameScript = "<script language='javascript'>window.opener.location.reload(true);window.close();</script>";
                    Response.Write(frameScript);
                }
                catch { }

                //Exit();
            }            
        }

        protected void BT_EXIT_Click(object sender, EventArgs e)
        {
            Exit();
        }

        protected void Exit()
        {
            ClientScript.RegisterStartupScript(typeof(Page), "closePage", "window.close();", true);
        }
    }
}