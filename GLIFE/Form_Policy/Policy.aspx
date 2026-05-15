<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Policy.aspx.cs" Inherits="GLIFE.Form_Policy.Policy" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style>
        .alert {
            display: block;
            border: 3px solid red;
            padding: 10px;
            animation: blinker 5s linear infinite;
            color: red;
        }

        @keyframes blinker {
            25% {
                opacity: 0.5;
            }
            50% {
                opacity: 0;
            }
            75% {
                opacity: 0.5;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_READONLY" runat="server" Font-Bold="False" Visible="False"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr id="TR_ID" runat="server">
                            <td style="width: 100px;">ID</td>
                            <td>
                                <asp:Label ID="LB_ID" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">POLICY NO</td>
                            <td>
                                <%--<asp:TextBox ID="TXT_POLICYNO_PRODUCT_CODE" runat="server" CssClass="ASPTextBox" MaxLength="4" Width="42px"></asp:TextBox> <%--djanuar 20200219--%>
                                <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="150px" Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">COMPANY</td>
                            <td>
                                <asp:DropDownList ID="DDL_COMPANY" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                <asp:Button ID="BT_COMPANY_SEARCH" runat="server" CssClass="ASPButton" Text="?" OnClick="BT_COMPANY_SEARCH_Click" />
                                <asp:Label ID="LB_COMPANY" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="LB_COMPANY_DESCR" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">PRODUCT GROUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_PRODUCTGROUP" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">AGENT</td>
                            <td>
                                <asp:TextBox ID="TXT_AGENTCODE" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="100px" Visible="False"></asp:TextBox>
                                <asp:Label ID="LB_AGENT" runat="server" Font-Bold="true"></asp:Label>
                                <asp:Button ID="BT_AGENT_SEARCH" runat="server" CssClass="ASPButton" Text="?" OnClick="BT_AGENT_SEARCH_Click" />
                            </td>
                        </tr>
                        <tr id="TR_STAT" runat="server">
                            <td style="width: 100px;">STATUS</td>
                            <td>
                                <asp:Label ID="LB_STATCODE" runat="server" Visible="true"></asp:Label>
                                <asp:Label ID="LB_STAT" runat="server" Font-Bold="true"></asp:Label>
                                &nbsp;
                               
                                <asp:Button ID="BT_STAT" runat="server" CssClass="ASPButton" Width="100px" Font-Bold="True" OnClick="BT_STAT_Click" />
                            </td>
                        </tr>
                        <tr id="TR_RENEWAL" runat="server">
                            <td style="width: 100px; font-weight: bold;">RENEWAL KE</td>
                            <td>
                                <asp:Label ID="LB_RENEWAL" runat="server" Visible="true" Font-Bold="true"></asp:Label>
                                <%--<asp:Label ID="LB_STATX" runat="server" Font-Bold="true"></asp:Label>
                                &nbsp;
                               
                                <asp:Button ID="BT_STATX" runat="server" CssClass="ASPButton" Width="100px" Font-Bold="True" OnClick="BT_STAT_Click" />--%>
                            </td>
                        </tr>
            
                        <%--<tr id="TR_SAVE" runat="server">
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="80px" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>--%>
                    </table>
                </td>
            </tr>

            <!--ASWIN ADD-->
             <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">
                                <asp:Label ID="LB_MODE" runat="server" Text="RENEWAL" CssClass="ASPLabel" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DDL_MODE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_MODE_SelectedIndexChanged">
                                    <asp:ListItem Value="0">TIDAK</asp:ListItem>
                                    <asp:ListItem Value="1">YA</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                    </table>
                    <%--<asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>--%>
                </td>
            </tr>
            <tr id="TR_INSERT" runat="server" style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr id="TR_UPLOAD" runat="server" visible="false" style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 100px;">POLICY LAST</td>
                            <td>
                                
                                <asp:DropDownList ID="DDL_POLISLAST" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                <asp:Button ID="BT_POLISLAST_SEARCH" runat="server" CssClass="ASPButton" Text="?" OnClick="BT_POLISLAST_SEARCH_Click" />
                                <asp:Label ID="LB_POLISLAST" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="LB_POLISLAST_DESCR" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>POLICY PERIODE</td>
                            <td class="auto-style2">
                                <asp:TextBox ID="TXT_PROCDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PROCDATE">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;- <asp:TextBox ID="TXT_PROCDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_PROCDATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr id="TR_SAVE" runat="server">
                            <td style="width: 100px;"></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="80px" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!--ASWIN END-->
           <tr>
                <td><asp:Label ID="LB_WARNING" runat="server"></asp:Label></td>
            </tr>
            <tr id="TR_BUTTONS" runat="server">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT1" runat="server" Text="TERM & COND." Width="100%" CssClass="ASPButton" OnClick="BT1_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT5" runat="server" Text="DISTRIBUTION" Width="100%" CssClass="ASPButton" OnClick="BT5_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT2" runat="server" Text="LOADING" Width="100%" CssClass="ASPButton" OnClick="BT2_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT7" runat="server" Text="INVOICE" Width="100%" CssClass="ASPButton" OnClick="BT7_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT3" runat="server" Text="REINSURANCE" Width="100%" CssClass="ASPButton" OnClick="BT3_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT6" runat="server" Text="OTHER SETUP" Width="100%" CssClass="ASPButton" OnClick="BT6_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT4" runat="server" Text="ARCHIEVE" Width="100%" CssClass="ASPButton" OnClick="BT4_Click" />
                                        </td>
                                        <td style="width: 12.5%">
                                            <asp:Button ID="BT8" runat="server" Text="IKHTISAR" Width="100%" CssClass="ASPButton" OnClick="BT8_Click" />
                                        </td>
                                        <td style="width: 14%">
                                            <asp:Button ID="btnCN" runat="server" Text="SETUP CN/CR" Width="100%" CssClass="ASPButton" OnClick="btnCN_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">
                                <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.8;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Width="80%" Style="height: 80vh; z-index: 111; background-color: White; position: fixed; left: 0px; top: 10px; border: outset 2px gray; padding: 5px; display: none; overflow: auto;">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                            <asp:Label ID="LB_SEARCH_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_SEARCH_CLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                    </tr>
                    <tr id="TR_COMPANY_SEARCH" runat="server">
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr>
                                    <td style="width: 100px;">COMPANY NAME</td>
                                    <td>
                                        <asp:TextBox ID="TXT_COMPANY_SEARCH" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>LINE OF BUSINESS</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_COMPANYLOB_SEARCH" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="TR_POLISLAST_SEARCH" runat="server">
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr>
                                    <td style="width: 100px;">NO POLICY</td>
                                    <td>
                                        <asp:TextBox ID="TXT_POLISLAST_SEARCH" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>COMPANY NAME</td>
                                     <%--<td>
                                        <asp:TextBox ID="TXT_COMPANYNAME_SEARCH" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                    </td>--%>
                                    <td>
                                        <asp:DropDownList ID="DDL_POLISLASTLOB_SEARCH" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="TR_AGENT_SEARCH" runat="server">
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr>
                                    <td style="width: 100px;">AGENT NAME</td>
                                    <td>
                                        <asp:TextBox ID="TXT_AGENTNAME_SEARCH" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>SUB CHANNEL</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_AGENTCHANNEL_SEARCH" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" />
                            &nbsp;
                           
                            <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                            <asp:DataGrid ID="DGR_SEARCH" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_SEARCH_ItemCommand">
                                <ItemStyle VerticalAlign="Top" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <HeaderStyle
                                    Wrap="False" />
                                <Columns>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBT_CODE" runat="server" CommandName="Select"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NAME">
                                        <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCR">
                                        <ItemStyle Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>