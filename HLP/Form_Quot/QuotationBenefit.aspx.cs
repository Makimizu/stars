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
    public partial class QuotationBenefit : System.Web.UI.Page
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
                FillDGR();
            }
        }

        protected void Setup()
        {
            if (Request.QueryString["readonly"].ToString() == "1")
            {
                BT_SAVE.Visible = false;
                TD_UPLOAD.Visible = false;
            }

            if (VerifySalesAccess())
                TD_UPLOAD.Visible = false;

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

            conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_DETAIL " +
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
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");

                if (Request.QueryString["readonly"].ToString() == "1")
                {
                    btDEL.Visible = false;
                }

                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");
            }


            for (int i = 107; i < 133; i++)
            {
                DGR.Columns[i].Visible = true;
            }


            for (int i = 3; i < 29; i++)
            {
                if (DGR.Items[0].Cells[i].Text.Replace("&nbsp;", "") == "")
                    DGR.Columns[i + 104].Visible = false;
                else
                {
                    string col = (i - 2).ToString();
                    if (col.Length == 1)
                        col = "0" + col;

                    for (int j = 0; j < DGR.Items.Count; j++)
                    {
                        TextBox txtRate = (TextBox)DGR.Items[j].FindControl("TXT_RATE" + col);
                        TextBox txtUP = (TextBox)DGR.Items[j].FindControl("TXT_UP" + col);
                        TextBox txtPremium = (TextBox)DGR.Items[j].FindControl("TXT_PREMIUM" + col);
                        System.Web.UI.HtmlControls.HtmlTableRow trRATE = (System.Web.UI.HtmlControls.HtmlTableRow)DGR.Items[j].FindControl("TR_DGR_RATE" + col);
                        System.Web.UI.HtmlControls.HtmlTableRow trPREMIUM = (System.Web.UI.HtmlControls.HtmlTableRow)DGR.Items[j].FindControl("TR_DGR_PREMIUM" + col);

                        if (Request.QueryString["readonly"].ToString() == "1")
                        {
                            txtRate.Enabled = false;
                            txtUP.Enabled = false;
                            txtPremium.Enabled = false;
                        }

                        txtUP.Text = DGR.Items[j].Cells[i + 26].Text;
                        txtRate.Text = DGR.Items[j].Cells[i + 52].Text;
                        txtPremium.Text = DGR.Items[j].Cells[i + 78].Text;

                        if (bSales)
                        {
                            trRATE.Visible = false;
                            trPREMIUM.Visible = false;
                        }
                    }
                }
            }
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from QUOTATION_VERSION_PACKAGE_PLAN_DETAIL " +
                                        "where " +
                                        "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                        "and VERNO = '" + LB_VER.Text + "' " +
                                        "and BENEFIT_DETAIL_ID = '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                    SaveALL();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                for (int i = 2; i < 28; i++)
                {
                    string col = (i - 1).ToString();
                    if (col.Length == 1)
                        col = "0" + col;

                    Label lbPPCODE = (Label)e.Item.FindControl("LB_PPCODE" + col);

                    conn.QueryString = "select " +
                                        "PP_CODE = aa.DESCR + ' - ' + replace(a.PP_CODE,'" + LB_QUOTNO.Text + "','') " +
                                        "from QUOTATION_VERSION_PACKAGE_PLAN a " +
                                        "inner join QUOTATION_VERSION_PACKAGE aa on a.QUOTNO=aa.QUOTNO and a.VERNO=aa.VERNO and a.PKG_NO=aa.PKG_NO " +
                                        "inner join PARAM_PRODUCT_PLAN b on a.PP_CODE=b.CODE " +
                                        "where " +
                                        "a.QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                        "and a.VERNO = " + LB_VER.Text + " " +
                                        "and a.PKG_NO = " + (i - 1).ToString() + " " +
                                        "and b.BENEFIT_ID = '" + DDL_BENEFIT.Text + "'";
                    conn.ExecuteQuery();
                    if (conn.GetRowCount() > 0)
                        lbPPCODE.Text = conn.GetFieldValue("PP_CODE").ToString();
                }
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            SaveALL();
            FillDGR();
        }

        protected void SaveALL()
        {
            LB_ERR.Text = "";

            for (int i = 3; i < 29; i++)
            {
                if (DGR.Items[0].Cells[i].Text.Replace("&nbsp;", "") == "")
                    DGR.Columns[i + 104].Visible = false;
                else
                {
                    string col = (i - 2).ToString();
                    if (col.Length == 1)
                        col = "0" + col;

                    string ppcode = "";
                    conn.QueryString = "select " +
                                        "a.PP_CODE " +
                                        "from QUOTATION_VERSION_PACKAGE_PLAN a " +
                                        "inner join PARAM_PRODUCT_PLAN b on a.PP_CODE = b.CODE " +
                                        "where " +
                                        "a.QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                        "and a.VERNO = " + LB_VER.Text + "  " +
                                        "and b.BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                        "and a.PKG_NO = " + (i - 2).ToString();
                    conn.ExecuteQuery();
                    ppcode = conn.GetFieldValue("PP_CODE").ToString();

                    for (int j = 0; j < DGR.Items.Count; j++)
                    {
                        TextBox txtRate = (TextBox)DGR.Items[j].FindControl("TXT_RATE" + col);
                        TextBox txtUP = (TextBox)DGR.Items[j].FindControl("TXT_UP" + col);
                        

                        try
                        {
                            conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_DETAIL_UPDATE " +
                                                "'" + LB_QUOTNO.Text + "'," +
                                                LB_VER.Text + "," +
                                                (i - 2).ToString() + "," +
                                                "'" + ppcode + "'," +
                                                "'" + DGR.Items[j].Cells[1].Text + "'," +
                                                "'" + txtRate.Text.Trim().Replace(",", "") + "'," +
                                                "'" + txtUP.Text.Trim().Replace(",", "") + "'," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteNonQuery();

                        }
                        catch (System.Exception ex)
                        {
                            LB_ERR.Text = "<BR>" + ex.Message;
                        }
                    }
                }
            }

            try
            {
                conn.QueryString = "exec SP_QUOTATION_VERSION_MEMBER_REKAP " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text;
                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERR.Text = "<BR>" + ex.Message;
            }
        }

        protected void BT_XL_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE='6'";
            conn.ExecuteQuery();

            string URL = conn.GetFieldValue("URL").ToString() + "&rs:Format=EXCEL&QUOTNO=" + LB_QUOTNO.Text + "&VERNO=" + LB_VER.Text;
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
                conn.QueryString = "exec SP_QUOTATION_VERSION_PACKAGE_PLAN_MEMBER_RECALC " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' " +
                                    "exec SP_QUOTATION_VERSION_MEMBER_REKAP " +
                                    "'" + LB_QUOTNO.Text + "'," +
                                    LB_VER.Text;
                conn.ExecuteNonQuery();
                FillDGR();

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
                ProcessFile(_fullpath);

                if (File.Exists(_fullpath))
                    File.Delete(_fullpath);
            }
        }

        protected void ProcessFile(string FullPath)
        {

            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FullPath + ";Extended Properties=Excel 8.0");
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select [PACKAGE SEQ],[PP CODE],[BENEFIT ID],[UP],[RATE] from [RPT_QUOTATION_VERSION_PACKAGE_P$] where PACKAGE<>''", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow myRow in dt.Rows)
            {
                string PKG_NO = "";
                string PP_CODE = "";
                string BENEFIT_DETAIL_ID = "";
                string UP = "";
                string RATE = "";

                foreach (DataColumn myCol in dt.Columns)
                {
                    switch (myCol.Caption)
                    {
                        case "PACKAGE SEQ": PKG_NO = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                        case "PP CODE": PP_CODE = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                        case "BENEFIT ID": BENEFIT_DETAIL_ID = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                        case "UP": UP = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                        case "RATE": RATE = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                            break;
                    }
                }

                try
                {
                    conn.QueryString = "update QUOTATION_VERSION_PACKAGE_PLAN_DETAIL set " +
                                        "UP = " + UP + "," +
                                        "RATE = " + RATE + ", " +
                                        "PREMIUM = " + UP + "*" + RATE + " " +
                                        "where " +
                                        "QUOTNO = '" + LB_QUOTNO.Text + "' " +
                                        "and VERNO = '" + LB_VER.Text + "' " +
                                        "and PKG_NO = " + PKG_NO + " " +
                                        "and PP_CODE = '" + PP_CODE + "' " +
                                        "and BENEFIT_DETAIL_ID = '" + BENEFIT_DETAIL_ID + "'";

                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = LB_ERR.Text + "- " + ex.Message + "<BR>";
                }
            }

            con.Close();

        }
    }
}
