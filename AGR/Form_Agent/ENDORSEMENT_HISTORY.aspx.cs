using DMS.DBConnection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGR.Form_Agent
{
    public partial class ENDORSEMENT_HISTORY : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            BT_PROCESS.Click += BT_PROCESS_Click;

            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["mode"]?.ToString();
                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();

                // ===============================
                // INIT STATE RELEASE REMUN
                // ===============================
                HF_SELECTED_REMUN.Value = "";   // hidden field untuk JS
                LB_AGENTCODE.Text = "";
            }

            string eventTarget = Request["__EVENTTARGET"];
            string eventArgument = Request["__EVENTARGUMENT"];

            // ============================================
            // OPEN POPUP RELEASE (CLICK STOP HOLD REMUN)
            // ============================================
            if (IsPostBack && eventTarget == "lnkClickMe")
            {
                string agentCode = eventArgument;
                LB_AGENTCODE.Text = eventArgument;
                HF_SELECTED_REMUN.Value = "";
                CBAGENT.Checked = false;
                CBCOMPANY.Checked = false;
                CBAGENT.Enabled = true;
                CBCOMPANY.Enabled = true;

                LB_UNHOLD_TITLE.Text = "Release to";

                FillDGRHOLD(agentCode);

                // ambil REMARK dari row yang diklik
                string remark = "";
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    if (DGR.Items[i].Cells[1].Text == agentCode)
                    {
                        remark = DGR.Items[i].Cells[6].Text; // kolom REMARK
                        break;
                    }
                }

                // Kirim agentCode ke JS
                string scriptPopup = string.Format(@"
                    currentAgentCode = '{0}';
                    currentRemark = '{1}';
                    openPopup('{0}');
                ", agentCode, remark.Replace("'", "\\'"));
                ClientScript.RegisterStartupScript(this.GetType(), "openPopup", scriptPopup, true);
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PARAM_ENDORSEMENT_TYPE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            try
            {
                DDL_TYPE.SelectedValue = LB_MODE.Text;
                LB_TITLE.Text = DDL_TYPE.SelectedItem.Text;
            }
            catch { }
        }

        public class ReleasePolicyDto
        {
            public string PolicyNo { get; set; }
            public string InsuredName { get; set; }
            public decimal Amount { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string RemunType { get; set; }
        }

        public class SelectedPolicyDto
        {
            public string PolicyNo { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string RemunType { get; set; }
        }

        public class HoldSummaryDto
        {
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public decimal Amount { get; set; }
            public string HoldStartDate { get; set; }
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static List<ReleasePolicyDto> GetReleasePolicy(string agentCode, List<string> remunTypes)
        {
            if (string.IsNullOrEmpty(agentCode))
                return new List<ReleasePolicyDto>();

            remunTypes = remunTypes ?? new List<string>();
            remunTypes = remunTypes.Where(x => !string.IsNullOrEmpty(x)).ToList();
            if (remunTypes.Count == 0) remunTypes.Add("0");

            string inClause = string.Join(",", remunTypes.Select(x => "'" + x.Replace("'", "''") + "'"));

            Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            conn.QueryString = string.Format(@"
                SELECT 
                    POLICY_NO,
                    INSURED_NAME,
                    AMOUNT,
                    POLICY_START_DATE,
                    POLICY_END_DATE,
                    REMUN_TYPE
                FROM M_AGENT_HOLD_REMUN_DETAIL
                WHERE CODE = '{0}'
                  AND REMUN_TYPE IN ({1})
                  AND RELEASEBY IS NULL
                  AND RELEASEDATE IS NULL
            ", agentCode, inClause);
            conn.ExecuteQuery();

            List<ReleasePolicyDto> result = new List<ReleasePolicyDto>();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                result.Add(new ReleasePolicyDto
                {
                    PolicyNo = conn.GetFieldValue(i, "POLICY_NO") ?? "",
                    InsuredName = conn.GetFieldValue(i, "INSURED_NAME") ?? "",
                    Amount = string.IsNullOrEmpty(conn.GetFieldValue(i, "AMOUNT")) ? 0 : Convert.ToDecimal(conn.GetFieldValue(i, "AMOUNT")),
                    StartDate = Convert.ToDateTime(conn.GetFieldValue(i, "POLICY_START_DATE")).ToString("yyyy-MM-dd"),
                    EndDate = conn.GetFieldValue(i, "POLICY_END_DATE") != null
                                ? Convert.ToDateTime(conn.GetFieldValue(i, "POLICY_END_DATE")).ToString("yyyy-MM-dd")
                                : null,
                    RemunType = conn.GetFieldValue(i, "REMUN_TYPE")
                });
            }
            return result;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static List<HoldSummaryDto> GetHoldSummary(
        string agentCode,
        List<string> policyNos,
        List<string> remunTypes,
        bool useSpecialQuery
        )
        {
            //if (string.IsNullOrEmpty(agentCode) || policyNos == null || policyNos.Count == 0)
                //return new List<HoldSummaryDto>();

            string policyIn = string.Join(",", policyNos.Select(x => "'" + x.Replace("'", "''") + "'"));
            string remunIn = string.Join(",", remunTypes.Select(x => "'" + x.Replace("'", "''") + "'"));

            Connection conn = new Connection(
                GlobalUse.GetConnString(
                    System.Configuration.ConfigurationManager.AppSettings["appid"]
                )
            );

            if (useSpecialQuery)
            {
                conn.QueryString = string.Format(@"
                SELECT
                    POLICY_START_DATE      = b.START_DATE,
                    POLICY_END_DATE        = b.END_DATE,
                    HOLD_START_DATE = b.HOLD_START_DATE,
                    AMOUNT          = e.AMOUNT
                FROM M_AGENT_HOLD_REMUN a WITH (NOLOCK)
                LEFT JOIN PERIOD_DETAIL_HOLD b WITH (NOLOCK)
                    ON b.AGENT_CODE = a.CODE
                   AND b.HOLD_START_DATE = a.START_DATE
                LEFT JOIN PR_REMUN_TYPE c WITH (NOLOCK)
                    ON c.CODE = b.REMUN_TYPE
                LEFT JOIN V_M_AGENTS d WITH (NOLOCK)
                    ON d.CODE = a.CODE
                LEFT JOIN PERIOD_DETAIL e WITH (NOLOCK)
                    ON e.AGENT_CODE = b.AGENT_CODE
                   AND e.REMUN_TYPE = b.REMUN_TYPE
                   AND e.START_DATE = b.START_DATE
                   AND e.END_DATE = b.END_DATE
                WHERE a.CODE = '{0}'
                  AND a.RELEASEBY IS NULL
                  AND ISNULL(e.AMOUNT,0) > 0
                ORDER BY b.END_DATE, b.REMUN_TYPE
            ", agentCode);
            }
            else
            {
                conn.QueryString = string.Format(@"
                SELECT
                    POLICY_START_DATE,
                    POLICY_END_DATE,
                    HOLD_START_DATE = START_DATE,
                    AMOUNT = SUM(AMOUNT)
                FROM M_AGENT_HOLD_REMUN_DETAIL
                WHERE CODE = '{0}'
                  AND POLICY_NO IN ({1})
                  AND REMUN_TYPE IN ({2})
                  AND RELEASEBY IS NULL
                GROUP BY
                    POLICY_START_DATE,
                    POLICY_END_DATE,
                    START_DATE
                ORDER BY
                    POLICY_START_DATE
            ", agentCode, policyIn, remunIn);
            }
            conn.ExecuteQuery();

            var result = new List<HoldSummaryDto>();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                result.Add(new HoldSummaryDto
                {
                    StartDate = Convert.ToDateTime(conn.GetFieldValue(i, "POLICY_START_DATE")).ToString("yyyy-MM-dd"),
                    EndDate = Convert.ToDateTime(conn.GetFieldValue(i, "POLICY_END_DATE")).ToString("yyyy-MM-dd"),
                    HoldStartDate = Convert.ToDateTime(conn.GetFieldValue(i, "HOLD_START_DATE")).ToString("yyyy-MM-dd"),
                    Amount = Convert.ToDecimal(conn.GetFieldValue(i, "AMOUNT"))
                });
            }

            return result;
        }

        protected void FillDGR()
        {
            string where = "";
            LB_RECORDS.Text = "";

            if (DDL_TYPE.SelectedValue != "")
                where = where + " and a.MODE='" + DDL_TYPE.SelectedValue + "' ";

            //additional filter TXT_AGENTCODE
            if (TXT_AGENTCODE.Text.Trim() != "")
                where = where + " and a.CODE like '%" + TXT_AGENTCODE.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_REMARK.Text.Trim() != "")
                where = where + " and a.REMARK like '%" + TXT_REMARK.Text.Trim() + "%' ";

            /*string strQuery = $@"DECLARE @IsUserApprove BIT = 0;
								SELECT @IsUserApprove = CASE WHEN ISNULL(UserId,'') != '' THEN 1 ELSE 0 END
								FROM PARAM_REMUN_APPROVAL_HOLD_DETAIL
								WHERE SysCode = '1'
								AND UserId = '{GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID")}';

                                SELECT CODE, 
                                FULLNAME, 
                                MODE, 
                                SEQ,
                                MODE_DESCR, 
                                --REMARK, 
                                REMARK = CASE WHEN a.MODE = '7' AND @IsUserApprove = 0 THEN
	                                REPLACE(REMARK, '<a ', '<a aria-current=""page""  ' )
	                                ELSE
	                                REMARK
                                END,
                                URL, 
                                COLOR, 
                                REQUESTBY	= REQUESTBY + ' (' + convert(varchar(100), REQUESTDATE) + ')', 
                                APPROVEBY	= APPROVEBY + ' (' + convert(varchar(100), APPROVEDATE) + ')' 
                                from		V_ENDORSEMENT_HISTORY a 
                                where 1=1   
                                {where}
                                order by a.APPROVEDATE";*/
            //fixing interpolated string in visual studio 2012
            string userId = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

            string strQuery = string.Format(@"
                                        DECLARE @IsUserApprove BIT = 0;
                                        SELECT @IsUserApprove = CASE WHEN ISNULL(UserId,'') != '' THEN 1 ELSE 0 END
                                        FROM PARAM_REMUN_APPROVAL_HOLD_DETAIL
                                        WHERE SysCode = '1'
                                        AND UserId = '{0}';

                                        SELECT 
                                            CODE, 
                                            FULLNAME, 
                                            MODE, 
                                            SEQ,
                                            MODE_DESCR, 
                                            REMARK = CASE 
                                                WHEN a.MODE = '7' AND @IsUserApprove = 0 THEN
                                                    REPLACE(REMARK, '<a ', '<a aria-current=""page""  ' )
                                                ELSE
                                                    REMARK
                                            END,
                                            URL, 
                                            COLOR, 
                                            REQUESTBY = REQUESTBY + ' (' + CONVERT(VARCHAR(100), REQUESTDATE) + ')', 
                                            APPROVEBY = APPROVEBY + ' (' + CONVERT(VARCHAR(100), APPROVEDATE) + ')' 
                                        FROM V_ENDORSEMENT_HISTORY a 
                                        WHERE 1=1   
                                        {1}
                                        ORDER BY a.APPROVEDATE;", userId, where);


            conn.QueryString = strQuery;
            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                lb.Text = DGR.Items[i].Cells[1].Text;
            }
        }



        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void lnkClickMe(object sender, EventArgs e)
        {
            LB_UNHOLD_TITLE.Text = "Release to";
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "alert('test');", true);
            //ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void BT_PROCESS_Click(object sender, EventArgs e)
        {
            //ShowProgress(); // kalau mau
            var json = HF_SELECTED_POLICY.Value;

            if (string.IsNullOrEmpty(json))
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(), "alert",
                    "alert('Policy belum dipilih');", true);
                return;
            }

            var policies = Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<SelectedPolicyDto>>(json);

            if (CBAGENT.Checked && CBCOMPANY.Checked)
            {
                // optional: validasi kalau nggak boleh dua-duanya
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "alert",
                    "alert('Pilih salah satu: Rekening YBS atau Rekening Takaful');",
                    true
                );
                return;
            }

            if (CBAGENT.Checked)
            {
                CBCOMPANY.Enabled = false;
                System.Threading.Thread.Sleep(2000);
                UpdateCheckboxStates("AGENT");
            }
            else if (CBCOMPANY.Checked)
            {
                CBAGENT.Enabled = false;
                System.Threading.Thread.Sleep(2000);
                UpdateCheckboxStates("COMPANY");
            }
            else
            {
                ScriptManager.RegisterStartupScript(
                    this, GetType(),
                    "alert",
                    "alert('Silakan pilih salah satu opsi terlebih dahulu');",
                    true
                );
            }
        }

        protected void BT_CANCEL_Click(object sender, EventArgs e)
        {
            CBAGENT.Checked = false;
            CBCOMPANY.Checked = false;

            CBAGENT.Enabled = true;
            CBCOMPANY.Enabled = true;

            // tutup popup
            pnlpopup.Style["display"] = "none";
        }

        //protected void CBAGENT_CheckedChanged(object sender, EventArgs e)
        //{
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ShowProgress();", true);
        //    CBCOMPANY.Enabled = false;
        //    System.Threading.Thread.Sleep(2000);
        //    UpdateCheckboxStates("AGENT");
        //}

        //protected void CBCOMPANY_CheckedChanged(object sender, EventArgs e)
        //{
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ShowProgress();", true);
        //    CBAGENT.Enabled = false;
        //    System.Threading.Thread.Sleep(2000);
        //    UpdateCheckboxStates("COMPANY");
        //}

        private void UpdateCheckboxStates(string src)
        {
            if (src == "AGENT")
                UpdateCheckboxPair(CBAGENT, CBCOMPANY, src);
            else
                UpdateCheckboxPair(CBCOMPANY, CBAGENT, src);
        }

        private void UpdateCheckboxPair(CheckBox source, CheckBox target, string src)
        {
            if (source.Checked)
            {
                target.Enabled = false;
                target.Checked = false;

                // Optional: update data based on source

                string releaseTo = src == "AGENT" ? "AGENT" : "COMP";
                SaveStatusToDatabase(releaseTo);
            }
            else
            {
                target.Enabled = true;
            }
        }

        private void SaveStatusToDatabase(string src)
        {
            string strQuery = string.Format(@"exec SP_M_AGENT_RELEASE_REMUN '{0}' , '{1}' , {2} ", LB_AGENTCODE.Text, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"), src);
            conn.QueryString = strQuery;
            conn.ExecuteNonQuery();
            var policies = JsonConvert.DeserializeObject<List<SelectedPolicyDto>>(
                HF_SELECTED_POLICY.Value
            );

            if (policies == null || policies.Count == 0)
                return;

            // 3️⃣ Loop UPDATE DETAIL
            foreach (var p in policies)
            {
                string endDateSql = string.IsNullOrEmpty(p.EndDate)
                    ? "NULL"
                    : "'" + p.EndDate + "'";

                string sql = string.Format(@"
                    exec SP_M_AGENT_RELEASE_REMUN_DETAIL
                        '{0}',
                        '{1}',
                        '{2}',
                        '{3}',
                        '{4}',
                        {5},
                        '{6}'
                ",
    LB_AGENTCODE.Text,
    GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"),
    src,
    p.PolicyNo,
    p.StartDate,
    endDateSql,
    p.RemunType
);

                conn.QueryString = sql;
                conn.ExecuteNonQuery();
            }
            Response.Redirect("ENDORSEMENT_HISTORY.aspx?mode=7");
        }

        protected void FillDGRHOLD(string agentcode)
        {
            conn.QueryString = "exec SP_HOLD_HISTORY " +
                                "'" + agentcode + "' ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            //DGR_HOLD.DataSource = dt;
            //DGR_HOLD.DataBind();
        }

        public void MessageBox(System.Web.UI.Page page)
        {
            //+ character added after strMsg "')"
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "Process", "ShowProgress()", true);

        }
        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("Agent_Frame.aspx?AGENTCODE=" + e.Item.Cells[1].Text);
            }


        }
    }
}