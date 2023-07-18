<%@ Page Title="Record Working Time" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" Inherits="UI_Working_Time_record_working_time"
    Theme="SkinFile" Culture="en-GB" CodeBehind="recordworking_time.aspx.cs" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js"> </script>
     <script src="https://cdn.rawgit.com/rainabba/jquery-table2excel/1.1.0/dist/jquery.table2excel.min.js"></script>

    <style type="text/css">
        .btn {
  pointer-events: auto;
  cursor: pointer;
}

        #ContentPlaceHolder1_MainContent_grdRecordTime div .label {
            text-align: left !important;
        }

        .txtsize {
            width: 40px !important;
        }

        .modalPopupDefault {
            width: 98vw;
        }

        #MainContent_UPRecordTime {
            width: 100vw !important;
            left: 0 !important;
            /*right:25px !important;*/
        }

        #MainContent_bdpFrom_Image {
            width: 28px !important;
        }

        .resize {
            resize: none !important;
        }

        .GreenCls {
            width: 23px;
            height: 16px;
            background-color: #23ba35;
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

        .RedCls {
            width: 23px;
            height: 16px;
            background-color: #f24343;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .RWTTxtBoX {
            width: 70px !important;
        }


        .modalBackground {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 700px;
            height: 500px;
        }

        .PnlPadding {
            padding-left: 10px;
        }

        .btnsize {
            font-size: 11px !important;
            color: #FFFFFF;
            font: lighter;
            font-weight: 100 !important;
        }

        #divbtngroup input[type='Submit'] {
            margin: 2px !important;
        }

        /*.completionList {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            background-color: #FFFFFF;
            font-size: xx-small;
        }

        .listItem {
            color: #1C1C1C;
        }

        .itemHighlighted {
            background-color: #dbf2ff;
        }*/

        .shw {
            display: block;
        }
    </style>
    <script src="../../Utilities/ValidationMessages.js" type="text/javascript"></script>
    <script src="../../Utilities/Validations.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Employees_Populateda(sender, e) {
            var autoList = $find("AutoCompleteExa").get_completionList();
            for (i = 0; i < autoList.childNodes.length; i++) {
                var imgeUrl = autoList.childNodes[i]._value;
                var text = autoList.childNodes[i].firstChild.nodeValue;
                autoList.childNodes[i]._value = text;
                autoList.childNodes[i].innerHTML = imgeUrl;
            }
        }

        function OnEmployeeSelecteda(source, eventArgs) {
           // $("#ContentPlaceHolder1_MainContent_grdRecordTime_lnknewAct_0").click();
            document.getElementById("ContentPlaceHolder1_MainContent_grdRecordTime_lnknewAct_0").click();
        }

        function ValidateEmptyValue() {

            clearDirty();
            var hours = document.getElementById("<%= hdHours.ClientID %>").value
            document.getElementById("<%= lblMessageBoard.ClientID %>").style.color = "red";
            var dd = new Array();
            //        var txt = '<%= grdRecordTime.ClientID %>';
            var gv = document.getElementById("<%= grdRecordTime.ClientID %>");
            var tb = gv.getElementsByTagName("input");


            dd = gv.getElementsByTagName("select");
            for (var i = 2; i < dd.length; i = i + 3) {
                if (dd.item(i).value == "0") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgSelectAttributeType
                    dd[i].focus();
                    return false;
                }
            }

            for (var j = 3; j < tb.length; j = j + 10) {
                if (tb[j].type == "text") {
                    if (isNaN(tb[j].value)) {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                        tb[j].focus();
                        return false;
                    }
                    if (parseFloat(tb[j].value) == "0") {
                        document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                        tb[j].focus();
                        return false;
                    }
                }
            }
            for (var j = 4; j < tb.length; j = j + 10) {
                if (isNaN(tb[j].value)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                    tb[j].focus();
                    return false;
                }
                if (parseFloat(tb[j].value) == "0") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                    tb[j].focus();
                    return false;
                }
            }
            for (var j = 5; j < tb.length; j = j + 10) {
                if (isNaN(tb[j].value)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                    tb[j].focus();
                    return false;
                }
                if (parseFloat(tb[j].value) == "0") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                    tb[j].focus();
                    return false;
                }
            }
            for (var j = 6; j < tb.length; j = j + 10) {
                if (isNaN(tb[j].value)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                    tb[j].focus();
                    return false;
                }
                if (parseFloat(tb[j].value) == "0") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                    tb[j].focus();
                    return false;
                }
            }
            for (var j = 7; j < tb.length; j = j + 10) {
                if (isNaN(tb[j].value)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                    tb[j].focus();
                    return false;
                }
                if (parseFloat(tb[j].value) == "0") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                    tb[j].focus();
                    return false;
                }
            }
            for (var k = 8; k < tb.length; k = k + 10) {
                if (isNaN(tb[k].value)) {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgHoursFormat;
                    tb[k].focus();
                    return false;
                }
                if (parseFloat(tb[k].value) == "0") {
                    document.getElementById("<%= lblMessageBoard.ClientID %>").innerText = msgZeroNotHours;
                    tb[k].focus();
                    return false;
                }
            }

        }
        function validateTextBox(textBoxId) {
            var textBoxRef = document.getElementById(textBoxId);
            var floatValue = parseFloat(textBoxRef.value);

            if (isNaN(floatValue) == true)
                textBoxRef.value = '0';
            else
                textBoxRef.value = floatValue;
        }
        function setFocus(Target) {
            if (window.event.keyCode == "13")
                document.getElementById(Target).focus();
        }

        function setDirty() {
            document.body.onbeforeunload = showMessage;
            //debugger;      
            document.getElementById("DirtyLabel").className = "show";
        }
        function clearDirty() {
            document.body.onbeforeunload = "";
            document.getElementById("DirtyLabel").className = "hide";
        }

        function showMessage() {
            return "If you click OK, the changes you have made will be lost."
        }
        function setControlChange() {
            if (typeof (event.srcElement) != 'undefined') {
                event.srcElement.onchange = setDirty;
            }
        }

        //function pageLoad(sender, args) { alert('<%= Session["AddVal"] %>'); }

        <%--var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    alert('<%= Session["AddVal"] %>');
                }
            });
        };--%>
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        prm.add_endRequest(function () {
            alert('<%= Session["AddVal"] %>');
        });
    </script>

    <%--<script type="text/javascript">

                    function Add() {
                        alert('<%= Session["AddVal"] %>');
                        if ('<%= Session["AddVal"] %>' == 0)
                            document.getElementById("ContentPlaceHolder1_MainContent_grdRecordTime_lnknewAct_0").className = "shw";
                        else
                            document.getElementById("ContentPlaceHolder1_MainContent_grdRecordTime_lnknewAct_0").className = "hidden";
                    }
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    prm.add_endRequest(Add);
                </script>--%>
    <div id="DirtyLabel" style="color: Red;" class="hide">
    </div>
    <span class="hidden">
        <asp:Button ID="btnEntryKey" runat="server" Text="" /></span>

    <asp:HiddenField ID="hdHours" runat="server" />

    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Record Working Time</li>
                    </ol>
                </div>
                <h4 class="page-title">Record Working Time
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>

    </div>

    <div class="card-box">

        <asp:UpdatePanel ID="UPRecordTime" runat="server">
            <ContentTemplate>

                <div class="row Fr">

                    <asp:LinkButton ID="lnkHL_RWT" runat="server" OnClick="HL_RWT_Click" CausesValidation="false" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right">
  <i class="mdi mdi-plus"></i> Create Timesheet
