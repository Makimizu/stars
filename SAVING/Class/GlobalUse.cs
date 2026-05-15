using System;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Web.Services.Protocols;
using DMS.DBConnection;
using System.Net;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mime;
//using System.Web.Mail;

/// <summary>
/// Summary description for GlobalUse
/// </summary>
public class GlobalUse
{
    
    public GlobalUse()
    {
        
    }

    public static string GetConnString(string appid)
    {
        string connstr = "";
        Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        conn.QueryString = "select * from M_APPS where CODE='" + appid + "'";
        conn.ExecuteQuery();

        connstr = "Data Source=" + conn.GetFieldValue("APP_DBSVR").ToString() + ";" +
                            "Initial Catalog=" + conn.GetFieldValue("APP_DBNAME").ToString() + ";" +
                            "uid=" + conn.GetFieldValue("APP_DBUID").ToString() + ";" +
                            "pwd=" + Crypto.DecryptStringAES(conn.GetFieldValue("APP_DBPWD").ToString()) + ";" +
                            "Pooling=true";

        return connstr;
    }

    public static string GetAppAttribute(string appid, string attribute)
    {
        string result = "";
        Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        conn.QueryString = "select * from M_APPS where CODE='" + appid + "'";
        conn.ExecuteQuery();

        result = conn.GetFieldValue(attribute).ToString();

        return result;
    }

