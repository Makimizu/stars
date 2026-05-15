using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;


namespace AGR
{
    public partial class ENDORSEMENT_APPROVAL : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PARAM_ENDORSEMENT_TYPE";
            conn.ExecuteQuery();
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            try
            {
                DDL_TYPE.SelectedValue = Request.QueryString["mode"].ToString();
                LB_TITLE.Text = DDL_TYPE.SelectedItem.Text;
            }
            catch { }
        }

        protected void FillDGR()
        {
            string where = "";
            LB_RECORDS.Text = "";

            if (DDL_TYPE.SelectedValue != "")
                where = where + " and a.MODE='" + DDL_TYPE.SelectedValue + "' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            conn.QueryString = "select " +
                                "CODE, " +
                                "FULLNAME, " +
                                "MODE, " +
                                "SEQ, " +
                                "MODE_DESCR, " +
                                "REMUN_TYPE, " +
                                "REMARK, " +
                                "URL, " +
                                "COLOR, " +
                                "REQUESTBY	= REQUESTBY + ' (' + convert(varchar(100), REQUESTDATE) + ')', " +
                                "UploadOriginalFileName, " +
                                "UploadStoredFileName, " +
                                "UploadFilePath, " +
                                "UploadContentType " +
                                "from		V_ENDORSEMENT_APPROVAL a " +
                                "where 1=1  " + where +
                                "order by a.REQUESTDATE";
            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                Button btAPPROVE = (Button)DGR.Items[i].FindControl("BT_APPROVE");
                Button btREJECT = (Button)DGR.Items[i].FindControl("BT_REJECT");

                lb.Text = DGR.Items[i].Cells[1].Text;
                btAPPROVE.Attributes.Add("onclick", "if(!confirm('Are you sure to APPROVE ?')){return false;};");
                btREJECT.Attributes.Add("onclick", "if(!confirm('Are you sure to REJECT ?')){return false;};");

                DGR.Items[i].Cells[7].BackColor = System.Drawing.Color.FromName(DGR.Items[i].Cells[4].Text);
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "DownloadDoc")
            {
                string agentCode = e.Item.Cells[1].Text;

                conn.QueryString =
                    "SELECT TOP 1 UploadOriginalFileName, UploadStoredFileName, UploadFilePath, UploadContentType " +
                    "FROM M_AGENT_HOLD_REMUN " +
                    "WHERE CODE = '" + agentCode + "' " +
                    "ORDER BY REQUESTDATE DESC";

                conn.ExecuteQuery();

                if (conn.GetRowCount() == 0)
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(), "nofile",
                        "alert('Tidak ada file yang diupload untuk agent ini');",
                        true
                    );
                    return;
                }

                string originalName = conn.GetFieldValue(0, "UploadOriginalFileName").ToString();
                string filePath = conn.GetFieldValue(0, "UploadFilePath").ToString();
                string contentType = conn.GetFieldValue(0, "UploadContentType").ToString();

                if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(), "nofilephysical",
                        "alert('File tidak ditemukan di server');",
                        true
                    );
                    return;
                }


                Response.Clear();
                Response.ContentType = contentType;
                Response.AddHeader(
                    "Content-Disposition",
                    "attachment; filename=\"" + originalName + "\""
                );
                Response.TransmitFile(filePath);
                Response.End();
            }

            if (e.CommandName == "Archieve")
            {
                string URL = "ENDORSEMENT_ARCHIEVE_FRAME.aspx?ID=" + e.Item.Cells[1].Text + "&USERID=" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "&mode=" + e.Item.Cells[2].Text;
                Response.Redirect(URL);
            }

            if (e.CommandName == "Detail")
            {
                Response.Redirect("Agent_Frame.aspx?AGENTCODE=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Approve")
            {
                conn.QueryString = "exec SP_ENDORSEMENT_APPROVAL " +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "'" + e.Item.Cells[2].Text + "'," +
                                    "'" + e.Item.Cells[3].Text + "'," +
                                    "'" + 1 + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                ////email notification uncomment email kalo naik ke prod
                //conn.QueryString = "exec SP_ENDORSEMENT_APPROVAL_EMAIL " +
                //                    "'" + e.Item.Cells[1].Text + "'," +
                //                    "'" + e.Item.Cells[2].Text + "'," +
                //                    "'" + e.Item.Cells[3].Text + "' ";
                //conn.ExecuteQuery();

                DGR.CurrentPageIndex = 0;
                FillDGR();
            }

            if (e.CommandName == "Reject")
            {
                conn.QueryString = "exec SP_ENDORSEMENT_APPROVAL " +
                                    "'" + e.Item.Cells[1].Text + "'," +
                                    "'" + e.Item.Cells[2].Text + "'," +
                                    "'" + e.Item.Cells[3].Text + "'," +
                                    "'" + 0 + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }
    }
}