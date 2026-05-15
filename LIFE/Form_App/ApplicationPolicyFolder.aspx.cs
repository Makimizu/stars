using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Runtime.InteropServices;
using System.Data.OleDb;

namespace LIFE.Form_App
{
    public partial class ApplicationPolicyFolder : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select * from V_XPRINS_FTP order by ID";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR.Items[i].FindControl("LB_SELECT");
                lbt.Text = DGR.Items[i].Cells[2].Text;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LB_TITLE.Text = e.Item.Cells[2].Text;

                //DataTable dtfilename = GlobalUse.FTPBrowseSpecificFiles(e.Item.Cells[1].Text, e.Item.Cells[3].Text, e.Item.Cells[4].Text);
                DataTable dtfilename = GlobalUse.FTPBrowseAllFiles(e.Item.Cells[1].Text, e.Item.Cells[3].Text);

                DGR_FILES.DataSource = dtfilename;
                DGR_FILES.DataBind();

                for (int i = 0; i < DGR_FILES.Items.Count; i++)
                {
                    LinkButton lbt = (LinkButton)DGR_FILES.Items[i].FindControl("LB_SELECT");
                    lbt.Text = DGR_FILES.Items[i].Cells[1].Text;
                }
            }
        }

        protected void DGR_FILES_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                GlobalUse.FTPDownloadFile(this, "6", e.Item.Cells[0].Text, e.Item.Cells[1].Text);
            }
        }
    }
}