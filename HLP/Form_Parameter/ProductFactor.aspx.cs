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
    public partial class ProductFactor : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["code"];

                Setup();
                FillDGR();
                FillDGR2();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PR_GENDER order by CREATEDATE";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_GENDER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "select CODE,DESCR from PR_PREMIUM_FACTOR";
            conn.ExecuteQuery();

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_FACTOR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR2()
        {
            conn.QueryString = "select " +
                                "BENEFIT_ID = a.CODE, " +
                                "a.DESCR, " +
                                "VAL = isnull(b.VAL,1)*100  " +
                                "from PR_BENEFIT a " +
                                "inner join PARAM_BENEFIT_SEQ c on c.CODE=a.CODE  " +
                                "left join PARAM_PRODUCT_BENEFIT_FACTOR b on a.CODE=b.BENEFIT_ID and b.PRODUCT_CODE='" + LB_CODE.Text + "' and b.FACTOR_CODE='" + DDL_FACTOR.SelectedValue + "' " +
                                "order by " +
                                "c.SEQ";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR2.DataSource = dt;
            DGR2.DataBind();

            for (int i = 0; i < DGR2.Items.Count; i++)
            {
                TextBox txtVal = (TextBox)DGR2.Items[i].FindControl("TXT_FACTOR0");

                txtVal.Text = DGR2.Items[i].Cells[2].Text;
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select " +
                                "a.BENEFIT_ID, " +
                                "GENDER_CODE = d.CODE, " +
                                "BENEFIT = c.DESCR, " +
                                "GENDER = d.DESCR, " +
                                "VAL = a.VAL*100, " +
                                "START_AGE, " +
                                "END_AGE " +
                                "from PARAM_PRODUCT_BENEFIT_GENDER_FACTOR a " +
                                "inner join PARAM_BENEFIT_SEQ b on a.BENEFIT_ID=b.CODE " +
                                "inner join PR_BENEFIT c on a.BENEFIT_ID=c.CODE " +
                                "inner join PR_GENDER d on a.GENDER=d.CODE " +
                                "where " +
                                "a.PRODUCT_CODE='" + LB_CODE.Text + "' " +
                                "and a.GENDER='" + DDL_GENDER.SelectedValue + "' " +
                                "order by " +
                                "b.SEQ, " +
                                "d.CREATEDATE, " +
                                "a.START_AGE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtVal = (TextBox)DGR.Items[i].FindControl("TXT_FACTOR");
                Button btDel = (Button)DGR.Items[i].FindControl("BT_DEL");

                txtVal.Text = DGR.Items[i].Cells[6].Text;
                btDel.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk untuk DELETE ?')){return false;};");
            }
        }

        protected void DDL_FACTOR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR2();
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            try
            {
                conn.QueryString = "insert into PARAM_PRODUCT_BENEFIT_GENDER_FACTOR " +
                                    "select " +
                                    "'" + LB_CODE.Text + "', " +
                                    "a.CODE, " +
                                    "b.CODE, " +
                                    TXT_AGE1.Text.Trim() + ", " +
                                    TXT_AGE2.Text.Trim() + ", " +
                                    "1, " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE(), " +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "GETDATE() " +
                                    "from PR_BENEFIT a " +
                                    "join PR_GENDER b on 1=1";
                conn.ExecuteNonQuery();

                FillDGR();
            }
            catch { }
        }

        protected void DDL_GENDER_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                try
                {
                    conn.QueryString = "delete from PARAM_PRODUCT_BENEFIT_GENDER_FACTOR " +
                                        "where " +
                                        "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                        "and BENEFIT_ID = '" + e.Item.Cells[0].Text + "' " +
                                        "and GENDER = '" + e.Item.Cells[1].Text + "' " +
                                        "and START_AGE = '" + e.Item.Cells[4].Text + "'";
                    conn.ExecuteNonQuery();

                    FillDGR();
                }
                catch { }
            }

            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR.Items.Count; i++)
                {
                    TextBox txtVal = (TextBox)DGR.Items[i].FindControl("TXT_FACTOR");
                    try
                    {
                        conn.QueryString = "update PARAM_PRODUCT_BENEFIT_GENDER_FACTOR set " +
                                            "VAL = (" + txtVal.Text.Trim() + ")/100, " +
                                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "LASTCHANGEDATE = GETDATE() " +
                                            "where " +
                                            "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                            "and BENEFIT_ID = '" + DGR.Items[i].Cells[0].Text + "' " +
                                            "and GENDER = '" + DGR.Items[i].Cells[1].Text + "' " +
                                            "and START_AGE = '" + DGR.Items[i].Cells[4].Text + "'";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
                FillDGR();
            }
        }

        protected void DGR2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {
                for (int i = 0; i < DGR2.Items.Count; i++)
                {
                    TextBox txtVal = (TextBox)DGR2.Items[i].FindControl("TXT_FACTOR0");
                    try
                    {
                        conn.QueryString = "update PARAM_PRODUCT_BENEFIT_FACTOR set " +
                                            "VAL = convert(float," + txtVal.Text.Trim() + ")/100.00, " +
                                            "LASTCHANGEBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "LASTCHANGEDATE = GETDATE() " +
                                            "where " +
                                            "PRODUCT_CODE = '" + LB_CODE.Text + "' " +
                                            "and BENEFIT_ID = '" + DGR2.Items[i].Cells[0].Text + "' " +
                                            "and FACTOR_CODE = '" + DDL_FACTOR.SelectedValue + "' ";
                        conn.ExecuteNonQuery();
                    }
                    catch { }
                }
                FillDGR();
            }
        }
    }
}