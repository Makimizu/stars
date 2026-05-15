<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationHistoryPos.aspx.cs" Inherits="LIFE.Form_App.ApplicationHistoryPos" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style>
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
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
            <table id="TBL_MEMBER" runat="server" style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td>
                        <asp:DataGrid ID="DGR_HISTORY" runat="server" ItemStyle-Wrap="true" AutoGenerateColumns="False" OnItemCommand="DGR_HISTORY_ItemCommand" Width="100%" CssClass="ASPDatagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <EditItemStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                            <ItemStyle Wrap="True" VerticalAlign="Top" BackColor="#E3EAEB" />
                            <AlternatingItemStyle BackColor="White" />
                            <Columns>
                                 <asp:BoundColumn DataField="REGNO"  Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SEQ"  Visible="false"></asp:BoundColumn>
                               <asp:TemplateColumn HeaderText="ENDORSEMENT TYPE">
                                    <ItemTemplate>
                                        <asp:LinkButton 
                                            ID="lnkEndorsement" 
                                            runat="server"
                                            Text='<%# Eval("ENDORSEMENT_TYPE_DESCR") %>'
                                            CommandName="ViewReport"
                                            CommandArgument='<%# Eval("REGNO") + "|" + Eval("SEQ") + "|"  + Eval("ENDORSEMENT_TYPE_DESCR") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="TRACK_DESCR" HeaderText="STATUS"></asp:BoundColumn>                                
                                <asp:BoundColumn DataField="REGISTERED_DATE" HeaderText="REGISTERED DATE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PENDING_DATE" HeaderText="PENDING DATE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="STOP_PENDING_DATE" HeaderText="STOP PENDING DATE"></asp:BoundColumn>  
                                <asp:BoundColumn DataField="PENDING_DATE_EXTERNAL" HeaderText="PENDING DATE EXTERNAL"></asp:BoundColumn>   
                                <asp:BoundColumn DataField="VERIFIED_DATE" HeaderText="VERIFIED DATE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="AUTHORIZED_DATE" HeaderText="AUTHORIZED DATE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="REMARK" HeaderText="REMARK"></asp:BoundColumn>
                            </Columns>
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        </asp:DataGrid>
                        <asp:Label ID="LB_WARNING" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
    </form>
</body>
</html>
