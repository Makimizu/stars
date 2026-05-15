<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationPremiumStatistic.aspx.cs" Inherits="LQ.Form_Finance.ApplicationPremiumStatistic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table id="TBL_STAT" runat="server" style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <center>
                                    <br />
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            <td>PAYMENT PERIOD</td>
                                            <td style="text-align:right;width:150px;">
                                                <asp:Label ID="LB_TENOR" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 150px;">LENGTH OF MEMBERSHIP</td>
                                            <td style="width: 100px;text-align:right;">
                                                <asp:Label ID="LB_LOM" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>REMAINING PAYMENT PERIOD</td>
                                            <td style="text-align:right; color:red;">
                                                <asp:Label ID="LB_REMAINING" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>ANNUAL PREMIUM</td>
                                            <td style="text-align:right; color:green;">
                                                <asp:Label ID="LB_ANNUAL_PREMIUM" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                </td>
                <td style="width: 50%;">
                    <center>
                                    <br />
                                    <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 200px;"></td>
                                        <td style="width: 100px;" class="TDBGColor">#TERM</td>
                                        <td style="width: 100px;" class="TDBGColor">AMOUNT</td>
                                    </tr>
                                    <tr>
                                        <td>MONTH TO DATE OVERDUE</td>
                                        <td style="text-align: center; color: blue;">
                                            <asp:Label ID="LB_MTD_TERM_EXPECTED" runat="server"></asp:Label></td>
                                        <td style="text-align: right; color: green;">
                                            <asp:Label ID="LB_MTD_AMOUNT_EXPECTED" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>MONTH TO DATE PAID</td>
                                        <td style="text-align: center; color: blue;">
                                            <asp:Label ID="LB_MTD_TERM_PAID" runat="server"></asp:Label></td>
                                        <td style="text-align: right; color: green;">
                                            <asp:Label ID="LB_MTD_AMOUNT_PAID" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr style="color: red;">
                                        <td>MONTH TO DATE OUTSTANDING</td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="LB_MTD_TERM_OUTSTANDING" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_MTD_AMOUNT_OUTSTANDING" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                        </td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>TOTAL EXPECTED</td>
                                        <td style="text-align: center; color: blue;">
                                            <asp:Label ID="LB_TOTAL_TERM_EXPECTED" runat="server"></asp:Label></td>
                                        <td style="text-align: right; color: green;">
                                            <asp:Label ID="LB_TOTAL_AMOUNT_EXPECTED" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>TOTAL PAID</td>
                                        <td style="text-align: center; color: blue;">
                                            <asp:Label ID="LB_TOTAL_TERM_PAID" runat="server"></asp:Label></td>
                                        <td style="text-align: right; color: green;">
                                            <asp:Label ID="LB_TOTAL_AMOUNT_PAID" runat="server"></asp:Label></td>
                                    </tr>
                                    <tr style="color: red;">
                                        <td>TOTAL INCOMPLETED</td>
                                        <td style="text-align: center;">
                                            <asp:Label ID="LB_TOTAL_TERM_INCOMPLETED" runat="server"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_TOTAL_AMOUNT_INCOMPLETED" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                </center>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>


