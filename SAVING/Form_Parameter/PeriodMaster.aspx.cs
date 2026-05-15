using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Text;
using System.Net.Mime;

namespace SAVING.Form_Parameter
{
    public partial class PeriodMaster : System.Web.UI.Page
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
            conn.QueryString = "select YEAR	= YEAR(GETDATE()) - a.SEQ + 1 from	SC_SEQ a where a.SEQ <= 5 order by 1 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_YEAR.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "CODE	= SEQ, " +
                                "DESCR	= UPPER(DateName( month , DateAdd( month , SEQ , -1 ))) " +
                                "from	SC_SEQ  " +
                                "where  " +
                                "SEQ		<= 12";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_MONTH.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select " +
                                "THISYEAR	= YEAR(GETDATE()), " +
                                "THISMONTH	= MONTH(GETDATE())";
            conn.ExecuteQuery();

            try
            {
                DDL_YEAR.SelectedValue = conn.GetFieldValue("THISYEAR").ToString();
            }
            catch { }

            try
            {
                DDL_MONTH.SelectedValue = conn.GetFieldValue("THISMONTH").ToString();
            }
            catch { }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_UNIT_LINK_SCHEDULE " +
                                DDL_YEAR.SelectedValue + "," +
                                DDL_MONTH.SelectedValue;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btSAVE = (Button)DGR.Items[i].FindControl("BT_SAVE");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DELETE");
                Label txtSTARTDD = (Label)DGR.Items[i].FindControl("TXT_STARTDATE");
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

                if (DGR.Items[i].Cells[7].Text == "1")
                {
                    txtSTARTDD.Enabled = true;
                    ddlSTARTHH.Enabled = true;
                    ddlSTARTMM.Enabled = true;
                    txtENDDD.Enabled = true;
                    ddlENDHH.Enabled = true;
                    ddlENDMM.Enabled = true;
                    btSAVE.Visible = true;
                    btDEL.Visible = true;

                    btSAVE.Attributes.Add("onclick", "if(!confirm('UPDATE THE SELECTED DATE?')){return false;};");
                    btDEL.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE YOU WANT TO DELETE THE SELECTED DATE?')){return false;};");
                }
            }
        }

        protected void DDL_YEAR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DDL_MONTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                Label txtSTART = (Label)e.Item.FindControl("TXT_STARTDATE");
                DropDownList ddlSTARTHH = (DropDownList)e.Item.FindControl("DDL_STARTHH");
                DropDownList ddlSTARTMM = (DropDownList)e.Item.FindControl("DDL_STARTMM");

                TextBox txtEND = (TextBox)e.Item.FindControl("TXT_ENDDATE");
                DropDownList ddlENDHH = (DropDownList)e.Item.FindControl("DDL_ENDHH");
                DropDownList ddlENDMM = (DropDownList)e.Item.FindControl("DDL_ENDMM");

                conn.QueryString = "update UNIT_LINK_SCHEDULE set " +                                    
                                    "END_DATE = '" + GlobalUse.GlobalDateFormat(txtEND.Text.Trim(), "d/M/yyyy") + " " + ddlENDHH.SelectedValue + ":" + ddlENDMM.SelectedValue + "' " +
                                    "where " +
                                    "THEDATE = '" + txtSTART.Text + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }

            if (e.CommandName == "Delete")
            {
                Label txtSTART = (Label)e.Item.FindControl("TXT_STARTDATE");

                conn.QueryString = "delete from UNIT_LINK_SCHEDULE where THEDATE = '" + txtSTART.Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }
    }
}