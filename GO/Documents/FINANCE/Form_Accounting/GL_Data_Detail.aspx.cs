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
    public partial class GL_Data_Detail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_VOUCHERNO.Text = Request.QueryString["VOUCHERNO"];
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "VOUCHERNO, " +
                                "PERIOD, " +
                                "CODE_DESCR = a.CODE + ' - ' + b.DESCR, " +
                                "PARAM_KEY, " +
                                "THEDATE = convert(varchar(20),a.THEDATE,106), " +
                                "a.DESCR, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
                                "CONFIRMEDBY, " +
                                "POSTEDBY " +
                                "from V_GL_DATA_MASTER a " +
                                "inner join PARAM_GL_JOURNAL b on a.CODE=b.CODE " +
                                "where VOUCHERNO = '" + LB_VOUCHERNO.Text + "'";
            conn.ExecuteQuery();

            LB_PERIOD.Text = conn.GetFieldValue("PERIOD").ToString();
            LB_PARAMKEY.Text = conn.GetFieldValue("PARAM_KEY").ToString();
            LB_DESCRIPTION.Text = conn.GetFieldValue("DESCR").ToString();
            LB_DATE.Text = conn.GetFieldValue("THEDATE").ToString();
            LB_CODE.Text = conn.GetFieldValue("CODE_DESCR").ToString();
            LB_CONF.Text = conn.GetFieldValue("CONFIRMEDBY").ToString();
            LB_POST.Text = conn.GetFieldValue("POSTEDBY").ToString();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.COA, " +
                                "b.DESCR,  " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
                                "T00, T01, T02, T03, T04, T05, T06, T07, T08, T09 " +
                                "from GL_DATA a " +
                                "inner join PARAM_GL_COA b on a.COA=b.COA " +
                                "where " +
                                "DC = 'D' and VOUCHERNO = '" + LB_VOUCHERNO.Text + "' order by a.COA";
            conn.ExecuteQuery();
            DGR_D.DataSource = conn.GetDataTable().Copy();
            DGR_D.DataBind();

            conn.QueryString = "select " +
                                "a.COA, " +
                                "b.DESCR,  " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,a.AMOUNT),1),'.00',''), " +
                                "T00, T01, T02, T03, T04, T05, T06, T07, T08, T09 " +
                                "from GL_DATA a " +
                                "inner join PARAM_GL_COA b on a.COA=b.COA " +
                                "where " +
                                "DC = 'C' and VOUCHERNO = '" + LB_VOUCHERNO.Text + "' order by a.COA";
            conn.ExecuteQuery();
            DGR_C.DataSource = conn.GetDataTable().Copy();
            DGR_C.DataBind();
        }
    }
}