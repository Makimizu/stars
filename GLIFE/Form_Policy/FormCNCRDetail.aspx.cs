using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using DMS.DBConnection;
using System.Drawing;

namespace GLIFE.Form_Policy
{
    public partial class FormCNCRDetail : System.Web.UI.Page
    {

        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        string _query = string.Empty;
        string _sts = string.Empty;
        string PolicyID = string.Empty;
        string StartDate = string.Empty;
        string GroupName = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            PolicyID = Request.QueryString["ID"].ToString();
            StartDate = Request.QueryString["START_DATE"].ToString();
            GroupName = Request.QueryString["GROUP_NAME"].ToString();

            if (!Page.IsPostBack) 
            {
                lblGroupName.Text = Request.QueryString["GROUP_NAME"].ToString();
                lblStartDate.Text = "Start Date : " +Request.QueryString["START_DATE"].ToString();
                FillGridKP();
                dvAddNew.Visible = false;
            }
            
        }
        
        protected void FillGridKP()
        {
            _query = "USP_GET_DATA_POLICY_CNCR_SETUP_DETAIL '" + PolicyID + "', '" + StartDate + "', '" + GroupName + "'";
            conn.QueryString = _query;
            conn.ExecuteQuery();
            GV_KP.DataSource = conn.GetDataTable().Copy();
            GV_KP.DataBind();
        }

