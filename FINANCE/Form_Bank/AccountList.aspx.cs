using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace FINANCE.Form_Bank
{
    public partial class AccoutList : System.Web.UI.Page
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
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, b.BANK " +
                                "from REKENING_MASTER a " +
                                "inner join PARAM_TBL_BANK b on a.BANK_CODE = b.CODE " +
                                "order by 2";
            conn.ExecuteQuery();

            DDL_BANK.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from PARAM_INVOICE_TYPE a " +
                                "inner join V_LINK_SC_M_APPS b on a.APP_ID = b.CODE " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_ARAP.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_ARAP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,BANK from PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BANKSELECTED.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select COA, DESCR = COA + ' - ' + DESCR from V_PARAM_GL_COA_BANK a order by a.DESCR";
            conn.ExecuteQuery();
            for (int j = 0; j < conn.GetRowCount(); j++)
                DDL_GLCOA.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

            conn.QueryString = "select CODE = '', DESCR = '' union all select CODE, DESCR from PR_ACCOUNT_TYPE order by DESCR";
            conn.ExecuteQuery();
            for (int k = 0; k < conn.GetRowCount(); k++)
                DDL_ACCTYPE.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (DDL_BANK.SelectedValue != "")
                where = where + " and a.BANK_CODE='" + DDL_BANK.SelectedValue + "' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.BANK like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (DDL_ARAP.SelectedValue != "")
                where = where + " and a.APPID_PRODUCT='" + DDL_ARAP.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "ACCNO		= NOREK, " +
                                "BOOKNAME	= BANK, " +
                                "BANK		= BANK_DESCR, " +
                                "BALANCE	= replace(convert(varchar(100),convert(money, BALANCE),1), '.00', ''), " +
                                "COL			= COL, " +
                                "STL			= STL, " +
                                "APPID_PRODUCT = APPID_PRODUCT " +
                                "from V_REKENING_MASTER a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by BANK_DESCR";
            conn.ExecuteQuery();
            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from PARAM_INVOICE_TYPE a " +
                                "inner join V_LINK_SC_M_APPS b on a.APP_ID = b.CODE " +
                                "order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR.Items[i].FindControl("LB_SELECT");
                CheckBox cbCOL = (CheckBox)DGR.Items[i].FindControl("CB_COL");
                CheckBox cbSTL = (CheckBox)DGR.Items[i].FindControl("CB_STL");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_APP");

                lbt.Text = DGR.Items[i].Cells[4].Text;
                if (DGR.Items[i].Cells[5].Text == "1")
                    cbCOL.Checked = true;
                if (DGR.Items[i].Cells[6].Text == "1")
                    cbSTL.Checked = true;

                ddl.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                try
                {
                    ddl.SelectedValue = DGR.Items[i].Cells[7].Text;
                }
                catch { }
            }
        }

        protected void DDL_BANK_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LoadAccount(e.Item.Cells[4].Text, e.Item.Cells[8].Text);
                ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
            }
        }

        protected void CB_COL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cbCOL = (CheckBox)DGR.Items[i].FindControl("CB_COL");

                if ((CheckBox)sender == cbCOL)
                {
                    string bit = "0";
                    if (((CheckBox)sender).Checked)
                        bit = "1";
                    conn.QueryString = "update REKENING_MASTER set " +
                                        "COL=" + bit + ", " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where NOREK='" + DGR.Items[i].Cells[4].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
            }
        }

        protected void CB_STL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cbSTL = (CheckBox)DGR.Items[i].FindControl("CB_STL");

                if ((CheckBox)sender == cbSTL)
                {
                    string bit = "0";
                    if (((CheckBox)sender).Checked)
                        bit = "1";
                    conn.QueryString = "update REKENING_MASTER set " +
                                        "STL=" + bit + ", " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where NOREK='" + DGR.Items[i].Cells[4].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
            }
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_APP");

                if ((DropDownList)sender == ddl)
                {
                    string app = "null";
                    if (((DropDownList)sender).SelectedValue != "")
                        app = "'" + ddl.SelectedValue + "'";
                    conn.QueryString = "update REKENING_MASTER set " +
                                        "APPID_PRODUCT=" + app + ", " +
                                        "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', " +
                                        "LASTCHANGEDATE = GETDATE() " +
                                        "where NOREK='" + DGR.Items[i].Cells[4].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
            }
        }

        protected void LoadAccount(string accno, string title)
        {
            LB_TITLE.Text = title;
            if (accno == "")
            {
                TXT_NOREK.Text = "";
                TXT_BANK.Text = "";
                TXT_NAMA.Text = "";
                TXT_NAMA_CABANG.Text = "";
                TXT_ALAMAT_1.Text = "";
                TXT_ALAMAT_2.Text = "";
            }
            else
            {
                conn.QueryString = "select * from REKENING_MASTER where NOREK = '" + accno + "'";
                conn.ExecuteQuery();

                TXT_NOREK.Text = conn.GetFieldValue("NOREK").ToString();
                TXT_BANK.Text = conn.GetFieldValue("BANK").ToString();
                TXT_NAMA.Text = conn.GetFieldValue("NAMA").ToString();
                TXT_NAMA_CABANG.Text = conn.GetFieldValue("NAMA_CABANG").ToString();
                TXT_ALAMAT_1.Text = conn.GetFieldValue("ADDRESS1").ToString();
                TXT_ALAMAT_2.Text = conn.GetFieldValue("ADDRESS2").ToString();

                try
                {
                    DDL_BANKSELECTED.SelectedValue = conn.GetFieldValue("BANK_CODE").ToString();
                }
                catch { }
                try
                {
                    DDL_COL.SelectedValue = conn.GetFieldValue("COL").ToString();
                }
                catch { }
                try
                {
                    DDL_STL.SelectedValue = conn.GetFieldValue("STL").ToString();
                }
                catch { }
                try
                {
                    DDL_GLCOA.SelectedValue = conn.GetFieldValue("COA").ToString();
                }
                catch { }
                try
                {
                    DDL_ACCTYPE.SelectedValue = conn.GetFieldValue("ACCOUNT_TYPE").ToString();
                }
                catch { }
            }
        }

        protected void BT_SIMPAN_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";
            if (TXT_NOREK.Text.Trim() == "" || TXT_BANK.Text.Trim() == "" || TXT_NAMA.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "exec SP_REKENING_MASTER_UPSERT '" +
                                   TXT_NOREK.Text.Trim().Replace(" ", "") + "','" +
                                   TXT_BANK.Text.Trim() + "','" +
                                   TXT_NAMA.Text.Trim() + "','" +
                                   DDL_BANKSELECTED.SelectedValue + "','" +
                                   TXT_NAMA_CABANG.Text.Trim() + "','" +
                                   TXT_ALAMAT_1.Text.Trim() + "','" +
                                   TXT_ALAMAT_2.Text.Trim() + "','" +
                                   DDL_COL.SelectedValue + "','" +
                                   DDL_STL.SelectedValue + "','" +
                                   DDL_GLCOA.SelectedValue + "','" +
                                   DDL_ACCTYPE.SelectedValue + "','" +
                                   GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                conn.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            FillDGR();
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            LoadAccount("", "NEW");
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }
    }
}