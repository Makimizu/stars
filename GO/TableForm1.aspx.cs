using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.CuBESCore;
using DMS.DBConnection;

namespace GO
{
    public partial class TableForm1 : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        protected Connection conn2 = new Connection(Crypto.DecryptStringAES(System.Configuration.ConfigurationManager.AppSettings["conn"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TABLENAME.Text = Request.QueryString["tablename"];
                LB_FIELDNAME.Text = Request.QueryString["fieldname"];
                LB_FIELDNAME2.Text = Request.QueryString["fieldname2"];
                try
                {
                    LB_PRIMAUTO.Text = Request.QueryString["primauto"];
                }
                catch { }
                Setup();
            }
        }

        protected void Clear()
        {
            LB_FIELDVALUE.Text = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                TextBox txtDD = (TextBox)DGR.Items[i].FindControl("TXT_DD");
                DropDownList ddlMMM = (DropDownList)DGR.Items[i].FindControl("DDL_MMM");
                TextBox txtYYYY = (TextBox)DGR.Items[i].FindControl("TXT_YYYY");

                txt.Text = "";
                txtDD.Text = "";
                txtYYYY.Text = "";
            }

            for (int i = 0; i < DGR_IMG.Items.Count; i++)
            {
                Image img = (Image)DGR_IMG.Items[i].FindControl("IMG_VAL");
                img.ImageUrl = "";
                img.Height = 0;
            }
        }

        protected void FillLB()
        {
            Clear();
            LB1.Items.Clear();
            conn.QueryString = "select CODE = " + LB_FIELDNAME.Text + ", DESCR = UPPER(" + LB_FIELDNAME2.Text + ") from " + LB_TABLENAME.Text + " where " + LB_FIELDNAME.Text + " + ' - ' + " + LB_FIELDNAME2.Text + " like '%" + TXT_SEARCH.Text.Trim() + "%' order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                LB1.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            DGR_LIST.DataSource = conn.GetDataTable().Copy();
            DGR_LIST.DataBind();
            for (int i = 0; i < DGR_LIST.Items.Count; i++)
            {
                LinkButton lbID = (LinkButton)DGR_LIST.Items[i].FindControl("LB_ID");
                lbID.Text = DGR_LIST.Items[i].Cells[1].Text + " - " + DGR_LIST.Items[i].Cells[2].Text;
            }
        }

