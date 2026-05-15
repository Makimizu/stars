<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementTopup.aspx.cs" Inherits="LIFE.Form_POS.EndorsementTopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script src="../Class/Global.js"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
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
        .auto-style1 {
            background-color: #DCDCDC;
            color: black;
            border: 2px groove #FFFFFF;
            text-align: center;
            width: 1030px;
        }
        .auto-style2 {
            width: 1030px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <div class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:DataGrid ID="DGR" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="70%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="FUND_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FUND_DESCR" HeaderText="FUND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CURRENCY_CODE">
                                <ItemStyle Width="40" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="TOPUP<BR>AMOUNT">
                                <HeaderStyle Width="80" HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_AMOUNT" runat="server" CssClass="ASPTextBoxNumber" Width="98%" onkeyup="FormatCurrency(this);"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CHARGE" HeaderText="% CHARGE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle Width="40" HorizontalAlign="Right" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT_INV" HeaderText="INVESTMENT<BR>AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle Width="80" HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>

                  <h4>SUMBER DANA</h4>

<asp:CheckBoxList ID="chkSumberDana" runat="server" 
    RepeatDirection="Horizontal" 
    RepeatColumns="2" 
    CellPadding="4" Width="62%">
    <asp:ListItem Value="Gaji">Gaji</asp:ListItem>
    <asp:ListItem Value="Bonus/Insentif/Komisi">Bonus / Insentif / Komisi</asp:ListItem>
    <asp:ListItem Value="Tabungan">Tabungan</asp:ListItem>
    <asp:ListItem Value="Bisnis Pribadi">Bisnis Pribadi</asp:ListItem>
    <asp:ListItem Value="Lainnya">Lainnya</asp:ListItem>
</asp:CheckBoxList>

                     <b><b>Penghasilan per tahun dan pengahasilan tambahan atau tunjangan  tetap per tahun dan  <br>sumber pengahasilan pembayar/ kontribusi Top Pup (apabila penghasilan yg diperoleh dalam mata uang asing, maka <br>yg tercantum adalah ekuivalen rupiah menggunakan kurs BI pada saat pengisian formulir ini) :</b>
                    <asp:RadioButtonList ID="rblPendapatan" runat="server" RepeatDirection="Vertical" Width="763px">
                        <asp:ListItem Value="s/d Rp 60 juta per-tahun">s/d Rp 60 juta per-tahun</asp:ListItem>
                        <asp:ListItem Value="Rp 60 juta s/d Rp 120 juta per-tahun">Rp 60 juta s/d Rp 120 juta per-tahun</asp:ListItem>
                        <asp:ListItem Value="Rp 120 juta s/d Rp 240 juta per-tahun">Rp 120 juta s/d Rp 240 juta per-tahun</asp:ListItem>
                        <asp:ListItem Value="> Rp 240 juta per-tahun">> Rp 240 juta per-tahun</asp:ListItem>
                    </asp:RadioButtonList>

                    <br />
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>

                    <br /><br />

                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_Click" OnClientClick="ShowProgress()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
