using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DMS.DBConnection;
using DMS.CuBESCore;

namespace UWBOX.Form_Parameter
{
    public partial class Param_UW_Document : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        public string DDLID;
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
            conn.QueryString = "select CODE,DESCR from PR_UW_CODE order by 1";
            conn.ExecuteQuery();
            DDL_UW.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_UW.Items.Add(new ListItem(conn.GetFieldValue(i, 0).ToString() + " - " + conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));

            FillGrid();
        }

        protected void FillGrid()
        {
            conn.QueryString = "select CODE,DESCR, COUNT=(select count(DOC_CODE) from PARAM_UW_CODE_DOCUMENT where DOC_CODE=a.CODE and UW_CODE='" + DDL_UW.SelectedValue + "') from PR_UW_REQUIRED_DOCUMENT a order by a.DESCR";
            conn.ExecuteQuery();
            DataTable dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_DOC.DataSource = dt;
            DGR_DOC.DataBind();
            for (int j = 0; j < DGR_DOC.Items.Count; j++)
            {
                CheckBox cb = (CheckBox)DGR_DOC.Items[j].FindControl("CB");
                if (DGR_DOC.Items[j].Cells[2].Text != "0")
                {
                    cb.Checked = true;
                    DGR_DOC.Items[j].Cells[1].Text = "<B>" + DGR_DOC.Items[j].Cells[1].Text + "</B>";
                    DGR_DOC.Items[j].BackColor = System.Drawing.Color.Yellow;
                }
            }

        }


        protected void DDL_UW_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PARAM_UW_CODE_DOCUMENT where UW_CODE = '" + DDL_UW.SelectedValue + "' ";
            for (int j = 0; j < DGR_DOC.Items.Count; j++)
            {
                CheckBox cb = (CheckBox)DGR_DOC.Items[j].FindControl("CB");
                if (cb.Checked)
                    conn.QueryString = conn.QueryString + "insert into PARAM_UW_CODE_DOCUMENT values ('" + DDL_UW.SelectedValue + "','" + 
                                                                                                            DGR_DOC.Items[j].Cells[0].Text + "'," +
                                                                                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate()," + 
                                                                                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "',GetDate())";
            }

            conn.ExecuteNonQuery();
            FillGrid();
        }
    }
}