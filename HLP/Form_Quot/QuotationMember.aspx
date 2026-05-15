<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationMember.aspx.cs" Inherits="HLP.Form_Quot.QuotationMember" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="../Script/PleaseWait.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="PleaseWait" runat="server">
        </div>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td style="border-bottom-style: ridge;">
                    <asp:DropDownList ID="DDL_MODE" CssClass="ASPDropDownList" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDL_MODE_SelectedIndexChanged">
                        <asp:ListItem Value="0">DATA INDIKATIF</asp:ListItem>
                        <asp:ListItem Value="1">DATA DETAIL</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="LB_ERR" runat="server" Font-Bold="True" ForeColor="Red" Style="font-size: x-small"></asp:Label>
                    <asp:Label ID="LB_QUOTNO" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_VER" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr id="TR_INDIKATIF" runat="server">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_SAVE_Click" Text="SAVE" Width="100px" />
                                        </td>
                                        <td id="TD_RATE_UPLOAD" runat="server">
                                            <table style="border-spacing: 0px;">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BT_XL" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_XL_Click" Text="XLS" BackColor="Green" /></td>
                                                    <td>
                                                        <table style="border-spacing: 0px;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Button ID="BT_XLS_UPLOAD" runat="server" OnClick="BT_XLS_UPLOAD_Click"
                                                                        Text="UPLOAD XLS" CssClass="ASPButton" Font-Bold="True" ForeColor="White" BackColor="Blue" />
                                                                </td>
                                                                <td>
                                                                    <input id="TXT_FILE_UPLOAD_PREMIUM" runat="server" name="TXT_FILE_UPLOAD" type="file"
                                                                        class="ASPTextBox" />&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">BENEFIT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_BENEFIT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_BENEFIT_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                                    BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Size="X-Small" GridLines="Vertical"
                                    PageSize="20" AutoGenerateColumns="False">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundColumn DataField="PKG_NO" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PP_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PACKAGE" HeaderText="PAKET">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PLAN_VALUE" HeaderText="PLAN">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="50px" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="MALE">
                                            <ItemTemplate>
                                                <asp:DataGrid ID="DGR_MALE" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" PageSize="20" ShowHeader="False" GridLines="None">
                                                    <ItemStyle VerticalAlign="Top" Wrap="False" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="START_AGE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="END_AGE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="MEMBER" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCR"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PREMIUM" Visible="false"></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TXT_MEMBER" runat="server" CssClass="ASPTextBoxNumber" Width="30px"></asp:TextBox>x
                                                                <asp:TextBox ID="TXT_PREMIUM" runat="server" CssClass="ASPTextBoxNumber" Width="60px" BackColor="Yellow"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="FEMALE">
                                            <ItemTemplate>
                                                <asp:DataGrid ID="DGR_FEMALE" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" PageSize="20" ShowHeader="False" GridLines="None">
                                                    <ItemStyle VerticalAlign="Top" Wrap="False" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="START_AGE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="END_AGE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="MEMBER" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCR"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PREMIUM" Visible="false"></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TXT_MEMBER0" runat="server" CssClass="ASPTextBoxNumber" Width="30px"></asp:TextBox>x
                                                                <asp:TextBox ID="TXT_PREMIUM0" runat="server" CssClass="ASPTextBoxNumber" Width="60px" BackColor="Yellow"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="MALE CHILD">
                                            <ItemTemplate>
                                                <asp:DataGrid ID="DGR_CHILD" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" PageSize="20" ShowHeader="False" GridLines="None">
                                                    <ItemStyle VerticalAlign="Top" Wrap="False" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="START_AGE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="END_AGE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="MEMBER" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCR"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PREMIUM" Visible="false"></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TXT_MEMBER1" runat="server" CssClass="ASPTextBoxNumber" Width="30px"></asp:TextBox>x
                                                                <asp:TextBox ID="TXT_PREMIUM1" runat="server" CssClass="ASPTextBoxNumber" Width="60px" BackColor="Yellow"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="FEMALE CHILD">
                                            <ItemTemplate>
                                                <asp:DataGrid ID="DGR_CHILD_F" runat="server" AutoGenerateColumns="False" Font-Size="X-Small" PageSize="20" ShowHeader="False" GridLines="None">
                                                    <ItemStyle VerticalAlign="Top" Wrap="False" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="START_AGE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="END_AGE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="MEMBER" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCR" HeaderText="DESCR"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PREMIUM" Visible="false"></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TXT_MEMBER2" runat="server" CssClass="ASPTextBoxNumber" Width="30px"></asp:TextBox>x
                                                                <asp:TextBox ID="TXT_PREMIUM2" runat="server" CssClass="ASPTextBoxNumber" Width="60px" BackColor="Yellow"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:TemplateColumn>



                                    </Columns>
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                        Mode="NumericPages" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_DETAIL" runat="server">
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td id="TD_UPLOAD_MEMBER" runat="server">
                                            <asp:Button ID="BT_EXCEL" runat="server" CssClass="ASPButton" OnClick="BT_EXCEL_Click" Text="DOWNLOAD EXCEL TEMPLATE FILE" Width="250px" />
                                            <asp:DropDownList ID="DDL_TEMPLATE" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem Value="4">TEMPLATE PAKET TERPISAH</asp:ListItem>
                                                <asp:ListItem Value="8">TEMPLATE PAKET MIX</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="TD_UPLOAD_MEMBER_1" runat="server">
                                            <table style="border-spacing: 0px;">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BT_UPLOAD" runat="server" Height="19px" OnClick="BT_UPLOAD_Click"
                                                            Text="UPLOAD EXCEL MEMBER DATA" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Width="250px" />
                                                    </td>
                                                    <td>
                                                        <input id="TXT_FILE_UPLOAD" runat="server" name="TXT_FILE_UPLOAD" type="file"
                                                            class="ASPTextBox" />&nbsp;
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
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <table style="border-spacing: 0px;">
                                                <tr>
                                                    <td>APPROVED</td>
                                                    <td>:</td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="LB_APPROVED" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>REJECTED</td>
                                                    <td>:</td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="LB_REJECTED" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>TOTAL</td>
                                                    <td>:</td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="LB_TOTAL" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_MEMBER" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_MEMBER_SelectedIndexChanged">
                                                            <asp:ListItem Value="0">REJECTED MEMBER</asp:ListItem>
                                                            <asp:ListItem Value="1">APPROVED MEMBER</asp:ListItem>
                                                            <asp:ListItem Value="2">APPROVED ADULT WOMEN MEMBER</asp:ListItem>
                                                            <asp:ListItem Value="3">APPROVED SUSPECT NEW MEMBER</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>:</td>
                                                    <td style="text-align: right;">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>

                                            <asp:DataGrid ID="DGR_MEMBER" runat="server" CellPadding="4" Font-Size="X-Small" GridLines="Horizontal"
                                                PageSize="20" ForeColor="#333333" AutoGenerateColumns="False" BorderColor="#666666" OnItemCommand="DGR_MEMBER_ItemCommand">
                                                <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" Wrap="False" VerticalAlign="Top" />
                                                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="PKG_NO" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NO" HeaderText="NO">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="MEMBER ID" HeaderText="MEMBER ID"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NAMA" HeaderText="NAMA"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TGL LAHIR" HeaderText="TGL LAHIR">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="USIA" HeaderText="USIA">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="GENDER" HeaderText="GENDER">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="PAKET" HeaderText="PAKET">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="E/S/C" HeaderText="E/S/C">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="KODE CAB" HeaderText="KODE CAB"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="KETERANGAN" HeaderText="KETERANGAN"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="MATERNITY" Visible="False">
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="MATERNITY" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CB" runat="server" AutoPostBack="True" OnCheckedChanged="CB_CheckedChanged" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:Button ID="BT_EDIT" runat="server" BackColor="Green" CommandName="Edit" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="E" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                                                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                            </asp:DataGrid>

                                            <table id="TBL_MEMBER_EDIT" runat="server" visible="false" style="border-spacing: 0px; background-color:yellow; border:outset;">
                                                <tr class="TDBGColor">
                                                    <td style="width: 100px;"></td>
                                                    <td><B>EDIT MEMBER</B></td>
                                                </tr>
                                                <tr>
                                                    <td>SEQ</td>
                                                    <td>
                                                        <asp:Label ID="LB_SEQ" runat="server" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>NAMA</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>TGL LAHIR</td>
                                                    <td>

                                                        <asp:TextBox ID="TXT_DOB" runat="server" CssClass="ASPTextBox" Width="70px"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB">
                                                        </ajaxToolkit:CalendarExtender>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>GENDER</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_SEX" runat="server" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>PAKET</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_PAKET" runat="server" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>E/S/C</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_ESC" runat="server" CssClass="ASPDropDownList">
                                                            <asp:ListItem>E</asp:ListItem>
                                                            <asp:ListItem>S</asp:ListItem>
                                                            <asp:ListItem>C</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">KODE CAB</td>
                                                    <td class="auto-style1">
                                                        <asp:TextBox ID="TXT_BRANCH" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="150px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style1">&nbsp;</td>
                                                    <td class="auto-style1">
                                                        <asp:Button ID="BT_UPDATEMEMBER" runat="server" CssClass="ASPButton" Text="UPDATE" OnClick="BT_UPDATEMEMBER_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