</asp:LinkButton>

<asp:LinkButton ID="lnkbtnClose" runat="server" OnClick="btnClose_Click" CausesValidation="false" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right">
  <i class="mdi mdi-step-backward"></i> Back
</asp:LinkButton>

                </div>
                </div>

                <div id="real_time_chart" style="">
                    <div id="divcal" runat="server">
                        <div id="Leave" style="width: 99%">
                            <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg"></asp:Label>
                            <br />
                            <div style="width: 99%; margin: 0.5% auto 0% auto; clear: both; overflow: hidden; text-align: center;">
                                <div style="margin: 0; display: block; clear: both; padding: 0 0 00px 0; overflow: hidden;">
                                    <div style="width: 20px; float: left; margin: 0.5% 0.5% 1% 1%;">
                                        <asp:ImageButton ID="btnPrev" runat="server" ImageUrl="~/images/PerviousImg.png"
                                            Height="28" value="" OnClick="btnPrev_Click" CausesValidation="false" OnClientClick="clearDirty();"
                                            meta:resourcekey="btnPrevResource1" />
                                    </div>
                                    <div style="width: 200px; height: 210px; float: left; margin: 0.5% 0.5% 1% 1%; padding: 3px;">
                                        <asp:Calendar ID="Calendar1" runat="server" CssClass="CalCls" ShowGridLines="True" meta:resourcekey="Calendar1Resource1"
                                            ShowNextPrevMonth="False" Width="200px" Height="190px" BorderColor="#CCCCCC" FirstDayOfWeek="Sunday"
                                            UseAccessibleHeader="False" OnSelectionChanged="Calendar1_SelectionChanged">
                                            <DayHeaderStyle BackColor="#FFFFCC" Font-Bold="True" />
                                            <DayStyle CssClass="Tp" />
                                            <OtherMonthDayStyle BackColor="#E9E9E9" />
                                        </asp:Calendar>
                                    </div>
                                    &nbsp;
                                    <div style="width: 200px; height: 200px; float: left; margin: 0.5% 0.5% 1% 1%; padding: 3px;">
                                        <asp:Calendar ID="Calendar2" runat="server" CssClass="CalCls" ShowGridLines="True" meta:resourcekey="Calendar2Resource1"
                                            ShowNextPrevMonth="False" Width="200px" Height="190px" BorderColor="#CCCCCC" FirstDayOfWeek="Sunday"
                                            UseAccessibleHeader="False" OnSelectionChanged="Calendar2_SelectionChanged">
                                            <DayHeaderStyle BackColor="#FFFFCC" Font-Bold="True" />
                                            <DayStyle CssClass="Tp" />
                                            <OtherMonthDayStyle BackColor="#E9E9E9" />
                                        </asp:Calendar>
                                    </div>
                                    &nbsp;
                                    <div style="width: 200px; height: 200px; float: left; margin: 0.5% 0.5% 1% 1%; padding: 3px;">
                                        <asp:Calendar ID="Calendar3" runat="server" CssClass="CalCls" ShowGridLines="True"
                                            ShowNextPrevMonth="False" Width="200px" Height="190px" BorderColor="#CCCCCC" FirstDayOfWeek="Sunday"
                                            UseAccessibleHeader="False" OnSelectionChanged="Calendar3_SelectionChanged">
                                            <DayHeaderStyle BackColor="#FFFFCC" Font-Bold="True" />
                                            <DayStyle CssClass="Tp" />
                                            <OtherMonthDayStyle BackColor="#E9E9E9" />
                                        </asp:Calendar>
                                    </div>
                                    <div style="width: 25px; float: left; margin: 0.5% 0.5% 1% 1%;">
                                        <asp:ImageButton ID="btnNext" runat="server" ImageUrl="~/images/NextImg.png" OnClientClick="clearDirty();"
                                            Height="28" value="" OnClick="btnNext_Click" CausesValidation="false" />
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="Cb">

                            <div class="clear">
                            </div>
                            <br />
                            <div class="form-group ">
                                <div class="row">
                                    <div class="col-md-1" style="width: 37px;"></div>
                                    <div class="" style="width: 27px">
                                        <div class="BlueCls"></div>
                                    </div>
                                    <div class="col-sm-2" style="width: 163px"><b>Pending for Approval</b></div>

                                    <div class="" style="width: 27px">
                                        <div class="GreenCls"></div>
                                    </div>
                                    <div class="col-sm-1" style="width: 90px"><b>Approved</b></div>

                                    <div class="" style="width: 27px">
                                        <div class="RedCls"></div>
                                    </div>
                                    <div class="col-sm-2"><b>Absent</b></div>
                                </div>
                            </div>
                            <div id="LeaveDetails" runat="server" class="col-sm-offset-1 respovrflw hidden">
                                <asp:GridView ID="GV_SelectedDateLeaveView" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Slno">
                                            <EditItemTemplate>
                                                <asp:Label ID="LblSlno1" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ATEXT" HeaderText="Type of leave">
                                            <ItemStyle />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BEGDA" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="From">
                                            <ItemStyle Width="12%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ENDDA" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="To">
                                            <ItemStyle Width="12%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TotalDays" HeaderText="Total Day(s)">
                                            <ItemStyle Width="15%" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <asp:HiddenField ID="hiddenRowCount" runat="server" />
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <div style="margin-left: 55px">
                    </div>
                    <div style="width: 99%;">
                        <%-- <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="HL_RWT" TargetControlID="blnkBtnSowReq" BehaviorID="Mp2"
                            BackgroundCssClass="popUpStyle">
                        </cc1:ModalPopupExtender>--%>
                    </div>
                    <asp:LinkButton ID="blnkBtnSowReq" runat="server"></asp:LinkButton>
                    <asp:Panel ID="pnlpopUp" runat="server" Visible="false">
                        <asp:Panel runat="server" ID="PnlRWT">
                            <div runat="server" id="RwtDiv">
                                <asp:Label ID="LblPopUp" runat="server" CssClass="lblMsg"></asp:Label><br />
                                <div class="DivSpacer01"></div>
                                <div class="form-group">
                                    <div class="row">
                                        <div style="width: 40px;">
                                            <asp:Button ID="btnPreviousWeek" runat="server" Text="&lt;" Font-Bold="True" OnClientClick="clearDirty();" OnClick="btnPreviousWeek_Click" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="bdpFrom" runat="server" CssClass="txtDropDownwidth" autocomplete="off" Enabled="false"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MEE_bdpFrom" runat="server" CultureName="en-GB" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="bdpFrom"
                                                AcceptAMPM="false" ClearTextOnInvalid="true" UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour" />
                                            <cc1:CalendarExtender ID="CE_bdpFrom" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="bdpFrom" PopupButtonID="bdpFrom">
                                            </cc1:CalendarExtender>
                                        </div>
                                        <div class="col-sm-1" style="width: 50px; text-align: center;"><b>To</b></div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="bdpTo" runat="server" CssClass="txtDropDownwidth"
                                                autocomplete="off" Enabled="false"></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MEE_bdpTo" runat="server" CultureName="en-GB" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="bdpTo" AcceptAMPM="false" ClearTextOnInvalid="true"
                                                UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour" />
                                            <cc1:CalendarExtender ID="CE_bdpTo" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="bdpTo" PopupButtonID="bdpTo">
                                            </cc1:CalendarExtender>
                                        </div>
                                        <div class="col-sm-1">
                                            <asp:Button ID="btnNextWeek" runat="server" Text="&gt;" OnClick="btnNextWeek_Click"
                                                meta:resourcekey="btnNextWeekResource1" OnClientClick="clearDirty();" />
                                            <asp:Button ID="btnGo" runat="server" Text="Go" OnClientClick="clearDirty();" meta:resourcekey="btnGoResource1" Visible="false" />
                                        </div>

                                    </div>
                                </div>
                                <br />
                                <div class="respovrflw">

                                    <asp:GridView ID="grdRecordTime" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                                        OnRowCreated="grdRecordTime_RowCreated" OnRowDataBound="grdRecordTime_RowDataBound" Width="99%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Project" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpdwnOrder" runat="server" AppendDataBoundItems="True" class="RWTTxtBoX"
                                                        AutoPostBack="true" OnSelectedIndexChanged="drpdwnOrder_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFV_drpdwnOrder" runat="server" ErrorMessage="*"
                                                        ForeColor="Red" ControlToValidate="drpdwnOrder" ValidationGroup="VG1" InitialValue="0"></asp:RequiredFieldValidator>

                                                </ItemTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WBS" HeaderStyle-CssClass="">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpdwnWbs" runat="server" AppendDataBoundItems="True" AutoPostBack="true" OnSelectedIndexChanged="drpdwnWbs_SelectedIndexChanged">
                                                        <%----%>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFV_drpdwnWbs" runat="server" ErrorMessage="*" ForeColor="Red"
                                                        ControlToValidate="drpdwnWbs" ValidationGroup="VG1" InitialValue="0"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Activity" HeaderStyle-CssClass="">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpdwnActtype" runat="server" Visible="false" AppendDataBoundItems="True">
                                                    </asp:DropDownList>

                                                    <asp:TextBox ID="txtmainSearch" runat="server" placeholder="Search"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="EmpNameAutoCompleteExtender" runat="server" TargetControlID="txtmainSearch" FirstRowSelected="True"
                                                        ServiceMethod="getactivits" ServicePath="~/WebService/Service.asmx" OnClientPopulated="Employees_Populateda"
                                                        MinimumPrefixLength="1" EnableCaching="false" CompletionSetCount="5" CompletionInterval="1" CompletionListCssClass="completionListmain"
                                                        BehaviorID="AutoCompleteExa" OnClientItemSelected="OnEmployeeSelecteda">
                                                    </cc1:AutoCompleteExtender>
                                                    <asp:LinkButton ID="lnknewAct" runat="server" CssClass="hidden" OnClick="lnknewAct_Click">ADD</asp:LinkButton>

                                                </ItemTemplate>
                                                <%--ServicePath="~/WebService/Service.asmx"--%>


                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Attendance type" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="drpdwnAttabsType" runat="server" AppendDataBoundItems="True"
                                                        meta:resourcekey="drpdwnAttabsTypeResource1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFV_drpdwnAttabsType" runat="server" ErrorMessage="*"
                                                        ForeColor="Red" ControlToValidate="drpdwnAttabsType" ValidationGroup="VG1" InitialValue="0"></asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblStaffGrade" runat="server" Text="Total" CssClass="label" Width="32px"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Staff Grade" HeaderStyle-CssClass="hd-small" meta:resourcekey="TemplateFieldResource5" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtStaffGrade" runat="server" Width="18px" Style="text-align: right;"
                                                        meta:resourcekey="txtStaffGradeResource1"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hours" HeaderStyle-CssClass="hd-small" meta:resourcekey="TemplateFieldResource6">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTotal" runat="server" Width="35px" ReadOnly="True" Style="text-align: right;"
                                                        meta:resourcekey="txtTotalResource1"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblHours" runat="server" CssClass="label" Width="25px" Style="text-align: right;"
                                                            meta:resourcekey="lblHoursResource1"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SUN" HeaderStyle-CssClass="hd-small" meta:resourcekey="TemplateFieldResource13">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSUN" runat="server" CssClass="" Width="35px" MaxLength="5"
                                                        Style="text-align: right;"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtSUN_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtSUN" FilterType="Custom, Numbers" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RangeValidator ID="rvtxtSUN" runat="server" ControlToValidate="txtSUN" MinimumValue="0"
                                                        MaximumValue="24" Type="Double" ForeColor="Red" ErrorMessage="*" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblSun" runat="server" CssClass="label" Width="90%" Style="text-align: right;"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MON" HeaderStyle-CssClass="hd-small" meta:resourcekey="TemplateFieldResource7">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMON" runat="server" Width="35px" MaxLength="5" meta:resourcekey="txtMONResource1"
                                                        Style="text-align: right;"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtMON_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtMON" FilterType="Custom, Numbers" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RangeValidator ID="rvtxtMON" runat="server" ControlToValidate="txtMON" MinimumValue="0"
                                                        MaximumValue="24" Type="Double" ForeColor="Red" ErrorMessage="*" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblMon" runat="server" CssClass="label" Width="90%" Style="text-align: right;"
                                                            meta:resourcekey="lblMonResource1"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TUE" HeaderStyle-CssClass="hd-small" meta:resourcekey="TemplateFieldResource8">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTUE" runat="server" Width="35px" MaxLength="5" Style="text-align: right;"
                                                        meta:resourcekey="txtTUEResource1"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtTUE_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtTUE" FilterType="Custom, Numbers" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RangeValidator ID="rvtxtTUE" runat="server" ControlToValidate="txtTUE" MinimumValue="0"
                                                        MaximumValue="24" Type="Double" ForeColor="Red" ErrorMessage="*" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblTues" runat="server" CssClass="label" Width="90%" Style="text-align: right;"
                                                            meta:resourcekey="lblTuesResource1"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WED" HeaderStyle-CssClass="hd-small" meta:resourcekey="TemplateFieldResource9">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtWED" runat="server" Width="35px" MaxLength="5" Style="text-align: right;"
                                                        meta:resourcekey="txtWEDResource1"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtWED_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtWED" FilterType="Custom, Numbers" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RangeValidator ID="rvtxtWED" runat="server" ControlToValidate="txtWED" MinimumValue="0"
                                                        MaximumValue="24" Type="Double" ForeColor="Red" ErrorMessage="*" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblWed" runat="server" CssClass="label" Width="90%" Style="text-align: right;"
                                                            meta:resourcekey="lblWedResource1"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="THU" HeaderStyle-CssClass="hd-small" meta:resourcekey="TemplateFieldResource10">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTHU" runat="server" Width="35px" MaxLength="5" Style="text-align: right;"
                                                        meta:resourcekey="txtTHUResource1"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtTHU_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtTHU" FilterType="Custom, Numbers" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RangeValidator ID="rvtxtTHU" runat="server" ControlToValidate="txtTHU" MinimumValue="0"
                                                        MaximumValue="24" Type="Double" ForeColor="Red" ErrorMessage="*" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblThu" runat="server" CssClass="label" Width="90%" Style="text-align: right;"
                                                            meta:resourcekey="lblThuResource1"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FRI" HeaderStyle-CssClass="hd-small" meta:resourcekey="TemplateFieldResource11">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFRI" runat="server" Width="35px" MaxLength="5" Style="text-align: right;"
                                                        meta:resourcekey="txtFRIResource1"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtFRI_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtFRI" FilterType="Custom, Numbers" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RangeValidator ID="rvtxtFRI" runat="server" ControlToValidate="txtFRI" MinimumValue="0"
                                                        MaximumValue="24" Type="Double" ForeColor="Red" ErrorMessage="*" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblFri" runat="server" CssClass="label" Width="90%" Style="text-align: right;"
                                                            meta:resourcekey="lblFriResource1"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SAT" HeaderStyle-CssClass="hd-small" meta:resourcekey="TemplateFieldResource12">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSAT" runat="server" CssClass="textbox" Width="35px" MaxLength="5"
                                                        Style="text-align: right;" meta:resourcekey="txtSATResource1"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="txtSAT_FilteredTextBoxExtender" runat="server" Enabled="True"
                                                        TargetControlID="txtSAT" FilterType="Custom, Numbers" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                    <asp:RangeValidator ID="rvtxtSAT" runat="server" ControlToValidate="txtSAT" MinimumValue="0"
                                                        MaximumValue="24" Type="Double" ForeColor="Red" ErrorMessage="*" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblSAt" runat="server" CssClass="label" Width="90%" Style="text-align: right;"
                                                            meta:resourcekey="lblSAtResource1"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REMARKS" HeaderStyle-CssClass="hd-small">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtREMARKS" runat="server" TextMode="MultiLine" Width="160px"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTB_txtREMARKS" runat="server" TargetControlID="txtREMARKS"
                                                        FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters"
                                                        ValidChars="./[]()&,\-@:;+=%$* ">
                                                    </cc1:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <FooterTemplate>
                                                    <div>
                                                        <asp:Label ID="lblRemarks" runat="server" CssClass="label" Visible="false"></asp:Label>
                                                    </div>
                                                </FooterTemplate>
                                                <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource14">
                                                <ItemTemplate>
                                                    <asp:Button ID="ButtonRemove" runat="server" Text="Remove" OnClientClick="clearDirty();"
                                                        OnClick="ButtonRemove_Click" meta:resourcekey="ButtonRemoveResource1" Width="62px" />
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Button ID="ButtonAdd" runat="server" Text="Add" OnClick="ButtonAdd_Click" OnClientClick="return ValidateEmptyValue()"
                                                        meta:resourcekey="ButtonAddResource1" Width="62px" />
                                                </FooterTemplate>
                                                <FooterStyle CssClass="btnsize" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <br />

                                

                                <div class="form-inline" id="DivBtns" runat="server">
                                    <div class="row" id="divbtngroup">
                                        <asp:Button ID="btnReview" runat="server" Text="Review" Width="70px" ValidationGroup="VG1" OnClick="btnReview_Click"
                                            meta:resourcekey="btnReviewResource1" />
                                        <asp:Button ID="btnSave" runat="server" Text="Submit" Width="70px" OnClick="btnSave_Click" OnClientClick="clearDirty();"
                                            meta:resourcekey="btnSaveResource1" />
                                        <asp:Button ID="btnPreviousStep" runat="server" Text="Previous Step" OnClientClick="clearDirty();" Width="70px"
                                            OnClick="btnPreviousStep_Click" meta:resourcekey="btnPreviousStepResource1" />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" Width="70px" OnClick="btnClose_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Exit" Width="70px" OnClientClick="clearDirty();"
                                            OnClick="btnCancel_Click" />
                                        <asp:Button ID="btnExit" runat="server" Visible="false" Text="Exit" Width="70px" OnClick="btnExit_Click" />
                                    </div>

                                </div>
                            </div>
                        </asp:Panel>

                    </asp:Panel>
                </div>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="lnkHL_RWT" />
                <asp:PostBackTrigger ControlID="lnkbtnClose" />
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnPrev" />
                <asp:PostBackTrigger ControlID="btnNext" />
                <asp:PostBackTrigger ControlID="Calendar3" />
                <asp:PostBackTrigger ControlID="Calendar1" />
                <asp:PostBackTrigger ControlID="Calendar2" />
                <asp:PostBackTrigger ControlID="btnClose" />

                <asp:PostBackTrigger ControlID="btnPreviousWeek" />
                <asp:PostBackTrigger ControlID="grdRecordTime" />
                <asp:PostBackTrigger ControlID="btnNextWeek" />

                <asp:PostBackTrigger ControlID="btnReview" />
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnPreviousStep" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

   
</asp:Content>

