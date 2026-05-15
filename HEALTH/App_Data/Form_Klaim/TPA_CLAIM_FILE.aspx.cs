using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DMS.DBConnection;

namespace HEALTH.Form_Klaim
{
    public partial class TPA_CLAIM_FILE : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_TPA.Text = Request.QueryString["TPA"];
                LB_BATCH.Text = Request.QueryString["BATCH_ID"];
                LB_TIPE.Text = Request.QueryString["TIPE"];
                Setup();
                FillDGR();
            }
        }

        protected void Setup()
        {
            DDL_DESTINATION.Items.Clear();
            DDL_SOURCE.Items.Clear();
            for (int i = 1; i < 61; i++)
            {
                string F = i.ToString();
                if (F.Length == 1)
                    F = "0" + F;
                F = "F" + F;
                DDL_DESTINATION.Items.Add(new ListItem(F, F));
                DDL_SOURCE.Items.Add(new ListItem(F, F));
            }

            conn.QueryString = "select SEQ,DESCR from TPA_CLAIM_FILE_QUERY " +
                                "where " +
                                "TPA='" + LB_TPA.Text + "' " +
                                "and FILETYPE='" + LB_TIPE.Text + "' " +
                                "order by SEQ";
            conn.ExecuteQuery();
            DDL_QUERY.Items.Clear();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_QUERY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }
        }

        protected void FillDGR()
        {
            conn.QueryString = "select SQL from TPA_CLAIM_FILE_QUERY " +
                                "where " +
                                "TPA='" + LB_TPA.Text + "' " +
                                "and FILETYPE='" + LB_TIPE.Text + "' " +
                                "and SEQ='" + DDL_QUERY.SelectedValue + "'";
            conn.ExecuteQuery();

            string sql = conn.GetFieldValue(0, 0).ToString().Replace("@batch", "'" + LB_BATCH.Text + "'");
            conn.QueryString = sql;
            conn.ExecuteQuery();

            LB_RECORD.Text = "Records : " + conn.GetRowCount().ToString();

            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox TXT01 = (TextBox)DGR.Items[i].FindControl("TXT01");
                TextBox TXT02 = (TextBox)DGR.Items[i].FindControl("TXT02");
                TextBox TXT03 = (TextBox)DGR.Items[i].FindControl("TXT03");
                TextBox TXT04 = (TextBox)DGR.Items[i].FindControl("TXT04");
                TextBox TXT05 = (TextBox)DGR.Items[i].FindControl("TXT05");
                TextBox TXT06 = (TextBox)DGR.Items[i].FindControl("TXT06");
                TextBox TXT07 = (TextBox)DGR.Items[i].FindControl("TXT07");
                TextBox TXT08 = (TextBox)DGR.Items[i].FindControl("TXT08");
                TextBox TXT09 = (TextBox)DGR.Items[i].FindControl("TXT09");
                TextBox TXT10 = (TextBox)DGR.Items[i].FindControl("TXT10");
                TextBox TXT11 = (TextBox)DGR.Items[i].FindControl("TXT11");
                TextBox TXT12 = (TextBox)DGR.Items[i].FindControl("TXT12");
                TextBox TXT13 = (TextBox)DGR.Items[i].FindControl("TXT13");
                TextBox TXT14 = (TextBox)DGR.Items[i].FindControl("TXT14");
                TextBox TXT15 = (TextBox)DGR.Items[i].FindControl("TXT15");
                TextBox TXT16 = (TextBox)DGR.Items[i].FindControl("TXT16");
                TextBox TXT17 = (TextBox)DGR.Items[i].FindControl("TXT17");
                TextBox TXT18 = (TextBox)DGR.Items[i].FindControl("TXT18");
                TextBox TXT19 = (TextBox)DGR.Items[i].FindControl("TXT19");
                TextBox TXT20 = (TextBox)DGR.Items[i].FindControl("TXT20");
                TextBox TXT21 = (TextBox)DGR.Items[i].FindControl("TXT21");
                TextBox TXT22 = (TextBox)DGR.Items[i].FindControl("TXT22");
                TextBox TXT23 = (TextBox)DGR.Items[i].FindControl("TXT23");
                TextBox TXT24 = (TextBox)DGR.Items[i].FindControl("TXT24");
                TextBox TXT25 = (TextBox)DGR.Items[i].FindControl("TXT25");
                TextBox TXT26 = (TextBox)DGR.Items[i].FindControl("TXT26");
                TextBox TXT27 = (TextBox)DGR.Items[i].FindControl("TXT27");
                TextBox TXT28 = (TextBox)DGR.Items[i].FindControl("TXT28");
                TextBox TXT29 = (TextBox)DGR.Items[i].FindControl("TXT29");
                TextBox TXT30 = (TextBox)DGR.Items[i].FindControl("TXT30");
                TextBox TXT31 = (TextBox)DGR.Items[i].FindControl("TXT31");
                TextBox TXT32 = (TextBox)DGR.Items[i].FindControl("TXT32");
                TextBox TXT33 = (TextBox)DGR.Items[i].FindControl("TXT33");
                TextBox TXT34 = (TextBox)DGR.Items[i].FindControl("TXT34");
                TextBox TXT35 = (TextBox)DGR.Items[i].FindControl("TXT35");
                TextBox TXT36 = (TextBox)DGR.Items[i].FindControl("TXT36");
                TextBox TXT37 = (TextBox)DGR.Items[i].FindControl("TXT37");
                TextBox TXT38 = (TextBox)DGR.Items[i].FindControl("TXT38");
                TextBox TXT39 = (TextBox)DGR.Items[i].FindControl("TXT39");
                TextBox TXT40 = (TextBox)DGR.Items[i].FindControl("TXT40");
                TextBox TXT41 = (TextBox)DGR.Items[i].FindControl("TXT41");
                TextBox TXT42 = (TextBox)DGR.Items[i].FindControl("TXT42");
                TextBox TXT43 = (TextBox)DGR.Items[i].FindControl("TXT43");
                TextBox TXT44 = (TextBox)DGR.Items[i].FindControl("TXT44");
                TextBox TXT45 = (TextBox)DGR.Items[i].FindControl("TXT45");
                TextBox TXT46 = (TextBox)DGR.Items[i].FindControl("TXT46");
                TextBox TXT47 = (TextBox)DGR.Items[i].FindControl("TXT47");
                TextBox TXT48 = (TextBox)DGR.Items[i].FindControl("TXT48");
                TextBox TXT49 = (TextBox)DGR.Items[i].FindControl("TXT49");
                TextBox TXT50 = (TextBox)DGR.Items[i].FindControl("TXT50");
                TextBox TXT51 = (TextBox)DGR.Items[i].FindControl("TXT51");
                TextBox TXT52 = (TextBox)DGR.Items[i].FindControl("TXT52");
                TextBox TXT53 = (TextBox)DGR.Items[i].FindControl("TXT53");
                TextBox TXT54 = (TextBox)DGR.Items[i].FindControl("TXT54");
                TextBox TXT55 = (TextBox)DGR.Items[i].FindControl("TXT55");
                TextBox TXT56 = (TextBox)DGR.Items[i].FindControl("TXT56");
                TextBox TXT57 = (TextBox)DGR.Items[i].FindControl("TXT57");
                TextBox TXT58 = (TextBox)DGR.Items[i].FindControl("TXT58");
                TextBox TXT59 = (TextBox)DGR.Items[i].FindControl("TXT59");
                TextBox TXT60 = (TextBox)DGR.Items[i].FindControl("TXT60");

                int seq = 1;
                TXT01.Text = DGR.Items[i].Cells[seq + 1].Text.Replace("&nbsp;", "");
                TXT02.Text = DGR.Items[i].Cells[seq + 2].Text.Replace("&nbsp;", "");
                TXT03.Text = DGR.Items[i].Cells[seq + 3].Text.Replace("&nbsp;", "");
                TXT04.Text = DGR.Items[i].Cells[seq + 4].Text.Replace("&nbsp;", "");
                TXT05.Text = DGR.Items[i].Cells[seq + 5].Text.Replace("&nbsp;", "");
                TXT06.Text = DGR.Items[i].Cells[seq + 6].Text.Replace("&nbsp;", "");
                TXT07.Text = DGR.Items[i].Cells[seq + 7].Text.Replace("&nbsp;", "");
                TXT08.Text = DGR.Items[i].Cells[seq + 8].Text.Replace("&nbsp;", "");
                TXT09.Text = DGR.Items[i].Cells[seq + 9].Text.Replace("&nbsp;", "");
                TXT10.Text = DGR.Items[i].Cells[seq + 10].Text.Replace("&nbsp;", "");
                TXT11.Text = DGR.Items[i].Cells[seq + 11].Text.Replace("&nbsp;", "");
                TXT12.Text = DGR.Items[i].Cells[seq + 12].Text.Replace("&nbsp;", "");
                TXT13.Text = DGR.Items[i].Cells[seq + 13].Text.Replace("&nbsp;", "");
                TXT14.Text = DGR.Items[i].Cells[seq + 14].Text.Replace("&nbsp;", "");
                TXT15.Text = DGR.Items[i].Cells[seq + 15].Text.Replace("&nbsp;", "");
                TXT16.Text = DGR.Items[i].Cells[seq + 16].Text.Replace("&nbsp;", "");
                TXT17.Text = DGR.Items[i].Cells[seq + 17].Text.Replace("&nbsp;", "");
                TXT18.Text = DGR.Items[i].Cells[seq + 18].Text.Replace("&nbsp;", "");
                TXT19.Text = DGR.Items[i].Cells[seq + 19].Text.Replace("&nbsp;", "");
                TXT20.Text = DGR.Items[i].Cells[seq + 20].Text.Replace("&nbsp;", "");
                TXT21.Text = DGR.Items[i].Cells[seq + 21].Text.Replace("&nbsp;", "");
                TXT22.Text = DGR.Items[i].Cells[seq + 22].Text.Replace("&nbsp;", "");
                TXT23.Text = DGR.Items[i].Cells[seq + 23].Text.Replace("&nbsp;", "");
                TXT24.Text = DGR.Items[i].Cells[seq + 24].Text.Replace("&nbsp;", "");
                TXT25.Text = DGR.Items[i].Cells[seq + 25].Text.Replace("&nbsp;", "");
                TXT26.Text = DGR.Items[i].Cells[seq + 26].Text.Replace("&nbsp;", "");
                TXT27.Text = DGR.Items[i].Cells[seq + 27].Text.Replace("&nbsp;", "");
                TXT28.Text = DGR.Items[i].Cells[seq + 28].Text.Replace("&nbsp;", "");
                TXT29.Text = DGR.Items[i].Cells[seq + 29].Text.Replace("&nbsp;", "");
                TXT30.Text = DGR.Items[i].Cells[seq + 30].Text.Replace("&nbsp;", "");
                TXT31.Text = DGR.Items[i].Cells[seq + 31].Text.Replace("&nbsp;", "");
                TXT32.Text = DGR.Items[i].Cells[seq + 32].Text.Replace("&nbsp;", "");
                TXT33.Text = DGR.Items[i].Cells[seq + 33].Text.Replace("&nbsp;", "");
                TXT34.Text = DGR.Items[i].Cells[seq + 34].Text.Replace("&nbsp;", "");
                TXT35.Text = DGR.Items[i].Cells[seq + 35].Text.Replace("&nbsp;", "");
                TXT36.Text = DGR.Items[i].Cells[seq + 36].Text.Replace("&nbsp;", "");
                TXT37.Text = DGR.Items[i].Cells[seq + 37].Text.Replace("&nbsp;", "");
                TXT38.Text = DGR.Items[i].Cells[seq + 38].Text.Replace("&nbsp;", "");
                TXT39.Text = DGR.Items[i].Cells[seq + 39].Text.Replace("&nbsp;", "");
                TXT40.Text = DGR.Items[i].Cells[seq + 40].Text.Replace("&nbsp;", "");
                TXT41.Text = DGR.Items[i].Cells[seq + 41].Text.Replace("&nbsp;", "");
                TXT42.Text = DGR.Items[i].Cells[seq + 42].Text.Replace("&nbsp;", "");
                TXT43.Text = DGR.Items[i].Cells[seq + 43].Text.Replace("&nbsp;", "");
                TXT44.Text = DGR.Items[i].Cells[seq + 44].Text.Replace("&nbsp;", "");
                TXT45.Text = DGR.Items[i].Cells[seq + 45].Text.Replace("&nbsp;", "");
                TXT46.Text = DGR.Items[i].Cells[seq + 46].Text.Replace("&nbsp;", "");
                TXT47.Text = DGR.Items[i].Cells[seq + 47].Text.Replace("&nbsp;", "");
                TXT48.Text = DGR.Items[i].Cells[seq + 48].Text.Replace("&nbsp;", "");
                TXT49.Text = DGR.Items[i].Cells[seq + 49].Text.Replace("&nbsp;", "");
                TXT50.Text = DGR.Items[i].Cells[seq + 50].Text.Replace("&nbsp;", "");
                TXT51.Text = DGR.Items[i].Cells[seq + 51].Text.Replace("&nbsp;", "");
                TXT52.Text = DGR.Items[i].Cells[seq + 52].Text.Replace("&nbsp;", "");
                TXT53.Text = DGR.Items[i].Cells[seq + 53].Text.Replace("&nbsp;", "");
                TXT54.Text = DGR.Items[i].Cells[seq + 54].Text.Replace("&nbsp;", "");
                TXT55.Text = DGR.Items[i].Cells[seq + 55].Text.Replace("&nbsp;", "");
                TXT56.Text = DGR.Items[i].Cells[seq + 56].Text.Replace("&nbsp;", "");
                TXT57.Text = DGR.Items[i].Cells[seq + 57].Text.Replace("&nbsp;", "");
                TXT58.Text = DGR.Items[i].Cells[seq + 58].Text.Replace("&nbsp;", "");
                TXT59.Text = DGR.Items[i].Cells[seq + 59].Text.Replace("&nbsp;", "");
                TXT60.Text = DGR.Items[i].Cells[seq + 60].Text.Replace("&nbsp;", "");
            }
        }

        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox TXT01 = (TextBox)DGR.Items[i].FindControl("TXT01");
                TextBox TXT02 = (TextBox)DGR.Items[i].FindControl("TXT02");
                TextBox TXT03 = (TextBox)DGR.Items[i].FindControl("TXT03");
                TextBox TXT04 = (TextBox)DGR.Items[i].FindControl("TXT04");
                TextBox TXT05 = (TextBox)DGR.Items[i].FindControl("TXT05");
                TextBox TXT06 = (TextBox)DGR.Items[i].FindControl("TXT06");
                TextBox TXT07 = (TextBox)DGR.Items[i].FindControl("TXT07");
                TextBox TXT08 = (TextBox)DGR.Items[i].FindControl("TXT08");
                TextBox TXT09 = (TextBox)DGR.Items[i].FindControl("TXT09");
                TextBox TXT10 = (TextBox)DGR.Items[i].FindControl("TXT10");
                TextBox TXT11 = (TextBox)DGR.Items[i].FindControl("TXT11");
                TextBox TXT12 = (TextBox)DGR.Items[i].FindControl("TXT12");
                TextBox TXT13 = (TextBox)DGR.Items[i].FindControl("TXT13");
                TextBox TXT14 = (TextBox)DGR.Items[i].FindControl("TXT14");
                TextBox TXT15 = (TextBox)DGR.Items[i].FindControl("TXT15");
                TextBox TXT16 = (TextBox)DGR.Items[i].FindControl("TXT16");
                TextBox TXT17 = (TextBox)DGR.Items[i].FindControl("TXT17");
                TextBox TXT18 = (TextBox)DGR.Items[i].FindControl("TXT18");
                TextBox TXT19 = (TextBox)DGR.Items[i].FindControl("TXT19");
                TextBox TXT20 = (TextBox)DGR.Items[i].FindControl("TXT20");
                TextBox TXT21 = (TextBox)DGR.Items[i].FindControl("TXT21");
                TextBox TXT22 = (TextBox)DGR.Items[i].FindControl("TXT22");
                TextBox TXT23 = (TextBox)DGR.Items[i].FindControl("TXT23");
                TextBox TXT24 = (TextBox)DGR.Items[i].FindControl("TXT24");
                TextBox TXT25 = (TextBox)DGR.Items[i].FindControl("TXT25");
                TextBox TXT26 = (TextBox)DGR.Items[i].FindControl("TXT26");
                TextBox TXT27 = (TextBox)DGR.Items[i].FindControl("TXT27");
                TextBox TXT28 = (TextBox)DGR.Items[i].FindControl("TXT28");
                TextBox TXT29 = (TextBox)DGR.Items[i].FindControl("TXT29");
                TextBox TXT30 = (TextBox)DGR.Items[i].FindControl("TXT30");
                TextBox TXT31 = (TextBox)DGR.Items[i].FindControl("TXT31");
                TextBox TXT32 = (TextBox)DGR.Items[i].FindControl("TXT32");
                TextBox TXT33 = (TextBox)DGR.Items[i].FindControl("TXT33");
                TextBox TXT34 = (TextBox)DGR.Items[i].FindControl("TXT34");
                TextBox TXT35 = (TextBox)DGR.Items[i].FindControl("TXT35");
                TextBox TXT36 = (TextBox)DGR.Items[i].FindControl("TXT36");
                TextBox TXT37 = (TextBox)DGR.Items[i].FindControl("TXT37");
                TextBox TXT38 = (TextBox)DGR.Items[i].FindControl("TXT38");
                TextBox TXT39 = (TextBox)DGR.Items[i].FindControl("TXT39");
                TextBox TXT40 = (TextBox)DGR.Items[i].FindControl("TXT40");
                TextBox TXT41 = (TextBox)DGR.Items[i].FindControl("TXT41");
                TextBox TXT42 = (TextBox)DGR.Items[i].FindControl("TXT42");
                TextBox TXT43 = (TextBox)DGR.Items[i].FindControl("TXT43");
                TextBox TXT44 = (TextBox)DGR.Items[i].FindControl("TXT44");
                TextBox TXT45 = (TextBox)DGR.Items[i].FindControl("TXT45");
                TextBox TXT46 = (TextBox)DGR.Items[i].FindControl("TXT46");
                TextBox TXT47 = (TextBox)DGR.Items[i].FindControl("TXT47");
                TextBox TXT48 = (TextBox)DGR.Items[i].FindControl("TXT48");
                TextBox TXT49 = (TextBox)DGR.Items[i].FindControl("TXT49");
                TextBox TXT50 = (TextBox)DGR.Items[i].FindControl("TXT50");
                TextBox TXT51 = (TextBox)DGR.Items[i].FindControl("TXT51");
                TextBox TXT52 = (TextBox)DGR.Items[i].FindControl("TXT52");
                TextBox TXT53 = (TextBox)DGR.Items[i].FindControl("TXT53");
                TextBox TXT54 = (TextBox)DGR.Items[i].FindControl("TXT54");
                TextBox TXT55 = (TextBox)DGR.Items[i].FindControl("TXT55");
                TextBox TXT56 = (TextBox)DGR.Items[i].FindControl("TXT56");
                TextBox TXT57 = (TextBox)DGR.Items[i].FindControl("TXT57");
                TextBox TXT58 = (TextBox)DGR.Items[i].FindControl("TXT58");
                TextBox TXT59 = (TextBox)DGR.Items[i].FindControl("TXT59");
                TextBox TXT60 = (TextBox)DGR.Items[i].FindControl("TXT60");

                conn.QueryString = "update TPA_CLAIM_HEADER set " +
                                    "F01 = '" + TXT01.Text.Trim().Replace("'", "`") + "', " +
                                    "F02 = '" + TXT02.Text.Trim().Replace("'", "`") + "', " +
                                    "F03 = '" + TXT03.Text.Trim().Replace("'", "`") + "', " +
                                    "F04 = '" + TXT04.Text.Trim().Replace("'", "`") + "', " +
                                    "F05 = '" + TXT05.Text.Trim().Replace("'", "`") + "', " +
                                    "F06 = '" + TXT06.Text.Trim().Replace("'", "`") + "', " +
                                    "F07 = '" + TXT07.Text.Trim().Replace("'", "`") + "', " +
                                    "F08 = '" + TXT08.Text.Trim().Replace("'", "`") + "', " +
                                    "F09 = '" + TXT09.Text.Trim().Replace("'", "`") + "', " +
                                    "F10 = '" + TXT10.Text.Trim().Replace("'", "`") + "', " +
                                    "F11 = '" + TXT11.Text.Trim().Replace("'", "`") + "', " +
                                    "F12 = '" + TXT12.Text.Trim().Replace("'", "`") + "', " +
                                    "F13 = '" + TXT13.Text.Trim().Replace("'", "`") + "', " +
                                    "F14 = '" + TXT14.Text.Trim().Replace("'", "`") + "', " +
                                    "F15 = '" + TXT15.Text.Trim().Replace("'", "`") + "', " +
                                    "F16 = '" + TXT16.Text.Trim().Replace("'", "`") + "', " +
                                    "F17 = '" + TXT17.Text.Trim().Replace("'", "`") + "', " +
                                    "F18 = '" + TXT18.Text.Trim().Replace("'", "`") + "', " +
                                    "F19 = '" + TXT19.Text.Trim().Replace("'", "`") + "', " +
                                    "F20 = '" + TXT20.Text.Trim().Replace("'", "`") + "', " +
                                    "F21 = '" + TXT21.Text.Trim().Replace("'", "`") + "', " +
                                    "F22 = '" + TXT22.Text.Trim().Replace("'", "`") + "', " +
                                    "F23 = '" + TXT23.Text.Trim().Replace("'", "`") + "', " +
                                    "F24 = '" + TXT24.Text.Trim().Replace("'", "`") + "', " +
                                    "F25 = '" + TXT25.Text.Trim().Replace("'", "`") + "', " +
                                    "F26 = '" + TXT26.Text.Trim().Replace("'", "`") + "', " +
                                    "F27 = '" + TXT27.Text.Trim().Replace("'", "`") + "', " +
                                    "F28 = '" + TXT28.Text.Trim().Replace("'", "`") + "', " +
                                    "F29 = '" + TXT29.Text.Trim().Replace("'", "`") + "', " +
                                    "F30 = '" + TXT30.Text.Trim().Replace("'", "`") + "', " +
                                    "F31 = '" + TXT31.Text.Trim().Replace("'", "`") + "', " +
                                    "F32 = '" + TXT32.Text.Trim().Replace("'", "`") + "', " +
                                    "F33 = '" + TXT33.Text.Trim().Replace("'", "`") + "', " +
                                    "F34 = '" + TXT34.Text.Trim().Replace("'", "`") + "', " +
                                    "F35 = '" + TXT35.Text.Trim().Replace("'", "`") + "', " +
                                    "F36 = '" + TXT36.Text.Trim().Replace("'", "`") + "', " +
                                    "F37 = '" + TXT37.Text.Trim().Replace("'", "`") + "', " +
                                    "F38 = '" + TXT38.Text.Trim().Replace("'", "`") + "', " +
                                    "F39 = '" + TXT39.Text.Trim().Replace("'", "`") + "', " +
                                    "F40 = '" + TXT40.Text.Trim().Replace("'", "`") + "', " +
                                    "F41 = '" + TXT41.Text.Trim().Replace("'", "`") + "', " +
                                    "F42 = '" + TXT42.Text.Trim().Replace("'", "`") + "', " +
                                    "F43 = '" + TXT43.Text.Trim().Replace("'", "`") + "', " +
                                    "F44 = '" + TXT44.Text.Trim().Replace("'", "`") + "', " +
                                    "F45 = '" + TXT45.Text.Trim().Replace("'", "`") + "', " +
                                    "F46 = '" + TXT46.Text.Trim().Replace("'", "`") + "', " +
                                    "F47 = '" + TXT47.Text.Trim().Replace("'", "`") + "', " +
                                    "F48 = '" + TXT48.Text.Trim().Replace("'", "`") + "', " +
                                    "F49 = '" + TXT49.Text.Trim().Replace("'", "`") + "', " +
                                    "F50 = '" + TXT50.Text.Trim().Replace("'", "`") + "', " +
                                    "F51 = '" + TXT51.Text.Trim().Replace("'", "`") + "', " +
                                    "F52 = '" + TXT52.Text.Trim().Replace("'", "`") + "', " +
                                    "F53 = '" + TXT53.Text.Trim().Replace("'", "`") + "', " +
                                    "F54 = '" + TXT54.Text.Trim().Replace("'", "`") + "', " +
                                    "F55 = '" + TXT55.Text.Trim().Replace("'", "`") + "', " +
                                    "F56 = '" + TXT56.Text.Trim().Replace("'", "`") + "', " +
                                    "F57 = '" + TXT57.Text.Trim().Replace("'", "`") + "', " +
                                    "F58 = '" + TXT58.Text.Trim().Replace("'", "`") + "', " +
                                    "F59 = '" + TXT59.Text.Trim().Replace("'", "`") + "', " +
                                    "F60 = '" + TXT60.Text.Trim().Replace("'", "`") + "' " +
                                    "where " +
                                    "BATCH_ID='" + DGR.Items[i].Cells[0].Text.Trim().Replace("'", "`") + "' " +
                                    "and SEQ='" + DGR.Items[i].Cells[1].Text.Trim().Replace("'", "`") + "'";
                //try
                //{
                conn.ExecuteNonQuery();
                //}
                //catch { }
            }

            try
            {
                conn.QueryString = "exec SP_CLM_TPA_UNPROCESS";
                conn.ExecuteQuery(50000);
            }
            catch { }

            FillDGR();
        }

        protected void BT_COPY_Click(object sender, EventArgs e)
        {
            if (DDL_DESTINATION.SelectedValue == DDL_SOURCE.SelectedValue)
                return;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                string DST = DDL_DESTINATION.SelectedValue.Replace("F", "TXT");
                string SRC = DDL_SOURCE.SelectedValue.Replace("F", "TXT");

                TextBox TXTDST = (TextBox)DGR.Items[i].FindControl(DST);
                TextBox TXTSRC = (TextBox)DGR.Items[i].FindControl(SRC);

                conn.QueryString = "update TPA_CLAIM_HEADER set " +
                                    DDL_DESTINATION.SelectedValue + "=isnull(" + DDL_SOURCE.SelectedValue + ",'') " +
                                    "where " +
                                    "BATCH_ID='" + DGR.Items[i].Cells[0].Text.Trim().Replace("'", "`") + "' " +
                                    "and SEQ='" + DGR.Items[i].Cells[1].Text.Trim().Replace("'", "`") + "'";

                try
                {
                    conn.ExecuteNonQuery();
                }
                catch { }
            }
            FillDGR();
        }

        protected void BT_COPY0_Click(object sender, EventArgs e)
        {
            if (DDL_DESTINATION.SelectedValue == DDL_SOURCE.SelectedValue)
                return;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                string DST = DDL_DESTINATION.SelectedValue.Replace("F", "TXT");
                string SRC = DDL_SOURCE.SelectedValue.Replace("F", "TXT");

                TextBox TXTDST = (TextBox)DGR.Items[i].FindControl(DST);
                TextBox TXTSRC = (TextBox)DGR.Items[i].FindControl(SRC);

                conn.QueryString = "update TPA_CLAIM_HEADER set " +
                                    DDL_DESTINATION.SelectedValue + "=isnull(" + DDL_DESTINATION.SelectedValue + ",'') + ' ' + isnull(" + DDL_SOURCE.SelectedValue + ",'') " +
                                    "where " +
                                    "BATCH_ID='" + DGR.Items[i].Cells[0].Text.Trim().Replace("'", "`") + "' " +
                                    "and SEQ='" + DGR.Items[i].Cells[1].Text.Trim().Replace("'", "`") + "'";

                try
                {
                    conn.ExecuteNonQuery();
                }
                catch { }
            }
            FillDGR();
        }

        protected void DDL_QUERY_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDGR();
        }
    }
}