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
    public partial class EndorsementPengkinianDataDetail : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                LoadMember(LB_ID.Text);
            }
        }

        protected void Setup()
        {
            LB_REGNO.Text = Request.QueryString["regno"].ToString();
            LB_SEQ.Text = Request.QueryString["seq"].ToString();
            LB_TYPE.Text = Request.QueryString["type"].ToString();
            LB_ID.Text = Request.QueryString["ID"];
            LB_MEMBER_TYPE.Text = Request.QueryString["MEMBER_TYPE"];


            //if (LB_TYPE.Text != "ALN01" && LB_TYPE.Text != "ALN09")
            //{
            //    TXT_DOB.Enabled = true;
            //}

            //conn.QueryString = "select CODE,DESCR from PR_MEMBER_RELATIONSHIP";
            //conn.ExecuteQuery();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_RELATION.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}

            //conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_PROPINSI";
            //conn.ExecuteQuery();
            ////DDL_PROVINCE.Items.Add(new ListItem("", ""));
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_PROVINCE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}

            //conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_CITIZENSHIP";
            //conn.ExecuteQuery();
            ////DDL_CITIZENSHIP.Items.Add(new ListItem("", ""));
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_CITIZENSHIP.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}

            //conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_COUNTRY";
            //conn.ExecuteQuery();
            ////DDL_COUNTRY.Items.Add(new ListItem("", ""));
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_COUNTRY.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}

            //conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_IDTYPE";
            //conn.ExecuteQuery();
            ////DDL_IDTYPE.Items.Add(new ListItem("", ""));
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_IDTYPE.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}

            //conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_JOB";
            //conn.ExecuteQuery();
            //DDL_JOB.Items.Add(new ListItem("", ""));
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_JOB.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}

            //conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_MARITAL";
            //conn.ExecuteQuery();
            //DDL_MARITAL.Items.Add(new ListItem("", ""));
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_MARITAL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}

            //conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_RELIGION";
            //conn.ExecuteQuery();
            //DDL_RELIGION.Items.Clear();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_RELIGION.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}

            //conn.QueryString = "select CODE,DESCR from CLIENT_BASE.dbo.PR_EDUCATION";
            //conn.ExecuteQuery();
            //DDL_EDUCATION.Items.Clear();
            //for (int i = 0; i < conn.GetRowCount(); i++)
            //{
            //    DDL_EDUCATION.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            //}
        }

        protected void LoadMember(string ID)
        {
            if (ID.Trim() == "")
                return;

            //conn.QueryString = "select " +
            //                    "FULLNAME, " +
            //                    "SEX, " +
            //                    "MMN, " +
            //                    "DOB = convert(varchar(20), DOB, 103), " +
            //                    "POB, " +
            //                    "JOB, " +
            //                    "RELIGION, " +
            //                    "MARITAL_STATUS, " +
            //                    "ID_TYPE, " +
            //                    "ID_NO, " +
            //                    "TAX_NO, " +
            //                    "CITIZENSHIP, " +
            //                    "PHONE_1, " +
            //                    "PHONE_2, " +
            //                    "EMAIL, " +
            //                    "ADDRESS_1, " +
            //                    "ADDRESS_2, " +
            //                    "CITY, " +
            //                    "PROVINCE, " +
            //                    "COUNTRY, " +
            //                    "ZIP_CODE, " +
            //                    "EDUCATION " +
            //                    "from	APPLICATION_ENDORSEMENT_MEMBER_DETAIL " +
            //                    "where " +
            //                    "REGNO                  = '" + LB_REGNO.Text + "' " +
            //                    "and SEQ                = '" + LB_SEQ.Text + "' " +
            //                    "and ENDORSEMENT_TYPE   = '" + LB_TYPE.Text + "' " +
            //                    "and ID                 = '" + ID + "'";

            conn.QueryString = "EXEC SP_APPLICATION_ENDORSEMENT_QUOT_PENGKINIAN_DATA_DETAIL '" + LB_REGNO.Text + "', '" + LB_SEQ.Text + "', '" + ID + "', '" + LB_MEMBER_TYPE.Text + "'";
            conn.ExecuteQuery();

            TXT_IDNO.Text = conn.GetFieldValue("ID_NO").ToString();
            TXT_JOB.Text = conn.GetFieldValue("JOB").ToString();
            TXT_NAME.Text = conn.GetFieldValue("FULLNAME").ToString();
            TXT_ADDRESS1.Text = conn.GetFieldValue("ADDRESS").ToString();
            TXT_RTRWW.Text = conn.GetFieldValue("rt_rw");
            TEXT_KELKEC.Text = conn.GetFieldValue("kelurahan_kecamatan");
            TEXT_KOTAKABUPATEN.Text = conn.GetFieldValue("kota_kabupaten").ToString();
            TXT_ZIPCODE.Text = conn.GetFieldValue("ZIPCODE").ToString();
            //TXT_PHONE1.Text = conn.GetFieldValue("PHONE").ToString();
            TXT_PHONE2.Text = conn.GetFieldValue("PHONE2").ToString();
            TXT_EMAIL.Text = conn.GetFieldValue("EMAIL").ToString();
            TXT_ADDRESSSURAT.Text = conn.GetFieldValue("alamat_surat_menyurat").ToString();
            TXT_NAMEBANK.Text = conn.GetFieldValue("nama_bank").ToString();
            TXT_REK.Text = conn.GetFieldValue("nomor_rekening").ToString();
            TXT_CABANGBANK.Text = conn.GetFieldValue("cabang_bank").ToString();
            TXT_NPR.Text = conn.GetFieldValue("nama_pemilik_rekening").ToString();
            TXT_SUMBERPENDAPATAN.Text = conn.GetFieldValue("sumber_pendapatan").ToString();


            //TXT_ADDRESS1.Text = conn.GetFieldValue("ADDRESS_1").ToString();
            //TXT_ADDRESS2.Text = conn.GetFieldValue("ADDRESS_2").ToString();
            //TXT_CITY.Text = conn.GetFieldValue("CITY").ToString();





            //TXT_MMN.Text = conn.GetFieldValue("MMN").ToString();


            //TXT_PHONE1.Text = conn.GetFieldValue("PHONE_1").ToString();
            //TXT_PHONE2.Text = conn.GetFieldValue("PHONE_2").ToString();

            //TXT_POB.Text = conn.GetFieldValue("POB").ToString();
            //TXT_TAXNO.Text = conn.GetFieldValue("TAX_NO").ToString();
            //TXT_ZIPCODE.Text = conn.GetFieldValue("ZIP_CODE").ToString();

            //TXT_DOB.Text = conn.GetFieldValue("DOB").ToString();


            //try
            //{
            //    DDL_CITIZENSHIP.SelectedValue = conn.GetFieldValue("CITIZENSHIP").ToString();
            //}
            //catch { }

            //try
            //{
            //    DDL_COUNTRY.SelectedValue = conn.GetFieldValue("COUNTRY").ToString();
            //}
            //catch { }

            //try
            //{
            //    DDL_IDTYPE.SelectedValue = conn.GetFieldValue("ID_TYPE").ToString();
            //}
            //catch { }

            //try
            //{
            //    DDL_JOB.SelectedValue = conn.GetFieldValue("JOB").ToString();
            //}
            //catch { }

            //try
            //{
            //    DDL_MARITAL.SelectedValue = conn.GetFieldValue("MARITAL_STATUS").ToString();
            //}
            //catch { }

            //try
            //{
            //    DDL_PROVINCE.SelectedValue = conn.GetFieldValue("PROVINCE").ToString();
            //}
            //catch { }

            //try
            //{
            //    if (conn.GetFieldValue("RELIGION").ToString() == "")
            //    {
            //        DDL_RELIGION.SelectedValue = "999";
            //    }
            //    else
            //    {
            //        DDL_RELIGION.SelectedValue = conn.GetFieldValue("RELIGION").ToString();
            //    }


            //}
            //catch { }

            //try
            //{
            //    DDL_SEX.SelectedValue = conn.GetFieldValue("SEX").ToString();
            //}
            //catch { }

            //try
            //{
            //    if (conn.GetFieldValue("EDUCATION").ToString() == "")
            //    {
            //        DDL_EDUCATION.SelectedValue = "999";
            //    }
            //    else
            //    {
            //        DDL_EDUCATION.SelectedValue = conn.GetFieldValue("EDUCATION").ToString();
            //    }
            //}
            //catch { }

            try
            {
                //conn.QueryString = "select MEMBER_RELATION from APPLICATION_ENDORSEMENT_MEMBER " +
                //                    "where " +
                //                    "REGNO                  = '" + LB_REGNO.Text + "' " +
                //                    "and SEQ                = '" + LB_SEQ.Text + "' " +
                //                    "and ENDORSEMENT_TYPE   = '" + LB_TYPE.Text + "' " +
                //                    "and MEMBER_TYPE        = '" + LB_MEMBER_TYPE.Text + "'";
                conn.QueryString = "select MEMBER_RELATION from APPLICATION_ENDORSEMENT_QUOT_MEMBER " +
                                    "where " +
                                    "REGNO                  = '" + LB_REGNO.Text + "' " +
                                    "and SEQ                = '" + LB_SEQ.Text + "' " +
                                    "and MEMBER_TYPE        = '" + LB_MEMBER_TYPE.Text + "'";
                conn.ExecuteQuery();
                //DDL_RELATION.SelectedValue = conn.GetFieldValue("MEMBER_RELATION").ToString();
            }
            catch { }

            conn.QueryString = "select REGNO from APPLICATION_MASTER where REGNO = '" + LB_REGNO.Text + "' and convert(varchar(255), MEMBER_ID) = '" + LB_ID.Text + "'";
            conn.ExecuteQuery();
            if (conn.GetRowCount() > 0)
            {
                //TR_RELATION.Visible = false;
            }
        }


        protected void BT_SAVE_Click(object sender, EventArgs e)
        {
            LB_ERROR.Text = "";

            Save();
        }

        protected void Save()
        {
            LB_ERROR.Text = "";

            string ID = "null";
            if (LB_ID.Text != "")
                ID = "'" + LB_ID.Text + "'";

            string address2 = string.Empty;
            


            try
            {
                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MEMBER_PENGKINIAN_DATA_DETAIL_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + LB_TYPE.Text + "'," +
                                    ID + "," +
                                    "'" + LB_MEMBER_TYPE.Text + "'," +
                                    "'" + TXT_NAME.Text.Trim() + "'," +
                                    //"'" + TXT_JOB.Text.Trim() + "'," +
                                    "'" + TXT_IDNO.Text.Trim() + "'," +
                                    //"'" + TXT_PHONE1.Text.Trim() + "'," +
                                    "'" + TXT_PHONE2.Text.Trim() + "'," +
                                    "'" + TXT_EMAIL.Text.Trim() + "'," +
                                    "'" + TXT_ADDRESS1.Text.Trim() + "'," +
                                    "'" + TXT_ADDRESSSURAT.Text.Trim() + "'," +
                                    "'" + TEXT_KOTAKABUPATEN.Text.Trim() + "'," +
                                    "'" + TXT_ZIPCODE.Text.Trim() + "'," +
                                    "'" + TXT_SUMBERPENDAPATAN.Text.Trim() + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";

                //"'" + DDL_RELATION.SelectedValue + "'," +

                //"'" + DDL_SEX.SelectedValue + "'," +
                //"'" + TXT_MMN.Text.Trim() + "'," +
                //"'" + GlobalUse.GlobalDateFormat(TXT_DOB.Text.Trim(), "d/M/yyyy") + "'," +
                //"'" + TXT_POB.Text.Trim() + "'," +
                //"'" + DDL_JOB.SelectedValue + "'," +
                //"'" + DDL_RELIGION.SelectedValue.ToString() + "'," +
                //"'" + DDL_MARITAL.SelectedValue + "'," +
                //"'" + DDL_IDTYPE.SelectedValue + "'," +

                //"'" + TXT_TAXNO.Text.Trim() + "'," +
                //"'" + DDL_CITIZENSHIP.SelectedValue + "'," +


                //"'" + TXT_CITY.Text.Trim() + "'," +
                //"'" + DDL_PROVINCE.SelectedValue + "'," +
                //"'" + DDL_COUNTRY.SelectedValue + "'," +

                //"'" + DDL_EDUCATION.SelectedValue.ToString() + "'," +

                conn.ExecuteQuery();

                ID = conn.GetFieldValue("ID").ToString();

                conn.QueryString = "exec SP_APPLICATION_ENDORSEMENT_MEMBER_UPSERT " +
                                    "'" + LB_REGNO.Text + "'," +
                                    LB_SEQ.Text + "," +
                                    "'" + LB_TYPE.Text + "'," +
                                    "'" + ID + "'," +
                                    "'" + LB_MEMBER_TYPE.Text + "'," +
                                    "'" + GlobalUse.GetUserMgmt(Session["s"].ToString(), "UserID") + "'";
                conn.ExecuteNonQuery();

                Response.Redirect("EndorsementPengkinianDataDetail.aspx?REGNO=" + LB_REGNO.Text + "&SEQ=" + LB_SEQ.Text + "&TYPE=" + LB_TYPE.Text + "&ID=" + ID + "&MEMBER_TYPE=" + LB_MEMBER_TYPE.Text, false);
            }
            catch (System.Exception ex)
            {
                LB_ERROR.Text = ex.Message;
            }

        }
    }
}