using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Globalization;

namespace LIFE.Form_POS
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
                LB_TRACK.Text = Request.QueryString["track"].ToString();
                Setup();

                
                //ShowGridHeaderOnly();
                FillDGR();

            }
        }

        private void ShowGridHeaderOnly()
        {
            DataTable dt = new DataTable();

            // Add all columns that exist in your DataGrid
            dt.Columns.Add("REGNO");
            dt.Columns.Add("SEQ");
            dt.Columns.Add("POLICY_NO");
            dt.Columns.Add("FULLNAME");
            dt.Columns.Add("PRODUCT");
            dt.Columns.Add("ENDORSEMENT_TYPE_DESCR");
            dt.Columns.Add("REG_DATE");
            dt.Columns.Add("CUSTODYNAVDATE");
            dt.Columns.Add("REG_BY");
            dt.Columns.Add("STAT_TRACK");
            dt.Columns.Add("PRODUCT_GROUP");

            dt.Rows.Add(dt.NewRow());   // Add one dummy row

            DGR.DataSource = dt;
            DGR.DataBind();

            // Hide the dummy row so only header appears
            if (DGR.Items.Count > 0)
                DGR.Items[0].Visible = false;
        }


        protected void Setup()
        {
            conn.QueryString = "exec SP_LINK_UB_ENDORSEMENT_LIST";
            conn.ExecuteQuery(1500000);
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, "ENDORSEMENT_DESCR").ToString(), conn.GetFieldValue(i, "ENDORSEMENT_CODE").ToString()));
            }
        }

        protected void FillDGR()
        {

            int IntTRACK = Convert.ToInt16(LB_TRACK.Text);

            int IntTRACK1 = 0;

            if (IntTRACK == 2)
            { IntTRACK1 = 2; };

            if (IntTRACK == 3)
            { IntTRACK1 = 3; };


            if (IntTRACK == 1)
            { IntTRACK1 = 4; };

            LB_RESULT.Text = "";


            string regDate1 = "";
            string regDate2 = "";

            if (!string.IsNullOrEmpty(TXT_REGDATE1.Text))
            {
                DateTime d1 = DateTime.ParseExact(
                    TXT_REGDATE1.Text.Trim(),
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                );
                regDate1 = d1.ToString("yyyy-MM-dd");
            }

            if (!string.IsNullOrEmpty(TXT_REGDATE2.Text))
            {
                DateTime d2 = DateTime.ParseExact(
                    TXT_REGDATE2.Text.Trim(),
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                );
                regDate2 = d2.ToString("yyyy-MM-dd");
            }

            int pageNumber = DGR.CurrentPageIndex + 1;
            int pageSize = DGR.PageSize;

            conn.QueryString =
            "EXEC SP_APPLICATION_ENDORSEMENT_PARENT_POS " +
            "'" + DDL_UNITIZE.SelectedValue + "'," +
            "'" + TXT_FULLNAME.Text.Trim() + "'," +
            "'" + TXT_PRODUCT.Text.Trim() + "'," +
            "'" + TXT_POLICYNO.Text.Trim() + "'," +
            "'" + TXT_REGNO.Text.Trim() + "'," +
            "'" + DDL_TYPE.SelectedItem.Text + "'," +
            "" + IntTRACK + "," +
            (string.IsNullOrEmpty(regDate1) ? "NULL" : "'" + regDate1 + "'") + "," +
            (string.IsNullOrEmpty(regDate2) ? "NULL" : "'" + regDate2 + "'") + "," +
            pageNumber + "," +
            pageSize;


            conn.ExecuteQuery(1500000);
           

            int MaxCount = DGR.PageSize;

            DataTable dt;
            dt = new DataTable();
            int totalRow = 0;
            dt = conn.GetDataTable().Copy();
            if (dt != null && dt.Rows.Count > 0)
            {
                totalRow = Convert.ToInt32(dt.Rows[0]["TOTAL_ROW"]);
            }
            else
            {
                totalRow = 0; // atau sesuai kebutuhan
            }
            LB_RESULT.Text = totalRow.ToString() + " Records";
          
            DGR.VirtualItemCount = totalRow;     // INI PENTING
            DGR.AllowCustomPaging = true;        // INI JUGA PENTING

            if (dt.Rows.Count > 0)
            {
                DGR.DataSource = dt;
                DGR.DataBind();

                DGR.Columns[8].Visible = false;

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                    Button btDELETE = (Button)DGR.Items[i].FindControl("BT_DEL");

                    lbCODE.Text = DGR.Items[i].Cells[3].Text;

                    btDELETE.Attributes.Add("onclick", "if(!confirm('Are you sure to ROLLBACK ?')){return false;};");

                    if (int.Parse(LB_TRACK.Text) > 3)
                    {
                        btDELETE.Visible = false;
                    }
                }

                if (int.Parse(LB_TRACK.Text) == 2)
                    DGR.Columns[8].Visible = true;
            }
            else
            {
                ShowGridHeaderOnly();
            }
        }
        protected void DGR_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }
        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {

            if (e.CommandName == "Select")
            {
                conn.QueryString = "SELECT TOP 1 PARAM_VALUE, TRACK_TYPE, TRACK1_DATE, TRACK2_DATE, TRACK3_DATE, TRACK4_DATE,TRACK5_DATE " +
                                     "FROM APPLICATION_TRACK WHERE TRACK_TYPE = 'POS' AND  REGNO = '" + e.Item.Cells[1].Text + "' " +
                                     "ORDER BY TRY_CONVERT(INT, PARAM_VALUE) DESC";
                conn.ExecuteQuery();

                string paramValue = conn.GetFieldValue("PARAM_VALUE");
                string trackType = conn.GetFieldValue("TRACK_TYPE");
                string track1Date = conn.GetFieldValue("TRACK1_DATE");
                string track2Date = conn.GetFieldValue("TRACK2_DATE");
                string track3Date = conn.GetFieldValue("TRACK3_DATE");
                string track4Date = conn.GetFieldValue("TRACK4_DATE");
                string track5Date = conn.GetFieldValue("TRACK5_DATE");


                if (e.Item.Cells[8].Text == "4" && DDL_UNITIZE.SelectedValue == "0" && e.Item.Cells[9].Text != "INDIVIDUAL - UNIT LINK" && LB_TRACK.Text == "1"
                   || e.Item.Cells[8].Text == "4" && DDL_UNITIZE.SelectedValue == "0" && e.Item.Cells[9].Text != "IUL" && LB_TRACK.Text == "1"
                   || e.Item.Cells[8].Text == "99" && DDL_UNITIZE.SelectedValue == "0" && e.Item.Cells[9].Text != "INDIVIDUAL - UNIT LINK" && LB_TRACK.Text == "1"//Tidak termasuk INDIVIDUAL - UNIT LINK
                   || e.Item.Cells[8].Text == "99" && DDL_UNITIZE.SelectedValue == "0" && e.Item.Cells[9].Text != "IUL" && LB_TRACK.Text == "1") //Tidak termasuk INDIVIDUAL - UNIT LINK
                {

                    if (trackType == "POS" && track1Date != null && track2Date != null && track3Date == "" && track4Date == "" && track5Date == ""
                        || trackType == "POS" && track1Date != null && track2Date == null && track3Date == "" && track4Date == "" && track5Date == "") // Jika Sudah input sebelumnya di POS.
                    {
                        Response.Redirect("EndorsementFrame.aspx?ID=" + e.Item.Cells[1].Text + "-" + paramValue); //Passing REGNO AND SEQ
                    }
                    else if (e.Item.Cells[8].Text == "4" && DDL_UNITIZE.SelectedValue == "1" && e.Item.Cells[9].Text == "INDIVIDUAL - UNIT LINK" && LB_TRACK.Text == "1") //Tidak termasuk INDIVIDUAL - UNIT LINK
                    {
                        conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PARENT_UPSERT " +
                         "'" + e.Item.Cells[1].Text + "'," +
                         "'" + e.Item.Cells[2].Text + "'," +
                         DDL_UNITIZE.SelectedValue + "," +
                         "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteQuery();
                        string SEQ = conn.GetFieldValue("SEQ").ToString();

                        Response.Redirect("EndorsementFrame.aspx?ID=" + e.Item.Cells[1].Text + "-" + SEQ); //Passing REGNO AND SEQ
                    }
                    else
                    {
                        //If Non Unitezed
                        conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PARENT_UPSERT_REGISTRATION " +
                                             "'" + e.Item.Cells[1].Text + "'," +
                                             "'" + e.Item.Cells[2].Text + "'," +
                                             DDL_UNITIZE.SelectedValue + "," +
                                             "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                        conn.ExecuteQuery();

                        string SEQ = conn.GetFieldValue("SEQ").ToString();

                        Response.Redirect("EndorsementFrame.aspx?ID=" + e.Item.Cells[1].Text + "-" + SEQ); //Passing REGNO AND SEQ

                    }
                }
                else if (trackType == "POS" && track1Date != null && track2Date != null && track3Date == "" && track4Date == "" && track5Date == ""
                        || trackType == "POS" && track1Date != null && track2Date == null && track3Date == "" && track4Date == "" && track5Date == "") // Jika Sudah input sebelumnya di POS.
                    {
                        Response.Redirect("EndorsementFrame.aspx?ID=" + e.Item.Cells[1].Text + "-" + paramValue); //Passing REGNO AND SEQ
                    }


                else if ( e.Item.Cells[8].Text == "4" && e.Item.Cells[2].Text == "0"
                    ||  e.Item.Cells[8].Text == "99" && e.Item.Cells[2].Text == "0"
                    ||  e.Item.Cells[8].Text == "99,5" && e.Item.Cells[2].Text == "0"
                    ||  e.Item.Cells[8].Text == "99,1" && e.Item.Cells[2].Text == "0")
                {

                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PARENT_UPSERT " +
                                           "'" + e.Item.Cells[1].Text + "'," +
                                           "null," +
                                           "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                    conn.ExecuteQuery();
                    string remark = conn.GetFieldValue("REMARK").ToString();
                    if (remark.Trim() != "")
                    {
                        LB_ERROR.Text = remark;
                        ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
                        return;
                    }

                    string REGNO = e.Item.Cells[1].Text + "-" + conn.GetFieldValue("SEQ").ToString();
                    Response.Redirect("EndorsementFrame.aspx?ID=" + REGNO);

                }
                else
                {
                    Response.Redirect("EndorsementFrame.aspx?ID=" + e.Item.Cells[1].Text + "-" + e.Item.Cells[2].Text); //Passing REGNO AND SEQ
                }
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PARENT_ROLLBACK '" + e.Item.Cells[1].Text + "'," + e.Item.Cells[2].Text;
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void CB_ALL_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                cb.Checked = ((CheckBox)sender).Checked;
            }
        }
    }
}