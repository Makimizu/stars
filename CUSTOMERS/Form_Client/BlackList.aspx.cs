using System;
using System.IO;
using DMS.DBConnection;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace CUSTOMERS.Form_Client
{
    public partial class BlackList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "startLoading", "startDotLoading();", true);
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                Setup();
            }

            
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from [CLIENT_BASE].[dbo].[PR_JOB]";
            conn.ExecuteQuery();
            DDL_JOB.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_JOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from [CLIENT_BASE].[dbo].[PR_PROPINSI] order by 2";
            conn.ExecuteQuery();
            DDL_PROVINCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            DGRUPLOAD.DataSource = null;
            DGRUPLOAD.DataBind();
            LB_RECORD.Text = "";
            LB_INFO.Text = "";

            conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_MEMBER_MASTER_BLACKLIST] " +
                                "'" + TXT_CITY.Text.Trim() + "'," +
                                "'" + TXT_IDNO.Text.Trim() + "'," +
                                "'" + TXT_NAME.Text.Trim() + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DOBSTART.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + GlobalUse.GlobalDateFormat(TXT_DOBTO.Text.Trim(), "d/M/yyyy") + "'," +
                                "'" + DDL_GENDER.SelectedValue + "'," +
                                "'" + DDL_JOB.SelectedValue + "'," +
                                "'" + DDL_PROVINCE.SelectedValue + "'";
            conn.ExecuteQuery();

            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbtCode = (LinkButton)DGR.Items[i].FindControl("LB_ID");
                lbtCode.Text = DGR.Items[i].Cells[1].Text;

                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");

                btDEL.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO DELETE ?')){return false;};");
            }

            LB_RECORD.Text = conn.GetRowCount().ToString() + " records";

            ClientScript.RegisterStartupScript(this.GetType(), "setGridWidth", "setGridWidth();", true);
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGRUPLOAD_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            //FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("BlackListDetail.aspx?ID=" + e.Item.Cells[0].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from [CLIENT_BASE].[dbo].[MEMBER_MASTER_BLACKLIST] where ID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            GlobalUse.SQLToFile("Template_Upload_BlackList.xls",
                                    "select top 1 THEFILE from [ARCHIEVE].[dbo].[LF_ARSIP] where NAMAFILE='Template_Upload_Blacklist.xls' and OWNER1 = 'SYSTEM'",
                                    Page);
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            DGR.DataSource = null;
            DGR.DataBind();
            DGRUPLOAD.DataSource = null;
            DGRUPLOAD.DataBind();

            LB_ERR.Text = "";
            LB_INFO.Text = "";

            if (!FU.HasFile)
                return;

            conn.QueryString = "select VALUE from [SECURITY].[dbo].[SC_GENERAL_SET] where APP_CODE = '0' and PARAMETER = 'UPLOAD_FOLDER'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            DateTime dt = DateTime.UtcNow;
            DateTime utc = dt.ToUniversalTime();
            long filename = (long)(utc - new DateTime(1970, 1, 1)).TotalSeconds;
            //long filename = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string extension = Path.GetExtension(FU.FileName);

            string fullpath = conn.GetFieldValue(0, 0).ToString() + "\\" + filename + extension;

            if (File.Exists(fullpath))
            {
                File.Delete(fullpath);
            }

            FU.SaveAs(fullpath);
            Process(fullpath);

            //FU.FileContent.Dispose();

            //FillDGR();

            ClientScript.RegisterStartupScript(this.GetType(), "hideLoading", "hideLoading();", true);
        }

        protected void Process(string FullPath)
        {
            //string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            string fileExtension = System.IO.Path.GetExtension(FullPath);

            string connstr = "";

            string folderPath = Path.GetDirectoryName(FullPath);
            string fileName = Path.GetFileName(FullPath);

            int records = 0;
            
            string info = @"Pastikan file Excel yang diunggah memiliki urutan kolom sebagai berikut:<br>
                fullname - alias - gender - mother name - date of birth - place of birth - job - religion - marital status - 
                id type - id no - tax no - citizenship - phone 1 - phone 2 - email - address 1 - address 2 -
                city - province - country - zipcode - education - flag - source - remark<br><br>";

            DataTable dta = new DataTable();
            dta.Columns.Add("ID");
            dta.Columns.Add("FULLNAME");
            dta.Columns.Add("GENDER");
            dta.Columns.Add("DOB", typeof(DateTime));
            dta.Columns.Add("POB");
            dta.Columns.Add("MOTHER_NAME");
            dta.Columns.Add("JOB");
            dta.Columns.Add("RELIGION");
            dta.Columns.Add("MARITAL_STATUS");
            dta.Columns.Add("ID_TYPE");
            dta.Columns.Add("ID_NO");
            dta.Columns.Add("TAX_NO");
            dta.Columns.Add("CITIZENSHIP");
            dta.Columns.Add("EDUCATION");
            dta.Columns.Add("PHONE_1");
            dta.Columns.Add("PHONE_2");
            dta.Columns.Add("EMAIL");
            dta.Columns.Add("ADDRESS_1");
            dta.Columns.Add("ADDRESS_2");
            dta.Columns.Add("CITY");
            dta.Columns.Add("PROVINCE");
            dta.Columns.Add("COUNTRY");
            dta.Columns.Add("ZIP_CODE");
            dta.Columns.Add("FLAG");
            dta.Columns.Add("SOURCE");
            dta.Columns.Add("REMARK");
            dta.Columns.Add("ALIAS");
            dta.Columns.Add("UPLOAD_STATUS");

            if (fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                //connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;
                //                    Data Source=" + folderPath + @";
                //                    Extended Properties='text;HDR=YES;FMT=Delimited(;);'";

                try
                {
                    using (StreamReader reader = new StreamReader(FullPath))
                    {
                        string line = reader.ReadLine();

                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();

                            if (string.IsNullOrEmpty(line) || String.IsNullOrWhiteSpace(line))
                            {
                                continue;
                            }

                            using (TextFieldParser parser = new TextFieldParser(new StringReader(line)))
                            {
                                parser.HasFieldsEnclosedInQuotes = true;
                                parser.SetDelimiters(",");

                                string[] fields = parser.ReadFields();

                                string fullname = "", sex = "", mmm = "", dob = "", pob = "", job = "",
                                    religion = "", maritalstatus = "", idtype = "", idno = "", taxno = "",
                                    citizenship = "", phone1 = "", phone2 = "", email = "", address1 = "",
                                    address2 = "", city = "", province = "", country = "", zipcode = "",
                                    education = "", flag = "", source = "", remark = "", alias = "";

                                for (int i = 0; i < fields.Length; i++)
                                {
                                    if (i == 0)
                                    {
                                        fullname = fields[i].ToString();
                                    }
                                    if (i == 1)
                                    {
                                        alias = fields[i].ToString();
                                    }
                                    if (i == 2)
                                    {
                                        sex = fields[i].ToString();
                                    }
                                    if (i == 3)
                                    {
                                        mmm = fields[i].ToString();
                                    }
                                    if (i == 4)
                                    {
                                        DateTime resultUS;
                                        DateTime resultGB;

                                        bool isDateUS = DateTime.TryParse(fields[i].ToString(), new CultureInfo("en-US"), DateTimeStyles.None, out resultUS);
                                        bool isDateGB = DateTime.TryParse(fields[i].ToString(), new CultureInfo("en-GB"), DateTimeStyles.None, out resultGB);

                                        if (isDateGB)
                                        {
                                            dob = resultGB.ToString("yyyy-MM-dd");
                                        }
                                        else if (isDateUS)
                                        {
                                            dob = resultUS.ToString("yyyy-MM-dd");
                                        }
                                    }
                                    if (i == 5)
                                    {
                                        pob = fields[i].ToString();
                                    }
                                    if (i == 6)
                                    {
                                        job = fields[i].ToString();
                                    }
                                    if (i == 7)
                                    {
                                        religion = fields[i].ToString();
                                    }
                                    if (i == 8)
                                    {
                                        maritalstatus = fields[i].ToString();
                                    }
                                    if (i == 9)
                                    {
                                        idtype = fields[i].ToString();
                                    }
                                    if (i == 10)
                                    {
                                        idno = fields[i].ToString();
                                    }
                                    if (i == 11)
                                    {
                                        taxno = fields[i].ToString();
                                    }
                                    if (i == 12)
                                    {
                                        citizenship = fields[i].ToString();
                                    }
                                    if (i == 13)
                                    {
                                        phone1 = fields[i].ToString();
                                    }
                                    if (i == 14)
                                    {
                                        phone2 = fields[i].ToString();
                                    }
                                    if (i == 15)
                                    {
                                        email = fields[i].ToString();
                                    }
                                    if (i == 16)
                                    {
                                        address1 = fields[i].ToString();
                                    }
                                    if (i == 17)
                                    {
                                        address2 = fields[i].ToString();
                                    }
                                    if (i == 18)
                                    {
                                        city = fields[i].ToString();
                                    }
                                    if (i == 19)
                                    {
                                        province = fields[i].ToString();
                                    }
                                    if (i == 20)
                                    {
                                        country = fields[i].ToString();
                                    }
                                    if (i == 21)
                                    {
                                        zipcode = fields[i].ToString();
                                    }
                                    if (i == 22)
                                    {
                                        education = fields[i].ToString();
                                    }
                                    if (i == 23)
                                    {
                                        flag = fields[i].ToString();
                                    }
                                    if (i == 24)
                                    {
                                        source = fields[i].ToString();
                                    }
                                    if (i == 25)
                                    {
                                        remark = fields[i].ToString();
                                    }        
                                }

                                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_MEMBER_MASTER_BLACKLIST_UPSERT] " +
                                "'" + fullname + "'," +
                                "'" + sex + "'," +
                                "'" + mmm + "'," +
                                "'" + dob + "'," +
                                "'" + pob + "'," +
                                "'" + job + "'," +
                                "'" + religion + "'," +
                                "'" + maritalstatus + "'," +
                                "'" + idtype + "'," +
                                "'" + idno + "'," +
                                "'" + taxno + "'," +
                                "'" + citizenship + "'," +
                                "'" + phone1 + "'," +
                                "'" + phone2 + "'," +
                                "'" + email + "'," +
                                "'" + address1 + "'," +
                                "'" + address2 + "'," +
                                "'" + city + "'," +
                                "'" + province + "'," +
                                "'" + country + "'," +
                                "'" + zipcode + "'," +
                                "'" + education + "'," +
                                "'" + flag + "'," +
                                "'" + source + "'," +
                                "'" + remark + "'," +
                                "'" + alias + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                                conn.ExecuteQuery();

                                DataRow newRow = dta.NewRow();

                                newRow["ID"] = conn.GetFieldValue(0, 0).ToString();
                                newRow["FULLNAME"] = conn.GetFieldValue(0, 1).ToString();
                                newRow["GENDER"] = conn.GetFieldValue(0, 2).ToString();
                                newRow["MOTHER_NAME"] = conn.GetFieldValue(0, 3).ToString();
                                if(conn.GetFieldValue(0, 4).ToString() == "")
                                {
                                    newRow["DOB"] = DBNull.Value;
                                }
                                else
                                {
                                    newRow["DOB"] = conn.GetFieldValue(0, 4).ToString();
                                }
                                
                                newRow["POB"] = conn.GetFieldValue(0, 5).ToString();
                                newRow["JOB"] = conn.GetFieldValue(0, 6).ToString();
                                newRow["RELIGION"] = conn.GetFieldValue(0, 7).ToString();
                                newRow["MARITAL_STATUS"] = conn.GetFieldValue(0, 8).ToString();
                                newRow["ID_TYPE"] = conn.GetFieldValue(0, 9).ToString();
                                newRow["ID_NO"] = conn.GetFieldValue(0, 10).ToString();
                                newRow["TAX_NO"] = conn.GetFieldValue(0, 11).ToString();
                                newRow["CITIZENSHIP"] = conn.GetFieldValue(0, 12).ToString();
                                newRow["PHONE_1"] = conn.GetFieldValue(0, 13).ToString();
                                newRow["PHONE_2"] = conn.GetFieldValue(0, 14).ToString();
                                newRow["EMAIL"] = conn.GetFieldValue(0, 15).ToString();
                                newRow["ADDRESS_1"] = conn.GetFieldValue(0, 16).ToString();

                                newRow["ADDRESS_2"] = conn.GetFieldValue(0, 17).ToString();
                                newRow["CITY"] = conn.GetFieldValue(0, 18).ToString();
                                newRow["PROVINCE"] = conn.GetFieldValue(0, 19).ToString();
                                newRow["COUNTRY"] = conn.GetFieldValue(0, 20).ToString();
                                newRow["ZIP_CODE"] = conn.GetFieldValue(0, 21).ToString();

                                newRow["FLAG"] = conn.GetFieldValue(0, 22).ToString();
                                newRow["SOURCE"] = conn.GetFieldValue(0, 23).ToString();
                                newRow["EDUCATION"] = conn.GetFieldValue(0, 24).ToString();
                                newRow["REMARK"] = conn.GetFieldValue(0, 25).ToString();
                                newRow["ALIAS"] = conn.GetFieldValue(0, 26).ToString();
                                newRow["UPLOAD_STATUS"] = conn.GetFieldValue(0, 27).ToString();
                                dta.Rows.Add(newRow);

                                records++;
                            }
                        }
                    }

                    LB_RECORD.Text = records + " records";
                    LB_INFO.Text = info;
                    DGRUPLOAD.DataSource = dta;
                    DGRUPLOAD.DataBind();

                    if (File.Exists(FullPath))
                        File.Delete(FullPath);

                    ClientScript.RegisterStartupScript(this.GetType(), "setGridUploadWidth", "setGridUploadWidth();", true);
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = ex.Message;

                    if (File.Exists(FullPath))
                        File.Delete(FullPath);
                }
            }
            else
            {
                if (fileExtension.Equals(".xls", StringComparison.OrdinalIgnoreCase))
                {
                    connstr = @"Provider=Microsoft.ACE.OLEDB.12.0; 
                    Data Source=" + FullPath + @"; 
                    Extended Properties=""Excel 8.0;IMEX=1;HDR=Yes;TypeGuessRows=0;ImportMixedTypes=Text"";";
                }
                else if (fileExtension.Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    connstr = @"Provider=Microsoft.ACE.OLEDB.12.0;
                    Data Source=" + FullPath + @";
                    Extended Properties=""Excel 12.0 Xml;IMEX=1;HDR=Yes;"";";
                }
                else 
                {
                    LB_ERR.Text = "Ekstensi " + fileExtension + " tidak didukung";
                    return;
                }

                OleDbConnection con = new OleDbConnection(connstr);

                try
                {
                    con.Open();

                    DataTable schemaTable = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (schemaTable != null && schemaTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in schemaTable.Rows)
                        {
                            string sheetName = row["TABLE_NAME"].ToString();
                            // Remove single quotes if present
                            if (sheetName.StartsWith("'") && sheetName.EndsWith("'"))
                            {
                                sheetName = sheetName.Substring(1, sheetName.Length - 2);
                            }

                            // Optional: Skip hidden/system sheets
                            if (!sheetName.EndsWith("$") && !sheetName.EndsWith("$'"))
                                continue;

                            OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheetName + "]", con);
                            DataTable dt = new DataTable();
                            da.Fill(dt);

                            foreach (DataRow myRow in dt.Rows)
                            {
                                string fullname = "", sex = "", mmm = "", dob = "", pob = "", job = "",
                                religion = "", maritalstatus = "", idtype = "", idno = "", taxno = "",
                                citizenship = "", phone1 = "", phone2 = "", email = "", address1 = "",
                                address2 = "", city = "", province = "", country = "", zipcode = "",
                                education = "", flag = "", source = "", remark = "", alias = "";

                                for (int i = 0; i < myRow.ItemArray.Length; i++)
                                {
                                    if (i == 0)
                                    {
                                        fullname = myRow[i].ToString();
                                    }
                                    if (i == 1)
                                    {
                                        alias = myRow[i].ToString();
                                    }
                                    if (i == 2)
                                    {
                                        sex = myRow[i].ToString();
                                    }
                                    if (i == 3)
                                    {
                                        mmm = myRow[i].ToString();
                                    }
                                    if (i == 4)
                                    {
                                        DateTime resultUS;
                                        DateTime resultGB;

                                        bool isDateUS = DateTime.TryParse(myRow[i].ToString(), new CultureInfo("en-US"), DateTimeStyles.None, out resultUS);
                                        bool isDateGB = DateTime.TryParse(myRow[i].ToString(), new CultureInfo("en-GB"), DateTimeStyles.None, out resultGB);

                                        if (isDateGB)
                                        {
                                            dob = resultGB.ToString("yyyy-MM-dd");
                                        }
                                        else if (isDateUS)
                                        {
                                            dob = resultUS.ToString("yyyy-MM-dd");
                                        }
                                    }
                                    if (i == 5)
                                    {
                                        pob = myRow[i].ToString();
                                    }
                                    if (i == 6)
                                    {
                                        job = myRow[i].ToString();
                                    }
                                    if (i == 7)
                                    {
                                        religion = myRow[i].ToString();
                                    }
                                    if (i == 8)
                                    {
                                        maritalstatus = myRow[i].ToString();
                                    }
                                    if (i == 9)
                                    {
                                        idtype = myRow[i].ToString();
                                    }
                                    if (i == 10)
                                    {
                                        idno = myRow[i].ToString();
                                    }
                                    if (i == 11)
                                    {
                                        taxno = myRow[i].ToString();
                                    }
                                    if (i == 12)
                                    {
                                        citizenship = myRow[i].ToString();
                                    }
                                    if (i == 13)
                                    {
                                        phone1 = myRow[i].ToString();
                                    }
                                    if (i == 14)
                                    {
                                        phone2 = myRow[i].ToString();
                                    }
                                    if (i == 15)
                                    {
                                        email = myRow[i].ToString();
                                    }
                                    if (i == 16)
                                    {
                                        address1 = myRow[i].ToString();
                                    }
                                    if (i == 17)
                                    {
                                        address2 = myRow[i].ToString();
                                    }
                                    if (i == 18)
                                    {
                                        city = myRow[i].ToString();
                                    }
                                    if (i == 19)
                                    {
                                        province = myRow[i].ToString();
                                    }
                                    if (i == 20)
                                    {
                                        country = myRow[i].ToString();
                                    }
                                    if (i == 21)
                                    {
                                        zipcode = myRow[i].ToString();
                                    }
                                    if (i == 22)
                                    {
                                        education = myRow[i].ToString();
                                    }
                                    if (i == 23)
                                    {
                                        flag = myRow[i].ToString();
                                    }
                                    if (i == 24)
                                    {
                                        source = myRow[i].ToString();
                                    }
                                    if (i == 25)
                                    {
                                        remark = myRow[i].ToString();
                                    }
                                }
                                
                                if (string.IsNullOrEmpty(fullname) || String.IsNullOrWhiteSpace(fullname))
                                {
                                    break;
                                }

                                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_MEMBER_MASTER_BLACKLIST_UPSERT] " +
                                        "'" + fullname + "'," +
                                        "'" + sex + "'," +
                                        "'" + mmm + "'," +
                                        "'" + dob + "'," +
                                        "'" + pob + "'," +
                                        "'" + job + "'," +
                                        "'" + religion + "'," +
                                        "'" + maritalstatus + "'," +
                                        "'" + idtype + "'," +
                                        "'" + idno + "'," +
                                        "'" + taxno + "'," +
                                        "'" + citizenship + "'," +
                                        "'" + phone1 + "'," +
                                        "'" + phone2 + "'," +
                                        "'" + email + "'," +
                                        "'" + address1 + "'," +
                                        "'" + address2 + "'," +
                                        "'" + city + "'," +
                                        "'" + province + "'," +
                                        "'" + country + "'," +
                                        "'" + zipcode + "'," +
                                        "'" + education + "'," +
                                        "'" + flag + "'," +
                                        "'" + source + "'," +
                                        "'" + remark + "'," +
                                        "'" + alias + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                                conn.ExecuteQuery();

                                DataRow newRow = dta.NewRow();

                                newRow["ID"] = conn.GetFieldValue(0, 0).ToString();
                                newRow["FULLNAME"] = conn.GetFieldValue(0, 1).ToString();
                                newRow["GENDER"] = conn.GetFieldValue(0, 2).ToString();
                                newRow["MOTHER_NAME"] = conn.GetFieldValue(0, 3).ToString();
                                if (conn.GetFieldValue(0, 4).ToString() == "")
                                {
                                    newRow["DOB"] = DBNull.Value;
                                }
                                else
                                {
                                    newRow["DOB"] = conn.GetFieldValue(0, 4).ToString();
                                }
                                newRow["POB"] = conn.GetFieldValue(0, 5).ToString();
                                newRow["JOB"] = conn.GetFieldValue(0, 6).ToString();
                                newRow["RELIGION"] = conn.GetFieldValue(0, 7).ToString();
                                newRow["MARITAL_STATUS"] = conn.GetFieldValue(0, 8).ToString();
                                newRow["ID_TYPE"] = conn.GetFieldValue(0, 9).ToString();
                                newRow["ID_NO"] = conn.GetFieldValue(0, 10).ToString();
                                newRow["TAX_NO"] = conn.GetFieldValue(0, 11).ToString();
                                newRow["CITIZENSHIP"] = conn.GetFieldValue(0, 12).ToString();
                                newRow["PHONE_1"] = conn.GetFieldValue(0, 13).ToString();
                                newRow["PHONE_2"] = conn.GetFieldValue(0, 14).ToString();
                                newRow["EMAIL"] = conn.GetFieldValue(0, 15).ToString();
                                newRow["ADDRESS_1"] = conn.GetFieldValue(0, 16).ToString();

                                newRow["ADDRESS_2"] = conn.GetFieldValue(0, 17).ToString();
                                newRow["CITY"] = conn.GetFieldValue(0, 18).ToString();
                                newRow["PROVINCE"] = conn.GetFieldValue(0, 19).ToString();
                                newRow["COUNTRY"] = conn.GetFieldValue(0, 20).ToString();
                                newRow["ZIP_CODE"] = conn.GetFieldValue(0, 21).ToString();

                                newRow["FLAG"] = conn.GetFieldValue(0, 22).ToString();
                                newRow["SOURCE"] = conn.GetFieldValue(0, 23).ToString();
                                newRow["EDUCATION"] = conn.GetFieldValue(0, 24).ToString();
                                newRow["REMARK"] = conn.GetFieldValue(0, 25).ToString();
                                newRow["ALIAS"] = conn.GetFieldValue(0, 26).ToString();
                                newRow["UPLOAD_STATUS"] = conn.GetFieldValue(0, 27).ToString();
                                dta.Rows.Add(newRow);

                                records++;
                            }
                        }
                    }

                    con.Close();

                    LB_RECORD.Text = records + " records";
                    LB_INFO.Text = info;
                    DGRUPLOAD.DataSource = dta;
                    DGRUPLOAD.DataBind();

                    if (File.Exists(FullPath))
                        File.Delete(FullPath);

                    ClientScript.RegisterStartupScript(this.GetType(), "setGridUploadWidth", "setGridUploadWidth();", true);
                }
                catch (System.Exception ex)
                {
                    con.Close();

                    LB_ERR.Text = ex.Message;

                    if (File.Exists(FullPath))
                        File.Delete(FullPath);
                }
            }
            
        }
    }
}
