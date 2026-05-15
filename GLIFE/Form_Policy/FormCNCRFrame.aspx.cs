using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace GLIFE.Form_Policy
{
    public partial class FormCNCRFrame : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) 
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                FillDGRSetupDate();
            }
        }

        protected void FillDGRSetupDate()
        {
            conn.QueryString = "SELECT a.POLICY_ID, " +
                                "       START_DATE = convert(varchar(20), a.START_DATE, 106), " +
                                "       a.SETUP_NAME " +
                                "FROM POLICY_CNCR_SETUP a " +
                                "WHERE a.POLICY_ID = " + LB_ID.Text + " " +
                                "ORDER BY a.START_DATE DESC";
                                conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SETUP_DATE.DataSource = dt;
            DGR_SETUP_DATE.DataBind();

            for (int i = 0; i < DGR_SETUP_DATE.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR_SETUP_DATE.Items[i].FindControl("LBT_DATE");
                Button btDEL = (Button)DGR_SETUP_DATE.Items[i].FindControl("BT_DEL");
                lbt.Text = DGR_SETUP_DATE.Items[i].Cells[1].Text;
                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "DELETE FROM POLICY_CNCR_SETUP WHERE POLICY_ID = " + LB_ID.Text + " AND START_DATE = '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();

                    conn.QueryString = "DELETE FROM POLICY_CNCR_SETUP_DETAIL WHERE POLICY_ID = " + LB_ID.Text + " AND START_DATE = '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();

                    FillDGRSetupDate();
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }

             if (e.CommandName == "Ketentuan")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'FormCNCRDetail.aspx?ID=" + LB_ID.Text + "&START_DATE="+ e.Item.Cells[1].Text + "&GROUP_NAME=Ketentuan Kepesertaan';</script>");
            }

            if (e.CommandName == "Pengecualian")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'FormCNCRDetail.aspx?ID=" + LB_ID.Text + "&START_DATE=" + e.Item.Cells[1].Text + "&GROUP_NAME=Pengecualian';</script>");
            }

            if (e.CommandName == "Klaim")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'FormCNCRDetail.aspx?ID=" + LB_ID.Text + "&START_DATE=" + e.Item.Cells[1].Text + "&GROUP_NAME=Klaim';</script>");
            }
        }

        protected void BT_ADD_Click(object sender, EventArgs e)
        {
            if (TXT_DATE.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "exec SP_POLICY_CNCR_SETUP_INSERT " +
                                    LB_ID.Text + "," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + TXT_SETUP_NAME.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRSetupDate();
            }
            catch (System.Exception ex)
            {
            }
        }

        protected void BT1_Click(object sender, EventArgs e)
        {
            
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'FormCNCRDetail.aspx?ID=" + LB_ID.Text + "';</script>");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'Pengecualian.aspx?ID=" + LB_ID.Text + "';</script>");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'Klaim.aspx?ID=" + LB_ID.Text + "';</script>");
        }
    }
}