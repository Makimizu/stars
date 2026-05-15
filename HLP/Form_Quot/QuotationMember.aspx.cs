using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Data.OleDb;

namespace HLP.Form_Quot
{
    public partial class QuotationMember : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        private string _fullpath, _path;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_QUOTNO.Text = Request.QueryString["code"];
                LB_VER.Text = Request.QueryString["ver"];
                Setup();
            }
        }

        protected void Setup()
        {
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                BT_SAVE.Visible = false;
                TD_RATE_UPLOAD.Visible = false;
                TD_UPLOAD_MEMBER.Visible = false;
                TD_UPLOAD_MEMBER_1.Visible = false;
            }

            conn.QueryString = "select distinct " +
                                "c.CODE, " +
                                "c.DESCR, " +
                                "d.SEQ " +
                                "from QUOTATION_VERSION_PACKAGE_PLAN a " +
                                "inner join PARAM_PRODUCT_PLAN b on a.PP_CODE=b.CODE " +
                                "inner join PR_BENEFIT c on b.BENEFIT_ID=c.CODE " +
                                "inner join PARAM_BENEFIT_SEQ d on b.BENEFIT_ID=d.CODE " +
                                "where " +
                                "a.QUOTNO='" + LB_QUOTNO.Text + "' " +
                                "and a.VERNO=" + LB_VER.Text + " " +
                                "order by " +
                                "d.SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            ChangeMode();
        }

        protected bool VerifySalesAccess()
        {
            string role = GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles");
            conn.QueryString = "select ROLE_CODE from PARAM_SALES_ROLE where ROLE_CODE = '" + role + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return false;

            return true;
        }

        protected void FillDGR()
        {
            bool bSales = VerifySalesAccess();

            conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER " +
                                "'" + LB_QUOTNO.Text + "'," +
                                LB_VER.Text + "," +
                                "'" + DDL_BENEFIT.SelectedValue + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DataGrid dgrMALE = (DataGrid)DGR.Items[i].FindControl("DGR_MALE");
                DataGrid dgrFEMALE = (DataGrid)DGR.Items[i].FindControl("DGR_FEMALE");
                DataGrid dgrCHILD = (DataGrid)DGR.Items[i].FindControl("DGR_CHILD");
                DataGrid dgrCHILDF = (DataGrid)DGR.Items[i].FindControl("DGR_CHILD_F");

                conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_DETAIL " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text + "," +
                                    "'" + DGR.Items[i].Cells[0].Text + "'," +
                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                    "'M'";
                conn.ExecuteQuery();
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                dgrMALE.DataSource = dt;
                dgrMALE.DataBind();

                for (int j = 0; j < dgrMALE.Items.Count; j++)
                {
                    TextBox txt = (TextBox)dgrMALE.Items[j].FindControl("TXT_MEMBER");
                    TextBox txtPREMIUM = (TextBox)dgrMALE.Items[j].FindControl("TXT_PREMIUM");
                    txt.Text = dgrMALE.Items[j].Cells[2].Text;
                    txtPREMIUM.Text = dgrMALE.Items[j].Cells[4].Text;

                    if (Request.QueryString["readonly"].ToString() == "1")
                    {
                        txt.Enabled = false;
                        txtPREMIUM.Enabled = false;
                    }

                    if (bSales)
                    {
                        txtPREMIUM.ReadOnly = true;
                    }
                }
                                
                conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_DETAIL " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text + "," +
                                    "'" + DGR.Items[i].Cells[0].Text + "'," +
                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                    "'F'";
                conn.ExecuteQuery();
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                dgrFEMALE.DataSource = dt;
                dgrFEMALE.DataBind();

                for (int j = 0; j < dgrFEMALE.Items.Count; j++)
                {
                    TextBox txt = (TextBox)dgrFEMALE.Items[j].FindControl("TXT_MEMBER0");
                    TextBox txtPREMIUM = (TextBox)dgrFEMALE.Items[j].FindControl("TXT_PREMIUM0");
                    txt.Text = dgrFEMALE.Items[j].Cells[2].Text;
                    txtPREMIUM.Text = dgrFEMALE.Items[j].Cells[4].Text;

                    if (Request.QueryString["readonly"].ToString() == "1")
                    {
                        txt.Enabled = false;
                        txtPREMIUM.Enabled = false;
                    }

                    if (bSales)
                    {
                        txtPREMIUM.ReadOnly = true;
                    }
                }

                conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_DETAIL " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text + "," +
                                    "'" + DGR.Items[i].Cells[0].Text + "'," +
                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                    "'C'";
                conn.ExecuteQuery();
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                dgrCHILD.DataSource = dt;
                dgrCHILD.DataBind();

                for (int j = 0; j < dgrCHILD.Items.Count; j++)
                {
                    TextBox txt = (TextBox)dgrCHILD.Items[j].FindControl("TXT_MEMBER1");
                    TextBox txtPREMIUM = (TextBox)dgrCHILD.Items[j].FindControl("TXT_PREMIUM1");
                    txt.Text = dgrCHILD.Items[j].Cells[2].Text;
                    txtPREMIUM.Text = dgrCHILD.Items[j].Cells[4].Text;

                    if (Request.QueryString["readonly"].ToString() == "1")
                    {
                        txt.Enabled = false;
                        txtPREMIUM.Enabled = false;
                    }

                    if (bSales)
                    {
                        txtPREMIUM.ReadOnly = true;
                    }
                }

                conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_DETAIL " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text + "," +
                                    "'" + DGR.Items[i].Cells[0].Text + "'," +
                                    "'" + DGR.Items[i].Cells[1].Text + "'," +
                                    "'D'";
                conn.ExecuteQuery();
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                dgrCHILDF.DataSource = dt;
                dgrCHILDF.DataBind();

                for (int j = 0; j < dgrCHILDF.Items.Count; j++)
                {
                    TextBox txt = (TextBox)dgrCHILDF.Items[j].FindControl("TXT_MEMBER2");
                    TextBox txtPREMIUM = (TextBox)dgrCHILDF.Items[j].FindControl("TXT_PREMIUM2");
                    txt.Text = dgrCHILDF.Items[j].Cells[2].Text;
                    txtPREMIUM.Text = dgrCHILDF.Items[j].Cells[4].Text;

                    if (Request.QueryString["readonly"].ToString() == "1")
                    {
                        txt.Enabled = false;
                        txtPREMIUM.Enabled = false;
                    }

                    if (bSales)
                    {
                        txtPREMIUM.ReadOnly = true;
                    }
                }
            }

            if (DDL_BENEFIT.SelectedValue == "MT")
            {
                DGR.Columns[4].Visible = false;
                DGR.Columns[6].Visible = false;
            }
            else
            {
                DGR.Columns[4].Visible = true;
                DGR.Columns[6].Visible = true;
            }
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DataGrid dgrMALE = (DataGrid)DGR.Items[i].FindControl("DGR_MALE");
                DataGrid dgrFEMALE = (DataGrid)DGR.Items[i].FindControl("DGR_FEMALE");
                DataGrid dgrCHILD = (DataGrid)DGR.Items[i].FindControl("DGR_CHILD");
                DataGrid dgrCHILDF = (DataGrid)DGR.Items[i].FindControl("DGR_CHILD_F");

                for (int j = 0; j < dgrMALE.Items.Count; j++)
                {
                    TextBox txt = (TextBox)dgrMALE.Items[j].FindControl("TXT_MEMBER");
                    TextBox txtPREMIUM = (TextBox)dgrMALE.Items[j].FindControl("TXT_PREMIUM");
                    try
                    {
                        conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_UPDATE " +
                                            "'" + LB_QUOTNO.Text + "'," +
                                            "'" + LB_VER.Text + "'," +
                                            "'" + DGR.Items[i].Cells[0].Text + "'," +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "'M'," +
                                            "'" + dgrMALE.Items[j].Cells[0].Text + "'," +
                                            txt.Text.Replace(",", "") + "," +
                                            txtPREMIUM.Text.Replace(",", "") + "," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = "<BR>" + ex.Message;
                    }
                }

                for (int j = 0; j < dgrFEMALE.Items.Count; j++)
                {
                    TextBox txt = (TextBox)dgrFEMALE.Items[j].FindControl("TXT_MEMBER0");
                    TextBox txtPREMIUM = (TextBox)dgrFEMALE.Items[j].FindControl("TXT_PREMIUM0");
                    try
                    {
                        conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_UPDATE " +
                                            "'" + LB_QUOTNO.Text + "'," +
                                            "'" + LB_VER.Text + "'," +
                                            "'" + DGR.Items[i].Cells[0].Text + "'," +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "'F'," +
                                            "'" + dgrFEMALE.Items[j].Cells[0].Text + "'," +
                                            txt.Text.Replace(",", "") + "," +
                                            txtPREMIUM.Text.Replace(",", "") + "," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = "<BR>" + ex.Message;
                    }
                }

                for (int j = 0; j < dgrCHILD.Items.Count; j++)
                {
                    TextBox txt = (TextBox)dgrCHILD.Items[j].FindControl("TXT_MEMBER1");
                    TextBox txtPREMIUM = (TextBox)dgrCHILD.Items[j].FindControl("TXT_PREMIUM1");
                    try
                    {
                        conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_UPDATE " +
                                            "'" + LB_QUOTNO.Text + "'," +
                                            "'" + LB_VER.Text + "'," +
                                            "'" + DGR.Items[i].Cells[0].Text + "'," +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "'C'," +
                                            "'" + dgrCHILD.Items[j].Cells[0].Text + "'," +
                                            txt.Text.Replace(",", "") + "," +
                                            txtPREMIUM.Text.Replace(",", "") + "," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = "<BR>" + ex.Message;
                    }
                }

                for (int j = 0; j < dgrCHILDF.Items.Count; j++)
                {
                    TextBox txt = (TextBox)dgrCHILDF.Items[j].FindControl("TXT_MEMBER2");
                    TextBox txtPREMIUM = (TextBox)dgrCHILDF.Items[j].FindControl("TXT_PREMIUM2");
                    try
                    {
                        conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_UPDATE " +
                                            "'" + LB_QUOTNO.Text + "'," +
                                            "'" + LB_VER.Text + "'," +
                                            "'" + DGR.Items[i].Cells[0].Text + "'," +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "'D'," +
                                            "'" + dgrCHILDF.Items[j].Cells[0].Text + "'," +
                                            txt.Text.Replace(",", "") + "," +
                                            txtPREMIUM.Text.Replace(",", "") + "," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = "<BR>" + ex.Message;
                    }
                }
            }

            FillDGR();
        }

        protected void BT_EXCEL_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE='" + DDL_TEMPLATE.SelectedValue + "'";
            conn.ExecuteQuery();

            string URL = conn.GetFieldValue("URL").ToString() +
                            "&QUOTNO=" + LB_QUOTNO.Text +
                            "&VERNO=" + LB_VER.Text +
                            "&rs:Format=EXCEL";
            Response.Redirect(URL);
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            if (TXT_FILE_UPLOAD.Value == "")
                return;

            try
            {
                UploadFile();
                conn.QueryString = "exec SP_QUOTATION_VERSION_MEMBER_VERIFY " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    "'" + LB_VER.Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
                Show_TR_MEMBER();

            }
            catch (Exception er)
            {
                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = er.Message;
                return;
            }

            LB_ERR.ForeColor = System.Drawing.Color.Black;
            LB_ERR.Text = "Upload success";
        }

        private void UploadFile()
        {
            _path = Request.PhysicalApplicationPath + "Upload/";
            string filename;

            HttpFileCollection uploadedFiles = Request.Files;
            HttpPostedFile userPostedFile = uploadedFiles[0];

            if (userPostedFile.ContentLength > 0)
            {
                conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
                conn.ExecuteQuery();

                string code = conn.GetFieldValue(0, 0).ToString();

                filename = code + "_" + Path.GetFileName(userPostedFile.FileName);
                _fullpath = _path + filename;

                userPostedFile.SaveAs(_fullpath);

                switch (DDL_TEMPLATE.SelectedValue)
                {
                    case "4": ProcessFile(_fullpath); break;
                    case "8": ProcessFileMixTemplate(_fullpath); break;
                }


                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);
            }
        }

        protected void ProcessFile(string FullPath)
        {
            conn.QueryString = "select TBL = convert(varchar(3),PKG_NO) + replace(DESCR,' ','') " +
                                "from QUOTATION_VERSION_PACKAGE  " +
                                "where  " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "'  " +
                                "and VERNO = " + LB_VER.Text + " " +
                                "order by PKG_NO";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            string[] Paket = new string[conn.GetRowCount()];
            for (int i = 0; i < conn.GetRowCount(); i++)
                Paket[i] = conn.GetFieldValue(i, "TBL").ToString();

            conn.QueryString = "delete from QUOTATION_VERSION_MEMBER " +
                                "where  " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "'  " +
                                "and VERNO = " + LB_VER.Text;
            conn.ExecuteNonQuery();

            int IDX = 0;

            for (int i = 0; i < Paket.Count(); i++)
            {
                OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + ";Extended Properties=Excel 8.0");
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter("select NAMA,DOB,GENDER,E,BRANCHCODE,MEMBERID from [" + Paket[i] + "$] where NAMA<>''", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                string EMPLOYEE_SEQ = "1";

                string NAMA = "";
                string DOB = "";
                string GENDER = "";
                string E = "";
                string BRANCHCODE = "";
                string MEMBERID = "";

                foreach (DataRow myRow in dt.Rows)
                {
                    IDX++;
                    string values = "";

                    foreach (DataColumn myCol in dt.Columns)
                    {
                        switch (myCol.Caption)
                        {
                            case "NAMA": NAMA = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                            case "DOB": DOB = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                            case "GENDER": GENDER = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                            case "E": E = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                            case "BRANCHCODE": BRANCHCODE = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                            case "MEMBERID": MEMBERID = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                        }
                    }

                    if (E == "E")
                        EMPLOYEE_SEQ = IDX.ToString();

                    values = "'" + LB_QUOTNO.Text + "'," +
                                "'" + LB_VER.Text + "'," +
                                "'" + (i + 1).ToString() + "'," +
                                "'" + IDX.ToString() + "'," +
                                "'" + NAMA + "'," +
                                "'" + DOB + "'," +
                                "'" + GENDER + "'," +
                                "'" + E + "'," +
                                "'" + BRANCHCODE + "'," +
                                "'" + MEMBERID + "'," +
                                "1," +
                                EMPLOYEE_SEQ + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                "GETDATE()";

                    conn.QueryString = "insert into QUOTATION_VERSION_MEMBER " +
                                        "(QUOTNO,VERNO,PKG_NO,SEQ,NAMA,DOB,GENDER,EMPLOYEE,BRANCH_CODE,MEMBER_ID,STAT,EMPLOYEE_SEQ,CREATEBY,CREATEDATE) " +
                                        "select " +
                                        values;

                    conn.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        protected void ProcessFileMixTemplate(string FullPath)
        {
            conn.QueryString = "select PKG_NO, DESCR " +
                                "from QUOTATION_VERSION_PACKAGE  " +
                                "where  " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "'  " +
                                "and VERNO = " + LB_VER.Text + " " +
                                "order by PKG_NO";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return;

            DataTable dtpaket;
            dtpaket = new DataTable();
            dtpaket = conn.GetDataTable().Copy();

            conn.QueryString = "delete from QUOTATION_VERSION_MEMBER " +
                                "where  " +
                                "QUOTNO = '" + LB_QUOTNO.Text + "'  " +
                                "and VERNO = " + LB_VER.Text;
            conn.ExecuteNonQuery();


            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + ";Extended Properties=Excel 8.0");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select NAMA,DOB,GENDER,E,[BRANCH CODE],[MEMBER ID],PAKET from [" + LB_QUOTNO.Text + "$] where NAMA<>''", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            int IDX = 0;
            string EMPLOYEE_SEQ = "1";

            string NAMA = "";
            string DOB = "";
            string GENDER = "";
            string E = "";
            string BRANCHCODE = "";
            string MEMBERID = "";
            string PAKET = "";

            foreach (DataRow myRow in dt.Rows)
            {
                IDX++;
                string values = "";

                foreach (DataColumn myCol in dt.Columns)
                {
                    switch (myCol.Caption)
                    {
                        case "NAMA": NAMA = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                        case "DOB": DOB = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                        case "GENDER": GENDER = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                        case "E": E = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                        case "BRANCH CODE": BRANCHCODE = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                        case "MEMBER ID": MEMBERID = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                        case "PAKET": PAKET = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`"); break;
                    }
                }

                if (E == "E")
                    EMPLOYEE_SEQ = IDX.ToString();

                string PKG_NO = "";
                for (int i = 0; i < dtpaket.Rows.Count; i++)
                {
                    if (dtpaket.Rows[i][1].ToString().ToUpper().Replace(" ", "").Trim() == PAKET.ToUpper().Replace(" ", "").Trim())
                    {
                        PKG_NO = dtpaket.Rows[i][0].ToString();
                        break;
                    }
                }

                if (PKG_NO == "")
                    continue;

                values = "'" + LB_QUOTNO.Text + "'," +
                            "'" + LB_VER.Text + "'," +
                            "'" + PKG_NO + "'," +
                            "'" + IDX.ToString() + "'," +
                            "'" + NAMA + "'," +
                            "'" + DOB + "'," +
                            "'" + GENDER + "'," +
                            "'" + E + "'," +
                            "'" + BRANCHCODE + "'," +
                            "'" + MEMBERID + "'," +
                            "1," +
                            EMPLOYEE_SEQ + "," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                            "GETDATE()";

                conn.QueryString = "insert into QUOTATION_VERSION_MEMBER " +
                                    "(QUOTNO,VERNO,PKG_NO,SEQ,NAMA,DOB,GENDER,EMPLOYEE,BRANCH_CODE,MEMBER_ID,STAT,EMPLOYEE_SEQ,CREATEBY,CREATEDATE) " +
                                    "select " +
                                    values;

                conn.ExecuteNonQuery();
            }

            con.Close();

        }

        protected void Show_TR_MEMBER()
        {
            conn.QueryString = "select CNT = count(SEQ) from QUOTATION_VERSION_MEMBER " +
                                    "where " +
                                    "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                    "and VERNO = " + LB_VER.Text;
            conn.ExecuteQuery();

            if (conn.GetFieldValue("CNT").ToString() != "0")
            {
                ShowSummary();
                FillDGRMember();
            }
        }

        protected void ShowSummary()
        {
            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_MEMBER_SUMMARY " +
                                    "'" + LB_QUOTNO.Text + "', " +
                                    LB_VER.Text;
                conn.ExecuteQuery();

                LB_APPROVED.Text = conn.GetFieldValue("APPROVED").ToString();
                LB_REJECTED.Text = conn.GetFieldValue("REJECTED").ToString();
                LB_TOTAL.Text = conn.GetFieldValue("TOTAL").ToString();
            }
            catch { }
        }

        protected void FillDGRMember()
        {
            conn.QueryString = "exec SP_QUOTATION_VERSION_MEMBER " +
                                "'" + LB_QUOTNO.Text + "', " +
                                LB_VER.Text + "," +
                                DDL_MEMBER.SelectedValue;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MEMBER.DataSource = dt;
            DGR_MEMBER.DataBind();

            switch (DDL_MEMBER.SelectedValue)
            {
                case "0": DGR_MEMBER.HeaderStyle.BackColor = System.Drawing.Color.Red;
                    DGR_MEMBER.HeaderStyle.ForeColor = System.Drawing.Color.White;
                    DGR_MEMBER.Columns[10].Visible = true;
                    DGR_MEMBER.Columns[12].Visible = false;
                    break;
                case "1": DGR_MEMBER.HeaderStyle.BackColor = System.Drawing.Color.Green;
                    DGR_MEMBER.HeaderStyle.ForeColor = System.Drawing.Color.White;
                    DGR_MEMBER.Columns[10].Visible = true;
                    DGR_MEMBER.Columns[12].Visible = false;
                    break;
                case "2": DGR_MEMBER.HeaderStyle.BackColor = System.Drawing.Color.Pink;
                    DGR_MEMBER.HeaderStyle.ForeColor = System.Drawing.Color.Red;
                    DGR_MEMBER.Columns[10].Visible = false;
                    DGR_MEMBER.Columns[12].Visible = true;
                    break;
                case "3": DGR_MEMBER.HeaderStyle.BackColor = System.Drawing.Color.Cyan;
                    DGR_MEMBER.HeaderStyle.ForeColor = System.Drawing.Color.Blue;
                    DGR_MEMBER.Columns[10].Visible = true;
                    DGR_MEMBER.Columns[12].Visible = false;
                    break;
            }

            for (int i = 0; i < DGR_MEMBER.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_MEMBER.Items[i].FindControl("CB");
                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    cb.Enabled = false;
                }

                if (DGR_MEMBER.Items[i].Cells[11].Text == "1")
                    cb.Checked = true;

                if (DGR_MEMBER.Items[i].Cells[2].Text.Trim().Replace("&nbsp;", "") == "")
                    DGR_MEMBER.Items[i].BackColor = System.Drawing.Color.Yellow;

                if (DGR_MEMBER.Items[i].Cells[8].Text == "E")
                {
                    DGR_MEMBER.Items[i].Cells[3].Font.Bold = true;
                    DGR_MEMBER.Items[i].Cells[3].ForeColor = System.Drawing.Color.Blue;
                }
            }

            if (Request.QueryString["readonly"].ToString() == "1")
            {
                DGR_MEMBER.Columns[13].Visible = false;
            }
        }

        protected void DDL_MEMBER_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRMember();
        }

        protected void BT_XL_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE='7'";
            conn.ExecuteQuery();

            string URL = conn.GetFieldValue("URL").ToString() + "&rs:Format=EXCEL&QUOTNO=" + LB_QUOTNO.Text + "&VERNO=" + LB_VER.Text;
            Response.Redirect(URL);
        }

        private void UploadFile2()
        {
            _path = Request.PhysicalApplicationPath + "Upload/";
            string filename;

            HttpFileCollection uploadedFiles = Request.Files;
            HttpPostedFile userPostedFile = uploadedFiles[0];

            if (userPostedFile.ContentLength > 0)
            {
                conn.QueryString = "select convert(varchar(30),GETDATE(),112) + replace(convert(varchar(30),GETDATE(),114),':','')";
                conn.ExecuteQuery();

                string code = conn.GetFieldValue(0, 0).ToString();

                filename = code + "_" + Path.GetFileName(userPostedFile.FileName);
                _fullpath = _path + filename;

                userPostedFile.SaveAs(_fullpath);
                ProcessFile2(_fullpath);

                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);
            }
        }

        protected void ProcessFile2(string FullPath)
        {

            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + ";Extended Properties=Excel 8.0");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select [PKG NO],[PP CODE],[GENDER],[START AGE],[END AGE],[PREMIUM] from [RPT_QUOTATION_VERSION_PACKAGE_P$] where DESCR<>''", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow myRow in dt.Rows)
            {
                string PKG_NO = "";
                string PP_CODE = "";
                string GENDER = "";
                string STARTAGE = "";
                string ENDAGE = "";
                string PREMIUM = "";

                foreach (DataColumn myCol in dt.Columns)
                {
                    switch (myCol.Caption)
                    {
                        case "PKG NO": PKG_NO = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                        case "PP CODE": PP_CODE = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                        case "GENDER": GENDER = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                        case "START AGE": STARTAGE = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                        case "END AGE": ENDAGE = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                        case "PREMIUM": PREMIUM = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                    }
                }

                try
                {
                    conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_XLS_INSERT " +
                                        "'" + LB_QUOTNO.Text + "'," +
                                        "'" + LB_VER.Text + "'," +
                                        PKG_NO + "," +
                                        "'" + PP_CODE + "'," +
                                        "'" + GENDER + "'," +
                                        STARTAGE + "," +
                                        ENDAGE + "," +
                                        PREMIUM + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "- " + ex.Message + "<BR>";
                }
            }

            con.Close();

        }

        protected void BT_XLS_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            if (TXT_FILE_UPLOAD_PREMIUM.Value == "")
                return;

            try
            {
                conn.QueryString = "delete from QUOTATION_VERSION_PACKAGE_PLAN_MEMBER where " +
                                    "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                    "and VERNO = " + LB_VER.Text;
                conn.ExecuteNonQuery();
                UploadFile2();
                conn.QueryString = "exec SP_QUOTATION_VERSION_MEMBER_VERIFY " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    "'" + LB_VER.Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
                Show_TR_MEMBER();
            }
            catch (Exception er)
            {
                LB_ERR.ForeColor = System.Drawing.Color.Red;
                LB_ERR.Text = LB_ERR.Text + "- " + er.Message;
                return;
            }

            LB_ERR.ForeColor = System.Drawing.Color.Black;
            LB_ERR.Text = "Upload success";
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_MEMBER.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_MEMBER.Items[i].FindControl("CB");

                if (cb == ((CheckBox)sender))
                {
                    string stat = "0";
                    if (cb.Checked)
                        stat = "1";

                    try
                    {
                        conn.QueryString = "exec SP_QUOTATION_VERSION_MEMBER_WOMEN_NO_MT_UPSERT " +
                                            "'" + LB_QUOTNO.Text + "'," +
                                            "'" + LB_VER.Text + "'," +
                                            "'" + DGR_MEMBER.Items[i].Cells[0].Text + "'," +
                                            "'" + DGR_MEMBER.Items[i].Cells[1].Text + "'," +
                                            "'" + stat + "'";
                        conn.ExecuteNonQuery();
                        
                        FillDGRMember();
                        return;
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = ex.Message;
                        LB_ERR.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
            }
        }

        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeMode();
        }

        protected void ChangeMode()
        {
            TR_INDIKATIF.Visible = false;
            TR_DETAIL.Visible = false;

            switch (DDL_MODE.SelectedValue)
            {
                case "0":   TR_INDIKATIF.Visible = true; 
                            FillDGR();
                            if (VerifySalesAccess())
                            {
                                TD_RATE_UPLOAD.Visible = false;
                            }
                            break;
                case "1":   TR_DETAIL.Visible = true;
                            Show_TR_MEMBER(); 
                            break;
            }
        }

        protected void DGR_MEMBER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                DGR_MEMBER.Visible = false;
                TBL_MEMBER_EDIT.Visible = true;
                LoadMember(e.Item.Cells[1].Text);
            }
        }
        
        protected void LoadMember(string seq)
        {
            conn.QueryString = "select CODE,DESCR from PR_GENDER";
            conn.ExecuteQuery();
            DDL_SEX.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_SEX.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select DESCR from QUOTATION_VERSION_PACKAGE where QUOTNO='" +LB_QUOTNO.Text+ "' and VERNO=" +LB_VER.Text+ " order by PKG_NO";
            conn.ExecuteQuery();
            DDL_PAKET.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PAKET.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select " +
                                "NAMA, " +
                                "DOB = convert(varchar(20),DOB,103), " +
                                "GENDER, " +
                                "b.DESCR, " +
                                "EMPLOYEE, " +
                                "BRANCH_CODE " +
                                "from QUOTATION_VERSION_MEMBER a " +
                                "inner join QUOTATION_VERSION_PACKAGE b on a.QUOTNO=b.QUOTNO and a.VERNO=b.VERNO and a.PKG_NO=b.PKG_NO " +
                                "where  " +
                                "a.QUOTNO='" + LB_QUOTNO.Text + "' " +
                                "and a.VERNO=" + LB_VER.Text + " " +
                                "and a.SEQ=" + seq;
            conn.ExecuteQuery();

            LB_SEQ.Text = seq;
            TXT_BRANCH.Text = conn.GetFieldValue("BRANCH_CODE").ToString();
            TXT_NAME.Text = conn.GetFieldValue("NAMA").ToString();
            TXT_DOB.Text = conn.GetFieldValue("DOB").ToString();
            DDL_ESC.SelectedValue = conn.GetFieldValue("EMPLOYEE").ToString();
            DDL_PAKET.SelectedValue = conn.GetFieldValue("DESCR").ToString();
            DDL_SEX.SelectedValue = conn.GetFieldValue("GENDER").ToString();

        }

        protected void BT_UPDATEMEMBER_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            try
            {
                conn.QueryString = "SP_QUOTATION_VERSION_MEMBER_UPDATE " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    "'" + LB_VER.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + TXT_NAME.Text.Trim() + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + DDL_SEX.SelectedValue + "'," +
                                    "'" + DDL_PAKET.SelectedValue + "'," +
                                    "'" + DDL_ESC.SelectedValue + "'," +
                                    "'" + TXT_BRANCH.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = ex.Message;
                return;
            }

            FillDGRMember();
            TBL_MEMBER_EDIT.Visible = false;
            DGR_MEMBER.Visible = true;
        }
    }
}
