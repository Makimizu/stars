using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Accounting
{
    public partial class GL_Journal_List : System.Web.UI.Page
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
            BT_NEW.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk INSERT ?')){return false;};");
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.DESCR, " +
                                "STAT = (case when isnull(b.CNT,0) > 0 and isnull(c.CNT,0) > 0 then 1 else 0 end) " +
                                "from PARAM_GL_JOURNAL a " +
                                "left join (select CODE, CNT=count(COA) from PARAM_GL_JOURNAL_DETAIL where DC='D' group by CODE) b on a.CODE=b.CODE " +
                                "left join (select CODE, CNT=count(COA) from PARAM_GL_JOURNAL_DETAIL where DC='C' group by CODE) c on a.CODE=c.CODE " +
                                "where " +
                                "a.CODE like '" + TXT_FIND_CODE.Text.Trim() + "%' " +
                                "and a.DESCR like '%" + TXT_FIND_DESCR.Text.Trim() + "%' " +
                                "order by a.CODE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                Button btDELETE = (Button)DGR.Items[i].FindControl("BT_DELETE");

                lbCODE.Text = DGR.Items[i].Cells[0].Text;
                btDELETE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");

                if (DGR.Items[i].Cells[3].Text != "1")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.journalbody.location.href = 'GL_Journal_Detail.aspx?CODE=" + e.Item.Cells[0].Text + "';</script>");
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_PARAM_GL_JOURNAL_DELETE '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void TXT_FIND_DESCR_TextChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void TXT_FIND_CODE_TextChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            if (TXT_FIND_CODE.Text.Trim() == "" || TXT_FIND_DESCR.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "insert into PARAM_GL_JOURNAL (CODE,DESCR) select " +
                                    "'" + TXT_FIND_CODE.Text.Trim() + "'," +
                                    "'" + TXT_FIND_DESCR.Text.Trim() + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
            catch { }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                conn.QueryString = "select " +
                                "a.CODE " +
                                "from PARAM_GL_JOURNAL a " +
                                "where " +
                                "CODE like '" + TXT_FIND_CODE.Text.Trim() + "%' " +
                                "and DESCR like '%" + TXT_FIND_DESCR.Text.Trim() + "%'";
                conn.ExecuteQuery();
                e.Item.Cells[4].Text = conn.GetRowCount().ToString();
            }
        }
    }
}