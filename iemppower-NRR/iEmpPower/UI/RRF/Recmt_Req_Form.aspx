<%@ Page Title="RRF" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recmt_Req_Form.aspx.cs" Inherits="iEmpPower.UI.RRF.Recmt_Req_Form"
    Culture="en-GB" UICulture="auto" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .mandt_input:after
        {
            content: " *";
            color: #f00;
            position: absolute;
            margin: 0px 0px 0px -20px;
            font-size: 17px;
            padding: 0 5px 0 0;
        }

        .form-inline
        {
            /*display: table;*/
            padding-top: 17px;
            border-style: solid;
            border-color: rgba(0,97,124,0.3);
            border-width: thin;
        }

        /*.form-group
        {
            display: table-row;
            
        }
            .form-group div
            {
                display: table-cell;
            }

            .form-group .form-group
            {
                border-style: none;
            }

        .htCr
        {
            height: auto;
        }*/


        #MainContent_rbtnReplceExt tr
        {
            display: inline !important;
        }

        .form-group
        {
            margin: 0 !important;
            padding: 0 !important;
        }

        .Cntrlwidth
        {
            max-width: 260px;
        }

        .modalBackground
        {
            background-color: black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            /*padding-top: 10px;*/
            padding-left: 10px;
            /*width: auto;
            height: auto;*/
        }

        .modal-content
        {
            /*display:none;*/
            z-index: -1;
            background-color: rgba(0,0,0,0.2);
            /*margin: 2% auto;*/ /* 15% from the top and centered */
            padding: 20px;
            border: 2px solid #888;
            width: 100%; /*Could be more or less, depending on screen size*/
            /*height:auto;*/
            height: 90vh !important;
            overflow-y: scroll;
        }

        #MainContent_FV_RRF_MyReq .htCr
        {
            color: white !important;
        }


        /*#MainContent_FV_RRF_MyReq
        {
            width: auto;
            height: 70vh !important;
            overflow-y: auto !important;
        }*/
        /*.card .header
        {
            position: initial;
        }*/
    </style>
    <script>
        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("CE_BE_txtBudgFrmMonth");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }

        function onCalendarShown() {

            var cal = $find("CE_BE_txtBudgFrmMonth");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("CE_BE_txtBudgFrmMonth");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }

        }

        //  function ShowModalPopup() {
        //alert("c");
        // $find("mpe2").show();
        // document.getElementById("<x%=Mp2.ClientID%>").show();
        //return false;
        // }
        //function HideModalPopup() {
        //alert("a");
        //  $find("mpe2").hide();
        //return false;
        // }
        //

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%-- <div class="Fr">

       
        <asp:Button ID="btnAppRej" Width="120px" runat="server" OnClick="lnkAppRej_Click" Text="Req App/Rej" OnClientClick="ShowModalPopup()" />
        <asp:Button ID="BtnSowReq" Width="120px" runat="server" OnClick="lnkBtnSowReq_Click" Text="Request Status" OnClientClick="ShowModalPopup()" />
    </div>--%>

    <div class="header">
        <div class="row clearfix">

            <div class="col-xs-12 col-sm-6">
                <span class="HeadFontSize">&nbsp;Recruitment Request Form</span><br />
            </div>
        </div>
        <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg"></asp:Label>
    </div>
    <div class="body">
        <div class="divfr" id="divbrdr">
            <div style="width: 99%;">
                <%-- <Ajx:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlRRF_MyReq" TargetControlID="lnkBtnSowReq" BehaviorID="mpe"
            BackgroundCssClass="modalBackground" CancelControlID="btnRRF_req_close">
        </Ajx:ModalPopupExtender>--%>
                <%-- <Ajx:ModalPopupExtender ID="Mp2" runat="server" PopupControlID="pnlRRF_MyReq" TargetControlID="lnkBtnSowReq" BehaviorID="mpe2"
            BackgroundCssClass="modalBackground" CancelControlID="btnRRF_req_close">
        </Ajx:ModalPopupExtender>--%>
            </div>
            <asp:LinkButton ID="lnkBtnSowReq" runat="server"></asp:LinkButton>

            <asp:UpdatePanel ID="UpdatePanel_RRF" runat="server">
                <ContentTemplate>

                    <div class="form-inline">
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">1. Indentor Name :</div>
                            <div class="col-sm-8">
                                <asp:Label ID="lblIndtrName" runat="server"></asp:Label>
                            </div>
                        </div>
                        <%--<div class="DivSpacer03"></div>--%>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">2. <span class="rcls">*</span>Requestor Name :</div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_ReqName" OnSelectedIndexChanged="ddl_ReqName_SelectedIndexChanged" AutoPostBack="true" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_ddl_ReqName" ValidationGroup="RRF_Cntrls" runat="server" ForeColor="Red" ControlToValidate="ddl_ReqName" ErrorMessage="Please Select Requestor Name !" Display="Dynamic" CssClass="lblValidation" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <Ajx:ListSearchExtender ID="LSE_ddl_ReqName" TargetControlID="ddl_ReqName" PromptCssClass="PromptlCSSClass" PromptText="Search Requestor Name" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" runat="server">
                                </Ajx:ListSearchExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">3. <span class="rcls">*</span>Requestor Designation :</div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_req_desig" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_ddl_req_desig" runat="server" ForeColor="Red" ValidationGroup="RRF_Cntrls" ControlToValidate="ddl_req_desig" CssClass="lblValidation" ErrorMessage="Please Select Requestor Designation !" Display="Dynamic" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <Ajx:ListSearchExtender ID="LSE_ddl_req_desig" TargetControlID="ddl_req_desig" PromptCssClass="PromptlCSSClass" PromptText="Search Requestor Designation" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" runat="server">
                                </Ajx:ListSearchExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">4. <span class="rcls">*</span>Department/Profit Centre :</div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_dept" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_ddl_dept" runat="server" ForeColor="Red" ValidationGroup="RRF_Cntrls" ControlToValidate="ddl_dept" CssClass="lblValidation" ErrorMessage="Please Select Requestor Department !" Display="Dynamic" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <Ajx:ListSearchExtender ID="LSE_ddl_dept" TargetControlID="ddl_dept" PromptCssClass="PromptlCSSClass" PromptText="Search Dept./Profit Centre" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" runat="server">
                                </Ajx:ListSearchExtender>
                            </div>
                        </div>
                    </div>
                    <div class="DivSpacer03"></div>

                    <div class="form-inline">
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">5. <span class="rcls">*</span>Designation to be recruited :</div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_rectPosDesig" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls" AutoPostBack="true" OnSelectedIndexChanged="ddl_rectPosDesig_SelectedIndexChanged"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_ddl_rectPosDesig" ForeColor="Red" runat="server" ValidationGroup="RRF_Cntrls" ControlToValidate="ddl_rectPosDesig" CssClass="lblValidation" ErrorMessage="Please Select Designation to be recruited !" Display="Dynamic" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <Ajx:ListSearchExtender ID="LSE_ddl_rectPosDesig" TargetControlID="ddl_rectPosDesig" PromptCssClass="PromptlCSSClass" PromptText="Search Designation" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" runat="server">
                                </Ajx:ListSearchExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth" style="max-width: 260px;">6. Replacement of existing candidate? :</div>
                            <div class="col-sm-8">
                                <div class="form-group">
                                    <div class="col-sm-4">
                                        <asp:RadioButtonList ID="rbtnReplceExt" runat="server" OnSelectedIndexChanged="rbtnReplceExt_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="True"> Yes </asp:ListItem>
                                            <asp:ListItem Value="False" Selected="True"> No </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:Panel ID="PanelExtEmp" runat="server" Visible="false">
                                            <div class="">
                                                <%--<div class="col-sm-4 htCr">7.1 From month :</div>--%>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="DDL_ExtEmpList" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                                    <%--<span class="mandt_input"></span>--%>
                                                    <asp:RequiredFieldValidator ID="RRF_DDL_ExtEmpList" InitialValue="0" ForeColor="Red" ValidationGroup="RRF_Cntrls" ControlToValidate="DDL_ExtEmpList" runat="server" CssClass="lblValidation" ErrorMessage="Please Select Existing Employee !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="">
                            <div class="form-group">
                                <div class="col-sm-4 htCr Cntrlwidth" style="max-width: 260px;">7. Required Position is Budgeted? :</div>
                                <div class="col-sm-8">
                                    <div class="form-group">
                                        <div class="col-sm-4">
                                            <asp:RadioButtonList ID="rbtnBudgted" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbtnBudgted_SelectedIndexChanged">
                                                <asp:ListItem Value="True"> Yes </asp:ListItem>
                                                <asp:ListItem Value="False" Selected="True"> No </asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <asp:Panel ID="pnlBudgt" runat="server" Visible="false">
                                            <div class="col-sm-8">
                                                <div class="">
                                                    <%--<div class="col-sm-4 htCr">7.1 From month :</div>--%>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtBudgFrmMonth" PlaceHolder="7.1 From Month" ValidationGroup="RRF_Cntrls" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                        <span class="mandt_input"></span>
                                                        <asp:RequiredFieldValidator ID="RFVtxtBudgFrmMonth" ForeColor="Red" ValidationGroup="RRF_Cntrls" ControlToValidate="txtBudgFrmMonth" runat="server" CssClass="lblValidation" ErrorMessage="Please Enter From Month !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        <Ajx:CalendarExtender ID="CE_txtBudgFrmMonth" BehaviorID="CE_BE_txtBudgFrmMonth" runat="server" Enabled="True" Format="MM/yyyy"
                                                            TargetControlID="txtBudgFrmMonth" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" PopupButtonID="txtBudgFrmMonth" DefaultView="Months">
                                                        </Ajx:CalendarExtender>
                                                        <%-- <Ajx:MaskedEditExtender ID="MEE_txtBudgFrmMonth" runat="server"
                                                CultureName="en-GB" ErrorTooltipEnabled="true" Mask="99-9999"
                                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError" TargetControlID="txtBudgFrmMonth" AcceptAMPM="false" ClearTextOnInvalid="true" UserDateFormat="DayMonthYear"
                                                 UserTimeFormat="TwentyFourHour" />
                                                        --%>

                                                        <%-- <Ajx:MaskedEditExtender ID="ME_txtBudgFrmMonth" runat="server"
                                                CultureName="en-GB" ErrorTooltipEnabled="true" Mask="99/9999"
                                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                OnInvalidCssClass="MaskedEditError" TargetControlID="txtBudgFrmMonth" AcceptAMPM="false" ClearMaskOnLostFocus="false"/>--%>
                                                    </div>
                                                    <%--  </div>
                                </div>
                                <div class="col-sm-11">
                                    <div class="">--%>
                                                    <%--<div class="col-sm-4 htCr">7.2 Budgeted Cost :</div>--%>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="txtBudgCost" PlaceHolder="7.2 Budgeted Cost" ValidationGroup="RRF_Cntrls" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                        <span class="mandt_input"></span>
                                                        <asp:RequiredFieldValidator ID="RFV_txtBudgCost" ForeColor="Red" ValidationGroup="RRF_Cntrls" ControlToValidate="txtBudgCost" runat="server" CssClass="lblValidation" ErrorMessage="Please Enter Budgeted Cost !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        <Ajx:FilteredTextBoxExtender ID="FTB_txtBudgCost" TargetControlID="txtBudgCost" FilterType="Numbers" runat="server"></Ajx:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-4 htCr" style="max-width: 260px;">8. Purpose of Hiring:</div>
                            <div class="col-sm-8">
                                <div class="form-group">
                                    <div class="col-sm-4">
                                        <asp:RadioButtonList ID="rtnpurpsOfHrng" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rtnpurpsOfHrng_SelectedIndexChanged">
                                            <asp:ListItem Value="IR" Selected="True"> Internal Resource </asp:ListItem>
                                            <asp:ListItem Value="SF"> Staffing </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:Panel ID="pnlPURPSIR" runat="server" Visible="false">

                                            <%--<div class="  ">
                                    <%--<div class="col-sm-4 htCr">8.1 Location :</div>--%>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddl_PurpsIRLocctn" CssClass="txtDropDownwidth" ValidationGroup="RRF_Cntrls" runat="server"></asp:DropDownList>
                                                <span class="mandt_input"></span>
                                                <asp:RequiredFieldValidator ID="RFV_ddl_PurpsIRLocctn" ValidationGroup="RRF_Cntrls" ControlToValidate="ddl_PurpsIRLocctn" runat="server" ForeColor="Red" CssClass="lblValidation" ErrorMessage="Please Select Location !" Display="Dynamic" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <Ajx:ListSearchExtender ID="LSE_ddl_PurpsIRLocctn" PromptCssClass="PromptlCSSClass" TargetControlID="ddl_PurpsIRLocctn" PromptText="Search Location" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" runat="server">
                                                </Ajx:ListSearchExtender>
                                            </div>
                                            <%--</div>--%>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPurpsSF" runat="server" Visible="false">
                                            <%-- <div class="col-sm-9">
                                <div class=" ">--%>
                                            <%--<div class="col-sm-4 htCr">8.2 Project:</div>--%>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddl_PurpsSFProj" CssClass="txtDropDownwidth" ValidationGroup="RRF_Cntrls" runat="server"></asp:DropDownList>
                                                <span class="mandt_input"></span>
                                                <asp:RequiredFieldValidator ID="RFV_ddl_PurpsSFProj" ForeColor="Red" ValidationGroup="RRF_Cntrls" runat="server" ControlToValidate="ddl_PurpsSFProj" CssClass="lblValidation" ErrorMessage="Please Select Project !" Display="Dynamic" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                <Ajx:ListSearchExtender ID="LSE_ddl_PurpsSFProj" PromptCssClass="PromptlCSSClass" TargetControlID="ddl_PurpsSFProj" PromptText="Search Project" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" runat="server">
                                                </Ajx:ListSearchExtender>
                                            </div>
                                            <%--  </div>
                            </div>--%>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">9. <span class="rcls">*</span>Position Reporting to :</div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_ReprtsTo" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_ddl_ReprtsTo" ForeColor="Red" ValidationGroup="RRF_Cntrls" ControlToValidate="ddl_ReprtsTo" runat="server" CssClass="lblValidation" ErrorMessage="Please Select Position Reporting for !" Display="Dynamic" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <Ajx:ListSearchExtender ID="LSE_ddl_ReprtsTo" TargetControlID="ddl_ReprtsTo" PromptCssClass="PromptlCSSClass" PromptText="Search Reporting to" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" runat="server">
                                </Ajx:ListSearchExtender>
                            </div>
                        </div>
                    </div>

                    <div class="DivSpacer03"></div>
                    <div class="form-inline">
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">10. <span class="rcls">*</span>Min. Educational Qualification :</div>
                            <div class="col-sm-8">
                                <asp:DropDownList ID="ddl_MinQuaEdu" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFV_ddl_MinQuaEdu" ControlToValidate="ddl_MinQuaEdu" ValidationGroup="RRF_Cntrls" ForeColor="Red" runat="server" CssClass="lblValidation" ErrorMessage="Please Enter Min. Educational Qualification !" Display="Dynamic" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <Ajx:ListSearchExtender ID="LSE_ddl_MinQuaEdu" TargetControlID="ddl_MinQuaEdu" PromptCssClass="PromptlCSSClass" PromptText="Search Edu. Qualin." PromptPosition="Top" QueryPattern="Contains" IsSorted="true" runat="server">
                                </Ajx:ListSearchExtender>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">11. <span class="rcls"></span>Min. Certifications :</div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtMinCertifin" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RFV_txtMinCertifin" ControlToValidate="txtMinCertifin" ValidationGroup="RRF_Cntrls" ForeColor="Red" runat="server" CssClass="lblValidation" ErrorMessage="Please Enter Min. Certifications !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">12. <span class="rcls">*</span>Min. Total Experience :</div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtMinTlExp" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls" MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV_txtMinTlExp" ControlToValidate="txtMinTlExp" ValidationGroup="RRF_Cntrls" ForeColor="Red" runat="server" CssClass="lblValidation" ErrorMessage="Please Enter Min. Total Experience !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <%--<Ajx:FilteredTextBoxExtender ID="FTBtxtMinTlExp" TargetControlID="txtMinTlExp" FilterType="Custom,Numbers" ValidChars="." runat="server"></Ajx:FilteredTextBoxExtender>--%>
                                <Ajx:MaskedEditExtender ID="MEE_txtMinTlExp" runat="server" AcceptNegative="Left" CultureName="en-GB"
                                    ErrorTooltipEnabled="true" Mask="99.99" MaskType="Number"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    TargetControlID="txtMinTlExp" />
                                <asp:RegularExpressionValidator ID="REV_txtMinTlExp" runat="server" ErrorMessage="Invalid Entry!" CssClass="lblValidation" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationExpression="^[0-9]{2}.[0-9]{1}[0-1]{1}$" ControlToValidate="txtMinTlExp"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">13. <span class="rcls">*</span>Min. Domain Experience :</div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtMinDomExp" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls" MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV_txtMinDomExp" ControlToValidate="txtMinDomExp" ValidationGroup="RRF_Cntrls" ForeColor="Red" runat="server" CssClass="lblValidation" ErrorMessage="Please Enter Min. Domain Experience !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <Ajx:MaskedEditExtender ID="MEE_txtMinDomExp" runat="server" AcceptNegative="Left" CultureName="en-GB"
                                    ErrorTooltipEnabled="true" Mask="99.99" MaskType="Number"
                                    MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                                    TargetControlID="txtMinDomExp" />

                                <asp:RegularExpressionValidator ID="REV_txtMinDomExp" runat="server" ErrorMessage="Invalid Entry!" CssClass="lblValidation" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationExpression="^[0-9]{2}.[0-9]{1}[0-1]{1}$" ControlToValidate="txtMinDomExp"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">14. <span class="rcls">*</span>Areas of Expertise Required :</div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtAraExpRequrd" CssClass="txtDropDownwidth" TextMode="MultiLine" runat="server" ValidationGroup="RRF_Cntrls"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV_txtAraExpRequrd" ControlToValidate="txtAraExpRequrd" ForeColor="Red" ValidationGroup="RRF_Cntrls" runat="server" CssClass="lblValidation" ErrorMessage="Please Enter Areas of Expertise Required !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">15. <span class="rcls"></span>Other Specific Requirement :</div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtOthSpecfiReqmt" CssClass="txtDropDownwidth" TextMode="MultiLine" runat="server" ValidationGroup="RRF_Cntrls"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RFV_txtOthSpecfiReqmt" ControlToValidate="txtOthSpecfiReqmt" ForeColor="Red" ValidationGroup="RRF_Cntrls" runat="server" CssClass="lblValidation" ErrorMessage="Please Enter Other Specific Requirement !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">16. <span class="rcls">*</span>Job Description :</div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtJobDesp" CssClass="txtDropDownwidth" TextMode="MultiLine" runat="server" ValidationGroup="RRF_Cntrls"></asp:TextBox>
                                <asp:FileUpload ID="FU_JobDesp" runat="server" />
                                <asp:LinkButton ID="lnkViewfile" runat="server" Visible="false" OnClick="lnkViewfile_Click">View Old file</asp:LinkButton>
                                <asp:HiddenField ID="HF_viewfile" runat="server" />
                            </div>
                            <div class="col-sm-4">
                                <asp:RequiredFieldValidator ID="REF_txtJobDesp" ControlToValidate="txtJobDesp" ValidationGroup="RRF_Cntrls" runat="server" ForeColor="Red" CssClass="lblValidation" ErrorMessage="Please Enter Job Description !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">17. <span class="rcls">*</span>Tentative Date on Board :</div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtTentDate" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV_txtTentDate" ControlToValidate="txtTentDate" ValidationGroup="RRF_Cntrls" runat="server" ForeColor="Red" CssClass="lblValidation" ErrorMessage="Please Enter Tentative Date on Board !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                <Ajx:CalendarExtender ID="CE_TxtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtTentDate" PopupButtonID="txtTentDate">
                                </Ajx:CalendarExtender>
                                <Ajx:MaskedEditExtender ID="MEE_txtTentDate" runat="server"
                                    CultureName="en-GB" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtTentDate" AcceptAMPM="false" ClearTextOnInvalid="true" UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour" />
                            </div>
                        </div>


                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">18. <span class="rcls">*</span>No. of Resourses :</div>
                            <div class="col-sm-8">
                                <asp:TextBox ID="txtNoofResource" TextMode="Number" Text="1" CssClass="txtDropDownwidth" runat="server" ValidationGroup="RRF_Cntrls"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV_txtNoofResource" ControlToValidate="txtNoofResource" ValidationGroup="RRF_Cntrls" runat="server" ForeColor="Red" CssClass="lblValidation" ErrorMessage="Please Enter no. of Resourses !" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <Ajx:FilteredTextBoxExtender ID="FTB_txtNoofResource" TargetControlID="txtNoofResource" FilterType="Numbers" runat="server"></Ajx:FilteredTextBoxExtender>
                                <asp:RangeValidator ID="RV_txtNoofResource" ForeColor="Red" Text="Must be between 1 to 999" ControlToValidate="txtNoofResource" MinimumValue="1" MaximumValue="999" ValidationGroup="RRF_Cntrls" Display="Dynamic" CssClass="lblValidation" SetFocusOnError="true" runat="server" ErrorMessage="RangeValidator"></asp:RangeValidator>

                            </div>
                        </div>
                    </div>

                    <div class="btn-group-lg">
                        <br />
                        <br />
                        <div class="col-sm-6">
                            <asp:HiddenField ID="HF_RID" runat="server" />
                            <asp:Button ID="btnSubmit" Width="80px" runat="server" Text="Submit" ValidationGroup="RRF_Cntrls" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnUpdate" Width="80px" runat="server" Text="Update" ValidationGroup="RRF_Cntrls" Visible="false" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnClear" Width="80px" runat="server" Text="Clear" OnClick="btnClear_Click" />
                            <asp:Button ID="btnCancl" Width="80px" runat="server" Text="Cancel" OnClick="btnCancl_Click" Visible="false" />
                        </div>
                        <br />
                        <br />
                    </div>
                    <%-- </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
                <asp:PostBackTrigger ControlID="btnClear" />
            </Triggers>
        </asp:UpdatePanel>--%>
            </div>


            <%--<asp:Panel ID="pnlRRF_MyReq" runat="server" CssClass="modal-content respovrflw">

               
                <div style="position: absolute; float: right; right: 15px; top: 5px">

                    
                    <asp:Button ID="BtnEdit" runat="server" Text="&laquo; Edit" CausesValidation="false" OnClick="BtnEdit_Click" OnClientClick="HideModalPopup()"
                        CssClass="Fnt02" />
                    <asp:Button ID="btnRRF_req_close" Width="30px" CausesValidation="false" runat="server" Text=" &times; " />
                </div>
                <div style="float: left; color: #FF4500; padding-left: 10px">
                    <b><asp:Label ID="lblErrorMsg" runat="server" Text=""></asp:Label></b>
                </div>
                <div id="divreqType" runat="server" class="form-inline">
                    <div class="form-group">
                        <div class="col-sm-8">
                            <div class="col-sm-2 htCr " style="color: white">
                                Request Type :
                            </div>
                            <div class="col-sm-6" style="color: white">
                                <asp:RadioButtonList ID="rbtnReqTypeStatus" OnSelectedIndexChanged="rbtnReqTypeStatus_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                    <asp:ListItem Value="1" Selected="True">My Request</asp:ListItem>
                                    <asp:ListItem Value="2">All Request</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="form-inline">

                    <div class="" style="color: whitesmoke; overflow: auto; display:none;">
                       
                        <asp:FormView ID="FV_RRF_MyReq" PagerStyle-CssClass="pager" runat="server" AllowPaging="true" ForeColor="WhiteSmoke" OnPageIndexChanging="FV_RRF_MyReq_PageIndexChanging" OnItemCommand="FV_RRF_MyReq_ItemCommand"
                            DataKeyNames="ID, INDNTR_NAME,REQTR_NAME,DES_RECUTD,REP_EXT_EMP,REP_EXT_EMP_ID,REQ_POS_BUDGT,REQ_POS_BUDGT_FRM_MONTH,
                REQ_POS_BUDGT_COST,PURPS_HIRNG,PURPS_HIRNG_LOC,PURPS_HIRNG_PROJ,POS_REPT_TO_ID,
      MIN_EDU_QLAFTN,MIN_CERTIFNTN,TOT_EXP,TOT_DOMAIN_EXP,AREA_EXPRTSE,OTHER_SPC_REQ,JOB_DISP,DISP_FILE,
                TENTTIVE_DATE,NORESOURCE,CREATED_ON,STATUS">
                            <ItemTemplate>
                                <div class="form-inline">
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">1. Request ID :</div>
                                        <div class="col-sm-8">
                                            <asp:Label ID="lblRRfID" Text='<%# Eval("ID")%>' runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">2. Indentor Name :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("IND_ENAME")%>
                                        </div>
                                    </div>

                                 
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">3. <span class="rcls"></span>Requestor Name :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("REQT_ENAME")%>
                                        </div>
                                    </div>
                                  
                                </div>
                                <div class="DivSpacer03"></div>

                                <div class="form-inline">
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">4. <span class="rcls"></span>Designation to be recruited :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("DESRTEXT")%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth" style="max-width: 260px;">5. Replacement of existing candidate? :</div>
                                        <div class="col-sm-8">
                                            <div class="form-group">
                                                <div class="col-sm-4">
                                                    <%# Eval("REP_EXT_EMP").ToString()=="True"?"Yes":"No"%>
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:Panel ID="Panel1" runat="server" Visible='<%# Eval("REP_EXT_EMP").ToString()=="True"?true:false%>'>
                                                        <div class="">
                                                            <div class="col-sm-3 htCr">5.1 Replace to :</div>
                                                            <div class="col-sm-6">
                                                                <%# Eval("REP_EXT_EMP_ENAME")%>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="">
                                        <div class="form-group">
                                            <div class="col-sm-4 htCr Cntrlwidth" style="max-width: 260px;">6. Required Position is Budgeted? :</div>
                                            <div class="col-sm-8">
                                                <div class="form-group">
                                                    <div class="col-sm-4">
                                                        <%# Eval("REQ_POS_BUDGT").ToString()=="True"?"Yes":"No"%>
                                                    </div>

                                                    <asp:Panel ID="Panel2" runat="server" Visible='<%# Eval("REQ_POS_BUDGT").ToString()=="True"?true:false%>'>
                                                        <div class="col-sm-8">
                                                            <div class="">
                                                                <div class="col-sm-3 htCr">6.1 From month :</div>
                                                                <div class="col-sm-3">
                                                                    <%# Eval("REQ_POS_BUDGT_FRM_MONTH")%>
                                                                </div>
                                                                <div class="col-sm-3 htCr">6.2 Budgt Cost :</div>
                                                                <div class="col-sm-3">
                                                                    <%# Eval("REQ_POS_BUDGT_COST")%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-4 htCr" style="max-width: 260px;">7. Purpose of Hiring:</div>
                                        <div class="col-sm-8">
                                            <div class="form-group">
                                                <div class="col-sm-4">
                                                    <%# Eval("PURPS_HIRNG").ToString()=="SF"?"Staffing":"Internal Resource"%>
                                                </div>
                                                <div class="col-sm-8">
                                                    <asp:Panel ID="Panel3" runat="server" Visible="true">
                                                        <div class="col-sm-3 htCr">7.1 Location :</div>
                                                        <div class="col-sm-3">
                                                            <%# Eval("PURPS_HIRNG_LOC_TEXT")%>
                                                        </div>

                                                    </asp:Panel>
                                                    <asp:Panel ID="Panel4" runat="server" Visible='<%# Eval("PURPS_HIRNG").ToString()=="SF"?true:false%>'>
                                                        <div class="col-sm-3 htCr">7.2 Project :</div>
                                                        <div class="col-sm-3">
                                                            <%# Eval("PURPS_HIRNG_PROJ_TEXT")%>
                                                        </div>

                                                    </asp:Panel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">8. <span class="rcls"></span>Position Reporting to :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("POS_REPT_TO_ID_ENAME")%>
                                        </div>
                                    </div>
                                </div>

                                <div class="DivSpacer03"></div>
                                <div class="form-inline">
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">9. <span class="rcls"></span>Min. Educational Qualification :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("MIN_EDU_QLAFTN")%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">10. <span class="rcls"></span>Min. Certifications :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("MIN_CERTIFNTN")%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">11. <span class="rcls"></span>Min. Total Experience :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("TOT_EXP")%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">12. <span class="rcls"></span>Min. Domain Experience :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("TOT_DOMAIN_EXP")%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">13. <span class="rcls"></span>Areas of Expertise Required :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("AREA_EXPRTSE")%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">14. <span class="rcls"></span>Other Specific Requirement :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("OTHER_SPC_REQ")%>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">15. <span class="rcls"></span>Job Description :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("JOB_DISP")%>
                                            &nbsp;(<asp:LinkButton ID="lnkDwnldjobDisp" CommandName="DOWNLOAD" runat="server" CommandArgument='<%#Eval("DISP_FILE") %>' Text="Download" Visible='<%#Eval("DISP_FILE").ToString()==""?false:true %>'></asp:LinkButton>)
                                        </div>

                                    </div>

                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">16. <span class="rcls"></span>Tentative Date on Board :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("TENTTIVE_DATE")%>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">17. <span class="rcls"></span>No. of Resourses :</div>
                                        <div class="col-sm-8">
                                            <%# Eval("NORESOURCE")%>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth">Status :</div>
                                        <div class="col-sm-8">
                                            <b>
                                                <asp:Label ID="lblStatus" Text='<%# Eval("STATUS")%>' runat="server"></asp:Label></b>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12" style="padding: 10px 0">
                                    <div class="col-sm-8" style="">
                                        <table class="gridview" style="border: inset 2px WhiteSmoke;">
                                            <tr style="text-underline-position: below">
                                                <th>Approver ID</th>
                                                <th>Approver Name</th>
                                                <th>Approved Date</th>
                                                <th>Approver Comment</th>
                                            </tr>
                                            <asp:Panel ID="pnlapp1" runat="server" Visible='<%#Eval("APPROVED1_ID").ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%#Eval("APPROVED1_ID") %></td>
                                                    <td><%#Eval("APPROVED1_ID_ENAME")%></td>
                                                    <td><%#Eval("APPROVED1_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900"?" - ":Eval("APPROVED1_ON")%></td>
                                                    <td><%#Eval("APPROVED1_REMARKS")%></td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlapp2" runat="server" Visible='<%#Eval("APPROVED2_ID").ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%#Eval("APPROVED2_ID") %></td>
                                                    <td><%#Eval("APPROVED2_ID_ENAME")%></td>
                                                    <td><%#Eval("APPROVED2_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900"?" - ":Eval("APPROVED2_ON")%></td>
                                                    <td><%#Eval("APPROVED2_REMARKS")%></td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlapp3" runat="server" Visible='<%#Eval("APPROVED3_ID").ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%#Eval("APPROVED3_ID") %></td>
                                                    <td><%#Eval("APPROVED3_ID_ENAME")%></td>
                                                    <td><%#Eval("APPROVED3_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900"?" - ":Eval("APPROVED3_ON")%></td>
                                                    <td><%#Eval("APPROVED3_REMARKS")%></td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlapp4" runat="server" Visible='<%#Eval("APPROVED4_ID").ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%#Eval("APPROVED4_ID") %></td>
                                                    <td><%#Eval("APPROVED4_ID_ENAME")%></td>
                                                    <td><%#Eval("APPROVED4_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900"?" - ":Eval("APPROVED4_ON")%></td>
                                                    <td><%#Eval("APPROVED4_REMARKS")%></td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlapp5" runat="server" Visible='<%#Eval("APPROVED5_ID").ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%#Eval("APPROVED5_ID") %></td>
                                                    <td><%#Eval("APPROVED5_ID_ENAME")%></td>
                                                    <td><%#Eval("APPROVED5_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900"?" - ":Eval("APPROVED5_ON")%></td>
                                                    <td><%#Eval("APPROVED5_REMARKS")%></td>
                                                </tr>
                                            </asp:Panel>
                                        </table>
                                    </div>
                                    <asp:Panel ID="divAppRej" runat="server" class="col-sm-3">
                                        <div class="Apprvrclass">
                                            <b>Request Approve / Reject : </b>
                                            <asp:TextBox ID="txtApprRemarks" TextMode="MultiLine" PlaceHolder="Enter Remarks" runat="server" CssClass="txtDropDownwidth"></asp:TextBox>
                                            <asp:Button ID="btnApprove" Width="80px" runat="server" OnClick="btnApprove_Click" Text="Approve" />
                                            <asp:Button ID="btnReject" Width="80px" runat="server" OnClick="btnReject_Click" Text="Reject" />
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div>
                                   
                                </div>
                            </ItemTemplate>
                        </asp:FormView>
                       
                    </div>
                </div>

            </asp:Panel>--%>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpdate" />
                    <asp:PostBackTrigger ControlID="btnCancl" />
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                    <asp:PostBackTrigger ControlID="btnClear" />
                    <asp:PostBackTrigger ControlID="lnkViewfile" />
                    <%--<asp:PostBackTrigger ControlID="FU_JobDesp" />--%>
                    <%--<asp:PostBackTrigger ControlID="btnAppRej" />
            <asp:PostBackTrigger ControlID="BtnSowReq" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
