using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMS.DBConnection;

namespace AGR
{
    public partial class AGENT_STRUCTURE : System.Web.UI.Page
    {
        #region PrivateVariables
        protected Connection conn = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Setup();
                FillTree();
            }
        }

        protected void Setup()
        {
            conn.QueryString = "select CODE, DESCR from PR_MARKET_SEGMENT order by 2 desc";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_CHANNEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLLevel();
        }

        protected void FillDDLLevel()
        {
            DDL_LEVEL.Items.Clear();

            DDL_LEVEL.Items.Add(new ListItem("", ""));
            conn.QueryString = "select SUB_CODE, DESCR from PARAM_SUB_CHANNEL_DISTRIBUTION where MARKET_SEGMENT = '" + DDL_CHANNEL.SelectedValue + "'";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_LEVEL.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            FillDDLUpliner();
        }

        protected void FillDDLUpliner()
        {
            DDL_UPLINER.Items.Clear();

            DDL_UPLINER.Items.Add(new ListItem("", ""));
            conn.QueryString = "select CODE, FULLNAME from V_M_AGENTS where isnull(UPLINER, '') = '' and MARKET_SEGMENT = '" + DDL_CHANNEL.SelectedValue + "' and SUBCD like '%" + DDL_LEVEL.SelectedValue + "%' order by 2   ";
            conn.ExecuteQuery();
            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                DDL_UPLINER.Items.Add(new ListItem(conn.GetFieldValue(i, 1).ToString(), conn.GetFieldValue(i, 0).ToString()));
            }

            //FillTree();
        }

        protected void FillTree()
        {
            TV_AGENT.Nodes.Clear();

            //string upliner = "";
            if (DDL_UPLINER.SelectedValue != "")
            {
                conn.QueryString = "select " +
                                    "a.CODE, " +
                                    "NAMA = a.FULLNAME, " +
                                    "ACTIVE = convert(int,ACTIVE) " +
                                    "from V_M_AGENTS a " +
                                    "where " +
                                    "a.CODE = '" + DDL_UPLINER.SelectedValue + "' ";
            }
            else
            {
                conn.QueryString = "select " +
                                    "a.CODE, " +
                                    "NAMA = a.FULLNAME, " +
                                    "ACTIVE = convert(int,ACTIVE) " +
                                    "from V_M_AGENTS a " +
                                    "where " +
                                    "isnull(a.UPLINER,'') = '' " +
                                    "and a.SUBCD like '%" + DDL_LEVEL.SelectedValue + "%' " +
                                    "order by 2";
            }


            conn.ExecuteQuery();

            Connection conn0 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

            for (int i = 0; i < conn.GetRowCount(); i++)
            {
                TreeNode ObjTreeParent = new TreeNode();
                ObjTreeParent.Text = "<span color: ''#0000FF''>" + conn.GetFieldValue(i, 1).ToString() + "</span>";
                ObjTreeParent.Value = conn.GetFieldValue(i, 0).ToString();
                ObjTreeParent.SelectAction = TreeNodeSelectAction.Expand;
                TV_AGENT.Nodes.Add(ObjTreeParent);

                if (conn.GetFieldValue(i, "ACTIVE").ToString() == "0")
                {
                    ObjTreeParent.Text = "<span color: ''#FF0000''>" + conn.GetFieldValue(i, 1).ToString() + "</span>";
                }

                conn0.QueryString = "select " +
                                   "CODE " +
                                   "from V_M_AGENTS " +
                                   "where " +
                                   "UPLINER = '" + ObjTreeParent.Value + "'";
                conn0.ExecuteQuery();
                if (conn0.GetRowCount() > 0)
                    FillTree2(ObjTreeParent);

                ObjTreeParent.ExpandAll();
            }
        }

        protected void FillTree2(TreeNode node)
        {
            Connection conn1 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));
            Connection conn2 = new Connection(GlobalUse.GetConnString(System.Configuration.ConfigurationManager.AppSettings["appid"]));

            conn1.QueryString = "select " +
                                "CODE, " +
                                "NAMA = FULLNAME, " +
                                "ACTIVE = convert(int,ACTIVE) " +
                                "from V_M_AGENTS " +
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
                    ObjTreeNode.Text = "<span color: ''#FF0000''>" + conn1.GetFieldValue(i, 1).ToString() + "</span>";
                }

                conn2.QueryString = "select " +
                                    "CODE " +
                                    "from V_M_AGENTS " +
                                    "where " +
                                    "UPLINER = '" + ObjTreeNode.Value + "'";
                conn2.ExecuteQuery();

                if (conn2.GetRowCount() > 0)
                {
                    //ObjTreeNode.PopulateOnDemand = true;
                    FillTree2(ObjTreeNode);
                    //ObjTreeNode.Text = ObjTreeNode.Text;                    
                }
            }
        }

        protected void TV_AGENT_SelectedNodeChanged(object sender, EventArgs e)
        {
            string val = TV_AGENT.SelectedValue;
            Response.Redirect("AGENT_FRAME.ASPX?AGENTCODE=" + TV_AGENT.SelectedValue);
        }

        protected void TV_AGENT_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            FillTree2(e.Node);
        }

        protected void DDL_CHANNEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLLevel();
            FillTree();
        }

        protected void DDL_LEVEL_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDDLUpliner();
            FillTree();
        }

        protected void DDL_UPLINER_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillTree();
        }

    }
}