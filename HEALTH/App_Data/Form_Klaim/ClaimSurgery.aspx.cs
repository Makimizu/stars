using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace HEALTH.Form_Klaim
{
    public partial class ClaimSurgery : System.Web.UI.Page
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
                    LB_CLAIMNO.Text = Request.QueryString["CLAIM_NO"].ToString();
                }
                catch { }

                Setup();
                FillDGRSURGERY();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select LAST_TRACK from V_CLM_CLAIM_MASTER where CLAIM_NO='" + LB_CLAIMNO.Text + "'";
            conn.ExecuteQuery();
            LB_TRACK.Text = conn.GetFieldValue("LAST_TRACK").ToString();

            BT_CARI_SURGERY.Attributes.Add("onclick", "window.open('../Form_Tools/Search_Mode2.aspx?field1=CODE&field1txt=CODE&field2=PART_OF_BODY&field2txt=PART OF BODY&field3=TREATMENT&field3txt=TREATMENT&field4=CATEGORY&field4txt=CATEGORY&tablename=V_CLM_PARAM_SURGERY&target=TXT_SURGERY&parent=0&submit=0','SURGERY','height=500px,width=800px,right=0,top=0,status=no,toolbar=no,scrollbars=yes,titlebar=no,menubar=no,location=no,dependent=yes');");
        }

        protected void FillDGRSURGERY()
        {
            conn.QueryString = "SELECT B.CODE,PART_OF_BODY,TREATMENT,CATEGORY " +
                                "FROM CLAIM_SURGERY A " +
                                "INNER JOIN dbo.V_CLM_PARAM_SURGERY B ON A.SURGERY_CODE=B.CODE " +
                                "WHERE A.CLAIM_NO='" + LB_CLAIMNO.Text.Trim() + "' " +
                                "ORDER BY A.USERDATE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DIAG_AWAL.DataSource = dt;
            DGR_DIAG_AWAL.DataBind();

            if (LB_TRACK.Text == "3" || LB_TRACK.Text == "4")
            {
                TXT_SURGERY.Visible = false;
                BT_CARI_SURGERY.Visible = false;
                BT_TAMBAH_SURGERY.Visible = false;
                DGR_DIAG_AWAL.Columns[2].Visible = false;
            }
        }

        protected void BT_TAMBAH_SURGERY_Click(object sender, EventArgs e)
        {
            if (Request.Form[TXT_SURGERY.UniqueID] == "")
                return;
            try
            {
                conn.QueryString = "exec SP_CLM_CLAIM_SURGERY_SAVE " +
                                    "'" + LB_CLAIMNO.Text + "'," +
                                    "'" + Request.Form[TXT_SURGERY.UniqueID].Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();
                FillDGRSURGERY();
                TXT_SURGERY.Text = "";
            }
            catch { }
        }

        protected void DGR_DIAG_AWAL_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from CLAIM_SURGERY " +
                                        "where " +
                                        "SURGERY_CODE='" + e.Item.Cells[0].Text + "' " +
                                        "and CLAIM_NO ='" + LB_CLAIMNO.Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGRSURGERY();
                }
                catch { }
            }
        }
    }
}