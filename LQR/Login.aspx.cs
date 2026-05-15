using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Management;
using DMS.DBConnection;

namespace LQR
{
    public partial class Login : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            LB_ID.Text = Request.QueryString["ID"].ToString();

            conn.QueryString = "select top 1 " +
                                    "LOGO       = a.BIG_LOGO, " +
                                    "COMPANY	= a.NAME, " +
                                    "ICON		= a.ICON " +
                                    "from SECURITY.dbo.SC_COMPANY a ";
            conn.ExecuteQuery();

            Page.Title = conn.GetFieldValue("COMPANY").ToString();
            IMG_LOGO.ImageUrl = GlobalUse.GetStringImageURL(conn.QueryString, "LOGO", conn);

            System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
            link.Attributes.Add("type", "image/vnd.microsoft.icon");
            link.Attributes.Add("rel", "icon");
            link.Attributes.Add("href", GlobalUse.GetStringImageURL(conn.QueryString, "ICON", conn));
            Page.Header.Controls.Add(link);

            Setup();
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "FULLNAME	= UPPER(c.FULLNAME), " +
                                "PRODUCT	= UPPER(aa.PRODUCT_DESCR), " +
                                "PASSCODE   = RIGHT('00' + convert(varchar(10),DAY(c.DOB)),2) + '' + RIGHT('00' + convert(varchar(10),MONTH(c.DOB)),2) + '' + convert(varchar(10),YEAR(c.DOB)) " +
                                "from       APPLICATION_AGREEMENT a  " +
                                "inner join V_APPLICATION_MASTER aa on a.REGNO = aa.REGNO " +
                                "inner join APPLICATION_MEMBER b on a.REGNO = b.REGNO and b.MEMBER_TYPE = '3' " +
                                "inner join V_LINK_CB_MEMBER_MASTER c on b.MEMBER_ID = c.ID " +
                                "where " +
                                "a.ID = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            LB_MEMBER.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_PRODUCT.Text = conn.GetFieldValue("PRODUCT").ToString();
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select " +
                                "REGNO      = a.REGNO, " +
                                "PASSCODE   = RIGHT('00' + convert(varchar(10),DAY(c.DOB)),2) + '' + RIGHT('00' + convert(varchar(10),MONTH(c.DOB)),2) + '' + convert(varchar(10),YEAR(c.DOB)) " +
                                "from       APPLICATION_AGREEMENT a  " +
                                "inner join V_APPLICATION_MASTER aa on a.REGNO = aa.REGNO " +
                                "inner join APPLICATION_MEMBER b on a.REGNO = b.REGNO and b.MEMBER_TYPE = '3' " +
                                "inner join V_LINK_CB_MEMBER_MASTER c on b.MEMBER_ID = c.ID " +
                                "where " +
                                "a.ID = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            if (TXT_PWD.Text.Trim() == conn.GetFieldValue("PASSCODE").ToString())
            {
                Session.Remove("s");
                Session.Add("s", conn.GetFieldValue("REGNO").ToString());

                if (Request.Browser.IsMobileDevice)
                {
                    Response.Redirect("Mobile/MobileMainInfo.aspx");
                }
                else
                {
                    Response.Redirect("ApplicationFrame.aspx");
                }
            }
            else
            {
                LB_MSG.Text = "Kata sandi yang dimasukkan salah.";
            }
        }
    }
}