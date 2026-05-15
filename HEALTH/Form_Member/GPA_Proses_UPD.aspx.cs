using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class GPA_Peserta_UpdateData : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        private string _dbip, _fullpath, _path;
        private string _stringMsgErr;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["BATCH_ID"];
                Setup();
                
                /*
                LoadDGR();
                LoadREGNO();
                DisableButton(LB_ID.Text);
                */
            }
        }

        protected bool isApproved()
        {
            bool bResult = false;

            try
            {
                conn.QueryString = "select ROW_ID from TRACK_DATA where TIPE_CODE='GPA' and OWNER='" + LB_ID.Text + "' and SEQ > 3";
                conn.ExecuteQuery(150000);
                if (conn.GetRowCount() > 0)
                    bResult = true;
            }
            catch { }

            return bResult;
        }

        protected void Setup()
        {
            if (isApproved())
            {
                TR_UPLOAD.Visible = false;
                TR_ENTRY.Visible = false;
                return;
            }

            /*
            conn.QueryString = "select " +
                                "c.REGNO, " +
                                "NAMA = LEFT(LTRIM(c.NAMA), 30) " +
                                "from PESERTA_NBRN a " +
                                "inner join ENDORSEMENT_BATCH b on a.BATCH_ID = b.POLICY_PERIOD_ID " +
                                "inner join PESERTA_MASTER c on a.REGNO = c.REGNO " +
                                "where " +
                                "1=1 " +
                                "and b.BATCH_ID = '" + LB_ID.Text + "' " +
                                "order by 2";
            conn.ExecuteQuery(150000);
            DDL_REGNO.Items.Clear();
            DDL_REGNO.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_REGNO.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "c.COMPANY_CODE " +
                                "from ENDORSEMENT_BATCH a " +
                                "inner join POLICY_PERIOD b on a.POLICY_PERIOD_ID=b.ID " +
                                "inner join POLICY c on b.POLICY_ID=c.ID " +
                                "where a.BATCH_ID='" + LB_ID.Text + "'";
            conn.ExecuteQuery(150000);
            LB_COMPANYCODE.Text = conn.GetFieldValue(0, 0).ToString();

            BTN_CARI_REGNO_EMP.Attributes.Add("onclick", "window.open('../Form_Tools/SearchEmployeCompany.aspx?company_code=" + LB_COMPANYCODE.Text + "&parent=0&target=DDL_REGNO','PESERTA','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
            */
        }

        protected void LoadDGR()
        {
            conn.QueryString = "select " +
                                "CODE, " +
                                "DESCR, " +
                                "FINANCIAL, " +
                                "FIELD_NAME, " +
                                "TABLE_NAME, " +
                                "DATA_LEN, " +
                                "TIPE_FIELD, " +
                                "FINANCIAL_DESCR = (case when FINANCIAL=1 then 'FINANCIAL' else 'NON FINANCIAL' end), " +
                                "SQL_REF = replace(replace(SQL_REF,'@REGNO','''" + DDL_REGNO.SelectedValue + "'''),'@COMPANY_CODE','''" + LB_COMPANYCODE.Text + "''') " +
                                "from PARAM_TBL_TIPE_PERUBAHAN_DATA_PESERTA a " +
                                "order by  " +
                                "a.FINANCIAL, " +
                                "convert(int,a.CODE)";
            conn.ExecuteQuery(150000);
            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtval = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                DropDownList ddlval = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                TextBox txtdate = (TextBox)DGR.Items[i].FindControl("TXT_DATE");

                switch (DGR.Items[i].Cells[6].Text)
                {
                    case "001": txtval.Visible = true;
                        txtval.Width = 200;
                        txtval.MaxLength = int.Parse(DGR.Items[i].Cells[5].Text);
                        break;
                    case "002": txtval.Visible = true;
                        txtval.Width = 40;
                        break;
                    case "003": txtval.Visible = true;
                        txtval.Width = 100;
                        txtval.MaxLength = int.Parse(DGR.Items[i].Cells[5].Text);
                        break;
                    case "004": txtdate.Visible = true;
                        break;
                    case "005": ddlval.Visible = true;
                        ddlval.Items.Add(new ListItem("YA", "1"));
                        ddlval.Items.Add(new ListItem("TIDAK", "0"));
                        break;
                    case "006": txtval.Visible = true;
                        txtval.Width = 200;
                        break;
                }

                if (DGR.Items[i].Cells[7].Text != "&nbsp;")
                {
                    txtval.Visible = false;
                    txtdate.Visible = false;
                    ddlval.Visible = true;

                    try
                    {
                        conn.QueryString = DGR.Items[i].Cells[7].Text;
                        conn.ExecuteQuery(150000);
                        for (int j = 0; j < conn.GetRowCount(); j++)
                            ddlval.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    }
                    catch { }
                }

                if (DGR.Items[i].Cells[9].Text != "FINANCIAL")
                    DGR.Items[i].Cells[9].ForeColor = System.Drawing.Color.Blue;
                else
                    DGR.Items[i].Cells[9].ForeColor = System.Drawing.Color.Red;

                DGR.Items[i].Cells[9].Text = "<B>" + DGR.Items[i].Cells[9].Text + "</B>";
            }
        }

        protected void LoadREGNO()
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtval = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                DropDownList ddlval = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                TextBox txtdate = (TextBox)DGR.Items[i].FindControl("TXT_DATE");

                string val = "";

                try
                {
                    conn.QueryString = "select " + DGR.Items[i].Cells[3].Text + " from " + DGR.Items[i].Cells[4].Text + " where REGNO='" + DDL_REGNO.SelectedValue + "'";
                    conn.ExecuteQuery(150000);
                    val = conn.GetFieldValue(0, 0).ToString();
                }
                catch { }

                if (txtval.Visible)
                {
                    switch (DGR.Items[i].Cells[6].Text)
                    {
                        case "001": txtval.Text = val; break;
                        case "002": txtval.Text = val; break;
                        case "003": conn.QueryString = "select VAL = convert(varchar(100),convert(money," + conn.GetFieldValue(0, 0).ToString() + "),1) ";
                            conn.ExecuteQuery(150000);
                            txtval.Text = conn.GetFieldValue(0, 0).ToString(); break;
                        case "006": txtval.Text = val; break;
                    }
                }

                if (ddlval.Visible)
                {
                    try
                    {
                        ddlval.SelectedValue = val;
                    }
                    catch { }
                }

                if (txtdate.Visible)
                {
                    try
                    {
                        DateTime date = DateTime.Parse(val);
                        txtdate.Text = date.Day.ToString() + "/" + date.Month.ToString() + "/" + date.Year.ToString();
                    }
                    catch { }
                }
            }
        }

        protected void DDL_REGNO_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadREGNO();
        }

        protected void BT_SET_Click(object sender, EventArgs e)
        {
            LoadREGNO();
        }

        protected void DisableButton(string BATCH_ID)
        {
            conn.QueryString = "SELECT * FROM DBO.V_GPA_ENDORSEMENT_BATCH WHERE BATCH_ID='" + BATCH_ID + "' AND PROS_END IS NOT NULL";
            conn.ExecuteQuery(150000);
            int sa = conn.GetRowCount();
            if (conn.GetRowCount() > 0)
            {
                DGR.Enabled = false;
                BT_SUBMIT.Enabled = false;
                BT_SET.Enabled = false;
            }
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtval = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                DropDownList ddlval = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                TextBox txtdate = (TextBox)DGR.Items[i].FindControl("TXT_DATE");

                string val = "";
                if (txtval.Visible)
                {
                    if (txtval.Text.Trim() != "")
                        val = txtval.Text.Trim();
                }
                if (ddlval.Visible)
                {
                    val = ddlval.SelectedValue;
                }
                if (txtdate.Visible)
                {
                    if (txtdate.Text.Trim() != "")
                    {
                        val = GlobalUse.GlobalDateFormat(txtdate.Text.Trim(), "d/M/yyyy");
                    }
                }


                try
                {
                    conn.QueryString = "if (select USER_ENDDATE from TRACK_DATA where TIPE_CODE='GPA' AND OWNER='" + LB_ID.Text.Trim() + "' AND SEQ=2) is not null " +
                                        "select isEnd = 1, RESULT = '' " +
                                        "else " +
                                        "select isEnd = 0, RESULT = '' ";
                    conn.ExecuteQuery();
                    if (conn.GetFieldValue("isEnd").Equals("1") && BT_SUBMIT.Enabled.Equals(true))
                    {
                        DisableButton(LB_ID.Text.Trim());
                        return;
                    }
                    conn.QueryString = "exec SP_GPA_PESERTA_UPDATEDATA_PROSES " +
                                        "'" + LB_ID.Text + "'," +
                                        "'" + DDL_REGNO.SelectedValue + "'," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        "'" + val + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery(150000);

                    if (conn.GetFieldValue("RESULT").ToString().Trim() != "")
                    {
                        LB_ERROR.Text = "<TABLE style='border-spacing:0px;'>" + conn.GetFieldValue("RESULT").ToString() + "</TABLE>";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = LB_ERROR.Text + "- " + ex.Message + "<BR>";
                }


            }

            Response.Write("<script language='javascript'>parent.gpaupdbody.location.href = '../Form_Member/GPA_Proses_UPD_List.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
            Response.Write("<script language='javascript'>parent.gpaupdheader.location.href = '../Form_Member/GPA_Proses_UPD.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
        }

        protected void BT_TEMPLATE_Click(object sender, EventArgs e)
        {
            //-- test GAS
            //LB_ID.Text = "UPD-71900000001028-0822-0001";
            //--

            conn.QueryString = "exec SP_GPA_PESERTA_UPDATEDATA_TEMPLATE '" + LB_ID.Text + "'";
            conn.ExecuteQuery(150000);
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();

            GlobalUse.ExportDataSetToExcel(dt, this, LB_ID.Text, true);
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            if (!FU1.HasFile)
                return;

            try
            {
                if (UploadFile() == 1)
                {
                    LB_ERROR.ForeColor = System.Drawing.Color.Black;
                    LB_ERROR.Text = "Upload success";

                    conn.QueryString = "select BATCH_ID, stuff((SELECT distinct ', ' + F40 FROM ENDORSEMENT_BATCH_DATAUPLOAD t2 " +
                                       "where t2.BATCH_ID = t1.BATCH_ID FOR XML PATH('')),1,1,'') ERROR from ENDORSEMENT_BATCH_DATAUPLOAD t1 " +
                                       "where BATCH_ID = '"+ LB_ID.Text +"' and F40 is not null group by BATCH_ID";
                    conn.ExecuteQuery(150000);
                    if (conn.GetRowCount() > 0)
                    {
                        LB_ERROR.Text = "Upload success with some Errors below <br>" + conn.GetFieldValue("ERROR").ToString();
                    }
                }
                else 
                {
                    LB_ERROR.ForeColor = System.Drawing.Color.Red;
                    LB_ERROR.Text = _stringMsgErr.ToString();
                    return;
                }
                //UploadFile();
            }
            catch (Exception er)
            {
                LB_ERROR.ForeColor = System.Drawing.Color.Red;
                LB_ERROR.Text = er.Message;
                return;
            }

            //LB_ERROR.ForeColor = System.Drawing.Color.Black;
            //LB_ERROR.Text = "Upload success";
        }

        private int UploadFile()
        {
            int retValue = 0;

            try
            {
                
                _path = Request.PhysicalApplicationPath + "Upload/";
                string filename;

                HttpFileCollection uploadedFiles = Request.Files;
                HttpPostedFile userPostedFile = uploadedFiles[0];

                if (userPostedFile.ContentLength > 0)
                {
                    conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
                    conn.ExecuteQuery(150000);

                    string code = conn.GetFieldValue(0, 0).ToString();

                    filename = code + "_" + Path.GetFileName(userPostedFile.FileName);
                    _fullpath = _path + filename;

                    userPostedFile.SaveAs(_fullpath);
                    retValue = ProcessFile(_fullpath);

                    if (File.Exists(_fullpath))
                        File.Delete(_fullpath);
                }
            }
            catch (Exception ex) 
            {
                _stringMsgErr = ex.Message.ToString();
                retValue = -1;
            }

            return retValue;
            
        }

        protected int ProcessFile(string FullPath)
        {
            int retValue = 0;

            conn.QueryString = "delete from ENDORSEMENT_BATCH_DATAUPLOAD where BATCH_ID = '" + LB_ID.Text + "' " +
                                "delete from PESERTA_UPDATEDATA_TEMP where BATCH_ID = '" + LB_ID.Text + "'";
            conn.ExecuteNonQuery();

            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
            
            //-- GAS
            //string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FullPath + ";Extended Properties=Excel 12.0;";

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
                    string values = "";
                    foreach (DataColumn myCol in dt.Columns)
                    {
                        string val = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                        values = values + "'" + val + "',";
                    }

                    //LB_ID.Text = "UPD-71900000001028-0822-0001";

                    values = "'" + LB_ID.Text + "'," +
                                "'" + LB_ID.Text + "'," +
                                "'" + IDX.ToString() + "'," +
                                values +
                                "null";

                    conn.QueryString = "insert into ENDORSEMENT_BATCH_DATAUPLOAD " +
                                        "(BATCH_ID,WORKSHEET,SEQ,F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,F13,F14,F15) " +
                                        "select " +
                                        values;

                    conn.ExecuteNonQuery();
                }

                con.Close();


                conn.QueryString = "exec SP_GPA_PESERTA_UPDATEDATA_PROSES_BATCH " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery(150000);

                retValue = 1; //--success upload

            }
            catch (System.Exception ex)
            {
                con.Close();
                conn.QueryString = "delete from ENDORSEMENT_BATCH_DATAUPLOAD where BATCH_ID = '" + LB_ID.Text + "'";
                conn.ExecuteNonQuery();
                LB_ERROR.Text = ex.Message;
                _stringMsgErr = ex.Message.ToString();
                retValue = -1; //-- failed Upload
            }

            return retValue;

            Response.Write("<script language='javascript'>parent.gpaupdbody.location.href = '../Form_Member/GPA_Proses_UPD_List.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
            Response.Write("<script language='javascript'>parent.gpaupdheader.location.href = '../Form_Member/GPA_Proses_UPD.aspx?BATCH_ID=" + LB_ID.Text + "';</script>");
        }
    }
}