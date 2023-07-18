<%@ Page Title="Edit Save Travel Claims" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="SavedTravelClaims.aspx.cs"
    Inherits="iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment.SavedTravelClaims" EnableEventValidation="false" Culture="en-GB" Theme="SkinFile" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #tblPRRQ td:nth-child(2), #tblPRRQ2 td:nth-child(2)
        {
            border-right-style: dashed;
            border-right-width: 2px;
            border-right-color: #dee2e6;
        }



        #tblPRRQ tr:first-child td:nth-child(2), #tblPRRQ tr:last-child td:nth-child(2), #tblPRRQ2 tr:first-child td:nth-child(2), #tblPRRQ2 tr:last-child td:nth-child(2)
        {
            border: none !important;
        }

        #tblPRRQ tr
        {
            width: 100%;
        }



        #tblPRRQ td:nth-child(odd)
        {
            width: 15%;
        }



        #tblPRRQ td:nth-child(even)
        {
            width: 35%;
        }

        .col-md-1
        {
            width: auto !important;
        }
    </style>
    <asp:UpdatePanel ID="UP" runat="server">

        <ContentTemplate>
            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box">
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="Travel_Requests.aspx">Travel Requests</a></li>
                                <li class="breadcrumb-item active">Edit Saved / Requested Travel Claims</li>
                            </ol>
                        </div>
                        <h4 class="page-title">Edit Saved / Requested Travel Claims&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                            <asp:Label ID="LblMsg" runat="server" CssClass="msgboard"></asp:Label>

                            <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1" Font-Size="Medium"></asp:Label>
                            <asp:Label ID="lblIndent" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1" Font-Size="Medium"></asp:Label>
                         </span></h4>
                    </div>
                </div>
            </div>
            <!-- end page title -->



            <div id="divSearch" runat="server" class="table-responsive card-box" visible="false">
                <h4>Travel Claims</h4>

                <div class="DivMsg">
                </div>

                <table>
                    <tr>
                        <td align="right">Select:</td>
                        <td>
                            <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="form-control-file" TabIndex="1">
                                <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Trip No" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Trip Type" Value="2"></asp:ListItem>

                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" TabIndex="2"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnsearch" runat="server" OnClick="btnsearch_Click" TabIndex="3" Text="Search" CssClass="btn btn-xs btn-secondary" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" TabIndex="4" Text="Clear" CssClass="btn btn-xs btn-secondary" />
                        </td>
                    </tr>


                    </tr>
                </table>

                <div id="gvtrips" runat="server" visible="false">
                    <%--                    <table>

                        <tr>
                            <td>--%>
                    <asp:GridView ID="GV_TravelReqUpdate" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None"
                        DataKeyNames="REINR,KZREA,KUNDE,ZORT1,ZLAND,DATV1,DATB1,WBS_ELEMT,SUM_ADVANC,ADDIT_AMNT,CURRENCY"
                        Width="100%" AllowPaging="True" AllowSorting="True" PageSize="1" OnPageIndexChanging="GV_TravelReqUpdate_PageIndexChanging"
                        OnRowCommand="GV_TravelReqUpdate_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Trip No">
                                <ItemTemplate>
                                    <%#Eval("REINR") %>
                                </ItemTemplate>
                                <ItemStyle Width="50px" VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Trip From & To">
                                <ItemTemplate>
                                    <ul class="UlCls01">
                                        <li class="Li01">
                                            <label class="Lbl01 W01">Trip Type </label>
                                            : <%# Eval("KZREA") %></li>
                                        <li class="Li01">
                                            <label class="Lbl01 W01">From </label>
                                            : <%# Eval("KUNDE") %></li>
                                        <li class="Li01">
                                            <label class="Lbl01 W01">To </label>
                                            : <%# Eval("ZORT1") %></li>
                                        <li class="Li01">
                                            <label class="Lbl01 W01">Country </label>
                                            : <%# Eval("ZLAND") %></li>
                                    </ul>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trip From & To Date">
                                <ItemTemplate>
                                    <ul class="UlCls01">
                                        <li class="Li01">
                                            <label class="Lbl01 W01">From Date </label>
                                            : <%# Eval("DATV1") %></li>
                                        <li class="Li01">
                                            <label class="Lbl01 W01">To Date </label>
                                            : <%# Eval("DATB1") %></li>
                                        <li class="Li01">
                                            <label class="Lbl01 W01">Project</label>
                                            : <%# Eval("WBS_ELEMT") %></li>
                                    </ul>
                                </ItemTemplate>

                                <ItemStyle Width="26%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Other Information">
                                <ItemTemplate>
                                    <ul class="UlCls01">
                                        <%-- <li class="Li01">
                                                <label class="Lbl01 W02">Additional Advance </label>
                                                : <%# Eval("ADDIT_AMNT") %></li>
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Total Advance </label>
                                                : <%# Eval("SUM_ADVANC") %></li>
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Currency </label>
                                                : <%# Eval("CURRENCY") %></li>--%>
                                        <li class="Li01">
                                            <label class="Lbl01 W02">Purpose </label>
                                            : <%# Eval("PURPOSE") %></li>
                                        <li class="Li01">
                                            <label class="Lbl01 W02">Settled </label>
                                            : <%# Eval("SETTLED") %></li>
                                    </ul>
                                </ItemTemplate>

                                <ItemStyle Width="24%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <ul class="UlCls01">
                                        <li class="Li01">
                                            <asp:LinkButton ID="GVLbtnView" runat="server" Text="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="View" CausesValidation="false"></asp:LinkButton>
                                        </li>
                                    </ul>
                                </ItemTemplate>

                                <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="HF_REINR" runat="server" />
                    <%-- </td>
                        </tr>
                    </table>--%>
                </div>

            </div>

            <div runat="server" visible="false">
                <asp:GridView ID="grdSavedTravelClaims" runat="server" AutoGenerateColumns="False" CssClass="gridview" Width="99%" OnRowCommand="grdSavedTravelClaims_RowCommand"
                    DataKeyNames="CID,ACTIVITY,RCURR,REINR,PRJID,WBS_ELEMT" AllowPaging="true" AllowSorting="true" OnSorting="grdSavedTravelClaims_Sorting" OnPageIndexChanging="grdSavedTravelClaims_PageIndexChanging" PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="CID" HeaderText="Claim Id" SortExpression="CID" />
                        <asp:BoundField DataField="REINR" HeaderText="Trip No" SortExpression="REINR" />


                        <asp:BoundField DataField="CREATED_BY" HeaderText="Employee ID" SortExpression="CREATED_BY" />
                        <asp:BoundField DataField="ENAME" HeaderText="Employee Name" SortExpression="ENAME" />

                        <asp:BoundField DataField="WBS_ELEMT" HeaderText="Project" SortExpression="WBS_ELEMT" />

                        <asp:BoundField DataField="ACTIVITY" HeaderText="Task" SortExpression="ACTIVITY" />
                        <%-- <asp:BoundField DataField="RE_AMT" HeaderText="Total Reimbursement Amount" SortExpression="RE_AMT" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"/>
                        --%>

                        <asp:TemplateField HeaderText="Total Reimbursement Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" SortExpression="RE_AMT">
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <ItemTemplate>

                                <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>
                                <%-- ( <%# Eval("WAERS") %>)--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RCURR" HeaderText="Reimbursement Currency" SortExpression="RCURR" />

                        <asp:BoundField DataField="CREATED_ON" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="CREATED_ON" />
                        <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnIExpenseView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>View</asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <!-- Tab Panel Start / -->
            <div class="col-xl-12 m-t-20">
                <div class="tab-content m-0 p-0">
                    <!-- Sub Tab Panel -->
                    <div class="card text-dark">
                        <div class="card-body border-bottom">
                            <div class="card-widgets">
                                <%--<a data-toggle="collapse" href="#cardCollpase7" role="button" aria-expanded="false" aria-controls="cardCollpase2" class="collapsed bg-warning btn-rounded">
                                        <i class="mdi mdi-minus font-20 text-white"></i></a>--%>
                                <asp:LinkButton runat="server" ID="Tab1" CssClass=" bg-warning btn-rounded">
                                    <i id="icollapse" runat="server" class="mdi mdi-minus font-20 text-white"></i>
                                </asp:LinkButton></li>
                                    <Ajx:CollapsiblePanelExtender ID="cpe" runat="Server"
                                        TargetControlID="pnlcardCollpase7"
                                        CollapsedSize="0"
                                        Collapsed="false"
                                        ExpandControlID="Tab1"
                                        CollapseControlID="Tab1"
                                        AutoCollapse="False"
                                        AutoExpand="False"
                                        TextLabelID="Label1"
                                        CollapsedText="Show Details..."
                                        ExpandedText="Hide Details"
                                        ImageControlID="Image1"
                                        ExpandedImage="~/images/collapse.jpg"
                                        CollapsedImage="~/images/expand.jpg"
                                        ExpandDirection="Vertical" />
                            </div>
                            <h4 class="card-title mb-0">Overview Summary</h4>
                        </div>
                        <div class="card-body mt-n2 alert-success">
                            <table class="table table-sm table-borderless font-16 mt-n2 mb-n2 mb-0 mt-0 text-dark">
                                <tbody>
                                   <tr>
                                        <td class="col-md-1"><b>Trip dates</b></td>
                                                <td class="col-md-1">
                                                    <asp:Literal ID="lttripdates" runat="server" Text="NA" /></td>

                                        <td class="col-md-1"><b>Claim ID</b></td>
                                        <td class="col-md-1">
                                            <asp:Literal ID="ltClaimID" runat="server" Text="NA" /></td>
                                        <td class="col-md-1"><b>Task</b></td>
                                        <td class="col-md-1">
                                            <asp:Literal ID="ltTask" runat="server" Text="NA" /></td>
                                        <%--<td><b>Project</b></td>
                                                <td>
                                                    <asp:Literal ID="ltProject" runat="server" Text="NA" /></td>--%>
                                        <td class="col-md-1"><b>Reimb. Amount</b></td>
                                        <td class="col-md-1">
                                            <asp:Literal ID="ltReimbAmt" runat="server" Text="NA" /></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>


                        <%-- <asp:Panel ID="PnlExpenseAdd" runat="server">--%>
                        <div id="PnlExpenseAdd1" runat="server" visible="false">
                            <asp:Panel ID="pnlcardCollpase7" runat="server">
                                <%--class='<%= state %>' runat="server"--%>
                                <div class="card-body">
                                    <%--  <br />
                <strong><span style="color: red">&nbsp;*</span>  are mandatory fields</strong><br />
                <br />--%>
                                    <div id="Div1" runat="server" class="table-responsive card-box">
                                        <table class="table table-borderless table_font_sm" id="tblPRRQ">
                                            <thead class="border-bottom">
                                                <tr class="border-bottom">
                                                    <th colspan="2">Travel Claim Request Form</th>
                                                    <th class="text-right" colspan="2"><code>*</code> are mandatory fields</th>
                                                </tr>
                                                <tr>
                                                    <%--<th colspan="4">Indentor : Shifaz K Mohammed</th>--%>
                                                </tr>
                                            </thead>
                                            <tr>
                                                <td>Task <code>*</code></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTask" runat="server" AutoPostBack="true" CssClass="form-control-file" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged" TabIndex="5" ValidationGroup="vg1">
                                                        <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Billable" Value="B"></asp:ListItem>
                                                        <asp:ListItem Text="Non-Billable" Value="NB"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFV_ddlTask" runat="server" ControlToValidate="ddlTask" ErrorMessage="*" ForeColor="Red" InitialValue="0" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>No. Of Days</td>
                                                <td>
                                                    <asp:TextBox ID="TxtNoOfDays" runat="server" AutoPostBack="true" CssClass="form-control-file" MaxLength="3" OnTextChanged="TxtNoOfDays_TextChanged" TabIndex="13" Text="1"></asp:TextBox>
                                                    <Ajx:FilteredTextBoxExtender ID="FTB_TxtNoOfDays" runat="server" FilterType="Numbers" TargetControlID="TxtNoOfDays">
                                                    </Ajx:FilteredTextBoxExtender>
                                                    <asp:RegularExpressionValidator ID="REVtxtNoofdays" runat="server" ControlToValidate="TxtNoOfDays" ErrorMessage="&gt; 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" ValidationGroup="VG2"></asp:RegularExpressionValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Reimbursement Currency<code>*</code></td>
                                                <td>
                                                    <asp:DropDownList ID="DDLReimbursementCurrency" runat="server" AutoPostBack="true" CssClass="form-control-file" Font-Size="12px" OnSelectedIndexChanged="DDLCurrency_SelectedIndexChanged" TabIndex="6">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFV_DDLReimbursementCurrency" runat="server" ControlToValidate="DDLReimbursementCurrency" ErrorMessage="*" ForeColor="Red" InitialValue="0" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>Country <code>*</code></td>
                                                <td>
                                                    <asp:DropDownList ID="DDLTrClaimCountry" runat="server" AutoPostBack="true" CssClass="form-control-file" OnSelectedIndexChanged="DDLTrClaimCountry_SelectedIndexChanged" TabIndex="14" ValidationGroup="vg2">
                                                    </asp:DropDownList>
                                                    <Ajx:ListSearchExtender ID="LSE_DDLTrClaimCountry" runat="server" IsSorted="true" PromptCssClass="PromptCSSClass" PromptPosition="Top" PromptText="Search Country" QueryPattern="Contains" TargetControlID="DDLTrClaimCountry">
                                                    </Ajx:ListSearchExtender>
                                                    <asp:RequiredFieldValidator ID="RFV_DDLTrClaimCountry" runat="server" ControlToValidate="DDLTrClaimCountry" ErrorMessage="*" ForeColor="Red" InitialValue="0" ValidationGroup="vg2"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Expense Date <code>*</code></td>
                                                <td>
                                                    <asp:TextBox ID="txtStartDate" runat="server" AutoPostBack="True" CssClass="form-control-file" OnTextChanged="txtStartDate_TextChanged" TabIndex="7" ValidationGroup="vg2"></asp:TextBox>
                                                    <Ajx:MaskedEditExtender ID="MEE_txtStartDate" runat="server" AcceptNegative="Left" CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999" MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="txtStartDate" />
                                                    <Ajx:CalendarExtender ID="CE_txtStartDate" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtStartDate">
                                                    </Ajx:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RFV_txtNoOfUnits" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="vg2"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RGV_txtStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" ErrorMessage="=Trip Dates" ForeColor="Red" MaximumValue="01/01/3019" MinimumValue="01/01/2015" Operator="LessThan" Type="Date" ValidationGroup="vg2"></asp:RangeValidator>
                                                </td>
                                                <td>Region</td>
                                                <td>
                                                    <asp:DropDownList ID="DDLRegion" runat="server" AutoPostBack="true" CssClass="form-control-file" Enabled="false" OnSelectedIndexChanged="DDLRegion_SelectedIndexChanged" TabIndex="15" ValidationGroup="vg2">
                                                    </asp:DropDownList>
                                                    <Ajx:ListSearchExtender ID="LSE_DDLRegion" runat="server" IsSorted="true" PromptCssClass="PromptCSSClass" PromptPosition="Top" PromptText="Search Region" QueryPattern="Contains" TargetControlID="DDLRegion">
                                                    </Ajx:ListSearchExtender>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>Expense Type <code>*</code></td>
                                                <td>
                                                    <asp:DropDownList ID="DDLExpenseType" runat="server" AutoPostBack="true" CssClass="form-control-file" Font-Size="12px" OnSelectedIndexChanged="DDLExpenseType_SelectedIndexChanged" TabIndex="8" ValidationGroup="vg2">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFV_ddlExpenseType" runat="server" ControlToValidate="DDLExpenseType" ErrorMessage="*" ForeColor="Red" InitialValue="0" ValidationGroup="vg2"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>Justification <code>*</code></td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtJustification" runat="server" CssClass="form-control-file" TabIndex="12" TextMode="MultiLine"></asp:TextBox>
                                                    <Ajx:FilteredTextBoxExtender ID="FTB_txtItemNote" runat="server" FilterMode="InvalidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" InvalidChars="'&amp;" TargetControlID="txtJustification">
                                                    </Ajx:FilteredTextBoxExtender>
                                                    <asp:HiddenField ID="HF_DeCurr" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Expenditure Currency <code>*</code></td>
                                                <td>
                                                    <asp:DropDownList ID="DDLExpenditureCurrency" runat="server" AutoPostBack="true" CssClass="form-control-file" Font-Size="12px" OnSelectedIndexChanged="ddlExpenditureCurrency_SelectedIndexChanged" TabIndex="9" ValidationGroup="vg2">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFV_DDLExpenditureCurrency" runat="server" ControlToValidate="DDLExpenditureCurrency" ErrorMessage="*" ForeColor="Red" InitialValue="0" ValidationGroup="vg2"></asp:RequiredFieldValidator>
                                                </td>

                                                <td>Daily Rate</td>
                                                <td>
                                                    <asp:Label ID="LblDailyRate" runat="server"></asp:Label>
                                                    <asp:Label ID="LblCurrency" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="HF_DailyRate" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Expenditure Amount <code>*</code></td>
                                                <td>
                                                    <asp:TextBox ID="txtExpenditureAmount" runat="server" AutoPostBack="true" CssClass="form-control-file" MaxLength="12" OnTextChanged="txtExpenditureAmount_TextChanged" TabIndex="10" ValidationGroup="vg2"></asp:TextBox>
                                                    <Ajx:FilteredTextBoxExtender ID="FTB_txtExpenditureAmount" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers" TargetControlID="txtExpenditureAmount" ValidChars=".">
                                                    </Ajx:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtExpenditureAmount" ErrorMessage="*" ForeColor="Red" ValidationGroup="vg2"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REVtxtExpenditureAmount" runat="server" ControlToValidate="txtExpenditureAmount" ErrorMessage="&gt; 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" ValidationGroup="vg2"></asp:RegularExpressionValidator>
                                                </td>
                                                <td>Deviation</td>
                                                <td>
                                                    <asp:Label ID="LblDeviation" runat="server"></asp:Label>
                                                    <asp:Label ID="LblCurrency1" runat="server"></asp:Label>
                                                    <asp:HiddenField ID="HF_Deviation" runat="server" />
                                                    <span id="Sp01" style="color: white;"></span></td>
                                                <td colspan="2"></td>

                                            </tr>
                                            <tr>
                                                <td>Exchange Rate</td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRate1" runat="server" AutoPostBack="true" CssClass="txteditwidth form-control-file" Enabled="False" MaxLength="11" OnTextChanged="txtExchangeRate1_TextChanged" TabIndex="11"></asp:TextBox>
                                                    <Ajx:FilteredTextBoxExtender ID="FTB_txtExchangeRate1" runat="server" FilterType="Numbers, Custom" TargetControlID="txtExchangeRate1" ValidChars="-.">
                                                    </Ajx:FilteredTextBoxExtender>
                                                    <asp:CheckBox ID="cbEdit" runat="server" AutoPostBack="True" CssClass="checkbox checkbox-info" OnCheckedChanged="cbEdit_CheckedChanged" Text="Edit" />
                                                    <asp:Label ID="txtExchangeRate" runat="server" Enabled="false" ForeColor="White"></asp:Label>
                                                    <asp:CompareValidator ID="CV_txtExchangeRate1" runat="server" ControlToValidate="txtExchangeRate1" Display="Dynamic" ErrorMessage="!0" ForeColor="Red" Operator="NotEqual" Type="Double" ValidationGroup="vg2" ValueToCompare="0"></asp:CompareValidator>
                                                </td>

                                                <td>Reimbursable Amount </td>
                                                <td class="fa-2x text-dark">
                                                    <asp:Label ID="LblReimbursableAmount" runat="server" Enabled="false"></asp:Label>
                                                    <asp:Label ID="LblReimbursableCurrency" runat="server" Enabled="false"></asp:Label>
                                                    <asp:HiddenField ID="HF_ReimbursAmnt" runat="server" />
                                                </td>
                                            </tr>
                                            <tr class="border-bottom">
                                                <td>Attachments</td>
                                                <td>
                                                    <asp:FileUpload ID="fuAttachments" runat="server" class="form-control-file" TabIndex="17" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="cb" runat="server" TabIndex="16" Text="Original Receipt Missing" Visible="false" />
                                                </td>
                                                <td>
                                                    <asp:Label ID="fuAttachmentsfname" runat="server"></asp:Label>
                                                    ( Note: Please add all the details, before adding any associated attachements.)</td>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" OnClick="btnAdd_Click" OnClientClick="return Validate()" TabIndex="18" Text="Add New Item" />
                                                    <asp:Button ID="btnUpdateItems" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" OnClick="btnUpdateItems_Click" OnClientClick="return Validate()" TabIndex="19" Text="Update" Visible="false" />
                                                </td>
                                                <td colspan="3">
                                                    <div class="alert alert-warning float-left ml-2 small" role="alert" id="dvlialert" runat="server" visible="false">
                                                        <i class="mdi mdi-alert-circle-outline mr-2"></i><b>In order to create, you have to add at least one line item</b>
                                                    </div>
                                                </td>
                                                <%-- <td></td>--%>
                                            </tr>
                                        </table>

                                    </div>
                            </asp:Panel>
                        </div>
                    </div>
                    <%-- <div class="mb-3">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" OnClick="btnAdd_Click" OnClientClick="return Validate()" TabIndex="18" Text="Add New Item" />
                        <asp:Button ID="btnUpdateItems" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" OnClick="btnUpdateItems_Click" OnClientClick="return Validate()" TabIndex="19" Text="Update" Visible="false" />
                    </div>
                    <div class="alert alert-warning float-left ml-2 small" role="alert">
                        <i class="mdi mdi-alert-circle-outline mr-2"></i><b>In order to create, you have to add at least one line item</b>
                    </div>
                    <div class="clearfix"></div>--%>
                    <div>
                        <div id="Div2" runat="server" class="table-responsive card-box">
                            <asp:GridView ID="GV_TravelExpReq" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%"
                                DataKeyNames="LID,EXP_TYPE,S_DATE,EXPT_AMT,EXPT_CURR,EXC_RATE,RE_AMT,RCURR,DAILY_RATE,DEVIATION_AMT,DEVIATION_CURR,JUSTIFY,RECEIPT_FILE,CountryID,RegoinID,EXPID,NO_DAYS,RECEIPT_FID,RECEIPT_FPATH,DAILY_CURR"
                                TabIndex="15" ShowFooter="True" FooterStyle-CssClass="foo01"
                                OnRowCommand="GV_TravelExpReq_RowCommand" OnRowDeleting="GV_TravelExpReq_RowDeleting" OnRowEditing="GV_TravelExpReq_RowEditing">
                                <Columns>
                                    <%--   <asp:BoundField DataField="CID" HeaderText="Travel Claim ID" />--%>
                                    <asp:BoundField DataField="LID" HeaderText="ID" />
                                    <asp:BoundField DataField="EXP_TYPE" HeaderText="Expense Type" />
                                    <asp:BoundField DataField="S_DATE" HeaderText="Expense Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                    <%--  <asp:BoundField DataField="NO_DAYS" HeaderText="No. of Days" />--%>
                                    <%--<asp:BoundField DataField="EXPT_AMT" HeaderText="Expenditure Amount" />
            <asp:BoundField DataField="EXPT_CURR" HeaderText="Expenditure Currency" />
                                    --%>
                                    <asp:TemplateField HeaderText="Expenditure Amount" ControlStyle-CssClass="right" ItemStyle-CssClass="right">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%--  <%# Eval("EXPT_AMT") %> ( <%# Eval("EXPT_CURR") %>)--%>
                                            <%# Math.Round(decimal.Parse(Eval("EXPT_AMT").ToString()),3).ToString("0.000") %> ( <%# Eval("EXPT_CURR") %>)
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />
                                    <%--<asp:BoundField DataField="RE_AMT" HeaderText="Reimbursable Amount" />--%>
                                    <asp:TemplateField HeaderText="Reimbursable Amount" ControlStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%# Math.Round(decimal.Parse(Eval("RE_AMT").ToString()),3).ToString("0.000") %> 
                            ( <%# Eval("RCURR") %>)
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Daily Rate" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%# Eval("DAILY_RATE") %>  <%--( <%# Eval("RCURR") %>)--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Deviation Amount" ControlStyle-CssClass="right" ItemStyle-CssClass="right">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%--  <%# Eval("DEVIATION_AMT") %>  ( <%# Eval("DEVIATION_CURR") %>)--%>
                                            <%#   (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Eval("DEVIATION_AMT") %>  <%# (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Eval("DEVIATION_CURR") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="JUSTIFY" HeaderText="Justification" />
                                    <asp:BoundField DataField="NO_DAYS" HeaderText="No of Days" />

                                    <asp:BoundField DataField="RECEIPT_FILE" HeaderText="Original Receipt Missing"  Visible="false" />
                                    <%--  <asp:BoundField DataField="RECEIPT_FPATH" HeaderText="Attachments" />--%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Attachments
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" /><br />
                                            <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete File" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                CommandName="DeleteFile" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle Width="100" />

                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnEditExp" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light" CausesValidation="false" Text="Edit"
                                                Font-Bold="false" Style="font-size: 11px; text-decoration: none;" CommandName="EDITITEMS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle Width="100" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnDeleteExp" runat="server" CssClass="btn btn-danger btn-xs waves-effect waves-light" CausesValidation="false" Text="Delete" OnClientClick="javascript:return confirm('Do you want to delete this item?')"
                                                Font-Bold="false" Style="font-size: 11px; text-decoration: none;" CommandName="DELETE" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle Width="100" />
                                    </asp:TemplateField>

                                </Columns>

                                <FooterStyle CssClass="foo01" ForeColor="Black"></FooterStyle>
                            </asp:GridView>
                        </div>
                        <div class="mb-3">
                            <asp:Button ID="BtnSave" runat="server" CausesValidation="false" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" OnClick="BtnSave_Click" OnClientClick="return ValidateSubmit()" TabIndex="20" Text="Save" Width="160px" />
                            <asp:Button ID="BtnSubmit" runat="server" CausesValidation="false" CssClass="btn bg-brand-btn waves-effect waves-light" OnClick="BtnSubmit_Click" OnClientClick="return ValidateSubmit()" TabIndex="21" Text="Send for approval" Width="160px" />
                        </div>


                    </div>
                    </asp:Panel>


            <%--<script type = "text/javascript">
        var ddlText, ddlValue, ddl, lblMesg;
        var ddlTextRC, ddlValueRC, ddlRC;
        var ddlTextEC, ddlValueEC, ddlEC;
        var ddlTextCountry, ddlValueddlTextCountry, ddlCountry;
        var ddlTextRegion, ddlValueddlTextRegion, ddlRegion;
        function CacheItems() {
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=DDLExpenseType.ClientID %>");


            //  lblMesg = document.getElementById("<%=lblIndent.ClientID%>");
            for (var i = 0; i < ddl.options.length; i++) {
                ddlText[ddlText.length] = ddl.options[i].text;
                ddlValue[ddlValue.length] = ddl.options[i].value;
            }



        }
        window.onload = CacheItems;

        function FilterItems(value) {
            ddl.options.length = 0;
            for (var i = 0; i < ddlText.length; i++) {
                if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                    AddItem(ddlText[i], ddlValue[i]);
                }
            }
            lblMesg.innerHTML = ddl.options.length + " items found.";
            if (ddl.options.length == 0) {
                AddItem("No items found.", "");
            }
        }
        //function FilterItemsRC(value) {
        //    ddlRC.options.length = 0;
        //    for (var i = 0; i < ddlTextRC.length; i++) {
        //        if (ddlTextRC[i].toLowerCase().indexOf(value) != -1) {
        //            AddItemRC(ddlTextRC[i], ddlValueRC[i]);
        //        }
        //    }
        //    lblMesg.innerHTML = ddlRC.options.length + " items found.";
        //    if (ddlRC.options.length == 0) {
        //        AddItemRC("No items found.", "");
        //    }
        //}

        //function FilterItemsEC(value) {
        //    ddlEC.options.length = 0;
        //    for (var i = 0; i < ddlTextEC.length; i++) {
        //        if (ddlTextEC[i].toLowerCase().indexOf(value) != -1) {
        //            AddItemEC(ddlTextEC[i], ddlValueEC[i]);
        //        }
        //    }
        //    lblMesg.innerHTML = ddlEC.options.length + " items found.";
        //    if (ddlEC.options.length == 0) {
        //        AddItemEC("No items found.", "");
        //    }
        //}
        //function FilterItemsCountry(value) {
        //    ddlCountry.options.length = 0;
        //    for (var i = 0; i < ddlTextCountry.length; i++) {
        //        if (ddlTextCountry[i].toLowerCase().indexOf(value) != -1) {
        //            AddItemCountry(ddlTextCountry[i], ddlValueddlTextCountry[i]);
        //        }
        //    }
        //    lblMesg.innerHTML = ddlCountry.options.length + " items found.";
        //    if (ddlCountry.options.length == 0) {
        //        AddItemCountry("No items found.", "");
        //    }
        //}

        //function FilterItemsRegion(value) {
        //    ddlRegion.options.length = 0;
        //    for (var i = 0; i < ddlTextRegion.length; i++) {
        //        if (ddlTextRegion[i].toLowerCase().indexOf(value) != -1) {
        //            AddItemRegion(ddlTextRegion[i], ddlValueddlTextRegion[i]);
        //        }
        //    }
        //    lblMesg.innerHTML = ddlRegion.options.length + " items found.";
        //    if (ddlRegion.options.length == 0) {
        //        AddItemRegion("No items found.", "");
        //    }

        //}

        function AddItem(text, value) {
            var opt = document.createElement("option");
            opt.text = text;
            opt.value = value;
            ddl.options.add(opt);
        }

        //function AddItemRC(text, value) {
        //    var optRC = document.createElement("option");
        //    optRC.text = text;
        //    optRC.value = value;
        //    ddlRC.options.add(optRC);
        //}


        //function AddItemEC(text, value) {
        //    var optEC = document.createElement("option");
        //    optEC.text = text;
        //    optEC.value = value;
        //    ddlEC.options.add(optEC);
        //}


        //function AddItemCountry(text, value) {
        //    var optCntry = document.createElement("option");
        //    optCntry.text = text;
        //    optCntry.value = value;
        //    ddlCountry.options.add(optCntry);
        //}


        //function AddItemRegion(text, value) {
        //    var optRegion = document.createElement("option");
        //    optRegion.text = text;
        //    optRegion.value = value;
        //    ddlRegion.options.add(optRegion);
        //}
</script>--%>

                    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
                    <script type="text/javascript" src="../../Old_App_Code/Scripts/jquery-1.8.3.min.js"></script>
                    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                    <%--<script src="../../Scripts/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>--%>
                    <script type="text/javascript">

                        function Validate() {
                            var isValid = false;
                            isValid = Page_ClientValidate('vg1');
                            if (isValid) {
                                isValid = Page_ClientValidate('vg2');
                            }
                            return isValid;
                        }

                        function ValidateSubmit() {
                            var isValid = false;
                            isValid = Page_ClientValidate('vg1');

                            return isValid;
                        }


                        //$(document).ready(function () {
                        //    $("select").searchable();
                        //});
                    </script>

                    <%--<script type="text/javascript">

        //----------- OTHER QUERY --------
        $('#<%=cb.ClientID%>').click(function () {
            if (this.checked)
                //alert('true');
                $("#<%=fuAttachments.ClientID%>").attr('disabled', 'display:none');
            else
                //alert('false');
                $("#<%=fuAttachments.ClientID%>").removeAttr('disabled');
        });
        //--------------------------------

        //$(document).load(function () {
        var prm = Sys.WebForms.PageRequestManager.getInstance(); prm.add_endRequest(function () {
            $("[id*=TxtNoOfDays]").val('1');
        
        
        $("input[type='text'],select").on("change keydown keyup blur", function () {
            var ReimbCurr = $.trim($("[id*=DDLReimbursementCurrency]").val());
            var ExpCurr = $.trim($("[id*=DDLExpenditureCurrency]").val());
            var ExpType = $.trim($("[id*=DDLExpenseType]").val());
            //var ExpAmount = parseFloat($.trim($("[id*=txtExpenditureAmount]").val()));
            var ExpAmount = parseFloat(isNaN($.trim($("[id*=txtExpenditureAmount]").val())) || $.trim($("[id*=txtExpenditureAmount]").val() == '' ? '0' : $.trim($("[id*=txtExpenditureAmount]").val())));
            var ExchangeRate = parseFloat($.trim($("[id*=txtExchangeRate]").val()));
            var CountryID = $.trim($("[id*=DDLTrClaimCountry]").val());
            var RegionID = $.trim($("[id*=DDLRegion]").val());
            var NoOfDays = $.trim($("[id*=TxtNoOfDays]").val()) == '' ? '0' : $("[id*=TxtNoOfDays]").val();


            //$("[id*=txtExchangeRate]").val(response.d);
            if (ExpCurr != '0' && ReimbCurr != '0') {
                var ExchRate1 = GetExchangeRate(ExpCurr, ReimbCurr);
                $("[id*=txtExchangeRate]").val(ExchRate1);
                //$("[id*=Sp01]").html(ReimbCurr + ' *' + ExpCurr + ' * ' + ExchRate1);


                //------- REIMBURSABLE AMOUNT DISPLAY -------------- START --------
                if (!isNaN(ExpAmount) && !isNaN(ExchRate1)) {
                    $("#<%= LblReimbursableAmount.ClientID %>").html(Math.abs(ExchRate1 < 0 ? ExpAmount / ExchRate1 : ExpAmount * ExchRate1).toFixed(2));
                    $("#<%= HF_ReimbursAmnt.ClientID %>").val(Math.abs(ExchRate1 < 0 ? ExpAmount / ExchRate1 : ExpAmount * ExchRate1));
                    $("#<%= LblReimbursableCurrency.ClientID %>").html(ReimbCurr);
                }
                else {
                    $("#<%= LblReimbursableAmount.ClientID %>").html('0.0');
                    $("#<%= HF_ReimbursAmnt.ClientID %>").val(Math.abs(ExchRate1 < 0 ? ExpAmount / ExchRate1 : ExpAmount * ExchRate1));
                    $("#<%= LblReimbursableCurrency.ClientID %>").html(ReimbCurr);
                }
                //------- REIMBURSABLE AMOUNT DISPLAY -------------- END --------

                //------- DAILY RATE DISPLAY -------------- START --------
                var DailyRateValue = GetDailyRate(CountryID, RegionID, ExpType);
                if (DailyRateValue.indexOf("~") >= 0) {
                    var DailyRate = DailyRateValue.split('~');
                    var DailyRate1 = parseFloat(isNaN(DailyRate[0]) || $.trim(DailyRate[0]) == '' ? '0.0' : (DailyRate[0] * $.trim(NoOfDays)));
                    $("[id*=LblDailyRate]").html(DailyRate1);
                    $("#<%= HF_DailyRate.ClientID %>").val(DailyRate1);
                    $("[id*=LblCurrency], [id*=LblCurrency1]").html(DailyRate[1]);
                    $("#<%= HF_DeCurr.ClientID %>").val(DailyRate[1]);

                    //------- DEVIATION AMOUNT CALCULATION -------
                    if ($.trim(DailyRate[1]) == $.trim(ExpCurr)) {
                        if (DailyRate1 > ExpAmount) {
                            $("[id*=LblDeviation]").html(0);
                            $("#<%= HF_Deviation.ClientID %>").val(0);

                        }
                        else {
                            $("[id*=LblDeviation]").html(ExpAmount - DailyRate1);
                            $("#<%= HF_Deviation.ClientID %>").val(ExpAmount - DailyRate1);
                        }
                    }
                    else {
                        var ExchRate2 = GetExchangeRate($.trim(ExpCurr), $.trim(DailyRate[1]));
                        if (DailyRate1 > (ExchRate2 * ExpAmount)) {
                            $("[id*=LblDeviation]").html(0);
                            $("#<%= HF_Deviation.ClientID %>").val(0);
                        }
                        else {
                            $("[id*=LblDeviation]").html((ExchRate2 * ExpAmount) - DailyRate1);
                            $("#<%= HF_Deviation.ClientID %>").val((ExchRate2 * ExpAmount) - DailyRate1);
                        }

                        $("[id*=Sp01]").html($.trim(ExpCurr) + ' *' + $.trim(DailyRate[1]) + ' * ' + ExchRate2 + ' * ' + ExchRate2 * ExpAmount);
                    }
                }
                else {
                    $("[id*=LblDeviation], [id*=LblDailyRate]").html('0');
                    $("#<%= HF_Deviation.ClientID %>").val(0);
                    $("#<%= HF_DailyRate.ClientID %>").val(0);
                    //$("[id*=Sp01]").html(ExchRate2);
                }
                //$("[id*=Sp01]").html(ReimbCurr + ' *' + ExpCurr + ' * ' + ExchRate1 + ' * ' + DailyRateValue + ' * ' + DailyRate[0] + ' * ' + DailyRate[1] + ' * ' + DailyRate1 + ' * ' + ExpAmount + ' * ' + NoOfDays);
                //------- DAILY RATE DISPLAY -------------- END ----------
            }
        });




        //--------------- EXCHANGE RATE FUNCTION ---------------------------

        function GetExchangeRate(ExpCurr, RembCurr) {
            var ExcRate;
            $.ajax({
                type: "POST",
                url: "/WebService/Service.asmx/GetExchangeRate",
                data: '{ExpenditureCurrency: "' + ExpCurr + '",Currency: "' + RembCurr + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                global: false,
                async: false,
                success: function (response) {
                    ExcRate = response.d;
                },
                failure: function (response) {
                    ExcRate = 0;
                },
                error: function (response) {
                    ExcRate = 0;
                }
            });
            return ExcRate
        }


        function GetDailyRate(CountryID, RegionID, ExpenseType) {
            var DailyRate;
            $.ajax({
                type: "POST",
                url: "/WebService/Service.asmx/GetExcRate",
                data: '{CountryID: "' + CountryID + '",Region: "' + RegionID + '", ExpenseType: "' + ExpenseType + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                global: false,
                async: false,
                success: function (response) {
                    DailyRate = response.d;
                },
                failure: function (response) {
                    DailyRate = 0;
                },
                error: function (response) {
                    DailyRate = 0;
                }
            });
            return DailyRate;
        }
       
        });
    </script>--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />

            <asp:PostBackTrigger ControlID="btnUpdateItems" />
            <asp:PostBackTrigger ControlID="BtnSave" />
            <asp:PostBackTrigger ControlID="BtnSubmit" />
            <asp:PostBackTrigger ControlID="GV_TravelExpReq" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
