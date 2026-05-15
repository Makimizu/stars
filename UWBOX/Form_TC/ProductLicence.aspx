<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductLicence.aspx.cs" Inherits="UWBOX.Form_TC.ProductLicence" %>

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
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 150px;">LICENCE NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_LICENCENO" runat="server" CssClass="ASPTextBox" MaxLength="25" Width="200px" required="required"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>LICENCE NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_LICENCENAME" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="250px" required="required"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>REMARK</td>
                                        <td>
                                            <asp:TextBox ID="TXT_REMARK" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="400px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>START DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" required="required"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr id="TR_ENDDATE" runat="server">
                                        <td>END DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_SAVE_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_OTHER" runat="server">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 50%;" class="TDBGColor">PRODUCTS</td>
                            <td style="width: 50%;" class="TDBGColor">ARCHIEVE</td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td style="vertical-align: top;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 150px;">PRODUCT GROUP</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_GROUP" runat="server" CssClass="ASPDropDownList" AutoPostBack="True"
                                                OnSelectedIndexChanged="DDL_GROUP_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TC NAME</td>
                                        <td>
                                            <asp:TextBox ID="TXT_TCNAME" runat="server" CssClass="ASPTextBox" Width="90%" AutoPostBack="True" OnTextChanged="TXT_TCNAME_TextChanged"></asp:TextBox></td>
                                    </tr>
                                </table>
                                <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                                <asp:DataGrid ID="DGR" runat="server" CssClass="ASPDatagrid" PageSize="40" AutoGenerateColumns="False" AllowPaging="True"
                                    Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" OnPageIndexChanged="DGR_PageIndexChanged"
                                    PagerStyle-Mode="NextPrev"
                                    PagerStyle-Position="Bottom"
                                    PagerStyle-HorizontalAlign="Center"
                                    PagerStyle-NextPageText="Next"
                                    PagerStyle-PrevPageText="Prev">
                                    <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle
                                        Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TAKEN" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CODE" HeaderText="TC CODE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="TC DESCRIPTION"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Width="30" HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CB" runat="server" OnCheckedChanged="CB_CheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                </asp:DataGrid>
                            </td>
                            <td>
                                <iframe id="I2" runat="server" style="width: 100%; height: 400px;"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
