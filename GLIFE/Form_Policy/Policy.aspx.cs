using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_Policy
{
    public partial class Policy : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                Setup();
                if (LB_ID.Text != "")
                {
                    LoadPolicy(LB_ID.Text);
                }
                else
                {
                    TR_ID.Visible = false;
                    TR_STAT.Visible = false;
                    TR_RENEWAL.Visible = false;
                    TR_BUTTONS.Visible = false;
                }
            }
        }

        protected void Setup()
        {
            DataBind();
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                LB_READONLY.Text = "&readonly=1";
                BT_SAVE.Visible = false;
                BT_STAT.Visible = false;
                BT_COMPANY_SEARCH.Visible = false;
                BT_AGENT_SEARCH.Visible = false;
                BT_POLISLAST_SEARCH.Visible = false;
                TXT_POLICYNO.Enabled = false;
                TXT_AGENTCODE.Enabled = false;
                DDL_COMPANY.Enabled = false;
                DDL_POLISLAST.Enabled = false;
                DDL_PRODUCTGROUP.Enabled = false;
                DDL_MODE.Enabled = false;
            }
            else
            {
                LB_READONLY.Text = "&readonly=";

                //djanuar 20200219 penambahan autonumber
                //conn.QueryString = "exec SP_POLICY_NUMBER";
                //conn.ExecuteQuery();

                //var data = conn.GetDataTable();
                //if (data != null){
                //var _maxNumber = data.Rows[0]["max_number"].ToString();
                //TXT_POLICYNO.Text = _maxNumber;
                //}
            }

            /*
            if (LB_ID.Text == "")
            {
                conn.QueryString = "select COMPANY_CODE, COMPANY_NAME = UPPER(COMPANY_NAME) from V_LINK_CB_COMPANY where LTRIM(isnull(COMPANY_NAME,'')) <> '' order by 2";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_COMPANY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
            else
            {
                conn.QueryString = "select COMPANY_CODE, COMPANY_NAME from V_POLICY where ID = " + LB_ID.Text;
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_COMPANY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
            */

            conn.QueryString = "select CODE,DESCR from V_LINK_UB_PRODUCT_GROUP order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCTGROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadPolicy(string ID)
        {
            LB_WARNING.Text = "";
            LB_WARNING.CssClass = "";

            conn.QueryString = "select " +
                                "a.ID, " +
                                "a.POLICY_NO, " +
                                "STAT = convert(int, isnull(a.STAT,0)), " +
                                "STATDESCR = (case when isnull(a.STAT,0)=1 then 'ACTIVE' else 'NOT ACTIVE' end), " +
                                "a.COMPANY_CODE, " +
                                "a.COMPANY_NAME, " +
                                "GROUP_CODE, " +
                                "a.AGENT_CODE, " +
                                "a.AGENT_NAME, " +
                                "h.POLICY_RENEWAL, " +
                                "h.POLICY_LAST, " +
                                "h1.POLICY_NO as POLICY_NO_LAST, " +
                                "h.SEQ, " +
                                "PERIOD_START = convert(varchar(20),i.PERIOD_START,103), " +
                                "PERIOD_END = convert(varchar(20),i.PERIOD_END,103), " +
                                "TERKENA_SANKSI = [GLIFE].[dbo].[UFN_CHECK_COMPANY_BLACKLISTED](a.COMPANY_NAME), " +
                                "RESIKO_TINGGI = [GLIFE].[dbo].[UFN_IS_RISKY_CUSTOMER](a.POLICY_NO) " +
                                "from V_POLICY a " +
                                "left join POLICY_RENEWAL h on a.ID = h.POLICY_ID " +
                                "left join POLICY h1 on h1.ID = h.POLICY_LAST " +
                                "left join POLICY_PERIOD i on a.ID = i.POLICY_ID " +
                                "where " +
                                "a.ID =  " + ID;
            conn.ExecuteQuery();

            if (conn.GetFieldValue("STAT").ToString() == "1")
            {
                TXT_POLICYNO.ReadOnly = true;
                TXT_AGENTCODE.ReadOnly = true;
                DDL_COMPANY.Enabled = false;
                DDL_POLISLAST.Enabled = false;
                DDL_PRODUCTGROUP.Enabled = false;
                DDL_MODE.Enabled = false;

                TR_SAVE.Visible = false;
                BT_COMPANY_SEARCH.Visible = false;
                BT_AGENT_SEARCH.Visible = false;
                BT_POLISLAST_SEARCH.Visible = false;
            }

            TXT_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            TXT_AGENTCODE.Text = conn.GetFieldValue("AGENT_CODE").ToString();
            LB_AGENT.Text = conn.GetFieldValue("AGENT_NAME").ToString();
            LB_STAT.Text = conn.GetFieldValue("STATDESCR").ToString();
            LB_STATCODE.Text = conn.GetFieldValue("STAT").ToString();
            LB_RENEWAL.Text = conn.GetFieldValue("SEQ").ToString();
            TXT_PROCDATE.Text = conn.GetFieldValue("PERIOD_START");
            TXT_PROCDATE2.Text = conn.GetFieldValue("PERIOD_END");

            if (LB_STATCODE.Text == "1")
            {
                BT_STAT.Attributes.Add("onclick", "if(!confirm('Are you sure to DISACTIVATE ?')){return false;};");
                BT_STAT.Text = "DISACTIVATE";
                BT_STAT.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                BT_STAT.Attributes.Add("onclick", "if(!confirm('Are you sure to ACTIVATE ?')){return false;};");
                BT_STAT.Text = "ACTIVATE";
                BT_STAT.ForeColor = System.Drawing.Color.Blue;
            }

            try
            {
                // DDL_COMPANY.SelectedValue = conn.GetFieldValue("COMPANY_CODE").ToString();
                DDL_COMPANY.Items.Clear();
                DDL_COMPANY.Items.Add(new ListItem(conn.GetFieldValue("COMPANY_NAME").ToString(), conn.GetFieldValue("COMPANY_CODE").ToString()));
            }
            catch { }

            try
            {
                if (conn.GetFieldValue("POLICY_RENEWAL").ToString() == "0")
                {
                    TR_INSERT.Visible = true;
                    TR_UPLOAD.Visible = false;
                }
                else
                {
                    TR_INSERT.Visible = false;
                    TR_UPLOAD.Visible = true;
                }
                DDL_MODE.SelectedValue = conn.GetFieldValue("POLICY_RENEWAL").ToString();
                //DDL_MODE.Items.Clear();
                //DDL_MODE.Items.Add(new ListItem(conn.GetFieldValue("POLICY_RENEWAL").ToString(), conn.GetFieldValue("POLICY_RENEWAL").ToString()));
            }
            catch { }

            try
            {
                //DDL_POLISLAST.SelectedValue = conn.GetFieldValue("POLICY_LAST").ToString();
                DDL_POLISLAST.Items.Clear();
                DDL_POLISLAST.Items.Add(new ListItem(conn.GetFieldValue("POLICY_NO_LAST").ToString(), conn.GetFieldValue("POLICY_LAST").ToString()));
            }
            catch { }

            try
            {
                DDL_PRODUCTGROUP.SelectedValue = conn.GetFieldValue("GROUP_CODE").ToString();
            }
            catch { }

            string status1 = conn.GetFieldValue("TERKENA_SANKSI").ToString();
            string status2 = conn.GetFieldValue("RESIKO_TINGGI").ToString();
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
            ShowTC();
            
        }

        protected void ShowTC()
        {
            LB_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'PolicyTCFrame.aspx?ID=" + LB_ID.Text + LB_READONLY.Text + "';</script>");
        }
        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowTC();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'PolicyLoading.aspx?ID=" + LB_ID.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'PolicyReinsuranceFrame.aspx?ID=" + LB_ID.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_02", LB_ID.Text, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = '" + URL + "';</script>");
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'PolicyChannelFrame.aspx?ID=" + LB_ID.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'PolicyOtherSetup.aspx?ID=" + LB_ID.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT7_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'PolicyInvoice.aspx?ID=" + LB_ID.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT8_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'IkhtisarFrame.aspx?ID=" + TXT_POLICYNO.Text + LB_READONLY.Text + "';</script>");
            
        }
 
        protected void BT_COMPANY_SEARCH_Click(object sender, EventArgs e)
        {

            //ClientScript.RegisterStartupScript(this.GetType(), "focus", "window.open('../Form_Tools/InquiryScreenPopup.aspx?CODE=0006&parent=0&targetcode=DDL_COMPANY&targetdescr=LB_COMPANY_DESCR','COMPANY','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');", true);


            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            DGR_SEARCH.Visible = false;
            TR_COMPANY_SEARCH.Visible = true;
            TR_POLISLAST_SEARCH.Visible = false;
            TR_AGENT_SEARCH.Visible = false;
            LB_SEARCH_TITLE.Text = "COMPANY SEARCH";
            LB_RECORDS.Text = "";

            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COMPANY_LOB";
            conn.ExecuteQuery();
            DDL_COMPANYLOB_SEARCH.Items.Clear();
            DDL_COMPANYLOB_SEARCH.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_COMPANYLOB_SEARCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_POLISLAST_SEARCH_Click(object sender, EventArgs e)
        {

            //ClientScript.RegisterStartupScript(this.GetType(), "focus", "window.open('../Form_Tools/InquiryScreenPopup.aspx?CODE=0006&parent=0&targetcode=DDL_COMPANY&targetdescr=LB_COMPANY_DESCR','COMPANY','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');", true);


            //ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('BT_POLISLAST_SEARCH_Click').style.display = 'block';", true);
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            DGR_SEARCH.Visible = false;
            TR_COMPANY_SEARCH.Visible = false;
            TR_POLISLAST_SEARCH.Visible = true;
            TR_AGENT_SEARCH.Visible = false;
            LB_SEARCH_TITLE.Text = "POLICY SEARCH";
            LB_RECORDS.Text = "";

            conn.QueryString = "select distinct ID, COMPANY_NAME from V_POLICY";
            conn.ExecuteQuery();
            DDL_POLISLASTLOB_SEARCH.Items.Clear();
            DDL_POLISLASTLOB_SEARCH.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_POLISLASTLOB_SEARCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 1).ToString()));
        }

        protected void BT_AGENT_SEARCH_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            DGR_SEARCH.Visible = false;
            TR_COMPANY_SEARCH.Visible = false;
            TR_POLISLAST_SEARCH.Visible = false;
            TR_AGENT_SEARCH.Visible = true;
            LB_SEARCH_TITLE.Text = "AGENT SEARCH";
            LB_RECORDS.Text = "";

            conn.QueryString = "select SUB_CODE,DESCR from V_LINK_MARKETING_PARAM_SUB_CHANNEL_DISTRIBUTION";
            conn.ExecuteQuery();
            DDL_AGENTCHANNEL_SEARCH.Items.Clear();
            DDL_AGENTCHANNEL_SEARCH.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_AGENTCHANNEL_SEARCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TR_COMPANY_SEARCH.Visible)
            {
                if (TXT_COMPANY_SEARCH.Text.Trim() != "")
                    where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY_SEARCH.Text.Trim() + "%' ";

                if (DDL_COMPANYLOB_SEARCH.SelectedValue != "")
                    where = where + " and a.COMPANY_LOB = '" + DDL_COMPANYLOB_SEARCH.Text.Trim() + "' ";

                conn.QueryString = "select " +
                                    "CODE = a.COMPANY_CODE, " +
                                    "NAME = a.COMPANY_NAME, " +
                                    "DESCR = b.DESCR " +
                                    "from V_LINK_CB_COMPANY a " +
                                    "inner join V_LINK_CB_PR_COMPANY_LOB b on a.COMPANY_LOB = b.CODE " +
                                    "where " +
                                    "isnull(a.COMPANY_NAME,'') <> '' " + where + " " +
                                    "order by 2";
                conn.ExecuteQuery();
                LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_SEARCH.DataSource = dt;
                DGR_SEARCH.DataBind();

                for (int i = 0; i < DGR_SEARCH.Items.Count; i++)
                {
                    LinkButton lbt = (LinkButton)DGR_SEARCH.Items[i].FindControl("LBT_CODE");
                    lbt.Text = DGR_SEARCH.Items[i].Cells[1].Text;
                }

                DGR_SEARCH.Visible = true;
            }

            if (TR_POLISLAST_SEARCH.Visible)
            {
                if (TXT_POLISLAST_SEARCH.Text.Trim() != "")
                    where = where + " and a.POLICY_NO like '%" + TXT_POLISLAST_SEARCH.Text.Trim() + "%' ";

                if (DDL_POLISLASTLOB_SEARCH.SelectedValue != "")
                    where = where + " and a.COMPANY_NAME = '" + DDL_POLISLASTLOB_SEARCH.Text.Trim() + "' ";

                conn.QueryString = "select distinct " +
                                    //"CODE = a.POLICY_NO, " +
                                    //"NAME = a.COMPANY_NAME, " +
                                    //"DESCR = a.TC_DESCR " +
                                    "CODE = a.ID, " +
                                    "NAME = a.POLICY_NO, " +
                                    "DESCR = a.COMPANY_NAME " +
                                    "from V_POLICY a " +
                                    "where " +
                                    "isnull(a.COMPANY_NAME,'') <> '' " + where + " " +
                                    "order by 2";
                conn.ExecuteQuery();
                LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_SEARCH.DataSource = dt;
                DGR_SEARCH.DataBind();

                for (int i = 0; i < DGR_SEARCH.Items.Count; i++)
                {
                    LinkButton lbt = (LinkButton)DGR_SEARCH.Items[i].FindControl("LBT_CODE");
                    lbt.Text = DGR_SEARCH.Items[i].Cells[1].Text;
                }

                DGR_SEARCH.Visible = true;
            }

            if (TR_AGENT_SEARCH.Visible)
            {
                if (TXT_AGENTNAME_SEARCH.Text.Trim() != "")
                    where = where + " and LTRIM(RTRIM(replace(isnull(a.FRONT_NAME,'') + ' ' + isnull(a.MID_NAME,'') + ' ' + isnull(a.LAST_NAME,''), '  ', ' '))) like '%" + TXT_AGENTNAME_SEARCH.Text.Trim() + "%' ";

                if (DDL_AGENTCHANNEL_SEARCH.SelectedValue != "")
                    where = where + " and a.SUBCD = '" + DDL_AGENTCHANNEL_SEARCH.Text.Trim() + "' ";

                conn.QueryString = "select " +
                                    "CODE = a.CODE, " +
                                    "NAME = LTRIM(RTRIM(replace(isnull(a.FRONT_NAME,'') + ' ' + isnull(a.MID_NAME,'') + ' ' + isnull(a.LAST_NAME,''), '  ', ' '))), " +
                                    "DESCR = b.DESCR " +
                                    "from V_LINK_MARKETING_M_AGENTS a " +
                                    "inner join V_LINK_MARKETING_PARAM_SUB_CHANNEL_DISTRIBUTION b on a.SUBCD = b.SUB_CODE " +
                                    "where " +
                                    "1=1 " + where + " " +
                                    "order by 2";
                conn.ExecuteQuery();
                LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_SEARCH.DataSource = dt;
                DGR_SEARCH.DataBind();

                for (int i = 0; i < DGR_SEARCH.Items.Count; i++)
                {
                    LinkButton lbt = (LinkButton)DGR_SEARCH.Items[i].FindControl("LBT_CODE");
                    lbt.Text = DGR_SEARCH.Items[i].Cells[1].Text;
                }

                DGR_SEARCH.Visible = true;
            }

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void DGR_SEARCH_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if (TR_COMPANY_SEARCH.Visible)
                {
                    //DDL_COMPANY.SelectedValue = e.Item.Cells[1].Text;
                    DDL_COMPANY.Items.Clear();
                    DDL_COMPANY.Items.Add(new ListItem(e.Item.Cells[2].Text, e.Item.Cells[1].Text));
                }

                if (TR_POLISLAST_SEARCH.Visible)
                {
                    DDL_POLISLAST.Items.Clear();
                    DDL_POLISLAST.Items.Add(new ListItem(e.Item.Cells[2].Text, e.Item.Cells[1].Text));
                }

                if (TR_AGENT_SEARCH.Visible)
                {
                    TXT_AGENTCODE.Text = e.Item.Cells[1].Text;
                    LB_AGENT.Text = e.Item.Cells[2].Text;
                }
            }
        }

        protected void BT_STAT_Click(object sender, EventArgs e)
        {
            string stat = "1";
            if (LB_STATCODE.Text == "1")
                stat = "0";

            conn.QueryString = "update POLICY set STAT = " + stat + " where ID = " + LB_ID.Text;
            conn.ExecuteNonQuery();
            Response.Redirect("Policy.aspx?ID=" + LB_ID.Text +"&readonly=");
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {

            if (DDL_PRODUCTGROUP.SelectedValue == "GTL")
            {
                if (TXT_POLICYNO.Text.Trim() == "" || TXT_AGENTCODE.Text.Trim() == "" || DDL_COMPANY.SelectedValue == "" || DDL_PRODUCTGROUP.SelectedValue == "" || TXT_PROCDATE.Text.Trim() == "" || TXT_PROCDATE2.Text.Trim() == "")
                    return;
            }
            else
            {
                if (TXT_POLICYNO.Text.Trim() == "" || TXT_AGENTCODE.Text.Trim() == "" || DDL_COMPANY.SelectedValue == "" || DDL_PRODUCTGROUP.SelectedValue == "")
                    return;
            }
            

            string ID = "null";
            string STAT = "0";
            if (LB_ID.Text != "")
            {
                ID = "'" + LB_ID.Text + "'";
                STAT = LB_STATCODE.Text;
            }


            conn.QueryString = "exec SP_POLICY_UPSERT " +
                                ID + "," +
                                "'" + TXT_POLICYNO.Text + "'," +
                                "'" + DDL_COMPANY.SelectedValue + "'," +
                                "'" + DDL_PRODUCTGROUP.SelectedValue + "'," +
                                "'" + TXT_AGENTCODE.Text + "'," +
                                "'" + LB_STATCODE.Text + "'," +
                                "'" + DDL_MODE.SelectedValue + "'," +
                                "'" + DDL_POLISLAST.SelectedValue + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_PROCDATE.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_PROCDATE2.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            if (LB_ID.Text != "")
                Response.Redirect("Policy.aspx?ID=" + conn.GetFieldValue("ID").ToString() + "&readonly=");
            else
                Response.Redirect("PolicyFrame.aspx?ID=" + conn.GetFieldValue("ID").ToString());

        }

        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_MODE.SelectedValue == "0")
            {
                TR_INSERT.Visible = true;
                TR_UPLOAD.Visible = false;
            }
            else
            {
                TR_INSERT.Visible = false;
                TR_UPLOAD.Visible = true;
            }
        }

        protected void btnCN_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'FormCNCRFrame.aspx?ID=" + LB_ID.Text + LB_READONLY.Text + "';</script>");
        }




    }
}