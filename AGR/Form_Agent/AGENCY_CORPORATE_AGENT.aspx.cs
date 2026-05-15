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
    public partial class AGENCY_CORPORATE_AGENT : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["ID"];
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PR_CHANNEL_DISTRIBUTION";
            conn.ExecuteQuery();
            DDL_CHANNEL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select SEQ, DESCR from PARAM_TRACK where TIPE_CODE = 'AGN' and SEQ > 1 order by SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
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

            if (DDL_STAT.SelectedValue != "")
                where = where + " and a.ACTIVE = " + DDL_STAT.SelectedValue + " ";

            if (DDL_CHANNEL.SelectedValue != "")
                where = where + " and a.CD = " + DDL_CHANNEL.SelectedValue + " ";

            if (DDL_STATUS.SelectedValue != "")
                where = where + " and a.TRACK = " + DDL_STATUS.SelectedValue + " ";

            conn.QueryString = "select " +
                                "CODE, " +
                                "TRACK, " +
                                "SUBCD_DESCR, " +
                                "FULLNAME, " +
                                "DOB = convert(varchar(20),a.DOB,106), " +
                                "UPLINER_NAME, " +
                                "BRANCH_DESCR, " +
                                "TRACK_DESCR, " +
                                "ACTIVE " +
                                "from V_M_AGENTS a " +
                                "where " +
                                "AGENCY_CODE = '" +LB_CODE.Text+ "' " + where +
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
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                lb.Text = DGR.Items[i].Cells[1].Text;
                if (DGR.Items[i].Cells[2].Text == "1")
                    cb.Checked = true;

                switch (DGR.Items[i].Cells[3].Text)
                {
                    case "2": DGR.Items[i].Cells[4].ForeColor = System.Drawing.Color.Green; break;
                    case "4": DGR.Items[i].Cells[4].ForeColor = System.Drawing.Color.Orange; cb.Visible = false; break;
                    case "3": DGR.Items[i].Cells[4].ForeColor = System.Drawing.Color.Red; cb.Visible = false; break;
                }
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
            if (e.CommandName == "Detail")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.location.href = 'Agent_Frame.aspx?AGENTCODE=" + e.Item.Cells[1].Text + "';</script>");
            }
        }
        
        protected void DB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                if ((CheckBox)sender == cb)
                {
                    string stat = "0";
                    if (cb.Checked)
                        stat = "1";

                    try
                    {
                        conn.QueryString = "update M_AGENTS set ACTIVE = " + stat + " where CODE = '" + DGR.Items[i].Cells[1].Text + "'";
                        conn.ExecuteNonQuery();
                        FillDGR();
                        return;
                    }
                    catch { }
                }
            }
        }

        protected void DDL_STATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}