using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Tools
{
    public partial class Email : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TIPE.Text = Request.QueryString["TIPE"];
                Setup();
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_ERROR.Text = "";
            string where = "";

            if (TXT_DOCNO.Text.Trim() != "")
                where = where + " and DOCNO='" + TXT_DOCNO.Text.Trim() + "' ";

            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_PROCDATE.Text.Trim() != "")
                where = where + " and convert(date,APPR_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_PROCDATE.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_PROCDATE2.Text.Trim() != "")
                where = where + " and convert(date,APPR_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_PROCDATE2.Text.Trim(), "d/M/yyyy") + "' ";


            conn.QueryString = "select " +
                                "DOCNO, " +
                                "APPR_DATE = convert(varchar(30),APPR_DATE), " +
                                "COMPANY, " +
                                "EMAIL, " +
                                "REPORT, " +
                                "FIRST_SEND = FIRST_SENDBY + ' (' + convert(varchar(100),FIRST_SENDDATE) + ')', " +
                                "LAST_SEND = LAST_SENDBY + ' (' + convert(varchar(100),LAST_SENDDATE) + ')' " +
                                "from V_AUTOEMAIL_LIST a " +
                                "where " +
                                "TIPE = '" + LB_TIPE.Text + "' " +
                                "and FIRST_SENDBY " + DDL_SENDED.SelectedValue + " " + where +
                                "order by a.APPR_DATE desc";
            try
            {
                conn.ExecuteQuery();
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = "<BR>" + ex.Message;
                return;
            }

            LB_RESULT.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbREPORT = (LinkButton)DGR.Items[i].FindControl("LB_REPORT");
                TextBox txtEMAIL = (TextBox)DGR.Items[i].FindControl("TXT_EMAIL");

                lbREPORT.Text = DGR.Items[i].Cells[1].Text.Trim().Replace("&nbsp;", "");
                txtEMAIL.Text = DGR.Items[i].Cells[4].Text.Trim().Replace("&nbsp;", "");

                lbREPORT.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[8].Text + "','DOCUMENT','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }
        }

        protected void Setup()
        {

        }


        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            LB_ERROR.Text = "";
            if (e.CommandName == "Send")
            {
                TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");

                if (txtEMAIL.Text.Trim() == "")
                    return;

                try
                {
                    SendEmail(e);
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = "<BR>" + ex.Message;
                    return;
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
        }

        protected void SendEmail(DataGridCommandEventArgs e)
        {
            TextBox txtEMAIL = (TextBox)e.Item.FindControl("TXT_EMAIL");
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }
    }
}