using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;
using System.Data;

namespace LQ.Form_Client
{
    public partial class QuotationMedicalQuestion : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LB_REGNO.Text = Request.QueryString["REGNO"].ToString();
                LoadQuestion();
            }
        }

        protected void LoadQuestion()
        {
            conn.QueryString = "exec SP_APPLICATION_TC_QUESTIONS '" + LB_REGNO.Text + "'";
            conn.ExecuteQuery();
            DGR_QUESTION.DataSource = conn.GetDataTable().Copy();
            DGR_QUESTION.DataBind();

            for (int j = 0; j < DGR_QUESTION.Items.Count; j++)
            {
                Label lbDESCR = (Label)DGR_QUESTION.Items[j].FindControl("LB_DESCR");
                DropDownList ddl = (DropDownList)DGR_QUESTION.Items[j].FindControl("DDL_REFF");
                TextBox txtVAL = (TextBox)DGR_QUESTION.Items[j].FindControl("TXT_VAL");
                Label lbQUESTION = (Label)DGR_QUESTION.Items[j].FindControl("LB_QUESTION");
                TextBox txtNEXTVAL = (TextBox)DGR_QUESTION.Items[j].FindControl("TXT_NEXTVAL");
                Label lbUNCHECKED = (Label)DGR_QUESTION.Items[j].FindControl("LB_UNCHECKED");
                System.Web.UI.HtmlControls.HtmlTableRow trQUESTION = (System.Web.UI.HtmlControls.HtmlTableRow)DGR_QUESTION.Items[j].FindControl("TR_NEXTQUESTION");

                lbDESCR.Text = DGR_QUESTION.Items[j].Cells[1].Text.Replace("&nbsp;", "");

                if (DGR_QUESTION.Items[j].Cells[2].Text.Replace("&nbsp;", "") != "")
                {
                    trQUESTION.Visible = true;
                    lbQUESTION.Visible = true;
                    txtNEXTVAL.Visible = true;
                    lbQUESTION.Text = DGR_QUESTION.Items[j].Cells[2].Text.Replace("&nbsp;", "");
                    txtNEXTVAL.Text = DGR_QUESTION.Items[j].Cells[6].Text.Replace("&nbsp;", "");
                }

                if (DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "") != "")
                {
                    ddl.Visible = true;
                    conn.QueryString = DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "");
                    conn.ExecuteQuery();
                    for (int k = 0; k < conn.GetRowCount(); k++)
                        ddl.Items.Add(new ListItem(conn.GetFieldValue(k, 1).ToString(), conn.GetFieldValue(k, 0).ToString()));
                    try
                    {
                        ddl.SelectedValue = DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                    }
                    catch { }
                }
                else
                {
                    txtVAL.Visible = true;
                    switch (DGR_QUESTION.Items[j].Cells[3].Text)
                    {
                        case "STR": txtVAL.Text = DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                            break;
                        case "INT": txtVAL.Text = DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                            txtVAL.Style["text-align"] = "center";
                            txtVAL.Width = 50;
                            break;
                        case "FLO": try
                            {
                                conn.QueryString = "select VAL = replace(convert(varchar(100),convert(money," + DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "") + "),1),'.00','')";
                                conn.ExecuteQuery();
                                txtVAL.Text = conn.GetFieldValue("VAL").ToString();
                                txtVAL.Style["text-align"] = "right";
                            }
                            catch { }
                            break;
                        case "BIT": txtVAL.Visible = false;
                            ddl.Visible = true;
                            ddl.Items.Add(new ListItem("NO", "0"));
                            ddl.Items.Add(new ListItem("YES", "1"));
                            try
                            {
                                ddl.SelectedValue = DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "");
                            }
                            catch { }
                            break;

                    }
                }

                //bool bCompleted = true;
                //if (DGR_QUESTION.Items[j].Cells[4].Text.Replace("&nbsp;", "") == "")
                //    bCompleted = false;
                ////if (DGR_QUESTION.Items[j].Cells[3].Text.Replace("&nbsp;", "") != "" && DGR_QUESTION.Items[j].Cells[5].Text.Replace("&nbsp;", "") == "")
                ////    bCompleted = false;
                //if (!bCompleted)
                //{
                //    lbUNCHECKED.Visible = true;
                //    DGR_QUESTION.Items[j].Cells[DGR_QUESTION.Columns.Count - 1].BackColor = System.Drawing.Color.Pink;
                //}
            }
        }
    }
}