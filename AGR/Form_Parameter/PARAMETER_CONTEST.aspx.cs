using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Threading.Tasks;

namespace AGR.Form_Parameter
{
    public partial class PARAMETER_CONTEST : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                               "ID, " +
                               "DESCR = convert(varchar(20), a.START_DATE, 112) + ' - ' + convert(varchar(20), a.END_DATE, 112) + ' : ' + a.DESCR " +
                               "from CONTEST_MASTER a " +
                               "order by " +
                               "a.START_DATE";
            conn.ExecuteQuery();
            DDL_MASTER.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MASTER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE='', DESCR='' union all select CODE, DESCR from UWBOX.dbo.PARAM_PRODUCT_GROUP order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_GROUP_UNSELECTED.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_GROUP_SELECTED.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE='', DESCR='-- ALL AREA --' union all select CODE, DESCR from PR_AREA order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_AREA_0.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_AREA_1.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }


        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_CONTEST_MASTER";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btPROCESS = (Button)DGR.Items[i].FindControl("BT_PROCESS");
                Button btPRODUCT = (Button)DGR.Items[i].FindControl("BT_PRODUCT");
                Button btX = (Button)DGR.Items[i].FindControl("BT_DELETE");

                btPROCESS.Attributes.Add("onclick", "if(!confirm('Are you sure to PROCESS ?')){return false;}else{ShowProgress();}");
                btX.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");

                btPRODUCT.Text = "PRODUCT " + DGR.Items[i].Cells[4].Text;

                if (DGR.Items[i].Cells[1].Text == "0")
                {
                    btPROCESS.Enabled = false;
                    btPROCESS.BackColor = System.Drawing.Color.Gray;
                }
            }
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            TR_LIST.Visible = false;
            TR_NEW.Visible = true;
        }

        protected void BT_CANCEL_Click(object sender, EventArgs e)
        {
            TR_LIST.Visible = true;
            TR_NEW.Visible = false;
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                TR_LIST.Visible = false;
                TR_DETAIL.Visible = true;
                LB_ID.Text = e.Item.Cells[0].Text;
                LB_DESCR.Text = e.Item.Cells[2].Text;
                LB_PERIOD.Text = e.Item.Cells[5].Text;

                TR_AGENCY.Visible = false;
                TR_PRODUCT.Visible = false;
                TR_GENERAL.Visible = false;
                TR_PARAMETER.Visible = true;

                FillDGRMarketSegment();
                if (DGR_CD.Items.Count == 0)
                    return;

                DV_PARAMETER.Visible = false;
                //LoadParameter(DGR_CD.Items[0].Cells[0].Text, DGR_CD.Items[0].Cells[1].Text);
            }

            if (e.CommandName == "Process")
            {
                //string SQL  = "exec SP_CONTEST_PARAMETER_PROCESS " +
                //                    "'" + e.Item.Cells[0].Text + "'," +
                //                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                //Task.Run(() => SQL_Thread(SQL));
                //System.Threading.Thread.Sleep(5000);

                conn.QueryString = "exec SP_CONTEST_PARAMETER_PROCESS " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery(500000);

                FillDGR();
                return;
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from CONTEST_MASTER where ID = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
                return;
            }

            if (e.CommandName == "Agency")
            {
                TR_LIST.Visible = false;
                TR_DETAIL.Visible = true;
                LB_ID.Text = e.Item.Cells[0].Text;
                LB_DESCR.Text = e.Item.Cells[2].Text;
                LB_PERIOD.Text = e.Item.Cells[5].Text;

                TR_AGENCY.Visible = true;
                TR_PRODUCT.Visible = false;
                TR_GENERAL.Visible = false;
                TR_PARAMETER.Visible = false;

                FillDGRAgency();
                return;
            }

            if (e.CommandName == "Product")
            {
                TR_LIST.Visible = false;
                TR_DETAIL.Visible = true;
                LB_ID.Text = e.Item.Cells[0].Text;
                LB_DESCR.Text = e.Item.Cells[2].Text;
                LB_PERIOD.Text = e.Item.Cells[5].Text;

                TR_AGENCY.Visible = false;
                TR_PRODUCT.Visible = true;
                TR_GENERAL.Visible = false;
                TR_PARAMETER.Visible = false;

                FillDGRProduct();
                return;
            }

            if (e.CommandName == "General")
            {
                TR_LIST.Visible = false;
                TR_DETAIL.Visible = true;
                LB_ID.Text = e.Item.Cells[0].Text;
                LB_DESCR.Text = e.Item.Cells[2].Text;
                LB_PERIOD.Text = e.Item.Cells[5].Text;


                TR_AGENCY.Visible = false;
                TR_PRODUCT.Visible = false;
                TR_GENERAL.Visible = true;
                TR_PARAMETER.Visible = false;

                FillParamGeneral();
                return;
            }

            if (e.CommandName == "Report")
            {
                Response.Redirect(e.Item.Cells[6].Text);
            }
        }

        protected void BT_CREATE_Click(object sender, EventArgs e)
        {
            if (TXT_DESCR.Text.Trim() == "" || TXT_PERIOD_START.Text.Trim() == "" || TXT_PERIOD_END.Text.Trim() == "")
                return;

            string COPY_FROM_ID = "null";
            if (DDL_MASTER.SelectedValue != "")
                COPY_FROM_ID = "'" + DDL_MASTER.SelectedValue + "'";

            conn.QueryString = "exec SP_CONTEST_MASTER_UPSERT " +
                                "null," +
                                COPY_FROM_ID + "," +
                                "'" + TXT_DESCR.Text.Trim() + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_PERIOD_START.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_PERIOD_END.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "s") + "'";
            conn.ExecuteNonQuery();


            FillDGR();
            TR_LIST.Visible = true;
            TR_NEW.Visible = false;
        }

        protected void BT_DETAIL_BACK_Click(object sender, EventArgs e)
        {
            TR_LIST.Visible = true;
            TR_DETAIL.Visible = false;
        }

        protected void FillDGRAgency()
        {
            LB_AGENCY_CNT_0.Text = "";
            LB_AGENCY_CNT_1.Text = "";
            DataTable dt;

            conn.QueryString = "SP_CONTEST_PARAMETER_AGENCY '" + LB_ID.Text + "','" + LB_AGENT_LEVEL.Text + "',0,'" + TXT_AGENCY_0.Text.Trim() + "','" + DDL_AREA_0.SelectedValue + "'";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AGENCY_0.DataSource = dt;
            DGR_AGENCY_0.DataBind();
            LB_AGENCY_CNT_0.Text = conn.GetRowCount().ToString() + " Records";

            conn.QueryString = "SP_CONTEST_PARAMETER_AGENCY '" + LB_ID.Text + "','" + LB_AGENT_LEVEL.Text + "',1,'" + TXT_AGENCY_1.Text.Trim() + "','" + DDL_AREA_1.SelectedValue + "'";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AGENCY_1.DataSource = dt;
            DGR_AGENCY_1.DataBind();
            LB_AGENCY_CNT_1.Text = conn.GetRowCount().ToString() + " Records";
        }

        protected void FillDGRProduct()
        {
            LB_PRODUCT_CNT_0.Text = "";
            LB_PRODUCT_CNT_1.Text = "";
            DataTable dt;

            conn.QueryString = "SP_CONTEST_PARAMETER_PRODUCT '" + LB_ID.Text + "',0,'" + TXT_PRODUCT_0.Text.Trim() + "','" + DDL_GROUP_UNSELECTED.SelectedValue + "'," + DDL_STAT_UNSELECTED.SelectedValue;
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PRODUCT_0.DataSource = dt;
            DGR_PRODUCT_0.DataBind();
            LB_PRODUCT_CNT_0.Text = conn.GetRowCount() + " Records";

            conn.QueryString = "SP_CONTEST_PARAMETER_PRODUCT '" + LB_ID.Text + "',1,'" + TXT_PRODUCT_1.Text.Trim() + "','" + DDL_GROUP_SELECTED.SelectedValue + "'," + DDL_STAT_SELECTED.SelectedValue;
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PRODUCT_1.DataSource = dt;
            DGR_PRODUCT_1.DataBind();
            LB_PRODUCT_CNT_1.Text = conn.GetRowCount() + " Records";
            for (int i = 0; i < DGR_PRODUCT_1.Items.Count; i++)
            {
                TextBox txtPCT = (TextBox)DGR_PRODUCT_1.Items[i].FindControl("TXT_PCT");
                TextBox txtPREMIUM = (TextBox)DGR_PRODUCT_1.Items[i].FindControl("TXT_PREMIUM");
                txtPCT.Text = DGR_PRODUCT_1.Items[i].Cells[2].Text;
                txtPREMIUM.Text = DGR_PRODUCT_1.Items[i].Cells[3].Text;
            }
        }

        protected void DGR_AGENCY_0_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "SelectAll")
            {
                for (int i = 0; i < DGR_AGENCY_0.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_AGENCY_0.Items[i].FindControl("CB_UNSELECTED");
                    if (cb.Checked)
                    {
                        conn.QueryString = "insert into CONTEST_AGENCY select '" + LB_ID.Text + "','" + LB_AGENT_LEVEL.Text + "','" + DGR_AGENCY_0.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                }

                FillDGRAgency();
                return;
            }
        }

        protected void DGR_AGENCY_1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAll")
            {
                for (int i = 0; i < DGR_AGENCY_1.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_AGENCY_1.Items[i].FindControl("CB_SELECTED");
                    if (cb.Checked)
                    {
                        conn.QueryString = "delete from CONTEST_AGENCY where ID = '" + LB_ID.Text + "' and COMPANY_CODE = '" + DGR_AGENCY_1.Items[i].Cells[0].Text + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "'";
                        conn.ExecuteNonQuery();
                    }
                }

                FillDGRAgency();
                return;
            }
        }

        protected void DGR_PRODUCT_0_ItemCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandName == "SelectAll")
            {
                for (int i = 0; i < DGR_PRODUCT_0.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_PRODUCT_0.Items[i].FindControl("CB_UNSELECTED");
                    if (cb.Checked)
                    {
                        conn.QueryString = "exec SP_CONTEST_PARAMETER_PRODUCT_INSERT'" + LB_ID.Text + "','" + DGR_PRODUCT_0.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                }

                FillDGRProduct();
                return;
            }
        }

        protected void DGR_PRODUCT_1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from CONTEST_PRODUCT where ID = '" + LB_ID.Text + "' and PRODUCT_CODE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();

                FillDGRProduct();
                return;
            }

            if (e.CommandName == "Save")
            {
                TextBox txtPCT = (TextBox)e.Item.FindControl("TXT_PCT");
                TextBox txtPREMIUM = (TextBox)e.Item.FindControl("TXT_PREMIUM");
                try
                {
                    conn.QueryString = "update              CONTEST_PRODUCT set " +
                                        "PCT                = " + txtPCT.Text.Replace(",", "").Trim() + ", " +
                                        "PREMIUM_TARGET     = " + txtPREMIUM.Text.Replace(",", "").Trim() + " " +
                                        "where " +
                                        "ID                 = '" + LB_ID.Text + "' " +
                                        "and PRODUCT_CODE   = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillDGRProduct();
                return;
            }

            if (e.CommandName == "DeleteAll")
            {
                for (int i = 0; i < DGR_PRODUCT_1.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_PRODUCT_1.Items[i].FindControl("CB_SELECTED");
                    if (cb.Checked)
                    {
                        conn.QueryString = "delete from CONTEST_PRODUCT where ID = '" + LB_ID.Text + "' and PRODUCT_CODE = '" + DGR_PRODUCT_1.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                }

                FillDGRProduct();
                return;
            }
        }

        protected void TXT_AGENCY_0_TextChanged(object sender, EventArgs e)
        {
            FillDGRAgency();
        }

        protected void TXT_AGENCY_1_TextChanged(object sender, EventArgs e)
        {
            FillDGRAgency();
        }

        protected void DDL_AREA_0_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRAgency();
        }

        protected void DDL_AREA_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRAgency();
        }

        protected void TXT_PRODUCT_0_TextChanged(object sender, EventArgs e)
        {
            FillDGRProduct();
        }

        protected void TXT_PRODUCT_1_TextChanged(object sender, EventArgs e)
        {
            FillDGRProduct();
        }

        protected void DDL_GROUP_UNSELECTED_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRProduct();
        }

        protected void DDL_GROUP_SELECTED_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRProduct();
        }

        protected void CB_UNSELECTED_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PRODUCT_0.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PRODUCT_0.Items[i].FindControl("CB_UNSELECTED");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void CB_UNSELECTED_ALL_CheckedChanged1(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PRODUCT_1.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PRODUCT_1.Items[i].FindControl("CB_SELECTED");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DDL_STAT_UNSELECTED_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRProduct();
        }

        protected void DDL_STAT_SELECTED_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRProduct();
        }

        protected void CB_UNSELECTED_ALL_CheckedChanged2(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_AGENCY_0.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_AGENCY_0.Items[i].FindControl("CB_UNSELECTED");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void CB_UNSELECTED_ALL_CheckedChanged3(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_AGENCY_1.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_AGENCY_1.Items[i].FindControl("CB_SELECTED");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void FillParamGeneral()
        {
            conn.QueryString = "exec SP_CONTEST_PARAMETER_GENERAL '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_GENERAL.DataSource = dt;
            DGR_GENERAL.DataBind();

            for (int i = 0; i < DGR_GENERAL.Items.Count; i++)
            {
                if (DGR_GENERAL.Items[i].Cells[1].Text != "DATE" && DGR_GENERAL.Items[i].Cells[3].Text.Trim().Replace("&nbsp;", "") == "")
                {
                    TextBox txt = (TextBox)DGR_GENERAL.Items[i].FindControl("TXT_GENERAL");
                    txt.Visible = true;
                    txt.Text = DGR_GENERAL.Items[i].Cells[4].Text.Replace("&nbsp;", "");

                    switch (DGR_GENERAL.Items[i].Cells[1].Text)
                    {
                        case "STR": txt.Width = Unit.Percentage(99); break;
                        case "INT": txt.Width = Unit.Pixel(80); txt.Style.Add("text-align", "right"); break;
                        case "FLO": txt.Width = Unit.Pixel(80); txt.Style.Add("text-align", "right"); break;
                    }
                }


                if (DGR_GENERAL.Items[i].Cells[1].Text == "DATE" && DGR_GENERAL.Items[i].Cells[3].Text.Trim().Replace("&nbsp;", "") == "")
                {
                    TextBox txtDATE = (TextBox)DGR_GENERAL.Items[i].FindControl("TXT_GENERAL_DATE");
                    txtDATE.Visible = true;
                    txtDATE.Text = DGR_GENERAL.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                }

                if (DGR_GENERAL.Items[i].Cells[2].Text == "1" && DGR_GENERAL.Items[i].Cells[3].Text.Trim().Replace("&nbsp;", "") != "")
                {
                    DropDownList ddlGENERAL = (DropDownList)DGR_GENERAL.Items[i].FindControl("DDL_GENERAL_VALUE");
                    ddlGENERAL.Visible = true;

                    string VAL = "";
                    conn.QueryString = DGR_GENERAL.Items[i].Cells[3].Text;
                    conn.ExecuteQuery();
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        ddlGENERAL.Items.Add(new ListItem(conn.GetFieldValue(j, "DESCR").ToString(), conn.GetFieldValue(j, "CODE").ToString()));
                        if (conn.GetFieldValue(j, "TAKEN").ToString() == "1")
                        {
                            VAL = conn.GetFieldValue(j, "CODE").ToString();
                        }
                    }

                    try
                    {
                        ddlGENERAL.SelectedValue = VAL;
                    }
                    catch { }
                }

                if ((DGR_GENERAL.Items[i].Cells[2].Text == "0" || DGR_GENERAL.Items[i].Cells[2].Text == "2") && DGR_GENERAL.Items[i].Cells[3].Text.Trim().Replace("&nbsp;", "") != "")
                {

                    DataGrid dgr = (DataGrid)DGR_GENERAL.Items[i].FindControl("DGR_GENERAL_VALUE");
                    conn.QueryString = DGR_GENERAL.Items[i].Cells[3].Text;
                    conn.ExecuteQuery();
                    dt = new DataTable();
                    dt = conn.GetDataTable().Copy();
                    dgr.DataSource = dt;
                    dgr.DataBind();

                    if (DGR_GENERAL.Items[i].Cells[2].Text == "2")
                    {
                        dgr.Columns[4].Visible = true;
                    }

                    for (int j = 0; j < dgr.Items.Count; j++)
                    {
                        CheckBox cbGENERAL = (CheckBox)dgr.Items[j].FindControl("CB_GENERAL");
                        TextBox txtOTHVAL = (TextBox)dgr.Items[j].FindControl("TXT_OTHVAL");
                        if (dgr.Items[j].Cells[1].Text == "1")
                            cbGENERAL.Checked = true;

                        if (DGR_GENERAL.Items[i].Cells[2].Text == "2")
                        {
                            txtOTHVAL.Text = dgr.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                        }
                    }
                }
            }
        }

        protected void BT_SAVE_GENERAL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_GENERAL.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_GENERAL.Items[i].FindControl("TXT_GENERAL");
                TextBox txtDATE = (TextBox)DGR_GENERAL.Items[i].FindControl("TXT_GENERAL_DATE");
                DropDownList ddlGENERAL = (DropDownList)DGR_GENERAL.Items[i].FindControl("DDL_GENERAL_VALUE");
                DataGrid dgr = (DataGrid)DGR_GENERAL.Items[i].FindControl("DGR_GENERAL_VALUE");

                conn.QueryString = "delete from CONTEST_PARAMETER_GENERAL where " +
                                    "ID = '" + LB_ID.Text + "' " +
                                    "and FIELD_CODE = '" + DGR_GENERAL.Items[i].Cells[0].Text + "' ";
                conn.ExecuteNonQuery();

                if (txt.Visible)
                {
                    string VAL = txt.Text.Trim();

                    if (DGR_GENERAL.Items[i].Cells[1].Text == "INT" || DGR_GENERAL.Items[i].Cells[1].Text == "FLO")
                        VAL = VAL.Replace(",", "");

                    conn.QueryString = "insert into CONTEST_PARAMETER_GENERAL select " +
                                        "ID         = '" + LB_ID.Text + "'," +
                                        "FIELD_CODE = '" + DGR_GENERAL.Items[i].Cells[0].Text + "'," +
                                        "VAL        = '" + VAL + "'," +
                                        "VAL2       = null," +
                                        "USERBY     = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "USERDATE   = GETDATE()";
                    conn.ExecuteNonQuery();
                }


                if (txtDATE.Visible)
                {
                    conn.QueryString = "insert into CONTEST_PARAMETER_GENERAL select " +
                                        "ID         = '" + LB_ID.Text + "'," +
                                        "FIELD_CODE = '" + DGR_GENERAL.Items[i].Cells[0].Text + "'," +
                                        "VAL        = '" + txtDATE.Text.Trim() + "'," +
                                        "VAL2       = null," +
                                        "USERBY     = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "USERDATE   = GETDATE()";
                    conn.ExecuteNonQuery();

                }

                if (ddlGENERAL.Visible)
                {
                    conn.QueryString = "insert into CONTEST_PARAMETER_GENERAL select " +
                                        "ID         = '" + LB_ID.Text + "'," +
                                        "FIELD_CODE = '" + DGR_GENERAL.Items[i].Cells[0].Text + "'," +
                                        "VAL        = '" + ddlGENERAL.SelectedValue + "'," +
                                        "VAL2       = null," +
                                        "USERBY     = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "USERDATE   = GETDATE()";
                    conn.ExecuteNonQuery();
                }

                if (dgr.Visible && dgr.Items.Count > 0)
                {
                    for (int j = 0; j < dgr.Items.Count; j++)
                    {
                        CheckBox cbGENERAL = (CheckBox)dgr.Items[j].FindControl("CB_GENERAL");
                        TextBox txtOTHVAL = (TextBox)dgr.Items[j].FindControl("TXT_OTHVAL");

                        string VAL2 = "null";
                        if (DGR_GENERAL.Items[i].Cells[2].Text == "2")
                            VAL2 = "'" + txtOTHVAL.Text.Trim() + "'";

                        if (cbGENERAL.Checked)
                        {
                            conn.QueryString = "insert into CONTEST_PARAMETER_GENERAL select " +
                                                "ID         = '" + LB_ID.Text + "'," +
                                                "FIELD_CODE = '" + DGR_GENERAL.Items[i].Cells[0].Text + "'," +
                                                "VAL        = '" + dgr.Items[j].Cells[0].Text + "'," +
                                                "VAL2       = " + VAL2 + "," +
                                                "USERBY     = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                                "USERDATE   = GETDATE()";
                            conn.ExecuteNonQuery();
                        }
                    }
                }
            }

            FillParamGeneral();
        }

        protected void FillDGRMarketSegment()
        {
            DV_PARAMETER.Visible = false;

            conn.QueryString = "select " +
                                "SUB_CODE    = SUB_CODE,  " +
                                "DESCR       = UPPER(DESCR), " +
                                "STAT        = (case when b.CURRENT_AGENT_LEVEL is not null then 1 else 0 end) " +
                                "from        PARAM_SUB_CHANNEL_DISTRIBUTION a " +
                                "inner join  CONTEST_PARAMETER_GENERAL bb on a.MARKET_SEGMENT = bb.VAL collate database_default and bb.ID = '" + LB_ID.Text + "' and bb.FIELD_CODE = 'CONGR00' " +
                                "left join   (select CURRENT_AGENT_LEVEL from CONTEST_PARAMETER where ID = '" + LB_ID.Text + "') b on a.SUB_CODE = b.CURRENT_AGENT_LEVEL collate database_default " +
                                "order by " +
                                "a.SEQ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CD.DataSource = dt;
            DGR_CD.DataBind();
            for (int i = 0; i < DGR_CD.Items.Count; i++)
            {
                Button bt = (Button)DGR_CD.Items[i].FindControl("BT_CD");
                Button btX = (Button)DGR_CD.Items[i].FindControl("BT_CD_X");
                bt.Text = DGR_CD.Items[i].Cells[1].Text;

                if (DGR_CD.Items[i].Cells[2].Text == "0")
                {
                    bt.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    bt.ForeColor = System.Drawing.Color.Blue;
                    btX.Visible = true;
                    btX.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
                }
            }
        }

        protected void FillDGRReward()
        {
            BT_REWARD_DETAIL.Enabled = false;

            conn.QueryString = "exec SP_CONTEST_PARAMETER_REWARD '" + LB_ID.Text + "','" + LB_AGENT_LEVEL.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_REWARD.DataSource = dt;
            DGR_REWARD.DataBind();


            for (int i = 0; i < DGR_REWARD.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_REWARD.Items[i].FindControl("TXT_REWARD");
                txt.Text = DGR_REWARD.Items[i].Cells[1].Text.Replace("&nbsp;", "");
                if (DGR_REWARD.Items[i].Cells[2].Text == "1")
                {
                    txt.BackColor = System.Drawing.Color.Yellow;
                }
            }

            DDL_REWARD.Items.Clear();
            conn.QueryString = "select SEQ, REWARD from CONTEST_PARAMETER_REWARD where ID = '" + LB_ID.Text + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "' order by SEQ";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                BT_REWARD_DETAIL.Enabled = true;

                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    DDL_REWARD.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString() + " - " + conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                }
            }
        }


        protected void LoadParameter(string AGENT_LEVEL, string TITLE)
        {
            DV_PARAMETER.Visible = true;
            TBL_PARAMETER.Visible = true;
            LB_AGENT_LEVEL.Text = AGENT_LEVEL;
            LB_PARAMETER_TITLE.Text = TITLE;


            conn.QueryString = "exec SP_CONTEST_PARAMETER '" + LB_ID.Text + "','" + AGENT_LEVEL + "'";
            conn.ExecuteQuery();

            TXT_AUTH_DATE_FROM.Text = conn.GetFieldValue("AUTH_DATE_FROM").ToString();
            TXT_AUTH_DATE_TO.Text = conn.GetFieldValue("AUTH_DATE_TO").ToString();

            FillDGRReward();
            DV_REWARD.Visible = false;
        }

        protected void BT_SAVE_PARAMETER_Click(object sender, EventArgs e)
        {
            string AUTH_DATE_FROM = "null";
            string AUTH_DATE_TO = "null";

            if (TXT_AUTH_DATE_FROM.Text.Trim() != "")
                AUTH_DATE_FROM = "'" + GlobalUse.GlobalDateFormat(TXT_AUTH_DATE_FROM.Text.Trim(), "d/M/yyyy") + "'";
            if (TXT_AUTH_DATE_TO.Text.Trim() != "")
                AUTH_DATE_TO = "'" + GlobalUse.GlobalDateFormat(TXT_AUTH_DATE_TO.Text.Trim(), "d/M/yyyy") + "'";


            try
            {
                conn.QueryString = "exec SP_CONTEST_PARAMETER_UPSERT " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + LB_AGENT_LEVEL.Text + "'," +
                                    AUTH_DATE_FROM + "," +
                                    AUTH_DATE_TO + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch
            {
                return;
            }


            for (int i = 0; i < DGR_REWARD.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_REWARD.Items[i].FindControl("TXT_REWARD");
                if (txt.Text.Trim() != "")
                {
                    conn.QueryString = "exec SP_CONTEST_PARAMETER_REWARD_UPSERT " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + LB_AGENT_LEVEL.Text + "'," +
                                        "'" + DGR_REWARD.Items[i].Cells[0].Text + "'," +
                                        "'" + txt.Text.Trim().Replace("'", "`") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                else
                {
                    conn.QueryString = "delete from CONTEST_PARAMETER_REWARD where ID = '" + LB_ID.Text + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "' and SEQ = " + DGR_REWARD.Items[i].Cells[0].Text;
                    conn.ExecuteNonQuery();
                }
            }

            FillDGRReward();
            DV_REWARD.Visible = false;

            //for (int i = 0; i < DGR_ITEM.Items.Count; i++)
            //{
            //    TextBox txt = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VAL");
            //    DropDownList ddl = (DropDownList)DGR_ITEM.Items[i].FindControl("DDL_REFF");

            //    string val = txt.Text.Trim();
            //    if (ddl.Visible)
            //        val = ddl.SelectedValue;
            //    else
            //    {
            //        if (DGR_ITEM.Items[i].Cells[1].Text == "FLO" || DGR_ITEM.Items[i].Cells[1].Text == "INT")
            //        {
            //            val = val.Replace(",", "");
            //        }
            //    }

            //    conn.QueryString = "exec SP_CONTEST_PARAMETER_ITEM_UPSERT " +
            //                        "'" + LB_ID.Text + "'," +
            //                        "'" + LB_AGENT_LEVEL.Text + "'," +
            //                        "'" + DGR_ITEM.Items[i].Cells[0].Text + "'," +
            //                        "'" + val + "'," +
            //                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            //    conn.ExecuteNonQuery();
            //}


            //SaveDGRANP();
            //SaveDGRTeams();
            //SaveDGRTraining();

            FillDGRMarketSegment();
            LoadParameter(LB_AGENT_LEVEL.Text, LB_PARAMETER_TITLE.Text);
        }

        protected void DGR_CD_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Button btSELECTED = (Button)e.Item.FindControl("BT_CD");
                LoadParameter(e.Item.Cells[0].Text, e.Item.Cells[1].Text);

                for (int i = 0; i < DGR_CD.Items.Count; i++)
                {
                    Button bt = (Button)DGR_CD.Items[i].FindControl("BT_CD");
                    if (bt == btSELECTED)
                    {
                        bt.BackColor = System.Drawing.Color.Cyan;
                    }
                    else
                    {
                        bt.BackColor = System.Drawing.SystemColors.ButtonFace;
                    }
                }
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from CONTEST_PARAMETER where ID = '" + LB_ID.Text + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "'";
                conn.ExecuteNonQuery();
                FillDGRMarketSegment();
            }
        }

        protected void FillDGRANP()
        {
            conn.QueryString = "exec SP_CONTEST_PARAMETER_ANP_SOURCE " +
                                "'" + LB_ID.Text + "'," +
                                "'" + LB_AGENT_LEVEL.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ANP.DataSource = dt;
            DGR_ANP.DataBind();
            for (int i = 0; i < DGR_ANP.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_ANP.Items[i].FindControl("CB_ANP");
                if (DGR_ANP.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void FillDGRTrainings()
        {
            conn.QueryString = "exec SP_CONTEST_PARAMETER_TRAINING " +
                                "'" + LB_ID.Text + "'," +
                                "'" + LB_AGENT_LEVEL.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TRAINING.DataSource = dt;
            DGR_TRAINING.DataBind();
            for (int i = 0; i < DGR_TRAINING.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_TRAINING.Items[i].FindControl("CB_TRAINING");
                if (DGR_TRAINING.Items[i].Cells[1].Text == "1")
                    cb.Checked = true;
            }
        }

        protected void DGR_REWARD_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from CONTEST_PARAMETER_REWARD where ID = '" + LB_ID.Text + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "' and SEQ = " + e.Item.Cells[0].Text;
                conn.ExecuteNonQuery();

                FillDGRReward();
                DV_REWARD.Visible = false;
            }
        }

        protected void BT_REWARD_AGENCY_Click(object sender, EventArgs e)
        {
            DV_REWARD.Visible = true;
            LB_REWARD_TITLE.Text = ((Button)sender).Text;

            TR_REWARD_AGENCY.Visible = true;
            TR_REWARD_PROD_SOURCE.Visible = false;
            TR_REWARD_TRAINING.Visible = false;
            TR_REWARD_DETAIL.Visible = false;

            FillDGRAgency();
        }

        protected void BT_REWARD_PROD_SOURCE_Click(object sender, EventArgs e)
        {
            DV_REWARD.Visible = true;
            LB_REWARD_TITLE.Text = ((Button)sender).Text;

            TR_REWARD_AGENCY.Visible = false;
            TR_REWARD_PROD_SOURCE.Visible = true;
            TR_REWARD_TRAINING.Visible = false;
            TR_REWARD_DETAIL.Visible = false;

            FillDGRANP();
        }

        protected void BT_REWARD_TRAINING_Click(object sender, EventArgs e)
        {
            DV_REWARD.Visible = true;
            LB_REWARD_TITLE.Text = ((Button)sender).Text;

            TR_REWARD_AGENCY.Visible = false;
            TR_REWARD_PROD_SOURCE.Visible = false;
            TR_REWARD_TRAINING.Visible = true;
            TR_REWARD_DETAIL.Visible = false;

            FillDGRTrainings();
        }

        protected void BT_REWARD_DETAIL_Click(object sender, EventArgs e)
        {
            DV_REWARD.Visible = true;
            LB_REWARD_TITLE.Text = ((Button)sender).Text;

            TR_REWARD_AGENCY.Visible = false;
            TR_REWARD_PROD_SOURCE.Visible = false;
            TR_REWARD_TRAINING.Visible = false;
            TR_REWARD_DETAIL.Visible = true;

            LoadRewardCriteria();
        }

        protected void BT_REWARD_PROD_SOURCE_SAVE_Click(object sender, EventArgs e)
        {
            SaveDGRANP();
            FillDGRANP();
        }

        protected void BT_REWARD_TRAINING_SAVE_Click(object sender, EventArgs e)
        {
            SaveDGRTraining();
            FillDGRTrainings();
        }

        protected void BT_REWARD_DETAIL_SAVE_Click(object sender, EventArgs e)
        {
            SaveRewardDetail();
        }

        protected void SaveDGRANP()
        {
            conn.QueryString = "delete from CONTEST_PARAMETER_ANP_SOURCE where ID = '" + LB_ID.Text + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "' ";
            for (int i = 0; i < DGR_ANP.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_ANP.Items[i].FindControl("CB_ANP");
                if (cb.Checked)
                {
                    conn.QueryString = conn.QueryString + "insert into CONTEST_PARAMETER_ANP_SOURCE select " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + LB_AGENT_LEVEL.Text + "'," +
                                        "'" + DGR_ANP.Items[i].Cells[0].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE() ";
                }
            }

            try
            {
                conn.ExecuteNonQuery();
            }
            catch { }
        }

        protected void SaveDGRTraining()
        {
            conn.QueryString = "delete from CONTEST_PARAMETER_TRAINING where ID = '" + LB_ID.Text + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "' ";
            for (int i = 0; i < DGR_TRAINING.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_TRAINING.Items[i].FindControl("CB_TRAINING");
                if (cb.Checked)
                {
                    conn.QueryString = conn.QueryString + "insert into CONTEST_PARAMETER_TRAINING select " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + LB_AGENT_LEVEL.Text + "'," +
                                        "'" + DGR_TRAINING.Items[i].Cells[0].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "GETDATE() ";
                }
            }

            try
            {
                conn.ExecuteNonQuery();
            }
            catch { }
        }

        protected void SaveRewardDetail()
        {
            conn.QueryString = "exec SP_CONTEST_PARAMETER_REWARD_UPDATE " +
                                "'" + LB_ID.Text + "'," +
                                "'" + LB_AGENT_LEVEL.Text + "'," +
                                "'" + DDL_REWARD.SelectedValue + "'," +
                                "'" + TXT_REWARD_PERSISTENCE.Text.Trim().Replace(",", "") + "'," +
                                "'" + TXT_REWARD_PREMIUM.Text.Trim().Replace(",", "") + "'," +
                                "'" + TXT_REWARD_POLICY.Text.Trim().Replace(",", "") + "'," +
                                "'" + TXT_REWARD_PA.Text.Trim().Replace(",", "") + "'," +
                                "'" + TXT_REWARD_AA.Text.Trim().Replace(",", "") + "'," +
                                "'" + TXT_REWARD_NAA.Text.Trim().Replace(",", "") + "'," +
                                "'" + TXT_REWARD_RECRUIT.Text.Trim().Replace(",", "") + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            LoadRewardCriteria();
        }

        protected void DDL_REWARD_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRewardCriteria();
        }

        protected void LoadRewardCriteria()
        {
            conn.QueryString = "exec SP_CONTEST_PARAMETER_REWARD_DETAIL " +
                                "'" + LB_ID.Text + "'," +
                                "'" + LB_AGENT_LEVEL.Text + "'," +
                                 DDL_REWARD.SelectedValue;
            conn.ExecuteQuery();

            TXT_REWARD_AA.Text = conn.GetFieldValue("MIN_AA").ToString();
            TXT_REWARD_NAA.Text = conn.GetFieldValue("MIN_NAA").ToString();
            TXT_REWARD_PA.Text = conn.GetFieldValue("MIN_PA").ToString();
            TXT_REWARD_PERSISTENCE.Text = conn.GetFieldValue("PERSISTENCE").ToString();
            TXT_REWARD_POLICY.Text = conn.GetFieldValue("MIN_POLICY").ToString();
            TXT_REWARD_PREMIUM.Text = conn.GetFieldValue("MIN_PREMIUM").ToString();
            TXT_REWARD_RECRUIT.Text = conn.GetFieldValue("MIN_RECRUIT").ToString();


            TXT_REWARD_AA.BackColor = System.Drawing.Color.Pink;
            TXT_REWARD_NAA.BackColor = System.Drawing.Color.Pink;
            TXT_REWARD_PA.BackColor = System.Drawing.Color.Pink;
            TXT_REWARD_PERSISTENCE.BackColor = System.Drawing.Color.Pink;
            TXT_REWARD_POLICY.BackColor = System.Drawing.Color.Pink;
            TXT_REWARD_PREMIUM.BackColor = System.Drawing.Color.Pink;
            TXT_REWARD_RECRUIT.BackColor = System.Drawing.Color.Pink;

            TXT_REWARD_AA.ForeColor = System.Drawing.Color.Red;
            TXT_REWARD_NAA.ForeColor = System.Drawing.Color.Red;
            TXT_REWARD_PA.ForeColor = System.Drawing.Color.Red;
            TXT_REWARD_PERSISTENCE.ForeColor = System.Drawing.Color.Red;
            TXT_REWARD_POLICY.ForeColor = System.Drawing.Color.Red;
            TXT_REWARD_PREMIUM.ForeColor = System.Drawing.Color.Red;
            TXT_REWARD_RECRUIT.ForeColor = System.Drawing.Color.Red;


            if (TXT_REWARD_AA.Text.Trim() != "" && TXT_REWARD_AA.Text.Trim() != "0")
            {
                TXT_REWARD_AA.BackColor = System.Drawing.Color.LightGreen;
                TXT_REWARD_AA.ForeColor = System.Drawing.Color.Green;
            }

            if (TXT_REWARD_NAA.Text.Trim() != "" && TXT_REWARD_NAA.Text.Trim() != "0")
            {
                TXT_REWARD_NAA.BackColor = System.Drawing.Color.LightGreen;
                TXT_REWARD_NAA.ForeColor = System.Drawing.Color.Green;
            }

            if (TXT_REWARD_PA.Text.Trim() != "" && TXT_REWARD_PA.Text.Trim() != "0")
            {
                TXT_REWARD_PA.BackColor = System.Drawing.Color.LightGreen;
                TXT_REWARD_PA.ForeColor = System.Drawing.Color.Green;
            }

            if (TXT_REWARD_PERSISTENCE.Text.Trim() != "" && TXT_REWARD_PERSISTENCE.Text.Trim() != "0")
            {
                TXT_REWARD_PERSISTENCE.BackColor = System.Drawing.Color.LightGreen;
                TXT_REWARD_PERSISTENCE.ForeColor = System.Drawing.Color.Green;
            }

            if (TXT_REWARD_POLICY.Text.Trim() != "" && TXT_REWARD_POLICY.Text.Trim() != "0")
            {
                TXT_REWARD_POLICY.BackColor = System.Drawing.Color.LightGreen;
                TXT_REWARD_POLICY.ForeColor = System.Drawing.Color.Green;
            }

            if (TXT_REWARD_PREMIUM.Text.Trim() != "" && TXT_REWARD_PREMIUM.Text.Trim() != "0")
            {
                TXT_REWARD_PREMIUM.BackColor = System.Drawing.Color.LightGreen;
                TXT_REWARD_PREMIUM.ForeColor = System.Drawing.Color.Green;
            }

            if (TXT_REWARD_RECRUIT.Text.Trim() != "" && TXT_REWARD_RECRUIT.Text.Trim() != "0")
            {
                TXT_REWARD_RECRUIT.BackColor = System.Drawing.Color.LightGreen;
                TXT_REWARD_RECRUIT.ForeColor = System.Drawing.Color.Green;
            }
        }
    }
}