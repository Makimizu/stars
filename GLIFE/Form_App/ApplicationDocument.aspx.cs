using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class ApplicationDocument : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                bDone = TrackDone();
                Setup();
                LoadDocument();
                //LoadArchieve();
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select SEQ = MAX(SEQ) from TRACK_DATA where TIPE_CODE='UW' and OWNER = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (int.Parse(conn.GetFieldValue("SEQ").ToString()) < 3)
                result = false;

            return result;
        }

        protected void Setup()
        {
            if (bDone || Request.QueryString["readonly"] == "1")
            {
                TR_DOCS_REMAINED.Visible = false;
            }
            else
            {
                FillDGRDocsRemained();
            }
        }

        protected void FillDGRDocsRemained()
        {
            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.DESCR " +
                                "from V_LINK_UB_PR_UW_REQUIRED_DOCUMENT a " +
                                "left join APPLICATION_MEDICAL_DOCUMENT b on a.CODE = b.DOCTYPE and b.REGNO = '" + LB_REGNO.Text + "' " +
                                "where " +
                                "b.DOCTYPE is null " +
                                "order by 2";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DOCS_REMAINED.DataSource = dt;
            DGR_DOCS_REMAINED.DataBind();
        }

        protected void LoadDocument()
        {
            conn.QueryString = "exec SP_APPLICATION_MEDICAL_DOCUMENT '" + LB_REGNO.Text + "'";
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
                                    "<table style='border-spacing:0px;width:100%;'>" +
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
                DGR_DOCS.Columns[6].Visible = false;
                DGR_DOCS.Columns[7].Visible = false;
                DGR_DOCS.Columns[8].Visible = false;
            }
        }

        protected void DGR_DOCS_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from ARCHIEVE.dbo.GL_ARSIP where CODE='" + e.Item.Cells[2].Text + "' " +
                                    "delete from APPLICATION_MEDICAL_DOCUMENT where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and DOCTYPE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGRDocsRemained();
                LoadDocument();
            }

            if (e.CommandName == "Clear")
            {
                conn.QueryString = "delete from ARCHIEVE.dbo.GL_ARSIP where CODE='" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                LoadDocument();
            }

            if (e.CommandName == "Download")
            {
                string filename = e.Item.Cells[3].Text.Replace(" ", "");
                GlobalUse.SQLToFile(filename.Trim(),
                                    "select THEFILE from ARCHIEVE.dbo.GL_ARSIP where CODE='" + e.Item.Cells[2].Text + "'",
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
                    string SQL = "delete from ARCHIEVE.dbo.GL_ARSIP where OWNER1 = '" + LB_REGNO.Text + "' and OWNER2 = '" + e.Item.Cells[0].Text + "' " +
                                    "insert into ARCHIEVE.dbo.GL_ARSIP values (" +
                                    "'" + code + "'," +
                                    "'GL_01'," +
                                    "'" + LB_REGNO.Text + "'," +
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

        protected void DGR_DOCS_REMAINED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                conn.QueryString = "insert into APPLICATION_MEDICAL_DOCUMENT select " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()";
                conn.ExecuteNonQuery();
                FillDGRDocsRemained();
                LoadDocument();
            }
        }
    }
}