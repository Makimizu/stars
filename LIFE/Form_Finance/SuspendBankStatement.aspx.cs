using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;


namespace LIFE.Form_Finance
{
    public partial class SuspendBankStatement : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                switch (Request.QueryString["mode"].ToString())
                {
                    case "I": LB_MODE.Text = " a.REGNO is not null "; LB_ACCNO.Text = "IDENTIFIED : "; break;
                    case "U": LB_MODE.Text = " a.REGNO is null "; LB_ACCNO.Text = "UN-DENTIFIED : "; break;
                }

                try
                {
                    DDL_ACCNO.SelectedValue = Request.QueryString["ACCNO"].ToString();
                    LB_ACCNO.Text = LB_ACCNO.Text + DDL_ACCNO.SelectedItem.Text;
                }
                catch { }

                DGR.CurrentPageIndex = 0;
                FillDGR();
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.SuspendBankStatement.location.href = '../Standard/default.html';</script>");
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select NOREK, DESCR = BANK from FINANCE.dbo.REKENING_MASTER where APPID_PRODUCT = 'LF' and COL = 1";
            conn.ExecuteQuery();
            DDL_ACCNO.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCNO.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            BT_SET.Attributes.Add("onclick", "if(!confirm('Are you sure to SET FLAG ?')){return false;};");
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec FINANCE.dbo.SP_PARAM_RK_VALIDASI " +
                                "'" + DDL_ACCNO.SelectedValue + "'," +
                                "'C'";
            conn.ExecuteQuery();
            DDL_FLAG.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FLAG.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }


            LB_RESULT.Text = "";
            string where = "";
            string postdate_start = "'1 jan 1980'";
            string postdate_end = "GETDATE()";
            string descr = "(case when a.REGNO is null then DESCR " +
                                "                        else DESCR +  " +
                                "                             '<BR><span style=\"font-size:xx-small;color:gray;\">' + POLICY_NO + ' - ' + VACC + '</span>' + " +
                                "                             '<BR><span style=\"font-size:xx-small;color:gray;\">' + FULLNAME + '</span>' + " +
                                "                             '<BR><span style=\"font-size:xx-small;color:gray;\">' + PRODUCT_NAME + '</span>' " +
                                "                        end)";

            if (DDL_ACCNO.SelectedValue != "")
                where = where + " and a.NOREK = '" + DDL_ACCNO.SelectedValue + "' ";

            if (TXT_DESCR.Text.Trim() != "")
                where = where + " and " + descr + " like '%" + TXT_DESCR.Text.Trim() + "%' ";

            if (TXT_POSTDATE1.Text.Trim() != "")
                postdate_start = "'" + GlobalUse.GlobalDateFormat(TXT_POSTDATE1.Text.Trim(), "d/M/yyyy") + "'";

            if (TXT_POSTDATE2.Text.Trim() != "")
                postdate_end = "'" + GlobalUse.GlobalDateFormat(TXT_POSTDATE2.Text.Trim(), "d/M/yyyy") + "'";

            //if (TXT_POSTDATE1.Text.Trim() != "")
            //    where = where + " and convert(date,a.POST_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            //if (TXT_POSTDATE2.Text.Trim() != "")
            //    where = where + " and convert(date,a.POST_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "ACCNO			= BANK, " +
                                "TRXID			= TRXID, " +
                                "POST_DATE		= convert(varchar(20), POST_DATE, 106), " +
                                "BALANCE		= replace(convert(varchar(100), convert(money, BALANCE), 1), '.00', ''), " +
                                "DESCR			= " + descr + ", " +
                                "REGNO			= REGNO " +
                                "from			V_LINK_FINANCE_REKENING_JURNAL_SUSPEND a " +
                                "where " +
                                LB_MODE.Text +
                                "and (convert(date, a.POST_DATE) between convert(date, " + postdate_start + ") and convert(date, " + postdate_end + ")) " +
                                where +
                                "order by " +
                                "a.POST_DATE";
            conn.ExecuteQuery(500000);

            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
                DGR.AllowPaging = false;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                if (e.Item.Cells[1].Text.Replace("&nbsp;", "") == "")
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.SuspendBankStatement.location.href = 'SuspendTRXID.aspx?TRXID=" + e.Item.Cells[0].Text + "';</script>");
                else
                    ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.SuspendBankStatement.location.href = 'ApplicationOutstanding.aspx?TRXID=" + e.Item.Cells[0].Text + "&REGNO=" + e.Item.Cells[1].Text + "';</script>");
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SET_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "exec FINANCE.dbo.SP_REKENING_JURNAL_FLAG_INSERT " +
                                            "'" + DGR.Items[i].Cells[0].Text + "'," +
                                            "'" + DDL_FLAG.SelectedValue + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = LB_ERR.Text + ex.Message + "<BR>";
                    }
                }
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}