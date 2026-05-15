using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Tools
{
    public partial class Remark : System.Web.UI.Page
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
            LB_TIPE.Text = Request.QueryString["tipe"];
            LB_OWNER.Text = Request.QueryString["owner"];

            conn.QueryString = "select SUB_TIPE,DESCR from PARAM_TIPE_SUB_REMARK where TIPE='" + LB_TIPE.Text + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.ID, " +
                                "TIPE_DESCR=b.DESCR, " +
                                "REMARK='<B>['+convert(varchar(50),a.CREATEDATE)+']</B><BR>'+replace(a.REMARK_TEXT,char(13),'<BR>') " +
                                "from REMARK a " +
                                "inner join PARAM_TIPE_SUB_REMARK b on a.TIPE=b.TIPE and a.SUB_TIPE=b.SUB_TIPE " +
                                "where " +
                                "a.OWNER='" + LB_OWNER.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REMARK.DataSource = dt;
            DGR_REMARK.DataBind();
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            if (LB_TIPE.Text == "ENDPOL")
            {
                try
                {
                    conn.QueryString = "delete from REMARK where OWNER = '" + LB_OWNER.Text + "' " +
                                       "and TIPE = '" + LB_TIPE.Text + "' " +
                                       "and SUB_TIPE = '" + DDL_TIPE.SelectedValue + "' ";
                    conn.ExecuteQuery(); 
                }
                catch
                {
                    return;
                }
            }

            conn.QueryString = "insert into REMARK select " +
                "NEWID()," +
                "'" + LB_OWNER.Text + "'," +
                "'" + LB_TIPE.Text + "'," +
                "'" + DDL_TIPE.SelectedValue + "'," +
                "'" + TXT_REMARK.Text.Trim().Replace("'", "") + "'," +
                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                "GETDATE()," +
                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                "GETDATE()";

            conn.ExecuteNonQuery();

            FillDGR();
        }

        protected void DGR_REMARK_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    string code = e.Item.Cells[0].Text;
                    conn.QueryString = "delete from REMARK where ID='" + code + "'";
                    conn.ExecuteQuery();
                }
                catch
                {
                    return;
                }

                FillDGR();
            }
        }
    }
}