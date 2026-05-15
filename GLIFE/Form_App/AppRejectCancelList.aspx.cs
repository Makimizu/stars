using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class AppRejectCancelList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TRACK.Text = Request.QueryString["track"].ToString();
                
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = " ";


            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and h.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and mb.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and i.DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and a.REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_CONFDATE1.Text.Trim() != "")
                where = where + " and convert(date,b.USER_ENDDATE) >= '" + GlobalUse.GlobalDateFormat(TXT_CONFDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_CONFDATE2.Text.Trim() != "")
                where = where + " and convert(date,b.USER_ENDDATE) <= '" + GlobalUse.GlobalDateFormat(TXT_CONFDATE2.Text.Trim(), "d/M/yyyy") + "' ";
            
            if (txtDOB.Text.Trim() != "")
                where = where + " and convert(date,mb.DOB) = '" + GlobalUse.GlobalDateFormat(txtDOB.Text.Trim(), "d/M/yyyy") + "' ";



            conn.QueryString = "select " +
                                "REGNO			= a.REGNO,   " +
                                "FULLNAME		= '<span style=\"color:green;\">' + mb.FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>',   " +
                                "POLICY_NO		= g.POLICY_NO,   " +
                                "COMPANY_NAME	= '<span style=\"color:black;\">' + h.COMPANY_NAME + '</span>' + '<BR><i>' + i.DESCR + '</i>',   " +
                                "UW_CODE			= UW_CODE,   " +
                                "PREMIUM			= (case	when isnull(f.VAL, 0) = 0 then replace(convert(varchar(100), convert(money,e.PREMIUM),1), '.00','') " +
                                "                        else (case when isnull(f.VAL, 0) > e.PREMIUM then replace(convert(varchar(100), convert(money,f.VAL),1), '.00','') else replace(convert(varchar(100), convert(money,e.PREMIUM),1), '.00','') end) " +
                                "                        end),  " +
                                "SUMINS			= replace(convert(varchar(100), convert(money, d.SUMINS),1), '.00',''),   " +
                                "REASON			= '<U>' + bb.DESCR collate database_default + ' by ' + b.USER_ENDBY + ' (' + convert(varchar(100),b.USER_ENDDATE) + '):</U><BR><I>' + isnull(b.COMMENT collate database_default,'') + '</I>',  " +
                                "LAST_TRACK		= b.SEQ " +
                                "from		TRACK_DATA b  " +
                                "inner join	PARAM_TRACK bb on b.TIPE_CODE = bb.TIPE_CODE and b.SEQ = bb.SEQ " +
                                "inner join	APPLICATION_MASTER a on a.REGNO=b.OWNER collate database_default " +
                                "inner join	APPLICATION_MAIN_INFO d on a.REGNO = d.REGNO " +
                                "inner join	POLICY g on a.POLICY_ID = g.ID " +
                                "inner join	V_LINK_CB_COMPANY h on g.COMPANY_CODE = h.COMPANY_CODE " +
                                "left join	V_LINK_UB_TC_MASTER i on a.TC_ID = i.CODE " +
                                "left join	V_LINK_CB_MEMBER_MASTER mb on a.MEMBER_ID = mb.ID " +
                                "inner join	(select REGNO, RATE = SUM(isnull(RATE,0)), PREMIUM = SUM(SUMINS * isnull(RATE,0))/1000 from APPLICATION_BENEFIT group by REGNO) e on a.REGNO = e.REGNO   " +
                                "left join	V_LINK_UB_TC_ITEMS f on f.TC_ITEM = '43' and ISNUMERIC(replace(f.VAL, ',','')) = 1 and a.TC_ID = f.TC_CODE " +
                                "where " +
                                "b.TIPE_CODE = 'UW' and b.SEQ = " + LB_TRACK.Text + " " + where;
            /*
            conn.QueryString = "select " +
                                "REGNO,  " +
                                "FULLNAME = '<span style=\"color:green;\">' + FULLNAME + '</span>' + '<BR><table style=\"width:100%;font-style:italic;font-size:8pt;\"><tr><td>' + convert(varchar(20),DOB,106) + '</td><td style=\"text-align:right;\">' + (case when SEX='M' then '<span style=\"color:blue;\">Male</span>' else '<span style=\"color:red;\">Female</span>' end) + '</td></tr></table>',  " +
                                "POLICY_NO,  " +
                                "COMPANY_NAME = '<span style=\"color:black;\">' + COMPANY_NAME + '</span>' + '<BR><i>' + TC_DESCR + '</i>',  " +
                                "UW_CODE,  " +
                                "PREMIUM = replace(convert(varchar(100), convert(money,PREMIUM),1), '.00',''),  " +
                                "SUMINS = replace(convert(varchar(100), convert(money,SUMINS),1), '.00',''),  " +
                                "REASON = '<U>' + a.LAST_TRACK_DESCR collate database_default + ' by ' + LEFT(UPPER(LTRIM(isnull(c.FRONT_NAME,'')) + RTRIM(' ' + isnull(c.MID_NAME,'')) + RTRIM(' ' + isnull(c.LAST_NAME,''))), 50) + ' (' + convert(varchar(100),b.USER_ENDDATE) + '):</U><BR><I>' + isnull(b.COMMENT collate database_default,'') + '</I>', " +
                                "a.LAST_TRACK " +
                                "from V_APPLICATION_MASTER a " +
                                "inner join TRACK_DATA b on b.TIPE_CODE='UW' and a.LAST_TRACK=b.SEQ and a.REGNO=b.OWNER collate database_default " +
                                "left join V_LINK_SC_M_USERS c on b.USER_ENDBY = c.CODE collate database_default " +
                                "where " +
                                "a.LAST_TRACK = " + LB_TRACK.Text + " " +
                                "and convert(date,GETDATE()) < convert(date,a.END_DATE) " + where +
                                " order by a.CONFIRMED_DATE";
            */

            conn.ExecuteQuery(50000);

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
                Button btDELETE = (Button)DGR.Items[i].FindControl("BT_DEL");

                lbCODE.Text = DGR.Items[i].Cells[1].Text;
                btDELETE.Attributes.Add("onclick", "if(!confirm('Are you sure to REINSTATE ?')){return false;};");
            }

            if (LB_TRACK.Text == "5")
            {
                DGR.Columns[DGR.Columns.Count - 1].Visible = true;
            }

            if (GlobalUse.IsReadOnly(GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles"), Request.QueryString["menucode"]))
            {
                DGR.Columns[DGR.Columns.Count - 1].Visible = false;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                AppApprove();
            }

            if (e.CommandName == "Select")
            {
                Response.Redirect("ApplicationFrame.aspx?ID=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_MASTER_REINSTATE '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void AppApprove()
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "exec SP_APPLICATION_MASTER_NEXT_TRACK " +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "4," +
                                            "null," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            try
            {
                FillDGR();
            }
            catch
            {
                DGR.CurrentPageIndex = 0;
                FillDGR();
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

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}