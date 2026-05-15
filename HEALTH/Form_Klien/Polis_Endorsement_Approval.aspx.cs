using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Endorsement_Approval : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {

        }

        protected void FillDGR()
        {
            string where = "";

            conn.QueryString = "select " +
                                "a.ID, " +
                                "a.SEQ,  " +
                                "POLICY_NO = '<a href=''Polis_Endorsement_Approval_Frame.aspx?ID=' +a.ID+ '&SEQ=' +convert(varchar(10),a.SEQ)+ '''>' + POLICY_NO + '</a>',  " +
                                "COMPANY_NAME,  " +
                                "PERIOD = convert(varchar(20),START_DATE,106) + ' - ' + convert(varchar(20),END_DATE,106),  " +
                                "CREATEBY,  " +
                                "CREATEDATE  " +
                                "from V_ALTER_POLICY_PERIOD a  " +
                                "left join (select distinct POLICY_PERIOD_ID, SEQ from V_ALTER_PERIOD_PACKAGE_NOT_COMPLETED) b on a.ID = b.POLICY_PERIOD_ID and a.SEQ = b.SEQ " +
                                "where  " +
                                "b.POLICY_PERIOD_ID is null and a.APPROVEBY is null " + where +
                                "order by " +
                                "COMPANY_NAME, ID, SEQ";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RECORD.Text = conn.GetRowCount().ToString() + " Records";

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                btDEL.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk DELETE ?')){return false;};");
            }
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
            if (e.CommandName == "Delete")
            {
                LB_ERROR.Text = "";

                try
                {
                    conn.QueryString = "exec SP_ALTER_POLICY_PERIOD_ROLLBACK '" + e.Item.Cells[0].Text + "'," + e.Item.Cells[2].Text;
                    conn.ExecuteNonQuery();
                }
                catch (System.Exception ex)
                {
                    LB_ERROR.Text = ex.Message;
                    return;
                }

                try
                {
                    FillDGR();
                }
                catch
                {
                    DGR.CurrentPageIndex = 0;
                    FillDGR();
                }
            }
        }
    }
}