<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TRAINING_GROUP.aspx.cs" Inherits="AGR.TRAINING_GROUP" %>

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
                <a href="TRAINING_LIBRARY_LIST.ASPX?mode=1" target="TrainingBody">
                    <table style="width: 100%; color: white; font-size: 8pt;">
                        <tr>
                            <td style="font-size: 16pt; width: 50px;"><i class="fa fa-book"></i></td>
                            <td style="text-align: center;">TRAINING<br />
                                LIBRARY
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>
        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="TRAINING_TRAINER_LIST.ASPX?mode=2" target="TrainingBody">
                    <table style="width: 100%; color: white; font-size: 8pt">
                        <tr>
                            <td style="font-size: 16pt; width: 50px;"><i class=" fa fa-user-plus"></i></td>
                            <td style="text-align: center;">TRAINING<br />
                                TRAINER
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>
        <div class="card text-dark bg-transparent o-hidden h-100">
            <div class="card-body" style="background-color: gray; padding: 5px;">
                <a href="TRAINING_SCHEDULE_LIST.ASPX?mode=2" target="TrainingBody">
                    <table style="width: 100%; color: white; font-size: 8pt">
                        <tr>
                            <td style="font-size: 16pt; width: 50px;"><i class="fa fa-clock-o"></i></td>
                            <td style="text-align: center;">TRAINING<br />
                                SCHEDULE
                            </td>
                        </tr>
                    </table>
                </a>
            </div>
        </div>
    </form>
</body>
</html>
