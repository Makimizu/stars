using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Threading.Tasks;


namespace UWBOX.Form_Tools
{
    public partial class SurplusUwDetail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LB_BATCHID.Text = Request.QueryString["ID"].ToString();
                    Setup();
                    FillDGRBatch();
                }
                catch { }
            }
        }

        protected void Setup()
        {
            DDL_DATE.Items.Clear();
            conn.QueryString = "select CODE, DESCR from V_UW_SURPLUS_DATE_VACANT order by CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_DATE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            if (DDL_DATE.SelectedValue == "")
            {
                return;
            }

            if (!FU.HasFile)
            {
                return;
            }

            string filename = Path.GetFileName(FU.FileName);
            string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            FU.SaveAs(fullpath);
            ProcessFile(filename, fullpath);

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            FillDGRBatch();
        }

        protected void ProcessFile(string filename, string FullPath)
        {
            conn.QueryString = "delete from BATCH_UW_SURPLUS where THEDATE = '" + DDL_DATE.SelectedValue + "' " + 
                                "insert into BATCH_UW_SURPLUS select " +
                                "'" + DDL_DATE.SelectedValue + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE() ";
            conn.ExecuteQuery();
            

            ProcessSheet(LB_BATCHID.Text.Trim(), "HEALTH", FullPath);
            ProcessSheet(LB_BATCHID.Text.Trim(), "GLIFE", FullPath);
            ProcessSheet(LB_BATCHID.Text.Trim(), "LIFE", FullPath);
        }

        protected void ProcessSheet(string BATCH_ID, string tablename, string FullPath)
        {
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);

            try
            {
                con.Open();

                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + tablename + "$]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                int IDX = 0;
                String AppID = "";
                switch (tablename)
                {
                    case "HEALTH": 
                        IDX = 0;
                        AppID = "HO";
                        break;
                    case "GLIFE": 
                        IDX = 100000;
                        AppID = "GL";
                        break;
                    case "LIFE": 
                        IDX = 200000;
                        AppID = "LF";
                        break;
                }
                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    try
                    {
                        string sql = "exec SP_UW_SURPLUS_INSERT " +
                                        "'" + GlobalUse.GlobalDateFormat(DDL_DATE.SelectedValue, "d/M/yyyy") + "'," +
                                        "'" + AppID + "'," +
                                        "'" + myRow[0].ToString().Trim().Replace("'", "`") + "', " +
                                        "'" + myRow[1].ToString().Trim().Replace("'", "`") + "', " +
                                        "'" + myRow[2].ToString().Trim().Replace("'", "`") + "', " +
                                        "'" + myRow[3].ToString().Trim().Replace("'", "`") + "', " +
                                        "'" + myRow[4].ToString().Trim().Replace("'", "`") + "', " +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.QueryString = sql;
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                    /*
                    for (int i = 0; i < 55; i++)
                    {
                        sql = sql + "''";
                        if (i < 54)
                        {
                            sql = sql + ",";
                        }
                    }
                    */
                    
                }

                con.Close();
                LB_ERR.ForeColor = System.Drawing.Color.Black;
                LB_ERR.Text = "Upload Complete.";
            }
            catch (System.Exception ex)
            {
                con.Close();
                //conn.QueryString = "delete from BATCH_MASTER where ID = '" + BATCH_ID + "'";
                //conn.ExecuteNonQuery();
                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = LB_ERR.Text + "<br>" + ex.Message;
            }
        }

        protected void ShowReport(String surplusDate)
        {
            conn.QueryString = "select APP_ID, CODE from SECURITY.dbo.REPORT_LIST where APP_ID = 'UW' and REPORT_NAME = 'RPT_UW_SURPLUS'";
            conn.ExecuteQuery();
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.parambody.location.href = '../../ReportViewer/Viewer.aspx?APPID=" + conn.GetFieldValue("APP_ID").ToString() + "&CODE=" + conn.GetFieldValue("CODE").ToString() + "&THEDATE=" + surplusDate + "';</script>");
        }

        protected void FillDGRBatch()
        {
            conn.QueryString = "exec SP_BATCH_UW_SURPLUS ";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BATCH.DataSource = dt;
            DGR_BATCH.DataBind();

            for (int i = 0; i < DGR_BATCH.Items.Count; i++)
            {
                Button btSHOW = (Button)DGR_BATCH.Items[i].FindControl("BT_SHOW");
                Button btDEL = (Button)DGR_BATCH.Items[i].FindControl("BT_DEL");

                btSHOW.Text = DGR_BATCH.Items[i].Cells[0].Text;
                btDEL.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO DELETE ?')){return false;};");
            }
        }

        protected void DGR_BATCH_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Show")
            {
                ShowReport(e.Item.Cells[0].Text);
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from BATCH_UW_SURPLUS where THEDATE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGRBatch();
            }
        }
    }
}