<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AGENCY_MENU.aspx.cs" Inherits="AGR.Form_Agent.AGENCY_MENU" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px; font-size: 8pt;">
                <a href="AGENCY_CORPORATE_LIST.ASPX" target="AgencyBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 50px;"><i class="fa fa-building-o"></i></td>
                            <td style="text-align: center;">AGENCY<br />
                                LIST
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>
        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px; font-size: 8pt;">
                <a href="AGENCY_BRANCH_LIST.ASPX" target="AgencyBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 50px;"><i class="fa fa-connectdevelop"></i></td>
                            <td style="text-align: center;">RO<br />
                                LIST
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>
    </form>
</body>
</html>
