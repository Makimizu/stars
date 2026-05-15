using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klaim
{
    public partial class PenjaminanSurat : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["NOSURAT"].ToString();
                FillDGRReport();
            }

        }

        protected void FillDGRReport()
        {
            conn.QueryString = "exec SP_CLM_SURAT_JAMINAN_REPORT '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REPORT.DataSource = dt;
            DGR_REPORT.DataBind();

            for (int i = 0; i < DGR_REPORT.Items.Count; i++)
            {
                Button bt1 = (Button)DGR_REPORT.Items[i].FindControl("BT_PDF1");
                Button bt2 = (Button)DGR_REPORT.Items[i].FindControl("BT_PDF2");
                DropDownList ddl = (DropDownList)DGR_REPORT.Items[i].FindControl("DDL_KIRIM");
                TextBox txtemailfax = (TextBox)DGR_REPORT.Items[i].FindControl("TXT_EMAILFAX");

                if (DGR_REPORT.Items[i].Cells[2].Text == "&nbsp;")
                    bt1.Enabled = false;
                if (DGR_REPORT.Items[i].Cells[3].Text == "&nbsp;")
                    bt2.Enabled = false;

                txtemailfax.Text = DGR_REPORT.Items[i].Cells[4].Text.Replace("&nbsp;", "");
            }
        }

        protected void DGR_REPORT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_KIRIM");
            TextBox txtemailfax = (TextBox)e.Item.FindControl("TXT_EMAILFAX");

            if (e.CommandName == "PDF")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + e.Item.Cells[2].Text + "','PENJAMINAN','height=400px,width=1100px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
            }

            if (e.CommandName == "PDF_EN")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + e.Item.Cells[3].Text + "','PENJAMINAN','height=400px,width=1100px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
            }

            if (e.CommandName == "Send")
            {
                LB_SURAT_ERROR.Text = "";
                if (txtemailfax.Text.Trim() == "")
                    return;

                string recipients = txtemailfax.Text.Trim().Replace(";", ",").Replace(" ", "");
                if (ddl.SelectedValue == "1")
                {
                    recipients = recipients.Replace("-", "").Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "");
                    recipients = recipients + "@takaful.fax";
                }

                conn.QueryString = "exec SP_EMAIL_BODY_SURAT_JAMINAN " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "EmployeeName") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();
                string sbj = conn.GetFieldValue("sbj").ToString();
                string body = conn.GetFieldValue("body").ToString();

                string path = Request.PhysicalApplicationPath + "Upload";
                string[] attachment = new string[1];

                string emailsender = e.Item.Cells[8].Text.Replace("&nbsp;", "");
                if (emailsender == "")
                    emailsender = GlobalUse.GetUserMgmt(Session["s"].ToString(), "Email");

                try
                {
                    attachment[0] = GlobalUse.RenderReport(e.Item.Cells[6].Text, "PDF", path, LB_ID.Text, "", "", "", "", "", "", "", "", "");
                    GlobalUse.SendEmail(emailsender, recipients, emailsender, "", sbj, body, attachment);

                    if (File.Exists(attachment[0]))
                        File.Delete(attachment[0]);
                }
                catch (System.Exception ex)
                {
                    LB_SURAT_ERROR.ForeColor = System.Drawing.Color.Red;
                    LB_SURAT_ERROR.Text = ex.Message;
                    return;
                }

                LB_SURAT_ERROR.ForeColor = System.Drawing.Color.Blue;
                LB_SURAT_ERROR.Text = "SUKSES";
            }
        }

        protected void DDL_KIRIM_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_REPORT.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_REPORT.Items[i].FindControl("DDL_KIRIM");
                TextBox txtemailfax = (TextBox)DGR_REPORT.Items[i].FindControl("TXT_EMAILFAX");

                if (ddl == (DropDownList)sender)
                {
                    if (ddl.SelectedValue == "0")
                        txtemailfax.Text = DGR_REPORT.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                    if (ddl.SelectedValue == "1")
                        txtemailfax.Text = DGR_REPORT.Items[i].Cells[5].Text.Replace("&nbsp;", "");
                }
            }
        }
    }
}