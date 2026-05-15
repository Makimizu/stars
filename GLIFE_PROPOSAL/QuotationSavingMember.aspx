<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationSavingMember.aspx.cs" Inherits="GLIFE_PROPOSAL.QuotationSavingMember" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="include/CommonStyle.css" rel="stylesheet" />
    <!-- Custom fonts for this template-->
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_QUOTNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_VERNO" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; font-size: x-small;">
            <tr>
                <td style="width: 250px;">XLS TEMPLATE</td>
                <td>
                    <asp:LinkButton ID="LBT_XLSTEMPLATE" runat="server" OnClick="LBT_XLSTEMPLATE_Click">Download here</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>UPLOAD MEMBER DATA</td>
                <td>
                    <asp:FileUpload ID="FU" runat="server" CssClass="ASPButton" />
                    &nbsp;
                        <asp:LinkButton ID="LBT_UPLOAD" runat="server" ToolTip="Upload" OnClick="LBT_UPLOAD_Click" OnClientClick="ShowProgress();">
                        <span class="fa fa-upload"></span>
                        </asp:LinkButton>
                </td>
            </tr>
        </table>

        <asp:Label ID="LB_ERR" runat="server" ForeColor="Red" Font-Size="X-Small"></asp:Label>

        <asp:DataGrid ID="DGR_MEMBER" runat="server" CellPadding="4" GridLines="None" Font-Size="8pt"
            PageSize="30" ForeColor="#333333" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanged="DGR_MEMBER_PageIndexChanged" CssClass="ASPDatagrid" Width="100%">
            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#E3EAEB" Wrap="False" VerticalAlign="Top" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <Columns>
                <asp:BoundColumn DataField="NBR" HeaderText="NO">
                    <HeaderStyle HorizontalAlign="Right" Width="40px" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SEX" HeaderText="GENDER">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ID_NO" HeaderText="ID NUMBER"></asp:BoundColumn>
                <asp:BoundColumn DataField="SUMINS" HeaderText="SUM<BR>INSURED">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SV_CLUN" HeaderText="LUNSUMP<BR>CONTRIBUTION">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SV_CREG" HeaderText="REGULER<BR>CONTRIBUTION">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SV_TOP" HeaderText="REGULER<BR>TOPUP">
                    <HeaderStyle HorizontalAlign="Right" />
                    <ItemStyle HorizontalAlign="Right" />
                </asp:BoundColumn>
            </Columns>
            <EditItemStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
        </asp:DataGrid>

    </form>
</body>
</html>
