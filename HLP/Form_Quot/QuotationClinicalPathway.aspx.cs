 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;
using System.Data.SqlClient;
using System.Web.Services;
using System.IO;

namespace HLP.Form_Quot
{
    public partial class QuotationClinicalPathway : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_CODE.Text = Request.QueryString["CODE"].ToString();
                Setup();
                //Fill_DGR_CLINICAL_PATHWAY();
            }
        }

        protected void Setup(string codeDiagnosa = null, string codeClass = null)
        {
            //conn.QueryString = "select CODE,DESCR_1 from ASKES_MIGRASI.dbo.PARAM_ICD_GROUP WHERE DESCR_1 IS NOT NULL ORDER BY DESCR_1";
            //conn.ExecuteQuery();
            //DDL_DIAGNOSA.Items.Add(new ListItem("", ""));
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //    DDL_DIAGNOSA.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            //conn.QueryString = "select CODE,DESCR from ASKES_MIGRASI.dbo.PR_PROVIDER_KELAS_KAMAR order by DESCR";
            //conn.ExecuteQuery();
            //DDL_TRF_KAMAR.Items.Add(new ListItem("", ""));
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //    DDL_TRF_KAMAR.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));


            //if (codeDiagnosa != null)
            //    DDL_DIAGNOSA.SelectedValue = codeDiagnosa;

            //if (codeClass != null)
            //    DDL_TRF_KAMAR.SelectedValue = codeClass;
        }


        protected void Fill_DGR_CLINICAL_PATHWAY(string filter = null)


        {

            conn.QueryString = " select a.ID, a.KODE_PROVIDER, b.NAMA as NAMA_PROVIDER, a.KODE_DIAGNOSA,c.DESCR DIAGNOSA,a.LAMA_RAWAT,a.KELAS_KAMAR, " +
       " FORMAT(pt.TARIF, '#,0.00') as BIAYA_KAMAR_FORMAT,FORMAT(a.TOTAL_BIAYA_CP, '#,0.00') as TOTAL_BIAYA_CP_FORMAT  " +
       " from ASKES_MIGRASI.dbo.PROVIDER_CLINICAL_PATHWAY AS a LEFT OUTER JOIN ASKES_MIGRASI.dbo.PROVIDER_MASTER AS b ON a.KODE_PROVIDER = b.KODE_PROVIDER LEFT OUTER JOIN" +
       " ASKES_MIGRASI.dbo.pr_icd AS c ON a.KODE_DIAGNOSA = c.CODE LEFT OUTER JOIN ASKES_MIGRASI.dbo.PROVIDER_TARIF pt  on pt.KODE_PROVIDER = b.KODE_PROVIDER and pt.KELAS_KAMAR=a.KODE_KAMAR LEFT OUTER JOIN ASKES_MIGRASI.dbo.PR_PROVIDER_KELAS_KAMAR AS d ON a.KELAS_KAMAR = d.CODE AND A.KODE_KAMAR= a.KODE_KAMAR WHERE 1=1 and a.TOTAL_BIAYA_CP is not null ";



            if (filter != null)
            {
                conn.QueryString += filter;
                conn.QueryString += "order by KODE_PROVIDER, DIAGNOSA asc";
                conn.ExecuteQuery();

                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_CLINICAL_PATHWAY.DataSource = dt;
                DGR_CLINICAL_PATHWAY.DataBind();
            }
        }
        protected void BT_EXPORT_Click(object sender, EventArgs e)
        {
            string filename = "ClinicalPathway_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            string attachment = "attachment; filename=" + filename;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            DGR_CLINICAL_PATHWAY.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            string filters = "";

            if (txt_diagnosa_search.Text != "")
            {
                filters += " AND a.KODE_DIAGNOSA = '" + txt_diagnosa_search.Text.Split('-')[0] + "' ";
            }

            if (txt_provider_search.Text != "")
            {
                filters += " AND a.KODE_PROVIDER = '" + txt_provider_search.Text.Split('-')[0] + "' ";
            }


            Fill_DGR_CLINICAL_PATHWAY(filters);
        }

        protected void BT_CLEAR_Click(object sender, EventArgs e)
        {

            Setup(null, null);

            txt_diagnosa_search.Text = string.Empty;
            txt_provider_search.Text = string.Empty;
            DGR_CLINICAL_PATHWAY.DataSource = null;
            DGR_CLINICAL_PATHWAY.DataBind();
        }


        protected void DGR_CLINICAL_PATHWAY_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            int id = int.Parse(e.Item.Cells[0].Text.Replace("&nbsp;", ""));
            string user = GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");

        }

        [WebMethod]
        public static List<string> SearchProvider(string prefixText, int count)
        {
            List<string> results = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select KODE_PROVIDER +'-'+ NAMA as NAMA from ASKES_MIGRASI.dbo.PROVIDER_MASTER WHERE KODE_PROVIDER +'-'+ NAMA LIKE '%'+@SearchText+'%' ORDER BY KODE_PROVIDER +'-'+ NAMA";
                    //"select TOP 10 CODE+' '+DESCR_1 as DESCR_1 from PARAM_ICD_GROUP WHERE DESCR_1 IS NOT NULL AND CODE+' '+DESCR_1 LIKE '%'+@SearchText+'%' ORDER BY DESCR_1";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            results.Add(sdr["NAMA"].ToString());
                        }
                    }
                    conn.Close();

                    return results;
                }
            }
        }

        [WebMethod]
        public static List<string> SearchDiagnosa(string prefixText, int count)
        {
            List<string> results = new List<string>();
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]);
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select TOP 10 CODE+'-'+DESCR_1 as DESCR_1 from ASKES_MIGRASI.dbo.PARAM_ICD_GROUP WHERE DESCR_1 IS NOT NULL AND CODE+'-'+DESCR_1 LIKE '%'+@SearchText+'%' ORDER BY CODE+'-'+DESCR_1";
                    //"select TOP 10 CODE+' '+DESCR_1 as DESCR_1 from PARAM_ICD_GROUP WHERE DESCR_1 IS NOT NULL AND CODE+' '+DESCR_1 LIKE '%'+@SearchText+'%' ORDER BY DESCR_1";
                    cmd.Parameters.AddWithValue("@SearchText", prefixText);
                    cmd.Connection = conn;
                    conn.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            results.Add(sdr["DESCR_1"].ToString());
                        }
                    }
                    conn.Close();

                    return results;
                }
            }
        }


    }
}