        protected void Setup()
        {
            FillLB();

            conn.QueryString = "select * from VS_SYSCOLUMNS where xtype not in (165) and tablename='" + LB_TABLENAME.Text + "' order by 1";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select * from VS_SYSCOLUMNS where xtype in (165) and tablename='" + LB_TABLENAME.Text + "' order by 1";
            conn.ExecuteQuery();
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_IMG.DataSource = dt;
            DGR_IMG.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                TextBox txtDD = (TextBox)DGR.Items[i].FindControl("TXT_DD");
                DropDownList ddlMMM = (DropDownList)DGR.Items[i].FindControl("DDL_MMM");
                TextBox txtYYYY = (TextBox)DGR.Items[i].FindControl("TXT_YYYY");

                DGR.Items[i].Cells[0].Text = DGR.Items[i].Cells[0].Text.Replace("_", " ");

                /*
                if (DGR.Items[i].Cells[4].Text == "1")
                {
                    txt.ReadOnly = true;
                    ddl.Enabled = false;
                }
                */

                if (DGR.Items[i].Cells[3].Text == "0")
                {
                    txt.BackColor = System.Drawing.Color.Yellow;
                    ddl.BackColor = System.Drawing.Color.Yellow;
                    txtDD.BackColor = System.Drawing.Color.Yellow;
                    ddlMMM.BackColor = System.Drawing.Color.Yellow;
                    txtYYYY.BackColor = System.Drawing.Color.Yellow;
                }

                if (DGR.Items[i].Cells[1].Text == "167")
                {
                    txt.MaxLength = int.Parse(DGR.Items[i].Cells[2].Text);
                    if (int.Parse(DGR.Items[i].Cells[2].Text) <= 10)
                        txt.Width = 100;
                }

                if (DGR.Items[i].Cells[1].Text == "40" || DGR.Items[i].Cells[1].Text == "61")
                {
                    txt.Width = 120;
                }

                if (DGR.Items[i].Cells[1].Text == "167" && (DGR.Items[i].Cells[0].Text.IndexOf("PASSWORD") >= 0 || DGR.Items[i].Cells[0].Text.IndexOf("PWD") >= 0))
                {
                    txt.TextMode = TextBoxMode.Password;
                }

                if (DGR.Items[i].Cells[1].Text == "61")
                {
                    txt.Enabled = false;
                }

                if (DGR.Items[i].Cells[1].Text == "40")
                {
                    txt.Visible = false;
                    txtDD.Visible = true;
                    ddlMMM.Visible = true;
                    txtYYYY.Visible = true;
                }

                if (DGR.Items[i].Cells[5].Text != "&nbsp;")
                {
                    txt.Visible = false;
                    ddl.Visible = true;
                    conn.QueryString = "select " + DGR.Items[i].Cells[6].Text + "," + DGR.Items[i].Cells[7].Text + " from " + DGR.Items[i].Cells[5].Text;
                    conn.ExecuteQuery();
                    for (int j = 0; j < conn.GetRowCount(); j++)
                    {
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    }
                }

                if (DGR.Items[i].Cells[4].Text == "1" && LB_PRIMAUTO.Text == "1")
                {
                    txt.Enabled = false;
                    ddl.Enabled = false;
                    txtDD.Enabled = false;
                    ddlMMM.Enabled = false;
                    txtYYYY.Enabled = false;
                }
            }
        }

        protected string GetPrimaryKey()
        {
            string SQL = "where 1=1 ";
            try
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                    DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                    TextBox txtDD = (TextBox)DGR.Items[i].FindControl("TXT_DD");
                    DropDownList ddlMMM = (DropDownList)DGR.Items[i].FindControl("DDL_MMM");
                    TextBox txtYYYY = (TextBox)DGR.Items[i].FindControl("TXT_YYYY");

                    if (DGR.Items[i].Cells[4].Text == "1")
                    {
                        SQL = SQL + " and " + DGR.Items[i].Cells[0].Text.Replace(" ", "_") + "='";
                        if (txt.Visible)
                            SQL = SQL + txt.Text + "' ";
                        if (ddl.Visible)
                            SQL = SQL + ddl.SelectedValue + "' ";
                        if (txtDD.Visible)
                            SQL = SQL + txtDD.Text + " " + ddlMMM.SelectedItem.Text + " " + txtYYYY.Text + "' ";
                    }
                }
            }
            catch
            {
                return "";
            }

            return SQL;
        }

        protected string GeneratePrimeKey()
        {
            conn.QueryString = "select PRIME = max(convert(int," + LB_FIELDNAME.Text + ")) from " + LB_TABLENAME.Text;
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
                return "1";

            int PrimeKey = int.Parse(conn.GetFieldValue("PRIME").ToString()) + 1;

            return PrimeKey.ToString();
        }

        protected string GetValue(string FIELDNAME)
        {
            string val = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                if (DGR.Items[i].Cells[0].Text.Replace(" ", "_") == FIELDNAME)
                {
                    TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                    val = txt.Text.Trim();
                    break;
                }
            }

            return val;
        }

        protected bool IsExist(string FIELDNAME, string val)
        {
            bool Exist = false;

            conn.QueryString = "select " + FIELDNAME + " from " + LB_TABLENAME.Text + " where " + FIELDNAME + "='" + val + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
                Exist = true;

            return Exist;
        }

        protected int IsNew()
        {
            if (LB_PRIMAUTO.Text == "1")
            {
                if (GetValue(LB_FIELDNAME.Text) == "")
                    return 1;
                else
                    return 0;
            }

            string PRIMEVAL = GetValue(LB_FIELDNAME.Text);

            if (PRIMEVAL.Trim() == "")
                return -1;
            else
            {
                if (IsExist(LB_FIELDNAME.Text, PRIMEVAL))
                    return 0;
                else
                    return 1;
            }
        }

        protected bool CheckMandatory()
        {
            bool result = true;
            LB_ERROR.Text = "";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                TextBox txtDD = (TextBox)DGR.Items[i].FindControl("TXT_DD");
                DropDownList ddlMMM = (DropDownList)DGR.Items[i].FindControl("DDL_MMM");
                TextBox txtYYYY = (TextBox)DGR.Items[i].FindControl("TXT_YYYY");

                if (DGR.Items[i].Cells[3].Text == "0")
                {
                    if (txt.Visible && txt.Text.Trim() == "")
                        result = false;

                    if (ddl.Visible && ddl.SelectedValue == "")
                        result = false;

                    if (txtDD.Visible && txtDD.Text.Trim() == "")
                        result = false;

                    if (txtDD.Visible && txtDD.Text.Trim() != "")
                    {
                        try
                        {
                            DateTime date = new DateTime(int.Parse(txtYYYY.Text.Trim()), int.Parse(ddlMMM.SelectedValue), int.Parse(txtDD.Text.Trim()));
                        }
                        catch
                        {
                            result = false;
                        }
                    }

                    if (!result)
                    {
                        LB_ERROR.Text = "Field " + DGR.Items[i].Cells[0].Text + " is MANDATORY, or invalid format";
                        return result;
                    }
                }
            }

            return result;
        }

        protected void LoadRecord(string field1)
        {
            FillDGR(field1);
            FillDGR_IMG(field1);
        }

        protected void FillDGR(string field1)
        {
            conn.QueryString = "select * from " + LB_TABLENAME.Text + " where " + LB_FIELDNAME.Text + "='" + field1 + "'";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                TextBox txtDD = (TextBox)DGR.Items[i].FindControl("TXT_DD");
                DropDownList ddlMMM = (DropDownList)DGR.Items[i].FindControl("DDL_MMM");
                TextBox txtYYYY = (TextBox)DGR.Items[i].FindControl("TXT_YYYY");

                string val = conn.GetFieldValue(DGR.Items[i].Cells[0].Text.Replace(" ", "_")).ToString();
                if (txt.Visible)
                {
                    txt.Text = val;
                    try
                    {
                        if (DGR.Items[i].Cells[1].Text == "167" && (DGR.Items[i].Cells[0].Text.IndexOf("PASSWORD") >= 0 || DGR.Items[i].Cells[0].Text.IndexOf("PWD") >= 0))
                            txt.Text = Crypto.DecryptStringAES(val);
                    }
                    catch { }

                    try
                    {
                        if (DGR.Items[i].Cells[1].Text == "61")
                        {
                            conn2.QueryString = "select VAL = CONVERT(varchar(100),convert(datetime,'" + val + "'),106) + ' ' + CONVERT(varchar(100),convert(datetime,'" + val + "'),108)";
                            conn2.ExecuteQuery();
                            txt.Text = conn2.GetFieldValue("VAL").ToString();
                        }
                    }
                    catch { }
                }

                if (ddl.Visible)
                {
                    try
                    {
                        ddl.SelectedValue = val;
                    }
                    catch { }
                }

                if (txtDD.Visible)
                {
                    try
                    {
                        DateTime dtm = DateTime.Parse(val);
                        txtDD.Text = dtm.Day.ToString();
                        ddlMMM.SelectedValue = dtm.Month.ToString();
                        txtYYYY.Text = dtm.Year.ToString();
                    }
                    catch { }
                }
            }
        }

        protected void FillDGR_IMG(string field1)
        {
            conn.QueryString = "select * from " + LB_TABLENAME.Text + " where " + LB_FIELDNAME.Text + "='" + field1 + "'";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR_IMG.Items.Count; i++)
            {
                Image img = (Image)DGR_IMG.Items[i].FindControl("IMG_VAL");
                FileUpload fup = (FileUpload)DGR_IMG.Items[i].FindControl("FILEUPLOAD");

                try
                {
                    string val = conn.GetFieldValue(DGR_IMG.Items[i].Cells[0].Text.Replace(" ", "_")).ToString();
                    img.ImageUrl = GlobalUse.GetStringImageURL(conn.QueryString, DGR_IMG.Items[i].Cells[0].Text.Replace(" ", "_"));
                    img.Height = 100;
                }
                catch
                {
                    img.Height = 0;
                }
            }
        }

        protected string SaveNew()
        {
            string SQL = "";
            string SQL2 = "";
            string PrimeKey = "";
            if (LB_PRIMAUTO.Text == "1")
                PrimeKey = GeneratePrimeKey();
            else
                PrimeKey = GetValue(LB_FIELDNAME.Text);


            conn.QueryString = "select USER_CODE from USER_LOG_HISTORY where ROWID='" + Session["s"] + "'";
            conn.ExecuteQuery();

            SQL = "insert into " + LB_TABLENAME.Text + " (CREATEBY,CREATEDATE";
            SQL2 = ") values ('" + conn.GetFieldValue("USER_CODE").ToString() + "',GETDATE()";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                if (DGR.Items[i].Cells[1].Text == "61")
                    continue;

                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                TextBox txtDD = (TextBox)DGR.Items[i].FindControl("TXT_DD");
                DropDownList ddlMMM = (DropDownList)DGR.Items[i].FindControl("DDL_MMM");
                TextBox txtYYYY = (TextBox)DGR.Items[i].FindControl("TXT_YYYY");

                SQL = SQL + "," + DGR.Items[i].Cells[0].Text.Replace(" ", "_");

                string val = "null";
                if (txt.Visible)
                {
                    if (DGR.Items[i].Cells[0].Text.Replace(" ", "_") == LB_FIELDNAME.Text)
                        val = "'" + PrimeKey + "'";
                    else
                    {
                        if (txt.Text.Trim() != "")
                        {
                            if (DGR.Items[i].Cells[0].Text.IndexOf("PASSWORD") >= 0 || DGR.Items[i].Cells[0].Text.IndexOf("PWD") >= 0)
                            {
                                val = "'" + Crypto.EncryptStringAES(txt.Text) + "'";
                            }
                            else
                            {
                                val = "'" + txt.Text + "'";
                            }
                        }
                    }
                }

                if (txtDD.Visible && txtDD.Text.Trim() != "" && txtYYYY.Text.Trim() != "")
                {
                    val = "'" + txtDD.Text + " " + ddlMMM.SelectedItem.Text + " " + txtYYYY.Text + "'";
                }

                if (ddl.Visible && ddl.SelectedValue != "")
                {
                    val = "'" + ddl.SelectedValue + "'";
                }

                SQL2 = SQL2 + "," + val;
            }

            SQL = SQL + SQL2 + ")";

            return SQL;
        }

        protected string SaveUpdate()
        {
            string SQL = "";
            string SQL2 = "";

            conn.QueryString = "select USER_CODE from USER_LOG_HISTORY where ROWID='" + Session["s"] + "'";
            conn.ExecuteQuery();

            SQL = "update " + LB_TABLENAME.Text + " set " +
                    "LASTCHANGEBY = '" + conn.GetFieldValue("USER_CODE").ToString() + "'," +
                    "LASTCHANGEDATE = GETDATE() ";

            SQL2 = " where " + LB_FIELDNAME.Text + " = '" + GetValue(LB_FIELDNAME.Text) + "'";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                if (DGR.Items[i].Cells[1].Text == "61")
                    continue;

                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_VAL");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_VAL");
                TextBox txtDD = (TextBox)DGR.Items[i].FindControl("TXT_DD");
                DropDownList ddlMMM = (DropDownList)DGR.Items[i].FindControl("DDL_MMM");
                TextBox txtYYYY = (TextBox)DGR.Items[i].FindControl("TXT_YYYY");


                SQL = SQL + "," + DGR.Items[i].Cells[0].Text.Replace(" ", "_") + "=";

                string val = "null";
                if (txt.Visible)
                {
                    if (txt.Text.Trim() != "")
                    {
                        if (DGR.Items[i].Cells[0].Text.IndexOf("PASSWORD") >= 0 || DGR.Items[i].Cells[0].Text.IndexOf("PWD") >= 0)
                        {
                            val = "'" + Crypto.EncryptStringAES(txt.Text) + "'";
                        }
                        else
                        {
                            val = "'" + txt.Text + "'";
                        }
                    }
                }

                if (txtDD.Visible && txtDD.Text.Trim() != "" && txtYYYY.Text.Trim() != "")
                {
                    val = "'" + txtDD.Text + " " + ddlMMM.SelectedItem.Text + " " + txtYYYY.Text + "'";
                }

                if (ddl.Visible && ddl.SelectedValue != "")
                {
                    val = "'" + ddl.SelectedValue + "'";
                }

                SQL = SQL + val;
            }

            SQL = SQL + SQL2;

            return SQL;
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "New")
            {
                Clear();
            }

            if (e.CommandName == "Save")
            {
                LB_ERROR.Text = "";
                int bNew = IsNew();

                if (!CheckMandatory())
                    return;

                /*
                if (bNew < 0)
                {
                    LB_ERROR.Text = "Field " + LB_FIELDNAME.Text + " could not be EMPTY";
                    return;
                }                
                */

                if (bNew > 0)
                {
                    try
                    {
                        conn.QueryString = SaveNew();
                        conn.ExecuteNonQuery();
                        //LB_ERROR.Text = SaveNew();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERROR.Text = ex.Message;
                        return;
                    }
                }
                else
                {
                    try
                    {
                        conn.QueryString = SaveUpdate();
                        conn.ExecuteNonQuery();
                        //LB_ERROR.Text = SaveUpdate();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERROR.Text = ex.Message;
                        return;
                    }
                }

                FillLB();
                GlobalTools.popMessage(this, "SUCCESS");
            }
        }

        protected void LB1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LB_FIELDVALUE.Text = LB1.SelectedValue;
            LoadRecord(LB1.SelectedValue);
        }

        protected void DGR_IMG_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Upload")
            {
                FileUpload fup = (FileUpload)e.Item.FindControl("FILEUPLOAD");
                if (fup.HasFile)
                {
                    try
                    {
                        string filename = Path.GetFileName(fup.FileName);
                        string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
                        if (File.Exists(fullpath))
                        {
                            File.Delete(fullpath);
                        }
                        fup.SaveAs(fullpath);

                        string SQL = "update " + LB_TABLENAME.Text + " set " + e.Item.Cells[0].Text.Replace(" ", "_") + "=@File where " + LB_FIELDNAME.Text + "='" + LB_FIELDVALUE.Text + "'";
                        GlobalUse.FileToSQL(fullpath, SQL);
                        FillDGR_IMG(LB_FIELDVALUE.Text);

                        if (File.Exists(fullpath))
                        {
                            File.Delete(fullpath);
                        }
                    }
                    catch { }
                }
            }

            if (e.CommandName == "Clear")
            {
                try
                {
                    conn.QueryString = "update " + LB_TABLENAME.Text + " set " + e.Item.Cells[0].Text.Replace(" ", "_") + "=null where " + LB_FIELDNAME.Text + "='" + LB_FIELDVALUE.Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR_IMG(LB_FIELDVALUE.Text);
                }
                catch { }
            }
        }

        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillLB();
        }

        protected void DGR_LIST_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                LB_FIELDVALUE.Text = e.Item.Cells[1].Text;
                LoadRecord(e.Item.Cells[1].Text);
            }
        }
    }
}