<%@ Page Title="Edit Saved Iexpense" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Saved_Other_Reimbursements.aspx.cs"
    Inherits="iEmpPower.UI.Benefits_Payment.Saved_Other_Reimbursements" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true" %>

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
    <asp:UpdatePanel ID="UpdatePnl" runat="server">
        <ContentTemplate>

            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box">
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="iExpense_Request.aspx">iExpense Requests</a></li>
                                <li class="breadcrumb-item active">IExpense Requisition</li>
                            </ol>
                        </div>
                        <h4 class="page-title">IExpense Requisition</h4>
                    </div>
                </div>
            </div>
            <!-- end page title -->
            <asp:Panel ID="pnlcontent" runat="server" DefaultButton="btnsearch">
                <div class="table-responsive card-box" runat="server" visible="false">
                    <h4>IExpense Requisition  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>

                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1" Font-Size="Medium"></asp:Label>
                         <asp:Label ID="Label2" runat="server" CssClass="msgboard" Font-Size="Medium"></asp:Label>
                     </span></h4>
                    <table>

                        <tr>
                            <td align="right">Select:</td>
                            <td>
                                <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="form-control-file" TabIndex="1">
                                    <asp:ListItem Text="-SEARCH-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Expense ID" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Task" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Status" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>


                            <td>
                                <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" placeholder="Enter Text" TabIndex="2"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnsearch" runat="server" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="3" Text="Search" CssClass="btn btn-xs btn-secondary" />
                                &nbsp;&nbsp;
                                    <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" TabIndex="4" Text="Clear" CssClass="btn btn-xs btn-secondary" />
                            </td>



                        </tr>
                    </table>
                    <br />
                    <asp:GridView ID="grdIexpdetails" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" OnRowCommand="grdIexpdetails_RowCommand"
                        DataKeyNames="IEXP_ID,PURPOSE,TASK,RCURR,PROJID" AllowPaging="true" AllowSorting="true" OnSorting="grdIexpdetails_Sorting" OnPageIndexChanging="grdIexpdetails_PageIndexChanging" PageSize="5" TabIndex="5">
                        <Columns>
                            <asp:BoundField DataField="IEXP_ID" HeaderText="Expense Id" SortExpression="IEXP_ID" />
                            <asp:BoundField DataField="POST1" HeaderText="Project" SortExpression="POST1" />
                            <%-- <asp:BoundField DataField="TASK" HeaderText="Task" SortExpression="TASK" />--%>
                            <asp:TemplateField HeaderText="Task" SortExpression="TASK">
                                <ItemTemplate>

                                    <%#(Eval("TASK").ToString().Trim()=="B") ? "Billable" : "Non-Billable"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="RE_AMT" HeaderText="Total Reimbursement Amount" SortExpression="RE_AMT" ItemStyle-HorizontalAlign="Right" />
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

                    <br />
                </div>

                <asp:Panel ID="pnexpForm" runat="server" Visible="false">

                    <asp:Label ID="Label1" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>

                    <%-- <br />
                    <strong>Fields marked <span style="color: red">*</span>  are mandatory</strong><br />--%>


                    <asp:Label ID="lblIndent" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
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
                        <asp:Panel ID="pnlcardCollpase7" runat="server">
                            <div class="table-responsive card-box">
                                <table class="table table-borderless table_font_sm" id="tblPRRQ">
                                    <thead class="border-bottom">
                                        <tr>
                                            <th colspan="2">Create Expense Report </th>
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
                                            </td>

                                            <td>Project <code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="ddlProjectCode" runat="server" CssClass="form-control-file" TabIndex="9"></asp:DropDownList>
                                                <Ajx:ListSearchExtender ID="LSE_ddlERPProjectCode" TargetControlID="ddlProjectCode" PromptText="Search Project" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" PromptCssClass="PromptCSSClass" runat="server">
                                                </Ajx:ListSearchExtender>
                                                <asp:RequiredFieldValidator ID="RFVddlProjectCode" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlProjectCode" ValidationGroup="vg2" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>

                                        </tr>


                                        <tr>
                                            <td>Reimbursement Currency<code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="DDLCurrency" runat="server" CssClass="form-control-file" TabIndex="3" OnSelectedIndexChanged="DDLCurrency_SelectedIndexChanged" AutoPostBack="true">
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
                                            <td>Expense Type <code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-control-file" TabIndex="5" ValidationGroup="vg2" AutoPostBack="true" OnSelectedIndexChanged="ddlExpenseType_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFV_ddlExpenseType" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlExpenseType" ValidationGroup="vg2" InitialValue="0"></asp:RequiredFieldValidator>
                                                &nbsp;<small> ( Note: Select Task to load Expense Type )</small>
                                            </td>
                                            <td>Attachments</td>
                                            <td>
                                                <asp:FileUpload ID="fuAttachments" runat="server" TabIndex="12" CssClass="form-control-file" />
                                                <asp:Label ID="fuAttachmentsfname" runat="server"></asp:Label>
                                                <br />
                                                <small>( Note: Please add all the details, before adding any associated attachements.)</small>
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
                                                &nbsp;
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
                                                <div class="alert alert-warning float-left ml-2 small" role="alert" id="dvlialert" runat="server" visible="false">
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
                   <%-- <div class="mb-3">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" Text="Add" Width="80px" TabIndex="14" OnClick="btnAdd_Click" ValidationGroup="vg2" />
                        <asp:Button ID="btnUpdateItems" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" Text="Update" Width="80px" TabIndex="15" OnClick="btnUpdateItems_Click" Visible="false" ValidationGroup="vg2" />
                    </div>
                    <div class="alert alert-warning float-left ml-2 small" role="alert">
                        <i class="mdi mdi-alert-circle-outline mr-2"></i><b>In order to create, you have to add at least one line item</b>
                    </div>
                    <div class="clearfix"></div>--%>

                    <div class="card-box border-0" role="alert">
                        <div class="table-responsive">
                            <table class="table table-sm table-borderless mb-0 table_font_sm">
                                <body>
                                    <tr>
                                        <td>
                                            <h4>iExpense Declaration Details</h4>
                                           <%-- <asp:Label ID="Label2" runat="server" CssClass="msgboard"></asp:Label>--%>
                                        </td>
                                        <td>
                                            <%--<button type="button" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right">
                                                    <i class="mdi mdi-plus"></i>Add New Item</button>--%></td>
                                    </tr>
                                </body>
                            </table>
                            <asp:GridView ID="grd_IExpInfo" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" Width="99%" DataKeyNames="ID,EXP_TYPE,S_DATE,EXPT_CURR,EXPT_AMT,RE_AMT,JUSTIFY,RECEIPT_FILE,RECEIPT_FID,RECEIPT_FPATH,NO_DAYS,EXC_RATE"
                                OnRowCommand="grd_IExpInfo_RowCommand" OnRowDeleting="grd_IExpInfo_RowDeleting"
                                ShowFooter="true" FooterStyle-CssClass="foo01" TabIndex="20">
                                <Columns>
                                    <asp:BoundField DataField="ID" HeaderText="ExpType No" />
                                    <asp:BoundField DataField="EXP_TYPE_TEXT" HeaderText="Expense Type" />
                                    <asp:BoundField DataField="S_DATE" HeaderText="Expense Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                    <%--  <asp:BoundField DataField="NO_DAYS" HeaderText="No. of Days" />--%>
                                    <%--<asp:BoundField DataField="EXPT_AMT" HeaderText="Expenditure Amount" />
            <asp:BoundField DataField="EXPT_CURR" HeaderText="Expenditure Currency" />
                                    --%>
                                    <asp:TemplateField HeaderText="Expenditure Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%-- <%# Eval("EXPT_AMT") %> ( <%# Eval("EXPT_CURR") %>)--%>
                                            <%# Convert.ToDouble(Eval("EXPT_AMT")).ToString("#,##0.00") %>   <%# Eval("EXPT_CURR") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" />
                                    <%-- <asp:BoundField DataField="RE_AMT" HeaderText="Reimbursable Amount" DataFormatString="{0:f2}" />--%>
                                    <asp:TemplateField HeaderText="Reimbursable Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right">
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <%--<%# String.Format("{0:f2}", decimal.Parse(DataBinder.Eval(Container.DataItem, "RE_AMT").ToString()))  %>--%>
                                            <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>    <%# Eval("RCURR") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="JUSTIFY" HeaderText="Justification" />
                                    <asp:BoundField DataField="RECEIPT_FILE" HeaderText="Original Receipt Missing"  Visible="false"/>
                                    <%--  <asp:BoundField DataField="RECEIPT_FPATH" HeaderText="Attachments" />--%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Attachments
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />

                                            <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete File" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                CommandName="DeleteFile" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle Width="100" />
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Action
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnEditExp" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light" CausesValidation="false" Text="Edit" Visible="true"
                                                CommandName="EDITITEMS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                        </ItemTemplate>
                                        <%--<ItemStyle Width="100" />--%>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnDeleteExp" runat="server" CssClass="btn btn-danger btn-xs waves-effect waves-light" CausesValidation="false" Text="Delete" OnClientClick="javascript:return confirm('Do you want to delete this item?')"
                                                CommandName="DELETE" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle Width="100" />
                                    </asp:TemplateField>

                                    <%--     <asp:TemplateField>
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnEditExp" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light"  CausesValidation="false" Text="Edit" Visible="true"
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
                                    </asp:TemplateField>--%>
                                </Columns>
                            </asp:GridView>
                            <div class="clearfix">&nbsp;</div>
                        </div>
                    </div>
                    <div class="mb-3">
                        <asp:Button ID="BtnSave" runat="server" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" Text="Save" TabIndex="17" CausesValidation="false" OnClick="BtnSave_Click" />
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn bg-brand-btn waves-effect waves-light" Text="Send for Approval" TabIndex="18" OnClick="btnSubmit_Click" />
                    </div>
                </asp:Panel>

                <script type="text/javascript" src="../../Scripts/jquery-1.8.3.min.js"></script>
                <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
                <script src="../../Scripts/jquery.searchabledropdown-1.0.8.min.js" type="text/javascript"></script>
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
                    $(document).ready(function () {
                        $("select").searchable();

                    });
                </script>

                <%--<script type = "text/javascript">
        var ddlText, ddlValue, ddl, lblMesg;
        var ddlTextRC, ddlValueRC, ddlRC;
        var ddlTextEC, ddlValueEC, ddlEC;
        var ddlTextCountry, ddlValueddlTextCountry, ddlCountry;
        var ddlTextRegion, ddlValueddlTextRegion, ddlRegion;
        function CacheItems() {
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=ddlProjectCode.ClientID %>");



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
            //lblMesg.innerHTML = ddl.options.length + " items found.";
            if (ddl.options.length == 0) {
                AddItem("No items found.", "");
            }
        }


        function AddItem(text, value) {
            var opt = document.createElement("option");
            opt.text = text;
            opt.value = value;
            ddl.options.add(opt);
        }


</script>--%>
            </asp:Panel>
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
