<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PenjaminanMonitoringEntri.aspx.cs" Inherits="HEALTH.Form_Klaim.PenjaminanMonitoringEntri" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
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
        <table style="border-spacing: 0px; position: absolute; top: 0px; left: 0px; width:100%;">
            <tr>
                <td>
                    <table style="border-spacing: 0px;">
                        <tr style="vertical-align: top;">
                            <td style="vertical-align: top;">
                                <table id="TBL_MON" runat="server" style="border-spacing: 0px">
                                    <tr>
                                        <td>NO SURAT JAMINAN</td>
                                        <td>
                                            <asp:TextBox ID="LB_NOSURAT" runat="server" BackColor="#CCCCCC" CssClass="ASPTextBox" Font-Bold="True" ReadOnly="True" Width="200px" style="text-align:center;"></asp:TextBox>
                                            <asp:Label ID="LB_ID" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
                                            <asp:Label ID="LB_SEQ" runat="server" CssClass="ASPLabel" Font-Bold="False" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TGL MONITORING</td>
                                        <td>
                                            <asp:Label ID="LB_MON_TGL" runat="server" CssClass="ASPLabel" Font-Bold="True"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>LAMA INAP</td>
                                        <td>
                                            <asp:TextBox ID="TXT_MON_LOS" runat="server" CssClass="ASPTextBox" Width="50"></asp:TextBox>
                                            &nbsp;HARI</td>
                                    </tr>
                                    <tr>
                                        <td>GOL OPERASI</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MON_GOL" runat="server" CssClass="ASPDropDownList">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="vertical-align: top;">RENCANA TINDAKAN</td>
                                        <td>
                                            <asp:TextBox ID="TXT_MON_TINDAKAN" runat="server" CssClass="ASPTextBox" Width="300px" MaxLength="500" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style11">WAKTU INPUT TINDAKAN</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2a" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXT_DATE_TINDAKAN1" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE_TINDAKAN1">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:DropDownList ID="DDL_HH_TINDAKAN1" runat="server" CssClass="ASPDropDownList">
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
                                                    <asp:DropDownList ID="DDL_MM_TINDAKAN1" runat="server" CssClass="ASPDropDownList">
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
                                        <td class="auto-style11">WAKTU KIRIM TINDAKAN</td>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel2b" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="TXT_DATE_TINDAKAN2" runat="server" CssClass="ASPTextBox" Width="60px"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TXT_DATE_TINDAKAN2">
                                                    </ajaxToolkit:CalendarExtender>
                                                    <asp:DropDownList ID="DDL_HH_TINDAKAN2" runat="server" CssClass="ASPDropDownList">
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
                                                    <asp:DropDownList ID="DDL_MM_TINDAKAN2" runat="server" CssClass="ASPDropDownList">
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
                                        <td>NOMOR MEDICAL</td>
                                        <td>
                                            <asp:TextBox ID="TXT_MON_NOMED" runat="server" CssClass="ASPTextBox" Width="254px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 15px;"></td>
                            <td>
                                <table style="border-spacing: 0px;">
                                    <tr>
                                        <td>KONTAK</td>
                                        <td>
                                            <asp:TextBox ID="TXT_MON_KONTAK" runat="server" CssClass="ASPTextBox" Width="150px" MaxLength="50"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>KELAS PERAWATAN</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MON_KELAS" runat="server" CssClass="ASPDropDownList"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>KET. BENEFIT</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MON_KELBEN" runat="server" CssClass="ASPDropDownList" Width="200px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>STAT. MONITORING</td>
                                        <td>
                                            <asp:DropDownList ID="DDL_MON_STAT" runat="server" CssClass="ASPDropDownList" Width="200px"></asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td>BIAYA SEMENTARA</td>
                                        <td>
                                            <asp:TextBox ID="TXT_MON_SEMENTARA" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>BIAYA AKHIR</td>
                                        <td>
                                            <asp:TextBox ID="TXT_MON_AKHIR" runat="server" CssClass="ASPTextBoxNumber" Width="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <asp:Button ID="BT_MON_SAVE" runat="server" CssClass="ASPButton" Text="SAVE" OnClick="BT_MON_SAVE_Click" />
                                            <asp:Button ID="BT_PRINT" runat="server" Text="PRINT" OnClick="BT_PRINT_Click" CssClass="ASPButton" /><br />
                                            <asp:Label ID="LB_MON_ERROR" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_MON0" style="vertical-align: top;" runat="server" visible="false">
                <td style="vertical-align: top;">
                    <table style="border-spacing: 0px;">
                        <tr>
                            <td>
                                <asp:Button ID="BT_MON1" runat="server" CssClass="ASPButton" Font-Bold="True" Text="INFO MEDIS" Width="200px" OnClick="BT_MON1_Click" />
                            </td>
                            <td>
                                <asp:Button ID="BT_MON2" runat="server" CssClass="ASPButton" Font-Bold="True" Text="DIAGNOSA - ICD" Width="200px" OnClick="BT_MON2_Click" />
                            </td>
                            <td>
                                <asp:Button ID="BT_MON3" runat="server" CssClass="ASPButton" Font-Bold="True" Text="DOKTER" Width="200px" OnClick="BT_MON3_Click" />
                            </td>
                            <td>
                                <asp:Button ID="BT_MON4" runat="server" CssClass="ASPButton" Font-Bold="True" Text="KONTAK" Width="200px" OnClick="BT_MON4_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="TR_MON1" style="vertical-align: top;" runat="server" visible="false">
                <td style="vertical-align: top;">
                    <asp:Button ID="BT_MON_INFO" runat="server" CssClass="ASPButton" OnClick="BT_MON_INFO_Click" Text="SAVE INFO &gt;&gt;" />
                    <asp:DataGrid ID="DGR_MON_INFO" runat="server" CellPadding="4" PageSize="20" GridLines="None" CssClass="ASPDatagrid" ForeColor="#333333" AutoGenerateColumns="False">
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="INFO">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CATATAN">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_MON_INFO" runat="server" CssClass="ASPTextBox" MaxLength="1000" Width="600px" Wrap="true"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top" />
                            </asp:TemplateColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="False" />
                        <ItemStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr id="TR_MON2" style="vertical-align: top;" runat="server" visible="false">
                <td style="vertical-align: top;" class="auto-style10">
                    <asp:TextBox ID="TXT_MON_ICD" runat="server" CssClass="ASPTextBox"></asp:TextBox>
                    <asp:Button ID="BT_MON_ICD" runat="server" CssClass="ASPButton" Text="CARI" />
                    <asp:Button ID="BT_MON_ICD_SAVE" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Blue" OnClick="BT_MON_ICD_SAVE_Click" Text="TAMBAH DIAGNOSA >>" />

                    <asp:DataGrid ID="DGR_MON_DIAG" runat="server" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" BorderColor="#0033CC" OnItemCommand="DGR_MON_DIAG_ItemCommand" BackColor="White" BorderStyle="None" BorderWidth="1px">
                        <AlternatingItemStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:BoundColumn DataField="ICD_CODE" HeaderText="KODE ICD"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="DIAGNOSA"></asp:BoundColumn>
                            <asp:ButtonColumn CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" Wrap="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr id="TR_MON3" style="vertical-align: top;" runat="server" visible="false">
                <td style="vertical-align: top; margin-left: 40px;">
                    <asp:Button ID="BT_MON_DOKTER_ADD" runat="server" CssClass="ASPButton" Font-Bold="True" ForeColor="Green" OnClick="BT_MON_DOKTER_ADD_Click" Text="TAMBAH DOKTER &gt;&gt;" />
                    <asp:DataGrid ID="DGR_MON_DOKTER" runat="server" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" BorderColor="#999999" OnItemCommand="DGR_MON_DOKTER_ItemCommand" BackColor="White" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black">
                        <AlternatingItemStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="KET_DOKTER_CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA_DOKTER" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:DropDownList ID="DDL_MON_DOKTER" runat="server" CssClass="ASPDropDownList">
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NAMA DOKTER">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_MON_DOKTER" runat="server" CssClass="ASPTextBox" MaxLength="50" Width="250px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_MON_DOKTER_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" />
                                    <asp:Button ID="BT_MON_DOKTER_DELETE" runat="server" CommandName="Delete" CssClass="ASPButton" Font-Bold="True" ForeColor="Red" Text="DELETE" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>
            <tr id="TR_MON4" style="vertical-align: top;" runat="server" visible="false">
                <td style="vertical-align: top;" class="auto-style10">

                    <asp:DataGrid ID="DGR_MON_KONTAK" runat="server" CellPadding="3" PageSize="20" GridLines="Vertical" CssClass="ASPDatagrid" AutoGenerateColumns="False" BorderColor="#999999" OnItemCommand="DGR_MON_KONTAK_ItemCommand" BackColor="White" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black">
                        <AlternatingItemStyle BackColor="#CCCCCC" />
                        <Columns>
                            <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCR" HeaderText="TIPE KONTAK"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NAMA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NOMOR" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="NAMA">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_MON_KON_NAMA" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="300px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NOMOR">
                                <ItemTemplate>
                                    <asp:TextBox ID="TXT_MON_KON_NOMOR" runat="server" CssClass="ASPTextBox" MaxLength="100" Width="200px"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="BT_MON_KON_SAVE" runat="server" CommandName="Save" CssClass="ASPButton" Text="SAVE" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Wrap="False" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    </asp:DataGrid>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
