using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Policy
{
    public partial class ApplicationDocumentLetter : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //LB_REGNO.Text = Request.QueryString["ID"].ToString();
                //LoadDocument();
                //LoadArchieve();
            }
        }

        //protected void LoadDocument()
        //{
        //    conn.QueryString = "exec SP_APPLICATION_DOCUMENT_LETTER '" + LB_REGNO.Text + "'";
        //    conn.ExecuteQuery();

        //    DataTable dt;
        //    dt = new DataTable();
        //    dt = conn.GetDataTable().Copy();
        //    DGR.DataSource = dt;
        //    DGR.DataBind();

        //    for (int i = 0; i < DGR.Items.Count; i++)
        //    {
        //        LinkButton lbtREPORT = (LinkButton)DGR.Items[i].FindControl("LBT_REPORT");
        //        lbtREPORT.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
        //    }
        //}

        //protected void LoadArchieve()
        //{
        //    string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_03", LB_REGNO.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
        //    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appdocumentarchieve.location.href = '" + URL + "';</script>");
        //}

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Report")
            {
                if (e.Item.Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    //Response.Redirect(e.Item.Cells[3].Text.Replace("&nbsp;", ""));
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appdocumentarchieve.location.href = '" + e.Item.Cells[3].Text.Replace("&nbsp;", "") + "';</script>");
                }
            }
        }
    }
}