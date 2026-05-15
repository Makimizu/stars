using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;
using System.EnterpriseServices.CompensatingResourceManager;

namespace AGR
{
    public partial class Agent_Registration : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TXT_CODE.Text = Request.QueryString["AGENTCODE"];
                Setup();
                LoadRecord();

            }
        }

        [System.Web.Services.WebMethod]
        public static string CheckAgent(string name, string dob)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(dob))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(dob))
            {
                return null;
            }

            Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            conn.QueryString = "select ALERT=[MARKETING].[dbo].[UFN_CHECK_AGENT_BLACKLISTED] ('" + name.Trim().ToUpper() + "', '" + dob.Trim() + "')";
            conn.ExecuteQuery();

            return conn.GetFieldValue("ALERT").ToString();

        }

        protected void Setup()
        {
            if (TXT_CODE.Text == "")
            {
                DV_BUTTONS.Visible = false;
            }

            conn.QueryString = "select CODE, DESCR from [CLIENT_BASE].[dbo].[PR_GENDER] order by CODE desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_GENDER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            BT_REFERRAL.Attributes.Add("onclick", "window.open('AGENT_REFERRAL_SEARCH.aspx?code=" + TXT_CODE.Text + "','REFERRAL','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
        }

        protected void LoadRecord()
        {
            LB_WARNING.Text = "";
            LB_WARNING.CssClass = "block";

            conn.QueryString = "select " +
                                "CODE = a.CODE, " +
                                "FRONT_NAME = a.FRONT_NAME, " +
                                "MID_NAME = a.MID_NAME, " +
                                "LAST_NAME = a.LAST_NAME, " +
                                "DOB = convert(varchar(20), a.DOB, 103), " +
                                "POB = a.POB, " +
                                "GENDER = a.GENDER, " +
                                "JOINTDATE = convert(varchar(20), a.JOINTDATE, 103), " +
                                "PHONE = a.PHONE, " +
                                "EMAIL = a.EMAIL, " +
                                "PHOTO = a.PHOTO, " +
                                "REFERRAL_CODE = a.REFERRAL_CODE, " +
                                "REFERRAL_NAME = (case when a.REFERRAL_NAME is not null then '- ' + a.REFERRAL_NAME else '' end) ," +
                                "AGENT_STATUS = (case when a.ACTIVE = 1 then 'ACTIVE' else 'INACTIVE' end), " +
                                "AREA = ac.DESCR, " +
                                "ALERT = [MARKETING].[dbo].[UFN_IS_AGENT_BLACKLISTED](a.CODE) " +
                                "from [MARKETING].[dbo].[V_M_AGENTS] a " +
                                "left join	[MARKETING].[dbo].[M_AREA_AGENT] ab on a.CODE = ab.AGENT_CODE " +
                                "left join   [MARKETING].[dbo].[M_AREA] ac on ab.AREA_CODE = ac.CODE " +
                                "where " +
                                "a.CODE = '" + TXT_CODE.Text + "'";
            conn.ExecuteQuery(1000);
            string code = conn.GetFieldValue("CODE").ToString();
            //  TXT_CODE.Text = conn.GetFieldValue("CODE").ToString();
            TXT_FRONTNAME.Text = conn.GetFieldValue("FRONT_NAME").ToString();
            TXT_MIDNAME.Text = conn.GetFieldValue("MID_NAME").ToString();
            TXT_LASTNAME.Text = conn.GetFieldValue("LAST_NAME").ToString();
            TXT_DOB.Text = conn.GetFieldValue("DOB").ToString();
            TXT_POB.Text = conn.GetFieldValue("POB").ToString();
            TXT_JOINTDATE.Text = conn.GetFieldValue("JOINTDATE").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();
            TXT_PHONE.Text = conn.GetFieldValue("PHONE").ToString();
            LB_REFERRAL_CODE.Text = conn.GetFieldValue("REFERRAL_CODE").ToString();
            LB_REFERRAL_NAME.Text = conn.GetFieldValue("REFERRAL_NAME").ToString();
            LB_STATUS.Text = conn.GetFieldValue("AGENT_STATUS").ToString();
            LB_AREA.Text = conn.GetFieldValue("AREA").ToString();

            if (LB_STATUS.Text == "INACTIVE")
            {
                LB_STATUS.ForeColor = System.Drawing.Color.Red;
            }

            DDL_GENDER.SelectedValue = conn.GetFieldValue("GENDER").ToString();

            string alert = conn.GetFieldValue("ALERT").ToString();

            IMG_PHOTO.ImageUrl = GlobalUse.GetStringImageURL("select PHOTO from [MARKETING].[dbo].[V_M_AGENTS] where CODE = '" + TXT_CODE.Text + "'", "PHOTO", conn);

            if (!string.IsNullOrEmpty(alert))
            {
                LB_WARNING.Text = alert;
                LB_WARNING.CssClass = "alert";
            }

            if (TXT_CODE.Text != "")
            {
                LoadPersonalInfo();
            }
                
        }

        protected void LoadPersonalInfo()
        {
            LBL_TITLE.Text = BT_BANKACCOUNT.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AgentBody.location.href = 'AGENT_PERSONAL_INFO.ASPX?code=" + TXT_CODE.Text + "';</script>");
        }

        protected void BT_ARCHIVE_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AgentBody.location.href = 'AGENT_DOCUMENT.ASPX?code=" + TXT_CODE.Text + "';</script>");
        }

        protected void BT_BANKACCOUNT_Click(object sender, EventArgs e)
        {
            LoadPersonalInfo();
        }

        protected void BT_AGENCY_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AgentBody.location.href = 'AGENT_MOVEMENT_ACTIVE.ASPX?code=" + TXT_CODE.Text + "';</script>");
        }

        protected void BT_EDUCATION_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.AgentBody.location.href = 'AGENT_EDUCATION_WORK.ASPX?code=" + TXT_CODE.Text + "';</script>");
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            //email validation
            if (TXT_EMAIL.Text.Contains("@") == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Wrong Email Format');", true);
                return;
            }

            conn.QueryString = "exec [MARKETING].[dbo].[SP_M_AGENT_UPSERT] " +
                                "@CODE = '" + TXT_CODE.Text + "', " +
                                "@FRONT_NAME =  '" + TXT_FRONTNAME.Text + "'," +
                                "@MID_NAME =  '" + TXT_MIDNAME.Text + "'," +
                                "@LAST_NAME =  '" + TXT_LASTNAME.Text + "'," +
                                "@DOB = '" + GlobalUse.GlobalDateFormat(TXT_DOB.Text, "d/M/yyyy") + "'," +
                                "@POB =  '" + TXT_POB.Text + "'," +
                                "@JOINTDATE = '" + GlobalUse.GlobalDateFormat(TXT_JOINTDATE.Text, "d/M/yyyy") + "'," +
                                "@GENDER = '" + DDL_GENDER.SelectedValue + "', " +
                                "@PHONE = '" + TXT_PHONE.Text + "'," +
                                "@EMAIL = '" + TXT_EMAIL.Text + "'," +
                                "@REFERRAL_CODE = '" + LB_REFERRAL_CODE.Text.Trim() + "'," +
                                "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";


            conn.ExecuteQuery();
                        
            if (conn.GetFieldValue("CODE").ToString() == "")
            {
                return;


            }
            else
            {

                if (TXT_CODE.Text == "")
                {
                    Response.Redirect("Agent_Frame.aspx?AGENTCODE=" + conn.GetFieldValue("CODE").ToString());
                }
                else
                {
                    Response.Redirect("AGENT_REGISTRATION.aspx?AGENTCODE=" + conn.GetFieldValue("CODE").ToString());
                }
            }

        }
    }
}