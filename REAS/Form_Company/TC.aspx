<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TC.aspx.cs" Inherits="REAS.Form_Company.TC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">ID</td>
                                        <td>
                                            <asp:TextBox ID="TXT_CODE" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>COMPANY</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_COMPANY" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DESCRIPTION</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DESCR" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DOC. NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="400px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>START DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_PRODUCT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TYPE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TYPE" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px;">OWN RETENTION</td>
                                        <td>
                                            <asp:TextBox ID="TXT_OR" runat="server" CssClass="ASPTextBox" Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% SHARE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_SHARE" runat="server" CssClass="ASPTextBox" Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% LOADING</td>
                                        <td>
                                            <asp:TextBox ID="TXT_LOADING" runat="server" CssClass="ASPTextBox" Width="100px" Style="text-align: right;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>MEDICAL TABLE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_UW" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_UW_SelectedIndexChanged"></asp:DropDownList>
                                            &nbsp;
                                            <asp:Button ID="BT_UW" runat="server" CssClass="ASPButton" Text="View" Visible="false" OnClick="BT_UW_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PREMIUM RATE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_RATE" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_RATE_SelectedIndexChanged"></asp:DropDownList>
                                            &nbsp;
                                            <asp:Button ID="BT_RATE" runat="server" CssClass="ASPButton" Text="View" Visible="false" OnClick="BT_RATE_Click" />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>PAYMENT FREQUENY</td>
                                        <td>
                                            <asp:DropDownList ID="ddPaymentFreq" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="ddPaymentFreq_SelectedIndexChanged">
                                                
                                            </asp:DropDownList>
                                            
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>PENURUNAN RESIKO</td>
                                        <td>
                                            <asp:DropDownList ID="ddPenurunanResiko" runat="server" CssClass="ASPDropDownList" >
                                                
                                            </asp:DropDownList>
                                            &nbsp;
                                            <asp:Button ID="btnViewRisk" runat="server" CssClass="ASPButton" Text="View" OnClick="btnViewRisk_Click" />
                                        </td>
                                    </tr>



                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                                            &nbsp;<asp:Button ID="BT_ARCHIEVE" runat="server" CssClass="ASPButton" Text="ARCHIEVE" Width="100px" OnClick="BT_ARCHIEVE_Click" BackColor="Green" ForeColor="White" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                </td>
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">MULTI COMPOSITION</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_COMPOSITION" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" PageSize="20" CssClass="ASPDatagrid">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="COMPANY_CODE" Visible="True"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PCT" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="REINSURANCE NAME"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="%">
                                            <HeaderStyle Width="40" HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_PCT" runat="server" CssClass="ASPTextBox" Width="40" Style="text-align: right;"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                </asp:DataGrid>
                                <asp:Button ID="BT_SAVE_COMPOSITION" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_COMPOSITION_Click" />

                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor">TC BENEFITS</td>
                        </tr>
                        <tr>
                            <td class="TDBGColor">
                                <table>
                                    <tr>
                                        <td>NB/RN
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_NBRN" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_NBRN_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="NB" Text="NB"></asp:ListItem>
                                                <asp:ListItem Value="RN" Text="RN"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>UW CODE
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DDL_UWCODE" runat="server" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_UWCODE_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="M" Text="MEDICAL"></asp:ListItem>
                                                <asp:ListItem Value="NM" Text="NON-MEDICAL"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="width: 100%; height: 300px; overflow: auto;">
                                    <asp:DataGrid ID="DGR_BENEFIT" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Names="Tahoma" Font-Size="XX-Small" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="True" OnItemCommand="DGR_BENEFIT_ItemCommand" Width="95%">
                                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                        <AlternatingItemStyle BackColor="#F7F7F7" />
                                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" VerticalAlign="Top" Wrap="false" />
                                        <HeaderStyle BackColor="#4A3C8C" ForeColor="#F7F7F7" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundColumn DataField="BENEFIT_CODE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT">
                                                <ItemStyle Width="225px" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="PREMIUM_RATE_TABLE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="NBRN" HeaderText="NB/RN"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="UW_CODE" HeaderText="UW CODE"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="RATE">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <table style="border-spacing: 0px;">
                                                        <tr>
                                                            <!--<td style="width: 100px;">RATE</td>-->
                                                            <td>
                                                                <asp:DropDownList ID="DDL_RATE" runat="server" CssClass="ASPDropDownList" Width="200" Enabled="false">
                                                                </asp:DropDownList>
                                                                <asp:Button ID="BT_RATE" runat="server" CssClass="ASPButton" BackColor="Green" ForeColor="White" Text="E" CommandName="Rate" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    </asp:DataGrid>
                                    <asp:Button ID="BT_SAVE_BENEFITS" runat="server" CssClass="ASPButton" Text="SAVE BENEFITS" Width="100" OnClick="BT_SAVE_BENEFITS_Click" />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_ERR_COMPOSITION" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="500px" Width="90%" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 60px; border: outset 2px gray; padding: 5px; display: none; overflow: auto;">
                <table style="border-spacing: 0px; width: 100%;">
                    <tr>
                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">
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
                            <asp:Label ID="lblStatus" runat="server" Visible="false" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None">
                                <AlternatingItemStyle BackColor="#DCDCDC" />
                                <ItemStyle Wrap="False" HorizontalAlign="Center" BackColor="#EEEEEE" ForeColor="Black" />
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle Wrap="False"
                                    HorizontalAlign="Center" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                            </asp:DataGrid>
                            <p></p>
                            <asp:Label ID="lblStatus2" runat="server" Visible="false" Font-Bold="True" Font-Size="Medium"></asp:Label>
                            <asp:DataGrid ID="DGR2" runat="server" CssClass="ASPDatagrid" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None">
                                <AlternatingItemStyle BackColor="#DCDCDC" />
                                <ItemStyle Wrap="False" HorizontalAlign="Center" BackColor="#EEEEEE" ForeColor="Black" />
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle Wrap="False"
                                    HorizontalAlign="Center" BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                            </asp:DataGrid>

                            <iframe id="iFrame" runat="server" style="width: 100%; height: auto;"></iframe>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
