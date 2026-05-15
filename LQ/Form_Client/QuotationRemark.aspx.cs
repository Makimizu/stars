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
    public partial class QuotationRemark : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LoadRemark();
            }
        }

        protected void LoadRemark()
        {
            conn.QueryString = "select " +
                                "a.SEQ, " +
                                "REMARK = '<B>' + UPPER(a.CREATEBY) + ' - [' + convert(varchar(50),a.CREATEDATE) + ']</B><BR>' + a.REMARK " +
                                "from APPLICATION_REMARK a " +
                                "where a.REGNO = '" + LB_REGNO.Text + "' order by a.CREATEDATE desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REMARK.DataSource = dt;
            DGR_REMARK.DataBind();

        }

        protected void BT_REMARK_SAVE_Click(object sender, EventArgs e)
        {
            //try
            //{
            if (TXT_REMARK.Text.Trim() == "")
                return;

            conn.QueryString = "exec SP_APPLICATION_REMARK_INSERT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + TXT_REMARK.Text.Trim().Replace("'", "`") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            LoadRemark();
            TXT_REMARK.Text = "";
            //}
            //catch { }
        }

        protected void DGR_REMARK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from APPLICATION_REMARK where " +
                                        "REGNO = '" + LB_REGNO.Text + "' " +
                                        "and SEQ=" + e.Item.Cells[0].Text + " " +
                                        "and CREATEBY='" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    LoadRemark();
                }
                catch { }
            }
        }
    }
}