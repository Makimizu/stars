using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Globalization;
using System.Configuration;
using System.Data;

namespace UWBOX.Form_TC
{
    public partial class ProductGroupEndorsement : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "CODE		= a.CODE, " +
                                "DESCR		= a.DESCR, " +
                                "PAYDI		= (case when a.PAYDI = 0 then 'NON PAYDI' else 'PAYDI' end), " +
                                "UNITIZE	= (case when a.UNITIZE = 0 then 'NON UNITIZE' else 'UNITIZE' end), " +
                                "SEGMENT	= (case when a.SEGMENT = 0 then 'INDIVIDU' else 'GROUP' end) " +
                                "from		PARAM_PRODUCT_GROUP a " +
                                "order by 2";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_GROUP.DataSource = null;
            DGR_GROUP.DataSource = dt;
            DGR_GROUP.DataBind();

            for (int i = 0; i < DGR_GROUP.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR_GROUP.Items[i].FindControl("LB_SELECT");
                lb.Text = DGR_GROUP.Items[i].Cells[1].Text;
            }

            FillDGREndorsement(DGR_GROUP.Items[0].Cells[0].Text, DGR_GROUP.Items[0].Cells[1].Text);
        }

        protected void FillDGREndorsement(string code, string descr)
        {
            LB_TITLE.Text = descr;

            conn.QueryString = "exec SP_PARAM_ENDORSEMENT_MATRIX '" + code + "'";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_ENDORSEMENT.DataSource = null;
            DGR_ENDORSEMENT.DataSource = dt;
            DGR_ENDORSEMENT.DataBind();

            for (int i = 0; i < DGR_ENDORSEMENT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_ENDORSEMENT.Items[i].FindControl("CB");
                if (DGR_ENDORSEMENT.Items[i].Cells[0].Text != "0")
                {
                    cb.Checked = true;
                }
            }
        }

        protected void DGR_GROUP_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                FillDGREndorsement(e.Item.Cells[0].Text, e.Item.Cells[1].Text);
            }
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PARAM_ENDORSEMENT_MATRIX where PRODUCT_GROUP_CODE = '" + DGR_ENDORSEMENT.Items[0].Cells[2].Text + "'";
            conn.ExecuteNonQuery();

            for (int i = 0; i < DGR_ENDORSEMENT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_ENDORSEMENT.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    conn.QueryString = "insert into PARAM_ENDORSEMENT_MATRIX select '" + DGR_ENDORSEMENT.Items[i].Cells[1].Text + "','" + DGR_ENDORSEMENT.Items[i].Cells[2].Text + "'";
                    conn.ExecuteNonQuery();
                }
            }

            FillDGREndorsement(DGR_ENDORSEMENT.Items[0].Cells[2].Text, LB_TITLE.Text);
        }
    }
}