using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace LIFE.Form_POS
{
    public partial class EndorsementKorespondensi : System.Web.UI.Page
    {

        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LB_SEQ.Text = Request.QueryString["SEQ"].ToString();
                LB_TYPE.Text = Request.QueryString["TYPE"].ToString();

                Setup();
                LoadAddress();
                if (DDL_ADDTYPE.SelectedValue.ToString() == "COR")
                {
                    FillDGR("ALN08");
                }
                else
                {
                    FillDGR("ALN07");
                }
            }
        }

        protected void Setup() 
        {
            if (LB_TYPE.Text != "ALF03")
            {
                LB_TITLE.Visible = true;
                conn.QueryString = "select " +
                                "DESCR	= e.DESCR + '<BR><B>' + UPPER(d.DESCR) + '</B>' " +
                                "from		UWBOX.dbo.PARAM_ENDORSEMENT d  " +
                                "inner join	UWBOX.dbo.PR_ENDORSEMENT_GROUP e on d.GROUP_CODE = e.CODE " +
                                "where " +
                                "d.CODE = '" + LB_TYPE.Text + "'";
                conn.ExecuteQuery();
                LB_TITLE.Text = conn.GetFieldValue("DESCR").ToString();
            }
            else
            {
                LB_TITLE.Visible = false;
            }

            conn.QueryString = "select CODE,DESCR from LIFE.dbo.PR_CLIENT_ADDRESS_TYPE";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_ADDTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDDLProvince();
            FillDDLCountry();
            
        }

        protected void FillDDLProvince()
        {
            DDL_PROVINCE.Items.Clear();
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_PROPINSI where DESCR like '%" + TXT_PROVINCE.Text.Trim() + "%'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDDLCountry()
        {
            DDL_COUNTRY.Items.Clear();
            conn.QueryString = "select CODE,DESCR from V_LINK_CB_PR_COUNTRY where DESCR like '%" + TXT_COUNTRY.Text.Trim() + "%'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_COUNTRY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        

        protected void LoadAddress()
        {
            TXT_ADDRESS.Text = "";
            TXT_CITY.Text = "";
            TXT_ZIPCODE.Text = "";
            TXT_PHONE1.Text = "";
            TXT_PHONE2.Text = "";
            TXT_EMAIL.Text = "";
            DDL_PROVINCE.SelectedIndex = 0;
            DDL_COUNTRY.SelectedIndex = 0;

            conn.QueryString = "exec SP_APPLICATION_ADDRESS '" + LB_REGNO.Text + "','" + DDL_ADDTYPE.SelectedValue + "'";
            conn.ExecuteQuery();

            TXT_ADDRESS.Text = conn.GetFieldValue("ADDRESS").ToString();
            TXT_CITY.Text = conn.GetFieldValue("CITY").ToString();
            TXT_ZIPCODE.Text = conn.GetFieldValue("ZIPCODE").ToString();
            TXT_PHONE1.Text = conn.GetFieldValue("PHONE").ToString();
            TXT_PHONE2.Text = conn.GetFieldValue("PHONE2").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();

            try
            {
                DDL_PROVINCE.SelectedValue = conn.GetFieldValue("PROVINCE").ToString();
            }
            catch { }

            try
            {
                DDL_COUNTRY.SelectedValue = conn.GetFieldValue("COUNTRY").ToString();
            }
            catch { }
        }

        protected void DDL_ADDTYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAddress();
            string addressType = string.Empty;
            if (DDL_ADDTYPE.SelectedValue.ToString() == "COR")
            {
                addressType = "ALN08";
            }
            else 
            {
                addressType = "ALN07";
            }
            FillDGR(addressType);

        }

        protected void TXT_PROVINCE_TextChanged(object sender, EventArgs e)
        {
            FillDDLProvince();
        }

        protected void TXT_COUNTRY_TextChanged(object sender, EventArgs e)
        {
            FillDDLCountry();
        }

        protected void BT_SAVE_ADDRESS_Click(object sender, EventArgs e)
        {
            try
            {
                string addresType = string.Empty;

                if (DDL_ADDTYPE.SelectedValue.ToString() == "COR")
                {
                    addresType = "ALN08"; // Alamat Surat Meyurat
                }
                else 
                {
                    addresType = "ALN07"; // Alamat Rumah
                }

                conn.QueryString = "EXEC SP_APPLICATION_ENDORSEMENT_MEMBER_KORESPONDENSI_DETAIL_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + TXT_ADDRESS.Text + "'," +
                                    "'" + TXT_CITY.Text.Trim() + "'," +
                                    "'" + DDL_PROVINCE.SelectedValue + "'," +
                                    "'" + DDL_COUNTRY.SelectedValue + "'," +
                                    "'" + TXT_ZIPCODE.Text.Trim() + "'," +
                                    "'" + TXT_PHONE1.Text.Trim() + "'," +
                                    "'" + TXT_PHONE2.Text.Trim() + "'," +
                                    "'" + TXT_EMAIL.Text.Trim() + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + addresType + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                    "'" + DDL_ADDTYPE.SelectedValue + "'";
                conn.ExecuteNonQuery();
                FillDGR(addresType);
                LoadAddress();
            }
            catch (Exception ex) 
            {
                ex.Message.ToString();
            }
        }

        protected void FillDGR(string addressType)
        {
            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_PERSONAL_DATA_CHANGE_2 " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + addressType + "'";
            conn.ExecuteQuery();



            if (conn.GetRowCount() == 0)
            {
                TBLADDRESSCHANGES.Visible = false;
            }
            else 
            {
                DGR.DataSource = conn.GetDataTable().Copy();
                DGR.DataBind();
                TBLADDRESSCHANGES.Visible = true;
            }
            
           
        }
    }
}