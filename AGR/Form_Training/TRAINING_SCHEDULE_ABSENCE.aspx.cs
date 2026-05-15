using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;
namespace AGR
{
    public partial class TRAINING_SCHEDULE_ABSENCE : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_SCHEDULE_CODE.Text = Request.QueryString["SCHEDULE_CODE"];
                FillDDLAgency();
                LoadHeader();
                FillDGR();
            }
        }

        protected void FillDDLAgency()
        {
            conn.QueryString = "select distinct " +
                                "c.AGENCY_CODE, " +
                                "c.AGENCY_NAME " +
                                "from		TRAINING_SCHEDULE a " +
                                "inner join	TRAINING_SUBMISSION b on a.SCHEDULE_CODE = b.SCHEDULE_CODE " +
                                "inner join	V_M_AGENTS c on b.AGENT_CODE = c.CODE and c.AGENCY_CODE is not null " +
                                "where " +
                                "a.SCHEDULE_CODE = '" + LB_SCHEDULE_CODE.Text + "' " +
                                "order by 2";
            conn.ExecuteQuery();
            DDL_AGENCY.Items.Clear();
            DDL_AGENCY.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_AGENCY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void LoadHeader()
        {
            conn.QueryString = "exec SP_TRAINING_SCHEDULE_DISPLAY_HEADER '" + LB_SCHEDULE_CODE.Text + "'";
            conn.ExecuteQuery();

            LB_ACTIVE.Text = conn.GetFieldValue("ACTIVE").ToString();
            LB_ATTENDEES.Text = conn.GetFieldValue("ATTENDEES").ToString();
            LB_CITY.Text = conn.GetFieldValue("CITY").ToString();
            LB_DATE.Text = conn.GetFieldValue("THEDATE").ToString();
            LB_HOUR.Text = conn.GetFieldValue("PRESENCE_TIME").ToString();
            LB_LOCATION.Text = conn.GetFieldValue("LOCATION").ToString();
            LB_MODE.Text = conn.GetFieldValue("MODE").ToString();
            LB_PARTICIPANTS.Text = conn.GetFieldValue("PARTICIPANTS").ToString();
            LB_PUBLISH.Text = conn.GetFieldValue("PUBLISH").ToString();
            LB_QUOTA.Text = conn.GetFieldValue("QUOTA").ToString();
            LB_TRAINERNAME.Text = conn.GetFieldValue("TRAINER_NAME").ToString();
            LB_TRAININGNAME.Text = conn.GetFieldValue("TRAINING_NAME").ToString();
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_TRAINING_SUBMISSION_DISPLAY  " +
                                "'" + LB_SCHEDULE_CODE.Text + "'," +
                                "'" + TXT_CHANNEL.Text.Trim() + "'," +
                                "'" + TXT_LEVEL.Text.Trim() + "'," +
                                "'" + TXT_FULLNAME.Text.Trim() + "'," +
                                "'" + DDL_AGENCY.SelectedValue + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "") != "")
                    cb.Checked = true;
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void TXT_FULLNAME_TextChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void TXT_CHANNEL_TextChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void TXT_LEVEL_TextChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_AGENCY_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb == (CheckBox)sender)
                {
                    string attend = "0";
                    if (cb.Checked)
                        attend = "1";

                    conn.QueryString = "exec SP_TRAINING_SUBMISSION_ATTEND " +
                                        "'" + LB_SCHEDULE_CODE.Text + "'," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        attend;
                    conn.ExecuteNonQuery();
                    LoadHeader();
                    FillDGR();
                    return;
                }
            }
        }

        protected void CB_X_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB_X");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB_X");
                    if (cb.Checked)
                    {
                        conn.QueryString = "delete from TRAINING_SUBMISSION where SCHEDULE_CODE = '" + LB_SCHEDULE_CODE.Text + "' and AGENT_CODE = '" + DGR.Items[i].Cells[0].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                }

                FillDDLAgency();
                DGR.CurrentPageIndex = 0;
                FillDGR();
                LoadHeader();
            }
        }


    }
}