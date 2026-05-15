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
    public partial class MedicalLabPackage : System.Web.UI.Page
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
                LoadPackage();
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

        protected void LoadPackage()
        {
            conn.QueryString = "exec SP_PARAM_MEDICAL_LAB_PACKAGE " +
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
                TextBox txtPACKAGE = (TextBox)DGR_ITEMS.Items[i].FindControl("TXT_PACKAGE");
                TextBox txtPRICE = (TextBox)DGR_ITEMS.Items[i].FindControl("TXT_PRICE");
                LinkButton lbtITEMS = (LinkButton)DGR_ITEMS.Items[i].FindControl("LBT_ITEMS");

                txtPACKAGE.Text = DGR_ITEMS.Items[i].Cells[1].Text.Replace("&nbsp;","");
                txtPRICE.Text = DGR_ITEMS.Items[i].Cells[2].Text;

                if (DGR_ITEMS.Items[i].Cells[3].Text != "-1")
                    lbtITEMS.Text = DGR_ITEMS.Items[i].Cells[3].Text;
                else
                    lbtITEMS.Visible = false;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_ITEMS.Items.Count; i++)
            {
                try
                {
                    TextBox txtPACKAGE = (TextBox)DGR_ITEMS.Items[i].FindControl("TXT_PACKAGE");
                    TextBox txtPRICE = (TextBox)DGR_ITEMS.Items[i].FindControl("TXT_PRICE");

                    conn.QueryString = "exec SP_PARAM_MEDICAL_LAB_PACKAGE_UPSERT " +
                                        "'" + LB_CODE.Text + "'," +
                                        DGR_ITEMS.Items[i].Cells[0].Text + "," +
                                        "'" + txtPACKAGE.Text.Trim().Replace("'", "`") + "'," +
                                        txtPRICE.Text.Trim().Replace(",", "") + "," +
                                        DDL_YEAR.SelectedValue + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            LoadPackage();
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPackage();
        }

        protected void DGR_ITEMS_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Items")
            {
                ShowItems(DDL_YEAR.SelectedValue, e.Item.Cells[0].Text, e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_MEDICAL_LAB_PACKAGE where " +
                                        "COMPANY_CODE='" + LB_CODE.Text + "' " +
                                        "and SEQ=" + e.Item.Cells[0].Text + " " +
                                        "and YEAR=" + DDL_YEAR.SelectedValue;
                    conn.ExecuteNonQuery();
                    LoadPackage();
                }
                catch { }
            }
        }

        protected void ShowItems(string year, string seq, string descr)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);

            LB_YEAR.Text = year;
            LB_SEQ.Text = seq;
            LB_TITLE.Text = descr;

            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.DESCR, " +
                                "COUNT = (case when b.DOC_CODE is null then 0 else 1 end) " +
                                "from PR_UW_REQUIRED_DOCUMENT a " +
                                "left join PARAM_MEDICAL_LAB_PACKAGE_DETAIL b on a.CODE = b.DOC_CODE  " +
                                "and b.COMPANY_CODE = '" + LB_CODE.Text + "' " +
                                "and b.SEQ = " + seq + " " +
                                "and b.YEAR = " + year + " " +
                                "order by a.DESCR";
            conn.ExecuteQuery();

            DataTable dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DOC.DataSource = dt;
            DGR_DOC.DataBind();
            for (int j = 0; j < DGR_DOC.Items.Count; j++)
            {
                CheckBox cb = (CheckBox)DGR_DOC.Items[j].FindControl("CB");
                if (DGR_DOC.Items[j].Cells[2].Text != "0")
                {
                    cb.Checked = true;
                    DGR_DOC.Items[j].Cells[1].Text = "<B>" + DGR_DOC.Items[j].Cells[1].Text + "</B>";
                    DGR_DOC.Items[j].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }

        protected void BT_ITEMSAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PARAM_MEDICAL_LAB_PACKAGE_DETAIL where " +
                                "COMPANY_CODE = '" + LB_CODE.Text + "' " +
                                "and b.SEQ = " + LB_SEQ.Text + " " +
                                "and b.YEAR = " + LB_YEAR.Text;

            for (int j = 0; j < DGR_DOC.Items.Count; j++)
            {
                CheckBox cb = (CheckBox)DGR_DOC.Items[j].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "insert into PARAM_MEDICAL_LAB_PACKAGE_DETAIL select " +
                                            "'" + LB_CODE.Text + "', " +
                                            "'" + LB_SEQ.Text + "', " +
                                            "'" + DGR_DOC.Items[j].Cells[0].Text + "', " +
                                            "'" + LB_YEAR.Text + "', " +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE(), " +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            LoadPackage();
            ShowItems(LB_YEAR.Text, LB_SEQ.Text, LB_TITLE.Text);
        }
    }
}