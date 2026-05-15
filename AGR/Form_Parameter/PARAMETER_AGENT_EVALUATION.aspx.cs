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
    public partial class PARAMETER_AGENT_EVALUATION : System.Web.UI.Page
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
                                "from EVALUATION_MASTER a " +
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


            //conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 1";
            //conn.QueryString = "select " +
            //                    "a.CODE,  " +
            //                    "a.DESCR " +
            //                    "from        PR_MARKET_SEGMENT a " +
            //                    "inner join  EVALUATION_PARAMETER_GENERAL b on a.CODE = b.VAL collate database_default and b.ID = '" + LB_ID.Text + "' and b.MODE = '" + DDL_MODE.SelectedValue + "' and b.FIELD_CODE = 'EVAGR00' " +
            //                    "order by 1";
            //conn.ExecuteQuery();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //    DDL_CD.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            //FillDGRMarketSegment();
        }

        protected void FillDGRMarketSegment()
        {
            DV_PARAMETER.Visible = false;


            conn.QueryString = "select " +
                                "SUB_CODE    = SUB_CODE,  " +
                                "DESCR       = UPPER(DESCR), " +
                                "STAT        = (case when b.CURRENT_AGENT_LEVEL is not null then 1 else 0 end) " +
                                "from        PARAM_SUB_CHANNEL_DISTRIBUTION a " +
                                "inner join  EVALUATION_PARAMETER_GENERAL bb on a.MARKET_SEGMENT = bb.VAL collate database_default and bb.ID = '" + LB_ID.Text + "' and bb.MODE = '" + DDL_MODE.SelectedValue + "' and bb.FIELD_CODE = 'EVAGR00' " +
                                "left join   EVALUATION_PARAMETER b on b.ID = bb.ID and b.MODE = bb.MODE and a.SUB_CODE = b.CURRENT_AGENT_LEVEL collate database_default " +
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

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_EVALUATION_MASTER";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btPROCESS = (Button)DGR.Items[i].FindControl("BT_PROCESS");
                Button btAGENCY = (Button)DGR.Items[i].FindControl("BT_AGENCY");
                Button btPRODUCT = (Button)DGR.Items[i].FindControl("BT_PRODUCT");
                Button btX = (Button)DGR.Items[i].FindControl("BT_DELETE");

                btPROCESS.Attributes.Add("onclick", "if(!confirm('Are you sure to PROCESS ?')){return false;}else{ShowProgress();}");
                btX.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");

                btAGENCY.Text = "AGENCY " + DGR.Items[i].Cells[3].Text;
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

                TR_GENERAL.Visible = false;
                TR_AGENCY.Visible = false;
                TR_PRODUCT.Visible = false;
                TR_PARAMETER.Visible = true;

                FillDGRMarketSegment();
                if (DGR_CD.Items.Count == 0)
                    return;

                DV_PARAMETER.Visible = false;
                //LoadParameter(DGR_CD.Items[0].Cells[0].Text, DGR_CD.Items[0].Cells[1].Text);
            }

            if (e.CommandName == "Process")
            {
                //string SQL  = "exec SP_EVALUATION_PARAMETER_PROCESS " +
                //                    "'" + e.Item.Cells[0].Text + "'," +
                //                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                //Task.Run(() => SQL_Thread(SQL));
                //System.Threading.Thread.Sleep(5000);

                conn.QueryString = "exec SP_EVALUATION_PARAMETER_PROCESS " +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery(500000);

                FillDGR();
                return;
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from EVALUATION_MASTER where ID = '" + e.Item.Cells[0].Text + "'";
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

                TR_GENERAL.Visible = false;
                TR_AGENCY.Visible = true;
                TR_PRODUCT.Visible = false;
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

                TR_GENERAL.Visible = false;
                TR_AGENCY.Visible = false;
                TR_PRODUCT.Visible = true;
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

                TR_GENERAL.Visible = true;
                TR_AGENCY.Visible = false;
                TR_PRODUCT.Visible = false;
                TR_PARAMETER.Visible = false;

                FillParamGeneral();
                return;
            }

            if (e.CommandName == "Report")
            {
                Response.Redirect(e.Item.Cells[6].Text);
            }
        }

        protected void FillParamGeneral()
        {
            conn.QueryString = "exec SP_EVALUATION_PARAMETER_GENERAL '" + LB_ID.Text + "'," + DDL_MODE_GENERAL.SelectedValue;
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

        protected void SQL_Thread(string SQL_query)
        {
            conn.QueryString = SQL_query;
            conn.ExecuteNonQuery();
        }

        protected void BT_CREATE_Click(object sender, EventArgs e)
        {
            if (TXT_DESCR.Text.Trim() == "" || TXT_PERIOD_START.Text.Trim() == "" || TXT_PERIOD_END.Text.Trim() == "")
                return;

            string COPY_FROM_ID = "null";
            if (DDL_MASTER.SelectedValue != "")
                COPY_FROM_ID = "'" + DDL_MASTER.SelectedValue + "'";

            conn.QueryString = "exec SP_EVALUATION_MASTER_UPSERT " +
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

            conn.QueryString = "SP_EVALUATION_PARAMETER_AGENCY '" + LB_ID.Text + "',0,'" + TXT_AGENCY_0.Text.Trim() + "','" + DDL_AREA_0.SelectedValue + "'";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AGENCY_0.DataSource = dt;
            DGR_AGENCY_0.DataBind();
            LB_AGENCY_CNT_0.Text = conn.GetRowCount().ToString() + " Records";

            conn.QueryString = "SP_EVALUATION_PARAMETER_AGENCY '" + LB_ID.Text + "',1,'" + TXT_AGENCY_1.Text.Trim() + "','" + DDL_AREA_1.SelectedValue + "'";
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

            conn.QueryString = "SP_EVALUATION_PARAMETER_PRODUCT '" + LB_ID.Text + "',0,'" + TXT_PRODUCT_0.Text.Trim() + "','" + DDL_GROUP_UNSELECTED.SelectedValue + "'," + DDL_STAT_UNSELECTED.SelectedValue;
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PRODUCT_0.DataSource = dt;
            DGR_PRODUCT_0.DataBind();
            LB_PRODUCT_CNT_0.Text = conn.GetRowCount() + " Records";

            conn.QueryString = "SP_EVALUATION_PARAMETER_PRODUCT '" + LB_ID.Text + "',1,'" + TXT_PRODUCT_1.Text.Trim() + "','" + DDL_GROUP_SELECTED.SelectedValue + "'," + DDL_STAT_SELECTED.SelectedValue;
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PRODUCT_1.DataSource = dt;
            DGR_PRODUCT_1.DataBind();
            LB_PRODUCT_CNT_1.Text = conn.GetRowCount() + " Records";
            for (int i = 0; i < DGR_PRODUCT_1.Items.Count; i++)
            {
                TextBox txtPCT = (TextBox)DGR_PRODUCT_1.Items[i].FindControl("TXT_PCT");
                txtPCT.Text = DGR_PRODUCT_1.Items[i].Cells[2].Text;
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
                        conn.QueryString = "insert into EVALUATION_AGENCY select '" + LB_ID.Text + "','" + DGR_AGENCY_0.Items[i].Cells[0].Text + "'";
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
                        conn.QueryString = "delete from EVALUATION_AGENCY where ID = '" + LB_ID.Text + "' and COMPANY_CODE = '" + DGR_AGENCY_1.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                }

                FillDGRAgency();
                return;
            }
        }

        protected void DGR_PRODUCT_0_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                conn.QueryString = "insert into EVALUATION_PRODUCT select '" + LB_ID.Text + "','" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();

                FillDGRProduct();
                return;
            }

            if (e.CommandName == "SelectAll")
            {
                for (int i = 0; i < DGR_PRODUCT_0.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_PRODUCT_0.Items[i].FindControl("CB_UNSELECTED");
                    if (cb.Checked)
                    {
                        conn.QueryString = "exec SP_EVALUATION_PARAMETER_PRODUCT_INSERT'" + LB_ID.Text + "','" + DGR_PRODUCT_0.Items[i].Cells[0].Text + "'";
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
                conn.QueryString = "delete from EVALUATION_PRODUCT where ID = '" + LB_ID.Text + "' and PRODUCT_CODE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();

                FillDGRProduct();
                return;
            }

            if (e.CommandName == "Save")
            {
                TextBox txtPCT = (TextBox)e.Item.FindControl("TXT_PCT");
                try
                {
                    conn.QueryString = "update EVALUATION_PRODUCT set " +
                                        "PCT = " + txtPCT.Text.Replace(",", "").Trim() + " " +
                                        "where ID = '" + LB_ID.Text + "' and PRODUCT_CODE = '" + e.Item.Cells[0].Text + "'";
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
                        conn.QueryString = "delete from EVALUATION_PRODUCT where ID = '" + LB_ID.Text + "' and PRODUCT_CODE = '" + DGR_PRODUCT_1.Items[i].Cells[0].Text + "'";
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

        protected void DDL_CD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRMarketSegment();
            LoadParameter(DGR_CD.Items[0].Cells[0].Text, DGR_CD.Items[0].Cells[1].Text);
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
                conn.QueryString = "delete from EVALUATION_PARAMETER where ID = '" + LB_ID.Text + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "' and MODE = " + DDL_MODE.SelectedValue;
                conn.ExecuteNonQuery();
                FillDGRMarketSegment();
            }
        }

        protected void FillDGR_ITEM()
        {
            conn.QueryString = "exec SP_EVALUATION_PARAMETER_ITEM " +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_MODE.SelectedValue + "'," +
                                "'" + LB_AGENT_LEVEL.Text + "'";
            conn.ExecuteQuery();

            DGR_ITEM.DataSource = conn.GetDataTable().Copy();
            DGR_ITEM.DataBind();

            for (int j = 0; j < DGR_ITEM.Items.Count; j++)
            {
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR_ITEM.Items[j].FindControl("TXT_VAL");

                if (DGR_ITEM.Items[j].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    ddl.Visible = true;
                    conn.QueryString = DGR_ITEM.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    for (int k = 0; k < conn.GetRowCount(); k++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                    try
                    {
                        ddl.SelectedValue = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
                else
                {
                    txtVAL.Visible = true;
                    switch (DGR_ITEM.Items[j].Cells[1].Text)
                    {
                        case "STR":
                            txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            if (DGR_ITEM.Items[j].Cells[4].Text != "0")
                            {
                                txtVAL.MaxLength = int.Parse(DGR_ITEM.Items[j].Cells[4].Text);
                            }
                            break;
                        case "INT":
                            txtVAL.Width = 100;
                            txtVAL.Font.Bold = true;
                            txtVAL.Style.Add("text-align", "right");
                            txtVAL.Text = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            txtVAL.Attributes.Add("text-align", "right");
                            txtVAL.BackColor = System.Drawing.Color.Cyan;
                            txtVAL.ForeColor = System.Drawing.Color.Blue;
                            ddl.BackColor = System.Drawing.Color.Cyan;
                            ddl.ForeColor = System.Drawing.Color.Blue;
                            break;
                        case "FLO":
                            txtVAL.Width = 100;
                            txtVAL.Font.Bold = true;
                            txtVAL.Style.Add("text-align", "right");
                            txtVAL.BackColor = System.Drawing.Color.LightGreen;
                            txtVAL.ForeColor = System.Drawing.Color.Green;
                            ddl.BackColor = System.Drawing.Color.LightGreen;
                            ddl.ForeColor = System.Drawing.Color.Green;
                            try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                txtVAL.Attributes.Add("text-align", "right");
                            }
                            catch { }
                            break;
                        case "BIT":
                            txtVAL.Visible = false;
                            ddl.Visible = true;
                            ddl.Items.Add(new ListItem("YES", "1"));
                            ddl.Items.Add(new ListItem("NO", "0"));
                            try
                            {
                                ddl.SelectedValue = DGR_ITEM.Items[j].Cells[3].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                            break;

                    }
                }
            }
        }


        protected void LoadParameter(string AGENT_LEVEL, string TITLE)
        {
            DV_PARAMETER.Visible = true;
            TBL_PARAMETER.Visible = true;
            LB_AGENT_LEVEL.Text = AGENT_LEVEL;
            LB_PARAMETER_TITLE.Text = TITLE + " - " + DDL_MODE.SelectedItem.Text;


            conn.QueryString = "exec SP_EVALUATION_PARAMETER '" + LB_ID.Text + "','" + DDL_MODE.SelectedValue + "','" + AGENT_LEVEL + "'";
            conn.ExecuteQuery();

            TXT_AUTH_DATE_FROM.Text = conn.GetFieldValue("AUTH_DATE_FROM").ToString();
            TXT_AUTH_DATE_TO.Text = conn.GetFieldValue("AUTH_DATE_TO").ToString();
            LB_TEAMS.Text = conn.GetFieldValue("MINIMUM_TEAMS").ToString();
            LB_TRAININGS.Text = conn.GetFieldValue("MINIMUM_TRAININGS").ToString();

            FillDGR_ITEM();
            FillDGRANP();
            FillDGRTrainings();
            FillDGRTeams();
        }


        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DV_PARAMETER.Visible = false;
            TBL_PARAMETER.Visible = false;
            FillDGRMarketSegment();
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
                conn.QueryString = "exec SP_EVALUATION_PARAMETER_UPSERT " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + DDL_MODE.SelectedValue + "'," +
                                    "'" + LB_AGENT_LEVEL.Text + "'," +
                                    AUTH_DATE_FROM + "," +
                                    AUTH_DATE_TO + "," +
                                    "null," +
                                    "null," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch
            {
                return;
            }


            for (int i = 0; i < DGR_ITEM.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_ITEM.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR_ITEM.Items[i].FindControl("DDL_REFF");

                string val = txt.Text.Trim();
                if (ddl.Visible)
                    val = ddl.SelectedValue;
                else
                {
                    if (DGR_ITEM.Items[i].Cells[1].Text == "FLO" || DGR_ITEM.Items[i].Cells[1].Text == "INT")
                    {
                        val = val.Replace(",", "");
                    }
                }

                conn.QueryString = "exec SP_EVALUATION_PARAMETER_ITEM_UPSERT " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + DDL_MODE.SelectedValue + "'," +
                                    "'" + LB_AGENT_LEVEL.Text + "'," +
                                    "'" + DGR_ITEM.Items[i].Cells[0].Text + "'," +
                                    "'" + val + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }


            SaveDGRANP();
            SaveDGRTeams();
            SaveDGRTraining();

            FillDGRMarketSegment();
            LoadParameter(LB_AGENT_LEVEL.Text, LB_PARAMETER_TITLE.Text);

            for (int i = 0; i < DGR_CD.Items.Count; i++)
            {
                Button bt = (Button)DGR_CD.Items[i].FindControl("BT_CD");
                if (DGR_CD.Items[i].Cells[0].Text == LB_AGENT_LEVEL.Text)
                {
                    bt.BackColor = System.Drawing.Color.Cyan;
                    return;
                }
            }
        }

        protected void SaveDGRANP()
        {
            conn.QueryString = "delete from EVALUATION_PARAMETER_ANP_SOURCE where ID = '" + LB_ID.Text + "' and MODE = '" + DDL_MODE.SelectedValue + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "' ";
            for (int i = 0; i < DGR_ANP.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_ANP.Items[i].FindControl("CB_ANP");
                if (cb.Checked)
                {
                    conn.QueryString = conn.QueryString + "insert into EVALUATION_PARAMETER_ANP_SOURCE select " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_MODE.SelectedValue + "'," +
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
            conn.QueryString = "delete from EVALUATION_PARAMETER_TRAINING where ID = '" + LB_ID.Text + "' and MODE = '" + DDL_MODE.SelectedValue + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "' ";
            for (int i = 0; i < DGR_TRAINING.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_TRAINING.Items[i].FindControl("CB_TRAINING");
                if (cb.Checked)
                {
                    conn.QueryString = conn.QueryString + "insert into EVALUATION_PARAMETER_TRAINING select " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_MODE.SelectedValue + "'," +
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

        protected void SaveDGRTeams()
        {
            conn.QueryString = "delete from EVALUATION_PARAMETER_MIN_MEMBER where ID = '" + LB_ID.Text + "' and MODE = '" + DDL_MODE.SelectedValue + "' and CURRENT_AGENT_LEVEL = '" + LB_AGENT_LEVEL.Text + "' ";
            for (int i = 0; i < DGR_TEAMS.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_TEAMS.Items[i].FindControl("TXT_TEAM");
                conn.QueryString = conn.QueryString + "insert into EVALUATION_PARAMETER_MIN_MEMBER select " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + DDL_MODE.SelectedValue + "'," +
                                    "'" + LB_AGENT_LEVEL.Text + "'," +
                                    "'" + DGR_TEAMS.Items[i].Cells[0].Text + "'," +
                                    txt.Text.Trim().Replace(",", "") + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE(), " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE() ";
            }

            try
            {
                conn.ExecuteNonQuery();
            }
            catch { }
        }

        protected void FillDGRANP()
        {
            conn.QueryString = "exec SP_EVALUATION_PARAMETER_ANP_SOURCE " +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_MODE.SelectedValue + "'," +
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
            conn.QueryString = "exec SP_EVALUATION_PARAMETER_TRAINING " +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_MODE.SelectedValue + "'," +
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

        protected void FillDGRTeams()
        {
            conn.QueryString = "exec SP_EVALUATION_PARAMETER_MIN_MEMBER " +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_MODE.SelectedValue + "'," +
                                "'" + LB_AGENT_LEVEL.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_TEAMS.DataSource = dt;
            DGR_TEAMS.DataBind();
            for (int i = 0; i < DGR_TEAMS.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_TEAMS.Items[i].FindControl("TXT_TEAM");
                txt.Text = DGR_TEAMS.Items[i].Cells[1].Text;
            }
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

        protected void DDL_MODE_GENERAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillParamGeneral();
        }

        protected void BT_SAVE_GENERAL_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_GENERAL.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_GENERAL.Items[i].FindControl("TXT_GENERAL");
                TextBox txtDATE = (TextBox)DGR_GENERAL.Items[i].FindControl("TXT_GENERAL_DATE");
                DropDownList ddlGENERAL = (DropDownList)DGR_GENERAL.Items[i].FindControl("DDL_GENERAL_VALUE");
                DataGrid dgr = (DataGrid)DGR_GENERAL.Items[i].FindControl("DGR_GENERAL_VALUE");

                conn.QueryString = "delete from EVALUATION_PARAMETER_GENERAL where " +
                                    "ID             = '" + LB_ID.Text + "' " +
                                    //"and MODE = " + DDL_MODE_GENERAL.SelectedValue + " " +
                                    "and FIELD_CODE = '" + DGR_GENERAL.Items[i].Cells[0].Text + "' ";
                conn.ExecuteNonQuery();

                string VAL = "";

                if (dgr.Items.Count == 0)
                {
                    if (txt.Visible)
                    {
                        VAL = txt.Text.Trim();

                        if (DGR_GENERAL.Items[i].Cells[1].Text == "INT" || DGR_GENERAL.Items[i].Cells[1].Text == "FLO")
                            VAL = VAL.Replace(",", "");
                    }

                    if (txtDATE.Visible)
                    {
                        VAL = txtDATE.Text.Trim();
                    }

                    if (ddlGENERAL.Visible)
                    {
                        VAL = ddlGENERAL.SelectedValue;
                    }

                    conn.QueryString = "insert into EVALUATION_PARAMETER_GENERAL select " +
                                            "ID         = '" + LB_ID.Text + "'," +
                                            "MODE       = a.SEQ - 1," +
                                            "FIELD_CODE = '" + DGR_GENERAL.Items[i].Cells[0].Text + "'," +
                                            "VAL        = '" + VAL + "'," +
                                            "VAL2       = null," +
                                            "USERBY     = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "USERDATE   = GETDATE() " +
                                            "from       (select SEQ from SC_SEQ where SEQ <= 2) a";
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
                            conn.QueryString = "insert into EVALUATION_PARAMETER_GENERAL select " +
                                                "ID         = '" + LB_ID.Text + "'," +
                                                "MODE       = a.SEQ - 1," +
                                                "FIELD_CODE = '" + DGR_GENERAL.Items[i].Cells[0].Text + "'," +
                                                "VAL        = '" + dgr.Items[j].Cells[0].Text + "'," +
                                                "VAL2       = " + VAL2 + "," +
                                                "USERBY     = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                                "USERDATE   = GETDATE() " +
                                                "from       (select SEQ from SC_SEQ where SEQ <= 2) a";
                            conn.ExecuteNonQuery();
                        }
                    }
                }
            }

            FillParamGeneral();
        }


    }
}