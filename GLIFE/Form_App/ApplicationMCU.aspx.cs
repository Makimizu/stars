using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class ApplicationMCU : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                Setup();
                LoadSummary();
                LoadDocument();
                LoadQuestion();
                LoadDGRMedicalLab();
                LoadDGRRequestedItems();
            }
        }

        protected void Setup()
        {
            LB_REGNO.Text = Request.QueryString["ID"].ToString();

            conn.QueryString = "select " +
                                "REGNO, " +
                                "FULLNAME, " +
                                "DOB = convert(varchar(20),DOB,106), " +
                                "START_DATE = convert(varchar(20),START_DATE,106), " +
                                "SEX = (case when SEX='M' then 'MALE' else 'FEMALE' end), " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "TC_DESCR, " +
                                "UW_CODE, " +
                                "SUMINS = replace(convert(varchar(100), convert(money,isnull(SUMINS,0)),1), '.00','')  " +
                                "from V_APPLICATION_MASTER " +
                                "where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_DOB.Text = conn.GetFieldValue("DOB").ToString();
            LB_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
            LB_GENDER.Text = conn.GetFieldValue("SEX").ToString();
            LB_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("TC_DESCR").ToString();
            LB_UWCODE.Text = conn.GetFieldValue("UW_CODE").ToString();
            LB_SUMINS.Text = conn.GetFieldValue("SUMINS").ToString();


            conn.QueryString = "select distinct " +
                                "b.COMPANY_CODE, " +
                                "COMPANY_NAME = UPPER(b.COMPANY_NAME) " +
                                "from V_LINK_UB_PARAM_MEDICAL_LAB_ITEMS a " +
                                "inner join V_LINK_UB_PARAM_MEDICAL_LAB b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MEDLAB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLYear();

            conn.QueryString = "select SEQ = 0, FULLNAME from V_APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "' " +
                                "union all " +
                                "select SEQ, FULLNAME from APPLICATION_JOIN_ACCOUNT where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_QUESTION.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (DDL_QUESTION.Items.Count > 1)
            {
                TBL_QUESTION.Visible = true;
            }
        }

        protected void LoadSummary()
        {
            conn.QueryString = "select " +
                                "REGNO, " +
                                "LAB = count(distinct COMPANY_CODE), " +
                                "ITEM = count(DOC_CODE), " +
                                "AMOUNT = replace(convert(varchar(100), convert(money,SUM(PRICE)),1),'.00','') " +
                                "from V_APPLICATION_MEDICAL_LAB_ITEMS " +
                                "where " +
                                "REGNO = '" + LB_REGNO.Text + "' " +
                                "group by REGNO";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                LB_TOTALLAB.Text = conn.GetFieldValue("LAB").ToString();
                LB_TOTALITEM.Text = conn.GetFieldValue("ITEM").ToString();
                LB_TOTALCHARGE.Text = conn.GetFieldValue("AMOUNT").ToString();
            }
        }

        protected void LoadQuestion()
        {
            conn.QueryString = "exec SP_APPLICATION_TC_QUESTIONS '" + LB_REGNO.Text + "'," + DDL_QUESTION.SelectedValue;
            conn.ExecuteQuery();
            DGR_QUESTION.DataSource = conn.GetDataTable().Copy();
            DGR_QUESTION.DataBind();

            for (int j = 0; j < DGR_QUESTION.Items.Count; j++)
            {
                Label lbDESCR = (Label)DGR_QUESTION.Items[j].FindControl("LB_QUESTDESCR");
                DropDownList ddl = (DropDownList)DGR_QUESTION.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR_QUESTION.Items[j].FindControl("TXT_VAL");
                Label lbQUESTION = (Label)DGR_QUESTION.Items[j].FindControl("LB_QUESTION");
                TextBox txtNEXTVAL = (TextBox)DGR_QUESTION.Items[j].FindControl("TXT_NEXTVAL");
                Label lbUNCHECKED = (Label)DGR_QUESTION.Items[j].FindControl("LB_UNCHECKED");
                System.Web.UI.HtmlControls.HtmlTableRow trQUESTION = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_QUESTION.Items[j].FindControl("TR_NEXTQUESTION");

                lbDESCR.Text = DGR_QUESTION.Items[j].Cells[6].Text.Replace("&nbsp;", "");

                if (DGR_QUESTION.Items[j].Cells[3].Text.Replace("&nbsp;", "") != "")
                {
                    trQUESTION.Visible = true;
                    lbQUESTION.Visible = true;
                    txtNEXTVAL.Visible = true;
                    lbQUESTION.Text = DGR_QUESTION.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                    txtNEXTVAL.Text = DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                }

                if (DGR_QUESTION.Items[j].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    ddl.Visible = true;
                    conn.QueryString = DGR_QUESTION.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    for (int k = 0; k < conn.GetRowCount(); k++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                    try
                    {
                        ddl.SelectedValue = DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
                else
                {
                    txtVAL.Visible = true;
                    switch (DGR_QUESTION.Items[j].Cells[1].Text)
                    {
                        case "STR": txtVAL.Text = DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                            break;
                        case "INT": txtVAL.Text = DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                            txtVAL.Style["text-align"] = "center";
                            txtVAL.Width = 50;
                            break;
                        case "FLO": try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                txtVAL.Style["text-align"] = "right";
                            }
                            catch { }
                            break;
                        case "BIT": txtVAL.Visible = false;
                            ddl.Visible = true;
                            ddl.Items.Add(new ListItem("YES", "1"));
                            ddl.Items.Add(new ListItem("NO", "0"));
                            try
                            {
                                ddl.SelectedValue = DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                            break;

                    }
                }

                bool bCompleted = true;
                if (DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "") == "")
                    bCompleted = false;

                if (!bCompleted)
                {
                    lbUNCHECKED.Visible = true;
                    DGR_QUESTION.Items[j].Cells[DGR_QUESTION.Columns.Count - 1].BackColor = System.Drawing.Color.Pink;
                }


                ddl.Enabled = false;
                txtVAL.Enabled = false;
                txtNEXTVAL.Enabled = false;
            }
        }

        protected void LoadDocument()
        {
            conn.QueryString = "select " +
                                "a.DOCTYPE, " +
                                "aa.DESCR, " +
                                "RECEIVE_DATE = b.CREATEDATE, " +
                                "b.CODE, " +
                                "b.NAMAFILE " +
                                "from APPLICATION_MEDICAL_DOCUMENT a " +
                                "inner join V_LINK_UB_PR_UW_REQUIRED_DOCUMENT aa on a.DOCTYPE = aa.CODE " +
                                "left join V_LINK_ARCHIEVE b on a.REGNO = b.OWNER1 and a.DOCTYPE = b.OWNER2 " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' order by 1";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DOCS.DataSource = dt;
            DGR_DOCS.DataBind();

            for (int i = 0; i < DGR_DOCS.Items.Count; i++)
            {
                Label lbDESCR = (Label)DGR_DOCS.Items[i].FindControl("LB_DESCR");
                LinkButton lbtDESCR = (LinkButton)DGR_DOCS.Items[i].FindControl("LBT_DESCR");

                if (DGR_DOCS.Items[i].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    lbtDESCR.Visible = true;
                    lbtDESCR.Text = DGR_DOCS.Items[i].Cells[1].Text + "<BR>" +
                                    "<table style='border-spacing:0px;width:100%;'>" +
                                    "<tr><td>Filename<td>:</td></td><td>" + DGR_DOCS.Items[i].Cells[3].Text + "</td></tr>" +
                                    "<tr><td>Upload date</td><td>:</td><td>" + DGR_DOCS.Items[i].Cells[4].Text + "</td></tr>" +
                                    "</table>";
                }
                else
                {
                    lbDESCR.Visible = true;
                    lbDESCR.Text = DGR_DOCS.Items[i].Cells[1].Text;
                }
            }
        }

        protected void FillDDLYear()
        {
            conn.QueryString = "select distinct YEAR " +
                                "from V_LINK_UB_PARAM_MEDICAL_LAB_ITEMS " +
                                "where COMPANY_CODE = '" + DDL_MEDLAB.SelectedValue + "'";
            conn.ExecuteQuery();

            DDL_MEDLABYEAR.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MEDLABYEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_MEDLABYEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDGRMedicalLab();
        }

        protected void DDL_MEDLAB_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLYear();
            LoadDGRMedicalLab();
        }

        protected void LoadDGRMedicalLab()
        {
            conn.QueryString = "exec SP_APPLICATION_MEDICAL_LAB_ITEMS_REMAINS " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + DDL_MEDLAB.SelectedValue + "'," +
                                DDL_MEDLABYEAR.SelectedValue;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MEDLAB.DataSource = dt;
            DGR_MEDLAB.DataBind();

            for (int i = 0; i < DGR_MEDLAB.Items.Count; i++)
            {
                LinkButton lbtDESCR = (LinkButton)DGR_MEDLAB.Items[i].FindControl("LBT_DESCR");
                lbtDESCR.Text = DGR_MEDLAB.Items[i].Cells[1].Text;
            }
        }

        protected void LoadDGRRequestedItems()
        {
            conn.QueryString = "select " +
                                "b.COMPANY_CODE, " +
                                "COMPANY_NAME = UPPER(b.COMPANY_NAME), " +
                                "AMOUNT = replace(convert(varchar(100), convert(money,SUM(a.PRICE)),1),'.00','') " +
                                "from (	select REGNO, COMPANY_CODE, PRICE from APPLICATION_MEDICAL_LAB_ITEMS union all " +
                                "        select REGNO, COMPANY_CODE, PRICE from APPLICATION_MEDICAL_LAB_PACKAGE " +
                                "        ) a " +
                                "inner join V_LINK_UB_PARAM_MEDICAL_LAB b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "' " +
                                "group by " +
                                "b.COMPANY_CODE, " +
                                "b.COMPANY_NAME";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_SELECTED.DataSource = dt;
            DGR_SELECTED.DataBind();

            for (int i = 0; i < DGR_SELECTED.Items.Count; i++)
            {
                Label lbLABNAME = (Label)DGR_SELECTED.Items[i].FindControl("LB_LABNAME");
                Label lbLABCODE = (Label)DGR_SELECTED.Items[i].FindControl("LB_LABCODE");
                Label lbLABAMOUNT = (Label)DGR_SELECTED.Items[i].FindControl("LB_LABAMOUNT");
                DataGrid dgr = (DataGrid)DGR_SELECTED.Items[i].FindControl("DGR_APPMED");

                lbLABNAME.Text = DGR_SELECTED.Items[i].Cells[1].Text;
                lbLABCODE.Text = DGR_SELECTED.Items[i].Cells[0].Text;
                lbLABAMOUNT.Text = DGR_SELECTED.Items[i].Cells[2].Text;

                conn.QueryString = "select " +
                                    "TYPE, " +
                                    "COMPANY_CODE, " +
                                    "DOC_CODE, " +
                                    "DESCR, " +
                                    "PRICE = replace(convert(varchar(100), convert(money,PRICE),1),'.00','') " +
                                    "from V_APPLICATION_MEDICAL_LAB_ITEMS " +
                                    "where " +
                                    "REGNO = '" + LB_REGNO.Text + "' " +
                                    "and COMPANY_CODE = '" + lbLABCODE.Text + "' " +
                                    "order by TYPE desc, DESCR";
                conn.ExecuteQuery();
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                dgr.DataSource = dt;
                dgr.DataBind();

                for (int j = 0; j < dgr.Items.Count; j++)
                {
                    TextBox txtPRICE = (TextBox)dgr.Items[j].FindControl("TXT_PRICE");
                    txtPRICE.Text = dgr.Items[j].Cells[2].Text;
                }
            }
        }

        protected void DGR_MEDLAB_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_MEDICAL_LAB_ITEMS_INSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + DDL_MEDLAB.SelectedValue + "'," +
                                        "'" + e.Item.Cells[3].Text + "'," +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + e.Item.Cells[4].Text.Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' " +
                                        "exec SP_APPLICATION_DOCUMENT_LETTER_INSERT '" + LB_REGNO.Text + "','009'";
                    conn.ExecuteNonQuery();

                    LoadSummary();
                    LoadDGRMedicalLab();
                    LoadDGRRequestedItems();
                }
                catch { }
            }
        }

        protected void DGR_APPMED_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    string tablename = "APPLICATION_MEDICAL_LAB_ITEMS";
                    string fieldname = "DOC_CODE";
                    if (e.Item.Cells[3].Text == "PACKAGE")
                    {
                        tablename = "APPLICATION_MEDICAL_LAB_PACKAGE";
                        fieldname = "SEQ";
                    }

                    conn.QueryString = "delete from " + tablename + " " +
                                        "where " +
                                        "REGNO = '" + LB_REGNO.Text + "' " +
                                        "and COMPANY_CODE = '" + e.Item.Cells[1].Text + "' " +
                                        "and " + fieldname + " = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }

                LoadSummary();
                LoadDGRMedicalLab();
                LoadDGRRequestedItems();
            }

            if (e.CommandName == "Save")
            {
                try
                {
                    string tablename = "APPLICATION_MEDICAL_LAB_ITEMS";
                    string fieldname = "DOC_CODE";
                    if (e.Item.Cells[3].Text == "PACKAGE")
                    {
                        tablename = "APPLICATION_MEDICAL_LAB_PACKAGE";
                        fieldname = "SEQ";
                    }

                    TextBox txtPRICE = (TextBox)e.Item.FindControl("TXT_PRICE");

                    conn.QueryString = "update " + tablename + " set " +
                                        "PRICE = " + txtPRICE.Text.Trim().Replace(",", "") + " " +
                                        "where " +
                                        "REGNO = '" + LB_REGNO.Text + "' " +
                                        "and COMPANY_CODE = '" + e.Item.Cells[1].Text + "' " +
                                        "and " + fieldname + " = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }

                LoadSummary();
                LoadDGRRequestedItems();
            }
        }

        protected void DDL_QUESTION_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadQuestion();
        }
    }
}