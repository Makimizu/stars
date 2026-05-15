<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationAgent.aspx.cs" Inherits="LQ.Form_Client.QuotationAgent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .fa {
            display: inline-block;
            font: normal normal normal 14px/1 FontAwesome;
            font-size: inherit;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CellPadding="2"
            ForeColor="#333333" GridLines="None" PageSize="20" CssClass="ASPDatagrid" Font-Size="8pt" CellSpacing="1">
            <EditItemStyle BackColor="#7C6F57" />
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
            <Columns>
                <asp:BoundColumn DataField="COMMISSION_TYPE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMMISSION_DESCR" HeaderText="COMMISSION TYPE">
                    <ItemStyle Width="150" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="AGENT CODE">
                    <ItemStyle Width="80" />
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_AGENTCODE" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME">
                    <ItemStyle Width="300" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#1C5E55" ForeColor="White" />
        </asp:DataGrid>
        <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SAVE" Width="100px" />
    </form>
</body>
</html>
