using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Accounting
{
    public partial class GL_TB : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
                FillDGRUnbalance();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE from GL_PERIOD where CLOSED_DATE is not null union all " +
                                "select min(CODE) from GL_PERIOD where CLOSED_DATE is null  " +
                                "order by CODE desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            try
            {
                conn.QueryString = "select CODE = min(CODE) from GL_PERIOD where CLOSED_BY is null";
                conn.ExecuteQuery();
                DDL_PERIOD.SelectedValue = conn.GetFieldValue("CODE").ToString();
            }
            catch { }
        }

        protected void FillDGRUnbalance()
        {
            DGR_UNBALANCE.Visible = false;
            LB_UNBALANCE.Visible = false;

            conn.QueryString = "select " +
                                "VOUCHERNO, " +
                                "CODE, " +
                                "DESCR, " +
                                "THEDATE = convert(varchar(20),THEDATE,106), " +
                                "DIFF = replace(convert(varchar(100),convert(money,DIFF),1),'.00','') " +
                                "from V_GL_DATA_UNBALANCE a " +
                                "where " +
                                "PERIOD = '" + DDL_PERIOD.SelectedValue + "' " +
                                "order by a.CODE, a.THEDATE";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                DGR_UNBALANCE.Visible = true;
                LB_UNBALANCE.Visible = true;

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_UNBALANCE.DataSource = dt;
                DGR_UNBALANCE.DataBind();

                for (int i = 0; i < DGR_UNBALANCE.Items.Count; i++)
                {
                    Button btDETAIL = (Button)DGR_UNBALANCE.Items[i].FindControl("BT_DETAIL");
                    btDETAIL.Attributes.Add("onclick", "window.open('GL_Data_Detail.aspx?VOUCHERNO=" + DGR_UNBALANCE.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "','JOURNAL','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                }
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec RPT_GL_TB '" + DDL_PERIOD.SelectedValue + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE='33'";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDETAIL = (Button)DGR.Items[i].FindControl("BT_DETAIL");
                string URL = conn.GetFieldValue("URL").ToString() + "&rc:Zoom=Page%20Width&CODE=&COA=" + DGR.Items[i].Cells[0].Text + "&PERIOD=" + DDL_PERIOD.SelectedValue;
                btDETAIL.Attributes.Add("onclick", "window.open('" + URL + "','JOURNAL','height=600px,width=1000px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }            
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "select " +
                                    "DEBET = replace(convert(varchar(100),convert(money,ROUND(SUM(DEBET),0)),1),'.00',''), " +
                                    "CREDIT = replace(convert(varchar(100),convert(money,ROUND(SUM(CREDIT),0)),1),'.00','') " +
                                    "from V_GL_TB " +
                                    "where " +
                                    "PERIOD = '" + DDL_PERIOD.SelectedValue + "'";
                conn.ExecuteQuery();

                e.Item.Cells[2].Text = conn.GetFieldValue("DEBET").ToString();
                e.Item.Cells[3].Text = conn.GetFieldValue("CREDIT").ToString();
            }
        }

        protected void DDL_PERIOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
        
        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL = URL + '&rc:Parameters=False&rs:Format=EXCEL&PERIOD=' from V_LINK_SC_REPORT_LIST where CODE='46'";
            conn.ExecuteQuery();
            Response.Redirect(conn.GetFieldValue("URL").ToString() + DDL_PERIOD.SelectedValue);
        }
    }
}