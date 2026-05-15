<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PARAMETER_MASTER_REWARD_BUTTON.aspx.cs" Inherits="AGR.Form_Parameter.PARAMETER_MASTER_REWARD_BUTTON" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div style="padding: 2px;">
            <div class="card text-dark bg-transparent o-hidden h-100">
                <div class="card-body" style="background-color: gainsboro; text-align: center; font-size: small;">
                    <a href="../Form_Data/DATA_COMMISSION_PERIOD.aspx?TYPE=3" target="ParameterRewardBody" style="color: green;">SETUP PERIOD
                    </a>
                </div>
            </div>
        </div>
        <div style="padding: 2px;">
            <div class="card text-dark bg-transparent o-hidden h-100">
                <div class="card-body" style="background-color: gainsboro; text-align: center; font-size: small;">
                    <a href="PARAMETER_MASTER_FRAME.ASPX?mode=3" target="ParameterRewardBody" style="color: green;">REWARD PARAMETER
                    </a>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
