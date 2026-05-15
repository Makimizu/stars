<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_TPA_Enrollment.aspx.cs" Inherits="HEALTH.Form_Member.GPA_TPA_Enrollment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

       <asp:Panel ID="PnlMessage" runat="server" CssClass="modalPanel" Style="display:none; background:white; border-radius:6px;">
            <div style="padding:20px; width:600px; border-radius:6px;">

                <!-- Header (tidak scroll) -->
                <h3 id="MessageEnrollTitle" runat="server">Message Enroll</h3>

                <!-- Content (scroll jika panjang) -->
                <div style="max-height:300px; overflow-y:auto; margin-top:10px; padding-right:5px;">
                    <asp:Label ID="LblMessageDetail" runat="server" Text=""></asp:Label>
                </div>

                <!-- Footer (tidak scroll) -->
                <div style="text-align:right; margin-top:20px;">
                    <asp:Button ID="BtnClose" runat="server" Text="Close" CssClass="ASPButton" />
                </div>

            </div>
        </asp:Panel>

        <!-- MODAL POPUP EXTENDER -->
        <ajaxToolkit:ModalPopupExtender 
            ID="ModalMessage" 
            runat="server"
            TargetControlID="HiddenTarget"
            PopupControlID="PnlMessage"
            BackgroundCssClass="modalBackground"
            CancelControlID="BtnClose" />

        <!-- Hidden button untuk trigger popup -->
        <asp:Button ID="HiddenTarget" runat="server" Style="display:none;" />

        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td class="auto-style1">
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 120px;">COMPANY</td>
                            <td>
                                <asp:TextBox ID="TXT_COMPANY" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>TIPE ENROLLMENT</td>
                            <td>
                                <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>TPA</td>
                            <td>
                                <asp:DropDownList ID="DDL_TPA" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>TGL SUBMIT</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" style="text-align:center;" Width="60px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                        </ajaxToolkit:CalendarExtender>
                                        &nbsp;- <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" style="text-align:center;" Width="60px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SEARCH" runat="server" CssClass="ASPButton" OnClick="BT_SEARCH_Click" Text="CARI" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Label ID="LB_RECORDS" runat="server" Font-Bold="True"></asp:Label>

                    <asp:DataGrid ID="DGR" runat="server" BorderColor="#003300" CellPadding="4" PageSize="20" OnPageIndexChanged="DGR_PageIndexChanged" OnItemCommand="DGR_ItemCommand"
                        AllowPaging="True" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" ForeColor="#333333">
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_VIEW" runat="server" CssClass="ASPButton" Font-Bold="True" Text="ENROLL" CommandName="Enroll" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="BATCH_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TPA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TRACK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="COMPANY">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPE_DESCR" HeaderText="TIPE ENROLLMENT">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TPA_DESCR" HeaderText="TPA">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="MEMBER" HeaderText="#MEMBER">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CREATEDATE" HeaderText="TGL SUBMIT">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="URL" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TANGGAL_ENROLL" HeaderText="TGL ENROLL">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="STATUS_ENROLL" HeaderText="STATUS">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                           <asp:TemplateColumn HeaderText="REMARKS">
                            <ItemTemplate>
                                <asp:Button 
                                    ID="BtnMsg" 
                                    runat="server" 
                                    CssClass="ASPButton"
                                    Text="VIEW"
                                    CommandName="ViewMsg"
                                    CommandArgument='<%# Eval("MESSAGE_ENROLL") %>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateColumn>
                            <asp:BoundColumn DataField="COMPANY_CODE" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            Wrap="False" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
