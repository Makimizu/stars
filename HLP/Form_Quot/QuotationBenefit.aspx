<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationBenefit.aspx.cs" Inherits="HLP.Form_Quot.QuotationBenefit" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript" src="../Script/PleaseWait.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:Label ID="LB_QUOTNO" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_VER" runat="server" Visible="False"></asp:Label>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_SAVE_Click" Text="SAVE" Width="100px" /></td>
                            <td id="TD_UPLOAD" runat="server">
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_XL" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_XL_Click" Text="XLS" BackColor="Green" /></td>
                                        <td>
                                            <table style="border-spacing: 0px;">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="BT_UPLOAD" runat="server" OnClick="BT_UPLOAD_Click"
                                                            Text="UPLOAD XLS" CssClass="ASPButton" Font-Bold="True" ForeColor="White" BackColor="Blue" />
                                                    </td>
                                                    <td>
                                                        <input id="TXT_FILE_UPLOAD" runat="server" name="TXT_FILE_UPLOAD" type="file"
                                                            class="ASPTextBox" />&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">BENEFIT</td>
                            <td>
                                <asp:DropDownList ID="DDL_BENEFIT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_BENEFIT_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" ForeColor="Red" Font-Bold="true"></asp:Label>

                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#000099" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Size="X-Small" GridLines="Vertical"
                        PageSize="20" OnItemCommand="DGR_ItemCommand" AutoGenerateColumns="False" OnItemDataBound="DGR_ItemDataBound">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="False" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" BackColor="Red" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CODE" HeaderText="KODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE01" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE02" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE03" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE04" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE05" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE06" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE07" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE08" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE09" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE10" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE11" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE12" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE13" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE14" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE15" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE16" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE17" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE18" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE19" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE20" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE21" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE22" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE23" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE24" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE25" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PPCODE26" Visible="False"></asp:BoundColumn>

                            <asp:BoundColumn DataField="UP01" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP02" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP03" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP04" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP05" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP06" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP07" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP08" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP09" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP10" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP11" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP12" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP13" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP14" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP15" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP16" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP17" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP18" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP19" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP20" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP21" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP22" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP23" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP24" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP25" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UP26" Visible="False"></asp:BoundColumn>

                            <asp:BoundColumn DataField="RATE01" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE02" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE03" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE04" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE05" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE06" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE07" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE08" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE09" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE10" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE11" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE12" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE13" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE14" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE15" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE16" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE17" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE18" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE19" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE20" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE21" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE22" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE23" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE24" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE25" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE26" Visible="False"></asp:BoundColumn>

                            <asp:BoundColumn DataField="PREMIUM01" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM02" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM03" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM04" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM05" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM06" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM07" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM08" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM09" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM10" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM11" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM12" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM13" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM14" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM15" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM16" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM17" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM18" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM19" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM20" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM21" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM22" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM23" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM24" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM25" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PREMIUM26" Visible="False"></asp:BoundColumn>


                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE01" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE01" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE01" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP01" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM01" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM01" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE02" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE02" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE02" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP02" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM02" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM02" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE03" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE03" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE03" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP03" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM03" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM03" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE04" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE04" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE04" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP04" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM04" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM04" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE05" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE05" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE05" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP05" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM05" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM05" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE06" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE06" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE06" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP06" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM06" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM06" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE07" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE07" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE07" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP07" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM07" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM07" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE08" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE08" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE08" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP08" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM08" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM08" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE09" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE09" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE09" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP09" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM09" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM09" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE10" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE10" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE10" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP10" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM10" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM10" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE11" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE11" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE11" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP11" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM11" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM11" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE12" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE12" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE12" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP12" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM12" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM12" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE13" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE13" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE13" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP13" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM13" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM13" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE14" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE14" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE14" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP14" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM14" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM14" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE15" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE15" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE15" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP15" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM15" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM15" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE16" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE16" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE16" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP16" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM16" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM16" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE17" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE17" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE17" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP17" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM17" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM17" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE18" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE18" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE18" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP18" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM18" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM18" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE19" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE19" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE19" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP19" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM19" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM19" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE20" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE20" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE20" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP20" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM20" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM20" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE21" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE21" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE21" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP21" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM21" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM21" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE22" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE22" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE22" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP22" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM22" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM22" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE23" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE23" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE23" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP23" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM23" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM23" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE24" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE24" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE24" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP24" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM24" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM24" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE25" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE25" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE25" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP25" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM25" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM25" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <asp:Label ID="LB_PPCODE26" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; vertical-align: top;">
                                        <tr id="TR_DGR_RATE26" runat="server">
                                            <td>RATE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RATE26" BackColor="#ffff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>UP</td>
                                            <td>
                                                <asp:TextBox ID="TXT_UP26" BackColor="#99ff66" runat="server" CssClass="ASPTextBoxNumber" Width="100px"></asp:TextBox></td>
                                        </tr>
                                        <tr id="TR_DGR_PREMIUM26" runat="server">
                                            <td>PREMIUM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PREMIUM26" runat="server" CssClass="ASPTextBoxNumber" ReadOnly="true" Width="100px"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
