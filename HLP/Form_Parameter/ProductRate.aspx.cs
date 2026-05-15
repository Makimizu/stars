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

namespace HLP.Form_Parameter
{
    public partial class ProductRate : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["code"];
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select a.CODE,a.DESCR from PR_BENEFIT a " +
                                "inner join PARAM_BENEFIT_SEQ b on a.CODE=b.CODE " +
                                "order by b.SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLPlan();
            FillDGR();
        }

        protected void FillDDLPlan()
        {
            DDL_PLAN.Items.Clear();
            DDL_PLAN.Items.Add(new ListItem("-- ALL --", ""));

            conn.QueryString = "select CODE, PLAN_VALUE from PARAM_PRODUCT_PLAN where PRODUCT_CODE='" + LB_CODE.Text + "' and BENEFIT_ID='" + DDL_BENEFIT.SelectedValue + "' order by PLAN_VALUE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PLAN.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_PRODUCT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLPlan();
            FillDGR();
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLPlan();
            FillDGR();
        }

        protected void DDL_PLAN_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void FillDGR()
        {
            DGR.Visible = false;
            DGR_ALL.Visible = false;

            if (DDL_PLAN.SelectedValue != "")
            {
                DGR.Visible = true;
                conn.QueryString = "select " +
                                    "a.BENEFIT_DETAIL_ID, " +
                                    "b.DESCR, " +
                                    "RATE = isnull(a.RATE,0) " +
                                    "from PARAM_PRODUCT_PLAN_RATE a " +
                                    "inner join PARAM_PRODUCT_PLAN aa on a.PP_CODE=aa.CODE " +
                                    "inner join PARAM_BENEFIT_DETAIL b on a.BENEFIT_DETAIL_ID=b.CODE " +
                                    "where " +
                                    "aa.PRODUCT_CODE='" + LB_CODE.Text + "' " +
                                    "and aa.BENEFIT_ID='" + DDL_BENEFIT.SelectedValue + "' " +
                                    "and a.PP_CODE='" + DDL_PLAN.SelectedValue + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR.DataSource = dt;
                DGR.DataBind();

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                    txt.Text = DGR.Items[i].Cells[2].Text;
                }
            }
            else
            {
                DGR_ALL.Visible = true;
                conn.QueryString = "exec SP_PARAM_PRODUCT_PLAN_RATE " +
                                    "'" + LB_CODE.Text + "'," +
                                    "'" + DDL_BENEFIT.SelectedValue + "'";
                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_ALL.DataSource = dt;
                DGR_ALL.DataBind();

                for (int i = 0; i < DGR_ALL.Items.Count; i++)
                {
                    DropDownList ddl = (DropDownList)DGR_ALL.Items[i].FindControl("DDL_PLAN");
                    conn.QueryString = "select " +
                                        "a.BENEFIT_DETAIL_ID " +
                                        "from PARAM_PRODUCT_BENEFIT_DETAIL a " +
                                        "inner join PARAM_BENEFIT_DETAIL b on a.BENEFIT_DETAIL_ID=b.CODE " +
                                        "where " +
                                        "a.PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                        "and b.BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                        "and a.BENEFIT_DETAIL_ID <> '" + DGR_ALL.Items[i].Cells[1].Text + "' " +
                                        "order by 1";
                    conn.ExecuteQuery();
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 0).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    }
                }
            }
        }

        protected void DGR_ALL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_PLAN");

            if (e.CommandName == "Copy")
            {
                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_PLAN_RATE_COPY " +
                                        "'" + LB_CODE.Text + "'," +
                                        "'" + ddl.SelectedValue + "'," +
                                        "'" + e.Item.Cells[1].Text + "'," +
                                        "1," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                    FillDGR();
                }
                catch { }
            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_PLAN");

                conn.QueryString = "select " +
                                        "a.CODE, " +
                                        "a.PLAN_VALUE " +
                                        "from PARAM_PRODUCT_PLAN a " +
                                        "where " +
                                        "a.PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                        "and a.BENEFIT_ID = '" + DDL_BENEFIT.SelectedValue + "' " +
                                        "and a.CODE <> '" + DDL_PLAN.SelectedValue + "' " +
                                        "order by a.PLAN_VALUE";
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_PLAN");

            if (e.CommandName == "Copy")
            {
                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_PLAN_RATE_COPY_V " +
                                        "'" + LB_CODE.Text + "'," +
                                        "'" + ddl.SelectedValue + "'," +
                                        "'" + DDL_PLAN.SelectedValue + "'," +
                                        "1," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();

                    FillDGR();
                }
                catch { }
            }

            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtVal = (TextBox)DGR.Items[i].FindControl("TXT_VAL");

                    try
                    {
                        conn.QueryString = "update PARAM_PRODUCT_PLAN_RATE set " +
                                            "RATE = " + txtVal.Text.Trim() + ", " +
                                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                            "LASTCHANGEDATE = GETDATE() " +
                                            "where " +
                                            "PP_CODE = '" + DDL_PLAN.SelectedValue + "' " +
                                            "and BENEFIT_DETAIL_ID = '" + DGR.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }

                FillDGR();
            }
        }

        protected void BT_DOWNLOAD_Click(object sender, EventArgs e)
        {
            conn.QueryString = "select URL from V_LINK_SC_REPORT_LIST where CODE=1";
            conn.ExecuteQuery();

            string PLANVALUE = "=" + DDL_PLAN.SelectedItem.Text;
            if (DDL_PLAN.SelectedValue == "")
                PLANVALUE = ":IsNull=true";

            Response.Redirect(conn.GetFieldValue("URL").ToString() + "&PRODUCT_CODE=" + LB_CODE.Text + "&BENEFIT_ID=" + DDL_BENEFIT.SelectedValue + "&PLAN_VALUE" + PLANVALUE + "&rs:Format=EXCEL"); ;
        }

        protected void BT_XLS_UPLOAD_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";

            if (TXT_FILE_UPLOAD.Value == "")
                return;

            try
            {
                UploadFile();
                FillDGR();
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

        private void UploadFile()
        {
            string _fullpath, _path;

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
            OleDbDataAdapter da = new OleDbDataAdapter("select [BENEFIT],[PLAN],[BENEFIT_DETAIL],[RATE],[UP] from [RPT_PARAM_PRODUCT_PLAN_RATE$] where BENEFIT<>''", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow myRow in dt.Rows)
            {
                string BENEFIT = "";
                string PLAN = "";
                string BENEFIT_DETAIL = "";
                string RATE = "";
                string UP = "";

                foreach (DataColumn myCol in dt.Columns)
                {
                    switch (myCol.Caption)
                    {
                        case "BENEFIT"          : BENEFIT = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                                                    break;
                        case "PLAN"             : PLAN = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                                                    break;
                        case "BENEFIT_DETAIL"   : BENEFIT_DETAIL = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                                                    break;
                        case "RATE"             : RATE = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                                                    break;
                        case "UP"               : UP = myRow[myCol].ToString().Replace(",", ".").Replace("'", "`");
                                                    break;
                    }
                }

                try
                {
                    conn.QueryString = "exec SP_PARAM_PRODUCT_RATEUP_UPSERT " +
                                        "'" + LB_CODE.Text + "'," +
                                        "'" + BENEFIT + "'," +
                                        "'" + PLAN + "'," +
                                        "'" + BENEFIT_DETAIL + "'," +
                                        RATE.Replace(",",".") + "," +
                                        UP.Replace(",", ".") + "," +
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
    }
}