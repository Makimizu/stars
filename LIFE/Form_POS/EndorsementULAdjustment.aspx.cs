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
    public partial class EndorsementULAdjustment : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();

                Setup();
                FillDGR();
                CheckTrack();
            }
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                //DGR_LINK.Enabled = false;
                //BT_SAVE.Visible = false;
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select THEDATE = convert(varchar(20), GETDATE(), 103)";
            conn.ExecuteQuery();
            TXT_DATE.Text = conn.GetFieldValue("THEDATE").ToString();

            conn.QueryString = "select " +
                                "DESCR	= e.DESCR + '<BR><B>' + UPPER(d.DESCR) + '</B>' " +
                                "from		UWBOX.dbo.PARAM_ENDORSEMENT d  " +
                                "inner join	UWBOX.dbo.PR_ENDORSEMENT_GROUP e on d.GROUP_CODE = e.CODE " +
                                "where " +
                                "d.CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();


            conn.QueryString = "select CODE, DESCR = UPPER(DESCR) from V_LINK_SAVING_PR_FUND where FUND_GROUP = 'SAV_UL' and STAT = 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_FUND.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLTrans();
            FillDDLAmountUnit();
            AmountUnitChange();
        }

        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTrans();
            FillDDLAmountUnit();
            AmountUnitChange();
        }

        protected void DDL_FUND_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTrans();
        }

        protected void FillDDLTrans()
        {
            DDL_TRANS.Items.Clear();

            conn.QueryString = "select CODE, DESCR from V_APPLICATION_FUND_TRANS_TYPE where REGNO = '" + LB_REGNO.Text + "' and DC = '" + DDL_MODE.SelectedValue + "' and FUND_CODE = '" + DDL_FUND.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRANS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDDLAmountUnit()
        {
            DDL_AMOUNTUNIT.Items.Clear();
            conn.QueryString = "select " +
                                "CODE = b.CURRENCY_CODE, " +
                                "DESCR = replace(b.CURRENCY_CODE, '1', 'I') " +
                                "from APPLICATION_MASTER a " +
                                "inner join UWBOX.dbo.PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE collate database_default " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DDL_AMOUNTUNIT.Items.Add(new ListItem(conn.GetFieldValue("DESCR").ToString(), conn.GetFieldValue("CODE").ToString()));

            if (DDL_MODE.SelectedValue == "D")
                DDL_AMOUNTUNIT.Items.Add(new ListItem("UNIT", "UNIT"));
        }

        protected void AmountUnitChange()
        {
            if (DDL_AMOUNTUNIT.SelectedValue != "UNIT")
            {
                TXT_AMOUNTUNIT.BackColor = System.Drawing.Color.LightGreen;
                TXT_AMOUNTUNIT.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                TXT_AMOUNTUNIT.BackColor = System.Drawing.Color.Cyan;
                TXT_AMOUNTUNIT.ForeColor = System.Drawing.Color.Blue;
            }
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            if (TXT_AMOUNTUNIT.Text.Replace(",", "").Trim() == "" || DDL_FUND.SelectedValue == "")
                return;

            string amount = "0";
            string unit = "0";

            if (DDL_AMOUNTUNIT.SelectedValue != "UNIT")
                amount = TXT_AMOUNTUNIT.Text.Replace(",", "").Trim();
            else
                unit = TXT_AMOUNTUNIT.Text.Replace(",", "").Trim();

            try
            {
                conn.QueryString = "insert into APPLICATION_ENDORSEMENT_UL_ADJUSTMENT select " +
                                    "REGNO              = '" + LB_REGNO.Text + "'," +
                                    "SEQ                = '" + LB_SEQ.Text + "'," +
                                    "ENDORSEMENT_TYPE   = '" + LB_TYPE.Text + "'," +
                                    "THEDATE            = '" + GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "FUND_CODE          = '" + DDL_FUND.SelectedValue + "'," +
                                    "TRANS_TYPE         = '" + DDL_TRANS.SelectedValue + "'," +
                                    "DC                 = '" + DDL_MODE.SelectedValue + "'," +
                                    "AMOUNT             = " + amount + "," +
                                    "UNIT               = " + unit + "," +
                                    "CREATEBY           = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "CREATEDATE         = GETDATE()," +
                                    "LASTCHANGEBY       = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "LASTCHANGEDATE     = GETDATE()";
                conn.ExecuteNonQuery();
                FillDGR();
            }
            catch { }
        }

        protected void DDL_AMOUNTUNIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            AmountUnitChange();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "FUND_CODE      = a.FUND_CODE, " +
                                "TRANS_TYPE     = a.TRANS_TYPE, " +
                                "DC             = a.DC, " +
                                "THEDATE        = convert(varchar(20), a.THEDATE, 106), " +
                                "FUND           = UPPER(c.DESCR), " +
                                "DC_DESCR       = (case when a.DC='C' then 'SUBSCRIPTION' else 'REDEMPTION' end)," +
                                "TRANS          = UPPER(b.DESCR), " +
                                "AMOUNT         = convert(varchar(100), convert(money, a.AMOUNT), 1), " +
                                "UNIT           = convert(varchar(100), convert(money, a.UNIT), 1) " +
                                "from APPLICATION_ENDORSEMENT_UL_ADJUSTMENT a " +
                                "inner join V_SAVING_TRANS_TYPE b on a.TRANS_TYPE = b.CODE collate database_default AND a.DC=b.DC " +
                                "inner join V_LINK_SAVING_PR_FUND c on a.FUND_CODE = c.CODE collate database_default " +
                                "where " +
                                "a.REGNO                  = '" + LB_REGNO.Text + "' " +
                                "and a.SEQ                = '" + LB_SEQ.Text + "' " +
                                "and a.ENDORSEMENT_TYPE   = '" + LB_TYPE.Text + "' " +
                                "order by " +
                                "a.THEDATE," +
                                "a.DC," +
                                "a.TRANS_TYPE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                if (DGR.Items[i].Cells[2].Text == "C")
                {
                    DGR.Items[i].Cells[3].ForeColor = System.Drawing.Color.Blue;
                    DGR.Items[i].Cells[4].ForeColor = System.Drawing.Color.Blue;
                    DGR.Items[i].Cells[5].ForeColor = System.Drawing.Color.Blue;
                    DGR.Items[i].Cells[6].ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    DGR.Items[i].Cells[3].ForeColor = System.Drawing.Color.Red;
                    DGR.Items[i].Cells[4].ForeColor = System.Drawing.Color.Red;
                    DGR.Items[i].Cells[5].ForeColor = System.Drawing.Color.Red;
                    DGR.Items[i].Cells[6].ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from APPLICATION_ENDORSEMENT_UL_ADJUSTMENT " +
                                    "where " +
                                    "REGNO                  = '" + LB_REGNO.Text + "' " +
                                    "and SEQ                = '" + LB_SEQ.Text + "' " +
                                    "and ENDORSEMENT_TYPE   = '" + LB_TYPE.Text + "' " +
                                    "and FUND_CODE          = '" + e.Item.Cells[0].Text + "' " +
                                    "and TRANS_TYPE         = '" + e.Item.Cells[1].Text + "' " +
                                    "and DC                 = '" + e.Item.Cells[2].Text + "' " +
                                    "and THEDATE            = '" + e.Item.Cells[3].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }
    }
}