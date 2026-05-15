using System;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Parameter
{
    public partial class PARAMETER_MASTER_BUSINESS_RECRUITMENT : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                Setup();
                ShowScheme();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "DESCR		= a.DESCRIPTION, " +
                                "TYPE		= c.DESCR, " +
                                "STARTDATE	= convert(varchar(20), a.STARTDATE, 106), " +
                                "ENDDATE	= convert(varchar(20), a.ENDDATE, 106) " +
                                "from		PARAM_REMUN_MASTER a " +
                                "inner join	PR_REMUN_TYPE c on a.TYPE = c.CODE " +
                                "where " +
                                "ID = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            LB_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
            LB_ENDDATE.Text = conn.GetFieldValue("ENDDATE").ToString();
            LB_STARTDATE.Text = conn.GetFieldValue("STARTDATE").ToString();
            LB_TYPE.Text = conn.GetFieldValue("TYPE").ToString();

            conn.QueryString = "select CODE, DESCR from PR_TRANS_TYPE order by 2";
            conn.ExecuteQuery();
            DDL_TRANSTYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TRANSTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from V_LINK_UB_PR_PRODUCT_GROUP order by 2 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GROUP_PRODUCT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }


        protected void FillDGR()
        {
            conn.QueryString = "exec SP_PARAM_REMUN_BONUS_ROYALTY " +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_TRANSTYPE.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();


            //for (int i = 0; i < DGR.Items.Count; i++)
            //{
            //    TextBox txtMINAMT = (TextBox)DGR.Items[i].FindControl("TXT_MIN_AMT");
            //    TextBox txtMAXAMT = (TextBox)DGR.Items[i].FindControl("TXT_MAX_AMT");
            //    TextBox txtCOMM = (TextBox)DGR.Items[i].FindControl("TXT_COMM");
            //    TextBox txtOR1 = (TextBox)DGR.Items[i].FindControl("TXT_OR1");
            //    TextBox txtOR2 = (TextBox)DGR.Items[i].FindControl("TXT_OR2");
            //    TextBox txtOR3 = (TextBox)DGR.Items[i].FindControl("TXT_OR3");
            //    TextBox txtOR4 = (TextBox)DGR.Items[i].FindControl("TXT_OR4");
            //    TextBox txtOR5 = (TextBox)DGR.Items[i].FindControl("TXT_OR5");
            //    TextBox txtOR6 = (TextBox)DGR.Items[i].FindControl("TXT_OR6");
            //    TextBox txtOR7 = (TextBox)DGR.Items[i].FindControl("TXT_OR7");
            //    TextBox txtOR8 = (TextBox)DGR.Items[i].FindControl("TXT_OR8");

            //    CheckBox cbNETT = (CheckBox)DGR.Items[i].FindControl("CB_NETT");

            //    txtMAXAMT.Text = DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "");
            //    txtMINAMT.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
            //    txtCOMM.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "");
            //    txtOR1.Text = DGR.Items[i].Cells[6].Text.Replace("&nbsp;", "");
            //    txtOR2.Text = DGR.Items[i].Cells[7].Text.Replace("&nbsp;", "");
            //    txtOR3.Text = DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "");
            //    txtOR4.Text = DGR.Items[i].Cells[9].Text.Replace("&nbsp;", "");
            //    txtOR5.Text = DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "");
            //    txtOR6.Text = DGR.Items[i].Cells[11].Text.Replace("&nbsp;", "");
            //    txtOR7.Text = DGR.Items[i].Cells[12].Text.Replace("&nbsp;", "");
            //    txtOR8.Text = DGR.Items[i].Cells[13].Text.Replace("&nbsp;", "");

            //    if (DGR.Items[1].Cells[4].Text == "1")
            //        cbNETT.Checked = true;
            //}
        }

        protected void DDL_TRANSTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "DeleteAll")
            {
                conn.QueryString = "delete from PARAM_REMUN_BONUS_ROYALTY where " +
                                    "ID = '" + LB_ID.Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }

            if (e.CommandName == "Delete")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                    if (cb.Checked)
                    {
                        conn.QueryString = "delete from PARAM_REMUN_BONUS_ROYALTY where " +
                                            "ID = '" + LB_ID.Text + "' " +
                                            "and TRANSTYPE = '" + DGR.Items[i].Cells[0].Text + "' " +
                                            "and YEAR = " + DGR.Items[i].Cells[1].Text;
                        conn.ExecuteNonQuery();
                    }
                }
                FillDGR();
            }
        }

        protected void FillDGRAgency()
        {
            conn.QueryString = "exec SP_PARAM_REMUN_MASTER_COMPANY '" + LB_ID.Text + "',0";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AGENCY_AVAIL.DataSource = dt;
            DGR_AGENCY_AVAIL.DataBind();

            conn.QueryString = "exec SP_PARAM_REMUN_MASTER_COMPANY '" + LB_ID.Text + "',1";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AGENCY_SELECTED.DataSource = dt;
            DGR_AGENCY_SELECTED.DataBind();
        }


        protected void FillDGRProduct()
        {
            conn.QueryString = "exec SP_PARAM_REMUN_MASTER_PRODUCT '" + LB_ID.Text + "',0,'" + DDL_GROUP_PRODUCT.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PRODUCT_AVAIL.DataSource = dt;
            DGR_PRODUCT_AVAIL.DataBind();

            conn.QueryString = "exec SP_PARAM_REMUN_MASTER_PRODUCT '" + LB_ID.Text + "',1,null";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PRODUCT_SELECTED.DataSource = dt;
            DGR_PRODUCT_SELECTED.DataBind();
        }

        protected void ShowScheme()
        {
            LB_TITLE.Text = BT_SCHEME.Text;

            DV_SCHEME.Visible = true;
            DV_AGENCY.Visible = false;
            DV_PRODUCT.Visible = false;

            FillDGR();
        }

        protected void BT_SCHEME_Click(object sender, EventArgs e)
        {
            ShowScheme();
        }

        protected void BT_AGENCY_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;

            DV_SCHEME.Visible = false;
            DV_AGENCY.Visible = true;
            DV_PRODUCT.Visible = false;

            FillDGRAgency();
        }

        protected void BT_PRODUCT_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;

            DV_SCHEME.Visible = false;
            DV_AGENCY.Visible = false;
            DV_PRODUCT.Visible = true;

            FillDGRProduct();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_AGENCY_AVAIL.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_AGENCY_AVAIL.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void CB_ALL_CheckedChanged1(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_AGENCY_SELECTED.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_AGENCY_SELECTED.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void BT_AGENCY_ADD_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_AGENCY_AVAIL.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_AGENCY_AVAIL.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "insert into PARAM_REMUN_MASTER_COMPANY select '" + LB_ID.Text + "', '" + DGR_AGENCY_AVAIL.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            FillDGRAgency();
        }

        protected void BT_AGENCY_DELETE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_AGENCY_SELECTED.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_AGENCY_SELECTED.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "delete from PARAM_REMUN_MASTER_COMPANY where ID = '" + LB_ID.Text + "' and COMPANY_CODE = '" + DGR_AGENCY_SELECTED.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            FillDGRAgency();
        }

        protected void DDL_GROUP_PRODUCT_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PARAM_REMUN_MASTER_PRODUCT '" + LB_ID.Text + "',0,'" + DDL_GROUP_PRODUCT.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PRODUCT_AVAIL.DataSource = dt;
            DGR_PRODUCT_AVAIL.DataBind();
        }

        protected void CB_AVAIL_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PRODUCT_AVAIL.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PRODUCT_AVAIL.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void CB_SELECTED_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PRODUCT_SELECTED.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PRODUCT_SELECTED.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void BT_PRODUCT_ADD_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PRODUCT_AVAIL.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PRODUCT_AVAIL.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "insert into PARAM_REMUN_MASTER_PRODUCT select '" + LB_ID.Text + "', '" + DGR_PRODUCT_AVAIL.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            FillDGRProduct();
        }

        protected void BT_PRODUCT_DELETE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PRODUCT_SELECTED.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PRODUCT_SELECTED.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "delete from PARAM_REMUN_MASTER_PRODUCT where ID = '" + LB_ID.Text + "' and PRODUCT_CODE = '" + DGR_PRODUCT_SELECTED.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            FillDGRProduct();
        }

        protected void BT_XLS_DOWNLOAD_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PARAM_REMUN_BONUS_ROYALTY_DOWNLOAD '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "PARAM_REMUN_BONUS_ROYALTY", true);
        }

        protected void BT_XLS_UPLOAD_Click(object sender, EventArgs e)
        {
            if (!FU.HasFile)
                return;

            UploadParamFile();
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                Button btALL = (Button)e.Item.FindControl("BT_ALL");
                Button bt = (Button)e.Item.FindControl("BT_X");

                btALL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ALL ?')){return false;};");
                bt.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }
        }


        protected void UploadParamFile()
        {
            conn.QueryString = "delete from PARAM_REMUN_BONUS_ROYALTY where ID = '" + LB_ID.Text + "'";
            conn.ExecuteNonQuery();

            string filename = Path.GetFileName(FU.FileName);
            string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            FU.SaveAs(fullpath);
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);

            con.Open();

            DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow myRow in dt.Rows)
            {
                try
                {
                    conn.QueryString = "exec SP_PARAM_REMUN_BONUS_ROYALTY_UPLOAD " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + myRow[0].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                        "'" + myRow[1].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                        "'" + myRow[2].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                        "'" + myRow[3].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                        "'" + myRow[4].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                        "'" + myRow[5].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                        "'" + myRow[6].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                        "'" + myRow[7].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                        "'" + myRow[8].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }
            con.Close();

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            FillDGR();
        }

        protected void BT_PARAM_GUIDANCE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PARAMETER_GUIDANCE '2b'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "PARAMETER_GUIDANCE", true);
        }
    }
}