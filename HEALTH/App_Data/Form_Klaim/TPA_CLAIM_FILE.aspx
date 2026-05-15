<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TPA_CLAIM_FILE.aspx.cs" Inherits="HEALTH.Form_Klaim.TPA_CLAIM_FILE" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="position: absolute; top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing:0px;">
                        <tr>
                            <td style="width:100px;">QUERY</td>
                            <td>
                                <asp:DropDownList ID="DDL_QUERY" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_QUERY_SelectedIndexChanged"></asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" OnClick="BT_SAVE_Click" Text="SAVE" Width="100px" />
                    <asp:Label ID="LB_TPA" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_BATCH" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_TIPE" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="LB_RECORD" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr id="TR_ACTION" runat="server" visible="false">
                <td>
                    <br />
                    <asp:DropDownList ID="DDL_DESTINATION" runat="server" CssClass="ASPDropDownList">
                    </asp:DropDownList>
                    &nbsp;<asp:Button ID="BT_COPY" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_COPY_Click" Text="&lt;" />
                    &nbsp;<asp:Button ID="BT_COPY0" runat="server" BackColor="Orange" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_COPY0_Click" Text="&lt;+" />
                    &nbsp;<asp:DropDownList ID="DDL_SOURCE" runat="server" CssClass="ASPDropDownList">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataGrid ID="DGR" runat="server" CellPadding="4" CssClass="ASPDatagrid" GridLines="Vertical" ForeColor="#333333" PageSize="15" AutoGenerateColumns="False" BorderColor="#000099">
                        <EditItemStyle BackColor="#7C6F57" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#E3EAEB" Wrap="False" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"
                            Wrap="False" />
                        <FooterStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                        <Columns>
                            <asp:BoundColumn DataField="BATCH_ID" HeaderText="BATCH_ID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SEQ" HeaderText="SEQ"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F01" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F02" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F03" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F04" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F05" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F06" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F07" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F08" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F09" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F10" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F11" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F12" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F13" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F14" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F15" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F16" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F17" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F18" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F19" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F20" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F21" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F22" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F23" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F24" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F25" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F26" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F27" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F28" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F29" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F30" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F31" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F32" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F33" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F34" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F35" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F36" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F37" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F38" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F39" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F40" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F41" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F42" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F43" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F44" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F45" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F46" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F47" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F48" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F49" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F50" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F51" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F52" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F53" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F54" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F55" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F56" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F57" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F58" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F59" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="F60" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="F01">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT01" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F02">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT02" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F03">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT03" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F04">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT04" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F05">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT05" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F06">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT06" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F07">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT07" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F08">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT08" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F09">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT09" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F10">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT10" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F11">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT11" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F12">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT12" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F13">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT13" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F14">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT14" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F15">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT15" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F16">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT16" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F17">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT17" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F18">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT18" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F19">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT19" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F20">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT20" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F21">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT21" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F22">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT22" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F23">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT23" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F24">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT24" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F25">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT25" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F26">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT26" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F27">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT27" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F28">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT28" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F29">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT29" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F30">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT30" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F31">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT31" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F32">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT32" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F33">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT33" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F34">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT34" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F35">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT35" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F36">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT36" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F37">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT37" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F38">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT38" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F39">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT39" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F40">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT40" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F41">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT41" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F42">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT42" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F43">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT43" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F44">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT44" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F45">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT45" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F46">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT46" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F47">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT47" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F48">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT48" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F49">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT49" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F50">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT50" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F51">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT51" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F52">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT52" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F53">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT53" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F54">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT54" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F55">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT55" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F56">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT56" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F57">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT57" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F58">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT58" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F59">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT59" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F60">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT60" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="150px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>

