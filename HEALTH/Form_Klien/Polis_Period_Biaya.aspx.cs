using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Period_Biaya : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PERIOD0.Text = Request.QueryString["PolicyPeriod"];
                
                FillDGRPeriodBiaya();


            }
        }





        protected void FillDGRPeriodBiaya()
        {


            conn.QueryString = "select " +
                                " a.TIPE_BIAYA, " +
                                " b.DESCR, " +
                                " AMOUNT = replace(convert(varchar(100),convert(money,a.BIAYA_AMOUNT),1),'.00','') " +
                                " from POLICY_PERIOD_BIAYA a " +
                                " inner join PR_BIAYA b on a.TIPE_BIAYA=b.CODE " +
                                " where " +
                                " a.POLICY_PERIOD_ID = '" + LB_PERIOD0.Text + "' ";


            conn.ExecuteQuery();
            DGR0.DataSource = conn.GetDataTable();
            DGR0.DataBind();

            for (int i = 0; i < DGR0.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR0.Items[i].FindControl("TXT_LOADING0");
                txt.Text = DGR0.Items[i].Cells[1].Text;
            }
        }

        protected void DGR0_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "exec SP_UW_POLICY_PERIOD_BIAYA_TOTAL " +
                                    "'" + LB_PERIOD0.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[3].Text = conn.GetFieldValue("VAL").ToString();
            }
        }



        protected void BT_SAVE0_Click(object sender, EventArgs e)
        {
            LB_ERR0.Text = "";

            for (int i = 0; i < DGR0.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR0.Items[i].FindControl("TXT_LOADING0");

                try
                {
                    conn.QueryString = "update POLICY_PERIOD_BIAYA set " +
                                        "BIAYA_AMOUNT = convert(float," + txt.Text + "), " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "POLICY_PERIOD_ID = '" + LB_PERIOD0.Text + "' " +
                                        "and TIPE_BIAYA = '" + DGR0.Items[i].Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR0.Text = LB_ERR0.Text + "<BR>- " + ex.Message;
                }
            }

            FillDGRPeriodBiaya();

        }

    }
}