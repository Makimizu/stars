using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Threading.Tasks;

namespace GLIFE.Form_App
{
    public partial class ApplicationHeaderSpecialRate : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected int track;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");

                Setup();
                track = GetTrack();
                ShowMainInfo();
                ShowTrack();
                //ShowInfoSpecialRate();
            }
        }

        protected int GetTrack()
        {
            conn.QueryString = "select SEQ = MAX(SEQ) from TRACK_DATA where TIPE_CODE='UW' and OWNER = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            return int.Parse(conn.GetFieldValue("SEQ").ToString());
        }

        protected void ShowTrack()
        {
            conn.QueryString = "select " +
                                "c.SEQ, " +
                                "DESCR = replace(c.DESCR,'ED',''), " +
                                "COMMENT = convert(int,c.IsCOMMENT) " +
                                "from PARAM_TRACK_NEXT a " +
                                "inner join PARAM_TRACK c on a.NEXT_SEQ = c.SEQ and a.TIPE_CODE = c.TIPE_CODE " +
                                "where " +
                                "a.SEQ = " + track.ToString() + " and a.TIPE_CODE = 'UW' " +
                                "order by convert(int,c.IsCOMMENT), c.SEQ";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                TR_PENDING.Visible = false;
                ShowStat();
                return;
            }

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TRACK.DataSource = dt;
            DGR_TRACK.DataBind();

            for (int i = 0; i < DGR_TRACK.Items.Count; i++)
            {
                Button btTRACK = (Button)DGR_TRACK.Items[i].FindControl("BT_TRACK");
                TextBox txtCOMMENT = (TextBox)DGR_TRACK.Items[i].FindControl("TXT_COMMENT");

                btTRACK.Text = DGR_TRACK.Items[i].Cells[1].Text;
                btTRACK.Attributes.Add("onclick", "if(!confirm('Are you sure to " + DGR_TRACK.Items[i].Cells[1].Text + " ?')){return false;};");

                if (DGR_TRACK.Items[i].Cells[2].Text == "1")
                {
                    txtCOMMENT.Visible = true;
                }
            }
        }

        protected void ShowStat()
        {
            //LB_NAME.Enabled = false;
            //DDL_BRANCH.Enabled = false;
            TBL_STAT.Visible = true;

            conn.QueryString = "select " +
                                "STAT       = b.DESCR, " +
                                "COMMENT    = a.COMMENT, " +
                                "COLOR      = (case when a.SEQ = 4 then 'Blue' else 'Red' end) " +
                                "from TRACK_DATA a " +
                                "inner join PARAM_TRACK b on b.TIPE_CODE = a.TIPE_CODE and a.SEQ = b.SEQ " +
                                "where " +
                                "a.TIPE_CODE='UW' " +
                                "and a.SEQ = " + track.ToString() + " " +
                                "and a.OWNER = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_STAT.Text = conn.GetFieldValue("STAT").ToString();
            LB_STATCOMMENT.Text = conn.GetFieldValue("COMMENT").ToString();

            LB_STAT.ForeColor = System.Drawing.Color.FromName(conn.GetFieldValue("COLOR").ToString());
            LB_STATCOMMENT.ForeColor = System.Drawing.Color.FromName(conn.GetFieldValue("COLOR").ToString());

            DDL_AGENT.Enabled = false;
            DDL_BRANCH.Enabled = false;
        }

        protected void Setup()
        {
            LB_REGNO.Text = Request.QueryString["ID"].ToString();
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                LB_READONLY.Text = "&readonly=1";
                DGR_TRACK.Visible = false;
                DDL_BRANCH.Enabled = false;
                DDL_AGENT.Enabled = false;
            }

            FillDDLBranch();
            FillDDLAgent();

            conn.QueryString = "select " +
                                "REGNO				= a.REGNO,  " +
                                "MEMBERID           = d.ID, " +
                                "FULLNAME			= d.FULLNAME,  " +
                                "DOB				= convert(varchar(20),d.DOB,106),  " +
                                "SEX				= (case when SEX='M' then 'MALE' else 'FEMALE' end),  " +
                                "POLICY_NO			= b.POLICY_NO,  " +
                                "COMPANY_NAME		= c.COMPANY_NAME,  " +
                                "TC_ID				= a.TC_ID,  " +
                                "TC_DESCR			= LTRIM(replace(replace(e.DESCR, a.TC_ID, ''), '-', '')),  " +
                                "PRODUCT_GROUP      = b.PRODUCT_GROUP, " +
                                "BRANCH_CODE		= a.BRANCH_CODE,  " +
                                "AGENT_NAME			= LTRIM(RTRIM(replace(isnull(i.FRONT_NAME,'') + ' ' + isnull(i.MID_NAME,'') + ' ' + isnull(i.LAST_NAME,''),'  ',' '))) , " +
                                "USERBY             = a.USERBY, " +
                                "SUBCD_DESCR		= SUBCD_DESCR " +
                                "from APPLICATION_MASTER a " +
                                "inner join POLICY b on a.POLICY_ID = b.ID " +
                                "inner join V_LINK_CB_COMPANY c on b.COMPANY_CODE = c.COMPANY_CODE " +
                                "inner join V_LINK_CB_MEMBER_MASTER d on a.MEMBER_ID = d.ID " +
                                "inner join V_LINK_UB_TC_MASTER e on a.TC_ID = e.CODE " +
                                "left join V_LINK_MARKETING_M_AGENTS i on a.USERBY = i.CODE " +
                                "where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();


            LB_MEMBERID.Text = conn.GetFieldValue("MEMBERID").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_GENDER.Text = conn.GetFieldValue("SEX").ToString();
            LB_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("TC_DESCR").ToString();
            LB_TC_ID.Text = conn.GetFieldValue("TC_ID").ToString();
            LB_TC_GROUP.Text = conn.GetFieldValue("PRODUCT_GROUP").ToString();
            LB_CHANNEL.Text = conn.GetFieldValue("SUBCD_DESCR").ToString();

            try
            {
                DDL_BRANCH.SelectedValue = conn.GetFieldValue("BRANCH_CODE").ToString();
            }
            catch { }

            try
            {
                DDL_AGENT.SelectedValue = conn.GetFieldValue("USERBY").ToString();
            }
            catch { }

            if (LB_TC_GROUP.Text == "SP")
            {
                BT6.Text = "SAVING";
            }
        }

        protected void FillDDLBranch()
        {
            conn.QueryString = "select " +
                                "c.BRANCH_CODE, " +
                                "c.NAMA_CABANG " +
                                "from APPLICATION_MASTER a " +
                                "inner join V_POLICY b on a.POLICY_ID = b.ID " +
                                "inner join V_LINK_CB_BRANCH c on b.COMPANY_CODE = c.COMPANY_CODE " +
                                "where a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            DDL_BRANCH.Items.Clear();
            DDL_BRANCH.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BRANCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }


        protected void FillDDLAgent()
        {
            conn.QueryString = "select " +
                                "c.CODE, " +
                                "FULLNAME = UPPER(LTRIM(isnull(c.FRONT_NAME,'')) + RTRIM(' ' + isnull(c.MID_NAME,'')) + RTRIM(' ' + isnull(c.LAST_NAME,''))) " +
                                "from APPLICATION_MASTER a " +
                                "inner join POLICY_CHANNEL_DISTRIBUTION b on a.POLICY_ID = b.ID " +
                                "inner join V_LINK_MARKETING_M_AGENTS c on b.SUBCD = c.SUBCD " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_AGENT.Items.Clear();
            DDL_AGENT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_AGENT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void ShowMainInfo()
        {
            LB_TITLE.Text = BT1.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationMainInfoFrame.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            ShowMainInfo();
        }

        protected void BT2_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationBenefitFrame.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT3_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationDocumentFrame.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT4_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationRemarkFrame.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT5_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_Parameter/TC.aspx?CODE=" + LB_TC_ID.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT6_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;

            if (LB_TC_GROUP.Text != "SP")
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationPremium.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
            else
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = '../Form_Saving/ApplicationSavingFrame.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
        }

        protected void LoadPendingItems()
        {
            LB_TITLE.Text = "UNCOMPLETED ITEMS";
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationUncompletedItems.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BT_PENDING_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.appbody.location.href = 'ApplicationPendingFrame.aspx?ID=" + LB_REGNO.Text + LB_READONLY.Text + "';</script>");
        }

        protected void BranchValidation()
        {
            conn.QueryString = "select BRANCH_CODE = isnull(BRANCH_CODE,'') from APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetFieldValue("BRANCH_CODE").ToString() == "" && DDL_BRANCH.SelectedValue != "")
            {
                conn.QueryString = "update APPLICATION_MASTER set " +
                                    "BRANCH_CODE = '" + DDL_BRANCH.SelectedValue + "' " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteNonQuery();
            }
        }

        protected void DGR_TRACK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Next")
            {
                TextBox txtCOMMENT = (TextBox)e.Item.FindControl("TXT_COMMENT");
                if (txtCOMMENT.Visible && txtCOMMENT.Text.Trim() == "")
                {
                    txtCOMMENT.Focus();
                    return;
                }

                conn.QueryString = "select " +
                                    "UW_CODE = (case when UW_CODE not in ('AC','NM') then 'MED' else UW_CODE end) " +
                                    "from APPLICATION_MAIN_INFO " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();

                string
                mode = conn.GetFieldValue("UW_CODE").ToString();

                if (e.Item.Cells[0].Text == "2" || e.Item.Cells[0].Text == "4") /**/
                {
                    conn.QueryString = "exec SP_APPLICATION_SPECIALRATE_VALIDATION '" + LB_REGNO.Text + "', '" + e.Item.Cells[0].Text + "', '" + txtCOMMENT.Text.Trim() + "', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }

                if (e.Item.Cells[0].Text == "4")
                {
                    BranchValidation();

                    conn.QueryString = "exec SP_APPLICATION_MASTER_APV_VALIDATION '" + LB_REGNO.Text + "'";
                    conn.ExecuteQuery();

                    if (conn.GetRowCount() > 0)
                    {
                        LoadPendingItems();
                        return;
                    }
                }

                try
                {
                    string URL = "AppNewListSpecialRate.aspx?mode=&track=2";
                    //string URL = "../Standard/default.html";
                    Task.Run(() => GoNextTrack(e.Item.Cells[0].Text, txtCOMMENT.Text.Trim()));
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = '" + URL + "';</script>");
                }
                catch { }
            }
        }


        protected void GoNextTrack(string nexttrack, string comment)
        {
            conn.QueryString = "exec SP_APPLICATION_MASTER_NEXT_TRACK " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + nexttrack + "'," +
                                        "'" + comment + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
        }

        protected void DDL_BRANCH_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.QueryString = "update APPLICATION_MASTER set " +
                                "BRANCH_CODE = '" + DDL_BRANCH.SelectedValue + "' " +
                                "where " +
                                "REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();
        }

        protected void DDL_AGENT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DDL_AGENT.SelectedValue == "")
                return;

            conn.QueryString = "update APPLICATION_MASTER set " +
                                "USERBY = '" + DDL_AGENT.SelectedValue + "' " +
                                "where " +
                                "REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();


            conn.QueryString = "select " +
                                "SUBCD_DESCR		= SUBCD_DESCR " +
                                "from APPLICATION_MASTER a " +
                                "left join V_LINK_MARKETING_M_AGENTS i on a.USERBY = i.CODE " +
                                "where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            LB_CHANNEL.Text = conn.GetFieldValue("SUBCD_DESCR").ToString();
        }


        /*
        protected void ShowInfoSpecialRate()
        {
            conn.QueryString = "SELECT INFO FROM [GLIFE_PROPOSAL].[dbo].[APPLICATION_SPECIALRATE] WHERE REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_INFOSPECIALRATE.Text = conn.GetFieldValue("INFO").ToString();
        }*/

    }
}
