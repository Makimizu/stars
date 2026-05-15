using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace AGR
{
    public partial class AGENT_DOCUMENT : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["code"];
                LoadDocument();
            }
        }

        protected void LoadDocument()
        {
            conn.QueryString = "exec SP_M_AGENT_DOCUMENT '" + LB_ID.Text + "'";
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
                    lbDESCR.Visible = true;
                    lbDESCR.Text = DGR_DOCS.Items[i].Cells[5].Text + " : " + DGR_DOCS.Items[i].Cells[1].Text;
                }
            }
        }

        protected void DGR_DOCS_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from ARCHIEVE.dbo.AGR_ARSIP where CODE='" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                LoadDocument();
            }

            if (e.CommandName == "Download")
            {
                string filename = e.Item.Cells[3].Text.Replace(" ", "");
                GlobalUse.SQLToFile(filename.Trim(),
                                    "select THEFILE from ARCHIEVE.dbo.AGR_ARSIP where CODE='" + e.Item.Cells[2].Text + "'",
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
                    string SQL = "delete from ARCHIEVE.dbo.AGR_ARSIP where OWNER1 = '" + LB_ID.Text + "' and OWNER2 = '" + e.Item.Cells[0].Text + "' and TIPE='AGR_1' " +
                                    "insert into ARCHIEVE.dbo.AGR_ARSIP values (" +
                                    "'" + code + "'," +
                                    "'AGR_1'," +
                                    "'" + LB_ID.Text + "'," +                                    
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
    }
}