using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Member
{
    public partial class GPA_Peserta_UpdateDate_List : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_ID.Text = Request.QueryString["BATCH_ID"];
                FillDGRTEMP();
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

        protected void FillDGRTEMP()
        {

            conn.QueryString = "exec SP_GPA_PESERTA_UPDATEDATA_TEMP '" + LB_ID.Text + "'";
            conn.ExecuteQuery();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

            DGR_TEMP.DataSource = conn.GetDataTable();
            DGR_TEMP.DataBind();

            for (int i = 0; i < DGR_TEMP.Items.Count; i++)
            {
                Label lbREGNO = (Label)DGR_TEMP.Items[i].FindControl("LB_REGNO");
                Label lbNAMA = (Label)DGR_TEMP.Items[i].FindControl("LB_NAMA");
                Label lbDOB = (Label)DGR_TEMP.Items[i].FindControl("LB_DOB");
                Label lbPACKAGE = (Label)DGR_TEMP.Items[i].FindControl("LB_PACKAGE");
                Label lbGROUPAGE = (Label)DGR_TEMP.Items[i].FindControl("LB_GROUPAGE");
                Label lbNEWGROUPAGE = (Label)DGR_TEMP.Items[i].FindControl("LB_NEWGROUPAGE");

                DataGrid dgrDETAIL = (DataGrid)DGR_TEMP.Items[i].FindControl("DGR_ENDORS_DETAIL");

                lbREGNO.Text = DGR_TEMP.Items[i].Cells[1].Text;
                lbNAMA.Text = DGR_TEMP.Items[i].Cells[2].Text;
                lbDOB.Text = DGR_TEMP.Items[i].Cells[3].Text;
                lbPACKAGE.Text = DGR_TEMP.Items[i].Cells[4].Text;
                lbGROUPAGE.Text = DGR_TEMP.Items[i].Cells[5].Text;
                lbNEWGROUPAGE.Text = DGR_TEMP.Items[i].Cells[6].Text;

                Button bt = (Button)DGR_TEMP.Items[i].FindControl("BT_DEL");
                bt.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk TAKE OUT ?')){return false;};");

                conn.QueryString = "exec SP_GPA_PESERTA_UPDATEDATA_TEMP_DeTAIL " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + DGR_TEMP.Items[i].Cells[1].Text + "'";
                conn.ExecuteQuery();
                dgrDETAIL.DataSource = conn.GetDataTable();
                dgrDETAIL.DataBind();

                for (int j = 0; j < dgrDETAIL.Items.Count; j++)
                {
                    Label lbOLDVAL = (Label)dgrDETAIL.Items[j].FindControl("LB_OLDVAL");
                    Label lbNEWVAL = (Label)dgrDETAIL.Items[j].FindControl("LB_NEWVAL");

                    if (dgrDETAIL.Items[j].Cells[0].Text == "FINANCIAL")
                    {
                        dgrDETAIL.Items[j].Cells[0].ForeColor = System.Drawing.Color.Red;
                    }

                    if (dgrDETAIL.Items[j].Cells[1].Text == "0")
                    {
                        lbOLDVAL.Text = dgrDETAIL.Items[j].Cells[3].Text;
                        lbNEWVAL.Text = dgrDETAIL.Items[j].Cells[4].Text;
                    }
                    else
                    {
                        try
                        {
                            conn.QueryString = dgrDETAIL.Items[j].Cells[3].Text;
                            conn.ExecuteQuery();
                            lbOLDVAL.Text = conn.GetFieldValue(0, 0).ToString();
                        }
                        catch { }

                        try
                        {
                            conn.QueryString = dgrDETAIL.Items[j].Cells[4].Text;
                            conn.ExecuteQuery();
                            lbNEWVAL.Text = conn.GetFieldValue(0, 0).ToString();
                        }
                        catch { }
                    }
                }
            }

            if (isApproved())
            {
                DGR_TEMP.Columns[0].Visible = false;
            }
        }

        protected void DGR_TEMP_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                LB_ERROR.Text = "";

                try
                {
                    conn.QueryString = "exec SP_GPA_PESERTA_UPDATEDATA_PROSES_DELETE '" + LB_ID.Text + "','" + e.Item.Cells[1].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                    return;
                }

                FillDGRTEMP();
            }
        }

        protected void DGR_TEMP_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                //conn.QueryString = "select OFFSET = replace(convert(varchar(100),convert(money,SUM(NEW_PREMIUM)+SUM(CHARGE)-SUM(OLD_PREMIUM)),1),'.00','') " +
                //                    "from V_PESERTA_UPDATEDATA_TEMP " +
                //                    "where " +
                //                    "BATCH_ID = '" + LB_ID.Text + "'";
                //yulia updated 2023/09/14
                conn.QueryString = "select " +
                                    "OFFSET = replace(convert(varchar(100),convert(money,SUM(NEW_PREMIUM)-SUM(OLD_PREMIUM)),1),'.00',''), " +
                                    "CHARGE = replace(convert(varchar(100),convert(money,SUM(CHARGE)),1),'.00','') " +
                                    "from V_PESERTA_UPDATEDATA_TEMP " +
                                    "where " +
                                    "BATCH_ID = '" + LB_ID.Text + "'";
                conn.ExecuteQuery();

                e.Item.Cells[10].Text = "TOTAL";
                e.Item.Cells[11].Text = conn.GetFieldValue("OFFSET").ToString();
                e.Item.Cells[12].Text = conn.GetFieldValue("CHARGE").ToString();
            }
        }
    }
}