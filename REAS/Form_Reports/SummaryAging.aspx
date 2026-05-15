<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SummaryAging.aspx.cs" Inherits="REAS.Form_Reports.SummaryAging" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <div id="loading" style="display:none; position:fixed; top:50%; left:50%; transform:translate(-50%, -50%); z-index:9999; background-color: rgba(255, 255, 255, 0.8); padding: 20px; border-radius: 10px; text-align: center;">
            <img src="../Content/loading.gif" alt="Loading..." style="border: 0; width: 100px;" />
            <p>Loading...</p>
        </div>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">REAS NAME</td>
                                        <td>
                                           <asp:DropDownList 
                                                ID="DDL_REAS" 
                                                runat="server" 
                                                AutoPostBack="True" 
                                                CssClass="ASPDropDownList" 
                                                OnSelectedIndexChanged="DDL_REAS_SelectedIndexChanged"  
                                                Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>STATUS</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_STATUS" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" 
                                                OnSelectedIndexChanged="DDL_STATUS_SelectedIndexChanged"  Width="200px">
                                                <asp:ListItem Value="settlement" Selected="True">SETTLEMENT</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="LBL_STATUS" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <%--<tr>
                                        <td style="width: 100px;"></td>
                                        <td>
                                            <asp:Label ID="LB_RESULT" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" />
                                            <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD" OnClick="BT_DOWNLOAD_Click" Width="100px" />
                                            
                                        </td>
                                    </tr>--%>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 100px;">DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodStart" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                            &nbsp;-
                                            <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDelegatePeriodEnd" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        
                                        <td>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 30px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" Width="100px" OnClientClick="showLoading();"/>
                                            <asp:Button ID="BT_DOWNLOAD" runat="server" CssClass="ASPButton" Text="DOWNLOAD" OnClick="BT_DOWNLOAD_Click" Width="100px" BackColor="Aqua"/>
                
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
                    <asp:Label ID="LB_DOWNLOAD" runat="server"></asp:Label>
                    <asp:Label ID="LB_RESULT" runat="server"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical"
                                Font-Size="11px"
                                AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
                                CssClass="ASPDatagrid" OnPageIndexChanged="DGR_PageIndexChanged">
                    
                         <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="false" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" VerticalAlign="Top" ForeColor="#F7F7F7" Wrap="false"/>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Left" Mode="NumericPages" />


                    <Columns>
                         <asp:BoundColumn DataField="TYPE" HeaderText="TYPE" />
                         <asp:BoundColumn DataField="ID" HeaderText="ID" >
                             <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Green" HorizontalAlign="Right" />
                         </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATUS" HeaderText="STATUS" />
                        <asp:BoundColumn DataField="REAS_NAME" HeaderText="REAS NAME" />
                        <asp:BoundColumn DataField="NAMA_PESERTA" HeaderText="NAMA PESERTA" />
                        <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT_1" HeaderText="NO SURAT FIN SETTLE 1" />
                        <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_1" HeaderText="AMOUNT FIN SETTLEMENT 1" />
                        <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT_2" HeaderText="NO SURAT FIN SETTLE 2" />
                        <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_2" HeaderText="AMOUNT FIN SETTLEMENT 2" />
                        
                        <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT_3" HeaderText="NO SURAT FIN SETTLE 3" />
                        <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_3" HeaderText="AMOUNT FIN SETTLEMENT 3" />
                        <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT_4" HeaderText="NO SURAT FIN SETTLE 4" />
                        <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_4" HeaderText="AMOUNT FIN SETTLEMENT 4" />

                        <asp:BoundColumn DataField="NO_SURAT_SETTLEMENT_5" HeaderText="NO SURAT FIN SETTLE 5" />
                        <asp:BoundColumn DataField="AMOUNT_SETTLEMENT_5" HeaderText="AMOUNT FIN SETTLEMENT 5" />
                    </Columns>
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Center" Mode="NumericPages" Font-Size="11px" />
                </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>