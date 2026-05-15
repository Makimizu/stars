<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CPbyNoSurat.aspx.cs" Inherits="HEALTH.Form_Klaim.CPbyNoSurat" %>

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
        <asp:Label ID="LB_ID" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <asp:TextBox ID="TXT_SEARCH" runat="server" CssClass="ASPTextBox" Width="200px" Placeholder="Search..."></asp:TextBox>
                    <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" Text="SEARCH" OnClick="BT_SEARCH_Click" />
                    <asp:DataGrid ID="DGR_REPORT" runat="server" AutoGenerateColumns="False" AllowSorting="True" 
                        AllowPaging="True" PageSize="15" OnPageIndexChanged="DGR_REPORT_PageIndexChanged"
                        OnSortCommand="DGR_REPORT_SortCommand" CellPadding="4" CssClass="ASPDatagrid" ForeColor="#333333" GridLines="None">
    
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_PDF1" runat="server" CssClass="ASPButton" Text="VIEW FILE" />
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:BoundColumn DataField="UPLOAD_PATH" Visible="False"></asp:BoundColumn>
                            
                            <asp:BoundColumn DataField="STATUS_CP" HeaderText="Provider" SortExpression="STATUS_CP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVIDER" HeaderText="Provider" SortExpression="PROVIDER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DIAGNOSA" HeaderText="Diagnosis (ID)" SortExpression="DIAGNOSA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DIAGNOSE" HeaderText="Diagnosis (EN)" SortExpression="DIAGNOSE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KELAS_KAMAR" HeaderText="Room Grade" SortExpression="KELAS_KAMAR"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AKTUAL_INAP" HeaderText="Actual Days Treated" SortExpression="AKTUAL_INAP"></asp:BoundColumn>
                            <asp:BoundColumn DataField="LAMA_RAWAT" HeaderText="CP Days Treated" SortExpression="LAMA_RAWAT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="BIAYA" HeaderText="Total Cost" DataFormatString="{0:N2}" SortExpression="TOTAL_BIAYA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TOTAL_BIAYA_CP" HeaderText="CP Total Cost" DataFormatString="{0:N2}" SortExpression="TOTAL_BIAYA"></asp:BoundColumn>
                        </Columns>

                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
