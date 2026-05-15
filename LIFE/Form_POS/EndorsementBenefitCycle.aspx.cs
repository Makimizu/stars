using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_POS
{
    public partial class EndorsementBenefitCycle : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();

                Setup();
                FillDGRBenefit();
                CheckTrack();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "DESCR	= e.DESCR + '<BR><B>' + UPPER(d.DESCR) + '</B>' " +
                                "from		UWBOX.dbo.PARAM_ENDORSEMENT d  " +
                                "inner join	UWBOX.dbo.PR_ENDORSEMENT_GROUP e on d.GROUP_CODE = e.CODE " +
                                "where " +
                                "d.CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();
        }

        protected void FillDGRBenefit()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_BENEFIT_CYCLE_VALIDATE " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            LB_ALERT.Text = conn.GetFieldValue("DEATH_CONDITION").ToString();

            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_BENEFIT_CYCLE_INSERT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_BENEFIT_CYCLE " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_BENEFIT.DataSource = dt;
            DGR_BENEFIT.DataBind();

            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_DATE");
                CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB");
                if (DGR_BENEFIT.Items[i].Cells[5].Text == "1")
                    cb.Checked = true;

                if (DGR_BENEFIT.Items[i].Cells[1].Text == "0")
                {
                    cb.Enabled = false;
                    ddl.Enabled = false;
                    conn.QueryString = "select '" + DGR_BENEFIT.Items[i].Cells[2].Text + "', convert(varchar(20), convert(date, '" + DGR_BENEFIT.Items[i].Cells[2].Text + "'), 106)";
                    conn.ExecuteQuery();
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(0, 1).ToString(), conn.GetFieldValue(0, 0).ToString()));
                }
                else
                {
                    string thedate_before = "null";
                    string thedate_after = "null";
                    if (DGR_BENEFIT.Items[i].Cells[3].Text.Replace("&nbsp;", "") != "")
                        thedate_before = "'" + DGR_BENEFIT.Items[i].Cells[3].Text.Replace("&nbsp;", "") + "'";
                    if (DGR_BENEFIT.Items[i].Cells[4].Text.Replace("&nbsp;", "") != "")
                        thedate_after = "'" + DGR_BENEFIT.Items[i].Cells[4].Text.Replace("&nbsp;", "") + "'";

                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_BENEFIT_CYCLE_DATE_RANGE " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + DGR_BENEFIT.Items[i].Cells[0].Text.Replace("&nbsp;", "") + "'," +
                                        thedate_before + "," +
                                        thedate_after;
                    conn.ExecuteQuery();
                    for (int j = 0; j < conn.GetRowCount(); j++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));

                    try
                    {
                        ddl.SelectedValue = DGR_BENEFIT.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                        if (DGR_BENEFIT.Items[i].Cells[0].Text != ddl.SelectedItem.Text)
                        {
                            ddl.BackColor = System.Drawing.Color.Pink;
                            ddl.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    catch { }
                }


            }
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                DGR_BENEFIT.Enabled = false;
                BT_SAVE.Visible = false;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_BENEFIT.Items.Count; i++)
            {
                if (DGR_BENEFIT.Items[i].Cells[1].Text == "0")
                    continue;

                DropDownList ddl = (DropDownList)DGR_BENEFIT.Items[i].FindControl("DDL_DATE");
                CheckBox cb = (CheckBox)DGR_BENEFIT.Items[i].FindControl("CB");

                string taken = "0";
                if (cb.Checked)
                    taken = "1";
                /*
                conn.QueryString = "update APPLICATION_ENDORSEMENT_BENEFIT_CYCLE set " +
                                    "NEWDATE                = '" + ddl.SelectedValue + "'," +
                                    "TOBEPAID               = " + taken + "," +
                                    "LASTCHANGEBY           = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "LASTCHANGEDATE         = GETDATE() " +
                                    "where " +
                                    "REGNO                  = '" + LB_REGNO.Text + "' " +
                                    "and SEQ                = '" + LB_SEQ.Text + "' " +
                                    "and ENDORSEMENT_TYPE   = '" + LB_TYPE.Text + "' " +
                                    "and THEDATE            = '" + DGR_BENEFIT.Items[i].Cells[0].Text + "'";
                */
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_QUOT_BENEFIT_CYCLE_UPDATE " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + DGR_BENEFIT.Items[i].Cells[9].Text + "'," +
                                    "'" + ddl.SelectedValue + "'," +
                                    taken + "," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
            }

            FillDGRBenefit();
        }
    }
}