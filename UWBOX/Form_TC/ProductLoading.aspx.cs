using System;
using System.IO;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;


namespace UWBOX.Form_TC
{
    public partial class ProductLoading : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["CODE"].ToString();
                Setup();

                if (DDL_TC.Items.Count > 0)
                {
                    FillDGRLoadingPeriodic();
                    FillDGRCharges();
                    ShowLoading();
                }
                else
                {
                    TBL_DATA.Visible = false;
                }
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select b.CODE, DESCR = b.CODE + ' - ' + b.DESCR from PARAM_PRODUCT_MASTER_TC a inner join TC_MASTER b on a.TC_ID = b.CODE where a.PRODUCT_CODE = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TC.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select CODE, DESCR from PR_TENOR_TYPE where CODE <> 'YNI' order by CODE desc";
            conn.ExecuteQuery();
            DDL_PERIODIC.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PERIODIC.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

        }

        protected void FillDGRCharges()
        {
            IFCHARGE.Attributes.Add("src", "../../ReportViewer/Viewer.aspx?APPID=UW&CODE=2&PRODUCT_CODE=" + LB_ID.Text + "&TC_ID=" + DDL_TC.SelectedValue);
        }

        protected void FillDGRLoadingPeriodic()
        {
            IFLOADING.Attributes.Add("src", "../../ReportViewer/Viewer.aspx?APPID=UW&CODE=1&PRODUCT_CODE=" + LB_ID.Text + "&TC_ID=" + DDL_TC.SelectedValue);
        }


        protected void DDL_TC_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRLoadingPeriodic();
            FillDGRCharges();
        }

        protected void DDL_CHARGE_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRCharges();
        }

        protected void DDL_LOADING_SEQ_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRLoadingPeriodic();
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_LOADINGCHARGE_DOWNLOAD " +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_TC.SelectedValue + "', 1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "LOADING_" + LB_ID.Text, true);
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            if (!FU.HasFile)
                return;

            UploadLoading();
        }

        protected void UploadLoading()
        {
            string filename = Path.GetFileName(FU.FileName);
            string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            //conn.QueryString = "exec SP_BATCH_MASTER_INSERT 'LOD','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'"; ;
            //conn.ExecuteQuery();
            //string ID = conn.GetFieldValue(0, 0).ToString();

            FU.SaveAs(fullpath);
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);

            con.Open();

            DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            //int i = 0;
            conn.QueryString = "delete from PARAM_PRODUCT_MASTER_LOADING_PERIODIC " +
                                "where " +
                                "PRODUCT_CODE	= '" + LB_ID.Text + "' " +
                                "and TC_ID		= '" + DDL_TC.SelectedValue + "'";
            conn.ExecuteNonQuery();

            string userby = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
            string SQL = "";

            foreach (DataRow myRow in dt.Rows)
            {

                for (int i = 1; i <= 80; i++)
                {
                    string seq = i.ToString();
                    string val = myRow[i + 3].ToString().Trim().Replace("'", "`").Replace(",", "");
                    if (val.Trim() == "")
                        val = "0";

                    if (val.IndexOf("#") >= 0)
                    {
                        i = 81;
                        continue;
                    }

                    SQL = "insert into PARAM_PRODUCT_MASTER_LOADING_PERIODIC " +
                                        "select " +
                                        "PRODUCT_CODE	= '" + LB_ID.Text + "'," +
                                        "TC_ID			= '" + DDL_TC.SelectedValue + "'," +
                                        "LOADING_CODE	= '" + myRow[2].ToString().Trim() + "'," +
                                        "SEQ			= " + seq + "," +
                                        "TRANS_TYPE		= '" + myRow[0].ToString().Trim() + "'," +
                                        "SEQ_TYPE		= '" + DDL_PERIODIC.SelectedValue + "'," +
                                        "VAL			= " + val + "," +
                                        "NETT			= 0," +
                                        "CREATEBY		= '" + userby + "'," +
                                        "CREATEDATE		= GETDATE()," +
                                        "LASTCHANGEBY	= '" + userby + "'," +
                                        "LASTCHANGEDATE	= GETDATE() ";
                    conn.QueryString = SQL;

                    try
                    {
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                /*
                //try
                //{
                conn.QueryString = "insert into BATCH_DETAIL_RAW select " +
                                    "'" + ID + "'," +
                                    i.ToString() + "," +
                                    "'" + myRow[0].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[1].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[2].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[3].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[4].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[5].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[6].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[7].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[8].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[9].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[10].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[11].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[12].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[13].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[14].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[15].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[16].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[17].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[18].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[19].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[20].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[21].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[22].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[23].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[24].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[25].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[26].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[27].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[28].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[29].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[30].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[31].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[32].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[33].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[34].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[35].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[36].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[37].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[38].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[39].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[40].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[41].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[42].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[43].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[44].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[45].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[46].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[47].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[48].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[49].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[50].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[51].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[52].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[53].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[54].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[55].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[56].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[57].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[58].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "''";
                conn.ExecuteNonQuery();
                i++;
                //}
                //catch { }
                */
            }
            con.Close();

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            //conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_LOADING_PERIODIC_UPLOAD " +
            //                    "'" + ID + "'," +
            //                    "'" + LB_ID.Text + "'," +
            //                    "'" + DDL_TC.SelectedValue + "'," +
            //                    "'" + DDL_PERIODIC.SelectedValue + "'";
            //conn.ExecuteNonQuery();

            try
            {
                conn.QueryString = SQL;
                conn.ExecuteNonQuery();
            }
            catch { }
            FillDGRLoadingPeriodic();
        }

        protected void BT_CHARGE_DOWNLOAD_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_LOADINGCHARGE_DOWNLOAD " +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_TC.SelectedValue + "', 2";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, "CHARGE_" + LB_ID.Text, true);
        }

        protected void BT_CHARGE_UPLOAD_Click(object sender, EventArgs e)
        {
            if (!FU_CHG.HasFile)
                return;

            UploadCharges();
        }

        protected void UploadCharges()
        {
            string filename = Path.GetFileName(FU_CHG.FileName);
            string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            conn.QueryString = "exec SP_BATCH_MASTER_INSERT 'CHG','" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'"; ;
            conn.ExecuteQuery();
            string ID = conn.GetFieldValue(0, 0).ToString();

            FU_CHG.SaveAs(fullpath);
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            OleDbConnection con = new OleDbConnection(connstr);

            con.Open();

            DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int i = 0;

            foreach (DataRow myRow in dt.Rows)
            {
                //try
                //{
                conn.QueryString = "insert into BATCH_DETAIL_RAW select " +
                                    "'" + ID + "'," +
                                    i.ToString() + "," +
                                    "'" + myRow[0].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[1].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[2].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[3].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[4].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[5].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[6].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[7].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[8].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[9].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[10].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[11].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[12].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[13].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[14].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[15].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[16].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[17].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[18].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[19].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[20].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[21].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[22].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[23].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[24].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[25].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[26].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[27].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[28].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[29].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[30].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[31].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[32].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[33].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[34].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[35].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[36].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[37].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[38].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[39].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[40].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[41].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[42].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[43].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[44].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[45].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[46].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[47].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[48].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[49].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[50].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[51].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[52].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[53].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[54].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[55].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "'" + myRow[56].ToString().Trim().Replace("'", "`").Replace(",", "") + "'," +
                                    "''," +
                                    "''," +
                                    "''";
                conn.ExecuteNonQuery();
                i++;
                //}
                //catch { }
            }
            con.Close();

            if (File.Exists(fullpath))
                File.Delete(fullpath);

            conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_CHARGE_UPLOAD " +
                                "'" + ID + "'," +
                                "'" + LB_ID.Text + "'," +
                                "'" + DDL_TC.SelectedValue + "'";
            conn.ExecuteNonQuery();
            FillDGRCharges();
        }

        protected void BT_LOADING_Click(object sender, EventArgs e)
        {
            ShowLoading();
        }

        protected void BT_CHARGES_Click(object sender, EventArgs e)
        {
            LB_TITLE.Text = ((Button)sender).Text;
            DV_LOADING.Visible = false;
            DV_CHARGES.Visible = true;
        }

        protected void ShowLoading()
        {
            LB_TITLE.Text = BT_LOADING.Text;
            DV_LOADING.Visible = true;
            DV_CHARGES.Visible = false;
        }
    }
}