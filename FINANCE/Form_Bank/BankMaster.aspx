<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BankMaster.aspx.cs" Inherits="FINANCE.Form_Bank.BankMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        .style2
        {
            height: 24px;
        }
        .style3
        {
            width: 101px;
        }
        .style4
        {
            height: 24px;
            width: 101px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="position: absolute; top: 0px; left: 0px; border-spacing:0px;">
        <tr>
            <td>
                <table style="border-spacing:0px;">
                    <tr>
                        <td>
                            <table style="border-spacing:0px;">
                                <tr>
                                    <td class="style3">
                                        CODE
                                    </td>
                                    <td>
                                    <asp:TextBox ID="TXT_CODE" runat="server" Width="50px" CssClass="ASPTextBox" 
                                            BackColor="#BFBFBF" ForeColor="Black" ReadOnly="True"></asp:TextBox>
                                            &nbsp&nbsp&nbsp
                                        <asp:Button ID="BT_NEW" runat="server" Text="NEW" CssClass="ASPButton" 
                                            onclick="BT_NEW_Click"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        GROUP BANK
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DDL_GROUPBANK" runat="server" CssClass="ASPDropDownList">
                                        </asp:DropDownList>
                                        <%--<asp:TextBox ID="TextBox1" runat="server" Width="250px" CssClass="ASPTextBox"></asp:TextBox>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        NAMA BANK
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXT_NAMA_BANK" runat="server" Width="250px" CssClass="ASPTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        KOTA
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXT_KOTA" runat="server" Width="250px" CssClass="ASPTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        CABANG
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXT_CABANG" runat="server" Width="250px" CssClass="ASPTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style4">
                                        KODE CABANG
                                    </td>
                                    <td class="style2">
                                        <asp:TextBox ID="TXT_KODE_CABANG" runat="server" Width="140px" CssClass="ASPTextBox"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        CLEARING CODE
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXT_CLEARING_CODE" runat="server" Width="140px" CssClass="ASPTextBox"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3">
                                        RTGS CODE
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TXT_RTGS_CODE" runat="server" Width="140px" CssClass="ASPTextBox"
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style3" align="center">
                                        <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red" Text="Label"></asp:Label>
                                        <td>
                                            <asp:Button ID="BT_SUBMIT" runat="server" Text="SUBMIT" CssClass="ASPButton" OnClick="BT_SUBMIT_Click" />
                                        </td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td> <asp:Button ID="BTN_CARI" runat="server" Text="SEARCH" CssClass=ASPButton OnClick="BTN_CARI_Click" />
                <asp:TextBox ID="TXT_SEARCH" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
            <td>
        </tr>
        <tr>
            <td>
                <asp:DataGrid ID="DGR" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                    BorderWidth="1px" CellPadding="3" PageSize="20" OnPageIndexChanged="DGR_PageIndexChanged"
                    OnItemCommand="DGR_ItemCommand" AllowPaging="True" GridLines="Vertical" AutoGenerateColumns="False"
                    CssClass="ASPDatagrid">
                    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <AlternatingItemStyle BackColor="#DCDCDC" />
                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" Wrap="False" />
                    <Columns>
                        <asp:ButtonColumn CommandName="Select" Text="Select">
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:ButtonColumn>
                        <asp:BoundColumn DataField="CODE" HeaderText="CODE" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="GROUP BANK" HeaderText="GROUP BANK"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BANK" HeaderText="BANK"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CITY" HeaderText="KOTA"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BRANCH" HeaderText="CABANG"></asp:BoundColumn>
                        <asp:BoundColumn DataField="BRANCH_CODE" HeaderText="KODE CABANG"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CLEARING_CODE" HeaderText="CLEARING CODE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="RTGS_CODE" HeaderText="RTGS CODE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CODE" HeaderText="CODE" Visible="false"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" Wrap="False" />
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
