using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Configuration;
using System.Data;

namespace AGR
{
    public partial class TRAINING_LIBRARY_LIST : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {

            conn.QueryString = "select CODE, DESCR =  DESCR +' ('+ CODE + ')' from PR_TRAINING_LEVEL";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TRAINING_LEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

        }

        protected void FillDGR()
        {
            conn.QueryString = "EXEC SP_TRAINING_MASTER_DISPLAY";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                lb.Text = DGR.Items[i].Cells[1].Text;
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            //  try
            // {
            conn.QueryString = "exec SP_TRAINING_MASTER_UPSERT " +
                                     "@TRAINING_CODE = null," +
                                     "@TRAINING_NAME = '" + TXT_TRAINING_NAME.Text + "'," +
                                     "@TRAINING_MATERIAL = '" + TXT_TRAINING_MATERIAL.Text + "'," +
                                     "@TRAINING_LEVEL = '" + DDL_TRAINING_LEVEL.SelectedValue + "'," +
                                     "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillDGR();
            //   }
            //   catch
            //   {
            //LB_ERROR.Text = "<BR> UPLINER CODE can not be blank <BR> UPLINER CODE might be wrong";
            //   }

            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Your Data has been Saved Successfully.');", true);
        }


        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                LoadRecord(e.Item.Cells[2].Text);
            }

            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from TRAINING_MASTER where TRAINING_CODE = '" + e.Item.Cells[2].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }

        protected void LoadRecord(string code)
        {
            TR_BUTTONS.Visible = true;

            conn.QueryString = "select " +
                                "TRAINING_NAME, " +
                                "TRAINING_MATERIAL, " +
                                "TRAINING_LEVEL " +
                                "from TRAINING_MASTER " +
                                "where TRAINING_CODE = '" + code + "'";

            conn.ExecuteQuery();
            LB_TRAINING_CODE.Text = code;
            TXT_TRAINING_NAME.Text = conn.GetFieldValue("TRAINING_NAME").ToString();
            TXT_TRAINING_MATERIAL.Text = conn.GetFieldValue("TRAINING_MATERIAL").ToString();
            DDL_TRAINING_LEVEL.SelectedValue = conn.GetFieldValue("TRAINING_LEVEL").ToString();

            LoadSubModule();
        }

        protected void LoadSubModule()
        {
            LBL_TITLE.Text = BT_SUBMODULE.Text;
            IF.Src = "TRAINING_SUBMODULE.aspx?TRAINING_CODE=" + LB_TRAINING_CODE.Text;
        }

        protected void BT_DOCUMENT_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            IF.Src = "../../ARCHIEVE/Arsip.aspx?app=" + System.Configuration.ConfigurationManager.AppSettings["appid"] + "&tipe=AGR_2&owner1=" + LB_TRAINING_CODE.Text + "&owner2=&owner3=&user=" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID");
        }

        protected void BT_REMARK_Click(object sender, EventArgs e)
        {
            LBL_TITLE.Text = ((Button)sender).Text;
            IF.Src = "TRAINING_REMARK.aspx?TRAINING_CODE=" + LB_TRAINING_CODE.Text;
        }

        protected void BT_SUBMODULE_Click(object sender, EventArgs e)
        {
            LoadSubModule();
        }

    }
}