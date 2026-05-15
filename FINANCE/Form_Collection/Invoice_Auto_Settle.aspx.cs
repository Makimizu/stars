using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Collection
{
    public partial class Invoice_Auto_Settle : System.Web.UI.Page
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

                Setup();
            }
        }

        protected void Setup()
        {
            BT_SETTLE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk SETTLE ?')){return false;};");

            conn.QueryString = "select CODE,DESCR from PR_INVOICE_SETTLE_METHOD order by convert(int,CODE)";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_METODA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            DDL_SHORTITEM.Items.Clear();
            for (int i = 0; i < DGR.Columns.Count; i++)
            {
                if (DGR.Columns[i].Visible && DGR.Columns[i].HeaderText.Replace("&nbsp;", "") != "")
                {
                    try
                    {
                        DDL_SHORTITEM.Items.Add(new ListItem(DGR.Columns[i].HeaderText.Replace("<BR>", " "), ((BoundColumn)DGR.Columns[i]).DataField));
                    }
                    catch { }
                }
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            BT_SETTLE.Visible = false;

            conn.QueryString = "exec SP_COL_INVOICE_RK_MATCH " +
                                "'" + DDL_METODA.SelectedValue + "'," +
                                "'" + DDL_TIPEINV.SelectedValue + "'," +
                                "'" + TXT_NOPOL.Text.Trim() + "'," +
                                "'" + TXT_COMPANY.Text.Trim() + "'," +
                                "'" + TXT_BRANCH.Text.Trim() + "'";
            conn.ExecuteQuery(1000);

            if (conn.GetRowCount() > 0)
                BT_SETTLE.Visible = true;

            LB_RESULT.Text = "Total : " + conn.GetRowCount().ToString() + " Records";
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            if (conn.GetRowCount() > 0)
            {
                dt.DefaultView.Sort = DDL_SHORTITEM.SelectedValue + " " + DDL_SHORT.SelectedValue;
                dt = dt.DefaultView.ToTable();
            }
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lbCompany = (Label)DGR.Items[i].FindControl("LB_COMPANY");

                lbCompany.Text = "<table>" +
                                    "<tr><td>PRSH</td><td>:</td><td><B>" + DGR.Items[i].Cells[4].Text + "</B></td></tr>" +
                                    "<tr><td>CABANG</td><td>:</td><td><B>" + DGR.Items[i].Cells[7].Text + "</B></td></tr>" +
                                    "<tr><td>NO POLIS</td><td>:</td><td>" + DGR.Items[i].Cells[5].Text + "</td></tr>" +
                                    "<tr><td>VIR ACC</td><td>:</td><td>" + DGR.Items[i].Cells[6].Text + "</td></tr>" +
                                    "</table>";
            }

        }

        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "All")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    cb.Checked = true;
                }
            }
        }

        protected void BT_SETTLE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                if (cb.Checked)
                {
                    //try
                    //{
                    conn.QueryString = "exec SP_COL_SETTLE_PREMI_RK '" + DGR.Items[i].Cells[1].Text + "','" + DGR.Items[i].Cells[14].Text + "','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    //}
                    //catch { }
                }
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}