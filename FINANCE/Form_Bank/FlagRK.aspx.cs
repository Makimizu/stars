using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace FINANCE.Form_Bank
{
    public partial class FlagRK : System.Web.UI.Page
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
                DGR.CurrentPageIndex = 0;
                FillDGR();
                DGR_UNFLAG.CurrentPageIndex = 0;
                FillDGRUnflag();
            }
        }

        protected void Setup()
        {
            DDL_NOREK.Items.Clear();
            DDL_UNFLAG_NOREK.Items.Clear();

            //DDL_NOREK.Items.Add(new ListItem("", ""));
            DDL_UNFLAG_NOREK.Items.Add(new ListItem("", ""));

            conn.QueryString = "select NOREK, BANK from REKENING_MASTER order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_NOREK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_UNFLAG_NOREK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLFlag();
            BT_SET.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk SET ?')){return false;};");

            conn.QueryString = "select STARTDATE = '1/' + convert(varchar(2),month(GETDATE())) +'/' + convert(varchar(4),year(GETDATE()))";
            conn.ExecuteQuery();
            TXT_POSTDATE.Text = conn.GetFieldValue("STARTDATE").ToString();

            SetMode();
        }

        protected void FillDGR()
        {
            LB_ERR.Text = "";
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_DESCR.Text.Trim() != "")
                where = where + "and a.DESCR like '%" + TXT_DESCR.Text.Trim() + "%' ";

            if (TXT_POSTDATE.Text.Trim() != "")
                where = where + "and convert(date,a.POST_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_POSTDATE2.Text.Trim() != "")
                where = where + "and convert(date,a.POST_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_POSTDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_NOREK.SelectedValue != "")
                where = where + "and a.NOREK = '" + DDL_NOREK.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "NOREK, " +
                                "a.TRXID, " +
                                "POST_DATE = convert(varchar(20),POST_DATE,106), " +
                                "DESCR, " +
                                "AMOUNT = (case when DEBET>0 then replace(convert(varchar(100),convert(money,DEBET),1),'.00','') else replace(convert(varchar(100),convert(money,CREDIT),1),'.00','') end) " +
                                "from V_REKENING_JURNAL_ORI a " +
                                "left join RETUR_MASTER b on b.REKAP_ID = case when LEFT(a.DESCR, 3) = 'LF-' then LEFT(a.DESCR, 20) when LEFT(a.DESCR, 6) = 'FNSUB-' then LEFT(a.DESCR, 18) else LEFT(a.DESCR, 13) end and b.TRXID = a.TRXID and b.REASON <> '-' " +
                                "where " +
                                "1=1 and b.RETUR_ID is null " + DDL_INOUT.SelectedValue + " " + where +
                                "order by a.POST_DATE";

            conn.ExecuteQuery(50000);
            LB_RECORDS.Text = conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select AMOUNT=replace(convert(varchar(100),convert(money,SUM(AMOUNT)),1),'.00','') from (" +
                                "select " +
                                "AMOUNT = (case when DEBET>0 then DEBET else CREDIT end) " +
                                "from V_REKENING_JURNAL_ORI a " +
                                "left join RETUR_MASTER b on b.REKAP_ID = case when LEFT(a.DESCR, 3) = 'LF-' then LEFT(a.DESCR, 20) when LEFT(a.DESCR, 6) = 'FNSUB-' then LEFT(a.DESCR, 18) else LEFT(a.DESCR, 13) end and b.TRXID = a.TRXID and b.REASON <> '-' " +
                                "where b.RETUR_ID is null and " +
                                "a.NOREK = '" + DDL_NOREK.SelectedValue + "' " + DDL_INOUT.SelectedValue + " " + where + ") a";

            conn.ExecuteQuery();
            LB_AMOUNT.Text = conn.GetFieldValue("AMOUNT").ToString();

            conn.QueryString = "SELECT NOREK FROM PARAM_GL_JOURNAL_BREAKDOWN_USAGE " +
                               "WHERE NOREK = '" + DDL_NOREK.SelectedValue + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    Button BtnBreakdown = (Button)DGR.Items[i].FindControl("BT_BREAKDOWN");
                    BtnBreakdown.Visible = true;
                }
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            //Settlement Retur di STARS - ANDEZ 09-06-2023 START
            Button btRetur = (Button)e.Item.FindControl("BT_RETUR");

            if (e.CommandName == "Retur")
            {
                conn.QueryString = "SELECT * FROM SECURITY..MENU_ROLE WHERE ROLE_CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "' AND MENU_CODE = '943'";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("AUTH_TYPE").ToString() == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('User tidak memiliki hak akses')", true);
                    return;
                }

                ToRetur();

                //Response.Write("<script>alert('TES');</script>");
            }

            if (e.CommandName == "Breakdown")
            {
                //conn.QueryString = "SELECT * FROM SECURITY..MENU_ROLE WHERE ROLE_CODE = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "ID_Roles") + "' AND MENU_CODE = '943'";
                //conn.ExecuteQuery();

                //if (conn.GetFieldValue("AUTH_TYPE").ToString() == "0")
                //{
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "ClientScript", "alert('User tidak memiliki hak akses')", true);
                //    return;
                //}
                conn.QueryString = "EXEC SP_REKENING_JURNAL_FLAG_BREAKDOWN_INSERT_DEFAULT " +
                                    "'" + e.Item.Cells[0].Text + "', " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                Response.Redirect("FlagRK_Split.aspx?TRXID=" + e.Item.Cells[0].Text);
            }
        }

        protected void DDL_INOUT_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
            FillDDLFlag();
        }

        protected void FillDDLFlag()
        {
            string DC = "C";
            if (DDL_INOUT.SelectedItem.Text == "OUTBOUND")
                DC = "D";

            conn.QueryString = "exec SP_PARAM_RK_VALIDASI " +
                                "'" + DDL_NOREK.SelectedValue + "'," +
                                "'" + DC + "'";
            conn.ExecuteQuery();
            DDL_FLAG.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FLAG.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DDL_NOREK_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
            FillDDLFlag();
        }

        protected void BT_SET_Click(object sender, EventArgs e)
        {
            LB_ERR.Text = "";
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "exec SP_REKENING_JURNAL_FLAG_INSERT " +
                                            "'" + DGR.Items[i].Cells[0].Text + "'," +
                                            "'" + DDL_FLAG.SelectedValue + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERR.Text = LB_ERR.Text + ex.Message + "<BR>";
                    }
                }
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetMode();
        }

        protected void SetMode()
        {
            LB_TITLE.Text = DDL_MODE.SelectedItem.Text;
            switch (DDL_MODE.SelectedValue)
            {
                case "FLAG": DV_FLAG.Visible = true; DV_UNFLAG.Visible = false; break;
                case "UNFLAG": DV_FLAG.Visible = false; DV_UNFLAG.Visible = true; break;
            }
        }

        protected void FillDGRUnflag()
        {
            LB_UNFLAG_RECORDS.Text = "";

            string ACCNO, VALIDASI, DESCR, POST_DATE_FROM, POST_DATE_TO;
            ACCNO = VALIDASI = DESCR = "";
            POST_DATE_FROM = POST_DATE_TO = "null";

            if (DDL_UNFLAG_NOREK.SelectedValue != "")
                ACCNO = DDL_UNFLAG_NOREK.SelectedValue;

            if (TXT_UNFLAG_VALIDATION.Text.Trim() != "")
                VALIDASI = TXT_UNFLAG_VALIDATION.Text.Trim();
            if (TXT_UNFLAG_DESCR.Text.Trim() != "")
                DESCR = TXT_UNFLAG_DESCR.Text.Trim();
            if (TXT_UNLFAG_POSTDATE.Text.Trim() != "")
                POST_DATE_FROM = "'" + GlobalUse.GlobalDateFormat(TXT_UNLFAG_POSTDATE.Text.Trim(), "d/M/yyyy") + "'";
            if (TXT_UNLFAG_POSTDATE2.Text.Trim() != "")
                POST_DATE_TO = "'" + GlobalUse.GlobalDateFormat(TXT_UNLFAG_POSTDATE2.Text.Trim(), "d/M/yyyy") + "'";

            conn.QueryString = "exec SP_REKENING_JURNAL_TOBE_UNFLAG " +
                                "'" + DDL_UNFLAG_DC.SelectedValue + "'," +
                                "'" + ACCNO + "'," +
                                "'" + VALIDASI + "'," +
                                POST_DATE_FROM + "," +
                                POST_DATE_TO + "," +
                                "'" + DESCR + "'";
            conn.ExecuteQuery(120000);

            LB_UNFLAG_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_UNFLAG.DataSource = dt;
            DGR_UNFLAG.DataBind();

            for (int i = 0; i < DGR_UNFLAG.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_UNFLAG.Items[i].FindControl("CB_UNFLAG");
                Button btBreakdownUnflag = (Button)DGR_UNFLAG.Items[i].FindControl("BT_BREAKDOWN_UNFLAG");
                if (DGR_UNFLAG.Items[i].Cells[4].Text == "BREAKDOWN")
                {
                    cb.Visible = false;
                    btBreakdownUnflag.Visible = true;
                }
                else
                {
                    cb.Visible = true;
                    btBreakdownUnflag.Visible = false;
                }
            }
        }

        protected void DDL_UNFLAG_DC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR_UNFLAG.CurrentPageIndex = 0;
            FillDGRUnflag();
        }

        protected void DDL_UNFLAG_NOREK_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR_UNFLAG.CurrentPageIndex = 0;
            FillDGRUnflag();
        }

        protected void BT_UNFLAG_SEARCH_Click(object sender, EventArgs e)
        {
            DGR_UNFLAG.CurrentPageIndex = 0;
            FillDGRUnflag();
        }

        protected void DGR_UNFLAG_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_UNFLAG.CurrentPageIndex = e.NewPageIndex;
            FillDGRUnflag();
        }

        protected void CB_UNFLAG_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_UNFLAG.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_UNFLAG.Items[i].FindControl("CB_UNFLAG");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void BT_UNFLAG_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_UNFLAG.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_UNFLAG.Items[i].FindControl("CB_UNFLAG");
                if (cb.Checked)
                {
                    conn.QueryString = "delete from REKENING_JURNAL_FLAG where TRXID = '" + DGR_UNFLAG.Items[i].Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
            }

            DGR_UNFLAG.CurrentPageIndex = 0;
            FillDGRUnflag();
        }

        //ANDEZ
        protected void FillDGRRetur(string IDGROUP)
        {
            conn.QueryString = "select RETUR_ID, RETUR_ID_GROUP, REKAP_ID, ACC_NO, ACC_NAME, AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00','') " +
                                " from RETUR_MASTER where RETUR_ID_GROUP = '" + IDGROUP + "'";
            conn.ExecuteQuery();
            DGR_RETUR.DataSource = conn.GetDataTable().Copy();
            DGR_RETUR.DataBind();

            for (int i = 0; i < DGR_RETUR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_RETUR.Items[i].FindControl("DDL_REASON");

                conn.QueryString = "SELECT CODE, DESCR FROM PR_REASON_RETUR ORDER BY CODE";
                conn.ExecuteQuery();
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }
            }
        }

        protected void DGR_RETUR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                ToUpdateRetur();
            }
        }

        protected void ToUpdateRetur()
        {
            for (int i = 0; i < DGR_RETUR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_RETUR.Items[i].FindControl("DDL_REASON");

                if (ddl.SelectedValue != "0")
                {
                    conn.QueryString = "UPDATE RETUR_MASTER " +
                    "SET REASON = '" + ddl.SelectedValue + "' " +
                    "WHERE RETUR_ID = '" + DGR_RETUR.Items[i].Cells[0].Text + "'";
                    conn.ExecuteQuery();

                    FillDGR();
                }
                else
                {
                    conn.QueryString = "DELETE FROM RETUR_MASTER " +
                    "WHERE RETUR_ID = '" + DGR_RETUR.Items[i].Cells[0].Text + "'";
                    conn.ExecuteQuery();

                    conn.QueryString = "DELETE FROM TRACK_RETUR_MASTER " +
                    "WHERE RETUR_ID = '" + DGR_RETUR.Items[i].Cells[0].Text + "'";
                    conn.ExecuteQuery();

                    Response.Write("<script>alert('REKAP_ID " + DGR_RETUR.Items[i].Cells[2].Text + " BELUM DI ISI');</script>");

                    FillDGR();
                }
            }
        }

        protected void DGR_RETUR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            ToRetur();
        }

        protected void ToRetur()
        {
            var guid = Guid.NewGuid().ToString();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    conn.QueryString = "exec SP_RETUR " +
                                                            "'-', " +
                                                            "'-', " +
                                                            "'-', " +
                                                            "'" + DGR.Items[i].Cells[3].Text + "'," +
                                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                                            "'" + DGR.Items[i].Cells[2].Text + "'," +
                                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "', 'MANUAL', '" + guid + "', '" + DGR.Items[i].Cells[0].Text + "'";
                    conn.ExecuteQuery();
                }
            }

            IDGROUP.Text = conn.GetFieldValue("IDGROUP").ToString();

            DGR_RETUR.CurrentPageIndex = 0;
            FillDGRRetur(IDGROUP.Text);

            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('ReturPopup').style.display = 'block';", true);
        }
        //ANDEZ

        protected void DGR_UNFLAG_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "BreakdownUnflag")
            {
                Response.Redirect("FlagRK_Split.aspx?TRXID=" + e.Item.Cells[0].Text);
            }
        }
    }
}