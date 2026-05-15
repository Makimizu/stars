using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Transfer
{
    public partial class RegistrationList : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                try
                {
                    TXT_LAST_TRACK.Text = Request.QueryString["track"];
                }
                catch { }

                Setup();
                DGR.CurrentPageIndex = 0;
            }
        }

        protected void Setup()
        {
            BT_APPROCE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk Approve data ?')){return false;};");
            BT_REJECT.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk Reject data ?')){return false;};");

            conn.QueryString = "select distinct " +
                                "b.CODE, " +
                                "b.APP_NAME " +
                                "from PARAM_TIPE_SETTLEMENT_TRANSFER_USAGE a " +
                                "inner join V_LINK_SEC_M_APPS b on a.APP_ID=b.CODE collate database_default";

            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_APP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            if (LB_APP.Text != "")
            {
                DDL_APP.SelectedValue = LB_APP.Text;
                DDL_APP.Enabled = false;
            }

            FillDDLTipe();

            if (LB_TIPE.Text != "")
            {
                DDL_TIPE.SelectedValue = LB_TIPE.Text;
                DDL_TIPE.Enabled = false;
            }

            if (TXT_LAST_TRACK.Text == "NEW")
            {
                BT_REJECT.Visible = false;
            }

            FillDGR();
        }

        protected void FillDDLTipe()
        {
            conn.QueryString = "select a.CODE, b.DESCR " +
                               "from PARAM_TIPE_SETTLEMENT_TRANSFER_USAGE a " +
                               "inner join PARAM_TIPE_SETTLEMENT b on a.APP_ID = b.APP_ID AND a.CODE = b.CODE " +
                               "where a.APP_ID = '" + DDL_APP.SelectedValue + "'";
            conn.ExecuteQuery();
            DDL_TIPE.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TIPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void DDL_APP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLTipe();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            LB_ERROR.Text = "";
            FillDGR();
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";

            conn.QueryString = "EXEC dbo.SP_REKENING_JURNAL_TRANSFER " +
                                "'" + DDL_APP.SelectedValue + "', " +
                                "'" + DDL_TIPE.SelectedValue + "', " +
                                "'" + TXT_LAST_TRACK.Text + "'";

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");

                if (DGR.Items[i].Cells[13].Text == "NEW")
                {
                    btDEL.Visible = true;
                    btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk Hapus data ?')){return false;};");
                }
            }
        }

        protected void BT_APPROCE_Click(object sender, EventArgs e)
        {
            string CurrTrack = Request.QueryString["track"];
            string NextTrack = "";

            if (CurrTrack == "NEW")
            {
                NextTrack = "REGISTER";
            } else if (CurrTrack == "REGISTER" )
            {
                NextTrack = "VERIFY";
            } else if (CurrTrack == "VERIFY" )
            {
                NextTrack = "APPROVAL";
            }

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "EXEC dbo.SP_REKENING_JURNAL_TRANSFER_NEXT_TRACK " +
                                            "'" + DGR.Items[i].Cells[1].Text + "', " +
                                            "'" + NextTrack + "', " +
                                            "'', " +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteQuery();

                        if (conn.GetRowCount() > 0)
                        {
                            LB_ERROR.Text = conn.GetFieldValue("error").ToString();
                            return;
                        }
                    }

                    catch (System.Exception ex)
                    {
                        LB_ERROR.Text = ex.Message;
                        return;
                    }
                }
            }

            conn.QueryString = "SELECT DATA = COUNT(ROW_ID) FROM REKENING_JURNAL_TRANSFER_LOG WHERE SENDED = 0 ";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                conn.QueryString = "EXEC dbo.SP_REKENING_JURNAL_TRANSFER_NEXT_TRACK_SEND_MAIL " +
                                "'" + NextTrack + "'";

                conn.ExecuteQuery();
            }

            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void BT_REJECT_Click(object sender, EventArgs e)
        {
            string NextTrack = "REJECT";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    try
                    {
                        conn.QueryString = "EXEC dbo.SP_REKENING_JURNAL_TRANSFER_NEXT_TRACK " +
                                            "'" + DGR.Items[i].Cells[1].Text + "', " +
                                            "'" + NextTrack + "', " +
                                            "'', " +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteQuery();

                        if (conn.GetRowCount() > 0)
                        {
                            LB_ERROR.Text = conn.GetFieldValue("error").ToString();
                            return;
                        }
                    }

                    catch (System.Exception ex)
                    {
                        LB_ERROR.Text = ex.Message;
                        return;
                    }
                }
            }

            conn.QueryString = "SELECT DATA = COUNT(ROW_ID) FROM REKENING_JURNAL_TRANSFER_LOG WHERE SENDED = 0 ";
            conn.ExecuteQuery();

            if (conn.GetRowCount() > 0)
            {
                conn.QueryString = "EXEC dbo.SP_REKENING_JURNAL_TRANSFER_NEXT_TRACK_SEND_MAIL " +
                                "'" + NextTrack + "'";

                conn.ExecuteQuery();
            }

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
            if (e.CommandName == "All")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    cb.Checked = true;
                }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_REKENING_JURNAL_TRANSFER_ROLLBACK " +
                                        "'" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteQuery();
                    FillDGR();

                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                }
            }
        }
    }
}