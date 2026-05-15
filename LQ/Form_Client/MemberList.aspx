<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberList.aspx.cs" Inherits="LQ.Client.MemberList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../include/css/tabpanel_ext.css" rel="stylesheet" />
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../include/css/controls.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card text-dark bg-transparent o-hidden h-100" style="border-color: silver;">
                    <a href="#EXISTING" role="tab" data-toggle="tab">
                        <div class="card-body" style="padding-top: 0px; padding-bottom: 0px;">
                            <center>
                        <table style="width: 100%; color: green;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="font-size: 20pt; width: 50px;"><i class="fa fa-list"></i></td>
                                            <td style="text-align: left; font-size: 12pt;">SEARCH EXISTING CUSTOMER&nbsp;:&nbsp;
                                                <asp:Label ID="LB_RESULT" runat="server" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:TextBox ID="TXT_FULLNAME_SEARCH" runat="server" AutoPostBack="true" CssClass="TextBox" Width="50%" placeholder="Search Name" OnTextChanged="TXT_FULLNAME_SEARCH_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        </center>
                        </div>
                    </a>
                </div>
            </div>
            <div class="col-xl-6 col-sm-6 mb-3">
                <div class="card text-dark bg-transparent o-hidden h-100" style="border-color: silver; padding: 0px;">
                    <a href="MemberEntry.aspx?ID=&QUOT=0">
                        <div class="card-body" style="padding-top: 0px; padding-bottom: 0px;">
                            <center>
                        <table style="width: 100%; color: blue;">
                            <tr>
                                <td>
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="font-size: 20pt; width: 50px;"><i class="fa fa-edit"></i></td>
                                            <td style="text-align: left; font-size: 12pt;">NEW CUSTOMER
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        </center>
                        </div>
                    </a>
                </div>
            </div>


        </div>


        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:UpdatePanel ID="UPD1" runat="server">
            <ContentTemplate>
                <table style="border-spacing: 0px; width: 100%;">

                    <tr>
                        <td>
                            <asp:DataGrid ID="DGR" runat="server" BackColor="White" AllowPaging="True"
                                BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical"
                                PageSize="20" OnItemCommand="DGR1_ItemCommand"
                                OnPageIndexChanged="DGR1_PageIndexChanged" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False">
                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="FULL NAME">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBT_FULLNAME" runat="server" CommandName="Select"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" />
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="ID_NO" HeaderText="ID CARD NO"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SEX" HeaderText="GENDER">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PROVINCE" HeaderText="PROVINCE"></asp:BoundColumn>
                                </Columns>
                                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                                    Mode="NumericPages" />
                            </asp:DataGrid>

                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
