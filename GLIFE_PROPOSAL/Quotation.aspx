<%@ Page Language="C#" MasterPageFile="~/Parent.Master" AutoEventWireup="true" CodeBehind="Quotation.aspx.cs" Inherits="GLIFE_PROPOSAL.Quotation" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <!-- Bootstrap core CSS-->
    <link href="include/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Custom fonts for this template-->
    <link href="include/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="include/vendor/font-awesome/css/all.min.css" rel="stylesheet" type="text/css" />
    <!-- Page level plugin CSS-->
    <link href="include/vendor/datatables/dataTables.bootstrap4.css" rel="stylesheet" />
    <!-- Custom styles for this template-->
    <link href="include/css/sb-admin.css" rel="stylesheet" />
    <link href="include/css/controls.css" rel="stylesheet" />

    <script>
        function FormatCurrency(ctrl) {
            //Check if arrow keys are pressed - we want to allow navigation around textbox using arrow keys
            if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40) {
                return;
            }

            var val = ctrl.value;

            val = val.replace(/,/g, "")
            ctrl.value = "";
            val += '';
            x = val.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';

            var rgx = /(\d+)(\d{3})/;

            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }

            ctrl.value = x1 + x2;
        }

        function CheckNumeric() {
            return event.keyCode >= 48 && event.keyCode <= 57;
        }
    </script>

    <form id="form1" runat="server">
        <ajaxToolkit:ToolkitScriptManager ID="tkScriptManager" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <asp:Label ID="LB_MEMBERID" runat="server" Visible="false"></asp:Label>

        <div id="DV_POLICY" runat="server">
            <table style="border-spacing: 0px; width: 100%; font-size: small;">
                <tr>
                    <td>
                        <table style="border-spacing: 0px; width: 100%; font-size: small;">
                            <tr>
                                <td style="width: 150px;">SEARCH POLICY</td>
                                <td>
                                    <asp:TextBox ID="TXT_POLICYSEARCH" runat="server" Width="80%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px;">SEARCH POLICY HOLDER</td>
                                <td>
                                    <asp:TextBox ID="TXT_POLICYHOLDERSEARCH" runat="server" Width="80%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Button ID="BT_SEARCH" runat="server" CssClass="ButtonColor" Text="SEARCH" Width="100px" OnClick="BT_SEARCH_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <%--<table style="border-spacing: 0px; width: 100%; font-size: small;">
                            <tr>
                                <td style="width: 120px;">SEARCH POLICY</td>
                                <td>
                                    <asp:TextBox ID="TXT_POLICYSEARCH" runat="server" AutoPostBack="true" Width="100%" OnTextChanged="TXT_POLICYSEARCH_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>--%>
                </tr>
                <tr>
                    <td>
                        <asp:DataGrid ID="DGR_POLICY" runat="server" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                            CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Vertical" OnItemCommand="DGR_POLICY_ItemCommand" Width="100%" ItemStyle-Wrap="true" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" OnPageIndexChanged="DGR_POLICY_PageIndexChanged">
                            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <AlternatingItemStyle BackColor="#F7F7F7" />
                            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" Wrap="True" VerticalAlign="Top" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TC_ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="POLICY_NO" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="POLICY NO">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LBT_POLICY" runat="server" CommandName="Select"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Wrap="false" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DESCR" HeaderText="PRODUCT"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMPANY_NAME" HeaderText="POLICY HOLDER"></asp:BoundColumn>
                            </Columns>
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        </asp:DataGrid>
                    </td>
                </tr>
            </table>
        </div>

        <div id="DV_ENTRY" runat="server" visible="false">
            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    <table style="border-spacing: 0px; width: 100%; font-size: small;">
                        <tr>
                            <td style="width: 150px;">REGNO</td>
                            <td>
                                <table style="border-spacing: 0px; width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:Label ID="LB_REGNO" runat="server"></asp:Label>
                                            <asp:Label ID="LB_TRACK" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Button ID="BT_BACK" runat="server" CssClass="Button" Visible="false" OnClick="BT_BACK_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>FULL NAME</td>
                            <td>
                                <asp:Label ID="LB_NAME" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>DATE OF BIRTH</td>
                            <td>
                                <asp:Label ID="LB_DOB" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>GENDER</td>
                            <td>
                                <asp:Label ID="LB_GENDER" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </div>
                <div class="col-xl-6 col-sm-6 mb-3">
                    <table style="border-spacing: 0px; width: 100%; font-size: small;">
                        <tr>
                            <td style="width: 150px;">POLICY NO</td>
                            <td>
                                <asp:Label ID="LB_POLICYNO" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>POLICY HOLDER</td>
                            <td>
                                <asp:Label ID="LB_COMPANY" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>PRODUCT</td>
                            <td>
                                <asp:Label ID="LB_PRODUCT" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>BRANCH</td>
                            <td>
                                <asp:DropDownList ID="DDL_BRANCH" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_BRANCH_SelectedIndexChanged" Width="100%"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr id="TR_AGENT" runat="server">
                            <td>AGENT</td>
                            <td>
                                <asp:DropDownList ID="DDL_AGENT" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DDL_AGENT_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>POLICY TYPE</td>
                            <td>
                                <asp:Label ID="LB_POLICY_TYPE" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>POLICY PERIODE START</td>
                            <td style="width: 100px;">
                                <asp:Label ID="LB_POLICY_PERIODE_START" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>POLICY PERIODE END</td>
                             <td style="width: 100px;">
                                <asp:Label ID="LB_POLICY_PERIODE_END" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <br />

            <div class="row">
                <div class="col-xl-6 col-sm-6 mb-3">
                    <div style="border: 1px solid rgba(0,0,0,.125); border-radius: .25rem;">
                        <div class="card-header" style="color: cyan; background-color: #2F327F;">
                            <i class="fa fa-info-circle"></i>&nbsp;&nbsp;MAIN INFO
                        </div>
                        <div class="card-body">
                            <table style="border-spacing: 0px; width: 100%;">
                                <tr style="vertical-align: top;">
                                    <td>
                                        <table style="border-spacing: 0px; width: 100%; font-size: small;">
                                            <tr>
                                                <td style="width: 100px;">SUM INSURED</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_SUMINS" runat="server" Width="150px" onkeypress="return CheckNumeric();" onkeyup="FormatCurrency(this);"></asp:TextBox>
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
                                                    <asp:DropDownList ID="DDL_DEFINE" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_DEFINE_SelectedIndexChanged">
                                                        <asp:ListItem Value="T">DEFINE TENOR</asp:ListItem>
                                                        <asp:ListItem Value="E">DEFINE END DATE</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="TR_TENOR" runat="server">
                                                <td>TENOR (Y-M-D)</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_TY" runat="server" Font-Size="Smaller">
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
                                                    <asp:DropDownList ID="DDL_TM" runat="server" Font-Size="Smaller">
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
                                                    </asp:DropDownList><asp:DropDownList ID="DDL_TD" runat="server" Font-Size="Smaller">
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
                                            <tr id="TR_SPECIALRATE" runat="server" visible="True">
                                                <td>SPECIAL RATE</td>
                                                <td>
                                                    <asp:DropDownList ID="DDL_SPECIALRATE" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_SPECIALRATE_SelectedIndexChanged">
                                                        <asp:ListItem Value="NO">NO</asp:ListItem>
                                                        <asp:ListItem Value="YES">YES</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr id="TR_TITLESPECIALRATE" runat="server" visible="false">
                                                <td colspan="2"><b>Data Special Rate :</b></td>
                                            </tr>
                                            <tr id="TR_NOTE" runat="server" visible="false">
                                                <td style="vertical-align:top">NOTE</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_NOTE" TextMode="multiline" runat="server" CssClass="ASPTextBox" Width="250px" Height="100px" Style="text-align: left;"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="TR_UPLOAD" runat="server" visible="false">
                                                <td>File</td>
                                                <td><asp:FileUpload ID="FU_ARCHIEVE_SPECIALRATE" runat="server" Width="100px" /></td>
                                            </tr>
                                            <tr id="TR_DESCUPLOAD" runat="server" visible="false">
                                                <td>DESCRIPTION</td>
                                                <td>
                                                    <asp:TextBox ID="TXT_ARCHIEVE_SPECIALRATE" runat="server" placeholder="Addition File Remark ..." Width="100%" MaxLength="1000"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="TR_BUTTONUPLOAD" runat="server" visible="false">
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:LinkButton ID="LBT_ARCHIEVE_SPECIALRATE" runat="server" ToolTip="Upload" OnClick="LBT_ARCHIEVE_SPECIALRATE_Click">
                                                        UPLOAD
                                                    </asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr id="TR_DOCLIST" runat="server" visible="false">
                                                <td colspan="2">
                                                    <asp:DataGrid ID="DGR_ARCHIEVE_SPECIALRATE" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                    ForeColor="#333333" GridLines="None" PageSize="20"
                                                    OnItemCommand="DGR_ARCHIEVE_SPECIALRATE_ItemCommand"  Width="100%" CssClass="ASPDatagrid" Font-Size="Small" ShowHeader="True">
                                                    <EditItemStyle BackColor="#7C6F57" />
                                                    <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                    <AlternatingItemStyle BackColor="White" />
                                                    <ItemStyle BackColor="#E3EAEB" />
                                                    <Columns>
                                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                        <asp:BoundColumn DataField="NAMAFILE" HeaderText="FILE" Visible="false"></asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="FILE">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBT_DOWNLOAD" runat="server" ToolTip="Download" CommandName="Download">
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="REMARK" HeaderText="DESC" Visible="true"></asp:BoundColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemStyle Width="30px" HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LBT_ARCHIEVE_DELETE" runat="server" ToolTip="Delete" CommandName="Delete" ForeColor="Red">
                                                                <span class="fa fa-remove"></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>


                                        </table>
                                    </td>
                                    <td style="width: 20px;"></td>
                                    <td>
                                        <table style="border-spacing: 0px; width: 100%; font-size: small;">
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
                                                <td>
                                                    <asp:Button ID="BT_MAINSAVE" runat="server" CssClass="ButtonColor" Text="SAVE" Width="100px" OnClick="BT_MAINSAVE_Click" /></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <asp:DataGrid ID="DGR_BENEFIT" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="8pt" GridLines="None" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" ForeColor="#333333" OnItemCommand="DGR_BENEFIT_ItemCommand">
                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#EFF3FB" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#666666" />
                                <HeaderStyle BackColor="#7F9BD4" Font-Bold="False" ForeColor="Navy" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <EditItemStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <Columns>
                                    <asp:BoundColumn DataField="BENEFIT_CODE" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCR" HeaderText="BENEFIT"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SUMINS" HeaderText="SUM INSURED" Visible="false">
                                        <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="END_DATE" HeaderText="END DATE" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="RATE" Visible="false"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="SUM INSURED">
                                        <HeaderStyle HorizontalAlign="Right" Width="100px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="TXT_BENEFITSUMINS" runat="server" Width="100%" onkeypress="return CheckNumeric();" onkeyup="FormatCurrency(this);" Style="text-align: right;" BackColor="#b7ffb7"></asp:TextBox>
                                            <asp:DropDownList ID="DDL_BENEFITSUMINS" runat="server" BackColor="#b7ffb7" Visible="false"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="&permil;&nbsp;RATE">
                                        <HeaderStyle HorizontalAlign="Right" Width="70px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="TXT_BENEFITRATE" runat="server" Width="100%" Style="text-align: right;" BackColor="#bfffdf"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="END DATE">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="TXT_BENEFITENDDATE" runat="server" CssClass="ASPTextBox" Width="80px" Style="text-align: center;" BackColor="#ffffb0"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_BENEFITENDDATE">
                                            </ajaxToolkit:CalendarExtender>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBT_BENEFITSAVE" runat="server" CommandName="Save" ToolTip="Save" ForeColor="Green">
                                                <span class="fa fa-save"></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="SQL_PLAN" Visible="False"></asp:BoundColumn>
                                </Columns>
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            </asp:DataGrid>
                            <br />
                            <asp:DataGrid ID="DGR_ACCU" runat="server" CellPadding="4" GridLines="None" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" CssClass="ASPDatagrid" OnItemCommand="DGR_ACCU_ItemCommand" Font-Size="8pt" ShowFooter="True" OnItemDataBound="DGR_ACCU_ItemDataBound" ForeColor="#333333">
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#EFF3FB" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#666666" />
                                <HeaderStyle BackColor="#7F9BD4" Font-Bold="False" ForeColor="Navy" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <EditItemStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#7F9BD4" ForeColor="Navy" Font-Bold="true" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Size="9pt" />
                                <Columns>
                                    <asp:BoundColumn DataField="REGNO" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="FULLNAME" Visible="false"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="FULLNAME">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LBT_REGNO" runat="server" CommandName="Select"></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="DOB" HeaderText="DOB">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SEX" HeaderText="GENDER">
                                        <HeaderStyle HorizontalAlign="Center" Width="60px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="START_DATE" HeaderText="START DATE">
                                        <HeaderStyle HorizontalAlign="Center" Width="80px" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="SUMINS" HeaderText="SUM INSURED">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                    <div id="DV_JOIN" runat="server" visible="false">
                        <div class="card mb-3">
                            <div class="card-header" style="color: white; background-color: #2C8772;">
                                <i class="fas fa-people-carry"></i>&nbsp;&nbsp;JOIN LIFE
                            </div>
                            <div class="card-body">
                                <asp:DataGrid ID="DGR_JOIN" runat="server" CellPadding="4" Font-Names="Tahoma" Font-Size="8pt" GridLines="None" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_JOIN_ItemCommand" ForeColor="#333333">
                                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#EFF3FB" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#666666" />
                                    <HeaderStyle BackColor="#66BAA7" Font-Bold="False" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <EditItemStyle BackColor="#2461BF" />
                                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                            <HeaderStyle HorizontalAlign="Right" Width="30px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="FULLNAME" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RELATION_CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="SEX" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DOB" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="FULLNAME">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_JOINFULLNAME" runat="server" MaxLength="100" Width="100%" CssClass="TextBox"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="GENDER">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_JOINSEX" runat="server" CssClass="DropDownList">
                                                    <asp:ListItem Text="MALE" Value="M"></asp:ListItem>
                                                    <asp:ListItem Text="FEMALE" Value="F"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="DOB">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TXT_JOINDOB" runat="server" CssClass="TextBox" Width="80px" Style="text-align: center;"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ceDate1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_JOINDOB">
                                                </ajaxToolkit:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RELATION">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="DDL_JOINRELATION" runat="server" CssClass="DropDownList"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBT_DELETE" runat="server" CommandName="Delete" ForeColor="Red" Font-Size="Small" ToolTip="Delete">
                                                    <span class="fa fa-remove"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                </asp:DataGrid>
                                <asp:Button ID="BT_JOINSAVE" runat="server" Text="SAVE" CssClass="ButtonColor" Width="100px" OnClick="BT_JOINSAVE_Click" />
                            </div>
                        </div>
                    </div>
                    <div id="DV_DOCS" runat="server" visible="false">
                        <div class="card mb-3">
                            <div class="card-header" style="color: white; background-color: #4E504C;">
                                <i class="fa fa-paperclip"></i>&nbsp;&nbsp;REQUIRED DOCUMENT & ARCHIEVE
                            </div>
                            <div class="card-body">
                                <asp:DataGrid ID="DGR_DOCS" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Names="Tahoma" Font-Size="8pt" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" ShowHeader="False" OnItemCommand="DGR_DOCS_ItemCommand" Width="100%">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#CCCCCC" ForeColor="#666666" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DOCTYPE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NAMAFILE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RECEIVE_DATE" Visible="False"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:Label ID="LB_DESCR" runat="server" Visible="False"></asp:Label>
                                                <asp:LinkButton ID="LBT_DESCR" runat="server" CommandName="Download" Visible="False" ToolTip="Download"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:FileUpload ID="FU_DOC" runat="server" Width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBT_UPLOAD" runat="server" CommandName="Upload" ToolTip="Upload">
                                                <span class="fa fa-upload"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBT_DELETE" runat="server" CommandName="Delete" ToolTip="Delete">
                                                <span class="fa fa-remove" style="color:red;"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                </asp:DataGrid>

                                <table style="width: 100%; border-spacing: 0px;">
                                    <tr>
                                        <td>
                                            <table style="width: 100%; border-spacing: 0px; font-size: 8pt;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="TXT_ARCHIEVE" runat="server" placeholder="Addition File Remark ..." Width="100%" MaxLength="1000"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 130px; text-align: right;">
                                                        <asp:FileUpload ID="FU_ARCHIEVE" runat="server" Width="100px" />
                                                        &nbsp;
                                                        <asp:LinkButton ID="LBT_ARCHIEVE" runat="server" ToolTip="Upload" OnClick="LBT_ARCHIEVE_Click">
                                                        <span class="fa fa-upload"></span>
                                                        </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DataGrid ID="DGR_ARCHIEVE" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                ForeColor="#333333" GridLines="None" PageSize="20"
                                                OnItemCommand="DGR_ARCHIEVE_ItemCommand" CssClass="ASPDatagrid" Width="100%" Font-Size="Small" ShowHeader="False">
                                                <EditItemStyle BackColor="#7C6F57" />
                                                <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <ItemStyle BackColor="#E3EAEB" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="NAMAFILE" HeaderText="FILE" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="REMARK" Visible="False"></asp:BoundColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBT_DOWNLOAD" runat="server" ToolTip="Download" CommandName="Download">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <ItemStyle Width="30px" HorizontalAlign="Right" />
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LBT_ARCHIEVE_DELETE" runat="server" ToolTip="Delete" CommandName="Delete" ForeColor="Red">
                                                            <span class="fa fa-remove"></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="DV_REMARK" runat="server" visible="false">
                        <div class="card mb-3">
                            <div class="card-header" style="color: white; background-color: #693D76;">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;REMARK
                            </div>
                            <div class="card-body">
                                <asp:TextBox ID="TXT_REMARK" runat="server" Width="100%" TextMode="MultiLine" Height="50px" Font-Size="Small" placeholder="Type your remark here ..." BackColor="#E4E4E4"></asp:TextBox>
                                <asp:Button ID="BT_REMARK_SAVE" runat="server" CssClass="ButtonColor" Text="ADD REMARK" Width="130px" OnClick="BT_REMARK_SAVE_Click" />

                                <asp:DataGrid ID="DGR_REMARK" runat="server" BackColor="White"
                                    BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3"
                                    CellSpacing="1" Font-Names="Tahoma" Font-Size="8pt" GridLines="Vertical" ItemStyle-Wrap="true" AutoGenerateColumns="False" Width="100%" OnItemCommand="DGR_REMARK_ItemCommand" ShowHeader="False">
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#666666" Wrap="True" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="SEQ" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="REMARK"></asp:BoundColumn>
                                        <asp:TemplateColumn>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemStyle Width="30px" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBT_REMARKDELETE" runat="server" CommandName="Delete" ToolTip="Delete">
                                                <span class="fa fa-remove" style="color:red;"></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                </asp:DataGrid>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-6 mb-3" id="DV_QUESTIONS" runat="server" visible="false">
                    <div class="card mb-3">
                        <div class="card-header" style="color: lightgreen; background-color: darkgreen;">
                            <i class="fa fa-question-circle"></i>&nbsp;&nbsp;MEDICAL QUESTIONS
                        </div>
                        <div class="card-body">
                            <table id="TBL_QUESTION" runat="server" visible="false" style="width: 100%; font-size: small;">
                                <tr>
                                    <td>MEDICAL QUESTION FOR :</td>
                                    <td style="text-align: right">
                                        <asp:DropDownList ID="DDL_QUESTION" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_QUESTION_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>

                            <!--Responsive table by Ahmad Zulfahmi-->
                            <div class="row">  
                            <div class="col-lg-12 ">  
                            <div class="table-responsive">
                            <!--Responsive table by Ahmad Zulfahmi-->

                            <asp:DataGrid ID="DGR_QUESTION" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="1" Font-Names="Tahoma" Font-Size="Small" GridLines="Horizontal" ItemStyle-Wrap="true" PageSize="20" ShowHeader="False" Width="100%">
                                <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <AlternatingItemStyle BackColor="#F7F7F7" />
                                <ItemStyle BackColor="#EAFFEA" ForeColor="#666666" VerticalAlign="Top" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <Columns>
                                    <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DATA_TYPE" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SQL_REFF" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="NEXT_QUESTION" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="VALUE" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="VALUE_NEXT" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCR" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="SEQ" HeaderText="NO">
                                        <ItemStyle HorizontalAlign="Right" Width="30px" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <table style="border-spacing: 0px; width: 100%;">
                                                <tr>
                                                    <td>
                                                        <table style="border-spacing: 0px; width: 100%;">
                                                            <tr style="vertical-align: top;">
                                                                <td>
                                                                    <asp:Label ID="LB_DESCR" runat="server" /></td>
                                                                <td style="text-align: right;">
                                                                    <asp:DropDownList ID="DDL_REFF" runat="server" CssClass="DropDownList" Visible="False"></asp:DropDownList>
                                                                    <asp:TextBox ID="TXT_VAL" runat="server" CssClass="TextBox" Visible="False" Width="150px" MaxLength="255"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="TR_NEXTQUESTION" runat="server" visible="false">
                                                    <td>
                                                        <table style="border-spacing: 0px; width: 100%;">
                                                            <tr style="vertical-align: top; border-top: ridge;">
                                                                <td>
                                                                    <asp:Label ID="LB_QUESTION" runat="server" Visible="false" /></td>
                                                                <td style="text-align: right;">
                                                                    <asp:TextBox ID="TXT_NEXTVAL" runat="server" CssClass="TextBox" Width="300px" MaxLength="255" Visible="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <ItemStyle HorizontalAlign="Right" Width="20px" />
                                        <ItemTemplate>
                                            <asp:Label ID="LB_UNCHECKED" runat="server" ForeColor="Red" Visible="false">
                                                <span class="fa fa-bell"></span>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>

                            <!--Responsive table by Ahmad Zulfahmi-->
                            </div>  
                            </div>  
                            </div>
                            <!--Responsive table by Ahmad Zulfahmi-->

                            <asp:Button ID="BT_QUESTIONSAVE" runat="server" Text="SAVE" CssClass="ButtonColor" Width="100px" OnClick="BT_QUESTIONSAVE_Click" />
                            <asp:Button ID="BT_SENDEMAIL" runat="server" Text="SEND EMAIL" CssClass="ButtonColor" Width="100px" OnClick="BT_SENDEMAIL_Click" />
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <asp:Label ID="LB_ERROR" runat="server" ForeColor="Red"></asp:Label>

        <!--Plugins add by Ahmad Zulfahmi-->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
        <link href="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/css/select2.min.css" rel="stylesheet" />
        <script src="https://cdn.jsdelivr.net/npm/select2@4.1.0-rc.0/dist/js/select2.min.js"></script>

        <script>
            $('#<%=DDL_BRANCH.ClientID%>').select2({ width: "100%" });
            $('#<%=DDL_AGENT.ClientID%>').select2({ width: "100%" });
        </script>
        <!--Plugins add by Ahmad Zulfahmi-->

    </form>
</asp:Content>
