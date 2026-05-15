using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Net;
using System.Data.SqlClient;

namespace GLIFE.Form_Policy
{
    public partial class Polis_PreRenewal : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        protected SqlConnection connection = new SqlConnection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
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
            BT_DETAILCLOSE.Attributes.Add("onclick", "document.getElementById('pnlpopup').style.display = 'none';");
        }


        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = " ";
            //string joinrange = " ";
            //string joinflag = " ";


            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";


            if (TXT_CONFDATE1.Text.Trim() != "")
                where = where + " and convert(date,b.PERIOD_END) >= '" + GlobalUse.GlobalDateFormat(TXT_CONFDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_CONFDATE2.Text.Trim() != "")
                where = where + " and convert(date,b.PERIOD_END) <= '" + GlobalUse.GlobalDateFormat(TXT_CONFDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_CONFDATE3.Text.Trim() != "")
                where = where + " and convert(date,b.RENEWAL_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_CONFDATE3.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_CONFDATE4.Text.Trim() != "")
                where = where + " and convert(date,b.RENEWAL_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_CONFDATE4.Text.Trim(), "d/M/yyyy") + "' ";


            if (DDL_PREMIUM.SelectedValue != "")
            {
                if (DDL_PREMIUM.SelectedValue == "0")
                    where = where + " and b.POLICY_RENEWAL = 0 ";
                if (DDL_PREMIUM.SelectedValue == "1")
                    where = where + " and b.POLICY_RENEWAL = 1 ";
            }

            conn.QueryString = 
                                "select a.ID, " + 
	                                   "a.POLICY_NO, " + 
	                                   "a.COMPANY_NAME, " + 
	                                   "START_DATE = convert(varchar(20),b.PERIOD_START,106), " + 
	                                   "END_DATE = convert(varchar(20),b.PERIOD_END,106), " + 
	                                   "[NB/RN] =  (case when b.POLICY_RENEWAL = 0 then 'NEW BUSINESS' when b.POLICY_RENEWAL = 1 then 'RENEWAL'  else '' end), " +
	                                   "MOP= b.MOP, " +
	                                   "a.MEMBER, " +
                                       //"[PREMIUM BILLED] = PREMIUM_BILLED, " +
                                       "[PREMIUM BILLED] = replace(convert(varchar(100), convert(money,SUM(CASE ISNULL(b.PREMIUM_BILLED,'') WHEN '' THEN 0 ELSE CONVERT(int, replace(b.PREMIUM_BILLED,',','')) END)),1), '.00',''), " +
	                                   "AGEN = a.AGENT_NAME, " +
                                       "[CHANNEL DISTRIBUSI] = b.CHANNEL_DISTRIBUSI, " +
	                                   "[TANGGAL JATUH TEMPO] = convert(varchar(20),b.PERIOD_END,106), " +
	                                   "[POLIS SEBELUMNYA] = b.POLICY_LAST, " +
	                                   "[TANGGAL RENEWAL] = convert(varchar(20),b.RENEWAL_DATE,106), " +
	                                   "[SEQ RENEWAL] = b.SEQ_NUMBER " +
                                "from V_POLICY a " +
                                "inner join V_POLICY_PRENEWAL b on a.POLICY_NO = b.POLICY_NO " +
                                "where b.PERIOD_END != '' and (Select dbo.menghitungbulan(b.PERIOD_END, GETDATE())) BETWEEN -2 AND -1" + where + " " +
                                "GROUP BY a.ID,a.POLICY_NO,a.COMPANY_NAME,b.PERIOD_START,b.PERIOD_END,b.POLICY_RENEWAL,b.MOP,a.MEMBER,a.AGENT_NAME,b.CHANNEL_DISTRIBUSI,b.POLICY_LAST,b.RENEWAL_DATE,b.SEQ_NUMBER " +
                                " ORDER BY b.PERIOD_END";
                
                                //"select " +
                                //"ID, " +
                                //"POLICY_ID, " +
                                //"POLICY_NO, " +
                                //"COMPANY_NAME, " +
                                //"START_DATE = convert(varchar(20),START_DATE,106), " +
                                //"END_DATE = convert(varchar(20),END_DATE,106), " +
                                //"MEMBER, " +
                                //"RESERVE_BILLED = replace(convert(varchar(100),convert(money,RESERVE_BILLED),1),'.00',''), " +
                                //"RESERVE_EARNED = replace(convert(varchar(100),convert(money,RESERVE_EARNED),1),'.00',''), " +
                                //"LOADING = replace(convert(varchar(100),convert(money,LOADING),1),'.00',''), " +
                                //"TABARRU = replace(convert(varchar(100),convert(money,TABARRU),1),'.00',''), " +
                                //"LOADING_AMT = replace(convert(varchar(100),convert(money,LOADING_AMT),1),'.00',''), " +
                                //"TABBARU_AMT = replace(convert(varchar(100),convert(money,TABBARU_AMT),1),'.00',''), " +
                                //"CLAIM_PAID = replace(convert(varchar(100),convert(money,isnull(CLAIM,0)),1),'.00',''), " +
                                //"REFUND_PREMIUM = replace(convert(varchar(100),convert(money,isnull(REFUND_PREMIUM,0)),1),'.00',''), " +
                                //"BALANCE = replace(convert(varchar(100),convert(money,isnull(BALANCE,0)),1),'.00',''), " +
                                //"CLAIM_RATIO = convert(varchar(100),convert(money,a.CLAIM_RATIO * 100),1), " +
                                //"CLAIM_RATIO_CALC, " +
                                //"TIPE, " +
                                //"MOP, " +
                                //"PRODUCT, " +
                                //"TPA, " +
                                //"SURPLUS = (case when isnull(BALANCE,0) >= 0 then 1 else 0 end) " +
                                //"from V_POLICY_PERIOD_PRERENEWAL a " +
                                //"order by a.END_DATE";
            conn.ExecuteQuery();

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton bt = (LinkButton)DGR.Items[i].FindControl("LB_ID");
                Button btR = (Button)DGR.Items[i].FindControl("BT_RESPOND");
                //Button btQ = (Button)DGR.Items[i].FindControl("BT_QUOTATION");
                bt.Text = DGR.Items[i].Cells[2].Text;


                //if (DGR.Items[i].Cells[22].Text == "0")
                //{
                //    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                //    DGR.Items[i].Cells[17].BackColor = System.Drawing.Color.Red;
                //    DGR.Items[i].Cells[17].ForeColor = System.Drawing.Color.White;
                //}

                //if (DGR.Items[i].Cells[23].Text == "0")
                //{
                //    btQ.BackColor = System.Drawing.Color.Red;
                //}

                //if (DGR.Items[i].Cells[24].Text == "0")
                //{
                //    btQ.Visible = false;
                //}
                //else
                //{
                    //btQ.Attributes.Add("onclick", "window.open('Polis_PreRenewal_Report_Frame.aspx?CODE=005&ID=" + DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "") + "','REPORT','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                //}
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Response.Redirect("PolicyFrame.aspx?ID=" + e.Item.Cells[1].Text + "&readonly=" + "");
            }

            if (e.CommandName == "Respon")
            {
                ShowRespond(e.Item.Cells[1].Text);
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void ShowRespond(string periodid)
        {
            LB_PERIOD.Text = periodid;

            conn.QueryString = "exec SP_POLICY_PERIOD_RENEWAL_REMARK '" + periodid + "'";
            conn.ExecuteQuery(15000);
            DGR_RESPOND.DataSource = conn.GetDataTable().Copy();
            DGR_RESPOND.DataBind();

            conn.QueryString = "select CODE, DESCR from PR_POLICY_RENEWAL_DECISION";
            conn.ExecuteQuery(15000);


            for (int i = 0; i < DGR_RESPOND.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_RESPOND.Items[i].FindControl("DDL_DECISION");
                TextBox txt = (TextBox)DGR_RESPOND.Items[i].FindControl("TXT_REMARK");

                if (DGR_RESPOND.Items[i].Cells[4].Text.Replace("&nbsp;", "").Trim() != "")
                {
                    System.Drawing.Color Clr = System.Drawing.Color.FromName(DGR_RESPOND.Items[i].Cells[4].Text.Replace("&nbsp;", "").Trim());
                    System.Drawing.Color ClInvert = System.Drawing.Color.FromArgb(Clr.ToArgb() ^ 0xffffff);

                    ddl.BackColor = Clr;
                    txt.BackColor = Clr;

                    ddl.ForeColor = ClInvert;
                    txt.ForeColor = ClInvert;
                }

                ddl.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddl.SelectedValue = DGR_RESPOND.Items[i].Cells[2].Text;
                }
                catch { }
                txt.Text = DGR_RESPOND.Items[i].Cells[3].Text.Replace("&nbsp;", "");
            }


            //conn.QueryString = "select " +
            //                    "POLICY_NO, " +
            //                    "COMPANY_NAME, " +
            //                    "PERIOD = convert(varchar(20),PERIOD_START,106) + ' - ' + convert(varchar(20),PERIOD_END,106), " +
            //                    "TABBARU_AMT = replace(convert(varchar(100),convert(money,'0'),1),'.00',''), " +
            //                    "CLAIM_PAID = replace(convert(varchar(100),convert(money,isnull('0',0)),1),'.00',''), " +
            //                    "CR_DEFAULT = convert(varchar(100),convert(money, '0' * 100),1), " +
            //                    "CR = convert(varchar(100),convert(money, isnull('0', '0') * 100),1) " +
            //                    //"from V_POLICY_PERIOD_PRERENEWAL a " +
            //                    "from V_POLICY a " +
            //                    //"left join POLICY_PERIOD_CLAIM_RATIO b on a.ID = b.POLICY_PERIOD_ID " +
            //                    "where ID = '" + LB_PERIOD.Text + "'";
            //conn.QueryString = "select POLICY_NO = b.POLICY_NO," +
            //                           "COMPANY_NAME = g.COMPANY_NAME," +
            //                           "PERIOD = CAST(CONVERT(varchar, i.PERIOD_START, 106) as varchar) + ' - ' + CAST(CONVERT(varchar, i.PERIOD_END, 106) as varchar)," +
            //                           //"TABBARU_AMT = replace(convert(varchar(100),convert(money,isnull(SUM(PREMIUM - (LOAD_001_AMT +  LOAD_002_AMT +  LOAD_003_AMT +  LOAD_004_AMT +  LOAD_005_AMT +  LOAD_006_AMT +  LOAD_007_AMT +  LOAD_008_AMT +  LOAD_009_AMT +  LOAD_010_AMT +  LOAD_011_AMT +  LOAD_012_AMT +  LOAD_013_AMT +  LOAD_014_AMT +  LOAD_015_AMT +  LOAD_016_AMT +  LOAD_017_AMT +  LOAD_018_AMT +  LOAD_019_AMT +  LOAD_020_AMT +  LOAD_021_AMT +  LOAD_022_AMT +  LOAD_023_AMT +  LOAD_024_AMT +  LOAD_025_AMT +  LOAD_026_AMT +  LOAD_027_AMT +  LOAD_028_AMT +  LOAD_029_AMT +  LOAD_030_AMT)),0)),1),'.00','')," +
            //                           //"pb.TABBARU_AMT, " +
            //                           "TABBARU_AMT = (select TABBARU_AMT = replace(convert(varchar(100),convert(money,isnull(SUM(PREMIUM - (LOAD_001_AMT +  LOAD_002_AMT +  LOAD_003_AMT +  LOAD_004_AMT +  LOAD_005_AMT +  LOAD_006_AMT +  LOAD_007_AMT +  LOAD_008_AMT +  LOAD_009_AMT +  LOAD_010_AMT +  LOAD_011_AMT +  LOAD_012_AMT +  LOAD_013_AMT +  LOAD_014_AMT +  LOAD_015_AMT +  LOAD_016_AMT +  LOAD_017_AMT +  LOAD_018_AMT +  LOAD_019_AMT +  LOAD_020_AMT +  LOAD_021_AMT +  LOAD_022_AMT +  LOAD_023_AMT +  LOAD_024_AMT +  LOAD_025_AMT +  LOAD_026_AMT +  LOAD_027_AMT +  LOAD_028_AMT +  LOAD_029_AMT +  LOAD_030_AMT)),0)),1),'.00','') from GLIFE..POLICY a inner join PRODUCTION..MEMBER_PRODUCTION_GL d on  a.POLICY_NO = d.POLICY_NO where ID = b.ID)," +
            //                           "CLAIM_PAID = replace(convert(varchar(100),convert(money,isnull(SUM(isnull(e.TOTAL_AMOUNT, 0) - isnull(rj.AMOUNT, 0)),0)),1),'.00','')," +
            //                           "CR_DEFAULT = convert(varchar(100),convert(money, '0' * 100),1)," +
            //                           "CR = convert(numeric(10,2),(isnull(SUM(isnull(e.TOTAL_AMOUNT, 0) - isnull(rj.AMOUNT, 0)),0) / (select isnull(SUM(PREMIUM - (LOAD_001_AMT +  LOAD_002_AMT +  LOAD_003_AMT +  LOAD_004_AMT +  LOAD_005_AMT +  LOAD_006_AMT +  LOAD_007_AMT +  LOAD_008_AMT +  LOAD_009_AMT +  LOAD_010_AMT +  LOAD_011_AMT +  LOAD_012_AMT +  LOAD_013_AMT +  LOAD_014_AMT +  LOAD_015_AMT +  LOAD_016_AMT +  LOAD_017_AMT +  LOAD_018_AMT +  LOAD_019_AMT +  LOAD_020_AMT +  LOAD_021_AMT +  LOAD_022_AMT +  LOAD_023_AMT +  LOAD_024_AMT +  LOAD_025_AMT +  LOAD_026_AMT +  LOAD_027_AMT +  LOAD_028_AMT +  LOAD_029_AMT +  LOAD_030_AMT)),0) from GLIFE..POLICY a inner join PRODUCTION..MEMBER_PRODUCTION_GL d on  a.POLICY_NO = d.POLICY_NO where ID = b.ID)) * 100)" +
            //                    "from GLIFE..POLICY b " +
            //                    "left join GLIFE..V_APPLICATION_CLAIM_MASTER a on b.POLICY_NO = a.POLICY_NO and a.TRACK = 4 " +
            //                    "left join V_APPLICATION_CLAIM_AMOUNT_SUMM e on a.REGNO = e.REGNO and a.SEQ = e.SEQ " +
            //                    "left join CLIENT_BASE..COMPANY g on b.COMPANY_CODE = g.COMPANY_CODE " +
            //                    //"left join (select POLICY_NO, TABBARU_AMT = replace(convert(varchar(100), convert(money,SUM(CASE ISNULL(PREMIUM_BILLED,'') WHEN '' THEN 0 ELSE CONVERT(int, replace(PREMIUM_BILLED,',','')) END)),1), '.00','') from GLIFE..V_POLICY GROUP BY POLICY_NO) pb on b.POLICY_NO = pb.POLICY_NO " +
            //                    "left join GLIFE..POLICY_PERIOD i on b.ID = i.POLICY_ID " +
            //                    "left join ( select CLAIM_NO = b.DOCNO, AMOUNT = a.AMOUNT from  FINANCE.dbo.SETTLEMENT_REFUND a left join FINANCE.dbo.SETTLEMENT_DETAIL b on a.STL_TRXID = b.TRXID where a.APP_ID = 'GL' and a.TIPE_SETTLEMENT = '1') rj on a.REGNO collate database_default + '-' + convert(varchar(10), a.SEQ) = rj.CLAIM_NO collate database_default " +
            //                    "left join PRODUCTION..MEMBER_PRODUCTION_GL f on b.POLICY_NO = f.POLICY_NO and e.REGNO = f.REGNO " +
            //                    "where ID =  '" + LB_PERIOD.Text + "'" +
            //                    "GROUP BY b.POLICY_NO,g.COMPANY_NAME,i.PERIOD_START,i.PERIOD_END,b.ID";
            conn.QueryString = "select POLICY_NO = b.POLICY_NO," +
       "COMPANY_NAME = g.COMPANY_NAME," +
       "PERIOD =  CAST(CONVERT(varchar, i.PERIOD_START, 106) as varchar) + ' - ' + CAST(CONVERT(varchar, i.PERIOD_END, 106) as varchar)," +
       "TABBARU_AMT = replace(convert(varchar(100),convert(money,isnull(SUM(PREMIUM - (LOAD_001_AMT +  LOAD_002_AMT +  LOAD_003_AMT +  LOAD_004_AMT +  LOAD_005_AMT +  LOAD_006_AMT +  LOAD_007_AMT +  LOAD_008_AMT +  LOAD_009_AMT +  LOAD_010_AMT +  LOAD_011_AMT +  LOAD_012_AMT +  LOAD_013_AMT +  LOAD_014_AMT +  LOAD_015_AMT +  LOAD_016_AMT +  LOAD_017_AMT +  LOAD_018_AMT +  LOAD_019_AMT +  LOAD_020_AMT +  LOAD_021_AMT +  LOAD_022_AMT +  LOAD_023_AMT +  LOAD_024_AMT +  LOAD_025_AMT +  LOAD_026_AMT +  LOAD_027_AMT +  LOAD_028_AMT +  LOAD_029_AMT +  LOAD_030_AMT)),0)),1),'.00','')," +
       "CLAIM_PAID = replace(convert(varchar(100),convert(money,isnull(SUM(isnull(e.TOTAL_AMOUNT, 0) - isnull(rj.AMOUNT, 0)),0)),1),'.00','')," +
       "CR_DEFAULT = convert(varchar(100),convert(money, '0' * 100),1)," +
       "CR = convert(numeric(10,2),(isnull(SUM(isnull(e.TOTAL_AMOUNT, 0) - isnull(rj.AMOUNT, 0)),0) / isnull(SUM(PREMIUM - (LOAD_001_AMT +  LOAD_002_AMT +  LOAD_003_AMT +  LOAD_004_AMT +  LOAD_005_AMT +  LOAD_006_AMT +  LOAD_007_AMT +  LOAD_008_AMT +  LOAD_009_AMT +  LOAD_010_AMT +  LOAD_011_AMT +  LOAD_012_AMT +  LOAD_013_AMT +  LOAD_014_AMT +  LOAD_015_AMT +  LOAD_016_AMT +  LOAD_017_AMT +  LOAD_018_AMT +  LOAD_019_AMT +  LOAD_020_AMT +  LOAD_021_AMT +  LOAD_022_AMT +  LOAD_023_AMT +  LOAD_024_AMT +  LOAD_025_AMT +  LOAD_026_AMT +  LOAD_027_AMT +  LOAD_028_AMT +  LOAD_029_AMT +  LOAD_030_AMT)),0)))" +
       "from GLIFE..POLICY b " +
       "left join GLIFE..V_APPLICATION_CLAIM_MASTER a on b.POLICY_NO = a.POLICY_NO and a.TRACK = 4 " +
       "left join V_APPLICATION_CLAIM_AMOUNT_SUMM e on a.REGNO = e.REGNO and a.SEQ = e.SEQ " +
       "left join CLIENT_BASE..COMPANY g on b.COMPANY_CODE = g.COMPANY_CODE " +
       "left join GLIFE..POLICY_PERIOD i on b.ID = i.POLICY_ID " +
       "left join (select CLAIM_NO = b.DOCNO, AMOUNT = a.AMOUNT from FINANCE.dbo.SETTLEMENT_REFUND a left join FINANCE.dbo.SETTLEMENT_DETAIL b on a.STL_TRXID = b.TRXID where a.APP_ID = 'GL' and a.TIPE_SETTLEMENT = '1') rj on a.REGNO collate database_default + '-' + convert(varchar(10), a.SEQ) = rj.CLAIM_NO collate database_default " +
       "inner join PRODUCTION..MEMBER_PRODUCTION_GL f on b.POLICY_NO = f.POLICY_NO  and e.REGNO = f.REGNO " +
       "where ID = '" + LB_PERIOD.Text + "'" +
       "GROUP BY b.POLICY_NO,g.COMPANY_NAME,i.PERIOD_START,i.PERIOD_END";
            conn.ExecuteQuery(15000); 
            LB_POLICYNO.Text = conn.GetFieldValue("POLICY_NO").ToString();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY_NAME").ToString();
            LB_PERIODDATE.Text = conn.GetFieldValue("PERIOD").ToString();
            LB_TABBARU.Text = conn.GetFieldValue("TABBARU_AMT").ToString();
            LB_CLAIM.Text = conn.GetFieldValue("CLAIM_PAID").ToString();
            //LB_CRVAL.Text = conn.GetFieldValue("CR_DEFAULT").ToString();
            TXT_CRVAL.Text = conn.GetFieldValue("CR").ToString();

            /*
            conn.QueryString = "exec SP_POLICY_PERIOD_RENEWAL_REMARK_NOTDONE '" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();
            BT_CRSAVE.Visible = true;
            if (conn.GetRowCount() > 0)
                BT_CRSAVE.Visible = false;
            */

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            GlobalUse.DataGridToExcel(this, DGR);
        }

        protected void DGR_RESPOND_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_DECISION");
                TextBox txt = (TextBox)e.Item.FindControl("TXT_REMARK");

                try
                {
                    conn.QueryString = "exec SP_POLICY_PERIOD_RENEWAL_REMARK_UPSERT " +
                                        "'" + LB_PERIOD.Text + "'," +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "'" + ddl.SelectedValue + "'," +
                                        "'" + txt.Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    ShowRespond(e.Item.Cells[0].Text);
                }
                catch { }
            }
        }

        //protected void BT_CRSAVE_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        conn.QueryString = "exec SP_POLICY_PERIOD_CLAIM_RATIO_UPSERT " +
        //                             "'" + LB_PERIOD.Text + "'," +
        //                             //TXT_CRVAL.Text.Trim() + ", " +
        //                             "null, " +
        //                             "null, " +
        //                             "null, " +
        //                             "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
        //        conn.ExecuteNonQuery();
        //        FillDGR();
        //        ShowRespond(LB_PERIOD.Text);
        //    }
        //    catch { }
        //}
       
    }
}