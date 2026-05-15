using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class GPA_Endorsement_Inquiry : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_MODE.Text = Request.QueryString["MODE"];
                LB_TIPE.Text = Request.QueryString["TIPE"];

                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_TIPE_ENDORSEMENT";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (LB_TIPE.Text != "")
            {
                DDL_TIPE.SelectedValue = LB_TIPE.Text;
                DDL_TIPE.Enabled = false;
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";
            string where = "";

            switch (LB_MODE.Text)
            {
                case "REG": where = where + " and a.REG_END is null "; break;
                case "PRC": where = where + " and a.REG_END is not null and a.PROS_END is null "; break;
                case "VER": where = where + " and a.PROS_END is not null and a.VER_END is null "; break;
            }

            if (DDL_TIPE.SelectedValue != "")
                where = where + " and a.TIPE_ENDORS='" + DDL_TIPE.SelectedValue + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and a.COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_ID.Text.Trim() != "")
                where = where + " and a.BATCH_ID like '%" + TXT_ID.Text.Trim() + "%' ";

            if (TXT_POLICY_NO.Text.Trim() != "")
                where = where + " and a.POLICY_NO like '%" + TXT_POLICY_NO.Text.Trim() + "%' ";

            if (TXT_DATE1.Text.Trim() != "")
                where = where + "and convert(date,a.TGL_BATCH) >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_DATE2.Text.Trim() != "")
                where = where + "and convert(date,a.TGL_BATCH) <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "') ";

            conn.QueryString = "select " +
                                "BATCH_ID, " +
                                "TIPE_ENDORS, " +
                                "TIPE_ENDORS_DESCR, " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "TGL_BATCH = convert(varchar(20),a.TGL_BATCH,106), " +
                                "CNT, " +
                                "DOCNO, " +
                                "REPORT_URL, " +
                                "REPORT_SSRS_URL " +
                                "from V_GPA_ENDORSEMENT_BATCH a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by a.TGL_BATCH";

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbBATCH = (LinkButton)DGR.Items[i].FindControl("LB_BATCH");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");

                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk ROLLBACK ?')){return false;};");
                lbBATCH.Text = DGR.Items[i].Cells[1].Text;

                if (DGR.Items[i].Cells[3].Text == "0")
                    DGR.Items[i].BackColor = System.Drawing.Color.Yellow;
            }

        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("GPA_Proses_Frame.aspx?BATCH_ID=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {
                LB_ERR.Text = "";

                try
                {
                    conn.QueryString = "exec SP_GPA_ENDORSEMENT_BATCH_ROLLBACK '" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();

                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
                catch (System.Exception ex)
                {
                    LB_ERR.Text = ex.Message;
                }
            }
        }
    }
}