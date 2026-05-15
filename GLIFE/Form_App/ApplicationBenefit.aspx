<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationBenefit.aspx.cs" Inherits="GLIFE.Form_App.ApplicationBenefit" %>

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
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 95%;">
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">BENEFIT
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_BENEFIT" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_BENEFIT_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="BENEFIT_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                            <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SUMINS" HeaderText="SUM INSURED">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Blue" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RATE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="RATE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_RATESAVE" runat="server" CssClass="ASPButton" Text="SAVE" CommandName="Save" />
                                    <asp:TextBox ID="TXT_RATE" runat="server" Width="40px" CssClass="ASPTextBoxNumber"></asp:TextBox>
                                    &permil;
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="PREMIUM" HeaderText="PREMIUM">
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    <br />
                </td>
            </tr>
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">LOADING
                </td>
            </tr>
            <tr id="TR_LOADING" runat="server">
                <td>
                    <asp:DataGrid ID="DGR_LOADING" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_LOADING_ItemCommand">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="LOADING"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="RATE">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_RATESAVE" runat="server" CssClass="ASPButton" Text="SAVE" CommandName="Save" />
                                    <asp:TextBox ID="TXT_RATE" runat="server" Width="40px" CssClass="ASPTextBoxNumber"></asp:TextBox>
                                    %
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="VAL_PREMIUM" HeaderText="AMOUNT">
                                <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>

                    <br />
                </td>
            </tr>
            <tr id="TR_LOADING_PERIODIC" runat="server" visible="false">
                <td>
                    <iframe id="IF_LOADING" runat="server" style="width: 100%; height: 300px; border: none; margin: 0px; padding: 0px;"></iframe>
                </td>
            </tr>
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">ACCUMULATION
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_ACCU" runat="server" BackColor="White"
                        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_ACCU_ItemCommand" OnItemDataBound="DGR_ACCU_ItemDataBound" ShowFooter="True">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="Yellow" ForeColor="#006600" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="REGNO">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_REGNO" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SEX" HeaderText="GENDER">
                                <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="START_DATE" HeaderText="START<BR>DATE">
                                <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="SUMINS" HeaderText="SUM<BR>INSURED">
                                <FooterStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="OWNRETENTION" HeaderText="OWN<BR>RETENTION">
                                <FooterStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" CommandName="Delete" Text="X" ForeColor="White" BackColor="Red" ToolTip="Rollback to Quotation" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
