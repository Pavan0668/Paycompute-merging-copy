<%@ Page Title="Employee claim" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeBehind="EmpClaim.aspx.cs"
    Inherits="iEmpPower.UI.Benefits_Payment.EmpClaim" Theme="SkinFile" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .DDLOverflow
        {
            overflow: hidden;
            width: 140px;
        }

        .DispNone
        {
            display: none;
        }

        .TblCls
        {
            width: 100%;
            /*border-collapse: collapse;*/
            font: normal normal normal 11px/22px Verdana,Arial,Helvetica,sans-serif;
        }

        .Td00
        {
            background-color: #2781ba;
            color: white;
        }

        .Td01
        {
            width: 20px;
            text-align: center;
        }

        .Td02
        {
            width: 70px;
            text-align: center;
        }

        .Td03
        {
            text-align: center;
        }

        .Td04
        {
            width: 145px;
        }

        .Td05
        {
            padding: 2px 5px;
        }

        .Td06
        {
            width: 220px;
            vertical-align: top;
        }

        .TxtRight
        {
            text-align: right;
        }

        .DivMsg
        {
            width: 99%;
            margin: 0 auto;
            clear: both;
            display: inline-block;
            padding: 2px 0 2px 5px;
        }
    </style>
    <script type="text/javascript">
        function Expand_DDL(element) {
            element.style.width = 'auto';
        }

        function Compress_DDL(element, width) {
            element.style.width = '' + width + 'px';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Employee claim</h2>
        <div class="DivMsg">
            <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
        </div>
        <div runat="server" id="TRGridView">
            <asp:GridView ID="TravelRequestGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="EMPLOYEE_NAME"
                Width="100%" OnRowDataBound="TravelRequestGridView_RowDataBound" AllowPaging="True"
                AllowSorting="True" OnPageIndexChanging="TravelRequestGridView_PageIndexChanging"
                OnSelectedIndexChanged="TravelRequestGridView_SelectedIndexChanged"
                OnSorting="TravelRequestGridView_Sorting"
                PageSize="4">
                <Columns>
                    <asp:BoundField DataField="FTPT_REQUEST_ID" HeaderText="Req. Id" SortExpression="FTPT_REQUEST_ID">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="gridTextStyle" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REQ_SEGMENT_ID" HeaderText="Req. seg Id" SortExpression="REQ_SEGMENT_ID">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="gridTextStyle" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TRAVEL_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Travel date"
                        SortExpression="TRAVEL_DATE">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="gridTextStyle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MODE_OF_TRANSPOPRT_FZTXT" HeaderText="Transport mode"
                        SortExpression="MODE_OF_TRANSPOPRT_FZTXT">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="gridTextStyle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MEDIA_OF_CATEGORY_TEXT25" HeaderText="Category"
                        SortExpression="MEDIA_OF_CATEGORY_TEXT25">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="gridTextStyle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REGION_TEXT25_FROM" HeaderText="From"
                        SortExpression="REGION_TEXT25_FROM">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="gridTextStyle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REGION_TEXT25_TO" HeaderText="To"
                        SortExpression="REGION_TEXT25_TO">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="gridTextStyle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TravelType" HeaderText="TravelType"
                        SortExpression="TravelType">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="gridTextStyle" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <br />
        <div id="grids" runat="server">
            <div>
                <div>
                    <div id="divFeedbackForm" runat="server" style="font-weight: bold;">Feedback Form :</div>
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="TravellerName  " CssClass="label"> </asp:Label>
                    :   
                        <asp:Label ID="LabelTravellerName" runat="server" Text=" " CssClass="label"> </asp:Label>
                </div>
                <br />

                <table class="TblCls" cellpadding="0" cellspacing="1">
                    <tr>
                        <th class="Td02 Td00">Sl.No</th>
                        <th class="Td03 Td00">Particulars</th>
                        <th class="Td04 Td00"></th>
                    </tr>
                    <tr>
                        <td colspan="3" class="TxtRight"><i><b>Feedback criteria</b>&nbsp;&nbsp;&nbsp;Ratings(Excellent = 4;&nbsp;&nbsp;Good = 3;&nbsp;&nbsp;Average = 2;&nbsp;&nbsp;Poor = 1;&nbsp;&nbsp;Horrible = 0)</i></td>
                    </tr>
                    <tr>
                        <td class="Td02">1.</td>
                        <td class="Td05">Travel / stay duration</td>
                        <td>
                            <asp:TextBox ID="txtCounter1" runat="server" MaxLength="1" Style="margin-left: 20px; text-align: center; font: normal normal normal 11px/16px Verdana,Arial,Helvetica,sans-serif;" Width="40"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="FTB_txtCounter1" runat="server" TargetControlID="txtCounter1" FilterType="Numbers"></Ajx:FilteredTextBoxExtender>
                            <asp:ImageButton ID="img1" runat="server" ImageUrl="~/images/down.gif"
                                AlternateText="Down" Width="15" Height="15" />
                            <asp:ImageButton ID="img2" runat="server" ImageUrl="~/images/up.gif"
                                AlternateText="Up" Width="15" Height="15" />
                            <Ajx:NumericUpDownExtender ID="NumericUpDownExtender1" runat="server" TargetControlID="txtCounter1" Width="80" TargetButtonDownID="img1"
                                TargetButtonUpID="img2" Maximum="255" Minimum="1" ServiceDownMethod="" ServiceUpMethod="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Td02">2.</td>
                        <td class="Td05">Travel Arrangement</td>
                        <td>
                            <asp:TextBox ID="txtCounter2" runat="server" MaxLength="1" Style="margin-left: 20px; text-align: center; font: normal normal normal 11px/16px Verdana,Arial,Helvetica,sans-serif;" Width="40"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="FTB_txtCounter2" runat="server" TargetControlID="txtCounter2" FilterType="Numbers"></Ajx:FilteredTextBoxExtender>
                            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/images/down.gif"
                                AlternateText="Down" Width="15" Height="15" />
                            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/images/up.gif"
                                AlternateText="Up" Width="15" Height="15" />
                            <Ajx:NumericUpDownExtender ID="NumericUpDownExtender2" runat="server" TargetControlID="txtCounter2" Width="80" TargetButtonDownID="ImageButton3"
                                TargetButtonUpID="ImageButton4" RefValues="0;1;2;3;4" ServiceDownMethod="" ServiceUpMethod="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Td02">3.</td>
                        <td class="Td05">Accommodation arrangement</td>
                        <td>
                            <asp:TextBox ID="txtCounter3" runat="server" MaxLength="1" Style="margin-left: 20px; text-align: center; font: normal normal normal 11px/16px Verdana,Arial,Helvetica,sans-serif;" Width="40"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="FTB_txtCounter3" runat="server" TargetControlID="txtCounter3" FilterType="Numbers"></Ajx:FilteredTextBoxExtender>
                            <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/down.gif"
                                AlternateText="Down" Width="15" Height="15" />
                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/images/up.gif"
                                AlternateText="Up" Width="15" Height="15" />
                            <Ajx:NumericUpDownExtender ID="NumericUpDownExtender3" runat="server"
                                TargetControlID="txtCounter3"
                                Width="80"
                                TargetButtonDownID="ImageButton5"
                                TargetButtonUpID="ImageButton6"
                                RefValues="0;1;2;3;4"
                                ServiceDownMethod=""
                                ServiceUpMethod="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Td02">4.</td>
                        <td class="Td05">Taxi arrangement</td>
                        <td>
                            <asp:TextBox ID="txtCounter4" runat="server" MaxLength="1" Style="margin-left: 20px; text-align: center; font: normal normal normal 11px/16px Verdana,Arial,Helvetica,sans-serif;" Width="40"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="FTB_txtCounter4" runat="server" TargetControlID="txtCounter4" FilterType="Numbers"></Ajx:FilteredTextBoxExtender>
                            <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/images/down.gif"
                                AlternateText="Down" Width="15" Height="15" />
                            <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/images/up.gif"
                                AlternateText="Up" Width="15" Height="15" />
                            <Ajx:NumericUpDownExtender ID="NumericUpDownExtender4" runat="server"
                                TargetControlID="txtCounter4"
                                Width="80"
                                TargetButtonDownID="ImageButton7"
                                TargetButtonUpID="ImageButton8"
                                RefValues="0;1;2;3;4"
                                ServiceDownMethod=""
                                ServiceUpMethod="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Td02">5.</td>
                        <td class="Td05">Visa & Passport arrangement</td>
                        <td>
                            <asp:TextBox ID="txtCounter5" runat="server" MaxLength="1" Style="margin-left: 20px; text-align: center; font: normal normal normal 11px/16px Verdana,Arial,Helvetica,sans-serif;" Width="40"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="FTB_txtCounter5" runat="server" TargetControlID="txtCounter5" FilterType="Numbers"></Ajx:FilteredTextBoxExtender>
                            <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/images/down.gif"
                                AlternateText="Down" Width="15" Height="15" />
                            <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/images/up.gif"
                                AlternateText="Up" Width="15" Height="15" />
                            <Ajx:NumericUpDownExtender ID="NumericUpDownExtender5" runat="server"
                                TargetControlID="txtCounter5"
                                Width="80"
                                TargetButtonDownID="ImageButton9"
                                TargetButtonUpID="ImageButton10"
                                RefValues="0;1;2;3;4"
                                ServiceDownMethod=""
                                ServiceUpMethod="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="Td02">6.</td>
                        <td class="Td05">Communication With travel desk</td>
                        <td>
                            <asp:TextBox ID="txtCounter6" runat="server" MaxLength="1" Style="margin-left: 20px; text-align: center; font: normal normal normal 11px/16px Verdana,Arial,Helvetica,sans-serif;" Width="40"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="FTB_txtCounter6" runat="server" TargetControlID="txtCounter6" FilterType="Numbers"></Ajx:FilteredTextBoxExtender>
                            <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/images/down.gif"
                                AlternateText="Down" Width="15" Height="15" />
                            <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/images/up.gif"
                                AlternateText="Up" Width="15" Height="15" />
                            <Ajx:NumericUpDownExtender ID="NumericUpDownExtender6" runat="server"
                                TargetControlID="txtCounter6"
                                Width="80"
                                TargetButtonDownID="ImageButton11"
                                TargetButtonUpID="ImageButton12"
                                RefValues="0;1;2;3;4"
                                ServiceDownMethod=""
                                ServiceUpMethod="" />
                        </td>
                    </tr>

                    <tr>
                        <td class="Td02">7.</td>
                        <td class="Td05">Overall travel experience</td>
                        <td>
                            <asp:TextBox ID="txtCounter7" runat="server" MaxLength="4" Style="margin-left: 20px; text-align: center; font: normal normal normal 11px/16px Verdana,Arial,Helvetica,sans-serif;" Width="40"
                                Enabled="False"></asp:TextBox>
                            <Ajx:FilteredTextBoxExtender ID="FTB_txtCounter7" runat="server" TargetControlID="txtCounter7" FilterType="Custom,Numbers" ValidChars="."></Ajx:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;</td>
                    </tr>
                </table>
                <table class="TblCls" cellpadding="0" cellspacing="1">
                    <tr>
                        <td class="Td02"></td>
                        <td class="Td06">Feedback / Comments&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="TxtFeedbackorComments" runat="server" TextMode="MultiLine" Columns="9" Style="font: normal normal normal 11px/16px Verdana,Arial,Helvetica,sans-serif; height: 30px; width: 99%; resize: none;" ValidationGroup="FbVG"></asp:TextBox><br />
                            <asp:RequiredFieldValidator ID="RFV_TxtFeedbackorComments" runat="server" ErrorMessage="Please give feedback" ControlToValidate="TxtFeedbackorComments" ValidationGroup="FbVG" ForeColor="Red" Font-Size="Small"></asp:RequiredFieldValidator>
                        </td>
                        <td class="Td04" style="vertical-align: top; padding: 0 0 0 15px;">
                            <asp:Button ID="btnsend" runat="server" Text="Send" OnClick="btnsend_Click" ValidationGroup="FbVG" />
                        </td>
                    </tr>
                </table>
                <%-- <div>
                    <asp:Label ID="lblTravelstayduration" runat="server" Text="Travel / stay duration" CssClass="label"></asp:Label>
                    : 
                        
                </div>
                <br />
                <h5>&nbsp;&nbsp;&nbsp;&nbsp; Feedback criteria&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Ratings&nbsp;&nbsp;&nbsp;(Excellent=4; Good=3; Average=2; Poor=1; Horrible=0)</h5>
                <br />
                <div>
                    <asp:Label ID="LabelTravelArrangement" runat="server" Text="Travel Arrangement" CssClass="label"></asp:Label>
                    :
        
                </div>
                <br />
                <div>
                    <asp:Label ID="lblAccommodationarrangement" runat="server" Text="Accommodation arrangement" CssClass="label"></asp:Label>
                    :
        
                </div>
                <br />
                <div>
                    <asp:Label ID="lblTaxiarrangement" runat="server" Text="Taxi arrangement" CssClass="label"></asp:Label>
                    :
             
                </div>
                <br />
                <div>
                    <asp:Label ID="lblVisaandPassportarrangement" runat="server" Text="Visa & Passport arrangement" CssClass="label"></asp:Label>
                    :
            
                </div>
                <br />
                <div>
                    <asp:Label ID="Labelcommunication" runat="server" Text="Communication" CssClass="label"></asp:Label>
                    :
             
                </div>
                <br />
                <div class="buttonrow">
                    <asp:Button ID="btnOveralltravel" runat="server" Text="Calculate" OnClick="btnOveralltravel_Click" />
                </div>
                <br />
                <div>

                    <asp:Label ID="lblOveralltravelexperience" runat="server" Text="Overall travel experience" CssClass="label"></asp:Label>
                    :
             
                    <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/images/down.gif"
                        AlternateText="Down" Width="14" Height="15" Visible="false" />
                    <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/images/up.gif"
                        AlternateText="Up" Width="15" Height="15" Visible="false" />
                     <Ajx:NumericUpDownExtender ID="NumericUpDownExtender7" runat="server"
                            TargetControlID="txtCounter7"
                            Width="80"
                            TargetButtonDownID="ImageButton13"
                            TargetButtonUpID="ImageButton14"
                            RefValues="0;1;2;3;4"
                            ServiceDownMethod=""
                            ServiceUpMethod="" />
                </div>
                <br />
                <div>
                    <asp:Label ID="lblFeedbackorComments" runat="server" Text="Feedback / Comments" CssClass="label"></asp:Label>
                    :
            
                </div>
                <br />
                <div class="buttonrow">
                </div>--%>
            </div>
            <%-- <table  cellpadding="0" cellspacing="1" style="border: 1px solid #333333;">
        <tr style="padding: 2px; background-color: #2781BA; color: #FFFFFF; font-weight: normal; font-size: 12px;">
            <th style="width:89px" rowspan="2" >
                Date
            </th>
            <th style="width:208px" rowspan="2"  >
                Journey Particulars( Booked by Travel Desk)
            </th>
            <th style="width:106px" rowspan="2" >
                Mode of Transportation
            </th>
            <th colspan="2" >
                Fares 
            </th>
            <th rowspan="2" style="width:100px">
                Advance Received
            </th>
            <th rowspan="2" style="width:206px">
                Remarks by Hod
            </th>
            <th rowspan="2" style="width:205px">
                Remarks by Travel Desk
            </th>
        </tr>
        <tr style="padding: 4px; background-color:#2781BA; color:#FFFFFF; font-weight: normal; font-size: 12px;">
            <th  style="width:107px">
                 Rs </th>
            <th style="width:58px">
                Paisa</th>
        </tr>
    </table>--%>
            <br />

            <div id="divClaims" runat="server" visible="false">
                <asp:GridView ID="JourneyDetailsGridView" runat="server" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="TRAVEL_DATE" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="80" />
                        <asp:BoundField DataField="JourneyParticulars" HeaderText="JourneyParticulars" ItemStyle-Width="200" />
                        <asp:BoundField DataField="MODE_OF_TRANSPOPRT_FZTXT" HeaderText="Mode of Transport" ItemStyle-Width="100" />
                        <asp:BoundField DataField="TICKET_FARE" HeaderText="Ticket Fare" ItemStyle-Width="100" />
                        <asp:BoundField DataField="ADVANCE" HeaderText="Advance" ItemStyle-Width="92" />
                        <%--        <asp:BoundField DataField="HOD_REMARKS"  HeaderText="Remarks by Hod"  ItemStyle-Width="196"/>
         <asp:BoundField DataField="TD_REMARKS"   HeaderText="Remarks by Travel Desk"  ItemStyle-Width="196"/>--%>
                    </Columns>
                </asp:GridView>

                <div>
                    <br />
                    <br />
                </div>

                <div>
                    <%--style="overflow:scroll; cursor: pointer; width:1300px;" >--%>
                    <asp:UpdatePanel ID="ippnlGrd" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdCorporateClaims" runat="server" AllowPaging="true" AutoGenerateColumns="false" OnPageIndexChanging="OnPageIndexChanging1" Width="100%">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>From Date
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextDateTravel" Visible="true" runat="server" Width="90px"></asp:TextBox>
                                            <Ajx:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="TextDateTravel" TargetControlID="TextDateTravel" Format="dd-MM-yyyy"></Ajx:CalendarExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="100" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>To Date
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="TextDateTravelTo" Visible="true" runat="server" Width="90px"></asp:TextBox>
                                            <Ajx:CalendarExtender ID="CE_TextDateTravelTo" runat="server" PopupButtonID="TextDateTravelTo" TargetControlID="TextDateTravelTo" Format="dd-MM-yyyy"></Ajx:CalendarExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="100" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>From
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="DDLOverflow">
                                                <asp:DropDownList ID="drpdwnFrom" runat="server" Visible="true" Style="font: normal normal normal 11px/17px Verdana !important; width: 130px; height: 22px;" onBlur="javascript:Compress_DDL(this,130)"
                                                    onChange="javascript:Compress_DDL(this,130);" onMouseDown="javascript:Expand_DDL(this)">
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="150" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>To
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="DDLOverflow">
                                                <asp:DropDownList ID="drpdwnTo" runat="server" Visible="true" Style="font: normal normal normal 11px/17px Verdana !important; width: 130px; height: 22px;" onBlur="javascript:Compress_DDL(this,130)"
                                                    onChange="javascript:Compress_DDL(this,130);" onMouseDown="javascript:Expand_DDL(this)">
                                                    <%--AutoPostBack="true" OnSelectedIndexChanged="drpdwnTo_SelectedIndexChanged">--%>
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="150" />
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>Mode
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpdwnMode" runat="server" Width="80px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>Expense Type
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="DDLOverflow">
                                                <asp:DropDownList ID="drpdwnEXPTYPE" runat="server" Visible="true" Style="font: normal normal normal 11px/17px Verdana !important; width: 130px; height: 22px;" onBlur="javascript:Compress_DDL(this,130)"
                                                    onChange="javascript:Compress_DDL(this,130);" onMouseDown="javascript:Expand_DDL(this)">
                                                </asp:DropDownList>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="150" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>Fares
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFares" Width="90px" runat="server" Text="0" AutoPostBack="true" OnTextChanged="txtFares_TextChanged"></asp:TextBox>
                                            <Ajx:FilteredTextBoxExtender ID="FTB_txtFares" runat="server" Enabled="true" TargetControlID="txtFares" ValidChars="." FilterType="Custom,Numbers"></Ajx:FilteredTextBoxExtender>
                                            <%--<Ajx:AutoCompleteExtender ID="ACE_txtFares" runat="server" Enabled="true" TargetControlID="txtFares" MinimumPrefixLength="1" EnableCaching="true" 
                                                CompletionSetCount="1" CompletionInterval="1" ServiceMethod="GetAmount" ContextKey="<%#((GridViewRow)Container).RowIndex%>" UseContextKey="true"></Ajx:AutoCompleteExtender>--%>
                                            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>Currency
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpdwncurrency" runat="server" Width="100px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>DA
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDA" Width="90px" runat="server" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>Loadging conveyance
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtLoadgingconveyanceRs" Width="90px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>Actual Loadging conveyance
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtActualLoadgingconveyanceRS" Width="90px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>Local conveyance
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtLocalconveyanceRs" Width="90px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>Misc Expenses Particulars
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtMiscExpencesParticulars" Width="100px" runat="server"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>Misc Expenses 
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtMiscExpencesParticularsRs" Width="90px" runat="server" />
                                            <%--AutoPostBack="true" OnTextChanged ="TxtMiscExpencesParticularsRs_TextChanged"></asp:TextBox>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <table>
                                                <tr>
                                                    <td>Total
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxttTotalRs" Width="90px" runat="server" Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>





                                    <%--     <asp:TemplateField>
                        <ItemTemplate>
                             <div style="padding-top: 100px">
                                 <asp:Button ID="btnAddClaims" runat="server" Text="Add"/>
                              </div>
                       </ItemTemplate>
                </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                            <%-- <script type="text/javascript" src="../../Scripts/jquery-1.4.1.min.js"></script>
                            <script type="text/javascript">
                                $(document).ready(function () {
                                    //$('#<%=grdCorporateClaims.ClientID %>').find('span[id$="txtFares"]').text('Your text.');
                                    $('#<%=grdCorporateClaims.ClientID %>').find('input:text[id$="txtFares"]').change(function () {
                                        var a = $('#<%=grdCorporateClaims.ClientID %>').find('span[id$="txtFares"]').val();
                                        alert(a);
                                    });
                                });
                            </script>--%>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="buttonrow">
                    <asp:Button ID="btnSave" runat="server" Text="Send for Approval" OnClick="btnSave_Click" Style="height: 26px" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
                </div>
            </div>
        </div>
        <%--<script type="text/javascript" src="../../Scripts/jquery-1.8.3.min.js"></script>--%>
        <script type="text/javascript">

            $(document).ready(function () {
                $("#<%=txtCounter2.ClientID %>,#<%=txtCounter3.ClientID %>,#<%=txtCounter4.ClientID %>,#<%=txtCounter5.ClientID %>,#<%=txtCounter6.ClientID %>").change(function () {
                    var Sum = parseInt($("#<%=txtCounter2.ClientID %>").val()) + parseInt($("#<%=txtCounter3.ClientID %>").val()) + parseInt($("#<%=txtCounter4.ClientID %>").val()) + parseInt($("#<%=txtCounter5.ClientID %>").val()) + parseInt($("#<%=txtCounter6.ClientID %>").val());
                    $("#<%=txtCounter7.ClientID %>").val(Sum / 5);
                });

            });
        </script>

        <%--<script type="text/javascript" src="../../Scripts/jquery-1.8.3.min.js"></script>--%>
        <script type="text/javascript" src="../../Scripts/JqblockUI.js"></script>
        <script lang="javascript" type="text/javascript">
            $(document).ready(function () {
                Page = Sys.WebForms.PageRequestManager.getInstance();
                Page.add_beginRequest(OnBeginRequest);
                Page.add_endRequest(endRequest);

                function OnBeginRequest(sender, args) {
                    $.blockUI();
                }
                function endRequest(sender, args) {
                    $.unblockUI();
                }
            });
        </script>
    </div>
</asp:Content>
