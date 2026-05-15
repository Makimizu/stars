<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_MASTER_COMMISSION_BUTTON.aspx.cs" Inherits="AGR.Form_Parameter.PARAMETER_MASTER_COMMISSION_BUTTON" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div style="padding: 2px;">
            <div class="card text-dark bg-transparent o-hidden h-100">
                <div class="card-body" style="background-color: gainsboro; text-align: center; font-size: small;">
                    <a href="../Form_Data/DATA_COMMISSION_PERIOD.aspx?TYPE=1" target="ParameterCommBody" style="color: green;">SETUP PERIOD
                    </a>
                </div>
            </div>
        </div>
        <div style="padding: 2px;">
            <div class="card text-dark bg-transparent o-hidden h-100">
                <div class="card-body" style="background-color: gainsboro; text-align: center; font-size: small;">
                    <a href="PARAMETER_MASTER_FRAME.ASPX?mode=1" target="ParameterCommBody" style="color: green;">BASIC COMMISSION & OVERRIDING
                    </a>
                </div>
            </div>
        </div>
        <div style="padding: 2px;">
            <div class="card text-dark bg-transparent o-hidden h-100">
                <div class="card-body" style="background-color: gainsboro; text-align: center; font-size: small;">
                    <a href="PARAMETER_MASTER_FRAME.ASPX?mode=2a" target="ParameterCommBody" style="color: green;">BUSINESS ALLOWANCE
                    </a>
                </div>
            </div>
        </div>
        <div style="padding: 2px;">
            <div class="card text-dark bg-transparent o-hidden h-100">
                <div class="card-body" style="background-color: gainsboro; text-align: center; font-size: small;">
                    <a href="PARAMETER_MASTER_FRAME.ASPX?mode=2b" target="ParameterCommBody" style="color: green;">BONUS ROYALTY
                    </a>
                </div>
            </div>
        </div>
        <div style="padding: 2px;">
            <div class="card text-dark bg-transparent o-hidden h-100">
                <div class="card-body" style="background-color: gainsboro; text-align: center; font-size: small;">
                    <a href="PARAMETER_MASTER_FRAME.ASPX?mode=2c" target="ParameterCommBody" style="color: green;">BONUS RECRUITMENT
                    </a>
                </div>
            </div>
        </div>
        <div style="padding: 2px;">
            <div class="card text-dark bg-transparent o-hidden h-100">
                <div class="card-body" style="background-color: gainsboro; text-align: center; font-size: small;">
                    <a href="PARAMETER_MASTER_FRAME.ASPX?mode=2d" target="ParameterCommBody" style="color: green;">BONUS PROMOTION
                    </a>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
