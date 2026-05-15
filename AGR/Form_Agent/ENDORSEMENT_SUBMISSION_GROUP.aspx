<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ENDORSEMENT_SUBMISSION_GROUP.aspx.cs" Inherits="AGR.MOVEMENT_GROUP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!-- Bootstrap core CSS-->
    <link href="../include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom fonts for this template-->
    <link href="../include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <!-- Page level plugin CSS-->
    <link href="../include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="../include/css/sb-admin.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding-bottom: 5px;">
            <a href="AGENT_TERMINATE_REACTIVATE.ASPX?mode=2" target="MovementBody">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: #cede9d; color: darkgreen; font-size: 9pt;">
                        <center>
                    <i class="fa fa-plug" style="font-size:15pt;"></i>
                    <br />
                    TERMINATION                 
                    </center>
                    </div>
                </div>
            </a>
        </div>
        <div style="padding-bottom: 5px;">
            <a href="AGENT_TERMINATE_REACTIVATE.ASPX?mode=1" target="MovementBody">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: #cede9d; color: darkgreen; font-size: 9pt;">
                        <center>
                    <i class="fa fa-battery-quarter" style="font-size:15pt;"></i>
                    <br />
                    RE-ACTIVATION                 
                    </center>
                    </div>
                </div>
            </a>
        </div>
        <div style="padding-bottom: 5px;">
            <a href="AGENT_TERMINATE_REACTIVATE.ASPX?mode=4" target="MovementBody">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: #cede9d; color: darkgreen; font-size: 9pt;">
                        <center>
                    <i class="fa fa-forward" style="font-size:15pt;"></i>
                    <br />
                    PRODUCTION TRANSFER                    
                    </center>
                    </div>
                </div>
            </a>
        </div>
        <div style="padding-bottom: 5px;">
            <a href="AGENT_HOLD_REMUN.ASPX?mode=2&target=1" target="MovementBody">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: #cede9d; color: darkgreen; font-size: 9pt;">
                        <center>
                    <i class="fa fa-hand-stop-o" style="font-size:15pt;"></i>
                    <br />
                    HOLD REMUNERATION                    
                    </center>
                    </div>
                </div>
            </a>
        </div>
        <div style="padding-bottom: 5px;">
            <a href="MOVEMENT_LIST.ASPX?mode=3" target="MovementBody">
                <div class="card text-dark bg-transparent o-hidden h-100">
                    <div class="card-body" style="background-color: #cede9d; color: darkgreen; font-size: 9pt;">
                        <center>
                    <i class="fa fa-edit" style="font-size:15pt;"></i>
                    <br />
                    UPDATE DATA                    
                    </center>
                    </div>
                </div>
            </a>
        </div>


    </form>
</body>
</html>
