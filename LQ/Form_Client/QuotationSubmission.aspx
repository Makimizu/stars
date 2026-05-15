<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationSubmission.aspx.cs" Inherits="LQ.Form_Client.QuotationSubmission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script type="text/javascript">
        function CheckNumeric() {
            return event.keyCode >= 48 && event.keyCode <= 57;
        }
    </script>
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
        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td class="TDBGColor">AGENT</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 130px;">AGENT CODE</td>
                            <td>
                                <asp:Label ID="LB_CODE" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>AGENT NAME</td>
                            <td>
                                <asp:Label ID="LB_NAME" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px;">CHANNEL</td>
                            <td>
                                <asp:Label ID="LB_CHANNEL" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>LEVEL</td>
                            <td>
                                <asp:Label ID="LB_LEVEL" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>AGENCY NAME</td>
                            <td>
                                <asp:Label ID="LB_AGENCY" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
            <tr>
                <td style="width: 48%;" class="TDBGColor">PESERTA UTAMA</td>
                <td></td>
                <td style="width: 48%;" class="TDBGColor">PRODUCT</td>
            </tr>
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="border-bottom: ridge;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 130px;">MODE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PERSON_MODE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_PERSON_MODE_SelectedIndexChanged">
                                                <asp:ListItem Value="0">DUMMY PERSON</asp:ListItem>
                                                <asp:ListItem Value="1">NEW ENTRY</asp:ListItem>
                                                <asp:ListItem Value="2">EXISTING PERSON</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="DV_DUMMY" runat="server">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 130px;">GENDER</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_GENDER_DUMMY" runat="server" CssClass="ASPDropDownList">
                                                    <asp:ListItem Value="M">MALE</asp:ListItem>
                                                    <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>DOB</td>
                                            <td>
                                                <asp:TextBox ID="TXT_DOB_DUMMY" runat="server" CssClass="ASPTextBoxUPPER" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB_DUMMY">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>TGL. TERIMA BERKAS</td>
                                            <td>
                                                <asp:TextBox ID="TXT_BERKAS_DUMMY" runat="server" CssClass="ASPTextBoxUPPER" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_BERKAS_DUMMY">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>

                                    </table>
                                </div>

                                <div id="DV_ENTRY" runat="server" visible="false">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 130px;">FULLNAME</td>
                                            <td>
                                                <asp:TextBox ID="TXT_FULLNAME_ENTRY" runat="server" CssClass="ASPTextBoxUPPER" Width="100%" oninput="checkMember()"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>GENDER</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_GENDER_ENTRY" runat="server" CssClass="ASPDropDownList">
                                                    <asp:ListItem Value="M">MALE</asp:ListItem>
                                                    <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>DOB</td>
                                            <td>
                                                <asp:TextBox ID="TXT_DOB_ENTRY" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" onchange="checkMember()" oninput="checkMember()"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB_ENTRY">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>TGL. TERIMA BERKAS</td>
                                            <td>
                                                <asp:TextBox ID="TXT_BERKAS_ENTRY" runat="server" CssClass="ASPTextBoxUPPER" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_BERKAS_ENTRY">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td style="width: 130px;">ID CODE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_IDNO_ENTRY" runat="server" onkeypress="return CheckNumeric();"></asp:TextBox>
                                        </tr>
                                    </table>
                                </div>

                                <div id="DV_EXISTING" runat="server" visible="false">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 130px;">FULLNAME</td>
                                            <td>
                                                <asp:TextBox ID="TXT_FULLNAME_EXISTING" runat="server" CssClass="ASPTextBoxUPPER" Width="100%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>GENDER</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_GENDER_EXISTING" runat="server" CssClass="ASPDropDownList">
                                                    <asp:ListItem Value="M">MALE</asp:ListItem>
                                                    <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>DOB</td>
                                            <td>
                                                <asp:TextBox ID="TXT_DOB_EXISTING" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB_EXISTING">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>TGL. TERIMA BERKAS</td>
                                            <td>
                                                <asp:TextBox ID="TXT_BERKAS_EXISTING" runat="server" CssClass="ASPTextBoxUPPER" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_BERKAS_EXISTING">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td style="width: 130px;">ID CODE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_IDNO_EXISTING" runat="server" onkeypress="return CheckNumeric();"></asp:TextBox>
                                                <%--<asp:TextBox ID="TXT_IDNO_EXISTING" runat="server" CssClass="ASPTextBoxUPPER" Width="100%"></asp:TextBox></td>--%>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_SEARCH_EXISTING" runat="server" Text="SEARCH" Width="100" CssClass="ASPTextBox" OnClick="BT_SEARCH_EXISTING_Click" /></td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="DGR_PERSON_EXISTING" runat="server" BackColor="White"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" CssClass="ASPDatagrid" OnPageIndexChanged="DGR_PERSON_EXISTING_PageIndexChanged">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="False" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" Visible="False">
                                                <ItemStyle Width="40" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DOB" HeaderText="DOB"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="GENDER" HeaderText="GENDER"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_NO" HeaderText="ID NUMBER"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle Width="30" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CB_EXISTING" runat="server" AutoPostBack="true" OnCheckedChanged="CB_EXISTING_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                            Mode="NumericPages" />
                                    </asp:DataGrid>

                                </div>
                                <br />
                                <asp:Label ID="LB_WARNING" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="border-bottom: ridge;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 130px;">PRODUCT GROUP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PRODUCTGROUP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_PRODUCTGROUP_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                    </tr>
                                </table>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_PRODUCT" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical"
                                    PageSize="20" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" CssClass="ASPDatagrid">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="False" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="PRODUCT_CODE" HeaderText="CODE">
                                            <ItemStyle Width="80" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PRODUCT_DESCR" HeaderText="PRODUCT NAME"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_TC" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="30" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB" runat="server" AutoPostBack="true" OnCheckedChanged="CB_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
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

        <br />
        <br />
        <center>
            <asp:Button ID="BT_SUBMIT" runat="server" Text="SUBMIT" CssClass="ASPButton" Width="200" BackColor="#3399ff" ForeColor="White" OnClick="BT_SUBMIT_Click" Font-Size="Small"></asp:Button>
        </center>
        <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
    </form>
</body>
</html>
