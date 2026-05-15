<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Param_Premium_Rate.aspx.cs" Inherits="UWBOX.Form_Parameter.Param_Premium_Rate" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function Submit() {
            form1.submit();
            ShowProgress();
        }

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
        $('form1').live("submit", function () {
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
    </style>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div class="loading" align="center">
            <img src="../Standard/image/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">SEARCH</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 150px;">PRODUCT GROUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_PRODUCT_GROUP_SEARCH" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_PRODUCT_GROUP_SEARCH_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>TABLE&nbsp; NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_SEARCH" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnTextChanged="TXT_SEARCH_TextChanged" Width="100%"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="LB_RECORD" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <div style="width: 100%; height: 100px; overflow: auto; border: inset;">
                        <asp:DataGrid ID="DGR_LIST" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" OnItemCommand="DGR_LIST_ItemCommand" BackColor="White" BorderColor="#E7E7FF" BorderWidth="1px" CellPadding="1" GridLines="None" Width="100%" BorderStyle="None">
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LB_ID" runat="server" CommandName="Detail"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CODE" ItemStyle-Width="200" Visible="False">
                                    <ItemStyle Width="200px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemStyle Width="60" />
                                    <ItemTemplate>
                                        <asp:Button ID="BT_DELETE" runat="server" Text="X" BackColor="Red" ForeColor="White" CssClass="ASPButton" CommandName="Delete" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle Wrap="False"
                                HorizontalAlign="Center" BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle PageButtonCount="5" BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                            <SelectedItemStyle BackColor="#738A9C" ForeColor="#F7F7F7" Font-Bold="True" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">TABLE INFO</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr id="TR_NEW" runat="server" visible="false">
                            <td></td>
                            <td>
                                <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW" Width="100px" OnClick="BT_NEW_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px;">CODE</td>
                            <td>
                                <asp:Label ID="LB_CODE" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>MATRIX NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TENOR TYPE</td>
                            <td>
                                <asp:DropDownList ID="DDL_TENOR" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                                <asp:Label ID="tenorTMP" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>PRODUCT GROUP</td>
                            <td>
                                <asp:DropDownList ID="DDL_PRODUCT_GROUP" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" BackColor="Blue" ForeColor="White" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                                <asp:Button ID="BT_CLEAR" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="CLEAR" Width="100px" OnClick="BT_CLEAR_Click" Visible="False" />
                            </td>
                        </tr>
                    </table>
                    <table id="TBL_XLS" runat="server" visible="false" style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 150px;">DOWNLOAD TEMPLATE / DATA</td>
                            <td>
                                <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" Font-Bold="True" ForeColor="White" BackColor="Green" Visible="True" OnClick="BT_XLS_Click" /></td>
                        </tr>
                        <tr>
                            <td>UPLOAD DATA</td>
                            <td>
                                <asp:FileUpload ID="FU" runat="server" CssClass="ASPButton" onchange="javascript:Submit()" Visible="False" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>

                    <table id="TBL_DETAIL" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">DATA</td>
                        </tr>
                        <tr id="TR_DATA" runat="server">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 150px;">GENDER</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_GENDER" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_GENDER_SelectedIndexChanged">
                                                <asp:ListItem Value="M">MALE</asp:ListItem>
                                                <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                                <asp:ListItem Value="MS">MALE-SMOKER</asp:ListItem>
                                                <asp:ListItem Value="FS">FEMALE-SMOKER</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div style="width: 100%; height: 800px; overflow: auto;">
                                    <asp:DataGrid ID="DGR_RATE" runat="server" CssClass="ASPDatagrid" OnItemCommand="DGR_LIST_ItemCommand" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None">
                                        <AlternatingItemStyle BackColor="#DCDCDC" />
                                        <ItemStyle Wrap="False" HorizontalAlign="Center" BackColor="#EEEEEE" ForeColor="Black" />
                                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                        <HeaderStyle Wrap="False"
                                            HorizontalAlign="Center" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                        <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                                    </asp:DataGrid>
                                </div>
                            </td>
                        </tr>
                        <tr id="TR_DATA_HP" runat="server" visible="false">
                            <td>
                                <iframe id="IFHP" runat="server" style="width: 100%; height: 80vh;" name="I1"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

