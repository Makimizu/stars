<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TC.aspx.cs" Inherits="LIFE.Form_Parameter.TC" %>

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

        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 80px;">CODE</td>
                            <td>
                                <asp:Label ID="LB_CODE" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>NAME</td>
                            <td>
                                <asp:Label ID="LB_NAME" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:DataGrid ID="DGR_SPEC" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False">
                        <ItemStyle VerticalAlign="Top" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="GRP" HeaderText="GROUP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="ITEM">
                                <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" HeaderText="VALUE">
                                <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:DataGrid ID="DGR_BENEFIT" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" OnItemCommand="DGR_BENEFIT_ItemCommand">
                        <ItemStyle VerticalAlign="Top" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="RATE_TABLE_CODE" Visible="False">
                                <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE_TABLE_NAME" Visible="False">
                                <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT" HeaderText="BENEFIT">
                                <ItemStyle Width="300px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_PAYMENT" HeaderText="TYPE">
                                <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PREMIUM RATE">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_RATE" runat="server" CommandName="Rate" Visible="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    MEDICAL TABLE :

                    <asp:DataGrid ID="DGR_UW" runat="server" CssClass="ASPDatagrid" PageSize="15" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                        <AlternatingItemStyle BackColor="White" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle
                            Wrap="False" BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="500px" Width="90%" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 60px; border: outset 2px gray; padding: 5px; display: none; overflow: auto;">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                            <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LB_RATECODE" runat="server" Visible="false"></asp:Label>
                            <table style="border-spacing: 0px;">
                                <tr>
                                    <td style="width: 100px;">GENDER</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_GENDER" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_GENDER_SelectedIndexChanged">
                                            <asp:ListItem Value="M">MALE</asp:ListItem>
                                            <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" Font-Bold="True" ForeColor="White" BackColor="Green" Visible="True" OnClick="BT_XLS_Click" />
                            <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None">
                                <AlternatingItemStyle BackColor="#DCDCDC" />
                                <ItemStyle Wrap="False" HorizontalAlign="Center" BackColor="#EEEEEE" ForeColor="Black" />
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle Wrap="False"
                                    HorizontalAlign="Center" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
