<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TC.aspx.cs" Inherits="UWBOX.Form_TC.TC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="DV_LOADING" class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>

        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">CODE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CODE" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox" Font-Bold="True" ReadOnly="True" Width="150px"></asp:TextBox>
                                            <asp:Button ID="BT_ARCHIEVE" runat="server" Text="ARCHIEVE" CssClass="ASPButton" Width="100px" BackColor="Green" ForeColor="White" OnClick="BT_ARCHIEVE_Click" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT GROUP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PRODUCTGROUP" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>LICENCE NAME</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_LICENCENO" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>START DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" Width="60px" Style="text-align: center;" CssClass="ASPTextBox"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>END DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ENDDATE" runat="server" Width="60px" Style="text-align: center;" CssClass="ASPTextBox"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr id="TR_SAVE" runat="server">
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="100px" BackColor="Blue" ForeColor="White" OnClick="BT_SAVE_Click" OnClientClick="ShowProgress()" />
                                            <asp:Button ID="BT_APPROVE" runat="server" Text="APPROVE" CssClass="ASPButton" Width="100px" BackColor="Cyan" ForeColor="Blue" Visible="false" OnClick="BT_APPROVE_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;">
                                <asp:DataGrid ID="DGR_TRACK" runat="server" AutoGenerateColumns="False" CellPadding="4" Font-Names="Tahoma" Font-Size="XX-Small" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%" ForeColor="#333333">
                                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#FFFBD6" ForeColor="#333333" VerticalAlign="Top" Wrap="false" />
                                    <HeaderStyle BackColor="#990000" ForeColor="White" VerticalAlign="Top" Font-Bold="True" />
                                    <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TRACK"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TRACK_BY"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TRACK_DATE">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">BENEFIT</td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 50%;">
                                            <asp:Button ID="BT_BENEFIT_DETAILS" runat="server" CssClass="ASPButton" Width="100%" Text="BENEFIT DETAIL" OnClick="BT_BENEFIT_DETAILS_Click" /></td>
                                        <td style="width: 50%;">
                                            <asp:Button ID="BT_BENEFIT_MEMBERS" runat="server" CssClass="ASPButton" Width="100%" Text="BENEFIT MEMBER" OnClick="BT_BENEFIT_MEMBERS_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <div style="width: 100%; height: 300px; overflow: auto;">
                                    <asp:DataGrid ID="DGR_BENEFIT" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Names="Tahoma" Font-Size="XX-Small" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" OnItemCommand="DGR_BENEFIT_ItemCommand" Width="100%">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" Wrap="false" />
                                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCR">
                                                <ItemStyle Font-Bold="true" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="PREMIUM_RATE_TABLE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BENEFIT_PAYMENT_TYPE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BENEFIT_PCT" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="INSURED_MEMBER_TYPE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="RIDER" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="COINSURANCE" Visible="false"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <table style="border-spacing: 0px;">
                                                        <tr>
                                                            <td style="width: 100px;">RATE</td>
                                                            <td>
                                                                <asp:DropDownList ID="DDL_RATE" runat="server" CssClass="ASPDropDownList" Width="200" Enabled="false">
                                                                </asp:DropDownList>
                                                                <asp:Button ID="BT_RATE" runat="server" CssClass="ASPButton" BackColor="Green" ForeColor="White" Text="E" CommandName="Rate" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>BENEFIT TYPE</td>
                                                            <td>
                                                                <asp:DropDownList ID="DDL_CLAIM" runat="server" CssClass="ASPDropDownList">
                                                                </asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                            <td>RIDER</td>
                                                            <td>
                                                                <asp:CheckBox ID="CB_RIDER" runat="server" Text="YES" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td>% AMOUNT</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_PCT" runat="server" CssClass="ASPTextBoxNumber" Width="30"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>COINSURANCE</td>
                                                            <td>
                                                                <asp:Label ID="LB_COINS" runat="server"></asp:Label>&nbsp;
                                                                            <asp:Button ID="BT_COINS" runat="server" CssClass="ASPButton" BackColor="Green" ForeColor="White" Text="SET" Visible="false" CommandName="Coins" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    </asp:DataGrid>
                                </div>
                            </td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td class="TDBGColor">
                                            <asp:Label ID="LB_BENEFIT_TITLE" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div style="width: 100%; height: 300px; overflow: auto;">
                                    <asp:DataGrid ID="DGR_BENEFIT_DETAIL" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Names="Tahoma" Font-Size="XX-Small" GridLines="Horizontal" ItemStyle-Wrap="true" ShowHeader="False" Width="100%">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" />
                                        <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" VerticalAlign="Top" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundColumn DataField="BENEFIT_CODE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BENEFIT_DETAIL_CODE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BENEFIT_PCT" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="LAPSE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BENEFIT_MASTER" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BENEFIT_DETAIL" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle Wrap="true" Width="220" />
                                                <ItemTemplate>
                                                    <asp:Label ID="LB_BENEFIT" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle VerticalAlign="Top" Width="50" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_PCT" CssClass="ASPTextBox" Width="20" runat="server"></asp:TextBox>&nbsp;%
                                                                    <br />
                                                    <asp:CheckBox ID="CB_LAPSE" Text="LAPSE" CssClass="ASPTextBox" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    <asp:DataGrid ID="DGR_MEMBER_BENEFIT" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Names="Tahoma" Font-Size="XX-Small" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="True" Width="100%">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" Wrap="false" />
                                        <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" VerticalAlign="Top" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundColumn DataField="BENEFIT_CODE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MEMBER_TYPE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TAKEN" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BENEFIT_DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MEMBER_TYPE_DESCR" HeaderText="MEMBER TYPE"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle Width="30" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CB" runat="server"></asp:CheckBox>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="DGR_GROUP" runat="server" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
                        PageSize="20" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" Width="100%">
                        <ItemStyle Wrap="True" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td class="TDBGColor" style="text-align: left;">
                                                <asp:Label ID="LBL_GROUP" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="X-Small"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DataGrid ID="DGR_ITEM" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Names="Tahoma" Font-Size="XX-Small" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False">
                                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" />
                                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="TC_ITEM" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CB" runat="server" CssClass="ASPTextBox" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="DESCR">
                                                            <ItemStyle Width="200px" />
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="150px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle BackColor="Transparent" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="500px" Width="700px" Style="z-index: 111; background-color: White; position: fixed; left: 60px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                            <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <center>
                            <iframe id="ifClaim" runat="server" src=""  width="100%" height="450"></iframe>
                            </center>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Panel ID="pnlCoins" runat="server" BackColor="White" Height="300px" Width="500px" Style="z-index: 111; background-color: White; position: fixed; left: 60px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                            <asp:Label ID="LB_COINS_BENEFIT" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                            <asp:Label ID="LB_COINS_BENEFIT_CODE" runat="server" Visible="false"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_COINSCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlCoins').style.display = 'none';return false;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DDL_COINS" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            <asp:Button ID="BT_COINSADD" runat="server" CssClass="ASPButton" Text="ADD" OnClick="BT_COINSADD_Click" />
                            <asp:Label ID="LB_COINSERROR" runat="server" ForeColor="Red"></asp:Label>
                            <asp:DataGrid ID="DGR_COINS" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" OnItemCommand="DGR_COINS_ItemCommand" Width="100%">
                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" Wrap="True" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <Columns>
                                    <asp:BoundColumn DataField="COMPANY_CODE" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="Co-Insurance Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SHARE" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="LOADING" Visible="false"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="% Share">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TXT_COINSSHARE" runat="server" CssClass="ASPTextBox" Width="50px" Style="text-align: center;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="% Loading">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TXT_COINSLOADING" runat="server" CssClass="ASPTextBox" Width="50px" Style="text-align: center;"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            <asp:Button ID="BT_COINSSAVE" runat="server" CssClass="ASPButton" Text="SAVE" BackColor="Green" ForeColor="White" CommandName="Save" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Button ID="BT_COINSDEL" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" CommandName="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

        </div>
    </form>
</body>
</html>
