using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Globalization;
namespace LIFE.Form_POS
{
    public partial class EndorsementSwitching : System.Web.UI.Page
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
                FillDGRLink();
                CheckTrack();
            }
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                DGR_LINK.Enabled = false;
                BT_SAVE.Visible = false;
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "DESCR	= e.DESCR + '<BR><B>' + UPPER(d.DESCR) + '</B>' " +
                                "from		UWBOX.dbo.PARAM_ENDORSEMENT d  " +
                                "inner join	UWBOX.dbo.PR_ENDORSEMENT_GROUP e on d.GROUP_CODE = e.CODE " +
                                "where " +
                                "d.CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

        }


        protected void FillDGRLink()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_SWITCHING " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LINK.DataSource = dt;
            DGR_LINK.DataBind();

            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            conn2.QueryString = "select CODE = 'UNIT', DESCR = 'UNIT' union all " +
                                "select CODE = '%', DESCR = '%' union all " +
                                "select " +
                                "CODE		= 'AMOUNT', " +
                                "DESCR		= replace(b.CURRENCY_CODE, '1', 'I') " +
                                "from		APPLICATION_MASTER a " +
                                "inner join	UWBOX.dbo.PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE collate database_default " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn2.ExecuteQuery();

            for (int i = 0; i < DGR_LINK.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_LINK.Items[i].FindControl("TXT_AMOUNT_LINK");
                TextBox txtUNIT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_UNIT_LINK");
                TextBox txtPCT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_PCT_LINK");
                DropDownList ddlTOGGLE = (DropDownList)DGR_LINK.Items[i].FindControl("DDL_TOGGLE");
                txt.Text = DGR_LINK.Items[i].Cells[2].Text.Trim().Replace("&nbsp;", "");
                txtUNIT.Text = DGR_LINK.Items[i].Cells[3].Text.Trim().Replace("&nbsp;", "");


                txt.Text = DGR_LINK.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtUNIT.Text = DGR_LINK.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                txtPCT.Text = DGR_LINK.Items[i].Cells[4].Text.Replace("&nbsp;", "");


                for (int k = 0; k < conn2.GetRowCount(); k++)
                    ddlTOGGLE.Items.Add(new ListItem(conn2.GetFieldValue(k, 1).ToString(), conn2.GetFieldValue(k, 0).ToString()));

                if (txtUNIT.Text.Trim() != "0" && txtUNIT.Text.Trim() != "")
                {
                    ddlTOGGLE.SelectedValue = "UNIT";
                    txt.Visible = false;
                    txtPCT.Visible = false;
                    txtUNIT.Visible = true;
                    DGR_LINK.Items[i].BackColor = System.Drawing.Color.Yellow;
                }

                if (txt.Text.Trim() != "0" && txt.Text.Trim() != "")
                {
                    ddlTOGGLE.SelectedValue = "AMOUNT";
                    txtUNIT.Visible = false;
                    txtPCT.Visible = false;
                    txt.Visible = true;
                    DGR_LINK.Items[i].BackColor = System.Drawing.Color.Yellow;
                }

                if (txtPCT.Text.Trim() != "0" && txtPCT.Text.Trim() != "")
                {
                    ddlTOGGLE.SelectedValue = "%";
                    txtUNIT.Visible = false;
                    txt.Visible = false;
                    txtPCT.Visible = true;
                    DGR_LINK.Items[i].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }


        protected void BT_SAVE_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < DGR_LINK.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_LINK.Items[i].FindControl("TXT_AMOUNT_LINK");
                TextBox txtUNIT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_UNIT_LINK");
                TextBox txtPCT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_PCT_LINK");
                DropDownList ddlTOGGLE = (DropDownList)DGR_LINK.Items[i].FindControl("DDL_TOGGLE");

                string txtUnitBalance = DGR_LINK.Items[i].Cells[8].Text; //AVAILABLE UNIT
                string txtAmountBalance = DGR_LINK.Items[i].Cells[9].Text; //ESTIMATEDBALANCE



                string dsds = txtPCT.Text;

                string unit, amount, pct;
                unit = amount = pct = "0";

                switch (ddlTOGGLE.SelectedValue)
                {
                    case "AMOUNT":
                        amount = txt.Text.Trim().Replace(",", "");
                        break;

                    case "UNIT":
                        unit = txtUNIT.Text.Trim().Replace(",", "");
                        break;

                    case "%":
                        pct = txtPCT.Text.Trim().Replace(",", "");
                        break;
                }

                decimal amountVal = 0, unitVal = 0, persenVal=0;
                decimal amountBalanceVal = 0, unitBalanceVal = 0, persenunitBalanceVal=0;

                // bersihin input user
                amount = amount.Replace(",", "");
                unit = unit.Replace(",", "");
                pct = pct.Replace(",", "");

                // parse input
                decimal.TryParse(amount, NumberStyles.Any, CultureInfo.InvariantCulture, out amountVal);
                decimal.TryParse(unit, NumberStyles.Any, CultureInfo.InvariantCulture, out unitVal);
                decimal.TryParse(pct, NumberStyles.Any, CultureInfo.InvariantCulture, out persenVal);

                // handle &nbsp; + format grid
                txtAmountBalance = txtAmountBalance.Replace("&nbsp;", "0").Replace(",", "");
                txtUnitBalance = txtUnitBalance.Replace("&nbsp;", "0").Replace(",", "");

                // parse balance
                decimal.TryParse(txtAmountBalance, NumberStyles.Any, CultureInfo.InvariantCulture, out amountBalanceVal);
                decimal.TryParse(txtUnitBalance, NumberStyles.Any, CultureInfo.InvariantCulture, out unitBalanceVal);
                decimal.TryParse(txtAmountBalance, NumberStyles.Any, CultureInfo.InvariantCulture, out persenunitBalanceVal);

                // VALIDATION
                if (amountVal > amountBalanceVal)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                     "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Amount melebihi balance!' }).then(() => { window.scrollTo(0,0); });",
                     true);
                    return;
                }

                if (unitVal > unitBalanceVal)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Unit melebihi balance!' });",
                    true);
                    return; 
                }
                if (persenVal > 100)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                    "Swal.fire({ icon: 'error', title: 'Oops...', text: 'Persentase melebihi balance!' });",
                    true);
                    return;
                }

                //if (txt.Visible)
                //    amount = txt.Text.Trim().Replace(",", "");
                //if (txtUNIT.Visible)
                //    unit = txtUNIT.Text.Trim().Replace(",", "");
                //if (txtPCT.Visible)
                //    pct = txtPCT.Text.Trim().Replace(",", "");

                /*
                switch (ddlTOGGLE.SelectedValue)
                {
                    case "AMOUNT": amount = txt.Text.Trim().Replace(",", ""); break;
                    case "UNIT": unit = txtUNIT.Text.Trim().Replace(",", ""); break;
                    case "%": pct = txtPCT.Text.Trim().Replace(",", ""); break;
                }
                */


                try
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_SWITCHING_UPSERT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'," +
                                "'" + DGR_LINK.Items[i].Cells[0].Text + "'," +
                                "'" + DGR_LINK.Items[i].Cells[1].Text + "'," +
                                amount + "," +
                                unit + "," +
                                pct + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }



            FillDGRLink();
        }

        protected void DDL_TOGGLE_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_LINK.Items.Count; i++)
            {
                DropDownList ddlTOGGLE = (DropDownList)DGR_LINK.Items[i].FindControl("DDL_TOGGLE");

                if (ddlTOGGLE == (DropDownList)sender)
                {
                    TextBox txt = (TextBox)DGR_LINK.Items[i].FindControl("TXT_AMOUNT_LINK");
                    TextBox txtUNIT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_UNIT_LINK");
                    TextBox txtPCT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_PCT_LINK");

                    // reset semua (hide)
                    txt.Style["display"] = "none";
                    txtUNIT.Style["display"] = "none";
                    txtPCT.Style["display"] = "none";

                    // tampilkan sesuai pilihan
                    switch (ddlTOGGLE.SelectedValue)
                    {
                        case "AMOUNT":
                            txt.Style["display"] = "inline";
                            break;

                        case "UNIT":
                            txtUNIT.Style["display"] = "inline";
                            break;

                        case "%":
                            txtPCT.Style["display"] = "inline";
                            break;
                    }

                    return;
                }
            }
        }

        //protected void DDL_TOGGLE_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < DGR_LINK.Items.Count; i++)
        //    {
        //        DropDownList ddlTOGGLE = (DropDownList)DGR_LINK.Items[i].FindControl("DDL_TOGGLE");

        //        if (ddlTOGGLE == (DropDownList)sender)
        //        {
        //            TextBox txt = (TextBox)DGR_LINK.Items[i].FindControl("TXT_AMOUNT_LINK");
        //            TextBox txtUNIT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_UNIT_LINK");
        //            TextBox txtPCT = (TextBox)DGR_LINK.Items[i].FindControl("TXT_PCT_LINK");

        //            switch (ddlTOGGLE.SelectedValue)
        //            {
        //                case "AMOUNT":
        //                    txt.Visible = true;
        //                    txtUNIT.Visible = false;
        //                    txtPCT.Visible = false; break;
        //                case "UNIT":
        //                    txt.Visible = false;
        //                    txtUNIT.Visible = true;
        //                    txtPCT.Visible = false; break;
        //                case "%":
        //                    txt.Visible = false;
        //                    txtUNIT.Visible = false;
        //                    txtPCT.Visible = true; break;
        //            }

        //            return;
        //        }
        //    }
        //}

    }
}