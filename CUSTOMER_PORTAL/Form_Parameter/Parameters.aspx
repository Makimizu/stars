<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parameters.aspx.cs" Inherits="CUSTOMER_PORTAL.Form_Parameter.Parameters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .style1 {
            border-collapse: collapse;
        }

        .style2 {
            width: 230px;
        }

        .style3 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxtoolkit:toolkitscriptmanager id="tkScriptManager" runat="server">
    </ajaxtoolkit:toolkitscriptmanager>
        <asp:UpdatePanel ID="upBody" runat="server">
            <ContentTemplate>
                <table class="style1"
                    style="top: 0px; left: 0px; width: 100%; position: absolute; height: 100%">
                    <tr>
                        <td class="style2" valign="top">
                            <asp:TextBox ID="TXT_PARAM" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnTextChanged="TXT_PARAM_TextChanged" Width="90%"></asp:TextBox>
                            <asp:ListBox ID="LBX_PARAM" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" Height="500px" OnSelectedIndexChanged="LBX_PARAM_SelectedIndexChanged" Width="100%"></asp:ListBox>
                            <asp:DataGrid ID="DGR0" runat="server" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small"
                                GridLines="Vertical" OnItemCommand="DGR0_ItemCommand" PageSize="20"
                                ShowHeader="False" Width="100%" ForeColor="Black">
                                <SelectedItemStyle BackColor="#00CC00" Font-Bold="True" ForeColor="White"
                                    Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#F7F7DE" HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <FooterStyle BackColor="#CCCC99" />
                                <Columns>
                                    <asp:BoundColumn DataField="name" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="alias" Visible="False"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBT" runat="server" CommandName="Select" Font-Bold="True"
                                                Font-Names="Tahoma"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right"
                                    Mode="NumericPages" />
                            </asp:DataGrid>
                        </td>
                        <td valign="top">
                            <table class="style3">
                                <tr>
                                    <td bgcolor="Silver">
                                        <asp:Label ID="LB_PARAM" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                            Font-Size="Small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DataGrid ID="DGR2" runat="server" BackColor="White"
                                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal"
                                            PageSize="20" Width="90%" AutoGenerateColumns="False"
                                            OnItemCommand="DGR2_ItemCommand" ShowFooter="True"
                                            OnSelectedIndexChanged="DGR2_SelectedIndexChanged">
                                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <AlternatingItemStyle BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <ItemStyle BackColor="#FFFFCC" ForeColor="#4A3C8C" Font-Bold="False"
                                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" />
                                            <HeaderStyle Font-Bold="True" ForeColor="#F7F7F7" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <FooterStyle ForeColor="#4A3C8C" Font-Bold="False" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <Columns>
                                                <asp:BoundColumn DataField="name"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="xtype" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="length" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="isnullable" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="prm" Visible="False"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <FooterTemplate>
                                                        <asp:Button ID="BT_SUBMIT" runat="server" CommandName="Submit" Font-Bold="True"
                                                            Font-Names="Tahoma" Font-Size="XX-Small" Text="SUBMIT" />
                                                    </FooterTemplate>
                                                    <HeaderTemplate>
                                                        <asp:Button ID="BT_NEW" runat="server" CommandName="New" Font-Bold="True"
                                                            Font-Names="Tahoma" Font-Size="XX-Small" Text="NEW RECORD" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_VAL" runat="server" Font-Names="Tahoma"
                                                            Font-Size="X-Small" Width="382px"></asp:TextBox>
                                                        <asp:TextBox ID="TXT_DATE" runat="server" Font-Names="Tahoma" Font-Size="X-Small"></asp:TextBox>
                                                        <ajaxtoolkit:calendarextender id="ceDelegatePeriodStart" runat="server" format="MM/dd/yyyy" targetcontrolid="TXT_DATE">
                                    </ajaxtoolkit:calendarextender>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                                Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                            Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DataGrid ID="DGR1" runat="server" BackColor="White"
                                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal"
                                            PageSize="20" OnItemCommand="DGR1_ItemCommand"
                                            OnPageIndexChanged="DGR1_PageIndexChanged" Width="600px" ItemStyle-Wrap="true">
                                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <AlternatingItemStyle BackColor="#F7F7F7" />
                                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <Columns>
                                                <asp:ButtonColumn CommandName="Select" Text="Select">
                                                    <HeaderStyle Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonColumn>
                                            </Columns>
                                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                                Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

