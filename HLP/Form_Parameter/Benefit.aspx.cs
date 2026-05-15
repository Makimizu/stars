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
    public partial class Benefit : System.Web.UI.Page
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
            conn.QueryString = "select a.CODE,a.DESCR from PR_BENEFIT a " +
                                "inner join PARAM_BENEFIT_SEQ b on a.CODE=b.CODE " +
                                "order by b.SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_BENEFIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_BENEFIT_UNIT_LIMIT";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_UNIT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }


            FillDGR();
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "CODE, " +
                                "DESCR, " +
                                "DESCR_ENG, " +
                                "UNIT, " +
                                "MAX " +
                                "from PARAM_BENEFIT_DETAIL a " +
                                "where " +
                                "a.BENEFIT_ID = '" +DDL_BENEFIT.SelectedValue+ "' " +
                                "order by 1";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            conn.QueryString = "select CODE,DESCR from PR_BENEFIT_UNIT_LIMIT";
            conn.ExecuteQuery();            

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txt = (TextBox)DGR.Items[i].FindControl("TXT_DSCR");
                TextBox txtEng = (TextBox)DGR.Items[i].FindControl("TXT_DSCR_ENG");
                DropDownList ddl = (DropDownList)DGR.Items[i].FindControl("DDL_UNT");
                TextBox txtMax = (TextBox)DGR.Items[i].FindControl("TXT_MX");

                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1).ToString(), conn.GetFieldValue(j, 0).ToString()));
                }

                txt.Text = DGR.Items[i].Cells[1].Text;
                txtEng.Text = DGR.Items[i].Cells[2].Text.Replace("&nbsp;", "");
                txtMax.Text = DGR.Items[i].Cells[4].Text.Replace("&nbsp;", "");
                try
                {
                    ddl.SelectedValue = DGR.Items[i].Cells[3].Text;
                }
                catch { }
            }
        }

        protected void DDL_BENEFIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                TextBox txt = (TextBox) e.Item.FindControl("TXT_DSCR");
                TextBox txtEng = (TextBox)e.Item.FindControl("TXT_DSCR_ENG");
                DropDownList ddl = (DropDownList)e.Item.FindControl("DDL_UNT");
                TextBox txtMax = (TextBox)e.Item.FindControl("TXT_MX");

                if (txt.Text.Trim() == "")
                    return;

                string max = "null";
                try
                {
                    max = "'" + float.Parse(txtMax.Text.Trim()) + "'";
                }
                catch { }

                conn.QueryString = "update PARAM_BENEFIT_DETAIL set " +
                                    "DESCR = '" + txt.Text.Trim() + "'," +
                                    "DESCR_ENG = '" + txtEng.Text.Trim() + "'," +
                                    "UNIT = '" + ddl.SelectedValue + "'," +
                                    "MAX = " + max + "," +
                                    "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "LASTCHANGEDATE = GETDATE() " +
                                    "where " +
                                    "CODE = '" + e.Item.Cells[0].Text + "'";
                conn.ExecuteNonQuery();

                FillDGR();
            }
        }

        protected void BT_INSERT_Click(object sender, EventArgs e)
        {
            if (TXT_CODE.Text.Trim() == "" || TXT_DESCR.Text.Trim() == "")
                return;

            try
            {
                string max = "null";
                try
                {
                    max = "'" + float.Parse(TXT_MAX.Text.Trim()) + "'";
                }
                catch { }

                conn.QueryString = "insert into PARAM_BENEFIT_DETAIL select " +
                                    "'" + TXT_CODE.Text.Trim() + "'," +
                                    "'" + TXT_DESCR.Text.Trim() + "'," +
                                    "'" + TXT_DESCR_ENG.Text.Trim() + "'," +
                                    "'" + DDL_UNIT.SelectedValue + "'," +
                                    "'" + DDL_BENEFIT.SelectedValue + "'," +
                                    max + "," +
                                    "1," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE()";
                conn.ExecuteNonQuery();
            }
            catch { return;  }

            FillDGR();
        }


    }
}