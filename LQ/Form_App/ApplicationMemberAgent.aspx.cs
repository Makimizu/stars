using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_App
{
    public partial class ApplicationMemberAgent : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString("LF"));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();

                Setup();
                FillDGRMember();
                FillDGRAgent();
                LoadAgentQuestion();
                CheckTrack();

                ShowMemberList();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from LIFE.dbo.PR_CLIENT_ADDRESS_TYPE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ADDTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE,DESCR from LIFE.dbo.PR_CLIENT_BANK_ACCOUNT_TYPE order by 1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLProvince();
            FillDDLCountry();
            FillDDLBank();


            LoadAddress();
            LoadAccount();
        }

        protected void LoadAddress()
        {
            TXT_ADDRESS.Text = "";
            TXT_CITY.Text = "";
            TXT_ZIPCODE.Text = "";
            TXT_PHONE1.Text = "";
            TXT_PHONE2.Text = "";
            TXT_EMAIL.Text = "";
            DDL_PROVINCE.SelectedIndex = 0;
            DDL_COUNTRY.SelectedIndex = 0;

            conn.QueryString = "exec SP_APPLICATION_ADDRESS '" + LB_REGNO.Text + "','" + DDL_ADDTYPE.SelectedValue + "'";
            conn.ExecuteQuery();

            TXT_ADDRESS.Text = conn.GetFieldValue("ADDRESS").ToString();
            TXT_CITY.Text = conn.GetFieldValue("CITY").ToString();
            TXT_ZIPCODE.Text = conn.GetFieldValue("ZIPCODE").ToString();
            TXT_PHONE1.Text = conn.GetFieldValue("PHONE").ToString();
            TXT_PHONE2.Text = conn.GetFieldValue("PHONE2").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();

            try
            {
                DDL_PROVINCE.SelectedValue = conn.GetFieldValue("PROVINCE").ToString();
            }
            catch { }

            try
            {
                DDL_COUNTRY.SelectedValue = conn.GetFieldValue("COUNTRY").ToString();
            }
            catch { }
        }

        protected void LoadAccount()
        {
            TXT_ACCNO.Text = "";
            TXT_ACCNAMA.Text = "";
            DDL_BANK.SelectedIndex = 0;

            conn.QueryString = "exec SP_APPLICATION_BANK_ACCOUNT '" + LB_REGNO.Text + "','" + DDL_ACCTYPE.SelectedValue + "'";
            conn.ExecuteQuery();

            TXT_ACCNO.Text = conn.GetFieldValue("ACCNO").ToString();
            TXT_ACCNAMA.Text = conn.GetFieldValue("ACCNAME").ToString();

            try
            {
                DDL_BANK.SelectedValue = conn.GetFieldValue("ACCBANK").ToString();
            }
            catch { }
        }

        protected void FillDDLProvince()
        {
            DDL_PROVINCE.Items.Clear();
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_PROPINSI ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDDLCountry()
        {
            DDL_COUNTRY.Items.Clear();
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COUNTRY";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_COUNTRY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDDLBank()
        {
            DDL_BANK.Items.Clear();

            DDL_BANK.Items.Add(new ListItem("", ""));
            conn.QueryString = "select CODE = KODE, DESCR = KODE + ' - ' + BANK from FINANCE.dbo.PARAM_TBL_BANK where isnull(KODE, '') <> '' order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void TXT_PROVINCE_TextChanged(object sender, EventArgs e)
        {
            FillDDLProvince();
        }

        protected void TXT_COUNTRY_TextChanged(object sender, EventArgs e)
        {
            FillDDLCountry();
        }

        protected void TXT_BANK_TextChanged(object sender, EventArgs e)
        {
            FillDDLBank();
        }

        protected void DDL_ADDTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAddress();
        }

        protected void DDL_ACCTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAccount();
        }

        protected void LoadAgentQuestion()
        {
            conn.QueryString = "select * from LQ.dbo.V_APPLICATION_QUESTION_MODULAR_GROUP where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
                return;

            string memberid = "";
            conn.QueryString = "select MEMBER_ID from APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            memberid = conn.GetFieldValue("MEMBER_ID").ToString();

            IF_AGENT.Src = "ApplicationQuestion.aspx?REGNO=" + LB_REGNO.Text + "&GROUP=99-AGN&MEMBERID=" + memberid + "&URL=";
        }

        protected void CheckTrack()
        {
            conn.QueryString = "select TRACK = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'UW', '')";
            conn.ExecuteQuery();

            int track = int.Parse(conn.GetFieldValue("TRACK").ToString());
            if (track >= 3)
            {
                //BT_SAVE.Visible = false;
                //DGR.Enabled = false;
            }
        }

        protected void FillDGRMember()
        {
            conn.QueryString = "exec SP_APPLICATION_MEMBER '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MEMBER.DataSource = dt;
            DGR_MEMBER.DataBind();

            for (int i = 0; i < DGR_MEMBER.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR_MEMBER.Items[i].FindControl("LBT_FULLNAME");
                lbt.Text = DGR_MEMBER.Items[i].Cells[1].Text;
            }
        }

        protected void FillDGRAgent()
        {
            conn.QueryString = "exec SP_APPLICATION_AGENT '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AGENT.DataSource = dt;
            DGR_AGENT.DataBind();
        }

        protected void DGR_MEMBER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ViewPersonalDetail(e.Item.Cells[0].Text, e.Item.Cells[4].Text);
            }
        }

        protected void ViewPersonalDetail(string memberid, string status)
        {
            LB_MEMBERTITLE.Text = status;
            IF.Src = "PersonalDetail.aspx?ID=" + memberid;// +"&READONLY=1";
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void ShowMemberList()
        {
            LB_TITLE.Text = BT_MEMBER.Text;
            TBL_MEMBER.Visible = true;
            TBL_ADDRESS.Visible = false;
            TBL_BANK.Visible = false;
            TBL_AGENT.Visible = false;
        }

        protected void BT_MEMBER_Click(object sender, EventArgs e)
        {
            ShowMemberList();
        }

        protected void BT_ADDRESS_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_MEMBER.Visible = false;
            TBL_ADDRESS.Visible = true;
            TBL_BANK.Visible = false;
            TBL_AGENT.Visible = false;
        }

        protected void BT_BANK_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_MEMBER.Visible = false;
            TBL_ADDRESS.Visible = false;
            TBL_BANK.Visible = true;
            TBL_AGENT.Visible = false;
        }

        protected void BT_AGENT_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            TBL_MEMBER.Visible = false;
            TBL_ADDRESS.Visible = false;
            TBL_BANK.Visible = false;
            TBL_AGENT.Visible = true;
        }
    }
}