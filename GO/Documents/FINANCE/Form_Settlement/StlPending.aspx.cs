using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Settlement
{
    public partial class StlPending : System.Web.UI.Page
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
                    LB_APP.Text = Request.QueryString["APPID"];
                }
                catch { }
                try
                {
                    LB_TIPE.Text = Request.QueryString["TIPE"];
                }
                catch { }

                Setup();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (LB_APP.Text != "")
            {
                DDL_APP.SelectedValue = LB_APP.Text;
                DDL_APP.Enabled = false;
            }

            FillDDLTipe();

            if (LB_TIPE.Text != "")
            {
                DDL_TIPE.SelectedValue = LB_TIPE.Text;
                DDL_TIPE.Enabled = false;
            }

            conn.QueryString = "select distinct " +
                                "a.NOREK_SOURCE, " +
                                "b.BANK " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join REKENING_MASTER b on a.NOREK_SOURCE=b.NOREK " +
                                "where a.NOREK_SOURCE is not null " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_ACCSOURCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ACCSOURCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

        }

        protected void FillDDLTipe()
        {
            DDL_TIPE.Items.Clear();
            conn.QueryString = "select CODE,DESCR from PARAM_TIPE_SETTLEMENT where APP_ID = '" + DDL_APP.SelectedValue + "' order by 2";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (DDL_APP.SelectedValue != "")
            {
                where = where + " and APP_ID='" + DDL_APP.SelectedValue + "'";
            }

            if (DDL_TIPE.SelectedValue != "")
            {
                where = where + " and TIPE_SETTLEMENT='" + DDL_TIPE.SelectedValue + "'";
            }

            if (TXT_ID.Text.Trim() != "")
            {
                where = where + " and REKAPID='" + TXT_ID.Text.Trim() + "'";
            }

            if (TXT_DESCR.Text.Trim() != "")
            {
                where = where + " and DESCR like '%" + TXT_DESCR.Text.Trim() + "%'";
            }

            if (TXT_DATE1.Text.Trim() != "")
            {
                where = where + " and convert(date,APPROVALDATE) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                where = where + " and convert(date,APPROVALDATE) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (DDL_ACCSOURCE.SelectedValue != "")
            {
                where = where + " and isnull(b.NOREK,'') = '" + DDL_ACCSOURCE.SelectedValue + "' ";
            }

            conn.QueryString = "select " +
                                "REKAPID, " +
                                "DESCR, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                "CNT," +
                                "DESTINATION_BANK_DESCR, " +
                                "APPROVAL = APPROVALBY +  ' (' + convert(varchar(50),APPROVALDATE) + ')', " +
                                "URL, " +
                                "DEFAULT_ACC_SOURCE = b.NOREK " +
                                "from V_SETTLEMENT_MASTER a " +
                                "left join (select BANK_CODE, NOREK = min(NOREK) from REKENING_MASTER where STL=1 group by BANK_CODE) b on a.DESTINATION_BANK=b.BANK_CODE " +
                                "where " +
                                "APPROVALBY is not null " +
                                "and PROCESSBY is null " + where +
                                "order by a.APPROVALDATE desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";

            conn.QueryString = "select NOREK,BANK from REKENING_MASTER where STL='1' order by 2";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");
                DropDownList ddlSOURCE = (DropDownList)DGR.Items[i].FindControl("DDL_SOURCE");

                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk ROLLBACK ?')){return false;};");

                lbREKAPID.Text = DGR.Items[i].Cells[2].Text;
                lbREKAPID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=850px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");

                ddlSOURCE.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlSOURCE.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                if (DGR.Items[i].Cells[11].Text.Replace("&nbsp;", "") != "")
                {
                    try
                    {
                        ddlSOURCE.SelectedValue = DGR.Items[i].Cells[11].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "All")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    cb.Checked = true;
                }
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "exec SP_SETTLEMENT_MASTER_PENDING_ROLLBACK '" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_APPROCE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                DropDownList ddlSOURCE = (DropDownList)DGR.Items[i].FindControl("DDL_SOURCE");
                DropDownList ddlTRANSFER = (DropDownList)DGR.Items[i].FindControl("DDL_TRANSFER");

                if (cb.Checked && ddlSOURCE.SelectedValue != "")
                {
                    try
                    {
                        conn.QueryString = "exec SP_SETTLEMENT_MASTER_PROCESS " +
                                            "'" + DGR.Items[i].Cells[2].Text + "'," +
                                            "'" + ddlSOURCE.SelectedValue + "'," +
                                            "'" + ddlTRANSFER.SelectedValue + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";                                            
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_TIPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}