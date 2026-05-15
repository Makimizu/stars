<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchUpload.aspx.cs" Inherits="GLIFE.Form_Saving.BatchUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="DV_POLICY" runat="server">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%;">
                            <tr>
                                <td style="width: 120px;">SEARCH POLICY</td>
                                <td>
                                    <asp:TextBox ID="TXT_POLICYSEARCH" runat="server" AutoPostBack="true" Width="100%" OnTextChanged="TXT_POLICYSEARCH_TextChanged" CssClass="ASPTextBox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="DGR_POLICY" runat="server" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" OnItemCommand="DGR_POLICY_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnPageIndexChanged="DGR_POLICY_PageIndexChanged" CssClass="ASPDatagrid">
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TC_CODE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="POLICY NO">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBT_POLICY" runat="server" CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="TC_DESCR" HeaderText="PRODUCT"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="POLICY HOLDER"></asp:BoundColumn>
                            </Columns>
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>

        <div id="DV_UPLOAD" runat="server" visible="false">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td>
                        <asp:LinkButton ID="LBT_POLICYBACK" runat="server" ForeColor="Red" ToolTip="Back to policy selection ..." OnClick="LBT_POLICYBACK_Click">
                            <span class="fa fa-backward"></span>&nbsp;BACK
                        </asp:LinkButton>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 120px;">POLICY NO</td>
                    <td>
                        <asp:Label ID="LB_POLICYNO" runat="server"></asp:Label>
                        <asp:Label ID="LB_POLICYID" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>POLICY HOLDER</td>
                    <td>
                        <asp:Label ID="LB_COMPANY" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>PRODUCT</td>
                    <td>
                        <asp:Label ID="LB_PRODUCT" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>AGENT</td>
                    <td>
                        <asp:DropDownList ID="DDL_AGENT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>BRANCH</td>
                    <td>
                        <asp:DropDownList ID="DDL_BRANCH" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                        &nbsp;
                        <asp:LinkButton ID="LB_BRANCHXLS" runat="server" ToolTip="Download Branch Code" ForeColor="Green" OnClick="LB_BRANCHXLS_Click">
                        <span class="fa fa-list"></span>
                        </asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td>UPLOAD FORMAT</td>
                    <td>
                        <asp:DropDownList ID="DDL_FORMAT" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                        &nbsp;
                        <asp:LinkButton ID="LBT_TEMPLATE_XLS" runat="server" ToolTip="Download Excel Template" ForeColor="Green" OnClick="LBT_TEMPLATE_XLS_Click">
                        <span class="fa fa-download"></span>
                        </asp:LinkButton></td>
                </tr>
                <tr>
                    <td>UPLOAD TEMPLATE</td>
                    <td>
                        <a href="SAVING - TEMPLATE.xls">XLS File Template</a>                        
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:FileUpload ID="FU" runat="server" CssClass="ASPButton" />
                        &nbsp;
                        <asp:LinkButton ID="LBT_UPLOAD" runat="server" ToolTip="Upload" OnClick="LBT_UPLOAD_Click">
                        <span class="fa fa-upload"></span>
                        </asp:LinkButton>
                                            <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" Text="UPLOAD" OnClick="BT_UPLOAD_Click" BackColor="Blue" ForeColor="White" Width="100px" /></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Label ID="LB_ERR" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
