<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent_Structure.aspx.cs" Inherits="SALESMARKET.Form_Agents.Agent_Structure" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%; height: 100%;">
            <tr style="vertical-align:top;">
                <td>
                    <asp:TreeView ID="TV_AGENT" Width="100%" Height="100%" runat="server" ExpandDepth="0" Font-Size="XX-Small" OnSelectedNodeChanged="TV_AGENT_SelectedNodeChanged"
                        OnTreeNodePopulate="TV_AGENT_TreeNodePopulate" BackColor="#FFFF99" ImageSet="Contacts" NodeIndent="10" >
                        <HoverNodeStyle Font-Underline="False" />
                        <NodeStyle  Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                        <ParentNodeStyle Font-Bold="True" ForeColor="#5555DD" />
                        <SelectedNodeStyle BackColor="#99FF33" Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                    </asp:TreeView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
