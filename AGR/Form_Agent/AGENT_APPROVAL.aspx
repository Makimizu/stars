<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_APPROVAL.aspx.cs" Inherits="AGR.AGENT_APPROVAL" %>

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

        <div id="DV_APPROVAL" runat="server">
            <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                <tr style="vertical-align: top;">
                    <td>
                        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
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
                    <td>
                        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
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
                                <td style="text-align: left;">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
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

            <asp:DataGrid ID="DGR" runat="server" CellPadding="2" PageSize="20"
                GridLines="None" CssClass="ASPDatagrid"
                OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                <ItemStyle Wrap="False" BackColor="#E3EAEB" Font-Size="XX-Small" />
                <SelectedItemStyle BackColor="#C5BBAF" ForeColor="#333333" Font-Bold="True" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="XX-Small" VerticalAlign="Top" />
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
                    <asp:BoundColumn DataField="BRANCH_DESCR" HeaderText="BRANCH"></asp:BoundColumn>
                    <asp:BoundColumn DataField="LISTING_TYPE" HeaderText="TINGKAT RESIKO"></asp:BoundColumn>
                    <asp:TemplateColumn>
                        <HeaderStyle HorizontalAlign="Left" Width="330" />
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderTemplate>
                            <table style="border-spacing: 0px; width: 100%">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="true" OnCheckedChanged="CB_ALL_CheckedChanged" /></td>
                                    <td>
                                        <asp:Button ID="BT_A" runat="server" CssClass="ASPButton" Text="APPROVE" BackColor="Blue" ForeColor="White" CommandName="Approve" Width="100" />
                                        <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" Text="REJECT" BackColor="Red" ForeColor="White" CommandName="Reject" Width="100" /></td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="border-spacing: 0px; width: 100%">
                                <tr>
                                    <td>
                                        <asp:CheckBox ID="CB" runat="server" /></td>
                                    <td>
                                        <asp:TextBox ID="TXT_REJECT" runat="server" CssClass="ASPTextBox" Width="300" placeholder="Reject Reason .."></asp:TextBox></td>
                                    <td>
                                        <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" Font-Bold="true" CommandName="Delete" /></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <EditItemStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Font-Size="X-Small" Mode="NumericPages" />
            </asp:DataGrid>
        </div>

        <div id="DV_VALIDATION" runat="server" visible="false">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td>
                        <asp:LinkButton ID="LB_BACK" runat="server" ForeColor="Red" Font-Size="X-Small" Font-Bold="true" OnClick="LB_BACK_Click">Back ..</asp:LinkButton></td>
                </tr>
                <tr>
                    <td style="border-top: ridge;">
                        <br />
                        <asp:Label ID="LB_VALIDATION" runat="server" ForeColor="Red" Font-Size="X-Small"></asp:Label>
                    </td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>

