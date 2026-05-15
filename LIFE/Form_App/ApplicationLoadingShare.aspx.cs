using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_App
{
    public partial class ApplicationLoadingOtherSetup : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                Setup();
                ShowCharges();

                if (GlobalUse.GetTrack(LB_REGNO.Text, "UW", "") < 3)
                {
                    BT_COMMISSION.Visible = false;
                    BT_LOADING.Visible = false;
                }
            }
        }


        protected void Setup()
        {
            conn.QueryString = "select " +
                                "a.PRODUCT_CODE, "+
                                "b.TC_ID " +
                                "from           APPLICATION_MASTER a " +
                                "inner join     APPLICATION_TC b on a.REGNO = b.REGNO " +
                                "where " +
                                "a.REGNO = '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                LB_PRODUCT_CODE.Text = conn.GetFieldValue("PRODUCT_CODE").ToString();
                LB_TC.Text = conn.GetFieldValue("TC_ID").ToString();
            }
        }
        
        protected void BT_COMMISSION_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            IF.Attributes.Remove("onload");
            IF.Attributes.Add("onload", " resizeIframe(this)");
            IF.Src = "../../ReportViewer/Viewer.aspx?APPID=LF&CODE=4&REGNO=" + LB_REGNO.Text;
        }

        protected void BT_LOADING_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            IF.Attributes.Remove("onload");
            IF.Attributes.Add("onload", " resizeIframe(this)");
            IF.Attributes.Add("src", "../../ReportViewer/Viewer.aspx?APPID=UW&CODE=1&PRODUCT_CODE=" + LB_PRODUCT_CODE.Text + "&TC_ID=" + LB_TC.Text);
        }

        protected void ShowCharges()
        {
            LB_TITLE.Text = BT_CHARGES.Text;
            IF.Attributes.Remove("onload");
            IF.Attributes.Add("onload", " resizeIframe(this)");
            IF.Attributes.Add("src", "../../ReportViewer/Viewer.aspx?APPID=UW&CODE=2&PRODUCT_CODE=" + LB_PRODUCT_CODE.Text + "&TC_ID=" + LB_TC.Text);
        }

        protected void BT_CHARGES_Click(object sender, EventArgs e)
        {
            ShowCharges();
        }
    }
}