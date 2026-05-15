using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DMS.DBConnection;
//using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;

namespace LQR.WebService
{
    /// <summary>
    /// Summary description for ApplicationRegnoService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class ApplicationRegnoService : System.Web.Services.WebService
    {

        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        [WebMethod]
        public void GetRegnoVA(string prefix)
        {
            string json = "";

            try
            {
                conn.QueryString = "exec SP_SVC_GET_REGNO_VA " +
                                    "'" + prefix + "'";
                conn.ExecuteQuery();
                if (conn.GetRowCount() > 0)
                {
                    json = "{" +
                        "\"stat\":\"1\"," +
                        "\"regno\":\""+ conn.GetFieldValue("REGNO").ToString() +"\"," +
                        "\"va\":\"" + conn.GetFieldValue("VACC_NO").ToString() + "\"," +
                        "\"errMsg\":\"\"" +
                        "}";
                    /*
                    json = JsonConvert.SerializeObject(new
                    {
                        stat = "1",
                        code = conn.GetFieldValue("CODE").ToString(),
                        fullName = conn.GetFieldValue("FULLNAME").ToString(),
                        gender = conn.GetFieldValue("GENDER").ToString(),
                        dob = conn.GetFieldValue("DOB").ToString(),
                        address = conn.GetFieldValue("ADDRESS").ToString(),
                        phone = conn.GetFieldValue("PHONE").ToString(),
                        idNo = conn.GetFieldValue("ID_NO").ToString(),
                        taxNo = conn.GetFieldValue("TAX_NO").ToString(),
                        provinceCode = conn.GetFieldValue("PROVINCE_CODE").ToString(),
                        cityCode = conn.GetFieldValue("CITY_CODE").ToString(),
                        areaCode = conn.GetFieldValue("AREA_CODE").ToString(),
                        errMsg = ""
                    });
                    */
                }
                else
                {
                    /*
                    json = JsonConvert.SerializeObject(new
                    {
                        stat = "0",
                        errMsg = "User Not Found"
                    });
                    */
                    json = "{" +
                        "\"stat\":\"0\"," +
                        "\"errMsg\":\"Invalid Prefix or REGNO not found.\"" +
                        "}";
                }
            }
            catch (Exception ex)
            {
                /*
                json = JsonConvert.SerializeObject(new
                {
                    stat = "0",
                    errMsg = ex.Message
                });
                */
                json =  "{" +
                        "\"stat\":\"0\"," +
                        "\"errMsg\":\""+ ex.Message + "\"" + 
                        "}";
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.AddHeader("content-length", json.Length.ToString());
            Context.Response.Flush();
            Context.Response.Write(json);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
