<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Parameter_More_Adv.aspx.cs" Inherits="CUSTOMERS.Form_Parameter.Parameter_More_Adv" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
    <style>
        .hidden {
            display: none;
        }
     </style>
</head>
<body>
    <script type="text/javascript">
    var dotInterval;

    function startDotLoading() {
        var label = document.getElementById('<%= LB_LOADING.ClientID %>');
        var dots = 0;

        dotInterval = setInterval(function () {
            dots = (dots + 1) % 4; // 0 to 3 dots
            label.innerText = 'Proses upload sedang berlangsung' + '.'.repeat(dots);
        }, 500);
    }

    function showLoading() {
        var label = document.getElementById('<%= LB_LOADING.ClientID %>');
        if (label) {
            label.classList.remove('hidden');
        }
    }
    function hideLoading() {
        var label = document.getElementById('<%= LB_LOADING.ClientID %>');
        if (label) {
            label.classList.add('hidden');
        }
    }

    function setGridWidth() {
        var screenWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
        screenWidth = screenWidth - 18;
        console.log("Screen width: " + screenWidth);

        document.getElementById('<%= LB_DGR.ClientID %>').style.maxWidth = screenWidth + 'px';
    }
    function CheckboxChange(cb, id) {
        var isChecked = cb.checked;
        
        PageMethods.UpdateCheckBox(id, isChecked, function (response) {
                //var cb = document.getElementById(headerCheckboxId);
                //if (cb) cb.checked = false;
                __doPostBack('UncheckHeader', '');
                console.log("Updated successfully.");
            },
            function (error) {
                console.log("Error: " + error.get_message());
            }
        );
    }
     
    function uncheckHeader() {
        var cb = document.getElementById(headerCheckboxId);
        if (cb) cb.checked = false;
    }
    </script>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server" EnablePageMethods="true">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="top: 0px; left: 0px; border-spacing: 0px; width: 100%;">
            <tr style="vertical-align: top;">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="LB_PARAM" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                    Font-Size="Small"></asp:Label>
                                <asp:Label ID="LB_PREFIX" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="LB_CODE" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small">UPLOAD</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr style="color:#4A3C8C;background-color:#FFFFCC;font-weight:normal;font-style:normal;text-decoration:none;">
                                        <td style="width: 150px;">DOWNLOAD TEMPLATE</td>
                                        <td>
                                            <asp:Button ID="BT_XLS" runat="server" CssClass="ASPButton" Text="XLS" Font-Bold="True" ForeColor="White" BackColor="Green" Visible="True" OnClick="BT_XLS_Click" />
                                        </td>
                                    </tr>
                                    <tr style="color:#4A3C8C;background-color:#CCFFCC;font-weight:normal;font-style:normal;text-decoration:none;">
                                        <td>UPLOAD EXCEL FILE<br />(.XLS, .CSV, .XLSX)</td>
                                        <td>
                                            <asp:FileUpload ID="FU" runat="server" CssClass="ASPTextBox" />
                                        </td>
                                    </tr>
                                    <tr style="color:#4A3C8C;background-color:#FFF;font-weight:normal;font-style:normal;text-decoration:none;">
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_UPLOAD" runat="server" CssClass="ASPButton" Width="100px" OnClick="BT_UPLOAD_Click" OnClientClick="showLoading();startDotLoading();" Text="Upload" EnableViewState="true" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                             <td>
                                 <br />
                             </td>
                         </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LB_LOADING" runat="server" CssClass="hidden" Font-Size="Large"></asp:Label>
                                <asp:Label ID="LB_ERR" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="LB_RECORD" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                <asp:Label ID="LB_DGRUPLOAD" runat="server" style="display:block;overflow:auto;">
                                   <asp:DataGrid ID="DGRUPLOAD" runat="server" CellPadding="2" PageSize="100"
                                           GridLines="None" CssClass="ASPDatagrid" OnItemDataBound="DGRUPLOAD_ItemDataBound"
                                           OnPageIndexChanged="DGRUPLOAD_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" 
                                           Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                                           <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                           <AlternatingItemStyle BackColor="White" />
                                           <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                           <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                           <EditItemStyle BackColor="#2461BF" />
                                           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                           <Columns>
                                               <asp:BoundColumn DataField="F1" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F2" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F3" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F4" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F5" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F6" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F7" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F8" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F9" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F10" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F11" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F12" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F13" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F14" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F15" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F16" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F17" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F18" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F19" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="F20" Visible="false"></asp:BoundColumn>
                                               <asp:BoundColumn DataField="STATUS" Visible="false"></asp:BoundColumn>
                                           </Columns>
           
                                           <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                       </asp:DataGrid>
                                   </asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="TDBGColor">
                                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small">APPROVAL</asp:Label>
                            </td>
                        </tr>
                        <tr>
                             <td style="position:relative;"">
                                 <asp:Label ID="LB_DGR_ERR" runat="server" ForeColor="Red"></asp:Label>
                                 <asp:Label ID="LB_DGR_RECORD" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                 <asp:Label ID="LB_DGR" runat="server" style="display:block;overflow:auto;">
                                    <asp:DataGrid ID="DGR" runat="server" CellPadding="2" PageSize="10"
                                            GridLines="None" CssClass="ASPDatagrid" OnItemDataBound="DGR_ItemDataBound" OnItemCommand="DGR_ItemCommand"
                                            OnPageIndexChanged="DGR_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False" 
                                            Width="100%" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333">
                                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <AlternatingItemStyle BackColor="White" />
                                            <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditItemStyle BackColor="#2461BF" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <Columns>
                                                <asp:BoundColumn DataField="F1" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F2" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F3" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F4" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F5" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F6" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F7" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F8" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F9" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F10" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F11" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F12" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F13" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F14" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F15" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F16" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F17" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F18" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F19" Visible="false"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="F20" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn Visible="false">
                                                    <ItemTemplate>
                                                        <%--<asp:CheckBox ID="CheckBox1" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("CHECKED")) %>' onclick='<%# "CheckboxChange(this, \"" + DataBinder.Eval(Container.DataItem, "F1", "{0:000}") + "\")" %>' />--%>
                                                        <asp:CheckBox ID="CHK_APPROVE" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("CHECKED")) %>' AutoPostBack="true" OnCheckedChanged="CHK_APPROVE_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="CHECKED" Visible="false"></asp:BoundColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <asp:Button ID="BT_EDITITEM" runat="server" CommandName="Edit" CssClass="ASPButton" Text="E" ForeColor="White" BackColor="Green" />
                                                        <asp:Button ID="BT_DELITEM" runat="server" CommandName="Delete" CssClass="ASPButton" Text="X" ForeColor="White" BackColor="Red" OnClientClick="if(!confirm('ARE YOU SURE TO DELETE ?')){return false;};" />
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                            
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
                                        </asp:DataGrid>
                                    </asp:Label>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" style="display:block;text-align:center;">
                                        <asp:Button ID="BT_APPROVE_ALL" runat="server" CssClass="ASPButton" Text="APPROVE" ForeColor="White" BackColor="Green" Font-Size="Small" OnClick="BT_APPROVE_ALL_Click" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="BT_DEL_ALL" runat="server" CssClass="ASPButton" Text="HAPUS" ForeColor="White" BackColor="Red" Font-Size="Small" OnClick="BT_DEL_ALL_Click" Visible="false" />
                                    </asp:Label>
                                    <br />
                                    
                                    <div style="background-color: transparent !important; opacity: 0.9;">
                                        <asp:Panel ID="PNL_POPUP" runat="server" BackColor="White" Height="350px" Width="80%" Style="z-index: 111; background-color: White; position: absolute; left: 0px; top: 0px; border: outset 2px gray; padding: 5px; display: none; overflow: auto;">
                                            <asp:LinkButton ID="LB_BACK" runat="server" ForeColor="Red" Font-Size="X-Small" OnClientClick="document.getElementById('PNL_POPUP').style.display = 'none';return false;">Close ..</asp:LinkButton>
                                            <table style="border-spacing: 0px; font-size: x-small; font-family: Tahoma; width: 100%;">
                                            <asp:Repeater ID="Repeater1" runat="server">
                                                <ItemTemplate>
                                                    <tr id="TR_F1" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LB_F1" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_F1" runat="server" Text="" Visible="false" ReadOnly="true"></asp:TextBox>
                                                            <asp:DropDownList ID="DDL_F1" runat="server" Visible="false"></asp:DropDownList>
                                                            <asp:HiddenField ID="HDN_F1" runat="server" Value='<%# Eval("F1") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr id="TR_F2" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LB_F2" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_F2" runat="server" Text="" Visible="false"></asp:TextBox>
                                                            <asp:DropDownList ID="DDL_F2" runat="server" Visible="false"></asp:DropDownList>
                                                            <asp:HiddenField ID="HDN_F2" runat="server" Value='<%# Eval("F2") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr id="TR_F3" runat="server" visible="false">
                                                        <td>
                                                            <asp:Label ID="LB_F3" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TXT_F3" runat="server" Text="" Visible="false"></asp:TextBox>
                                                            <asp:DropDownList ID="DDL_F3" runat="server" Visible="false"></asp:DropDownList>
                                                            <asp:HiddenField ID="HDN_F3" runat="server" Value='<%# Eval("F3") %>' />
                                                        </td>
                                                    </tr>
                                                        <tr id="TR_F4" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F4" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F4" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F4" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F4" runat="server" Value='<%# Eval("F4") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F5" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F5" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F5" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F5" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F5" runat="server" Value='<%# Eval("F5") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F6" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F6" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F6" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F6" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F6" runat="server" Value='<%# Eval("F6") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F7" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F7" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F7" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F7" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F7" runat="server" Value='<%# Eval("F7") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F8" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F8" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F8" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F8" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F8" runat="server" Value='<%# Eval("F8") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F9" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F9" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F9" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F9" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F9" runat="server" Value='<%# Eval("F9") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F10" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F10" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F10" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F10" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F10" runat="server" Value='<%# Eval("F10") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F11" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F11" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F11" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F11" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F11" runat="server" Value='<%# Eval("F11") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F12" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F12" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F12" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F12" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F12" runat="server" Value='<%# Eval("F12") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F13" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F13" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F13" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F13" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F13" runat="server" Value='<%# Eval("F13") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F14" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F14" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F14" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F14" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F14" runat="server" Value='<%# Eval("F14") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F15" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F15" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F15" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F15" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F15" runat="server" Value='<%# Eval("F15") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F16" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F16" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F16" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F16" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F16" runat="server" Value='<%# Eval("F16") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F17" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F17" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F17" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F17" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F17" runat="server" Value='<%# Eval("F17") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F18" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F18" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F18" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F18" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F18" runat="server" Value='<%# Eval("F18") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F19" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F19" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F19" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F19" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F19" runat="server" Value='<%# Eval("F19") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr id="TR_F20" runat="server" visible="false">
                                                            <td>
                                                                <asp:Label ID="LB_F20" runat="server" Text="" Visible="false"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="TXT_F20" runat="server" Text="" Visible="false"></asp:TextBox>
                                                                <asp:DropDownList ID="DDL_F20" runat="server" Visible="false"></asp:DropDownList>
                                                                <asp:HiddenField ID="HDN_F20" runat="server" Value='<%# Eval("F20") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td><br /></td>
                                                            <td><br /></td>
                                                        </tr>
                                                        <tr id="TR_F21" runat="server" visible="false">
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton" Text="SIMPAN" ForeColor="White" BackColor="Green" OnClick="BT_SAVE_Click" Visible="false" />&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="BT_DEL" runat="server" CssClass="ASPButton" Text="HAPUS" ForeColor="White" BackColor="Red" OnClick="BT_DEL_Click" OnClientClick="if(!confirm('ARE YOU SURE TO DELETE ?')){return false;};" Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                            </asp:Repeater>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </td>
                         </tr>
                         <tr>
                             <td class="TDBGColor">
                                 <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="Small">LIST</asp:Label>
                             </td>
                         </tr>
                        <tr>
                            <td>
                                <asp:Label ID="LB_DGRQUERY_ERR" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="LB_DGRQUERY_RECORD" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                <asp:DataGrid ID="DGRQUERY" runat="server" PageSize="10" OnPageIndexChanged="DGRQUERY_PageIndexChanged" AllowPaging="True" AutoGenerateColumns="False"
                                    CellPadding="4" Font-Names="Tahoma" Font-Size="X-Small" OnItemCommand="DGRQUERY_ItemCommand" OnItemCreated="DGRQUERY_ItemCreated"
                                    GridLines="Vertical" ForeColor="#333333" BorderColor="#6699FF" OnItemDataBound="DGRQUERY_ItemDataBound">
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#EFF3FB" HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <Columns>
                                        <asp:BoundColumn DataField="F1" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F2" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F3" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F4" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F5" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F6" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F7" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F8" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F9" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F10" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F11" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F12" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F13" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F14" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F15" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F16" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F17" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F18" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F19" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="F20" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F1" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL1" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL1" runat="server" CssClass="ASPDropDownList" Visible="False">
</asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F2" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL2" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL2" runat="server" CssClass="ASPDropDownList" Visible="False">
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F3" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL3" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL3" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F4" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL4" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL4" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F5" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL5" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL5" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F6" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL6" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL6" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F7" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL7" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL7" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F8" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL8" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL8" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F9" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL9" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL9" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F10" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL10" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL10" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F11" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL11" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL11" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F12" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL12" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL12" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F13" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL13" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL13" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F14" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL14" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL14" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F15" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL15" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL15" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F16" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL16" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL16" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F17" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL17" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL17" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F18" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL18" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL18" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F19" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL19" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL19" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_F20" runat="server" Font-Bold="True" Visible="false"></asp:Label>
                                                <asp:TextBox ID="TXT_VAL20" runat="server" CssClass="ASPTextBox" Visible="False"></asp:TextBox>
                                                <asp:DropDownList ID="DDL_VAL20" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Button ID="BT_SAVEITEM" runat="server" CommandName="Save" CssClass="ASPButton" Text="S" ForeColor="White" BackColor="Green" />
                                                <asp:Button ID="BT_DELITEM" runat="server" CommandName="Delete" CssClass="ASPButton" Text="X" ForeColor="White" BackColor="Red" OnClientClick="if(!confirm('ARE YOU SURE TO DELETE ?')){return false;};" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="center" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" Mode="NumericPages" />
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
