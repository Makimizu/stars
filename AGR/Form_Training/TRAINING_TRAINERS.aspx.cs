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
    public partial class TRAINING_TRAINERS : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TXT_TRAINER_CODE.Text = Request.QueryString["TRAINER_CODE"];

                Setup();
                FillDGR();

            }
        }

        protected void Setup()
        {


        }

        protected void FillDGR()
        {
            conn.QueryString = "exec SP_TRAINING_TRAINER_DISPLAY";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {


            //   try
            //    {
            conn.QueryString = "exec SP_TRAINING_TRAINER_UPSERT " +
                                     "@TRAINER_CODE = null ," +
                                     "@TRAINER_NAME = '" + TXT_TRAINER_NAME.Text + "'," +
                                     "@NARASUMBER = '" + TXT_NARASUMBER.Text + "'," +
                                     "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteNonQuery();
            FillDGR();
            //   }
            //   catch
            //  {
            //LB_ERROR.Text = "<BR> UPLINER CODE can not be blank <BR> UPLINER CODE might be wrong";
            //  }

        }


        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.QueryString = "delete from TRAINING_TRAINER where TRAINER_CODE = '" + e.Item.Cells[1].Text + "'";
                conn.ExecuteNonQuery();
                FillDGR();
            }
        }

        protected void LoadRecord()
        {
            conn.QueryString = "exec SP_TRAINING_TRAINER_DISPLAY ";
            conn.ExecuteQuery();
            TXT_TRAINER_CODE.Text = conn.GetFieldValue("TRAINER_CODE").ToString();
            TXT_TRAINER_NAME.Text = conn.GetFieldValue("TRAINER_NAME").ToString();
            TXT_NARASUMBER.Text = conn.GetFieldValue("NARASUMBER").ToString();
        }


    }
}