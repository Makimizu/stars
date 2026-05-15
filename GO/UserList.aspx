<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="GO.UserList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="include/css/bootstrap.min.css" rel="stylesheet" />
    <link href="CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>


        <div class="row">
            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 100px;">USER ID / NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_SEARCH" runat="server" CssClass="ASPTextBox" Width="200px" AutoPostBack="True" OnTextChanged="TXT_SEARCH_TextChanged"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ROLE</td>
                            <td>
                                <asp:DropDownList ID="DDL_ROLE" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_ROLE_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>STATUS</td>
                            <td>
                                <asp:DropDownList ID="DDL_STATUS" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_STATUS_SelectedIndexChanged">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                    <asp:ListItem Value="0">NOT ACTIVE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>LOCKED ?</td>
                            <td>
                                <asp:DropDownList ID="DDL_LOCKED" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_LOCKED_SelectedIndexChanged">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1">LOCKED</asp:ListItem>
                                    <asp:ListItem Value="0">NOT LOCKED</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="text-align: right;">
                                <asp:Label ID="LB_CNT" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div style="overflow: auto; height: 250px; width: 100%;">
                        <asp:DataGrid ID="DGR_LIST" runat="server" AutoGenerateColumns="False" CssClass="ASPDatagrid" ShowHeader="False" OnItemCommand="DGR_LIST_ItemCommand" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                <asp:BoundColumn DataField="CODE" ItemStyle-Width="200" Visible="False">
                                    <ItemStyle Width="200px"></ItemStyle>
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ACTIVE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="LOCKED" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LB_ID" runat="server" CommandName="Detail" Font-Bold="true" ForeColor="Green"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LB_NAME" runat="server" CommandName="Detail"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <table style="border-spacing: 0px;">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="RD_STAT" runat="server" Text="&nbsp;ACTIVE" AutoPostBack="true" ForeColor="Blue" OnCheckedChanged="RD_STAT_Change" />
                                                </td>
                                                <td style="width: 20px;"></td>
                                                <td>
                                                    <asp:CheckBox ID="RD_LOCKED" runat="server" Text="&nbsp;LOCKED" AutoPostBack="true" ForeColor="Red" OnCheckedChanged="RD_LOCKED_Change" />
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="#EFF3FB" />
                            <EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle Wrap="False"
                                HorizontalAlign="Center" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle PageButtonCount="5" BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" />
                        </asp:DataGrid>
                    </div>
                </div>
            </div>


            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card-body" style="width: 100%; left: 0px; top: 0px;">
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_NEW" runat="server" CssClass="ASPButton" Text="NEW" Width="100px" OnClick="BT_NEW_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">USER ID</td>
                            <td>
                                <asp:TextBox ID="TXT_CODE" runat="server" MaxLength="10" CssClass="ASPTextBox" Width="100px" Style="text-align: center; background-color: yellow;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>PASSWORD</td>
                            <td>
                                <asp:TextBox ID="TXT_PASSWORD" runat="server" MaxLength="100" Width="100%" CssClass="ASPTextBox" TextMode="Password" Style="background-color: yellow;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ROLE</td>
                            <td>
                                <asp:DropDownList ID="DDL_USERROLE" runat="server" CssClass="ASPDropDownList" Style="background-color: yellow;"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>FRONT NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_FRONTNAME" runat="server" MaxLength="100" CssClass="ASPTextBox" Width="100%" Style="background-color: yellow;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>LAST NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_LASTNAME" runat="server" MaxLength="20" CssClass="ASPTextBox" Width="100%" Style="background-color: yellow;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>MIDDLE NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_MIDNAME" runat="server" MaxLength="20" CssClass="ASPTextBox" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>DOB</td>
                            <td>
                                <asp:TextBox ID="TXT_DOB" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center; background-color: yellow;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>ID EMPLOYEE</td>
                            <td>
                                <asp:TextBox ID="TXT_IDEMPLOYEE" runat="server" MaxLength="20" CssClass="ASPTextBox" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>EMAIL</td>
                            <td>
                                <asp:TextBox ID="TXT_EMAIL" runat="server" MaxLength="255" CssClass="ASPTextBox" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>UPLINER</td>
                            <td>
                                <asp:DropDownList ID="DDL_UPLINER" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>BRANCH</td>
                            <td>
                                <asp:DropDownList ID="DDL_BRANCH" runat="server" CssClass="ASPDropDownList"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>POSITION TITLE</td>
                            <td>
                                <asp:TextBox ID="TXT_TITLE" runat="server" MaxLength="50" CssClass="ASPTextBox" Width="100%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="100px" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                        </tr>
                    </table>
                    <table id="TBL_BINARY" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td>
                                <br />
                                <br />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="width: 50%; text-align: center;">PROFILE PHOTO</td>
                            <td style="width: 50%; text-align: center;">SIGNATURE</td>
                        </tr>
                        <tr>
                            <td style="text-align: center;">
                                <asp:Image ID="IMG_PHOTO" runat="server" Height="100px" /><br />
                                <asp:FileUpload ID="PHOTOUPLOAD" runat="server" CssClass="ASPTextBox" /><br />
                                <asp:Button ID="BT_PHOTOUPLOAD" runat="server" CommandName="Upload" CssClass="ASPButton" Text="UPLOAD" OnClick="BT_PHOTOUPLOAD_Click" />
                                <asp:Button ID="BT_PHOTOCLEAR" runat="server" CommandName="Clear" CssClass="ASPButton" Text="CLEAR" OnClick="BT_PHOTOCLEAR_Click" />
                            </td>
                            <td style="text-align: center;">
                                <asp:Image ID="IMG_SIGNATURE" runat="server" Height="100px" /><br />
                                <asp:FileUpload ID="SIGNATUREUPLOAD" runat="server" CssClass="ASPTextBox" /><br />
                                <asp:Button ID="BT_SIGNATUREUPLOAD" runat="server" CommandName="Upload" CssClass="ASPButton" Text="UPLOAD" OnClick="BT_SIGNATUREUPLOAD_Click" />
                                <asp:Button ID="BT_SIGNATURECLEAR" runat="server" CommandName="Clear" CssClass="ASPButton" Text="CLEAR" OnClick="BT_SIGNATURECLEAR_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
