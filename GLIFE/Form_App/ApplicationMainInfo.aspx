<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationMainInfo.aspx.cs" Inherits="GLIFE.Form_App.ApplicationMainInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_REGNO" runat="server" Visible="false"></asp:Label>
        <table style="border-spacing: 0px; width: 100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 100px;">SUM INSURED</td>
                                        <td>
                                            <asp:TextBox ID="TXT_SUMINS" runat="server" Width="150px" onkeypress="return CheckNumeric();" onkeyup="FormatCurrency(this);" CssClass="ASPTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>START DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STARTDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STARTDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:DropDownList ID="DDL_DEFINE" runat="server" CssClass="ASPDropDownList" AutoPostBack="true" OnSelectedIndexChanged="DDL_DEFINE_SelectedIndexChanged">
                                                <asp:ListItem Value="T">DEFINE TENOR</asp:ListItem>
                                                <asp:ListItem Value="E">DEFINE END DATE</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="TR_TENOR" runat="server">
                                        <td>TENOR (Y-M-D)</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TY" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem>0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                                <asp:ListItem>30</asp:ListItem>
                                                <asp:ListItem>31</asp:ListItem>
                                                <asp:ListItem>32</asp:ListItem>
                                                <asp:ListItem>33</asp:ListItem>
                                                <asp:ListItem>34</asp:ListItem>
                                                <asp:ListItem>35</asp:ListItem>
                                                <asp:ListItem>36</asp:ListItem>
                                                <asp:ListItem>37</asp:ListItem>
                                                <asp:ListItem>38</asp:ListItem>
                                                <asp:ListItem>39</asp:ListItem>
                                                <asp:ListItem>40</asp:ListItem>
                                                <asp:ListItem>41</asp:ListItem>
                                                <asp:ListItem>42</asp:ListItem>
                                                <asp:ListItem>43</asp:ListItem>
                                                <asp:ListItem>44</asp:ListItem>
                                                <asp:ListItem>45</asp:ListItem>
                                                <asp:ListItem>46</asp:ListItem>
                                                <asp:ListItem>47</asp:ListItem>
                                                <asp:ListItem>48</asp:ListItem>
                                                <asp:ListItem>49</asp:ListItem>
                                                <asp:ListItem>50</asp:ListItem>


                                            </asp:DropDownList>
                                            <asp:DropDownList ID="DDL_TM" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem>0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem Value="2"></asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                            </asp:DropDownList><asp:DropDownList ID="DDL_TD" runat="server" CssClass="ASPDropDownList">
                                                <asp:ListItem>0</asp:ListItem>
                                                <asp:ListItem>1</asp:ListItem>
                                                <asp:ListItem>2</asp:ListItem>
                                                <asp:ListItem>3</asp:ListItem>
                                                <asp:ListItem>4</asp:ListItem>
                                                <asp:ListItem>5</asp:ListItem>
                                                <asp:ListItem>6</asp:ListItem>
                                                <asp:ListItem>7</asp:ListItem>
                                                <asp:ListItem>8</asp:ListItem>
                                                <asp:ListItem>9</asp:ListItem>
                                                <asp:ListItem>10</asp:ListItem>
                                                <asp:ListItem>11</asp:ListItem>
                                                <asp:ListItem>12</asp:ListItem>
                                                <asp:ListItem>13</asp:ListItem>
                                                <asp:ListItem>14</asp:ListItem>
                                                <asp:ListItem>15</asp:ListItem>
                                                <asp:ListItem>16</asp:ListItem>
                                                <asp:ListItem>17</asp:ListItem>
                                                <asp:ListItem>18</asp:ListItem>
                                                <asp:ListItem>19</asp:ListItem>
                                                <asp:ListItem>20</asp:ListItem>
                                                <asp:ListItem>21</asp:ListItem>
                                                <asp:ListItem>22</asp:ListItem>
                                                <asp:ListItem>23</asp:ListItem>
                                                <asp:ListItem>24</asp:ListItem>
                                                <asp:ListItem>25</asp:ListItem>
                                                <asp:ListItem>26</asp:ListItem>
                                                <asp:ListItem>27</asp:ListItem>
                                                <asp:ListItem>28</asp:ListItem>
                                                <asp:ListItem>29</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr id="TR_ENDDATE" runat="server" visible="false">
                                        <td>END DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_ENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_ENDDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>STNC DATE</td>
                                        <td>
                                            <asp:TextBox ID="TXT_STNC" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_STNC">
                                            </ajaxToolkit:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_MAINSAVE" runat="server" CssClass="ASPButton" Text="SAVE" Width="70px" OnClick="BT_MAINSAVE_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 50%;">
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td style="width: 80px;">START AGE</td>
                                        <td>
                                            <asp:Label ID="LB_AGE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="vertical-align: top;">
                                        <td>U/W CODE</td>
                                        <td>
                                            <asp:Label ID="LB_UWCODE" runat="server" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PREMIUM</td>
                                        <td>
                                            <asp:Label ID="LB_PREMIUM" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>NETT PREMIUM</td>
                                        <td>
                                            <asp:Label ID="LB_NETTPREMIUM" runat="server" Font-Bold="true" ForeColor="Blue"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr id="TR_JOIN" runat="server" visible="false">
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr>
                            <td bgcolor="#CCCCCC" style="border-style: outset; text-align: center" colspan="2">JOIN LIFE
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DataGrid ID="DGR_JOIN" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Names="Tahoma" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                            <HeaderStyle HorizontalAlign="Right" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FULLNAME" HeaderText="FULLNAME"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RELATION" HeaderText="RELATION"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SEX" HeaderText="GENDER">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <br />
        <iframe src="../Form_Tools/OtherSetup.aspx?ID=<%=Request.QueryString["ID"]%>&readonly=<%=Request.QueryString["readonly"]%>&MODE=UW&s=<%=Session["s"]%>" width="100%" style="height: 200px; border: 0;"></iframe>
    </form>
</body>
</html>
