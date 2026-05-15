<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="true" CodeBehind="IkhtisarInput.aspx.cs" Inherits="GLIFE.Form_Policy.IkhtisarInput" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
    <%--<script src="https://cdn.ckeditor.com/4.22.1/standard/ckeditor.js"></script>--%>
    <link rel="stylesheet" href="https://cdn.ckeditor.com/ckeditor5/42.0.1/ckeditor5.css">
</head>
<body>
     
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_readonly" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="LB_s" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">INPUT DATA
                </td>
            </tr>
            <tr>
                <td>
                    <table ID="GTL" style="border-spacing: 0px; width: 100%; display: none">
                        <tr>
                        <td style="width:100px;">POLICY NO</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:TextBox ID="TXT_POLICYNO" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="150px" Enabled="true" ReadOnly></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>MASA PERJANJIAN</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:TextBox ID="TXT_DATE3" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE3">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-
                                <asp:TextBox ID="TXT_DATE4" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE4">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>TANGGAL BERLAKUNYA POLIS</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:TextBox ID="TXT_DATE1" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE1">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-
                                <asp:TextBox ID="TXT_DATE2" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE2">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>MANFAAT TAKAFUL</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:DropDownList ID="DDL_MANFAAT" runat="server" CssClass="ASPDropDownList" Width="90%">
                                    <%--<asp:ListItem Value="1">Manfaat Normal Death</asp:ListItem>
                                    <asp:ListItem Value="2">Manfaat Normal Death + PA-A</asp:ListItem>
                                    <asp:ListItem Value="3">Manfaat Normal Death + PA-A + PA-B</asp:ListItem>
                                    <asp:ListItem Value="4">Manfaat Normal Death + PA-A + PA-B + PA-D</asp:ListItem>
                                    <asp:ListItem Value="5">Manfaat Normal Death + PA-A + PA-D</asp:ListItem>
                                    <asp:ListItem Value="6">Siswa/Mahasiswa dan Guru/Staf Manfaat Normal Death + PA-A + PA-B + PA-D + Cash Plan</asp:ListItem>
                                    <asp:ListItem Value="7">Siswa/Mahasiswa dan Guru/Staf Manfaat Normal Death + PA-A + PA-B + PA-D + Cash Plan + Santunan Biaya Pemakaman</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>KETENTUAN UNDERWRITING</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:Panel ID="editor2" runat="server">

                                </asp:Panel>
                                <asp:TextBox ID="inputValue2" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="100%" TextMode="MultiLine" Height="60px" style="display: none"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>KETENTUAN LAIN</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:TextBox ID="TXT_OTHER" runat="server" CssClass="ASPTextBox" Width="90%" TextMode="MultiLine" Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>USIA PESERTA SAAT DIAJUKAN</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:Panel ID="editor3" runat="server">

                                </asp:Panel>
                                <asp:TextBox ID="inputValue3" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="100%" TextMode="MultiLine" Height="60px" style="display: none"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BT_SAVE" runat="server" CssClass="ASPButton Load" Text="SAVE" Width="100px" Enabled="true" OnClientClick="return check1()" OnClick="BT_SAVE_Click" />
                            </td>
                        </tr>
                    </table>
                    <table ID="CL" style="border-spacing: 0px; width: 100%; display: none">
                        <tr>
                        <td style="width:100px;">POLICY NO</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:TextBox ID="TXT_POLICYNO1" runat="server" CssClass="ASPTextBox" MaxLength="30" Width="100%" Enabled="true" ReadOnly></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>MASA PERJANJIAN</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:TextBox ID="TXT_DATE5" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE5">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-
                                <asp:TextBox ID="TXT_DATE6" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE6">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>TANGGAL BERLAKUNYA POLIS</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:TextBox ID="TXT_DATE7" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE7">
                                </ajaxToolkit:CalendarExtender>
                                &nbsp;-
                                <asp:TextBox ID="TXT_DATE8" runat="server" CssClass="ASPTextBox" Width="60px" Style="text-align: center;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE8">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>MANFAAT TAKAFUL</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:Panel ID="editor" runat="server">

                                </asp:Panel>
                                <asp:TextBox ID="inputValue" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="100%" TextMode="MultiLine" Height="60px" style="display: none">></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>TINGKAT MARGIN PEMBIAYAAN</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:TextBox ID="TXT_MARGIN" runat="server" CssClass="ASPTextBox" Width="100%" Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>KETENTUAN SELEKSI RESIKO</td>
                        </tr>
                        <tr>
                            <td style="margin-bottom:10px;">
                                <asp:Panel ID="editor1" runat="server">

                                </asp:Panel>
                                <asp:TextBox ID="inputValue1" runat="server" CssClass="ASPTextBox" MaxLength="255" Width="100%" TextMode="MultiLine" Height="60px" style="display: none"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>FEE BASE</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TXT_FEEBASE" runat="server" CssClass="ASPTextBox" Width="100%" Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>BIAYA PENGELOLAAN</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TXT_PENGELOLAAN" runat="server" CssClass="ASPTextBox" Width="100%" Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>BIAYA ADMINISTRASI KLAIM</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TXT_KLAIM" runat="server" CssClass="ASPTextBox" Width="100%" Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>NISBAH SURPLUS DANA TABARRU</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TXT_SURPLUS" runat="server" CssClass="ASPTextBox" Width="100%" Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>LAMPIRAN</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="TXT_LAMPIRAN" runat="server" CssClass="ASPTextBox" Width="100%" Enabled="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="BT_SAVE_BANCAS" runat="server" CssClass="ASPButton Load" Text="SAVE" Width="100px" OnClientClick="return check()" OnClick="BT_SAVE_BANCAS_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
         </table>
    </form>

    <script  type="text/javascript">
        function check() {

            

            document.getElementById('inputValue').value = editor.getData();
            document.getElementById('inputValue1').value = editor1.getData();
            return true;
        }

        function check1() {



            document.getElementById('inputValue2').value = editor2.getData();
            document.getElementById('inputValue3').value = editor3.getData();

            return true;
        }
    </script>
   
    <script type="importmap">
            {
                "imports": {
                    "ckeditor5": "https://cdn.ckeditor.com/ckeditor5/42.0.1/ckeditor5.js",
                    "ckeditor5/": "https://cdn.ckeditor.com/ckeditor5/42.0.1/"
                }
            }
        </script>
        <script type="module">
            import {
                ClassicEditor,
                Essentials,
                Paragraph,
                Bold,
                Italic,
                Font,
                List,
                Table,
                TableToolbar,
                Alignment
            } from 'ckeditor5';

            ClassicEditor
                .create( document.querySelector( '#editor2' ), {
                    plugins: [ Essentials, Paragraph, Bold, Italic, Font, List, Alignment ],
                    toolbar: [
						'bold', 'italic', '|', 'bulletedList',
                    'numberedList', '|', 'fontSize', 'fontColor', 'fontBackgroundColor', '|', 'alignment'
                    ]
                } )
                .then( editor2 => {
                    window.editor2 = editor2;
                } )
                .catch( error => {
                    console.error( error );
                } );

            ClassicEditor
                .create( document.querySelector( '#editor1' ), {
                    plugins: [ Table, TableToolbar, Essentials, Paragraph, Bold, Italic, Font, List ],
                    toolbar: [
						'bold', 'italic', '|', 'insertTable', '|', 'bulletedList',
                    'numberedList', '|', 'fontSize', 'fontFamily', 'fontColor', 'fontBackgroundColor'
                    ],
                    table: {
                                contentToolbar: [ 'tableColumn', 'tableRow', 'mergeTableCells' ]
                            }
                } )
                .then( editor1 => {
                    window.editor1 = editor1;
                } )
                .catch( error => {
                    console.error( error );
                } );

            ClassicEditor
                .create( document.querySelector( '#editor' ), {
                    plugins: [ Essentials, Paragraph, Bold, Italic, Font, List ],
                    toolbar: [
						'bold', 'italic', '|', 'bulletedList',
                    'numberedList', '|', 'fontSize', 'fontFamily', 'fontColor', 'fontBackgroundColor'
                    ]
                } )
                .then( editor => {
                    window.editor = editor;
                } )
                .catch( error => {
                    console.error( error );
                } );

            ClassicEditor
                .create( document.querySelector( '#editor3' ), {
                    plugins: [ Essentials, Paragraph, Bold, Italic, Font, List ],
                    toolbar: [
						'bold', 'italic', '|', 'bulletedList',
                    'numberedList', '|', 'fontSize', 'fontFamily', 'fontColor', 'fontBackgroundColor'
                    ]
                } )
                .then( editor3 => {
                    window.editor3 = editor3;
                } )
                .catch( error => {
                    console.error( error );
                } );
        </script>
</body>
</html>
