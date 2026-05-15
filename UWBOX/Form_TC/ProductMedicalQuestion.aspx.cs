using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace UWBOX.Form_TC
{
    public partial class ProductMedicalQuestion : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PR_DATA_TYPE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_DATATYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MEDICAL_QUESTION '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select CODE, DESCR from PR_DATA_TYPE";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtQ1 = (TextBox)DGR.Items[i].FindControl("TXT_Q1");
                TextBox txtQ2 = (TextBox)DGR.Items[i].FindControl("TXT_Q2");
                TextBox txtSQL = (TextBox)DGR.Items[i].FindControl("TXT_SQLREFF");
                DropDownList ddlTYPE = (DropDownList)DGR.Items[i].FindControl("DDL_DATATYPE");

                txtQ1.Text = DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                txtQ2.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtSQL.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");

                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlTYPE.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                try
                {
                    ddlTYPE.SelectedValue = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
                }
                catch { }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_PRODUCT_MEDICAL_QUESTION where PRODUCT_CODE = '" + LB_CODE.Text + "' and SEQ = " + e.Item.Cells[0].Text;
                    conn.ExecuteQuery();
                    FillDGR();
                }
                catch { }
            }

            if (e.CommandName == "Save")
            {
                TextBox txtQ1 = (TextBox)e.Item.FindControl("TXT_Q1");
                TextBox txtQ2 = (TextBox)e.Item.FindControl("TXT_Q2");
                TextBox txtSQL = (TextBox)e.Item.FindControl("TXT_SQLREFF");
                DropDownList ddlTYPE = (DropDownList)e.Item.FindControl("DDL_DATATYPE");

                if (TXT_Q1.Text.Trim() == "")
                    return;

                conn.QueryString = "update PARAM_PRODUCT_MEDICAL_QUESTION set " +
                                    "DESCR = '" + TXT_Q1.Text.Trim() + "'," +
                                    "DATA_TYPE = '" + ddlTYPE.SelectedValue + "'," +
                                    "SQL_REFF = '" + txtSQL.Text.Trim() + "'," +
                                    "NEXT_QUESTION = '" + txtQ2.Text.Trim() + "'," +
                                    "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "LASTCHANGEDATE = GETDATE() " +
                                    "where " +
                                    "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                    "and SEQ = " + e.Item.Cells[0].Text;
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }

        protected void BT_INSERT_Click(object sender, EventArgs e)
        {
            if (TXT_Q1.Text.Trim() == "")
                return;

            conn.QueryString = "exec SP_PARAM_PRODUCT_MEDICAL_QUESTION_INSERT " +
                                "'" + LB_CODE.Text + "'," +
                                "'" + TXT_Q1.Text.Trim() + "'," +
                                "'" + DDL_DATATYPE.SelectedValue + "'," +
                                "'" + TXT_SQLREFF.Text.Trim() + "'," +
                                "'" + TXT_Q2.Text.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillDGR();
        }
    }
}