<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationHeader.aspx.cs" Inherits="GLIFE.Form_App.ApplicationHeader" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style>
        .empty {
            display: block;
            height: 40px;
        }
        .alert {
            display: block;
            border: 3px solid red;
            padding: 10px;
            animation: blinker 5s linear infinite;
            color: red;
        }

        @keyframes blinker {
            25% {
                opacity: 0.5;
            }
            50% {
                opacity: 0;
            }
            75% {
                opacity: 0.5;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 25%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">REGNO</td>
                                        <td>
                                            <asp:Label ID="LB_REGNO" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>FULL NAME</td>
                                        <td>
                                            <asp:Label ID="LB_NAME" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="LB_MEMBERID" runat="server" Font-Bold="False" Visible="False"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>DATE OF BIRTH</td>
                                        <td>
                                            <asp:Label ID="LB_DOB" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>GENDER</td>
                                        <td>
                                            <asp:Label ID="LB_GENDER" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr id="TR_PENDING" runat="server">
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_PENDING" runat="server" CssClass="ASPButton" Text="PENDING" Width="100px" BackColor="Yellow" ForeColor="Red" OnClick="BT_PENDING_Click" /></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 35%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">POLICY NO</td>
                                        <td>
                                            <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>POLICY HOLDER</td>
                                        <td style="text-wrap: normal;">
                                            <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT</td>
                                        <td>
                                            <asp:Label ID="LB_PRODUCT" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Label ID="LB_TC_ID" runat="server" Font-Bold="False" Visible="False"></asp:Label>
                                            <asp:Label ID="LB_TC_GROUP" runat="server" Font-Bold="False" Visible="False"></asp:Label>
                                            <asp:Label ID="LB_READONLY" runat="server" Font-Bold="False" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BRANCH</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_BRANCH" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_BRANCH_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>AGENT NAME</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_AGENT" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_AGENT_SelectedIndexChanged"></asp:DropDownList>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>CHANNEL</td>
                                        <td>
                                            <asp:Label ID="LB_CHANNEL" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                            <td style="text-align: right; width: 40%;">

                                <asp:DataGrid ID="DGR_TRACK" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_TRACK_ItemCommand" ShowHeader="False">
                                    <ItemStyle Wrap="True" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="COMMENT" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%;">
                                                    <tr style="vertical-align: top;">
                                                        <td style="width: 100px;">
                                                            <asp:Button ID="BT_TRACK" runat="server" CommandName="Next" CssClass="ASPButton" Width="100px" /></td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_COMMENT" runat="server" Height="25px" Width="98%" Visible="false" MaxLength="1000" TextMode="MultiLine" CssClass="ASPTextBox" placeholder="Type your reason comment ..."></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>

                                <table id="TBL_STAT" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px; text-align: left;">STATUS</td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="LB_STAT" runat="server" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td style="text-align: left;">COMMENT</td>
                                        <td style="text-align: left;">
                                            <asp:Label ID="LB_STATCOMMENT" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                    <asp:Label ID="LB_WARNING" runat="server" CssClass="empty"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td style="width: 16%;">
                                <asp:Button ID="BT1" runat="server" CssClass="ASPButton" Text="MAIN INFO" Width="100%" OnClick="BT1_Click" />
                            </td>
                            <td style="width: 16%;">
                                <asp:Button ID="BT2" runat="server" CssClass="ASPButton" Text="BENEFIT & SHARE" Width="100%" OnClick="BT2_Click" />
                            </td>
                            <td style="width: 16%;">
                                <asp:Button ID="BT6" runat="server" CssClass="ASPButton" Text="PREMIUM" Width="100%" OnClick="BT6_Click" />
                            </td>
                            <td style="width: 16%;">
                                <asp:Button ID="BT5" runat="server" CssClass="ASPButton" Text="PRODUCT INFO" Width="100%" OnClick="BT5_Click" />
                            </td>
                            <td style="width: 16%;">
                                <asp:Button ID="BT3" runat="server" CssClass="ASPButton" Text="REQ. DOCUMENT & ARCHIEVE" Width="100%" OnClick="BT3_Click" />
                            </td>
                            <td style="width: 16%;">
                                <asp:Button ID="BT4" runat="server" CssClass="ASPButton" Text="REMARKS &amp; TRACKS" Width="100%" OnClick="BT4_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">
                    <asp:Label ID="LB_TITLE" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
            </tr>
        </table>

        <!--Plugins add by Ahmad Zulfahmi-->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
        <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
        <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

        <script>
            $('#<%=DDL_BRANCH.ClientID%>').select2({ width: "100%" });
            $('#<%=DDL_AGENT.ClientID%>').select2({ width: "100%" });
        </script>
        <!--Plugins add by Ahmad Zulfahmi-->

    </form>
</body>
</html>
