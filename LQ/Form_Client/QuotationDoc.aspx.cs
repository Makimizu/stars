using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationDoc : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                FillDGRMedicalDoc();
                LoadArchieve();
            }
        }

        protected void FillDGRMedicalDoc()
        {
            conn.QueryString = "select " +
                                "a.DOCTYPE, " +
                                "aa.DESCR, " +
                                "RECEIVE_DATE = b.CREATEDATE, " +
                                "b.CODE, " +
                                "b.NAMAFILE " +
                                "from APPLICATION_MEDICAL_DOCUMENT a " +
                                "inner join V_LINK_UW_PR_UW_REQUIRED_DOCUMENT aa on a.DOCTYPE = aa.CODE " +
                                "left join V_LINK_ARCHIEVE b on a.REGNO = b.OWNER1 and a.DOCTYPE = b.OWNER2 " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
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
                LinkButton lbtDELETE = (LinkButton)DGR_DOCS.Items[i].FindControl("LBT_DELETE");

                if (DGR_DOCS.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lbtDESCR.Visible = true;
                    lbtDESCR.Text = "<B>" + DGR_DOCS.Items[i].Cells[1].Text + "</B><BR>" +
                                    "<table style='border-spacing:0px;width:100%;'>" +
                                    "<tr><td style='width:130px;'>Filename<td>:</td></td><td>" + DGR_DOCS.Items[i].Cells[3].Text + "</td></tr>" +
                                    "<tr><td>Upload date</td><td>:</td><td>" + DGR_DOCS.Items[i].Cells[4].Text + "</td></tr>" +
                                    "</table>";
                }
                else
                {
                    lbtDELETE.Visible = false;
                    lbDESCR.Visible = true;
                    lbDESCR.Text = DGR_DOCS.Items[i].Cells[1].Text;
                }
            }
        }

        protected void LoadArchieve()
        {
            TXT_ARCHIEVE.Text = "";
            conn.QueryString = "select CODE, REMARK, NAMAFILE from " +
                                "ARCHIEVE.dbo.LF_ARSIP " +
                                "where " +
                                "OWNER1 = '" + LB_REGNO.Text + "' " +
                                "and TIPE='LF_03'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ARCHIEVE.DataSource = dt;
            DGR_ARCHIEVE.DataBind();

            for (int i = 0; i < DGR_ARCHIEVE.Items.Count; i++)
            {
                LinkButton lbtDESCR = (LinkButton)DGR_ARCHIEVE.Items[i].FindControl("LBT_DOWNLOAD");
                lbtDESCR.Text = DGR_ARCHIEVE.Items[i].Cells[2].Text;
            }
        }

        protected void DGR_DOCS_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from ARCHIEVE.dbo.LF_ARSIP where CODE='" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                FillDGRMedicalDoc();
            }

            if (e.CommandName == "Download")
            {
                string filename = e.Item.Cells[3].Text.Replace(" ", "");
                GlobalUse.SQLToFile(filename.Trim(),
                                    "select THEFILE from ARCHIEVE.dbo.LF_ARSIP where CODE='" + e.Item.Cells[2].Text + "'",
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
                    string SQL = "delete from ARCHIEVE.dbo.LF_ARSIP where OWNER1 = '" + LB_REGNO.Text + "' and OWNER2 = '" + e.Item.Cells[0].Text + "' " +
                                    "insert into ARCHIEVE.dbo.LF_ARSIP values (" +
                                    "'" + code + "'," +
                                    "'LF_01'," +
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

                    FillDGRMedicalDoc();
                }
            }
        }

        protected void LBT_ARCHIEVE_Click(object sender, EventArgs e)
        {
            if (TXT_ARCHIEVE.Text.Trim() == "")
                return;

            if (!FU_ARCHIEVE.HasFile)
                return;

            string filename = Path.GetFileName(FU_ARCHIEVE.FileName);
            string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            FU_ARCHIEVE.SaveAs(fullpath);
            conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
            conn.ExecuteQuery();
            string code = conn.GetFieldValue(0, 0).ToString();
            string SQL = "insert into ARCHIEVE.dbo.LF_ARSIP select " +
                            "ARCHIEVE.dbo.UFN_GET_NEWID()," +
                            "'LF_03'," +
                            "'" + LB_REGNO.Text + "'," +
                            "null," +
                            "null," +
                            "'" + TXT_ARCHIEVE.Text.Trim().Replace("'", "`") + "'," +
                            "'" + filename + "'," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," +
                            "@File";
            GlobalUse.FileToSQL(fullpath, SQL);

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            LoadArchieve();
        }

        protected void DGR_ARCHIEVE_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from ARCHIEVE.dbo.LF_ARSIP where CODE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                LoadArchieve();
            }

            if (e.CommandName == "Download")
            {
                string filename = e.Item.Cells[1].Text.Replace(" ", "");
                GlobalUse.SQLToFile(filename.Trim(),
                                    "select THEFILE from ARCHIEVE.dbo.LF_ARSIP where CODE='" + e.Item.Cells[0].Text + "'",
                                    Page);
            }
        }
    }
}