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
    public partial class TRAINING_SCHEDULE_LIST : System.Web.UI.Page
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
            conn.QueryString = "select " +
                                "CITY_CODE, " +
                                "CITY_NAME	= PROVINCE_NAME + ' - ' + CITY_NAME collate database_default " +
                                "from V_LINK_CB_PARAM_CITY " +
                                "order by 2";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CITY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select " +
                                "CODE		= a.TRAINING_CODE, " +
                                "DESCR		= b.DESCR + ' - ' + a.TRAINING_NAME " +
                                "from		TRAINING_MASTER a " +
                                "inner join	PR_TRAINING_LEVEL b on a.TRAINING_LEVEL = b.CODE " +
                                "order by " +
                                "b.CODE, " +
                                "a.TRAINING_CODE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TRAINING.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            DDL_TRAINING_LIBRARY.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TRAINING_LIBRARY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select TRAINER_CODE, TRAINER_NAME =  TRAINER_NAME +' ('+ TRAINER_CODE + ')' from TRAINING_TRAINER";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TRAINER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE, DESCR from PR_TRAINING_MODE order by CODE desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MODE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select HH = RIGHT('0' + convert(varchar(2), SEQ-1), 2) from SC_SEQ where SEQ <= 24 order by SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_HH_START.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_HH_END.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select HH = RIGHT('0' + convert(varchar(2), SEQ-1), 2) from SC_SEQ where SEQ <= 60 order by SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MM_START.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
                DDL_MM_END.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "SELECT THEDATE = convert(varchar(20), DATEADD(yy, DATEDIFF(yy, 0, GETDATE()), 0), 103)";
            conn.ExecuteQuery();
            TXT_START_DATE_FROM.Text = conn.GetFieldValue("THEDATE").ToString();
        }

        protected void FillDGR()
        {
            string TRAINING_CODE = "null";
            string START_DATE_FROM = "null";

            if (DDL_TRAINING_LIBRARY.SelectedValue != "")
                TRAINING_CODE = "'" + DDL_TRAINING_LIBRARY.SelectedValue + "'";

            if (TXT_START_DATE_FROM.Text.Trim() != "")
                START_DATE_FROM = "'" + GlobalUse.GlobalDateFormat(TXT_START_DATE_FROM.Text.Trim(), "d/M/yyyy") + "'";

            conn.QueryString = "exec SP_TRAINING_SCHEDULE_DISPLAY " + TRAINING_CODE + "," + START_DATE_FROM;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                Button btPRT = (Button)DGR.Items[i].FindControl("BT_PARTICIPANT");

                lb.Text = DGR.Items[i].Cells[1].Text;

                if (DGR.Items[i].Cells[3].Text != "1")
                    btDEL.Visible = false;

                if (DGR.Items[i].Cells[4].Text == "0")
                    btPRT.Enabled = false;

                btPRT.Text = "Participants : " + DGR.Items[i].Cells[4].Text;
                btDEL.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO DELETE ?')){return false;};");
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {

            string vacc = "0";
            if (CB_VACC.Checked)
                vacc = "1";

            string pub = "0";
            if (CB_PUB.Checked)
                pub = "1";

            string SCHEDULE_CODE = "null";
            if (LB_SCHEDULE_CODE.Text != "")
                SCHEDULE_CODE = "'" + LB_SCHEDULE_CODE.Text + "'";

            conn.QueryString = "exec SP_TRAINING_SCHEDULE_UPSERT " +
                                "@SCHEDULE_CODE = " + SCHEDULE_CODE + "," +
                                "@TRAINING_CODE = '" + DDL_TRAINING.SelectedValue + "'," +
                                "@START_DATE = '" + GlobalUse.GlobalDateFormat(TXT_START_DATE.Text, "d/M/yyyy") + "'," +
                                "@END_DATE = '" + GlobalUse.GlobalDateFormat(TXT_END_DATE.Text, "d/M/yyyy") + "'," +
                                "@DURATION = '" + TXT_DURATION.Text + "'," +
                                "@PRESENCE_TIME_START = '" + DDL_HH_START.SelectedValue + ":" + DDL_MM_START.SelectedValue + "'," +
                                "@PRESENCE_TIME_END = '" + DDL_HH_END.SelectedValue + ":" + DDL_MM_END.SelectedValue + "'," +
                                "@QUOTA = '" + TXT_QUOTA.Text + "'," +
                                "@CITY_CODE = '" + DDL_CITY.SelectedValue + "'," +
                                "@MODE = '" + DDL_MODE.SelectedValue + "'," +
                                "@LOCATION = '" + TXT_LOCATION.Text + "'," +
                                "@TRAINER_CODE = '" + DDL_TRAINER.SelectedValue + "'," +
                                "@PUBLISH = " + pub + "," +
                                "@ACTIVE = " + vacc + "," +
                                "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            LoadRecord(LB_SCHEDULE_CODE.Text);
            FillDGR();

            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Your Data has been Saved Successfully.');", true);
        }


        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandName == "Detail")
            {
                LoadRecord(e.Item.Cells[2].Text);
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from TRAINING_SCHEDULE where SCHEDULE_CODE = '" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                Response.Redirect("TRAINING_SCHEDULE_LIST.ASPX");
            }

            if (e.CommandName == "Participant")
            {
                Response.Redirect("TRAINING_SCHEDULE_ABSENCE.ASPX?SCHEDULE_CODE=" + e.Item.Cells[2].Text);
            }
        }

        protected void LoadRecord(string code)
        {
            TR_BUTTONS.Visible = true;

            CB_PUB.Checked = false;
            CB_VACC.Checked = false;

            conn.QueryString = "exec SP_TRAINING_SCHEDULE '" + code + "'";
            conn.ExecuteQuery();

            LB_SCHEDULE_CODE.Text = code;
            TXT_START_DATE.Text = conn.GetFieldValue("START_DATE").ToString();
            TXT_END_DATE.Text = conn.GetFieldValue("END_DATE").ToString();
            TXT_DURATION.Text = conn.GetFieldValue("DURATION").ToString();
            TXT_QUOTA.Text = conn.GetFieldValue("QUOTA").ToString();
            TXT_LOCATION.Text = conn.GetFieldValue("LOCATION").ToString();

            try
            {
                DDL_TRAINING.SelectedValue = conn.GetFieldValue("TRAINING_CODE").ToString();
            }
            catch { }

            try
            {
                DDL_CITY.SelectedValue = conn.GetFieldValue("CITY_CODE").ToString();
            }
            catch { }

            try
            {
                DDL_MODE.SelectedValue = conn.GetFieldValue("MODE").ToString();
            }
            catch { }

            try
            {
                DDL_HH_START.SelectedValue = conn.GetFieldValue("PRESENCE_TIME_START_HH").ToString();
            }
            catch { }

            try
            {
                DDL_MM_START.SelectedValue = conn.GetFieldValue("PRESENCE_TIME_START_MM").ToString();
            }
            catch { }

            try
            {
                DDL_HH_END.SelectedValue = conn.GetFieldValue("PRESENCE_TIME_END_HH").ToString();
            }
            catch { }

            try
            {
                DDL_MM_END.SelectedValue = conn.GetFieldValue("PRESENCE_TIME_END_MM").ToString();
            }
            catch { }

            if (conn.GetFieldValue("PUBLISH").ToString() == "1")
                CB_PUB.Checked = true;

            if (conn.GetFieldValue("ACTIVE").ToString() == "1")
                CB_VACC.Checked = true;

            LoadSubModule();
            BT_NEW.Visible = true;
        }


        protected void LoadSubModule()
        {
            LBL_TITLE.Text = BT_SUB.Text;
            IF.Src = "TRAINING_SCHEDULE_SUBMODULE.aspx?SCHEDULE_CODE=" + LB_SCHEDULE_CODE.Text;
        }

        protected void BT_CHANNEL_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            IF.Src = "TRAINING_PARTICIPANT_CHANNEL.aspx?SCHEDULE_CODE=" + LB_SCHEDULE_CODE.Text;
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void BT_SUB_Click(object sender, EventArgs e)
        {
            LoadSubModule();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect("TRAINING_SCHEDULE_LIST.ASPX");
        }

    }
}
