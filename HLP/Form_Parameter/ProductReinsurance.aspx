<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductReinsurance.aspx.cs" Inherits="HLP.Form_Parameter.ProductReinsurance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; left: 0px; top: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="width:20px;">
                    &nbsp;</td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr style="vertical-align:top;">
                <td>
                    <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" HeaderText="CODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANYNAME" HeaderText="COMPANY NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCNO" HeaderText="DOC NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STARTDATE" HeaderText="START DATE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TYPE" HeaderText="TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OWNRETENTION" HeaderText="OWN RETENTION"></asp:BoundColumn>
                            <asp:BoundColumn DataField="QUOTASHARE" HeaderText="QUOTA SHARE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LOADING" HeaderText="UJROH RE"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" CommandName="Save" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPButton" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
