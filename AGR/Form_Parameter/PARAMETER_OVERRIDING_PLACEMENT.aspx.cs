using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace AGR.Form_Parameter
{
    public partial class PARAMETER_OVERRIDING_PLACEMENT : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillDGR();
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_PARAM_OVERRIDING_PLACEMENT";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select SUB_CODE, DESCR = SUB_CODE + ' - ' + DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION order by MARKET_SEGMENT, SUB_CODE";
            conn.ExecuteQuery();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddlOR1 = (DropDownList)DGR.Items[i].FindControl("DDL_OR1");
                DropDownList ddlOR2 = (DropDownList)DGR.Items[i].FindControl("DDL_OR2");
                DropDownList ddlOR3 = (DropDownList)DGR.Items[i].FindControl("DDL_OR3");
                DropDownList ddlOR4 = (DropDownList)DGR.Items[i].FindControl("DDL_OR4");
                DropDownList ddlOR5 = (DropDownList)DGR.Items[i].FindControl("DDL_OR5");

                ddlOR1.Items.Add(new ListItem("", ""));
                ddlOR2.Items.Add(new ListItem("", ""));
                ddlOR3.Items.Add(new ListItem("", ""));
                ddlOR4.Items.Add(new ListItem("", ""));
                ddlOR5.Items.Add(new ListItem("", ""));

                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddlOR1.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    ddlOR2.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    ddlOR3.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    ddlOR4.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                    ddlOR5.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                try
                {
                    ddlOR1.SelectedValue = DGR.Items[i].Cells[3].Text;
                }
                catch { }
                try
                {
                    ddlOR2.SelectedValue = DGR.Items[i].Cells[4].Text;
                }
                catch { }
                try
                {
                    ddlOR3.SelectedValue = DGR.Items[i].Cells[5].Text;
                }
                catch { }
                try
                {
                    ddlOR4.SelectedValue = DGR.Items[i].Cells[6].Text;
                }
                catch { }
                try
                {
                    ddlOR5.SelectedValue = DGR.Items[i].Cells[7].Text;
                }
                catch { }

                if (ddlOR1.SelectedValue != "")
                    ddlOR1.BackColor = System.Drawing.Color.Yellow;
                if (ddlOR2.SelectedValue != "")
                    ddlOR2.BackColor = System.Drawing.Color.Yellow;
                if (ddlOR3.SelectedValue != "")
                    ddlOR3.BackColor = System.Drawing.Color.Yellow;
                if (ddlOR4.SelectedValue != "")
                    ddlOR4.BackColor = System.Drawing.Color.Yellow;
                if (ddlOR5.SelectedValue != "")
                    ddlOR5.BackColor = System.Drawing.Color.Yellow;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddlOR1 = (DropDownList)DGR.Items[i].FindControl("DDL_OR1");
                DropDownList ddlOR2 = (DropDownList)DGR.Items[i].FindControl("DDL_OR2");
                DropDownList ddlOR3 = (DropDownList)DGR.Items[i].FindControl("DDL_OR3");
                DropDownList ddlOR4 = (DropDownList)DGR.Items[i].FindControl("DDL_OR4");
                DropDownList ddlOR5 = (DropDownList)DGR.Items[i].FindControl("DDL_OR5");

                string OR1, OR2, OR3, OR4, OR5;
                OR1 = OR2 = OR3 = OR4 = OR5 = "null";

                if (ddlOR1.SelectedValue != "")
                    OR1 = "'" + ddlOR1.SelectedValue + "'";
                if (ddlOR2.SelectedValue != "")
                    OR2 = "'" + ddlOR2.SelectedValue + "'";
                if (ddlOR3.SelectedValue != "")
                    OR3 = "'" + ddlOR3.SelectedValue + "'";
                if (ddlOR4.SelectedValue != "")
                    OR4 = "'" + ddlOR4.SelectedValue + "'";
                if (ddlOR5.SelectedValue != "")
                    OR5 = "'" + ddlOR5.SelectedValue + "'";

                //try
                //{
                    conn.QueryString = "exec SP_PARAM_OVERRIDING_PLACEMENT_UPSERT " +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        OR1 + "," +
                                        OR2 + "," +
                                        OR3 + "," +
                                        OR4 + "," +
                                        OR5 + "," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                //}
                //catch { }
            }
            FillDGR();
        }
    }
}