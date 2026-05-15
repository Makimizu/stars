using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Tools
{
    public partial class Letters : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                LoadLetters();
            }
        }

        protected void LoadLetters()
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER_LETTERS " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LETTER.DataSource = dt;
            DGR_LETTER.DataBind();


            for (int i = 0; i < DGR_LETTER.Items.Count; i++)
            {
                TextBox txtDOCNO = (TextBox)DGR_LETTER.Items[i].FindControl("TXT_DOCNO");
                TextBox txtDATE = (TextBox)DGR_LETTER.Items[i].FindControl("TXT_DATE");

                txtDOCNO.Text = DGR_LETTER.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtDATE.Text = DGR_LETTER.Items[i].Cells[2].Text.Replace("&nbsp;", "");
            }
        }

        protected void DGR_LETTER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "PDF")
            {
            }

            if (e.CommandName == "Email")
            {
            }
        }
    }
}