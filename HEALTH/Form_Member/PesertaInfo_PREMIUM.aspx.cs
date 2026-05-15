using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class PesertaInfo_PREMIUM : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"];
                LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];
                FillDGRPremium();
                FillDGRInvoice();
                FillDGREndorsement();
            }
        }

        protected void FillDGRInvoice()
        {
            conn.QueryString = "exec SP_UW_PESERTA_INFO '" + LB_REGNO.Text + "','7','" + LB_PERIOD.Text + "',null";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_INVOICE.DataSource = dt;
            DGR_INVOICE.DataBind();

            for (int i = 0; i < DGR_INVOICE.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_INVOICE.Items[i].FindControl("LB_INVOICE");

                lb.Attributes.Add("onclick", "window.open('" + DGR_INVOICE.Items[i].Cells[1].Text.Replace("&nbsp;", "").Trim() + "','INVOICE','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                lb.Text = DGR_INVOICE.Items[i].Cells[0].Text;
            }
        }

        protected void FillDGRPremium()
        {
            conn.QueryString = "exec SP_UW_PESERTA_INFO '" + LB_REGNO.Text + "','7a','" + LB_PERIOD.Text + "',null";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PREMIUM.DataSource = dt;
            DGR_PREMIUM.DataBind();
        }

        protected void FillDGREndorsement()
        {
            conn.QueryString = "exec SP_UW_PESERTA_INFO '" + LB_REGNO.Text + "','7b','" + LB_PERIOD.Text + "',null";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_ENDORSEMENT.DataSource = dt;
                DGR_ENDORSEMENT.DataBind();

                for (int i = 0; i < DGR_ENDORSEMENT.Items.Count; i++)
                {
                    LinkButton lb = (LinkButton)DGR_ENDORSEMENT.Items[i].FindControl("LB_BATCH");

                    lb.Attributes.Add("onclick", "window.open('" + DGR_ENDORSEMENT.Items[i].Cells[1].Text.Replace("&nbsp;", "").Trim() + "','INVOICE','height=600px,width=800px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                    lb.Text = DGR_ENDORSEMENT.Items[i].Cells[0].Text;
                }
            }
        }
    }
}