using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_POS
{
    public partial class EndorsementPending : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                FillDGR();
            }
        }

        protected void Setup()
        {

        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.DESCR, " +
                                "PENDINGSTART = b.PENDINGBY + ' [' + convert(varchar(50),b.PENDINGDATE) + ']', " +
                                "PENDINGSTOP = b.CLOSINGBY + ' [' + convert(varchar(50),b.CLOSINGDATE) + ']' " +
                                "from PARAM_PENDING_TYPE a " +
                                "left join APPLICATION_MASTER_PENDING b on a.CODE = b.PENDING_CODE and b.REGNO = '" + LB_REGNO.Text + "' and b.SEQ = '" + LB_SEQ.Text + "' " +
                                "where " +
                                "a.PROCESS_CODE = 'POS'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbtSELECT = (LinkButton)DGR.Items[i].FindControl("LBT_DETAIL");
                Button btSTART = (Button)DGR.Items[i].FindControl("BT_START");
                Button btSTOP = (Button)DGR.Items[i].FindControl("BT_STOP");
                Label lbSTART = (Label)DGR.Items[i].FindControl("LB_START");
                Label lbSTOP = (Label)DGR.Items[i].FindControl("LB_STOP");

                btSTART.Attributes.Add("onclick", "if(!confirm('Are you sure to START PENDING ?')){return false;};");
                btSTOP.Attributes.Add("onclick", "if(!confirm('Are you sure to STOP PENDING ?')){return false;};");

                lbtSELECT.Text = DGR.Items[i].Cells[1].Text;
                if (DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "") == "" && DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "") == "")
                {
                    lbtSELECT.Enabled = false;
                    btSTART.Visible = true;
                }

                if (DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "" && DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "") == "")
                {
                    lbSTART.Text = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    btSTOP.Visible = true;
                }

                if (DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "" && DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    lbSTART.Text = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                    lbSTOP.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERROR.Text = "";

            if (e.CommandName == "Select")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.POSpendingbody.location.href = 'EndorsementPending" + e.Item.Cells[0].Text + ".aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "';</script>");
            }

            if (e.CommandName == "Start")
            {
                conn.QueryString = "exec SP_APPLICATION_MASTER_PENDING_INSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }

            if (e.CommandName == "Stop")
            {
                conn.QueryString = "exec SP_APPLICATION_MASTER_PENDING_STOP_VALIDATION " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() > 0)
                {
                    LB_ERROR.Text = "<BR>UNCOMPLETED ITEMS :<table>";
                    for (int i = 0; i < conn.GetRowCount(); i++)
                    {
                        LB_ERROR.Text = LB_ERROR.Text +
                                        "<tr><td style='width: 20px;'>" + conn.GetFieldValue(i, 0).ToString() + ".</td><td>" + conn.GetFieldValue(i, 1).ToString().ToUpper() + "</td></tr>";
                    }
                    LB_ERROR.Text = LB_ERROR.Text + "</table>";
                    return;
                }

                conn.QueryString = "update APPLICATION_MASTER_PENDING set " +
                                    "CLOSINGBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "CLOSINGDATE = GETDATE() " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and SEQ = '" + LB_SEQ.Text + "' " +
                                    "and PENDING_CODE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }
    }
}