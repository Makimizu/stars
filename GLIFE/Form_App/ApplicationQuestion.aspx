<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationQuestion.aspx.cs" Inherits="GLIFE.Form_App.ApplicationQuestion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        *, ::after, ::before {
            text-shadow: none !important;
            box-shadow: none !important;
        }

        *, ::after, ::before {
            box-sizing: border-box;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">MEDICAL QUESTION
                </td>
            </tr>
            <tr>
                <td>
                    <table id="TBL_QUESTION" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>MEDICAL QUESTION FOR :</td>
                            <td style="text-align: right">
                                <asp:DropDownList ID="DDL_QUESTION" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_QUESTION_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <asp:DataGrid ID="DGR_QUESTION" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Names="Tahoma" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%" CssClass="ASPDatagrid" Font-Size="X-Small">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#EAFFEA" ForeColor="#666666" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEXT_QUESTION" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VALUE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VALUE_NEXT" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr>
                                            <td>
                                                <table style="border-spacing: 0px; width: 100%;">
                                                    <tr style="vertical-align: top;">
                                                        <td>
                                                            <asp:Label ID="LB_DESCR" runat="server" /></td>
                                                        <td style="text-align: right;">
                                                            <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                                            <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="150px" MaxLength="255"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="TR_NEXTQUESTION" runat="server" visible="false">
                                            <td>
                                                <table style="border-spacing: 0px; width: 100%;">
                                                    <tr style="vertical-align: top; border-top: ridge;">
                                                        <td>
                                                            <asp:Label ID="LB_QUESTION" runat="server" Visible="false" /></td>
                                                        <td style="text-align: right;">
                                                            <asp:TextBox ID="TXT_NEXTVAL" runat="server" CssClass="ASPTextBox" Width="300px" MaxLength="255" Visible="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemStyle HorizontalAlign="Right" Width="20px" />
                                <ItemTemplate>
                                    <asp:Label ID="LB_UNCHECKED" runat="server" ForeColor="Red" Visible="false" Text="V" Font-Bold="true"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="100px" OnClick="BT_SAVE_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
