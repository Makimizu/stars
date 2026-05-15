using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace UWBOX.Form_Partners
{
    public partial class MedicalLabItems : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["code"].ToString();
                Setup();
                LoadItems();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select YYYY = YEAR(GETDATE()) - (a.SEQ - 1) " +
                                "from SC_SEQ a " +
                                "where a.SEQ <= 5 order by a.SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadItems()
        {
            conn.QueryString = "exec SP_PARAM_MEDICAL_LAB_ITEMS " +
                                "'" + LB_CODE.Text + "'," +
                                DDL_YEAR.SelectedValue;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ITEMS.DataSource = dt;
            DGR_ITEMS.DataBind();

            for (int i = 0; i < DGR_ITEMS.Items.Count; i++)
            {
                TextBox txtPRICE = (TextBox)DGR_ITEMS.Items[i].FindControl("TXT_PRICE");
                txtPRICE.Text = DGR_ITEMS.Items[i].Cells[1].Text;
            }
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadItems();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PARAM_MEDICAL_LAB_ITEMS where " +
                                "COMPANY_CODE = '" + LB_CODE.Text + "' " +
                                "and YEAR = " + DDL_YEAR.SelectedValue;
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_ITEMS.Items.Count; i++)
            {
                TextBox txtPRICE = (TextBox)DGR_ITEMS.Items[i].FindControl("TXT_PRICE");

                if (txtPRICE.Text.Trim() != "0")
                {
                    try
                    {
                        conn.QueryString = "insert into PARAM_MEDICAL_LAB_ITEMS select " +
                                            "'" + LB_CODE.Text + "', " +
                                            "'" + DGR_ITEMS.Items[i].Cells[0].Text + "', " +
                                            "'" + txtPRICE.Text.Trim().Replace(",", "") + "', " +
                                            DDL_YEAR.SelectedValue + ", " +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            LoadItems();
        }
    }
}