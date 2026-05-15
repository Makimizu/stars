<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENT_REGISTRATION.aspx.cs" Inherits="AGR.Agent_Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/CommonStyle.css" rel="stylesheet" />
    <style>
        .block {
            display: block;
            height: 38px;
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
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
        <script type="text/javascript">
            function checkAgent() {
                var frontname = document.getElementById('<%= TXT_FRONTNAME.ClientID %>');
                var lastname = document.getElementById('<%= TXT_LASTNAME.ClientID %>');
                var midname = document.getElementById('<%= TXT_MIDNAME.ClientID %>');
                var dob = document.getElementById('<%= TXT_DOB.ClientID %>');
                var warning = document.getElementById('<%= LB_WARNING.ClientID %>');
                var fullname = frontname.value.trim() + " " + midname.value.trim() + " " + lastname.value.trim();
                warning.innerHTML = ''
                warning.className = ''
                console.log(warning)
                $.ajax({
                    type: "POST",
                    url: "AGENT_REGISTRATION.aspx/CheckAgent",
                    data: JSON.stringify({ name: fullname, dob: dob.value }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (!response.d || response.d.trim().length === 0) {
                            return;
                        }
                        console.log(response.d)
                        warning.innerHTML = response.d
                        warning.className = 'alert'
                    },
                    error: function (xhr, status, error) {
                        console.log("Error: " + error)
                    }
                });
            }
        </script>
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <span class="fa fa-user-circle" style="font-size: medium; color: navy;"></span><span style="color: navy;">&nbsp;&nbsp;<b>AGENT INFORMATION</b></span>

        <table style="border-spacing: 0px; font-size: xx-small; width: 100%; border-top: ridge;">
            <tr style="vertical-align: top;">
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; font-size: xx-small; width: 100%;">
                        <tr>
                            <td style="width: 100px;">AGENT CODE</td>
                            <td>
                                <asp:TextBox ID="TXT_CODE" runat="server" Width="200px" ReadOnly="True" BackColor="#CCCCCC" Font-Size="XX-Small"></asp:TextBox>
                                <asp:Label ID="LB_ID" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>FIRST NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_FRONTNAME" runat="server" Width="90%" Font-Size="XX-Small" oninput="checkAgent()"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>LAST NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_LASTNAME" runat="server" Width="90%" Font-Size="XX-Small"  oninput="checkAgent()"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>MID NAME</td>
                            <td>
                                <asp:TextBox ID="TXT_MIDNAME" runat="server" Width="90%" Font-Size="XX-Small" oninput="checkAgent()"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">DOB</td>
                            <td>
                                <asp:TextBox ID="TXT_DOB" runat="server" Width="80px" Style="text-align: center;" Font-Size="XX-Small" onchange="checkAgent()"  oninput="checkAgent()"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DOB">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100px;">PLACE OF BIRTH</td>
                            <td>
                                <asp:TextBox ID="TXT_POB" runat="server" Width="90%" Font-Size="XX-Small"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 50%;">
                    <table style="border-spacing: 0px; font-size: xx-small; font-family: Tahoma; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>GENDER</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_GENDER" runat="server" CssClass="ASPDropDownList" Font-Size="XX-Small">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PHONE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_PHONE" runat="server" Width="90%" Font-Size="XX-Small" MaxLength="15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>EMAIL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_EMAIL" runat="server" Width="90%" Font-Size="XX-Small"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>RECRUITER</td>
                                        <td>
                                            <asp:Label ID="LB_REFERRAL_CODE" runat="server"></asp:Label>
                                            <asp:Label ID="LB_REFERRAL_NAME" runat="server" Font-Bold="True"></asp:Label>
                                            <asp:Button ID="BT_REFERRAL" runat="server" Text="?" CssClass="ASPButton" Width="30px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>AREA</td>
                                        <td>
                                            <asp:Label ID="LB_AREA" runat="server" CssClass="ASPLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 100px;">JOINT DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_JOINTDATE" runat="server" Width="80px" Style="text-align: center;" Font-Size="XX-Small"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_JOINTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>STATUS</td>
                                        <td>
                                            <asp:Label ID="LB_STATUS" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SAVE" runat="server" Text="SAVE" CssClass="ASPButton" Width="80" OnClick="BT_SAVE_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 80px;">
                                <asp:Image ID="IMG_PHOTO" runat="server" Width="100%" ImageUrl="~/include/image/avatar.png" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <asp:Label ID="LB_WARNING" runat="server"></asp:Label>

        <div id="DV_BUTTONS" runat="server">
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td style="width: 25%;">
                        <asp:Button ID="BT_BANKACCOUNT" runat="server" CssClass="ASPButton" Text="PERSONAL INFO" Width="100%" Font-Size="X-Small" OnClick="BT_BANKACCOUNT_Click" BackColor="#6699FF" ForeColor="White" Font-Names="Verdana" />
                    </td>
                    <td style="width: 25%;">
                        <asp:Button ID="BT_AGENCY" runat="server" CssClass="ASPButton" Text="LEVEL STRUCTURE MOVEMENT" Width="100%" Font-Size="X-Small" OnClick="BT_AGENCY_Click" BackColor="#6699FF" ForeColor="White" Font-Names="Verdana" />
                    </td>
                    <td style="width: 25%;">
                        <asp:Button ID="BT_EDUCATION" runat="server" CssClass="ASPButton" Text="EDUCATION &amp; WORK EXPERIENCE" Width="100%" Font-Size="X-Small" OnClick="BT_EDUCATION_Click" BackColor="#6699FF" ForeColor="White" Font-Names="Verdana" />
                    </td>
                    <td style="width: 25%;">
                        <asp:Button ID="BT_ARCHIVE" runat="server" CssClass="ASPButton" Text="ARCHIEVE" Width="100%" Font-Size="XX-Small" OnClick="BT_ARCHIVE_Click" BackColor="#6699FF" ForeColor="White" Font-Names="Verdana" />
                    </td>
                </tr>
            </table>
            <table style="border-spacing: 0px; width: 100%;">
                <tr>
                    <td class="TDBGColor">
                        <asp:Label ID="LBL_TITLE" runat="server" CssClass="ASPLabel" Font-Size="XX-Small" Font-Names="Verdana"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>





