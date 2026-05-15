using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Web.UI.HtmlControls;

namespace HEALTH.Form_Klien
{
    public partial class Polis_Period_TPA : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_PERIOD.Text = Request.QueryString["PolicyPeriod"];
                LB_TITLE.Text = DDL_SHOW.SelectedItem.Text;
                FillCharge();
                FillPlanCode();
                FillBenefitMapping();
            }
        }

        protected void FillCharge()
        {
            conn.QueryString = "select " +
                                "b.DESCR, " +
                                "d.CORPORATE_CODE, " +
                                "BASIC_CHG = replace(convert(varchar(100),convert(money,a.BASIC_CHG),1),'.00',''), " +
                                "EXT_CHG1 = replace(convert(varchar(100),convert(money,a.EXT_CHG1),1),'.00',''), " +
                                "EXT_CHG2 = replace(convert(varchar(100),convert(money,a.EXT_CHG2),1),'.00','') " +
                                "from POLICY_PERIOD_TPA a " +
                                "inner join PARAM_ACT_TPA b on a.TPA=b.CODE " +
                                "inner join POLICY_PERIOD c on a.POLICY_PERIOD_ID = c.ID " +
                                "left join POLICY_TPA_MAPPING d on c.POLICY_ID = d.POLICY_ID and c.TPA = d.TPA " +
                                "where a.POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "'";
            conn.ExecuteQuery();

            LB_TPA.Text = conn.GetFieldValue("DESCR").ToString();
            LB_CORPORATECODE.Text = conn.GetFieldValue("CORPORATE_CODE").ToString();
            TXT_BASIC_CHG.Text = conn.GetFieldValue("BASIC_CHG").ToString();
            TXT_EXT_CHG1.Text = conn.GetFieldValue("EXT_CHG1").ToString();
            TXT_EXT_CHG2.Text = conn.GetFieldValue("EXT_CHG2").ToString();
        }

        protected void FillPlanCode()
        {
            conn.QueryString = "select * from V_POLICY_PERIOD_PACKAGE_PLAN_TPA_CODE " +
                                "where " +
                                "POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "' " +
                                "order by BENEFIT_SEQ,PACKAGE_SEQ";
            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR_PLANCODE.DataSource = dt;
            DGR_PLANCODE.DataBind();


            for (int i = 0; i < DGR_PLANCODE.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR_PLANCODE.Items[i].FindControl("TXT_PLANCODE");
                txt.Text = DGR_PLANCODE.Items[i].Cells[3].Text.Replace("&nbsp;", "");
            }
        }

        protected void FillBenefitMapping()
        {
            conn.QueryString = "select * from V_POLICY_PERIOD_TPA_BENEFIT_MAPPING where POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "' order by ID";
            conn.ExecuteQuery();

            DataTable dt = new DataTable();
            dt = conn.GetDataTable();
            DGR.DataSource = dt;
            DGR.DataBind();



            for (int i = 0; i < DGR.Items.Count; i++)
            {
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_BENCODE_TPA");

                conn.QueryString = "select ID, ID + ' - ' + DIS_BENEFIT_DETAIL_NAME from PARAM_ACT_BENEFIT_DETAIL where " +
                                    "ID <> '" + DGR.Items[i].Cells[0].Text + "' " +
                                    "and DIS_BENEFIT_DETAIL_NAME not like '%MAX%'  " +
                                    "and DIS_BENEFIT_DETAIL_NAME not like '%null%'  " +
                                    "and DIS_BENEFIT_DETAIL_NAME not like '%uno %'  " +
                                    "and DIS_BENEFIT_DETAIL_NAME not like '%idle%'  " +
                                    "order by ID";
                conn.ExecuteQuery();
                ddl.Items.Add(new ListItem("", ""));
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                try
                {
                    ddl.SelectedValue = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                }
                catch { }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                //try
                //{
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_BENCODE_TPA");

                conn.QueryString = "exec SP_POLICY_PERIOD_TPA_BENEFIT_MAPPING_UPSERT " +
                                    "'" + LB_PERIOD.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'," +
                                    "'" + ddl.SelectedValue + "'";
                conn.ExecuteNonQuery();
                FillBenefitMapping();
                //}
                //catch { }
            }
        }

        protected void DDL_SHOW_SelectedIndexChanged(object sender, EventArgs e)
        {
            LB_TITLE.Text = DDL_SHOW.SelectedItem.Text;

            for (int i = 1; i < 11; i++)
            {
                HtmlTableRow tr = (HtmlTableRow)this.FindControl("TR" + i.ToString());
                try
                {
                    tr.Visible = false;
                }
                catch { }
            }

            HtmlTableRow trv = (HtmlTableRow)this.FindControl("TR" + (DDL_SHOW.SelectedIndex + 1).ToString());
            try
            {
                trv.Visible = true;                
            }
            catch { }
        }

        protected void DGR_PLANCODE_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                TextBox txt = (TextBox)e.Item.FindControl("TXT_PLANCODE");
                try
                {
                    conn.QueryString = "update POLICY_PERIOD_PACKAGE_PLAN set " +
                                        "PLAN_CODE_TPA = '" + txt.Text.Trim() + "' " +
                                        "where " +
                                        "ID = '" + e.Item.Cells[0].Text + "'";
                    conn.ExecuteNonQuery();
                }
                catch { }
                FillPlanCode();
            }
        }

        protected void BT_CHARGE_SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "update POLICY_PERIOD_TPA set " +
                                    "BASIC_CHG = " + TXT_BASIC_CHG.Text.Trim().Replace(",", "") + "," +
                                    "EXT_CHG1 = " + TXT_EXT_CHG1.Text.Trim().Replace(",", "") + "," +
                                    "EXT_CHG2 = " + TXT_EXT_CHG2.Text.Trim().Replace(",", "") + " " +
                                    "where " +
                                    "POLICY_PERIOD_ID = '" + LB_PERIOD.Text + "'";
                conn.ExecuteNonQuery();
            }
            catch { }

            FillCharge();
        }
    }
}