    public static string GlobalDateFormat(string sDate, string Format)
    {
        string res = sDate;
        DateTime date;
        if (DateTime.TryParseExact(sDate, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            string MM = "";
            switch (date.Month)
            {
                case 1: MM = "Jan"; break;
                case 2: MM = "Feb"; break;
                case 3: MM = "Mar"; break;
                case 4: MM = "Apr"; break;
                case 5: MM = "May"; break;
                case 6: MM = "Jun"; break;
                case 7: MM = "Jul"; break;
                case 8: MM = "Aug"; break;
                case 9: MM = "Sep"; break;
                case 10: MM = "Oct"; break;
                case 11: MM = "Nov"; break;
                case 12: MM = "Dec"; break;
            }

            res = date.Day.ToString() + " " + MM + " " + date.Year.ToString();
        }

        return res;
    }

    public static string SendEmail(string sender, string recipients, string CC, string BCC, string subject, string body, string[] attachment)
    {        
        Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        conn.QueryString = "select distinct " +
                            "SMTP = isnull(b.VALUE,''),  " +
                            "SMTP_PWD = isnull(c.VALUE,''),  " +
                            "SMTP_UID = isnull(d.VALUE,''), " +
                            "DEFAULT_SENDER = isnull(e.VALUE,'')   " +
                            "from SECURITY.dbo.M_APPS a  " +
                            "left join SECURITY.dbo.SC_GENERAL_SET b on b.APP_CODE=a.CODE and b.PARAMETER='SMTP'  " +
                            "left join SECURITY.dbo.SC_GENERAL_SET c on c.APP_CODE=a.CODE and c.PARAMETER='SMTP_PWD'  " +
                            "left join SECURITY.dbo.SC_GENERAL_SET d on d.APP_CODE=a.CODE and d.PARAMETER='SMTP_UID'  " +
                            "left join SECURITY.dbo.SC_GENERAL_SET e on d.APP_CODE=a.CODE and e.PARAMETER='DEFAULT_SENDER' " +
                            "where  " +
                            "a.APP_DBNAME = DB_NAME() " +
                            "and isnull(b.VALUE,'') <> ''";
        string SMTP = "";
        string UID = "";
        string PWD = "";
        string DEFAULT_SENDER = "";

        try
        {
            conn.ExecuteQuery();
            SMTP = conn.GetFieldValue("SMTP").ToString();
            UID = conn.GetFieldValue("SMTP_UID").ToString();
            PWD = Crypto.DecryptStringAES(conn.GetFieldValue("SMTP_PWD").ToString());
            DEFAULT_SENDER = conn.GetFieldValue("DEFAULT_SENDER").ToString();
        }
        catch(System.Exception ex)  
        {   return ex.Message;        }

        if (sender == "")
            sender = DEFAULT_SENDER;

        System.Net.Mail.MailMessage oMsg = new System.Net.Mail.MailMessage(sender,recipients);
        oMsg.Subject = subject;
        oMsg.IsBodyHtml = true;
        oMsg.Body = body;

        if(CC != "")
            oMsg.CC.Add(CC);

        if (BCC != "")
            oMsg.Bcc.Add(BCC);

        System.Net.Mail.SmtpClient objSMTPClient = new System.Net.Mail.SmtpClient();
        objSMTPClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        objSMTPClient.Host = SMTP;

        //if(sender == "" && UID != "")
        objSMTPClient.Credentials = new System.Net.NetworkCredential(UID, PWD);

        

        try
        { 
            /*
            if (attachment != "")
            {
                if (File.Exists(attachment))
                {
                    System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(attachment, System.Net.Mime.MediaTypeNames.Application.Octet);
                    oMsg.Attachments.Add(data);
                    objSMTPClient.Send(oMsg);
                    data.Dispose();
                }
            }
            else
                objSMTPClient.Send(oMsg);
            */

            if (attachment.Count() > 0)
            {
                System.Net.Mail.Attachment[] data = new System.Net.Mail.Attachment[attachment.Count()];

                for (int i = 0; i < attachment.Count(); i++)
                {
                    if (File.Exists(attachment[i]))
                    {
                        //System.Net.Mail.Attachment data;
                        data[i] = new System.Net.Mail.Attachment(attachment[i], System.Net.Mime.MediaTypeNames.Application.Octet);
                        oMsg.Attachments.Add(data[i]);                        
                    }
                }
                objSMTPClient.Send(oMsg);

                for (int j = 0; j < attachment.Count(); j++)
                    data[j].Dispose();
            }
            else
                objSMTPClient.Send(oMsg);


            oMsg = null;
        }
        catch (System.Exception ex)
        {
            oMsg = null;
            return ex.Message;
        }
            
        return "";
    }

    public static void SetReadOnly(Page sender, string RoleID, string menucode)
    {
        Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        
        try
        {
            conn.QueryString = "exec SP_MENU_READONLY_CONTROL '" + menucode + "','" + RoleID + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() == 0)
                return;

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                try
                {
                    System.Web.UI.Control c = (Control)sender.FindControl(conn.GetFieldValue(i, "CONTROL_ID").ToString());

                    switch (c.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.DropDownList": ((DropDownList)c).Enabled = false; break;
                        case "System.Web.UI.WebControls.ListBox"    : ((ListBox)c).Enabled = false; break;
                        case "System.Web.UI.WebControls.TextBox"    : ((TextBox)c).Visible = false; break;
                        case "System.Web.UI.WebControls.Button"     : ((Button)c).Visible = false; break;
                        case "System.Web.UI.WebControls.DataGrid"   : ((DataGrid)c).Enabled = false; break;
                    }
                    
                }
                catch { }
            }

        }
        catch { return; }
                
    }

    public static void FileToSQL(string varFilePath, string SQL)
    {
        byte[] file;
        using (var stream = new FileStream(varFilePath, FileMode.Open, FileAccess.Read))
        {
            using (var reader = new BinaryReader(stream))
            {
                file = reader.ReadBytes((int)stream.Length);
            }
        }
        SqlConnection con = new SqlConnection(GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        con.Open();
        using (var sqlWrite = new SqlCommand(SQL, con))
        {
            sqlWrite.Parameters.Add("@File", SqlDbType.VarBinary, file.Length).Value = file;
            sqlWrite.ExecuteNonQuery();
        }
        con.Close();
    }

    public static void SQLToFile(string varFilePath, string SQL, Page page)
    {
        SqlConnection con = new SqlConnection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        con.Open();
        SqlCommand cmd = new SqlCommand(SQL, con);
        SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            byte[] fileData = (byte[])reader.GetValue(0);

            page.Response.Buffer = true;
            page.Response.Charset = "";
            page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //page.Response.ContentType = "application/pdf";
            page.Response.ContentType = "application/octet-stream";
            page.Response.AddHeader("Content-Disposition", "attachment; filename=" + varFilePath);
            page.Response.BinaryWrite(fileData);
            page.Response.Flush();
            page.Response.End();
        }

        reader.Close();
        con.Close();  
    }
    

    public static void ToCSV(DataTable table,Page page, string filename, bool header, string extrachar, string delimiter)
    {
        var result = new StringBuilder();
        if (header)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(extrachar + table.Columns[i].ColumnName + extrachar);
                result.Append(i == table.Columns.Count - 1 ? "\r\n" : delimiter);
            }
        }

        foreach (DataRow row in table.Rows)
        {
            for (int i = 0; i < table.Columns.Count; i++)
            {
                //var rowValue = row[i].ToString().Contains(",") ? string.Format("\"{0}\"", row[i].ToString()) : row[i].ToString();
                var rowValue = extrachar + row[i].ToString() + extrachar;
                result.Append(rowValue);
                result.Append(i == table.Columns.Count - 1 ? "\r\n" : delimiter);
            }
        }

        page.Response.Clear();
        page.Response.Buffer = true;

        page.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".csv");
        page.Response.Charset = "";
        page.Response.ContentType = "application/text";
        page.Response.Output.Write(result.ToString());
        page.Response.Flush();
        page.Response.End();
    }

    public static void ExportDataSetToExcel(DataTable ds, Page page, string filename, bool header)
    {
        // first let's clean up the response.object
        page.Response.Clear();
        page.Response.Charset = "";
        page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        // set the response mime type for excel
        page.Response.ContentType = "application/vnd.ms-excel";
        page.Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xls");
        string strStyle = @"<style> td { mso-number-format: ""\@""; } </style> ";
        page.Response.Write(strStyle.ToString());
        // create a string writer
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                // instantiate a datagrid
                DataGrid dg = new DataGrid();
                dg.DataSource = ds;
                dg.ShowHeader = header;
                dg.ItemStyle.BorderStyle = BorderStyle.None;
                dg.GridLines = GridLines.None;
                dg.DataBind();
                dg.RenderControl(htw);
                //page.Response.Write(@"<style>.text { mso-number-format:\@; } </style>");
                page.Response.Write(sw.ToString());
                page.Response.Flush();
                page.Response.End();
            }
        }
    }

    public static string GetStringImageURL(Connection conn, string SQL, string field)
    {
        conn.QueryString = SQL;
        conn.ExecuteQuery();
        DataTable dt;
        dt = new DataTable();
        dt = conn.GetDataTable().Copy();
        byte[] bytes = (byte[])dt.Rows[0][field];
        string base64String = "data:image/png;base64," + Convert.ToBase64String(bytes, 0, bytes.Length);

        return base64String;
    }

    public static string PopupCenterWindow(string URL, string Title, int Width, int Height)
    {
        string command = "<script language='javascript'> " +
                            "var left = (screen.width/2)-(" + Width.ToString()+ "/2); " +
                            "var top = (screen.height/2)-(" + Height.ToString()+ "/2); " +
                            "window.open('" + URL + "', '" + Title + "', 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=" + Width.ToString() + ", height=" + Height.ToString() + ", top='+top+', left='+left); " +
                            "</script>";
        return command;
    }

    public static string GetUserMgmt(string session, string mode)
    {
        string result = "";

        try
        {
            Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));


            conn.QueryString = "select " +
                                "UserID			= a.USER_CODE, " +
                                "ID_Roles		= b.ROLE_CODE, " +
                                "EmployeeName	= b.FRONT_NAME + ' ' + isnull(b.MID_NAME + ' ',' ') + b.LAST_NAME, " +
                                "RoleName		= c.DESCR, " +
                                "Email			= b.EMAIL " +
                                "from USER_LOG_HISTORY a " +
                                "inner join M_USERS b on a.USER_CODE=b.CODE " +
                                "inner join M_ROLES c on b.ROLE_CODE=c.CODE " +
                                "where a.ROWID = '" + session + "'";
            conn.ExecuteQuery();

            switch (mode)
            {
                case "UserID": result = conn.GetFieldValue("UserID").ToString(); break;
                case "ID_Roles": result = conn.GetFieldValue("ID_Roles").ToString(); break;
                case "EmployeeName": result = conn.GetFieldValue("EmployeeName").ToString(); break;
                case "RoleName": result = conn.GetFieldValue("RoleName").ToString(); break;
                case "Email": result = conn.GetFieldValue("Email").ToString(); break;
            }
        }
        catch { }

        return result;
    }

    public static string GetArsipURL(string app, string tipe, string owner1, string owner2, string owner3, string user)
    {
        string result = "";
        Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));

        try
        {
            conn.QueryString = "select VALUE from SC_GENERAL_SET " +
                                    "where " +
                                    "APP_CODE='" + app + "' " +
                                    "and PARAMETER='URL_ARCHIEVE'";
            conn.ExecuteQuery();


            result = conn.GetFieldValue("VALUE").ToString() + "?" +
                        "app=" + app + "&" +
                        "tipe=" + tipe + "&" +
                        "owner1=" + owner1 + "&" +
                        "owner2=" + owner2 + "&" +
                        "owner3=" + owner3 + "&" +
                        "user=" + user;
        }
        catch { }

        return result;
    }

    public static DataTable FTPBrowse(string ftpID, string extPath)
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("PATH");
        dt.Columns.Add("FOLDER");
        dt.Columns.Add("DATE");
        String[] List;

        try
        {
            Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
            conn.QueryString = "select * from SC_FTP where FTP_CODE = '" + ftpID + "'";
            conn.ExecuteQuery();

            string uri = conn.GetFieldValue("FTP_ADDRESS").ToString() + conn.GetFieldValue("FTP_REMOTE_FOLDER").ToString() + extPath;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + uri);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            request.Credentials = new NetworkCredential(conn.GetFieldValue("FTP_UID").ToString(), Crypto.DecryptStringAES(conn.GetFieldValue("FTP_PWD").ToString()));

            if (conn.GetFieldValue("SSL").ToString() == "1")
            {
                request.EnableSsl = true;
                ServicePoint sp = request.ServicePoint;
                sp.ConnectionLimit = 1;
            }

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string names = reader.ReadToEnd();

            reader.Close();
            response.Close();

            List = names.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < List.Count(); i++)
            {

                string[] splits = List[i].Split(new string[] { " ", }, StringSplitOptions.RemoveEmptyEntries);
                if (splits[0].Substring(0, 1) == "d")
                {
                    string date = string.Join(" ", splits[5], splits[6], splits[7]);
                    string name = string.Empty;
                    for (int j = 8; j < splits.Length; j++)
                    {
                        name = string.Join(" ", name, splits[j]);
                    }

                    dt.Rows.Add(uri, name, date);
                }

                if (splits[2] == "<DIR>")
                {
                    string name = splits[3];
                    string date = splits[0];
                    dt.Rows.Add(uri, name, date);
                }
            }

        }
        catch (System.Exception ex)
        {
            dt.Rows.Add("ERROR", ex.Message);
        }

        dt.DefaultView.Sort = "DATE ASC";
        return dt;
    }

    public static DataTable FTPBrowseSpecificFiles(string ftpID, string fullpath, string format)
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("FULLPATH");
        dt.Columns.Add("FILENAME");
        String[] List;

        try
        {
            Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
            conn.QueryString = "select * from SC_FTP where FTP_CODE = '" + ftpID + "'";
            conn.ExecuteQuery();

            string uri = fullpath;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + uri);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            request.Credentials = new NetworkCredential(conn.GetFieldValue("FTP_UID").ToString(), Crypto.DecryptStringAES(conn.GetFieldValue("FTP_PWD").ToString()));

            if (conn.GetFieldValue("SSL").ToString() == "1")
            {
                request.EnableSsl = true;
                ServicePoint sp = request.ServicePoint;
                sp.ConnectionLimit = 1;
            }

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string names = reader.ReadToEnd();

            reader.Close();
            response.Close();

            List = names.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < List.Count(); i++)
            {

                string[] splits = List[i].Split(new string[] { " ", }, StringSplitOptions.RemoveEmptyEntries);

                /*
                if (splits[0].Substring(0, 1) != "d")
                {
                    string date = string.Join(" ", splits[5], splits[6], splits[7]);
                    string name = string.Empty;
                    for (int j = 8; j < splits.Length; j++)
                    {
                        name = string.Join(" ", name, splits[j]);
                    }

                    if (name.Substring(name.Length - format.Length, format.Length) == format)
                        dt.Rows.Add(fullpath, name);
                }
                */

                if (splits[2] != "<DIR>")
                {
                    string date = splits[0];
                    string name = splits[3];
                    if (name.Substring(name.Length - format.Length, format.Length).ToUpper() == format.ToUpper())
                        dt.Rows.Add(fullpath, name);
                }
            }

        }
        catch (System.Exception ex)
        {
            dt.Rows.Add("ERROR", ex.Message);
        }

        dt.DefaultView.Sort = "FILENAME DESC";
        return dt;
    }

    public static void FTPDownloadFile(string ftpID, string SourcePath, string FileName, string DestinationPath)
    {
        Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        conn.QueryString = "select * from SC_FTP where FTP_CODE = '" + ftpID + "'";
        conn.ExecuteQuery();

        string uri = SourcePath.Trim() + FileName.Trim();

        FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + uri);
        request.Method = WebRequestMethods.Ftp.DownloadFile;
        request.Credentials = new NetworkCredential(conn.GetFieldValue("FTP_UID").ToString(), Crypto.DecryptStringAES(conn.GetFieldValue("FTP_PWD").ToString()));

        FileName = FileName.Trim();

        if (File.Exists(DestinationPath + "\\" + FileName))
            File.Delete(DestinationPath + "\\" + FileName);

        Stream reader = request.GetResponse().GetResponseStream();
        FileStream fileStream = new FileStream(DestinationPath + "\\" + FileName, FileMode.Create);

        int bytesRead = 0;
        byte[] buffer = new byte[2048];

        while (true)
        {
            bytesRead = reader.Read(buffer, 0, buffer.Length);

            if (bytesRead == 0)
                break;

            fileStream.Write(buffer, 0, bytesRead);
        }
        fileStream.Close();
        
    }

}
