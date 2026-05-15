using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR
{
    public partial class PARAMETER_LIST : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
              if (Session["s"] == null) 

                    Response.Redirect("logout.aspx"); 

                TXT_MODE.Text = Request.QueryString["mode"]; 

                Setup(); 

               LoadRecord();

               if (TXT_MODE.Text == "1")
               {
                  // div1.Visible = false; => tampilkan DGR1
                   div2.Visible = false;
                   div3.Visible = false;
                   div4.Visible = false;
                   div5.Visible = false;
                   div6.Visible = false;
                   //isi DGR1 (commission)
                   LoadRecord1(); 

               }
               else if (TXT_MODE.Text == "2")
               {
                   div1.Visible = false;
                 //  div2.Visible = false;
                   div3.Visible = false;
                   div4.Visible = false;
                   div5.Visible = false;
                   div6.Visible = false;
               }
               else if (TXT_MODE.Text == "3")
               {
                   div1.Visible = false;
                   div2.Visible = false;
                 //  div3.Visible = false;
                   div4.Visible = false;
                   div5.Visible = false;
                   div6.Visible = false;
               }
               else if (TXT_MODE.Text == "4")
               {
                   div1.Visible = false;
                   div2.Visible = false;
                   div3.Visible = false;
                 //  div4.Visible = false;
                   div5.Visible = false;
                   div6.Visible = false;
               }
               else if (TXT_MODE.Text == "5")
               {
                   div1.Visible = false;
                   div2.Visible = false;
                   div3.Visible = false;
                   div4.Visible = false;
                 //  div5.Visible = false;
                   div6.Visible = false;
               }
               else 
               {
                   div1.Visible = false;
                   div2.Visible = false;
                   div3.Visible = false;
                   div4.Visible = false;
                   div5.Visible = false;
                //   div6.Visible = false;
               }
        }

        protected void Setup()
        {

            conn.QueryString = "select " +
                               "CODE, DESCR " +
                                "from PR_REMUN_TYPE " +
                                "order by code";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_TYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        } 

       protected void LoadRecord() 
        {
            conn.QueryString = "exec SP_PARAM_REMUN_MASTER_DISPLAY @TYPE = '" + TXT_MODE.Text + "'";
            conn.ExecuteQuery(); 
            DataTable dt; 
            dt = new DataTable(); 
            dt = conn.GetDataTable().Copy(); 
            DGR.DataSource = dt; 
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
               
                lb.Text = DGR.Items[i].Cells[3].Text;
            }

            
        }

        
       protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
       {
          // Response.Redirect("PARAMETER_GROUP.aspx");
           if (e.CommandName == "Detil")
           {
             // Response.Redirect("Agent_Frame.aspx?AGENTCODE=" + e.Item.Cells[1].Text);
               Response.Redirect("Logout.aspx");
               //Response.Redirect("PARAMETER_GROUP.aspx");
               
              
           }
       }




       protected void BT_SAVE_Click(object sender, EventArgs e)
       {
           conn.QueryString = "exec SP_PARAM_REMUN_MASTER_UPSERT " +
                               "@ID = null, " +
                               "@TYPE = '" + DDL_TYPE.SelectedValue + "', " +
                               "@DESCRIPTION = '" + TXT_DESCRIPTION.Text.Trim() + "'," +
                               "@STARTDATE = '" + GlobalUse.GlobalDateFormat(TXT_START_DATE.Text, "d/M/yyyy") + "'," +
                               "@ENDDATE  = '" + GlobalUse.GlobalDateFormat(TXT_END_DATE.Text, "d/M/yyyy") + "'," +
                               "@COMPANY_CODE = '" + TXT_COMPANY_CODE.Text.Trim() + "'," +
                               "@USERBY = '" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

           conn.ExecuteQuery();
           LoadRecord();
       }
        // menampilkan data commision
       protected void LoadRecord1()
       {
           conn.QueryString = "exec SP_PARAM_REMUN_COMMISSION_DISPLAY";
           conn.ExecuteQuery();
           DataTable dt;
           dt = new DataTable();
           dt = conn.GetDataTable().Copy();
           DGR1.DataSource = dt;
           DGR1.DataBind();
       }

       protected void BT1_SAVE_Click(object sender, EventArgs e)
       {
          
            
       }

       protected void BT2_SAVE_Click(object sender, EventArgs e)
       {

       }

       protected void BT3_SAVE_Click(object sender, EventArgs e)
       {

       }

       protected void BT5_SAVE_Click(object sender, EventArgs e)
       {

       } 
    }
}
