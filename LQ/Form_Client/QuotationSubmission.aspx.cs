using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;
using System.Globalization;

namespace LQ.Form_Client
{
    public partial class QuotationSubmission : System.Web.UI.Page
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
                    if (Request.QueryString["code"].ToString() == null || Request.QueryString["code"].ToString() == "")
                    {
                        Response.Redirect("QuotationSubmissionAgent.aspx");
                    }
                }
                catch
                {
                    Response.Redirect("QuotationSubmissionAgent.aspx");
                }

                LB_CODE.Text = Request.QueryString["code"].ToString();
                Setup();
            }
        }

        [System.Web.Services.WebMethod]
        public static string CheckMember(string name, string dob, string idno)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(dob))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(dob))
            {
                return null;
            }

            Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            conn.QueryString = "select ALERT=[LQ].[dbo].[UFN_CHECK_MAIN_INSURED_BLACKLISTED] ('" + name.Trim().ToUpper() + "', '" + dob.Trim() + "', '" + idno.Trim().ToUpper() + "')";
            conn.ExecuteQuery();

            return conn.GetFieldValue("ALERT").ToString();

        }

        protected void RegisterScript()
        {
            string script = string.Format(@"
                    function checkMember() {{
                        var fullname = document.getElementById('TXT_FULLNAME_ENTRY');
                        var dob = document.getElementById('TXT_DOB_ENTRY');
                        var idno = document.getElementById('TXT_IDNO_ENTRY');
                        var warning = document.getElementById('{0}');

                        warning.innerHTML = '';
                        warning.className = '';
        
                        $.ajax({{
                            type: ""POST"",
                            url: ""QuotationSubmission.aspx/CheckMember"",
                            data: JSON.stringify({{ name: fullname.value, dob: dob.value, idno: idno.value }}),
                            contentType: ""application/json; charset=utf-8"",
                            dataType: ""json"",
                            success: function (response) {{
                                if (!response.d || response.d.trim().length === 0) {{
                                    return;
                                }}
                                console.log(response.d);
                                warning.innerHTML = response.d;
                                warning.className = 'alert';
                            }},
                            error: function (xhr, status, error) {{
                                console.log(""Error: "" + error);
                            }}
                        }});
                    }}

                    window.onload = checkMember;
                ", LB_WARNING.ClientID);


            ClientScript.RegisterStartupScript(this.GetType(), "CheckMemberScript", script, true);
        }

        protected void Setup()
        {
            conn.QueryString = "select " +
                                "FULLNAME, " +
                                "CD_DESCR, " +
                                "SUBCD_DESCR, " +
                                "AGENCY_NAME " +
                                "from MARKETING.dbo.V_M_AGENTS  " +
                                "where  " +
                                "CODE = '" + LB_CODE.Text + "'";
            conn.ExecuteQuery();
            LB_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            LB_CHANNEL.Text = conn.GetFieldValue("CD_DESCR").ToString();
            LB_LEVEL.Text = conn.GetFieldValue("SUBCD_DESCR").ToString();
            LB_AGENCY.Text = conn.GetFieldValue("AGENCY_NAME").ToString();

            //conn.QueryString = "select SEQ from UWBOX.dbo.SC_SEQ where SEQ between 10 and 100 order by 1";
            //conn.ExecuteQuery();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //    DDL_AGE_DUMMY.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString(), conn.GetFieldValue(i, 0).ToString()));

            //DDL_AGE_DUMMY.SelectedValue = "30";

            conn.QueryString = "select distinct PRODUCT_GROUP, PRODUCT_GROUP_DESCR from UWBOX.dbo.V_PARAM_PRODUCT_MASTER where SEGMENT = 0";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_PRODUCTGROUP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillDGRProduct();
        }

        protected void DDL_PERSON_MODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (DDL_PERSON_MODE.SelectedValue)
            {
                case "0": DV_DUMMY.Visible = true;
                    DV_ENTRY.Visible = false;
                    DV_EXISTING.Visible = false;
                    break;
                case "1": DV_DUMMY.Visible = false;
                    DV_ENTRY.Visible = true;
                    DV_EXISTING.Visible = false;
                    RegisterScript();
                    break;
                case "2": DV_DUMMY.Visible = false;
                    DV_ENTRY.Visible = false;
                    DV_EXISTING.Visible = true;
                    break;
            }
        }

        protected void FillDGRProduct()
        {
            conn.QueryString = "select		distinct "+	
                                "b.PRODUCT_CODE, "+
                                "b.PRODUCT_DESCR "+
                                "from		UWBOX.dbo.PARAM_PRODUCT_MASTER_TC a "+
                                "inner join	UWBOX.dbo.V_PARAM_PRODUCT_MASTER b on a.PRODUCT_CODE = b.PRODUCT_CODE and b.PRODUCT_GROUP = '" + DDL_PRODUCTGROUP.SelectedValue + "' and b.STAT = 1 "+
                                "order by 2";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PRODUCT.DataSource = dt;
            DGR_PRODUCT.DataBind();

            for (int i = 0; i < DGR_PRODUCT.Items.Count; i++)
            {
                conn.QueryString = "select " +		
                                    "b.CODE," +		
                                    "b.DESCR " +		
                                    "from		UWBOX.dbo.PARAM_PRODUCT_MASTER_TC a " +		
                                    "inner join	UWBOX.dbo.TC_MASTER b on a.TC_ID = b.CODE " +
                                    "where " +		
                                    "a.PRODUCT_CODE = '" + DGR_PRODUCT.Items[i].Cells[0].Text + "'";
                conn.ExecuteQuery();
                DropDownList ddl = (DropDownList)DGR_PRODUCT.Items[i].FindControl("DDL_TC");
                for (int j = 0; j < conn.GetRowCount(); j++)
                {
                    ddl.Items.Add(new ListItem(conn.GetFieldValue(j, 1), conn.GetFieldValue(j, 0)));
                }
            }
        }

        protected void DDL_PRODUCTGROUP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGRProduct();

            SetWarning();
        }

        protected void CB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PRODUCT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PRODUCT.Items[i].FindControl("CB");
                if (cb != (CheckBox)sender)
                {
                    cb.Checked = false;
                }
            }

            SetWarning();
        }

        protected void SetWarning()
        {
            LB_WARNING.Text = "";
            LB_WARNING.CssClass = "";

            conn.QueryString = "select ALERT=[LQ].[dbo].[UFN_CHECK_MAIN_INSURED_BLACKLISTED] ('" + TXT_FULLNAME_ENTRY.Text.Trim() + "','" + TXT_DOB_ENTRY.Text.Trim() + "', '" + TXT_IDNO_ENTRY.Text.Trim() + "')";
            conn.ExecuteQuery();

            string warning = conn.GetFieldValue("ALERT").ToString();

            if (!string.IsNullOrEmpty(warning))
            {
                LB_WARNING.Text = warning;
                LB_WARNING.CssClass = "alert";
            }
        }

        protected void FillDGRExisting()
        {
            LB_WARNING.Text = "";
            LB_WARNING.CssClass = "";

            if (TXT_FULLNAME_EXISTING.Text.Trim() == "" && TXT_DOB_EXISTING.Text.Trim() == "" && TXT_IDNO_EXISTING.Text.Trim() == "")
                return;

            string where = "";

            if (TXT_IDNO_EXISTING.Text.Trim() != "")
                where = where + " and a.ID_NO like '%" + TXT_IDNO_EXISTING.Text.Trim() + "%' ";

            if (TXT_FULLNAME_EXISTING.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_FULLNAME_EXISTING.Text.Trim() + "%' ";

            if (TXT_DOB_EXISTING.Text.Trim() != "")
                where = where + " and a.DOB = '" + GlobalUse.GlobalDateFormat(TXT_DOB_EXISTING.Text.Trim(), "d/M/yyyy") + "' ";

            conn.QueryString = "select " +
                                "ID			= a.ID, " +
                                "FULLNAME	= LTRIM(a.FULLNAME), " +
                                "DOB		= convert(varchar(20), a.DOB, 106), " +
                                "GENDER		= (case when a.SEX = 'M' then 'MALE' when a.SEX = 'F' then 'FEMALE' else '' end), " +
                                "ID_NO		= a.ID_NO " +
                                "from		V_LINK_CB_MEMBER_MASTER a " +
                                "where " +
                                "a.SEX = '" + DDL_GENDER_EXISTING.SelectedValue + "' " + where + " " +
                                "order by 2";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_PERSON_EXISTING.DataSource = dt;
            DGR_PERSON_EXISTING.DataBind();
        }

        protected void BT_SEARCH_EXISTING_Click(object sender, EventArgs e)
        {
            DGR_PERSON_EXISTING.CurrentPageIndex = 0;
            FillDGRExisting();
        }

        protected void DGR_PERSON_EXISTING_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR_PERSON_EXISTING.CurrentPageIndex = e.NewPageIndex;
            FillDGRExisting();
        }

        protected void CB_EXISTING_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR_PERSON_EXISTING.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PERSON_EXISTING.Items[i].FindControl("CB_EXISTING");
                if (cb != (CheckBox)sender)
                {
                    cb.Checked = false;
                }
            }
        }

        protected void BT_SUBMIT_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = Validate();

            if (LB_ERROR.Text != "")
                return;

            string memberid, dummydob, sex, productcode, tc_id;
            memberid = dummydob = sex = productcode = tc_id = "null";

            string tglberaksdummy = "";

            if (!string.IsNullOrWhiteSpace(TXT_BERKAS_ENTRY.Text))
            {
                tglberaksdummy = "'" +
                DateTime.ParseExact(TXT_BERKAS_ENTRY.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture)
                    .ToString("yyyy-MM-dd") + "'";


            }
            else if (!string.IsNullOrWhiteSpace(TXT_BERKAS_EXISTING.Text))
            {
                tglberaksdummy = "'" +
                DateTime.ParseExact(TXT_BERKAS_EXISTING.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture)
                    .ToString("yyyy-MM-dd") + "'";

            }
            else if (!string.IsNullOrWhiteSpace(TXT_BERKAS_DUMMY.Text))
            {
                tglberaksdummy = "'" +
               DateTime.ParseExact(TXT_BERKAS_DUMMY.Text.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture)
                   .ToString("yyyy-MM-dd") + "'";
            }

            if (DDL_PERSON_MODE.SelectedValue == "0")
            {
                dummydob = "'" + GlobalUse.GlobalDateFormat(TXT_DOB_DUMMY.Text.Trim(), "d/M/yyyy") + "'";
                sex = DDL_GENDER_DUMMY.SelectedValue;
            }

            if (DDL_PERSON_MODE.SelectedValue == "1")
            {
                conn.QueryString = "exec SP_LINK_CB_MEMBER_MASTER_UPSERT " +
                                    "null," +
                                    "'" + TXT_FULLNAME_ENTRY.Text.Trim() + "'," +
                                    "'" + DDL_GENDER_ENTRY.SelectedValue + "'," +
                                    "null," +
                                    "'" + GlobalUse.GlobalDateFormat(TXT_DOB_ENTRY.Text.Trim(), "d/M/yyyy") + "'," +
                                    "''," +
                                    "'000'," +
                                    "null," +
                                    "'001'," +
                                    "'000'," +
                                    "'" + TXT_IDNO_ENTRY.Text.Trim() + "'," +
                                    "''," +
                                    "'1NA'," +
                                    "''," +
                                    "''," +
                                    "''," +
                                    "''," +
                                    "''," +
                                    "''," +
                                    "'00'," +
                                    "'1NA'," +
                                    "''," +
                                    "null," +
                                    "null," +
                                    "null," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                conn.ExecuteQuery(120);
                memberid = "'" + conn.GetFieldValue("ID").ToString() + "'";
            }

            if (DDL_PERSON_MODE.SelectedValue == "2")
            {
                for (int i = 0; i < DGR_PERSON_EXISTING.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_PERSON_EXISTING.Items[i].FindControl("CB_EXISTING");
                    if (cb.Checked)
                    {
                        memberid = "'" + DGR_PERSON_EXISTING.Items[i].Cells[0].Text + "'";
                        break;
                    }
                }
            }

            for (int i = 0; i < DGR_PRODUCT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PRODUCT.Items[i].FindControl("CB");
                DropDownList ddl = (DropDownList)DGR_PRODUCT.Items[i].FindControl("DDL_TC");
                if (cb.Checked)
                {
                    productcode = "'" + DGR_PRODUCT.Items[i].Cells[0].Text + "'";
                    tc_id = "'" + ddl.SelectedValue + "'";
                    break;
                }
            }

            try
            {
                conn.QueryString = "exec SP_APPLICATION_MASTER_UPSERT_BERKAS " +
                     "null," +
                     memberid + "," +
                     "'" + LB_CODE.Text + "'," +
                     sex + "," +
                     productcode + "," +
                     tc_id + "," +
                     "null," +
                     dummydob + "," +
                     "null," +
                     "null," +
                     "null," +
                     "null," +
                     "null," +
                     "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                      tglberaksdummy;

                conn.ExecuteQuery();

                //conn.QueryString = "exec SP_APPLICATION_MASTER_UPSERT " +
                //                    "null," +
                //                    memberid + "," +
                //                    "'" + LB_CODE.Text + "'," +
                //                    sex + "," +
                //                    productcode + "," +
                //                    tc_id + "," +
                //                    "null," +
                //                    dummydob + "," +
                //                    "null," +
                //                    "null," +
                //                    "null," +
                //                    "null," +
                //                    "null," +
                //                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "' "
                //                    ;
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
                return;
            }

            string regno = conn.GetFieldValue("REGNO").ToString();
            Response.Redirect("QuotationFrame.aspx?REGNO=" + regno);
        }

        protected string Validate()
        {
            string result = "";

            bool product = false;
            for (int i = 0; i < DGR_PRODUCT.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR_PRODUCT.Items[i].FindControl("CB");
                if (cb.Checked)
                {
                    product = true;
                    break;
                }
            }

            if (!product)
                result = result + "-&nbsp;&nbsp;NO PRODUCT HAS BEEN SELECTED YET<BR>";

            if (DV_EXISTING.Visible)
            {
                bool existing = false;
                for (int i = 0; i < DGR_PERSON_EXISTING.Items.Count; i++)
                {
                    CheckBox cb = (CheckBox)DGR_PERSON_EXISTING.Items[i].FindControl("CB_EXISTING");
                    if (cb.Checked)
                    {
                        existing = true;
                        break;
                    }
                }
                if (!existing)
                    result = result + "-&nbsp;&nbsp;NO EXISTING PERSON HAS BEEN SELECTED YET<BR>";
            }

            if (DV_ENTRY.Visible)
            {
                if (TXT_FULLNAME_ENTRY.Text.Trim() == "")
                    result = result + "-&nbsp;&nbsp;POLICY HOLDER NAME IS EMPTY<BR>";
                if (TXT_DOB_ENTRY.Text.Trim() == "")
                    result = result + "-&nbsp;&nbsp;POLICY HOLDER DATE OF BIRTH IS EMPTY<BR>";
                if (TXT_IDNO_ENTRY.Text.Trim() == "")
                    result = result + "-&nbsp;&nbsp;POLICY HOLDER ID NUMBER IS EMPTY<BR>";
            }

            if (LB_NAME.Text == "")
            {
                result = result + "-&nbsp;&nbsp;AGENT CODE IS NOT VALID<BR>";
            }

            return result;
        }
    }
}