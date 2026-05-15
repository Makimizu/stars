using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace HLP.Form_Agents
{
    public partial class Agent_Structure : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillTree();
            }
        }

        protected void FillTree()
        {
            TV_AGENT.Nodes.Clear();

            conn.QueryString = "select CODE, DESCR=UPPER(DESCR) from PR_CHANNEL_DISTRIBUTION order by 2";            
            conn.ExecuteQuery();

            Connection conn0 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));            

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                TreeNode ObjTreeParent = new TreeNode();
                ObjTreeParent.Text = "<B><U><span color: ''#0000FF''>" + conn.GetFieldValue(i, 1).ToString() + "</span></U></B>";
                ObjTreeParent.Value = conn.GetFieldValue(i, 0).ToString();
                ObjTreeParent.SelectAction = TreeNodeSelectAction.Expand;
                TV_AGENT.Nodes.Add(ObjTreeParent);

                conn0.QueryString = "select " +
                                    "a.CODE, " +
                                    "NAMA = UPPER(RTRIM(LTRIM(isnull(FRONT_NAME,'') + ' ' +isnull(LAST_NAME,'')))), " +
                                    "ACTIVE = convert(int,a.ACTIVE) " +
                                    "from M_AGENTS a " +
                                    "inner join PARAM_SUB_CHANNEL_DISTRIBUTION b on a.SUB_CODE=b.SUB_CODE " +
                                    "where " +
                                    "isnull(a.UPLINER,'') = '' " +
                                    "and b.CD_CODE = '" + conn.GetFieldValue(i, 0).ToString() + "' " +
                                    "order by 2";
                conn0.ExecuteQuery();
                for (int j = 0; j < conn0.GetRowCount(); j++)
                {
                    TreeNode ObjTreeNode = new TreeNode();
                    ObjTreeNode.Text = conn0.GetFieldValue(j, 1).ToString();
                    ObjTreeNode.Value = conn0.GetFieldValue(j, 0).ToString();
                    ObjTreeParent.ChildNodes.Add(ObjTreeNode);

                    if (conn0.GetFieldValue(j, "ACTIVE").ToString() == "0")
                    {
                        ObjTreeNode.Text = "<B><U><span color: ''#FF0000''>" + conn0.GetFieldValue(j, 1).ToString() + "</span></U></B>";
                    }

                    FillTree2(ObjTreeNode);
                }
                
                ObjTreeParent.ExpandAll();
            }
        }

        protected void FillTree2(TreeNode node)
        {
            Connection conn1 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

            conn1.QueryString = "select " +
                                "CODE, " +
                                "NAMA = UPPER(RTRIM(LTRIM(isnull(FRONT_NAME,'') + ' ' +isnull(LAST_NAME,'')))), " +
                                "ACTIVE = convert(int,ACTIVE) " +
                                "from M_AGENTS " +
                                "where " +
                                "UPLINER = '" + node.Value + "' " +
                                "order by 2";
            conn1.ExecuteQuery();

            for (int i = 0; i < conn1.GetRowCount(); i++)
            {
                TreeNode ObjTreeNode = new TreeNode();
                ObjTreeNode.Text = conn1.GetFieldValue(i, 1).ToString();
                ObjTreeNode.Value = conn1.GetFieldValue(i, 0).ToString();
                node.ChildNodes.Add(ObjTreeNode);

                if (conn1.GetFieldValue(i, "ACTIVE").ToString() == "0")
                {
                    ObjTreeNode.Text = "<B><U><span color: ''#FF0000''>" + conn1.GetFieldValue(i, 1).ToString() + "</span></U></B>";
                }

                conn2.QueryString = "select " +
                                    "CODE, " +
                                    "NAMA = UPPER(RTRIM(LTRIM(isnull(FRONT_NAME,'') + ' ' +isnull(LAST_NAME,'')))), " +
                                    "ACTIVE = convert(int,ACTIVE) " +
                                    "from M_AGENTS " +
                                    "where " +
                                    "UPLINER = '" + ObjTreeNode.Value + "' " +
                                    "order by 2";
                conn2.ExecuteQuery();

                if (conn2.GetRowCount() > 0)
                {
                    ObjTreeNode.PopulateOnDemand = true;
                    //ObjTreeNode.Text = ObjTreeNode.Text;
                    //FillTree2(ObjTreeNode);
                }
            }
        }

        protected void TV_AGENT_SelectedNodeChanged(object sender, EventArgs e)
        {
            string val = TV_AGENT.SelectedValue;
            Response.Write("<script language='javascript'>parent.pagebody.location.href = 'Agent_Entry.aspx?code=" + val + "';</script>");
        }

        protected void TV_AGENT_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            FillTree2(e.Node);
        }
    }
}