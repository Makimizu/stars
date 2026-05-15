using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Text;
using System.Net.Mime;
using System.Collections;
using System.Data.OleDb;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;


namespace LIFE.Form_App
{
    public partial class ApplicationPolicyPrinting : System.Web.UI.Page
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
            conn.QueryString = "select YEAR	= YEAR(GETDATE()) - a.SEQ + 1 from	SC_SEQ a where a.SEQ <= 5 order by 1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "CODE	= SEQ, " +
                                "DESCR	= UPPER(DateName( month , DateAdd( month , SEQ , -1 ))) " +
                                "from	SC_SEQ  " +
                                "where  " +
                                "SEQ		<= 12";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MONTH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "THISYEAR	= YEAR(GETDATE()), " +
                                "THISMONTH	= MONTH(GETDATE())";
            conn.ExecuteQuery();

            try
            {
                DDL_YEAR.SelectedValue = conn.GetFieldValue("THISYEAR").ToString();
            }
            catch { }

            try
            {
                DDL_MONTH.SelectedValue = conn.GetFieldValue("THISMONTH").ToString();
            }
            catch { }
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_MONTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_PRINTING_SCHEDULE " +
                                DDL_YEAR.SelectedValue + "," +
                                DDL_MONTH.SelectedValue;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select " +
                                "PRODUCT_GROUP_CODE		= a.PRODUCT_GROUP_CODE, " +
                                "DESCR                  = b.DESCR, " + 
                                "SP_GENERATE		    = a.SP_GENERATE " +
                                "from		PARAM_APPLICATION_PRINTING a " +
                                "inner join	UWBOX.dbo.PR_PRODUCT_GROUP b on a.PRODUCT_GROUP_CODE = b.CODE " +
                                "order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddlREPORT = (DropDownList)DGR.Items[i].FindControl("DDL_REPORT");
                
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlREPORT.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            string productGroupCode = "", genDate = "";

            DropDownList ddlREPORT = (DropDownList)e.Item.FindControl("DDL_REPORT");

            productGroupCode = ddlREPORT.SelectedValue.ToString();
            genDate = e.Item.Cells[0].Text.ToString().Replace("&nbsp;", "");

            if (e.CommandName == "Generate")
            {
                Task.Run(() => GenerateDBF(productGroupCode, genDate));
                //GenerateDBF(productGroupCode, genDate);
            }

