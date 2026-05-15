<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Mobile/MobileParent.Master" CodeBehind="MobileAgreement.aspx.cs" Inherits="LQR.Mobile.MobileAgreement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link href="../include/css/controls.css" rel="stylesheet" />
    <script src="../include/js/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../include/js/jquery.jqscribble.js" type="text/javascript"></script>
    <script src="../include/js/jqscribble.extrabrushes.js" type="text/javascript"></script>
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

    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <div class="loading" align="center">
            <img src="../include/image/Loader_Chrome.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%; font-size: 9pt;">
            <tr>
                <td>
                    <asp:Button ID="BT_AGREEMENT" runat="server" Width="100%" Text="PERNYATAAN PEMEGANG POLIS / PESERTA" OnClick="BT_AGREEMENT_Click" />
                </td>
            </tr>
            <tr id="TR_AGREEMENT" runat="server">
                <td>
                    <center>
                    <div style="width: 98%; height: 60vh; overflow: auto;border-style:inset;background-color:lightyellow;">
                        Dengan selalu mengharap ridha dan ampunan Allah SWT, saya dengan ini menyatakan bahwa :<br /><br />
                        <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False"   GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%">
                            <ItemStyle VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundColumn DataField="SEQ">
                                    <ItemStyle Width="30" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="REMARK">
                                    <ItemStyle VerticalAlign="Top" />
                                </asp:BoundColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    </center>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_SIGN" runat="server" Width="100%" Text="TANDA TANGAN ANDA" OnClick="BT_SIGN_Click" />
                </td>
            </tr>
            <tr>
                <td id="TR_SIGN" runat="server" visible="false">
                    <table style="border-spacing: 0px; width: 100%; text-align: center; font-size: 9pt;">
                        <tr>
                            <td style="text-align: center;">
                                <asp:Image ID="IMG_SIGNATURE" runat="server" Width="80%" Visible="false" />
                                <div id="DV_CANVAS" runat="server" placeholder="Tanda tangan di sini ..">
                                    <canvas id="signature" style="width: 90%; height: 50vh; border-style: inset;"></canvas>
                                </div>
                                <b>CALON PEMEGANG POLIS</b><br />
                                <asp:Label ID="LB_NAME" runat="server"></asp:Label><br />
                                <asp:Label ID="LB_LOCATION" runat="server"></asp:Label>
                                <asp:TextBox ID="TXT_SIGNATURE" runat="server" Style="display: none"></asp:TextBox>
                                <asp:Label ID="LB_SIGNATURE" runat="server"></asp:Label>
                                <asp:Button ID="BT_APPROVE" runat="server" Text="SETUJU" Font-Size="Small" Width="100" Font-Bold="True" OnClick="BT_APPROVE_Click" CssClass="ButtonColor" OnClientClick="ShowProgress()" />
                                <asp:Button ID="BT_CLEAR" runat="server" Text="KOREKSI" Font-Size="Small" Width="100" Font-Bold="True" OnClientClick="return clearSignature();" CssClass="ButtonColor" BackColor="Yellow" ForeColor="Black" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#signature").jqScribble();
        });

        function getImageData() {
            console.log("getImageData called");
            $("#signature").data("jqScribble").save(function (imageData) {
                //console.log(window.$log = imageData);
                $('#TXT_SIGNATURE').val(imageData.toString());
            });
        }

        function clearSignature() {
            $("#signature").data("jqScribble").clear();

            return false;
        }
    </script>


</asp:Content>
