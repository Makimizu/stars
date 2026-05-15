using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Threading.Tasks;

namespace GLIFE_PROPOSAL
{
    public partial class QuotationSavingMember : System.Web.UI.Page
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

                LB_QUOTNO.Text = Request.QueryString["quotno"];
                LB_VERNO.Text = Request.QueryString["verno"];

                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_SAVING_MEMBER '" + LB_QUOTNO.Text + "'," + LB_VERNO.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MEMBER.DataSource = dt;
            DGR_MEMBER.DataBind();
        }

        protected void DGR_MEMBER_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_MEMBER.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void LBT_XLSTEMPLATE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "exec SP_SAVING_MEMBER_RANDOM_TEMPLATE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            GlobalUse.ExportDataSetToExcel(dt, this, LB_QUOTNO.Text + '_' + LB_VERNO.Text, true);
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
                Response.Redirect("QuotationSavingMember.aspx?QUOTNO=" + LB_QUOTNO.Text + "&VERNO=" + LB_VERNO.Text);
        }

        protected void ProcessSpecialTemplate(string filename, string FullPath)
        {
            conn.QueryString = "delete from QUOTATION_VERSION_SAVING_MEMBER where QUOTNO = '" + LB_QUOTNO.Text + "' and VERNO = " + LB_VERNO.Text;
            conn.ExecuteNonQuery();

            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + ";Extended Properties=Excel 8.0");

            try
            {
                con.Open();

                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter("select FULLNAME,DOB,SEX,ID_NO,SUMINS,SV_CLUN,SV_CREG,SV_TOP from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "] where FULLNAME<>''", con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                int IDX = 0;
                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    string sql = "insert into QUOTATION_VERSION_SAVING_MEMBER select " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    "'" + LB_VERNO.Text + "'," +
                                    IDX.ToString() + ",";

                    foreach (DataColumn myCol in dt.Columns)
                    {
                        string val = "'" + myRow[myCol].ToString() + "'";
                        switch (myCol.Caption)
                        {
                            case "FULLNAME": val = "'" + myRow[myCol].ToString().Replace(",", ".").Replace("'", "`") + "'"; break;
                            case "DOB": val = "convert(date,'" + myRow[myCol].ToString() + "',105)"; break;
                        }

                        sql = sql + val + ",";
                    }

                    conn.QueryString = sql + "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', GETDATE(), " +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', GETDATE()";
                    conn.ExecuteNonQuery();
                }


                con.Close();
                Task.Run(() => ProcessBatch());
            }
            catch (System.Exception ex)
            {
                con.Close();
                conn.QueryString = "delete from QUOTATION_VERSION_SAVING_MEMBER where QUOTNO = '" + LB_QUOTNO.Text + "' and VERNO = " + LB_VERNO.Text;
                conn.ExecuteNonQuery();
                LB_ERR.Text = ex.Message;
            }

        }

        protected void ProcessBatch()
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_SAVING_MEMBER_TRX '" + LB_QUOTNO.Text + "'," + LB_VERNO.Text;
            conn.ExecuteNonQuery();
        }
    }
}