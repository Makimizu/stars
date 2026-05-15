<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationMCU.aspx.cs" Inherits="GLIFE.Form_App.ApplicationMCU" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <style type="text/css">
        * {
            box-sizing: border-box;
        }

        * {
            text-shadow: none !important;
            box-shadow: none !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 33%;">
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
                                            <asp:Label ID="LB_NAME" runat="server" Font-Bold="True"></asp:Label></td>
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
                                </table>
                            </td>
                            <td style="width: 33%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">POLICY NO</td>
                                        <td>
                                            <asp:Label ID="LB_POLICYNO" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>POLICY HOLDER</td>
                                        <td>
                                            <asp:Label ID="LB_COMPANY" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>START DATE</td>
                                        <td>
                                            <asp:Label ID="LB_STARTDATE" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>PRODUCT</td>
                                        <td>
                                            <asp:Label ID="LB_PRODUCT" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>U/W CODE</td>
                                        <td>
                                            <asp:Label ID="LB_UWCODE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SUM INSURED</td>
                                        <td>
                                            <asp:Label ID="LB_SUMINS" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 33%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 160px;">TOTAL LABORATORY</td>
                                        <td>
                                            <asp:Label ID="LB_TOTALLAB" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>TOTAL ITEMS/PACKAGES</td>
                                        <td>
                                            <asp:Label ID="LB_TOTALITEM" runat="server" Font-Bold="True"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>TOTAL ESTIMATION CHARGE</td>
                                        <td>
                                            <asp:Label ID="LB_TOTALCHARGE" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center; width: 33%;">REQUESTED BY SYSTEM
                            </td>
                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center; width: 33%;">MEDICAL LAB ITEMS/PACKAGES
                            </td>
                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">SELECTED ITEMS
                            </td>
                        </tr>
                        <tr style="vertical-align: top;">
                            <td>
                                <asp:DataGrid ID="DGR_DOCS" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" Width="100%" CssClass="ASPDatagrid">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DOCTYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NAMAFILE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RECEIVE_DATE" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Label ID="LB_DESCR" runat="server" Visible="False"></asp:Label>
                                                <asp:LinkButton ID="LBT_DESCR" runat="server" CommandName="Download" Visible="False" ToolTip="Download"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                </asp:DataGrid>

                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center">MEDICAL QUESTIONS
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="TBL_QUESTION" runat="server" visible="false" style="border-spacing: 0px; width: 100%;">
                                                <tr>
                                                    <td>MEDICAL QUESTION FOR :</td>
                                                    <td style="text-align: right">
                                                        <asp:DropDownList ID="DDL_QUESTION" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_QUESTION_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:DataGrid ID="DGR_QUESTION" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Names="Tahoma" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%" CssClass="ASPDatagrid" Font-Size="X-Small">
                                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                                <ItemStyle BackColor="#EAFFEA" ForeColor="#666666" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NEXT_QUESTION" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="VALUE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="VALUE_NEXT" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                                        <ItemStyle HorizontalAlign="Right" Width="30px" />
                                                    </asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <table style="border-spacing: 0px; width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <table style="border-spacing: 0px; width: 100%;">
                                                                            <tr style="vertical-align: top;">
                                                                                <td>
                                                                                    <asp:Label ID="LB_QUESTDESCR" runat="server" /></td>
                                                                                <td style="text-align: right;">
                                                                                    <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                                                                    <asp:TextBox ID="TXT_VAL" runat="server" CssClass="ASPTextBox" Visible="False" Width="150px" MaxLength="255"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr id="TR_NEXTQUESTION" runat="server" visible="false">
                                                                    <td>
                                                                        <table style="border-spacing: 0px; width: 100%;">
                                                                            <tr style="vertical-align: top; border-top: ridge;">
                                                                                <td>
                                                                                    <asp:Label ID="LB_QUESTION" runat="server" Visible="false" /></td>
                                                                                <td style="text-align: right;">
                                                                                    <asp:TextBox ID="TXT_NEXTVAL" runat="server" CssClass="ASPTextBox" Width="300px" MaxLength="255" Visible="false"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemStyle HorizontalAlign="Right" Width="20px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="LB_UNCHECKED" runat="server" ForeColor="Red" Visible="false" Text="V" Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>

                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">MEDICAL LAB</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MEDLAB" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_MEDLAB_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>YEAR OF PRICE</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MEDLABYEAR" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_MEDLABYEAR_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <asp:DataGrid ID="DGR_MEDLAB" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_MEDLAB_ItemCommand">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DOC_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="NAME">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBT_DESCR" runat="server" CommandName="Select"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="TYPE" HeaderText="TYPE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="PRICE" HeaderText="PRICE">
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                </asp:DataGrid>
                            </td>
                            <td>
                                <asp:DataGrid ID="DGR_SELECTED" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" Width="100%" CssClass="ASPDatagrid">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="COMPANY_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="COMPANY_NAME" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AMOUNT" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <table style="border-spacing: 0px; width: 100%; border-bottom: ridge;">
                                                    <tr>
                                                        <td style="width: 100px;">MEDICAL LAB</td>
                                                        <td>
                                                            <asp:Label ID="LB_LABNAME" runat="server" Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="LB_LABCODE" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>TOTAL CHARGE</td>
                                                        <td>
                                                            <asp:Label ID="LB_LABAMOUNT" runat="server" ForeColor="Red"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Button ID="BT_SENDEMAIL" runat="server" CssClass="ASPButton" Text="EMAIL TO" Width="100%" CommandName="Email" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="BT_EMAIL" runat="server" CssClass="ASPTextBox" Width="95%"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />

                                                <asp:DataGrid ID="DGR_APPMED" runat="server" GridLines="None" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" ShowHeader="False" OnItemCommand="DGR_APPMED_ItemCommand">
                                                    <ItemStyle Wrap="True" VerticalAlign="Top" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="DOC_CODE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="COMPANY_CODE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="PRICE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="TYPE">
                                                            <ItemStyle Font-Bold="true" Width="100px" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCR"></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemStyle Width="160px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="TXT_PRICE" runat="server" CssClass="ASPTextBoxNumber" Width="80px"></asp:TextBox>
                                                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="S" BackColor="Blue" ForeColor="White" CommandName="Save" ToolTip="Save" />
                                                                <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" Text="X" BackColor="Red" ForeColor="White" CommandName="Delete" ToolTip="Delete" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                                <br />

                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

