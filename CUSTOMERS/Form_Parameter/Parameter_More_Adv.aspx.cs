using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;
using System.IO;
using System.Globalization;
using Microsoft.VisualBasic.FileIO;
using System.Data.OleDb;
using System.Web.DynamicData;
using System.Web.UI.HtmlControls;
using System.Drawing;
//using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using Antlr.Runtime;

namespace CUSTOMERS.Form_Parameter
{
    public partial class Parameter_More_Adv : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "startLoading", "startDotLoading();", true);

                Session["DTDGR"] = null;
                Session["DTDGRQUERY"] = null;
                Session["DTDGRUPLOAD"] = null;
                Session["CHKAPPROVEALL"] = false;
                Session["FIRECHKAPPROVEALL"] = false;
                Session["DGRINDEX"] = null;
                Session["DGRQUERYINDEX"] = null;
                Session["DGRQUERYREBIND"] = false;
                //LB_CODE.Text = Request.QueryString["code"];
                Setup();
            }

            //string eventTarget = Request["__EVENTTARGET"];
            //if (eventTarget == "UncheckHeader")
            //{
            //    if (DGR.DataSource != null)
            //    {
            //        DataGridItem header = null;

            //        foreach (DataGridItem item in DGR.Controls[0].Controls)
            //        {
            //            if (item.ItemType == ListItemType.Header)
            //            {
            //                header = item;
            //                break;
            //            }
            //        }

            //        if (header != null && header.ItemType == ListItemType.Header)
            //        {
            //            CheckBox cb = header.FindControl("CHK_APPROVE_ALL") as CheckBox;

            //            if (cb != null)
            //            {
            //                Session["CHKAPPROVEALL"] = false;
            //                cb.Checked = false;

            //            }
            //        }
            //    }
            //}
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LB_CODE.Text = Request.QueryString["code"];

            ReloadDGR();

            if (DGR.DataSource != null)
            {
                DataGridItem header = null;

                foreach (DataGridItem item in DGR.Controls[0].Controls)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        header = item;
                        break;
                    }
                }

                if (header != null && header.ItemType == ListItemType.Header)
                {
                    CheckBox cb = header.FindControl("CHK_APPROVE_ALL") as CheckBox;

                    if (cb != null)
                    {
                        bool fireEvent = (bool)Session["FIRECHKAPPROVEALL"];

                        if (fireEvent)
                        {
                            CHK_APPROVE_ALL_CheckedChanged(cb, EventArgs.Empty);
                        }
                        Session["FIRECHKAPPROVEALL"] = false;
                    }
                }
            }

            FillDGRQUERY();
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (DGR.DataSource != null)
            {
                DataGridItem header = null;

                foreach (DataGridItem item in DGR.Controls[0].Controls)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        header = item;
                        break;
                    }
                }

                if (header != null && header.ItemType == ListItemType.Header)
                {
                    CheckBox cb = header.FindControl("CHK_APPROVE_ALL") as CheckBox;

                    if (cb != null)
                    {
                        if ((bool)Session["CHKAPPROVEALL"])
                        {
                            cb.Checked = true;
                        }
                        else
                        {
                            cb.Checked = false;
                        }
                    }
                }
            }

        }

        protected void Setup()
        {
            LB_PARAM.Text = LB_CODE.Text;
            FillDGR();
            FillDGRQUERY();
        }

        protected void FillDGR()
        {
            DGR.DataSource = null;
            DGR.DataBind();

            conn.QueryString = "exec SP_TABLE_SCHEMA '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            DataTable dta = conn.GetDataTable();

            bool exists = dta.AsEnumerable()
                                .Any(row => row.Field<string>("name") == "STATUSTYPE");

            if (exists)
            {
                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_LOAD] " +
                                                    "@F1 = '', " +
                                                    "@F2 = '', " +
                                                    "@F3 = '', " +
                                                    "@F4 = '', " +
                                                    "@F5 = '', " +
                                                    "@F6 = '', " +
                                                    "@F7 = '', " +
                                                    "@F8 = '', " +
                                                    "@F9 = '', " +
                                                    "@F10 = '', " +
                                                    "@F11 = '', " +
                                                    "@F12 = '', " +
                                                    "@F13 = '', " +
                                                    "@F14 = '', " +
                                                    "@F15 = '', " +
                                                    "@F16 = '', " +
                                                    "@F17 = '', " +
                                                    "@F18 = '', " +
                                                    "@F19 = '', " +
                                                    "@F20 = '', " +
                                                    "@TABLENAME = '" + LB_CODE.Text + "'";
                conn.ExecuteQuery();

                dta = conn.GetDataTable();

                dta.Columns.Add("CHECKED");

                foreach (DataRow row in dta.Rows)
                {
                    row["CHECKED"] = false;
                }

                if (Session["DGRINDEX"] != null)
                {
                    int pageSize = DGRQUERY.PageSize;
                    int pageIndex = (int)Session["DGRINDEX"];

                    int startRow = pageIndex * pageSize;
                    int endRow = Math.Min(startRow + pageSize, dta.Rows.Count);

                    if (endRow > startRow)
                    {
                        Session["DGRINDEX"] = pageIndex;
                        DGRQUERY.CurrentPageIndex = pageIndex;
                    }
                    else if (pageIndex > 0)
                    {
                        Session["DGRINDEX"] = pageIndex - 1;
                        DGRQUERY.CurrentPageIndex = pageIndex - 1;
                    }
                }
                else
                {
                    DGRQUERY.CurrentPageIndex = 0;
                }

                Session["DTDGR"] = dta;
                DGR.DataSource = dta;
                DGR.DataBind();

                DataGridItem header = null;

                foreach (DataGridItem item in DGR.Controls[0].Controls)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        header = item;
                        break;
                    }
                }

                if (header != null && header.ItemType == ListItemType.Header)
                {
                    CheckBox cb = header.FindControl("CHK_APPROVE_ALL") as CheckBox;

                    if (cb != null)
                    {
                        cb.Checked = false;
                        Session["CHKAPPROVEALL"] = false;
                    }
                }

                if (dta.Rows.Count < 1)
                {
                    LB_DGR_RECORD.Text = "";
                }
                else
                {
                    LB_DGR_RECORD.Text = dta.Rows.Count.ToString() + " records";
                }

                BT_APPROVE_ALL.Visible = true;
                BT_DEL_ALL.Visible = true;
            }

        }

        protected void ReloadDGR()
        {
            if (Session["DTDGR"] != null)
            {
                DataTable dta = (DataTable)Session["DTDGR"];

                if (Session["DGRINDEX"] != null)
                {
                    int pageSize = DGRQUERY.PageSize;
                    int pageIndex = (int)Session["DGRINDEX"];

                    int startRow = pageIndex * pageSize;
                    int endRow = Math.Min(startRow + pageSize, dta.Rows.Count);

                    if (endRow > startRow)
                    {
                        Session["DGRINDEX"] = pageIndex;
                        DGRQUERY.CurrentPageIndex = pageIndex;
                    }
                    else if (pageIndex > 0)
                    {
                        Session["DGRINDEX"] = pageIndex - 1;
                        DGRQUERY.CurrentPageIndex = pageIndex - 1;
                    }
                }
                else
                {
                    DGRQUERY.CurrentPageIndex = 0;
                }

                foreach (DataRow r in dta.Rows)
                {
                    //System.Diagnostics.Debug.WriteLine($"Row {r["F1"]}, Checked: {r["CHECKED"]}");
                }

                DGR.DataSource = dta;
                DGR.DataBind();

                //for (int i = 0; i < DGR.Items.Count; i++)
                //{
                //    if (dta.Rows[i]["CHECKED"] != DBNull.Value)
                //    {
                //        bool isChecked = Convert.ToBoolean(dta.Rows[i]["CHECKED"]);

                //        if (isChecked)
                //        {
                //            CheckBox chk = (CheckBox)DGR.Items[i].FindControl("CHK_APPROVE");

                //            chk.Enabled = true;
                //            chk.Checked = true;
                //        }
                //    }
                //}
            }
        }
        protected void ReloadDGRUPLOAD()
        {
            if (Session["DTDGRUPLOAD"] != null)
            {
                DataTable dta = (DataTable)Session["DTDGRUPLOAD"];
                DGRUPLOAD.DataSource = dta;
                DGRUPLOAD.DataBind();

            }
        }

        protected void ClearDGRUPLOAD()
        {
            Session["DTDGRUPLOAD"] = null;
            DGRUPLOAD.DataSource = null;
            DGRUPLOAD.DataBind();
            LB_RECORD.Text = "";
            LB_ERR.Text = "";

        }
        protected void FillDGRQUERY()
        {
            conn.QueryString = "exec SP_TABLE_QUERY '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable();
            Session["DTDGRQUERY"] = dt;

            if (Session["DGRQUERYINDEX"] != null)
            {
                int pageSize = DGRQUERY.PageSize;
                int pageIndex = (int)Session["DGRQUERYINDEX"];

                int startRow = pageIndex * pageSize;
                int endRow = Math.Min(startRow + pageSize, dt.Rows.Count);

                if (endRow > startRow)
                {
                    Session["DGRQUERYINDEX"] = pageIndex;
                    DGRQUERY.CurrentPageIndex = pageIndex;
                }
                else if (pageIndex > 0)
                {
                    Session["DGRQUERYINDEX"] = pageIndex - 1;
                    DGRQUERY.CurrentPageIndex = pageIndex - 1;
                }
            }
            else
            {
                DGRQUERY.CurrentPageIndex = 0;
            }

            DGRQUERY.DataSource = dt;
            DGRQUERY.DataBind();

            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

            if (DGRQUERY.DataSource != null)
            {
                Table table = (Table)DGRQUERY.Controls[0];
                DataGridItem emptyRow = new DataGridItem(0, 0, ListItemType.Item);
                emptyRow.BackColor = System.Drawing.Color.Yellow;

                int index = 0;

                conn.QueryString = "exec SP_TABLE_SCHEMA '" + LB_CODE.Text + "'";
                conn.ExecuteQuery();

                for (int i = 0; i < DGRQUERY.Columns.Count; i++)
                {
                    TableCell cell = new TableCell();

                    if (DGRQUERY.Columns[i] is TemplateColumn)
                    {
                        if (i == 40)
                        {
                            Button btn = new Button();
                            btn.ID = "BT_SAVEITEM";
                            btn.CommandName = "Save";
                            btn.CssClass = "ASPButton";
                            btn.Text = "S";
                            btn.ForeColor = Color.White;
                            btn.BackColor = Color.Green;
                            cell.Controls.Add(btn);
                        }
                        else
                        {
                            if (index < conn.GetRowCount())
                            {
                                if (conn.GetFieldValue(index, 6).ToString() != "")
                                {
                                    DropDownList ddl = new DropDownList();
                                    ddl.ID = "DDL_VAL" + (index + 1).ToString();
                                    ddl.CssClass = "ASPDropDownList";

                                    if (conn.GetFieldValue(index, 5).ToString() == "1")
                                        ddl.Items.Add(new ListItem("", ""));

                                    conn2.QueryString = "select * from " + conn.GetFieldValue(index, 6).ToString();
                                    conn2.ExecuteQuery();

                                    for (int k = 0; k < conn2.GetRowCount(); k++)
                                    {
                                        ddl.Items.Add(new ListItem(conn2.GetFieldValue(k, 1).ToString(), conn2.GetFieldValue(k, 0).ToString()));
                                    }
                                    cell.Controls.Add(ddl);
                                    index++;
                                }
                                else
                                {
                                    TextBox txt = new TextBox();
                                    txt.ID = "TXT_VAL" + (index + 1).ToString();
                                    txt.CssClass = "ASPTextBox";

                                    if (conn.GetFieldValue(index, 2).ToString() == "STR" || conn.GetFieldValue(index, 2).ToString() == "INT")
                                    {
                                        txt.MaxLength = int.Parse(conn.GetFieldValue(index, 3).ToString());
                                        txt.Width = int.Parse(conn.GetFieldValue(index, 3).ToString()) + 50;
                                    }

                                    cell.Controls.Add(txt);
                                    index++;

                                }
                            }

                        }
                    }
                    else if (DGRQUERY.Columns[i] is BoundColumn)
                    {
                        cell.Text = string.Empty;
                    }

                    emptyRow.Cells.Add(cell);
                }

                // Insert below header
                table.Rows.AddAt(2, emptyRow);
            }

            if (dt.Rows.Count < 1)
            {
                LB_DGRQUERY_RECORD.Text = "";
            }
            else
            {
                LB_DGRQUERY_RECORD.Text = dt.Rows.Count.ToString() + " records";
            }

            conn.QueryString = "exec SP_TABLE_SCHEMA '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();

            for (int i = 0; i < DGRQUERY.Items.Count; i++)
            {
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    Label lbl = (Label)DGRQUERY.Items[i].FindControl("LB_F" + (j + 1).ToString());
                    TextBox txt = (TextBox)DGRQUERY.Items[i].FindControl("TXT_VAL" + (j + 1).ToString());
                    Button del = (Button)DGRQUERY.Items[i].FindControl("BT_DELITEM");
                    DropDownList ddl = (DropDownList)DGRQUERY.Items[i].FindControl("DDL_VAL" + (j + 1).ToString());

                    if (conn.GetFieldValue(j, 4).ToString() != "")
                    {
                        lbl.Visible = true;
                        lbl.Text = dt.Rows[i + DGRQUERY.CurrentPageIndex * DGRQUERY.PageSize][j].ToString();
                    }
                    else if (conn.GetFieldValue(j, 6).ToString() != "")
                    {
                        ddl.Visible = true;

                        if (conn.GetFieldValue(j, 5).ToString() == "1")
                            ddl.Items.Add(new ListItem("", ""));

                        conn2.QueryString = "select * from " + conn.GetFieldValue(j, 6).ToString();
                        conn2.ExecuteQuery();

                        for (int k = 0; k < conn2.GetRowCount(); k++)
                        {
                            ddl.Items.Add(new ListItem(conn2.GetFieldValue(k, 1).ToString(), conn2.GetFieldValue(k, 0).ToString()));
                        }

                        ddl.SelectedValue = dt.Rows[i + DGRQUERY.CurrentPageIndex * DGRQUERY.PageSize][j].ToString();

                    }
                    else
                    {
                        txt.Visible = true;

                        if (conn.GetFieldValue(j, 2).ToString() == "STR" || conn.GetFieldValue(j, 2).ToString() == "INT")
                        {
                            txt.MaxLength = int.Parse(conn.GetFieldValue(j, 3).ToString());
                            txt.Width = int.Parse(conn.GetFieldValue(j, 3).ToString()) + 50;

                        }

                        txt.Text = dt.Rows[i + DGRQUERY.CurrentPageIndex * DGRQUERY.PageSize][j].ToString();
                    }
                }
            }

        }

        protected void DGRQUERY_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                for (int i = 20; i < DGRQUERY.Columns.Count - 1; i++)
                {
                    DGRQUERY.Columns[i].Visible = false;
                }

                conn.QueryString = "exec SP_TABLE_SCHEMA '" + LB_CODE.Text + "'";
                conn.ExecuteQuery();

                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    e.Item.Cells[20 + i].Text = conn.GetFieldValue(i, 1).ToString();
                    DGRQUERY.Columns[20 + i].Visible = true;
                }
            }


        }

        public static Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
                return root;

            foreach (Control child in root.Controls)
            {
                Control result = FindControlRecursive(child, id);
                if (result != null)
                    return result;
            }

            return null;
        }


        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                for (int i = 0; i < DGR.Columns.Count - 1; i++)
                {
                    DGR.Columns[i].Visible = false;
                }

                conn.QueryString = "exec SP_TABLE_SCHEMA '" + LB_CODE.Text + "'";
                conn.ExecuteQuery();

                DataTable dt = conn.GetDataTable();

                bool exists = dt.AsEnumerable()
                                    .Any(row => row.Field<string>("name") == "STATUSTYPE");

                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    string columnName = conn.GetFieldValue(i, 1).ToString();

                    if (columnName != "CHANGEDBY" && columnName != "STATUSTYPE")
                    {
                        e.Item.Cells[i].Text = conn.GetFieldValue(i, 1).ToString();
                        DGR.Columns[i].Visible = true;
                    }

                }

                if (exists)
                {
                    e.Item.Cells[20].VerticalAlign = VerticalAlign.Middle;
                    Literal label = new Literal();
                    label.Text = "<div style='display: flex; flex-direction: row; align-items: center;'>STATUSTYPE ";

                    CheckBox cb = new CheckBox();
                    cb.ID = "CHK_APPROVE_ALL";
                    cb.AutoPostBack = true;
                    cb.CheckedChanged += new EventHandler(CHK_APPROVE_ALL_CheckedChanged);


                    e.Item.Cells[20].Controls.Add(label);
                    e.Item.Cells[20].Controls.Add(cb);

                    Literal endDiv = new Literal();
                    endDiv.Text = "</div>";
                    e.Item.Cells[20].Controls.Add(endDiv);

                    DGR.Columns[20].Visible = true;
                }
            }
        }

        protected void DGRUPLOAD_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                for (int i = 0; i < DGRUPLOAD.Columns.Count - 1; i++)
                {
                    DGRUPLOAD.Columns[i].Visible = false;
                }

                conn.QueryString = "exec SP_TABLE_SCHEMA '" + LB_CODE.Text + "'";
                conn.ExecuteQuery();

                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    string columnName = conn.GetFieldValue(i, 1).ToString();

                    if (columnName != "CHANGEDBY" && columnName != "STATUSTYPE")
                    {
                        e.Item.Cells[i].Text = conn.GetFieldValue(i, 1).ToString();
                        DGRUPLOAD.Columns[i].Visible = true;
                    }

                }

                e.Item.Cells[20].Text = "";
                DGRUPLOAD.Columns[20].Visible = true;

            }
        }

        protected void CHK_APPROVE_CheckedChanged(object sender, EventArgs e)
        {
            if (DGR.DataSource != null)
            {
                DataGridItem header = null;

                foreach (DataGridItem item in DGR.Controls[0].Controls)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        header = item;
                        break;
                    }
                }

                if (header != null && header.ItemType == ListItemType.Header)
                {
                    CheckBox cb = header.FindControl("CHK_APPROVE_ALL") as CheckBox;

                    if (cb != null)
                    {
                        cb.Checked = false;
                        Session["CHKAPPROVEALL"] = false;
                    }
                }
            }

            CheckBox chk = (CheckBox)sender;
            DataGridItem row = (DataGridItem)chk.NamingContainer;

            DataTable dta = (DataTable)Session["DTDGR"];

            dta.Rows[(DGR.CurrentPageIndex * DGR.PageSize) + row.ItemIndex]["CHECKED"] = chk.Checked;

            Session["DTDGR"] = dta;
        }

        protected void CHK_APPROVE_ALL_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null)
            {
                //cb.CheckedChanged += new EventHandler(CHK_APPROVE_ALL_CheckedChanged);

                //if((bool)Session["CHKAPPROVEALL"])
                //{
                //    cb.Checked = false;
                //    Session["CHKAPPROVEALL"] = false;
                //}
                //else
                //{
                //    cb.Checked = true;
                //    Session["CHKAPPROVEALL"] = true;
                //}

                bool isChecked = cb.Checked;
                Session["CHKAPPROVEALL"] = cb.Checked;

                if (isChecked)
                {
                    DataTable dta = (DataTable)Session["DTDGR"];

                    for (int i = 0; i < dta.Rows.Count; i++)
                    {
                        dta.Rows[i]["CHECKED"] = true;

                        //if(i < DGR.Items.Count)
                        //{
                        //    CheckBox chk = (CheckBox)DGR.Items[i].FindControl("CHK_APPROVE");

                        //    chk.Enabled = true;
                        //    chk.Checked = true;
                        //}
                    }

                    Session["DTDGR"] = dta;
                }
                else
                {
                    DataTable dta = (DataTable)Session["DTDGR"];

                    for (int i = 0; i < dta.Rows.Count; i++)
                    {
                        dta.Rows[i]["CHECKED"] = false;

                        //if(i < DGR.Items.Count)
                        //{
                        //    CheckBox chk = (CheckBox)DGR.Items[i].FindControl("CHK_APPROVE");

                        //    chk.Enabled = true;
                        //    chk.Checked = false;
                        //}

                    }

                    Session["DTDGR"] = dta;
                }
            }
            //DGR.DataSource = null;
            //DGR.DataBind();
            ReloadDGR();
            ClearDGRUPLOAD();
            //FillDGRQUERY();
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void UpdateCheckBox(string id, bool approved)
        {
            if (HttpContext.Current.Session["DTDGR"] != null)
            {
                DataTable dta = (DataTable)HttpContext.Current.Session["DTDGR"];
                DataRow[] rows = dta.Select(string.Format("F1 = '{0}'", id));

                if (rows.Length > 0)
                {
                    if (approved)
                    {
                        rows[0]["CHECKED"] = true;

                    }
                    else
                    {
                        rows[0]["CHECKED"] = false;

                    }
                }
                dta.AcceptChanges();
                HttpContext.Current.Session["DTDGR"] = dta;

                HttpContext.Current.Session["CHKAPPROVEALL"] = false;
                HttpContext.Current.Session["FIRECHKAPPROVEALL"] = true;
            }

        }

        protected void BT_XLS_Click(object sender, EventArgs e)
        {
            string filename = "template_upload_" + LB_CODE.Text.Replace("PARAM_", "").Replace("PR_", "").ToLower() + ".xlsx";
            GlobalUse.SQLToFile(filename,
                                    "exec [CLIENT_BASE].[dbo].[SP_DOWNLOAD_TEMPLATE] '" + LB_CODE.Text + "'",
                                    Page);
        }

        protected void BT_UPLOAD_Click(object sender, EventArgs e)
        {
            ClearDGRUPLOAD();

            LB_ERR.Text = "";
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

            ViewState["IsClicked"] = null;
            ClientScript.RegisterStartupScript(this.GetType(), "hideLoading", "hideLoading();", true);
        }

        protected void BT_APPROVE_ALL_Click(object sender, EventArgs e)
        {
            if (DGR.DataSource != null)
            {
                DataGridItem header = null;

                foreach (DataGridItem item in DGR.Controls[0].Controls)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        header = item;
                        break;
                    }
                }

                if (header != null && header.ItemType == ListItemType.Header)
                {
                    CheckBox cb = header.FindControl("CHK_APPROVE_ALL") as CheckBox;

                    if (cb != null)
                    {
                        cb.Checked = false;
                    }
                }
            }

            if (Session["DTDGR"] != null)
            {
                DataTable dta = (DataTable)Session["DTDGR"];

                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_SCHEMA] " +
                                           "@NAME = '" + LB_CODE.Text + "'";

                conn.ExecuteQuery();

                foreach (DataRow row in dta.Rows)
                {

                    string f1 = "", f2 = "", f3 = "", f4 = "", f5 = "",
                                f6 = "", f7 = "", f8 = "", f9 = "", f10 = "",
                                f11 = "", f12 = "", f13 = "", f14 = "", f15 = "",
                                f16 = "", f17 = "", f18 = "", f19 = "", f20 = "";

                    string value = "";

                    if (Convert.ToBoolean(row["CHECKED"].ToString()) == true)
                    {
                        for (int i = 0; i < conn.GetRowCount(); i++)
                        {
                            if (conn.GetFieldValue(i, "name").ToString() == "CHANGEDBY")
                            {
                                continue;
                            }

                            if (conn.GetFieldValue(i, "name").ToString() == "STATUSTYPE")
                            {
                                value = "1";
                            }
                            else
                            {
                                value = row[i].ToString();
                            }

                            if (i == 0)
                            {
                                f1 = value;
                            }
                            if (i == 1)
                            {
                                f2 = value;
                            }
                            if (i == 2)
                            {
                                f3 = value;
                            }
                            if (i == 3)
                            {
                                f4 = value;
                            }
                            if (i == 4)
                            {
                                f5 = value;
                            }
                            if (i == 5)
                            {
                                f6 = value;
                            }
                            if (i == 6)
                            {
                                f7 = value;
                            }
                            if (i == 7)
                            {
                                f8 = value;
                            }
                            if (i == 8)
                            {
                                f9 = value;
                            }
                            if (i == 9)
                            {
                                f10 = value;
                            }
                            if (i == 10)
                            {
                                f11 = value;
                            }
                            if (i == 11)
                            {
                                f12 = value;
                            }
                            if (i == 12)
                            {
                                f13 = value;
                            }
                            if (i == 13)
                            {
                                f14 = value;
                            }
                            if (i == 14)
                            {
                                f15 = value;
                            }
                            if (i == 15)
                            {
                                f16 = value;
                            }
                            if (i == 16)
                            {
                                f17 = value;
                            }
                            if (i == 17)
                            {
                                f18 = value;
                            }
                            if (i == 18)
                            {
                                f19 = value;
                            }
                            if (i == 19)
                            {
                                f20 = value;
                            }
                        }

                        Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

                        conn2.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_UPSERT] " +
                                "@F1 = '" + f1 + "', " +
                                "@F2 = '" + f2 + "', " +
                                "@F3 = '" + f3 + "', " +
                                "@F4 = '" + f4 + "', " +
                                "@F5 = '" + f5 + "', " +
                                "@F6 = '" + f6 + "', " +
                                "@F7 = '" + f7 + "', " +
                                "@F8 = '" + f8 + "', " +
                                "@F9 = '" + f9 + "', " +
                                "@F10 = '" + f10 + "', " +
                                "@F11 = '" + f11 + "', " +
                                "@F12 = '" + f12 + "', " +
                                "@F13 = '" + f13 + "', " +
                                "@F14 = '" + f14 + "', " +
                                "@F15 = '" + f15 + "', " +
                                "@F16 = '" + f16 + "', " +
                                "@F17 = '" + f17 + "', " +
                                "@F18 = '" + f18 + "', " +
                                "@F19 = '" + f19 + "', " +
                                "@F20 = '" + f20 + "', " +
                                "@TABLENAME = '" + LB_CODE.Text + "', " +
                                "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                        conn2.ExecuteQuery();

                        if (conn2.GetFieldValue(0, "STATUS").ToString().Contains("ERROR"))
                        {
                            LB_DGR_ERR.Text = conn.GetFieldValue(0, "STATUS").ToString();
                            break;
                        }
                    }

                }

                FillDGR();
                FillDGRQUERY();
                ClearDGRUPLOAD();
            }
        }

        protected void BT_DEL_ALL_Click(object sender, EventArgs e)
        {
            //ClientScript.RegisterStartupScript(this.GetType(), "uncheckHeader", "uncheckHeader();", true);
            //Session["CHKAPPROVEALL"] = false;

            if (Session["DTDGR"] != null)
            {
                DataTable dta = (DataTable)Session["DTDGR"];

                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_SCHEMA] " +
                                           "@NAME = '" + LB_CODE.Text + "'";

                conn.ExecuteQuery();

                string f1 = "", f2 = "", f3 = "", f4 = "", f5 = "",
                        f6 = "", f7 = "", f8 = "", f9 = "", f10 = "",
                        f11 = "", f12 = "", f13 = "", f14 = "", f15 = "",
                        f16 = "", f17 = "", f18 = "", f19 = "", f20 = "";

                int a = dta.Rows.Count;
                int index = 0;
                foreach (DataRow row in dta.Rows)
                {
                    if (Convert.ToBoolean(row["CHECKED"].ToString()) == true)
                    {
                        f1 = row[0].ToString();
                        f2 = row[1].ToString();
                        f3 = row[2].ToString();
                        f4 = row[3].ToString();
                        f5 = row[4].ToString();
                        f6 = row[5].ToString();
                        f7 = row[6].ToString();
                        f8 = row[7].ToString();
                        f9 = row[8].ToString();
                        f10 = row[9].ToString();
                        f11 = row[10].ToString();
                        f12 = row[11].ToString();
                        f13 = row[12].ToString();
                        f14 = row[13].ToString();
                        f15 = row[14].ToString();
                        f16 = row[15].ToString();
                        f17 = row[16].ToString();
                        f18 = row[17].ToString();
                        f19 = row[18].ToString();
                        f20 = row[19].ToString();

                        conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_DELETE] " +
                                "@F1 = '" + f1 + "', " +
                                "@F2 = '" + f2 + "', " +
                                "@F3 = '" + f3 + "', " +
                                "@F4 = '" + f4 + "', " +
                                "@F5 = '" + f5 + "', " +
                                "@F6 = '" + f6 + "', " +
                                "@F7 = '" + f7 + "', " +
                                "@F8 = '" + f8 + "', " +
                                "@F9 = '" + f9 + "', " +
                                "@F10 = '" + f10 + "', " +
                                "@F11 = '" + f11 + "', " +
                                "@F12 = '" + f12 + "', " +
                                "@F13 = '" + f13 + "', " +
                                "@F14 = '" + f14 + "', " +
                                "@F15 = '" + f15 + "', " +
                                "@F16 = '" + f16 + "', " +
                                "@F17 = '" + f17 + "', " +
                                "@F18 = '" + f18 + "', " +
                                "@F19 = '" + f19 + "', " +
                                "@F20 = '" + f20 + "', " +
                                "@TABLENAME = '" + LB_CODE.Text + "'";

                        conn.ExecuteQuery();
                        index++;
                    }

                }

                int b = index;
                FillDGR();
                FillDGRQUERY();
                ClearDGRUPLOAD();
            }
        }

        protected void BT_DEL_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('PNL_POPUP').style.display = 'none';", true);

            conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_SCHEMA] " +
                                           "@NAME = '" + LB_CODE.Text + "'";

            conn.ExecuteQuery();

            string f1 = "", f2 = "", f3 = "", f4 = "", f5 = "",
                    f6 = "", f7 = "", f8 = "", f9 = "", f10 = "",
                    f11 = "", f12 = "", f13 = "", f14 = "", f15 = "",
                    f16 = "", f17 = "", f18 = "", f19 = "", f20 = "";

            string value = "";

            foreach (RepeaterItem item in Repeater1.Items)
            {
                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    if (conn.GetFieldValue(i, "is_primary_key").ToString() != "")
                    {
                        HiddenField hdn = (HiddenField)item.FindControl("HDN_F" + (i + 1).ToString());

                        value = hdn.Value.Trim();

                        if (i == 0)
                        {
                            f1 = value;
                        }
                        if (i == 1)
                        {
                            f2 = value;
                        }
                        if (i == 2)
                        {
                            f3 = value;
                        }
                        if (i == 3)
                        {
                            f4 = value;
                        }
                        if (i == 4)
                        {
                            f5 = value;
                        }
                        if (i == 5)
                        {
                            f6 = value;
                        }
                        if (i == 6)
                        {
                            f7 = value;
                        }
                        if (i == 7)
                        {
                            f8 = value;
                        }
                        if (i == 8)
                        {
                            f9 = value;
                        }
                        if (i == 9)
                        {
                            f10 = value;
                        }
                        if (i == 10)
                        {
                            f11 = value;
                        }
                        if (i == 11)
                        {
                            f12 = value;
                        }
                        if (i == 12)
                        {
                            f13 = value;
                        }
                        if (i == 13)
                        {
                            f14 = value;
                        }
                        if (i == 14)
                        {
                            f15 = value;
                        }
                        if (i == 15)
                        {
                            f16 = value;
                        }
                        if (i == 16)
                        {
                            f17 = value;
                        }
                        if (i == 17)
                        {
                            f18 = value;
                        }
                        if (i == 18)
                        {
                            f19 = value;
                        }
                        if (i == 19)
                        {
                            f20 = value;
                        }
                    }
                }

                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_DELETE] " +
                                "@F1 = '" + f1 + "', " +
                                "@F2 = '" + f2 + "', " +
                                "@F3 = '" + f3 + "', " +
                                "@F4 = '" + f4 + "', " +
                                "@F5 = '" + f5 + "', " +
                                "@F6 = '" + f6 + "', " +
                                "@F7 = '" + f7 + "', " +
                                "@F8 = '" + f8 + "', " +
                                "@F9 = '" + f9 + "', " +
                                "@F10 = '" + f10 + "', " +
                                "@F11 = '" + f11 + "', " +
                                "@F12 = '" + f12 + "', " +
                                "@F13 = '" + f13 + "', " +
                                "@F14 = '" + f14 + "', " +
                                "@F15 = '" + f15 + "', " +
                                "@F16 = '" + f16 + "', " +
                                "@F17 = '" + f17 + "', " +
                                "@F18 = '" + f18 + "', " +
                                "@F19 = '" + f19 + "', " +
                                "@F20 = '" + f20 + "', " +
                                "@TABLENAME = '" + LB_CODE.Text + "'";

                conn.ExecuteQuery();

                FillDGR();
                FillDGRQUERY();
                ClearDGRUPLOAD();
            }


            Repeater1.DataSource = null;
            Repeater1.DataBind();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            string f1 = "", f2 = "", f3 = "", f4 = "", f5 = "",
                    f6 = "", f7 = "", f8 = "", f9 = "", f10 = "",
                    f11 = "", f12 = "", f13 = "", f14 = "", f15 = "",
                    f16 = "", f17 = "", f18 = "", f19 = "", f20 = "";

            conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_SCHEMA] " +
                                   "@NAME = '" + LB_CODE.Text + "'";

            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                if (conn.GetFieldValue(i, "name").ToString() == "STATUSTYPE" || conn.GetFieldValue(i, "name").ToString() == "CHANGEDBY")
                {
                    continue;
                }

                foreach (RepeaterItem item in Repeater1.Items)
                {
                    string value = "";

                    if (conn.GetFieldValue(i, "is_primary_key").ToString() != "")
                    {
                        HiddenField hdn = (HiddenField)item.FindControl("HDN_F" + (i + 1).ToString());
                        value = hdn.Value.Trim();
                    }
                    else if (conn.GetFieldValue(i, "referenced_table_name").ToString() != "")
                    {
                        DropDownList ddl = (DropDownList)item.FindControl("DDL_F" + (i + 1).ToString());
                        value = ddl.SelectedValue.Trim();
                    }
                    else
                    {
                        TextBox txt = (TextBox)item.FindControl("TXT_F" + (i + 1).ToString());
                        value = txt.Text.Trim();
                    }

                    if (i == 0)
                    {
                        f1 = value;
                    }
                    if (i == 1)
                    {
                        f2 = value;
                    }
                    if (i == 2)
                    {
                        f3 = value;
                    }
                    if (i == 3)
                    {
                        f4 = value;
                    }
                    if (i == 4)
                    {
                        f5 = value;
                    }
                    if (i == 5)
                    {
                        f6 = value;
                    }
                    if (i == 6)
                    {
                        f7 = value;
                    }
                    if (i == 7)
                    {
                        f8 = value;
                    }
                    if (i == 8)
                    {
                        f9 = value;
                    }
                    if (i == 9)
                    {
                        f10 = value;
                    }
                    if (i == 10)
                    {
                        f11 = value;
                    }
                    if (i == 11)
                    {
                        f12 = value;
                    }
                    if (i == 12)
                    {
                        f13 = value;
                    }
                    if (i == 13)
                    {
                        f14 = value;
                    }
                    if (i == 14)
                    {
                        f15 = value;
                    }
                    if (i == 15)
                    {
                        f16 = value;
                    }
                    if (i == 16)
                    {
                        f17 = value;
                    }
                    if (i == 17)
                    {
                        f18 = value;
                    }
                    if (i == 18)
                    {
                        f19 = value;
                    }
                    if (i == 19)
                    {
                        f20 = value;
                    }
                }
            }

            conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_UPSERT] " +
                                "@F1 = '" + f1 + "', " +
                                "@F2 = '" + f2 + "', " +
                                "@F3 = '" + f3 + "', " +
                                "@F4 = '" + f4 + "', " +
                                "@F5 = '" + f5 + "', " +
                                "@F6 = '" + f6 + "', " +
                                "@F7 = '" + f7 + "', " +
                                "@F8 = '" + f8 + "', " +
                                "@F9 = '" + f9 + "', " +
                                "@F10 = '" + f10 + "', " +
                                "@F11 = '" + f11 + "', " +
                                "@F12 = '" + f12 + "', " +
                                "@F13 = '" + f13 + "', " +
                                "@F14 = '" + f14 + "', " +
                                "@F15 = '" + f15 + "', " +
                                "@F16 = '" + f16 + "', " +
                                "@F17 = '" + f17 + "', " +
                                "@F18 = '" + f18 + "', " +
                                "@F19 = '" + f19 + "', " +
                                "@F20 = '" + f20 + "', " +
                                "@TABLENAME = '" + LB_CODE.Text + "', " +
                                "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

            conn.ExecuteQuery();

            if (conn.GetFieldValue(0, "STATUS").ToString().Contains("ERROR"))
            {
                LB_DGR_ERR.Text = conn.GetFieldValue(0, "STATUS").ToString();
            }


            FillDGR();
            FillDGRQUERY();
            ClearDGRUPLOAD();

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('PNL_POPUP').style.display = 'none';", true);

            Repeater1.DataSource = null;
            Repeater1.DataBind();
        }

        protected void Process(string FullPath)
        {
            string fileExtension = System.IO.Path.GetExtension(FullPath);

            string connstr = "";

            string folderPath = Path.GetDirectoryName(FullPath);
            string fileName = Path.GetFileName(FullPath);

            int records = 0;

            DataTable dta = new DataTable();
            dta.Columns.Add("F1");
            dta.Columns.Add("F2");
            dta.Columns.Add("F3");
            dta.Columns.Add("F4");
            dta.Columns.Add("F5");
            dta.Columns.Add("F6");
            dta.Columns.Add("F7");
            dta.Columns.Add("F8");
            dta.Columns.Add("F9");
            dta.Columns.Add("F10");
            dta.Columns.Add("F11");
            dta.Columns.Add("F12");
            dta.Columns.Add("F13");
            dta.Columns.Add("F14");
            dta.Columns.Add("F15");
            dta.Columns.Add("F16");
            dta.Columns.Add("F17");
            dta.Columns.Add("F18");
            dta.Columns.Add("F19");
            dta.Columns.Add("F20");
            dta.Columns.Add("STATUS");

            int minNumCol = dta.Columns.Count;

            List<string> columns = new List<string>();

            conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_SCHEMA] " +
                                                    "@NAME = '" + LB_CODE.Text + "'";

            conn.ExecuteQuery();

            DataTable tblcols = conn.GetDataTable();

            if (fileExtension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
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

                                if (fields.Length == 0)
                                {
                                    parser.SetDelimiters(";");
                                    fields = parser.ReadFields();

                                    if (fields.Length == 0)
                                    {
                                        continue;
                                    }
                                }

                                string f1 = "", f2 = "", f3 = "", f4 = "", f5 = "",
                                    f6 = "", f7 = "", f8 = "", f9 = "", f10 = "",
                                    f11 = "", f12 = "", f13 = "", f14 = "", f15 = "",
                                    f16 = "", f17 = "", f18 = "", f19 = "", f20 = "";



                                for (int i = 0; i < tblcols.Rows.Count; i++)
                                {
                                    DataRow tblrow = tblcols.Rows[i];

                                    if (i == 0)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f1 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f1 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f1 = "0";
                                        }
                                    }
                                    if (i == 1)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f2 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f2 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f2 = "0";
                                        }
                                    }
                                    if (i == 2)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f3 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f3 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f3 = "0";
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f4 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f4 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f4 = "0";
                                        }
                                    }
                                    if (i == 4)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f5 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f5 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f5 = "0";
                                        }
                                    }
                                    if (i == 5)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f6 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f6 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f6 = "0";
                                        }
                                    }
                                    if (i == 6)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f7 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f7 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f7 = "0";
                                        }
                                    }
                                    if (i == 7)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f8 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f8 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f8 = "0";
                                        }
                                    }
                                    if (i == 8)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f9 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f9 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f9 = "0";
                                        }
                                    }
                                    if (i == 9)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f10 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f10 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f10 = "0";
                                        }
                                    }
                                    if (i == 10)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f11 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f11 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f11 = "0";
                                        }
                                    }
                                    if (i == 11)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f12 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f12 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f12 = "0";
                                        }
                                    }

                                    if (i == 12)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f13 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f13 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f13 = "0";
                                        }
                                    }
                                    if (i == 13)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f14 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f14 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f14 = "0";
                                        }
                                    }
                                    if (i == 14)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f15 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f15 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f15 = "0";
                                        }
                                    }
                                    if (i == 15)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f16 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f16 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f16 = "0";
                                        }
                                    }
                                    if (i == 16)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f17 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f17 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f17 = "0";
                                        }
                                    }
                                    if (i == 17)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f18 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f18 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f18 = "0";
                                        }
                                    }
                                    if (i == 18)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f19 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f19 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f19 = "0";
                                        }
                                    }
                                    if (i == 19)
                                    {
                                        if (i >= 0 && i < fields.Length)
                                        {
                                            if (!string.IsNullOrEmpty(fields[i].ToString()) && fields[i].ToString().StartsWith("'"))
                                            {
                                                f20 = fields[i].ToString().Substring(1);
                                            }
                                            else
                                            {
                                                f20 = fields[i].ToString();
                                            }
                                        }
                                        if (tblrow["name"].ToString() == "STATUSTYPE")
                                        {
                                            f20 = "0";
                                        }
                                    }
                                }

                                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_UPSERT] " +
                                                    "@F1 = '" + f1 + "', " +
                                                    "@F2 = '" + f2 + "', " +
                                                    "@F3 = '" + f3 + "', " +
                                                    "@F4 = '" + f4 + "', " +
                                                    "@F5 = '" + f5 + "', " +
                                                    "@F6 = '" + f6 + "', " +
                                                    "@F7 = '" + f7 + "', " +
                                                    "@F8 = '" + f8 + "', " +
                                                    "@F9 = '" + f9 + "', " +
                                                    "@F10 = '" + f10 + "', " +
                                                    "@F11 = '" + f11 + "', " +
                                                    "@F12 = '" + f12 + "', " +
                                                    "@F13 = '" + f13 + "', " +
                                                    "@F14 = '" + f14 + "', " +
                                                    "@F15 = '" + f15 + "', " +
                                                    "@F16 = '" + f16 + "', " +
                                                    "@F17 = '" + f17 + "', " +
                                                    "@F18 = '" + f18 + "', " +
                                                    "@F19 = '" + f19 + "', " +
                                                    "@F20 = '" + f20 + "', " +
                                                    "@TABLENAME = '" + LB_CODE.Text + "', " +
                                                    "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                                conn.ExecuteQuery();

                                DataRow newRow = dta.NewRow();
                                newRow["F1"] = conn.GetFieldValue(0, "F1").ToString();
                                newRow["F2"] = conn.GetFieldValue(0, "F2").ToString();
                                newRow["F3"] = conn.GetFieldValue(0, "F3").ToString();
                                newRow["F4"] = conn.GetFieldValue(0, "F4").ToString();
                                newRow["F5"] = conn.GetFieldValue(0, "F5").ToString();
                                newRow["F6"] = conn.GetFieldValue(0, "F6").ToString();
                                newRow["F7"] = conn.GetFieldValue(0, "F7").ToString();
                                newRow["F8"] = conn.GetFieldValue(0, "F8").ToString();
                                newRow["F9"] = conn.GetFieldValue(0, "F9").ToString();
                                newRow["F10"] = conn.GetFieldValue(0, "F10").ToString();
                                newRow["F11"] = conn.GetFieldValue(0, "F11").ToString();
                                newRow["F12"] = conn.GetFieldValue(0, "F12").ToString();
                                newRow["F13"] = conn.GetFieldValue(0, "F13").ToString();
                                newRow["F14"] = conn.GetFieldValue(0, "F14").ToString();
                                newRow["F15"] = conn.GetFieldValue(0, "F15").ToString();
                                newRow["F16"] = conn.GetFieldValue(0, "F16").ToString();
                                newRow["F17"] = conn.GetFieldValue(0, "F17").ToString();
                                newRow["F18"] = conn.GetFieldValue(0, "F18").ToString();
                                newRow["F19"] = conn.GetFieldValue(0, "F19").ToString();
                                newRow["F20"] = conn.GetFieldValue(0, "F20").ToString();
                                newRow["STATUS"] = conn.GetFieldValue(0, "STATUS").ToString();

                                dta.Rows.Add(newRow);
                                records++;

                            }

                        }
                    }

                    LB_RECORD.Text = records + " records";

                    Session["DTDGRUPLOAD"] = dta;

                    ReloadDGRUPLOAD();
                    FillDGR();
                    FillDGRQUERY();

                    if (File.Exists(FullPath))
                        File.Delete(FullPath);

                    ClientScript.RegisterStartupScript(this.GetType(), "setGridWidth", "setGridWidth();", true);


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

                using (OleDbConnection con = new OleDbConnection(connstr))
                {
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

                                using (OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheetName + "]", con))
                                {
                                    DataTable dt = new DataTable();
                                    da.Fill(dt);

                                    foreach (DataRow myRow in dt.Rows)
                                    {
                                        string f1 = "", f2 = "", f3 = "", f4 = "", f5 = "",
                                        f6 = "", f7 = "", f8 = "", f9 = "", f10 = "",
                                        f11 = "", f12 = "", f13 = "", f14 = "", f15 = "",
                                        f16 = "", f17 = "", f18 = "", f19 = "", f20 = "";

                                        for (int i = 0; i < tblcols.Rows.Count; i++)
                                        {
                                            DataRow tblrow = tblcols.Rows[i];

                                            if (i == 0)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f1 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f1 = myRow[i].ToString();
                                                    }
                                                }

                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f1 = "0";
                                                }
                                            }
                                            if (i == 1)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f2 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f2 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f2 = "0";
                                                }
                                            }
                                            if (i == 2)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f3 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f3 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f3 = "0";
                                                }
                                            }
                                            if (i == 3)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f4 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f4 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f4 = "0";
                                                }
                                            }
                                            if (i == 4)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f5 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f5 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f5 = "0";
                                                }
                                            }
                                            if (i == 5)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f6 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f6 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f6 = "0";
                                                }
                                            }
                                            if (i == 6)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f7 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f7 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f7 = "0";
                                                }
                                            }
                                            if (i == 7)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f8 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f8 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f8 = "0";
                                                }
                                            }
                                            if (i == 8)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f9 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f9 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f9 = "0";
                                                }
                                            }
                                            if (i == 9)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f10 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f10 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f10 = "0";
                                                }
                                            }
                                            if (i == 10)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f11 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f11 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f11 = "0";
                                                }
                                            }
                                            if (i == 11)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f12 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f12 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f12 = "0";
                                                }

                                            }

                                            if (i == 12)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f13 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f13 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f13 = "0";
                                                }
                                            }
                                            if (i == 13)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f14 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f14 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f14 = "0";
                                                }
                                            }
                                            if (i == 14)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f15 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f15 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f15 = "0";
                                                }
                                            }
                                            if (i == 15)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f16 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f16 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f16 = "0";
                                                }
                                            }
                                            if (i == 16)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f17 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f17 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f17 = "0";
                                                }
                                            }
                                            if (i == 17)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f18 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f18 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f18 = "0";
                                                }
                                            }
                                            if (i == 18)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f19 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f19 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f19 = "0";
                                                }
                                            }
                                            if (i == 19)
                                            {
                                                if (i < dt.Columns.Count)
                                                {
                                                    if (!string.IsNullOrEmpty(myRow[i].ToString()) && myRow[i].ToString().StartsWith("'"))
                                                    {
                                                        f20 = myRow[i].ToString().Substring(1);
                                                    }
                                                    else
                                                    {
                                                        f20 = myRow[i].ToString();
                                                    }
                                                }
                                                if (tblrow["name"].ToString() == "STATUSTYPE")
                                                {
                                                    f20 = "0";
                                                }
                                            }
                                        }

                                        conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_UPSERT] " +
                                                    "@F1 = '" + f1 + "', " +
                                                    "@F2 = '" + f2 + "', " +
                                                    "@F3 = '" + f3 + "', " +
                                                    "@F4 = '" + f4 + "', " +
                                                    "@F5 = '" + f5 + "', " +
                                                    "@F6 = '" + f6 + "', " +
                                                    "@F7 = '" + f7 + "', " +
                                                    "@F8 = '" + f8 + "', " +
                                                    "@F9 = '" + f9 + "', " +
                                                    "@F10 = '" + f10 + "', " +
                                                    "@F11 = '" + f11 + "', " +
                                                    "@F12 = '" + f12 + "', " +
                                                    "@F13 = '" + f13 + "', " +
                                                    "@F14 = '" + f14 + "', " +
                                                    "@F15 = '" + f15 + "', " +
                                                    "@F16 = '" + f16 + "', " +
                                                    "@F17 = '" + f17 + "', " +
                                                    "@F18 = '" + f18 + "', " +
                                                    "@F19 = '" + f19 + "', " +
                                                    "@F20 = '" + f20 + "', " +
                                                    "@TABLENAME = '" + LB_CODE.Text + "', " +
                                                    "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                                        conn.ExecuteQuery();

                                        DataRow newRow = dta.NewRow();
                                        newRow["F1"] = conn.GetFieldValue(0, "F1").ToString();
                                        newRow["F2"] = conn.GetFieldValue(0, "F2").ToString();
                                        newRow["F3"] = conn.GetFieldValue(0, "F3").ToString();
                                        newRow["F4"] = conn.GetFieldValue(0, "F4").ToString();
                                        newRow["F5"] = conn.GetFieldValue(0, "F5").ToString();
                                        newRow["F6"] = conn.GetFieldValue(0, "F6").ToString();
                                        newRow["F7"] = conn.GetFieldValue(0, "F7").ToString();
                                        newRow["F8"] = conn.GetFieldValue(0, "F8").ToString();
                                        newRow["F9"] = conn.GetFieldValue(0, "F9").ToString();
                                        newRow["F10"] = conn.GetFieldValue(0, "F10").ToString();
                                        newRow["F11"] = conn.GetFieldValue(0, "F11").ToString();
                                        newRow["F12"] = conn.GetFieldValue(0, "F12").ToString();
                                        newRow["F13"] = conn.GetFieldValue(0, "F13").ToString();
                                        newRow["F14"] = conn.GetFieldValue(0, "F14").ToString();
                                        newRow["F15"] = conn.GetFieldValue(0, "F15").ToString();
                                        newRow["F16"] = conn.GetFieldValue(0, "F16").ToString();
                                        newRow["F17"] = conn.GetFieldValue(0, "F17").ToString();
                                        newRow["F18"] = conn.GetFieldValue(0, "F18").ToString();
                                        newRow["F19"] = conn.GetFieldValue(0, "F19").ToString();
                                        newRow["F20"] = conn.GetFieldValue(0, "F20").ToString();
                                        newRow["STATUS"] = conn.GetFieldValue(0, "STATUS").ToString();

                                        dta.Rows.Add(newRow);
                                        records++;

                                    }
                                }
                            }
                        }

                        con.Close();

                        LB_RECORD.Text = records + " records";

                        Session["DTDGRUPLOAD"] = dta;

                        ReloadDGRUPLOAD();
                        FillDGR();
                        FillDGRQUERY();

                        if (File.Exists(FullPath))
                            File.Delete(FullPath);

                        ClientScript.RegisterStartupScript(this.GetType(), "setGridWidth", "setGridWidth();", true);

                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = ex.Message;
                        con.Close();
                        if (File.Exists(FullPath))
                            File.Delete(FullPath);
                    }
                }

            }

        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            Session["DGRINDEX"] = e.NewPageIndex;
            DGR.CurrentPageIndex = e.NewPageIndex;
            ReloadDGR();
            ClearDGRUPLOAD();
            FillDGRQUERY();
        }

        protected void DGRUPLOAD_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGRUPLOAD.CurrentPageIndex = e.NewPageIndex;
            ReloadDGRUPLOAD();
            FillDGR();
            FillDGRQUERY();
        }

        protected void DGRQUERY_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            Session["DGRQUERYINDEX"] = e.NewPageIndex;
            DGRQUERY.CurrentPageIndex = e.NewPageIndex;
            FillDGRQUERY();
            ReloadDGR();
            ClearDGRUPLOAD();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                string f1 = e.Item.Cells[0].Text.Trim().Replace("&nbsp;", ""),
                    f2 = e.Item.Cells[1].Text.Trim().Replace("&nbsp;", ""),
                    f3 = e.Item.Cells[2].Text.Trim().Replace("&nbsp;", ""),
                    f4 = e.Item.Cells[3].Text.Trim().Replace("&nbsp;", ""),
                    f5 = e.Item.Cells[4].Text.Trim().Replace("&nbsp;", ""),
                    f6 = e.Item.Cells[5].Text.Trim().Replace("&nbsp;", ""),
                    f7 = e.Item.Cells[6].Text.Trim().Replace("&nbsp;", ""),
                    f8 = e.Item.Cells[7].Text.Trim().Replace("&nbsp;", ""),
                    f9 = e.Item.Cells[8].Text.Trim().Replace("&nbsp;", ""),
                    f10 = e.Item.Cells[9].Text.Trim().Replace("&nbsp;", ""),
                    f11 = e.Item.Cells[10].Text.Trim().Replace("&nbsp;", ""),
                    f12 = e.Item.Cells[11].Text.Trim().Replace("&nbsp;", ""),
                    f13 = e.Item.Cells[12].Text.Trim().Replace("&nbsp;", ""),
                    f14 = e.Item.Cells[13].Text.Trim().Replace("&nbsp;", ""),
                    f15 = e.Item.Cells[14].Text.Trim().Replace("&nbsp;", ""),
                    f16 = e.Item.Cells[15].Text.Trim().Replace("&nbsp;", ""),
                    f17 = e.Item.Cells[16].Text.Trim().Replace("&nbsp;", ""),
                    f18 = e.Item.Cells[17].Text.Trim().Replace("&nbsp;", ""),
                    f19 = e.Item.Cells[18].Text.Trim().Replace("&nbsp;", ""),
                    f20 = e.Item.Cells[19].Text.Trim().Replace("&nbsp;", "");

                conn.QueryString = "exec SP_TABLE_LOAD " +
                                    "@F1 = '" + f1 + "', " +
                                    "@F2 = '" + f2 + "', " +
                                    "@F3 = '" + f3 + "', " +
                                    "@F4 = '" + f4 + "', " +
                                    "@F5 = '" + f5 + "', " +
                                    "@F6 = '" + f6 + "', " +
                                    "@F7 = '" + f7 + "', " +
                                    "@F8 = '" + f8 + "', " +
                                    "@F9 = '" + f9 + "', " +
                                    "@F10 = '" + f10 + "', " +
                                    "@F11 = '" + f11 + "', " +
                                    "@F12 = '" + f12 + "', " +
                                    "@F13 = '" + f13 + "', " +
                                    "@F14 = '" + f14 + "', " +
                                    "@F15 = '" + f15 + "', " +
                                    "@F16 = '" + f16 + "', " +
                                    "@F17 = '" + f17 + "', " +
                                    "@F18 = '" + f18 + "', " +
                                    "@F19 = '" + f19 + "', " +
                                    "@F20 = '" + f20 + "', " +
                                    "@TABLENAME = '" + LB_CODE.Text + "'";

                conn.ExecuteQuery();
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable();

                Repeater1.DataSource = dt;
                Repeater1.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('PNL_POPUP').style.display = 'block';", true);

                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_SCHEMA] " +
                                   "@NAME = '" + LB_CODE.Text + "'";

                conn.ExecuteQuery();

                Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    if (conn.GetFieldValue(i, "name").ToString() == "STATUSTYPE" || conn.GetFieldValue(i, "name").ToString() == "CHANGEDBY")
                    {
                        continue;
                    }

                    foreach (RepeaterItem item in Repeater1.Items)
                    {
                        HtmlTableRow tr = (HtmlTableRow)item.FindControl("TR_F" + (i + 1).ToString());
                        tr.Visible = true;

                        if (i == 0)
                        {
                            tr = (HtmlTableRow)item.FindControl("TR_F" + (20 + 1).ToString());
                            tr.Visible = true;

                            Button save = (Button)item.FindControl("BT_SAVE");
                            Button del = (Button)item.FindControl("BT_DEL");
                            save.Visible = true;
                            del.Visible = true;
                        }

                        if (conn.GetFieldValue(i, "referenced_table_name").ToString() != "")
                        {
                            Label lbl = (Label)item.FindControl("LB_F" + (i + 1).ToString());
                            lbl.Visible = true;
                            lbl.Text = conn.GetFieldValue(i, "name").ToString();

                            conn2.QueryString = "select * from " + conn.GetFieldValue(i, "referenced_table_name").ToString();
                            conn2.ExecuteQuery();

                            DropDownList ddl = (DropDownList)item.FindControl("DDL_F" + (i + 1).ToString());
                            ddl.Visible = true;
                            if (conn.GetFieldValue(i, "is_primary_key").ToString() == "")
                                if (conn.GetFieldValue(i, "is_nullable").ToString() == "1")
                                    ddl.Items.Add(new ListItem("", ""));

                            if (conn.GetFieldValue(i, "is_primary_key").ToString() == "")
                            {
                                for (int k = 0; k < conn2.GetRowCount(); k++)
                                {
                                    ddl.Items.Add(new ListItem(conn2.GetFieldValue(k, 1).ToString(), conn2.GetFieldValue(k, 0).ToString()));
                                }
                            }
                            else
                            {
                                for (int k = 0; k < conn2.GetRowCount(); k++)
                                {
                                    if (conn2.GetFieldValue(k, 0).ToString() == dt.Rows[0][i].ToString())
                                    {
                                        ddl.Items.Add(new ListItem(conn2.GetFieldValue(k, 1).ToString(), conn2.GetFieldValue(k, 0).ToString()));
                                        break;
                                    }
                                }
                            }

                            ddl.SelectedValue = dt.Rows[0][i].ToString();

                        }
                        else
                        {
                            Label lbl = (Label)item.FindControl("LB_F" + (i + 1).ToString());
                            lbl.Visible = true;
                            lbl.Text = conn.GetFieldValue(i, "name").ToString();

                            TextBox txt = (TextBox)item.FindControl("TXT_F" + (i + 1).ToString());
                            txt.Visible = true;
                            txt.Text = dt.Rows[0][i].ToString();

                        }

                        HiddenField hdn = (HiddenField)item.FindControl("HDN_F" + (i + 1).ToString());
                        hdn.Value = dt.Rows[0][i].ToString();
                    }
                }

                FillDGR();
                FillDGRQUERY();
                ClearDGRUPLOAD();

            }

            if (e.CommandName == "Delete")
            {
                string f1 = e.Item.Cells[0].Text.Trim().Replace("&nbsp;", ""),
                        f2 = e.Item.Cells[1].Text.Trim().Replace("&nbsp;", ""),
                        f3 = e.Item.Cells[2].Text.Trim().Replace("&nbsp;", ""),
                        f4 = e.Item.Cells[3].Text.Trim().Replace("&nbsp;", ""),
                        f5 = e.Item.Cells[4].Text.Trim().Replace("&nbsp;", ""),
                        f6 = e.Item.Cells[5].Text.Trim().Replace("&nbsp;", ""),
                        f7 = e.Item.Cells[6].Text.Trim().Replace("&nbsp;", ""),
                        f8 = e.Item.Cells[7].Text.Trim().Replace("&nbsp;", ""),
                        f9 = e.Item.Cells[8].Text.Trim().Replace("&nbsp;", ""),
                        f10 = e.Item.Cells[9].Text.Trim().Replace("&nbsp;", ""),
                        f11 = e.Item.Cells[10].Text.Trim().Replace("&nbsp;", ""),
                        f12 = e.Item.Cells[11].Text.Trim().Replace("&nbsp;", ""),
                        f13 = e.Item.Cells[12].Text.Trim().Replace("&nbsp;", ""),
                        f14 = e.Item.Cells[13].Text.Trim().Replace("&nbsp;", ""),
                        f15 = e.Item.Cells[14].Text.Trim().Replace("&nbsp;", ""),
                        f16 = e.Item.Cells[15].Text.Trim().Replace("&nbsp;", ""),
                        f17 = e.Item.Cells[16].Text.Trim().Replace("&nbsp;", ""),
                        f18 = e.Item.Cells[17].Text.Trim().Replace("&nbsp;", ""),
                        f19 = e.Item.Cells[18].Text.Trim().Replace("&nbsp;", ""),
                        f20 = e.Item.Cells[19].Text.Trim().Replace("&nbsp;", "");

                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_DELETE] " +
                                "@F1 = '" + f1 + "', " +
                                "@F2 = '" + f2 + "', " +
                                "@F3 = '" + f3 + "', " +
                                "@F4 = '" + f4 + "', " +
                                "@F5 = '" + f5 + "', " +
                                "@F6 = '" + f6 + "', " +
                                "@F7 = '" + f7 + "', " +
                                "@F8 = '" + f8 + "', " +
                                "@F9 = '" + f9 + "', " +
                                "@F10 = '" + f10 + "', " +
                                "@F11 = '" + f11 + "', " +
                                "@F12 = '" + f12 + "', " +
                                "@F13 = '" + f13 + "', " +
                                "@F14 = '" + f14 + "', " +
                                "@F15 = '" + f15 + "', " +
                                "@F16 = '" + f16 + "', " +
                                "@F17 = '" + f17 + "', " +
                                "@F18 = '" + f18 + "', " +
                                "@F19 = '" + f19 + "', " +
                                "@F20 = '" + f20 + "', " +
                                "@TABLENAME = '" + LB_CODE.Text + "'";

                conn.ExecuteQuery();

                FillDGR();
                FillDGRQUERY();
                ClearDGRUPLOAD();

            }
        }

        protected void DGRQUERY_ItemCreated(object sender, DataGridItemEventArgs e)
        {

        }

        protected void DGRQUERY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                string f1 = "", f2 = "", f3 = "", f4 = "", f5 = "",
                        f6 = "", f7 = "", f8 = "", f9 = "", f10 = "",
                        f11 = "", f12 = "", f13 = "", f14 = "", f15 = "",
                        f16 = "", f17 = "", f18 = "", f19 = "", f20 = "";

                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_SCHEMA] " +
                                   "@NAME = '" + LB_CODE.Text + "'";

                conn.ExecuteQuery();

                for (int i = 0; i < conn.GetRowCount(); i++)
                {
                    if (conn.GetFieldValue(i, "name").ToString() == "STATUSTYPE" || conn.GetFieldValue(i, "name").ToString() == "CHANGEDBY")
                    {
                        continue;
                    }

                    if (conn.GetFieldValue(i, "is_primary_key").ToString() != "")
                    {
                        if (i == 0)
                        {
                            f1 = e.Item.Cells[i].Text;
                        }
                        if (i == 1)
                        {
                            f2 = e.Item.Cells[i].Text;
                        }
                        if (i == 2)
                        {
                            f3 = e.Item.Cells[i].Text;
                        }
                        if (i == 3)
                        {
                            f4 = e.Item.Cells[i].Text;
                        }
                        if (i == 4)
                        {
                            f5 = e.Item.Cells[i].Text;
                        }
                        if (i == 5)
                        {
                            f6 = e.Item.Cells[i].Text;
                        }
                        if (i == 6)
                        {
                            f7 = e.Item.Cells[i].Text;
                        }
                        if (i == 7)
                        {
                            f8 = e.Item.Cells[i].Text;
                        }
                        if (i == 8)
                        {
                            f9 = e.Item.Cells[i].Text;
                        }
                        if (i == 9)
                        {
                            f10 = e.Item.Cells[i].Text;
                        }
                        if (i == 10)
                        {
                            f11 = e.Item.Cells[i].Text;
                        }
                        if (i == 11)
                        {
                            f12 = e.Item.Cells[i].Text;
                        }
                        if (i == 12)
                        {
                            f13 = e.Item.Cells[i].Text;
                        }
                        if (i == 13)
                        {
                            f14 = e.Item.Cells[i].Text;
                        }
                        if (i == 14)
                        {
                            f15 = e.Item.Cells[i].Text;
                        }
                        if (i == 15)
                        {
                            f16 = e.Item.Cells[i].Text;
                        }
                        if (i == 16)
                        {
                            f17 = e.Item.Cells[i].Text;
                        }
                        if (i == 17)
                        {
                            f18 = e.Item.Cells[i].Text;
                        }
                        if (i == 18)
                        {
                            f19 = e.Item.Cells[i].Text;
                        }
                        if (i == 19)
                        {
                            f20 = e.Item.Cells[i].Text;
                        }
                    }
                    else if (conn.GetFieldValue(i, "referenced_table_name").ToString() != "")
                    {
                        DropDownList ddl = (DropDownList)FindControlRecursive(e.Item, "DDL_VAL" + (i + 1).ToString());

                        if (i == 0)
                        {
                            f1 = ddl.SelectedValue;
                        }
                        if (i == 1)
                        {
                            f2 = ddl.SelectedValue;
                        }
                        if (i == 2)
                        {
                            f3 = ddl.SelectedValue;
                        }
                        if (i == 3)
                        {
                            f4 = ddl.SelectedValue;
                        }
                        if (i == 4)
                        {
                            f5 = ddl.SelectedValue;
                        }
                        if (i == 5)
                        {
                            f6 = ddl.SelectedValue;
                        }
                        if (i == 6)
                        {
                            f7 = ddl.SelectedValue;
                        }
                        if (i == 7)
                        {
                            f8 = ddl.SelectedValue;
                        }
                        if (i == 8)
                        {
                            f9 = ddl.SelectedValue;
                        }
                        if (i == 9)
                        {
                            f10 = ddl.SelectedValue;
                        }
                        if (i == 10)
                        {
                            f11 = ddl.SelectedValue;
                        }
                        if (i == 11)
                        {
                            f12 = ddl.SelectedValue;
                        }
                        if (i == 12)
                        {
                            f13 = ddl.SelectedValue;
                        }
                        if (i == 13)
                        {
                            f14 = ddl.SelectedValue;
                        }
                        if (i == 14)
                        {
                            f15 = ddl.SelectedValue;
                        }
                        if (i == 15)
                        {
                            f16 = ddl.SelectedValue;
                        }
                        if (i == 16)
                        {
                            f17 = ddl.SelectedValue;
                        }
                        if (i == 17)
                        {
                            f18 = ddl.SelectedValue;
                        }
                        if (i == 18)
                        {
                            f19 = ddl.SelectedValue;
                        }
                        if (i == 19)
                        {
                            f20 = ddl.SelectedValue;
                        }
                    }
                    else
                    {
                        TextBox txt = (TextBox)FindControlRecursive(e.Item, "TXT_VAL" + (i + 1).ToString());

                        if (i == 0)
                        {
                            f1 = txt.Text;
                        }
                        if (i == 1)
                        {
                            f2 = txt.Text;
                        }
                        if (i == 2)
                        {
                            f3 = txt.Text;
                        }
                        if (i == 3)
                        {
                            f4 = txt.Text;
                        }
                        if (i == 4)
                        {
                            f5 = txt.Text;
                        }
                        if (i == 5)
                        {
                            f6 = txt.Text;
                        }
                        if (i == 6)
                        {
                            f7 = txt.Text;
                        }
                        if (i == 7)
                        {
                            f8 = txt.Text;
                        }
                        if (i == 8)
                        {
                            f9 = txt.Text;
                        }
                        if (i == 9)
                        {
                            f10 = txt.Text;
                        }
                        if (i == 10)
                        {
                            f11 = txt.Text;
                        }
                        if (i == 11)
                        {
                            f12 = txt.Text;
                        }
                        if (i == 12)
                        {
                            f13 = txt.Text;
                        }
                        if (i == 13)
                        {
                            f14 = txt.Text;
                        }
                        if (i == 14)
                        {
                            f15 = txt.Text;
                        }
                        if (i == 15)
                        {
                            f16 = txt.Text;
                        }
                        if (i == 16)
                        {
                            f17 = txt.Text;
                        }
                        if (i == 17)
                        {
                            f18 = txt.Text;
                        }
                        if (i == 18)
                        {
                            f19 = txt.Text;
                        }
                        if (i == 19)
                        {
                            f20 = txt.Text;
                        }
                    }
                }

                conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_UPSERT] " +
                                        "@F1 = '" + f1 + "', " +
                                        "@F2 = '" + f2 + "', " +
                                        "@F3 = '" + f3 + "', " +
                                        "@F4 = '" + f4 + "', " +
                                        "@F5 = '" + f5 + "', " +
                                        "@F6 = '" + f6 + "', " +
                                        "@F7 = '" + f7 + "', " +
                                        "@F8 = '" + f8 + "', " +
                                        "@F9 = '" + f9 + "', " +
                                        "@F10 = '" + f10 + "', " +
                                        "@F11 = '" + f11 + "', " +
                                        "@F12 = '" + f12 + "', " +
                                        "@F13 = '" + f13 + "', " +
                                        "@F14 = '" + f14 + "', " +
                                        "@F15 = '" + f15 + "', " +
                                        "@F16 = '" + f16 + "', " +
                                        "@F17 = '" + f17 + "', " +
                                        "@F18 = '" + f18 + "', " +
                                        "@F19 = '" + f19 + "', " +
                                        "@F20 = '" + f20 + "', " +
                                        "@TABLENAME = '" + LB_CODE.Text + "', " +
                                        "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                conn.ExecuteQuery();

                if (conn.GetFieldValue(0, "STATUS").ToString().Contains("ERROR"))
                {
                    LB_DGRQUERY_ERR.Text = conn.GetFieldValue(0, "STATUS").ToString();
                }


                FillDGRQUERY();
                FillDGR();
                ClearDGRUPLOAD();
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    string f1 = "", f2 = "", f3 = "", f4 = "", f5 = "",
                        f6 = "", f7 = "", f8 = "", f9 = "", f10 = "",
                        f11 = "", f12 = "", f13 = "", f14 = "", f15 = "",
                        f16 = "", f17 = "", f18 = "", f19 = "", f20 = "";


                    conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_SCHEMA] " +
                                       "@NAME = '" + LB_CODE.Text + "'";

                    conn.ExecuteQuery();

                    string value = "";

                    for (int i = 0; i < conn.GetRowCount(); i++)
                    {
                        if (conn.GetFieldValue(i, "name").ToString() == "STATUSTYPE" || conn.GetFieldValue(i, "name").ToString() == "CHANGEDBY")
                        {
                            continue;
                        }

                        if (conn.GetFieldValue(i, "is_primary_key").ToString() != "")
                        {
                            Label lbl = (Label)FindControlRecursive(e.Item, "LB_F" + (i + 1).ToString());

                            value = lbl.Text;
                        }
                        else if (conn.GetFieldValue(i, "referenced_table_name").ToString() != "")
                        {
                            DropDownList ddl = (DropDownList)FindControlRecursive(e.Item, "DDL_VAL" + (i + 1).ToString());

                            value = ddl.SelectedValue;
                        }
                        else
                        {
                            TextBox txt = (TextBox)FindControlRecursive(e.Item, "TXT_VAL" + (i + 1).ToString());

                            value = txt.Text;
                        }


                        if (i == 0)
                        {
                            f1 = value;
                        }
                        if (i == 1)
                        {
                            f2 = value;
                        }
                        if (i == 2)
                        {
                            f3 = value;
                        }
                        if (i == 3)
                        {
                            f4 = value;
                        }
                        if (i == 4)
                        {
                            f5 = value;
                        }
                        if (i == 5)
                        {
                            f6 = value;
                        }
                        if (i == 6)
                        {
                            f7 = value;
                        }
                        if (i == 7)
                        {
                            f8 = value;
                        }
                        if (i == 8)
                        {
                            f9 = value;
                        }
                        if (i == 9)
                        {
                            f10 = value;
                        }
                        if (i == 10)
                        {
                            f11 = value;
                        }
                        if (i == 11)
                        {
                            f12 = value;
                        }
                        if (i == 12)
                        {
                            f13 = value;
                        }
                        if (i == 13)
                        {
                            f14 = value;
                        }
                        if (i == 14)
                        {
                            f15 = value;
                        }
                        if (i == 15)
                        {
                            f16 = value;
                        }
                        if (i == 16)
                        {
                            f17 = value;
                        }
                        if (i == 17)
                        {
                            f18 = value;
                        }
                        if (i == 18)
                        {
                            f19 = value;
                        }
                        if (i == 19)
                        {
                            f20 = value;
                        }
                    }

                    conn.QueryString = "exec [CLIENT_BASE].[dbo].[SP_TABLE_DELETE] " +
                            "@F1 = '" + f1 + "', " +
                            "@F2 = '" + f2 + "', " +
                            "@F3 = '" + f3 + "', " +
                            "@F4 = '" + f4 + "', " +
                            "@F5 = '" + f5 + "', " +
                            "@F6 = '" + f6 + "', " +
                            "@F7 = '" + f7 + "', " +
                            "@F8 = '" + f8 + "', " +
                            "@F9 = '" + f9 + "', " +
                            "@F10 = '" + f10 + "', " +
                            "@F11 = '" + f11 + "', " +
                            "@F12 = '" + f12 + "', " +
                            "@F13 = '" + f13 + "', " +
                            "@F14 = '" + f14 + "', " +
                            "@F15 = '" + f15 + "', " +
                            "@F16 = '" + f16 + "', " +
                            "@F17 = '" + f17 + "', " +
                            "@F18 = '" + f18 + "', " +
                            "@F19 = '" + f19 + "', " +
                            "@F20 = '" + f20 + "', " +
                            "@TABLENAME = '" + LB_CODE.Text + "'";

                    conn.ExecuteQuery();

                    FillDGRQUERY();
                    FillDGR();
                    ClearDGRUPLOAD();
                }
                catch { }
            }
        }
    }
}