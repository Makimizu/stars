<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableForm1.aspx.cs" Inherits="GO.TableForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" type="text/css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <table style="width: 100%; height: 100%; left: 0px; top: 0px;">
            <tr>
                <td style="vertical-align: top; width: 200px;">
                    <asp:TextBox ID="TXT_SEARCH" runat="server" AutoPostBack="True" CssClass="ASPTextBox" OnTextChanged="TXT_SEARCH_TextChanged" Width="100%"></asp:TextBox>
                    <asp:DataGrid ID="DGR_LIST" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" OnItemCommand="DGR_LIST_ItemCommand" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None" Width="100%">
                        <AlternatingItemStyle BackColor="PaleGoldenrod" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LB_ID" runat="server" CommandName="Detail"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="CODE" ItemStyle-Width="200" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <ItemStyle Wrap="False" HorizontalAlign="Left" />
                        <FooterStyle BackColor="Tan" />
                        <HeaderStyle Wrap="False"
                            HorizontalAlign="Center" BackColor="Tan" Font-Bold="True" />
                        <PagerStyle PageButtonCount="5" BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                    </asp:DataGrid>
                    <asp:ListBox ID="LB1" runat="server" BackColor="#CCFFFF" CssClass="ASPTextBox" Height="100%" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="LB1_SelectedIndexChanged" Visible="False"></asp:ListBox>
                    <asp:Label ID="LB_TABLENAME" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_FIELDNAME" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_FIELDNAME2" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_FIELDVALUE" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_PRIMAUTO" runat="server" Visible="False"></asp:Label>
                </td>
                <td style="vertical-align: top;">
                    <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Lime"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" OnItemCommand="DGR_ItemCommand">
                        <Columns>
                            <asp:BoundColumn DataField="name" ItemStyle-Width="200">
                                <ItemStyle Width="200px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="xtype" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="length" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="isnullable" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="primkey" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FKTABLENAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FKFIELDNAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FKFIELDNAME2" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:Button ID="BT_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Font-Bold="True" Text="SAVE" />
                                    <asp:Button ID="BT_NEW" runat="server" CommandName="New" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Text="NEW" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Width="300px"></asp:TextBox>
                                    <asp:DropDownList ID="DDL_VAL" runat="server" CssClass="ASPDropDownList" Visible="False">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TXT_DD" runat="server" CssClass="ASPTextBox" MaxLength="2" Visible="False" Width="20px"></asp:TextBox>
                                    <asp:DropDownList ID="DDL_MMM" runat="server" CssClass="ASPDropDownList" Visible="False">
                                        <asp:ListItem Value="1">Jan</asp:ListItem>
                                        <asp:ListItem Value="2">Feb</asp:ListItem>
                                        <asp:ListItem Value="3">Mar</asp:ListItem>
                                        <asp:ListItem Value="4">Apr</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">Jun</asp:ListItem>
                                        <asp:ListItem Value="7">Jul</asp:ListItem>
                                        <asp:ListItem Value="8">Aug</asp:ListItem>
                                        <asp:ListItem Value="9">Sep</asp:ListItem>
                                        <asp:ListItem Value="10">Oct</asp:ListItem>
                                        <asp:ListItem Value="11">Nov</asp:ListItem>
                                        <asp:ListItem Value="12">Dec</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TXT_YYYY" runat="server" CssClass="ASPTextBox" MaxLength="4" Visible="False" Width="35px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle Wrap="False" HorizontalAlign="Left" />
                        <HeaderStyle Wrap="False"
                            HorizontalAlign="Center" />
                        <PagerStyle PageButtonCount="5" />
                    </asp:DataGrid>
                    <asp:DataGrid ID="DGR_IMG" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" OnItemCommand="DGR_IMG_ItemCommand">
                        <Columns>
                            <asp:BoundColumn DataField="name" ItemStyle-Width="200">
                                <ItemStyle Width="200px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="xtype" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="length" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="isnullable" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="primkey" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FKTABLENAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FKFIELDNAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="FKFIELDNAME2" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Image ID="IMG_VAL" runat="server" Height="0px" /><br />
                                    <asp:FileUpload ID="FILEUPLOAD" runat="server" CssClass="ASPTextBox" />
                                    <asp:Button ID="BT_UPLOAD" runat="server" CommandName="Upload" CssClass="ASPButton" Text="UPLOAD" />
                                    <asp:Button ID="BT_CLEAR" runat="server" CommandName="Clear" CssClass="ASPButton" Text="CLEAR" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle Wrap="False" HorizontalAlign="Left" />
                        <HeaderStyle Wrap="False"
                            HorizontalAlign="Center" />
                        <PagerStyle PageButtonCount="5" />
                    </asp:DataGrid></td>
            </tr>
        </table>
    </form>
</body>
</html>
