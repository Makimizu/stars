using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace GLIFE.Form_App
{
    public partial class AppPOS : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TRACK.Text = Request.QueryString["seq"].ToString();
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from V_LINK_UB_PR_ENDORSEMENT_TYPE";
            conn.ExecuteQuery(150000);
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";


            if (TXT_COMPANY.Text.Trim() != "")
                where = where + " and COMPANY_NAME like '%" + TXT_COMPANY.Text.Trim() + "%' ";

            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_REGNO.Text.Trim() != "")
                where = where + " and REGNO like '%" + TXT_REGNO.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_REGDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.REG_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_REGDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.REG_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_TYPE.SelectedValue != "")
                where = where + " and a.ENDORSEMENT_TYPE = '" + DDL_TYPE.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "a.REGNO, " +
                                "a.SEQ, " +
                                "a.ENDORSEMENT_TYPE, " +
                                "a.FULLNAME, " +
                                "DOB = convert(varchar(20), a.DOB, 106), " +
                                "REG_DATE = convert(varchar(20), a.REG_DATE, 106), " +
                                "a.TC_DESCR, " +
                                "a.POLICY_NO, " +
                                "a.COMPANY_NAME, " +
                                "AR_AMOUNT = replace(convert(varchar(100), convert(money, a.AR_AMOUNT),1), '.00',''), " +
                                "AP_AMOUNT = replace(convert(varchar(100), convert(money, a.AP_AMOUNT),1), '.00',''), " +
                                "SETOFF = replace(convert(varchar(100), convert(money, a.SETOFF),1), '.00',''), " +
                                "a.ENDORSEMENT_TYPE_DESCR " +
                                "from V_APPLICATION_ENDORSEMENT_MASTER a " +
                                "where " +
                                "a.LAST_TRACK = " + LB_TRACK.Text + " " + where + " " +
                                "order by a.REG_DATE desc";
            conn.ExecuteQuery(150000);

            LB_RESULT.Text = conn.GetRowCount().ToString() + " Records";
            int MaxCount = DGR.PageSize;
            if (conn.GetRowCount() <= MaxCount)
                DGR.AllowPaging = false;

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR.Items[i].FindControl("LBT_REGNO");
                Button btDELETE = (Button)DGR.Items[i].FindControl("BT_DEL");

                lbCODE.Text = DGR.Items[i].Cells[4].Text;
                btDELETE.Attributes.Add("onclick", "if(!confirm('Are you sure to ROLLBACK ?')){return false;};");
            }



        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("AppPOSFrame.aspx?REGNO=" + e.Item.Cells[1].Text + "&SEQ=" + e.Item.Cells[2].Text + "&TYPE=" + e.Item.Cells[3].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_ROLLBACK '" + e.Item.Cells[1].Text + "'," + e.Item.Cells[2].Text + ",'" + e.Item.Cells[3].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                }
                catch { }
            }

            if (e.CommandName == "Execute")
            {
                int nexttrack = int.Parse(LB_TRACK.Text);
                nexttrack = nexttrack + 1;

                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    if (cb.Checked)
                    {
                        if (nexttrack == 4)
                        {
                            try
                            {
                                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_APV_VALIDATION " +
                                             "'" + DGR.Items[i].Cells[1].Text + "'," +
                                             DGR.Items[i].Cells[2].Text + "," +
                                             "'" + DGR.Items[i].Cells[3].Text + "'," +
                                             "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                                conn.ExecuteQuery(150000);


                                if (conn.GetRowCount() > 0)
                                    continue;
                            }
                            catch
                            {
                                continue;
                            }                            
                        }

                        try
                        {
                            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MASTER_NEXT_TRACK " +
                                                "'" + DGR.Items[i].Cells[1].Text + "-" + DGR.Items[i].Cells[2].Text + "-" + DGR.Items[i].Cells[3].Text + "'," +
                                                "'" + nexttrack.ToString() + "'," +
                                                "''," +
                                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                            conn.ExecuteNonQuery();
                        }
                        catch { }
                    }
                }

                FillDGR();
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