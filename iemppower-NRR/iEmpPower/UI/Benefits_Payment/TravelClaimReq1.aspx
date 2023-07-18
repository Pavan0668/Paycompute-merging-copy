<%@ Page Title="Travel Claim Request" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="TravelClaimReq1.aspx.cs"
    Inherits="iEmpPower.UI.Benefits_Payment.TravelClaimReq1" EnableEventValidation="false" Culture="en-GB" Theme="SkinFile" %>

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

        .card-box
        {
            padding-top: 5px !important;
            padding-bottom: 0px !important;
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
                                <li class="breadcrumb-item active">Travel Claim Request</li>
                            </ol>
                        </div>
                        <h4 class="page-title">Create Travel Claim Requisition&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>
                        <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1" Font-Size="Medium"></asp:Label>
                        <asp:Label ID="LblMsg" runat="server" CssClass="msgboard" Font-Size="Medium"></asp:Label>
                        </span></h4>

                        <asp:LinkButton runat="server" ID="lbtAddNew" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right" Visible="false" OnClick="lbtAddNew_Click"><i class="mdi mdi-plus"></i>
Add New Travel Claim Request </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lbtnEdit" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right" Visible="false" OnClick="lbtnEdit_Click"><i class="mdi mdi-pencil"></i>
Edit same Travel Claim Request </asp:LinkButton>
                    </div>
                </div>
            </div>
            <!-- end page title -->
            <div class="tab-pane active" id="H-Tab-1">
                <div>
                    <%--class="table-responsive"--%>

                    <!-- Tab Panel Start / -->
                    <div id="divSearch" runat="server" class="table-responsive card-box" visible="false">
                        <%--<h2>Travel Claim Request</h2>--%>
                        <div class="DivMsg">
                            <%--  <asp:Label ID="LblMsg" runat="server" CssClass="msgboard"></asp:Label>--%>
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
                                    <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-xs btn-secondary" OnClick="btnsearch_Click" TabIndex="3" Text="Search" />
                                    &nbsp;&nbsp;
                                <asp:Button ID="btnclear" runat="server" CssClass="btn btn-xs btn-secondary" OnClick="btnclear_Click" TabIndex="4" Text="Clear" />
                                </td>
                            </tr>
                        </table>

                        <div runat="server" visible="false">
                            <%--                        <table class="table table-striped table-sm mb-0 table_font_sm">
                            <tr>
                                <td>--%>
                            <asp:GridView ID="GV_TravelReqUpdate" runat="server" AllowPaging="True" AllowSorting="True" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" DataKeyNames="REINR,KZREA,KUNDE,ZORT1,ZLAND,DATV1,DATB1,WBS_ELEMT,SUM_ADVANC,ADDIT_AMNT,CURRENCY" OnPageIndexChanging="GV_TravelReqUpdate_PageIndexChanging" OnRowCommand="GV_TravelReqUpdate_RowCommand" PageSize="1" TabIndex="5">
                                <Columns>
                                    <asp:TemplateField HeaderText="Trip No">
                                        <ItemTemplate>
                                            <%#Eval("REINR") %>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trip From &amp; To">
                                        <ItemTemplate>
                                            <ul class="UlCls01">
                                                <li class="Li01">
                                                    <label class="Lbl01 W01">
                                                        Trip Type
                                                    </label>
                                                    : <%# Eval("KZREA") %></li>
                                                <li class="Li01">
                                                    <label class="Lbl01 W01">
                                                        From
                                                    </label>
                                                    : <%# Eval("KUNDE") %></li>
                                                <li class="Li01">
                                                    <label class="Lbl01 W01">
                                                        To
                                                    </label>
                                                    : <%# Eval("ZORT1") %></li>
                                                <li class="Li01">
                                                    <label class="Lbl01 W01">
                                                        Country
                                                    </label>
                                                    : <%# Eval("ZLAND") %></li>
                                            </ul>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trip From &amp; To Date">
                                        <ItemTemplate>
                                            <ul class="UlCls01">
                                                <li class="Li01">
                                                    <label class="Lbl01 W01">
                                                        From Date
                                                    </label>
                                                    : <%# Eval("DATV1") %></li>
                                                <li class="Li01">
                                                    <label class="Lbl01 W01">
                                                        To Date
                                                    </label>
                                                    : <%# Eval("DATB1") %></li>
                                                <li class="Li01">
                                                    <label class="Lbl01 W01">
                                                        Project</label>
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
                                                    <label class="Lbl01 W02">
                                                        Purpose
                                                    </label>
                                                    : <%# Eval("PURPOSE") %></li>
                                                <li class="Li01">
                                                    <label class="Lbl01 W02">
                                                        Settled
                                                    </label>
                                                    : <%# Eval("SETTLED") %></li>
                                            </ul>
                                        </ItemTemplate>
                                        <ItemStyle Width="24%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <ul class="UlCls01">
                                                <li class="Li01">
                                                    <asp:LinkButton ID="GVLbtnView" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="View" Text="View"></asp:LinkButton>
                                                </li>
                                            </ul>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="4%" />
                                    </asp:TemplateField>
                                </Columns>
                                <%--<SelectedRowStyle BackColor="#000DDD" CssClass="gvselected" />--%>
                            </asp:GridView>
                            <asp:HiddenField ID="HF_REINR" runat="server" />
                            <%-- </td>
                            </tr>
                        </table>--%>
                        </div>

                    </div>
                    </tr>
                <tr>
                </tr>
                    </tbody>
                </table>
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
                                <%--<div class="card-box border-0" role="alert" id="dvlineitem" runat="server" visible="false">
                                    <div class="table-responsive">
                                        <table class="table table-sm table-borderless mb-0 table_font_sm" runat="server" visible="false">
                                            <body>
                                                <tr>
                                                    <td>
                                                        <h4>Travel Declaration Details</h4>
                                                        <asp:Label ID="lblIndent" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </body>
                                        </table>

                                        <asp:GridView ID="GV_TravelExpReq" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" DataKeyNames="ID" FooterStyle-CssClass="foo01" OnRowCommand="GV_TravelExpReq_RowCommand" OnRowDeleting="GV_TravelExpReq_RowDeleting" ShowFooter="true" TabIndex="15" Width="99%">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                <asp:BoundField DataField="EXP_TYPE_NAME" HeaderText="Expense Type" />

                                                <asp:BoundField DataField="S_DATE" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Expense Date" />

                                                <asp:TemplateField ControlStyle-CssClass="rightJustify" HeaderText="Expenditure Amount" ItemStyle-CssClass="rightJustify">
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <%# Math.Round(decimal.Parse(Eval("EXPT_AMT").ToString()),3).ToString("0.000") %>( <%# Eval("EXPT_CURR") %>)
                                                    </ItemTemplate>

                                                </asp:TemplateField>



                                                <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" />

                                                <asp:TemplateField ControlStyle-CssClass="rightJustify" HeaderText="Reimbursable Amount" ItemStyle-CssClass="rightJustify">
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <%# Math.Round(decimal.Parse(Eval("RE_AMT").ToString()),3).ToString("0.000") %>( <%# Eval("RCURR") %>)
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField ControlStyle-CssClass="rightJustify" HeaderText="Daily Rate" ItemStyle-CssClass="rightJustify">
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <%# Eval("DAILY_RATE") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ControlStyle-CssClass="rightJustify" HeaderText="Deviation Amount" ItemStyle-CssClass="rightJustify">
                                                    <EditItemTemplate>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <%#   (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Eval("DEVIATION_AMT") %><%# (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Eval("DEVIATION_CURR") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="JUSTIFY" HeaderText="Justification" />

                                                <asp:BoundField DataField="NO_DAYS" HeaderText="No of Days" />

                                                <asp:BoundField DataField="RECEIPT_FILE" HeaderText="Original Receipt Missing" Visible="false" />
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Attachments
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="Lbtndownload" runat="server" CausesValidation="false" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CommandName="download" Font-Bold="True" Text='<%#Eval("RECEIPT_FIID") %>' />
                                                        <br />
                                                        <asp:LinkButton ID="LbtnDelete" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DeleteFile" Text="Delete File" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FIID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbtnEditExp" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EDITITEMS" Font-Bold="false" Style="font-size: 11px; text-decoration: none;" Text="Edit" Visible="true"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LbtnDeleteExp" runat="server" CssClass="btn btn-danger btn-xs waves-effect waves-light" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DELETE" Font-Bold="false" OnClientClick="javascript:return confirm('Do you want to delete this item?')" Style="font-size: 11px; text-decoration: none;" Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="foo01" ForeColor="Black" />
                                        </asp:GridView>
                                        <div class="clearfix">
                                            &nbsp;
                                        </div>
                                    </div>
                                </div>--%>
                                <div id="PnlExpenseAdd" runat="server" visible="false">
                                    <%-- class="table-responsive card-box" --%>
                                    <asp:Panel ID="pnlcardCollpase7" runat="server">
                                        <%--class='<%= state %>' runat="server"--%>
                                        <div class="card-body">
                                            <table class="table table-borderless table_font_sm" id="tblPRRQ">
                                                <thead class="border-bottom" runat="server">
                                                    <tr class="border-bottom">
                                                        <th colspan="2">Add Line items</th>
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
                                                        <asp:TextBox ID="TxtNoOfDays" runat="server" AutoPostBack="true" CssClass="form-control-file" MaxLength="3" OnTextChanged="TxtNoOfDays_TextChanged" TabIndex="12" Text="1"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FTB_TxtNoOfDays" runat="server" FilterType="Numbers" TargetControlID="TxtNoOfDays">
                                                        </Ajx:FilteredTextBoxExtender>
                                                        <asp:RegularExpressionValidator ID="REVtxtNoofdays" runat="server" ControlToValidate="TxtNoOfDays" ErrorMessage="&gt; 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" ValidationGroup="VG2"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Reimbursement Curr.<code>*</code></td>
                                                    <td>
                                                        <asp:DropDownList ID="DDLReimbursementCurrency" runat="server" AutoPostBack="true" CssClass="form-control-file" Font-Size="12px" OnSelectedIndexChanged="DDLCurrency_SelectedIndexChanged" TabIndex="6">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RFV_DDLReimbursementCurrency" runat="server" ControlToValidate="DDLReimbursementCurrency" ErrorMessage="*" ForeColor="Red" InitialValue="0" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>Country <code>*</code></td>
                                                    <td>
                                                        <asp:DropDownList ID="DDLTrClaimCountry" runat="server" AutoPostBack="true" CssClass="form-control-file" OnSelectedIndexChanged="DDLTrClaimCountry_SelectedIndexChanged" TabIndex="13" ValidationGroup="vg2">
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
                                                        <asp:DropDownList ID="DDLRegion" runat="server" AutoPostBack="true" CssClass="form-control-file" Enabled="false" OnSelectedIndexChanged="DDLRegion_SelectedIndexChanged" TabIndex="14" ValidationGroup="vg2">
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
                                                    <td>
                                                        <asp:TextBox ID="txtJustification" runat="server" CssClass="form-control-file" TabIndex="15" TextMode="MultiLine"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FTB_txtItemNote" runat="server" FilterMode="InvalidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" InvalidChars="'&amp;" TargetControlID="txtJustification">
                                                        </Ajx:FilteredTextBoxExtender>
                                                        <asp:HiddenField ID="HF_DeCurr" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Expenditure Currency<code>*</code></td>
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
                                                        <%-- <span id="Sp01" style="color: white;"></span>--%>
                                                    </td>
                                                </tr>
                                                <tr class="border-bottom">
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
                                                    <td colspan="2">
                                                        <asp:Label ID="fuAttachmentsfname" runat="server"></asp:Label>
                                                        ( Note: Please add all the details, before adding any associated attachements.)
                                                        <asp:CheckBox ID="cb" runat="server" TabIndex="16" Text="Original Receipt Missing" Visible="false" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" OnClick="btnAdd_Click" OnClientClick="return Validate()" TabIndex="18" Text="Add New Item" />
                                                        <asp:Button ID="btnUpdateItems" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" OnClick="btnUpdateItems_Click" OnClientClick="return Validate()" TabIndex="19" Text="Update" Visible="false" />
                                                    </td>
                                                    <td colspan="3">
                                                        <div class="alert alert-warning float-left ml-2 small" role="alert"><%-- id="dvlialert" runat="server" visible="false">--%>
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
                            <%--<div class="mb-3 float-left">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" OnClick="btnAdd_Click" OnClientClick="return Validate()" TabIndex="18" Text="Add New Item" />
                                <asp:Button ID="btnUpdateItems" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" OnClick="btnUpdateItems_Click" OnClientClick="return Validate()" TabIndex="19" Text="Update" Visible="false" />
                            </div>
                            <div class="alert alert-warning float-left ml-2 small" role="alert">
                                <i class="mdi mdi-alert-circle-outline mr-2"></i><b>In order to create, you have to add at least one line item</b>
                            </div>
                            <div class="clearfix"></div>--%>
                            <!-- ////// Purchase Request Declaration //// -->
                            <div class="card-box border-0" role="alert" id="dvlineitem" runat="server" visible="false">
                                <div class="table-responsive">
                                    <table id="Table1" class="table table-sm table-borderless mb-0 table_font_sm" runat="server" visible="false">
                                        <body>
                                            <tr>
                                                <td>
                                                    <h4>Travel Declaration Details</h4>
                                                    <asp:Label ID="lblIndent" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </body>
                                    </table>

                                    <asp:GridView ID="GV_TravelExpReq" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" DataKeyNames="ID" FooterStyle-CssClass="foo01" OnRowCommand="GV_TravelExpReq_RowCommand" OnRowDeleting="GV_TravelExpReq_RowDeleting" ShowFooter="true" TabIndex="15" Width="99%" >
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:BoundField DataField="EXP_TYPE_NAME" HeaderText="Expense Type" />

                                            <asp:BoundField DataField="S_DATE" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Expense Date" />

                                            <asp:TemplateField HeaderText="Expenditure Amount"  ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%# Math.Round(decimal.Parse(Eval("EXPT_AMT").ToString()),3).ToString("0.000") %>( <%# Eval("EXPT_CURR") %>)
                                                </ItemTemplate>

                                            </asp:TemplateField>



                                            <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" />

                                            <asp:TemplateField HeaderText="Reimbursable Amount"  ItemStyle-CssClass="right" HeaderStyle-CssClass="right" FooterStyle-CssClass="right">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%# Math.Round(decimal.Parse(Eval("RE_AMT").ToString()),3).ToString("0.000") %>( <%# Eval("RCURR") %>)
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField ControlStyle-CssClass="rightJustify" HeaderText="Daily Rate" ItemStyle-CssClass="rightJustify">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("DAILY_RATE") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Deviation Amount"  ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%#   (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Eval("DEVIATION_AMT") %><%# (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Eval("DEVIATION_CURR") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="JUSTIFY" HeaderText="Justification" />

                                            <asp:BoundField DataField="NO_DAYS" HeaderText="No of Days" />

                                            <asp:BoundField DataField="RECEIPT_FILE" HeaderText="Original Receipt Missing" Visible="false" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Attachments
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lbtndownload" runat="server" CausesValidation="false" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CommandName="download" Font-Bold="True" Text='<%#Eval("RECEIPT_FIID") %>' />
                                                    <br />
                                                    <asp:LinkButton ID="LbtnDelete" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DeleteFile" Text="Delete File" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FIID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="100" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnEditExp" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EDITITEMS" Font-Bold="false" Style="font-size: 11px; text-decoration: none;" Text="Edit" Visible="true"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="100" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnDeleteExp" runat="server" CssClass="btn btn-danger btn-xs waves-effect waves-light" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DELETE" Font-Bold="false" OnClientClick="javascript:return confirm('Do you want to delete this item?')" Style="font-size: 11px; text-decoration: none;" Text="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="100" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="foo01" ForeColor="Black" />
                                    </asp:GridView>
                                    <div class="clearfix">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>
                            <%--   <div class="card-box border-0" role="alert" id="dvlineitem" runat="server" visible="false">
                                <div class="table-responsive">
                                    <table class="table table-sm table-borderless mb-0 table_font_sm">
                                        <body>
                                            <tr>
                                                <td>
                                                    <h4>Travel Declaration Details</h4>
                                                    <asp:Label ID="lblIndent" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </body>
                                    </table>

                                    <asp:GridView ID="GV_TravelExpReq" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" DataKeyNames="ID" FooterStyle-CssClass="foo01" OnRowCommand="GV_TravelExpReq_RowCommand" OnRowDeleting="GV_TravelExpReq_RowDeleting" ShowFooter="true" TabIndex="15" Width="99%">
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                            <asp:BoundField DataField="EXP_TYPE_NAME" HeaderText="Expense Type" />
                                            
                                            <asp:BoundField DataField="S_DATE" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="Expense Date" /> 
                                            
                                            <asp:TemplateField ControlStyle-CssClass="rightJustify" HeaderText="Expenditure Amount" ItemStyle-CssClass="rightJustify">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                  <%# Math.Round(decimal.Parse(Eval("EXPT_AMT").ToString()),3).ToString("0.000") %>( <%# Eval("EXPT_CURR") %>)
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>

                                            

                                            <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" />
                                              
                                            <asp:TemplateField ControlStyle-CssClass="rightJustify" HeaderText="Reimbursable Amount" ItemStyle-CssClass="rightJustify">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%# Math.Round(decimal.Parse(Eval("RE_AMT").ToString()),3).ToString("0.000") %>( <%# Eval("RCURR") %>)
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                           
                                            <asp:TemplateField ControlStyle-CssClass="rightJustify" HeaderText="Daily Rate" ItemStyle-CssClass="rightJustify">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%# Eval("DAILY_RATE") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                            <asp:TemplateField ControlStyle-CssClass="rightJustify" HeaderText="Deviation Amount" ItemStyle-CssClass="rightJustify">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                   <%#   (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Eval("DEVIATION_AMT") %><%# (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Eval("DEVIATION_CURR") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="JUSTIFY" HeaderText="Justification" />
                                            
                                            <asp:BoundField DataField="NO_DAYS" HeaderText="No of Days" />
                                            
                                            <asp:BoundField DataField="RECEIPT_FILE" HeaderText="Original Receipt Missing" Visible="false"/>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Attachments
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lbtndownload" runat="server" CausesValidation="false" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CommandName="download" Font-Bold="True" Text='<%#Eval("RECEIPT_FIID") %>' />
                                                    <br />
                                                    <asp:LinkButton ID="LbtnDelete" runat="server" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DeleteFile" Text="Delete File" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FIID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="100" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnEditExp" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="EDITITEMS" Font-Bold="false" Style="font-size: 11px; text-decoration: none;" Text="Edit" Visible="true"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="100" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnDeleteExp" runat="server" CssClass="btn btn-danger btn-xs waves-effect waves-light" CausesValidation="false" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="DELETE" Font-Bold="false" OnClientClick="javascript:return confirm('Do you want to delete this item?')" Style="font-size: 11px; text-decoration: none;" Text="Delete"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="100" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="foo01" ForeColor="Black" />
                                    </asp:GridView>
                                    <div class="clearfix">
                                        &nbsp;
                                    </div>
                                </div>
                            </div>--%>
                            <div class="mb-3">
                                <asp:Button ID="BtnSave" runat="server" CausesValidation="false" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" OnClick="BtnSave_Click" OnClientClick="return ValidateSubmit()" TabIndex="20" Text="Save" Visible="False" Width="160px" />
                                <asp:Button ID="BtnSubmit" runat="server" CausesValidation="false" CssClass="btn bg-brand-btn waves-effect waves-light" OnClick="BtnSubmit_Click" OnClientClick="return ValidateSubmit()" TabIndex="21" Text="Send for approval" Visible="False" Width="160px" />
                            </div>
                        </div>
                        <!-- end Tab Panel-->
                    </div>
                    <!-- end row -->
                    <div>
                        <%-- id="PnlExpenseAdd" runat="server" visible="false">--%>
                        <div>
                            <br />
                        </div>
                    </div>
                </div>



            </div>



            <script type="text/javascript" src="../../Scripts/jquery-1.8.3.min.js"></script>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <%--  <script src="../../Scripts/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
            --%>
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
