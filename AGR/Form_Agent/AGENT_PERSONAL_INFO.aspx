<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_PERSONAL_INFO.aspx.cs" Inherits="AGR.AGENT_PERSONAL_INFO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>

<body>
    <form id="form1" runat="server">

        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>


        <table style="border-spacing: 0px; font-size: xx-small; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <asp:UpdatePanel ID="UpdatePanel0" runat="server">
                        <ContentTemplate>
                            <table style="border-spacing: 0px; font-size: xx-small; width: 100%;">
                                <tr style="vertical-align: top;">
                                    <td>
                                        <asp:DataGrid ID="DGR_ITEM" runat="server" AutoGenerateColumns="False" CellPadding="2" Font-Names="Verdana" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" ShowFooter="True" Width="100%" OnItemCommand="DGR_ITEM_ItemCommand" OnSelectedIndexChanged="DGR_ITEM_SelectedIndexChanged">
                                            <ItemStyle VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" Height="20" />
                                            <Columns>
                                                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="LEN" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DESCR">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn>

                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DDL_REFF" runat="server" Visible="False" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="TXT_VAL" runat="server" Visible="False" Width="98%" Font-Size="XX-Small"></asp:TextBox>
                                                        <asp:Button ID="BT_COPY" runat="server" Visible="False" CssClass="ASPButton" CommandName="Copy" Text="" Width="80" />
                                                        <asp:Label ID="LB_VAL" runat="server" Visible="false" CssClass="ASPLabel"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterTemplate>
                                                        <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" CommandName="Save" Text="SAVE" Width="80" />
                                                    </FooterTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                    <td style="width: 100px;">
                                        <table style="border-spacing: 0px; font-size: xx-small; width: 100%;">
                                            <tr style="height: 20px;">
                                                <td></td>
                                            </tr>
                                            <tr style="height: 20px;">
                                                <td></td>
                                            </tr>
                                            <tr style="height: 20px;">
                                                <td></td>
                                            </tr>
                                            <tr style="height: 20px;">
                                                <td></td>
                                            </tr>
                                            <tr style="height: 20px;">
                                                <td></td>
                                            </tr>
                                            <tr style="height: 20px;">
                                                <td>
                                                    <asp:Button ID="BT_COPY_ADDRESS" runat="server" Text="COPY FROM ID" Width="97%" Font-Size="XX-Small" OnClick="BT_COPY_ADDRESS_Click" /></td>
                                            </tr>
                                            <tr style="height: 20px;">
                                                <td></td>
                                            </tr>
                                            <tr style="height: 20px;">
                                                <td></td>
                                            </tr>
                                            <tr style="height: 20px;">
                                                <td></td>
                                            </tr>
                                            <tr style="height: 20px;">
                                                <td>
                                                    <asp:Button ID="BT_COPY_IDNO0" runat="server" Font-Size="XX-Small" OnClick="BT_COPY_IDNO_Click" Text="COPY FROM ID" Width="97%" />
                                                </td>
                                            </tr>
                                            <tr style="height: 20px;">
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 50%;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table style="border-spacing: 0px; font-size: xx-small; width: 100%;">
                                <tr style="vertical-align: top;">
                                    <td style="width: 100px;">
                                        <asp:Button ID="BT_BANK" runat="server" Text="BANK ACCOUNT" Font-Size="XX-Small" Width="100%" OnClick="BT_BANK_Click" Height="25" />
                                        <asp:Button ID="BT_LICENCE" runat="server" Text="LICENCE" Font-Size="XX-Small" Width="100%" OnClick="BT_LICENCE_Click" Height="25" />
                                        <asp:Button ID="BT_FAMILY" runat="server" Text="FAMILY" Font-Size="XX-Small" Width="100%" OnClick="BT_FAMILY_Click" Height="25" />
                                        <asp:Button ID="BT_NONFAMILY" runat="server" Text="NON FAMILY" Font-Size="XX-Small" Width="100%" OnClick="BT_NONFAMILY_Click" Height="25" />
                                        <asp:Button ID="BT_SOCMED" runat="server" Text="SOCIAL MEDIA" Font-Size="XX-Small" Width="100%" OnClick="BT_SOCMED_Click" Height="25" />
                                        <asp:Button ID="BT_MARITAL" runat="server" Text="MARITAL STATUS" Font-Size="XX-Small" Width="100%" OnClick="BT_MARITAL_Click" Height="25" />
                                    </td>
                                    <td>
                                        <table style="border-spacing: 0px; font-size: xx-small; font-family: Verdana; width: 100%;">
                                            <tr>
                                                <td class="TDBGColor">
                                                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
                                            </tr>
                                        </table>
                                        <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                                        <div id="DV_BANK" runat="server">
                                            <table id="TBL_BANK" runat="server" style="border-spacing: 0px; font-size: xx-small; font-family: Verdana; width: 100%;">
                                                <tr>
                                                    <td style="width: 150px;">ACC. NUMBER</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_ACCOUNT" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style3">ACC. BANK</td>
                                                    <td class="auto-style3">
                                                        <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ACC. NAME</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_NAME" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="vertical-align: middle;">
                                                    <td>VIRTUAL ACCOUNT</td>
                                                    <td>
                                                        <asp:CheckBox ID="CB_VACC" runat="server" Text="&amp;nbsp;YES" CssClass="ASPTextBox" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Button ID="BT_SAVE" runat="server" Text="ADD" CssClass="ASPButton" OnClick="BT_SAVE_Click" Width="80px" />
                                            &nbsp;<asp:Button ID="BT_BANK_CANCEL" runat="server" Text="CANCEL" CssClass="ASPButton" BackColor="Red" ForeColor="White" Width="80px" Visible="false" OnClick="BT_BANK_CANCEL_Click" />
                                            <br />
                                            <asp:DataGrid ID="DGR" runat="server" CellPadding="2" PageSize="20"
                                                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand" ShowHeader="False">
                                                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="ACCBANK_CODE" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ACCNO" HeaderText="ACC NO." Visible="false"></asp:BoundColumn>
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
                                        </div>

                                        <div id="DV_LICENCE" runat="server" visible="false">
                                            <table id="TBL_LICENCE" runat="server" style="border-spacing: 0px; font-size: xx-small; font-family: Verdana; width: 100%;">
                                                <tr>
                                                    <td class="auto-style1">LICENCE NUMBER</td>
                                                    <td class="auto-style2">
                                                        <asp:TextBox ID="TXT_LICENCE_NUMBER" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;">PERIOD</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_START_DATE_LICENCE" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE_LICENCE">
                                                        </ajaxToolkit:CalendarExtender>
                                                        &nbsp;-&nbsp;
                                            <asp:TextBox ID="TXT_END_DATE_LICENCE" runat="server" Width="60px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE_LICENCE">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ISSUED BY</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_LICENSE" runat="server" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>LICENCE STATUS</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_LICENSE_STATUS" runat="server" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Button ID="BT_SAVE_LICENCE" runat="server" Text="ADD" CssClass="ASPButton" Width="80" OnClick="BT_SAVE_LICENCE_click" />
                                            &nbsp;<asp:Button ID="BT_LICENCE_CANCEL" runat="server" Text="CANCEL" CssClass="ASPButton" BackColor="Red" ForeColor="White" Width="80px" Visible="false" OnClick="BT_LICENCE_CANCEL_Click" />
                                            <br />
                                            <asp:DataGrid ID="DGR_LICENCE" runat="server" CellPadding="4" PageSize="20"
                                                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" OnItemCommand="DGR_LICENCE_ItemCommand">
                                                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="LICENCE_NO" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="STARTDATE" Visible="false"></asp:BoundColumn>
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
                                        </div>

                                        <div id="DV_FAMILY" runat="server" visible="false">
                                            <table id="TBL_FAMILY" runat="server" style="border-spacing: 0px; font-size: xx-small; font-family: Verdana; width: 100%;">
                                                <tr>
                                                    <td style="width: 150px;">FAMILY RELATIONSHIP</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_FAMILY_STATUS" runat="server" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>NAME</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_FAMILY_NAME" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>POB</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_FAMILY_POB" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100px;">DOB</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_FAMILY_DOB" runat="server" Width="70px" Style="text-align: center;" Font-Size="X-Small"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="ceDate2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_FAMILY_DOB">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>GENDER</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_FAMILY_GENDER" runat="server" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>ADDRESS</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_FAMILY_ADDRESS" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>EDUCATION</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_FAMILY_EDUCATION" runat="server" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>PROFESSION</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_FAMILY_PROFESSION" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>PHONE</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_FAMILY_PHONE" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Button ID="BT_SAVEFAMILY" runat="server" Text="ADD" CssClass="ASPButton" Width="80" OnClick="BT_SAVEFAMILY_Click" />
                                            &nbsp;<asp:Button ID="BT_FAMILY_CANCEL" runat="server" Text="CANCEL" CssClass="ASPButton" BackColor="Red" ForeColor="White" Width="80px" Visible="false" OnClick="BT_FAMILY_CANCEL_Click" />
                                            <br />
                                            <asp:DataGrid ID="DGR_FAMILY" runat="server" CellPadding="4" PageSize="20"
                                                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" OnItemCommand="DGR_FAMILY_ItemCommand">
                                                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="SEQ" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DISPLAY_FORMAT"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemStyle HorizontalAlign="Right" Width="30" VerticalAlign="Top" />
                                                        <ItemTemplate>
                                                            <asp:Button ID="BT_FAMILY_DEL" runat="server" Text="X" CommandName="Delete" ForeColor="White" BackColor="Red" CssClass="ASPButton" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <EditItemStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                            </asp:DataGrid>
                                        </div>

                                        <div id="DV_NONFAMILY" runat="server" visible="false">
                                            <table id="TBL_NONFAMILY" runat="server" style="border-spacing: 0px; font-size: xx-small; font-family: Verdana; width: 100%;">
                                                <tr>
                                                    <td style="width: 150px;">NAME</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_NONFAMILY_NAME" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ADDRESS</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_NONFAMILY_ADDRESS" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>PHONE</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_NONFAMILY_PHONE" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>RELATION</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_NONFAMILY_RELATION" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>RELATIONSHIP PERIOD</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_NONFAMILY_PERIOD_RELATIONSHIP" runat="server" Width="95%" Font-Size="X-Small"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Button ID="BT_NONFAMILY_SAVE" runat="server" Text="ADD" CssClass="ASPButton" Width="80" OnClick="BT_NONFAMILY_SAVE_Click" />
                                            &nbsp;<asp:Button ID="BT_NONFAMILY_CANCEL" runat="server" Text="CANCEL" CssClass="ASPButton" BackColor="Red" ForeColor="White" Width="80px" Visible="false" OnClick="BT_NONFAMILY_CANCEL_Click" />
                                            <br />
                                            <asp:DataGrid ID="DGR_NONFAMILY" runat="server" CellPadding="4" PageSize="20"
                                                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False" OnItemCommand="DGR_NONFAMILY_ItemCommand">
                                                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="SEQ" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DISPLAY_FORMAT"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemStyle HorizontalAlign="Right" Width="30" VerticalAlign="Top" />
                                                        <ItemTemplate>
                                                            <asp:Button ID="BT_NONFAMILY_DEL" runat="server" Text="X" CommandName="Delete" ForeColor="White" BackColor="Red" CssClass="ASPButton" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <EditItemStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                            </asp:DataGrid>
                                        </div>

                                        <div id="DV_SOCIALMEDIA" runat="server" visible="false">
                                            <asp:DataGrid ID="DGR_SOCIALMEDIA" runat="server" CellPadding="4" PageSize="20"
                                                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" ShowHeader="False">
                                                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SOCMED_ID" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR">
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000CC" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="URL"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemStyle HorizontalAlign="Right" Width="30" VerticalAlign="Top" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TXT_SOCMED" CssClass="ASPTextBox" MaxLength="100" Width="200" runat="server" placeholder="Social Media ID ..."></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <EditItemStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                            </asp:DataGrid>
                                            <asp:Button ID="BT_SAVE_SOCMED" runat="server" Text="SAVE" CssClass="ASPButton" Width="100" OnClick="BT_SAVE_SOCMED_Click" />
                                        </div>

                                        <div id="DV_MARITAL" runat="server" visible="false">
                                            <table id="TBL_MARITAL" runat="server" style="border-spacing: 0px; font-size: xx-small; font-family: Verdana; width: 100%;">
                                                <tr>
                                                    <td style="width: 150px;">YEAR</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_YEAR" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style3">MARITAL STATUS</td>
                                                    <td class="auto-style3">
                                                        <asp:DropDownList ID="DDL_MARITAL" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style3">ANOTHER INCOME</td>
                                                    <td class="auto-style3">
                                                        <asp:DropDownList ID="DDL_ANOTHERINCOME" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Button ID="BT_MARITAL_SAVE" runat="server" Text="ADD" CssClass="ASPButton" OnClick="BT_MARITAL_SAVE_Click" Width="80px" />
                                            &nbsp;<asp:Button ID="BT_MARITAL_CANCEL" runat="server" Text="CANCEL" CssClass="ASPButton" BackColor="Red" ForeColor="White" Width="80px" Visible="false" OnClick="BT_MARITAL_CANCEL_Click" />
                                            <br />
                                            <asp:DataGrid ID="DGR_MARITAL" runat="server" CellPadding="2" PageSize="20"
                                                GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" OnItemCommand="DGR_MARITAL_ITEMCOMMAND" ShowHeader="False">
                                                <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                                <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="YEAR" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="MARITAL_STATUS" HeaderText="MARITAL STATUS" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="MARITAL_STATUS_DESCR" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ANOTHER_INCOME" Visible="false"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="LASTCHANGEBY" HeaderText="LAST CHANGE BY" Visible="false"></asp:BoundColumn>
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
                                        </div>
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
