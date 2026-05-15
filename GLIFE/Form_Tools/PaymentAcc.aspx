<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentAcc.aspx.cs" Inherits="GLIFE.Form_Tools.PaymentAcc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_DISABLE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">PAYABLE ACCOUNT
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 120px">ACCOUNT NO.</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ACCOUNT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK</td>
                            <td>
                                <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                           
                                <asp:Button ID="BT_INQUIRY" BackColor="Green" ForeColor="White" runat="server" CssClass="ASPButton" Text="INQUIRY" OnClick="BT_INQUIRY_Click" />
                            </td>
                        </tr>
                    </table>

                    <br />

                    <table>
                         <tr>
                            <td>

                                <span id="spanInquery" runat="server" visible="false">
                                  <table style="width:150%;">
                                      <tr>
                                          <td class="auto-style1">DESTINATION NAME</td>
                                          <td>:</td>
                                          <td class="auto-style2"><asp:Label ID="lblDestName" runat="server" Font-Bold="true" /></td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style1">DESTINATION ACCOUNT NO</td>
                                          <td>:</td>
                                          <td class="auto-style2"><asp:Label ID="lblDestAccNo" runat="server" Font-Bold="true" /></td>
                                      </tr>
                                      <tr>
                                          <td class="auto-style1">DESTINATION BANK</td>
                                          <td>:</td>
                                          <td class="auto-style2"><asp:Label ID="lblDestBank" runat="server" Font-Bold="true" /></td>
                                      </tr>

                                  </table>
                                </span>
                               

                            </td>
                          </tr>
                    </table>


                </td>
            </tr>
        </table>

    </form>
</body>
</html>
