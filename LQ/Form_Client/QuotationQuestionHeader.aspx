<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuotationQuestionHeader.aspx.cs" Inherits="LQ.Form_Client.QuotationQuestionHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>

        <asp:DataGrid ID="DGR_BUTTON" runat="server" AutoGenerateColumns="False" Font-Names="Tahoma" Font-Size="8pt" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_BUTTON_ItemCommand">
            <ItemStyle VerticalAlign="Top" />
            <Columns>
                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="ADD_URL" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="BT_GROUP" runat="server" CssClass="ASPButton" Width="100%" CommandName="Detail" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        <br />
        <br />
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr id="TR_SEND_EMAIL" runat="server">
                <td>
                    <asp:Button ID="BT_SEND_EMAIL" runat="server" CssClass="ASPButton" Width="100%" Text="KIRIM EMAIL KONFIRMASI KE NASABAH" BackColor="Navy" ForeColor="White" OnClick="BT_SEND_EMAIL_Click" />
                    <br />
                    <br />
                    <asp:Label ID="LB_VALIDATION" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr id="TR_SIGNATURE" runat="server">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">PERSETUJUAN NASABAH</td>
                        </tr>
                        <tr>
                            <td>
                                <center>
                                <asp:Image ID="IMG_SIGNATURE" runat="server" Width="80%" ImageUrl="~/include/image/signature.png" />
                                <br />
                                DISETUJUI TANGGAL
                                <br />
                                <asp:Label ID="LB_AGREEMENT_DATE" runat="server" Font-Bold="true"></asp:Label>
                                </center>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>


    </form>
</body>
</html>
