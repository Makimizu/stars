using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Runtime.InteropServices;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;


namespace HEALTH.Form_Klaim
{
    public partial class TPA_IncomingClaim : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                BrowseFTP();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PARAM_ACT_TPA where isnull(FTP_CODE,'')<>'' order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_FTP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select Y = YEAR(GETDATE()) union all " +
                                "select Y = YEAR(GETDATE())-1 union all " +
                                "select Y = YEAR(GETDATE())-2 union all " +
                                "select Y = YEAR(GETDATE())-3  " +
                                "order by 1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select Y = YEAR(GETDATE()), M = MONTH(GETDATE())";
            conn.ExecuteQuery();

            DDL_YEAR.SelectedValue = conn.GetFieldValue("Y").ToString();
            DDL_MONTH.SelectedValue = conn.GetFieldValue("M").ToString();
        }

        protected void BrowseFTP()
        {
            LB_ERROR.Text = "";

            conn.QueryString = "select FTP_CODE, FORMAT_FILE from PARAM_ACT_TPA where CODE = '" + DDL_FTP.SelectedValue + "'";
            conn.ExecuteQuery();

            string ftpcode = conn.GetFieldValue("FTP_CODE").ToString();
            string format = conn.GetFieldValue("FORMAT_FILE").ToString();

            conn.QueryString = "exec SP_FTP_TPA_FOLDER_BROWSE '" + ftpcode + "','" + DDL_YEAR.SelectedValue + "','" + DDL_MONTH.SelectedValue + "'";
            conn.ExecuteQuery();

            string root = conn.GetFieldValue("ROOT").ToString();
            string folder = conn.GetFieldValue("FOLDER").ToString();

            DataTable dt = GlobalUse.FTPBrowse(ftpcode, root);

            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddlFILE = (DropDownList)DGR.Items[i].FindControl("DDL_FILENAME");
                Button btSET = (Button)DGR.Items[i].FindControl("BT_SET");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                Button btHEADER = (Button)DGR.Items[i].FindControl("BT_HEADER");
                Button btDETAIL = (Button)DGR.Items[i].FindControl("BT_DETAIL");
                Label lbHEADER = (Label)DGR.Items[i].FindControl("LB_HEADER");
                Label lbDETAIL = (Label)DGR.Items[i].FindControl("LB_DETAIL");
                Label lbPROCESSBY = (Label)DGR.Items[i].FindControl("LB_PROCESSBY");
                Label lbPROCESSDATE = (Label)DGR.Items[i].FindControl("LB_PROCESSDATE");
                Label lbBATCH = (Label)DGR.Items[i].FindControl("LB_BATCH");
                Label lbEXC = (Label)DGR.Items[i].FindControl("LB_EXC");

                btSET.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk PROSES ?')){return false;};");
                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");

                conn.QueryString = "select a.*, " +
                                    "EXC = (select count(F09) from TPA_CLAIM_HEADER where F09 not in (select CLAIM_NO from CLAIM_MASTER) and BATCH_ID = a.BATCH_ID) " +
                                    "from TPA_CLAIM_BATCH a " +
                                    "where SOURCE_DIR = '" + DGR.Items[i].Cells[0].Text + DGR.Items[i].Cells[1].Text + "'";
                conn.ExecuteQuery();

                if (conn.GetRowCount() == 0)
                {
                    string fullpath = DGR.Items[i].Cells[0].Text.Trim() + DGR.Items[i].Cells[1].Text.Trim() + "/" + folder;
                    DataTable dtfilename = GlobalUse.FTPBrowseSpecificFiles(ftpcode, fullpath, format);

                    if (dtfilename.Rows.Count > 0)
                    {
                        for (int j = 0; j < dtfilename.Rows.Count; j++)
                        {
                            ddlFILE.Items.Add(new ListItem(dtfilename.Rows[j][1].ToString().Trim(), dtfilename.Rows[j][0].ToString().Trim() + dtfilename.Rows[j][1].ToString().Trim()));
                        }

                        btSET.Text = "SET HEADER";
                    }
                    else
                    {
                        ddlFILE.Visible = false;
                        btSET.Visible = false;
                    }
                }
                else
                {
                    btDEL.Visible = true;

                    lbHEADER.Text = conn.GetFieldValue("HEADER_FILENAME").ToString();
                    lbDETAIL.Text = conn.GetFieldValue("DETAIL_FILENAME").ToString();
                    lbPROCESSBY.Text = conn.GetFieldValue("USERBY").ToString();
                    lbPROCESSDATE.Text = conn.GetFieldValue("USERDATE").ToString();
                    lbBATCH.Text = conn.GetFieldValue("BATCH_ID").ToString();
                    lbEXC.Text = conn.GetFieldValue("EXC").ToString();

                    if (conn.GetFieldValue("HEADER_FILENAME").ToString().Trim() == "" || conn.GetFieldValue("DETAIL_FILENAME").ToString().Trim() == "")
                    {
                        string fullpath = DGR.Items[i].Cells[0].Text.Trim() + DGR.Items[i].Cells[1].Text.Trim() + "/" + folder;
                        DataTable dtfilename = GlobalUse.FTPBrowseSpecificFiles(ftpcode, fullpath, format);

                        for (int j = 0; j < dtfilename.Rows.Count; j++)
                        {
                            if (conn.GetFieldValue("HEADER_FILENAME").ToString() != dtfilename.Rows[j][1].ToString().Trim() && conn.GetFieldValue("DETAIL_FILENAME").ToString() != dtfilename.Rows[j][1].ToString().Trim())
                                ddlFILE.Items.Add(new ListItem(dtfilename.Rows[j][1].ToString(), dtfilename.Rows[j][0].ToString() + dtfilename.Rows[j][1].ToString()));

                            if (conn.GetFieldValue("HEADER_FILENAME").ToString() == "")
                                btSET.Text = "SET HEADER";
                            else
                                btSET.Text = "SET DETAIL";
                        }
                    }
                    else
                    {
                        ddlFILE.Visible = false;
                        btSET.Visible = false;
                    }

                    if (lbHEADER.Text != "")
                    {
                        btHEADER.Visible = true;
                        btHEADER.Attributes.Add("onclick", "window.open('TPA_CLAIM_FILE.aspx?TPA=" + DDL_FTP.SelectedValue + "&BATCH_ID=" + lbBATCH.Text + "&TIPE=H','HEADER','height=400px,width=1000px,left=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
                    }

                    if (lbDETAIL.Text != "")
                    {
                        btDETAIL.Visible = true;
                        btDETAIL.Attributes.Add("onclick", "window.open('TPA_CLAIM_FILE.aspx?TPA=" + DDL_FTP.SelectedValue + "&BATCH_ID=" + lbBATCH.Text + "&TIPE=D','DETAIL','height=400px,width=1000px,left=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
                    }
                }
            }
        }

        protected void DDL_FTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrowseFTP();
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrowseFTP();
        }

        protected void DDL_MONTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            BrowseFTP();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERROR.Text = "";

            Label lbBATCH = (Label)e.Item.FindControl("LB_BATCH");
            Button btSET = (Button)e.Item.FindControl("BT_SET");
            DropDownList ddlFILE = (DropDownList)e.Item.FindControl("DDL_FILENAME");

            if (e.CommandName == "Set")
            {
                try
                {
                    string sqlbatch = "";
                    string batch = lbBATCH.Text.Trim();
                    string headerfilename = "";
                    string detailfilename = "";

                    if (btSET.Text == "SET HEADER")
                        headerfilename = ddlFILE.SelectedItem.Text.Trim();
                    else
                        detailfilename = ddlFILE.SelectedItem.Text.Trim();

                    if (batch == "")
                    {
                        conn.QueryString = "select BATCH = replace(convert(varchar(30),GETDATE(),112) + convert(varchar(30),GETDATE(),114),':','')";
                        conn.ExecuteQuery();
                        batch = conn.GetFieldValue("BATCH").ToString();

                        sqlbatch = "insert into TPA_CLAIM_BATCH select " +
                                            "'" + batch + "'," +
                                            "'" + DDL_FTP.SelectedValue + "'," +
                                            "'" + e.Item.Cells[0].Text + e.Item.Cells[1].Text + "'," +
                                            "'" + headerfilename + "'," +
                                            "null," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "GETDATE()";
                    }
                    else
                    {
                        sqlbatch = "update TPA_CLAIM_BATCH set " +
                                            "DETAIL_FILENAME = '" + detailfilename + "'," +
                                            "USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "USERDATE = GETDATE() " +
                                            "where " +
                                            "BATCH_ID = '" + batch + "'";
                    }

                    conn.QueryString = sqlbatch;
                    conn.ExecuteNonQuery();

                    conn.QueryString = "select * from PARAM_ACT_TPA where CODE = '" + DDL_FTP.SelectedValue + "'";
                    conn.ExecuteQuery();

                    string ftpcode = conn.GetFieldValue("FTP_CODE").ToString();
                    string format = conn.GetFieldValue("FORMAT_FILE").ToString();
                    string downloadpath = conn.GetFieldValue("DOWNLOAD_PATH").ToString();

                    GlobalUse.FTPDownloadFile(ftpcode, e.Item.Cells[0].Text.Trim() + e.Item.Cells[1].Text.Trim() + "\\", ddlFILE.SelectedItem.Text, downloadpath);
                    string filePath = downloadpath + "\\" + ddlFILE.SelectedItem.Text.Trim();

                    if (btSET.Text == "SET HEADER")
                    {
                        ProcessFILE("header", batch, filePath, format.ToUpper());
                    }
                    else
                    {
                        try
                        {
                            ProcessFILE("detail", batch, filePath, format.ToUpper());
                        }
                        catch (System.Exception ex)
                        {
                            LB_ERROR.Text = ex.Message;
                        }
                        SaveArsip(batch, downloadpath + "\\");
                    }

                    BrowseFTP();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }

            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_CLM_TPA_CLAIM_BATCH_ROLLBACK '" + lbBATCH.Text + "'";
                    conn.ExecuteNonQuery();
                    BrowseFTP();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
        }

        protected void SaveArsip(string batch, string path)
        {
            conn.QueryString = "select HEADER_FILENAME, DETAIL_FILENAME from TPA_CLAIM_BATCH where BATCH_ID = '" + batch + "'";
            conn.ExecuteQuery();
            string fileheader = conn.GetFieldValue("HEADER_FILENAME").ToString();
            string filedetail = conn.GetFieldValue("DETAIL_FILENAME").ToString();

            /*
            conn.QueryString = "select BATCH_ID from CLAIM_MASTER_BATCH where BATCH_ID like '" + batch + "-%'";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                if (File.Exists(path + fileheader))
                {
                    GlobalUse.SaveArsip(path + fileheader, "CLMBATCH", conn.GetFieldValue(i, 0).ToString(), "", "", "File HEADER", fileheader, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
                }

                if (File.Exists(path + filedetail))
                {
                    GlobalUse.SaveArsip(path + filedetail, "CLMBATCH", conn.GetFieldValue(i, 0).ToString(), "", "", "File DETAIL", filedetail, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
                }
            }
            */

            if (File.Exists(path + fileheader))
            {
                try
                {
                    File.Delete(path + fileheader);
                }
                catch { }
            }

            if (File.Exists(path + filedetail))
            {
                try
                {
                    File.Delete(path + filedetail);
                }
                catch { }
            }
        }

        protected void ProcessFILE(string mode, string batch, string fullpath, string format)
        {
            if (format == "TXT")
            {
                StreamReader reader = File.OpenText(fullpath);
                string line;
                int seq = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    seq++;

                    string values = "";
                    int nfield = 0;
                    string[] splits = line.Split(new string[] { "\",", }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string value in splits)
                    {
                        nfield++;
                        string val = value.Replace("\"", "").Replace("'", "`");
                        if (val.Length > 255)
                            val = value.Substring(0, 255);

                        values = values + ",'" + val + "'";
                    }

                    int cntfields = 60;
                    string tablename = "TPA_CLAIM_HEADER";
                    if (mode == "detail")
                    {
                        cntfields = 30;
                        tablename = "TPA_CLAIM_DETAIL";
                    }

                    for (int i = 0; i < cntfields - nfield; i++)
                    {
                        values = values + ",null";
                    }

                    values = "'" + batch + "'," +
                             seq.ToString() +
                             values + "," +
                             "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                             "GETDATE()";

                    try
                    {
                        conn.QueryString = "insert into " + tablename + " select " + values;
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                reader.Close();

            }

            if (format.Substring(0,3) == "XLS")
            {
                string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
                OleDbConnection con = new OleDbConnection(connstr);
                con.Open();

                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                int cntfields = 60;
                string tablename = "TPA_CLAIM_HEADER";
                if (mode == "detail")
                {
                    cntfields = 30;
                    tablename = "TPA_CLAIM_DETAIL";
                }

                int IDX = 0;
                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    string sql = "insert into " +tablename+ " select " +
                                    "'" + batch + "'," +
                                    "'" + IDX.ToString() + "',";
                                        
                    for (int i = 0; i < cntfields; i++)
                    {
                        string val = "";
                        try
                        {
                            val = myRow[i].ToString().Trim().Replace("'", "`");
                        }
                        catch { }

                        sql = sql + "'" + val + "',";
                    }

                    sql = sql + "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE()";

                    conn.QueryString = sql;
                    conn.ExecuteNonQuery();
                }


                con.Close();
            }

            if (mode == "detail")
            {
                conn.QueryString = "exec SP_CLM_TPA_CLAIM_BATCH_INSERT '" + batch + "'";
                conn.ExecuteNonQuery();
            }

        }

        protected void DGR_FILENAME_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERROR.Text = "";

            if (e.CommandName == "Download")
            {
                conn.QueryString = "select * from PARAM_ACT_TPA where CODE = '" + DDL_FTP.SelectedValue + "'";
                conn.ExecuteQuery();

                string ftpcode = conn.GetFieldValue("FTP_CODE").ToString();
                string format = conn.GetFieldValue("FORMAT_FILE").ToString();
                string downloadpath = conn.GetFieldValue("DOWNLOAD_PATH").ToString();

                try
                {
                    GlobalUse.FTPDownloadFile(ftpcode, e.Item.Cells[0].Text.Trim(), e.Item.Cells[1].Text.Trim(), downloadpath);

                    string filePath = downloadpath + "\\" + e.Item.Cells[1].Text.Trim();
                    System.IO.FileInfo file = new System.IO.FileInfo(filePath);
                    if (file.Exists)
                    {
                        Response.Clear();
                        Response.ClearHeaders();
                        Response.ClearContent();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                        Response.AddHeader("Content-Length", file.Length.ToString());
                        Response.ContentType = "text/plain";
                        Response.Flush();
                        Response.TransmitFile(file.FullName);
                        Response.End();
                    }

                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = Environment.UserName + "<BR>" + ex.Message;
                }
            }
        }
    }
}