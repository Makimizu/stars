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
    public partial class TC : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillTC();
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

        protected void FillTC()
        {
            conn.QueryString = "select " +
                                "BENEFIT = isnull(b.CODE,'0'), " +
                                "TC = a.CODE,  " +
                                "BENEFIT_DESCR = isnull(b.DESCR,'GENERAL'), " +
                                "TC_DESCR = a.DESCR, " +
                                "MALE = (case when c.GENDER is null then 0 else 1 end), " +
                                "FEMALE = (case when d.GENDER is null then 0 else 1 end), " +
                                "CHILD = (case when e.GENDER is null then 0 else 1 end), " +
                                "CHILD_D = (case when f.GENDER is null then 0 else 1 end) " +
                                "from PARAM_TC_FORMULA a " +
                                "left join PR_BENEFIT b on b.CODE=a.BENEFIT_ID " +
                                "left join PARAM_TC_FORMULA_BENEFIT c on a.BENEFIT_ID=c.BENEFIT_ID and a.CODE=c.TC_CODE and c.GENDER='M' " +
                                "left join PARAM_TC_FORMULA_BENEFIT d on a.BENEFIT_ID=d.BENEFIT_ID and a.CODE=d.TC_CODE and d.GENDER='F' " +
                                "left join PARAM_TC_FORMULA_BENEFIT e on a.BENEFIT_ID=e.BENEFIT_ID and a.CODE=e.TC_CODE and e.GENDER='C' " +
                                "left join PARAM_TC_FORMULA_BENEFIT f on a.BENEFIT_ID=f.BENEFIT_ID and a.CODE=f.TC_CODE and f.GENDER='D' " +
                                "where " +
                                "a.BENEFIT_ID='" +DDL_BENEFIT.SelectedValue+ "' " +
                                "order by  " +
                                "a.CODE";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCode = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_TCDESCR");
                CheckBox cbM = (CheckBox)DGR.Items[i].FindControl("CB_MALE");
                CheckBox cbF = (CheckBox)DGR.Items[i].FindControl("CB_FEMALE");
                CheckBox cbC = (CheckBox)DGR.Items[i].FindControl("CB_CHILD");
                CheckBox cbD = (CheckBox)DGR.Items[i].FindControl("CB_CHILD_D");

                lbCode.Text = DGR.Items[i].Cells[1].Text;
                txt.Text = DGR.Items[i].Cells[3].Text;

                if (DGR.Items[i].Cells[4].Text == "1")
                    cbM.Checked = true;
                if (DGR.Items[i].Cells[5].Text == "1")
                    cbF.Checked = true;
                if (DGR.Items[i].Cells[6].Text == "1")
                    cbC.Checked = true;
                if (DGR.Items[i].Cells[7].Text == "1")
                    cbD.Checked = true;
            }
        }

        protected void FillSubTC()
        {
            if (DGR.Items.Count > 0)
                TDSUB.Visible = true;
            else
                return;

            conn.QueryString = "select " +
                                "CODE, " +
                                "DESCR, " +
                                "NOTE, " +
                                "FACTOR = FACTOR*100 " +
                                "from PARAM_TC_FORMULA_DETAIL " +
                                "where " +
                                "TC_CODE='" +LB_TCCODE.Text+ "' " +
                                "and BENEFIT_ID='" +DDL_BENEFIT.SelectedValue+ "' " +
                                "order by CODE";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SUBTC.DataSource = dt;
            DGR_SUBTC.DataBind();

            for (int i = 0; i < DGR_SUBTC.Items.Count; i++)
            {
                TextBox txtDescr= (TextBox)DGR_SUBTC.Items[i].FindControl("TXT_DESCR");
                TextBox txtNote = (TextBox)DGR_SUBTC.Items[i].FindControl("TXT_NOTE");
                TextBox txtFactor = (TextBox)DGR_SUBTC.Items[i].FindControl("TXT_FACTOR");
                DataGrid dgr = (DataGrid)DGR_SUBTC.Items[i].FindControl("DGR_BENVAR2");

                txtDescr.Text = DGR_SUBTC.Items[i].Cells[1].Text.Replace("&nbsp;","");
                txtFactor.Text = DGR_SUBTC.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtNote.Text = DGR_SUBTC.Items[i].Cells[3].Text.Replace("&nbsp;", "");

                dgr.Visible = false;
                conn.QueryString = "select distinct " +
                                    "b.CODE,  " +
                                    "b.DESCR, " +
                                    "a.TAKEN " +
                                    "from PARAM_TC_FORMULA_BENEFIT_VARIAN a  " +
                                    "inner join PARAM_BENEFIT_DETAIL b on a.BENEFIT_DETAIL_ID=b.CODE  " +
                                    "where  " +
                                    "a.BENEFIT_ID = '" +DDL_BENEFIT.SelectedValue+ "'  " +
                                    "and a.TC_CODE = " +LB_TCCODE.Text+ " " +
                                    "and a.CODE=" +DGR_SUBTC.Items[i].Cells[0].Text;
                conn.ExecuteQuery();
                
                if (conn.GetRowCount() > 0)
                {
                    dgr.Visible = true;

                    dt = new DataTable();
                    dt = conn.GetDataTable().Copy();
                    dgr.DataSource = dt;
                    dgr.DataBind();

                    for (int j = 0; j < dgr.Items.Count; j++)
                    {
                        CheckBox cb= (CheckBox)dgr.Items[j].FindControl("CB_BENVAR");
                        if (dgr.Items[j].Cells[2].Text == "1")
                            cb.Checked = true;
                    }
                }
            }

            
        }

        protected void FillBenefitVarian()
        {
            conn.QueryString = "select " +
                                "CODE, " +
                                "DESCR " +
                                "from PARAM_BENEFIT_DETAIL " +
                                "where " +
                                "BENEFIT_ID='" + DDL_BENEFIT.SelectedValue + "' " +
                                "and CODE not in " +
                                "(select BENEFIT_DETAIL_ID from PARAM_TC_FORMULA_BENEFIT_VARIAN  " +
                                "where " +
                                "BENEFIT_ID='" + DDL_BENEFIT.SelectedValue + "' " +
                                "and TC_CODE=" + LB_TCCODE.Text + ") " +
                                "order by 1";
            conn.ExecuteQuery();
            DDL_BENEFITDETAIL.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BENEFITDETAIL.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString() + " - " + conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.DESCR " +
                                "from PARAM_TC_FORMULA_BENEFIT_VARIAN a " +
                                "inner join PARAM_BENEFIT_DETAIL b on a.BENEFIT_DETAIL_ID=b.CODE " +
                                "where " +
                                "a.BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                "and a.TC_CODE = " + LB_TCCODE.Text + " " +
                                "order by 1";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENVAR.DataSource = dt;
            DGR_BENVAR.DataBind();
        }

        protected void DGR_SUBTC_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            TextBox txtDescr= (TextBox)e.Item.FindControl("TXT_DESCR");
            TextBox txtNote = (TextBox)e.Item.FindControl("TXT_NOTE");
            TextBox txtFactor = (TextBox)e.Item.FindControl("TXT_FACTOR");

            if (e.CommandName == "New")
            {
                try
                {
                    conn.QueryString = "exec SP_PARAM_TC_FORMULA_DETAIL_INSERT " +
                                        "'" + DDL_BENEFIT.SelectedValue + "'," +
                                        "'" + LB_TCCODE.Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }

                FillSubTC();
                FillBenefitVarian();
            }

            if (e.CommandName == "Save")
            {
                try
                {
                    conn.QueryString = "update PARAM_TC_FORMULA_DETAIL set " +
                                        "DESCR = '" + txtDescr.Text.Trim() + "'," +
                                        "NOTE = '" + txtNote.Text.Trim() + "'," +
                                        "FACTOR = convert(float," + txtFactor.Text.Trim().Replace(",", "") + ")/100.00 , " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                        "and TC_CODE = '" + LB_TCCODE.Text + "' " +
                                        "and CODE=" + e.Item.Cells[0].Text;
                                        
                    conn.ExecuteNonQuery();
                }
                catch { }

                SaveTCBenefitVarian(e);
                FillSubTC();
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_TC_FORMULA_BENEFIT_VARIAN where " +
                                        "BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                        "and TC_CODE = '" + LB_TCCODE.Text + "' " +
                                        "and CODE = " + e.Item.Cells[0].Text + " " +
                                        "delete from PARAM_TC_FORMULA_DETAIL where " +
                                        "BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                        "and TC_CODE = '" + LB_TCCODE.Text + "' " +
                                        "and CODE = " + e.Item.Cells[0].Text;
                    conn.ExecuteNonQuery();
                }
                catch { }

                FillSubTC();
                FillBenefitVarian();
            }
        }

        protected void SaveTCBenefitVarian(System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            DataGrid dgr = (DataGrid)e.Item.FindControl("DGR_BENVAR2");
            for (int i = 0; i < dgr.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)dgr.Items[i].FindControl("CB_BENVAR");
                string taken = "0";
                if (cb.Checked)
                    taken = "1";

                try
                {
                    conn.QueryString = "update PARAM_TC_FORMULA_BENEFIT_VARIAN set " +
                                        "TAKEN = " + taken + ", " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                        "and TC_CODE = '" + LB_TCCODE.Text + "' " +
                                        "and CODE = '" + e.Item.Cells[0].Text + "' " +
                                        "and BENEFIT_DETAIL_ID = '" + dgr.Items[i].Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }

            FillSubTC();
        }

        protected void DGR_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                LB_TCCODE.Text = e.Item.Cells[1].Text;
                LB_TCDESCR.Text = e.Item.Cells[3].Text.ToUpper();

                FillSubTC();
                FillBenefitVarian();
            }

            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtDescr = (TextBox)DGR.Items[i].FindControl("TXT_TCDESCR");
                    CheckBox cbM = (CheckBox)DGR.Items[i].FindControl("CB_MALE");
                    CheckBox cbF = (CheckBox)DGR.Items[i].FindControl("CB_FEMALE");
                    CheckBox cbC = (CheckBox)DGR.Items[i].FindControl("CB_CHILD");
                    CheckBox cbD = (CheckBox)DGR.Items[i].FindControl("CB_CHILD_D");

                    conn.QueryString = "update PARAM_TC_FORMULA set " +
                                        "DESCR = '" + txtDescr.Text.Trim().Replace("'", "`") + "', " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where " +
                                        "BENEFIT_ID = '" + DGR.Items[i].Cells[0].Text + "' " +
                                        "and CODE = '" + DGR.Items[i].Cells[1].Text + "'";
                           
                    conn.ExecuteNonQuery();

                    string M = "0";
                    string F = "0";
                    string C = "0";
                    string D = "0";
                    if (cbM.Checked)
                        M = "1";
                    if (cbF.Checked)
                        F = "1";
                    if (cbC.Checked)
                        C = "1";
                    if (cbD.Checked)
                        D = "1";

                    conn.QueryString = "exec SP_PARAM_TC_FORMULA_BENEFIT_INSERT " +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        "'" + DGR.Items[i].Cells[1].Text + "'," +
                                        M + "," +
                                        F + "," +
                                        C + "," +
                                        D + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                
                FillTC();
            }
        }

        protected void BT_INSERT_Click(object sender, EventArgs e)
        {
            if (TXT_TC.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "insert into PARAM_TC_FORMULA select " +
                                    "'" + DDL_BENEFIT.SelectedValue + "'," +
                                    "'" + TXT_CODE.Text.Trim() + "'," +
                                    "'" + TXT_TC.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE(), " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()";
                conn.ExecuteNonQuery();

                FillTC();
                FillSubTC();
            }
            catch { }
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTC();
        }

        protected void BT_ADDBENEFITDETAIL_Click(object sender, EventArgs e)
        {
            conn.QueryString = "insert into PARAM_TC_FORMULA_BENEFIT_VARIAN select distinct " +
                                "BENEFIT_ID," +
                                "TC_CODE," +
                                "CODE, " +
                                "'" + DDL_BENEFITDETAIL.SelectedValue + "'," +
                                "0," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE(), " +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE() " +
                                "from PARAM_TC_FORMULA_DETAIL " +
                                "where " +
                                "BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                "and TC_CODE = '" + LB_TCCODE.Text + "'";
            conn.ExecuteNonQuery();
            FillSubTC();
            FillBenefitVarian();
        }

        protected void DGR_BENVAR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PARAM_TC_FORMULA_BENEFIT_VARIAN where " +
                                    "BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                    "and TC_CODE = '" + LB_TCCODE.Text + "' " +
                                    "and BENEFIT_DETAIL_ID = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillSubTC();
                FillBenefitVarian();
            }
        }


    }
}