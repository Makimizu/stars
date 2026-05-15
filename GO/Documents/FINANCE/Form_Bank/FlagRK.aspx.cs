using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace FINANCE.Form_Bank
{
    public partial class FlagRK : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void Setup()
        {
            DDL_NOREK.Items.Clear();
            conn.QueryString = "select NOREK, BANK from REKENING_MASTER order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_NOREK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLFlag();
            BT_SET.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk SET ?')){return false;};");

            conn.QueryString = "select STARTDATE = '1/' + convert(varchar(2),month(GETDATE())) +'/' + convert(varchar(4),year(GETDATE()))";
            conn.ExecuteQuery();
            TXT_POSTDATE.Text = conn.GetFieldValue("STARTDATE").ToString();
        }

        protected void FillDGR()
        {
            LB_ERR.Text = "";
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_DESCR.Text.Trim() != "")
                where = where + "and a.DESCR like '%" + TXT_DESCR.Text.Trim() + "%' ";

            if (TXT_POSTDATE.Text.Trim() != "")
                where = where + "and convert(date,a.POST_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_POSTDATE2.Text.Trim() != "")
                where = where + "and convert(date,a.POST_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "NOREK, " +
                                "TRXID, " +
                                "POST_DATE = convert(varchar(20),POST_DATE,106), " +
                                "DESCR, " +
                                "AMOUNT = (case when DEBET>0 then replace(convert(varchar(100),convert(money,DEBET),1),'.00','') else replace(convert(varchar(100),convert(money,CREDIT),1),'.00','') end) " +
                                "from V_REKENING_JURNAL_ORI a " +
                                "where " +
                                "a.NOREK = '" + DDL_NOREK.SelectedValue + "' " + DDL_INOUT.SelectedValue + " " + where +
                                "order by a.POST_DATE";

            conn.ExecuteQuery(50000);
            LB_RECORDS.Text = conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select AMOUNT=replace(convert(varchar(100),convert(money,SUM(AMOUNT)),1),'.00','') from (" +
                                "select " +
                                "AMOUNT = (case when DEBET>0 then DEBET else CREDIT end) " +
                                "from V_REKENING_JURNAL_ORI a " +
                                "where " +
                                "a.NOREK = '" + DDL_NOREK.SelectedValue + "' " + DDL_INOUT.SelectedValue + " " + where + ") a";

            conn.ExecuteQuery();
            LB_AMOUNT.Text = conn.GetFieldValue("AMOUNT").ToString();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {

        }

        protected void DDL_INOUT_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
            FillDDLFlag();
        }

        protected void FillDDLFlag()
        {
            string DC = "C";
            if (DDL_INOUT.SelectedItem.Text == "OUTBOUND")
                DC = "D";

            conn.QueryString = "exec SP_PARAM_RK_VALIDASI " +
                                "'" + DDL_NOREK.SelectedValue + "'," +
                                "'" + DC + "'";
            conn.ExecuteQuery();
            DDL_FLAG.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FLAG.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
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

        protected void DDL_NOREK_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
            FillDDLFlag();
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
                        conn.QueryString = "exec SP_REKENING_JURNAL_FLAG_INSERT " +
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