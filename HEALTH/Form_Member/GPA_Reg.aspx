<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GPA_Reg.aspx.cs" Inherits="HEALTH.Form_Member.GPA_Reg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../standard/CommonStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <table style="position: absolute; top: 0px; left: 0px; width: 100%; border-spacing: 0px;">
            <tr>
                <td>
                    <table style="border-spacing: 0px; width: 100%;">
                        <tr style="vertical-align: top;">
                            <td style="width: 600px;">
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td style="width: 120px;">BATCH ID</td>
                                        <td>
                                            <asp:TextBox ID="LB_ID" runat="server" CssClass="ASPTextBox" ReadOnly="true" BackColor="#CCCCCC" Font-Bold="True" Width="147px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>PERUSAHAAN</td>
                                        <td>
                                            <asp:Label ID="LB_COMPANY" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>POLIS PERIOD</td>
                                        <td>
                                            <asp:Label ID="LB_PERIOD" runat="server"></asp:Label>&nbsp;
                                            <asp:Label ID="LB_PERIODID" runat="server" Visible="false"></asp:Label>
                                            <asp:Button ID="BTN_INFO_TC" runat="server" CssClass="ASPButton" Text="Show TC" Height="17px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>DOCUMENT NO</td>
                                        <td>
                                            <asp:TextBox ID="TXT_DOCNO" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>EMAIL PIC</td>
                                        <td>
                                            <asp:TextBox ID="TXT_EMAIL" runat="server" CssClass="ASPTextBox" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>SUMBER DOKUMEN</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_SOURCE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TIPE ENDORSEMENT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_TIPE" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TGL ENDORSEMENT</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXT_DATE" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:DropDownList ID="DDL_HOUR" runat="server" CssClass="ASPDropDownList">
                                                        <asp:ListItem>00</asp:ListItem>
                                                        <asp:ListItem>01</asp:ListItem>
                                                        <asp:ListItem>02</asp:ListItem>
                                                        <asp:ListItem>03</asp:ListItem>
                                                        <asp:ListItem>04</asp:ListItem>
                                                        <asp:ListItem>05</asp:ListItem>
                                                        <asp:ListItem>06</asp:ListItem>
                                                        <asp:ListItem>07</asp:ListItem>
                                                        <asp:ListItem>08</asp:ListItem>
                                                        <asp:ListItem>09</asp:ListItem>
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
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="DDL_MINUTE" runat="server" CssClass="ASPDropDownList">
                                                        <asp:ListItem>00</asp:ListItem>
                                                        <asp:ListItem>01</asp:ListItem>
                                                        <asp:ListItem>02</asp:ListItem>
                                                        <asp:ListItem>03</asp:ListItem>
                                                        <asp:ListItem>04</asp:ListItem>
                                                        <asp:ListItem>05</asp:ListItem>
                                                        <asp:ListItem>06</asp:ListItem>
                                                        <asp:ListItem>07</asp:ListItem>
                                                        <asp:ListItem>08</asp:ListItem>
                                                        <asp:ListItem>09</asp:ListItem>
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
                                                        <asp:ListItem>51</asp:ListItem>
                                                        <asp:ListItem>52</asp:ListItem>
                                                        <asp:ListItem>53</asp:ListItem>
                                                        <asp:ListItem>54</asp:ListItem>
                                                        <asp:ListItem>55</asp:ListItem>
                                                        <asp:ListItem>56</asp:ListItem>
                                                        <asp:ListItem>57</asp:ListItem>
                                                        <asp:ListItem>58</asp:ListItem>
                                                        <asp:ListItem>59</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="BT_SUBMIT" runat="server" CssClass="ASPButton" OnClick="BT_SUBMIT_Click" Text="SAVE" Width="100px" />
                                            &nbsp;</td>
                                    </tr>
                                </table>
                                <asp:Label ID="LB_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </td>
                            <td style="text-align: right;">
                                <asp:Button ID="BT_GOTO" runat="server" Text="GO TO :" CssClass="ASPButton" OnClick="BT_GOTO_Click" Visible="False" />
                                <asp:DropDownList ID="DDL_GOTO" runat="server" CssClass="ASPDropDownList" Visible="False"></asp:DropDownList><br />
                                <iframe id="I2" runat="server" frameborder="no" height="180" name="I1" scrolling="auto" width="100%"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="BT_ARSIP" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Green" Visible="false" OnClick="BT_ARSIP_Click" Text="ARSIP" Width="200px" />
                    <asp:Button ID="BT_REMARK" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" Visible="false" OnClick="BT_REMARK_Click" Text="REMARK" Width="200px" />
                </td>
            </tr>
            <tr>
                <td>
                    <iframe id="I1" runat="server" frameborder="no" height="400px" name="I2" scrolling="auto"
                        width="100%"></iframe>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
