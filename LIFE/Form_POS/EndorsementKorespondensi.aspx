<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementKorespondensi.aspx.cs" Inherits="LIFE.Form_POS.EndorsementKorespondensi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
                        </tr>
                    </table>

                    <table id="TBL_MEMBER" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>

                    <table id="TBL_ADDRESS" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">ADDRESS TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ADDTYPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_ADDTYPE_SelectedIndexChanged" >
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>ADDRESS</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ADDRESS" runat="server" MaxLength="255" Width="90%" TextMode="MultiLine" Height="60px" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CITY</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CITY" runat="server" MaxLength="50" Width="90%" Style="background-color: yellow" CssClass="ASPTextBox"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>PROVINCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PROVINCE" runat="server" BackColor="Yellow" CssClass="ASPDropDownList"></asp:DropDownList>
                                            <asp:TextBox ID="TXT_PROVINCE" runat="server" CssClass="ASPTextBox" AutoPostBack="true" Width="30%" OnTextChanged="TXT_PROVINCE_TextChanged" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>COUNTRY</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_COUNTRY" runat="server" BackColor="Yellow" CssClass="ASPDropDownList"></asp:DropDownList>
                                            <asp:TextBox ID="TXT_COUNTRY" runat="server" CssClass="ASPTextBox" AutoPostBack="true" Width="30%" OnTextChanged="TXT_COUNTRY_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ZIP CODE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ZIPCODE" runat="server" MaxLength="10" Width="100px" Style="background-color: yellow" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PHONE 1</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PHONE1" runat="server" MaxLength="50" Width="90%" Style="background-color: yellow" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PHONE 2</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PHONE2" runat="server" MaxLength="50" Width="90%" CssClass="ASPTextBox"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>EMAIL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_EMAIL" runat="server" MaxLength="100" Width="90%" Style="background-color: yellow" CssClass="ASPTextBox"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE_ADDRESS" runat="server" CssClass="ASPButton" Text="SAVE ADDRESS" Width="100" OnClick="BT_SAVE_ADDRESS_Click"/>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">PREVIEW<br />
                                ADDRESS DATA CHANGES</td>
                        </tr>
                    </table>
                    <table id="Table1" runat="server" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                &nbsp;

                            </td>
                        </tr>
                    </table>
                    <table id="TBLADDRESSCHANGES" runat="server" style="border-spacing: 0px; width: 100%;" visible="false">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="DGR" runat="server" ItemStyle-Wrap="true" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundColumn DataField="DESCR" HeaderText="PARAMETER"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="OLD_VAL" HeaderText="OLD VALUE"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="NEW_VAL" HeaderText="NEW VALUE"></asp:BoundColumn>
                                            </Columns>
                                               <EditItemStyle BackColor="#2461BF" />
                                               <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                               <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" VerticalAlign="Top" />
                                               <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#EFF3FB" />
                                               <AlternatingItemStyle BackColor="White" />
                                               <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                               <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            </asp:DataGrid>
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
