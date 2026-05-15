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

namespace UWBOX.Form_TC
{
    public partial class ProductMinMaxSA : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                conn.QueryString = "select b.CODE, DESCR = b.CODE + ' - ' + b.DESCR from PARAM_PRODUCT_MASTER_TC a inner join TC_MASTER b on a.TC_ID = b.CODE where a.PRODUCT_CODE = '" + LB_CODE.Text + "'";
                conn.ExecuteQuery();
                for (int i = 0; i < conn.GetRowCount(); i++)
                    DDL_TC.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

                LoadRecord(LB_CODE.Text);
                LoadRecord_LS(LB_CODE.Text);
            }
            else
            {
                UploadMinMax();
                UploadMinMax_LS();
            }
        }

        protected void UploadMinMax()
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

                conn.QueryString = "delete from PRODUCT_MASTER_MINMAX_SUM_ASSURED where PRODUCT_CODE='" + LB_CODE.Text + "' and FLAG_LUMPSUM = 0 ";
                conn.ExecuteNonQuery();

                string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
                //string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullpath + ";Extended Properties=Excel 12.0;";
                OleDbConnection con = new OleDbConnection(connstr);
                con.Open();

                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                foreach (DataRow myRow in dt.Rows)
                {
                    try
                    {
                        float amount = float.Parse(myRow[0].ToString().Trim());
                    }
                    catch
                    {
                        continue;
                    }

                    try
                    {
                        conn.QueryString = "insert into PRODUCT_MASTER_MINMAX_SUM_ASSURED select " +
                                            "'" + LB_CODE.Text + "'," +
                                            "'" + myRow[0].ToString().Trim() + "'," +
                                            "0," +
                                            "'" + myRow[1].ToString().Trim().Replace(",", ".") + "'," +
                                            "'" + myRow[2].ToString().Trim().Replace(",", ".") + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "GETDATE()," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "GETDATE()";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERROR.Text = LB_ERROR.Text + "<BR>" + ex.Message;
                    }
                }

                LoadRecord(LB_CODE.Text);
            }
        }

        protected void UploadMinMax_LS()
        {
            if (FU_LS.HasFile)
            {
                LB_ERROR_LS.Text = "";

                string filename = Path.GetFileName(FU_LS.FileName);
                string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
                if (File.Exists(fullpath))
                {
                    File.Delete(fullpath);
                }
                FU_LS.SaveAs(fullpath);

                conn.QueryString = "delete from PRODUCT_MASTER_MINMAX_SUM_ASSURED where PRODUCT_CODE='" + LB_CODE.Text + "' and FLAG_LUMPSUM = 1 ";
                conn.ExecuteNonQuery();

                string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
                //string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullpath + ";Extended Properties=Excel 12.0;";
                OleDbConnection con = new OleDbConnection(connstr);
                con.Open();

                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                foreach (DataRow myRow in dt.Rows)
                {
                    try
                    {
                        float amount = float.Parse(myRow[0].ToString().Trim());
                    }
                    catch
                    {
                        continue;
                    }

                    try
                    {
                        conn.QueryString = "insert into PRODUCT_MASTER_MINMAX_SUM_ASSURED select " +
                                            "'" + LB_CODE.Text + "'," +
                                            "'" + myRow[0].ToString().Trim() + "'," +
                                            "1," +
                                            "'" + myRow[1].ToString().Trim().Replace(",", ".") + "'," +
                                            "'" + myRow[2].ToString().Trim().Replace(",", ".") + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "GETDATE()," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "GETDATE()";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERROR_LS.Text = LB_ERROR_LS.Text + "<BR>" + ex.Message;
                    }
                }

                LoadRecord_LS(LB_CODE.Text);
            }
        }

        protected void LoadRecord(string code)
        {
            FU.Visible = true;
            BT_CLEAR.Visible = false;
            //LB_CODE.Text = conn.GetFieldValue("CODE").ToString();

            conn.QueryString = "exec SP_PRODUCT_MASTER_MINMAX_SA '" + code + "','0' ";
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

        protected void LoadRecord_LS(string code)
        {
            FU_LS.Visible = true;
            BT_CLEAR_LS.Visible = false;
            //LB_CODE.Text = conn.GetFieldValue("CODE").ToString();

            conn.QueryString = "exec SP_PRODUCT_MASTER_MINMAX_SA '" + code + "','1' ";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                BT_CLEAR_LS.Visible = true;
                DGR_LUMPSUM.Visible = true;
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_LUMPSUM.DataSource = dt;
                DGR_LUMPSUM.DataBind();
            }
            else
                DGR_LUMPSUM.Visible = false;
        }

        protected void BT_CLEAR_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PRODUCT_MASTER_MINMAX_SUM_ASSURED where PRODUCT_CODE='" + LB_CODE.Text + "' and FLAG_LUMPSUM = 0 ";
            conn.ExecuteNonQuery();
            LoadRecord(LB_CODE.Text);
        }

        protected void BT_CLEAR_LS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PRODUCT_MASTER_MINMAX_SUM_ASSURED where PRODUCT_CODE='" + LB_CODE.Text + "' and FLAG_LUMPSUM = 1 ";
            conn.ExecuteNonQuery();
            LoadRecord_LS(LB_CODE.Text);
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PRODUCT_MASTER_MINMAX_SA '" + LB_CODE.Text + "','0' ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, LB_CODE.Text, true);
        }

        protected void BT_XLS_LS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PRODUCT_MASTER_MINMAX_SA '" + LB_CODE.Text + "','1' ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, LB_CODE.Text, true);
        }

        protected void DDL_TC_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}