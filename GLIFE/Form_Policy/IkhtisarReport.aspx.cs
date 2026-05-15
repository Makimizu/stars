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
    public partial class IkhtisarReport : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                LoadDocument();
                //LoadArchieve();
            }
        }

        protected void LoadDocument()
        {
            conn.QueryString = "select * from policy where POLICY_NO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("PRODUCT_GROUP").ToString() == "GTL")
            {
                conn.QueryString = "select " +
                                "b.CODE,  " +
                                "b.DESCR,  " +
                                "DOCNO = 'VIEW', " +
                                "REPORT_URL   = b.URLAPP + '&POLICY_NO=' + a.POLICY_NO  collate database_default,  " +
                                "REPORT_APP_ID = a.APP_ID,  " +
                                "REPORT_CODE = b.CODE  " +
                            "from IKHTISAR_POLICY a " +
                            "INNER JOIN V_LINK_SC_REPORT_LIST b ON b.APP_ID = a.APP_ID COLLATE DATABASE_DEFAULT AND b.CODE IN ('72', '71', '76') " +
                            "where a.POLICY_NO = '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
            }
            else
            {
                conn.QueryString = "select " +
                                "b.CODE,  " +
                                "b.DESCR,  " +
                                "DOCNO = 'VIEW', " +
                                "REPORT_URL   = b.URLAPP + '&POLICY_NO=' + a.POLICY_NO  collate database_default,  " +
                                "REPORT_APP_ID = a.APP_ID,  " +
                                "REPORT_CODE = b.CODE  " +
                            "from IKHTISAR_POLICY_BANCASS a " +
                            "INNER JOIN V_LINK_SC_REPORT_LIST b ON b.APP_ID = a.APP_ID COLLATE DATABASE_DEFAULT AND b.CODE IN ('75', '74', '77') " +
                            "where a.POLICY_NO = '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
            }

            

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbtREPORT = (LinkButton)DGR.Items[i].FindControl("LBT_REPORT");
                lbtREPORT.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
            }
        }

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