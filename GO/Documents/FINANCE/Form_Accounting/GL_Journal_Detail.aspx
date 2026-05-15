<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GL_Journal_Detail.aspx.cs" Inherits="FINANCE.Form_Accounting.GL_Journal_Detail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">CODE</td>
                                        <td>
                                            <asp:Label ID="LB_CODE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DESCRIPTION</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" Width="400px" BackColor="#FFFF66" Font-Bold="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SQL PARAM KEY</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PARAMKEY" runat="server" CssClass="ASPTextBox" Width="200px" MaxLength="100" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SQL PARAM DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PARAMDATE" runat="server" CssClass="ASPTextBox" Width="200px" MaxLength="30" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SQL DESCR</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PARAMDESCR" runat="server" CssClass="ASPTextBox" Width="200px" MaxLength="100" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>SQL FROM</td>
                                        <td>
                                            <asp:TextBox ID="TXT_SQLFROM" runat="server" CssClass="ASPTextBox" TextMode="MultiLine" Width="400px" Height="71px" MaxLength="1000" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    </table>

                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SAVE" Font-Bold="True" />
                                        <br />

                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 70px;">BASIC_AMT</td>
                                        <td>
                                            <asp:TextBox ID="TXT_BAMT" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RSV_AMT00</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RAMT00" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RSV_AMT01</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RAMT01" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RSV_AMT02</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RAMT02" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RSV_AMT03</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RAMT03" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RSV_AMT04</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RAMT04" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RSV_AMT05</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RAMT05" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RSV_AMT06</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RAMT06" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RSV_AMT07</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RAMT07" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RSV_AMT08</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RAMT08" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RSV_AMT09</td>
                                        <td>
                                            <asp:TextBox ID="TXT_RAMT09" runat="server" CssClass="ASPTextBox" Width="200px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 20px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 30px;">T00</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PT00" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>T01</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PT01" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>T02</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PT02" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>T03</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PT03" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>T04</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PT04" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>T05</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PT05" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>T06</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PT06" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>T07</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PT07" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>T08</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PT08" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>T09</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PT09" runat="server" CssClass="ASPTextBox" Width="60px" BackColor="Silver"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>

                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                </td>
            </tr>
            <tr>
                <td><strong>DEBET<br />
                </strong>
                    <asp:DropDownList ID="DDL_COA_D" runat="server" BackColor="#FFFF66" CssClass="ASPDropDownList">
                    </asp:DropDownList>
                    <asp:Button ID="BT_D_ADD" runat="server" BackColor="#FF3300" CssClass="ASPButton" Font-Bold="True" ForeColor="#FFFFCC" Text="+" OnClick="BT_D_ADD_Click" />
                    &nbsp;<asp:DataGrid ID="DGR_DEBET" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
                        PageSize="40" ItemStyle-Wrap="false" AutoGenerateColumns="False" ForeColor="#333333" OnItemCommand="DGR_DEBET_ItemCommand">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="false" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#006600" Font-Bold="True" ForeColor="Lime" Wrap="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="COA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="COA">
                                <HeaderStyle Width="300px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T00" HeaderText="T00" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T01" HeaderText="T01" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T02" HeaderText="T02" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T03" HeaderText="T03" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T04" HeaderText="T04" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T05" HeaderText="T05" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T06" HeaderText="T06" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T07" HeaderText="T07" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T08" HeaderText="T08" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T09" HeaderText="T09" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_AMOUNT" HeaderText="SQL AMOUNT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="T00">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T00" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Lime"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T01">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T01" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Lime"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T02">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T02" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Lime"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T03">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T03" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Lime"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T04">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T04" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Lime"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T05">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T05" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Lime"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T06">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T06" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Lime"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T07">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T07" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Lime"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T08">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T08" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Lime"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T09">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T09" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Lime"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F. AMOUNT">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_SQLAMOUNT" runat="server" BackColor="Lime" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DELETE" runat="server" CommandName="Delete" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr>
                <td class="auto-style1"><strong>
                    <br />
                    CREDIT</strong>
                    <br />
                    <asp:DropDownList ID="DDL_COA_C" runat="server" BackColor="#FFFF66" CssClass="ASPDropDownList">
                    </asp:DropDownList>
                    <asp:Button ID="BT_C_ADD" runat="server" BackColor="#FF3300" CssClass="ASPButton" Font-Bold="True" ForeColor="#FFFFCC" Text="+" OnClick="BT_C_ADD_Click" />
                    <asp:DataGrid ID="DGR_CREDIT" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" GridLines="None"
                        PageSize="40" ItemStyle-Wrap="false" AutoGenerateColumns="False" ForeColor="#333333" OnItemCommand="DGR_CREDIT_ItemCommand">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="false" VerticalAlign="Top" />
                        <HeaderStyle BackColor="Blue" Font-Bold="True" ForeColor="Aqua" Wrap="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="COA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="COA">
                                <HeaderStyle Width="300px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T00" HeaderText="T00" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T01" HeaderText="T01" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T02" HeaderText="T02" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T03" HeaderText="T03" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T04" HeaderText="T04" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T05" HeaderText="T05" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T06" HeaderText="T06" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T07" HeaderText="T07" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T08" HeaderText="T08" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_T09" HeaderText="T09" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_AMOUNT" HeaderText="SQL AMOUNT" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="T00">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T00" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Aqua"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T01">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T01" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Aqua"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T02">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T02" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Aqua"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T03">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T03" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Aqua"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T04">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T04" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Aqua"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T05">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T05" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Aqua"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T06">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T06" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Aqua"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T07">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T07" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Aqua"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T08">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T08" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Aqua"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="T09">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_T09" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="40px" BackColor="Aqua"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F. AMOUNT">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_SQLAMOUNT" runat="server" BackColor="Lime" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_DELETE" runat="server" CommandName="Delete" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" Text="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
