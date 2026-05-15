using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationQuestionHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LoadButtons();
                LoadFirstGroup();
                ShowSignature();
            }
        }

        protected void ShowSignature()
        {
            conn.QueryString = "select " +
                                "SIGNATURE		= SIGNATURE, " +
                                "AGREEMENT_DATE	= dbo.UFN_SET_INDO_DATE(AGREEMENT_DATE) + '  ' + convert(varchar(100), AGREEMENT_DATE, 108) " +
                                "from LQ.dbo.APPLICATION_AGREEMENT  " +
                                "where REGNO = '" + LB_REGNO.Text + "' " +
                                "and AGREEMENT_DATE is not null";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                TR_SEND_EMAIL.Visible = true;
                TR_SIGNATURE.Visible = false;
            }
            else
            {
                TR_SEND_EMAIL.Visible = false;
                TR_SIGNATURE.Visible = true;

                LB_AGREEMENT_DATE.Text = conn.GetFieldValue("AGREEMENT_DATE").ToString();
                try
                {
                    IMG_SIGNATURE.ImageUrl = conn.GetFieldValue("SIGNATURE").ToString();
                }
                catch { }
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

                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuestionBody.location.href = 'QuotationQuestion.aspx?REGNO=" + LB_REGNO.Text + "&GROUP=" + e.Item.Cells[0].Text + "&MEMBERID=" + e.Item.Cells[2].Text + "&URL=" + URL + "';</script>");
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

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuestionBody.location.href = 'QuotationQuestion.aspx?REGNO=" + LB_REGNO.Text + "&GROUP=" + DGR_BUTTON.Items[0].Cells[0].Text + "&MEMBERID=" + DGR_BUTTON.Items[0].Cells[2].Text + "&URL=" + URL + "';</script>");
        }

        protected void BT_SEND_EMAIL_Click(object sender, EventArgs e)
        {
            LB_VALIDATION.Text = "";
            conn.QueryString = "exec SP_APPLICATION_QUESTION_VALIDATION '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                conn.QueryString = "exec SP_APPLICATION_AGREEMENT_EMAIL '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
            }
            else
            {
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    LB_VALIDATION.Text = LB_VALIDATION.Text + "<tr style='vertical-align:top;'><td style='width:10px;'>-</td><td>" + conn.GetFieldValue(i, 0).ToString() + "</td></tr>";
                }
                LB_VALIDATION.Text = "<table style='border-spacing:0px;width:90%;'>" + LB_VALIDATION.Text + "</table>";
            }
        }
    }
}