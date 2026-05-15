<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_REGISTRATION_LIST.aspx.cs" Inherits="AGR.Form_Agent.AGENT_REGISTRATION_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">AGENT CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>UPLINER NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_UPLINER" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">DIVISION IN CHARGE</td>
                            <td>
                                <asp:DropDownList ID="DDL_CHANNEL" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>LEVEL</td>
                            <td>
                                <asp:TextBox ID="TXT_CHANNEL" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                            &nbsp;
                                            <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW RECORD" BackColor="Green" ForeColor="White" OnClick="BT_NEW_Click" />
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_RECORDS" runat="server"></asp:Label></td>
                                    </tr>
                                </table>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


        <asp:DataGrid ID="DGR" runat="server" CellPadding="4"
            GridLines="None" CssClass="ASPDatagrid"
            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
            <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="X-Small" />
            <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
            <Columns>
                <asp:TemplateColumn HeaderText="AGENT CODE">
                    <ItemTemplate>
                        <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="SUBCD_DESCR" HeaderText="LEVEL"></asp:BoundColumn>
                <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                    <HeaderStyle HorizontalAlign="Center" Width="250" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="POB" HeaderText="POB"></asp:BoundColumn>
                <asp:BoundColumn DataField="BRANCH_DESCR" HeaderText="BRANCH"></asp:BoundColumn>
                <asp:BoundColumn DataField="UPLINER_NAME" HeaderText="UPLINER NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="LISTING_TYPE" HeaderText="TINGKAT RESIKO"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemStyle Width="30" HorizontalAlign="Right" />
                    <ItemTemplate>
                        <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" Font-Bold="true" CommandName="Delete" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
        </asp:DataGrid>
        </div>
    </form>
</body>
</html>

