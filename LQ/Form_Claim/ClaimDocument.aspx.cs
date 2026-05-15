using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Claim
{
    public partial class ClaimDocument : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LoadDocument();
                LoadArchieve();
            }
        }

        protected void LoadDocument()
        {
            conn.QueryString = "exec SP_APPLICATION_CLAIM_DOCUMENT '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
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
                    lbDESCR.Text = DGR_DOCS.Items[i].Cells[1].Text;
                }
            }
        }

        protected void LoadArchieve()
        {
            string URL = GlobalUse.GetArsipURL("LF", "LF_04", LB_REGNO.Text + "-" + LB_SEQ.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.claimdocbody.location.href = '" + URL + "';</script>");
        }

        protected void DGR_DOCS_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                string filename = e.Item.Cells[3].Text.Replace(" ", "");
                GlobalUse.SQLToFile(filename.Trim(),
                                    "select THEFILE from ARCHIEVE.dbo.LF_ARSIP where CODE='" + e.Item.Cells[2].Text + "'",
                                    Page);
            }

        }
    }
}