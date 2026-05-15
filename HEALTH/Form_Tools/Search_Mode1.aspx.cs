using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Tools
{
    public partial class Search_Mode1 : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB1.Text = Request.QueryString["field1"];
                LB2.Text = Request.QueryString["field2"];
                LB21.Text = Request.QueryString["field3"];
                LB22.Text = Request.QueryString["field4"];
                LB3.Text = Request.QueryString["tablename"];
                LB4.Text = Request.QueryString["target"];
                LB5.Text = Request.QueryString["parent"];
                callbak.Text = Request.QueryString["callback"];

                //FillGrid();
            }
        }

        protected void FillGrid()
        {
            string Fields = "";
            if (LB1.Text != "")
                Fields = LB1.Text;
            if (LB2.Text != "")
                Fields += "," + LB2.Text;
            if (LB21.Text != "")
                Fields += "," + LB21.Text;
            if (LB22.Text != "")
                Fields += "," + LB22.Text;

            string sql = "select " + Fields + " from " + LB3.Text + " where " + LB2.Text + " like '%" + TXT_CARI.Text + "%' and " + LB1.Text + " like '%" + TXT_CODE.Text.Trim() + "%' ";

            sql = sql + " order by " + LB2.Text;

            try
            {
                conn.QueryString = sql;
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR1.DataSource = dt;
                DGR1.DataBind();
            }
            catch { }
        }

        protected void DGR1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                //Response.Write("<script language='javascript'> window.opener.document.forms[0]." +LB4.Text+ ".value = '" + e.Item.Cells[1].Text + "'; window.close(); </script>");

                string mode = "";
                switch (LB5.Text)
                {
                    case "0": mode = "forms[0]"; break;
                    case "1": mode = "form1"; break;
                }


                string callback = "";
                if (callbak.Text != "")
                    callback = "window.opener.document." + mode + "." + callbak.Text + ";";

                string script = "<script language='javascript'> " +
                                    "window.opener.document." + mode + "." + LB4.Text + ".value = '" + e.Item.Cells[1].Text + "'; " +
                                    callback +
                                    "window.close(); " +
                                "</script>";
                Response.Write(script);
            }
        }
        protected void BT_CARI_Click(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}