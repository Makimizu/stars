<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Approval.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Approval" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style2 {
            width: 100%;
        }
        .auto-style3 {
            width: 129px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:Label ID="LB_URL_REFERRER" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="LB_ID" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                <asp:Label ID="LB_TIPE" runat="server" Font-Bold="False" Visible="false"></asp:Label>
                                <asp:Label ID="LB_POLICY_PERIOD" runat="server" Font-Bold="False" Visible="false"></asp:Label>

                                <asp:DataGrid ID="DGR_INFO_COMPANY" runat="server" BorderStyle="None" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="300px">
                                    <ItemStyle VerticalAlign="Middle"/>
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
                                    <ItemStyle VerticalAlign="Middle" />
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
                            <td style="width:500px; vertical-align: top;">
                                <iframe id="I2" runat="server" frameborder="no" height="100px" name="I2" scrolling="auto"
                                    width="100%"></iframe>  
                                <asp:Button ID="BT_APPROVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Green" OnClick="BT_APPROVE_Click" Text="APPROVE" />
                                <asp:Button ID="BT_GOTO" runat="server" Font-Bold="true" Text="GO TO :" CssClass="ASPButton" OnClick="BT_GOTO_Click" BackColor="Red" />
                                <asp:DropDownList ID="DDL_GOTO" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="BT_PREVIEW" runat="server" Text="DATA PREVIEW" CssClass="ASPButton" ForeColor="Blue" Font-Bold="true" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="auto-style2">
                        <tr>
                            <td style="width: 33%;">
                                <asp:Button ID="BT_PROSES" runat="server" CssClass="ASPButton" Font-Bold="True" Text="PROSES" Width="100%" OnClick="BT_PROSES_Click" />
                            </td>
                            <td style="width: 33%;">
                                <asp:Button ID="BT_ARSIP" runat="server" CssClass="ASPButton" Font-Bold="True" Text="ARSIP" Width="100%" OnClick="BT_ARSIP_Click" />
                            </td>
                            <td style="width: 33%;">
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
