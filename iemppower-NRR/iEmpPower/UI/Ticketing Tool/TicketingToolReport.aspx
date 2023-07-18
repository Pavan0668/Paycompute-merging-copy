<%@ Page Title="Ticketing Tool Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TicketingToolReport.aspx.cs"
    Inherits="iEmpPower.UI.Ticketing_Tool.TicketingToolReport" EnableEventValidation="false" Culture="en-GB" UICulture="auto" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        #MainContent_PnlRating
        /*UPCustRating*/
        {
            border-color: black;
            border-style: solid;
            background-color: white !important;
            width: 50% !important;
            /*right:25px !important;*/
        }

        .PnlPaddingRating
        {
            padding-left: 25%;
        }


        .modal-content
        {
            /*display:none;*/
            z-index: -1;
            background-color: rgba(0,94,124,0.2);
            /*margin: 15% auto;*/ /* 15% from the top and centered */
            padding: 20px;
            border: 2px solid #888;
            width: 50%; /*Could be more or less, depending on screen size*/
        }

        .hidden
        {
            visibility: hidden;
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
            width: 50% !important;
            height: 500px;
        }

        .Td07
        {
            color: #004080;
            font-size: 13px;
            width: 10px;
            padding: 8px;
            text-align: center;
            line-height: 20px !important;
        }

        .Td07Task
        {
            color: #fff;
            font-size: 13px;
            width: 10px;
            padding: 8px;
            text-align: center;
            line-height: 20px !important;
        }

        .Td10
        {
            width: 300px !important;
            padding: 3px !important;
            text-align: left !important;
            line-height: 20px !important;
        }

        .Td06
        {
            color: #004080 !important;
            font-size: 14px !important;
            width: 100px !important;
            padding: 3px !important;
            text-align: left !important;
            line-height: 20px !important;
        }

        .Td06Task
        {
            color: #fff !important;
            font-size: 14px !important;
            width: 180px !important;
            padding: 3px !important;
            text-align: left !important;
            line-height: 20px !important;
        }

        .resize
        {
            resize: none !important;
        }
        /*AutoComplete flyout */
        .completionList
        {
            border: solid 1px #444444;
            margin: 0px;
            padding: 2px;
            height: 100px;
            overflow: auto;
            background-color: #FFFFFF;
            font-size: xx-small;
        }

        .listItem
        {
            color: #1C1C1C;
        }

        .itemHighlighted
        {
            background-color: #ffc0c0;
        }

        .accordionContent
        {
            background-color: #D3DEEF;
            border-color: -moz-use-text-color #2F4F4F #2F4F4F;
            border-right: 1px dashed #2F4F4F;
            border-style: none dashed dashed;
            border-width: medium 1px 1px;
            padding: 10px 5px 5px;
            width: 70%;
        }

        .accordionHeaderSelected
        {
            background-color: #00617c;
            border: 1px solid #2F4F4F;
            color: white !important;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 70%;
        }

        .accordionHeader
        {
            background-color: #00617c;
            border: 1px solid #2F4F4F;
            color: white !important;
            cursor: pointer;
            font-family: Arial,Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            margin-top: 5px;
            padding: 5px;
            width: 70%;
        }

        .href
        {
            color: White !important;
            font-weight: bold;
            text-decoration: none;
        }

        .PromptCSS
        {
            color: Orchid;
            font-size: large;
            font-style: italic;
            font-weight: bold;
            background-color: Snow;
            border: solid 1px Orchid;
            height: 30px;
        }

        .RedCls
        {
            width: 23px;
            height: 16px;
            background-color: #f75f0b;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .darkRed
        {
            width: 23px;
            height: 16px;
            background-color: #ea0d14;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .GreenCls
        {
            width: 23px;
            height: 16px;
            background-color: #008000;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .AmberCls
        {
            width: 23px;
            height: 16px;
            background-color: #FFBF00;
            border: 1px solid #ccc;
            padding: 0;
            margin: 0 4px;
        }

        .Brd01
        {
            border: 1px solid #666;
        }

        .Tdlegend1
        {
            color: #004080;
            font-size: 13px;
            line-height: 12px !important;
            padding: 3px;
        }

        .Tdlegend2
        {
            color: #004080;
            font-size: 13px;
            text-align: center;
            line-height: 12px !important;
            width: 10px;
            padding: 8px;
        }

        .Ul01, .Ul02
        {
            list-style: none;
            margin: 0;
            padding: 0;
        }

            .Ul01 li
            {
                padding: 5px 0 5px 10px;
                margin: 3px;
            }

            .Ul02 li
            {
                padding: 3px 10px 3px 10px;
                margin: 3px;
            }

        .TxtC
        {
            text-align: center !important;
        }

        .FntPIPO
        {
            border-bottom: 1px solid #ccc !important;
        }

        .lgdpos
        {
            float: right;
            right: 145px;
            position: absolute;
            top: 138px;
        }

        /*#MainContent_pnlgrid
        {
            overflow-x: scroll !important;
        }*/

        .rowheight
        {
            height: 90px !important;
        }

        .paddingstyl
        {
            padding-left: 90px !important;
        }
    </style>


    <script type="text/javascript">

        function DisplayName() {
            $find("mpe").show();
            return false;
        }

        function HideModalPopup() {
            $find("mpe").hide();
            return false;
        }

        function HideModalPopupc() {
            $find("mpe").hide();
            alert("Ticket Updated Successfully !!..")
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">
                <span class="HeadFontSize">Reports</span>

            </div>
        </div>
    </div>


    <div class="body">
        <div class="divfr" id="divbrdr">

            <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
            <%--<div class=" col-sm-3 lgdpos">
                <div id="divcont" runat="server" style="width: 350px">
                    <ul class="Ul02">

                        <li class="Fl Brd01">
                            <ul class="Ul01">
                                <li class="TxtC FntPIPO">
                                    <div id="menu">
                                        <b>
                                            <asp:LinkButton ID="lnkbtn" runat="server" CssClass="linkbtn">SLA Time Progress Legend</asp:LinkButton></b>
                                    </div>
                                 
                                    <Ajx:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="lnkbtn"
                                        PopupControlID="pnlSLADetls" PopupPosition="Left" HoverCssClass="modalBackground">
                                    </Ajx:HoverMenuExtender>
                                    
                                </li>
                                <li>
                                    <table>
                                        <tr>
                                            <td class="Tdlegend1">
                                                <div class="GreenCls"></div>
                                            </td>
                                            <td class="Tdlegend2">- </td>
                                            <td><b>SLA Time 0-40% Completed</b></td>
                                        </tr>
                                        <tr>
                                            <td class="Tdlegend1">
                                                <div class="AmberCls"></div>
                                            </td>
                                            <td class="Tdlegend2">- </td>
                                            <td><b>SLA Time 40-80% Completed</b></td>
                                        </tr>
                                        <tr>
                                            <td class="Tdlegend1">
                                                <div class="RedCls"></div>
                                            </td>
                                            <td class="Tdlegend2">- </td>
                                            <td><b>SLA Time 80% and Above Completed</b></td>
                                        </tr>

                                    </table>

                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
          

          
            <fieldset style="width: 60%; border: 1px solid grey; padding-bottom: 10px; padding-left: 5px; padding-right: 5px;">
                <legend style="width: 50px; border: 0px; font-size: 15px;">Search</legend>
                <table>
                    <tr>
                        <td class="Td06">Customer</td>
                        <td class="Td07"><b>:</b> </td>
                        <td>
                            <asp:DropDownList ID="DDLCustomerList" runat="server" TabIndex="1" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLCustomerList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </td>
                       
                        <td>&nbsp;&nbsp;&nbsp;</td>
                        <td>&nbsp;&nbsp;&nbsp;</td>
                        <td class="Td06">Status</td>
                        <td class="Td07"><b>:</b> </td>
                        <td>
                            <asp:DropDownList runat="server" ID="DDLStatusSearch" TabIndex="2" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLSearch_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList></td>


                    </tr>
                    <tr>
                        <td class="Td06">Begin date</td>
                        <td class="Td07"><b>:</b> </td>
                        <td>
                            <asp:TextBox ID="TxtFromDate" runat="server" TabIndex="3" CssClass="txtDropDownwidth" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'"
                                OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <Ajx:MaskedEditExtender ID="MEE_TxtFromDate" runat="server" AcceptNegative="Left"
                                CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" TargetControlID="TxtFromDate" />
                            <Ajx:CalendarExtender ID="CE_TxtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="TxtFromDate">
                            </Ajx:CalendarExtender>
                            <asp:RegularExpressionValidator ID="REV_TxtFromDate" runat="server" Display="Dynamic"
                                CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtFromDate"
                                SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;</td>
                        <td>&nbsp;&nbsp;&nbsp;</td>
                       
                        <td class="Td06">End date</td>
                        <td class="Td07"><b>:</b> </td>
                        <td>
                            <asp:TextBox ID="TxtToDate" runat="server" TabIndex="4" CssClass="txtDropDownwidth" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'"
                                OnTextChanged="TxtToDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <Ajx:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" TargetControlID="TxtToDate" />
                            <Ajx:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="TxtToDate">
                            </Ajx:CalendarExtender>

                            <asp:CompareValidator ID="CV_TxtToDate" runat="server" ControlToCompare="TxtFromDate"
                                CssClass="lblValidation" ControlToValidate="TxtToDate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                                Operator="GreaterThanEqual" Type="Date" ForeColor="Red"></asp:CompareValidator>
                            <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic"
                                CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtToDate"
                                SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnclear" OnClick="btnclear_Click" Text="Clear" TabIndex="5" /></td>
                    </tr>
                </table>
            </fieldset>--%>


            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-sm-12">



                        <%--<asp:Button ID="BtnNewTicket" runat="server" Text="New Ticket" OnClick="BtnNewTicket_Click" />--%>
                        <%--   <fieldset style="width: 60%; border: 1px solid grey; padding-bottom: 10px; padding-left: 5px; padding-right: 5px;">
                <legend style="width: 50px; border: 0px; font-size: 15px;">Search</legend>--%>
                        <div class="col-sm-8 Brd01">
                            <div class="form-inline">
                                <div class="col-sm-12 ">
                                    <h4 class="FntPIPO" style="padding: 2px;">Search</h4>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2 htCr" style="width: 100px;">Customer <b>:</b></div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-4" style="width: 165px;">
                                        <asp:DropDownList ID="DDLCustomerList" runat="server" TabIndex="1" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLCustomerList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <%-- </tr>

            <tr>
                            <td>&nbsp;&nbsp;&nbsp;</td>
                            <td>&nbsp;&nbsp;&nbsp;</td>--%>
                                    <div class="col-sm-2 htCr" style="width: 100px;">Status <b>:</b></div>
                                    <%-- <td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-3" style="width: 165px;">
                                        <asp:DropDownList runat="server" ID="DDLStatusSearch" TabIndex="2" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLSearch_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>


                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2 htCr" style="width: 100px;">Begin date <b>:</b></div>
                                    <%--  <td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-4" style="width: 165px;">
                                        <asp:TextBox ID="TxtFromDate" PlaceHolder="Begin Date" runat="server" TabIndex="3" CssClass="txtDropDownwidth"
                                            OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <Ajx:MaskedEditExtender ID="MEE_TxtFromDate" runat="server" AcceptNegative="Left"
                                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" TargetControlID="TxtFromDate" />
                                        <Ajx:CalendarExtender ID="CE_TxtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="TxtFromDate">
                                        </Ajx:CalendarExtender>
                                        <asp:RegularExpressionValidator ID="REV_TxtFromDate" runat="server" Display="Dynamic"
                                            CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtFromDate"
                                            SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                    <%-- <td>&nbsp;&nbsp;&nbsp;</td>
                            <td>&nbsp;&nbsp;&nbsp;</td>
                             </tr>
            <tr>--%>
                                    <div class="col-sm-2 htCr" style="width: 100px;">End date <b>:</b></div>
                                    <%--<td class="Td07"><b>:</b> </td>--%>
                                    <div class="col-sm-3" style="width: 165px;">
                                        <asp:TextBox ID="TxtToDate" runat="server" PlaceHolder="End Date" TabIndex="4" CssClass="txtDropDownwidth"
                                            OnTextChanged="TxtToDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <Ajx:MaskedEditExtender ID="MaskedEditExtender1" runat="server" AcceptNegative="Left"
                                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                            OnInvalidCssClass="MaskedEditError" TargetControlID="TxtToDate" />
                                        <Ajx:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                            TargetControlID="TxtToDate">
                                        </Ajx:CalendarExtender>

                                        <asp:CompareValidator ID="CV_TxtToDate" runat="server" ControlToCompare="TxtFromDate"
                                            CssClass="lblValidation" ControlToValidate="TxtToDate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                                            Operator="GreaterThanEqual" Type="Date" ForeColor="Red"></asp:CompareValidator>
                                        <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic"
                                            CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtToDate"
                                            SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-2">
                                        <asp:Button runat="server" ID="btnclear" OnClick="btnclear_Click" CausesValidation="false" Text="Clear" TabIndex="5" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-4">
                            <div class="Fr">


                                <table>
                                    <tr>
                                        <td>
                                            <asp:LinkButton ID="BtnFeedbackReport" runat="server" CssClass="Fr btnStyle" CausesValidation="false" Text="Feedback Report" OnClick="BtnFeedbackReport_Click"></asp:LinkButton>

                                        </td>
                                        <td>
                                            <p class="paddingstyl"></p>

                                        </td>
                                        <td>

                                            <asp:LinkButton ID="BTnBackdashboard" runat="server" CssClass="Fr  linkbtn" CausesValidation="false" Text="Back" OnClick="BTnBackdashboard_Click"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <%--<asp:LinkButton ID="BTnBackdashboard" runat="server" CssClass="Fr linkbtn" CausesValidation="false" TabIndex="8" Text="Back" OnClick="BTnBackdashboard_Click"></asp:LinkButton>--%>
                            <%--<br />--%>
                            <div id="divcont" runat="server" style="width: 100%">
                                <div style="margin-bottom: 20px !important"></div>
                                <ul class="Ul02">

                                    <li class="Fl Brd01" style="padding-top: 0 !important; margin-top: 0 !important">
                                        <ul class="Ul01">
                                            <li class="TxtC FntPIPO">

                                                <b id="sla1" runat="server">SLA Time Progress Legend</b>

                                                <%--<Ajx:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlSLADetls" TargetControlID="lnkbtn"
                                                                        BackgroundCssClass="modalBackground" CancelControlID="btnADvanceClose">
                                                                    </Ajx:ModalPopupExtender>--%>
                                                <Ajx:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="sla1"
                                                    PopupControlID="pnlSLADetls" PopupPosition="Left" HoverCssClass="modalBackground">
                                                </Ajx:HoverMenuExtender>
                                                <%--<Ajx:HoverMenuExtender ID="HoverMenuExtender2" runat="server" TargetControlID="sla2"
                                                    PopupControlID="pnlSLADetls" PopupPosition="Left" HoverCssClass="modalBackground">
                                                </Ajx:HoverMenuExtender>
                                                <Ajx:HoverMenuExtender ID="HoverMenuExtender3" runat="server" TargetControlID="sla3"
                                                    PopupControlID="pnlSLADetls" PopupPosition="Left" HoverCssClass="modalBackground">
                                                </Ajx:HoverMenuExtender>
                                                <Ajx:HoverMenuExtender ID="HoverMenuExtender4" runat="server" TargetControlID="sla4"
                                                    PopupControlID="pnlSLADetls" PopupPosition="Left" HoverCssClass="modalBackground">
                                                </Ajx:HoverMenuExtender>--%>
                                                <%--<asp:Button ID="Button1" runat="server" Style="Display: none;" Text="Button" />--%></li>
                                            <li>
                                                <table>
                                                    <tr>
                                                        <td class="Tdlegend1">
                                                            <div class="GreenCls"></div>
                                                        </td>
                                                        <td class="Tdlegend2">- </td>
                                                        <td><b><%--<a style="color: #6d5555 !important" id="sla1" runat="server">SLA Time</a> 0-40%--%>
                                                            <asp:LinkButton ID="lnkBtnSLA1" OnClick="lnkBtnSLA1_Click" CssClass="linkbtn" runat="server">SLA Time 0-40%</asp:LinkButton>
                                                        </b></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Tdlegend1">
                                                            <div class="AmberCls"></div>
                                                        </td>
                                                        <td class="Tdlegend2">- </td>
                                                        <td><b><%--<a style="color: #6d5555 !important" id="sla2" runat="server">SLA Time</a> 40-80%--%>
                                                            <asp:LinkButton ID="lnkBtnSLA2" OnClick="lnkBtnSLA2_Click" CssClass="linkbtn" runat="server">SLA Time 40-80%</asp:LinkButton>
                                                        </b></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Tdlegend1">
                                                            <div class="RedCls"></div>
                                                        </td>
                                                        <td class="Tdlegend2">- </td>
                                                        <td><b><%--<a style="color: #6d5555 !important" id="sla3" runat="server">SLA Time</a> Above 80%--%>
                                                            <asp:LinkButton ID="lnkBtnSLA3" OnClick="lnkBtnSLA3_Click" CssClass="linkbtn" runat="server">SLA Time Above 80%</asp:LinkButton>
                                                        </b></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="Tdlegend1">
                                                            <div class="darkRed"></div>
                                                        </td>
                                                        <td class="Tdlegend2">- </td>
                                                        <td><b><%--<a style="color: #6d5555 !important" id="sla4" runat="server">SLA Time</a> Breached--%>
                                                            <asp:LinkButton ID="lnkBtnSLA4" OnClick="lnkBtnSLA4_Click" CssClass="linkbtn" runat="server">SLA Time Breached</asp:LinkButton>
                                                        </b></td>
                                                    </tr>

                                                </table>

                                            </li>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                    <div class="DivSpacer03">
                        <hr />
                    </div>
                    <%--  <fieldset style="width: 100%; border: 1px solid grey">
                <legend style="width: 70px; border: 0px;">Tickets</legend>--%>


                    <div id="ticketdiv" runat="server" style="width: 99%; margin: 0 auto; padding: 2px;">
                        <asp:Label ID="LblTicket" runat="server" CssClass="lblValidation"></asp:Label>
                        <asp:Panel ID="pnlgrid" runat="server" Width="99%">
                            <div class="respovrflw" style="max-height: 350px">
                                <asp:GridView runat="server" ID="GV_Tickets" AutoGenerateColumns="False" Width="99%" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager"
                                    DataKeyNames="TID,PERCENTAGE,STATUS,CATEGORY,SecSLA" OnRowDataBound="GV_Tickets_RowDataBound" OnRowCommand="GV_Tickets_RowCommand" OnSorting="GV_Tickets_Sorting" AllowSorting="true">

                                    <Columns>
                                        <%-- AllowPaging="true" PageSize="10" OnPageIndexChanging="GV_Tickets_PageIndexChanging"--%>
                                        <asp:TemplateField HeaderText="Slno" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TID" HeaderText="Ticket ID" SortExpression="TID" />
                                        <asp:BoundField DataField="TITLE" HeaderText="Title" SortExpression="TITLE" />
                                        <%--<asp:BoundField DataField="CLIENT" HeaderText="Client" />--%>
                                        <asp:TemplateField HeaderText="Client" SortExpression="CLIENT">
                                            <ItemTemplate>
                                                <%#Eval("CLIENT") + "-"+Eval("CLIENTNNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FRMUSR" HeaderText="From User" SortExpression="FRMUSR" />
                                        <%--<asp:BoundField DataField="ASSIGNEE" HeaderText="Assignee" />--%>
                                        <asp:TemplateField HeaderText="Assignee" SortExpression="ASSIGNEE">
                                            <ItemTemplate>
                                                <%#Eval("ASSIGNEE") + "-"+ Eval("TOASSIGNEENNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PriorityTxt" HeaderText="Priority" SortExpression="PriorityTxt" />
                                        
                                        <asp:BoundField DataField="CategoryTxt" HeaderText="Category" SortExpression="CategoryTxt" />
                                        <asp:BoundField DataField="ISSCATEGORYCSSTxt" HeaderText="Module" SortExpression="Module" />
                                        <asp:BoundField DataField="IssueTypeTxt" HeaderText="Issue Type" SortExpression="IssueType" />

                                        <asp:TemplateField HeaderText="SLA">
                                            <ItemTemplate>
                                                <%#((Eval("CATEGORY").ToString().Trim() =="2" || Eval("CATEGORY").ToString().Trim() =="3") ? "Yes" :"No") %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <%--<asp:BoundField DataField="AGENT" HeaderText="Agent" />--%>
                                        <asp:TemplateField HeaderText="Agent" SortExpression="AGENT">
                                            <ItemTemplate>
                                                <%#Eval("AGENT") + "-"+ Eval("AGENTNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Plndhrs" HeaderText="Planned Hours" SortExpression="Plndhrs" />
                                         <asp:BoundField DataField="Actualhrs" HeaderText="Actual Hours" SortExpression="Actulhrs" />
                                        <asp:TemplateField HeaderText="Ticket Ref. To" SortExpression="TIDREF">
                                            <ItemTemplate>
                                                <%#((string.IsNullOrEmpty(Eval("TIDREF").ToString()) || Eval("TIDREF").ToString().Trim()=="0") ? "-"  : Eval("TIDREF"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="StatusTxt" HeaderText="Status" SortExpression="StatusTxt" />
                                        <%--<asp:BoundField DataField="PERCENTAGE" HeaderText="percentage"/>--%>

                                        <asp:BoundField DataField="CREATED_ON" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HeaderText="Created On" SortExpression="CREATED_ON" />
                                        <%-- <asp:BoundField DataField="LASTMODIFIED_ON" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Last Modified On" />--%>

                                        <asp:TemplateField HeaderText="Last Modified By" SortExpression="LASTMODIFIED_BY">
                                            <ItemTemplate>
                                                <%#(Eval("LASTMODIFIED_BY")=="" ?Eval("CLIENT") +"-"+ Eval("CLIENTNNAME")  : Eval("LASTMODIFIED_BY") + "-" + Eval("MODIFIEDONNAME"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Modified On" SortExpression="LASTMODIFIED_ON">
                                            <ItemTemplate>
                                                <%#(Eval("LASTMODIFIED_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900") ? Eval("CREATED_ON" ,"{0:dd-MMM-yyyy HH:mm:ss} ") : Eval("LASTMODIFIED_ON","{0:dd-MMM-yyyy HH:mm:ss} ")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Breached?">
                                            <ItemTemplate>
                                                <%#Eval("SecSLA")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Breached Time (Days.hh:mm:ss)">
                                            <ItemTemplate>
                                                <%#Eval("SecSLA")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Average Rating">
                                            <ItemTemplate>
                                                <%#(decimal.Parse(Eval("TRAVERAGE").ToString().Trim()) <=0 ? " - " : Eval("TRAVERAGE"))%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnFeedbackView" runat="server" CausesValidation="false" CommandName="FBVIEW" TabIndex="23"
                                                    Visible='<%# bool.Parse(string.Format("{0}", (Eval("STATUS").ToString().Trim()=="8" || Eval("STATUS").ToString().Trim()=="9") ?"true" : "false"))%>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="View Feedback"></asp:LinkButton>
                                                &nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="LASTMODIFIED_BY" HeaderText="Last Modified By" />--%>
                                        <%-- <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnTicketView" runat="server" CausesValidation="false" CommandName="VIEW"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="View"></asp:LinkButton>
                                &nbsp;
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                    </Columns>
                                    <RowStyle CssClass="rowheight" />
                                </asp:GridView>
                            </div>

                            <div class="DivSpacer01 Div02">
                                <div class="col-sm-9">
                                    <asp:Repeater ID="RptrPendingPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="PendingPage_Changed" CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled linkbtn" : "page_disabled linkbtn" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="col-sm-2 htCr" style="width: 105px">Page Size <b>:</b></div>
                                <div class="col-sm-1">
                                    <asp:DropDownList ID="ddlPagesize" runat="server" CssClass="txtDropDownwidth" AutoPostBack="true" OnSelectedIndexChanged="ddlPagesize_SelectedIndexChanged" Style="float: right; width: 60px;">
                                        <asp:ListItem Text="10" Value="10" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    <%--</fieldset>--%>
                    <div class="DivSpacer01"></div>


                    <%--OnRowCommand="GV_Tickets_RowCommand" --%>
                    <%--        <asp:FormView runat="server" ID="FormTicket" Width="99%">

            <ItemTemplate>

                <div class="DivSpacer01"></div>
                <div id="div1" style="float: left; width: 49%">
                    <table style="border-collapse: collapse; width: 100%;">


                        <tr>
                            <td class="Td06">Ticket ID</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("TID"))%> 
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Title</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("TITLE"))%> 
                            </td>
                        </tr>

                        <tr>
                            <td class="Td06">Issue Description</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("ISSDESC"))%> 
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Client</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("CLIENT")) +"-"+ (Eval("CLIENTNNAME"))%> 
                            </td>
                        </tr>

                        <tr>
                            <td class="Td06">Raised By</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("FRMUSR"))%> 
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Email</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("USRMAILID"))%> 
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Priority</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("PriorityTxt"))%> 
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Category</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("CategoryTxt"))%> 
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Category</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("IssueCategoryTxt"))%> 
                            </td>
                        </tr>

                    </table>
                </div>
                <div id="div2" style="float: right; width: 49%">
                    <table style="border-collapse: collapse; width: 100%;">


                        <tr>
                            <td class="Td06">Current Status</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("StatusTxt"))%> 
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Currently With</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("ASSIGNEE")) + " - " +(Eval("TOASSIGNEENNAME"))%> 
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Created on</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("CREATED_ON"))%> 
                            </td>
                        </tr>

                        <tr>
                            <td class="Td06">Last Modified By</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("LASTMODIFIED_BY")=="" ?Eval("CLIENT") + "-"+Eval("CLIENTNNAME") : Eval("LASTMODIFIED_BY") +"-"+Eval("MODIFIEDONNAME"))%>  
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Last Modified On</td>
                            <td class="Td07"><b>:</b> </td>

                            <td>
                                <%#(Eval("LASTMODIFIED_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900") ? Eval("CREATED_ON" ,"{0:dd-MMM-yyyy HH:mm:ss} ") : Eval("LASTMODIFIED_ON","{0:dd-MMM-yyyy HH:mm:ss} ")%>
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Agent</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("AGENT")) + "-"+(Eval("AGENTNAME"))%> 
                            </td>
                        </tr>
                        <tr>
                            <td class="Td06">Ticket ref</td>
                            <td class="Td07"><b>:</b> </td>
                            <td><%#(Eval("TIDREF"))%> 
                            </td>
                        </tr>

                    </table>
                </div>
            </ItemTemplate>
        </asp:FormView>

        <div runat="server" id="divAccordion">

            <Ajx:Accordion ID="UserAccordion" runat="server" SelectedIndex="0" HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent" FadeTransitions="true" SuppressHeaderPostbacks="true" TransitionDuration="250" FramesPerSecond="40" RequireOpenedPane="false" AutoSize="None">
                <Panes>


                    <Ajx:AccordionPane ID="AccordionPane2" runat="server">
                        <Header><a href="#" class="href">Comments</a></Header>
                        <Content>
                            <div>


                                <asp:GridView ID="grdTicketsComments" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="99%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Slno">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TID" HeaderText="Ticket ID" />
                                        <asp:BoundField DataField="COMMENTS" HeaderText="Comments" />
                                        <asp:TemplateField HeaderText="Commented By">
                                            <ItemTemplate>
                                                <%# Eval("MODIFIED_BY") + " - " + Eval("MODIFIEDONNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MODIFIED_ON" HeaderText="Commented On" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </Content>
                    </Ajx:AccordionPane>

                    <Ajx:AccordionPane ID="AccordionPane1" runat="server">
                        <Header><a href="#" class="href">Attachments</a></Header>
                        <Content>
                            <div>


                                <asp:GridView ID="GrdTicketsAttachments" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="99%"
                                    OnRowCommand="GrdTicketsAttachments_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Slno">
                                            <ItemTemplate>
                                                <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="TID" HeaderText="Ticket ID" />
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Attachments
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("ATTACHEMENT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("ATTACHEMENT_FPATH") %>' CausesValidation="false" />
                                            </ItemTemplate>
                                            <ItemStyle Width="100" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Uploaded By">
                                            <ItemTemplate>
                                                <%# Eval("MODIFIED_BY") + " - " + Eval("MODIFIEDONNAME")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="MODIFIED_ON" HeaderText="Uploaded On" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </Content>
                    </Ajx:AccordionPane>

                    <Ajx:AccordionPane ID="AccordionPane3" runat="server">
                        <Header><a href="#" class="href">Status</a> </Header>
                        <Content>


                            <asp:GridView ID="GrdTicketStatus" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="99%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Slno">
                                        <ItemTemplate>
                                            <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TID" HeaderText="Ticket ID" />
                                    <asp:TemplateField HeaderText="From User">
                                        <ItemTemplate>
                                            <%#Eval("FRMASSIGNEE") + "-"+Eval("FRMASSIGNEENNAME") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To User">
                                        <ItemTemplate>
                                            <%#Eval("TOASSIGNEE") + "-"+Eval("TOASSIGNEENNAME") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From Status">
                                        <ItemTemplate>
                                            <%#Eval("FRMSTATUSTXT") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Status">
                                        <ItemTemplate>
                                            <%#Eval("TOSTATUSTXT") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="By">
                                        <ItemTemplate>
                                            <%# Eval("MODIFIED_BY") + " - " + Eval("MODIFIEDONNAME")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="MODIFIED_ON" HeaderText="On" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" />
                                </Columns>
                            </asp:GridView>
                        </Content>
                    </Ajx:AccordionPane>
                </Panes>
            </Ajx:Accordion>
        </div>
        <div class="DivSpacer01"></div>

        <fieldset id="Taskfs" runat="server" style="width: 100%; border: 1px solid grey">
            <legend style="width: 70px; border: 0px;">Task</legend>
            <div id="taskdiv" runat="server" style="width: 99%; margin: 0 auto; padding: 2px;">
                <asp:Label ID="LblTask" runat="server" CssClass="lblValidation"></asp:Label>

                <asp:GridView ID="GV_Task" runat="server" AutoGenerateColumns="False" Width="99%" CssClass="Grid" GridLines="Both"
                    DataKeyNames="TICKETID,TASKID" OnRowCommand="GV_Task_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Slno">
                            <ItemTemplate>
                                <asp:Label ID="LblSlno" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="TICKETID" HeaderText="Ticket ID" />
                        <asp:BoundField DataField="TASKID" HeaderText="Task ID" />
                        <asp:BoundField DataField="TASKTITLE" HeaderText="Title" />
                        <asp:TemplateField HeaderText="Agent">
                            <ItemTemplate>
                                <%#Eval("TASKAGENT") + " - " + Eval("AGENTNAME") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Task Assigned By">
                            <ItemTemplate>
                                <%#  Eval("TASKCREATED_BY") + " - " + Eval("CREATEDONNAME") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="StatusTxt" HeaderText="Status" />
                        <asp:BoundField DataField="TASKCREATED_ON" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" HeaderText="Created On" />

                        <asp:TemplateField HeaderText="Last Modified By">
                            <ItemTemplate>
                                <%#(Eval("TASKMODIFIED_BY")=="" ? Eval("TASKCREATED_BY") + " - " + Eval("CREATEDONNAME") : Eval("TASKMODIFIED_BY") + " - " + Eval("MODIFIEDONNAME"))%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Modified On">
                            <ItemTemplate>
                                <%#(Eval("TASKMODIFIED_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900") ? Eval("TASKCREATED_ON" ,"{0:dd-MMM-yyyy HH:mm:ss} ") : Eval("TASKMODIFIED_ON","{0:dd-MMM-yyyy HH:mm:ss} ")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnTicketView" runat="server" CausesValidation="false" CommandName="VIEW"
                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="View"></asp:LinkButton>
                                &nbsp;
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />

            </div>
        </fieldset>


        <div class="DivSpacer01"></div>--%>


                    <div runat="server" id="TicketExports">
                        <div class="form-inline">
                            <div class="btn-group-lg">
                                <div class="col-sm-2">
                                    <asp:Button ID="BtnExporttoXl" Width="120px" runat="server" TabIndex="6" Text="Export To Excel" OnClick="BtnExporttoXl_Click" CausesValidation="false" />
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="BtnExporttoPDF" Width="120px" runat="server" TabIndex="7" Text="Export To PDF" OnClick="BtnExporttoPDF_Click" />
                                </div>
                                <br />
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="viewcheck" runat="server" />


                    <asp:Panel ID="pnlSLADetls" runat="server" CssClass="modal-content">
                        <div>
                            <div style="float: left; top: 0px;">
                                <h4>SLA Time Details</h4>
                                <%--<asp:Button ID="btnADvanceClose" runat="server" Text="&times; Close"  />--%>
                            </div>
                            <div class="respovrflw">
                                <asp:GridView ID="GV_SLADetls" runat="server" AutoGenerateColumns="False" Width="99%" CssClass="Grid1" AllowSorting="True" PagerStyle-CssClass="cssPager">

                                    <Columns>
                                        <asp:BoundField DataField="Trouble" HeaderText="Trouble" />
                                        <%--<asp:BoundField DataField="priority" HeaderText="Priority"/>--%>
                                        <asp:BoundField DataField="priority_type" HeaderText="Priority Type" />
                                        <asp:BoundField DataField="Resp_time" HeaderText="Response Time" />
                                        <asp:BoundField DataField="Resol_time" HeaderText="Resolution Time" />
                                        <asp:BoundField DataField="Resol_days" HeaderText="Resolution Days" />
                                    </Columns>
                                    <PagerStyle CssClass="cssPager" />

                                </asp:GridView>
                    </asp:Panel>

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="BtnExporttoXl" />
                    <asp:PostBackTrigger ControlID="BtnExporttoPDF" />
                    <asp:PostBackTrigger ControlID="btnclear" />
                    
                </Triggers>
            </asp:UpdatePanel>


        </div>
    </div>

    <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
    <Ajx:ModalPopupExtender ID="mp1" runat="server" PopupControlID="PnlRating" TargetControlID="lnkDummy" BehaviorID="mpe"
        BackgroundCssClass="modalBackground">
    </Ajx:ModalPopupExtender>


    <%--  <asp:UpdatePanel ID="UPCustRating" runat="server">
                <ContentTemplate>--%>
    <asp:Panel runat="server" ID="PnlRating" Width="99%" Height="600px">
        <div runat="server" id="RatingDiv">
            <div id="CustRatings" runat="server" class="col-sm-12">
                <div>
                    <h3 style="color: black;">Customer feedback for Ticket ID
                                <asp:Label ID="RTID" runat="server"></asp:Label></h3>
                    <div style="width: 99%">
                        <b style="color: black;">Please take a few minutes to give us feedback about our service by filling in this short Customer Feedback Form. We are conducting this research in order
                                        to measure your level of satisfaction with the quality of our service. We thank you for your participation.
                        </b>
                    </div>
                    <div class="DivSpacer01"></div>
                    <div class="DivSpacer01"></div>
                    <div class="form-inline">

                        <div class="form-group">
                            <div class="htCrrating Cb">
                                <b>Processing time for this message - How satisfied are you?</b>
                            </div>

                        </div>
                        <div class="DivSpacer01"></div>
                        <div class="DivSpacer01"></div>
                        <div class="form-group">
                            <div class="col-sm-6 htCrrating">
                                <p>With the initial reaction time from ITChamps:</p>
                            </div>
                            <div class="col-sm-3">
                                <Ajx:Rating runat="server" ID="Rating1" Enabled="false" BehaviorID="RatingBehavior1" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                    WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                </Ajx:Rating>
                            </div>
                        </div>
                        <div class="DivSpacer01"></div>
                        <div class="DivSpacer01"></div>
                        <div class="form-group">
                            <div class="col-sm-6 htCrrating">
                                <p>With the total time used by ITChamps for solving this issue:</p>
                            </div>
                            <div class="col-sm-3">
                                <Ajx:Rating runat="server" ID="Rating2" Enabled="false" BehaviorID="RatingBehavior2" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                    WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                </Ajx:Rating>
                            </div>
                        </div>
                    </div>
                    <div class="DivSpacer01"></div>
                    <div class="DivSpacer01"></div>
                    <div class="form-inline">
                        <div class="form-group">
                            <div class="htCrrating Cb">
                                <b>ITChamps Employee who solved your issue - How satisfied are you?</b>
                            </div>

                        </div>
                        <div class="DivSpacer01"></div>
                        <div class="DivSpacer01"></div>
                        <div class="form-group">
                            <div class="col-sm-6 htCrrating">
                                <p>With his/her experience and knowledge for the job:</p>
                            </div>
                            <div class="col-sm-3">
                                <Ajx:Rating runat="server" ID="Rating3" Enabled="false" BehaviorID="RatingBehavior3" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                    WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                </Ajx:Rating>
                            </div>
                        </div>
                        <div class="DivSpacer01"></div>
                        <div class="DivSpacer01"></div>
                        <div class="form-group">
                            <div class="col-sm-6 htCrrating">
                                <p>With his/her friendliness and behaviour:</p>
                            </div>
                            <div class="col-sm-3">
                                <Ajx:Rating runat="server" ID="Rating4" Enabled="false" BehaviorID="RatingBehavior4" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                    WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                </Ajx:Rating>
                            </div>
                        </div>
                        <div class="DivSpacer01"></div>
                        <div class="DivSpacer01"></div>
                        <div class="form-group">
                            <div class="col-sm-6 htCrrating">
                                <p>How satisfied are you with Quality of Solution provided:</p>
                            </div>
                            <div class="col-sm-3">
                                <Ajx:Rating runat="server" ID="Rating5" Enabled="false" BehaviorID="RatingBehavior5" MaxRating="5" CurrentRating="3" CssClass="ratingStar" StarCssClass="ratingItem"
                                    WaitingStarCssClass="Saved" FilledStarCssClass="Filled" EmptyStarCssClass="Empty">
                                </Ajx:Rating>
                            </div>
                        </div>


                    </div>
                    <div class="DivSpacer01"></div>
                    <div class="DivSpacer01"></div>
                    <div class="form-inline">
                        <div class="form-group">
                            <div class="col-sm-6 htCrrating">
                                <p>Suggestions / Comments</p>
                            </div>
                            <div class="col-sm-3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Label runat="server" ID="RatingSug"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="DivSpacer01"></div>
            <div class="DivSpacer01"></div>
            <div class="DivSpacer01" style="padding-left: 20px">
                <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="return HideModalPopup()" />
            </div>
        </div>
    </asp:Panel>
    <%--</ContentTemplate>
            </asp:UpdatePanel>--%>



    <script type="text/javascript" language="javascript">
        function pageLoad() {
            //change its title

            var RatingID1 = "<%=Rating1.ClientID %>";
                    for (i = 1; i <= $find("RatingBehavior1").get_MaxRating() ; i++) {
                        switch (i) {
                            case 1: $get(RatingID1 + "_Star_" + (i).toString()).title = "Very Dissatisfied"; break;
                            case 2: $get(RatingID1 + "_Star_" + (i).toString()).title = "Dissatisfied"; break;
                            case 3: $get(RatingID1 + "_Star_" + (i).toString()).title = "Acceptable"; break;
                            case 4: $get(RatingID1 + "_Star_" + (i).toString()).title = "Satisfied"; break;
                            case 5: $get(RatingID1 + "_Star_" + (i).toString()).title = "Very Satisfied"; break;
                            default: break;
                        }
                    }

                    var RatingID2 = "<%=Rating2.ClientID %>";
            for (j = 1; j <= $find("RatingBehavior2").get_MaxRating() ; j++) {
                switch (j) {
                    case 1: $get(RatingID2 + "_Star_" + (j).toString()).title = "Very Dissatisfied"; break;
                    case 2: $get(RatingID2 + "_Star_" + (j).toString()).title = "Dissatisfied"; break;
                    case 3: $get(RatingID2 + "_Star_" + (j).toString()).title = "Acceptable"; break;
                    case 4: $get(RatingID2 + "_Star_" + (j).toString()).title = "Satisfied"; break;
                    case 5: $get(RatingID2 + "_Star_" + (j).toString()).title = "Very Satisfied"; break;
                    default: break;
                }
            }


            var RatingID3 = "<%=Rating3.ClientID %>";
            for (k = 1; k <= $find("RatingBehavior3").get_MaxRating() ; k++) {
                switch (k) {
                    case 1: $get(RatingID3 + "_Star_" + (k).toString()).title = "Very Dissatisfied"; break;
                    case 2: $get(RatingID3 + "_Star_" + (k).toString()).title = "Dissatisfied"; break;
                    case 3: $get(RatingID3 + "_Star_" + (k).toString()).title = "Acceptable"; break;
                    case 4: $get(RatingID3 + "_Star_" + (k).toString()).title = "Satisfied"; break;
                    case 5: $get(RatingID3 + "_Star_" + (k).toString()).title = "Very Satisfied"; break;
                    default: break;
                }
            }

            var RatingID4 = "<%=Rating4.ClientID %>";
            for (m = 1; m <= $find("RatingBehavior4").get_MaxRating() ; m++) {
                switch (m) {
                    case 1: $get(RatingID4 + "_Star_" + (m).toString()).title = "Very Dissatisfied"; break;
                    case 2: $get(RatingID4 + "_Star_" + (m).toString()).title = "Dissatisfied"; break;
                    case 3: $get(RatingID4 + "_Star_" + (m).toString()).title = "Acceptable"; break;
                    case 4: $get(RatingID4 + "_Star_" + (m).toString()).title = "Satisfied"; break;
                    case 5: $get(RatingID4 + "_Star_" + (m).toString()).title = "Very Satisfied"; break;
                    default: break;
                }
            }

            var RatingID5 = "<%=Rating5.ClientID %>";
            for (n = 1; n <= $find("RatingBehavior5").get_MaxRating() ; n++) {
                switch (n) {
                    case 1: $get(RatingID5 + "_Star_" + (n).toString()).title = "Very Dissatisfied"; break;
                    case 2: $get(RatingID5 + "_Star_" + (n).toString()).title = "Dissatisfied"; break;
                    case 3: $get(RatingID5 + "_Star_" + (n).toString()).title = "Acceptable"; break;
                    case 4: $get(RatingID5 + "_Star_" + (n).toString()).title = "Satisfied"; break;
                    case 5: $get(RatingID5 + "_Star_" + (n).toString()).title = "Very Satisfied"; break;
                    default: break;
                }
            }

        }
    </script>
</asp:Content>
