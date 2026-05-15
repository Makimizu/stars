using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_POS
{
    public partial class HistoryList : System.Web.UI.Page
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
            conn.QueryString = "exec SP_LINK_UB_ENDORSEMENT_LIST";
            conn.ExecuteQuery(15000000);
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, "ENDORSEMENT_DESCR").ToString(), conn.GetFieldValue(i, "ENDORSEMENT_CODE").ToString()));
            }

            conn.QueryString = "select " +
                                "FOM		= convert(varchar(20), convert(date, convert(varchar(6), GETDATE(), 112) + '01'), 103), " +
                                "EOM		= convert(varchar(20), EOMONTH(GETDATE()), 103)";
            conn.ExecuteQuery(15000000);
            TXT_DATE1.Text = conn.GetFieldValue("FOM").ToString();
            TXT_DATE2.Text = conn.GetFieldValue("EOM").ToString();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            //string where = "";

            //if (TXT_REGNO.Text.Trim() != "")
            //    where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            //if (TXT_FULLNAME.Text.Trim() != "")
            //    where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            //if (TXT_PRODUCT.Text.Trim() != "")
            //    where = where + " and TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            //if (TXT_POLICYNO.Text.Trim() != "")
            //    where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            //if (TXT_DATE1.Text.Trim() != "")
            //    where = where + " and convert(date, (case when a.LAST_TRACK = 4 then aa.TRACK4_DATE when a.LAST_TRACK = 5 then aa.TRACK5_DATE else null end)) >= '" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "' ";

            //if (TXT_DATE2.Text.Trim() != "")
            //    where = where + " and convert(date, (case when a.LAST_TRACK = 4 then aa.TRACK4_DATE when a.LAST_TRACK = 5 then aa.TRACK5_DATE else null end)) <= '" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "' ";

            //if (DDL_TYPE.SelectedValue != "")
            //    where = where + " and a.ENDORSEMENT_TYPE_DESCR like '%" + DDL_TYPE.SelectedItem.Text + "%' ";

            //if (DDL_UNITIZE.SelectedValue != "")
            //    where = where + " and pg.UNITIZE = " + DDL_UNITIZE.SelectedValue + " ";
            string whereOuter = "";

            if (TXT_REGNO.Text.Trim() != "")
                whereOuter += " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                whereOuter += " and a.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                whereOuter += " and a.PRODUCT_NAME like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                whereOuter += " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_DATE1.Text.Trim() != "")
                whereOuter += " and convert(date, (case when a.LAST_TRACK = 4 then a.TRACK4_DATE when a.LAST_TRACK = 5 then a.TRACK5_DATE end)) >= '" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_DATE2.Text.Trim() != "")
                whereOuter += " and convert(date, (case when a.LAST_TRACK = 4 then a.TRACK4_DATE when a.LAST_TRACK = 5 then a.TRACK5_DATE end)) <= '" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_TYPE.SelectedValue != "")
                whereOuter += " and a.ENDORSEMENT_TYPE_DESCR like '%" + DDL_TYPE.SelectedItem.Text + "%' ";

            if (DDL_UNITIZE.SelectedValue != "")
                whereOuter += " and a.PRODUCT_GROUP is not null "; // opsional, karena pg sudah join

            conn.QueryString = @"
WITH DATA AS (
    SELECT 
        a.REGNO,
        a.SEQ,
        a.FULLNAME,
        a.ENDORSEMENT_TYPE_DESCR,
        a.DOB,
        a.PRODUCT_NAME,
        a.PRODUCT_GROUP,
        a.POLICY_NO,
        a.LAST_TRACK,

        aa.TRACK4_DATE,
        aa.TRACK5_DATE,
        aa.TRACK4_BY,
        aa.TRACK5_BY,

        CASE WHEN b.REGNO IS NOT NULL THEN 1 ELSE 0 END AS ENABLE_ROLLBACK,

        ROW_NUMBER() OVER (
            PARTITION BY a.REGNO
            ORDER BY a.SEQ DESC
        ) AS RN

    FROM V_APPLICATION_ENDORSEMENT_PARENT a

    INNER JOIN UWBOX.dbo.PARAM_PRODUCT_GROUP pg 
        ON a.PRODUCT_GROUP_CODE = pg.CODE

    LEFT JOIN (
        SELECT DISTINCT REGNO, SEQ 
        FROM V_APPLICATION_ENDORSEMENT_MASTER_ENABLE_ROLLBACK
    ) b 
        ON a.REGNO = b.REGNO 
        AND a.SEQ = b.SEQ

    OUTER APPLY (
        SELECT TOP 1 *
        FROM APPLICATION_TRACK aa
        WHERE aa.REGNO = a.REGNO
        AND aa.TRACK_TYPE = 'POS'
        AND aa.PARAM_VALUE = CONVERT(varchar(10), a.SEQ)
    ) aa
)

SELECT 
    REGNO = a.REGNO,
    SEQ = a.SEQ,
    FULLNAME = a.FULLNAME,
    ENDORSEMENT_TYPE_DESCR = a.ENDORSEMENT_TYPE_DESCR,
    DOB = CONVERT(varchar(20), a.DOB, 106),

    PRODUCT = a.PRODUCT_NAME + '<BR><B>' + a.PRODUCT_GROUP + '</B>',
    POLICY_NO = a.POLICY_NO,

    AUTHOR_DATE = CONVERT(varchar(20),
        CASE 
            WHEN a.LAST_TRACK = 4 THEN a.TRACK4_DATE
            WHEN a.LAST_TRACK = 5 THEN a.TRACK5_DATE
        END, 106),

    AUTHOR_BY = 
        CASE 
            WHEN a.LAST_TRACK = 4 THEN a.TRACK4_BY
            WHEN a.LAST_TRACK = 5 THEN a.TRACK5_BY
        END,

    ENABLE_ROLLBACK = a.ENABLE_ROLLBACK

FROM DATA a
WHERE a.RN = 1
AND a.LAST_TRACK = " + DDL_STAT.SelectedValue + " " + whereOuter + @"

ORDER BY 
    CASE 
        WHEN a.LAST_TRACK = 4 THEN a.TRACK4_DATE
        WHEN a.LAST_TRACK = 5 THEN a.TRACK5_DATE
    END DESC
";




            conn.ExecuteQuery(15000000);




            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
                DGR.AllowPaging = false;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                Button btDELETE = (Button)DGR.Items[i].FindControl("BT_R");

                lbCODE.Text = DGR.Items[i].Cells[3].Text;
                btDELETE.Attributes.Add("onclick", "if(!confirm('Are you sure to ROLLBACK ?')){return false;};");

                if (DGR.Items[i].Cells[4].Text == "1")
                {
                    btDELETE.Visible = true;
                }
            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("EndorsementFrame.aspx?ID=" + e.Item.Cells[1].Text + "-" + e.Item.Cells[2].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PARENT_ROLLBACK_AUTHOR '" + e.Item.Cells[1].Text + "'," + e.Item.Cells[2].Text;
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}