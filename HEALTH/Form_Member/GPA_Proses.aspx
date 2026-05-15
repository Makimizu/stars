<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Proses.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Proses" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    </head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; position: absolute; top: 0px; left: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="LB_URL_REFERRER" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="LB_ID" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                <asp:Label ID="LB_TIPE" runat="server" Font-Bold="False" Visible="false"></asp:Label>
                                <asp:Label ID="LB_POLICY_PERIOD" runat="server" Font-Bold="False" Visible="false"></asp:Label>

                                <asp:DataGrid ID="DGR_INFO_COMPANY" runat="server" BorderStyle="None" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="300px">
                                    <ItemStyle VerticalAlign="Middle" BackColor="#99FF33" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
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
                            </td>
                            <td style="vertical-align: top;">
                                <asp:DataGrid ID="DGR_INFO_BATCH" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="300px">
                                    <ItemStyle VerticalAlign="Middle" BackColor="#00CCFF" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle
                                        Wrap="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE">
                                            <ItemStyle Width="150px" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR">
                                            <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                            <td style="width: 500px; vertical-align: top;">
                                <iframe id="I2" runat="server" frameborder="no" height="100px" name="I2" scrolling="auto"
                                    width="100%"></iframe>
                                <asp:Button ID="BT_GOTO" runat="server" Text="GO TO :" CssClass="ASPButton" OnClick="BT_GOTO_Click" />
                                <asp:DropDownList ID="DDL_GOTO" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 20%;">
                                <asp:Button ID="BT_PROSES" runat="server" CssClass="ASPButton" Font-Bold="True" Text="PROSES" Width="100%" OnClick="BT_PROSES_Click" />
                            </td>
                            <td style="width: 20%;">
                                <asp:Button ID="BT_PLAN" runat="server" CssClass="ASPButton" Font-Bold="True" Text="POLICY PLAN" Width="100%" OnClick="BT_PLAN_Click"/>
                            </td>
                            <td style="width: 20%;">
                                <asp:Button ID="BT_REPORT" runat="server" CssClass="ASPButton" Font-Bold="True" Text="REPORT" Width="100%" OnClick="BT_REPORT_Click"/>
                            </td>
                            <td style="width: 20%;">
                                <asp:Button ID="BT_ARSIP" runat="server" CssClass="ASPButton" Font-Bold="True" Text="ARSIP" Width="100%" OnClick="BT_ARSIP_Click" />
                            </td>
                            <td style="width: 20%;">
                                <asp:Button ID="BT_REMARK" runat="server" CssClass="ASPButton" Font-Bold="True" Text="REMARK" Width="100%" OnClick="BT_REMARK_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>