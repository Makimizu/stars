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
    public partial class AGENT_LIST : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
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
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_MARKET.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
            FillDDL_LEVEL();

            conn.QueryString = "select SEQ, DESCR from PARAM_TRACK where TIPE_CODE = 'AGN' and SEQ > 1 order by SEQ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_STATUS.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

        }

        protected void FillDGR()
        {
            LB_RECORDS.Text = "";
            string where = "";

            if (TXT_CODE.Text.Trim() != "")
                where = where + " and a.CODE like '%" + TXT_CODE.Text.Trim() + "%' ";

            if (TXT_AGENCY.Text.Trim() != "")
                where = where + " and a.AGENCY_NAME like '%" + TXT_AGENCY.Text.Trim() + "%' ";

            if (TXT_NAME.Text.Trim() != "")
                where = where + " and a.FULLNAME like '%" + TXT_NAME.Text.Trim() + "%' ";

            if (TXT_UPLINER.Text.Trim() != "")
                where = where + " and up1name.FULLNAME like '%" + TXT_UPLINER.Text.Trim() + "%' ";

            if (TXT_UPLINER2.Text.Trim() != "")
                where = where + " and up2name.FULLNAME like '%" + TXT_UPLINER2.Text.Trim() + "%' ";

            if (TXT_RO.Text.Trim() != "")
                where = where + " and a.BRANCH_DESCR like '%" + TXT_RO.Text.Trim() + "%' ";

            if (DDL_LEVEL.SelectedValue != "")
                where = where + " and a.SUBCD = '" + DDL_LEVEL.SelectedValue + "' ";

            if (DDL_STAT.SelectedValue != "")
                where = where + " and a.ACTIVE = " + DDL_STAT.SelectedValue + " ";

            if (DDL_MARKET.SelectedValue != "")
                where = where + " and a.MARKET_SEGMENT = '" + DDL_MARKET.SelectedValue + "' ";

            if (DDL_STATUS.SelectedValue != "")
                where = where + " and a.TRACK = " + DDL_STATUS.SelectedValue + " ";

            conn.QueryString = "select " +
                                "AGENT_CODE             = a.CODE,  " +
                                "STATUS_APPROVAL        = a.TRACK,   " +
                                "STATUS_APPROVAL_DESCR  = a.TRACK_DESCR, " +
                                "AGENT_LEVEL            = a.SUBCD_DESCR,   " +
                                "AGENT_NAME             = a.FULLNAME,  " +
                                "TGL_OTORISASI          = convert(varchar, a.JOINTDATE, 106),  " +
                                "RO_NAME                = isnull(a.BRANCH_DESCR, ''),  " +
                                "AGENCY_NAME            = a.AGENCY_NAME,   " +
                                "UPLINER1_LEVEL         = up1.SUBCD_DESCR, " +
                                "UPLINER1               = up1.CODE,  " +
                                "UPLINER1_NAME          = up1.FULLNAME,  " +
                                "UPLINER2_LEVEL         = up2.SUBCD_DESCR, " +
                                "UPLINER2               = up2.CODE,  " +
                                "UPLINER2_NAME          = up2.FULLNAME,  " +
                                "STATUS_ACTIVE          = a.ACTIVE, " +
                                "STATUS_LISTING         = [MARKETING].[dbo].[UFN_GET_AGENT_LISTING_TYPE](a.CODE) " +
                                "from                   [MARKETING].[dbo].[V_M_AGENTS] a " +
                                "left join              [MARKETING].[dbo].[V_M_AGENTS_UPLINER] b on a.CODE = b.AGENT_CODE " +
                                "left join              [MARKETING].[dbo].[V_M_AGENTS] up1 on b.UPLINER_1 = up1.CODE " +
                                "left join              [MARKETING].[dbo].[V_M_AGENTS] up2 on b.UPLINER_2 = up2.CODE " +
                                //"from                   [MARKETING].[dbo].[V_M_AGENTS] a " +
                                //"left join              [MARKETING].[dbo].[V_M_AGENTS_UPLINER_BYLEVEL] up1 on a.CODE = up1.CODE and up1.LEVEL = 1 " +
                                //"left join              [MARKETING].[dbo].[V_M_AGENTS] up1name on up1.UPLINER = up1name.CODE " +
                                //"left join              [MARKETING].[dbo].[V_M_AGENTS_UPLINER_BYLEVEL] up2a on up1.UPLINER = up2a.CODE and up2a.LEVEL = 1 " +
                                //"left join              [MARKETING].[dbo].[V_M_AGENTS_UPLINER_BYLEVEL] up2b on a.CODE = up2b.CODE and up2b.LEVEL = 2 " +
                                //"left join              [MARKETING].[dbo].[V_M_AGENTS] up2name on isnull(up2a.UPLINER, up2b.UPLINER) = up2name.CODE " +
                                "where " +
                                "1=1 " + where +
                                "order by " +
                                "a.FULLNAME";

            conn.ExecuteQuery();

            LB_RECORDS.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                LinkButton lb = (LinkButton)DGR.Items[i].FindControl("LB_CODE");
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                lb.Text = DGR.Items[i].Cells[1].Text;
                if (DGR.Items[i].Cells[2].Text == "1")
                    cb.Checked = true;

                switch (DGR.Items[i].Cells[3].Text)
                {
                    case "2": DGR.Items[i].Cells[4].ForeColor = System.Drawing.Color.Green; break;
                    case "4": DGR.Items[i].Cells[4].ForeColor = System.Drawing.Color.Orange; cb.Visible = false; break;
                    case "3": DGR.Items[i].Cells[4].ForeColor = System.Drawing.Color.Red; cb.Visible = false; break;
                }
            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Detail")
            {
                Response.Redirect("Agent_Frame.aspx?AGENTCODE=" + e.Item.Cells[1].Text);
            }
        }

        
        protected void DB_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)DGR.Items[i].FindControl("CB");

                if ((CheckBox)sender == cb)
                {
                    string stat = "0";
                    if (cb.Checked)
                        stat = "1";

                    try
                    {
                        conn.QueryString = "update M_AGENTS set ACTIVE = " + stat + " where CODE = '" + DGR.Items[i].Cells[1].Text + "'";
                        conn.ExecuteNonQuery();
                        FillDGR();
                        return;
                    }
                    catch { }
                }
            }
        }

        protected void DDL_STATUS_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void FillDDL_LEVEL()
        {
            conn.QueryString = "select SUB_CODE, DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT = '" + DDL_MARKET.SelectedValue + "' order by SEQ";
            conn.ExecuteQuery();

            DDL_LEVEL.Items.Clear();
            DDL_LEVEL.Items.Add(new ListItem("", ""));
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void DDL_MARKET_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDL_LEVEL();
        }

        //protected void BT_XLS_Click(object sender, EventArgs e)
        //{
        //    BT_XLS.Attributes.Add("onclick", "document.body.style.cursor = 'wait';");

        //    string where = "";

        //    if (TXT_CODE.Text.Trim() != "")
        //        where = where + " and a.CODE like '%" + TXT_CODE.Text.Trim() + "%' ";

        //    if (TXT_AGENCY.Text.Trim() != "")
        //        where = where + " and a.AGENCY_NAME like '%" + TXT_AGENCY.Text.Trim() + "%' ";

        //    if (TXT_NAME.Text.Trim() != "")
        //        where = where + " and a.FULLNAME like '%" + TXT_NAME.Text.Trim() + "%' ";

        //    if (TXT_UPLINER.Text.Trim() != "")
        //        where = where + " and c.UPLINER_NAME like '%" + TXT_UPLINER.Text.Trim() + "%' and c.LEVEL = 'TSM' ";

        //    if (TXT_UPLINER2.Text.Trim() != "")
        //        where = where + " and c2.UPLINER_NAME like '%" + TXT_UPLINER2.Text.Trim() + "%' and c2.LEVEL = 'TAD' ";

        //    if (TXT_RO.Text.Trim() != "")
        //        where = where + " and a.BRANCH_DESCR like '%" + TXT_RO.Text.Trim() + "%' ";

        //    if (DDL_LEVEL.SelectedValue != "")
        //        where = where + " and a.SUBCD = '" + DDL_LEVEL.SelectedValue + "' ";

        //    if (DDL_STAT.SelectedValue != "")
        //        where = where + " and a.ACTIVE = " + DDL_STAT.SelectedValue + " ";

        //    if (DDL_MARKET.SelectedValue != "")
        //        where = where + " and b.DESCR like '%" + DDL_MARKET.SelectedItem.Text + "%' ";

        //    if (DDL_STATUS.SelectedValue != "")
        //        where = where + " and a.TRACK = " + DDL_STATUS.SelectedValue + " ";

        //    conn.QueryString = "select  " +
        //                        "AGENT_CODE = a.CODE,    " +
        //                        "STATUS_APPROVAL = a.TRACK,    " +
        //                        "STATUS_APPROVAL_DESCR = a.TRACK_DESCR,    " +
        //                        "AGENT_LEVEL = a.SUBCD_DESCR,    " +
        //                        "AGENT_NAME = a.FULLNAME,   " +
        //                        "DOB = convert(varchar(20),a.DOB,106),    " +
        //                        "PLACE_OF_BIRTH = isnull(POB,''),   " +
        //                        "TGL_OTORISASI = a.JOINTDATE,   " +
        //                        "RO_CODE = isnull(a.BRANCH_CODE,''), " +
        //                        "RO_NAME = isnull(a.BRANCH_DESCR,''), " +
        //                        "a.AGENCY_CODE, " +
        //                        "a.AGENCY_NAME, " +
        //                        "UPLINER1 = c.UPLINER, " +
        //                        "UPLINER1_LEVEL = isnull(c.LEVEL,''),  " +
        //                        "UPLINER1_NAME = c.UPLINER_NAME, " +
        //                        "UPLINER2 = isnull(c2.UPLINER,''),   " +
        //                        "UPLINER2_LEVEL = isnull(c2.LEVEL,''),   " +
        //                        "UPLINER2_NAME = isnull(c2.UPLINER_NAME,''),   " +
        //                        "NAMA_AREA = '', " +
        //                        "NAMA_RM = '',   " +
        //                        "KODE_PEREKRUT = isnull(e.AGENT_CODE,''),   " +
        //                        "NAMA_PEREKRUT = replace(LTRIM(RTRIM(isnull(e.FRONT_NAME, '') + ' ' + isnull(e.MID_NAME, '') + ' ' + isnull(e.LAST_NAME, ''))), '  ', ' '),   " +
        //                        "Promotor_Ke_TSM    = isnull(i.PROMOTING_AGENT,''),  " +
        //                        "Promotor_ke_TAD	= isnull(j.PROMOTING_AGENT,''),  " +
        //                        "NPWP =  isnull(NPWP.VAL,''),   " +
        //                        "Jenis_PTKP =  isnull(TAX_OBJECT.VAL,''),   " +
        //                        "No_Rekening = f.ACCNO,   " +
        //                        "Nama_Bank = f.ACCBANK_DESCR,   " +
        //                        "an_Rekening = f.ACCNAME,   " +
        //                        "Email = isnull(a.EMAIL,''),   " +
        //                        "HP = isnull(a.PHONE,''),   " +
        //                        "Alamat_KTP =  isnull(ADD_ID.VAL,''),   " +
        //                        "Alamat_Domisili =  isnull(ADD_DOM.VAL,''),  " +
        //                        "Kota_Domisili =  isnull(CITY.VAL,''),   " +
        //                        "No_lisensi = isnull(g.LICENCE_NO,''),   " +
        //                        "tanggal_awal_Lisensi = isnull(convert(varchar,g.STARTDATE),''),   " +
        //                        "tanggal_akhir_Lisensi = isnull(convert(varchar,g.ENDDATE),''),   " +
        //                        "STATUS_ACTIVE_DESCR = case when a.ACTIVE = 0 then 'NOT ACTIVE' else 'ACTIVE' end    " +
        //                        "from V_M_AGENTS a    " +
        //                        "left join PR_MARKET_SEGMENT b on a.MARKET_SEGMENT = b.CODE  " +
        //                        "left join (select AGENT_CODE,max(UPLINER)UPLINER,max(UPLINER_NAME)UPLINER_NAME,max(LEVEL)LEVEL,max(START_DATE)START_DATE from V_M_AGENTS_MOVEMENT group by AGENT_CODE) c on a.CODE = c.AGENT_CODE  " +
        //                        "left join (select AGENT_CODE,max(UPLINER)UPLINER,max(UPLINER_NAME)UPLINER_NAME,max(LEVEL)LEVEL,max(START_DATE)START_DATE from V_M_AGENTS_MOVEMENT group by AGENT_CODE) c2 on c.UPLINER = c2.AGENT_CODE " +
        //                        "left join (select distinct(AGENT_CODE),b.FRONT_NAME,b.MID_NAME,b.LAST_NAME,a.DESCR from DATA_COMMISSION a inner join M_AGENTS b on a.AGENT_CODE = b.CODE where a.COMM_TYPE = 'BR' ) e on e.DESCR like '%' + a.CODE + ''  " +
        //                        "left join (select a.CODE,a.ACCBANK,a.ACCNO,a.ACCNAME,a.ACCBANK_DESCR from V_M_AGENTS_ACCOUNT a where a.ACCNAME not like '%test%' ) f on f.CODE = a.CODE  " +
        //                        "left join (select CODE,max(STARTDATE)STARTDATE,max(ENDDATE)ENDDATE,max(LICENCE_NO)LICENCE_NO from M_AGENT_LICENCE group by CODE) g on a.CODE = g.CODE  " +
        //                        "left join (select AGENT_CODE,VAL from M_AGENT_OTHER_INFO where FIELD_CODE = 'AGN09') NPWP on NPWP.AGENT_CODE  = a.CODE	 " +
        //                        "left join (select AGENT_CODE,VAL from M_AGENT_OTHER_INFO where FIELD_CODE = 'AGN14') TAX_OBJECT on TAX_OBJECT.AGENT_CODE  = a.CODE	 " +
        //                        "left join (select AGENT_CODE,VAL from M_AGENT_OTHER_INFO where FIELD_CODE = 'AGN04') ADD_ID on ADD_ID.AGENT_CODE  = a.CODE " +
        //                        "left join (select AGENT_CODE,VAL from M_AGENT_OTHER_INFO where FIELD_CODE = 'AGN07A') ADD_DOM on ADD_DOM.AGENT_CODE  = a.CODE	 " +
        //                        "left join (select AGENT_CODE,VAL from M_AGENT_OTHER_INFO where FIELD_CODE = 'AGN07C') CITY on CITY.AGENT_CODE  = a.CODE	 " +
        //                        "left join V_AGENT_PROMOTOR i on i.PROMOTED_AGENT = a.CODE and i.PROMOTED_AGENT_LEVEL = '013' " +
        //                        "left join V_AGENT_PROMOTOR j on j.PROMOTED_AGENT = a.CODE and j.PROMOTED_AGENT_LEVEL = '012' " +
        //                        "where   " +
        //                        "1=1 " + where +
        //                        "order by " +
        //                        "FULLNAME";

        //    conn.ExecuteQuery();

        //    DataTable dt;
        //    dt = new DataTable();
        //    dt = conn.GetDataTable().Copy();

        //    GlobalUse.ExportDataSetToExcel(dt, this, "AGENTS", true);
        //}
    }
}