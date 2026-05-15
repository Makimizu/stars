using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using OfficeOpenXml;
using System.Xml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.IO;

namespace FINANCE.Form_Settlement
{
    public partial class StlFundType : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LB_APP.Text = Request.QueryString["APPID"];
                }
                catch { }
                try
                {
                    LB_TIPE.Text = Request.QueryString["TIPE"];
                }
                catch { }

                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();
                //FillDGRRek();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (LB_APP.Text != "")
            {
                DDL_APP.SelectedValue = LB_APP.Text;
                DDL_APP.Enabled = false;
            }

            FillDDLTipe();

            if (LB_TIPE.Text != "")
            {
                DDL_TIPE.SelectedValue = LB_TIPE.Text;
                DDL_TIPE.Enabled = false;
            }




            conn.QueryString = "SELECT DISTINCT REPLACE(LEFT(convert(varchar(20), STL_DATE, 23),7), '-', '') FROM FINANCE..V_LINK_ACC_INVOICE_MASTER_SETTLE " +
                               "WHERE STL_DATE >= '2021-06-01' " +
                               "ORDER BY REPLACE(LEFT(convert(varchar(20), STL_DATE, 23),7), '-', '') DESC";
            conn.ExecuteQuery();
            //DDL_PERIOD.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PERIOD.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

        }

        protected void FillDDLTipe()
        {
            DDL_TIPE.Items.Clear();
            conn.QueryString = "select 'SHF01' FUND, 'UJROH' CODE UNION select 'TBR02' FUND, 'TABARRU' CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            DDL_TIPE.Items.Add(new ListItem("ALL", ""));
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";
            /*
            if (DDL_PERIOD.SelectedValue != "")
            {
                where = where + " and PERIOD ='" + DDL_APP.SelectedValue + "'";
            }

            if (DDL_TIPE.SelectedValue != "")
            {
                where = where + " and TIPE_SETTLEMENT='" + DDL_TIPE.SelectedValue + "'";
            }*/
            //conn.QueryString = "DELETE FROM TEMP_V_LINK_ACC_INVOICE_MASTER_SETTLE WHERE 1 = 1";
            //conn.ExecuteQuery();

            conn.QueryString = "EXEC [PRODUCTION].[dbo].[SP_FUND_NOREK_DESCR_FIXFIRST]";
            conn.ExecuteQuery();

            conn.QueryString = "EXEC [PRODUCTION].[dbo].[SP_FUND] '" + DDL_PERIOD.SelectedValue + "','" + DDL_TIPE.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";

            //conn.QueryString = "select NOREK,BANK from REKENING_MASTER where STL='1' order by 2";
            //conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                //Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                LinkButton lbDATE = (LinkButton)DGR.Items[i].FindControl("LB_DATE");
                lbDATE.Text = DGR.Items[i].Cells[1].Text;
                //lbDATE.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[0].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=850px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

            }
        }

        protected void FillDGRDetail(string date)
        {
            LB_RESULT.Text = "";
            string where = "";


            conn.QueryString = "SELECT a.STL_DATE, " +
                               "        a.STL_ID, " +
                               "        a.INVOICENO, " +
                               "        a.INVOICE_DATE, " +
                               "        a.AR_DATE, " +
                               "        a.AMOUNT, " +
                               "        a.CUSTOMER_NAME, " +
                               "        a.INVOICE_TYPE_DESCR, " +
                               "        a.NOREK_DESCR, " +
                               "        a.PAYMENT_AMOUNT, " +
                               " UJROH			= ROUND(a.AMOUNT  * (1-isnull(f.VAL,0)) * (e.VAL - isnull(f.VAL,0))/(1 - isnull(f.VAL,0)),0), " +
                               " TABARRU			= a.AMOUNT - ROUND(a.AMOUNT  * (1-isnull(f.VAL,0)) * (e.VAL - isnull(f.VAL,0))/(1 - isnull(f.VAL,0)),0) " +
                               " FROM FINANCE..V_LINK_ACC_INVOICE_MASTER_SETTLE a " +
                               " inner join ASKES_MIGRASI.dbo.V_POLICY_PERIOD_INVOICE b on a.INVOICENO=b.INVOICENO  collate database_default " +
                               " inner join (select POLICY_PERIOD_ID, VAL = SUM(VAL) from ASKES_MIGRASI.dbo.POLICY_PERIOD_LOADING a group by POLICY_PERIOD_ID) e on b.POLICY_PERIOD_ID=e.POLICY_PERIOD_ID " +
                               " left join ASKES_MIGRASI.dbo.POLICY_PERIOD_LOADING f on b.POLICY_PERIOD_ID = f.POLICY_PERIOD_ID and f.LOADING_CODE = '011' " +
                               " WHERE DATEADD(dd, 0, DATEDIFF(dd, 0, STL_DATE)) = '" + date + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGRdetail.DataSource = dt;
            DGRdetail.DataBind();

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";

            //conn.QueryString = "select NOREK,BANK from REKENING_MASTER where STL='1' order by 2";
            //conn.ExecuteQuery();

            for (int i = 0; i < DGRdetail.Items.Count; i++)
            {
                //Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                //LinkButton lbDATE = (LinkButton)DGR.Items[i].FindControl("LB_DATE");
                //lbDATE.Text = DGR.Items[i].Cells[1].Text;
                //lbDATE.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[0].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=850px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

            }
        }

        protected void FillDGRRek()
        {
            LB_RESULT.Text = "";
            string where = "";

            conn.QueryString = "EXEC [PRODUCTION].[dbo].[SP_FUND_NOREK_DESCR] '" + DDL_PERIOD.SelectedValue + "'";
            conn.ExecuteQuery(120);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGRRek.DataSource = dt;
            DGRRek.DataBind();

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";


            for (int i = 0; i < DGRRek.Items.Count; i++)
            {
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "All")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    cb.Checked = true;
                }
            }

            if (e.CommandName == "Detail")
            {
                FillDGRDetail(e.Item.Cells[0].Text);
                DGR.Visible = false;
                DGRdetail.Visible = true;
                BT_APPROCE.Text = "BACK";
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_SETTLEMENT_MASTER_PENDING_ROLLBACK '" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGRdetail_ItemCommand(object source, DataGridCommandEventArgs e)
        { }

        protected void DGRdetail_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGRdetail.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_APPROCE_Click(object sender, EventArgs e)
        {
            BT_APPROCE.Text = "PROCESS";
            DGRdetail.Visible = false;
            DGR.Visible = true;

            int j = 0; string isi = "";
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                if (cb.Checked)
                {
                    j++;
                    //try
                    //{
                    conn.QueryString = "exec [PRODUCTION].[dbo].SP_FUND_PROCESS " +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        "'" + DGR.Items[i].Cells[5].Text + "'," +
                                        "'" + DGR.Items[i].Cells[4].Text + "'," +
                                        "'" + DGR.Items[i].Cells[6].Text + "'," +
                                        "'" + DGR.Items[i].Cells[7].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                        "'" + DGR.Items[i].Cells[8].Text + "'";
                    conn.ExecuteNonQuery();
                    //}
                    //catch { }
                }
            }
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + j + '|' + isi + "')", true);

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_TIPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }


        public void RunSample1()
        {
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventory");
                //Add the headers
                worksheet.Cells[1, 1].Value = "Tanggal Proses";
                worksheet.Cells[1, 2].Value = "Rekening Sumber";
                worksheet.Cells[1, 3].Value = "Ujroh";
                worksheet.Cells[1, 4].Value = "Tabarru";
                worksheet.Cells[1, 5].Value = "Periode";
                worksheet.Cells[1, 6].Value = "Total (Ujroh + Tabarru)";

                for (int i = 0; i < DGRRek.Items.Count; i++)
                {
                    worksheet.Cells["A" + (i + 2)].Value = DGRRek.Items[i].Cells[0].Text;
                    worksheet.Cells["B" + (i + 2)].Value = DGRRek.Items[i].Cells[1].Text;
                    worksheet.Cells["C" + (i + 2)].Value = DGRRek.Items[i].Cells[2].Text;
                    worksheet.Cells["D" + (i + 2)].Value = DGRRek.Items[i].Cells[3].Text;
                    worksheet.Cells["E" + (i + 2)].Value = DGRRek.Items[i].Cells[4].Text;
                    worksheet.Cells["F" + (i + 2)].Value = DGRRek.Items[i].Cells[5].Text;
                }

                //Add a formula for the value-column
                //worksheet.Cells["E2:E4"].Formula = "C2*D2";

                //Ok now format the values;
                using (var range = worksheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.DarkBlue);
                    range.Style.Font.Color.SetColor(Color.White);
                }

                /*
                worksheet.Cells["A5:E5"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A5:E5"].Style.Font.Bold = true;

                worksheet.Cells[5, 3, 5, 5].Formula = string.Format("SUBTOTAL(9,{0})", new ExcelAddress(2, 3, 4, 3).Address);
                worksheet.Cells["C2:C5"].Style.Numberformat.Format = "#,##0";
                worksheet.Cells["D2:E5"].Style.Numberformat.Format = "#,##0.00";
                */
                //Create an autofilter for the range
                worksheet.Cells["A1:E4"].AutoFilter = true;

                worksheet.Cells["A2:A4"].Style.Numberformat.Format = "@";   //Format as text

                //There is actually no need to calculate, Excel will do it for you, but in some cases it might be useful. 
                //For example if you link to this workbook from another workbook or you will open the workbook in a program that hasn't a calculation engine or 
                //you want to use the result of a formula in your program.
                worksheet.Calculate();

                worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                // lets set the header text 
                //worksheet.HeaderFooter.OddHeader.CenteredText = "&24&U&\"Arial,Regular Bold\" Inventory";
                // add the page number to the footer plus the total number of pages
                /*
                worksheet.HeaderFooter.OddFooter.RightAlignedText =
                    string.Format("Page {0} of {1}", ExcelHeaderFooter.PageNumber, ExcelHeaderFooter.NumberOfPages);
                 */
                // add the sheet name to the footer
                //worksheet.HeaderFooter.OddFooter.CenteredText = ExcelHeaderFooter.SheetName;
                // add the file path to the footer
                //worksheet.HeaderFooter.OddFooter.LeftAlignedText = ExcelHeaderFooter.FilePath + ExcelHeaderFooter.FileName;

                //worksheet.PrinterSettings.RepeatRows = worksheet.Cells["1:2"];
                //worksheet.PrinterSettings.RepeatColumns = worksheet.Cells["A:G"];

                // Change the sheet view to show it in page layout mode
                //.worksheet.View.PageLayoutView = true;

                // set some document properties
                package.Workbook.Properties.Title = "DATA";
                package.Workbook.Properties.Author = "TAKAFUL";
                package.Workbook.Properties.Comments = "DATA CLAIM";

                // set some extended property values
                package.Workbook.Properties.Company = "TAKAFUL KELUARGA";

                // set some custom property values
                //package.Workbook.Properties.SetCustomPropertyValue("Checked by", "Jan Källman");
                //package.Workbook.Properties.SetCustomPropertyValue("AssemblyName", "EPPlus");

                //var xlFile = Utils.GetFileInfo("sample1.xlsx",true);
                // save our new workbook in the output directory and we are done!
                //package.SaveAs(xlFile);
                //return xlFile.FullName;

                package.Workbook.Properties.Title = "Attempts";
                this.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                this.Response.AddHeader(
                          "content-disposition",
                          string.Format("attachment;  filename={0}", "ExcellData.xlsx"));
                this.Response.BinaryWrite(package.GetAsByteArray());
            }
        }

        protected void LB_EXCEL_Click(object sender, EventArgs e)
        {
            FillDGRRek();
            RunSample1();
        }
    }
}