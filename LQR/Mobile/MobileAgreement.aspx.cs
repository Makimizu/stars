using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Net;
using System.IO;
using ZeroDep;

namespace LQR.Mobile
{
    public partial class MobileAgreement : System.Web.UI.Page
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
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../SessionExpired.aspx");
                }

                LB_REGNO.Text = Session["s"].ToString();
                FillDGR();
                Setup();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_AGREEMENT_STATEMENT '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void Setup()
        {
            conn.QueryString = "select REGNO,SIGNATURE from APPLICATION_AGREEMENT where AGREEMENT_DATE is not null and REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                BT_APPROVE.Visible = BT_CLEAR.Visible = false;
                try
                {
                    IMG_SIGNATURE.ImageUrl = conn.GetFieldValue("SIGNATURE").ToString();
                    IMG_SIGNATURE.Visible = true;
                    DV_CANVAS.Visible = false;
                }
                catch { }
            }
            else
            {
                BT_APPROVE.Attributes.Add("onclick", "if(!confirm('ANDA YAKIN UNTUK MENYETUJUI ?')){return false;}else{getImageData(); return true;}");
            }

            conn.QueryString = "select FULLNAME from V_APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            LB_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();

            try
            {
                //LB_LOCATION.Text = Request.ServerVariables["REMOTE_ADDR"];
                //LB_LOCATION.Text = GetIp();
                string json = string.Empty;
                string url = @"https://ipwho.is/" + GetIp();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }

                JsonParse(json);
            }
            catch { }
        }

        protected void BT_APPROVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "update APPLICATION_AGREEMENT set " +
                                "AGREEMENT_DATE = GETDATE(), " +
                                "SIGNATURE = '" + TXT_SIGNATURE.Text + "'" +
                                "where REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();

            try
            {
                conn.QueryString = "exec SP_APPLICATION_QUOTATION_ARCHIEVING '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
            }
            catch { }

            Setup();
        }

        protected string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (ip.IndexOf(',') > 0)
            {
                string[] nodes = ip.Split(',');
                ip = nodes[0];
            }
            return ip;
        }

        protected void JsonParse(string jsonString)
        {
            string city = string.Empty;
            string country = string.Empty;

            try
            {
                //var listObject = Json.Deserialize(jsonString);
                var listObject = (Dictionary<string, object>)Json.Deserialize(jsonString);
                city = listObject["city"].ToString();
                country = listObject["country"].ToString();

                if (city != "")
                    LB_LOCATION.Text = city + ", " + country;
            }
            catch { }
        }

        protected void BT_AGREEMENT_Click(object sender, EventArgs e)
        {
            TR_AGREEMENT.Visible = true;
            TR_SIGN.Visible = false;
        }

        protected void BT_SIGN_Click(object sender, EventArgs e)
        {
            TR_AGREEMENT.Visible = false;
            TR_SIGN.Visible = true;
        }
    }
}