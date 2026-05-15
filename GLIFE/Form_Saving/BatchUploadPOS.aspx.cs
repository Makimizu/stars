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
    public partial class BatchUploadPOS : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["mode"];
                FillDGRPolicy();
            }
        }

        protected void FillDGRPolicy()
        {
            DV_POLICY.Visible = true;


            string product_filer = "and GROUP_CODE in ('SP','GTLR') ";
            if (LB_MODE.Text == "SV_UPD")
                product_filer = "";

            conn.QueryString = "select * from V_POLICY_GTL " +
                                "where " +
                                "isnull(TC_DESCR,'')<>'' " +
                                product_filer +
                                "and (POLICY_NO like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or COMPANY_NAME like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or TC_DESCR like '%" + TXT_POLICYSEARCH.Text.Trim() + "%') " +
                                //"and isnull(STAT,0) = 1 " +
                                "order by TC_DESCR, COMPANY_NAME";
            conn.ExecuteQuery(150000);

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
            }
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
                                "'" + LB_MODE.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE() " +
                                "select BATCH_ID = @BATCH_ID";
            conn.ExecuteQuery(150000);
            string BATCH_ID = conn.GetFieldValue("BATCH_ID").ToString();

            //string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FullPath + @";Extended Properties=""Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);

            try
            {
                con.Open();

                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
                //OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + LB_MODE.Text + "$]", con);
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
                                    "''," +
                                    "'" + LB_MODE.Text + "'," +
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
                    conn.QueryString = sql + "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', GETDATE()";
                    conn.ExecuteNonQuery();
                }


                con.Close();

                conn.QueryString = "select INBOUND from V_LINK_UB_PR_ENDORSEMENT_TYPE_INBOUND where CODE = '" + LB_MODE.Text + "'";
                conn.ExecuteQuery(150000);
                if (conn.GetFieldValue("INBOUND").ToString() == "0")
                    Task.Run(() => ProcessBatch(BATCH_ID));
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
            GlobalUse.SQLToFile("XLS_" + LB_MODE.Text + ".xls",
                                "select THEFILE from ARCHIEVE.dbo.GL_ARSIP where CODE='XLS_" + LB_MODE.Text + "'",
                                Page);
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            if (!FU.HasFile)
                return;


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
                Response.Redirect("BatchListPOS.aspx?mode=" + LB_MODE.Text);
        }

        protected void LBT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_POS_TEMPLATE_XLS " + LB_POLICYID.Text + ",'" + LB_MODE.Text + "'";
            conn.ExecuteQuery(150000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, LB_MODE.Text, true);
        }
    }
}