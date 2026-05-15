<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccountList.aspx.cs" Inherits="FINANCE.Form_Bank.AccoutList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
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
                            <td style="width: 100px;">BANK</td>
                            <td>
                                <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_BANK_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ACCOUNT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>DEDICATED TO</td>
                            <td>
                                <asp:DropDownList ID="DDL_ARAP" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_BANK_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="SEARCH" />
                                            &nbsp;
                                            <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW" BackColor="Blue" ForeColor="White" Font-Bold="true" OnClick="BT_NEW_Click" />
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" PageSize="20" OnItemCommand="DGR_ItemCommand"
                        GridLines="Vertical" CssClass="ASPDatagrid"
                        AutoGenerateColumns="False" Width="100%">
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="ACC NO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_SELECT" runat="server" CommandName="Select" CssClass="ASPLabel"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="A/R">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB_COL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_COL_CheckedChanged" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="A/P">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB_STL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_STL_CheckedChanged" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DEDICATED TO">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_APP" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_APP_SelectedIndexChanged"></asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ACCNO" HeaderText="ACC NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="STL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="APPID_PRODUCT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BOOKNAME" HeaderText="BOOK NAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BANK" HeaderText="BANK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Blue" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                        <FooterStyle BackColor="Gray" ForeColor="Black" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="300px" Width="60%" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td class="TDBGColor">
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
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr>
                                    <td style="width: 150px;">ACC NO</td>
                                    <td>
                                        <asp:TextBox ID="TXT_NOREK" runat="server" CssClass="ASPTextBox" MaxLength="100"
                                            Width="256px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>BOOK NAME</td>
                                    <td>
                                        <asp:TextBox ID="TXT_BANK" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="433px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ACC NAME
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXT_NAMA" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="433px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>BANK NAME</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_BANKSELECTED" runat="server" CssClass="ASPDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>BRANCH</td>
                                    <td>
                                        <asp:TextBox ID="TXT_NAMA_CABANG" runat="server" CssClass="ASPTextBox" MaxLength="255"
                                            Width="433px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ADDRESS 1</td>
                                    <td>
                                        <asp:TextBox ID="TXT_ALAMAT_1" runat="server" CssClass="ASPTextBox" MaxLength="255"
                                            Width="433px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ADDRESS 2</td>
                                    <td>
                                        <asp:TextBox ID="TXT_ALAMAT_2" runat="server" CssClass="ASPTextBox" MaxLength="255"
                                            Width="433px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ACCOUNT RECEIVABLE</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_COL" runat="server" CssClass="ASPDropDownList">
                                            <asp:ListItem Value="0">NO</asp:ListItem>
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>ACCOUNT PAYABLE</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_STL" runat="server" CssClass="ASPDropDownList">
                                            <asp:ListItem Value="0">NO</asp:ListItem>
                                            <asp:ListItem Value="1">YES</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>COA</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_GLCOA" runat="server" CssClass="ASPDropDownList">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="BT_SIMPAN" runat="server" CssClass="ASPButton" OnClick="BT_SIMPAN_Click"
                                            Text="SAVE" Width="76px" />
                                        <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" ForeColor="Red" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
