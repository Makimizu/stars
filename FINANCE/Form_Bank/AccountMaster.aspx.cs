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
    public partial class AccountMaster : System.Web.UI.Page
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
                ClearEntry();
                FillGrid();
                GlobalUse.SetReadOnly(Page, GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"), "RekeningMaster.aspx.cs");

            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,BANK from PARAM_TBL_BANK order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select COA, DESCR = COA + ' - ' + DESCR from V_PARAM_GL_COA_BANK a order by a.DESCR";
            conn.ExecuteQuery();
            for (int j = 0; j < conn.GetRowCount(); j++)
                DDL_GLCOA.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

            conn.QueryString = "select CODE = '', DESCR = '' union all select CODE, DESCR from PR_ACCOUNT_TYPE order by DESCR";
            conn.ExecuteQuery();
            for (int k = 0; k < conn.GetRowCount(); k++)
                DDL_ACCTYPE.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ClearEntry();
                conn.QueryString = "select * from REKENING_MASTER where NOREK = '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteQuery();

                TXT_NOREK.Text = conn.GetFieldValue("NOREK").ToString();
                TXT_BANK.Text = conn.GetFieldValue("BANK").ToString();
                TXT_NAMA.Text = conn.GetFieldValue("NAMA").ToString();
                TXT_NAMA_CABANG.Text = conn.GetFieldValue("NAMA_CABANG").ToString();
                TXT_ALAMAT_1.Text = conn.GetFieldValue("ADDRESS1").ToString();
                TXT_ALAMAT_2.Text = conn.GetFieldValue("ADDRESS2").ToString();

                try
                {
                    DDL_BANK.SelectedValue = conn.GetFieldValue("BANK_CODE").ToString();
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

            if (e.CommandName == "Mutasi")
            {
                Response.Redirect("RekeningMutasi.aspx?norek=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from REKENING_MASTER where NOREK='" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = "<BR><B>" + ex.Message + "</B>";
                    return;
                }
                FillGrid();
            }
        }

        protected void FillGrid()
        {
            conn.QueryString = "select " +
                                "NOREK, " +
                                "BANK, " +
                                "NAMA, " +
                                "BANK_CODE, " +
                                "BANK_DESCR, " +
                                "COL = (case when isnull(COL,0) = 1 then 'YES' else 'NO' end), " +
                                "STL = (case when isnull(STL,0) = 1 then 'YES' else 'NO' end), " +
                                "COA_DESCR, " +
                                "BALANCE = replace(convert(varchar(100),convert(money,BALANCE),1),'.00','') " +
                                "from V_REKENING_MASTER " +
                                "order by 2";

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR.Items[i].FindControl("LB_SELECT");
                DropDownList ddlCOA = (DropDownList)DGR.Items[i].FindControl("DDL_COA");
                Button btDel = (Button)DGR.Items[i].FindControl("BT_DEL");

                btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");
                lbt.Text = DGR.Items[i].Cells[1].Text;

                if (DGR.Items[i].Cells[5].Text != "0")
                    DGR.Items[i].Cells[5].ForeColor = System.Drawing.Color.Red;

                
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
                                   DDL_BANK.SelectedValue + "','" +
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

            FillGrid();
            ClearEntry();
        }

        protected void ClearEntry()
        {
            TXT_NOREK.Text = "";
            TXT_BANK.Text = "";
            TXT_NAMA.Text = "";
            TXT_NAMA_CABANG.Text = "";
            TXT_ALAMAT_1.Text = "";
            TXT_ALAMAT_2.Text = "";
            GlobalTools.SetFocus(this, TXT_NOREK);
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                conn.QueryString = "select BALANCE = replace(convert(varchar(100),convert(money,SUM(BALANCE)),1),'.00','') from V_REKENING_MASTER";
                conn.ExecuteQuery();
                e.Item.Cells[5].Text = "<B>" + conn.GetFieldValue(0, 0).ToString() + "</B>";
                e.Item.Cells[4].Text = "<B>TOTAL</B>";
            }
        }
    }
}