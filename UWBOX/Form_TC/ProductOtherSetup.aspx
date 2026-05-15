<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductOtherSetup.aspx.cs" Inherits="UWBOX.Form_TC.ProductOtherSetup" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <script type="text/javascript">
        function hourglass() {
            document.body.style.cursor = "wait";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr>
                <td style="width: 130px;">TERM & CONDITION</td>
                <td>
                    <asp:DropDownList ID="DDL_TC" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_TC_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>
        </table>


        <table id="TBL_PARAMETERS" runat="server" style="border-spacing: 0px; width: 100%; font-size: xx-small;">
            <tr style="vertical-align: top;">
                <td style="width: 200px;">
                    <asp:Button ID="BT_OTHER" runat="server" Width="98%" Text="OTHER SETTINGS" OnClick="BT_OTHER_Click" Font-Size="X-Small" />
                    <asp:Button ID="BT_FOP" runat="server" Font-Size="X-Small" Width="98%" Text="FREQUENCY OF PAYMENT" OnClick="BT_FOP_Click" />
                    <asp:Button ID="BT_LIEN" runat="server" Font-Size="X-Small" Width="98%" Text="LIEN CONDITION" OnClick="BT_LIEN_Click" />
                    <asp:Button ID="BT_MINMAX_PERIOD" runat="server" Font-Size="X-Small" Width="98%" Text="MINIMUM MAXIMUM PERIOD" OnClick="BT_MINMAX_PERIOD_Click" />
                    <asp:Button ID="BT_MINMAX_AGE" runat="server" Font-Size="X-Small" Width="98%" Text="MINIMUM MAXIMUM MEMBER AGE" OnClick="BT_MINMAX_AGE_Click" />
                    <asp:Button ID="BT_CLAIM" runat="server" Font-Size="X-Small" Width="98%" Text="CLAIM MEMBER STATUS" OnClick="BT_CLAIM_Click" />
                    <asp:Button ID="BT_SURPLUS" runat="server" Font-Size="X-Small" Width="98%" Text="SURPLUS UNDERWRITING" OnClick="BT_SURPLUS_Click" />
                    <asp:Button ID="BT_FUND" runat="server" Font-Size="X-Small" Width="98%" Text="FUND" OnClick="BT_FUND_Click" />
                    <asp:Button ID="BT_FINTERM" runat="server" Font-Size="X-Small" Width="98%" Text="FINANCING TERM" OnClick="BT_FINTERM_Click" />
                    <asp:Button ID="BT_WAKAF" runat="server" Font-Size="X-Small" Width="98%" Text="WAKAF" OnClick="BT_WAKAF_Click" />
                    <asp:Button ID="BT_RIDER" runat="server" Font-Size="X-Small" Width="98%" Text="RIDER SETTING" OnClick="BT_RIDER_Click" />
                </td>
                <td>
                    <table style="border-spacing: 0px; width: 100%; font-size: xx-small;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_TITLE" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <div id="DV_OTHER" runat="server">
                        <asp:DataGrid ID="DGR_ITEM" runat="server" AutoGenerateColumns="False" CellPadding="2" Font-Names="Verdana" Font-Size="XX-Small" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" ShowFooter="True" Width="98%" OnItemCommand="DGR_ITEM_ItemCommand">
                            <ItemStyle VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="VAL" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR">
                                    <ItemStyle Width="300px" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DDL_REFF" runat="server" Visible="False" CssClass="ASPDropDownList">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="TXT_VAL" runat="server" Visible="False" Width="100%" CssClass="ASPTextBox"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" CommandName="Save" Text="SAVE" Width="80" />
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </div>
                    <div id="DV_FOP" runat="server" visible="false">
                        <asp:DataGrid ID="DGR_FOP" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" Font-Size="XX-Small">
                            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                            <EditItemStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle
                                Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR" HeaderText="FREQUENCY TYPE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="LOADING" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="% LOADING">
                                    <HeaderStyle Width="60" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_LOADING" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;%
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MIN. PREMIUM">
                                    <HeaderStyle Width="150" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_MIN_PREMIUM" CssClass="ASPTextBoxNumber" Width="150" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MIN. PREMIUM TOP UP">
                                    <HeaderStyle Width="150" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_MIN_PREMIUM_TOPUP" CssClass="ASPTextBoxNumber" Width="150" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MAX. PREMIUM TOP UP<br/> MULTIPLIER">
                                    <HeaderStyle Width="150" HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_MAX_PREMIUM_TOPUP" CssClass="ASPTextBoxNumber" Width="50" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ENABLED" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="ENABLED">
                                    <HeaderStyle Width="50" HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CB_ENABLED" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>

                        <asp:Button ID="BT_SAVE_FOP" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_FOP_Click" Text="SAVE FOP % LOADING" Width="150px" OnClientClick="hourglass(); return true;" />
                    </div>
                    <div id="DV_LIEN" runat="server" visible="false">
                        <table style="width: 100%; border-spacing: 0px;">
                            <tr>
                                <td>
                                    <table style="width: 100%; border-spacing: 0px;">
                                        <tr>
                                            <td style="width: 100px;">AGE RANGE</td>
                                            <td>
                                                <asp:TextBox ID="TXT_STARTAGE" runat="server" CssClass="ASPTextBox" Width="40"></asp:TextBox>
                                                &nbsp;-&nbsp;
                                            <asp:TextBox ID="TXT_ENDAGE" runat="server" CssClass="ASPTextBox" Width="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>% CLAIM</td>
                                            <td>
                                                <asp:TextBox ID="TXT_PCTCLAIM" runat="server" CssClass="ASPTextBox" Width="40"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="BT_LIEN_SAVE" runat="server" Text="ADD" CssClass="ASPButton" Width="100" OnClick="BT_LIEN_SAVE_Click" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DataGrid ID="DGR_LIENCONDITION" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="400px" CellPadding="2" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_LIENCONDITION_ItemCommand" Font-Size="XX-Small">
                                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                        <EditItemStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle
                                            Wrap="False" BackColor="#0000CC" ForeColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="START_AGE" HeaderText="START AGE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="END_AGE" HeaderText="END AGE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PCT_CLAIM" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="% CLAIM">
                                                <HeaderStyle Width="100" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_PCTCLAIM" runat="server" Width="80" CssClass="ASPTextBoxNumber"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="100" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" CommandName="Save" />
                                                    <asp:Button ID="BT_X" runat="server" Text="X" CssClass="ASPButton" CommandName="Delete" BackColor="Red" ForeColor="White" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="DV_MINMAX_PERIOD" runat="server" visible="false">
                        <asp:DataGrid ID="DGR_MINMAX_PRD" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" Font-Size="XX-Small">
                            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                            <EditItemStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle
                                Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="PERIOD_MODE_CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PERIOD_MODE_DESCR" HeaderText="PERIOD MODE" ItemStyle-Width="60%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MINIMUM" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MAXIMUM" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="MINIMUM">
                                    <HeaderStyle Width="100" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_MIN" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;Month
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MAXIMUM">
                                    <HeaderStyle Width="100" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_MAX" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;Month
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>

                        <asp:Button ID="BT_SAVE_MINMAX_PRD" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_MINMAX_PRD_Click" Text="SAVE MIN MAX PERIOD" Width="225px" OnClientClick="hourglass(); return true;" />
                    </div>
                    <div id="DV_MINMAX_AGE" runat="server" visible="false">
                        <asp:DataGrid ID="DGR_MINMAX_AGE" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" Font-Size="XX-Small">
                            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                            <EditItemStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle
                                Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="MEMBER_TYPE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MEMBER_TYPE_DESCR" HeaderText="MEMBER TYPE" ItemStyle-Width="60%"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MIN_AGE" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MAX_AGE" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="MINIMUM">
                                    <HeaderStyle Width="100" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_MIN" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;Years
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="MAXIMUM">
                                    <HeaderStyle Width="100" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_MAX" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;Years
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>

                        <asp:Button ID="BT_SAVE_MINMAX_AGE" runat="server" CssClass="ASPButton" Text="SAVE MIN MAX AGE" Width="225px" OnClick="BT_SAVE_MINMAX_AGE_Click" OnClientClick="hourglass(); return true;" />

                    </div>
                    <div id="DV_CLAIM" runat="server" visible="false">
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 130px;">BENEFIT</td>
                                <td>
                                    <asp:DropDownList ID="DDL_BENEFIT_CLAIM" runat="server" AutoPostBack="true" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_BENEFIT_CLAIM_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>

                        <asp:DataGrid ID="DGR_BENEFIT_CLAIM" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="Horizontal" ShowHeader="False" BorderColor="#CCCCCC" Font-Size="XX-Small">
                            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                            <EditItemStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle
                                Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="MEMBER_TYPE_CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="LAPSE" ItemStyle-Width="60%" Visible="False">
                                    <ItemStyle Width="60%"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MEMBER_TYPE_DESCR"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DDL_LAPSE" CssClass="ASPDropDownList" runat="server">
                                            <asp:ListItem Value="1">LAPSE</asp:ListItem>
                                            <asp:ListItem Value="0">NOT LAPSE</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>

                        <asp:Button ID="BT_SAVE_BENEFITCLAIM" runat="server" CssClass="ASPButton" Text="SAVE MEMBER CLAIM STATUS" Width="225px" OnClientClick="hourglass(); return true;" OnClick="BT_SAVE_BENEFITCLAIM_Click" />

                    </div>
                    <div id="DV_SURPLUS" runat="server" visible="false">
                        <asp:DataGrid ID="DGR_SURPLUS" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" Font-Size="XX-Small">
                            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                            <EditItemStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle
                                Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR" HeaderText="BENEFICARY"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PCT" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="% SURPLUS">
                                    <HeaderStyle Width="60" HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:TextBox ID="TXT_SURPLUS" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;%
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>

                        <asp:Button ID="BT_SAVE_SURPLUS" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_SURPLUS_Click" Text="SAVE  SURPLUS UNDERWRITING" Width="225px" OnClientClick="hourglass(); return true;" />

                    </div>
                    <div id="DV_FUND" runat="server" visible="false">
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr id="TR_1_FUND" runat="server">
                                <td>
                                    <asp:DataGrid ID="DGR_FUND" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_FUND_ItemCommand" Font-Size="XX-Small">
                                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                        <EditItemStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle
                                            Wrap="False" BackColor="#1C5E55" ForeColor="White" VerticalAlign="Top" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="FUND_CODE" HeaderText="FUND<BR>CODE">
                                                <HeaderStyle Width="30" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="AGREEMENT_CODE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FUND_DESC" HeaderText="FUND NAME"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FUND_PCT" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="LOWRATE1" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MEDIUMRATE1" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="HIGHRATE1" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="LOWRATE2" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MEDIUMRATE2" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="HIGHRATE2" Visible="false"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="30" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CB" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <table style="border-spacing: 0px; font-size: xx-small;">
                                                        <tr>
                                                            <td>DESCRIPTION</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_DESCRIPTION" CssClass="ASPTextBox" Width="200" runat="server" MaxLength="255"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>AGREEMENT</td>
                                                            <td>
                                                                <asp:DropDownList ID="DDL_AGREEMENT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <table style="width: 100%; border-spacing: 0px; font-size: xx-small;">
                                                        <tr>
                                                            <td style="border-top-style: ridge;">MODE</td>
                                                            <td style="border-top-style: ridge;">% LOW RATE</td>
                                                            <td style="border-top-style: ridge;">% MED RATE</td>
                                                            <td style="border-top-style: ridge;">% HIGH RATE</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Death Benefit</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_LOW1" runat="server" CssClass="ASPTextBoxNumber" Width="40" BackColor="Red" ForeColor="White" Text="0"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_MID1" runat="server" CssClass="ASPTextBoxNumber" Width="40" BackColor="Green" ForeColor="White" Text="0"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_HIGH1" runat="server" CssClass="ASPTextBoxNumber" Width="40" BackColor="Blue" ForeColor="White" Text="0"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>Life Benefit</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_LOW2" runat="server" CssClass="ASPTextBoxNumber" Width="40" BackColor="Red" ForeColor="White" Text="0"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_MED2" runat="server" CssClass="ASPTextBoxNumber" Width="40" BackColor="Green" ForeColor="White" Text="0"></asp:TextBox></td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_HIGH2" runat="server" CssClass="ASPTextBoxNumber" Width="40" BackColor="Blue" ForeColor="White" Text="0"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemStyle Width="30" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_D" runat="server" Text="D" CssClass="ASPButton" BackColor="Green" ForeColor="White" CommandName="Detail" Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid><asp:Button ID="BT_SAVE_FUND" runat="server" Text="SAVE FUND" CssClass="ASPButton" Width="150px" OnClick="BT_SAVE_FUND_Click" OnClientClick="hourglass(); return true;" />
                                </td>
                            </tr>
                            <tr id="TR_1_INSTRUMENT" runat="server" visible="false">
                                <td>
                                    <table style="border-spacing: 0px; width: 100%;">
                                        <tr hidden="hidden">
                                            <td>FUND CODE</td>
                                            <td>
                                                <asp:Label ID="LB_FUNDCODE" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr hidden="hidden">
                                            <td class="auto-style2">FUND NAME</td>
                                            <td class="auto-style2">
                                                <asp:Label ID="LB_FUNDNAME" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <asp:DataGrid ID="DGR_INSTRUMENT" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" OnItemCommand="DGR_FUND_ItemCommand" Font-Size="XX-Small">
                                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                        <EditItemStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle
                                            Wrap="False" BackColor="#1C5E55" ForeColor="White" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="FUND_CODE" HeaderText="FUND CODE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="TC_ID" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="INSTRUMENT_CODE" Visible="false"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MIN_PCT" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="MAX_PCT" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="INSTRUMENT_NAME" HeaderText="INSTRUMENT NAME"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="MINIMUM">
                                                <HeaderStyle Width="120" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_MIN_INSPCT" runat="server" CssClass="ASPTextBoxNumber" Width="60"></asp:TextBox>&nbsp;%
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="MAXIMUM">
                                                <HeaderStyle Width="120" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_INSPCT" runat="server" CssClass="ASPTextBoxNumber" Width="60"></asp:TextBox>&nbsp;%
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid><asp:Button ID="BT_SAVE_INSTRUMENT" runat="server" Text="SAVE FUND INSTRUMENT" CssClass="ASPButton" OnClick="BT_SAVE_INSTRUMENT_Click" Width="200" OnClientClick="hourglass(); return true;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="DV_FINTERM" runat="server" visible="false">
                        <table style="width: 100%; border-spacing: 0px;">
                            <tr>
                                <td style="width: 50%;">IF INSURED PERSON WERE ALIVE</td>
                                <td>IF INSURED PERSON WERE DEAD</td>
                            </tr>
                            <tr style="vertical-align: top;">
                                <td>
                                    <div style="width: 100%; height: 200px; overflow: auto;">
                                        <asp:DataGrid ID="DGR_FINANCING1" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" Font-Size="XX-Small">
                                            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                            <EditItemStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle
                                                Wrap="False" BackColor="#0000CC" ForeColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundColumn DataField="DEATH" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="AGE" HeaderText="#">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PCT" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SCHOOL_GRADE" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="% BENEFIT">
                                                    <HeaderStyle Width="60" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_PCT1" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;%
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="SCHOOL GRADE/TAHAPAN">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DDL_SCHOOL_GRADE1" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                                <td>
                                    <div style="width: 100%; height: 200px; overflow: auto;">
                                        <asp:DataGrid ID="DGR_FINANCING2" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" Font-Size="XX-Small">
                                            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                            <EditItemStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle
                                                Wrap="False" BackColor="#FF3300" ForeColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundColumn DataField="DEATH" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="AGE" HeaderText="#">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="PCT" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SCHOOL_GRADE" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="% BENEFIT">
                                                    <HeaderStyle Width="60" HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_PCT2" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;%
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="SCHOOL GRADE/TAHAPAN">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DDL_SCHOOL_GRADE2" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="BT_SAVE_FINANCING" runat="server" Text="SAVE FINANCING TERM" CssClass="ASPButton" Width="180px" OnClick="BT_SAVE_FINANCING_Click" OnClientClick="hourglass(); return true;" />
                    </div>
                    <div id="DV_FINTERMSAVING" runat="server" visible="false">
                        <table style="width: 100%; border-spacing: 0px;">
                            <tr>
                                <td style="width: 50%;">IF POLICE PARTICIPANT WERE ALIVE</td>
                                <td>IF POLICE PARTICIPANT WERE DEAD</td>
                            </tr>
                            <tr style="vertical-align: top;">
                                <td>
                                    <div style="width: 100%; height: 200px; overflow: auto;">
                                        <asp:DataGrid ID="DGRSAVTERM1" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" Font-Size="XX-Small">
                                            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                            <EditItemStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle
                                                Wrap="False" BackColor="#0000CC" ForeColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundColumn DataField="DEATH" Visible="False"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="PERIOD">
                                                    <HeaderStyle Width="60" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_PERIOD" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="PCT" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PERIOD" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MINAGE" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MAXAGE" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SEQ" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="% BENEFIT">
                                                    <HeaderStyle Width="60" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_PCT1" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;%
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="MIN AGE">
                                                    <HeaderStyle Width="60" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_MINAGE" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="MAX AGE">
                                                    <HeaderStyle Width="60" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_MAXAGE" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                                <td>
                                    <div style="width: 100%; height: 200px; overflow: auto;">
                                        <asp:DataGrid ID="DGRSAVTERM2" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" CellPadding="2" ForeColor="#333333" GridLines="None" Font-Size="XX-Small">
                                            <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                            <EditItemStyle BackColor="#7C6F57" />
                                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle
                                                Wrap="False" BackColor="#FF3300" ForeColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <Columns>
                                                <asp:BoundColumn DataField="DEATH" Visible="False"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="PERIOD">
                                                    <HeaderStyle Width="60" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_PERIOD" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="PCT" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="PERIOD" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MINAGE" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="MAXAGE" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="SEQ" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="% BENEFIT">
                                                    <HeaderStyle Width="60" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_PCT1" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;%
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="MIN AGE">
                                                    <HeaderStyle Width="60" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_MINAGE" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="MAX AGE">
                                                    <HeaderStyle Width="60" HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TXT_MAXAGE" CssClass="ASPTextBoxNumber" Width="30" runat="server"></asp:TextBox>&nbsp;
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <asp:Button ID="BT_SAVE_SAVINGFINTERM" runat="server" Text="SAVE FINANCING TERM" CssClass="ASPButton" Width="180px" OnClick="BT_SAVE_SAVINGFINTERM_Click" OnClientClick="hourglass(); return true;" />
                    </div>
                    <div id="DV_WAKAF" runat="server" visible="false">
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 50%;" class="TDBGColor">WAKAF RISK BENEFIT SHARE</td>
                                <td style="width: 50%;" class="TDBGColor">WAKAF INVESTMENT SHARE</td>
                            </tr>
                            <tr style="vertical-align: top;">
                                <td>
                                    <asp:DataGrid ID="DGR_OTHERCORP_CLM" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" GridLines="None" ShowHeader="False" OnItemCommand="DGR_OTHERCORP_CLM_ItemCommand" Font-Size="XX-Small">
                                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                        <EditItemStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle
                                            Wrap="False" BackColor="#0000CC" ForeColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="CLAIM_MODE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PCT" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="100" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_PCT_CLM" runat="server" Width="40" CssClass="ASPTextBoxNumber" BackColor="LightPink"></asp:TextBox>&nbsp;%
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="100" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" CommandName="Save" />
                                                    <asp:Button ID="BT_X" runat="server" Text="X" CssClass="ASPButton" CommandName="Delete" BackColor="Red" ForeColor="White" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                </td>
                                <td>
                                    <asp:DataGrid ID="DGR_OTHERCORP_INV" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" CellPadding="2" ForeColor="#333333" GridLines="None" ShowHeader="False" OnItemCommand="DGR_OTHERCORP_INV_ItemCommand" Font-Size="XX-Small">
                                        <ItemStyle VerticalAlign="Top" BackColor="#E3EAEB" />
                                        <EditItemStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle
                                            Wrap="False" BackColor="#0000CC" ForeColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                        <AlternatingItemStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundColumn DataField="CLAIM_MODE" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PCT" Visible="False"></asp:BoundColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="100" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TXT_PCT_INV" runat="server" Width="40" CssClass="ASPTextBoxNumber" BackColor="LightGreen"></asp:TextBox>&nbsp;%
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="100" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Button ID="BT_SAVE_INV" runat="server" Text="SAVE" CssClass="ASPButton" CommandName="Save" />
                                                    <asp:Button ID="BT_X_INV" runat="server" Text="X" CssClass="ASPButton" CommandName="Delete" BackColor="Red" ForeColor="White" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="DV_RIDER" runat="server" visible="false">
                        <asp:DataGrid ID="DGR_RIDER" runat="server" CssClass="ASPDatagrid" PageSize="15" AutoGenerateColumns="False" Width="100%" Font-Size="XX-Small" ShowHeader="False">
                            <ItemStyle VerticalAlign="Top" />
                            <HeaderStyle
                                Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="BENEFIT_CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="BENEFIT_DESCR" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="LOADING" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MIN_PCT" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MIN_AMOUNT" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MAX_PCT" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MAX_AMOUNT" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MIN_AGE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MAX_AGE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MIN_PP" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="MAX_XN" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemStyle Width="98%" />
                                    <ItemTemplate>
                                        <table style="border-spacing: 0px; width: 100%;">
                                            <tr style="vertical-align: top;">
                                                <td class="TDBGColor" style="width: 10%; vertical-align: middle;">
                                                    <asp:Label ID="LB_BENEFIT" runat="server" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 30%;">
                                                    <table style="border-spacing: 0px; width: 100%;">
                                                        <tr>
                                                            <td style="width: 130px;">MINIMUM SUMINS</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_MIN_PCT" runat="server" CssClass="ASPTextBoxNumber" Width="30" BackColor="#ccffcc" ForeColor="DarkGreen"></asp:TextBox>%&nbsp;-&nbsp;
                                                                <asp:TextBox ID="TXT_MIN" runat="server" CssClass="ASPTextBoxNumber" Width="60" BackColor="#ccffcc" ForeColor="DarkGreen"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>MAXIMUM SUMINS</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_MAX_PCT" runat="server" CssClass="ASPTextBoxNumber" Width="30" BackColor="#ffecff" ForeColor="Red"></asp:TextBox>%&nbsp;-&nbsp;
                                                                <asp:TextBox ID="TXT_MAX" runat="server" CssClass="ASPTextBoxNumber" Width="60" BackColor="#ffecff" ForeColor="Red"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 30%;">
                                                    <table style="border-spacing: 0px; width: 100%;">
                                                        <tr>
                                                            <td style="width: 160px;">MINIMUM POLICY PERIOD (year)</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_MIN_PP" runat="server" CssClass="ASPTextBoxNumber" Width="30" BackColor="#ccffcc" ForeColor="DarkGreen"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td>MAXIMUM X+N (year)</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_XN" runat="server" CssClass="ASPTextBoxNumber" Width="30" BackColor="#ffecff" ForeColor="Red"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 30%;">
                                                    <table style="border-spacing: 0px; width: 100%;">
                                                        <tr>
                                                            <td>LOADING</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_LOADING" runat="server" CssClass="ASPTextBoxNumber" Width="30" BackColor="#ccffff" ForeColor="Blue"></asp:TextBox>%
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 130px;">LIMIT AGE (year)</td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_MIN_AGE" runat="server" CssClass="ASPTextBoxNumber" Width="30" BackColor="#ccffcc" ForeColor="DarkGreen"></asp:TextBox>&nbsp;-&nbsp;
                                                                <asp:TextBox ID="TXT_MAX_AGE" runat="server" CssClass="ASPTextBoxNumber" Width="30" BackColor="#ffecff" ForeColor="Red"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>

                        <asp:Button ID="BT_SAVE_RIDER" runat="server" Text="SAVE RIDER" CssClass="ASPButton" Width="180px" OnClientClick="hourglass(); return true;" OnClick="BT_SAVE_RIDER_Click" />

                    </div>
                </td>
            </tr>
        </table>

        <%--<table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td style="width: 49%;" class="TDBGColor">FUND</td>
                <td></td>
                <td style="width: 49%;" class="TDBGColor">OTHER SETTINGS</td>
            </tr>
            <tr id="TR_1" runat="server" style="vertical-align: top;">
                <td></td>
                <td></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="TDBGColor">FREQUENCY OF PAYMENT RATING FOR TABARRU DEDUCTION</td>
                <td></td>
                <td class="TDBGColor">SURPLUS UNDERWRITING</td>
            </tr>
            <tr id="TR_2" runat="server" style="vertical-align: top;">
                <td></td>

                <td>&nbsp;</td>
                <td></td>
            </tr>
            <tr>
                <td class="TDBGColor">FINANCING TERM</td>
                <td></td>
                <td class="TDBGColor">MINIMUM MAXIMUM PERIOD</td>
            </tr>
            <tr id="TR_3" runat="server" style="vertical-align: top;">
                <td>

                    <br />
                    <br />

                    <br />

                </td>
                <td></td>
                <td style="vertical-align: top;">


                    <br />
                    <br />
                    <br />
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">MINIMUM MAXIMUM MEMBER AGE</td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">RISK BENEFIT CLAIM MEMBER STATUS</td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>--%>
    </form>
</body>
</html>
