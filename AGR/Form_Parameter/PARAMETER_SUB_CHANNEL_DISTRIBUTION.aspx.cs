using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace AGR.Form_Parameter
{
    public partial class PARAMETER_SUB_CHANNEL_DISTRIBUTION : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 2";
            conn.ExecuteQuery();
            for (int j = 0; j < conn.GetRowCount(); j++)
                DDL_SEGMENT.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from PR_CHANNEL_DISTRIBUTION order by 2";
            conn.ExecuteQuery();
            for (int j = 0; j < conn.GetRowCount(); j++)
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

            conn.QueryString = "select SUB_CODE, DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION order by 2";
            conn.ExecuteQuery();
            for (int j = 0; j < conn.GetRowCount(); j++)
                DDL_UPPER.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
        }

        protected void FillDGR()
        {
            string where = "";

            conn.QueryString = "select " +
                                "SUB_CODE, " +
                                "MARKET_SEGMENT, " +
                                "CD_CODE, " +
                                "DESCR, " +
                                "UPPER, " +
                                "SEQ " +
                                "from		PARAM_SUB_CHANNEL_DISTRIBUTION a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by " +
                                "a.MARKET_SEGMENT, " +
                                "a.SEQ";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtDESCR = (TextBox)DGR.Items[i].FindControl("TXT_DESCR");
                TextBox txtSEQ = (TextBox)DGR.Items[i].FindControl("TXT_SEQ");
                DropDownList ddlSEGMENT = (DropDownList)DGR.Items[i].FindControl("DDL_SEGMENT");
                DropDownList ddlCHANNEL = (DropDownList)DGR.Items[i].FindControl("DDL_CHANNEL");
                DropDownList ddlUPPER = (DropDownList)DGR.Items[i].FindControl("DDL_UPPER");

                txtDESCR.Text = DGR.Items[i].Cells[3].Text.Trim().Replace("&nbsp;", "");
                txtSEQ.Text = DGR.Items[i].Cells[5].Text.Trim().Replace("&nbsp;", "");

                for (int j = 0; j < DDL_SEGMENT.Items.Count; j++)
                    ddlSEGMENT.Items.Add(new ListItem(DDL_SEGMENT.Items[j].Text, DDL_SEGMENT.Items[j].Value));

                for (int j = 0; j < DDL_CHANNEL.Items.Count; j++)
                    ddlCHANNEL.Items.Add(new ListItem(DDL_CHANNEL.Items[j].Text, DDL_CHANNEL.Items[j].Value));

                for (int j = 0; j < DDL_UPPER.Items.Count; j++)
                    ddlUPPER.Items.Add(new ListItem(DDL_UPPER.Items[j].Text, DDL_UPPER.Items[j].Value));

                try
                {
                    ddlSEGMENT.SelectedValue = DGR.Items[i].Cells[1].Text;
                }
                catch { }

                try
                {
                    ddlCHANNEL.SelectedValue = DGR.Items[i].Cells[2].Text;
                }
                catch { }

                try
                {
                    ddlUPPER.SelectedValue = DGR.Items[i].Cells[4].Text;
                }
                catch { }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                TextBox txtDESCR = (TextBox)e.Item.FindControl("TXT_DESCR");
                TextBox txtSEQ = (TextBox)e.Item.FindControl("TXT_SEQ");
                DropDownList ddlSEGMENT = (DropDownList)e.Item.FindControl("DDL_SEGMENT");
                DropDownList ddlCHANNEL = (DropDownList)e.Item.FindControl("DDL_CHANNEL");
                DropDownList ddlUPPER = (DropDownList)e.Item.FindControl("DDL_UPPER");

                try
                {
                    conn.QueryString = "update PARAM_SUB_CHANNEL_DISTRIBUTION set " +
                                        "CD_CODE            = '" + ddlCHANNEL.SelectedValue + "'," +
                                        "DESCR              = '" + txtDESCR.Text.Trim() + "'," +
                                        "UPPER              = '" + ddlUPPER.SelectedValue + "'," +
                                        "MARKET_SEGMENT     = '" + ddlSEGMENT.SelectedValue + "'," +
                                        "SEQ                = '" + txtSEQ.Text.Trim() + "'," +
                                        "LASTCHANGEBY       = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE     = GETDATE() " +
                                        "where SUB_CODE = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteQuery();
                    FillDGR();
                }
                catch { }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_SUB_CHANNEL_DISTRIBUTION where SUB_CODE = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_CODE.Text.Trim() == "" || TXT_DESCR.Text.Trim() == "" || TXT_SEQ.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "insert into PARAM_SUB_CHANNEL_DISTRIBUTION select " +
                                    "SUB_CODE           = '" + TXT_CODE.Text.Trim() + "'," +
                                    "CD_CODE            = '" + DDL_CHANNEL.SelectedValue + "'," +
                                    "DESCR              = '" + TXT_DESCR.Text.Trim() + "'," +
                                    "UPPER              = '" + DDL_UPPER.SelectedValue + "'," +
                                    "MARKET_SEGMENT     = '" + DDL_SEGMENT.SelectedValue + "'," +
                                    "SEQ                = '" + TXT_SEQ.Text.Trim() + "'," +
                                    "CREATEBY           = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "CREATEDATE         = GETDATE()," +
                                    "LASTCHANGEBY       = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "LASTCHANGEDATE     = GETDATE()";
                conn.ExecuteQuery();
                FillDGR();
            }
            catch { }
        }
    }
}