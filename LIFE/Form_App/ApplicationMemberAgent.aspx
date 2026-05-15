<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationMemberAgent.aspx.cs" Inherits="LIFE.Form_App.ApplicationMemberAgent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <asp:Button ID="BT_MEMBER" runat="server" CssClass="ASPButton" Text="MEMBER LIST" Width="100%" OnClick="BT_MEMBER_Click" />
                    <asp:Button ID="BT_ADDRESS" runat="server" CssClass="ASPButton" Text="ADDRESS" Width="100%" OnClick="BT_ADDRESS_Click" />
                    <asp:Button ID="BT_BANK" runat="server" CssClass="ASPButton" Text="BANK ACOUNT" Width="100%" OnClick="BT_BANK_Click" />
                    <asp:Button ID="BT_AGENT" runat="server" CssClass="ASPButton" Text="AGENT" Width="100%" OnClick="BT_AGENT_Click" />
                </td>
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
                                <asp:DataGrid ID="DGR_MEMBER" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_MEMBER_ItemCommand">
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="FULLNAME" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="FULLNAME">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBT_FULLNAME" runat="server" CommandName="Select"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="STAT" HeaderText="STATUS"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RELATION" HeaderText="RELATION"></asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                                <asp:Label ID="LB_WARNING" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_ADDRESS" runat="server" style="border-spacing: 0px; width: 100%;" visible="false">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">ADDRESS TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ADDTYPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_ADDTYPE_SelectedIndexChanged">
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
                                            <asp:TextBox ID="TXT_PROVINCE" runat="server" CssClass="ASPTextBox" AutoPostBack="true" Width="30%" OnTextChanged="TXT_PROVINCE_TextChanged"></asp:TextBox>
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
                                            <asp:Button ID="BT_SAVE_ADDRESS" runat="server" CssClass="ASPButton" Text="SAVE ADDRESS" Width="100" OnClick="BT_SAVE_ADDRESS_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_BANK" runat="server" style="border-spacing: 0px; width: 100%;" visible="false">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">ACCOUNT TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ACCTYPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_ACCTYPE_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BANK NAME</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList" Width="60%">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="TXT_BANK" runat="server" CssClass="ASPTextBox" AutoPostBack="true" Width="30%" OnTextChanged="TXT_BANK_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ACCOUNT NAME</td>
                                        <td>
                                            <asp:TextBox CssClass="ASPTextBox" ID="TXT_ACCNAMA" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ACCOUNT NUMBER</td>
                                        <td>
                                            <asp:TextBox CssClass="ASPTextBox" ID="TXT_ACCNO" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE_ACCOUNT" runat="server" CssClass="ASPButton" Text="SAVE ACCOUNT" Width="100" OnClick="BT_SAVE_ACCOUNT_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_AGENT" runat="server" style="border-spacing: 0px; width: 100%;" visible="false">
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_AGENT" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="4" GridLines="None">
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#F7F7F7" />
                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <Columns>
                                        <asp:BoundColumn DataField="THEDATE" HeaderText="EFFECTIVE DATE">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="COMMISSION_TYPE" HeaderText="COMMISSION TYPE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AGENT_CODE" HeaderText="AGENT CODE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AGENT_NAME" HeaderText="AGENT NAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CHANNEL" HeaderText="CHANNEL"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL"></asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                </asp:DataGrid>
                                <br />
                                <iframe id="IF_AGENT" runat="server" style="width: 100%; height: 70vh; border: none;"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="340px" Width="80%" Style="z-index: 111; background-color: White; position: fixed; left: 60px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="LB_BACK" runat="server" Text="Back .." ForeColor="Red" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDBGColor">
                            <asp:Label ID="LB_MEMBERTITLE" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <iframe id="IF" runat="server" style="width: 100%; height: 300px; border: none;"></iframe>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
