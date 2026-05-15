using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Quot
{
    public partial class QuotationPackage : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_QUOTNO.Text = Request.QueryString["code"];
                LB_VER.Text = Request.QueryString["ver"];
                FillDGRRsv();
            }
        }

        protected void FillDGRRsv()
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN " +
                                "'" + LB_QUOTNO.Text + "'," + LB_VER.Text;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR0.DataSource = dt;
            DGR0.DataBind();

            for (int i = 0; i < DGR0.Items.Count; i++)
            {
                TextBox txtPKG = (TextBox)DGR0.Items[i].FindControl("TXT_PKGNAME0");
                txtPKG.Text = DGR0.Items[i].Cells[2].Text.Replace("&nbsp;", "");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    txtPKG.Enabled = false;
                }
            }

            for (int i = 1; i < DGR0.Columns.Count-24; i++)
            {
                conn.QueryString = "select distinct d.SEQ " +
                                    "from QUOTATION a " +
                                    "inner join PARAM_PRODUCT_BENEFIT_DETAIL b on a.PRODUCT_CODE=b.PRODUCT_CODE " +
                                    "inner join PARAM_BENEFIT_DETAIL c on b.BENEFIT_DETAIL_ID=c.CODE " +
                                    "inner join PARAM_BENEFIT_SEQ d on c.BENEFIT_ID=d.CODE " +
                                    "where " +
                                    "d.SEQ = " + i.ToString();
                conn.ExecuteQuery();

                if (conn.GetRowCount() == 0)
                {
                    DGR0.Columns[i + 23].Visible = false;
                }
                else
                {
                    conn.QueryString = "select " +
                                        "b.CODE, " +
                                        "b.PLAN_VALUE " +
                                        "from QUOTATION a " +
                                        "inner join PARAM_PRODUCT_PLAN b on a.PRODUCT_CODE=b.PRODUCT_CODE " +
                                        "inner join PARAM_BENEFIT_SEQ c on b.BENEFIT_ID=c.CODE and c.SEQ=" + i.ToString() + " " +
                                        "where " +
                                        "a.QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                        "order by " +
                                        "b.PLAN_VALUE";
                    conn.ExecuteQuery();

                    for (int j = 0; j < DGR0.Items.Count; j++)
                    {
                        DropDownList ddl = (DropDownList)DGR0.Items[j].FindControl("DDL_" + i.ToString());
                        DropDownList ddl1 = (DropDownList)DGR0.Items[j].FindControl("DDL_1");

                        if (Request.QueryString["readonly"].ToString() == "1")
                        {
                            ddl.Enabled = false;
                        }

                        if (i != 1)
                        {
                            conn.QueryString = "exec SP_PARAM_PRODUCT_PLAN_GROUP " +
                                            "'" + ddl1.SelectedValue + "'," +
                                            i.ToString();
                            conn.ExecuteQuery();
                        }


                        ddl.Items.Add(new ListItem("", ""));
                        for (int k = 0; k < conn.GetRowCount(); k++)
                        {
                            ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                        }

                        try
                        {
                            ddl.SelectedValue = DGR0.Items[j].Cells[i + 3].Text;
                        }
                        catch { }

                    }

                    
                }
            }
        }

        protected void DGR0_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                for (int i = 1; i < DGR0.Columns.Count - 24; i++)
                {

                    conn.QueryString = "select distinct d.CODE " +
                                        "from QUOTATION a " +
                                        "inner join PARAM_PRODUCT_BENEFIT_DETAIL b on a.PRODUCT_CODE=b.PRODUCT_CODE " +
                                        "inner join PARAM_BENEFIT_DETAIL c on b.BENEFIT_DETAIL_ID=c.CODE " +
                                        "inner join PARAM_BENEFIT_SEQ d on c.BENEFIT_ID=d.CODE " +
                                        "where " +
                                        "d.SEQ = " + i.ToString();
                    conn.ExecuteQuery();

                    if (conn.GetRowCount() > 0)
                    {
                        e.Item.Cells[i + 23].Text = conn.GetFieldValue("CODE").ToString();
                    }

                }

                Button btnSaveAll = (Button)e.Item.FindControl("BT_SAVEALL");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    btnSaveAll.Visible = false;
                }
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnSave = (Button)e.Item.FindControl("BT_SAVE0");
                Button btnDel = (Button)e.Item.FindControl("BT_DEL");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    btnSave.Visible = false;
                    btnDel.Visible = false;
                }
            }
        }

        protected void SavePackage(DataGridItem e)
        {
            TextBox txtPKG = (TextBox)e.FindControl("TXT_PKGNAME0");

            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_UPSERT " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text + "," +
                                    e.Cells[0].Text + "," +
                                    "'" + txtPKG.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch
            {
                return;
            }

            for (int i = 1; i < DGR0.Columns.Count - 24; i++)
            {
                DropDownList ddl = (DropDownList)e.FindControl("DDL_" + i.ToString());

                if (i == 1 && ddl.SelectedValue == "")
                {
                    return;
                }

                if (ddl.SelectedValue != "")
                {
                    conn.QueryString = "insert into QUOTATION_VERSION_PACKAGE_PLAN select " +
                                        "'" + LB_QUOTNO.Text + "'," +
                                        LB_VER.Text + "," +
                                        e.Cells[0].Text + "," +
                                        "'" + ddl.SelectedValue + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GETDATE()";
                    conn.ExecuteNonQuery();

                    conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_DETAIL_INSERT " +
                                        "'" + LB_QUOTNO.Text + "'," +
                                        LB_VER.Text + "," +
                                        e.Cells[0].Text + "," +
                                        "'" + ddl.SelectedValue + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
            }
        }

        protected void DGR0_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "SaveAll")
            {
                for (int i = 0; i < DGR0.Items.Count; i++)
                {
                    bool bFound = false;

                    for (int j = 1; j < DGR0.Columns.Count - 24; j++)
                    {
                        DropDownList ddl = (DropDownList)DGR0.Items[i].FindControl("DDL_" + j.ToString());

                        if (j == 1 && ddl.SelectedValue == "")
                            break;

                        if (ddl.SelectedValue != "")
                        {
                            bFound = true;
                            break;
                        }
                    }

                    if (bFound)
                        SavePackage(DGR0.Items[i]);
                }

                FillDGRRsv();
            }

            if (e.CommandName == "Save")
            {
                SavePackage(e.Item);
                FillDGRRsv();
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_VER_DELETE " +
                                        "'" + LB_QUOTNO.Text + "'," +
                                        LB_VER.Text + "," +
                                        e.Item.Cells[0].Text;
                    conn.ExecuteNonQuery();
                    FillDGRRsv();
                }
                catch { }
            }
        }

        protected void DDL_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int j = 0; j < DGR0.Items.Count; j++)
            {
                DropDownList ddl1 = (DropDownList)DGR0.Items[j].FindControl("DDL_1");
                if ((DropDownList)sender != ddl1)
                    continue;

                for (int i = 2; i < DGR0.Columns.Count - 24; i++)
                {
                    DropDownList ddl = (DropDownList)DGR0.Items[j].FindControl("DDL_" + i.ToString());

                    conn.QueryString = "exec SP_PARAM_PRODUCT_PLAN_GROUP " +
                                        "'" + ddl1.SelectedValue + "'," +
                                        i.ToString();
                    conn.ExecuteQuery();

                    ddl.Items.Clear();
                    ddl.Items.Add(new ListItem("", ""));
                    for (int k = 0; k < conn.GetRowCount(); k++)
                    {
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                    }   
                }
            }
        }


    }
}
