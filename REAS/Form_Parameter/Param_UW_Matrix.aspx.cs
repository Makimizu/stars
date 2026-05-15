using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.CuBESCore;
using DMS.DBConnection;
using System.Data.OleDb;

namespace REAS.Form_Parameter
{
    public partial class Param_UW_Matrix : System.Web.UI.Page
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
                    string s = Session["s"].ToString();
                }
                catch
                {
                    Response.Redirect("../Standard/FailedSession.aspx");
                }

                Setup();
                FillDGR();
            }
            else
            {
                Upload();
            }
        }

        protected void Setup()
        {
            BT_CLEAR.Attributes.Add("onclick", "if(!confirm('ARE YOU SURE TO CLEAR ?')){return false;};");
        }

        protected void Upload()
        {
            if (FU.HasFile)
            {
                LB_ERROR.Text = "";

                string filename = Path.GetFileName(FU.FileName);
                string fullpath = Server.MapPath("~/Upload/") + Session["s"] + filename;
                if (File.Exists(fullpath))
                {
                    File.Delete(fullpath);
                }
                FU.SaveAs(fullpath);

                conn.QueryString = "delete from PARAM_UW_MATRIX_DETAIL where CODE='" + LB_CODE.Text + "'";
                conn.ExecuteNonQuery();

                string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fullpath + @";Extended Properties=""Excel 8.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text""";
                OleDbConnection con = new OleDbConnection(connstr);
                con.Open();

                DataTable sheets = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                OleDbDataAdapter da = new OleDbDataAdapter("select * from [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();

                foreach (DataRow myRow in dt.Rows)
                {
                    try
                    {
                        float amount = float.Parse(myRow[0].ToString().Trim());
                    }
                    catch
                    {
                        continue;
                    }

                    try
                    {
                        conn.QueryString = "insert into PARAM_UW_MATRIX_DETAIL select " +
                                            "'" + LB_CODE.Text + "'," +
                                            "'" + myRow[0].ToString().Trim() + "'," +
                                            "'" + myRow[1].ToString().Trim() + "'," +
                                            "'" + myRow[2].ToString().Trim().Replace(",", ".") + "'," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "GETDATE()," +
                                            "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'," +
                                            "GETDATE()";
                        conn.ExecuteNonQuery();
                    }
                    catch (System.Exception ex)
                    {
                        LB_ERROR.Text = LB_ERROR.Text + "<BR>" + ex.Message;
                    }
                }



                LoadRecord(LB_CODE.Text);
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select CODE, DESCR from PARAM_UW_MATRIX_MASTER where DESCR like '%" + TXT_SEARCH.Text.Trim() + "%' order by 2";
            conn.ExecuteQuery();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_LIST.DataSource = dt;
            DGR_LIST.DataBind();

            for (int i = 0; i < DGR_LIST.Items.Count; i++)
            {
                LinkButton lbCODE = (LinkButton)DGR_LIST.Items[i].FindControl("LB_ID");
                lbCODE.Text = DGR_LIST.Items[i].Cells[2].Text;
            }
        }

        protected void TXT_SEARCH_TextChanged(object sender, EventArgs e)
        {
            FillDGR();
        }

        protected void DGR_LIST_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                LoadRecord(e.Item.Cells[1].Text);
            }
        }

        protected void LoadRecord(string code)
        {
            TR_NEW.Visible = true;
            FU.Visible = true;
            BT_CLEAR.Visible = true;

            conn.QueryString = "select CODE, DESCR from PARAM_UW_MATRIX_MASTER where CODE = '" + code + "'";
            conn.ExecuteQuery();

            LB_CODE.Text = conn.GetFieldValue("CODE").ToString();
            TXT_NAME.Text = conn.GetFieldValue("DESCR").ToString();

            conn.QueryString = "exec SP_PARAM_UW_MATRIX_DETAIL '" + code + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                DGR_UW.Visible = true;
                BT_CLEAR.Visible = true;
                DataTable dt;
                dt = new DataTable();
                dt = conn.GetDataTable().Copy();
                DGR_UW.DataSource = dt;
                DGR_UW.DataBind();
            }
            else
                DGR_UW.Visible = false;
        }

        protected void BT_NEW_Click(object sender, EventArgs e)
        {
            Response.Redirect("Param_UW_Matrix.aspx");
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            if (TXT_NAME.Text.Trim() == "")
                return;

            string ID = "null";
            if (LB_CODE.Text != "")
                ID = "'" + LB_CODE.Text + "'";

            conn.QueryString = "exec SP_PARAM_UW_MATRIX_MASTER_UPSERT " +
                                ID + "," +
                                "'" + TXT_NAME.Text.Trim() + "'," +
                                "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
            conn.ExecuteQuery();
            LoadRecord(conn.GetFieldValue("CODE").ToString());
        }

        protected void BT_CLEAR_Click(object sender, EventArgs e)
        {
            conn.QueryString = "delete from PARAM_UW_MATRIX_DETAIL where CODE='" + LB_CODE.Text + "'";
            conn.ExecuteNonQuery();
            LoadRecord(LB_CODE.Text);
        }
    }
}