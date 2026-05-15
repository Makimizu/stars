<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationReinsurance.aspx.cs" Inherits="LQ.Form_App.ApplicationReinsurance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
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
        <div class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 98%;">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" GridLines="None" CellPadding="4" ForeColor="#333333" Enabled="False">
                        <HeaderStyle BackColor="#990000" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" Font-Size="XX-Small" BackColor="#FFFBD6" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGREEMENT_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REINS_SUMINS" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REINS_RATE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OWN_RETENTION_SELF" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OWN_RETENTION" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="QUOTA_SHARE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MEMBER_NAME" HeaderText="INSURED PERSON"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BENEFIT" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SUMINS" HeaderText="ORIGIN SUM INSURED<BR>REINS SUM INSURED">
                                <HeaderStyle HorizontalAlign="Right" Width="100" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="AGREEMENT">
                                <HeaderStyle Width="250" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_AGREEMENT" runat="server" CssClass="ASPDropDownList" Width="98%"></asp:DropDownList><br />
                                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small; color: green;">
                                        <tr>
                                            <td style="width: 100px;">OWN RETENTION</td>
                                            <td>:&nbsp;
                                                <asp:Label ID="LB_OR" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>% QUOTA SHARE</td>
                                            <td>:&nbsp;
                                                <asp:Label ID="LB_QS" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="200" />
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td style="width: 100px;">REINS SUM INSURED</td>
                                            <td>
                                                <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="95%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>OWN RETENTION</td>
                                            <td>
                                                <asp:TextBox ID="TXT_OR" runat="server" CssClass="ASPTextBoxNumber" Width="95%" Enabled="false"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&permil; REINS RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE" runat="server" CssClass="ASPTextBoxNumber" Width="95%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <center>
        <asp:Label ID="LB_FACULTATIVE_ALERT" runat="server"></asp:Label>
        </center>
    </form>
</body>
</html>
