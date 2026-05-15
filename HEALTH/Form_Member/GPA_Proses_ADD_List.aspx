<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses_ADD_List.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Proses_ADD_List" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <<link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; position: absolute; top: 0px; left: 0px;">
            <tr>
                <td>
                    <asp:DropDownList ID="DDL_MODE" CssClass="ASPDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_MODE_SelectedIndexChanged">
                        <asp:ListItem Value="1">APPROVED</asp:ListItem>
                        <asp:ListItem Value="0">REJECTED</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="LB_BATCH_ID" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>                    
                </td>
            </tr>
            <tr id="TR_APPROVED" runat="server">
                <td>
                    <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" ForeColor="#333333" OnItemCommand="DGR_ItemCommand"
                        CssClass="ASPDatagrid" AutoGenerateColumns="False" GridLines="Vertical" OnItemDataBound="DGR_ItemDataBound" ShowFooter="True">
                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle ForeColor="#333333" BackColor="#C5BBAF" Font-Bold="true" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Button ID="BTN_HEADER_HAPUS" runat="server" BackColor="Red" CommandName="deleteAll" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Button ID="BTN_HAPUS" runat="server" Text="X" CommandName="delete" CssClass="ASPButton" BackColor="Red" Font-Bold="True" ForeColor="White" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REGNO" HeaderText="REGNO" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="REGNO_EMP" HeaderText="REGNO EMP" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA_CABANG" HeaderText="NAMA CABANG" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" HeaderText="NAMA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEX" HeaderText="SEX" ItemStyle-HorizontalAlign="Center" Visible="False">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FAMILY_GROUP" HeaderText="FAMILY GROUP" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VIP" HeaderText="VIP" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="USIA_REG" HeaderText="USIA REG" ItemStyle-HorizontalAlign="Center" Visible="False">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_TYPE" HeaderText="ID TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_NO" HeaderText="ID NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PHONE" HeaderText="PHONE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="EMAIL" HeaderText="EMAIL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TGL_MASUK" HeaderText="TGL MASUK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BANK" HeaderText="BANK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NO" HeaderText="ACC NO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACC_NAMA" HeaderText="ACC NAMA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PAKET" HeaderText="PAKET" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DATA PRIBADI">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr style="vertical-align: top;">
                                            <td>NAMA</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_NAMA" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>REGNO</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_REGNO" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>PESERTA UTAMA</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_EMPLOYEE" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>GRUP</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_GRUP" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>GENDER</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_GENDER" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>TGL LAHIR</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_DOB" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>USIA REG</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_AGE" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>
                                                <asp:Label ID="LB_TIPEID" runat="server"></asp:Label></td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_NOID" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="INFO POLIS">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr style="vertical-align: top;">
                                            <td>PAKET</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_TGLMASUK" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>TGL MASUK</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_PAKET" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>BRANCH</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_BRANCH" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="REKENING">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr style="vertical-align: top;">
                                            <td>BANK</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_BANK" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>ACC NO</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_ACCNO" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>NAMA</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_ACCNAMA" runat="server" Font-Bold="true"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CONTACT">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr style="vertical-align: top;">
                                            <td>TELEPON</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_PHONE" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                            <td>EMAIL</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="LB_EMAIL" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FULL_PREMIUM" HeaderText="NORMAL<BR>PREMIUM">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MASA" HeaderText="MASA<BR>KEPESERTAAN">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM" HeaderText="PREMIUM" Visible="False">
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PREMIUM">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_PREMIUM" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="EMPLOYEE" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>
            
            <tr id="TR_REJECT" runat="server" visible="false">
                <td>
                    <asp:Label ID="LB_REJECTED" runat="server" CssClass="ASPLabel" Font-Bold="true"></asp:Label>
                    <asp:DataGrid ID="DGR_REJECT" runat="server" BorderColor="#990000" CellPadding="4" ForeColor="#333333" OnItemCommand="DGR_ItemCommand"
                        CssClass="ASPDatagrid" GridLines="Vertical">
                        <ItemStyle VerticalAlign="Top" BackColor="#FFFBD6" Wrap="False" ForeColor="#333333" />
                        <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                        <SelectedItemStyle ForeColor="Navy" BackColor="#FFCC66" Font-Bold="true" />
                        <AlternatingItemStyle BackColor="White" />
                        <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#990000" Font-Bold="True" HorizontalAlign="Left" ForeColor="White" />
                    </asp:DataGrid>
                </td>
            </tr>
            
        </table>
    </form>
</body>
</html>
