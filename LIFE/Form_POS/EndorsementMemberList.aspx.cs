using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LIFE.Form_POS
{
    public partial class EndorsementMemberList : System.Web.UI.Page
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


                switch (LB_TYPE.Text)
                {
                    case "ALN01": FillDGRMember(); break;
                    case "ALN09": FillDGRMember(); break;
                    case "ALN03": FillDGRMemberUpsert(); break;
                    case "ALF03": FillDGRMemberUpsert(); break;
                }

                CheckTrack();
            }
        }

        protected void Setup()
        {
            if (LB_TYPE.Text != "ALF03")
            {
                TBL_TITLE.Visible = true;
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
                TBL_TITLE.Visible = false;
            }
        }

        protected void CheckTrack()
        {
            if (GlobalUse.GetTrack(LB_REGNO.Text, "POS", LB_SEQ.Text) > 3)
            {
                DGR_MEMBER_UPSERT.Columns[DGR_MEMBER_UPSERT.Columns.Count - 1].Visible = false;
            }
        }

        protected void FillDGRMember()
        {
            TBL_MEMBER.Visible = true;
            TBL_MEMBER_UPSERT.Visible = false;

            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MEMBER " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MEMBER.DataSource = dt;
            DGR_MEMBER.DataBind();

            for (int i = 0; i < DGR_MEMBER.Items.Count; i++)
            {
                LinkButton lbt = (LinkButton)DGR_MEMBER.Items[i].FindControl("LBT_FULLNAME");
                lbt.Text = DGR_MEMBER.Items[i].Cells[1].Text;
            }
        }

        protected void FillDGRMemberUpsert()
        {
            TBL_MEMBER.Visible = false;
            TBL_MEMBER_UPSERT.Visible = true;

            conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MEMBER " +
                                "'" + LB_REGNO.Text + "'," +
                                "'" + LB_SEQ.Text + "'," +
                                "'" + LB_TYPE.Text + "'";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR_MEMBER_UPSERT.DataSource = dt;
            DGR_MEMBER_UPSERT.DataBind();

        }

        protected void DGR_MEMBER_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                ViewPersonalDetail(e.Item.Cells[2].Text, e.Item.Cells[5].Text);
            }
        }

        protected void ViewPersonalDetail(string URL, string status)
        {
            LB_MEMBERTITLE.Text = status;
            IF.Src = URL;
            ClientScript.RegisterStartupScript(this.GetType(), "focus", "document.getElementById('pnlpopup').style.display = 'block';", true);
        }

        protected void DGR_MEMBER_UPSERT_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "E")
            {
                ViewPersonalDetail(e.Item.Cells[1].Text, e.Item.Cells[2].Text);
            }

            if (e.CommandName == "X")
            {
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MEMBER_DELETE " +
                                    "'" + LB_REGNO.Text + "'," +
                                    "'" + LB_SEQ.Text + "'," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'" + e.Item.Cells[0].Text + "'";
                conn.ExecuteQuery();
                switch (LB_TYPE.Text)
                {
                    case "ALN01": FillDGRMember(); break;
                    case "ALN03": FillDGRMemberUpsert(); break;
                    case "ALF03": FillDGRMemberUpsert(); break;
                }
            }
        }

        protected void LB_BACK_Click(object sender, EventArgs e)
        {
            switch (LB_TYPE.Text)
            {
                case "ALN01": FillDGRMember(); break;
                case "ALN03": FillDGRMemberUpsert(); break;
                case "ALF03": FillDGRMemberUpsert(); break;
            }
        }
    }
}