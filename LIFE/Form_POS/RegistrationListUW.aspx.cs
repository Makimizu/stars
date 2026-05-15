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
    public partial class RegistrationListUW : System.Web.UI.Page
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
                FillDGR();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "exec SP_LINK_UB_ENDORSEMENT_LIST";
            conn.ExecuteQuery();
            DDL_TYPE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, "ENDORSEMENT_DESCR").ToString(), conn.GetFieldValue(i, "ENDORSEMENT_CODE").ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";


            if (TXT_FULLNAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_FULLNAME.Text.Trim() + "%' ";

            if (TXT_PRODUCT.Text.Trim() != "")
                where = where + " and TC_DESCR like '%" + TXT_PRODUCT.Text.Trim() + "%' ";

            if (TXT_POLICYNO.Text.Trim() != "")
                where = where + " and POLICY_NO like '%" + TXT_POLICYNO.Text.Trim() + "%' ";

            if (TXT_REGDATE1.Text.Trim() != "")
                where = where + " and convert(date,a.REG_DATE) >= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE1.Text.Trim(), "d/M/yyyy") + "' ";

            if (TXT_REGDATE2.Text.Trim() != "")
                where = where + " and convert(date,a.REG_DATE) <= '" + GlobalUse.GlobalDateFormat(TXT_REGDATE2.Text.Trim(), "d/M/yyyy") + "' ";

            if (DDL_TYPE.SelectedValue != "")
                where = where + " and a.ENDORSEMENT_TYPE_DESCR like '%" + DDL_TYPE.SelectedItem.Text + "%' ";

            conn.QueryString = "select " +
                                "REGNO                  = a.REGNO, " +
                                "SEQ                    = a.SEQ, " +
                                "FULLNAME               = a.FULLNAME, " +
                                "ENDORSEMENT_TYPE_DESCR = a.ENDORSEMENT_TYPE_DESCR, " +
                                "DOB                    = convert(varchar(20), a.DOB, 106), " +
                                "REG_DATE               = convert(varchar(20), a.REG_DATE, 106), " +
                                "REG_BY                 = a.REG_BY, " +
                                "PRODUCT                = a.PRODUCT_NAME + '<BR><B>' + a.PRODUCT_GROUP + '</B>', " +
                                "POLICY_NO              = a.POLICY_NO " +
                                "from                   V_APPLICATION_ENDORSEMENT_PARENT a " +
                                "where " +
                                "a.LAST_TRACK = " + LB_TRACK.Text + " and a.UW_VERIFICATION = 1 " + where + " " +
                                "order by a.REG_DATE desc";
            conn.ExecuteQuery();

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

                lbCODE.Text = DGR.Items[i].Cells[3].Text;
                btDELETE.Attributes.Add("onclick", "if(!confirm('Are you sure to ROLLBACK ?')){return false;};");
            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                Response.Redirect("EndorsementFrame.aspx?ID=" + e.Item.Cells[1].Text + "-" + e.Item.Cells[2].Text);
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