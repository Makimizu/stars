using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using AjaxControlToolkit;

namespace LIFE.Form_POS
{
    public partial class EndorsementSubmissionButton : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));     
        public int intFlag = 0;
        public Boolean flagCheck = false;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Flag"] = 0;
                LB_REGNO.Text = Request.QueryString["regno"].ToString();
                LB_SEQ.Text = Request.QueryString["seq"].ToString();
                FillDGR();
                CheckTrack(); 
            }           
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    cb.Visible = false;
                }
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER '" + LB_REGNO.Text + "'," + LB_SEQ.Text;
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_SELECT");
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                lb.Text = DGR.Items[i].Cells[1].Text;
              
                if (DGR.Items[i].Cells[2].Text == "1")
                {
                    cb.Checked = true;

                    if (new[] { "MBR", "ALN", "ALF" }.Any(p => DGR.Items[i].Cells[0].Text.Contains(p)))
                    {
                        intFlag = intFlag + 1;
                        // Store the new value back into Session
                        Session["Flag"] = intFlag;
                    }
                    flagCheck = true;
                    //else 
                    //{
                    //    Session["Flag"] = 0;
                    //}
                }
                else
                {
                    lb.Enabled = false;
                    lb.Font.Bold = false;
              
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.EndorsementSubmissionbody.location.href = '" + e.Item.Cells[3].Text + "';</script>");
            }
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbSelect = (CheckBox)sender;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                if (cb != cbSelect)
                    continue;

                string code = DGR.Items[i].Cells[0].Text;

                // VALIDASI
                if (new[] { "MBR", "SV" , "ALF01", "ALF20" }
                    .Any(p => code.Contains(p)))
                {
                    if ((int)Session["Flag"] > 0 && flagCheck == false)
                    {
                        cbSelect.Checked = false;
                        return;
                    }                    
                }

                // CHECK
                if (cbSelect.Checked)
                {
                    if (code == "ALN07")
                    {
                        string[] items = { "ALN07", "ALN08" };

                        foreach (string item in items)
                        {
                            conn.QueryString =
                                "exec SP_APPLICATION_ENDORSEMENT_MASTER_UPSERT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + item + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                            conn.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        conn.QueryString =
                            "exec SP_APPLICATION_ENDORSEMENT_MASTER_UPSERT " +
                            "'" + LB_REGNO.Text + "'," +
                            "'" + LB_SEQ.Text + "'," +
                            "'" + code + "'," +
                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                        conn.ExecuteNonQuery();
                    }
                }
                // UNCHECK
                else
                {
                    if (code == "ALN07")
                    {
                        string[] items = { "ALN07", "ALN08" };

                        foreach (string item in items)
                        {
                            conn.QueryString =
                                "exec SP_APPLICATION_ENDORSEMENT_MASTER_ROLLBACK " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + item + "'";

                            conn.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        conn.QueryString =
                            "exec SP_APPLICATION_ENDORSEMENT_MASTER_ROLLBACK " +
                            "'" + LB_REGNO.Text + "'," +
                            "'" + LB_SEQ.Text + "'," +
                            "'" + code + "'";

                        conn.ExecuteNonQuery();
                    }
                }
                FillDGR();
                return;
            }
        }
    }
}