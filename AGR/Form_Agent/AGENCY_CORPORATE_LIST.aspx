<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENCY_CORPORATE_LIST.aspx.cs" Inherits="AGR.AGENCY_CORPORATE_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="row">
            <table style="border-spacing: 0px; font-size: xx-small; width: 100%;">
                <tr>
                    <td class="TDBGColor">AGENCY LIST</td>
                </tr>
            </table>

            <table style="width: 100%; font-size: xx-small;">
                <tr style="vertical-align: top;">
                    <td style="width: 50%;">
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 150px;">COMPANY CODE
                                </td>
                                <td>
                                    <asp:TextBox ID="TXT_COMPANY_CODE" runat="server" Width="90%" CssClass="ASPTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>COMPANY NAME
                                </td>
                                <td>
                                    <asp:TextBox ID="TXT_COMPANY_NAME" runat="server" Width="90%" CssClass="ASPTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>HAS AGENTS</td>
                                <td>
                                    <asp:DropDownList ID="DDL_AGENT" runat="server" CssClass="ASPDropDownList">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="and AGENTS &gt; 0">YES</asp:ListItem>
                                        <asp:ListItem Value="and AGENTS = 0">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table style="border-spacing: 0px; font-family: Verdana; width: 100%;">
                            <tr>
                                <td style="width: 150px;">ADDRESS</td>
                                <td>
                                    <asp:TextBox ID="TXT_ADDRESS" runat="server" Width="90%" CssClass="ASPTextBox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>PROVINCE
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDL_PROVINCE" runat="server" Width="100%" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>HAS BRANCHES</td>
                                <td>
                                    <asp:DropDownList ID="DDL_BRANCH" runat="server" CssClass="ASPDropDownList">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="and BRANCHS &gt; 0">YES</asp:ListItem>
                                        <asp:ListItem Value="and BRANCHS = 0">NO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_CARI" runat="server" Text="SEARCH" OnClick="BT_CARI_Click" CssClass="ASPButton" />&nbsp;
                                <asp:Label ID="LB_RECORD" runat="server" EnableViewState="False" ForeColor="Blue"></asp:Label>&nbsp;
                                <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" Font-Bold="True" ForeColor="White" BackColor="Green" Visible="True" OnClick="BT_XLS_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>



        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
            OnPageIndexChanged="DGR_PageIndexChanged"
            AllowPaging="True" AutoGenerateColumns="False" GridLines="None" ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" Width="100%">
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#E3EAEB" Wrap="False" BorderColor="Black" BorderWidth="0" />
            <Columns>
                <asp:BoundColumn DataField="COMPANY_CODE" HeaderText="CODE" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="CODE">
                    <ItemTemplate>
                        <asp:Label ID="LB_CODE" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="COMPANY_ADDRESS" HeaderText="ADDRESS"></asp:BoundColumn>
                <asp:BoundColumn DataField="PROPINSI_DESCR" HeaderText="PROVINCE"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENTS" HeaderText="#AGENT">
                    <HeaderStyle Width="40" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="BRANCHS" HeaderText="#BRANCH">
                    <HeaderStyle Width="40" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White"
                Wrap="False" HorizontalAlign="Left" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            <HeaderStyle HorizontalAlign="Left" />
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
        </asp:DataGrid>
    </form>
</body>
</html>
