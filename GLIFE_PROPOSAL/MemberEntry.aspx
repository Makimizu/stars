<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="MemberEntry.aspx.cs" Inherits="GLIFE_PROPOSAL.MemberEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!-- Bootstrap core CSS-->
    <link href="include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom fonts for this template-->
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <!-- Page level plugin CSS-->
    <link href="include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="include/css/sb-admin.css" rel="stylesheet" />
    <link href="include/css/controls.css" rel="stylesheet" />

    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_QUOT" runat="server" Visible="false"></asp:Label>

        <div id="DV_ENTRY" runat="server">
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    <table style="border-spacing: 0px; width: 100%; font-size: small;">
                        <tr>
                            <td style="width: 150px;">FULL NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" MaxLength="255" Width="100%" Style="text-transform: uppercase;"></asp:TextBox>
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
                        <tr id = "trow" runat = "server">
                            <td>MOTHER MAIDEN NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_MMN" runat="server" MaxLength="255" Width="100%" Style="text-transform: uppercase;"></asp:TextBox>
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
                        <tr id = "trow2" runat = "server">
                            <td>PLACE OF BIRTH</td>
                            <td>
                                <asp:TextBox ID="TXT_POB" runat="server" MaxLength="255" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id = "trow3" runat = "server">
                            <td>JOB</td>
                            <td>
                                <asp:DropDownList ID="DDL_JOB" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id = "trow4" runat = "server">
                            <td>RELIGION</td>
                            <td>
                                <asp:DropDownList ID="DDL_RELIGION" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id = "trow5" runat = "server">
                            <td>MARITAL STATUS</td>
                            <td>
                                <asp:DropDownList ID="DDL_MARITAL" runat="server"></asp:DropDownList>
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
                                <asp:TextBox ID="TXT_IDNO" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id = "trow7" runat = "server">
                            <td>TAX NO.</td>
                            <td>
                                <asp:TextBox ID="TXT_TAXNO" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="col-xl-6 col-sm-6 mb-3">
                    <table style="border-spacing: 0px; width: 100%; font-size: small;">
                        <tr id = "trow8" runat = "server">
                            <td style="width: 150px;">CITIZENSHIP</td>
                            <td>
                                <asp:DropDownList ID="DDL_CITIZENSHIP" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id = "trow9" runat = "server">
                            <td>PHONE 1</td>
                            <td>
                                <asp:TextBox ID="TXT_PHONE1" runat="server" MaxLength="50" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id = "trow10" runat = "server">
                            <td>PHONE 2</td>
                            <td>
                                <asp:TextBox ID="TXT_PHONE2" runat="server" MaxLength="50" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr id = "trow11" runat = "server">
                            <td>EMAIL</td>
                            <td>
                                <asp:TextBox ID="TXT_EMAIL" runat="server" MaxLength="100" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr id = "trow12" runat = "server">
                            <td>ADDRESS 1</td>
                            <td>
                                <asp:TextBox ID="TXT_ADDRESS1" runat="server" MaxLength="255" Width="100%" TextMode="MultiLine" Height="60px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id = "trow13" runat = "server" style="vertical-align: top;">
                            <td>ADDRESS 2</td>
                            <td>
                                <asp:TextBox ID="TXT_ADDRESS2" runat="server" MaxLength="255" Width="100%" TextMode="MultiLine" Height="60px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id = "trow14" runat = "server">
                            <td>CITY</td>
                            <td>
                                <asp:TextBox ID="TXT_CITY" runat="server" MaxLength="50" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr id = "trow15" runat = "server">
                            <td>PROVINCE</td>
                            <td>
                                <asp:DropDownList ID="DDL_PROVINCE" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id = "trow16" runat = "server">
                            <td>COUNTRY</td>
                            <td>
                                <asp:DropDownList ID="DDL_COUNTRY" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id = "trow17" runat = "server">
                            <td>ZIP CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_ZIPCODE" runat="server" MaxLength="10" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TGL. TERIMA BERKAS</td>
                            <td>
                                <asp:TextBox ID="TXT_TRM_BERKAS" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TRM_BERKAS">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    <table style="border-spacing: 0px; font-size: small; width: 100%;">
                        <tr id = "trow18" runat = "server">
                            <td style="width: 150px;">BANK NAME</td>
                            <td>
                                <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="DropDownList" Width="100%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr id = "trow19" runat = "server">
                            <td>ACCOUNT NAME</td>
                            <td>
                                <asp:TextBox CssClass="TextBox" ID="TXT_ACCNAMA" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id = "trow20" runat = "server">
                            <td>ACCOUNT NUMBER</td>
                            <td>
                                <asp:TextBox CssClass="TextBox" ID="TXT_ACCNO" runat="server" MaxLength="100" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <center>
                    <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ButtonColor" OnClick="BT_SAVE_Click" Width="100px" />
                </center>
            <br />
        </div>
        <%-- ASWIN END --%>

        <div id="DV_ERROR" runat="server" visible="false">
            <table style="border-spacing: 0px; width: 100%;">
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
               <%-- <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td>FOUND SIMILARITIES&nbsp;:&nbsp;<asp:Label ID="LB_SIMCNT" runat="server"></asp:Label>

                        <br />
                        You can use existing similar records by clicking name on the list below :--%>
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3" style="width: auto; height: auto; overflow-y: scroll;">
                        <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical" OnItemCommand="DGR1_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False">
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
       <%-- </div>--%>

        <!--Plugins add by Ahmad Zulfahmi-->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
        <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
        <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

        <script>
            $('#<%=DDL_CITIZENSHIP.ClientID%>').select2({ width: "100%" });
            $('#<%=DDL_PROVINCE.ClientID%>').select2({ width: "100%" });
            $('#<%=DDL_COUNTRY.ClientID%>').select2({ width: "100%" });
            $('#<%=DDL_BANK.ClientID%>').select2({ width: "100%" });
        </script>
        <!--Plugins add by Ahmad Zulfahmi-->

    </form>
</asp:Content>
