<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationBenefitSeries.aspx.cs" Inherits="GLIFE.Form_App.ApplicationBenefitSeries" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 40%;">
                                <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="DOWNLOAD TIMELINE XLS" Font-Bold="True" ForeColor="White" BackColor="Green" OnClick="BT_XLS_Click" /></td>
                            <td>
                                <asp:FileUpload ID="FU" runat="server" CssClass="ASPButton" />
                                <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" Text="UPLOAD TIMELINE" Font-Bold="True" ForeColor="White" BackColor="Orange" OnClick="BT_UPLOAD_Click" />
                                <asp:Label ID="LB_ERR" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Button ID="BT_DEFAULT" runat="server" CssClass="ASPButton" Text="GENERATE TIMELINE" Font-Bold="True" ForeColor="White" BackColor="Blue" OnClick="BT_DEFAULT_Click" /></td>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="VALUEDATE" HeaderText="VALUE DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Font-Bold="true" ForeColor="Black" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="START_BALANCE" HeaderText="START BALANCE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PRINCIPAL" HeaderText="PRINCIPAL">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MARGIN" HeaderText="MARGIN">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BALANCE" HeaderText="END BALANCE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Red" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OWNRETENTION" HeaderText="OWN<BR>RETENTION">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" Font-Bold="true" ForeColor="Green" />
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
