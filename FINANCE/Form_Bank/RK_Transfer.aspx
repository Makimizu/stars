<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RK_Transfer.aspx.cs" Inherits="FINANCE.Form_Bank.RK_Transfer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <link href="../Standard/CustomStyle.css" type="text/css" rel="stylesheet">

    <style type="text/css">
    .auto-style1 {
        height: 22px;
    }
    </style>
    <script type="text/javascript" src="../Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.blockUI.js"></script>

    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });

        document.onload = function () {
            var state = document.readyState
            if (state == 'interactive') {
                ShowProgress();
            } else if (state == 'complete') {
                setTimeout(function () {
                    document.getElementById('interactive');
                    document.getElementById('DV_LOADING').style.visibility = "hidden";
                }, 1000);
            }
        }

        function hourglass() {
            document.body.style.cursor = "wait";
        }
    </script>
    <style type="text/css">
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
        .auto-style2 {
            height: 30px;
        }
        .auto-style3 {
            width: 997px;	
        }
        .auto-style4 {
            background-color: #DCDCDC;
            color: black;
            border: 2px groove #FFFFFF;
            text-align: center;
            width: 997px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="DV_LOADING" class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 1000px;">
            <tr>
                <td class="auto-style3">
                    <asp:DataGrid ID="DGR_INFO" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="500px">
                        <ItemStyle VerticalAlign="Top" BackColor="#CCFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE">
                                <ItemStyle Width="100px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR">
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td class="auto-style3"><br /></td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <strong>BENEFICIARY
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <table style="border-spacing: 0px;" class="ASPTableHeader">
                        <tr>
                            <td>SETTLEMENT TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_SETTLEMENTTYPE" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-right: 20px;">ACCOUNT MASTER</td>
                            <td>
                                <asp:DropDownList ID="DDL_ACCMASTER" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_ACCMASTER_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>ACC NO</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNO" CssClass="ASPTextBox" runat="server" Width="321px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ACC NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" Width="321px" MaxLength="255"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK</td>
                            <td>
                                <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>AMOUNT</td>
                            <td>
                                <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBox" Width="168px" MaxLength="255"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>DESCR</td>
                            <td>
                                <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="321px" MaxLength="1000" Height="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>T00</td>
                            <td>
                                <asp:DropDownList ID="DDL_T00" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                <asp:TextBox ID="TXT_ID" CssClass="ASPTextBox" runat="server" Visible="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SUBMIT" runat="server" CssClass="ASPButton" Text="SUBMIT" OnClick="BT_SUBMIT_Click" />
                          
                                <asp:Button ID="BT_INQUIRY" BackColor="Green" ForeColor="White" runat="server" CssClass="ASPButton" Text="INQUIRY" OnClick="BT_INQUIRY_Click" />                    
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <br />

                    <table>
                            <tr>
                            <td>

                                <div id="spanInquery" runat="server" visible="false">
                                    <table style="width:150%;">
                                        <tr>
                                            <td>DESTINATION NAME</td>
                                            <td>:</td>
                                            <td><asp:Label ID="lblDestName" runat="server" Font-Bold="true" /></td>
                                        </tr>
                                        <tr>
                                            <td>DESTINATION ACCOUNT NO</td>
                                            <td>:</td>
                                            <td><asp:Label ID="lblDestAccNo" runat="server" Font-Bold="true" /></td>
                                        </tr>
                                        <tr>
                                            <td>DESTINATION BANK</td>
                                            <td>:</td>
                                            <td><asp:Label ID="lblDestBank" runat="server" Font-Bold="true" /></td>
                                        </tr>

                                    </table>
                                </div>
                               

                            </td>
                            </tr>
                    </table>

                    <table>
                        <tr>
                           <td>
                               <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                                BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical"
                                ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowFooter="True" OnItemCommand="DGR_ItemCommand">
                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7"/>
                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="true" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                   <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="false" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TRXID" HeaderText="TRXID">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPE_SETTLEMENT_DESCR" HeaderText="SETTLEMENT TYPE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ACC_NO" HeaderText="ACC NO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="ACC_NAME" HeaderText="ACC NAME">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="AMOUNT_FORMATTED" HeaderText="AMOUNT">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="80px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="USERBY" HeaderText="CREATED BY">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="USERDATE_FORMATTED" HeaderText="CREATED DATE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="LAST_TRACK" HeaderText="LAST TRACK">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <ItemTemplate>
                                            <asp:Button ID="BT_DEL" CommandName="Delete" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" visible="false"/>
                                            <asp:Button ID="BT_EDIT" CommandName="Edit" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="Edit" visible="false"/>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                </asp:DataGrid>
                           </td>
                        </tr>
                    </table>


                </td>
            </tr>
        </table>
    </form>
</body>
</html>
