using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Threading.Tasks;


namespace GLIFE_PROPOSAL
{
    public partial class QuotationBatchMember : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_POLICYID.Text = Request.QueryString["ID"].ToString();
                LB_QUOTNO.Text = Request.QueryString["QUOTNO"].ToString();
                LB_VERNO.Text = Request.QueryString["VERNO"].ToString();

                Setup();
            }
        }

        protected void Setup()
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
            FillAgent();
            FillBranch();
            FillFormat();
        }

        protected void DGR_POLICY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {

                FillBranch();
                FillFormat();
                FillAgent();
            }
        }

        protected void FillAgent()
        {
            DDL_AGENT.Items.Clear();
            conn.QueryString = "select " +
                                "c.CODE,  " +
                                "FULLNAME = c.CODE + ' - ' + UPPER(LTRIM(isnull(c.FRONT_NAME,'')) + RTRIM(' ' + isnull(c.MID_NAME,'')) + RTRIM(' ' + isnull(c.LAST_NAME,'')))  " +
                                "from V_LINK_GLIFE_POLICY_CHANNEL_DISTRIBUTION a " +
                                "inner join V_LINK_MARKETING_M_AGENTS c on a.SUBCD = c.SUBCD  " +
                                "where  " +
                                "a.ID = " + LB_POLICYID.Text + " " +
                                "order by UPPER(LTRIM(isnull(c.FRONT_NAME,'')) + RTRIM(' ' + isnull(c.MID_NAME,'')) + RTRIM(' ' + isnull(c.LAST_NAME,'')))";
            conn.ExecuteQuery();
            DDL_AGENT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_AGENT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            try
            {
                DDL_AGENT.SelectedValue = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
            }
            catch { }
        }

        protected void FillBranch()
        {
            DDL_BRANCH.Items.Clear();
            conn.QueryString = "select b.BRANCH_CODE, b.NAMA_CABANG " +
                                "from V_LINK_GLIFE_POLICY a " +
                                "inner join V_LINK_CB_BRANCH b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                "where " +
                                "a.ID = '" + LB_POLICYID.Text + "' " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BRANCH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillFormat()
        {
            DDL_FORMAT.Items.Clear();
            conn.QueryString = "select FORMAT_CODE, FORMAT_DESCR from V_LINK_GLIFE_POLICY_UW_EXCEL_UPLOAD_FORMAT " +
                                "where POLICY_ID =  '" + LB_POLICYID.Text + "' " +
                                "order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_FORMAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LBT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            if (!FU.HasFile)
                return;

            if (DDL_FORMAT.SelectedValue == "")
                return;

            if (DDL_AGENT.SelectedValue == "")
            {
                LB_ERR.Text = "Agent has not been selected yet";
                return;
            }

            string filename = Path.GetFileName(FU.FileName);
            string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            FU.SaveAs(fullpath);
            ProcessSpecialTemplate(filename, fullpath);

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            if (LB_ERR.Text.Trim() == "")
            {
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void ProcessSpecialTemplate(string filename, string FullPath)
        {
            conn.QueryString =  "declare @BATCH_ID uniqueidentifier " +
                                "select @BATCH_ID = BATCH_ID from QUOTATION_VERSION where QUOTNO = '" + LB_QUOTNO.Text + "' and VERNO = " + LB_VERNO.Text + " " +
                                "update QUOTATION_VERSION set BATCH_ID = null where QUOTNO = '" + LB_QUOTNO.Text + "' and VERNO = " + LB_VERNO.Text + " " +
                                "delete from BATCH_MASTER where ID = @BATCH_ID";
            conn.ExecuteNonQuery();

            conn.QueryString = "declare @BATCH_ID uniqueidentifier " +
                                "set @BATCH_ID = NEWID() " +
                                "insert into BATCH_MASTER select " +
                                "@BATCH_ID," +
                                "'U'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE() " +
                                "select BATCH_ID = @BATCH_ID";
            conn.ExecuteQuery();
            string BATCH_ID = conn.GetFieldValue("BATCH_ID").ToString();

            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);

            try
            {
                con.Open();

                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                int IDX = 0;
                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    string sql = "insert into QUOTATION_DATA_RAW select " +
                                    "'U' + dbo.UFN_GET_NEWID() + RIGHT('000'+convert(varchar(10)," + IDX.ToString() + "),3)," +
                                    "'" + BATCH_ID + "'," +
                                    "'" + LB_POLICYID.Text + "'," +
                                    "'" + DDL_BRANCH.SelectedValue + "'," +
                                    "'" + DDL_FORMAT.SelectedValue + "'," +
                                    "'',";

                    for (int i = 0; i < 60; i++)
                    {
                        string val = "";
                        try
                        {
                            val = myRow[i].ToString().Trim().Replace("'", "`");
                        }
                        catch { }

                        sql = sql + "'" + val + "',";
                    }
                    conn.QueryString = sql + "'" + DDL_AGENT.SelectedValue + "', GETDATE()";
                    conn.ExecuteNonQuery();
                }



                con.Close();
            }
            catch (System.Exception ex)
            {
                con.Close();
                conn.QueryString = "delete from BATCH_MASTER where ID = '" + BATCH_ID + "'";
                conn.ExecuteNonQuery();
                LB_ERR.Text = ex.Message;
                return;
            }


            //Task.Run(() => ProcessBatch(BATCH_ID));
            ProcessBatch(BATCH_ID);
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void ProcessBatch(string BATCH_ID)
        {
            conn.QueryString = "update QUOTATION_VERSION set BATCH_ID = '" + BATCH_ID + "' where QUOTNO = '" + LB_QUOTNO.Text + "' and VERNO = " + LB_VERNO.Text + " " +
                                "exec SP_BATCH_MASTER_UPLOAD '" + BATCH_ID + "'";
            conn.ExecuteQuery(50000);
        }

        protected void LBT_TEMPLATE_XLS_Click(object sender, EventArgs e)
        {
            GlobalUse.SQLToFile("XLS_" + DDL_FORMAT.SelectedValue + ".xls",
                                "select THEFILE from ARCHIEVE.dbo.GL_ARSIP where CODE='XLS_" + DDL_FORMAT.SelectedValue + "'",
                                Page);
        }

        protected void LB_BRANCHXLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select a.BRANCH_CODE, a.NAMA_CABANG " +
                                "from V_LINK_CB_BRANCH a " +
                                "inner join V_LINK_GLIFE_POLICY b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                "where " +
                                "b.ID = '" + LB_POLICYID.Text + "' " +
                                "order by 2";

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "XLS_" + LB_VERNO.Text.Replace(" ", ""), false);
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "a.FULLNAME, " +
                                "DOB = convert(varchar(20),a.DOB,106)," +
                                "GENDER = (case when a.SEX='M' then 'Male' else 'Female' end)," +
                                "a.COMPANY_NAME, " +
                                "a.BRANCH_CODE, " +
                                "a.UW_CODE, " +
                                "SUMINS = replace(convert(varchar(100), convert(money, a.SUMINS),1), '.00','')," +
                                "PREMIUM = replace(convert(varchar(100), convert(money, a.PREMIUM),1), '.00',''), " +
                                "REGDATE = convert(varchar(100), a.USERDATE), " +
                                "MQ = ((isnull(q.CNT,0) + 1) *isnull(tc.CNT,0)) - isnull(tcq.CNT,0) " +
                                "from V_QUOTATION_MASTER a " +
                                "inner join QUOTATION_VERSION b on a.BATCH_ID = b.BATCH_ID and b.QUOTNO = '" + LB_QUOTNO.Text + "' and VERNO = " + LB_VERNO.Text + " " +
                                 "inner join (select TC_CODE, CNT = count(CODE) from V_LINK_UW_TC_QUESTIONS group by TC_CODE) tc on a.TC_ID = tc.TC_CODE " +
                                 "left join (select REGNO, CNT = count(CODE) from APPLICATION_TC_QUESTIONS where LTRIM(isnull(VALUE,'')) <> '' group by REGNO) tcq on a.REGNO = tcq.REGNO " +
                                 "left join (select REGNO, CNT = count(SEQ) from APPLICATION_JOIN_ACCOUNT group by REGNO) q on a.REGNO = q.REGNO " +
                                 "order by USERDATE";
            conn.ExecuteQuery();

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
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                lbCODE.Text = DGR.Items[i].Cells[1].Text; ;

                if (DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "") == "")
                    cb.Visible = false;

                //if (DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "") != "0")
                //    cb.Visible = false;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("QuotationREGNO.aspx?REGNO=" + e.Item.Cells[1].Text);
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Visible)
                    cb.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}