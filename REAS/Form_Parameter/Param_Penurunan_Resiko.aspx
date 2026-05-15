<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Param_Penurunan_Resiko.aspx.cs" Inherits="REAS.Form_Parameter.Param_Penurunan_Resiko" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="Head1">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 157px;
        }
    </style>
    <script type="text/javascript">
        function alertMessage(msg) {
            alert(msg);
        }
    </script>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure want to delete this data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <table style="width: 100%; left: 0px; top: 0px; border-spacing: 0px;">
            <tr>
                <td style="vertical-align: top; ">
                    <asp:TextBox ID="TXT_SEARCH" runat="server" AutoPostBack="True" CssClass="ASPTextBox" Width="300px" OnTextChanged="TXT_SEARCH_TextChanged"></asp:TextBox>
                    <p></p>
                    <asp:GridView ID="gvLists" runat="server" OnRowCommand="gvLists_RowCommand" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="308px">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="CODE" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblCode" runat="server" Text='<%# Bind("CODE") %>' CommandName="selectx" CommandArgument="<%# Container.DataItemIndex %>" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblDescr" runat="server" Text='<%# Bind("DESCR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="ACTION" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Button ID="BT_DELETE" runat="server" Text="X" BackColor="Red" ForeColor="White" CssClass="ASPButton" OnClientClick="Confirm()" CommandName="deletex" CommandArgument="<%# Container.DataItemIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </td>
                <td style="width: 30px;"></td>
                <td style="vertical-align: top; width: 100%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr id="TR_NEW" runat="server" visible="false">
                            <td class="auto-style1"></td>
                            <td>
                                <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW" Width="100px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">CODE</td>
                            <td>
                                <asp:Label ID="lblCodeRisk" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">PENURUNAN RESIKO NAME</td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">TABEL TAHUNAN</td>
                            <td>
                                <asp:FileUpload id="fuTblTahunan" runat="server" CssClass="ASPTextBox"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">TABEL BULANAN</td>
                            <td>
                                <asp:FileUpload id="fuTblBulanan" runat="server" CssClass="ASPTextBox"/>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="auto-style1"></td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="SAVE" BackColor="Blue" ForeColor="White" Width="100px" CssClass="ASPButton" OnClick="btnSave_Click" />
                                <asp:Button ID="btnClear" runat="server" Text="CLEAR" BackColor="RED" ForeColor="White" Width="100px" CssClass="ASPButton" OnClick="btnClear_Click" Visible="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td><asp:Label ID="lblError" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Medium"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblStatus" runat="server" Visible="false" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                <asp:GridView runat="server" ID="gvListTahunan" AutoGenerateColumns="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                </asp:GridView>
                                <p></p>
                                <asp:Label ID="lblStatus2" runat="server" Visible="false" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                <asp:GridView runat="server" ID="gvListBulanan" AutoGenerateColumns="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </td>
                        </tr>

                    </table>
                    <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                    <p></p>
                    
                    
                    <p></p>
                    

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
