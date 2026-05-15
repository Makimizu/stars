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
    public partial class QuotationHeader : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LoadRecord();
            }
        }

        protected void LoadRecord()
        {
            LB_WARNING.Text = "";
            LB_WARNING.CssClass = "";

            conn.QueryString = "exec SP_APPLICATION_MASTER_HEADER " +
                                "'" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_FULLNAME.Text = conn.GetFieldValue("MAIN_INSURED").ToString();
            LB_POLICY_HOLDER.Text = conn.GetFieldValue("POLICY_HOLDER").ToString();
            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_PRODUCTGROUP.Text = conn.GetFieldValue("PRODUCT_GROUP_DESCR").ToString();
            LB_PRODUCTNAME.Text = conn.GetFieldValue("PRODUCT_DESCR").ToString();
            LB_STARTAGE.Text = conn.GetFieldValue("START_AGE").ToString();
            LB_UWCODE.Text = conn.GetFieldValue("UW_CODE_DESCR").ToString();

            LB_SUMINS.Text = conn.GetFieldValue("SUMINS").ToString();
            LB_POLICY_PERIOD.Text = conn.GetFieldValue("POLICY_PERIOD").ToString();
            LB_PAYMENT_PERIOD.Text = conn.GetFieldValue("PAYMENT_PERIOD").ToString();

            LB_INPUTER.Text = conn.GetFieldValue("INPUTER").ToString();

            //LB_AGENT_NAME.Text = conn.GetFieldValue("AGENT_NAME").ToString();
            //LB_AGENT_CODE.Text = conn.GetFieldValue("AGENT_CODE").ToString();            
            //LB_CHANNEL.Text = conn.GetFieldValue("AGENT_CHANNEL").ToString();
            //LB_LEVEL.Text = conn.GetFieldValue("AGENT_LEVEL").ToString();
            //LB_AGENCY.Text = conn.GetFieldValue("AGENCY_NAME").ToString();


            if (conn.GetFieldValue("MEMBERID").ToString() == "")
            {
                BT4.Enabled = false;
                BT4.BackColor = System.Drawing.Color.Gray;
            }

            string status1 = conn.GetFieldValue("MAIN_INSURED_BLOCK_STATUS").ToString();
            string status2 = conn.GetFieldValue("POLICY_HOLDER_BLOCK_STATUS").ToString();
            string warning = "";

            if (!string.IsNullOrEmpty(status1) && !string.IsNullOrWhiteSpace(status1))
            {
                warning = status1;
            }

            if (!string.IsNullOrEmpty(status2) && !string.IsNullOrWhiteSpace(status2))
            {
                if (warning == "")
                {
                    warning = status2;
                }
                else
                {
                    warning += "<br>" + status2;
                }
            }

            if (warning != "")
            {
                LB_WARNING.Text = warning;
                LB_WARNING.CssClass = "alert";
            }

            ShowPolicyInfo();
            ShowButton();
        }

        protected void ShowButton()
        {
            conn.QueryString = "exec SP_APPLICATION_TRACK_BUTTON '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            LB_TRACK.Text = conn.GetFieldValue("TRACK_NEXT").ToString();
            if (conn.GetFieldValue("SHOW").ToString() == "1")
            {
                BT_NEXT.Visible = true;
                BT_NEXT.Text = conn.GetFieldValue("TRACK_NEXT_DESCR").ToString();
                BT_NEXT.Attributes.Add("onclick", "if(!confirm('Are you sure to go to \"" + BT_NEXT.Text + "\" ?')){return false;}else{ShowProgress();}");
            }
            else
            {
                BT_NEXT.Visible = false;
            }
        }

        protected void ShowPolicyInfo()
        {
            conn.QueryString = "select " +
                                "POLICY_INFO	= 'QuotationPolicyInfo_P' + convert(char(1), a.PAYDI) + '.aspx?REGNO=' + a.REGNO + '&U=' + convert(char(1), a.UNITIZE) " +
                                "from		V_APPLICATION_MASTER a " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_TITLE.Text = BT2.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = '" + conn.GetFieldValue("POLICY_INFO").ToString() + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'QuotationPersonHeader.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            ShowPolicyInfo();
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'QuotationDoc.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'QuotationQuestionFrame.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'QuotationRemark.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_PRINT_VALIDATION_LOG '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                LB_TITLE.Text = ((Button)sender).Text;
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'QuotationReportFrame.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
            }
            else
            {
                LB_TITLE.Text = "VALIDATION";
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'QuotationValidationLog.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
            }
        }

        protected void BT_NEXT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_VALIDATION_LOG '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                LB_TITLE.Text = "VALIDATION";
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'QuotationValidationLog.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
            }
            else
            {
                conn.QueryString = "exec SP_APPLICATION_TRACK_INSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    LB_TRACK.Text + "," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery(150000);
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'QuotationList.aspx?TRACK=" + (int.Parse(LB_TRACK.Text) - 1).ToString() + "';</script>");
            }

        }

        protected void BT7_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.QuotationBody.location.href = 'QuotationAgentQuestionFrame.aspx?REGNO=" + LB_REGNO.Text + "';</script>");
        }
    }
}