        protected void GV_KP_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "editx")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GV_KP.Rows[rowIndex];
                txtPolKP.Text = PolicyID;
                txtDate.Text = StartDate;
                txtKTKP.Text = GroupName;
                txtDescKP.Text = (row.FindControl("lblDesc") as Label).Text.Replace("<br />", "\n");
                txtNoUrut.Text = (row.FindControl("lblNoUrut") as Label).Text;
                string isChecked = (row.FindControl("lblisHeader") as Label).Text;
                if (isChecked == "HEADER")
                {
                    chkHeadKP.Checked = true;
                }
                else
                {
                    chkHeadKP.Checked = false;
                }
                dvAddNew.Visible = true;
                btnAdd.Visible = false;
                btnSave.Visible = true;
                btnCancel.Visible = true;
                Session["editsession"] = "EDIT";

            }
            else if (e.CommandName == "deletex")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GV_KP.Rows[rowIndex];
                string noUrutKP = (row.FindControl("lblNoUrut") as Label).Text;

                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    DeleteKP(PolicyID, noUrutKP, StartDate, GroupName);
                    FillGridKP();
                }
            }
            else if (e.CommandName == "upKP")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int i = rowIndex;
                GridViewRow prevRow = GV_KP.Rows[i];
                int PrevNoUrut = Convert.ToInt32((prevRow.FindControl("lblNoUrut") as Label).Text);
                int prevAutoNo = Convert.ToInt32((prevRow.FindControl("lblAutoNo") as Label).Text);
                int policyID = Convert.ToInt32((prevRow.FindControl("lblPolicyID") as Label).Text);

                if (i != 0)
                {
                    GridViewRow UpRow = GV_KP.Rows[i - 1];
                    int UpNoUrut = Convert.ToInt32((UpRow.FindControl("lblNoUrut") as Label).Text);
                    int UpAutoNo = Convert.ToInt32((UpRow.FindControl("lblAutoNo") as Label).Text);

                    UpdatePref(UpNoUrut, prevAutoNo);
                    UpdatePref(PrevNoUrut, UpAutoNo);
                    FillGridKP();
                }
                else 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage();", true);
                    FillGridKP();
                }                
            }
            else if (e.CommandName == "downKP")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int i = rowIndex;
                GridViewRow prevRow = GV_KP.Rows[rowIndex];
                int PrevNoUrut = Convert.ToInt32((prevRow.FindControl("lblNoUrut") as Label).Text);
                int prevAutoNo = Convert.ToInt32((prevRow.FindControl("lblAutoNo") as Label).Text);
                int policyID = Convert.ToInt32((prevRow.FindControl("lblPolicyID") as Label).Text);

                int RowCount = GV_KP.Rows.Count - 1;

                if (i != RowCount)
                {
                    GridViewRow DownRow = GV_KP.Rows[i + 1];
                    int DownNoUrut = Convert.ToInt32((DownRow.FindControl("lblNoUrut") as Label).Text);
                    int DownAutoNo = Convert.ToInt32((DownRow.FindControl("lblAutoNo") as Label).Text);

                    UpdatePref(DownNoUrut, prevAutoNo);
                    UpdatePref(PrevNoUrut, DownAutoNo);
                    FillGridKP();
                }
                else 
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alertMessage2();", true);
                    FillGridKP();
                }
            }
        }

        private void UpdatePref(int noUrut, int autoNo)
        {
            try
            {
                _query = "UPDATE POLICY_CNCR_SETUP_DETAIL SET NO_URUT = '"+ noUrut + "' " +
                         "WHERE POLICY_ID = '"+ PolicyID + "' AND START_DATE = '"+ StartDate + "' AND GROUP_NAME = '"+ GroupName + "' AND AUTO_NO = '"+ autoNo + "'";
                conn.QueryString = _query;
                conn.ExecuteQuery();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }

        private void DeleteKP(string noPolKP, string noUrutKP, string startDate, string groupName)
        {
            try
            {
                //_query = "USP_DELETE_KP '" + noPolKP + "'," + "'" + noUrutKP + "'";
                _query = "DELETE FROM POLICY_CNCR_SETUP_DETAIL WHERE POLICY_ID = " + noPolKP + " AND NO_URUT = " + noUrutKP + " AND START_DATE = '" + startDate + "' AND GROUP_NAME = '" + groupName + "'";
                conn.QueryString = _query;
                conn.ExecuteQuery();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblError.Text = "something went wrong, pls check your data";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            txtPolKP.Text = Request.QueryString["ID"].ToString();
            txtDate.Text = Request.QueryString["START_DATE"].ToString();
            Session["editsession"] = "ADD";
            txtKTKP.Text = Request.QueryString["GROUP_NAME"].ToString();
            btnCancel.Visible = true;
            btnAdd.Visible = false;
            btnSave.Visible = true;
            dvAddNew.Visible = true;
            btnBack.Visible = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Session["editsession"].ToString() == "ADD")
            {
                if (txtDescKP.Text != "")
                {
                    InsertKP();
                    FillGridKP();
                    dvAddNew.Visible = false;
                    btnAdd.Visible = true;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                    txtDescKP.Text = "";
                    chkHeadKP.Checked = false;
                    btnBack.Visible = true;
                }
                else
                {
                    lblError.Text = "Description cannot be empty";
                }
            }
            else if (Session["editsession"].ToString() == "EDIT")
            {
                if (txtDescKP.Text != "")
                {
                    UpdateKP();
                    FillGridKP();
                    dvAddNew.Visible = false;
                    btnAdd.Visible = true;
                    btnCancel.Visible = false;
                    btnSave.Visible = false;
                    btnBack.Visible = true;
                }
                else
                {
                    lblError.Text = "Description cannot be empty";
                }
            }

        }

        private void InsertKP()
        {
            try
            {
                string description = txtDescKP.Text.Replace("\n", "<br />");
                int isHeader = 0;

                if (chkHeadKP.Checked)
                {
                    isHeader = 1;
                }
                else
                {
                    isHeader = 0;
                }

                //_query = "USP_INSERT_KP '" + policyNo + "'," + "'" + description + "'," + "'" + isHeader + "'";
                _query = "USP_INSERT_POLICY_CNCR_SETUP_DETAIL '" + PolicyID + "'," + "'" + StartDate + "'," + "'" + GroupName + "'," + "'" + description + "'," + "'" + isHeader + "'";
                conn.QueryString = _query;
                conn.ExecuteQuery();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblError.Text = "something went wrong, pls check your data";
            }
        }

        private void UpdateKP()
        {
            try
            {
                string noUrut = txtNoUrut.Text;
                string description = txtDescKP.Text.Replace("\n", "<br />");
                int isHeader = 0;
                if (chkHeadKP.Checked)
                {
                    isHeader = 1;
                }
                else
                {
                    isHeader = 0;
                }
                _query = "USP_UPDATE_POLICY_CNCR_SETUP_DETAIL '" + PolicyID + "'," + "'" + StartDate + "','" + GroupName + "', '" + noUrut + "', '" + description + "', '" + isHeader + "'";
                conn.QueryString = _query;
                conn.ExecuteQuery();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                lblError.Text = "something went wrong, pls check your data";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            dvAddNew.Visible = false;
            txtDescKP.Text = "";
            txtKTKP.Text = "";
            txtPolKP.Text = "";
            btnSave.Visible = false;
            btnAdd.Visible = true;
            btnCancel.Visible = false;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.policybody.location.href = 'FormCNCRFrame.aspx?ID=" + PolicyID + "';</script>");
            
        }
    }
}