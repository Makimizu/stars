<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="ClaimDetail.aspx.cs" Inherits="CORPORATE_PORTAL.ClaimDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="include/css/controls.css" rel="stylesheet" />

    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <div id="DV_SUBMIT" runat="server">
            <div class="row" style="width: 95%;">
                <div class="col-xl-6 col-sm-6 mb-3">
                    <table style="border-spacing: 0px; width: 100%; border-right: ridge; font-size: small; color: grey;">
                        <tr>
                            <td></td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 120px;">Claim No</td>
                                        <td>
                                            <asp:Label ID="LB_CLAIMNO" runat="server" Font-Bold="False" ForeColor="Black"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Claimant</td>
                                        <td>
                                            <asp:Label ID="LB_MEMBER" runat="server" Font-Bold="False" ForeColor="Black"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Care Date</td>
                                        <td>
                                            <asp:Label ID="LB_DATE" runat="server" Font-Bold="False" ForeColor="Black"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Benefit</td>
                                        <td>
                                            <asp:Label ID="LB_BENEFIT" runat="server" Font-Bold="False" ForeColor="Black"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Status</td>
                                        <td>
                                            <asp:Label ID="LB_STAT" runat="server" Font-Bold="False" ForeColor="Black"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-xl-6 col-sm-6 mb-3">
                    <table style="border-spacing: 0px; width: 100%; font-size: small; color: grey;">
                        <tr>
                            <td style="width: 80px;">P/R</td>
                            <td><asp:Label ID="LB_PR" runat="server" Font-Bold="False" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr id="TR_PROV" runat="server" style="border-bottom: ridge;">
                            <td>Hospital</td>
                            <td>
                                <asp:Label ID="LBL_PROVIDER_NAME" runat="server" ForeColor="Black"></asp:Label>
                            </td>
                        </tr>
                        <tr id="TR_ICD" runat="server" style="border-bottom: ridge; vertical-align: top;">
                            <td>Diagnoses</td>
                            <td>
                                <asp:DataGrid ID="DGR_ICD" runat="server" CellPadding="4" PageSize="40" GridLines="Vertical" CssClass="DataGrid" AutoGenerateColumns="False" ForeColor="#333333" BorderColor="Transparent" Width="100%" ShowHeader="False" Font-Size="8pt">
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <HeaderStyle BackColor="#CCCCCC" ForeColor="#666666"
                                        Wrap="False" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" HeaderText="Benefit"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Incurred" HeaderStyle-Width="110px">
                                            <ItemStyle HorizontalAlign="Center" Width="20px" ForeColor="Red" />
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>Archieve</td>
                            <td>
                                <asp:DataGrid ID="DGR_ARSIP" runat="server" CellPadding="4" PageSize="40" GridLines="Vertical" AutoGenerateColumns="False" ForeColor="#333333" BorderColor="Transparent" Width="100%" OnItemCommand="DGR_ARSIP_ItemCommand1" ShowHeader="False" Font-Size="8pt">
                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <HeaderStyle BackColor="#CCCCCC" ForeColor="#666666"
                                        Wrap="False" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="CODE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NAMAFILE" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SQL" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Wrap="false" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LB_VIEW" runat="server" CommandName="View"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle Wrap="false" HorizontalAlign="Right" ForeColor="Red" />
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <EditItemStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <asp:DataGrid ID="DGR_DONE" runat="server" CellPadding="4" PageSize="40" GridLines="Vertical" CssClass="DataGrid" AutoGenerateColumns="False" ForeColor="#333333" BorderColor="Transparent" Width="100%" Font-Size="8pt">
                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                <AlternatingItemStyle BackColor="White" />
                <HeaderStyle BackColor="#CCCCCC" ForeColor="#666666"
                    Wrap="False" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <Columns>
                    <asp:BoundColumn DataField="DESCR" HeaderText="ITEMS">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="INCURRED" HeaderText="INCURRED">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Wrap="False" ForeColor="Green" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="REJECTED" HeaderText="REJECTED">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Wrap="False" ForeColor="Red" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PAID" HeaderText="APPROVED">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Width="100px" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Wrap="False" ForeColor="Blue" />
                    </asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid>
        </div>

        <div id="DV_PENDING" runat="server" visible="false">
            <br />
            <asp:DataGrid ID="DGR_REASONTP" runat="server" CellPadding="2" PageSize="20" GridLines="Vertical" CssClass="Datagrid" BorderColor="Gray" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderWidth="1px" ForeColor="Black" Width="100%" Font-Size="8pt">
                <AlternatingItemStyle BackColor="PaleGoldenrod" />
                <Columns>
                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        <ItemStyle Width="40px" HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCR" HeaderText="PENDING REASONS"></asp:BoundColumn>
                </Columns>
                <FooterStyle BackColor="Tan" />
                <HeaderStyle BackColor="Tan" Font-Bold="False" HorizontalAlign="Left" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <ItemStyle VerticalAlign="Top" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            </asp:DataGrid>
        </div>


        <div id="DV_IMAGE" runat="server" visible="false">
            <table style="width: 100%;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="LB_DOWNLOAD" runat="server" OnClick="LB_DOWNLOAD_Click">Not an image file ? Download the file&nbsp;&nbsp;<span class="fa fa-download"></span></asp:LinkButton>
                                </td>
                                <td style="text-align: right;">
                                    <asp:LinkButton ID="LB_BACK" runat="server" Text="BACK ..." ForeColor="Red" Font-Bold="true" OnClick="LB_BACK_Click"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LB_FILENAME" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="LB_SQL" runat="server" Visible="false"></asp:Label>
                        <asp:Image ID="IMG" Width="100%" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
    </form>

</asp:Content>

