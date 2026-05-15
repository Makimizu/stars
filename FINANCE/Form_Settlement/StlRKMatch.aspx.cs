using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Settlement
{
    public partial class StlRKMatch : System.Web.UI.Page
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
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from SETTLEMENT_MASTER a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLTipe();

            BT_SETTLE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk SETTLE ?')){return false;};");
        }

        protected void FillDDLTipe()
        {
            conn.QueryString = "select CODE,DESCR from PARAM_TIPE_SETTLEMENT where APP_ID = '" + DDL_APP.SelectedValue + "' order by 2";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            BT_SETTLE.Visible = false;
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

            if (TXT_DOCNO.Text.Trim() != "")
            {
                where = where + " and REKAPID like '%" + TXT_DOCNO.Text.Trim() + "%'";
            }

            if (TXT_STLDESCR.Text.Trim() != "")
            {
                where = where + " and STL_DESCR like '%" + TXT_STLDESCR.Text.Trim() + "%'";
            }

            if (TXT_RKDESCR.Text.Trim() != "")
            {
                where = where + " and RK_DESCR like '%" + TXT_RKDESCR.Text.Trim() + "%'";
            }

            if (TXT_BENEF.Text.Trim() != "")
            {
                where = where + " and ACC_NAME like '%" + TXT_BENEF.Text.Trim() + "%'";
            }

            if (TXT_DATE1.Text.Trim() != "")
            {
                where = where + " and datediff(day,convert(date,a.POST_DATE),convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')) <= 0 ";
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                where = where + " and datediff(day,convert(date,a.POST_DATE),convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')) >= 0 ";
            }

            conn.QueryString = "select " +
                                "REKAPID, " +
                                "STL_DESCR, " +
                                "ACC_NO, " +
                                "ACC_BANK, " +
                                "ACC_NAME, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,AMOUNT),1),'.00',''), " +
                                "DEBET = replace(convert(varchar(100),convert(money,DEBET),1),'.00',''), " +
                                "RK_DESCR, " +
                                "POST_DATE = convert(varchar(20),POST_DATE,106), " +
                                "NOREK_SOURCE, " +
                                "TRXID, " +
                                "URL " +
                                "from V_REKENING_JURNAL_SETTLEMENT_MATCH a " +
                                "where 1=1 " + where + " " +
                                "order by " +
                                "a.POST_DATE desc";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            if (conn.GetRowCount() > 0)
            {
                BT_SETTLE.Visible = true;
            }

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";



            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_ID");

                lbREKAPID.Text = DGR.Items[i].Cells[1].Text;                
                lbREKAPID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[10].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=850px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");                

                if (i > 0)
                {
                    if (DGR.Items[i].Cells[1].Text.Replace("&nbsp;", "") == DGR.Items[i-1].Cells[1].Text.Replace("&nbsp;", ""))
                    {
                        DGR.Items[i - 1].BackColor = System.Drawing.Color.Yellow;
                        DGR.Items[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
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
        }

        protected void BT_SETTLE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "exec SP_REKENING_JURNAL_CREDIT_INSERT " +
                                            "'" + DGR.Items[i].Cells[11].Text + "'," +
                                            "'" + DGR.Items[i].Cells[12].Text + "'," +
                                            "'" + DGR.Items[i].Cells[1].Text + "'," +
                                            "'" + DGR.Items[i].Cells[2].Text + "'," +
                                            "'" + DGR.Items[i].Cells[3].Text + "'," +
                                            "'" + DGR.Items[i].Cells[4].Text + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch
                    {
                        return;
                    }

                }
            }

            try
            {
                FillDGR();
            }
            catch
            {
                DGR.CurrentPageIndex = 0;
                FillDGR();
            }
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if ((CheckBox)sender == cb)
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                }
            }
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
        }
    }
}