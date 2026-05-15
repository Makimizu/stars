using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace Archieve
{
    public partial class ArchieveHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_APPID.Text = Request.QueryString["APPID"].ToString();
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_ARCHIEVE_TYPE '" + LB_APPID.Text + "'";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button bt = (Button)DGR.Items[i].FindControl("BT");

                conn.QueryString = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                conn.ExecuteQuery();
                bt.Text = DGR.Items[i].Cells[1].Text.ToUpper() + " : " + conn.GetFieldValue(0, 0).ToString();

                if (conn.GetFieldValue(0, 0).ToString() != "0")
                {
                    bt.Font.Bold = true;
                }
                else
                {
                    bt.Enabled = false;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.archievebody.location.href = 'ArchieveDetail.aspx?APPID=" + LB_APPID.Text + "&TYPE=" + e.Item.Cells[0].Text + "';</script>");
            }
        }
    }
}