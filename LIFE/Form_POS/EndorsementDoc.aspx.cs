using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_POS
{
    public partial class EndorsementDoc : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["regno"].ToString();
                LB_TYPE.Text = Request.QueryString["type"].ToString();
                LB_SEQ.Text = Request.QueryString["seq"].ToString();
                bDone = TrackDone();
                LoadDocument();
                LoadArchieve();

            }
            if (Request["__EVENTTARGET"] == "DL_YES")
            {
                RunDownload();
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select TRACK = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'POS', '" + LB_SEQ.Text + "')";
            conn.ExecuteQuery();

            if (int.Parse(conn.GetFieldValue("TRACK").ToString()) < 4)
                result = false;

            return result;
        }

        protected void LoadDocument()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_DOCUMENT '" + LB_REGNO.Text + "','" + LB_TYPE.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DOCS.DataSource = dt;
            DGR_DOCS.DataBind();

            for (int i = 0; i < DGR_DOCS.Items.Count; i++)
            {
                Label lbDESCR = (Label)DGR_DOCS.Items[i].FindControl("LB_DESCR");
                LinkButton lbtDESCR = (LinkButton)DGR_DOCS.Items[i].FindControl("LBT_DESCR");
                Button lbtCLEAR = (Button)DGR_DOCS.Items[i].FindControl("BT_CLEAR");

                if (DGR_DOCS.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lbtDESCR.Visible = true;
                    lbtDESCR.Text = DGR_DOCS.Items[i].Cells[1].Text + "<BR>" +
                                    "<table style='border-spacing:0px;width:100%;font-size:6pt;'>" +
                                    "<tr><td>Filename<td>:</td></td><td>" + DGR_DOCS.Items[i].Cells[3].Text + "</td></tr>" +
                                    "<tr><td>Upload date</td><td>:</td><td>" + DGR_DOCS.Items[i].Cells[4].Text + "</td></tr>" +
                                    "</table>";
                }
                else
                {
                    lbtCLEAR.Visible = false;
                    lbDESCR.Visible = true;
                    lbDESCR.Text = DGR_DOCS.Items[i].Cells[1].Text;
                }
            }

            if (bDone || Request.QueryString["readonly"] == "1")
            {
                DGR_DOCS.Columns[DGR_DOCS.Columns.Count - 3].Visible = false;
                DGR_DOCS.Columns[DGR_DOCS.Columns.Count - 2].Visible = false;
                DGR_DOCS.Columns[DGR_DOCS.Columns.Count - 1].Visible = false;
            }
        }

        protected void LoadArchieve()
        {
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_07", LB_REGNO.Text, LB_TYPE.Text, LB_SEQ.Text, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.archieve.location.href = '" + URL + "';</script>");
        }

        protected void DGR_DOCS_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from ARCHIEVE.dbo.LF_ARSIP where CODE='" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                LoadDocument();
            }

            if (e.CommandName == "Clear")
            {
                conn.QueryString = "delete from ARCHIEVE.dbo.LF_ARSIP where CODE='" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                LoadDocument();
            }

            if (e.CommandName == "Download")
            {


                string reg = e.Item.Cells[2].Text;     // simpan data untuk dikirim ke postback
                string file = e.Item.Cells[3].Text;


                //string filename = e.Item.Cells[3].Text.Replace(" ", "");
                //GlobalUse.SQLToFile(filename.Trim(),
                //                    "select THEFILE from ARCHIEVE.dbo.LF_ARSIP where CODE='" + e.Item.Cells[2].Text + "'",
                //                    Page);

                ViewState["DL_CODE"] = reg;
                ViewState["DL_FILE"] = file;

                ScriptManager.RegisterStartupScript(this, GetType(), "confirmDL",
                    "Swal.fire({" +
                    "title: 'Download File?', " +
                    "text: 'Yakin ingin download file ini?', " +
                    "icon: 'question'," +
                    "showCancelButton: true," +
                    "confirmButtonText: 'Yes'," +
                    "cancelButtonText: 'No'" +
                    "}).then((result) => {" +
                    "   if (result.isConfirmed) {" +
                    "       __doPostBack('DL_YES','');" +   // panggil server YES
                    "   }" +
                    "});",
                    true);

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
                    string SQL = "delete from ARCHIEVE.dbo.LF_ARSIP where OWNER1 = '" + LB_REGNO.Text + "' and OWNER2 = '" + LB_TYPE.Text + "-" + LB_SEQ.Text + "' and OWNER3 = '" + e.Item.Cells[0].Text + "' and TIPE='LF_07a' " +
                                    "insert into ARCHIEVE.dbo.LF_ARSIP values (" +
                                    "'" + code + "'," +
                                    "'LF_07a'," +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_TYPE.Text + "-" + LB_SEQ.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
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
        private void RunDownload()
        {
            string code = ViewState["DL_CODE"] + "";
            string file = ViewState["DL_FILE"] + "";

            string filename = file.Replace(" ", "");
            GlobalUse.SQLToFile(filename.Trim(),
                                "select THEFILE from ARCHIEVE.dbo.LF_ARSIP where CODE='" + code + "'",
                                Page);
        }
    }
}