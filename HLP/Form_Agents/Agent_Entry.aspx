<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agent_Entry.aspx.cs" Inherits="HLP.Form_Agents.Agent_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td style="width: 180px;" class="auto-style5">KODE AGEN</td>
                            <td>
                                <asp:TextBox ID="TXT_CODE" runat="server" CssClass="ASPTextBox" MaxLength="50"></asp:TextBox>
                                <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">CHANNEL DISTRIBUTION</td>
                            <td>
                                <asp:DropDownList ID="DDL_CD" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_CD_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">SUB CHANNEL DISTRIBUTION</td>
                            <td>
                                <asp:DropDownList ID="DDL_SUBCD" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_SUBCD_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">NAMA DEPAN</td>
                            <td>
                                <asp:TextBox ID="TXT_NMFRONT" runat="server" CssClass="ASPTextBox" MaxLength="20" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">NAMA BELAKANG</td>
                            <td>
                                <asp:TextBox ID="TXT_NMLAST" runat="server" CssClass="ASPTextBox" MaxLength="20" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">NAMA TENGAH</td>
                            <td>
                                <asp:TextBox ID="TXT_NMMID" runat="server" CssClass="ASPTextBox" MaxLength="20" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">TGL LAHIR</td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TXT_DOB" runat="server" CssClass="ASPTextBox" Width="70px" Style="text-align: center;"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB">
                                        </ajaxToolkit:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 120px;">EMPLOYEE ID</td>
                            <td>
                                <asp:TextBox ID="TXT_EMPID" runat="server" CssClass="ASPTextBox" MaxLength="20" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">EMAIL</td>
                            <td>
                                <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">ATASAN</td>
                            <td>
                                <asp:DropDownList ID="DDL_UPLINER" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">CABANG</td>
                            <td>
                                <asp:DropDownList ID="DDL_BRANCH" runat="server" CssClass="ASPDropDownList">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>JABATAN</td>
                            <td>
                                <asp:TextBox ID="TXT_TITLE" runat="server" CssClass="ASPTextBox" MaxLength="20" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="TR_ACC" runat="server" visible="false" style="vertical-align: top;">
                            <td class="auto-style5">TRACK</td>
                            <td>
                                <asp:Label ID="LB_STATUS" runat="server" Font-Bold="True"></asp:Label>
                                <br />

                            </td>
                        </tr>

                        </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR_REK" runat="server" BackColor="LightGoldenrodYellow"
                        BorderColor="Tan" BorderWidth="1px" CellPadding="2"  Font-Size="X-Small" GridLines="None"
                        PageSize="20" AutoGenerateColumns="False" ShowHeader="False" ForeColor="Black">
                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <AlternatingItemStyle BackColor="PaleGoldenrod" />
                        <ItemStyle Wrap="False" VerticalAlign="Top" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <Columns>
                            <asp:BoundColumn DataField="ACCNO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCBANK" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACCNAME" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ACTIVE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="VIRTUAL_ACC" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="ACC#">
                                <ItemTemplate>
                                    <table style="border-spacing: 0px;">
                                        <tr>
                                            
                                            <td style="width: 180px;">ACC#</td>
                                            <td>
                                                <asp:TextBox ID="TXT_ACC" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="150px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>BANK</td>
                                            <td>
                                                <asp:DropDownList ID="DDL_BANK" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>NAMA</td>
                                            <td>
                                                <asp:TextBox ID="TXT_ACCNAMA" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="250px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <strong>VIRT.ACC</strong>?<asp:CheckBox ID="CB_VA" runat="server" CssClass="ASPButton" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>ACTIVE</strong>?
                                                <asp:CheckBox ID="CB_ACC" runat="server" CssClass="ASPButton" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="Tan" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" OnClick="BT_SAVE_Click" Text="SAVE" Height="20px" Font-Bold="True" Width="100px" />
                    <asp:Label ID="LB_ERR" runat="server" CssClass="ASPLabel" ForeColor="Red" Font-Bold="True"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr id="TR_PHOTO" runat="server" visible="false" style="vertical-align: top;">
                            <td class="auto-style5">FOTO<br />
                                <asp:FileUpload ID="FILEUPLOAD" runat="server" CssClass="ASPTextBox" Width="110px" /><br />
                                <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" Text="UPLOAD" OnClick="BT_UPLOAD_Click" /><br />
                                <asp:Button ID="BT_CLEAR" runat="server" CssClass="ASPButton" Text="CLEAR" OnClick="BT_CLEAR_Click" Visible="False" />
                            </td>
                            <td>
                                <asp:Image ID="IMG_PHOTO" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

    </form>
</body>
</html>
