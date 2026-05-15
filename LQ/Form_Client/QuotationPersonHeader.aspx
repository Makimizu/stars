<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationPersonHeader.aspx.cs" Inherits="LQ.Form_Client.QuotationPersonHeader" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <script>
        function resizeIframe(obj) {
            obj.style.height = obj.contentWindow.document.documentElement.scrollHeight + 'px';
        }
    </script>
    <script type="text/javascript">
        function CheckNumeric() {
            return event.keyCode >= 48 && event.keyCode <= 57;
        }

        function ValidateEmail(ctrl) {
            var val = ctrl.value;
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(val)) {
                return (true)
            }
            alert("You have entered an invalid email address!")
            return (false)
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
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_CATEGORY" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td style="width: 250px;">
                    <asp:Button ID="BT_LIST" runat="server" CssClass="ASPButton" Width="100%" Text="PESERTA" OnClick="BT_LIST_Click" />
                    <asp:Button ID="BT_BEN" runat="server" CssClass="ASPButton" Width="100%" Text="PENERIMA MANFAAT" OnClick="BT_BEN_Click" />
                    <asp:Button ID="BT_ADDRESS" runat="server" CssClass="ASPButton" Width="100%" Text="ALAMAT" OnClick="BT_ADDRESS_Click" />
                    <asp:Button ID="BT_BANK" runat="server" CssClass="ASPButton" Width="100%" Text="REKENING BANK" OnClick="BT_BANK_Click" />
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
                        </tr>
                    </table>

                    <table id="TBL_BANK" runat="server" visible="false" style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">ACCOUNT TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ACCTYPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_ACCTYPE_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 130px;">BANK NAME</td>
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
                                            <asp:TextBox CssClass="ASPTextBox" ID="TXT_ACCNO" runat="server" MaxLength="100" Width="250px" onkeypress="return CheckNumeric();"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <asp:DataGrid ID="DGR_ITEM" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%" Visible="True">
                                    <ItemStyle VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR">
                                            <ItemStyle Width="130px" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="ASPDropDownList" Visible="False" AutoPostBack="true" OnSelectedIndexChanged="DDL_REFF_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBoxUPPER" Visible="False" Width="150px"></asp:TextBox>
                                                <asp:TextBox ID="TXT_VALDATE" runat="server" CssClass="ASPTextBox" Visible="False" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_VALDATE">
                                                </ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SAVE_ACCOUNT" runat="server" CssClass="ASPButton" Text="SAVE ACCOUNT" Width="100" OnClick="BT_SAVE_ACCOUNT_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_LIST" runat="server" style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr>
                            <td>
                                <div id="DV_LIST" runat="server">
                                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                                        <tr>
                                            <td>
                                                <asp:DataGrid ID="DGR_MEMBER" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="1" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_MEMBER_ItemCommand">
                                                    <EditItemStyle BackColor="#7C6F57" />
                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                                                    <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" Font-Size="XX-Small" />
                                                    <AlternatingItemStyle BackColor="White" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="MEMBER_TYPE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="MEMBERID" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="FULLNAME" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="HAS_REL" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ENABLE_DELETE" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="GENDER" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="AGE" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="RELATION" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ENABLE_EN" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ENABLE_COPY" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ENABLE_DUMMY" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DOB" Visible="false"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="MEMBER_TYPE_DESCR" HeaderText="STATUS">
                                                            <ItemStyle Width="130" />
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="FULLNAME">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBT_FULLNAME" runat="server" CommandName="Select"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="RELATION">
                                                            <ItemStyle Width="100" />
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DDL_RELATION" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_RELATION_SelectedIndexChanged" Visible="false"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="GENDER">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle Width="60" HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="LB_GENDER" runat="server"></asp:Label>
                                                                <asp:DropDownList ID="DDL_GENDER" runat="server" CssClass="ASPDropDownList" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="DDL_GENDER_SelectedIndexChanged">
                                                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                                                    <asp:ListItem Value="M" Text="MALE"></asp:ListItem>
                                                                    <asp:ListItem Value="F" Text="FEMALE"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="DOB">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="LB_DOB" runat="server"></asp:Label>
                                                                <asp:TextBox ID="TXT_DOB_DUMMY" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" OnTextChanged="TXT_DOB_DUMMY_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB_DUMMY">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="AGE">
                                                            <HeaderStyle HorizontalAlign="Center" Width="30" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="LB_AGE" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Button ID="BT_EDIT" runat="server" Text="E" CssClass="ASPButton" BackColor="Green" ForeColor="White" CommandName="Edit" ToolTip="Edit" Font-Size="6pt" />
                                                                            <asp:Button ID="BT_N" runat="server" Text="N" CssClass="ASPButton" BackColor="LightGreen" ForeColor="Green" CommandName="New" ToolTip="New" Font-Size="6pt" />
                                                                            <asp:Button ID="BT_C" runat="server" Text="C" CssClass="ASPButton" BackColor="Blue" ForeColor="White" CommandName="Copy" ToolTip="Copy" Font-Size="6pt" />
                                                                            <asp:Button ID="BT_I" runat="server" Text="I" CssClass="ASPButton" BackColor="LightBlue" ForeColor="DarkBlue" CommandName="Info" ToolTip="Other Info" Font-Size="6pt" />
                                                                            <asp:Button ID="BT_D" runat="server" Text="D" CssClass="ASPButton" BackColor="Gray" ForeColor="White" CommandName="Dummy" ToolTip="Dummy" Font-Size="6pt" />
                                                                            <asp:Button ID="BT_X" runat="server" Text="X" CssClass="ASPButton" BackColor="Red" ForeColor="White" CommandName="Delete" ToolTip="Remove" Font-Size="6pt" />

                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="MEMBERID" Visible="false"></asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <asp:Label ID="LB_MEMBERTYPE" runat="server" Visible="false"></asp:Label>
                                <div id="DV_SEARCH" runat="server" visible="false">
                                    <table style="border-spacing: 0px; width: 90%; font-size: x-small;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="LB_BACK" runat="server" ForeColor="Red" OnClick="LB_BACK_Click">Back ..</asp:LinkButton></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="TDBGColor">SEARCH EXISTING PERSON</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px;">FULLNAME</td>
                                            <td>
                                                <asp:TextBox ID="TXT_FULLNAME_EXISTING" runat="server" CssClass="ASPTextBox" Width="100%"></asp:TextBox></td>
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
                                            <td style="width: 130px;">ID NUMBER</td>
                                            <td>
                                                <asp:TextBox ID="TXT_IDNO_EXISTING" runat="server" CssClass="ASPTextBox" Width="100%" onkeypress="return CheckNumeric();"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_SEARCH_EXISTING" runat="server" Text="SEARCH" Width="100" CssClass="ASPTextBox" OnClick="BT_SEARCH_EXISTING_Click" /></td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="DGR_PERSON_EXISTING" runat="server" BackColor="White"
                                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                        CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" CssClass="ASPDatagrid" OnPageIndexChanged="DGR_PERSON_EXISTING_PageIndexChanged" OnItemCommand="DGR_PERSON_EXISTING_ItemCommand">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="False" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="XX-Small" Font-Strikeout="False" Font-Underline="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="FULLNAME">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LBT_SELECT_EXISTING" runat="server" CommandName="Select"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="DOB" HeaderText="DOB"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="GENDER" HeaderText="GENDER"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_NO" HeaderText="ID NUMBER"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle Width="30" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LBT_SELECT" runat="server" CommandName="Select"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                            Mode="NumericPages" />
                                    </asp:DataGrid>
                                </div>

                                <div id="DV_NEW" runat="server" visible="false">
                                    <table style="border-spacing: 0px; width: 90%; font-size: x-small;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="LB_NEW_BACK" runat="server" ForeColor="Red" OnClick="LB_NEW_BACK_Click">Back ..</asp:LinkButton></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td class="TDBGColor">CREATE NEW PERSON</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px;">FULLNAME</td>
                                            <td>
                                                <asp:TextBox ID="TXT_FULLNAME_ENTRY" runat="server" CssClass="ASPTextBox" Width="100%"></asp:TextBox></td>
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
                                                <asp:TextBox ID="TXT_DOB_ENTRY" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB_ENTRY">
                                                </ajaxToolkit:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 130px;">ID NUMBER</td>
                                            <td>
                                                <asp:TextBox ID="TXT_IDNO_ENTRY" runat="server" CssClass="ASPTextBox" Width="100%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_NEW_SAVE" runat="server" Text="SAVE" Width="100" CssClass="ASPTextBox" OnClick="BT_NEW_SAVE_Click" /></td>
                                        </tr>
                                    </table>
                                </div>

                                <div id="DV_MEMBER" runat="server" visible="false">
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td class="TDBGColor">
                                                <asp:Label ID="LB_MEMBER_TITLE" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="LB_MEMBER_BACK" runat="server" Text="Back .." ForeColor="Red" OnClick="LB_MEMBER_BACK_Click"></asp:LinkButton>
                                                <iframe id="IF_MEMBER" runat="server" style="width: 98%; height: 80vh; border-style: none;"></iframe>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>

                    <table id="TBL_ADDRESS" runat="server" visible="false" style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">ADDRESS TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_ADDTYPE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_ADDTYPE_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>ADDRESS</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ADDRESS" runat="server" MaxLength="255" Width="90%" TextMode="MultiLine" Height="60px" CssClass="ASPTextBoxUPPER"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PROVINCE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PROVINCE" runat="server" BackColor="Yellow" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_PROVINCE_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:TextBox ID="TXT_PROVINCE" runat="server" CssClass="ASPTextBoxUPPER" AutoPostBack="true" Width="30%" OnTextChanged="TXT_PROVINCE_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CITY</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_CITY" runat="server" BackColor="Yellow" CssClass="ASPDropDownList"></asp:DropDownList>
                                            <asp:TextBox ID="TXT_CITY" runat="server" CssClass="ASPTextBoxUPPER" AutoPostBack="true" Width="30%" OnTextChanged="TXT_CITY_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>COUNTRY</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_COUNTRY" runat="server" BackColor="Yellow" CssClass="ASPDropDownList"></asp:DropDownList>
                                            <asp:TextBox ID="TXT_COUNTRY" runat="server" CssClass="ASPTextBoxUPPER" AutoPostBack="true" Width="30%" OnTextChanged="TXT_COUNTRY_TextChanged"></asp:TextBox>
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
                                            <asp:DropDownList ID="DDL_PHONE_COUNTRY_1" runat="server" BackColor="Yellow" CssClass="ASPDropDownList"></asp:DropDownList>
                                            <asp:TextBox ID="TXT_PHONE1" runat="server" MaxLength="50" Width="75%" Style="background-color: yellow" CssClass="ASPTextBox" onkeypress="return CheckNumeric();"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PHONE 2</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PHONE_COUNTRY_2" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                            <asp:TextBox ID="TXT_PHONE2" runat="server" MaxLength="50" Width="75%" CssClass="ASPTextBox" onkeypress="return CheckNumeric();"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>EMAIL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_EMAIL" runat="server" MaxLength="100" Width="90%" Style="background-color: yellow" CssClass="ASPTextBox" onblur="return ValidateEmail(this);"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE_ADDRESS" runat="server" CssClass="ASPButton" Text="SAVE ADDRESS" Width="100" OnClick="BT_SAVE_ADDRESS_Click" />
                                            <asp:Label ID="LB_ERR_ADDRESS" runat="server" CssClass="ASPLabel" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>


                </td>
            </tr>

            <tr style="vertical-align: top;">
                <td style="width: 250px;">&nbsp;</td>
                <td><asp:Label ID="LB_WARNING" runat="server"></asp:Label></td>
            </tr>
        </table>

    </form>
</body>
</html>
