using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace UWBOX.Form_TC
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
                LB_ID.Text = Request.QueryString["CODE"].ToString();
                Setup();
                if (DDL_TC_MASTER.Items.Count == 0)
                    TBL_REINS.Visible = false;
                else
                {
                    FillDGR();
                }
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select b.CODE, DESCR = b.CODE + ' - ' + b.DESCR from PARAM_PRODUCT_MASTER_TC a inner join TC_MASTER b on a.TC_ID = b.CODE where a.PRODUCT_CODE = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TC_MASTER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            conn.QueryString = "select ID, DESCR from V_LINK_REINS_TC_MASTER where TYPE='001' order by DESCR";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_TC.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            LoadLastTC();
        }

        protected void LoadLastTC()
        {
            conn.QueryString = "select top 1 REINS_TC_ID from PARAM_PRODUCT_MASTER_REINS " +
                                "where " +
                                "PRODUCT_CODE = '" + LB_ID.Text + "' " +
                                "order by START_DATE desc";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
                LoadTC(conn.GetFieldValue("REINS_TC_ID").ToString());
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "CODE = a.REINS_TC_ID, " +
                                "START_DATE = convert(varchar(20),a.START_DATE,106), " +
                                "DESCR = a.REINS_TC_ID + ' - ' + b.DESCR " +
                                "from PARAM_PRODUCT_MASTER_REINS a " +
                                "left join V_LINK_REINS_TC_MASTER b on a.REINS_TC_ID = b.ID " +
                                "where " +
                                "a.PRODUCT_CODE = '" + LB_ID.Text + "' " +
                                "and a.TC_ID = '" + DDL_TC_MASTER.SelectedValue + "'" + 
                                "order by " +
                                "a.START_DATE desc";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR.Items[i].FindControl("LBT_DATE");
                Button btDEL = (Button)DGR.Items[i].FindControl("BT_DEL");
                lbt.Text = DGR.Items[i].Cells[2].Text;
                btDEL.Attributes.Add("onclick", "if(!confirm('Are you sure to DELETE ?')){return false;};");
            }

        }

        protected void LoadTC(string code)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "<script language='javascript'>parent.productReinsBody.location.href = '../Form_Parameter/ProductReinsTC.aspx?CODE=" + code + "';</script>");
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                LoadTC(e.Item.Cells[1].Text);
            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_PRODUCT_MASTER_REINS where " +
                                        "PRODUCT_CODE = '" + LB_ID.Text + "' " +
                                        "and START_DATE = '" + e.Item.Cells[2].Text + "'";
                    conn.ExecuteNonQuery();
                    FillDGR();
                    LoadLastTC();
                }
                catch { }
            }
        }

        protected void BT_ADD_Click(object sender, EventArgs e)
        {
            if (TXT_DATE.Text.Trim() == "")
                return;

            try
            {
                conn.QueryString = "exec SP_PARAM_PRODUCT_MASTER_REINS_INSERT " +
                                    "'" + LB_ID.Text + "'," +
                                    "'" + DDL_TC_MASTER.SelectedValue + "'," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DATE.Text.Trim(), "d/M/yyyy") + "'," +
                                    "'" + DDL_TC.SelectedValue + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                conn.ExecuteNonQuery();
                FillDGR();
                LoadLastTC();
            }
            catch { }
        }

        protected void DGR_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DDL_TC_MASTER_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}