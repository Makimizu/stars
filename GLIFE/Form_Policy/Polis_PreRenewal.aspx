<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Polis_PreRenewal.aspx.cs" Inherits="GLIFE.Form_Policy.Polis_PreRenewal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    </script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">POLICY NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PERUSAHAAN</td>
                                        <td>
                                            <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NB/RN</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PREMIUM" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="0">NEW BUSINESS</asp:ListItem>
                                                <asp:ListItem Value="1">RENEWAL</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    
                                    <%--<tr>
                                        <td>PRODUCT</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PRODUCT" runat="server" CssClass="ASPTextBox" Width="90%"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                                    
                                    <tr>
                                        <td>TANGGAL JATUH TEMPO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CONFDATE1" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_CONFDATE1">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-&nbsp;
                                            <asp:TextBox ID="TXT_CONFDATE2" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_CONFDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TANGGAL RENEWAL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CONFDATE3" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_CONFDATE1">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-&nbsp;
                                            <asp:TextBox ID="TXT_CONFDATE4" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_CONFDATE2">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" Width="100px" OnClick="BT_SEARCH_Click" OnClientClick="ShowProgress()" />
                                            <asp:Label ID="LB_MODE" runat="server" Visible="False"></asp:Label>
                                            <asp:Label ID="LB_TRACK" runat="server" Visible="False"></asp:Label>
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
                    <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                    <asp:Label ID="LB_RECORD" runat="server" Text="" Font-Bold="true" CssClass="ASPLabel"></asp:Label>
                    <br />
                    <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" OnClick="BT_XLS_Click" BackColor="Green" Font-Bold="True" ForeColor="White" />
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="40" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333" OnItemCommand="DGR_ItemCommand">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="NO POLIS">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_ID" runat="server" CssClass="ASPLabel" CommandName="View"></asp:LinkButton>
                                    &nbsp;
                                    <asp:Button ID="BT_RESPOND" runat="server" BackColor="Green" CommandName="Respon" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="R" />
                                    <%--&nbsp;<asp:Button ID="BT_QUOTATION" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="Q" />--%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <%--<asp:BoundColumn DataField="POLICY_ID" Visible="False"></asp:BoundColumn>--%>
                            <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="AWAL PERIODE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="AKHIR PERIODE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NB/RN" HeaderText="NB/RN">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MOP" HeaderText="MOP">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MEMBER" HeaderText="MEMBER">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM BILLED" HeaderText="PREMIUM BILLED">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AGEN" HeaderText="AGEN"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CHANNEL DISTRIBUSI" HeaderText="CHANNEL DISTRIBUSI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TANGGAL JATUH TEMPO" HeaderText="TANGGAL JATUH TEMPO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="POLIS SEBELUMNYA" HeaderText="POLIS SEBELUMNYA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TANGGAL RENEWAL" HeaderText="TANGGAL RENEWAL">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="blue" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SEQ RENEWAL" HeaderText="SEQ RENEWAL"></asp:BoundColumn>
                            <%--<asp:BoundColumn DataField="RESERVE_BILLED" HeaderText="PREMIUM BILLED">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RESERVE_EARNED" HeaderText="PREMIUM EARNED">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LOADING" HeaderText="% LOADING">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#6600FF" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TABARRU" HeaderText="% TABARRU">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#666666" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="LOADING_AMT" HeaderText="LOADING AMT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#6600FF" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TABBARU_AMT" HeaderText="TABBARU AMT">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="#666666" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CLAIM_PAID" HeaderText="CLAIM">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="REFUND_PREMIUM" HeaderText="REFUND PREMIUM">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CLAIM_RATIO" HeaderText="% CR">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MOP" HeaderText="MOP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PRODUCT" HeaderText="PRODUCT">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TPA" HeaderText="TPA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SURPLUS" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CLAIM_RATIO_CALC" Visible="False"></asp:BoundColumn>--%>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="400px" Width="700px" Style="z-index: 111; background-color: White; position: fixed; left: 100px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
                            <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small" Text="POLICY PRERENEWAL RESPONDS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="LB_PERIOD" Visible="false"></asp:Label>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="vertical-align: top;">
                                    <td>
                                        <table style="border-spacing: 0px; width: 100%;">
                                            <tr>
                                                <td style="width: 100px;">NO POLIS</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="LB_POLICYNO" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>PERUSAHAAN</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="LB_COMPANY" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>PERIODE</td>
                                                <td>:</td>
                                                <td>
                                                    <asp:Label ID="LB_PERIODDATE" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="DGR_RESPOND" runat="server" BackColor="LightGoldenrodYellow" BorderColor="#996633"
                                BorderWidth="1px" CellPadding="2"
                                GridLines="Vertical" CssClass="ASPDatagrid"
                                AutoGenerateColumns="False" Width="100%" ForeColor="Black" AllowPaging="false" OnItemCommand="DGR_RESPOND_ItemCommand">
                                <ItemStyle Wrap="true" VerticalAlign="Top" BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <AlternatingItemStyle BackColor="PaleGoldenrod" VerticalAlign="Top" Wrap="true" />
                                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <HeaderStyle BackColor="Tan" Font-Bold="True" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundColumn DataField="POLICY_PERIOD_ID" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="UNIT_CODE" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DECISION" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="REMARK" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="COLOR" Visible="false"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="UNIT_DESCR" HeaderText="DEPARTMENT"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="REMARK" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DDL_DECISION" runat="server" CssClass="ASPDropDownList" BackColor="Yellow"></asp:DropDownList>
                                            <asp:TextBox runat="server" ID="TXT_REMARK" TextMode="MultiLine" CssClass="ASPTextBox" Width="400px" Height="30px" BackColor="Yellow"></asp:TextBox><br />
                                            <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" CommandName="Save" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="300px" />
                                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="USERBY" HeaderText="USER"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="USERDATE" HeaderText="DATE"></asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="border-spacing: 0px;">
                                <tr>
                                    <td style="width: 100px;">TABBARU</td>
                                    <td>:</td>
                                    <td style="text-align: right">
                                        <asp:Label ID="LB_TABBARU" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>CLAIM PAID</td>
                                    <td>:</td>
                                    <td style="text-align: right">
                                        <asp:Label ID="LB_CLAIM" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                </tr>
                                <%--<tr>
                                    <td>% CR DEFAULT</td>
                                    <td>:</td>
                                    <td style="text-align: right">
                                        <asp:Label ID="LB_CRVAL" runat="server" Text="" CssClass="ASPLabel" Font-Bold="true"></asp:Label></td>
                                </tr>--%>
                                <tr>
                                    <td>% CR</td>
                                    <td>:</td>
                                    <td style="text-align: right">
                                        <asp:TextBox ID="TXT_CRVAL" runat="server" CssClass="ASPTextBoxNumber" Width="80px" Font-Bold="true" BackColor="Yellow"></asp:TextBox></td>
                                </tr>
                               <%-- <tr>
                                    <td></td>
                                    <td></td>
                                    <td style="text-align: right">
                                        <asp:Button ID="BT_CRSAVE" runat="server" Text="SAVE" CssClass="ASPButton" OnClick="BT_CRSAVE_Click" /></td>
                                </tr>--%>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
