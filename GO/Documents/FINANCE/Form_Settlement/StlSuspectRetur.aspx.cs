using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace FINANCE.Form_Settlement
{
    public partial class StlSuspectRetur : System.Web.UI.Page
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
            conn.QueryString = "select NOREK,BANK, count(BANK) from V_REKENING_JURNAL_RETUR_SUSPECT group by NOREK,BANK order by 3 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
                DDL_BANK.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
        }

        protected void FillDGR()
        {
            LB_RESULT.Text = "";
            string where = "";

            if (DDL_BANK.SelectedValue != "")
            {
                where = where + " and a.NOREK = '" + DDL_BANK.SelectedValue + "' ";
            }

            if (TXT_DESCR.Text.Trim() != "")
            {
                where = where + " and a.DESCR like '%" + TXT_DESCR.Text.Trim() + "%' ";
            }

            if (TXT_DATE1.Text.Trim() != "")
            {
                where = where + " and a.POST_DATE >= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE1.Text.Trim(), "d/M/yyyy") + "')";
            }

            if (TXT_DATE2.Text.Trim() != "")
            {
                where = where + " and a.POST_DATE <= convert(date,'" + GlobalUse.GlobalDateFormat(TXT_DATE2.Text.Trim(), "d/M/yyyy") + "')";
            }

            conn.QueryString = "select " +
                                "TRXID, " +
                                "BANK, " +
                                "POST_DATE = convert(varchar(20),POST_DATE,106), " +
                                "DESCR, " +
                                "AMOUNT = replace(convert(varchar(100),convert(money,ORI_AMOUNT),1),'.00',''), " +
                                "ACC_NO, " +
                                "ACC_NAME, " +
                                "STL_DESCR, " +
                                "ORI_POST_DATE = convert(varchar(20),ORI_POST_DATE,106), " +
                                "REKAPID, " +
                                "URL " +
                                "from V_REKENING_JURNAL_RETUR_SUSPECT a " +
                                "where 1=1 " + where +
                                "order by " +
                                "a.POST_DATE, " +
                                "a.TRXID, " +
                                "a.ORI_POST_DATE";
            conn.ExecuteQuery();
            DataTable dt;
            dt = new DataTable();
            dt = conn.GetDataTable().Copy();
            DGR.DataSource = dt;
            DGR.DataBind();

            LB_RESULT.Text = "Records : " + conn.GetRowCount() + "<BR>";

            bool bColorChange = false;
            System.Drawing.Color color = new System.Drawing.Color();
            color = System.Drawing.Color.Cyan;

            for (int i = 0; i < DGR.Items.Count; i++)
            {
                TextBox txtACCNO = (TextBox)DGR.Items[i].FindControl("TXT_ACCNO");
                TextBox txtACCNAME = (TextBox)DGR.Items[i].FindControl("TXT_ACCNAME");
                LinkButton lbREKAPID = (LinkButton)DGR.Items[i].FindControl("LB_REKAPID");
                Label lbSTL = (Label)DGR.Items[i].FindControl("LB_STL");
                Button btSAVE = (Button)DGR.Items[i].FindControl("BT_SAVE");

                txtACCNO.Text = DGR.Items[i].Cells[5].Text.Replace("&nbsp;", "").Trim();
                txtACCNAME.Text = DGR.Items[i].Cells[6].Text.Replace("&nbsp;", "").Trim();
                lbSTL.Text = DGR.Items[i].Cells[7].Text.Replace("&nbsp;", "").Trim();
                lbREKAPID.Text = DGR.Items[i].Cells[8].Text.Replace("&nbsp;", "").Trim();

                lbREKAPID.Attributes.Add("onclick", "window.open('" + DGR.Items[i].Cells[9].Text.Replace("&nbsp;", "") + "','INVOICE','height=500px,width=700px,left=0,top=0,status=no,toolbar=no,scrollbars=no,titlebar=no,menubar=no,location=no,dependent=yes');");
                btSAVE.Attributes.Add("onclick", "if(!confirm('Anda yakin untuk SAVE ?')){return false;};");

                if (i > 0)
                {
                    if (DGR.Items[i].Cells[0].Text == DGR.Items[i - 1].Cells[0].Text)
                    {
                        bColorChange = false;
                        DGR.Items[i].BackColor = color;
                        DGR.Items[i - 1].BackColor = color;
                    }
                    else
                    {
                        if(!bColorChange)
                        {
                            bColorChange = true;
                            if (color == System.Drawing.Color.Cyan)
                                color = System.Drawing.Color.LawnGreen;
                            else
                                color = System.Drawing.Color.Cyan;
                        }
                    }
                }
            }
        }

        protected void DGR_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            DGR.CurrentPageIndex = e.NewPageIndex;
            FillDGR();
        }

        protected void DGR_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Save")
            {

            }
        }

        protected void BT_SEARCH_Click(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }

        protected void DDL_BANK_SelectedIndexChanged(object sender, EventArgs e)
        {
            DGR.CurrentPageIndex = 0;
            FillDGR();
        }
    }
}