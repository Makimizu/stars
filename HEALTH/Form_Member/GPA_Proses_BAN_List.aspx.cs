using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{

    public partial class GPA_Proses_BAN_List : System.Web.UI.Page
    {

        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["BATCH_ID"];
                FillDGR();
            }
        }

        protected bool isApproved()
        {
            bool bResult = false;

            try
            {
                conn.QueryString = "select ROW_ID from TRACK_DATA where TIPE_CODE='GPA' and OWNER='" + LB_ID.Text + "' and SEQ > 3";
                conn.ExecuteQuery();
                if (conn.GetRowCount() > 0)
                    bResult = true;
            }
            catch { }

            return bResult;
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";

            conn.QueryString = "exec SP_GPA_PESERTA_BANKACC_TEMP " +
                                "'" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button bt = (Button)DGR.Items[i].FindControl("BT_DEL");
                bt.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk TAKE OUT ?')){return false;};");

                Label lbOLDACCBANK = (Label)DGR.Items[i].FindControl("LB_OLD_BANK");
                Label lbOLDACCNO = (Label)DGR.Items[i].FindControl("LB_OLD_ACCNO");
                Label lbOLDACCNAMA = (Label)DGR.Items[i].FindControl("LB_OLD_ACCNAMA");
                Label lbNEWACCBANK = (Label)DGR.Items[i].FindControl("LB_NEW_BANK");
                Label lbNEWACCNO = (Label)DGR.Items[i].FindControl("LB_NEW_ACCNO");
                Label lbNEWACCNAMA = (Label)DGR.Items[i].FindControl("LB_NEW_ACCNAMA");

                lbOLDACCBANK.Text = DGR.Items[i].Cells[4].Text;
                lbOLDACCNO.Text = DGR.Items[i].Cells[5].Text;
                lbOLDACCNAMA.Text = DGR.Items[i].Cells[6].Text;
                lbNEWACCBANK.Text = DGR.Items[i].Cells[7].Text;
                lbNEWACCNO.Text = DGR.Items[i].Cells[8].Text;
                lbNEWACCNAMA.Text = DGR.Items[i].Cells[9].Text;

            }

            if (isApproved())
            {
                DGR.Columns[0].Visible = false;
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                LB_ERROR.Text = "";

                try
                {
                    conn.QueryString = "exec SP_GPA_PESERTA_BANKACC_PROSES_DELETE '" + LB_ID.Text + "','" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                    return;
                }

                FillDGR();
            }
        }

        protected void DGR_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                try
                {
                    e.Item.Cells[9].Text  = "";
                    e.Item.Cells[10].Text = "";
                }
                catch { }
            }
        }

    }

}