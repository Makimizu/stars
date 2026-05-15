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
    public partial class GL_Period : System.Web.UI.Page
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
            conn.QueryString = "select distinct Y = year(START_DATE) from GL_PERIOD order by 1 desc";
            conn.ExecuteQuery();

            for(int i=0;i<conn.GetRowCount();i++)
            {
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "CODE, " +
                                "START_DATE_DD = convert(varchar(20),START_DATE_DD,103), " +
                                "START_DATE_HH, " +
                                "START_DATE_MM, " +
                                "END_DATE_DD = convert(varchar(20),END_DATE_DD,103), " +
                                "END_DATE_HH, " +
                                "END_DATE_MM, " +
                                "CLOSED_BY, " +
                                "CLOSED_DATE, " +
                                "STAT " +
                                "from V_GL_PERIOD a " +
                                "where " +
                                "YEAR(a.START_DATE_DD) = " + DDL_YEAR.SelectedValue + " " +
                                "order by a.START_DATE_DD";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btSAVE = (Button)DGR.Items[i].FindControl("BT_SAVE");
                Button btCLOSE = (Button)DGR.Items[i].FindControl("BT_CLOSE");
                TextBox txtSTARTDD = (TextBox)DGR.Items[i].FindControl("TXT_STARTDATE");
                DropDownList ddlSTARTHH = (DropDownList)DGR.Items[i].FindControl("DDL_STARTHH");
                DropDownList ddlSTARTMM = (DropDownList)DGR.Items[i].FindControl("DDL_STARTMM");
                TextBox txtENDDD = (TextBox)DGR.Items[i].FindControl("TXT_ENDDATE");
                DropDownList ddlENDHH = (DropDownList)DGR.Items[i].FindControl("DDL_ENDHH");
                DropDownList ddlENDMM = (DropDownList)DGR.Items[i].FindControl("DDL_ENDMM");

                txtSTARTDD.Text = DGR.Items[i].Cells[1].Text;
                ddlSTARTHH.SelectedValue = DGR.Items[i].Cells[2].Text;
                ddlSTARTMM.SelectedValue = DGR.Items[i].Cells[3].Text;
                txtENDDD.Text = DGR.Items[i].Cells[4].Text;
                ddlENDHH.SelectedValue = DGR.Items[i].Cells[5].Text;
                ddlENDMM.SelectedValue = DGR.Items[i].Cells[6].Text;

                btSAVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk SAVE ?')){return false;};");

                if (DGR.Items[i].Cells[7].Text == "0")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.Pink;
                    btSAVE.Visible = false;
                }
                if (DGR.Items[i].Cells[7].Text == "1")
                {
                    DGR.Items[i].BackColor = System.Drawing.Color.Yellow;
                    btCLOSE.Visible = true;
                    btCLOSE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk CLOSE PERIOD ?')){return false;};");
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                TextBox txtSTART = (TextBox)e.Item.FindControl("TXT_STARTDATE");
                DropDownList ddlSTARTHH = (DropDownList)e.Item.FindControl("DDL_STARTHH");
                DropDownList ddlSTARTMM = (DropDownList)e.Item.FindControl("DDL_STARTMM");

                TextBox txtEND = (TextBox)e.Item.FindControl("TXT_ENDDATE");
                DropDownList ddlENDHH = (DropDownList)e.Item.FindControl("DDL_ENDHH");
                DropDownList ddlENDMM = (DropDownList)e.Item.FindControl("DDL_ENDMM");

                conn.QueryString = "update GL_PERIOD set " +
                                    "START_DATE = '" + GlobalUse.GlobalDateFormat(txtSTART.Text.Trim(), "d/M/yyyy") + " " + ddlSTARTHH.SelectedValue + ":" + ddlSTARTMM.SelectedValue + "', " +
                                    "END_DATE = '" + GlobalUse.GlobalDateFormat(txtEND.Text.Trim(), "d/M/yyyy") + " " + ddlENDHH.SelectedValue + ":" + ddlENDMM.SelectedValue + "' " +
                                    "where " +
                                    "CODE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }

            if (e.CommandName == "Close")
            {
                try
                {
                    conn.QueryString = "exec SP_GL_PERIOD_CLOSE " +
                                        "'" + e.Item.Cells[0].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}