<%@ Page Title="Iexpense Request" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Other_Reimbursements.aspx.cs"
    Inherits="iEmpPower.UI.Benefits_Payment.Other_Reimbursements" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        #tblPRRQ td:nth-child(2), #tblPRRQ2 td:nth-child(2) {
            border-right-style: dashed;
            border-right-width: 2px;
            border-right-color: #dee2e6;
        }

        #tblPRRQ tr:first-child td:nth-child(2), #tblPRRQ tr:last-child td:nth-child(2), #tblPRRQ2 tr:first-child td:nth-child(2), #tblPRRQ2 tr:last-child td:nth-child(2) {
            border: none !important;
        }

        #tblPRRQ tr {
            width: 100%;
        }



        #tblPRRQ td:nth-child(odd) {
            width: 15%;
        }



        #tblPRRQ td:nth-child(even) {
            width: 35%;
        }

        .card-box {
            padding-top: 5px !important;
            padding-bottom: 0px !important;
        }

        .col-md-1 {
            width: auto !important;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePnl" runat="server">
        <ContentTemplate>

            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box">
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="iExpense_Request.aspx">iExpense Status</a></li>
                                <li class="breadcrumb-item active">iExpense Request</li>
                            </ol>
                        </div>
                        <h4 class="page-title">Create iExpense Requisition&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>
                        <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" Font-Size="Medium"></asp:Label>
                        <asp:Label ID="lblIndent" runat="server" CssClass="msgboard" Font-Size="Medium"></asp:Label>
                    </span></h4>
                        <%-- <a href="Other_Reimbursements.aspx">
                            <button type="button" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right">
                                <i class="mdi mdi-plus"></i>Add New iExpense Request</button></a>
                        <a href="Other_Reimbursements.aspx">
                            <button type="button" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right">
                                <i class="mdi mdi-pencil"></i>Edit same iExpense Request</button></a>--%>


                        <asp:LinkButton runat="server" ID="lbtAddNew" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right" Visible="false" OnClick="lbtAddNew_Click"><i class="mdi mdi-plus"></i>
Add New iExpense Request </asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lbtnEdit" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right" Visible="false" OnClick="lbtnEdit_Click"><i class="mdi mdi-pencil"></i>
Edit same iExpense Request </asp:LinkButton>
                    </div>

                </div>
            </div>
            <!-- end page title -->

            <div class="row">

                <!-- Tab Panel Start / -->
                <div class="col-xl-12 m-t-20">
                    <div class="tab-content m-0 p-0">

                        <!-- =========== Tab Panel 1 ==================-->


                        <%--<div class="table-responsive card-box">--%>
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
                                            <td class="col-md-1"><b>iExp ID</b></td>
                                            <td class="col-md-1">
                                                <asp:Literal ID="ltiExpID" runat="server" Text="NA" /></td>
                                            <td class="col-md-1"><b>Task</b></td>
                                            <td class="col-md-1">
                                                <asp:Literal ID="ltTask" runat="server" Text="NA" /></td>
                                            <td class="col-md-1"><b>Project</b></td>
                                            <td class="col-md-1">
                                                <asp:Literal ID="ltProject" runat="server" Text="NA" /></td>
                                            <td class="col-md-1"><b>Reimb. Amount</b></td>
                                            <td class="col-md-1">
                                                <asp:Literal ID="ltReimbAmt" runat="server" Text="NA" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <%-- <div id="dvlineitem" class="card-box border-0" role="alert" runat="server" visible="false">
                                <div class="table-responsive">
                                    <table class="table table-sm table-borderless mb-0 table_font_sm" runat="server" visible="false">
                                        <body>
                                            <tr>
                                                <td>
                                                    <h4>iExpense Declaration Details</h4>
                                                    <asp:Label ID="lblIndent" runat="server" CssClass="msgboard"></asp:Label>
                                                </td>
                                                <td></td>
                                            </tr>
                                        </body>
                                    </table>

                                    <asp:GridView ID="grd_IExpInfo" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" DataKeyNames="ID"
                                        OnRowCommand="grd_IExpInfo_RowCommand" OnRowDeleting="grd_IExpInfo_RowDeleting"
                                        ShowFooter="true" TabIndex="16">
                                        <Columns>
                                            <asp:BoundField DataField="IEXP_ID" HeaderText="#" />

                                            <asp:BoundField DataField="EXP_TYPE_TEXT" HeaderText="Expense Type" />

                                            <asp:BoundField DataField="S_DATE" HeaderText="Expense Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                            <asp:TemplateField HeaderText="Expenditure Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(Eval("EXPT_AMT")).ToString("#,##0.00") %>   <%# Eval("EXPT_CURR") %>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="rightJustify" />
                                                <ItemStyle CssClass="rightJustify" />
                                            </asp:TemplateField>


                                            <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" />

                                            <asp:TemplateField HeaderText="Reimbursable Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                                                <EditItemTemplate>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>    <%# Eval("RCURR") %>
                                                </ItemTemplate>
                                                <ControlStyle CssClass="rightJustify" />
                                                <ItemStyle CssClass="rightJustify" />
                                            </asp:TemplateField>


                                            <asp:BoundField DataField="JUSTIFY" HeaderText="Justification" />

                                            <asp:BoundField DataField="RECEIPT_FILE" HeaderText="Original Receipt Missing" />

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Attachments
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />

                                                    <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete File" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                        CommandName="DeleteFile" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Action
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnEditExpGrid" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light" CausesValidation="false" Text="Edit" Visible="true"
                                                        CommandName="EDITITEMS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnDeleteExpGrid" runat="server" CssClass="btn btn-danger btn-xs waves-effect waves-light" CausesValidation="false" Text="Delete" OnClientClick="javascript:return confirm('Do you want to delete this item?')"
                                                        CommandName="DELETE" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>

                                                <ItemStyle Width="100" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <%--<div class="clearfix">&nbsp;</div>--%>
                            <%--  </div>--%>
                            <%--</div>--%>

                            <asp:Panel ID="pnlcardCollpase7" runat="server">
                                <%--class='<%= state %>' runat="server"--%>
                                <div class="table-responsive card-box">
                                    <%--"card-body" >--%>
                                    <table class="table table-borderless table_font_sm" id="tblPRRQ">
                                        <thead class="border-bottom" runat="server">
                                            <tr>
                                                <th colspan="2">Add Line items</th>
                                                <th colspan="2" class="text-right"><code>*</code> are mandatory fields</th>
                                            </tr>
                                        </thead>
                                        <tbody>

                                            <tr>
                                                <td>Task <code>*</code></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlTask" runat="server" CssClass="form-control-file" TabIndex="2" ValidationGroup="vg2" OnSelectedIndexChanged="ddlTask_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Billable" Value="B"></asp:ListItem>
                                                        <asp:ListItem Text="Non-Billable" Value="NB"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFV_ddlTask" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlTask" ValidationGroup="vg2" InitialValue="0"></asp:RequiredFieldValidator>
                                                    <br />
                                                    <br />
                                                </td>
                                                <td>Project <code>*</code></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlProjectCode" runat="server" CssClass="form-control-file" TabIndex="9" OnSelectedIndexChanged="ddlProjectCode_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                                    <Ajx:ListSearchExtender ID="LSE_ddlERPProjectCode" TargetControlID="ddlProjectCode" PromptText="Search Project" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" PromptCssClass="PromptCSSClass" runat="server">
                                                    </Ajx:ListSearchExtender>
                                                    <asp:RequiredFieldValidator ID="RFVddlProjectCode" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlProjectCode" ValidationGroup="vg2" InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>


                                            <tr>
                                                <td>Reimbursement Currency<code>*</code></td>
                                                <td>
                                                    <asp:DropDownList ID="DDLCurrency" runat="server" CssClass="form-control-file" TabIndex="3" OnSelectedIndexChanged="DDLCurrency_SelectedIndexChanged" AutoPostBack="true"  EnableViewState = "true"

AppendDataBoundItems="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFVddlRemcurr" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="DDLCurrency" ValidationGroup="vg2" InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>Justification <code>*</code></td>
                                                <td>
                                                    <asp:TextBox ID="txtJustification" runat="server" CssClass="form-control-file" TextMode="MultiLine" TabIndex="10" MaxLength="250"></asp:TextBox>
                                                    <Ajx:FilteredTextBoxExtender ID="FTB_txtItemNote" runat="server" TargetControlID="txtJustification" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ./[](),\-@:;"></Ajx:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RFV_txtJustification" runat="server" ControlToValidate="txtJustification" ValidationGroup="vg2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>



                                            </tr>

                                            <tr>
                                                <td>Expense Date <code>*</code></td>
                                                <td>
                                                    <asp:TextBox ID="txtStartDate" runat="server" ValidationGroup="vg2" CssClass="form-control-file" TabIndex="4"></asp:TextBox>
                                                    <Ajx:MaskedEditExtender ID="MEE_txtStartDate" runat="server" AcceptNegative="Left"
                                                        CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                        MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                        OnInvalidCssClass="MaskedEditError" TargetControlID="txtStartDate" />
                                                    <Ajx:CalendarExtender ID="CE_txtStartDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                        TargetControlID="txtStartDate">
                                                    </Ajx:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RFV_txtNoOfUnits" runat="server" ControlToValidate="txtStartDate" ValidationGroup="vg2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <%--<asp:HiddenField ID="HiddenTodayDate" runat="server" />--%>
                                                    <asp:CompareValidator ID="CV_txtStartDate" runat="server" ErrorMessage="<= current date." ValidationGroup="vg2" Display="Dynamic" ForeColor="Red" ControlToValidate="txtStartDate"
                                                        Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
                                                </td>
                                                <td>Purpose <code>*</code></td>
                                                <td>
                                                    <asp:TextBox ID="txtPurpose" runat="server" MaxLength="200" TabIndex="11" CssClass="form-control-file" TextMode="MultiLine"></asp:TextBox>
                                                    <Ajx:FilteredTextBoxExtender ID="FTB_txtPurpose" runat="server" TargetControlID="txtPurpose" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ./[](),\-@:;"></Ajx:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RFV_txtPurpose" runat="server" ControlToValidate="txtPurpose" ValidationGroup="vg2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>Expense Type <code>*</code><br />
                                                    <asp:Label ID="lblTask" runat="server"></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-control-file" TabIndex="5" ValidationGroup="vg2" AutoPostBack="false" OnSelectedIndexChanged="ddlExpenseType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFV_ddlExpenseType" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlExpenseType" ValidationGroup="vg2" InitialValue="0"></asp:RequiredFieldValidator>
                                                    &nbsp;<small> ( Note: Select Task to load Expense Type )</small>
                                                </td>
                                                <td>Attachments</td>
                                                <td>
                                                    <asp:FileUpload ID="fuAttachments" runat="server" TabIndex="12" CssClass="form-control-file" />
                                                    <asp:Label ID="fuAttachmentsfname" runat="server"></asp:Label>
                                                    <br />
                                                    <small>( Note: Please add all details, before adding attachements.)</small>
                                                    <asp:TextBox ID="txtNoofDays" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                                                    <asp:CheckBox ID="cb" runat="server" Text="Original Receipt Missing" TabIndex="11" AutoPostBack="true" OnCheckedChanged="cb_CheckedChanged" Visible="false" />
                                                </td>

                                            </tr>

                                            <tr>
                                                <td>Expenditure Currency <code>*</code></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlExpenditureCurrency" runat="server" ValidationGroup="vg2" CssClass="form-control-file" TabIndex="6" OnSelectedIndexChanged="ddlExpenditureCurrency_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlExpenditureCurrency" ValidationGroup="vg2" ErrorMessage="*" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>Reimbursable Amount</td>
                                                <td class="fa-2x text-dark">
                                                    <asp:Label ID="lblReimbursableAmount" runat="server" Enabled="false"></asp:Label>
                                                    <asp:Label ID="LblReimbursableCurrency" runat="server" Enabled="false"></asp:Label>
                                                    <asp:HiddenField ID="HF_ReimbursAmnt" runat="server" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>Expenditure Amount <code>*</code></td>
                                                <td>
                                                    <asp:TextBox ID="txtExpenditureAmount" runat="server" ValidationGroup="vg2" CssClass="form-control-file" TabIndex="7" MaxLength="12" AutoPostBack="true" OnTextChanged="txtExpenditureAmount_TextChanged"></asp:TextBox>
                                                    <Ajx:FilteredTextBoxExtender ID="FTB_txtExpenditureAmount" runat="server" TargetControlID="txtExpenditureAmount" FilterMode="ValidChars" FilterType="Custom,Numbers" ValidChars="."></Ajx:FilteredTextBoxExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtExpenditureAmount" ValidationGroup="vg2" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="REVtxtExpenditureAmount" runat="server" ControlToValidate="txtExpenditureAmount" ErrorMessage="> 0" ForeColor="Red" ValidationGroup="vg2" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"></asp:RegularExpressionValidator>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>Exchange Rate </td>
                                                <td>
                                                    <asp:TextBox ID="txtExchangeRate1" runat="server" CssClass="txteditwidth form-control-file" TabIndex="8" OnTextChanged="txtExchangeRate_TextChanged" AutoPostBack="true" Enabled="False"></asp:TextBox>
                                                    <Ajx:FilteredTextBoxExtender ID="FTB_txtExchangeRate1" runat="server" TargetControlID="txtExchangeRate1" FilterType="Numbers, Custom" ValidChars="-."></Ajx:FilteredTextBoxExtender>

                                                    <asp:CheckBox ID="cbEdit" runat="server" AutoPostBack="True" OnCheckedChanged="cbEdit_CheckedChanged" Text=" Edit" CssClass="checkbox checkbox-info" />
                                                    <asp:Label ID="txtExchangeRate" runat="server" Enabled="false" ForeColor="White"></asp:Label>
                                                    <%--&nbsp;--%>
                                                    <asp:CompareValidator ID="CV_txtExchangeRate1" runat="server" ControlToValidate="txtExchangeRate1" ErrorMessage="!0" Display="Dynamic"
                                                        Operator="NotEqual" ValidationGroup="vg2" ValueToCompare="0" Type="Double" ForeColor="Red"></asp:CompareValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" Text="Add" Width="80px" TabIndex="14" OnClick="btnAdd_Click" ValidationGroup="vg2" />
                                                    <asp:Button ID="btnUpdateItems" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" Text="Update" Width="80px" TabIndex="15" OnClick="btnUpdateItems_Click" Visible="false" ValidationGroup="vg2" />
                                                </td>
                                                <td colspan="3">
                                                    <div class="alert alert-warning float-left ml-2 small" role="alert">
                                                        <%-- id="dvlialert" runat="server" visible="false">--%>
                                                        <i class="mdi mdi-alert-circle-outline mr-2"></i><b>In order to create, you have to add at least one line item</b>
                                                    </div>
                                                </td>
                                                <%-- <td></td>--%>
                                            </tr>
                                        </tbody>

                                    </table>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <%--  </div>--%>

                <%-- <div class="mb-3 float-left">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" Text="Add" Width="80px" TabIndex="14" OnClick="btnAdd_Click" ValidationGroup="vg2" />
                    <asp:Button ID="btnUpdateItems" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" Text="Update" Width="80px" TabIndex="15" OnClick="btnUpdateItems_Click" Visible="false" ValidationGroup="vg2" />
                </div>

                <div class="alert alert-warning float-left ml-2 small" role="alert">
                    <i class="mdi mdi-alert-circle-outline mr-2"></i><b>In order to create, you have to add at least one line item</b>
                </div>
                <div class="clearfix"></div>--%>


                <!-- ////// iExpense Declaration Details //////// -->

                <div id="dvlineitem" class="card-box border-0" role="alert" runat="server" visible="false">
                    <div class="table-responsive">
                        <table id="Table1" class="table table-sm table-borderless mb-0 table_font_sm" runat="server" visible="false">
                            <body>
                                <tr>
                                    <td>
                                        <h4>iExpense Declaration Details</h4>
                                        <%--<asp:Label ID="lblIndent" runat="server" CssClass="msgboard"></asp:Label>--%>
                                    </td>
                                    <td></td>
                                </tr>
                            </body>
                        </table>

                        <asp:GridView ID="grd_IExpInfo" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" DataKeyNames="ID"
                            OnRowCommand="grd_IExpInfo_RowCommand" OnRowDeleting="grd_IExpInfo_RowDeleting"
                            ShowFooter="true" TabIndex="16">
                            <Columns>
                                <asp:BoundField DataField="IEXP_ID" HeaderText="#" />

                                <asp:BoundField DataField="EXP_TYPE_TEXT" HeaderText="Expense Type" />

                                <asp:BoundField DataField="S_DATE" HeaderText="Expense Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                <asp:TemplateField HeaderText="Expenditure Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("EXPT_AMT")).ToString("#,##0.00") %>   <%# Eval("EXPT_CURR") %>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>


                                <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" />

                                <asp:TemplateField HeaderText="Reimbursable Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>    <%# Eval("RCURR") %>
                                    </ItemTemplate>
                                  
                                </asp:TemplateField>


                                <asp:BoundField DataField="JUSTIFY" HeaderText="Justification" />

                                <asp:BoundField DataField="RECEIPT_FILE" HeaderText="Original Receipt Missing" Visible="false" />

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Attachments
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />

                                        <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete File" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                            CommandName="DeleteFile" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Action
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnEditExpGrid" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light" CausesValidation="false" Text="Edit" Visible="true"
                                            CommandName="EDITITEMS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnDeleteExpGrid" runat="server" CssClass="btn btn-danger btn-xs waves-effect waves-light" CausesValidation="false" Text="Delete" OnClientClick="javascript:return confirm('Do you want to delete this item?')"
                                            CommandName="DELETE" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                    </ItemTemplate>

                                    <ItemStyle Width="100" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <%--<div class="clearfix">&nbsp;</div>--%>
                    </div>
                </div>

                <%-- <div id="dvlineitem" class="card-box border-0" role="alert" runat="server" visible="false">
                            <div class="table-responsive">
                                <table class="table table-sm table-borderless mb-0 table_font_sm">
                                    <body>
                                        <tr>
                                            <td>
                                                <h4>iExpense Declaration Details</h4>
                                                <asp:Label ID="lblIndent" runat="server" CssClass="msgboard"></asp:Label>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </body>
                                </table>

                                <asp:GridView ID="grd_IExpInfo" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" DataKeyNames="ID"
                                    OnRowCommand="grd_IExpInfo_RowCommand" OnRowDeleting="grd_IExpInfo_RowDeleting"
                                    ShowFooter="true" TabIndex="16">
                                    <Columns>
                                        <asp:BoundField DataField="IEXP_ID" HeaderText="ExpType No" />

                                        <asp:BoundField DataField="EXP_TYPE_TEXT" HeaderText="Expense Type" />

                                        <asp:BoundField DataField="S_DATE" HeaderText="Expense Date" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:TemplateField HeaderText="Expenditure Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("EXPT_AMT")).ToString("#,##0.00") %>   <%# Eval("EXPT_CURR") %>
                                            </ItemTemplate>
                                            <ControlStyle CssClass="rightJustify" />
                                            <ItemStyle CssClass="rightJustify" />
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" />

                                        <asp:TemplateField HeaderText="Reimbursable Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>    <%# Eval("RCURR") %>
                                            </ItemTemplate>
                                            <ControlStyle CssClass="rightJustify" />
                                            <ItemStyle CssClass="rightJustify" />
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="JUSTIFY" HeaderText="Justification" />

                                        <asp:BoundField DataField="RECEIPT_FILE" HeaderText="Original Receipt Missing" />

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Attachments
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />

                                                <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete File" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                    CommandName="DeleteFile" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                Action
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnEditExpGrid" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light" CausesValidation="false" Text="Edit" Visible="true"
                                                    CommandName="EDITITEMS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnDeleteExpGrid" runat="server" CssClass="btn btn-danger btn-xs waves-effect waves-light" CausesValidation="false" Text="Delete" OnClientClick="javascript:return confirm('Do you want to delete this item?')"
                                                    CommandName="DELETE" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>

                                            <ItemStyle Width="100" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <div class="clearfix">&nbsp;</div>
                            </div>
                        </div>--%>

                <div class="mb-3">
                    <asp:Button ID="BtnSave" runat="server" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" Text="Save" TabIndex="17" CausesValidation="false" OnClick="BtnSave_Click" Visible="false" />
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn bg-brand-btn waves-effect waves-light" Text="Send for Approval" TabIndex="18" OnClick="btnSubmit_Click" Visible="false" />
                </div>

            </div>
            <!-- end Tab Panel-->

            </div>
                <!-- end row -->

            </div>
            <!-- container -->





            <%--  <script type="text/javascript" src="../../Scripts/jquery-1.8.3.min.js"></script>
            <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
            <script type="text/javascript">


               

                $(document).ready(function () {
                    // $("select").searchable();

                });
            </script>--%>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />

            <asp:PostBackTrigger ControlID="btnUpdateItems" />
            <asp:PostBackTrigger ControlID="BtnSave" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="grd_IExpInfo" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
