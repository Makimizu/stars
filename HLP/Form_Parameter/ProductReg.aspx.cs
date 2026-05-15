using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HLP.Form_Parameter
{
    public partial class ProductReg : System.Web.UI.Page
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
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                BT_SAVE.Attributes.Add("onclick", "if(!confirm('Are you sure to SAVE ?')){return false;};");
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_CODE.Text.Trim() == "" || TXT_PRODUCT.Text.Trim() == "" || TXT_TGLSTART.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "insert into PARAM_PRODUCT select " +
                                    "'" + TXT_CODE.Text.Trim() + "'," +
                                    "'" + TXT_PRODUCT.Text.Trim().Replace("'", "`") + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_TGLSTART.Text.Trim(), "d/M/yyyy") + "'," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()";

                conn.ExecuteNonQuery();


                Response.Redirect("ProductFrame.aspx?code=" + TXT_CODE.Text.Trim());
            }
            catch { }
        }
    }
}