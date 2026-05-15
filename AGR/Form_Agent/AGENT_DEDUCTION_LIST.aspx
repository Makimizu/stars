<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_DEDUCTION_LIST.aspx.cs" Inherits="AGR.AGENT_DEDUCTION_LIST" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        [aria-current="page"] {
            pointer-events: none;
            cursor: default;
            text-decoration: none;
            color: black;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.6;
            filter: alpha(opacity=80);
            -moz-opacity: 0.6;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: transparent;
            z-index: 999;
        }

        .fa {
            display: inline-block;
            font: normal normal normal 14px/1 FontAwesome;
            font-size: inherit;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
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
                <td>DEDUCTION TYPE</td>
                <td>
                    <asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_TYPE_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>STATUS</td>
                <td>
                    <asp:DropDownList ID="DDL_STATUS" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_STATUS_SelectedIndexChanged">
                        <asp:ListItem Value="a.APPROVEBY is null ">REGISTER</asp:ListItem>
                        <asp:ListItem Value="a.APPROVEBY is not null and a.DEDUCTION_SETTLE_DATE is null">APPROVED &amp; NOT PAID</asp:ListItem>
                        <asp:ListItem Value="a.APPROVEBY is not null and a.DEDUCTION_SETTLE_DATE is not null ">APPROVED &amp; PAID</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="TR_SETTLE_DATE" runat="server" visible="false">
                <td style="width: 100px;">SETTLE DATE</td>
                <td>
                    <asp:TextBox ID="TXT_START_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE">
                    </ajaxToolkit:CalendarExtender>
                    &nbsp;-&nbsp;
                    <asp:TextBox ID="TXT_END_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE">
                    </ajaxToolkit:CalendarExtender>
                </td>
            </tr>
            <tr id="TR_OUTSTANDING" runat="server" visible="false">
                <td>OUTSTANDING</td>
                <td>
                    <asp:DropDownList ID="DDL_OUTSTANDING" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_STATUS_SelectedIndexChanged">
                        <asp:ListItem Value="">YES &amp; NO</asp:ListItem>
                        <asp:ListItem Value="and ROUND(isnull(a.DEDUCTION_OUTSTANDING, 0), 0) > 0 ">YES</asp:ListItem>
                        <asp:ListItem Value="and ROUND(isnull(a.DEDUCTION_OUTSTANDING, 0), 0) = 0 ">NO</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                    &nbsp;
                    <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW" Width="100px" BackColor="Green" ForeColor="White" OnClick="BT_NEW_Click" />
                </td>
            </tr>
        </table>


        <asp:Label ID="LB_RECORDS" runat="server" Font-Size="X-Small"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20"
            GridLines="None" CssClass="ASPDatagrid"
            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" OnItemCommand="DGR_ItemCommand" Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
            <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
            <Columns>
                <asp:TemplateColumn HeaderText="AGENT CODE">
                    <HeaderStyle Width="100" />
                    <ItemTemplate>
                        <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Select"></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="DEDUCTION_ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTION_TYPE_DESCR" HeaderText="DEDUCTION TYPE"></asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTION_AMOUNT" HeaderText="AMOUNT">
                    <HeaderStyle Width="100" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="Green" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTION_PAYMENT" HeaderText="PAYMENT">
                    <HeaderStyle Width="100" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="Blue" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTION_OUTSTANDING" HeaderText="OUTSTANDING">
                    <HeaderStyle Width="100" HorizontalAlign="Right" />
                    <ItemStyle ForeColor="Red" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTION_SETTLE_DATE" HeaderText="SETTLE DATE">
                    <HeaderStyle Width="80" HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="File Deduction">
                    <ItemTemplate>
                        <asp:Label ID="LB_DESCR" runat="server" Visible="False" Font-Size="XX-Small" ForeColor="Gray"></asp:Label>
                        <asp:LinkButton ID="LBT_DESCR" runat="server" Visible="False" ToolTip="Download" Text="Download" CommandName="Download"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="REQUESTBY" HeaderText="REQUEST BY"></asp:BoundColumn>
                <asp:BoundColumn DataField="APPROVEBY" HeaderText="APPROVE BY"></asp:BoundColumn>
                <asp:BoundColumn DataField="NAMAFILE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
            </Columns>
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
        </asp:DataGrid>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="270px" Width="60%" Style="z-index: 111; background-color: White; position: fixed; left: 120px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td class="TDBGColor">
                            <asp:Label ID="LB_DEDUCTION_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                            <asp:Label ID="LB_DEDUCTIONID" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td width="20px">
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <center>
                                <asp:DataGrid ID="DGR_DUDUCTION_HISTORY" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" PageSize="30"
                                    GridLines="Horizontal" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%">
                                    <itemstyle wrap="False" verticalalign="Top" backcolor="#E7E7FF" forecolor="#4A3C8C" />
                                    <selecteditemstyle backcolor="#738A9C" forecolor="#F7F7F7" font-bold="True" />
                                    <alternatingitemstyle backcolor="#F7F7F7" verticalalign="Top" />
                                    <itemstyle backcolor="#EEEEEE" forecolor="Black" />
                                    <headerstyle backcolor="#4A3C8C" font-bold="True" forecolor="#F7F7F7" />
                                    <columns>
                                        <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT"></asp:BoundColumn>
                                    </columns>
                                    <footerstyle backcolor="#B5C7DE" forecolor="#4A3C8C" />
                                    <pagerstyle backcolor="#E7E7FF" forecolor="#4A3C8C" horizontalalign="Right" mode="NumericPages" />
                                </asp:DataGrid>
                            </center>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

    </form>
</body>
</html>

