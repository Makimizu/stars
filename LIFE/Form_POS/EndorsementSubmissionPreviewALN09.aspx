<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementSubmissionPreviewALN09.aspx.cs" Inherits="LIFE.Form_POS.EndorsementSubmissionPreviewALN09" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>

        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">PREVIEW<br />
                    PERSONAL DATA CHANGES</td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" ItemStyle-Wrap="true" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundColumn DataField="DESCR" HeaderText="PARAMETER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OLD_VAL" HeaderText="OLD VALUE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEW_VAL" HeaderText="NEW VALUE"></asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" VerticalAlign="Top" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#EFF3FB" />
                        <AlternatingItemStyle BackColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

