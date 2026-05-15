<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EndorsementMemberList.aspx.cs" Inherits="LIFE.Form_POS.EndorsementMemberList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function CheckNumeric() {
            return event.keyCode >= 48 && event.keyCode <= 57;
        }

        function ValidateEmail(ctrl) {
            var val = ctrl.value;
            if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(val)) {
                return (true)
            }
            alert("You have entered an invalid email address!")
            return (false)
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_SEQ" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_TYPE" runat="server" Visible="false"></asp:Label>

        <table id="TBL_TITLE" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
            <tr>
                <td class="TDBGColor">
                    <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
            </tr>
        </table>

        <table id="TBL_MEMBER" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_MEMBER" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_MEMBER_ItemCommand">
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="MEMBER_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FULLNAME" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="FULLNAME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LBT_FULLNAME" runat="server" CommandName="Select"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="STAT" HeaderText="STATUS"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

        <table id="TBL_MEMBER_UPSERT" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_MEMBER_UPSERT" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_MEMBER_UPSERT_ItemCommand">
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                        <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="MEMBER_TYPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="false"></asp:BoundColumn>
                            <asp:BoundColumn DataField="MEMBER_TYPE_DESCR" HeaderText="MEMBER TYPE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="OLD_VALUE" HeaderText="EXISTING MEMBER"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NEW_VALUE" HeaderText="NEW MEMBER"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemStyle Width="70" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Button ID="BT_E" runat="server" CssClass="ASPButton" Text="E" BackColor="Green" ForeColor="White" CommandName="E" />
                                    <asp:Button ID="BT_X" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" CommandName="X" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>

        <div style="background-color: transparent !important; opacity: 0.9;">
            <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Style="z-index: 111; background-color: White; position: fixed; left: 0px; top: 10px; border: outset 2px gray; padding: 5px; display: none; width: 98%; height: 90vh;">
                <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="LB_BACK" runat="server" Text="Back .." ForeColor="Red" OnClick="LB_BACK_Click"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="TDBGColor">
                            <asp:Label ID="LB_MEMBERTITLE" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <iframe id="IF" runat="server" style="width: 100%; height: 80vh; border: none;"></iframe>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
