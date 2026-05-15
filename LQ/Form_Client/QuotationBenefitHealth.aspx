<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationBenefitHealth.aspx.cs" Inherits="LQ.Form_Client.QuotationBenefitHealth" %>

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
    <form id="form2" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_ERR" runat="server" ForeColor="Red"></asp:Label>
        <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" GridLines="None" CellPadding="4" ForeColor="#333333" ShowFooter="True" OnItemDataBound="DGR_ItemDataBound">
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="Silver" ForeColor="Black" />
            <HeaderStyle BackColor="#1C5E55" ForeColor="White" />
            <ItemStyle Wrap="True" VerticalAlign="Top" Font-Size="XX-Small" BackColor="#E3EAEB" />
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="MEMBER_TYPE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="RELATION_CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="PACKAGE_ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="PACKAGE_SQL" Visible="False"></asp:BoundColumn>

                <asp:BoundColumn DataField="MEMBER_TYPE_DESCR" HeaderText="MEMBER TYPE"></asp:BoundColumn>
                <asp:BoundColumn DataField="FULLNAME" HeaderText="MEMBER NAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                    <HeaderStyle HorizontalAlign="Center" Width="60" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="START_AGE" HeaderText="AGE">
                    <HeaderStyle HorizontalAlign="Center" Width="40" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="GENDER" HeaderText="GENDER">
                    <HeaderStyle HorizontalAlign="Center" Width="30" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="PACKAGE">
                    <HeaderStyle Width="200" />
                    <ItemTemplate>
                        <asp:DropDownList ID="DDL_PACKAGE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                    <FooterTemplate>
                        TOTAL :&nbsp;
                    </FooterTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="PREMIUM" HeaderText="PREMIUM">
                    <HeaderStyle HorizontalAlign="Right" Width="80" />
                    <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                    <FooterStyle HorizontalAlign="Right" ForeColor="Blue" Font-Bold="true" />
                </asp:BoundColumn>
            </Columns>
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        </asp:DataGrid>

        <asp:Button ID="BT_SAVE_BENEFIT" runat="server" CssClass="ASPButton" Text="SAVE BENEFIT" Width="150px" OnClick="BT_SAVE_BENEFIT_Click" OnClientClick="ShowProgress()" />

    </form>
</body>
</html>
