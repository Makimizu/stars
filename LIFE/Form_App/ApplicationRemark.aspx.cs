using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_App
{
    public partial class ApplicationRemark : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected bool bDone;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                bDone = TrackDone();
                LoadRemark();
            }
        }

        protected bool TrackDone()
        {
            bool result = true;
            conn.QueryString = "select TRACK = dbo.UFN_GET_APP_TRACK('" + LB_REGNO.Text + "', 'UW', '')";
            conn.ExecuteQuery();

            if (float.Parse(conn.GetFieldValue("TRACK").ToString()) < 3)
                result = false;

            return result;
        }

        protected void LoadRemark()
        {
            conn.QueryString = "select " +
                                "a.SEQ, " +
                                "REMARK = '<B>' + UPPER(a.CREATEBY) + ' - [' + convert(varchar(50),a.CREATEDATE) + ']</B><BR><BR>' + a.REMARK " +
                                "from APPLICATION_REMARK a " +
                                "where a.REGNO = '" + LB_REGNO.Text + "' " +
                                "and SEQ > 0 order by a.CREATEDATE desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REMARK.DataSource = dt;
            DGR_REMARK.DataBind();

            for (int i = 0; i < DGR_REMARK.Items.Count; i++)
            {
                //TextBox txtREMARK = (TextBox)DGR_REMARK.Items[i].FindControl("TXT_REMARK");
                //txtREMARK.Text = DGR_REMARK.Items[i].Cells[1].Text.Trim().Replace("&nbsp;", "");
            }

            if (bDone)
            {
                TR_INSERT.Visible = false;
                DGR_REMARK.Columns[DGR_REMARK.Columns.Count - 1].Visible = false;
            }
        }

        protected void BT_REMARK_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                if (TXT_REMARK.Text.Trim() == "")
                    return;

                conn.QueryString = "exec SP_APPLICATION_REMARK_INSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + TXT_REMARK.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                LoadRemark();
            }
            catch { }
        }

        protected void DGR_REMARK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from APPLICATION_REMARK where REGNO = '" + LB_REGNO.Text + "' and SEQ=" + e.Item.Cells[0].Text;
                    conn.ExecuteNonQuery();
                    LoadRemark();
                }
                catch { }
            }
        }
    }
}