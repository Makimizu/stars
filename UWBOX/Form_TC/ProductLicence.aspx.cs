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
    public partial class ProductLicence : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TXT_LICENCENO.Text = Request.QueryString["CODE"];
                Setup();

                try
                {
                    if (TXT_LICENCENO.Text != "")
                    {
                        LoadRecord(TXT_LICENCENO.Text);
                    }
                    else
                    {
                        TR_ENDDATE.Visible = false;
                        TR_OTHER.Visible = false;
                    }
                }
                catch { }
            }
        }

        protected void LoadRecord(string code)
        {
            conn.QueryString = "select " +
                                "LICENCENO, " +
                                "LICENCENAME = UPPER(LICENCENAME), " +
                                "REMARK, " +
                                "START_DATE = convert(varchar(20), START_DATE, 103), " +
                                "END_DATE = convert(varchar(20), START_DATE, 103) " +
                                "from PRODUCT_LICENCE " +
                                "where LICENCENO = '" + code + "'";
            conn.ExecuteQuery();

            TXT_LICENCENAME.Text = conn.GetFieldValue("LICENCENAME").ToString();
            TXT_REMARK.Text = conn.GetFieldValue("REMARK").ToString();
            TXT_STARTDATE.Text = conn.GetFieldValue("START_DATE").ToString();
            TXT_ENDDATE.Text = conn.GetFieldValue("END_DATE").ToString();

            LoadRecordDetail(code);

            string URL = GlobalUse.GetArsipURL(System.Configuration.ConfigurationManager.AppSettings["appid"], System.Configuration.ConfigurationManager.AppSettings["appid"] + "_PROD", code, "", "", GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID"));
            I2.Attributes.Add("src", URL);
        }

        protected void LoadRecordDetail(string code)
        {
            conn.QueryString = "exec SP_PRODUCT_LICENCE_TC '" + code + "','" + TXT_TCNAME.Text.Trim() + "','" + DDL_GROUP.SelectedValue + "'";
            conn.ExecuteQuery();


            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = null;
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (DGR.Items[i].Cells[0].Text != "0")
                {
                    cb.Checked = true;
                    DGR.Items[i].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE,DESCR from PARAM_PRODUCT_GROUP order by 2";
            conn.ExecuteQuery();
            DDL_GROUP.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_GROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            var formatInfo = new DateTimeFormatInfo() { ShortDatePattern = "dd/MM/yyyy" };

            string STARTDATE = GlobalUse.GlobalDateFormat(TXT_STARTDATE.Text.Trim(), "d/M/yyyy");
            string ENDDATE = "null";
            if (TXT_ENDDATE.Text.Trim().Length > 1)
            {
                ENDDATE = "'" + GlobalUse.GlobalDateFormat(TXT_ENDDATE.Text.Trim(), "d/M/yyyy") + "'";
            }

            try
            {
                conn.QueryString = "exec SP_PRODUCT_LICENCE_UPSERT " +
                                "'" + TXT_LICENCENO.Text.Trim() + "'," +
                                "'" + TXT_LICENCENAME.Text.Trim() + "'," +
                                "'" + TXT_REMARK.Text.Trim() + "'," +
                                "'" + STARTDATE + "'," +
                                ENDDATE + "," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteQuery();

                LoadRecord(TXT_LICENCENO.Text);
                LoadRecordDetail(TXT_LICENCENO.Text);
                TR_OTHER.Visible = true;
            }
            catch (SystemException ex)
            {
                LB_ERROR.Text = ex.Message;
            }
        }


        protected void DDL_GROUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecordDetail(TXT_LICENCENO.Text);
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            LoadRecordDetail(TXT_LICENCENO.Text);
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            string LicenceTmp = "";
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");
                if (cb == (CheckBox)sender)
                {
                    if (!((CheckBox)sender).Checked)
                    {
                        LicenceTmp = "";
                    }
                    else
                    {
                        LicenceTmp = TXT_LICENCENO.Text;
                    }

                    conn.QueryString = "update TC_MASTER set LICENCE_NO = '" + LicenceTmp + "' where CODE = '" + DGR.Items[i].Cells[1].Text + "';";
                    conn.ExecuteNonQuery();
                    //LB_TITLE.Text = conn.QueryString;
                }
            }
            LoadRecordDetail(TXT_LICENCENO.Text);
        }

        protected void TXT_TCNAME_TextChanged(object sender, EventArgs e)
        {
            LoadRecordDetail(TXT_LICENCENO.Text);
        }


    }
}