using DMS.DBConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using System.Net;
using System.Data.SqlClient;

namespace AGR.WebService
{
    /// <summary>
    /// Summary description for AgentRegister
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AgentRegister : System.Web.Services.WebService
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        [WebMethod]
        public void UploadData(string codeAgent, string fullName, string dob, string pob, string phone, string email, string referal, string gender, string joindate, string accno, string accbank, string accname, string nik, string address, string zipcode, string npwp, string fileFoto, string fileKtp, string fileNpwp, string fileRekening, string filePojk)
        {
            string json = "";
            try
            {
                conn.QueryString = "exec SP_M_AGENT_UPSERT_TAIS " +
                                                    "'" + codeAgent + "'," +
                                                    "'" + fullName + "'," +
                                                    "'" + dob + "'," +
                                                    "'" + pob + "'," +
                                                    "'" + phone + "'," +
                                                    "'" + email + "'," +
                                                    "'" + referal + "'," +
                                                    "'" + joindate + "'," +
                                                    "'" + gender + "'," +
                                                    "'" + accno + "'," +
                                                    "'" + accbank + "'," +
                                                    "'" + accname + "'," +
                                                    "'" + nik + "'," +
                                                    "'" + address + "'," +
                                                    "'" + zipcode + "'," +
                                                    "'" + npwp + "'";
                conn.ExecuteQuery();

                LoadDocument(fileFoto, codeAgent, "FOTO", "000");
                LoadDocument(fileKtp, codeAgent, "KTP", "001");
                LoadDocument(fileNpwp, codeAgent, "NPWP", "002");
                LoadDocument(fileRekening, codeAgent, "BUKU REKENING", "006");
                LoadDocument(filePojk, codeAgent, "KARTU/SURAT PERIJINAN", "003");

                json = "{" +
                    "\"stat\":\"0\"," +
                    "\"Msg\":\"" + "success" + "\"" +
                    "}";
            }
            catch (Exception ex)
            {
                json = "{" +
                        "\"stat\":\"1\"," +
                        "\"Msg\":\"" + ex.Message + "\"" +
                        "}";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", json.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(json);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void LoadDocument(string fileName, string codeAgent, string remark, string fileCode)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string dir = "C:\\tmp\\" + fileName;
            //download local stars
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://taisnexdev.takaful.com/formulir-rekrut/documents/" + fileName, dir);

            //filetoSQL
            conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
            conn.ExecuteQuery();

            string code = conn.GetFieldValue(0, 0).ToString();

            string SQL = "insert into ARCHIEVE.dbo.AGR_ARSIP values (" +
                                "'" + code + "'," +
                                "'AGR_1'," +
                                "'" + codeAgent + "'," +
                                "'" + fileCode + "'," +
                                "null," +
                                "'" + remark + "'," +
                                "'" + fileName + "'," +
                                "'system',GetDate()," +
                                "'system',GetDate()," +
                                "@File)";

            GlobalUse.FileToSQL(dir, SQL);

            if (File.Exists(dir))
                File.Delete(dir);
        }
    }
}
