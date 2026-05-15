<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductMinMaxSA.aspx.cs" Inherits="UWBOX.Form_TC.ProductMinMaxSA" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function Submit() {
            form1.submit();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <asp:Label ID="LB_CODE" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 130px;">TERM & CONDITION</td>
                            <td>
                                <asp:DropDownList ID="DDL_TC" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_TC_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>

                        <tr>
                            <td style="width: 150px;"><a href="MIN_MAX_SA.xls">DOWNLOAD XLS TEMPLATE</a></td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">SUM ASSURED</td>
                            <td class="TDBGColor">LUMP SUM</td>
                        </tr>
                        <tr>
                            <td>
                                <table id="TBL_FU" runat="server" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_CLEAR" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="CLEAR" Width="100px" OnClick="BT_CLEAR_Click" Visible="False" />
                                            <asp:FileUpload ID="FU" runat="server" CssClass="ASPButton" onchange="javascript:Submit()" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" Font-Bold="True" ForeColor="White" BackColor="Green" Visible="True" OnClick="BT_XLS_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="width: 100%; height: 70vh; overflow: auto;">
                                                <asp:DataGrid ID="DGR_RATE" runat="server" CssClass="ASPDatagrid" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None" Font-Size="XX-Small">
                                                    <AlternatingItemStyle BackColor="#DCDCDC" />
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" BackColor="#EEEEEE" ForeColor="Black" />
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle Wrap="False"
                                                        HorizontalAlign="Center" BackColor="#000084" ForeColor="White" />
                                                    <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                                    <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table id="TBL_FU_LS" runat="server" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_CLEAR_LS" runat="server" CssClass="ASPButton" BackColor="Red" ForeColor="White" Text="CLEAR" Width="100px" OnClick="BT_CLEAR_LS_Click" Visible="False" />
                                            <asp:FileUpload ID="FU_LS" runat="server" CssClass="ASPButton" onchange="javascript:Submit()" Visible="False" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="LB_ERROR_LS" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BT_XLS_LS" runat="server" CssClass="ASPButton" Text="XLS" Font-Bold="True" ForeColor="White" BackColor="Green" Visible="True" OnClick="BT_XLS_LS_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="width: 100%; height: 70vh; overflow: auto;">
                                                <asp:DataGrid ID="DGR_LUMPSUM" runat="server" CssClass="ASPDatagrid" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None" Font-Size="XX-Small">
                                                    <AlternatingItemStyle BackColor="#DCDCDC" />
                                                    <ItemStyle Wrap="False" HorizontalAlign="Center" BackColor="#EEEEEE" ForeColor="Black" />
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <HeaderStyle Wrap="False"
                                                        HorizontalAlign="Center" BackColor="#000084" ForeColor="White" />
                                                    <PagerStyle PageButtonCount="5" BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Mode="NumericPages" />
                                                    <SelectedItemStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <%--<tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        
                        
                    </table>
                </td>

            </tr>--%>
    </form>
</body>
</html>
