using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Data.OleDb;

namespace GLIFE.Form_App
{
    public partial class ApplicationBenefitSeries : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["ID"].ToString();
                LoadSeries();
            }
        }

        protected void LoadSeries()
        {
            conn.QueryString = "select " +
                                "SEQ, " +
                                "VALUEDATE = convert(varchar(20),VALUEDATE,106), " +
                                "START_BALANCE = replace(convert(varchar(100),convert(money,START_BALANCE),1),'.00',''), " +
                                "PRINCIPAL = replace(convert(varchar(100),convert(money,PRINCIPAL),1),'.00',''), " +
                                "MARGIN = replace(convert(varchar(100),convert(money,MARGIN),1),'.00',''), " +
                                "BALANCE = replace(convert(varchar(100),convert(money,BALANCE),1),'.00',''), " +
                                "OWNRETENTION = replace(convert(varchar(100),convert(money,OWNRETENTION),1),'.00','') " +
                                "from APPLICATION_SUMINS_SERIES a " +
                                "where REGNO='" + LB_REGNO.Text + "' " +
                                "order by " +
                                "a.REGNO, a.SEQ";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select " +
                                "SEQ, " +
                                "VALUEDATE = convert(varchar(20), VALUEDATE, 103), " +
                                "PRINCIPAL, " +
                                "MARGIN " +
                                "from APPLICATION_SUMINS_SERIES " +
                                "where " +
                                "REGNO = '" + LB_REGNO.Text + "' order by SEQ";
            conn.ExecuteQuery();
            GlobalUse.ExportDataSetToExcel(conn.GetDataTable().Copy(), this, LB_REGNO.Text + ".xls", true);
        }

        protected void BT_DEFAULT_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_APPLICATION_SUMINS_SERIES_GENERATE_REGNO '" + LB_REGNO.Text + "'";
            conn.ExecuteNonQuery();
            LoadSeries();
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

            LoadSeries();
        }

        protected void ProcessSpecialTemplate(string filename, string FullPath)
        {
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

                    string SEQ = myRow[0].ToString().Trim();
                    string PRINCIPAL = myRow[2].ToString().Trim();
                    string MARGIN = myRow[3].ToString().Trim();

                    conn.QueryString = "update APPLICATION_SUMINS_SERIES set " +
                                        "PRINCIPAL = " + PRINCIPAL + ", " +
                                        "MARGIN = " + MARGIN + " " +
                                        "where " +
                                        "REGNO = '" + LB_REGNO.Text + "' " +
                                        "and SEQ = " + SEQ;
                    conn.ExecuteNonQuery();
                }


                con.Close();

                conn.QueryString = "exec SP_APPLICATION_SUMINS_SERIES_GENERATE_UPDATE '" + LB_REGNO.Text + "'";
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                con.Close();
                LB_ERR.Text = ex.Message;
            }

        }
    }
}