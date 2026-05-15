using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR
{
    public partial class PARAMETER_MASTER_REWARD : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["ID"].ToString();
                Setup();
                FillDGR();
            }
        }


        protected void Setup()
        {
            conn.QueryString = "select " +
                                "DESCR		= a.DESCRIPTION, " +
                                "TYPE		= c.DESCR, " +
                                "COMPANY	= b.COMPANY_NAME, " +
                                "STARTDATE	= convert(varchar(20), a.STARTDATE, 106), " +
                                "ENDDATE	= convert(varchar(20), a.ENDDATE, 106) " +
                                "from		PARAM_REMUN_MASTER a " +
                                "inner join	V_LINK_CB_COMPANY b on a.COMPANY_CODE = b.COMPANY_CODE " +
                                "inner join	PR_REMUN_TYPE c on a.TYPE = c.CODE " +
                                "where " +
                                "ID = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            LB_COMPANY.Text = conn.GetFieldValue("COMPANY").ToString();
            LB_DESCR.Text = conn.GetFieldValue("DESCR").ToString();
            LB_ENDDATE.Text = conn.GetFieldValue("ENDDATE").ToString();
            LB_STARTDATE.Text = conn.GetFieldValue("STARTDATE").ToString();
            LB_TYPE.Text = conn.GetFieldValue("TYPE").ToString();

        }


        protected void FillDGR()
        {
            conn.QueryString = "exec SP_PARAM_REMUN_COMMISSION '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();


            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtMINAMT = (TextBox)DGR.Items[i].FindControl("TXT_MIN_AMT");
                TextBox txtMAXAMT = (TextBox)DGR.Items[i].FindControl("TXT_MAX_AMT");
                TextBox txtCOMM = (TextBox)DGR.Items[i].FindControl("TXT_COMM");
                TextBox txtOR1 = (TextBox)DGR.Items[i].FindControl("TXT_OR1");
                TextBox txtOR2 = (TextBox)DGR.Items[i].FindControl("TXT_OR2");
                TextBox txtOR3 = (TextBox)DGR.Items[i].FindControl("TXT_OR3");
                TextBox txtOR4 = (TextBox)DGR.Items[i].FindControl("TXT_OR4");
                TextBox txtOR5 = (TextBox)DGR.Items[i].FindControl("TXT_OR5");
                TextBox txtOR6 = (TextBox)DGR.Items[i].FindControl("TXT_OR6");
                TextBox txtOR7 = (TextBox)DGR.Items[i].FindControl("TXT_OR7");
                TextBox txtOR8 = (TextBox)DGR.Items[i].FindControl("TXT_OR8");

                CheckBox cbNETT = (CheckBox)DGR.Items[i].FindControl("CB_NETT");

                txtMAXAMT.Text = DGR.Items[1].Cells[2].Text;
                txtMINAMT.Text = DGR.Items[1].Cells[3].Text;
                txtCOMM.Text = DGR.Items[1].Cells[5].Text;
                txtOR1.Text = DGR.Items[1].Cells[6].Text;
                txtOR2.Text = DGR.Items[1].Cells[7].Text;
                txtOR3.Text = DGR.Items[1].Cells[8].Text;
                txtOR4.Text = DGR.Items[1].Cells[9].Text;
                txtOR5.Text = DGR.Items[1].Cells[10].Text;
                txtOR6.Text = DGR.Items[1].Cells[11].Text;
                txtOR7.Text = DGR.Items[1].Cells[12].Text;
                txtOR8.Text = DGR.Items[1].Cells[13].Text;

                if (DGR.Items[1].Cells[4].Text == "1")
                    cbNETT.Checked = true;
            }
        }
    }
}