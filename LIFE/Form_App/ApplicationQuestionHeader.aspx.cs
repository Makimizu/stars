using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_App
{
    public partial class ApplicationQuestionHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LQ"));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LoadButtons();
                LoadFirstGroup();
            }
        }

        protected void LoadButtons()
        {
            conn.QueryString = "exec SP_APPLICATION_QUESTION_GROUP '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DGR_BUTTON.DataSource = conn.GetDataTable().Copy();
            DGR_BUTTON.DataBind();

            for (int i = 0; i < DGR_BUTTON.Items.Count; i++)
            {
                Button bt = (Button)DGR_BUTTON.Items[i].FindControl("BT_GROUP");
                bt.Text = DGR_BUTTON.Items[i].Cells[1].Text;
            }
        }

        protected void DGR_BUTTON_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                string URL = "";
                if (e.Item.Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    URL = Crypto.EncryptStringAES(e.Item.Cells[3].Text.Replace("&nbsp;", ""));
                    while (URL.IndexOf("/") >= 0 || URL.IndexOf("+") >= 0)
                        URL = Crypto.EncryptStringAES(e.Item.Cells[3].Text.Replace("&nbsp;", ""));
                }

                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuestionBody.location.href = 'ApplicationQuestion.aspx?REGNO=" + LB_REGNO.Text + "&GROUP=" + e.Item.Cells[0].Text + "&MEMBERID=" + e.Item.Cells[2].Text + "&URL=" + URL + "';</script>");
            }
        }

        protected void LoadFirstGroup()
        {
            string URL = "";
            if (DGR_BUTTON.Items[0].Cells[3].Text.Replace("&nbsp;", "") != "")
            {
                URL = Crypto.EncryptStringAES(DGR_BUTTON.Items[0].Cells[3].Text.Replace("&nbsp;", ""));
                while (URL.IndexOf("/") >= 0 || URL.IndexOf("+") >= 0)
                    URL = Crypto.EncryptStringAES(DGR_BUTTON.Items[0].Cells[3].Text.Replace("&nbsp;", ""));
            }

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuestionBody.location.href = 'ApplicationQuestion.aspx?REGNO=" + LB_REGNO.Text + "&GROUP=" + DGR_BUTTON.Items[0].Cells[0].Text + "&MEMBERID=" + DGR_BUTTON.Items[0].Cells[2].Text + "&URL=" + URL + "';</script>");
        }
    }
}