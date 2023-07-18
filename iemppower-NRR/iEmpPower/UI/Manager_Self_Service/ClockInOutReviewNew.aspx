<%@ Page Title="Punch In / Punch Out" Language="C#" MasterPageFile="~/UI/SubSiteMaster.Master" AutoEventWireup="true" CodeBehind="ClockInOutReviewNew.aspx.cs"
    Inherits="iEmpPower.UI.Manager_Self_Service.ClockInOutReviewNew" EnableEventValidation="false" Culture="en-GB" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .HrCls {
            width: 100%;
            border: 0;
            height: 1px;
            background: #333;
            background-image: linear-gradient(to right, #333, #333, #ccc);
            padding: 0;
            margin: 3px 0;
        }

        .Td06 {
            color: #004080;
            font-size: 13px;
            width: 150px;
            padding: 3px;
            text-align: justify !important;
            line-height: 30px;
        }

        .Tdlegend1 {
            color: #004080;
            font-size: 13px;
            line-height: 12px !important;
            padding: 3px;
        }

        .Tdlegend2 {
            color: #004080;
            font-size: 13px;
            text-align: center;
            line-height: 12px !important;
            width: 10px;
            padding: 8px;
        }


        .Div01 {
            width: 60%;
            margin: 0 1%;
        }


        .page_enabled, .page_disabled {
            display: inline-block;
            text-align: center;
            text-decoration: none;
            padding: 0 8px;
            border: 1px solid #ccc;
            font: normal normal normal 11px/25px Verdana,sans-serif;
            border-radius: 3px;
            box-shadow: 1px 1px 3px #ccc;
        }

        .page_enabled {
            background-color: #eee;
            color: #000;
        }

        .page_disabled {
            background-color: #6C6C6C;
            color: #fff !important;
        }

        .Pager span {
            text-align: center;
            color: #999;
            display: inline-block;
            width: 24px;
            background-color: #A1DCF2;
            margin-right: 3px;
            line-height: 150%;
            border: 1px solid #3AC0F2;
            font: normal normal normal 10px/22px opensans,Verdana,Arial,Helvetica,sans-serif;
        }

        .Pager a {
            text-align: center;
            display: inline-block;
            width: 24px;
            background-color: #3AC0F2;
            color: #fff;
            border: 1px solid #3AC0F2;
            margin-right: 3px;
            line-height: 150%;
            text-decoration: none;
            font: normal normal normal 10px/22px opensans,Verdana,Arial,Helvetica,sans-serif;
        }

        .container {
            margin: 0;
            border: 0px solid #000;
        }

        .Ul01, .Ul02 {
            list-style: none;
            margin: 0;
            padding: 0;
        }

            .Ul01 li {
                padding: 5px 0 5px 10px;
                margin: 3px;
            }

            .Ul02 li {
                padding: 3px 10px 3px 10px;
                margin: 3px;
            }

        .TxtC {
            text-align: center !important;
        }

        .Brd01 {
            border: 1px solid #666;
        }

        .FntPIPO {
            border-bottom: 1px solid #ccc !important;
        }

        .close {
            background-color: white !important;
            border: none !important;
            font-size: small;
        }

        .popUpStyle {
            /*font: normal 11px auto "Trebuchet MS", Verdana;*/
            background-color: #000000;
            /*color: #4f6b72;*/
            /*padding: 6px;*/
            filter: alpha(opacity=80);
            opacity: 0.15;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }


        .modalPopupDefault {
            text-align: left;
            background-color: #FFFFFF;
            /* border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;*/
            /*padding-left: 10px;*/
            width: auto;
            height: 80vh;
            overflow: auto;
        }
    </style>

    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Punch&nbsp;In&nbsp;/&nbsp;Out&nbsp; Details</li>
                    </ol>
                </div>
                <h4 class="page-title">Punch&nbsp;In&nbsp;/&nbsp;Out&nbsp; Details
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
    </div>

    <div class="card-box">
        <div id="real_time_chart" class="dashboard-flot-chart">
            <div class="header-title">Detailed Report</div>
            <hr class="HrCls" />
            <br />

            <div class="form-group">
                <div class="row">
                    <asp:RangeValidator ID="RV_TxtToDate" runat="server" ControlToValidate="TxtToDate" Display="Dynamic"
                        CssClass="lblMsg" MinimumValue='01/01/2010' MaximumValue='01/01/2019' ValidationGroup="PVg"
                        ErrorMessage='<%# string.Format("To dates should be less than {0}.", new DateTime(DateTime.Today.Year, DateTime.Today.Month,  DateTime.Today.Day).AddDays(-1).ToString("dd/MM/yyyy"))%>'
                        Type="Date" ForeColor="Red"></asp:RangeValidator>
                    &nbsp;&nbsp;
                          <asp:DropDownList ID="DDLEmpNames" runat="server" TabIndex="1" CssClass="txtDropDownwidth" ValidationGroup="PVg"></asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RFV_DDLEmpNames" runat="server" ControlToValidate="DDLEmpNames" ErrorMessage="*"
                        CssClass="lblValidation" Display="dynamic" InitialValue="0" ValidationGroup="PVg" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;                                        
                                            <asp:TextBox ID="TxtFrmDate" runat="server" placeholder="DD/MM/YYYY" CssClass="txtDropDownwidth" TabIndex="2" ValidationGroup="PVg"></asp:TextBox>
                    <Ajx:MaskedEditExtender ID="MEE_TxtFrmDate" runat="server" AcceptNegative="Left" CultureName="en-GB"
                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                        MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                        TargetControlID="TxtFrmDate" />
                    <Ajx:CalendarExtender ID="CE_TxtFrmDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="TxtFrmDate">
                    </Ajx:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RFV_TxtFrmDate" runat="server" ControlToValidate="TxtFrmDate" ErrorMessage="*"
                        CssClass="lblValidation" Display="Static" ValidationGroup="PVg" ForeColor="Red"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;
                                            <asp:TextBox ID="TxtToDate" runat="server" placeholder="DD/MM/YYYY" CssClass="txtDropDownwidth" TabIndex="3" ValidationGroup="PVg"></asp:TextBox>
                    <Ajx:MaskedEditExtender ID="MEE_TxtToDate" runat="server" AcceptNegative="Left" CultureName="en-GB"
                        DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date"
                        MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError"
                        TargetControlID="TxtToDate" />
                    <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="TxtToDate">
                    </Ajx:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RFV_TxtToDate" runat="server" ControlToValidate="TxtToDate" ErrorMessage="*"
                        CssClass="lblValidation" Display="Static" ValidationGroup="PVg" ForeColor="Red"></asp:RequiredFieldValidator>

                    &nbsp;&nbsp;
                                        <asp:Button ID="BtnSubmit" Width="65px" runat="server" Text="Search" ValidationGroup="PVg" OnClick="BtnSubmit_Click" CausesValidation="true" />
                    &nbsp;&nbsp;
                                        <asp:Button ID="btnClear" Width="65px" runat="server" Text="Clear" OnClick="btnClear_Click" />
                    &nbsp;&nbsp;
                                        <asp:Button ID="btnExportToExcel" Width="65px" runat="server" Text="Export" OnClick="btnExportToExcel_Click" Visible="false" />



                </div>
                <asp:CompareValidator ID="CV_TxtToDate" runat="server" ControlToCompare="TxtFrmDate" CssClass="lblValidation"
                    ControlToValidate="TxtToDate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                    Operator="GreaterThanEqual" Type="Date" ValidationGroup="PVg" ForeColor="Red"></asp:CompareValidator>
                <asp:UpdatePanel ID="up" runat="server">
                    <ContentTemplate>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExportToExcel" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>




            <div id="DetaildRpt" runat="server">
                <div class="respovrflw">
                    <asp:GridView ID="GV_ClockInClockOut" runat="server" AutoGenerateColumns="False" DataKeyNames="ROWNUMBERCICO,DATES,count,PERNR"
                        OnRowDataBound="GV_ClockInClockOut_RowDataBound" OnRowCommand="GV_ClockInClockOut_RowCommand">

                        <Columns>
                            <asp:BoundField DataField="ROWNUMBERCICO" HeaderText="Sl No."></asp:BoundField>
                            <asp:BoundField DataField="PERNR" HeaderText="Emp ID"></asp:BoundField>
                            <asp:BoundField DataField="DATES1" HeaderText="Date"></asp:BoundField>
                            <asp:BoundField DataField="PUNCH_IN" HeaderText="Punch In"></asp:BoundField>
                            <asp:BoundField DataField="PUNCH_OUT" HeaderText="Punch Out"></asp:BoundField>
                            <asp:BoundField DataField="TOTAL_HOURS" HeaderText="Total Working Hrs" DataFormatString="{0:hh:mm:ss}"></asp:BoundField>
                            <asp:BoundField DataField="brkhrs" HeaderText="Total Break Hrs"></asp:BoundField>
                            <asp:BoundField DataField="STATUS" HeaderText="Status"></asp:BoundField>
                            <asp:BoundField DataField="TXT_LONG" HeaderText="Holiday Name"></asp:BoundField>

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkviewindetail" ToolTip="View" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false"
                                        CommandName="view" CssClass="fe-eye"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%#Eval("PERNR") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>


                    <div class="row col-md-12" id="DivPaging" runat="server">
                        <div class="col-md-3" style="margin-top: 5px" id="divpendingrecordcount" runat="server"></div>
                        <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                            <asp:Repeater ID="RptrClkInClkOutPager" runat="server">
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





                <Ajx:ModalPopupExtender BackgroundCssClass="popUpStyle" ID="modal1"
                    runat="server" PopupControlID="divpopupcomp" TargetControlID="Button1" CancelControlID="LK_closeComp">
                </Ajx:ModalPopupExtender>
                <button style="display: none;" id="Button1" runat="server"></button>

                <div id="divpopupcomp" runat="server" class="modalPopupDefault" align="center">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title">Punch In/ Out Details </h4>
                                <asp:LinkButton ID="LK_closeComp" class="close" data-dismiss="modal" aria-hidden="true" runat="server" Text="X" />
                            </div>
                            <div class="modal-body">
                                <asp:Panel ID="pnl_empdesidmgr" runat="server">
                                    <asp:GridView ID="GV_punchinfulldetails" runat="server" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl NO.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DATES" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                                            <asp:BoundField DataField="DATES" HeaderText="Day" DataFormatString="{0:dddd}" />
                                            <asp:BoundField DataField="PUNCH_OUT" HeaderText="Punch-Out" />
                                            <%--<asp:BoundField DataField="" HeaderText="" />--%>

                                            <asp:TemplateField HeaderText="Punch-In">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpunchin" runat="server" Text='<%#Eval("PUNCH_IN") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfoottext" runat="server" Text="Total Break Hrs :" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Break Hrs">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbrk" runat="server" Text='<%#Eval("TOTAL_HOURS") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotal" runat="server" Font-Bold="true"></asp:Label>
                                                </FooterTemplate>

                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle ForeColor="#6c758d" />
                                    </asp:GridView>
                                </asp:Panel>

                            </div>
                        </div>
                    </div>
                </div>




            </div>

            <br />
            <div runat="server" id="DivSummary" visible="false">
                <div runat="server" id="SummaryRpt">
                    <h5 class="header-title">Summary Report</h5>
                    <asp:Label ID="LblPunchInOutReport" runat="server" CssClass="lblValidation"></asp:Label>

                    <div id="DivReport" runat="server">
                        <div class="row">
                            <div class="col-sm-5">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-4 htCr" style="width: 130px;">Total Days&nbsp;<b>:</b></div>

                                        <div class="col-sm-6">
                                            <asp:Label ID="LblTotalDays" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4 htCr" style="width: 130px;">Total WeekOffs&nbsp;<b>:</b></div>

                                        <div class="col-sm-6">
                                            <asp:Label ID="LblTWeekOffs" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4 htCr" style="width: 130px;">Total Holidays&nbsp;<b>:</b></div>

                                        <div class="col-sm-6">
                                            <asp:Label ID="LblTHolidays" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-5">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-sm-4 htCr" style="width: 150px;">Total Working Days&nbsp;<b>:</b></div>

                                        <div class="col-sm-6">
                                            <asp:Label ID="LblTWorkingDays" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4 htCr" style="width: 150px;">Hours/Day&nbsp;<b>:</b></div>

                                        <div class="col-sm-6">
                                            <asp:Label ID="LblTHoursDay" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-4 htCr" style="width: 150px;">Total Hours&nbsp;<b>:</b></div>

                                        <div class="col-sm-6">
                                            <asp:Label ID="LblTotalHours" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row " style="margin-bottom: 5px">
                        <div class="col-sm-12 text-right">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="BtnExptoExclPunchInOutReport" runat="server" Text="Export" OnClick="BtnExptoExclPunchInOutReport_Click" Visible="false" /><div class="DivSpacer01"></div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="BtnExptoExclPunchInOutReport" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="respovrflw">
                        <asp:GridView runat="server" ID="PunchInOutReport" AutoGenerateColumns="false" Visible="false">
                            <Columns>
                                <asp:BoundField DataField="ROWNUMBERREPORT" HeaderText="Sl No." ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="PERNR" HeaderText="Emp ID" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="Name" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="DATEOFJOINING" HeaderText="Date of Joining" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="LEAVECOUNTD" HeaderText="Leaves count" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="REGULARATTDNCE" HeaderText="Regular Attendance" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="TWORKINGDAYS" HeaderText="Total Working Days" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="TOTALHRS" HeaderText="Total Hours" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="TOLHRSBYEMP" HeaderText="Total Hours by Emp" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="MISSEDPUNCH" HeaderText="Missed Punch" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="AVGAVAILABILITY" HeaderText="Average Availability" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <div class="row col-md-12" id="DivPunchInOutReport" runat="server">
                            <div class="col-md-3" style="margin-top: 5px" id="divcnt" runat="server"></div>
                            <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                <asp:Repeater ID="RepeaterPunchInOutReport" runat="server">
                                    <ItemTemplate>
                                        <ul class="pagination pagination-rounded" style="display: inline-block">
                                            <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="lnkPagePunchInOutReport_Click" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>


</asp:Content>
