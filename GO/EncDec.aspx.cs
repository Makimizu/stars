using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GO
{
    public partial class EncDec : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BT_ENC_Click(object sender, EventArgs e)
        {
            LB.Text = "";
            try
            {
                LB.Text = Crypto.EncryptStringAES(TXT.Text.Trim());
            }
            catch { }
        }

        protected void BT_DEC_Click(object sender, EventArgs e)
        {
            LB.Text = "";
            try
            {
                LB.Text = Crypto.DecryptStringAES(TXT.Text.Trim());
            }
            catch { }
        }
    }
}