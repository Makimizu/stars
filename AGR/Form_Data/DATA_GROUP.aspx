<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DATA_GROUP.aspx.cs" Inherits="AGR.DATA_GROUP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../Standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="../../reportviewer/viewer.aspx?APPID=AGR&CODE=26" target="DataBody">
                    <table style="width: 90%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 50px;"><i class="fa fa-apple"></i></td>
                            <td style="text-align: center;">PRODUCTION<br />
                                DATA
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>
        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="ExternalRevenue.aspx" target="DataBody">
                    <table style="width: 90%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 50px;"><i class="fa fa-plus-circle"></i></td>
                            <td style="text-align: center;">EXTERNAL<br />
                                REVENUE
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>
        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="../Form_Parameter/PARAMETER_MASTER_REMUN_FRAME.ASPX?mode=3" target="DataBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 50px;"><i class="fa fa-calculator"></i></td>
                            <td style="text-align: center;">REMUN<br />
                                OUTPUT DATA
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>
        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="DATA_LIST.ASPX?mode=5" target="DataBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 50px;"><i class="fa fa-rocket"></i></td>
                            <td style="text-align: center;">PROMOTION<br />
                                DEMOTION
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>
        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="../../ReportViewer/Viewer.aspx?APPID=AGR&CODE=35" target="DataBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 50px;"><i class="fa fa-filter"></i></td>
                            <td style="text-align: center;">AGENT<br />EVALUATION
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>
    </form>
</body>
</html>


