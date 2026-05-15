using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Policy_NBRN : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {

        }

        protected void FillDGR()
        {
            string where = "";
            
            conn.QueryString = "select distinct " +
                                "ID, " +
                                "POLICY_ID, " +
                                "POLICY_NO, " +
                                "COMPANY_NAME, " +
                                "START_DATE = convert(varchar(20),START_DATE,106), " +
                                "END_DATE = convert(varchar(20),END_DATE,106), " +
                                "TIPE_DESCR, " +
                                "MOP_DESCR, " +
                                "TPA_DESCR, " +
                                "MEMBER, " +
                                "PREMIUM = replace(convert(varchar(100),convert(money,PREMIUM),1),'.00',''), " +
                                "AGENT_NAME, " +
                                "PROCESSDATE " +
                                "from V_POLICY_PERIOD_QUOTATION_CLOSING " +
                                "where 1=1 " + where +
                                "order by " +
                                "PROCESSDATE desc";
            conn.ExecuteQuery();

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

            conn.QueryString = "select URL = URL + '&rc:Parameters=False&POLICY_PERIOD_ID=@PERIOD&rs:Format=WORD', DESCR from V_LINK_SC_REPORT_LIST where CODE in ('289','280','281','282') order by DESCR";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton bt = (LinkButton)DGR.Items[i].FindControl("LB");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_PRINT");

                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString().Replace("@PERIOD",DGR.Items[i].Cells[3].Text)));
                }

                bt.Text = DGR.Items[i].Cells[2].Text;
            }
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

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Response.Redirect("PolisFrame.aspx?ID=" + e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Print")
            {
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_PRINT");
                //Response.Redirect(ddl.SelectedValue);
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'> window.open('" + ddl.SelectedValue + "','CETAK','height=500px,width=1000px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes,resizable=no'); </script>");
            }
        }
    }
}