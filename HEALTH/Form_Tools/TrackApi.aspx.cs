using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HEALTH.Form_Tools
{
    public partial class TrackApi : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TIPE.Text = Request.QueryString["tipe"];
                LB_OWNER.Text = Request.QueryString["owner"];
                FillDGRTrack();
            }
        }

        protected void FillDGRTrack()
        {
            conn.QueryString = "exec SP_TRACK_VIEW1 '" + LB_TIPE.Text + "','" + LB_OWNER.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                Label lb = (Label)DGR.Items[i].FindControl("LB_TRACK");

                lb.Text = "<B>" + DGR.Items[i].Cells[1].Text + "</B><BR><table style='border-spacing: 0px'>" +
                            "<tr><td>START</td><td>:</td><td>" + DGR.Items[i].Cells[2].Text + "</td></tr>";

                if (DGR.Items[i].Cells[3].Text != "&nbsp;")
                    lb.Text = lb.Text + "<tr><td>END</td><td>:</td><td>" + DGR.Items[i].Cells[3].Text + " [" + DGR.Items[i].Cells[4].Text + "]</td></tr>";

                if (DGR.Items[i].Cells[1].Text != "&nbsp;")
                    lb.Text = lb.Text + "<tr style='vertical-align:top;'><td>COMMENT</td><td>:</td><td>" + DGR.Items[i].Cells[5].Text + "</td></tr></table>";

                if (DGR.Items[i].Cells[6].Text == "1")
                {
                    lb.Text = lb.Text + "<BR>";
                }
            }
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            
        }
    }
}