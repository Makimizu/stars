<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SavingMaturity.aspx.cs" Inherits="GLIFE.Form_Saving.SavingMaturity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 100px;">FULLNAME</td>
                            <td>
                                <asp:TextBox ID="TXT_NAME" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>POLICY NO</td>
                            <td>
                                <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>COMPANY</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PRODUCT</td>
                            <td>
                                <asp:TextBox ID="TXT_PRODUCT" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" /></td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="LB_RECORDS" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" PageSize="20" OnPageIndexChanged="DGR_PageIndexChanged" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" OnItemCommand="DGR_ItemCommand" AllowPaging="True">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Middle" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="CB_ALL" runat="server" AutoPostBack="True" OnCheckedChanged="CB_ALL_CheckedChanged" CommandName="All" />
                                    <asp:Button ID="BT_EXECUTE" runat="server" BackColor="Green" CommandName="Execute" CssClass="ASPButton" ForeColor="White" Text="V" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CB" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="POLICY_NO" HeaderText="POLICY NO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TC_DESCR" HeaderText="PRODUCT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PERIODE" HeaderText="PERIODE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE" HeaderText="BALANCE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" Font-Bold="true" />
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="300px" Width="60%" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 0px; border: outset 2px gray; padding: 5px; display: none">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td class="TDBGColor">
                            <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="BT_DETAILCLOSE" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="X" Font-Bold="True" OnClientClick="document.getElementById('pnlpopup').style.display = 'none';return false;" />
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
                                    <td>&nbsp;
                                    </td>
                                    <td>
                                        <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_SAVE_Click" />
                                     
                                        <asp:Button ID="BT_INQUIRY" BackColor="Green" ForeColor="White" runat="server" CssClass="ASPButton" Text="INQUIRY" OnClick="BT_INQUIRY_Click" />
                                        
                                        <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" ForeColor="Red" Font-Bold="True"></asp:Label>
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
            </asp:Panel>
        </div>

    </form>
</body>
</html>
