<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaimHead.aspx.cs" Inherits="HEALTH.Form_Klaim.ClaimHead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB1" runat="server" CssClass="ASPLabel" Font-Bold="True">INFO BATCH</asp:Label></td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 80px;">
                                            <asp:Label ID="Label1" runat="server" CssClass="ASPLabel" Font-Bold="False">NOMOR SM</asp:Label></td>
                                        <td>
                                            <asp:Label ID="LB_SM" runat="server" CssClass="ASPLabel"></asp:Label>
                                            <asp:Label ID="LB_BATCHID" runat="server" Visible="False"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label2" runat="server" CssClass="ASPLabel" Font-Bold="False">P / R</asp:Label></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PR" runat="server" CssClass="ASPDropDownList" Enabled="False">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="Label3" runat="server" CssClass="ASPLabel" Font-Bold="False">TGL SM</asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="TXT_TGLSM" runat="server" CssClass="ASPTextBox" Width="80px" Enabled="False" Style="text-align: center;"></asp:TextBox>
                                            <asp:DropDownList ID="DDL_PROV" runat="server" CssClass="ASPDropDownList" Enabled="False" Visible="False">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DDL_POLIS" runat="server" CssClass="ASPDropDownList" Enabled="False" Visible="False">
                                            </asp:DropDownList>
                                            <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_NEW_Click" Text="BUAT PENGAJUAN BARU" Visible="False" />
                                            <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td class="TDBGColor">
                                <asp:Label ID="LB2" runat="server" CssClass="ASPLabel" Font-Bold="True">INFO PENGAJUAN</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <table style="border-spacing: 0px;">
                                                <tr>
                                                    <td style="width: 100px;">
                                                        <asp:Label ID="Label4" runat="server" CssClass="ASPLabel" Font-Bold="False">NOREG CLM</asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="LB_CLAIMNO" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox" Font-Bold="True" ReadOnly="True" Width="150px"></asp:TextBox>
                                                        <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" OnClick="BT_SAVE_Click" Text="SAVE" ForeColor="Blue" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label5" runat="server" CssClass="ASPLabel" Font-Bold="False">TRACK</asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_TRACK" runat="server" CssClass="ASPDropDownList" Enabled="False" BackColor="#CCCCCC">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label6" runat="server" CssClass="ASPLabel" Font-Bold="False">TIPE</asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList" Enabled="False" BackColor="#CCCCCC">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label7" runat="server" CssClass="ASPLabel" Font-Bold="False">TGL KLAIM</asp:Label></td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="TXT_TGLCLAIM" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TGLCLAIM">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label8" runat="server" CssClass="ASPLabel">
                                                            <asp:Label ID="Label9" runat="server" CssClass="ASPLabel">TGL RAWAT</asp:Label></asp:Label></td>
                                                    <td>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <asp:TextBox ID="TXT_TGLRAWATDARI" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TGLRAWATDARI">
                                                                </ajaxToolkit:CalendarExtender>
                                                                -
                                                    <asp:TextBox ID="TXT_TGLRAWATSAMPAI" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_TGLRAWATSAMPAI">
                                                                </ajaxToolkit:CalendarExtender>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="DGR_INFO_PESERTA" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="350px">
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
                                            <asp:Label ID="LB_REGNO" runat="server" Visible="False"></asp:Label>
                                            <asp:Button ID="BT_CARI1" runat="server" CssClass="ASPButton" OnClick="BT_CARI1_Click" Text="CARI PESERTA" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="border-spacing: 0px;">
                                                <tr id="TR_REK" runat="server" visible="true">
                                                    <td style="width: 100px;">
                                                        <asp:Button ID="BT_NOREK" runat="server" Text="COPY REK  :" CssClass="ASPLabel" OnClick="BT_NOREK_Click" Width="100%" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_NOREK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label10" runat="server" CssClass="ASPLabel">ACC NO</asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label11" runat="server" CssClass="ASPLabel">ACC NAMA</asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_ACCNAMA" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label12" runat="server" CssClass="ASPLabel">ACC BANK</asp:Label></td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="TR_REFUNDACC" runat="server" visible="false" style="background-color: yellow;">
                                        <td>
                                            <span class="auto-style1"><strong>REFUND ACCOUNT</strong></span><table style="border-spacing: 0px;">
                                                <tr>
                                                    <td style="width: 100px;">ACC NO</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_ACCNO_RFD" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ACC NAMA</td>
                                                    <td>
                                                        <asp:TextBox ID="TXT_ACCNAMA_RFD" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ACC BANK</td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_ACCBANK_RFD" runat="server" CssClass="ASPDropDownList">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">

                                <asp:DataGrid ID="DGR_INFO_PROVIDER" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="350px">
                                    <ItemStyle VerticalAlign="Top" BackColor="#FFFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
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
                                <asp:Label ID="LB_KODE_PROVIDER" runat="server" Visible="False"></asp:Label>
                                <asp:Button ID="BT_CARI2" runat="server" CssClass="ASPButton" OnClick="BT_CARI2_Click" Text="CARI PROVIDER" />


                            </td>
                        </tr>
                    </table>
                </td>
                <td id="TD_BUTTONS" runat="server" style="width: 120px;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="DIAGNOSA" Width="90%" OnClick="BT2_Click" Height="30px" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT3" runat="server" CssClass="ASPButton" Text="BENEFIT" Width="90%" OnClick="BT3_Click" CausesValidation="False" Height="30px" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT_LIMIT" runat="server" CssClass="ASPButton" Text="BENEFIT LIMIT" Width="90%" CausesValidation="False" Height="30px" Font-Bold="True" OnClick="BT_LIMIT_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT8" runat="server" CssClass="ASPButton" Text="SURG. CTGR" Width="90%" OnClick="BT8_Click" Height="30px" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT0" runat="server" CssClass="ASPButton" Text="ADD INFO" Width="90%" OnClick="BT0_Click" Height="30px" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT9" runat="server" CssClass="ASPButton" Text="REMARK" Width="90%" OnClick="BT9_Click" Height="30px" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="ARSIP" Width="90%" OnClick="BT1_Click" Height="30px" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT_ADD_REASON" runat="server" CssClass="ASPButton" Text="ADD REASON" Width="90%" OnClick="BT_ADD_REASON_Click" Height="30px" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT4" runat="server" CssClass="ASPButton" Text="TRACK" Width="90%" OnClick="BT4_Click" Height="30px" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT5" runat="server" CssClass="ASPButton" Text="REV HISTORY" Width="90%" OnClick="BT5_Click" Height="30px" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT_HP" runat="server" CssClass="ASPButton" Text="CLAIM HIST." Width="90%" OnClick="BT_HP_Click" Height="30px" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Button ID="BT_TC" runat="server" CssClass="ASPButton" Text="TC POLIS" Width="90%" Height="30px" Font-Bold="True" OnClick="BT_TC_Click" />
                            </td>
                        </tr>
                        <tr id="TR_DISCOUNT" runat="server">
                            <td style="text-align: center;">
                                <asp:Button ID="BT_DISCOUNT" runat="server" CssClass="ASPButton" Text="DISCOUNT" Width="90%" Height="30px" Font-Bold="True" OnClick="BT_DISCOUNT_Click" />
                            </td>
                        </tr>
                        <tr id="TR_PENDING" runat="server">
                            <td style="text-align: center;">
                                <asp:Button ID="BT6" runat="server" CssClass="ASPButton" Text="PENDING" Width="90%" OnClick="BT6_Click" BackColor="Yellow" Font-Bold="True" ForeColor="#FF6600" Height="30px" />
                            </td>
                        </tr>
                        <tr id="TR_REJECT" runat="server">
                            <td style="text-align: center;">
                                <asp:Button ID="BT7" runat="server" CssClass="ASPButton" Text="TOLAK" Width="90%" BackColor="Pink" Font-Bold="True" ForeColor="Red" OnClick="BT7_Click" Height="30px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlMEMBER" runat="server" BackColor="White" Height="400px" Width="500px" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="vertical-align:top;">
                                    <td>
                                        <table style="border-spacing: 0px;">
                                            <tr>
                                                <td style="font-weight: bold; width: 120px;">PERUSAHAAN</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_CARI_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold; width: 120px;">NO POLIS</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_CARI_NOPOL" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold; width: 120px;">NAMA PESERTA</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_CARI_PESERTA" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold; width: 120px;">NO PESERTA</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_CARI_REGNO" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BT_CARI_REGNO" runat="server" CssClass="ASPButton" Text="CARI" OnClick="BT_CARI_REGNO_Click" /></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button ID="BT_MEMBERCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" />
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="DGR_CARI_PESERTA" runat="server" CellPadding="4" CssClass="ASPDatagrid" GridLines="None"
                                OnItemCommand="DGR_CARI_PESERTA_ItemCommand" Width="500px" ForeColor="#333333" AllowPaging="True" OnPageIndexChanged="DGR_CARI_PESERTA_PageIndexChanged" PageSize="10" ShowHeader="False" AutoGenerateColumns="False">
                                <EditItemStyle BackColor="#7C6F57" />
                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#E3EAEB" Wrap="true" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                                    Wrap="False" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <Columns>
                                    <asp:ButtonColumn CommandName="Select" Text="Select">
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonColumn>
                                    <asp:BoundColumn DataField="REGNO" HeaderText="REGNO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NAMA" HeaderText="NAMA"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="PERUSAHAAN"></asp:BoundColumn>
                                </Columns>
                                <FooterStyle BackColor="Tan" />
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlPROVIDER" runat="server" BackColor="White" Height="400px" Width="500px" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 12%; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td>
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="vertical-align:top;">
                                    <td>
                                        <table style="border-spacing: 0px;">
                                            <tr>
                                                <td style="font-weight: bold; width: 120px;"><strong>NAMA PROVIDER</strong></td>
                                                <td class="auto-style4">
                                                    <asp:TextBox ID="TXT_NAMA_PROVIDER" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold; width: 120px;">STATUS</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_STAT_PROVIDER" runat="server" CssClass="ASPDropDownList">
                                                        <asp:ListItem Selected="True" Value="1">AKTIF</asp:ListItem>
                                                        <asp:ListItem Value="0">NON AKTIF</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style2">&nbsp;</td>
                                                <td>
                                                    <asp:Button ID="BT_CARI_PROVIDER" runat="server" CssClass="ASPButton" Text="CARI" OnClick="BT_CARI_PROVIDER_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button ID="BT_PROVIDERCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DataGrid ID="DGR_CARI_PROVIDER" runat="server" CellPadding="4" CssClass="ASPDatagrid" GridLines="None" AutoGenerateColumns="false"
                                OnItemCommand="DGR_CARI_PROVIDER_ItemCommand" Width="500px" ForeColor="#333333" AllowPaging="True" OnPageIndexChanged="DGR_CARI_PROVIDER_PageIndexChanged" PageSize="15" ShowHeader="False">
                                <EditItemStyle BackColor="#7C6F57" />
                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                                    Wrap="False" />
                                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                <Columns>
                                    <asp:ButtonColumn CommandName="Select" Text="Select">
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonColumn>
                                    <asp:BoundColumn DataField="KODE_PROVIDER" HeaderText="KODE"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NAMA" HeaderText="PROVIDER"></asp:BoundColumn>
                                </Columns>
                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>

