<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Mobile/MobileParent.Master" CodeBehind="MobileQuestion.aspx.cs" Inherits="LQR.Mobile.MobileQuestion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />

    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_GROUP" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_MEMBERID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_URL" runat="server" Visible="false"></asp:Label>

        <table id="TBL_PARENT" runat="server" style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label>
                </td>
            </tr>
            <tr id="TR_MEMBER" runat="server">
                <td style="text-align: center;">PERTANYAAN UNTUK :&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="DDL_MEMBER" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_MEMBER_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_QUESTION" runat="server" AutoGenerateColumns="False" CellPadding="3" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" OnItemCommand="DGR_QUESTION_ItemCommand">
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" VerticalAlign="Top" ForeColor="#4A3C8C" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <Columns>
                            <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                <ItemStyle HorizontalAlign="Right" Width="30px" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEXT_QUESTION" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ANSWER1" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ANSWER2" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="QUESTION_CHILD" Visible="False"></asp:BoundColumn>
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
                                                        <td style="width: 200px;">
                                                            <asp:DropDownList ID="DDL_REFF" runat="server" Visible="False" Enabled="false" Style="border: 0; color: black;"></asp:DropDownList>
                                                            <asp:TextBox ID="TXT_VAL" runat="server" Visible="False" Width="95%" MaxLength="255" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="TR_NEXTQUESTION" runat="server" visible="false">
                                            <td>
                                                <table style="border-spacing: 0px; width: 100%; border-top: inset;">
                                                    <tr style="vertical-align: top;">
                                                        <td>
                                                            <asp:Label ID="LB_QUESTION" runat="server" Visible="false" />
                                                        </td>
                                                        <td style="width: 200px;">
                                                            <asp:TextBox ID="TXT_NEXTVAL" runat="server" Width="95%" MaxLength="255" Visible="false" BackColor="LightYellow" TextMode="MultiLine" Height="30" ReadOnly="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Button ID="BT_Q" runat="server" Text="TAMBAHAN PERTANYAAN TERKAIT" ForeColor="White" CommandName="Child" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="150" OnClick="BT_SAVE_Click" Visible="False" />
                </td>
            </tr>
        </table>

        <table id="TBL_CHILD" runat="server" style="border-spacing: 0px; width: 95%;" visible="false">
            <tr>
                <td class="TDBGColor">REFLECTIVE QUESTION :
                    <br />
                    <asp:Label ID="LB_CHILD_TITLE" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LB_QUESTION_GROUP_SEQ" runat="server" Visible="false"></asp:Label>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:Label ID="LB_MODULARCODE" runat="server" Visible="false"></asp:Label>
                                <asp:Button ID="LB_BACK" runat="server" BackColor="Red" ForeColor="White" Text="KEMBALI .." OnClick="LB_BACK_Click" Width="100%"></asp:Button>
                                <asp:DataGrid ID="DGR_BUTTON" runat="server" AutoGenerateColumns="False" Font-Size="8pt" GridLines="None" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_BUTTON_ItemCommand">
                                    <ItemStyle VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_MODULAR" runat="server" Width="100%" CommandName="Modular" Style="white-space: normal;" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td class="TDBGColor">
                                            <asp:Label ID="LB_MODULARDESCR" runat="server" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="width: 100%; height: 80vh; overflow: auto; border-style: inset;">
                                                <asp:DataGrid ID="DGR_QUESTION_CHILD" runat="server" AutoGenerateColumns="False" CellPadding="3" Font-Size="8pt" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%" CssClass="ASPDatagrid" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px">
                                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                    <ItemStyle BackColor="#E7E7FF" VerticalAlign="Top" ForeColor="#4A3C8C" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                                            <ItemStyle HorizontalAlign="Right" Width="30px" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="NEXT_QUESTION" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ANSWER1" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ANSWER2" Visible="False"></asp:BoundColumn>
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
                                                                                    <td style="width: 200px;">
                                                                                        <asp:DropDownList ID="DDL_REFF" runat="server" Visible="False" Enabled="false" Style="border: 0; color: black;"></asp:DropDownList>
                                                                                        <asp:TextBox ID="TXT_VAL" runat="server" Visible="False" Width="95%" MaxLength="255" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr id="TR_NEXTQUESTION" runat="server" visible="false">
                                                                        <td>
                                                                            <table style="border-spacing: 0px; width: 100%; border-top: inset;">
                                                                                <tr style="vertical-align: top;">
                                                                                    <td>
                                                                                        <asp:Label ID="LB_QUESTION" runat="server" Visible="false" />
                                                                                    </td>
                                                                                    <td style="width: 200px;">
                                                                                        <asp:TextBox ID="TXT_NEXTVAL" runat="server" Width="95%" MaxLength="255" Visible="false" BackColor="LightYellow" TextMode="MultiLine" Height="30" ReadOnly="true"></asp:TextBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemStyle HorizontalAlign="Right" Width="40px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="LB_UNCHECKED" runat="server" Visible="false">
                                                                <span class="fa fa-bell"></span>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Button ID="BT_SAVE_SUB" runat="server" CssClass="ASPButton" Text="SAVE" Width="100" OnClick="BT_SAVE_SUB_Click" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>


</asp:Content>
