using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;


namespace AGR
{
    public partial class AGENT_APPROVAL : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT";
            conn.ExecuteQuery();
            DDL_CHANNEL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_CODE.Text.Trim() != "")
                where = where + " and a.CODE like '%" + TXT_CODE.Text.Trim() + "%' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_UPLINER.Text.Trim() != "")
                where = where + " and a.UPLINER_NAME like '%" + TXT_UPLINER.Text.Trim() + "%' ";

            if (TXT_CHANNEL.Text.Trim() != "")
                where = where + " and a.SUBCD_DESCR like '%" + TXT_CHANNEL.Text.Trim() + "%' ";

            if (DDL_CHANNEL.SelectedValue != "")
                where = where + " and a.MARKET_SEGMENT = " + DDL_CHANNEL.SelectedValue + " ";

            conn.QueryString = "select " +
                                "CODE, " +
                                "SUBCD_DESCR, " +
                                "FULLNAME, " +
                                "DOB = convert(varchar(20),a.DOB,106), " +
                                "UPLINER_NAME, " +
                                "BRANCH_DESCR, " +
                                "LISTING_TYPE = [MARKETING].[dbo].[UFN_GET_AGENT_LISTING_TYPE](a.CODE) " +
                                "from [MARKETING].[dbo].[V_M_AGENTS] a " +
                                "where " +
                                "TRACK = 1 " + where +
                                "order by " +
                                "a.FULLNAME";
            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                lb.Text = DGR.Items[i].Cells[1].Text;
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_VALIDATION.Text = "";

            if (e.CommandName == "Detail")
            {
                Response.Redirect("Agent_Frame.aspx?AGENTCODE=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Approve")
            {
                try
                {
                    for (int i = 0; i < DGR.Items.Count; i++)
                    {
                        CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                        if (cb.Checked)
                        {
                            conn.QueryString = "exec SP_M_AGENTS_APPROVAL_VALIDATION " +
                                                "'" + DGR.Items[i].Cells[1].Text + "'," +
                                                "''," +
                                                "2";
                            conn.ExecuteQuery();

                            LB_VALIDATION.Text = LB_VALIDATION.Text + conn.GetFieldValue("RESULT").ToString() + "<BR>";

                            if (conn.GetFieldValue("RESULT").ToString().Trim().Length == 0)
                            {
                                conn.QueryString = "exec SP_GENERATE_RANDOM";
                                conn.ExecuteQuery();
                                string pwd = conn.GetFieldValue("PWD").ToString();

                                conn.QueryString = "exec SP_M_AGENTS_APPROVAL " +
                                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                                    "2," +
                                                    "'" + Crypto.EncryptStringAES(pwd) + "'," +
                                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                                conn.ExecuteQuery();
                                string code = conn.GetFieldValue("CODE").ToString();

                                conn.QueryString = "exec SP_M_AGENTS_APPROVAL_EMAIL " +
                                                    "'" + code + "'," +
                                                    //"'" + pwd + "'," +
                                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                                conn.ExecuteQuery();
                            }
                        }
                    }

                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
                catch { }

                if (LB_VALIDATION.Text.Trim().Length > 0)
                {
                    DV_APPROVAL.Visible = false;
                    DV_VALIDATION.Visible = true;
                }
            }

            if (e.CommandName == "Reject")
            {
                try
                {
                    for (int i = 0; i < DGR.Items.Count; i++)
                    {
                        CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                        TextBox txtREJECT = (TextBox)DGR.Items[i].FindControl("TXT_REJECT");

                        if (cb.Checked && txtREJECT.Text.Trim() != "")
                        {
                            conn.QueryString = "exec SP_M_AGENTS_APPROVAL_VALIDATION " +
                                                "'" + DGR.Items[i].Cells[1].Text + "'," +
                                                "'" + txtREJECT.Text.Trim().Replace("'", "") + "'," +
                                                "3";
                            conn.ExecuteQuery();

                            LB_VALIDATION.Text = LB_VALIDATION.Text + conn.GetFieldValue("RESULT").ToString() + "<BR>";

                            if (conn.GetFieldValue("RESULT").ToString().Trim().Length == 0)
                            {
                                conn.QueryString = "exec SP_M_AGENTS_APPROVAL " +
                                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                                    "3," +
                                                    "'" + txtREJECT.Text.Trim() + "'," +
                                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                                conn.ExecuteNonQuery();

                                conn.QueryString = "exec SP_M_AGENTS_REJECTION_EMAIL " +
                                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                                conn.ExecuteQuery();
                            }
                        }
                    }

                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
                catch { }

                if (LB_VALIDATION.Text.Trim().Length > 0)
                {
                    DV_APPROVAL.Visible = false;
                    DV_VALIDATION.Visible = true;
                }
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from M_AGENTS where CODE = '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }



        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DGR_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void LB_BACK_Click(object sender, EventArgs e)
        {
            LB_VALIDATION.Text = "";
            DV_APPROVAL.Visible = true;
            DV_VALIDATION.Visible = false;
        }

    }
}