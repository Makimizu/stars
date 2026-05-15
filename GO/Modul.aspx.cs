using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace GO
{
    public partial class Modul : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_SES.Text = Request.QueryString["s"];
                SessionCheck(LB_SES.Text);
                Setup(LB_SES.Text);
                SetHeader();
            }

        }

        protected void SessionCheck(string s)
        {
            try
            {
                conn.QueryString = "select * from USER_LOG_HISTORY where ROWID='" + s + "' and LOGOUT is null";
                conn.ExecuteQuery();

                if (conn.GetRowCount() == 0)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            catch
            {
                Response.Redirect("Login.aspx");
            }

        }

        protected void SetHeader()
        {
            conn.QueryString = "select top 1 " +
                                    "COMPANY	= a.NAME, " +
                                    "ICON		= a.ICON " +
                                    "from SC_COMPANY a ";
            conn.ExecuteQuery();
            Page.Title = conn.GetFieldValue("COMPANY").ToString();

            System.Web.UI.HtmlControls.HtmlLink link = new System.Web.UI.HtmlControls.HtmlLink();
            link.Attributes.Add("type", "image/vnd.microsoft.icon");
            link.Attributes.Add("rel", "icon");
            link.Attributes.Add("href", GlobalUse.GetStringImageURL(conn.QueryString, "ICON"));
            Header.Controls.Add(link);
        }

        protected void Setup(string session)
        {
            try
            {
                string SQL = "select " +
                                    "NAME = UPPER(ISNULL(a.FRONT_NAME,'') + REPLACE(' ' + ISNULL(MID_NAME,'') + ' ','  ',' ') + ISNULL(a.LAST_NAME,'')), " +
                                    "a.ROLE_CODE, " +
                                    "PHOTO = isnull(a.PHOTO,(select top 1 DEFAULT_PROFILE_PIC from SC_COMPANY)) " +
                                    "from M_USERS a " +
                                    "inner join USER_LOG_HISTORY b on a.CODE=b.USER_CODE " +
                                    "where " +
                                    "b.ROWID='" + session + "'";
                conn.QueryString = SQL;
                conn.ExecuteQuery();

                LB_UID.Text = "Welcome <B>" + conn.GetFieldValue("NAME").ToString() + "</B>";
                LB_GROUP.Text = conn.GetFieldValue("ROLE_CODE").ToString();

                FillGrid();

                IMG1.ImageUrl = GlobalUse.GetStringImageURL(SQL, "PHOTO");
                IMG1.Height = 100;
            }
            catch
            {
                return;
            }
        }

        protected void FillGrid()
        {
            conn.QueryString = "select * from V_ROLE_APPS where ROLE_CODE='" + LB_GROUP.Text + "' order by APP_NAME";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 1)
            {
                string URL = conn.GetFieldValue("PATH").ToString() + "?s=" + LB_SES.Text;
                Response.Redirect(URL);
            }

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                Button btApp = (Button)DGR.Items[i].FindControl("BT_APP");
                btApp.Text = DGR.Items[i].Cells[1].Text;
            }
        }

        protected void DGR1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Go")
            {
                string URL = e.Item.Cells[2].Text + "?s=" + LB_SES.Text;
                Response.Redirect(URL);
            }

            if (e.CommandName == "Logout")
            {
                Response.Redirect("Logout.aspx");
            }
        }
    }
}