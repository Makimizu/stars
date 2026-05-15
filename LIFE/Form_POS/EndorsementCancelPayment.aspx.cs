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
    public partial class EndorsementCancelPayment : System.Web.UI.Page
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

                FillDGR();
            }
        }

        protected void FillDGR()
        {
            LB_REMARK.Text = "";

            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_CANCEL_PAYMENT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'," +
                                "0";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                if (DGR.Items[i].Cells[1].Text == "1")
                {
                    cb.Checked = true;
                }
            }

            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_CANCEL_PAYMENT " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'," +
                                "1";
            conn.ExecuteQuery();
            LB_REMARK.Text = conn.GetFieldValue("REMARK").ToString();
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb == (CheckBox)sender)
                {
                    //try
                    //{
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_CANCEL_PAYMENT_INSERT " +
                                        "'" + LB_REGNO.Text + "'," +
                                        "'" + LB_SEQ.Text + "'," +
                                        "'" + LB_TYPE.Text + "'," +
                                        "'" + DGR.Items[i].Cells[2].Text + "'," +
                                        "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                    //}
                    //catch { }                    
                    return;
                }
            }
        }
    }
}