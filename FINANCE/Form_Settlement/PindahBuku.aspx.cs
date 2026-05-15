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
    public partial class PindahBuku : System.Web.UI.Page
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
            conn.QueryString = "select MONTH = MONTH(GETDATE())";
            conn.ExecuteQuery();
            DDL_MONTH.SelectedValue = conn.GetFieldValue("MONTH").ToString();

            conn.QueryString = "select YEAR = YEAR(GETDATE()) - SEQ + 1 from SC_SEQ where SEQ <= 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_REKENING_MASTER_TRANSFER_SUMMARY " + DDL_YEAR.SelectedValue + "," + DDL_MONTH.SelectedValue;
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            Connection connSOURCE = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            Connection connDESTINATION = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddlSOURCE = (DropDownList)DGR.Items[i].FindControl("DDL_SOURCE");
                DropDownList ddlDESTINATION = (DropDownList)DGR.Items[i].FindControl("DDL_DESTINATION");
                Button btDETAIL = (Button)DGR.Items[i].FindControl("BT_DETAIL");

                connSOURCE.QueryString = "select NOREK, BANK from REKENING_MASTER where ACCOUNT_TYPE = '" + DGR.Items[i].Cells[0].Text + "' order by 2";
                connDESTINATION.QueryString = "select NOREK, BANK from REKENING_MASTER where ACCOUNT_TYPE = '" + DGR.Items[i].Cells[1].Text + "' order by 2";

                connSOURCE.ExecuteQuery();
                connDESTINATION.ExecuteQuery();
                ddlSOURCE.Items.Add(new ListItem("", ""));
                for (int a = 0; a < connSOURCE.GetRowCount(); a++)
                {
                    ddlSOURCE.Items.Add(new ListItem(connSOURCE.GetFieldValue(a, 1).ToString(), connSOURCE.GetFieldValue(a, 0).ToString()));
                }

                ddlDESTINATION.Items.Add(new ListItem("", ""));
                for (int b = 0; b < connDESTINATION.GetRowCount(); b++)
                {
                    ddlDESTINATION.Items.Add(new ListItem(connDESTINATION.GetFieldValue(b, 1).ToString(), connDESTINATION.GetFieldValue(b, 0).ToString()));
                }

                try
                {
                    if (DGR.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                    {
                        ddlSOURCE.SelectedValue = DGR.Items[i].Cells[3].Text;
                        ddlSOURCE.Enabled = false;
                    }
                }
                catch { }

                //btDETAIL.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "") + "','DETAIL','height=500px,width=700px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
            }
        }

        protected void DDL_MONTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Process")
            {
                DropDownList ddlSOURCE = (DropDownList)e.Item.FindControl("DDL_SOURCE");
                DropDownList ddlDESTINATION = (DropDownList)e.Item.FindControl("DDL_DESTINATION");
                if (ddlSOURCE.SelectedValue == "" || ddlDESTINATION.SelectedValue == "")
                    return;

                conn.QueryString = "exec SP_REKENING_MASTER_TRANSFER_PROCESS " +
                                    "'" + e.Item.Cells[5].Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "-" + e.Item.Cells[1].Text + "'," +
                                    "'" + ddlSOURCE.SelectedValue + "'," +
                                    "'" + ddlDESTINATION.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }

            if (e.CommandName == "Detail")
            {
                DV_PORCESS.Visible = false;
                DV_DETAIL.Visible = true;
                IF.Src = e.Item.Cells[4].Text;
            }
        }

        protected void LB_BACK_Click(object sender, EventArgs e)
        {
            DV_PORCESS.Visible = true;
            DV_DETAIL.Visible = false;
        }

    }
}