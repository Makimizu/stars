using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Data.OleDb;

namespace UWBOX.Form_Parameter
{
    public partial class Param_Premium_Rate : System.Web.UI.Page
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
            else
            {
                Upload();
            }
        }

        protected void Upload()
        {
            if (FU.HasFile)
            {
                LB_ERROR.Text = "";

                string filename = Path.GetFileName(FU.FileName);
                string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
                if (File.Exists(fullpath))
                {
                    File.Delete(fullpath);
                }
                FU.SaveAs(fullpath);

                conn.QueryString = "select TENOR_CODE from PARAM_PREMIUM_RATE_MASTER where CODE = '" + LB_CODE.Text + "'";
                conn.ExecuteQuery();
                if (conn.GetFieldValue("TENOR_CODE").ToString() != "HP")
                    Process(fullpath);
                else
                    ProcessHealthPlan(fullpath);

                if (File.Exists(fullpath))
                    File.Delete(fullpath);
                LoadRecord(LB_CODE.Text);
            }
        }

        protected void ProcessHealthPlan(string fullpath)
        {
            conn.QueryString = "delete from PARAM_PREMIUM_RATE_DETAIL_HEALTH where CODE='" + LB_CODE.Text + "' " +
                                "exec SP_BATCH_MASTER_INSERT 'HP','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'"; ;
            conn.ExecuteQuery();
            string ID = conn.GetFieldValue(0, 0).ToString();

            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);
            con.Open();

            conn.QueryString = "select BENEFIT_ID from PARAM_HEALTH_BENEFIT order by SEQ";
            conn.ExecuteQuery();
            DataTable dtBENEFIT = new DataTable();
            dtBENEFIT = conn.GetDataTable().Copy();

            int ii = 0;
            for (int i = 0; i < dtBENEFIT.Rows.Count; i++)
            {
                try
                {
                    DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + dtBENEFIT.Rows[i][0].ToString() + "$]", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow myRow in dt.Rows)
                    {
                        conn.QueryString = "insert into BATCH_DETAIL_RAW select " +
                                            "'" + ID + "'," +
                                            ii.ToString() + "," +
                                            "'" + dtBENEFIT.Rows[i][0].ToString() + "'";

                        for (int j = 0; j < 59; j++)
                        {
                            string val = "";
                            try
                            {
                                val = myRow[j].ToString().Trim().Replace("'", "`").Replace(",", "");
                            }
                            catch { }

                            if (val != "")
                                conn.QueryString = conn.QueryString + ",'" + val + "'";
                            else
                            {
                                for (int jj = j; jj < 59; jj++)
                                    conn.QueryString = conn.QueryString + ",''";

                                break;
                            }
                        }

                        conn.ExecuteNonQuery();
                        ii++;
                    }
                }
                catch { }
            }
            con.Close();

            conn.QueryString = "exec SP_PARAM_PREMIUM_RATE_DETAIL_HEALTH_INSERT '" + ID + "','" + LB_CODE.Text + "'";
            conn.ExecuteNonQuery();

        }

        protected void Process(string fullpath)
        {
            conn.QueryString = "delete from PARAM_PREMIUM_RATE_DETAIL where CODE='" + LB_CODE.Text + "'";
            conn.ExecuteNonQuery();

            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);


            for (int i = 0; i < DDL_GENDER.Items.Count; i++)
            {
                con.Open();
                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + DDL_GENDER.Items[i].Text + "$]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                foreach (DataRow myRow in dt.Rows)
                {
                    if (myRow[0].ToString().Trim() == "")
                        break;

                    try
                    {
                        float amount = float.Parse(myRow[0].ToString().Trim());
                    }
                    catch
                    {
                        continue;
                    }

                    int columns = dt.Columns.Count;

                    for (int j = 1; j < columns; j++)
                    {
                        string tenor = dt.Rows[0][j].ToString().Replace("#", "").Trim();
                        if (tenor == "")
                            break;

                        try
                        {
                            conn.QueryString = "exec SP_PARAM_PREMIUM_RATE_DETAIL_UPSERT " +
                                                "'" + LB_CODE.Text + "'," +
                                                "'" + myRow[0].ToString().Trim() + "'," +
                                                "'" + tenor + "'," +
                                                "'" + myRow[j].ToString().Trim().Replace(",", ".") + "'," +
                                                "'" + DDL_GENDER.Items[i].Value + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteNonQuery();
                        }
                        catch (System.Exception ex)
                        {
                            LB_ERROR.Text = LB_ERROR.Text + "<BR>" + ex.Message;
                        }
                    }
                }
            }
        }

        protected void Setup()
        {
            BT_CLEAR.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO CLEAR ?')){return false;};");

            conn.QueryString = "select CODE, DESCR from PR_TENOR_TYPE";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TENOR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from PARAM_PRODUCT_GROUP order by 2";
            conn.ExecuteQuery();
            DDL_PRODUCT_GROUP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PRODUCT_GROUP_SEARCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_PRODUCT_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";

            conn.QueryString = "select CODE, DESCR from PARAM_PREMIUM_RATE_MASTER " +
                                "where " +
                                "DESCR like '%" + TXT_SEARCH.Text.Trim() + "%' " +
                                "and PRODUCT_GROUP = '" + DDL_PRODUCT_GROUP_SEARCH.SelectedValue + "' " +
                                "order by 2";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LIST.DataSource = dt;
            DGR_LIST.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR_LIST.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR_LIST.Items[i].FindControl("LB_ID");
                lbCODE.Text = DGR_LIST.Items[i].Cells[2].Text;
            }
        }

        protected void DGR_LIST_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                LoadRecord(e.Item.Cells[1].Text);
            }
            else if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from PARAM_PREMIUM_RATE_MASTER where CODE = '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteQuery();

                FillDGR();
            }
        }

        protected void LoadRecord(string code)
        {
            TR_NEW.Visible = true;
            FU.Visible = true;
            BT_CLEAR.Visible = false;

            conn.QueryString = "select CODE, DESCR, TENOR_CODE, PRODUCT_GROUP from PARAM_PREMIUM_RATE_MASTER where CODE = '" + code + "'";
            conn.ExecuteQuery();

            LB_CODE.Text = conn.GetFieldValue("CODE").ToString();
            TXT_NAME.Text = conn.GetFieldValue("DESCR").ToString();

            try
            {
                DDL_TENOR.SelectedValue = conn.GetFieldValue("TENOR_CODE").ToString();
            }
            catch { }

            DDL_PRODUCT_GROUP.SelectedValue = "";
            try
            {
                DDL_PRODUCT_GROUP.SelectedValue = conn.GetFieldValue("PRODUCT_GROUP").ToString();
            }
            catch { }

            conn.QueryString = "select TENOR_CODE from PARAM_PREMIUM_RATE_MASTER where CODE = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetFieldValue("TENOR_CODE").ToString() != "HP")
            {
                TR_DATA.Visible = true;
                TR_DATA_HP.Visible = false;

                conn.QueryString = "exec SP_PARAM_PREMIUM_RATE_DETAIL '" + code + "','" + DDL_GENDER.SelectedValue + "'";
                conn.ExecuteQuery();
                if (conn.GetRowCount() > 0)
                {
                    BT_CLEAR.Visible = true;
                    DGR_RATE.Visible = true;
                    DataTable dt;
                    dt = new DataTable();
                    dt = conn.GetDataTable().Copy();
                    DGR_RATE.DataSource = dt;
                    DGR_RATE.DataBind();
                }
                else
                    DGR_RATE.Visible = false;
            }
            else
            {
                TR_DATA.Visible = false;
                TR_DATA_HP.Visible = true;
                IFHP.Src = "../../ReportViewer/Viewer.aspx?APPID=UW&CODE=4&RATE_CODE=" + code;
            }


            TBL_XLS.Visible = true;
            TBL_DETAIL.Visible = true;
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect("Param_Premium_Rate.aspx");
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_NAME.Text.Trim() == "")
                return;

            string ID = "null";
            if (LB_CODE.Text != "" || DDL_PRODUCT_GROUP.SelectedValue == "")
                ID = "'" + LB_CODE.Text + "'";

            conn.QueryString = "exec SP_PARAM_PREMIUM_RATE_MASTER_UPSERT " +
                                ID + "," +
                                "'" + TXT_NAME.Text.Trim() + "'," +
                                "'" + DDL_TENOR.SelectedValue + "'," +
                                "'" + DDL_PRODUCT_GROUP.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            LoadRecord(conn.GetFieldValue("CODE").ToString());
        }

        protected void BT_CLEAR_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PARAM_PREMIUM_RATE_DETAIL where CODE='" + LB_CODE.Text + "'";
            conn.ExecuteNonQuery();
            LoadRecord(LB_CODE.Text);
        }

        protected void DDL_GENDER_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord(LB_CODE.Text);
        }


        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            //conn.QueryString = "exec SP_PARAM_PREMIUM_RATE_DETAIL '" + LB_CODE.Text + "','" + DDL_GENDER.SelectedValue + "'";
            //conn.ExecuteQuery();
            //DataTable dt;
            //dt = new DataTable();
            //dt = conn.GetDataTable().Copy();
            //GlobalUse.ExportDataSetToExcel(dt, this, TXT_NAME.Text, true);

            conn.QueryString = "select " +
                                "URL = '../../ReportViewer/Viewer.aspx?APPID=' + APP_ID + '&CODE=' + convert(varchar(10), CODE) " +
                                "from SECURITY.dbo.REPORT_LIST where APP_ID='" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "' and REPORT_NAME = 'RPT_PARAM_PREMIUM_RATE'";
            conn.ExecuteQuery();

            string URL = conn.GetFieldValue("URL").ToString() + "&TABLE_ID=" + LB_CODE.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>window.open('" + URL + "','_blank');</script>");
        }

        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_PRODUCT_GROUP_SEARCH_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}