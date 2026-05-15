<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_LIST.aspx.cs" Inherits="AGR.PARAMETER_LIST" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../include/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>

        <div class="row" style="width: 90%;">
           
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                    <tr>
                        <td style="width: 150px;">MODE</td>
                        <td>
                            <asp:TextBox ID="TXT_MODE" runat="server" CssClass="ASPTextBox"  Width="20px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>TYPE</td>
                        <td>
                            <asp:DropDownList ID="DDL_TYPE" runat="server"></asp:DropDownList> 

                         </td>
                    </tr>
                    <tr>
                        <td>DESCRIPTION</td>
                        <td>
                            <asp:TextBox ID="TXT_DESCRIPTION" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                        <tr> 
                        <td style="width: 100px;">START DATE</td> 
                        <td> 
                            <asp:TextBox ID="TXT_START_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox> 
                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_START_DATE"> 
                            </ajaxToolkit:CalendarExtender> 
                        </td> 
                    </tr> 
                     <tr> 
                        <td style="width: 100px;">END DATE</td> 
                        <td> 
                            <asp:TextBox ID="TXT_END_DATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox> 
                            <ajaxToolkit:CalendarExtender ID="ceDate2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_END_DATE"> 
                            </ajaxToolkit:CalendarExtender> 
                        </td> 
                    </tr> 
                    <tr>
                        <td style="width: 150px;">COMPANY CODE</td>
                        <td>
                            <asp:TextBox ID="TXT_COMPANY_CODE" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                                 <tr>
                                      <td> 
                                        <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ButtonColor" Width="80" OnClick="BT_SAVE_Click" /> 
                                     </td> 
                                </tr>                      
                </table>
            </div>           
        </div>

        <asp:DataGrid ID="DGR" runat="server" CellPadding="4" PageSize="20" 
                    GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"> 
                    <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" /> 
                    <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" /> 
                    <AlternatingItemStyle BackColor="White" /> 
                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" /> 
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" /> 
                    <Columns> 

                         <asp:TemplateColumn HeaderText="DESCRIPTION">
                        <ItemTemplate>
                        <asp:LinkButton ID="LB_CODE" runat="server" CommandName="Detail"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="100" />
                        </asp:TemplateColumn>

                         <asp:BoundColumn DataField="DESCR" HeaderText="TYPE"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="ID" HeaderText="ID" ></asp:BoundColumn> 
                          <asp:BoundColumn DataField="DESCRIPTION" HeaderText="DESCRIPTION" Visible="false" ></asp:BoundColumn> 
                        <asp:BoundColumn DataField="STARTDATE" HeaderText="START DATE"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="ENDDATE" HeaderText="END DATE"></asp:BoundColumn> 
                         <asp:BoundColumn DataField="COMPANY_CODE" HeaderText="COMPANY CODE"></asp:BoundColumn> 
                    </Columns> 
                    <EditItemStyle BackColor="#2461BF" /> 
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /> 
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" /> 
                </asp:DataGrid> 

        <div id="div1" runat="server">
            <div class="row" style="width: 90%;">
           
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                   <tr>
                        <td style="width: 150px;">ID</td>
                        <td>
                            <asp:TextBox ID="ID1" runat="server" CssClass="ASPTextBox"  Width="20px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">PRODUCT CODE</td>
                        <td>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="ASPTextBox"  Width="20px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>CHANNEL</td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList> 

                         </td>
                    </tr>
                    <tr>
                        <td>LEVEL</td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                      
                    <tr>
                        <td style="width: 150px;">YEAR</td>
                        <td>
                            <asp:TextBox ID="TextBox5" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">MIN AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">MAX AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">DESCRIPTION</td>
                        <td>
                            <asp:TextBox ID="TextBox26" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">NETT</td>
                        <td>
                            <asp:TextBox ID="TextBox27" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">COMMPCT</td>
                        <td>
                            <asp:TextBox ID="TextBox28" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">OR1</td>
                        <td>
                            <asp:TextBox ID="TextBox29" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">OR2</td>
                        <td>
                            <asp:TextBox ID="TextBox30" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">OR3</td>
                        <td>
                            <asp:TextBox ID="TextBox31" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">OR4</td>
                        <td>
                            <asp:TextBox ID="TextBox32" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">OR5</td>
                        <td>
                            <asp:TextBox ID="TextBox33" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">OR6</td>
                        <td>
                            <asp:TextBox ID="TextBox34" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">OR7</td>
                        <td>
                            <asp:TextBox ID="TextBox35" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">OR8</td>
                        <td>
                            <asp:TextBox ID="TextBox36" runat="server" CssClass="ASPTextBox" Width="50px"></asp:TextBox>
                        </td>
                    </tr>
                                 <tr>
                                      <td> 
                                        <asp:Button ID="Button1" runat="server" Text="SAVE" CssClass="ButtonColor" Width="80" OnClick="BT1_SAVE_Click" /> 
                                     </td> 
                                </tr>                      
                </table>
            </div>           
        </div>

        <asp:DataGrid ID="DGR1" runat="server" CellPadding="4" PageSize="20" 
                    GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"> 
                    <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" /> 
                    <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" /> 
                    <AlternatingItemStyle BackColor="White" /> 
                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" /> 
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" /> 
                    <Columns> 
                         <asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="PRODUCT_CODE" HeaderText="PRODUCT CODE"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="CHANNEL" HeaderText="CHANNEL"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="LEVEL" HeaderText="LEVEL"></asp:BoundColumn> 
                         <asp:BoundColumn DataField="YEAR" HeaderText="YEAR"></asp:BoundColumn> 
                    </Columns> 
                    <EditItemStyle BackColor="#2461BF" /> 
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /> 
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" /> 
                </asp:DataGrid> 

        </div>
        <div id="div2" runat="server">

 <div class="row" style="width: 90%;">
           
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                    <tr>
                        <td style="width: 150px;">PRODUCT CODE</td>
                        <td>
                            <asp:TextBox ID="TextBox6" runat="server" CssClass="ASPTextBox"  Width="20px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>CHANNEL</td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList> 

                         </td>
                    </tr>
                   
                     
                    <tr>
                        <td style="width: 150px;">LEVEL</td>
                        <td>
                            <asp:TextBox ID="TextBox10" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>

                      <tr>
                        <td style="width: 150px;">YEAR</td>
                        <td>
                            <asp:TextBox ID="TextBox8" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <td style="width: 150px;">MIN AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TextBox9" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">MAX AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TextBox37" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">DESCRIPTION</td>
                        <td>
                            <asp:TextBox ID="TextBox38" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <td style="width: 150px;">FIX_AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TextBox39" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">ALLOWANCEPCT</td>
                        <td>
                            <asp:TextBox ID="TextBox40" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                     
                                 <tr>
                                      <td> 
                                        <asp:Button ID="Button2" runat="server" Text="SAVE" CssClass="ButtonColor" Width="80" OnClick="BT2_SAVE_Click" /> 
                                     </td> 
                                </tr>                      
                </table>
            </div>           
        </div>

        <asp:DataGrid ID="DataGrid2" runat="server" CellPadding="4" PageSize="20" 
                    GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"> 
                    <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" /> 
                    <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" /> 
                    <AlternatingItemStyle BackColor="White" /> 
                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" /> 
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" /> 
                    <Columns> 
                         <asp:BoundColumn DataField="DESCR" HeaderText="TYPE"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="STARTDATE" HeaderText="START DATE"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="ENDDATE" HeaderText="END DATE"></asp:BoundColumn> 
                         <asp:BoundColumn DataField="COMPANY_CODE" HeaderText="COMPANY CODE"></asp:BoundColumn> 
                    </Columns> 
                    <EditItemStyle BackColor="#2461BF" /> 
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /> 
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" /> 
                </asp:DataGrid> 

        </div>
        <div id="div3" runat="server">
             <div class="row" style="width: 90%;">
           
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                 <tr>
                        <td style="width: 150px;">PRODUCT CODE</td>
                        <td>
                            <asp:TextBox ID="TextBox7" runat="server" CssClass="ASPTextBox"  Width="20px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>CHANNEL</td>
                        <td>
                            <asp:DropDownList ID="DropDownList3" runat="server"></asp:DropDownList> 

                         </td>
                    </tr>
                   
                     
                    <tr>
                        <td style="width: 150px;">LEVEL</td>
                        <td>
                            <asp:TextBox ID="TextBox11" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>

                      <tr>
                        <td style="width: 150px;">YEAR</td>
                        <td>
                            <asp:TextBox ID="TextBox12" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <td style="width: 150px;">MIN AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TextBox13" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">MAX AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TextBox14" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">DESCRIPTION</td>
                        <td>
                            <asp:TextBox ID="TextBox15" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <td style="width: 150px;">FIX_AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TextBox41" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">REWARDPCT</td>
                        <td>
                            <asp:TextBox ID="TextBox42" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                                 <tr>
                                      <td> 
                                        <asp:Button ID="Button3" runat="server" Text="SAVE" CssClass="ButtonColor" Width="80" OnClick="BT3_SAVE_Click" /> 
                                     </td> 
                                </tr>                      
                </table>
            </div>           
        </div>

        <asp:DataGrid ID="DataGrid3" runat="server" CellPadding="4" PageSize="20" 
                    GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"> 
                    <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" /> 
                    <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" /> 
                    <AlternatingItemStyle BackColor="White" /> 
                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" /> 
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" /> 
                    <Columns> 
                         <asp:BoundColumn DataField="DESCR" HeaderText="TYPE"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="STARTDATE" HeaderText="START DATE"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="ENDDATE" HeaderText="END DATE"></asp:BoundColumn> 
                         <asp:BoundColumn DataField="COMPANY_CODE" HeaderText="COMPANY CODE"></asp:BoundColumn> 
                    </Columns> 
                    <EditItemStyle BackColor="#2461BF" /> 
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /> 
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" /> 
                </asp:DataGrid> 


         </div>
        <div id="div4" runat="server">

       

        </div>
        <div id="div5" runat="server">

             <div class="row" style="width: 90%;">
           
            <div class="col-xl-6 col-sm-6 mb-3">
                <table style="border-spacing: 0px; width: 100%; font-size: x-small;">
                   <tr>
                        <td style="width: 150px;">PRODUCT CODE</td>
                        <td>
                            <asp:TextBox ID="TextBox21" runat="server" CssClass="ASPTextBox"  Width="20px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>CHANNEL</td>
                        <td>
                            <asp:DropDownList ID="DropDownList5" runat="server"></asp:DropDownList> 

                         </td>
                    </tr>
                   
                     
                    <tr>
                        <td style="width: 150px;">LEVEL</td>
                        <td>
                            <asp:TextBox ID="TextBox22" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>

                      <tr>
                        <td style="width: 150px;">YEAR</td>
                        <td>
                            <asp:TextBox ID="TextBox23" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <td style="width: 150px;">MIN AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TextBox24" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 150px;">MAX AMOUNT</td>
                        <td>
                            <asp:TextBox ID="TextBox25" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 150px;">DESCRIPTION</td>
                        <td>
                            <asp:TextBox ID="TextBox43" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <td style="width: 150px;">DECISION_CODE</td>
                        <td>
                            <asp:TextBox ID="TextBox44" runat="server" CssClass="ASPTextBox" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                   
                                 <tr>
                                      <td> 
                                        <asp:Button ID="Button5" runat="server" Text="SAVE" CssClass="ButtonColor" Width="80" OnClick="BT5_SAVE_Click" /> 
                                     </td> 
                                </tr>                      
                </table>
            </div>           
        </div>

        <asp:DataGrid ID="DataGrid5" runat="server" CellPadding="4" PageSize="20" 
                    GridLines="None" CssClass="ASPDatagrid" AutoGenerateColumns="False" Width="90%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"> 
                    <ItemStyle Wrap="False" BackColor="#EFF3FB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" /> 
                    <SelectedItemStyle BackColor="#D1DDF1" ForeColor="#333333" Font-Bold="True" /> 
                    <AlternatingItemStyle BackColor="White" /> 
                    <ItemStyle BackColor="#EEEEEE" ForeColor="Black" /> 
                    <HeaderStyle BackColor="#507CD1" Font-Bold="False" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="X-Small" /> 
                    <Columns> 
                         <asp:BoundColumn DataField="DESCR" HeaderText="TYPE"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="DESCRIPTION" HeaderText="DESCRIPTION"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="STARTDATE" HeaderText="START DATE"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="ENDDATE" HeaderText="END DATE"></asp:BoundColumn> 
                         <asp:BoundColumn DataField="COMPANY_CODE" HeaderText="COMPANY CODE"></asp:BoundColumn> 
                    </Columns> 
                    <EditItemStyle BackColor="#2461BF" /> 
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" /> 
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" /> 
                </asp:DataGrid> 


        </div>
        <div id="div6" runat="server">
         </div>

    </form>
</body>
</html>
