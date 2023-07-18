<%@ Page Title="View Task Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false"
    CodeBehind="ViewPlannedActivities.aspx.cs" Inherits="iEmpPower.UI.Working_Time.ViewPlannedActivities" Culture="en-GB" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .Td06
        {
            color: #004080 !important;
            font-size: 13px !important;
            width: 190px !important;
            padding: 3px !important;
            text-align: justify !important;
            line-height: 18px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">

                <span class="HeadFontSize">&nbsp;View Task Details</span><br />

            </div>
        </div>
    </div>
    <div class="body">
        <div id="real_time_chart" class="dashboard-flot-chart">

            <div id="divbrdr" class="divfr">
                <div class="search-section">



                    <asp:Panel ID="pnlPlndActivities" runat="server">
                        <div>
                            <asp:UpdatePanel ID="pnlParent" runat="server">
                                <ContentTemplate>

                                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg" meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />
                                    <div>

                                        <div class="form-inline">
                                            <div class="form-group">
                                                <div class="col-sm-2 htCr"><span class="rcls">*</span>Display data for&nbsp;<b>:</b></div>
                                                <%--<td class="Td06">Display data for</td>--%>
                                                <%--<td class="Td07"><b>:</b> </td>--%>
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="DDLEmpList" runat="server" CssClass="txtDropDownwidth" TabIndex="1">
                                                    </asp:DropDownList>
                                                    <Ajx:CascadingDropDown ID="CDD_DDLEmpList" runat="server" Category="EmpList" EmptyText="- SELECT -" EmptyValue="0"
                                                        LoadingText="[LOADING Emplyoee List....]" PromptText="- SELECT -" PromptValue="0" TargetControlID="DDLEmpList"
                                                        ServicePath="~/WebService/Service.asmx" ServiceMethod="GetEmpTC">
                                                    </Ajx:CascadingDropDown>



                                                    <asp:RequiredFieldValidator ID="RFV_DDLEmpList" runat="server" ControlToValidate="DDLEmpList" Display="Dynamic" CssClass="lblValidation"
                                                        ErrorMessage="Please select data for type" ForeColor="Red" InitialValue="0" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-2 htCr"><span class="rcls">*</span>From&nbsp;<b>:</b></div>
                                                <%-- <td class="Td06">
                                                   From
                                                </td>
                                                <td class="Td07"><b>:</b> </td>--%>
                                                <div class="col-sm-6">
                                                    <%--<BDP:BDPLite ID="bdpTimeSheetFrom" runat="server" CssClass="bold">
                  <TextBoxStyle CssClass="textbox" />
            </BDP:BDPLite>--%>


                                                    <asp:TextBox ID="TxtFromDate" runat="server" CssClass="txtDropDownwidth" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'"
                                                        ValidationGroup="vg1" TabIndex="2"></asp:TextBox>
                                                    <Ajx:MaskedEditExtender ID="MEE_TxtFromDate" runat="server" AcceptNegative="Left"
                                                        CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                        MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                        OnInvalidCssClass="MaskedEditError" TargetControlID="TxtFromDate" />
                                                    <Ajx:CalendarExtender ID="CE_TxtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        TargetControlID="TxtFromDate">
                                                    </Ajx:CalendarExtender>







                                                    <asp:RequiredFieldValidator ID="RFV_TxtFromDate" runat="server" ControlToValidate="TxtFromDate" ValidationGroup="vg1"
                                                        CssClass="lblValidation" Display="Dynamic" ErrorMessage="Please select from date" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REV_TxtFromDate" runat="server" Display="Dynamic" CssClass="lblValidation"
                                                        ErrorMessage="Invalid Date" ControlToValidate="TxtFromDate" ValidationGroup="vg1"
                                                        SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>

                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-sm-2 htCr"><span class="rcls">*</span>To&nbsp;<b>:</b></div>
                                                <%--<td class="Td06">To</td>
                                                <td class="Td07"><b>:</b> </td>--%>
                                                <div class="col-sm-6">
                                                    <%-- <BDP:BDPLite ID="bdpTimeSheetTo" runat="server" CssClass="bold">
                                            <TextBoxStyle CssClass="textbox" />
                                        </BDP:BDPLite>--%>

                                                    <asp:TextBox ID="TxtToDate" runat="server" CssClass="txtDropDownwidth" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'"
                                                        ValidationGroup="vg1" TabIndex="3"></asp:TextBox>
                                                    <Ajx:MaskedEditExtender ID="MEE_TxtToDate" runat="server" AcceptNegative="Left"
                                                        CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                        MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                        OnInvalidCssClass="MaskedEditError" TargetControlID="TxtToDate" />
                                                    <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        TargetControlID="TxtToDate">
                                                    </Ajx:CalendarExtender>




                                                    <asp:RequiredFieldValidator ID="RFV_TxtToDate" runat="server" ControlToValidate="TxtToDate" ValidationGroup="vg1"
                                                        CssClass="lblValidation" Display="Dynamic" ErrorMessage="Please select to date" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CV_TxtToDate" runat="server" ControlToCompare="TxtFromDate" ValidationGroup="vg1"
                                                        CssClass="lblValidation" ControlToValidate="TxtToDate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                                                        Operator="GreaterThanEqual" Type="Date" ForeColor="Red"></asp:CompareValidator>
                                                    <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic"
                                                        CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtToDate" ValidationGroup="vg1"
                                                        SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>

                                                </div>

                                            </div>
                                            <%-- <tr>
                                                <td>&nbsp;</td>
                                            </tr>--%>
                                            <div class="btn-group-sm">

                                                <div class="col-sm-1" style="width:90px">
                                                    <asp:Button ID="btnDisplay" Width="80px" runat="server" Text="Display" ValidationGroup="vg1" CausesValidation="true"
                                                        OnClick="btnDisplay_Click" TabIndex="4" />
                                                </div>
                                                <div class="col-sm-1"  style="width:90px">
                                                    <asp:Button ID="BtnClear" Width="80px" runat="server" Text="Clear" CausesValidation="false"
                                                        OnClick="BtnClear_Click" TabIndex="5" />
                                                </div>
                                                <div class="col-sm-1"  style="width:90px">
                                                    <asp:Button ID="btnExcel"  Width="80px" runat="server" Text="Export" CausesValidation="false"
                                                        Visible="false" OnClick="btnExcel_Click" TabIndex="6" />
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <br />
                                    <div>

                                        <div class="respovrflw">
                                            <br />
                                            <asp:GridView ID="grdPlannedActivities" Width="99%" runat="server" AutoGenerateColumns="False" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager">
                                                <Columns>

                                                    <asp:BoundField DataField="EMPID" HeaderText="Employee ID" />
                                                    <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="MODULE" HeaderText="Modlue" />
                                                    <%-- <asp:BoundField DataField="TASKACTIVITY" HeaderText="Activity" />--%>
                                                    <asp:BoundField DataField="TASKWORKDATE" HeaderText="Working Dates" />
                                                    <asp:BoundField DataField="TASKWORK" HeaderText="Assigned Task" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField DataField="TASKHOURS" HeaderText="Actual Hours" />
                                                </Columns>
                                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </div>
                                    </div>


                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExcel" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
