<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SQ.aspx.cs" Inherits="GO.SQ" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html>
<head>

  <meta charset="UTF-8">
  
    <link rel="apple-touch-icon" type="image/png" href="https://cpwebassets.codepen.io/assets/favicon/apple-touch-icon-5ae1a0698dcc2402e9712f7d01ed509a57814f994c660df9f7a952f3060705ee.png" />
    <meta name="apple-mobile-web-app-title" content="CodePen">

   <%-- <link rel="shortcut icon" type="image/x-icon" href="https://cpwebassets.codepen.io/assets/favicon/favicon-aec34940fbc1a6e787974dcd360f2c6b63348d4b1f4e06c77743096d55480f33.ico" />

    <link rel="mask-icon" type="image/x-icon" href="https://cpwebassets.codepen.io/assets/favicon/logo-pin-8f3771b1072e3c38bd662872f6b673a722f4b3ca2421637d5596661b4e2132cc.svg" color="#111" />
    --%>
    <link type="image/vnd.microsoft.icon" rel="icon" href="data:image/png;base64,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" /></head>


  <title>405 - Not Allowed</title>
  
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/meyer-reset/2.0/reset.min.css">

  
  
<style>
@import url("https://fonts.googleapis.com/css2?family=Fontdiner+Swanky&family=Roboto:wght@500&display=swap");
* {
  box-sizing: 0;
  margin: 0;
  padding: 0;
  cursor: url("https://s3-us-west-2.amazonaws.com/s.cdpn.io/4424790/cursors-edge.png"), auto;
}

body {
  background: linear-gradient(to right, white 50%, #383838 50%);
  font-family: "Roboto", sans-serif;
  font-size: 18px;
  font-weight: 500;
  line-height: 1.5;
  color: white;
}

div {
  display: flex;
  align-items: center;
  height: 100vh;
  max-width: 1000px;
  width: calc(100% - 4rem);
  margin: 0 auto;
}
div > * {
  display: flex;
  flex-flow: column;
  align-items: center;
  justify-content: center;
  height: 100vh;
  max-width: 500px;
  width: 100%;
  padding: 2.5rem;
}

aside {
  background-image: url("https://s3-us-west-2.amazonaws.com/s.cdpn.io/4424790/right-edges.png");
  background-position: top right;
  background-repeat: no-repeat;
  background-size: 25px 100%;
}
aside img {
  display: block;
  height: auto;
  width: 100%;
}

main {
  text-align: center;
}
main h1 {
  font-family: "Fontdiner Swanky", cursive;
  font-size: 4rem;
  color: #c5dc50;
  margin-bottom: 1rem;
}
main p {
  margin-bottom: 2.5rem;
}
main p em {
  font-style: italic;
  color: #c5dc50;
}
main button {
  font-family: "Fontdiner Swanky", cursive;
  font-size: 1rem;
  color: #383838;
  border: none;
  background-color: #f36a6f;
  padding: 1rem 2.5rem;
  transform: skew(-5deg);
  transition: all 0.1s ease;
  cursor: url("https://s3-us-west-2.amazonaws.com/s.cdpn.io/4424790/cursors-eye.png"), auto;
}
main button:hover {
  background-color: #c5dc50;
  transform: scale(1.15);
}

@media (max-width: 700px) {
  body {
    background: #383838;
    font-size: 16px;
  }

  div {
    flex-flow: column;
  }
  div > * {
    max-width: 700px;
    height: 100%;
  }

  aside {
    background-image: none;
    background-color: white;
  }
  aside img {
    max-width: 300px;
  }
}
</style>

  <script>
      window.console = window.console || function (t) { };
</script>

  
  
  <script>
      if (document.location.search.match(/type=embed/gi)) {
          window.parent.postMessage("resize", "*");
      }
</script>


</head>

<body translate="no" >
  <div>
  <aside><img src="https://s3-us-west-2.amazonaws.com/s.cdpn.io/4424790/Mirror.png" alt="405 Image" />
  </aside>
  <main>
    <h1>Ooopps!</h1>
    <p>
      Hey Dude, Your page doesn't exists anymore <em>. . . TURN BACK NOW.</em>
    </p>
    <button>You can go now!</button>
  </main>
</div>
  
  
  
  

</body>

</html>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="border-spacing: 0px; width: 100%; top: 0px; left: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 200px;">
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width:80px;">DATABASE</td>
                                        <td>:
                                            <asp:DropDownList ID="DDL_DB" runat="server" CssClass="ASPDropDownList" AutoPostBack="True" OnSelectedIndexChanged="DDL_DB_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>OBJECT TYPE</td>
                                        <td>:
                                            <asp:DropDownList ID="DDL_OBJECT" runat="server" AutoPostBack="True" CssClass="ASPDropDownList" OnSelectedIndexChanged="DDL_OBJECT_SelectedIndexChanged">
                                                <asp:ListItem Value="U">TABLE</asp:ListItem>
                                                <asp:ListItem Value="V">VIEW</asp:ListItem>
                                                <asp:ListItem Value="P">PROCEDURE</asp:ListItem>
                                                <asp:ListItem Value="FN">FUNCTION</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_VIEW" runat="server" BackColor="Blue" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_VIEW_Click" Text="V" />
                                            &nbsp;<asp:Button ID="BT_PICK" runat="server" BackColor="Yellow" CssClass="ASPButton" Font-Bold="True" OnClick="BT_PICK_Click" Text="P" /></td>
                                    </tr>
                                </table>
                                <asp:ListBox ID="LB_OBJECT" runat="server" BackColor="#66FF33" CssClass="ASPDropDownList" Height="200px" Width="200px"></asp:ListBox>
                            </td>
                            <td>
                                <asp:TextBox ID="TXT_SQL" runat="server" BackColor="Yellow" CssClass="ASPTextBox" Height="200px" TextMode="MultiLine" Width="95%"></asp:TextBox><br />
                                <asp:Button ID="BT_SQL" runat="server" BackColor="Red" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_SQL_Click" Text="!" Width="50px" />
                                &nbsp;<asp:Button ID="BT_XLS" runat="server" BackColor="Green" CssClass="ASPButton" Font-Bold="True" ForeColor="White" OnClick="BT_XLS_Click" Text="XLS" Width="50px" />
                                <asp:Label ID="LB_ERROR" runat="server" CssClass="ASPLabel" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="border-top-style: ridge;">
                    <asp:Label ID="LB_RESULT" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                    <asp:DataGrid ID="DGR" runat="server" BackColor="White"
                        BorderColor="#003366" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                        CellSpacing="1" Font-Names="Tahoma" Font-Size="X-Small" GridLines="Vertical"
                        PageSize="50" ItemStyle-Wrap="true" AllowPaging="True" OnPageIndexChanged="DGR_PageIndexChanged">
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right"
                            Mode="NumericPages" Position="Top" />
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>--%>
