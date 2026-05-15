<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormCNCRDetail.aspx.cs" Inherits="GLIFE.Form_Policy.FormCNCRDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type = "text/javascript">
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

        function alertMessage() {
            alert('Data sudah paling atas');
        }

        function alertMessage2() {
            alert('Data sudah paling bawah');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
    <div>
    <table id="TBL_KP" runat="server" style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor"><asp:Label ID="lblGroupName" runat="server" Font-Size="12px" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblStartDate" runat="server" Width="50%" Font-Size="12px" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblText" runat="server" Visible="false" Width="50%"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GV_KP" ShowHeaderWhenEmpty="True" runat="server" AutoGenerateColumns="False" CellPadding="3" Width="100%" BackColor="White" 
                        BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
                        GridLines="None" OnRowCommand="GV_KP_RowCommand" CellSpacing="1">
                        <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                        <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                        <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#594B9C" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#33276A" />
                        
                        <Columns>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                        <asp:Label ID="lblAutoNo" runat="server" Text='<%# Bind("AUTO_NO") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="NO" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                        <asp:Label ID="lblNo" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" Visible="false" HeaderText="NO URUT" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                        <asp:Label ID="lblNoUrut" runat="server" Text='<%# Bind("NO_URUT") %>'></asp:Label>
                                </ItemTemplate>


                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="ID" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblPolicyID" Text='<%# Bind("POLICY_ID") %>' ></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="START DATE" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblStartdate" Text='<%# Bind("START_DATE") %>' ></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="HEADER" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblisHeader" Text='<%# Bind("IS_HEADER") %>' ></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="DESCRIPTION" ItemStyle-Width="80%" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDesc" Text='<%# Bind("DESCRIPTION") %>'></asp:Label>
                                </ItemTemplate>

                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>



                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="ACTIONS" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="editx" CommandArgument="<%# Container.DataItemIndex %>"><asp:Image ID="imgEdit" Width="15" runat="server" ImageUrl="~/icons8-edit-file-24.png" ToolTip="Edit Data" /></asp:LinkButton>
                                    <asp:LinkButton ID="lnkUpArrow" runat="server" CommandName="upKP" CommandArgument="<%# Container.DataItemIndex %>"><asp:Image ID="imgUpArrow" Width="15" runat="server" ImageUrl="~/icons8-thick-arrow-pointing-up-24.png" ToolTip="Move Up" /></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDownArrow" runat="server" CommandName="downKP" CommandArgument="<%# Container.DataItemIndex %>"><asp:Image ID="imgDownArrow" Width="15" runat="server" ImageUrl="~/icons8-thick-arrow-pointing-down-24.png" ToolTip="Move Down" /></asp:LinkButton>
                                    <asp:LinkButton ID="lnkDelete" runat="server" OnClientClick="Confirm()" CommandName="deletex" CommandArgument="<%# Container.DataItemIndex %>"><asp:Image Width="15" ID="imgDelete" runat="server" ImageUrl="~/deleteicon.png" ToolTip="Delete Data" /></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>


                            </asp:TemplateField>

                        </Columns>
                    <EmptyDataTemplate>  
                    <div align="center">No Records Found.</div>
                                  
                    </EmptyDataTemplate>  
                         <HeaderStyle BorderWidth="1" />
                    </asp:GridView>
                    <br />
                </td>
            </tr>
        </table>
        <div id="dvAddNew" runat="server">
            <table>
                <tr>
                    <td><asp:TextBox ID="txtNoUrut" runat="server" Visible="false" CssClass="ASPTextBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>POLICY ID</td>
                    <td><asp:TextBox ID="txtPolKP" runat="server" CssClass="ASPTextBox" ReadOnly="true" Enabled="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>START DATE</td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="ASPTextBox" Width="80px"  ReadOnly="true" Enabled="false" Style="text-align: center;"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  runat="server" Format="dd/MM/yyyy" TargetControlID="txtDate">
                        </ajaxToolkit:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>KATEGORI</td>
                    <td><asp:TextBox ID="txtKTKP" runat="server" CssClass="ASPTextBox" ReadOnly="true" Enabled="false" Width="230px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>SET AS HEADER</td>
                    <td><asp:CheckBox ID="chkHeadKP" runat="server"/></td>
                </tr>
                <tr>
                    <td>DESCRIPTION</td>
                    <td><asp:TextBox ID="txtDescKP" runat="server" CssClass="ASPTextBox" TextMode="MultiLine" Rows="5" Width="700px" >
                    </asp:TextBox></td>
                    <td><asp:Label ID="lblError" runat="server"></asp:Label></td>
                </tr>
                
            </table>
        </div>
        
        <div>
            <table>
                <tr>
                    <td><asp:Button ID="btnAdd" runat="server" CssClass="ASPButton" Text="ADD" Width="100px" OnClick="btnAdd_Click"/></td>
                    <td><asp:Button ID="btnBack" runat="server" CssClass="ASPButton" Text="BACK" Width="100px" OnClick="btnBack_Click"/></td>
                    <td><asp:Button ID="btnSave" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="btnSave_Click" Visible="false" /> </td>
                    <td><asp:Button ID="btnCancel" runat="server" CssClass="ASPButton" Text="CANCEL" Width="100px" Visible="false" OnClick="btnCancel_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
