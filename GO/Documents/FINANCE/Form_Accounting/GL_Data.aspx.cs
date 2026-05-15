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
    public partial class GL_Data : System.Web.UI.Page
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
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE from GL_PERIOD where CLOSED_DATE is not null union all " +
                                "select min(CODE) from GL_PERIOD where CLOSED_DATE is null  " +
                                "order by CODE";
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

            FillDDLCODE();
        }

        protected void FillDDLCODE()
        {
            conn.QueryString = "select distinct b.CODE, DESCR = b.CODE + ' - ' + b.DESCR " +
                                "from GL_DATA_MASTER a " +
                                "inner join PARAM_GL_JOURNAL b on a.CODE=b.CODE " +
                                "where " +
                                "a.PERIOD='" + DDL_PERIOD.SelectedValue + "' " +
                                "order by b.CODE";
            conn.ExecuteQuery();

            DDL_CODE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CODE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_DESCR.Text.Trim() != "")
            {
                where = where + "and a.DESCR like '%" + TXT_DESCR.Text.Trim() + "%' ";
            }

            if (TXT_DOCNO.Text.Trim() != "")
            {
                where = where + "and a.PARAM_KEY like '%" + TXT_DOCNO.Text.Trim() + "%' ";
            }

            if (TXT_DATE1.Text.Trim() != "")
                where = where + " and convert(date,a.THEDATE) >= '" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_DATE2.Text.Trim() != "")
                where = where + " and convert(date,a.THEDATE) <= '" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "VOUCHERNO, " +
                                "PERIOD, " +
                                "CODE, " +
                                "PARAM_KEY, " +
                                "THEDATE = convert(varchar(20), THEDATE, 106),  " +
                                "AMOUNT = replace(convert(varchar(100),convert(money, AMOUNT),1),'.00',''), " +
                                "DESCR = LEFT(DESCR,100), " +
                                "ISCONFIRMED, " +
                                "ISPOSTED, " +
                                "CONFIRMEDBY, " +
                                "POSTEDBY " +
                                "from V_GL_DATA_MASTER a " +
                                "where " +
                                "PERIOD = '" + DDL_PERIOD.SelectedValue + "' " +
                                "and CODE = '" + DDL_CODE.SelectedValue + "' " +
                                "and ISCONFIRMED " + DDL_CONF.SelectedValue + " " +
                                "and ISPOSTED " + DDL_POST.SelectedValue + " " +
                                where +
                                "order by a.CODE,a.THEDATE";
            conn.ExecuteQuery();
            
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDETAIL = (Button)DGR.Items[i].FindControl("BT_DETAIL");
                CheckBox cbCONF = (CheckBox)DGR.Items[i].FindControl("CB_CONF");
                CheckBox cbPOST = (CheckBox)DGR.Items[i].FindControl("CB_POST");

                btDETAIL.Attributes.Add("onclick", "window.open('GL_Data_Detail.aspx?VOUCHERNO=" + DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "','JOURNAL','height=300px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                if (DGR.Items[i].Cells[9].Text == "1")
                {
                    cbCONF.Checked = true;
                    cbPOST.Checked = true;

                    cbCONF.Visible = false;
                    cbPOST.Visible = false;
                }
                else
                {   
                    cbPOST.Checked = false;

                    if (DGR.Items[i].Cells[8].Text == "0")
                    {
                        cbCONF.Checked = false;
                        cbCONF.Visible = true;
                        cbPOST.Visible = false;
                    }
                    else
                    {
                        cbCONF.Checked = true;
                        cbCONF.Visible = true;
                        cbPOST.Visible = true;
                    }
                }
            }

            conn.QueryString = "select " +
                                "CNT = COUNT(VOUCHERNO), " +
                                "AMOUNT = replace(convert(varchar(100),convert(money, SUM(AMOUNT)),1),'.00','') " +
                                "from V_GL_DATA_MASTER a " +
                                "where " +
                                "PERIOD = '" + DDL_PERIOD.SelectedValue + "' " +
                                "and CODE = '" + DDL_CODE.SelectedValue + "' " +
                                "and ISCONFIRMED " + DDL_CONF.SelectedValue + " " +
                                "and ISPOSTED " + DDL_POST.SelectedValue + " " +
                                where;
            conn.ExecuteQuery();

            LB_RECORDS.Text = "<table style='border-spacing:0px;'>" +
                                "<tr><td>Total Records</td><td>:</td><td>" + conn.GetFieldValue("CNT").ToString() + "</td></tr>" +
                                "<tr><td>Total Amount</td><td>:</td><td>" + conn.GetFieldValue("AMOUNT").ToString() + "</td></tr>" +
                                "</table>";
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {

            }
        }

        protected void CB_POSTALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {   
                CheckBox cbPOST = (CheckBox)DGR.Items[i].FindControl("CB_POST");
                if (cbPOST.Visible)
                    cbPOST.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void CB_CONFALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cbCONF = (CheckBox)DGR.Items[i].FindControl("CB_CONF");
                if (cbCONF.Visible)
                    cbCONF.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DDL_CONF_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_POST_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_CODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_PERIOD_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLCODE();
            DGR.CurrentPageIndex = 0;            
            FillDGR();
        }

    }
}