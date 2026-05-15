using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_Tools
{
    public partial class Track : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected int track;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGR(Request.QueryString["REGNO"].ToString(), Request.QueryString["TRACK_TYPE"].ToString(), Request.QueryString["PARAM_VALUE"].ToString());
            }
        }

        protected void FillDGR(string REGNO, string TRACK_TYPE, string PARAM_VALUE)
        {

            if (PARAM_VALUE == "")
            {
                conn.QueryString = "exec SP_APPLICATION_TRACK '" + REGNO + "','" + TRACK_TYPE + "','" + PARAM_VALUE + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR.DataSource = dt;
                DGR.DataBind();

                conn.QueryString = "exec SP_APPLICATION_TRACK '" + REGNO + "','POS',1";
                //conn.QueryString = "exec SP_APPLICATION_TRACK_PARAM_VALUE '" + REGNO + "','POS',1";
                conn.ExecuteQuery();
                DataTable dt1;
                dt1 = new DataTable();
                dt1 = conn.GetDataTable().Copy();
                DGR1.DataSource = dt1;
                DGR1.DataBind();

                conn.QueryString = "select  b.SEQ," +
                             "a.DESCR, " +
                             "a.LASTCHANGEBY + ',' +  FORMAT(b.PENDINGDATE, 'dd MMM yyyy HH:mm:ss') as USERBY " +                           
                             "from PARAM_PENDING_TYPE a " +
                             "left join APPLICATION_MASTER_PENDING b on a.CODE = b.PENDING_CODE and b.REGNO = '" + REGNO + "' and b.SEQ = 1 " +
                             "where " +
                             "a.PROCESS_CODE = 'POS'";
                conn.ExecuteQuery();

                DataTable dt2;
                dt2 = new DataTable();
                dt2 = conn.GetDataTable().Copy();
                DGR2.DataSource = dt2;
                DGR2.DataBind();



                DataTable dtTitle = new DataTable();

                // Add all columns that exist in your DataGrid
                dtTitle.Columns.Add("ENDORSEMENT_TYPE_DESCR");
                dtTitle.Columns.Add("STATUS");
                dtTitle.Columns.Add("REG_DATE");
                dtTitle.Columns.Add("PENDINGDATE");
                dtTitle.Columns.Add("CLOSINGDATE");
                dtTitle.Columns.Add("VERIFYDATE");
                dtTitle.Columns.Add("AUTHORIZEDATE");
                dtTitle.Columns.Add("REMARK");

                dtTitle.Rows.Add(dtTitle.NewRow());   // Add one dummy row

                DGR3.DataSource = dtTitle;
                DGR3.DataBind();

                // Hide the dummy row so only header appears
                //if (DGR3.Items.Count > 0)
                //    DGR3.Items[0].Visible = false;



                conn.QueryString = " select  a.ENDORSEMENT_TYPE_DESCR," +
                  "(select DESCR  from  [dbo].[PARAM_TRACK] WHERE TIPE_CODE='POS' AND SEQ=a.LAST_TRACK) AS STATUS,a.REG_DATE," +
                  "(select  top 1   amp.PENDINGDATE  from PARAM_PENDING_TYPE ppt  left join APPLICATION_MASTER_PENDING amp on ppt.CODE = amp.PENDING_CODE  where   ppt.CODE='005'  and amp.REGNO = a.regno) AS PENDINGDATE, " +
                  "(select  top 1   amp.CLOSINGDATE  from PARAM_PENDING_TYPE ppt  left join APPLICATION_MASTER_PENDING amp on ppt.CODE = amp.PENDING_CODE  where   ppt.CODE='005'  and amp.REGNO = a.regno) AS CLOSINGDATE, " +
                  " '' AS VERIFYDATE, " +
                  " convert(varchar(20),  (case   when a.LAST_TRACK = 4 then aa.TRACK4_DATE  when a.LAST_TRACK = 5 then aa.TRACK5_DATE    else null end), 106)  AS AUTHORIZEDATE, " +
                  "(select  top 1 AER.REMARK from PR_POS_REMARK_TYPE PPR left join APPLICATION_ENDORSEMENT_REMARK AER on PPR.CODE = AER.REMARK_TYPE where  AER.REGNO = a.regno) as REMARK " +
                  " from V_APPLICATION_ENDORSEMENT_PARENT a " +
                  "inner join  APPLICATION_TRACK aa on a.REGNO = aa.REGNO and aa.TRACK_TYPE = 'POS' and aa.PARAM_VALUE = convert(varchar(10), a.SEQ) " +
                  "where a.regno =  '" + REGNO + "'"; 
                 
                conn.ExecuteQuery();

                DataTable dt3;
                dt3 = new DataTable();
                dt3 = conn.GetDataTable().Copy();
                DGR3.DataSource = dt3;
                DGR3.DataBind();

            }
            else
            {
                conn.QueryString = "exec SP_APPLICATION_TRACK '" + REGNO + "','" + TRACK_TYPE + "','" + PARAM_VALUE + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR.DataSource = dt;
                DGR.DataBind();
            }





        }

    }
}