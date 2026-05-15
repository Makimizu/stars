using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HLP.Form_Parameter
{
    public partial class ProductReinsurance : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["code"];
                FillDGR();
            }
        }

        protected void FillDGR()
        {

            conn.QueryString = "select CODE = ID, " +
                                  "[COMPANYNAME] = UPPER(COMPANY_NAME), " +
                                  "[DESCRIPTION] = DESCR, " +
                                  "[DOCNO] = DOCNO, " +
                                  "[STARTDATE] = convert(varchar(20), START_DATE, 106), " +
                                  "[TYPE] = TYPE_DESCR, " +
                                  "[OWNRETENTION] = replace(convert(varchar(100), convert(money,OWN_RETENTION),1), '.00',''), " +
                                  "[QUOTASHARE] = convert(varchar(10),QUOTA_SHARE), " +
                                  "TAKEN = (case when isnull(b.REINS_ID,'')<>'' then 1 else 0 end), " +
                                  "LOADING " +
                              "from REINSURANCE.dbo.V_TC_MASTER a " +
                              "left join PARAM_PRODUCT_REINSURANCE b on  a.ID = b.REINS_ID and b.PRODUCT_CODE='" + LB_CODE.Text + "'";

            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (DGR.Items[i].Cells[8].Text == "1")
                    cb.Checked = true;
            }

        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                    string taken = "1";
                    if (!cb.Checked)
                        taken = "0";

                    conn.QueryString = "exec SP_PARAM_PRODUCT_REINSURANCE_DETAIL " +
                                        "'" + LB_CODE.Text + "'," +
                                        "'" + DGR.Items[i].Cells[0].Text + "'," +
                                        taken;
                    conn.ExecuteNonQuery();
                    
                }

                FillDGR();
            }
        }
    }
}