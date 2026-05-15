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
    public partial class AGENT_PRODUCTION_TRANSFER_DETAIL : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["AGENTCODE"].ToString();
                LoadAgent();
                LoadPortfolio();
            }
        }

        protected void LoadAgent()
        {
            //conn.QueryString = "select " +
            //                    "BRANCH_DESCR       = a.BRANCH_DESCR, " +
            //                    "FULLNAME           = a.FULLNAME, " +
            //                    "SUBCD_DESCR        = a.SUBCD_DESCR, " +
            //                    "UPLINER            = bb.CODE + ' - ' + bb.FULLNAME, " +
            //                    "CASES              = isnull(c.CASES, 0), " +
            //                    "REQ_CASES          = isnull(cc.CASES, 0), " +
            //                    "LAST_REQ_BY        = LTRIM(RTRIM((isnull(b.FRONT_NAME, '') + ' ' + isnull(b.MID_NAME, '') + ' ' + isnull(b.LAST_NAME, '')))), " +
            //                    "LAST_REQ_DATE      = cc.REQUEST_DATE " +
            //                    "from               V_M_AGENTS a " +
            //                    "left join          V_M_AGENTS bb on a.UPLINER = bb.CODE " +
            //                    "left join          (select AGENT_CODE, CASES = count(CUSTOMER_ID) from V_M_AGENTS_PORTFOLIO group by AGENT_CODE) c on a.CODE = c.AGENT_CODE " +
            //                    "left join          (select " +
            //                    "                            AGENT_CODE = CODE, " +
            //                    "                            CASES           = count(CUSTOMER_ID), " +
            //                    "                            REQUEST_BY      = MIN(REQUEST_BY), " +
            //                    "                            REQUEST_DATE    = MAX(REQUEST_DATE) " +
            //                    "                            from M_AGENT_PRODUCTION_TRANSFER " +
            //                    "                            where " +
            //                    "                            APPROVE_DATE    is null " +
            //                    "                            and NEW_AGENT_CODE is not null " +
            //                    "                            group by CODE " +
            //                    "                        ) cc on a.CODE = cc.AGENT_CODE " +
            //                    "left join       V_LINK_SC_M_USERS b on cc.REQUEST_BY = b.CODE collate database_default " +
            //                    "where a.CODE   = '" + LB_CODE.Text + "'";
            //conn.ExecuteQuery();

            //LB_BRANCH.Text = conn.GetFieldValue("BRANCH_DESCR").ToString();
            //LB_FULLNAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            //LB_LEVEL.Text = conn.GetFieldValue("SUBCD_DESCR").ToString();
            //LB_UPLINER.Text = conn.GetFieldValue("UPLINER").ToString();
            //LB_CASES.Text = conn.GetFieldValue("CASES").ToString();            
            //LB_TCASES.Text = conn.GetFieldValue("REQ_CASES").ToString();
            //LB_REQBY.Text = conn.GetFieldValue("LAST_REQ_BY").ToString();
            //LB_REQDATE.Text = conn.GetFieldValue("LAST_REQ_DATE").ToString();

            conn.QueryString = "select " +
                                "[AGENT CODE]       = a.AGENT_CODE,  " +
                                "[AGENT NAME]       = a.AGENT_NAME,  " +
                                "[DIV IN CHARGE]    = a.MARKET_SEGMENT,  " +
                                "[LEVEL]            = a.AGENT_LEVEL,  " +
                                "[#CASES]			= count(a.AGENT_CODE)  " +
                                "from               V_M_AGENTS_PORTFOLIO a  " +
                                "where  " +
                                "a.AGENT_CODE collate database_default in (select a.Item from SECURITY.dbo.SplitStrings_XML('" + LB_CODE.Text + "', ',') a) " +
                                "group by " +
                                "a.AGENT_CODE, " +
                                "a.AGENT_NAME, " +
                                "a.MARKET_SEGMENT, " +
                                "a.AGENT_LEVEL " +
                                "order by 2";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void LoadPortfolio()
        {
            conn.QueryString = "select URL = '../../ReportViewer/Viewer.aspx?APPID=' + APP_ID + '&CODE=' + convert(varchar(10), CODE) + '&AGENT_CODE=' from SECURITY.dbo.REPORT_LIST where APP_ID = 'AGR' and REPORT_NAME = 'RPT_PRODUCTION_TRANSFER_REQUEST'";
            conn.ExecuteQuery();
            string URL = conn.GetFieldValue("URL").ToString() + LB_CODE.Text;
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.ProdTransferBody.location.href = '" + URL + "';</script>");
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            if (!FU.HasFile)
                return;

            string filename = Path.GetFileName(FU.FileName);
            string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }
            FU.SaveAs(fullpath);

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

            //string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);
            con.Open();

            DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = "";
            if (extension == "xlsx")
            {
                foreach (DataRow row in sheets.Rows)
                {
                    //LB_ERR.Text = LB_ERR.Text + row["TABLE_NAME"].ToString();
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
                //LB_ERR.Text = sheets.Rows[0]["TABLE_NAME"].ToString();
            }
            //OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + SheetName + "]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow myRow in dt.Rows)
            {
                try
                {
                    conn.QueryString = "exec SP_M_AGENT_PRODUCTION_TRANSFER_INSERT " +
                                        "'" + myRow[0].ToString().Trim().Replace("'", "`") + "'," +
                                        "'" + myRow[1].ToString().Trim().Replace("'", "`") + "'," +
                                        "'" + myRow[2].ToString().Trim().Replace("'", "`") + "'," +
                                        "'" + myRow[3].ToString().Trim().Replace("'", "`") + "'," +
                                        "'" + myRow[4].ToString().Trim().Replace("'", "`") + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
            }
            con.Close();

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            LoadPortfolio();
        }
    }
}