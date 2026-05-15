<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationSaving.aspx.cs" Inherits="GLIFE.Form_Saving.ApplicationSaving" %>

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
        <table style="border-spacing: 0px; width: 95%;">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_SAVING" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" CssClass="ASPDatagrid" Width="100%">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="Yellow" ForeColor="#006600" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="CONT_DATE" HeaderText="DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ER_AMOUNT" HeaderText="ER AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="EE_AMOUNT" HeaderText="EE AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LOADING" HeaderText="LOADING">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COI" HeaderText="COI">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
    </form>
</body>
</html>
