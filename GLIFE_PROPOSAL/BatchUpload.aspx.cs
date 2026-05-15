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

namespace GLIFE_PROPOSAL
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
                if (Session["s"] == null)
                    Response.Redirect("logout.aspx");
                FillDGRPolicy();
            }
        }

        protected void FillDGRPolicy()
        {
            DV_POLICY.Visible = true;

            conn.QueryString = "select a.*,VAL = CASE WHEN f.VAL = 1 THEN 'CLOSE' ELSE 'OPEN' END,PERIOD_START = convert(varchar(20),g.PERIOD_START,103),PERIOD_END = convert(varchar(20),g.PERIOD_END,103),STNC = convert(varchar(20),GETDATE(),103) from V_LINK_GLIFE_POLICY a " +
                                "inner join GLIFE.dbo.POLICY_OTHER_SETUP f on a.ID = f.POLICY_ID and f.CODE = 'POL10' " +
                                "left join GLIFE.dbo.POLICY_PERIOD g on a.ID = g.POLICY_ID " +
                                "where " +
                                "isnull(DESCR,'')<>'' " +
                                "and (POLICY_NO like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or COMPANY_NAME like '%" + TXT_POLICYSEARCH.Text.Trim() + "%' " +
                                "or DESCR like '%" + TXT_POLICYSEARCH.Text.Trim() + "%') " +
                                "and isnull(STAT,0) = 1 " +
                                "order by DESCR, COMPANY_NAME";
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

                //string url = "/GLIFE/Form_Policy/PolicyFrameProposal.aspx?ID=" + DGR_POLICY.Items[i].Cells[0].Text + "&userid=" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "&type=3";

                //lbCODE.Attributes.Add("onclick", "if (confirm('Cek Polis = OK ; Upload = Cancel') == true) {window.open('" + url + "'); return false} else {};");

            }

            TXT_STNC.Text = conn.GetFieldValue("STNC").ToString();

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
                LB_POLICY_TYPE.Text = e.Item.Cells[6].Text;
                LB_POLICY_PERIODE_START.Text = e.Item.Cells[7].Text;
                LB_POLICY_PERIODE_END.Text = e.Item.Cells[8].Text;
                //TXT_STNC.Text = e.Item.Cells[10].Text;
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
                DDL_AGENT.Enabled = false;
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
                Response.Redirect("BatchUploadList.aspx");
        }


        protected void ProcessSpecialTemplate(string filename, string FullPath)
        {
            LB_ERR.Text = "";
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


            string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";

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
                    //string sqlstnc = "insert into stnc select " +
                    //                "'U' + dbo.UFN_GET_NEWID() + RIGHT('000'+convert(varchar(10)," + IDX.ToString() + "),3)," +
                    //                "'" + BATCH_ID + "'," +
                    //                "'" + LB_POLICYID.Text + "'," +                       
                    //                "'" + DDL_BRANCH.SelectedValue + "'," +
                    //                "'" + DDL_FORMAT.SelectedValue + "'," +
                    //                "'" + TXT_STNC.Text + "'," +
                    //                "'',";

                    for (int i = 0; i < 60; i++)
                    {
                        string val = "";
                        try
                        {

                            val = myRow[i].ToString().Trim().Replace("'", "`");

                            if (LB_POLICY_TYPE.Text == "CLOSE")
                            {
                                if (i == 11)
                                {
                                    DateTime enteredDate = DateTime.ParseExact(val, "d/M/yyyy", null);//DateTime.Parse(val);
                                    DateTime endDate = DateTime.ParseExact(LB_POLICY_PERIODE_START.Text, "d/M/yyyy", null);//DateTime.Parse(LB_POLICY_PERIODE_START.Text);
                                    if (enteredDate < endDate)//DateTime.Parse(LB_POLICY_PERIODE_END.Text))
                                    {
                                        con.Close();
                                        //conn.QueryString = "delete from BATCH_MASTER where ID = '" + BATCH_ID + "'";
                                        //conn.ExecuteNonQuery();
                                        LB_ERR.Text = "POLICY TYPE CLOSE PLEASE CHECK PERIODE";

                                        //LB_ERR.Text = "POLICY TYPE CLOSE PLEASE CHECK PERIODE";
                                        return;
                                    }

                                }

                                if (i == 12)
                                {
                                    DateTime enteredDate = DateTime.ParseExact(val, "d/M/yyyy", null);//DateTime.Parse(val);
                                    //DateTime endDate = DateTime.Parse(LB_POLICY_PERIODE_END.Text);
                                    DateTime endDate = DateTime.ParseExact(LB_POLICY_PERIODE_END.Text, "d/M/yyyy", null);//DateTime.Parse(LB_POLICY_PERIODE_END.Text);
                                    if (enteredDate != endDate)
                                    {
                                        //LB_ERR.Text = "POLICY TYPE CLOSE POLICY PERIODE END";
                                        //return;
                                        con.Close();
                                        //conn.QueryString = "delete from BATCH_MASTER where ID = '" + BATCH_ID + "'";
                                        //conn.ExecuteNonQuery();
                                        LB_ERR.Text = "POLICY TYPE CLOSE POLICY PERIODE END";
                                    }
                                }
                            }

                            if (LB_POLICY_TYPE.Text == "OPEN")
                            {
                                if (i == 11)
                                {
                                    DateTime enteredDate = DateTime.ParseExact(val, "d/M/yyyy", null);
                                    DateTime endDate = DateTime.ParseExact(LB_POLICY_PERIODE_END.Text, "d/M/yyyy", null);
                                    if (enteredDate > endDate)
                                    {
                                        con.Close();
                                        LB_ERR.Text = "Tanggal Mulai Asuransi peserta tidak boleh melebihi tanggal akhir periode polis!";
                                        return;
                                    }
                                }
                            }

                            if (LB_POLICY_TYPE.Text == "OPEN")
                            {
                                if (i == 11)
                                {
                                    DateTime enteredDate = DateTime.ParseExact(val, "d/M/yyyy", null);
                                    DateTime endDate = DateTime.ParseExact(LB_POLICY_PERIODE_START.Text, "d/M/yyyy", null);
                                    if (enteredDate < endDate)
                                    {
                                        con.Close();
                                        LB_ERR.Text = "Tanggal Mulai Asuransi peserta tidak boleh kurang dari tanggal awal periode polis!";
                                        return;
                                    }
                                }
                            }
                        }
                        catch { }



                        sql = sql + "'" + val + "',";
                    }
                    conn.QueryString = sql + "'" + DDL_AGENT.SelectedValue + "', GETDATE()";
                    conn.ExecuteNonQuery();
                    //if (TXT_STNC.Text != "" )
                    //{
                    //    conn.QueryString = sqlstnc + "'" + DDL_AGENT.SelectedValue + "', GETDATE()";
                    //    conn.ExecuteNonQuery();
                    //}
                    //else {
                    //    conn.QueryString = sql + "'GETDATE()'," + "'" + DDL_AGENT.SelectedValue + "', GETDATE()";
                    //    conn.ExecuteNonQuery();
                    //}

                }



                con.Close();
            }
            catch (System.Exception ex)
            {
                con.Close();
                conn.QueryString = "delete from BATCH_MASTER where ID = '" + BATCH_ID + "'";
                conn.ExecuteNonQuery();
                LB_ERR.Text = ex.Message;
            }

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
    }
}
