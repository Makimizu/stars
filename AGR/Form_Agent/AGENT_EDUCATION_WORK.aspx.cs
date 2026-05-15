using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;
namespace AGR
{
    public partial class AGENT_EDUCATION_WORK : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["code"];
                Setup();
                LoadRecord_EDUCATION();
                LoadRecord_WORKEXP();
            }

        }
        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PR_SCHOOL_GRADE order by CODE desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_GRADE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from PR_AGENT_WORKING_FIELD";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_WORKEXP_FIELD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from PR_AGENT_WORKING_STATUS";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_WORKEXP_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void LoadRecord_EDUCATION()
        {
            TR_EDUCATION.Visible = false;
            BT_EDUCATION_CANCEL.Visible = false;
            DGR_EDUCATION.Visible = true;

            conn.QueryString = "exec SP_M_AGENT_EDUCATION '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_EDUCATION.DataSource = dt;
            DGR_EDUCATION.DataBind();

        }

        protected void LoadRecord_WORKEXP()
        {
            TR_WORKEXP.Visible = false;
            BT_WORKEXP_CANCEL.Visible = false;
            DGR_WORKEXP.Visible = true;

            conn.QueryString = "exec SP_M_AGENT_WORKING_EXPERIENCE '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_WORKEXP.DataSource = dt;
            DGR_WORKEXP.DataBind();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TR_EDUCATION.Visible)
            {
                LB_ERROR.Text = "";
                try
                {
                    conn.QueryString = "exec SP_M_AGENT_EDUCATION_INSERT " +
                                            "@CODE = '" + LB_ID.Text + "'," +
                                            "@START_DATE = '" + GlobalUse.GlobalDateFormat(TXT_START_DATE.Text, "d/M/yyyy") + "'," +
                                            "@END_DATE = '" + GlobalUse.GlobalDateFormat(TXT_END_DATE.Text, "d/M/yyyy") + "'," +
                                            "@INSTITUTION_NAME = '" + TXT_INSTITUTION_NAME.Text + "'," +
                                            "@GRADE_CODE = '" + DDL_GRADE.SelectedValue + "'," +
                                            "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";


                    conn.ExecuteNonQuery();
                    LoadRecord_EDUCATION();

                    //data saved notification
                    //string message = "Your Data has been Saved Successfully.";
                    //string script = "window.onload = function(){ alert('";
                    //script += message;
                    //script += "')};";
                    //ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Your Data has been Saved Successfully.');", true);


                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
            else
            {
                TR_EDUCATION.Visible = true;
                BT_EDUCATION_CANCEL.Visible = true;
                DGR_EDUCATION.Visible = false;
            }
        }

        protected void BT_SAVE_WORKEXP_Click(object sender, EventArgs e)
        {
            if (TR_WORKEXP.Visible)
            {
                LB_ERROR.Text = "";
                try
                {
                    conn.QueryString = "exec SP_M_AGENT_WORKING_EXPERIENCE_INSERT " +
                                            "@CODE = '" + LB_ID.Text + "'," +
                                            "@START_DATE = '" + GlobalUse.GlobalDateFormat(TXT_START_DATE_WORKEXP.Text, "d/M/yyyy") + "'," +
                                            "@END_DATE = '" + GlobalUse.GlobalDateFormat(TXT_END_DATE_WORKEXP.Text, "d/M/yyyy") + "'," +
                                            "@COMPANY_NAME = '" + TXT_WORKEXP_COMPANY.Text + "'," +
                                            "@TITLE = '" + TXT_WORKEXP_TITLE.Text + "'," +
                                            "@REMARK = '" + TXT_WORKEXP_REMARK.Text + "'," +
                                            "@WORKING_STATUS = '" + DDL_WORKEXP_STATUS.SelectedValue + "'," +
                                            "@WORKING_FIELD = '" + DDL_WORKEXP_FIELD.SelectedValue + "'," +
                                            "@LAST_POSITION = '" + TXT_WORKEXP_POSITION.Text + "'," +
                                            "@LAST_SALARY = '" + TXT_WORKEXP_SALARY.Text.Replace(",", "") + "'," +
                                            "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";


                    conn.ExecuteNonQuery();
                    LoadRecord_WORKEXP();

                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
            else
            {
                TR_WORKEXP.Visible = true;
                BT_WORKEXP_CANCEL.Visible = true;
                DGR_WORKEXP.Visible = false;
            }
        }

        protected void DGR_EDUCATION_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from M_AGENT_EDUCATION where CODE = '" + LB_ID.Text + "' and START_DATE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                LoadRecord_EDUCATION();
            }
        }

        protected void DGR_WORKEXP_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from M_AGENT_WORKING_EXPERIENCE where CODE = '" + LB_ID.Text + "' and START_DATE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                LoadRecord_WORKEXP();
            }
        }

        protected void BT_WORKEXP_CANCEL_Click(object sender, EventArgs e)
        {
            TR_WORKEXP.Visible = false;
            BT_WORKEXP_CANCEL.Visible = false;
            DGR_WORKEXP.Visible = true;
        }

        protected void BT_EDUCATION_CANCEL_Click(object sender, EventArgs e)
        {
            TR_EDUCATION.Visible = false;
            BT_EDUCATION_CANCEL.Visible = false;
            DGR_EDUCATION.Visible = true;
        }
    }
}