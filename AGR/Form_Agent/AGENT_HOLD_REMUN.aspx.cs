using AjaxControlToolkit;
using DMS.DBConnection;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace AGR.Form_Agent
{
    public partial class AGENT_HOLD_REMUN : System.Web.UI.Page
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

        //private void BindYearDropdown()
        //{
        //    DDL_YEAR.Items.Clear();
        //    DDL_YEAR.Items.Add(new ListItem("ALL", "ALL"));

        //    int currentYear = DateTime.Now.Year;

        //    // contoh: mulai 2024 sampai tahun sekarang + 1
        //    for (int year = 2024; year <= currentYear + 1; year++)
        //    {
        //        DDL_YEAR.Items.Add(new ListItem(year.ToString(), year.ToString()));
        //    }

        //    DDL_YEAR.SelectedValue = "ALL";
        //}

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 2";
            conn.ExecuteQuery();
            DDL_CHANNEL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            //BindYearDropdown();

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static PolicyResultDto GetPolicyList(
        string agentCode,
        string remunType,
        string filterDate,
        int page,
        int pageSize
)
        {
            PolicyResultDto result = new PolicyResultDto
            {
                Data = new List<PolicyDto>(),
                TotalRows = 0
            };

            int startRow = ((page - 1) * pageSize) + 1;
            int endRow = page * pageSize;

            DateTime? filterStartDate = null;

            if (!string.IsNullOrEmpty(filterDate))
            {
                if (!DateTime.TryParseExact(
                    filterDate,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsedDate))
                {
                    throw new Exception("Format tanggal filter tidak valid");
                }

                filterStartDate = parsedDate;
            }

            Connection conn = new Connection(
                GlobalUse.GetConnString(
                    ConfigurationManager.AppSettings["appid"]
                )
            );

            conn.QueryString = "EXEC MARKETING.dbo.SP_MAPPING_REMUN_TYPE_AGENT " +
                               "'" + agentCode + "', " +
                               "'" + remunType + "', " +
                               (filterStartDate == null ? "NULL" : "'" + filterStartDate.Value.ToString("yyyy-MM-dd") + "'") + ", " +
                               startRow + ", " +
                               endRow;

            conn.ExecuteQuery(600);

            int rowCount = conn.GetRowCount();

            for (int i = 0; i < rowCount; i++)
            {
                result.Data.Add(new PolicyDto
                {
                    PolicyNo = conn.GetFieldValue(i, "POLICY_NO").ToString(),
                    PolicyHolder = conn.GetFieldValue(i, "POLICY_HOLDER").ToString(),
                    Amount = Convert.ToDecimal(conn.GetFieldValue(i, "AMOUNT")),
                    StartDate = Convert.ToDateTime(conn.GetFieldValue(i, "START_DATE")),
                    EndDate = Convert.ToDateTime(conn.GetFieldValue(i, "END_DATE"))
                });
            }

            if (rowCount > 0)
                result.TotalRows = Convert.ToInt64(
                    conn.GetFieldValue(0, "TOTAL_ROWS")
                );

            return result;
        }


        public class PolicyDto
        {
            public string PolicyNo { get; set; }
            public string PolicyHolder { get; set; }
            public decimal Amount { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        public class PolicyResultDto
        {
            public long TotalRows { get; set; }
            public List<PolicyDto> Data { get; set; }
        }

        public class HoldRemunDetailDto
        {
            public string AgentCode { get; set; }
            public string RemunType { get; set; }
            public string PolicyNo { get; set; }
            public string InsuredName { get; set; }
            public decimal Amount { get; set; }
            public string PolicyStartDate { get; set; }
            public string PolicyEndDate { get; set; }
        }

        public class HoldRemunPayloadDto
        {
            public bool IsSelectAll { get; set; }
            public int TotalRows { get; set; }
            public string AgentCode { get; set; }     // ⭐ BARU
            public string RemunType { get; set; }     // ⭐ BARU
            public List<HoldRemunDetailDto> Items { get; set; }
        }

        public class TempFileInfo
        {
            public string OriginalFileName { get; set; }
            public string ContentType { get; set; }
            public long FileSize { get; set; }
            public byte[] Content { get; set; }
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void UploadTempFile(
            string agentCode,
            string fileName,
            string contentType,
            long fileSize,
            string base64Data
)
        {
            if (string.IsNullOrEmpty(base64Data))
                throw new Exception("File kosong");

            byte[] bytes = Convert.FromBase64String(base64Data);

            HttpContext.Current.Session["HOLD_FILE_" + agentCode] =
                new TempFileInfo
                {
                    OriginalFileName = fileName,
                    ContentType = contentType,
                    FileSize = fileSize,
                    Content = bytes
                };
        }

        private static string GenerateRandomString(int length = 10)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var rnd = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_CODE.Text.Trim() != "")
                where = where + " and a.CODE like '%" + TXT_CODE.Text.Trim() + "%' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_AGENCY.Text.Trim() != "")
                where = where + " and a.AGENCY_NAME like '%" + TXT_AGENCY.Text.Trim() + "%' ";

            if (TXT_UPLINER.Text.Trim() != "")
                where = where + " and a.UPLINER_NAME like '%" + TXT_UPLINER.Text.Trim() + "%' ";

            if (TXT_CHANNEL.Text.Trim() != "")
                where = where + " and a.SUBCD_DESCR like '%" + TXT_CHANNEL.Text.Trim() + "%' ";

            if (DDL_CHANNEL.SelectedValue != "")
                where = where + " and a.MARKET_SEGMENT = " + DDL_CHANNEL.SelectedValue + " ";

            conn.QueryString = @"
                                SELECT *
                                FROM (
                                    SELECT 
                                        a.CODE,
                                        FULLNAME =
                                            '<table style=""border-spacing:0px;width:100%;font-size:xx-small;text-wrap:normal;"">' +
                                            '   <tr><td style=""width:80px;"">FULLNAME</td><td>' + ISNULL(a.FULLNAME, '') + '</td></tr>' +
                                            '   <tr><td>DOB</td><td>' + CONVERT(varchar(20), a.DOB, 106) + '</td></tr>' +
                                            '   <tr><td>LEVEL</td><td>' + ISNULL(a.SUBCD_DESCR, '') + '</td></tr>' +
                                            '   <tr><td>UPLINER</td><td>' + ISNULL(a.UPLINER_NAME, '') + '</td></tr>' +
                                            '   <tr><td>BRANCH</td><td>' + ISNULL(a.BRANCH_DESCR, '') + '</td></tr>' +
                                            '</table>',
                                        REMUN_TYPE =
                                            '<div class=""remun-cell"">' +
                                                RT.REMUN_TYPE_HTML +
                                            '</div>'
                                    FROM V_M_AGENTS a
                                    CROSS APPLY (
                                        SELECT
                                            '<div class=""remun-wrapper"" style=""margin-bottom:5px;"">' +
                                                '<input type=""checkbox"" class=""remun-select-all-agent"" ' +
                                                ' data-agent=""' + CAST(a.CODE AS varchar(20)) + '"" ' +
                                                ' onclick=""selectAllRemunPerAgent(this)"" /> ' +
                                                '<span style=""font-weight:bold;"">Select All Remun Type</span>' +
                                            '</div>' +

                                            REPLACE(
                                                STUFF((
                                                    SELECT
                                                        '<div class=""remun-wrapper"">' +
                                                            '<label style=""white-space:nowrap;"">' +
                                                                '<input type=""checkbox"" class=""remun-check remun-item remun-' + CAST(a.CODE AS varchar(20)) + '"" ' +
                                                                ' data-agent=""' + CAST(a.CODE AS varchar(20)) + '"" ' +
                                                                ' data-remun=""' + rt.CODE + '"" ' +
                                                                ' value=""' + rt.CODE + '""' +
                                                                ' onclick=""onRemunClick(this)"" /> ' +
                                                                rt.DESCR +
                                                                ' <span class=""remun-label""></span>' +
                                                            '</label>' +
                                                        '</div>'
                                                    FROM PR_REMUN_TYPE rt
                                                    WHERE rt.CODE IN ('1','2','2a','2b','2c','2d')
                                                    ORDER BY rt.CODE
                                                    FOR XML PATH(''), TYPE
                                                ).value('.', 'varchar(max)'), 1, 0, ''),
                                                '&amp;', '&'
                                            ) AS REMUN_TYPE_HTML
                                    ) RT
                                    WHERE
                                    --    a.CODE NOT IN (
                                    --        SELECT CODE
                                    --        FROM M_AGENT_HOLD_REMUN
                                    --        WHERE APPROVEBY IS NULL OR END_DATE IS NULL
                                    --    )
                                    --    AND 
                                    a.ACTIVE = 1
                                        "
                                        + where +
                                        @"
                                ) x
                                ORDER BY FULLNAME;
                                ";

            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            if (!dt.Columns.Contains("REMUN_TYPE"))
            {
                throw new Exception("Kolom REMUN_TYPE tidak ditemukan di DataTable");
            }
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select convert(varchar(10), GETDATE(), 103)";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                TextBox txtSTARTDATE = (TextBox)DGR.Items[i].FindControl("TXT_STARTDATE");
                lb.Text = DGR.Items[i].Cells[1].Text;
                txtSTARTDATE.Text = conn.GetFieldValue(0, 0).ToString();
            }

            if(DGR.Items.Count == 0)
            {
                System.Threading.Thread.Sleep(3000);
                string message = "agen dalam proses hold remun";
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "ShowAlert('" + message + "');", true);
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

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            
            if (e.CommandName == "Detail")
            {
                Response.Redirect("Agent_Frame.aspx?AGENTCODE=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Approve")
            {
                string remunRaw = HF_SELECTED_REMUN_TYPE.Value;

                if (string.IsNullOrEmpty(remunRaw))
                    throw new Exception("Remun Type belum dipilih");

                string[] remunTypes = remunRaw
                    .Split(',')
                    .Select(x => x.Trim())
                    .Where(x => x != "")
                    .ToArray();
                string json = Request.Form[HF_POLICY_DETAIL.UniqueID];

                bool isSelectAll = HF_IS_SELECT_ALL.Value == "1";

                HoldRemunPayloadDto payload = null;

                if (!isSelectAll && !string.IsNullOrEmpty(json))
                {
                    payload = JsonConvert.DeserializeObject<HoldRemunPayloadDto>(json);
                }

                // ❌ ERROR hanya kalau BUKAN select all
                if (!isSelectAll && string.IsNullOrEmpty(json))
                    throw new Exception("Data policy belum dipilih");

                List<HoldRemunDetailDto> details =
                    payload?.Items ?? new List<HoldRemunDetailDto>();
                try
                {

                    string[] selectedAgents =
                        HF_SELECTED_AGENT.Value.Split(
                            new[] { ',' },
                            StringSplitOptions.RemoveEmptyEntries
                        );

                    if (selectedAgents.Length == 0)
                        throw new Exception("Tidak ada agent terpilih");

                    string strAgent = "";

                    for (int i = 0; i < DGR.Items.Count; i++)
                    {
                        string agentCode = DGR.Items[i].Cells[1].Text;

                        var tempFile =
                        Session["HOLD_FILE_" + agentCode] as TempFileInfo;

                        string uploadDir = Server.MapPath("~/Upload/Hold/");
                        if (!Directory.Exists(uploadDir))
                            Directory.CreateDirectory(uploadDir);

                        string storedFileName = null;
                        string fullPath = null;

                        if (tempFile != null)
                        {
                            storedFileName =
                                GenerateRandomString() + "_" + tempFile.OriginalFileName;

                            fullPath = Path.Combine(uploadDir, storedFileName);

                            File.WriteAllBytes(fullPath, tempFile.Content);
                        }

                        // hanya agent yg checkbox REMUN_TYPE = 1 dicentang
                        if (!selectedAgents.Contains(agentCode))
                            continue;

                        TextBox txtREASON =
                            (TextBox)DGR.Items[i].FindControl("TXT_REASON");
                        TextBox txtSTARTDATE =
                            (TextBox)DGR.Items[i].FindControl("TXT_STARTDATE");

                        if (txtREASON.Text.Trim() == "" ||
                            txtSTARTDATE.Text.Trim() == "")
                            continue;

                        strAgent += agentCode + ",";

                        conn.QueryString = "exec SP_M_AGENT_HOLD_REMUN " +
                            "'" + agentCode + "'," +
                            "'" + GlobalUse.GlobalDateFormat(
                                    txtSTARTDATE.Text.Trim(), "d/M/yyyy") + "'," +
                            "'" + txtREASON.Text.Trim().Replace("'", "") + "'," +
                            "'" + GlobalUse.GetUserMgmt(
                                    Session["s"].ToString(), "UserID") + "'," +
                            "'" + (tempFile?.OriginalFileName ?? "") + "'," +
                            "'" + (storedFileName ?? "") + "'," +
                            "'" + (fullPath ?? "") + "'," +
                            "'" + (tempFile?.ContentType ?? "") + "'," +
                            (tempFile?.FileSize ?? 0);

                        conn.ExecuteQuery(200000);

                        Session.Remove("HOLD_FILE_" + agentCode);

                        if (!isSelectAll)
                        {
                            foreach (var d in details.Where(x => x.AgentCode == agentCode))
                            {
                                if (!DateTime.TryParseExact(
                                        d.PolicyStartDate,
                                        "yyyy-MM-dd",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out DateTime policyStartDate))
                                {
                                    throw new Exception("Format PolicyStartDate tidak valid: " + d.PolicyStartDate);
                                }

                                if (!DateTime.TryParseExact(
                                        d.PolicyEndDate,
                                        "yyyy-MM-dd",
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out DateTime policyEndDate))
                                {
                                    throw new Exception("Format PolicyEndDate tidak valid: " + d.PolicyEndDate);
                                }

                                conn.QueryString = "exec SP_M_AGENT_HOLD_REMUN_DETAIL " +
                                    "'" + agentCode + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(txtSTARTDATE.Text, "d/M/yyyy") + "'," +
                                    "'" + txtREASON.Text.Replace("'", "") + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "'" + d.RemunType + "'," +
                                    "'" + d.PolicyNo + "'," +
                                    "'" + d.InsuredName.Replace("'", "") + "'," +
                                    d.Amount + "," +
                                    "'" + policyStartDate.ToString("yyyy-MM-dd") + "'," +
                                    "'" + policyEndDate.ToString("yyyy-MM-dd") + "'";

                                conn.ExecuteQuery();
                            }
                        }
                        else
                        {
                            // ===============================================
                            // MODE SELECT ALL ⭐⭐⭐⭐⭐
                            // DETAIL DIAMBIL LANGSUNG DARI DATABASE
                            // ===============================================

                            DateTime startDate = DateTime.ParseExact(
                                txtSTARTDATE.Text.Trim(),
                                "dd/MM/yyyy",
                                System.Globalization.CultureInfo.InvariantCulture
                            );
                            foreach (string remunType in remunTypes)
                            {
                                conn.QueryString =
                                    "EXEC dbo.SP_M_AGENT_HOLD_REMUN_DETAIL_SELECT_ALL " +
                                    "@CODE = '" + agentCode + "', " +
                                    "@START_DATE = '" + startDate.ToString("yyyy-MM-dd") + "', " +
                                    "@REMARK = '" + txtREASON.Text.Replace("'", "") + "', " +
                                    "@REQUESTBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                    "@REMUN_TYPE = '" + remunType + "', " +
                                    "@START_DATE_FILTER = '" + startDate.ToString("yyyy-MM-dd") + "' ";

                                conn.ExecuteQuery();
                            }
                        }

                        if (!isSelectAll && payload != null && payload.IsSelectAll)
                        {
                            DateTime startDate = DateTime.ParseExact(
                                txtSTARTDATE.Text.Trim(),
                                "dd/MM/yyyy",
                                CultureInfo.InvariantCulture
                            );

                            conn.QueryString =
                                "EXEC dbo.SP_M_AGENT_HOLD_REMUN_DETAIL_SELECT_ALL " +
                                "@CODE = '" + payload.AgentCode + "', " +
                                "@START_DATE = '" + startDate.ToString("yyyy-MM-dd") + "', " +
                                "@REMARK = '" + txtREASON.Text.Replace("'", "") + "', " +
                                "@REQUESTBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                "@REMUN_TYPE = '" + payload.RemunType + "', " +
                                "@START_DATE_FILTER = '" + startDate.ToString("yyyy-MM-dd") + "' ";

                            conn.ExecuteQuery(600);
                        }
                    }

                    if (strAgent != "")
                        strAgent = strAgent.TrimEnd(',');

                    string message = string.Format(
                        "Agent code : {0} berhasil di hold", strAgent);

                    ScriptManager.RegisterStartupScript(
                        this, GetType(), "alertMessage",
                        "ShowAlert('" + message + "');", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(
                        this, GetType(), "alertMessage",
                        "ShowAlert('" + ex.Message + "');", true);
                }

                DGR.CurrentPageIndex = 0;
                FillDGR();
            }

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