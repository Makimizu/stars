<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementCancelPayment.aspx.cs" Inherits="LIFE.Form_POS.EndorsementCancelPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>

        <asp:Label ID="LB_REMARK" runat="server"></asp:Label>
        <br />
        <br />
        <asp:DataGrid ID="DGR" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:BoundColumn DataField="SETTLEMENT_ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="INVOICENO" HeaderText="INVOICE NO"></asp:BoundColumn>
                <asp:BoundColumn DataField="SETTLE_DATE" HeaderText="SETTLE DATE">
                    <HeaderStyle HorizontalAlign="Center" Width="80" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="INVOICE_TYPE" HeaderText="PREMIUM TYPE"></asp:BoundColumn>
                <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                    <HeaderStyle HorizontalAlign="Right" Width="80" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="RK_TRANSFER_DATE" HeaderText="TRANSFER DATE">
                    <HeaderStyle HorizontalAlign="Center" Width="80" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ACC_NAME" HeaderText="BANK ACCOUNT"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle HorizontalAlign="Right" Width="30" />
                    <ItemTemplate>
                        <asp:CheckBox ID="CB" runat="server" OnCheckedChanged="CB_CheckedChanged" AutoPostBack="true" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
            <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
            <AlternatingItemStyle BackColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        </asp:DataGrid>

    </form>
</body>
</html>
