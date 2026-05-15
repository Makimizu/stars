using System;
using DMS.DBConnection;
using System.Configuration;
using System.Web.UI.WebControls;

namespace CUSTOMERS.Form_Client
{
    public partial class Personal : System.Web.UI.Page
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

                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_JOB";
            conn.ExecuteQuery();
            DDL_JOB.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_JOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_PROPINSI order by 2";
            conn.ExecuteQuery();
            DDL_PROVINCE.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            LB_RECORD.Text = "";
            string where = "";

            if (TXT_CITY.Text.Trim() != "")
                where = where + " and CITY like '%" + TXT_CITY.Text.Trim() + "%' ";

            if (TXT_IDNO.Text.Trim() != "")
                where = where + " and ID_NO like '%" + TXT_IDNO.Text.Trim() + "%' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and FULLNAME like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_DOBSTART.Text.Trim() != "")
                where = where + " and DOB >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DOBSTART.Text.Trim(), "d/M/yyyy") + "') ";

            if (TXT_DOBTO.Text.Trim() != "")
                where = where + " and DOB <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DOBTO.Text.Trim(), "d/M/yyyy") + "') ";

            if (DDL_GENDER.SelectedValue != "")
                where = where + " and a.SEX ='" + DDL_GENDER.SelectedValue + "' ";

            if (DDL_JOB.SelectedValue != "")
                where = where + " and a.JOB ='" + DDL_JOB.SelectedValue + "' ";

            if (DDL_PROVINCE.SelectedValue != "")
                where = where + " and a.PROVINCE ='" + DDL_PROVINCE.SelectedValue + "' ";

            conn.QueryString = "select " +
                                "ID, " +
                                "FULLNAME, " +
                                "SEX = (case when SEX='M' then 'MALE' else 'FEMALE' end), " +
                                "DOB = convert(varchar(20),DOB,106), " +
                                "JOB_DESCR, " +
                                "MARITAL_STATUS_DESCR, " +
                                "CITIZENSHIP_DESCR, " +
                                "CITY, " +
                                "PROVINCE_DESCR " +
                                "from V_MEMBER_MASTER a " +
                                "where " +
                                "1=1 " + where + " " +
                                "order by FULLNAME";
            conn.ExecuteQuery();

            DGR.DataSource = conn.GetDataTable();
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbtCode = (LinkButton)DGR.Items[i].FindControl("LB_ID");
                lbtCode.Text = DGR.Items[i].Cells[1].Text;

            }

            LB_RECORD.Text = conn.GetRowCount().ToString() + " records";
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
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
            if (e.CommandName == "Select")
            {
                Response.Redirect("PersonalDetail.aspx?ID=" + e.Item.Cells[0].Text);
            }
        }

    }
}