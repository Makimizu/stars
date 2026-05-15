<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_EDUCATION_WORK.aspx.cs" Inherits="AGR.AGENT_EDUCATION_WORK" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>

        <table style="border-spacing: 0px; font-size: x-small; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <asp:UpdatePanel ID="UpdatePanel0" runat="server">
                        <ContentTemplate>
                            <table style="border-spacing: 0px; font-size: x-small; Width: 100%;">
                                <tr>
                                    <td class="TDBGColor">EDUCATION</td>
                                </tr>
                                <tr id="TR_EDUCATION" runat="server">
                                    <td>
                                        <table style="border-spacing: 0px; font-size: x-small; font-family: Tahoma; width: 100%;">

                                            <tr>
                                                <td style="width: 100px;">PERIOD</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_START_DATE" runat="server" Width="70px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;-&nbsp;
                                                    <asp:TextBox ID="TXT_END_DATE" runat="server" Width="70px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="ceDate2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>INSTITUTION</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_INSTITUTION_NAME" runat="server" Width="90%" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>GRADE</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_GRADE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="BT_SAVE" runat="server" Text="ADD" CssClass="ASPButton" Width="80" OnClick="BT_SAVE_Click" />
                                        &nbsp;<asp:Button ID="BT_EDUCATION_CANCEL" runat="server" Text="CANCEL" CssClass="ASPButton" BackColor="Red" ForeColor="White" Width="80px" Visible="false" OnClick="BT_EDUCATION_CANCEL_Click" />
                                        <asp:DataGrid ID="DGR_EDUCATION" runat="server" CellPadding="3" PageSize="20"
                                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ShowHeader="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" OnItemCommand="DGR_EDUCATION_ItemCommand">
                                            <ItemStyle Wrap="False" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#4A3C8C" />
                                            <SelectedItemStyle BackColor="#738A9C" ForeColor="#F7F7F7" Font-Bold="True" />
                                            <AlternatingItemStyle BackColor="#CCFFFF" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Font-Size="X-Small" />
                                            <Columns>
                                                <asp:BoundColumn DataField="START_DATE" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DISPLAY_FORMAT"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemStyle HorizontalAlign="Right" Width="30" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="BT_DEL" runat="server" Text="X" CommandName="Delete" ForeColor="White" BackColor="Red" CssClass="ASPButton" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Font-Size="X-Small" Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 50%;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="border-spacing: 0px; font-size: x-small; Width: 100%;">
                                <tr>
                                    <td class="TDBGColor">WORK EXPERIENCE</td>
                                </tr>
                                <tr id="TR_WORKEXP" runat="server">
                                    <td>
                                        <table style="border-spacing: 0px; font-size: x-small; font-family: Tahoma; width: 100%;">

                                            <tr>
                                                <td style="width: 80px;">PERIOD</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_START_DATE_WORKEXP" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE_WORKEXP">
                                                    </ajaxToolkit:CalendarExtender>
                                                    &nbsp;-&nbsp;
                                                    <asp:TextBox ID="TXT_END_DATE_WORKEXP" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE_WORKEXP">
                                                    </ajaxToolkit:CalendarExtender>
                                                </td>
                                                <td style="width: 80px;">STATUS</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_WORKEXP_STATUS" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>COMPANY</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_WORKEXP_COMPANY" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td>FIELD</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_WORKEXP_FIELD" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>LAST POSITION</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_WORKEXP_POSITION" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td>LAST SALARY</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_WORKEXP_SALARY" runat="server" Width="100" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>REMARK</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_WORKEXP_REMARK" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                </td>
                                                <td><%--TITLE--%></td>
                                                <td>
                                                    <asp:TextBox ID="TXT_WORKEXP_TITLE" runat="server" Width="95%" Font-Size="X-Small" Visible="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="BT_SAVE_WORKEXP" runat="server" Text="ADD" CssClass="ASPButton" Width="80" OnClick="BT_SAVE_WORKEXP_Click" />
                                        &nbsp;<asp:Button ID="BT_WORKEXP_CANCEL" runat="server" Text="CANCEL" CssClass="ASPButton" BackColor="Red" ForeColor="White" Width="80px" Visible="false" OnClick="BT_WORKEXP_CANCEL_Click" />
                                        <asp:DataGrid ID="DGR_WORKEXP" runat="server" CellPadding="2" PageSize="20"
                                            GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" OnItemCommand="DGR_WORKEXP_ItemCommand">
                                            <ItemStyle Wrap="False" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                            <AlternatingItemStyle BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                            <Columns>
                                                <asp:BoundColumn DataField="START_DATE" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DISPLAY_FORMAT"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemStyle HorizontalAlign="Right" Width="30" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Button ID="BT_DEL" runat="server" Text="X" CommandName="Delete" ForeColor="White" BackColor="Red" CssClass="ASPButton" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <EditItemStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>


    </form>
</body>
</html>
