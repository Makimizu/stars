<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ClaimSendEmail.aspx.cs" Inherits="GLIFE.Form_Claim.ClaimSendEmail" %>

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
                            <td style="width: 120px;">APPLICATION</td>
                            <td>
                                <asp:DropDownList ID="DDL_SENDED" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="">SELECT</asp:ListItem>
                                    <asp:ListItem Value="is not null">SENDED</asp:ListItem>
                                    <asp:ListItem Value="is null">NOT SENDED</asp:ListItem>
                                    <asp:ListItem Value="is not null">SENDED</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">CLAIM STATUS</td>
                            <td>
                                <asp:DropDownList ID="DDL_STATUS" runat="server" CssClass="ASPDropDownList">
                                    <asp:ListItem Value="">SELECT</asp:ListItem>
                                    <asp:ListItem Value="APPROVED">APPROVED</asp:ListItem>
                                    <asp:ListItem Value="PENDING">PENDING</asp:ListItem>
                                    <asp:ListItem Value="REJECTED">REJECTED</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>DOCNO</td>
                            <td>
                                <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>REGNO</td>
                            <td>
                                <asp:TextBox ID="TXT_REGNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>FULLNAME</td>
                            <td>
                                <asp:TextBox ID="TXT_FULLNAME" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>RECIPIENT COMPANY</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">CLAIM DATE</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="TXT_CLAIMDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:calendarextender id="CalendarExtender1" runat="server" format="dd/MM/yyyy" targetcontrolid="TXT_CLAIMDATE1">
                                </ajaxToolkit:calendarextender>
                                &nbsp;-
                                <asp:TextBox ID="TXT_CLAIMDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:calendarextender id="CalendarExtender2" runat="server" format="dd/MM/yyyy" targetcontrolid="TXT_CLAIMDATE2">
                                </ajaxToolkit:calendarextender>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                <asp:Label ID="LB_TRACK" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" OnItemCommand="DGR_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged" PageSize="20" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="REGNO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_REGNO" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REGNO" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOCNO" HeaderText="DOCNO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CLAIM_DATE" HeaderText="CLAIM DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY NAME"></asp:BoundColumn>                                                      
                            <asp:BoundColumn DataField="TOTAL" HeaderText="TOTAL">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LAST_TRACK_DESCR" HeaderText="CLAIM STATUS"></asp:BoundColumn>                                                      
                            <asp:BoundColumn DataField="BRANCH_CODE" Visible="false"></asp:BoundColumn>  
                            <asp:BoundColumn DataField="BRANCH_PIC" Visible="false"></asp:BoundColumn> 
                            <asp:BoundColumn DataField="PIC_NAMA" Visible="false"></asp:BoundColumn> 
                            <asp:BoundColumn DataField="PIC_EMAIL" Visible="false"></asp:BoundColumn> 
                            <asp:TemplateColumn HeaderText="PIC EMAIL">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" BackColor="#ffff99" Width="100px"></asp:TextBox>
                                    <asp:Button ID="BT_EMAIL" runat="server" CommandName="Process" CssClass="ASPButton" Text="PROCESS" ForeColor="White" BackColor="Green" Font-Bold="True" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Wrap="false" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle
                            Wrap="False" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <AlternatingItemStyle BackColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