            if (e.CommandName == "Attachment")
            {
                Task.Run(() => GenerateAttachment(productGroupCode, genDate));
            }
        }

        protected void GenerateDBF(string productGroup, string genDate)
        {
            conn.QueryString = "select " +
                                    "FTP_ADDRESS, " +
                                    "FTP_UID, " +
                                    "FTP_PWD, " +
                                    "FTP_LOCAL_FOLDER, " +
                                    "FTP_REMOTE_FOLDER " +
                                    "from SECURITY.dbo.SC_FTP " +
                                    "where " +
                                    "FTP_CODE = 6";
            conn.ExecuteQuery();

            string FileName = "";
            //string Path = "C:/FTP/XPRINS/";
            string Path = conn.GetFieldValue("FTP_LOCAL_FOLDER").ToString();
            //string Params = "";

            string ftpUsername = conn.GetFieldValue("FTP_UID").ToString();
            string ftpPassword = Crypto.DecryptStringAES(conn.GetFieldValue("FTP_PWD").ToString());
            string remoteDestination = "ftp://" + conn.GetFieldValue("FTP_ADDRESS").ToString() + conn.GetFieldValue("FTP_REMOTE_FOLDER").ToString() + productGroup + "/";

            try
            {
                conn.QueryString = "select " +
                                    "SP_GENERATE, " +
                                    "GENDATE = convert(varchar(15),convert(date,'" + genDate + "'),112) " +
                                    "from PARAM_APPLICATION_PRINTING " +
                                    "where PRODUCT_GROUP_CODE = '" + productGroup + "'";
                conn.ExecuteQuery();

                //FileName = "cet_pol_stars_ITL_20220516";
                FileName = "CET_POL_STARS_" + productGroup + "_" + conn.GetFieldValue("GENDATE").ToString();

                //conn.QueryString = "select CODE = '12345',DESCR = 'NamaKu 12345' union all select CODE = '789',DESCR = 'NamaKu 7989'";
                //conn.QueryString = "exec SP_RENDER_DBF " +
                //                    "'" + ddlREPORT.SelectedValue.ToString() + "'," +
                //                    "'" + e.Item.Cells[0].Text.ToString().Replace("&nbsp;", "") + "'";
                //conn.ExecuteQuery();

                conn.QueryString = "exec SP_APPLICATION_PRINTING_" + productGroup + " " +
                                        "'" + genDate + "'";
                conn.ExecuteQuery();

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();

                DataSet ds;
                ds = new DataSet();
                ds.Merge(dt);

                DataSetIntoDBF(Path, FileName, ds);
                //FTP to Remote Folder
                string localFile = Path + FileName + ".dbf";
                remoteDestination = remoteDestination + FileName + ".dbf";

                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    client.UploadFile(remoteDestination, WebRequestMethods.Ftp.UploadFile, localFile);
                }

                if (File.Exists(localFile))
                {
                    //File.SetAttributes(localFile, FileAttributes.Normal);
                    File.Delete(localFile);
                }

                LB_STAT.Text = "File Generated " + FileName + ".dbf, Please check FTP Folder.";
            }
            catch (Exception ex)
            {
                LB_STAT.Text = ex.Message;
            }
        }

        protected void GenerateAttachment(string productGroup, string genDate)
        {
            try
            {
                conn.QueryString = "select " +
                                "FTP_ADDRESS, " +
                                "FTP_UID, " +
                                "FTP_PWD, " +
                                "FTP_LOCAL_FOLDER, " +
                                "FTP_REMOTE_FOLDER " +
                                "from SECURITY.dbo.SC_FTP " +
                                "where " +
                                "FTP_CODE = 6";
                conn.ExecuteQuery();

                string FileName = "";
                string Path = conn.GetFieldValue("FTP_LOCAL_FOLDER").ToString();

                string ftpUsername = conn.GetFieldValue("FTP_UID").ToString();
                string ftpPassword = Crypto.DecryptStringAES(conn.GetFieldValue("FTP_PWD").ToString());
                string remoteDestination = "ftp://" + conn.GetFieldValue("FTP_ADDRESS").ToString() + conn.GetFieldValue("FTP_REMOTE_FOLDER").ToString() + productGroup + "/";

                conn.QueryString = "select " +
                                    "SP_GENERATE, " +
                                    "GENDATE = convert(varchar(15),convert(date,'" + genDate + "'),112) " +
                                    "from PARAM_APPLICATION_PRINTING " +
                                    "where PRODUCT_GROUP_CODE = '" + productGroup + "'";
                conn.ExecuteQuery();

                FileName = "CET_POL_STARS_" + productGroup + "_" + conn.GetFieldValue("GENDATE").ToString() + ".zip";

                conn.QueryString = "exec SP_APPLICATION_PRINTING_" + productGroup + " " +
                                        "'" + genDate + "'";
                conn.ExecuteQuery();

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();

                string folder = Path + "ATTACH/";
                string filePath = "";
                string regno = "";
                string sqlString = "";
                string sourcePath = folder;
                string destPath = "";

                foreach (DataRow row in dt.Rows)
                {
                    regno = row["NOAPLI"].ToString();
                    conn.QueryString = "exec SP_APPLICATION_PRINTING_ATTACHMENTS '" + regno + "'";
                    conn.ExecuteQuery();

                    DataTable data;
                    data = new DataTable();
                    data = conn.GetDataTable().Copy();
                    foreach (DataRow rowData in data.Rows)
                    {
                        filePath = folder + rowData["NEWFILE"];
                        sqlString = "select THEFILE from ARCHIEVE.dbo.LF_ARSIP where CODE = '" + rowData["FILEID"] + "'";
                        try
                        {
                            GlobalUse.SQLToFilePath(filePath, sqlString);
                        }
                        catch { }
                    }
                }

                destPath = Path + FileName;
                ZipFile.CreateFromDirectory(sourcePath, destPath);

                string localFile = destPath;
                remoteDestination = remoteDestination + FileName;

                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    client.UploadFile(remoteDestination, WebRequestMethods.Ftp.UploadFile, localFile);
                }

                System.IO.DirectoryInfo di = new DirectoryInfo(sourcePath);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                if (File.Exists(localFile))
                {
                    File.Delete(localFile);
                }

                LB_STAT.Text = "Zip File Generated " + FileName + ", Please check FTP Folder.";
            }
            catch (Exception ex)
            {
                LB_STAT.Text = ex.Message;
            }
        }

        protected void DataSetIntoDBF(string Path, string fileName, DataSet dataSet)
        {
            int columnLimit = 254;
            int columnCount = 0;
            ArrayList list = new ArrayList();

            if (File.Exists(Path + fileName + ".dbf"))
            {
                File.SetAttributes(Path + fileName + ".dbf", FileAttributes.Normal);
                File.Delete(Path + fileName + ".dbf");
            }

            string createSql = "create table " + fileName + " (";

            foreach (DataColumn dc in dataSet.Tables[0].Columns)
            {
                string fieldName = dc.ColumnName;

                string type = dc.DataType.ToString();
                string len = "";

                columnCount++;

                if (columnCount > columnLimit)
                    break;
                
                len = "(100)";

                switch (fieldName)
                {
                    case "KDPROD":
                    case "KDPRO1":
                    case "KDPRO2":
                    case "KDPRO3":
                    case "KDPRO4":
                    case "KDPRO5":
                    case "KDPRO6":
                    case "KDPRO7":
                    case "NOAPLI":
                    case "VALUTA":
                        len = "(10)";
                        break;
                    case "ID":
                        len = "(11)";
                        break;
                    case "USERID":
                        len = "(15)";
                        break;
                    case "TGLGEN":
                        len = "(19)";
                        break;
                    case "TGLPOL":
                    case "TGLKNT":
                    case "TGLAKN":
                    case "TGLLHR":
                    case "TGLKNTP":
                    case "TGLAKNP":
                    case "CARBAY":
                        len = "(17)";
                        break;
                    case "NOPOLI":
                        len = "(20)";
                        break;
                    case "NOMOHP":
                    case "NMPROD":
                        len = "(50)";
                        break;
                    case "NMPRO1":
                    case "NMPRO2":
                    case "NMPRO3":
                    case "NMPRO4":
                    case "NMPRO5":
                    case "NMPRO6":
                    case "NMPRO7":
                    case "JMLPRE":
                    case "MSPRJ":
                        len = "(30)";
                        break;
                    case "BARCODE":
                        len = "(200)";
                        break;
                    case "NMLENG":
                    //case "ALAMT1":
                    //case "ALAMT2":
                    //case "ALAMT3":
                    //case "ALAMT4":
                    //case "SETIAP":
                    case "BYAPOL":
                        len = "(50)";
                        break;
                }

                //type = "varchar" + len; // use for DBASE IV dbf
                type = "C" + len; //use for foxpro type dbf

                createSql = createSql + "[" + fieldName + "]" + " " + type + ",";
                //createSql = createSql + fieldName + " " + type + ",";

                list.Add(fieldName);
            }

            createSql = createSql.Substring(0, createSql.Length - 1) + ")";

            LB_STAT.Text = createSql;
            
            OleDbConnection con = new OleDbConnection(GetConnection(Path));
            //OleDbConnection con = new OleDbConnection(@"Provider=VFPOLEDB;Data Source=c:\Temp");
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = con;
            con.Open();

            cmd.CommandText = createSql;
            cmd.ExecuteNonQuery();
            
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                string insertSql = "insert into " + fileName + " values(";

                for (int i = 0; i < list.Count; i++)
                {
                    insertSql = insertSql + "'" + ReplaceEscape(row[list[i].ToString()].ToString()) + "',";
                }

                insertSql = insertSql.Substring(0, insertSql.Length - 1) + ")";

                cmd.CommandText = insertSql;
                cmd.ExecuteNonQuery();
            }

            con.Close();
        }

        private static string GetConnection(string path)
        {
            //return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=dBASE IV;";
            return "Provider=VFPOLEDB;Data Source=" + path;
            //return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=dBASE IV;";
        }

        public static string ReplaceEscape(string str)
        {
            str = str.Replace("'", "''");
            str = str.Replace(System.Environment.NewLine, "");
            return str;
        }
    }
}