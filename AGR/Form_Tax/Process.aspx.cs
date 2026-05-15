using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Tax
{
    public partial class Process : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Setup() 
        {
            conn.QueryString = "select " +
                                "THISYEAR = YEAR(GETDATE()) - SEQ + 1 " +
                                "from SC_SEQ  " +
                                "where " +
                                "SEQ < 3 " +
                                "order by " +
                                "1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select SEQ, MON = UPPER(DateName( month , DateAdd( month , SEQ , -1 ))), MTD = MONTH(GETDATE()) from SC_SEQ where SEQ <= 12 order by SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MONTH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDGR_CD();
            LB_TITLE.Text = DGR_CD.Items[0].Cells[1].Text;
            LB_CD.Text = DGR_CD.Items[0].Cells[0].Text;

            FillDGR();
        }

        protected void FillDGR_CD()
        {
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_CD.DataSource = dt;
            DGR_CD.DataBind();

            for (int i = 0; i < DGR_CD.Items.Count; i++)
            {
                Button bt = (Button)DGR_CD.Items[i].FindControl("BT_CD");
                bt.Text = DGR_CD.Items[i].Cells[1].Text;
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_TAX_YTD_PROCESS " +
                                    "'" + DDL_YEAR.SelectedValue + "'," +
                                    "'" + DDL_MONTH.SelectedValue + "'," +
                                    "'" + LB_CD.Text + "'";
            conn.ExecuteQuery(500000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");

                lbREKAPID.Text = DGR.Items[i].Cells[2].Text;
                lbREKAPID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");


                if (DGR.Items[i].Cells[12].Text == "0")
                    cb.Visible = false;

            }
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_MONTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_CD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_PROCESS_Click(object sender, EventArgs e)
        {

        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGR();
        }
        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {

        }

        protected void DGR_CD_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Button bt = (Button)e.Item.FindControl("BT_CD");
                LB_TITLE.Text = bt.Text;
                LB_CD.Text = e.Item.Cells[0].Text;
                FillDGR();
            }
        }
    }
}