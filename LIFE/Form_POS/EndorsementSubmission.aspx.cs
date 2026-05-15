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
    public partial class EndorsementSubmission : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["regno"].ToString();
                LB_SEQ.Text = Request.QueryString["seq"].ToString();
                LB_TYPE.Text = Request.QueryString["type"].ToString();

                Setup();
                FillDGR();
                CheckTrack();
            }
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                DGR.Enabled = false;
                BT_SAVE.Visible = false;
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_TEMP " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            if (conn.GetRowCount() == 0)
            {
                return;
            }

            DGR.DataSource = conn.GetDataTable().Copy();
            DGR.DataBind();

            for (int j = 0; j < DGR.Items.Count; j++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR.Items[j].FindControl("TXT_VAL");
                TextBox txtDATE = (TextBox)DGR.Items[j].FindControl("TXT_DATE");

                if (DGR.Items[j].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    ddl.Visible = true;
                    conn.QueryString = DGR.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    for (int k = 0; k < conn.GetRowCount(); k++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                    try
                    {
                        ddl.SelectedValue = DGR.Items[j].Cells[1].Text.Replace("&nbsp;", "");
                    }
                    catch { }

                    switch (DGR.Items[j].Cells[2].Text)
                    {
                        case "INT": ddl.Width = 80; break;
                        case "FLO": ddl.Width = 80; break;
                    }
                }
                else
                {
                    txtVAL.Visible = true;
                    switch (DGR.Items[j].Cells[2].Text)
                    {
                        case "STR": txtVAL.Text = DGR.Items[j].Cells[1].Text.Replace("&nbsp;", "");
                            break;
                        case "INT": txtVAL.Text = DGR.Items[j].Cells[1].Text.Replace("&nbsp;", "");
                            txtVAL.Width = 150;
                            txtVAL.Attributes.Add("onkeypress", "return CheckNumeric();");
                            break;
                        case "FLO":
                            try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR.Items[j].Cells[1].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                            }
                            catch { }
                            txtVAL.Text = DGR.Items[j].Cells[1].Text.Replace("&nbsp;", "");
                            txtVAL.Width = 150;
                            txtVAL.Attributes.Add("onkeyup", "FormatCurrency(this);");
                            break;
                        case "BIT": txtVAL.Visible = false;
                            ddl.Visible = true;
                            ddl.Items.Add(new ListItem("", ""));
                            ddl.Items.Add(new ListItem("YES", "1"));
                            ddl.Items.Add(new ListItem("NO", "0"));
                            try
                            {
                                ddl.SelectedValue = DGR.Items[j].Cells[1].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                            break;
                        case "DATE": txtVAL.Visible = false;
                            ddl.Visible = false;
                            txtDATE.Visible = true;
                            txtDATE.Text = DGR.Items[j].Cells[1].Text.Replace("&nbsp;", "");
                            break; ;
                    }
                }
            }

            conn.QueryString = "select GROUP_CODE from UWBOX.dbo.PARAM_ENDORSEMENT where CODE = '" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetFieldValue(0, 0).ToString() == "MBR")
            {
                DGR.Columns[6].Visible = false;
                DGR.ShowHeader = false;
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

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < DGR.Items.Count; j++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR.Items[j].FindControl("TXT_VAL");
                TextBox txtDATE = (TextBox)DGR.Items[j].FindControl("TXT_DATE");

                string val = "";

                if (ddl.Visible)
                {
                    val = ddl.SelectedValue;
                }

                if (txtVAL.Visible)
                {
                    val = txtVAL.Text.Trim();
                }

                if (txtDATE.Visible)
                {
                    val = txtDATE.Text.Trim();

                    try
                    {
                        conn.QueryString = "select convert(date, '" + val + "',103)";
                        conn.ExecuteNonQuery();
                    }
                    catch
                    { val = ""; }
                }

                if (DGR.Items[j].Cells[2].Text == "INT" || DGR.Items[j].Cells[2].Text == "FLO")
                {
                    try
                    {
                        conn.QueryString = "select convert(float, " + val.Replace(",", "") + ")";
                        conn.ExecuteNonQuery();
                    }
                    catch
                    { val = ""; }
                }

                conn.QueryString = "update APPLICATION_ALTERATION_DETAIL set NEW_VAL = '" + val + "' where " +
                                        "REGNO                  = '" + LB_REGNO.Text + "' " +
                                        "and SEQ                = " + LB_SEQ.Text + " " +
                                        "and ENDORSEMENT_TYPE   = '" + LB_TYPE.Text + "' " +
                                        "and CODE               = '" + DGR.Items[j].Cells[0].Text + "'";
                conn.ExecuteNonQuery();
            }

            conn.QueryString = "exec SP_APPLICATION_ALTERATION_DETAIL_INSERT_EXT " +
                                "'" + LB_REGNO.Text + "'," +
                                LB_SEQ.Text + "," +
                                "'" + LB_TYPE.Text + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();

            FillDGR();

            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.endorsementsubmissionbody.location.href = 'EndorsementSubmissionPreview" + LB_TYPE.Text + ".aspx?regno=" + LB_REGNO.Text + "&seq=" + LB_SEQ.Text + "&type=" + LB_TYPE.Text + "';</script>");
        }
    }
}