<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RK_Refund.aspx.cs" Inherits="FINANCE.Form_Bank.RK_Refund" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_INFO" runat="server" CssClass="ASPDatagrid" PageSize="15" ShowHeader="False" AutoGenerateColumns="False" Width="500px">
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
                    <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td><br /></td>
            </tr>
            <tr>
                <td class="TDBGColor">
                    <strong>BENEFICIARY
                    </strong>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 80px;">ACC NO</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" Width="321px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ACC NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" Width="321px" MaxLength="255"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK</td>
                            <td>
                                <asp:DropDownList ID="DDL_ACCBANK" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SUBMIT" runat="server" CssClass="ASPButton" Text="SUBMIT" OnClick="BT_SUBMIT_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
