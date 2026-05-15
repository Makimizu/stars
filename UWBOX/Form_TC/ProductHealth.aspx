<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductHealth.aspx.cs" Inherits="UWBOX.Form_TC.ProductHealth" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 130px;">TERM & CONDITION</td>
                            <td>
                                <asp:DropDownList ID="DDL_TC" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_TC_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 150px;">
                                <asp:Button ID="BT_PACKAGE" runat="server" Width="100%" Font-Size="XX-Small" Text="PACKAGE" OnClick="BT_PACKAGE_Click" />
                                <asp:Button ID="BT_PLAN" runat="server" Width="100%" Font-Size="XX-Small" Text="PLAN" OnClick="BT_PLAN_Click" />
                                <asp:Button ID="BT_PLAN_DETAIL" runat="server" Width="100%" Font-Size="XX-Small" Text="PACKAGE DETAIL" OnClick="BT_PLAN_DETAIL_Click" />
                                <asp:Button ID="BT_BENEFIT_DETAIL" runat="server" Width="100%" Font-Size="XX-Small" Text="BENEFIT DETAIL" OnClick="BT_BENEFIT_DETAIL_Click" />
                                <asp:Button ID="BT_TPA" runat="server" Width="100%" Font-Size="XX-Small" Text="TPA" OnClick="BT_TPA_Click" />
                                <asp:Button ID="BT_NOTE" runat="server" Width="100%" Font-Size="XX-Small" Text="BENEFIT NOTE" OnClick="BT_NOTE_Click" />
                            </td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td class="TDBGColor">
                                            <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                                <div id="DV_PACKAGE" runat="server" visible="true">
                                    <asp:Button ID="BT_NEW_PACKAGE" runat="server" CssClass="ASPButton" Text="NEW PACKAGE" Width="100" OnClick="BT_NEW_PACKAGE_Click" />
                                    <asp:DataGrid ID="DGR_PACKAGE" runat="server" CssClass="ASPDatagrid" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None" Font-Size="XX-Small" AutoGenerateColumns="False" OnItemCommand="DGR_PACKAGE_ItemCommand">
                                        <AlternatingItemStyle BackColor="#DCDCDC" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" BackColor="#EEEEEE" ForeColor="Black" />
                                        <Columns>
                                            <asp:BoundColumn DataField="PACKAGE_ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PACKAGE_DESCR" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="IP_PLAN" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="OP_PLAN" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MT_PLAN" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DT_PLAN" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="GL_PLAN" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SN_PLAN" Visible="false"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="PACKAGE NAME">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_PACKAGE" runat="server" CssClass="ASPTextBox" Width="200" MaxLength="150"></asp:TextBox>
                                                    <asp:Button ID="BT_DEL_PACKAGE" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <HeaderStyle Wrap="False"
                                            HorizontalAlign="Center" BackColor="#000084" ForeColor="White" />
                                        <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                                    </asp:DataGrid>
                                    <asp:Button ID="BT_SAVE_PACKAGE" runat="server" CssClass="ASPButton" Text="SAVE PACKAGE" Width="100" OnClick="BT_SAVE_PACKAGE_Click" />
                                </div>
                                <div id="DV_PLAN" runat="server" visible="false">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>
                                    <iframe id="IF_PLAN" runat="server" style="width: 98%; height: 80vh;"></iframe>

                                </div>
                                <div id="DV_PLAN_DETAIL" runat="server" visible="false">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="DDL_PACKAGE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_PACKAGE_SelectedIndexChanged"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:DropDownList ID="DDL_BENEFIT_ADD" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_BENEFIT_ADD_SelectedIndexChanged"></asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="DDL_PLAN_VALUE_ADD" runat="server" CssClass="ASPDropDownList" AutoPostBack="true"></asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="BT_ADD_PACKAGE_DETAIL" runat="server" CssClass="ASPButton" Text="ADD" OnClick="BT_ADD_PACKAGE_DETAIL_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DataGrid ID="DGR_PACKAGE_DETAIL" runat="server" CssClass="ASPDatagrid" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None" Font-Size="XX-Small" AutoGenerateColumns="False" OnItemCommand="DGR_PACKAGE_DETAIL_ItemCommand">
                                                    <AlternatingItemStyle BackColor="#DCDCDC" />
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" BackColor="#EEEEEE" ForeColor="Black" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="BENEFIT_ID" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PLAN_VALUE" Visible="False"></asp:BoundColumn>

                                                        <asp:TemplateColumn HeaderText="PACKAGE NAME">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DDL_BENEFIT" runat="server" CssClass="ASPDropDownList" AutoPostBack="true"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="PACKAGE NAME">
                                                            <ItemTemplate>
                                                                <%--<asp:TextBox ID="TXT_PLAN_VALUE" runat="server" CssClass="ASPTextBox" Width="200" MaxLength="150"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="DDL_PLAN_VALUE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="PACKAGE NAME">
                                                            <ItemTemplate>
                                                                <asp:Button ID="BT_SAVE_PACKAGE_DETAIL" runat="server" CssClass="ASPButton" Text="S" BackColor="Green" ForeColor="White" CommandName="Save" />
                                                                <asp:Button ID="BT_DEL_PACKAGE_DETAIL" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle Wrap="False"
                                                        HorizontalAlign="Center" BackColor="#000084" ForeColor="White" />
                                                    <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                                    <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="DV_BENEFIT_DETAIL" runat="server" visible="false">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>
                                    <iframe id="IF_BENEFIT_DETAIL" runat="server" style="width: 98%; height: 80vh;"></iframe>
                                </div>
                                <div id="DV_TPA" runat="server" visible="true">

                                    <asp:DataGrid ID="DGR_TPA" runat="server" CssClass="ASPDatagrid" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None" Font-Size="XX-Small" AutoGenerateColumns="False">
                                        <AlternatingItemStyle BackColor="#DCDCDC" />
                                        <ItemStyle Wrap="False" BackColor="#EEEEEE" ForeColor="Black" />
                                        <Columns>
                                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CHARGE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCR" HeaderText="TPA NAME">
                                                <HeaderStyle Width="200" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="CHARGE">
                                                <HeaderStyle Width="80" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_CHARGE" runat="server" CssClass="ASPTextBoxNumber" Width="98%"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="30" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CB" runat="server" CssClass="ASPButton" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <HeaderStyle Wrap="False" BackColor="#000084" ForeColor="White" />
                                        <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                                    </asp:DataGrid>

                                    <asp:Button ID="BT_SAVE_TPA" runat="server" CssClass="ASPButton" Text="SAVE TPA" Width="100px" OnClick="BT_SAVE_TPA_Click" />

                                </div>
                                <div id="DV_NOTE" runat="server" visible="false">
                                    <iframe id="IF_NOTE" runat="server" style="width: 98%; height: 80vh;"></iframe>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
