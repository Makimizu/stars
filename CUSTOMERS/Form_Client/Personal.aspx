<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Personal.aspx.cs" Inherits="CUSTOMERS.Form_Client.Personal" %>

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
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">FULLNAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>GENDER</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_GENDER" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="M">MALE</asp:ListItem>
                                                <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DOB</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DOBSTART" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" TargetControlID="TXT_DOBSTART" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-&nbsp;
                                                <asp:TextBox ID="TXT_DOBTO" runat="server" Font-Names="Tahoma" Font-Size="X-Small" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="TXT_REGDATETO_CalendarExtender" runat="server" TargetControlID="TXT_DOBTO" Format="dd/MM/yyyy">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ID NUMBER</td>
                                        <td>
                                            <asp:TextBox ID="TXT_IDNO" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">CITY</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CITY" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>JOB</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_JOB" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PROVINCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PROVINCE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Width="100px" OnClick="BT_SEARCH_Click" Text="SEARCH" />
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
                    <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True" EnableViewState="false" ForeColor="Blue"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="40"
                        OnPageIndexChanged="DGR_PageIndexChanged"
                        AllowPaging="True" AutoGenerateColumns="False" CssClass="ASPDatagrid" GridLines="None" ForeColor="#333333" Width="100%" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" BorderColor="Black" BorderWidth="0" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="FULL NAME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_ID" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SEX" HeaderText="GENDER">
                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="JOB_DESCR" HeaderText="JOB"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MARITAL_STATUS_DESCR" HeaderText="MARITAL"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CITIZENSHIP_DESCR" HeaderText="CITIZENSHIP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CITY" HeaderText="CITY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVINCE_DESCR" HeaderText="PROVINCE"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                            Wrap="False" HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
