<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_GROUP.aspx.cs" Inherits="AGR.PARAMETER_GROUP" %>

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
                <a href="PARAMETER_GENERAL_LIST.ASPX" target="ParameterBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 40px;"><i class="fa fa-wrench"></i></td>
                            <td style="text-align: left;">PARAMETER<br />
                                GENERAL
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>

        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="PARAMETER_MASTER_REMUN_FRAME.ASPX?mode=1" target="ParameterBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 40px;"><i class="fa fa-calendar"></i></td>
                            <td style="text-align: left;">SETUP PERIOD
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>

        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="PARAMETER_MASTER_REMUN_FRAME.ASPX?mode=2" target="ParameterBody">
                    <table style="width: 100%; color: white; font-size: 8pt">
                        <tr>
                            <td style="font-size: 16pt; width: 40px;"><i class="fa fa-calculator"></i></td>
                            <td style="text-align: left;">REMUN SCHEME
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>

        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="PARAMETER_NON_BASIC.ASPX?CODE=PARAM_TAX_TYPE_DETAIL" target="ParameterBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 40px;"><i class="fa fa-scissors"></i></td>
                            <td style="text-align: left;">TAX
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>

        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="PARAMETER_AGENT_EVALUATION.ASPX" target="ParameterBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 40px;"><i class="fa fa-check-square"></i></td>
                            <td style="text-align: left;">EVALUATION</td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>

        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="PARAMETER_CONTEST.ASPX" target="ParameterBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 40px;"><i class="fa fa-smile-o"></i></td>
                            <td style="text-align: left;">CONTEST</td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>

    </form>
</body>
</html>

