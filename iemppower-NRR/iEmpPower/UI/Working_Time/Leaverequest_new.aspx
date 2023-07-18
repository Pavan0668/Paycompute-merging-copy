<%@ Page Title="Leave Request" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Leaverequest_new.aspx.cs"
    Inherits="iEmpPower.UI.Working_Time.Leaverequest_new" EnableEventValidation="false" Culture="en-GB" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/tabcontent.js" type="text/javascript"></script>


    <%--<link type="text/css" href="http://ajax.microsoft.com/ajax/jquery.ui/1.8.5/themes/blitzer/jquery-ui.css" rel="Stylesheet" />--%>

    <style type="text/css">
        @media (max-width:600px) {
            #Grd_HolidayCalendar {
                width: 99%;
            }
        }

        #MainContent_WZ_LeaveReq td {
            text-align: left !important;
        }

        #__tab_MainContent_tcLeave_tabApply, #__tab_MainContent_tcLeave_tabOverview, #__tab_MainContent_tcLeave_TabQuota, #__tab_MainContent_tcLeave_TabQuota {
            height: 18.5px !important;
        }

        /*#MainContent_Grd_HolidayCalendar {
            overflow: scroll !important;
        }

        #MainContent_view5 {
            overflow: scroll !important;
        }*/


        td.Tp a {
            outline: medium none;
        }

            td.Tp a:hover {
                text-decoration: none;
            }

        a {
            color: #1155cc;
        }

        td.Tp a span {
            display: none;
            line-height: 16px;
            margin-left: 20px;
            margin-top: 0;
            padding: 14px 20px;
            width: 330px;
            z-index: 10;
            text-align: justify;
            font: normal normal normal 11px/15px "Helvetica Neue", "Lucida Grande", "Segoe UI",Arial,Helvetica,Verdana,sans-serif;
        }

        td.Tp a:hover span {
            background: none repeat scroll 0 0 #fffaf0;
            border: 1px solid #dca;
            color: #111;
            display: inline;
            position: absolute;
        }

        .callout {
            border: 0 none;
            left: -12px;
            position: absolute;
            top: 18px;
            z-index: 20;
        }

        td.Tp a span {
            box-shadow: 5px 5px 8px #ccc;
        }



        .href {
            color: White;
            font-weight: bold;
            text-decoration: none;
        }

        .CalCls {
            font: normal normal normal 11px/15px 'opensans',Verdana,Arial,Helvetica,sans-serif;
        }

            .CalCls table {
                border-collapse: collapse;
            }

            .CalCls a {
                text-decoration: none !important;
                color: #3e3e3e !important;
            }

            .CalCls td:hover {
                text-decoration: underline !important;
                background-color: #d5e5ef;
            }

        .TxtCalCls {
            letter-spacing: 1px;
            background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important;
            border: 1px solid #666666;
            margin-bottom: 4px;
            margin-left: 2px;
            padding: 2px;
        }

        .LiNone {
            list-style: none;
            margin: 0;
            padding: 0;
        }

            .LiNone li {
                padding: 4px 0;
            }

        .TxtResize {
            resize: none;
        }

        .TblCls {
            border-collapse: collapse;
        }

        .Td01 {
            color: #004080;
            font-size: 13px;
            width: 250px;
            padding: 3px;
            text-align: justify !important;
        }

        .Td02 {
            color: #004080;
            font-size: 13px;
            width: 10px;
            padding: 8px;
            text-align: center;
            line-height: 20px !important;
        }


        .HrCls {
            width: 100%;
            border: 0;
            height: 1px;
            background: #333;
            background-image: linear-gradient(to right, #333, #333, #ccc);
            padding: 0;
            margin: 3px 0;
        }


        .DivSpacer01 {
            width: 99%;
            clear: both;
            margin: 0 auto;
            padding: 5px 0;
        }

        .RedCls {
            width: 23px;
            height: 16px;
            background-color: Red;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .GreenCls {
            width: 23px;
            height: 16px;
            background-color: #008000; /*#23ba35*/
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .BlueCls {
            width: 23px;
            height: 16px;
            background-color: #6495ED;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .GrayCls {
            width: 23px;
            height: 16px;
            background-color: Gray;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .Fl {
            float: left;
        }

        .label {
            padding: 4px;
            position: relative;
            width: 151px;
            display: inline-block;
            margin-right: 2px;
            top: -5px;
            left: -5px; /*11px;*/
            color: #004080;
            font-size: 13px;
            text-decoration: none;
        }


        .labelMsg {
            padding: 4px;
            position: relative;
            width: 151px;
            display: inline-block;
            top: -5px;
            left: -5px; /*11px;*/
            color: #004080;
            font-size: 13px;
            text-decoration: none;
        }

        .textbox {
            border: 1px solid #666666;
            padding: 2px;
            margin-bottom: 4px;
            width: 200px;
            margin-left: 2px;
            resize: none;
        }

        .Td06 {
            color: #004080 !important;
            font-size: 13px !important;
            width: 190px !important;
            padding: 3px !important;
            text-align: justify !important;
            line-height: 25px !important;
        }

        .buttonsspacing {
            /*margin-right: 10px !important;*/
            margin-left: 15px !important;
        }
    </style>



    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Leave/Attd. Request</li>
                    </ol>
                </div>
                <h4 class="page-title">Leave/Attd. Request
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
    </div>



    <div class="card-box">

        <div id="real_time_chart" class="row">

            <div id="Leave" class="col-sm-3" style="overflow: hidden; border-right: dotted">
                <div style="width: 99%; margin: 0.5% auto 0% auto; clear: both; overflow: hidden; text-align: center;">
                    <div style="margin: 0; display: block; clear: both; padding: 0 0 00px 0; overflow: hidden;">
                        <div class="row">
                            <div class="offset-1" style="width: 25px;">
                                <asp:ImageButton ID="ImgBtnNext" runat="server" ImageUrl="~/images/PerviousImg.png"
                                    Height="28" value="" OnClick="ImgBtnNext_Click" CausesValidation="false" />
                            </div>
                            <div style="width: 150px;">
                            </div>
                            <div style="width: 25px;">
                                <asp:ImageButton ID="ImgBtnPrevious" runat="server" ImageUrl="~/images/NextImg.png"
                                    Height="28" value="" OnClick="ImgBtnPrevious_Click" CausesValidation="false" />
                            </div>
                        </div>
                        <asp:UpdatePanel ID="up" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-12">
                                    <asp:Calendar ID="Cal_Previous" runat="server" CssClass="CalCls" ShowGridLines="True"
                                        ShowNextPrevMonth="False" Width="200px" Height="190px" BorderColor="#CCCCCC"
                                        OnDayRender="Cal_Previous_DayRender" FirstDayOfWeek="Sunday" UseAccessibleHeader="False"
                                        OnSelectionChanged="Cal_Previous_SelectionChanged">
                                        <DayHeaderStyle BackColor="#FFFFCC" Font-Bold="True" />
                                        <DayStyle CssClass="Tp" />
                                        <OtherMonthDayStyle BackColor="#E9E9E9" />
                                    </asp:Calendar>
                                </div>
                                <br />
                                <div class="col-sm-12">
                                    <asp:Calendar ID="Cal_Current" runat="server" CssClass="CalCls" ShowGridLines="True"
                                        ShowNextPrevMonth="False" Width="200px" Height="190px" BorderColor="#CCCCCC"
                                        OnDayRender="Cal_Current_DayRender" FirstDayOfWeek="Sunday" UseAccessibleHeader="False"
                                        OnSelectionChanged="Cal_Current_SelectionChanged">
                                        <DayHeaderStyle BackColor="#FFFFCC" Font-Bold="True" />
                                        <DayStyle CssClass="Tp" />
                                        <OtherMonthDayStyle BackColor="#E9E9E9" />
                                    </asp:Calendar>
                                </div>
                                <br />
                                <div class="col-sm-12">
                                    <asp:Calendar ID="Cal_Next" runat="server" CssClass="CalCls" ShowGridLines="True"
                                        ShowNextPrevMonth="False" Width="200px" Height="190px" BorderColor="#CCCCCC"
                                        OnDayRender="Cal_Next_DayRender" FirstDayOfWeek="Sunday" UseAccessibleHeader="False"
                                        OnSelectionChanged="Cal_Next_SelectionChanged">
                                        <DayHeaderStyle BackColor="#FFFFCC" Font-Bold="True" />
                                        <DayStyle CssClass="Tp" />
                                        <OtherMonthDayStyle BackColor="#E9E9E9" />
                                    </asp:Calendar>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <%-- <asp:PostBackTrigger ControlID="Cal_Previous" />
                                    <asp:PostBackTrigger ControlID="Cal_Current" />
                                    <asp:PostBackTrigger ControlID="Cal_Next" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="col-sm-12" style="margin-top: 10px; margin-left: 10px;">
                            <div class="row">
                                <div style="width: 27px">
                                    <div class="GreenCls"></div>
                                </div>
                                <span><b>&nbsp;&nbsp;Approved</b></span>
                            </div>
                            <div class="row">
                                <div style="width: 27px">
                                    <div class="BlueCls"></div>
                                </div>
                                <span><b>&nbsp;&nbsp;Pending for approval</b></span>

                            </div>
                            <div class="row">
                                <div style="width: 27px">
                                    <div class="GrayCls"></div>
                                </div>
                                <span><b>&nbsp;&nbsp;Deletion requested</b></span>

                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-9">
                <div>
                    <ul class="nav nav-pills navtab-bg">

                        <li class="nav-item font-12">
                            <asp:LinkButton runat="server" ID="Tab1" class="nav-link p-2" OnClick="Tab1_Click" CausesValidation="false"><i class="fe-edit" ></i>
   Apply Leave/Attd.</asp:LinkButton></li>
                        <li class="nav-item font-12">
                            <asp:LinkButton runat="server" ID="Tab2" class="nav-link  p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-grid"></i>
    Overview of Leave/Attd.</asp:LinkButton></li>
                        <li class="nav-item font-12">
                            <asp:LinkButton runat="server" ID="Tab3" class="nav-link  p-2" OnClick="Tab3_Click" CausesValidation="false"><i class="fe-gift"></i>
    Leave Quota </asp:LinkButton></li>
                        <li class="nav-item font-12">
                            <asp:LinkButton runat="server" ID="Tab5" class="nav-link  p-2" OnClick="Tab5_Click" CausesValidation="false"><i class="fe-calendar"></i>
    Holiday Calendar </asp:LinkButton></li>
                        <li class="nav-item font-12" visible="false">
                            <asp:LinkButton runat="server" Visible="false" ID="Tab4" class="nav-link  p-2" OnClick="Tab4_Click" CausesValidation="false"><i class="fe-calendar"></i>
   Team Calendar (Leave) </asp:LinkButton></li>
                    </ul>

                    <div class="tabcontents">
                        <div id="view1" runat="server" visible="false">
                            <br />
                            <div class="header-title">Apply Leave/Attd.</div>
                            <hr class="HrCls" />
                            <div class="Cb">
                                <asp:UpdatePanel ID="up1" runat="server">
                                    <ContentTemplate>
                                        <asp:Wizard ID="WZ_LeaveReq" runat="server" DisplaySideBar="false" OnActiveStepChanged="WZ_LeaveReq_ActiveStepChanged"
                                            OnFinishButtonClick="WZ_LeaveReq_FinishButtonClick" Width="100%" OnNextButtonClick="WZ_LeaveReq_NextButtonClick" OnPreviousButtonClick="WZ_LeaveReq_PreviousButtonClick" ActiveStepIndex="0">
                                            <NavigationStyle />
                                            <NavigationButtonStyle CssClass="btn btn-xs btn-secondary" />
                                            <WizardSteps>
                                                <asp:WizardStep ID="WS_CreateLeaveReq" runat="server" Title="" StepType="Start" AllowReturn="true">
                                                    <div class="DivSpacer02 Cb" style="margin-bottom: 0px;">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-3 htCr">
                                                                    Type of Request&nbsp;<b>:</b>
                                                                </div>

                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="DropDownReqTyp" runat="server" CssClass=" txtDropDownwidth" TabIndex="1" OnSelectedIndexChanged="DropDownReqTyp_SelectedIndexChanged1" AutoPostBack="true">
                                                                    </asp:DropDownList>
                                                                </div>


                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 htCr">Request Sub-type&nbsp;<b>:</b></div>

                                                                <div class="col-sm-8">
                                                                    <asp:DropDownList ID="DDL_LeaveType" runat="server" CssClass=" txtDropDownwidth" OnSelectedIndexChanged="DDL_LeaveType_SelectedIndexChanged" AutoPostBack="true"
                                                                        TabIndex="2">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RFV_DDL_LeaveType" runat="server" ControlToValidate="DDL_LeaveType" Display="Dynamic"
                                                                        CssClass="lblValidation" ErrorMessage="Select Leave type" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>


                                                                    <p id="pnote" runat="server" visible="false" class="lblValidation">( Note: Comp-Off 4-6hrs Half Day. More than 6hrs Full Day)</p>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 htCr">
                                                                    From Date&nbsp;<b>:</b>
                                                                </div>

                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="TxtFromDate" runat="server" class="txtDropDownwidth" placeholder="DD/MM/YYYY"
                                                                        TabIndex="3" autocomplete="off" OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                    <Ajx:MaskedEditExtender ID="MEE_TxtFromDate" runat="server"
                                                                        CultureName="en-GB" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                                        MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                        OnInvalidCssClass="MaskedEditError" TargetControlID="TxtFromDate" AcceptAMPM="false" ClearTextOnInvalid="true" UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour" />
                                                                    <Ajx:CalendarExtender ID="CE_TxtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                        TargetControlID="TxtFromDate" PopupButtonID="TxtFromDate">
                                                                    </Ajx:CalendarExtender>


                                                                    <asp:RequiredFieldValidator ID="RFV_TxtFromDate" runat="server" ControlToValidate="TxtFromDate"
                                                                        CssClass="lblValidation" Display="Dynamic" ErrorMessage="Enter from date" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="REV_TxtFromDate" runat="server" Display="Dynamic"
                                                                        CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtFromDate"
                                                                        SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                    <asp:RangeValidator ID="RV_TxtFromDate" runat="server" ControlToValidate="TxtFromDate" Display="Dynamic"
                                                                        CssClass="lblValidation" MinimumValue='01/01/2010' MaximumValue='01/01/2019'
                                                                        ErrorMessage='<%# string.Format("Leave dates should be between {0} and {1}.", new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-2).ToString("dd/MM/yyyy"),new DateTime(DateTime.Today.Year, 12, 31).ToString("dd/MM/yyyy")) %>'
                                                                        Type="Date" ForeColor="Red"></asp:RangeValidator>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 htCr">
                                                                    To Date&nbsp;<b>:</b>
                                                                </div>

                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="TxtToDate" runat="server" class="txtDropDownwidth" placeholder="DD/MM/YYYY"
                                                                        TabIndex="4" autocomplete="off" OnTextChanged="TxtToDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                                    <Ajx:MaskedEditExtender ID="MEE_TxtToDate" runat="server"
                                                                        CultureName="en-GB" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                                        MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                        OnInvalidCssClass="MaskedEditError" TargetControlID="TxtToDate" AcceptAMPM="false" ClearTextOnInvalid="true"
                                                                        UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour" />
                                                                    <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                        TargetControlID="TxtToDate" PopupButtonID="TxtToDate">
                                                                    </Ajx:CalendarExtender>

                                                                    <asp:RequiredFieldValidator ID="RFV_TxtToDate" runat="server" ControlToValidate="TxtToDate"
                                                                        CssClass="lblValidation" Display="Dynamic" ErrorMessage="Enter to date" ForeColor="Red"></asp:RequiredFieldValidator>

                                                                    <asp:CompareValidator ID="CV_TxtToDate" runat="server" ControlToCompare="TxtFromDate"
                                                                        CssClass="lblValidation" ControlToValidate="TxtToDate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                                                                        Operator="GreaterThanEqual" Type="Date" ForeColor="Red"></asp:CompareValidator>
                                                                    <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic"
                                                                        CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtToDate"
                                                                        SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>

                                                                </div>
                                                            </div>
                                                            <asp:Panel ID="PnlFrmToTime" runat="server">
                                                                <div class="row">
                                                                    <div class="col-sm-3 htCr">
                                                                        From Time&nbsp;<b>:</b>
                                                                    </div>

                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="TxtFromTime" runat="server" class="txtDropDownwidth" placeholder="HH:mm"
                                                                            ValidationGroup="VGLeaveReq" TabIndex="5" AutoPostBack="true" OnTextChanged="TxtFromTime_TextChanged"></asp:TextBox>
                                                                        <Ajx:MaskedEditExtender ID="MEE_TxtFromTime" runat="server" AcceptNegative="Left" CultureName="en-GB"
                                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99:99" MaskType="Time"
                                                                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                            TargetControlID="TxtFromTime" />

                                                                        [Note : 24Hr format]&nbsp;
                                                <asp:RequiredFieldValidator ID="RFV_TxtFromTime" runat="server" ControlToValidate="TxtFromTime"
                                                    CssClass="lblValidation" Display="Dynamic" ErrorMessage="Enter From time" ValidationGroup="VGLeaveReq" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="REV_TxtFromTime" runat="server" Display="Dynamic"
                                                                            CssClass="lblValidation" ErrorMessage="Invalid from Time" ControlToValidate="TxtFromTime" ValidationGroup="VGLeaveReq"
                                                                            SetFocusOnError="True" ValidationExpression="^(2[0-3]|1[0-9]|0[0-9]|[^0-9][0-9]):([0-5][0-9]|[0-9])$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                                    </div>
                                                                </div>
                                                                <div class="row">
                                                                    <div class="col-sm-3 htCr">
                                                                        To Time&nbsp;<b>:</b>
                                                                    </div>

                                                                    <div class="col-sm-8">
                                                                        <asp:TextBox ID="TxtToTime" runat="server" class="txtDropDownwidth" placeholder="HH:mm"
                                                                            ValidationGroup="VGLeaveReq" TabIndex="6" OnTextChanged="TxtToTime_TextChanged" AutoPostBack="true"> </asp:TextBox>
                                                                        <Ajx:MaskedEditExtender ID="MEE_TxtToTime" runat="server" AcceptNegative="Left" CultureName="en-GB"
                                                                            DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99:99" MaskType="Time"
                                                                            MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                                                            TargetControlID="TxtToTime" />

                                                                        [Note : 24Hr format]&nbsp;
                                                <asp:RequiredFieldValidator ID="RFV_TxtToTime" runat="server" ControlToValidate="TxtToTime"
                                                    CssClass="lblValidation" Display="Dynamic" ErrorMessage="Enter From time" ValidationGroup="VGLeaveReq" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="REV_TxtToTime" runat="server" Display="Dynamic"
                                                                            CssClass="lblValidation" ErrorMessage="Invalid to Time" ControlToValidate="TxtToTime" ValidationGroup="VGLeaveReq"
                                                                            SetFocusOnError="True" ValidationExpression="^(2[0-3]|1[0-9]|0[0-9]|[^0-9][0-9]):([0-5][0-9]|[0-9])$" ForeColor="Red"></asp:RegularExpressionValidator>

                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="PnlMode" runat="server">
                                                                <div class="row">
                                                                    <div class="col-sm-3 htCr">
                                                                        Mode Of Leave&nbsp;<b>:</b>
                                                                    </div>

                                                                    <div class="col-sm-8">
                                                                        <asp:RadioButton ID="rbtnHalfDay" runat="server" GroupName="LeaveMode" Text="Half Day" TabIndex="7" OnCheckedChanged="rbtnHalfDay_CheckedChanged" AutoPostBack="true" />
                                                                        <asp:RadioButton ID="rbtnFullDay" runat="server" GroupName="LeaveMode" Text="Full Day" Checked="True" TabIndex="8" OnCheckedChanged="rbtnFullDay_CheckedChanged" AutoPostBack="true" />
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                            <asp:Panel ID="Pnl_2HalfDay" runat="server" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-sm-3 htCr">
                                                                        Half Day Duration&nbsp;<b>:</b>
                                                                    </div>

                                                                    <div class="col-sm-8">

                                                                        <asp:RadioButton ID="rbtn1Half" runat="server" GroupName="HalfLeaveMode" Text="1st Half" Checked="True" TabIndex="9" />
                                                                        <asp:RadioButton ID="rbtn2Half" runat="server" GroupName="HalfLeaveMode" Text="2nd Half" TabIndex="10" />

                                                                    </div>


                                                                </div>
                                                            </asp:Panel>

                                                            <asp:Panel ID="Pnl_Approver" runat="server">
                                                                <div class="row">
                                                                    <div class="col-sm-3 htCr">
                                                                        Approver&nbsp;<b>:</b>
                                                                    </div>

                                                                    <div class="col-sm-8">
                                                                        <asp:DropDownList ID="DDLApprover" runat="server" TabIndex="11"
                                                                            class="txtDropDownwidth">
                                                                        </asp:DropDownList>

                                                                        <asp:RequiredFieldValidator ID="RFV_DDLApprover" runat="server" ControlToValidate="DDLApprover"
                                                                            CssClass="lblValidation" Display="Dynamic" ErrorMessage="Select your approver" InitialValue="0" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <div class="row">
                                                                <div class="col-sm-3 htCr" valign="top">
                                                                    Reason for Request&nbsp;<b>:</b>
                                                                </div>

                                                                <div class="col-sm-8">
                                                                    <asp:TextBox ID="TxtNoteForApprover" runat="server" class="txtDropDownwidth" TextMode="MultiLine" placeholder="Enter Reason for Leave"
                                                                        Columns="9" TabIndex="12"></asp:TextBox>


                                                                    <asp:RequiredFieldValidator ID="RFV_TxtNoteForApprover" runat="server" ControlToValidate="TxtNoteForApprover"
                                                                        CssClass="lblValidation" Display="Dynamic" ErrorMessage="Enter any note for approver" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>

                                                            <div class="row">
                                                                <div class="Fr" style="width: 99%; color: Red;">
                                                                    <asp:Literal ID="LtrMsg" runat="server"></asp:Literal>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </asp:WizardStep>
                                                <asp:WizardStep ID="WS_CreateLeaveReqFinish" runat="server" Title="" StepType="Finish" AllowReturn="false">
                                                    <div class="DivSpacer02 Cb">

                                                        <div class="form-group">
                                                            <div class="row">
                                                                <div class="col-sm-3 htCr">
                                                                    Leave Type&nbsp;<b>:</b>
                                                                </div>
                                                                <div class="col-sm-8">
                                                                    <% Response.Write(DDL_LeaveType.SelectedItem.Text); %>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 htCr">
                                                                    From Date&nbsp;<b>:</b>
                                                                </div>
                                                                <div class="col-sm-8">
                                                                    <% Response.Write(TxtFromDate.Text); %>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 htCr">
                                                                    To Date&nbsp;<b>:</b>
                                                                </div>
                                                                <div class="col-sm-8">
                                                                    <% Response.Write(TxtToDate.Text); %>
                                                                </div>
                                                            </div>
                                                            <div id="divmol" class="row" runat="server">
                                                                <div class="col-sm-3 htCr">
                                                                    Mode Of Leave&nbsp;<b>:</b>
                                                                </div>
                                                                <div class="col-sm-8">
                                                                    <% Response.Write(rbtnHalfDay.Checked == bool.Parse("True") ? rbtnHalfDay.Text : rbtnFullDay.Text); %>
                                                                </div>
                                                            </div>
                                                            <div id="divft" class="row" runat="server">
                                                                <div class="col-sm-3 htCr">
                                                                    From Time&nbsp;<b>:</b>
                                                                </div>
                                                                <div class="col-sm-8">
                                                                    <% Response.Write(TxtFromTime.Text); %>
                                                                </div>
                                                            </div>
                                                            <div id="divtt" class="row" runat="server">
                                                                <div class="col-sm-3 htCr">
                                                                    To Time&nbsp;<b>:</b>
                                                                </div>
                                                                <div class="col-sm-8">
                                                                    <% Response.Write(TxtToTime.Text); %>
                                                                </div>
                                                            </div>
                                                            <div class="row" id="divApprover" runat="server">
                                                                <div class="col-sm-3 htCr">
                                                                    Approver&nbsp;<b>:</b>
                                                                </div>
                                                                <div class="col-sm-8">
                                                                    <% Response.Write(DDLApprover.SelectedItem.Text); %>
                                                                </div>
                                                            </div>
                                                            <div class="row">
                                                                <div class="col-sm-3 htCr">
                                                                    Reason for request&nbsp;<b>:</b>
                                                                </div>
                                                                <div class="col-sm-8">
                                                                    <% Response.Write(TxtNoteForApprover.Text); %>
                                                                </div>
                                                            </div>
                                                            <div class="Fr" style="width: 99%; color: Red;">
                                                                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </asp:WizardStep>
                                            </WizardSteps>
                                        </asp:Wizard>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="WZ_LeaveReq" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="DivSpacer02 Cb">
                                <div class="DivSpacer01">
                                    <div class="header-title">
                                        Delete Leave/Attd.
                                    </div>
                                </div>
                                <hr class="HrCls" />
                                <div class="DivSpacer01">
                                </div>
                                <div class="respovrflw">
                                    <asp:GridView ID="GV_SelectedDateLeaveView" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                        DataKeyNames="LEAVE_REQ_ID,TABLETYPE,STATUS" PageSize="10" OnRowCommand="GV_SelectedDateLeaveView_RowCommand"
                                        OnRowDeleting="GV_SelectedDateLeaveView_RowDeleting" OnPageIndexChanging="GV_SelectedDateLeaveView_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Slno">
                                                <EditItemTemplate>
                                                    <asp:Label ID="LblSlno1" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ATEXT" HeaderText="Type of leave/Attendance">
                                                <ItemStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BEGDA" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="From"></asp:BoundField>
                                            <asp:BoundField DataField="ENDDA" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="To"></asp:BoundField>
                                            <asp:BoundField DataField="BEGUZ" HeaderText="From time"></asp:BoundField>
                                            <asp:BoundField DataField="ENDUZ" HeaderText="To time"></asp:BoundField>
                                            <asp:BoundField DataField="TotalDays" HeaderText="Total Day(s)"></asp:BoundField>
                                            <asp:BoundField DataField="STATUS" HeaderText="Status"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnLeaveAttDelete" runat="server" CommandName="Delete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                        CausesValidation="false" OnClientClick="javascript:return confirm('Do you want to delete this request ?')"
                                                        Visible='<%# bool.Parse(string.Format("{0}",Eval("STATUS").ToString() =="DELETION REQUESTED"?"false":"true")) %>' CssClass="btn btn-xs btn-warning"><i class="fe-trash-2"></i></asp:LinkButton>

                                                </ItemTemplate>
                                                <ItemStyle VerticalAlign="Middle" HorizontalAlign="Center" Width="80px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <span style="color: red;">No Record found !</span>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                                <div class="DivSpacer01">
                                </div>
                            </div>
                        </div>
                        <div id="view2" runat="server" visible="false">
                            <br />
                            <div class="header-title">Overview of Leave/Attd.</div>
                            <hr class="HrCls" />
                            <br />
                            <asp:Label ID="LnlLeaveOverviewErrMsg" runat="server" CssClass="Fr lblValidation"></asp:Label>
                            <div class="row marginbtm7">
                                <div class="col-sm-3">
                                    <asp:TextBox ID="TxtLeaveOverView" runat="server" CssClass="txtDropDownwidth" placeholder="DD/MM/YYYY"
                                        ValidationGroup="VGLeaveOV"></asp:TextBox>
                                    <Ajx:MaskedEditExtender ID="MEE_TxtLeaveOverView" runat="server" AcceptNegative="Left"
                                        CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                        MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                        OnInvalidCssClass="MaskedEditError" TargetControlID="TxtLeaveOverView" />
                                    <Ajx:CalendarExtender ID="CE_TxtLeaveOverView" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                        TargetControlID="TxtLeaveOverView">
                                    </Ajx:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RFV_TxtLeaveOverView" runat="server" ControlToValidate="TxtLeaveOverView" ForeColor="Red"
                                        CssClass="lblValidation" ValidationGroup="VGLeaveOV" ErrorMessage="Enter From Date" Display="Dynamic"> </asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="BtnViewLeaveOverview" Width="70px" runat="server" Text="Display" OnClick="BtnViewLeaveOverview_Click"
                                        ValidationGroup="VGLeaveOV" />
                                    <asp:Button ID="BtnRestLeaveOverview" Width="70px" runat="server" Text="Reset" OnClick="BtnRestLeaveOverview_Click" CausesValidation="false" />
                                </div>
                            </div>
                            <br />
                            <div class="respovrflw">
                                <asp:GridView ID="GV_LeaveOverView" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                    DataKeyNames="LEAVE_REQ_ID" AllowSorting="true" ShowHeaderWhenEmpty="false" EmptyDataText="No Data Found!">
                                    <Columns>
                                        <%--OnSorting="GV_LeaveOverView_Sorting"--%>
                                        <asp:BoundField DataField="RowNumber" HeaderText="Slno"></asp:BoundField>
                                        <asp:BoundField DataField="LTYPE" HeaderText="Request Type"></asp:BoundField>
                                        <asp:BoundField DataField="ATEXT" HeaderText="Type of leave"></asp:BoundField>
                                        <asp:BoundField DataField="BEGDA" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="From"></asp:BoundField>
                                        <asp:BoundField DataField="ENDDA" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="To"></asp:BoundField>
                                        <asp:BoundField DataField="BEGUZ" HeaderText="From time"></asp:BoundField>
                                        <asp:BoundField DataField="ENDUZ" HeaderText="To time"></asp:BoundField>
                                        <asp:BoundField DataField="TotalDays" HeaderText="Total Days"></asp:BoundField>
                                        <asp:BoundField DataField="STATUS" HeaderText="Status"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="row col-md-12">
                                <div class="col-md-3" style="margin-top: 5px" id="divpendingrecordcount" runat="server"></div>
                                <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                    <asp:Repeater ID="RptrLeaveOverViewPager" runat="server">
                                        <ItemTemplate>
                                            <ul class="pagination pagination-rounded" style="display: inline-block">
                                                <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                </li>
                                            </ul>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </div>
                        <div id="view3" runat="server" visible="false">
                            <br />
                            <div class="header-title">Leave Quota</div>
                            <hr class="HrCls" />
                            <br />
                            <div class="row marginbtm7">
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="DDLLeaveQuotaYear" runat="server" TabIndex="10" CssClass="txtDropDownwidth" ValidationGroup="VGqu"></asp:DropDownList>
                                    <%-- <Ajx:CascadingDropDown ID="CCD_DDLLeaveQuotaYear" runat="server" ServicePath="~/WebService/Service.asmx" ServiceMethod="GetQuotaYear" Category="QYear"
                                                    UseContextKey="false" TargetControlID="DDLLeaveQuotaYear">
                                                </Ajx:CascadingDropDown>--%>
                                </div>
                                <div class="col-sm-3">
                                    <asp:Button ID="BtnViewLeaveQuotaView" Width="80px" runat="server" TabIndex="13" Text="Display" OnClick="BtnViewLeaveQuotaView_Click"
                                        ValidationGroup="VGqu" />
                                    <asp:Button ID="BtnViewLeaveQuotaReset" Width="80px" runat="server" TabIndex="14" Text="Reset" OnClick="BtnViewLeaveQuotaReset_Click"
                                        ValidationGroup="VGqu" />
                                </div>
                                <div class="col-md-7 text-right" id="divcnt" runat="server"></div>
                            </div>
                            <br />
                            <div class=" respovrflw">
                                <asp:GridView ID="GV_LeaveQuota" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Slno">
                                            <EditItemTemplate>
                                                <asp:Label ID="LblSlno1" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ATEXT" HeaderText="Type of leave" SortExpression="ATEXT" />
                                        <asp:BoundField DataField="LEAVE_QUOTA_START_DATE" DataFormatString="{0:dd-MMM-yyyy}"
                                            HeaderText="Deduction from" SortExpression="LEAVE_QUOTA_START_DATE"></asp:BoundField>
                                        <asp:BoundField DataField="LEAVE_QUOTA_END_DATE" DataFormatString="{0:dd-MMM-yyyy}"
                                            HeaderText="Deduction to" SortExpression="LEAVE_QUOTA_END_DATE"></asp:BoundField>
                                        <asp:BoundField DataField="ANZHL" HeaderText="Entitlement" SortExpression="ANZHL"></asp:BoundField>
                                        <asp:BoundField DataField="KVERB" HeaderText="Entitlement used" SortExpression="KVERB"></asp:BoundField>
                                        <asp:BoundField DataField="AVAILABLE_DAYS" HeaderText="Available days" SortExpression="AVAILABLE_DAYS"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="DivSpacer01">
                                <asp:Repeater ID="Repeater1" runat="server">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnRptOverView" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' ValidationGroup="Vg01"
                                            Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed" CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>


                        <div id="view5" runat="server" visible="false">
                            <br />
                            <asp:UpdatePanel ID="Panel1" runat="server">
                                <ContentTemplate>
                                    <div class="header-title">Holiday Calendar</div>

                                    <div class="DivSpacer01">
                                        <asp:Label ID="Label2" runat="server" Text="" CssClass="lblValidation hidden"></asp:Label>
                                    </div>

                                    <div class="row marginbtm7">

                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlHolidayYear" runat="server" TabIndex="1" CssClass="txtDropDownwidth"
                                                OnSelectedIndexChanged="ddlHolidayYear_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>&nbsp;&nbsp;
                                        </div>
                                        <div class="col-md-10 text-right" id="divHCcnt" runat="server"></div>
                                    </div>
                                    <br />
                                    <asp:GridView ID="Grd_HolidayCalendar" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                                <ItemTemplate>
                                                    <%# Eval("HOLIDAYDATE","{0:dd-MM-yyyy}") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Day" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                                <ItemTemplate>
                                                    <%# Eval("HOLIDAYDATE","{0:dddd}") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Holidays">
                                                <ItemTemplate>
                                                    <%# Eval("HOLIDAYS") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <%# Eval("KLASSTXT") %>
                                                </ItemTemplate>
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>



                        <div id="view4" style="display: none" runat="server" visible="false">
                            <br />
                            <asp:UpdatePanel ID="UD_LeaveQuotaOverView" runat="server">
                                <ContentTemplate>


                                    <div class="header-title">Team Calender</div>
                                    <hr class="HrCls" />
                                    <br />
                                    <asp:Label ID="TClbl" runat="server" CssClass="lblValidation hidden"></asp:Label>

                                    <div class="row marginbtm7">
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="DDLEmpList" runat="server" CssClass="txtDropDownwidth" TabIndex="1">
                                            </asp:DropDownList>

                                            <Ajx:CascadingDropDown ID="CDD_DDLEmpList" runat="server" Category="EmpList" EmptyText="- SELECT -" EmptyValue="0"
                                                LoadingText="[LOADING Emplyoee List....]" PromptText="- SELECT -" PromptValue="0" TargetControlID="DDLEmpList"
                                                ServicePath="~/WebService/Service.asmx" ServiceMethod="GetEmpTC">
                                            </Ajx:CascadingDropDown>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="DDLMonth" runat="server" CssClass="txtDropDownwidth">
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="DDLYears" runat="server" TabIndex="10" CssClass="txtDropDownwidth" ValidationGroup="VGqu"></asp:DropDownList>
                                            <Ajx:CascadingDropDown ID="CCD_DDLYears" runat="server" ServicePath="~/WebService/Service.asmx" ServiceMethod="GetTCYear" Category="QYear"
                                                UseContextKey="false" TargetControlID="DDLYears">
                                            </Ajx:CascadingDropDown>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="BtnShowTeamCalendar" runat="server" Text="Show" OnClick="BtnShowTeamCalendar_Click" CausesValidation="false" />
                                        </div>
                                        <div class="offset-2 col-sm-2 ">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <div class="GreenCls"></div>
                                                    </td>
                                                    <td><b>Absent</b></td>
                                                    <td>
                                                        <div class="BlueCls"></div>
                                                    </td>
                                                    <td><b>Sent</b></td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>



                                    <div class="respovrflw">
                                        <asp:GridView ID="GV_TeamCalendar" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager"
                                            PageSize="1">
                                        </asp:GridView>
                                    </div>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>





                    </div>
                </div>

            </div>
        </div>
    </div>

</asp:Content>
