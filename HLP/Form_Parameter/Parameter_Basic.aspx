<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parameter_Basic.aspx.cs" Inherits="HLP.Form_Parameter.Parameter_Basic" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
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
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="upBody" runat="server">
            <ContentTemplate>
                <table class="style1"
                    style="top: 0px; left: 0px; width: 100%; position: absolute; height: 100%">
                    <tr>
                        <td class="style2" valign="top">
                            <asp:TextBox ID="TXT_PARAM" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnTextChanged="TXT_PARAM_TextChanged" Width="100%"></asp:TextBox>
                            <asp:ListBox ID="LBX_PARAM" runat="server" AutoPostBack="True" CssClass="ASPListBox" Height="500px" OnSelectedIndexChanged="LBX_PARAM_SelectedIndexChanged" Width="100%"></asp:ListBox>
                        </td>
                        <td valign="top">
                            <table class="style3">
                                <tr>
                                    <td bgcolor="Silver">
                                        <asp:Label ID="LB_PARAM" runat="server" Font-Bold="True" 
                                            Font-Size="Small"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DataGrid ID="DGR2" runat="server"  Font-Size="X-Small" GridLines="None"
                                            PageSize="20" Width="90%" AutoGenerateColumns="False"
                                            OnItemCommand="DGR2_ItemCommand" ShowFooter="True"
                                            OnSelectedIndexChanged="DGR2_SelectedIndexChanged">
                                            <Columns>
                                                <asp:BoundColumn DataField="name">
                                                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="xtype" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="length" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="isnullable" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="prm" Visible="False"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <FooterTemplate>
                                                        <asp:Button ID="BT_SUBMIT" runat="server" CommandName="Submit" Font-Bold="True"
                                                             Font-Size="XX-Small" Text="SUBMIT" />
                                                    </FooterTemplate>
                                                    <HeaderTemplate>
                                                        <asp:Button ID="BT_NEW" runat="server" CommandName="New" Font-Bold="True"
                                                             Font-Size="XX-Small" Text="NEW RECORD" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_VAL" runat="server" 
                                                            Font-Size="X-Small" Width="382px" CssClass="ASPTextBox"></asp:TextBox>
                                                        <asp:TextBox ID="TXT_DATE" runat="server"  Font-Size="X-Small" CssClass="ASPTextBox"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="MM/dd/yyyy" TargetControlID="TXT_DATE">
                                                        </ajaxToolkit:CalendarExtender>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" 
                                            Font-Size="XX-Small" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DataGrid ID="DGR1" runat="server" BackColor="White"
                                            BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                            CellSpacing="1"  Font-Size="X-Small" GridLines="Vertical"
                                            PageSize="20" OnItemCommand="DGR1_ItemCommand"
                                            OnPageIndexChanged="DGR1_PageIndexChanged" ItemStyle-Wrap="true">
                                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <AlternatingItemStyle BackColor="#F7F7F7" />
                                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top"  />
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
