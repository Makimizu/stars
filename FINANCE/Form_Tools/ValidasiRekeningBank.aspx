<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidasiRekeningBank.aspx.cs" Inherits="LIFE.Form_App.ValidasiRekeningBank" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet" />


    <script>



    </script>

    <style type="text/css">



    </style>

</head>
<body>
    <form id="form1" runat="server">
      
<%--        <div id="DV_LOADING" class="loading" align="center">
            <img src="../Standard/Loader.gif" style="border: 0; width: 100px;" alt="" />
        </div>--%>

        <asp:Label ID="LB_APP" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TIPE" runat="server" Visible="false"></asp:Label>


        <br /><br />

           <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
<%--            <tr>
                <td class="TDBGColor">VALIDASI REKENING BANK
                </td>
            </tr>--%>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width:30%; padding-left:10px;">
                        <tr>
                            <td style="width: 100px">ACCOUNT NO.</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNO" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="90%" onkeypress="return CheckNumeric();"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ACCOUNT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_ACCNAME" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>BANK</td>
                            <td>
                                <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList" Width="90%"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                               
                                <asp:Button ID="BT_INQUIRY" BackColor="Green" ForeColor="White" runat="server" CssClass="ASPButton" Text="INQUIRY" OnClick="BT_INQUIRY_Click"  />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <br />
                                <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" ForeColor="Red"></asp:Label>
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
