using System;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR.Form_Agent
{
    public partial class ENDORSEMENT_BATCH : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["mode"].ToString();
                LB_SEQ.Text = Request.QueryString["seq"].ToString();
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select DESCR from PR_BATCH_TYPE where CODE = '" + LB_MODE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();

            if (LB_SEQ.Text != "1")
            {
                DV_UPLOAD.Visible = false;
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "BATCH_ID = BATCH_ID, " +
                                "DESCR = ENDORSEMENT_TYPE_DESCR, " +
                                "RECORDS         = '<a href=''' + '../../ReportViewer/Viewer.aspx?APPID=' + b.APP_ID + '&CODE=' + convert(varchar(10), b.CODE) + '&ID=' + convert(varchar(255), a.BATCH_ID) + '''>' + convert(varchar(10), RECORDS) + '</a>', " +
                                "CREATEBY        = CREATEBY + ' (' + convert(varchar(100), CREATEDATE) + ')', " +
                                "DECISION        = DECISION_DESCR, " +
                                "AUTHOR          = AUTH_BY + ' (' + convert(varchar(100), AUTH_DATE) + ')' " +
                                "from            V_BATCH_ENDORSEMENT a " +
                                "inner join      SECURITY.dbo.REPORT_LIST b on b.APP_ID = 'AGR' and b.REPORT_NAME = 'RPT_BATCH_ENDORSEMENT' " +
                                "where " +
                                "ENDORSEMENT_TYPE = '" + LB_MODE.Text + "' " +
                                "and isnull(a.DECISION, 0)		= (case when " + LB_SEQ.Text + " in (1, 2) then 0 else a.DECISION end) " +
                                "order by " +
                                "a.CREATEDATE desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btAPPROVE = (Button)DGR.Items[i].FindControl("BT_APPROVE");
                Button btX = (Button)DGR.Items[i].FindControl("BT_X");

                btAPPROVE.Attributes.Add("onclick", "if(!confirm('Are you sure to APPROVE ?')){return false;};");
                btX.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");

                switch (LB_SEQ.Text)
                {
                    case "1":
                        btAPPROVE.Visible = false;
                        break;
                    case "2":
                        btAPPROVE.Visible = true;
                        btX.Visible = true;
                        break;
                    default:
                        btAPPROVE.Visible = false;
                        btX.Visible = false;
                        break;
                }
            }

            switch (LB_SEQ.Text)
            {
                case "1":
                    DGR.Columns[4].Visible = false;
                    DGR.Columns[5].Visible = false;
                    break;
                case "2":
                    DGR.Columns[4].Visible = false;
                    DGR.Columns[5].Visible = false;
                    break;
                default:
                    DGR.Columns[4].Visible = true;
                    DGR.Columns[5].Visible = true;
                    break;
            }
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            GlobalUse.SQLToFile(LB_TITLE.Text.Replace(" ", "_") + ".xls",
                                    "select THEFILE from ARCHIEVE.dbo.AGR_ARSIP where CODE = '" + LB_MODE.Text + "'",
                                    Page);
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            if (!FU1.HasFile)
                return;

            conn.QueryString = "declare @ID uniqueidentifier " +
                                "set @ID = NEWID() " +
                                "insert into BATCH_MASTER select @ID, '" + LB_MODE.Text + "', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', GETDATE() " +
                                "select ID = @ID";
            conn.ExecuteQuery();
            string BATCHID = conn.GetFieldValue("ID").ToString();

            string filename = Path.GetFileName(FU1.FileName);
            string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
            FU1.SaveAs(fullpath);

            string extension = "";
            string connstr = "";

            var dataSplit = filename.Split(new string[] { "." }, StringSplitOptions.None);
            extension = dataSplit[dataSplit.Length - 1];

            if (extension == "xlsx")
            {
                connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fullpath + @";Extended Properties=""Excel 12.0 Xml;HDR=YES""";
            }
            else
            {
                connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            }

            OleDbConnection con = new OleDbConnection(connstr);
            con.Open();

            DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = "";
            if (extension == "xlsx")
            {
                foreach (DataRow row in sheets.Rows)
                {
                    if (row["TABLE_NAME"].ToString().EndsWith("$"))
                    {
                        SheetName = row["TABLE_NAME"].ToString();
                        break;
                    }
                }

                if (SheetName == "")
                {
                    SheetName = sheets.Rows[0]["TABLE_NAME"].ToString();
                }
            }
            else
            {
                SheetName = sheets.Rows[0]["TABLE_NAME"].ToString();
            }

            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + SheetName + "]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int SEQ = 1;

            foreach (DataRow myRow in dt.Rows)
            {
                if (myRow[0].ToString().Trim() == "")
                    continue;

                conn.QueryString = "insert into BATCH_DETAIL_RAW select '" + BATCHID + "'," + SEQ.ToString();

                for (int i = 0; i < 60; i++)
                {
                    try
                    {
                        conn.QueryString = conn.QueryString + ",'" + myRow[i].ToString().Trim().Replace("'", "`") + "'";
                    }
                    catch
                    {
                        conn.QueryString = conn.QueryString + ",''";
                    }
                }

                conn.ExecuteNonQuery();
                SEQ++;
            }
            con.Close();

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            conn.QueryString = "exec SP_BATCH_ENDORSEMENT_UPLOAD_CONFIRMATION '" + BATCHID + "'";
            conn.ExecuteNonQuery();

            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Archieve")
            {
                string URL = "ENDORSEMENT_ARCHIEVE_FRAME.aspx?ID=" + e.Item.Cells[0].Text + "&USERID=" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "&mode=" + LB_MODE.Text;
                Response.Redirect(URL);
            }

            if (e.CommandName == "Approve")
            {
                conn.QueryString = "exec SP_BATCH_ENDORSEMENT_APPROVE '" + e.Item.Cells[0].Text + "',1,'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from BATCH_MASTER where ID = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }
    }
}