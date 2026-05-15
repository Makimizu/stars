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

namespace GLIFE.Form_Saving
{
    public partial class BatchUpload : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGRPolicy();
            }
        }

        protected void FillDGRPolicy()
        {
            DV_POLICY.Visible = true;

            conn.QueryString = "select * from V_POLICY " +
                                "where " +
                                "GROUP_CODE = 'SP' " +
                                "and isnull(TC_DESCR,'')<>'' " +
                                "and (POLICY_NO like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or COMPANY_NAME like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or TC_DESCR like '%" + TXT_POLICYSEARCH.Text.Trim() + "%') " +
                                "and isnull(STAT,0) = 1 " +
                                "order by TC_DESCR, COMPANY_NAME";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_POLICY.DataSource = dt;
            DGR_POLICY.DataBind();

            for (int i = 0; i < DGR_POLICY.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR_POLICY.Items[i].FindControl("LBT_POLICY");

                lbCODE.Text = DGR_POLICY.Items[i].Cells[2].Text; ;
            }
        }

        protected void TXT_POLICYSEARCH_TextChanged(object sender, EventArgs e)
        {
            FillDGRPolicy();
        }

        protected void DGR_POLICY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                DV_POLICY.Visible = false;
                DV_UPLOAD.Visible = true;

                LB_POLICYID.Text = e.Item.Cells[0].Text;
                LB_POLICYNO.Text = e.Item.Cells[2].Text;
                LB_PRODUCT.Text = e.Item.Cells[4].Text;
                LB_COMPANY.Text = e.Item.Cells[5].Text;
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
                                "FULLNAME = UPPER(LTRIM(isnull(c.FRONT_NAME,'')) + RTRIM(' ' + isnull(c.MID_NAME,'')) + RTRIM(' ' + isnull(c.LAST_NAME,'')))  " +
                                "from POLICY_CHANNEL_DISTRIBUTION a " +
                                "inner join V_LINK_MARKETING_M_AGENTS c on a.SUBCD = c.SUBCD  " +
                                "where  " +
                                "a.ID = " + LB_POLICYID.Text + " " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_AGENT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_AGENT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            try
            {
                DDL_AGENT.SelectedValue = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
                DDL_AGENT.Enabled = false;
            }
            catch { }
        }

        protected void FillBranch()
        {
            DDL_BRANCH.Items.Clear();
            conn.QueryString = "select b.BRANCH_CODE, b.NAMA_CABANG " +
                                "from POLICY a " +
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
            conn.QueryString = "select b.CODE, b.DESCR " +
                                "from POLICY_UW_EXCEL_UPLOAD_FORMAT a " +
                                "inner join PR_UW_EXCEL_UPLOAD_FORMAT b on a.FORMAT_CODE = b.CODE " +
                                "where a.POLICY_ID =  '" + LB_POLICYID.Text + "' " +
                                "order by 1";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_FORMAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DGR_POLICY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_POLICY.CurrentPageIndex = e.NewPageIndex;
            FillDGRPolicy();
        }

        protected void LBT_POLICYBACK_Click(object sender, EventArgs e)
        {
            DV_POLICY.Visible = true;
            DV_UPLOAD.Visible = false;
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
                Response.Redirect("BatchList.aspx");
        }


        protected void ProcessSpecialTemplate(string filename, string FullPath)
        {
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

                //DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                //OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [SV_NEW$]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                int IDX = 0;
                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    string sql = "insert into APPLICATION_DATA_RAW select " +
                                    "'U' + dbo.UFN_GET_NEWID() + RIGHT('000'+convert(varchar(10)," + IDX.ToString() + "),3)," +
                                    "'" + BATCH_ID + "'," +
                                    "'" + LB_POLICYID.Text + "'," +
                                    "'" + DDL_BRANCH.SelectedValue + "'," +
                                    "'" + DDL_FORMAT.SelectedValue + "'," +
                                    "'',";

                    for (int i = 0; i < 70; i++)
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
                //Task.Run(() => ProcessBatch(BATCH_ID));
            }
            catch (System.Exception ex)
            {
                con.Close();
                conn.QueryString = "delete from BATCH_MASTER where ID = '" + BATCH_ID + "'";
                conn.ExecuteNonQuery();
                LB_ERR.Text = ex.Message;
            }

        }

        protected void ProcessBatch(string batchid)
        {
            conn.QueryString = "exec SP_BATCH_MASTER_UPLOAD_SAVING '" + batchid + "'";
            conn.ExecuteNonQuery();
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
                                "inner join V_LINK_CB_COMPANY b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                "where " +
                                "b.POLICY_NO = '" + LB_POLICYNO.Text + "' " +
                                "order by 2";

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "XLS_" + LB_COMPANY.Text.Replace(" ", ""), false);
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
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
                Response.Redirect("BatchList.aspx");
        }
    }
}