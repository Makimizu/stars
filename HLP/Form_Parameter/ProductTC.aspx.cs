using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Parameter
{
    public partial class ProductTC : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["code"];
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select a.CODE,a.DESCR from PR_BENEFIT a " +
                                "inner join PARAM_BENEFIT_SEQ b on a.CODE=b.CODE " +
                                "order by b.SEQ";
            conn.ExecuteQuery();
            DDL_BENEFIT.Items.Add(new ListItem("GENERAL", "0"));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_TC_FORMULA " +
                                "'" + LB_CODE.Text + "', " +
                                "'" + DDL_BENEFIT.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (DGR.Items[i].Cells[2].Text == "1")
                    cb.Checked = true;
            }

            conn.QueryString = "exec SP_PARAM_PRODUCT_TC_NOTE " +
                                "'" + LB_CODE.Text + "', " +
                                "'" + DDL_BENEFIT.SelectedValue + "'";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR2.DataSource = dt;
            DGR2.DataBind();

            for (int i = 0; i < DGR2.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR2.Items[i].FindControl("TXT_NOTE");
                txt.Text = DGR2.Items[i].Cells[1].Text.Replace("&nbsp;", "");
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    string taken = "0";
                    if (cb.Checked)
                        taken = "1";

                    try
                    {
                        conn.QueryString = "exec SP_PARAM_PRODUCT_TC_FORMULA_SAVE " +
                                            "'" + LB_CODE.Text + "', " +
                                            "'" + DDL_BENEFIT.SelectedValue + "', " +
                                            "'" + DGR.Items[i].Cells[0].Text + "', " +
                                            taken + "," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                FillDGR();
            }
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "New")
            {
                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_TC_NOTE_INSERT " +
                                        "'" + LB_CODE.Text + "', " +
                                        "'" + DDL_BENEFIT.SelectedValue + "', " +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGR();
            }


            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR2.Items.Count; i++)
                {
                    try
                    {
                        TextBox txt = (TextBox)DGR2.Items[i].FindControl("TXT_NOTE");
                        conn.QueryString = "update PARAM_PRODUCT_TC_NOTE set " +
                                            "NOTE = '" + txt.Text.Trim() + "'," +
                                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "LASTCHANGEDATE = GETDATE() " +
                                            "where " +
                                            "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                            "and BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                            "and SEQ = " + DGR2.Items[i].Cells[0].Text;
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                FillDGR();
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_PRODUCT_TC_NOTE where " +
                                        "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                        "and BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                        "and SEQ = " + e.Item.Cells[0].Text;
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGR();
            }
        }
    }
}