<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReinsTC.aspx.cs" Inherits="UWBOX.Form_Parameter.ReinsTC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
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
                                            <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DESCRIPTION</td>
                                        <td>
                                            <asp:Label ID="LB_DESCR" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DOC. NO</td>
                                        <td>
                                            <asp:Label ID="LB_DOCNO" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>START DATE</td>
                                        <td>
                                            <asp:Label ID="LB_STARTDATE" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TYPE</td>
                                        <td>
                                            <asp:Label ID="LB_TYPE" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px;">OWN RETENTION</td>
                                        <td>
                                            <asp:Label ID="LB_OR" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% SHARE</td>
                                        <td>
                                            <asp:Label ID="LB_SHARE" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% LOADING</td>
                                        <td>
                                            <asp:Label ID="LB_LOADING" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>MEDICAL TABLE</td>
                                        <td>
                                            <asp:Label ID="LB_UW" runat="server" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="LB_UW_CODE" runat="server" Visible="false"></asp:Label>
                                            &nbsp;
                                            <asp:Button ID="BT_UW" runat="server" CssClass="ASPButton" Text="View" OnClick="BT_UW_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PREMIUM RATE</td>
                                        <td>
                                            <asp:Label ID="LB_RATE" runat="server" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="LB_RATE_CODE" runat="server" Visible="false"></asp:Label>
                                            &nbsp;
                                            <asp:Button ID="BT_RATE" runat="server" CssClass="ASPButton" Text="View" OnClick="BT_RATE_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_ARCHIEVE" runat="server" CssClass="ASPButton" Text="ARCHIEVE" Width="100px" OnClick="BT_ARCHIEVE_Click" BackColor="Green" ForeColor="White" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
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
                            <asp:Label ID="LB_RATECODE" runat="server" Visible="false"></asp:Label>
                            <table id="TBL_GENDER" runat="server" visible="false" style="border-spacing: 0px;">
                                <tr>
                                    <td style="width: 100px;">GENDER</td>
                                    <td>
                                        <asp:DropDownList ID="DDL_GENDER" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_GENDER_SelectedIndexChanged">
                                            <asp:ListItem Value="M">MALE</asp:ListItem>
                                            <asp:ListItem Value="F">FEMALE</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" BorderStyle="None">
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
