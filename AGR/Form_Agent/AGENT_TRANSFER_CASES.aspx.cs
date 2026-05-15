using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Agent
{
    public partial class AGENT_TRANSFER_CASES : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();

                conn.QueryString = "select " +
                                    "a.FULLNAME, " +
                                    "a.CD_DESCR, " +
                                    "a.SUBCD_DESCR " +
                                    "from	V_M_AGENTS a " +
                                    "where " +
                                    "a.CODE = '" + LB_CODE.Text + "'";
                conn.ExecuteQuery();
                LB_FULLNAME.Text = conn.GetFieldValue("FULLNAME").ToString();
                LB_CHANNEL.Text = conn.GetFieldValue("CD_DESCR").ToString();
                LB_LEVEL.Text = conn.GetFieldValue("SUBCD_DESCR").ToString();


                FillDGRUnselected();
                FillDGRSelected();
            }
        }

        protected void FillDGRSelected()
        {
            conn.QueryString = "exec SP_M_AGENT_TRANSFER_CASE_DETAIL '" + LB_CODE.Text + "'," + LB_SEQ.Text + ",1," +
                                "'" + TXT_POLICYNO_SELECTED.Text.Trim() + "'," +
                                "'" + TXT_INSUREDNAME_SELECTED.Text.Trim() + "'," +
                                "'" + TXT_PRODUCTNAME_SELECTED.Text.Trim() + "'";
            conn.ExecuteQuery();
            LB_RECORDS_SELECTED.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SELECTED.DataSource = dt;
            DGR_SELECTED.DataBind();
        }

        protected void FillDGRUnselected()
        {
            conn.QueryString = "exec SP_M_AGENT_TRANSFER_CASE_DETAIL '" + LB_CODE.Text + "'," + LB_SEQ.Text + ",0," +
                                "'" + TXT_POLICYNO_UNSELECTED.Text.Trim() + "'," +
                                "'" + TXT_INSUREDNAME_UNSELECTED.Text.Trim() + "'," +
                                "'" + TXT_PRODUCTNAME_UNSELECTED.Text.Trim() + "'";
            conn.ExecuteQuery();
            LB_RECORDS_UNSELECTED.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_UNSELECTED.DataSource = dt;
            DGR_UNSELECTED.DataBind();
        }

        protected void CB_UNSELECTED_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_UNSELECTED.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_UNSELECTED.Items[i].FindControl("CB_UNSELECTED");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void CB_SELECTED_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_SELECTED.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_SELECTED.Items[i].FindControl("CB_SELECTED");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DGR_UNSELECTED_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_UNSELECTED.CurrentPageIndex = e.NewPageIndex;
            FillDGRUnselected();
        }

        protected void DGR_SELECTED_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_SELECTED.CurrentPageIndex = e.NewPageIndex;
            FillDGRSelected();
        }

        protected void TXT_POLICYNO_UNSELECTED_TextChanged(object sender, EventArgs e)
        {
            DGR_UNSELECTED.CurrentPageIndex = 0;
            FillDGRUnselected();
        }

        protected void TXT_INSUREDNAME_UNSELECTED_TextChanged(object sender, EventArgs e)
        {
            DGR_UNSELECTED.CurrentPageIndex = 0;
            FillDGRUnselected();
        }

        protected void TXT_PRODUCTNAME_UNSELECTED_TextChanged(object sender, EventArgs e)
        {
            DGR_UNSELECTED.CurrentPageIndex = 0;
            FillDGRUnselected();
        }

        protected void TXT_POLICYNO_SELECTED_TextChanged(object sender, EventArgs e)
        {
            DGR_SELECTED.CurrentPageIndex = 0;
            FillDGRSelected();
        }

        protected void TXT_INSUREDNAME_SELECTED_TextChanged(object sender, EventArgs e)
        {
            DGR_SELECTED.CurrentPageIndex = 0;
            FillDGRSelected();
        }

        protected void TXT_PRODUCTNAME_SELECTED_TextChanged(object sender, EventArgs e)
        {
            DGR_SELECTED.CurrentPageIndex = 0;
            FillDGRSelected();
        }

        protected void BT_SELECT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_UNSELECTED.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_UNSELECTED.Items[i].FindControl("CB_UNSELECTED");
                if (cb.Checked)
                {
                    conn.QueryString = "insert into M_AGENT_TRANSFER_CASE_DETAIL select " +
                                        "CODE = '" + LB_CODE.Text + "'," +
                                        "SEQ = " + LB_SEQ.Text + "," +
                                        "POLICY_NO = '" + DGR_UNSELECTED.Items[i].Cells[0].Text + "'," +
                                        "PRODUCT_CODE = '" + DGR_UNSELECTED.Items[i].Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                }
            }

            DGR_UNSELECTED.CurrentPageIndex = 0;
            FillDGRUnselected();
            DGR_SELECTED.CurrentPageIndex = 0;
            FillDGRSelected();
        }

        protected void BT_UNSELECT_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_SELECTED.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_SELECTED.Items[i].FindControl("CB_SELECTED");
                if (cb.Checked)
                {
                    conn.QueryString = "delete from M_AGENT_TRANSFER_CASE_DETAIL where " +
                                        "CODE = '" + LB_CODE.Text + "' " +
                                        "and SEQ = " + LB_SEQ.Text + " " +
                                        "and POLICY_NO = '" + DGR_SELECTED.Items[i].Cells[0].Text + "' " +
                                        "and PRODUCT_CODE = '" + DGR_SELECTED.Items[i].Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                }
            }

            DGR_UNSELECTED.CurrentPageIndex = 0;
            FillDGRUnselected();
            DGR_SELECTED.CurrentPageIndex = 0;
            FillDGRSelected();
        }
    }
}