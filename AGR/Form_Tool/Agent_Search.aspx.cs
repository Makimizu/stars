using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;


namespace AGR.Form_Tool
{
    public partial class Agent_Search : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            LB_TARGET1.Text = Request.QueryString["target1"].ToString();
            LB_TARGET2.Text = Request.QueryString["target2"].ToString();
            LB_PARENT.Text = Request.QueryString["parent"].ToString();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.CODE, " +
                                "a.FULLNAME, " +
                                "a.SUBCD_DESCR, " +
                                "a.AGENCY_NAME " +
                                "from		V_M_AGENTS a " +
                                "where " +
                                "a.FULLNAME like '%" + TXT_UPLINER.Text.Trim() + "%' " +
                                "and a.SUBCD_DESCR like '%" + TXT_LEVEL.Text.Trim() + "%' " +
                                "and a.AGENCY_NAME like '%" + TXT_AGENCY.Text.Trim() + "%' " +
                                "order by " +
                                "2,3";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_AGENT.DataSource = dt;
            DGR_AGENT.DataBind();

            LB_RECORDS.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR_AGENT.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_AGENT.Items[i].FindControl("LB_UPLINER_CODE");
                lb.Text = DGR_AGENT.Items[i].Cells[1].Text;
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR_AGENT.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_AGENT_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_AGENT.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_AGENT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string mode = "";
                switch (LB_PARENT.Text)
                {
                    case "0": mode = "forms[0]."; break;
                    case "1": mode = "form1."; break;
                }


                string script = "<script language='javascript'> " +
                    //"window.opener.document." + mode + "getElementById(\"" + LB_TARGET1.Text + "\").value = \"" + e.Item.Cells[1].Text + "\"; " +
                    //"window.opener.document." + mode + "getElementById(\"" + LB_TARGET2.Text + "\").value = \" - " + e.Item.Cells[2].Text + "\"; " +
                                    "window.opener.location.reload();" +
                                    "window.close(); " +
                                "</script>";
                //Response.Write(script);
                ClientScript.RegisterStartupScript(GetType(), "", script);
            }
        }
    }
}