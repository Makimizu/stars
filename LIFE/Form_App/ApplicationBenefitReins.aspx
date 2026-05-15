<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationBenefitReins.aspx.cs" Inherits="LIFE.Form_App.ApplicationBenefitReins" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr id="TR_REINSADD" runat="server">
                <td>
                    <asp:Button ID="BT_REINSADD" runat="server" CssClass="ASPButton" Text="ADD :" Width="80px" OnClick="BT_REINSADD_Click" />
                    <asp:DropDownList ID="DDL_TYPE" runat="server" AutoPostBack="true" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_TYPE_SelectedIndexChanged"></asp:DropDownList>
                    <asp:DropDownList ID="DDL_REINS" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="SEQ" Visible="false">
                                <HeaderStyle Width="30px" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SHARE_TYPE" HeaderText="SHARE&lt;BR&gt;TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY" HeaderText="COMPANY NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DESCRIPTION"></asp:BoundColumn>
                            <%--<asp:BoundColumn DataField="LOADING" HeaderText="LOADING" Visible="false"></asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="TYPE" HeaderText="TYPE"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Button ID="BT_SHARESAVE" runat="server" CssClass="ASPButton" Text="SAVE" CommandName="Save" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="BT_SHAREDEL" runat="server" CssClass="ASPButton" Text="X" CommandName="Delete" BackColor="Red" ForeColor="White" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SHARED AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_SHAREAMOUNT" runat="server" Width="120px" CssClass="ASPTextBoxNumber"></asp:TextBox>
                                    <asp:Label ID="LB_SHAREAMOUNT" runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="&permil; RATE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_SHARERATE" runat="server" Width="80px" CssClass="ASPTextBoxNumber"></asp:TextBox>
                                    <asp:Label ID="LB_SHARERATE" runat="server" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="30px" HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_EMAIL" runat="server" CssClass="ASPButton" Text="EMAIL" CommandName="Email" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="TC_CODE" Visible="false"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>

        </table>
    </form>
</body>
</html>

