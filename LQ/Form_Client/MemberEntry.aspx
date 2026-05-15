<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberEntry.aspx.cs" Inherits="LQ.Form_Client.MemberEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <%--<link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="../include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <link href="../include/css/sb-admin.css" rel="stylesheet" />
    <link href="../include/css/controls.css" rel="stylesheet" />--%>
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
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_QUOT" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_MEMBER_TYPE" runat="server" Visible="false"></asp:Label>

        <div id="DV_MEMBER" runat="server" visible="false">
            <center>
            <span style="color: red; font-size: xx-small;"><b>MEMBER INI SUDAH TERDAFTAR SEBAGAI PENERIMA MANFAAT DI APLIKASI LAIN :</b></span>
            <asp:DataGrid ID="DGR_MEMBER" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="Small" GridLines="None"
                PageSize="20" ItemStyle-Wrap="true" ForeColor="#333333">
                <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" Wrap="True" VerticalAlign="Top" Font-Size="Smaller" />
                <HeaderStyle BackColor="#990000" ForeColor="White" Font-Size="Smaller" />
                <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            </asp:DataGrid>
            <br />
            </center>
        </div>

        <div id="DV_ENTRY" runat="server">
            <table style="border-spacing: 0px; width: 90%; font-size: xx-small;">
                <tr>
                    <td style="width: 130px;">FULL NAME</td>
                    <td>
                        <asp:TextBox ID="TXT_NAME" runat="server" MaxLength="255" Width="90%" Style="text-transform: uppercase;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>GENDER</td>
                    <td>
                        <asp:DropDownList ID="DDL_SEX" runat="server">
                            <asp:ListItem Value="M">MALE</asp:ListItem>
                            <asp:ListItem Value="F">FEMALE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="trow" runat="server">
                    <td>MOTHER MAIDEN NAME</td>
                    <td>
                        <asp:TextBox ID="TXT_MMN" runat="server" MaxLength="255" Width="90%" Style="text-transform: uppercase; background-color: yellow" required="required"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>DATE OF BIRTH</td>
                    <td>
                        <asp:TextBox ID="TXT_DOB" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr id="trow2" runat="server">
                    <td>PLACE OF BIRTH</td>
                    <td>
                        <asp:TextBox ID="TXT_POB" runat="server" MaxLength="255" Width="90%" Style="background-color: yellow" CssClass="ASPTextBoxUPPER" required="required"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow3" runat="server" visible="false">
                    <td>JOB</td>
                    <td>
                        <asp:DropDownList ID="DDL_JOB" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="trow4" runat="server">
                    <td>RELIGION</td>
                    <td>
                        <asp:DropDownList ID="DDL_RELIGION" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="trow5" runat="server">
                    <td>MARITAL STATUS</td>
                    <td>
                        <asp:DropDownList ID="DDL_MARITAL" runat="server" BackColor="Yellow"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>ID TYPE</td>
                    <td>
                        <asp:DropDownList ID="DDL_IDTYPE" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>ID NO.</td>
                    <td>
                        <asp:TextBox ID="TXT_IDNO" runat="server" MaxLength="50" Width="90%"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow7" runat="server">
                    <td>TAX NO.</td>
                    <td>
                        <asp:TextBox ID="TXT_TAXNO" runat="server" MaxLength="50" Width="90%"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="border-spacing: 0px; width: 90%; font-size: xx-small;">
                <tr id="trow8" runat="server">
                    <td style="width: 130px;">CITIZENSHIP</td>
                    <td>
                        <asp:DropDownList ID="DDL_CITIZENSHIP" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="TXT_CITIZENSHIP" runat="server" CssClass="ASPTextBoxUPPER" AutoPostBack="true" Width="30%" OnTextChanged="TXT_CITIZENSHIP_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow21" runat="server">
                    <td style="width: 130px;">EDUCATION</td>
                    <td>
                        <asp:DropDownList ID="DDL_EDUCATION" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr id="trow9" runat="server">
                    <td>PHONE 1</td>
                    <td>
                        <asp:DropDownList ID="DDL_PHONE_COUNTRY_1" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="TXT_PHONE1" runat="server" MaxLength="50" Width="75%" onkeypress="return CheckNumeric();"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow10" runat="server">
                    <td>HANDPHONE</td>
                    <td>
                        <asp:DropDownList ID="DDL_PHONE_COUNTRY_2" runat="server"></asp:DropDownList>
                        <asp:TextBox ID="TXT_PHONE2" runat="server" MaxLength="50" Width="75%" Style="background-color: yellow" required="required" onkeypress="return CheckNumeric();"></asp:TextBox></td>
                </tr>
                <tr id="trow11" runat="server">
                    <td>EMAIL</td>
                    <td>
                        <asp:TextBox ID="TXT_EMAIL" runat="server" MaxLength="100" Width="90%" Style="background-color: yellow" required="required" onblur="return ValidateEmail(this);"></asp:TextBox></td>
                </tr>
                <tr id="trow12" runat="server" style="vertical-align: top;">
                    <td><%--ADDRESS 1--%>HOME ADDRESS</td>
                    <td>
                        <asp:TextBox ID="TXT_ADDRESS1" CssClass="ASPTextBoxUPPER" runat="server" MaxLength="255" Width="90%" TextMode="MultiLine" Height="60px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow13" runat="server" style="vertical-align: top;">
                    <td><%--ADDRESS 2--%>CORRESPONDENCE ADDRESS</td>
                    <td>
                        <asp:TextBox ID="TXT_ADDRESS2" CssClass="ASPTextBoxUPPER" runat="server" MaxLength="255" Width="90%" TextMode="MultiLine" Height="60px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow14" runat="server">
                    <td>CITY</td>
                    <td>
                        <asp:DropDownList ID="DDL_CITY" runat="server" BackColor="Yellow" CssClass="ASPDropDownList"></asp:DropDownList>
                        <asp:TextBox ID="TXT_CITY" runat="server" CssClass="ASPTextBoxUPPER" AutoPostBack="true" Width="30%" OnTextChanged="TXT_CITY_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow15" runat="server">
                    <td>PROVINCE</td>
                    <td>
                        <asp:DropDownList ID="DDL_PROVINCE" runat="server" BackColor="Yellow" OnSelectedIndexChanged="DDL_PROVINCE_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <asp:TextBox ID="TXT_PROVINCE" runat="server" CssClass="ASPTextBoxUPPER" AutoPostBack="true" Width="30%" OnTextChanged="TXT_PROVINCE_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow16" runat="server">
                    <td>COUNTRY</td>
                    <td>
                        <asp:DropDownList ID="DDL_COUNTRY" runat="server" BackColor="Yellow"></asp:DropDownList>
                        <asp:TextBox ID="TXT_COUNTRY" runat="server" CssClass="ASPTextBoxUPPER" AutoPostBack="true" Width="30%" OnTextChanged="TXT_COUNTRY_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow17" runat="server">
                    <td>ZIP CODE</td>
                    <td>
                        <asp:TextBox ID="TXT_ZIPCODE" runat="server" MaxLength="10" Width="90%" Style="background-color: yellow" required="required"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table style="border-spacing: 0px; font-size: x-small; width: 90%;">
                <tr id="trow18" runat="server">
                    <td style="width: 130px;">BANK NAME</td>
                    <td>
                        <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="DropDownList" Width="60%">
                        </asp:DropDownList>
                        <asp:TextBox ID="TXT_BANK" runat="server" CssClass="ASPTextBoxUPPER" AutoPostBack="true" Width="30%" OnTextChanged="TXT_BANK_TextChanged"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow19" runat="server">
                    <td>ACCOUNT NAME</td>
                    <td>
                        <asp:TextBox CssClass="ASPTextBoxUPPER" ID="TXT_ACCNAMA" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trow20" runat="server">
                    <td>ACCOUNT NUMBER</td>
                    <td>
                        <asp:TextBox CssClass="ASPTextBoxUPPER" ID="TXT_ACCNO" runat="server" MaxLength="100" Width="250px" onkeypress="return CheckNumeric();"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <center>
                    <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" OnClick="BT_SAVE_Click" Width="100px" />
                </center>
            <br />
        </div>

        <div id="DV_ERROR" runat="server" visible="false">
            <table style="border-spacing: 0px; width: 90%;">
                <tr>
                    <td>
                        <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="BT_ERRORCLOSE" runat="server" CssClass="ButtonColor" BackColor="Red" ForeColor="White" Text="BACK" Font-Bold="True" Width="100px" OnClick="BT_ERRORCLOSE_Click" />
                    </td>
                </tr>
            </table>
        </div>

        <div id="DV_SIMILARITY" runat="server" visible="false">
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    FOUND SIMILARITIES&nbsp;:&nbsp;<asp:Label ID="LB_SIMCNT" runat="server"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    You can use existing similar records by clicking name on the list below :
                </div>
            </div>
            <%-- <table style="border-spacing: 0px; width: 90%;">
                <tr>
                    <td>FOUND SIMILARITIES&nbsp;:&nbsp;<asp:Label ID="LB_SIMCNT" runat="server"></asp:Label>

                        <br />
                        You can use existing similar records by clicking name on the list below :--%>
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3" style="width: auto; height: auto; overflow-y: scroll;">
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical" OnItemCommand="DGR1_ItemCommand" Width="90%" ItemStyle-Wrap="true" AutoGenerateColumns="False">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="FULL NAME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_FULLNAME" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID_NO" HeaderText="ID CARD NO">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SEX" HeaderText="GENDER">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REMARK" HeaderText="REMARK">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    Or continue with your new entry by clicking continue button.
                </div>
            </div>
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    <asp:Button ID="BT_CONTINUE" runat="server" CssClass="ButtonColor" ForeColor="White" Text="CONTINUE" Font-Bold="True" Width="120px" OnClick="BT_CONTINUE_Click" />&nbsp;
                    <asp:Button ID="BT_SIMILARITYCLOSE" runat="server" CssClass="ButtonColor" BackColor="Red" ForeColor="White" Text="BACK" Font-Bold="True" Width="100px" OnClick="BT_SIMILARITYCLOSE_Click" />
                </div>
                <%--<br />
                        Or continue with your new entry by clicking continue button.</td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="BT_CONTINUE" runat="server" CssClass="ButtonColor" ForeColor="White" Text="CONTINUE" Font-Bold="True" Width="120px" OnClick="BT_CONTINUE_Click" />&nbsp;
                        <asp:Button ID="BT_SIMILARITYCLOSE" runat="server" CssClass="ButtonColor" BackColor="Red" ForeColor="White" Text="BACK" Font-Bold="True" Width="100px" OnClick="BT_SIMILARITYCLOSE_Click" />
                    </td>
                </tr>
            </table>--%>
            </div>
        </div>

    </form>
</body>
</html>
