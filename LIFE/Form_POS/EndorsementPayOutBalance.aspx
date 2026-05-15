<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementPayOutBalance.aspx.cs" Inherits="LIFE.Form_POS.EndorsementPayOutBalance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script>
        function onlyNumber(evt) {
            var charCode = evt.which ? evt.which : evt.keyCode;

            // Allow: Backspace(8), Tab(9), Delete(46), Arrow keys
            if (charCode == 8 || charCode == 9 || charCode == 46 ||
                (charCode >= 37 && charCode <= 40)) {
                return true;
            }

            // Allow digits (0–9)
            if (charCode >= 48 && charCode <= 57) {
                return true;
            }

            return false;
        }</script>
</head>
<body>





    <form id="form1" runat="server">


    <asp:Panel ID="PNL_ADD_TRX" runat="server" CssClass="boxForm" Width="100%">
    <table style="width:100%;">
        <tr>
            <td class="TDBGColor" colspan="2">DETAIL SUSPEND
            </td>
        </tr>
        <tr>
        <td>Transaction Type</td>
        <td>
            <asp:DropDownList ID="DDL_TRX_TYPE" runat="server" Width="200px">
                <asp:ListItem Text="Kontribusi Pertama" Value="Kontribusi Pertama"></asp:ListItem>
                <asp:ListItem Text="Kontribusi Lanjutan" Value="Kontribusi Lanjutan"></asp:ListItem>
                <asp:ListItem Text="TOP UP Irregular" Value="TOP UP Irregular"></asp:ListItem>
            </asp:DropDownList>
        </td>
        </tr>
         <tr>     <td>Policy No / Application No</td>
            <td><asp:TextBox ID="TXT_POLICY_NO" runat="server" Width="180px"></asp:TextBox></td>   </tr>
        <tr>      <td>Amount</td>
            <td><asp:TextBox ID="TXT_AMOUNT" runat="server" Width="120px"
    onkeypress="return onlyNumber(event);" /></td>    </tr>
     <tr>
       <td> </td>      <td><asp:Button ID="BT_SAVE_TRX" runat="server" Text="SAVE" CssClass="btn" OnClick="BT_SAVE_TRX_Click"  /></td>
     </tr>
       
    </table>
</asp:Panel>

<hr />  

<asp:DataGrid ID="DGR_TRX_LIST" runat="server"  BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true"   CssClass="ASPDatagrid"
    OnItemCommand="DGR_TRX_LIST_ItemCommand" Width="98%">
        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
    <AlternatingItemStyle BackColor="#F7F7F7" />
    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
    <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
    <Columns>
        <asp:BoundColumn HeaderText="Transaction Type" DataField="TRX_TYPE">
            <ItemStyle Width="200px" />
            <HeaderStyle Width="200px" />
        </asp:BoundColumn>

        <asp:BoundColumn HeaderText="Policy / Application No" DataField="POLICY_NO">
            <ItemStyle Width="250px" />
            <HeaderStyle Width="250px" />
        </asp:BoundColumn>

        <asp:BoundColumn HeaderText="Amount" DataField="AMOUNT" DataFormatString="{0:N0}">
            <ItemStyle HorizontalAlign="Right" Width="120px" />
            <HeaderStyle Width="120px" />
        </asp:BoundColumn>

        <asp:ButtonColumn Text="Update" CommandName="Update">
            <ItemStyle Width="70px" HorizontalAlign="Center" />
        </asp:ButtonColumn>

        <asp:ButtonColumn Text="Delete" CommandName="Delete">
            <ItemStyle Width="70px" HorizontalAlign="Center" />
        </asp:ButtonColumn>
    </Columns>
</asp:DataGrid>


          



        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_POLICYNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_REMARK" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_STATUS" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_EDIT_TRX_TYPE" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_EDIT_POLICYNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_EDIT_AMOUNT" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td class="TDBGColor">TRANSACTION SUMMARY<br />
                    HISTORY</td>
            </tr>
            <tr>
                <td>

                    <asp:DataGrid ID="DGR_TRX" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="DC" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="TRANSACTION TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AMOUNT" HeaderText="AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>

                    <asp:DataGrid ID="DGR_TRX_UNITLINK" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="DC" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FUND" HeaderText="FUND"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRANS_TYPE" HeaderText="TRANSACTION TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="UNIT" HeaderText="UNIT">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
