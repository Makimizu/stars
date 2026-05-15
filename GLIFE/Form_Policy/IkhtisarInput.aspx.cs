using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace GLIFE.Form_Policy
{
    public partial class IkhtisarInput : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            inputValue1.ValidateRequestMode = ValidateRequestMode.Disabled;
            inputValue.ValidateRequestMode = ValidateRequestMode.Disabled;

            if (!IsPostBack)
            {
                TXT_POLICYNO.Text = Request.QueryString["ID"].ToString();
                TXT_POLICYNO1.Text = Request.QueryString["ID"].ToString();
                LB_readonly.Text = Request.QueryString["readonly"].ToString();
                LB_s.Text = Request.QueryString["s"].ToString();

                conn.QueryString = "SELECT GROUP_CODE FROM V_POLICY WHERE POLICY_NO = '" + TXT_POLICYNO.Text + "'";
                conn.ExecuteQuery();

                if (conn.GetFieldValue("GROUP_CODE").ToString() == "GTL")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('GTL').style.display = 'block';", true);
                }
                else if (conn.GetFieldValue("GROUP_CODE").ToString() == "CL")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('CL').style.display = 'block';", true);
                }

                LoadData();
            }
        }

        protected void LoadData()
        {
            conn.QueryString = "select CODE,TITLE from IKHTISAR_POLICY_GTL_MANFAAT order by 1";
            conn.ExecuteQuery();
            DDL_MANFAAT.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MANFAAT.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            conn.QueryString = "SELECT POLICY_NO, UW, OTHER, AGE, POLICY_START_DATE = convert(varchar(20), POLICY_START_DATE,103), POLICY_END_DATE = convert(varchar(20), POLICY_END_DATE,103), START_DATE = convert(varchar(20), START_DATE,103), END_DATE = convert(varchar(20), END_DATE,103), MANFAAT_CODE FROM IKHTISAR_POLICY WHERE POLICY_NO = '" + TXT_POLICYNO.Text + "'";
            conn.ExecuteQuery();

            

            //if (conn.GetRowCount() > 0)
            //{
            //    BT_SAVE.Enabled = false;
            //}


            //inputValue2.Text = conn.GetFieldValue("UW").ToString();
            TXT_OTHER.Text = conn.GetFieldValue("OTHER").ToString();
            //inputValue3.Text = conn.GetFieldValue("AGE").ToString();
            DDL_MANFAAT.SelectedValue = conn.GetFieldValue("MANFAAT_CODE").ToString();
            TXT_DATE1.Text = conn.GetFieldValue("POLICY_START_DATE").ToString();
            TXT_DATE2.Text = conn.GetFieldValue("POLICY_END_DATE").ToString();
            TXT_DATE3.Text = conn.GetFieldValue("START_DATE").ToString();
            TXT_DATE4.Text = conn.GetFieldValue("END_DATE").ToString();
        }

        //protected void DDL_MANFAAT_SelectedIndexChanged(object sender, EventArgs e)
        //{
            
        //}

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {

            conn.QueryString = "EXEC SP_IKHTISAR_POLIS_GTL_UPSERT " +
                "'" + TXT_POLICYNO.Text + "', '" + TXT_DATE3.Text + "', '" + TXT_DATE4.Text + "', '" + TXT_DATE1.Text + "', '" + TXT_DATE2.Text + "', " + DDL_MANFAAT.SelectedValue + ", '" + inputValue2.Text.Replace("'", "''") + "', '" + TXT_OTHER.Text.Replace("'", "''") + "', '" + inputValue3.Text.Replace("'", "''") + "', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            //Response.Redirect("IkhtisarFrame.aspx?ID=" + TXT_POLICYNO.Text + LB_readonly.Text);
            Response.Write("<script language='javascript'>parent.appdocumentchild.location.href = '../Form_Policy/IkhtisarReport.aspx?ID=" + TXT_POLICYNO.Text + "';</script>");
            Response.Write("<script language='javascript'>parent.appdocument.location.href = '../Form_Policy/IkhtisarInput.aspx?ID=" + TXT_POLICYNO.Text + "'&readonly='" + LB_readonly.Text + "'&s='" + LB_s.Text + "';</script>");

        }

        protected void BT_SAVE_BANCAS_Click(object sender, EventArgs e)
        {

            conn.QueryString = "EXEC SP_IKHTISAR_POLIS_BANCASS_UPSERT " +
                "'" + TXT_POLICYNO1.Text + "', '" + TXT_DATE5.Text + "', '" + TXT_DATE6.Text + "', '" + TXT_DATE7.Text + "', '" + TXT_DATE8.Text + "', '" + inputValue.Text.Replace("'", "''") + "', '" + TXT_MARGIN.Text + "', '" + inputValue1.Text + "', '" + TXT_FEEBASE.Text + "', '" + TXT_PENGELOLAAN.Text + "', '" + TXT_KLAIM.Text + "', '" + TXT_SURPLUS.Text + "', '" + TXT_LAMPIRAN.Text + "', '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();

            //Response.Redirect("IkhtisarFrame.aspx?ID=" + TXT_POLICYNO.Text + LB_readonly.Text);
            Response.Write("<script language='javascript'>parent.appdocumentchild.location.href = '../Form_Policy/IkhtisarReport.aspx?ID=" + TXT_POLICYNO1.Text + "';</script>");
            Response.Write("<script language='javascript'>parent.appdocument.location.href = '../Form_Policy/IkhtisarInput.aspx?ID=" + TXT_POLICYNO1.Text + "'&readonly='" + LB_readonly.Text + "'&s='" + LB_s.Text + "';</script>");

        }
    }
}