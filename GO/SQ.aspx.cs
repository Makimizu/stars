using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;
using System.Net;
using System.Net.Sockets;

namespace GO
{
    public partial class SQ : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //LB_ERROR.Text = GetIPAddress();
                    //Setup();
                }
            }
            catch (Exception ex)
            {
                //LB_ERROR.Text = ex.Message;

                throw;
            }
            
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"] + " - " + context.Request.ServerVariables["REMOTE_USER"];
        }

        protected void Setup()
        {
            //BT_XLS.Visible = false;

            //conn.QueryString = "select APP_CODE, APP_DBNAME from V_CONSTRING order by 2";
            //conn.ExecuteQuery();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_DB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}

            //FIllLB_OBJECT();
        }

        protected void FIllLB_OBJECT()
        {
            //string constr = "";
            //conn.QueryString = "select CONSTR, APP_DBPWD from V_CONSTRING where APP_CODE = '" + DDL_DB.SelectedValue + "'";
            //conn.ExecuteQuery();

            //constr = conn.GetFieldValue("CONSTR").ToString() + Crypto.DecryptStringAES(conn.GetFieldValue("APP_DBPWD").ToString());

            //try
            //{
            //    Connection conn2 = new Connection(constr);
            //    conn2.QueryString = "select name from sysobjects where xtype='" + DDL_OBJECT.SelectedValue + "' order by 1";
            //    conn2.ExecuteQuery();

            //    LB_OBJECT.Items.Clear();
            //    for (int i = 0; i < conn2.GetRowCount(); i++)
            //    {
            //        LB_OBJECT.Items.Add(new ListItem(conn2.GetFieldValue(i, 0).ToString(), conn2.GetFieldValue(i, 0).ToString()));
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    LB_ERROR.Text = "<BR>" + ex.Message;
            //}
        }

        protected void BT_SQL_Click(object sender, EventArgs e)
        {
            //DGR.CurrentPageIndex = 0;
            //FillDGR();
        }

        protected void FillDGR()
        {
            //BT_XLS.Visible = false;
            //LB_ERROR.Text = "";
            //LB_RESULT.Text = "";
            //string constr = "";
            //conn.QueryString = "select CONSTR, APP_DBPWD from V_CONSTRING where APP_CODE = '" + DDL_DB.SelectedValue + "'";
            //conn.ExecuteQuery();

            //constr = conn.GetFieldValue("CONSTR").ToString() + Crypto.DecryptStringAES(conn.GetFieldValue("APP_DBPWD").ToString());

            //try
            //{
            //    Connection conn2 = new Connection(constr);
            //    conn2.QueryString = TXT_SQL.Text.Trim();
            //    conn2.ExecuteQuery(50000);

            //    if (conn2.GetRowCount() > 0)
            //    {
            //        LB_RESULT.Text = "Records : " + conn2.GetRowCount().ToString();
            //        BT_XLS.Visible = true;
            //    }

            //    DataTable dt;
            //    dt = new DataTable();
            //    dt = conn2.GetDataTable().Copy();
            //    DGR.DataSource = dt;
            //    DGR.DataBind();
            //}
            //catch (System.Exception ex)
            //{
            //    LB_ERROR.Text = "<BR>" + ex.Message;
            //}
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            //BT_XLS.Visible = false;
            //LB_ERROR.Text = "";
            //LB_RESULT.Text = "";
            //string constr = "";
            //conn.QueryString = "select CONSTR, APP_DBPWD from V_CONSTRING where APP_CODE = '" + DDL_DB.SelectedValue + "'";
            //conn.ExecuteQuery();

            //constr = conn.GetFieldValue("CONSTR").ToString() + Crypto.DecryptStringAES(conn.GetFieldValue("APP_DBPWD").ToString());

            //try
            //{
            //    Connection conn2 = new Connection(constr);
            //    conn2.QueryString = TXT_SQL.Text.Trim();
            //    conn2.ExecuteQuery(50000);

            //    if (conn2.GetRowCount() == 0)
            //        return;

            //    DataTable dt;
            //    dt = new DataTable();
            //    dt = conn2.GetDataTable().Copy();
            //    GlobalUse.ExportDataSetToExcel(dt, this, "RESULT", true);
            //}
            //catch (System.Exception ex)
            //{
            //    LB_ERROR.Text = "<BR>" + ex.Message;
            //}
        }

        protected void DDL_OBJECT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FIllLB_OBJECT();
        }

        protected void DDL_DB_SelectedIndexChanged(object sender, EventArgs e)
        {
            FIllLB_OBJECT();
        }

        protected void BT_VIEW_Click(object sender, EventArgs e)
        {
            //string constr = "";
            //conn.QueryString = "select CONSTR, APP_DBPWD from V_CONSTRING where APP_CODE = '" + DDL_DB.SelectedValue + "'";
            //conn.ExecuteQuery();

            //constr = conn.GetFieldValue("CONSTR").ToString() + Crypto.DecryptStringAES(conn.GetFieldValue("APP_DBPWD").ToString());

            //try
            //{
            //    Connection conn2 = new Connection(constr);

            //    if (DDL_OBJECT.SelectedValue != "U")
            //    {
            //        conn2.QueryString = "select text from syscomments where object_name(id) = '" + LB_OBJECT.SelectedValue + "'";
            //        conn2.ExecuteQuery();
            //        TXT_SQL.Text = conn2.GetFieldValue(0, 0).ToString();
            //    }
            //    else
            //    {
            //        conn2.QueryString = "select * from syscolumns where object_name(id) = '" + LB_OBJECT.SelectedValue + "'";
            //        conn2.ExecuteQuery();

            //        if (conn2.GetRowCount() > 0)
            //        {
            //            LB_RESULT.Text = "Columns of " + LB_OBJECT.Text + " :";
            //            BT_XLS.Visible = true;
            //        }

            //        DataTable dt;
            //        dt = new DataTable();
            //        dt = conn2.GetDataTable().Copy();
            //        DGR.DataSource = dt;
            //        DGR.DataBind();
            //    }
            //}
            //catch (System.Exception ex)
            //{
            //    LB_ERROR.Text = "<BR>" + ex.Message;
            //}
        }

        protected void BT_PICK_Click(object sender, EventArgs e)
        {
            //TXT_SQL.Text = TXT_SQL.Text + LB_OBJECT.SelectedValue;
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            //DGR.CurrentPageIndex = e.NewPageIndex;
            //FillDGR();
        }
    }
}