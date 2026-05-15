using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Settlement
{
    public partial class ReturValidasi : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                DGR.CurrentPageIndex = 0;
                FillDGR();
                LBL_TITLE.Text = "RETUR VALIDASI";

                conn.QueryString = "SELECT DESCR, ID FROM RETUR_PARAM_REASON";

                conn.ExecuteQuery();
                DDL_REASON.Items.Add(new ListItem("", ""));
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_REASON.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 1).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                    "NBR = ROW_NUMBER() over (order by a.USERDATE), " +
                                    "a.TRXID, " +
                                    "a.DOCNO, " +
                                    "a.RESERVED_PARAM1, " +
                                    "a.DESCR, " +
                                    "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00','') " +
                                    "from SETTLEMENT_DETAIL a " +
                                    "where " +
                                    "a.REKAPID = '" + Request.QueryString["REKAPID"] + "' " +
                                    "and a.ACC_NO = '" + Request.QueryString["ACC_NO"] + "' " +
                                    "order by a.USERDATE";
            conn.ExecuteQuery(1000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btIB = (Button)DGR.Items[i].FindControl("BT_FILE");
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            Button btFILE = (Button)e.Item.FindControl("BT_FILE_ALL");

            if (e.CommandName == "IBALL")
            {
                if (DDL_REASON.SelectedValue != "")
                {
                    ToRetur();
                }
                else
                {
                    Response.Write("<script>alert('REASON TIDAK BOLEH KOSONG');</script>");
                }
            }
        }

        protected void ToRetur()
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    conn.QueryString = "exec SP_RETUR '" + DGR.Items[i].Cells[2].Text + "', " + DDL_REASON.SelectedValue + ", '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', 'OTOMATIS'";
                    conn.ExecuteNonQuery();
                }
            }

            Response.Redirect("Retur.aspx");
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Visible)
                    cb.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}