<%@ Page Title="Purchase Requisition" Language="C#" MasterPageFile="~/UI/SubSiteMaster.Master" AutoEventWireup="true" CodeBehind="Purchase_Request.aspx.cs" Inherits="iEmpPower.UI.PR.Purchase_Request"
    Theme="SkinFile" Culture="en-GB" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
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

 

        .rcls
        {
            color: red;
        }

 

        /*select, select option
        {
            font-size: 11px !important;
        }*/

 

        .TblCls
        {
            width: 90%;
            border-collapse: collapse;
        }

 

        .TblCls01
        {
            width: 90%;
            border-collapse: collapse;
        }

 

        #tblPRRQ, #grd_ItemInfo, #tblPRRQ2
        {
            width: 100%;
        }

 

 

            #tblPRRQ tr, #tblPRRQ2 tr, #grd_ItemInfo tr
            {
                width: 100%;
            }

 

 

            #tblPRRQ td:nth-child(odd), #tblPRRQ2 td:nth-child(odd)
            {
                width: 15%;
            }

 

 

            #tblPRRQ td:nth-child(even), #tblPRRQ2 td:nth-child(even)
            {
                width: 35%;
            }

 

 


        .rbtnspc input[type="radio"]
        {
            margin-left: 10px;
            margin-right: 1px;
        }

 


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

 

        .card-box
        {
            padding-top: 5px !important;
        }

 

        .col-md-1
        {
            width: auto !important;
        }

 

        .right
        {
            text-align: right !important;
        }

 

        /*#grd_ItemInfo tr td, th
        {
            width: 10% !important;
        }*/

    </style>
    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item"><a href="Purchase_Requisitions.aspx">Purchase Requisitions</a></li>
                        <li class="breadcrumb-item active">Purchase Requisitions Form</li>
                    </ol>
                </div>
                <%--<h4 class="page-title">Create Purchase Requisition
                <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
                    <asp:Label ID="lblIndent" runat="server" CssClass="msgboard"></asp:Label>
                </h4>--%>
                <h4 class="page-title">Create Purchase Requisition&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>
                        <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" Font-Size="Medium"></asp:Label>
                        <asp:Label ID="lblIndent" runat="server" CssClass="msgboard" Font-Size="Medium"></asp:Label>
                    </span></h4>
                <asp:LinkButton runat="server" ID="lbtAddNew" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right" Visible="false" OnClick="lbtAddNew_Click"><i class="mdi mdi-plus"></i>
Add New Purchase Request </asp:LinkButton>
                <asp:LinkButton runat="server" ID="lbtnEdit" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right" Visible="false" OnClick="lbtnEdit_Click"><i class="mdi mdi-pencil"></i>
Edit same Purchase Request </asp:LinkButton>
            </div>
        </div>
    </div>
    <!-- end page title -->


    <div class="row" >

        <%--  <asp:UpdatePanel ID="UPContent" runat="server">
            <ContentTemplate>--%>
        <asp:Panel ID="pnlcontent" runat="server" DefaultButton="btnSubmit">
            <div id="divSearch" runat="server" class="table-responsive card-box" visible="false">
                <h4>My PR Requisitions</h4>
                <div class="DivMsg">
                    <asp:Label ID="LblMsg" runat="server"></asp:Label>
                </div>
                <table>
                    <tr>
                        <td align="right">Select:</td>
                        <td>
                            <asp:DropDownList ID="ddlSeachSelect" runat="server" TabIndex="1" CssClass="form-control-file">
                                <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Purchase No" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Originator" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Status" Value="3"></asp:ListItem>

                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtsearch" runat="server" TabIndex="2" placeholder="Enter Text" CssClass="form-control-file"></asp:TextBox>
                        </td>
                        <td align="right">Created On</td>
                        <td>
                            <asp:TextBox ID="txtCreatedOn" runat="server" TabIndex="3" placeholder="Select Date" CssClass="form-control-file"></asp:TextBox>
                            <Ajx:MaskedEditExtender ID="MEE_txtStartDate" runat="server" AcceptNegative="Left"
                                CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                OnInvalidCssClass="MaskedEditError" TargetControlID="txtCreatedOn" />
                            <Ajx:CalendarExtender ID="CE_txtCreatedOn" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                TargetControlID="txtCreatedOn">
                            </Ajx:CalendarExtender>
                        </td>
                        <td>&nbsp;&nbsp;
                                    <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" TabIndex="4" CssClass="btn btn-xs btn-secondary" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnclear" runat="server" Text="Clear" OnClick="btnclear_Click" TabIndex="5" CssClass="btn btn-xs btn-secondary" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnNewPR" runat="server" Text="New PR" OnClick="btnNewPR_Click" TabIndex="6" CssClass="btn btn-xs btn-secondary" />
                        </td>
                    </tr>
                </table>

                <h4>PR Template Copy</h4>
                <asp:GridView ID="grdPurchaseItemDetails" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" DataKeyNames="PRID" OnRowCommand="grdPurchaseItemDetails_RowCommand"
                    AllowPaging="true" OnPageIndexChanging="grdPurchaseItemDeatils_PageIndexChanging"
                    AllowSorting="true" OnSorting="grdPurchaseItemDeatils_Sorting" OnRowCancelingEdit="grdPurchaseItemDetails_RowCancelingEdit">
                    <Columns>
                        <asp:BoundField DataField="PRID" HeaderText="PR No." SortExpression="PRID" />
                        <asp:BoundField DataField="IPERNR" HeaderText="Indentor" SortExpression="INDENTOR" />
                        <asp:BoundField DataField="RPERNR" HeaderText="Requestor" SortExpression="REQUESTOR" />
                        <asp:BoundField DataField="SUG_SUPP" HeaderText="Supplier" SortExpression="SUG_SUPP" />
                        <asp:BoundField DataField="IN_BUDGET" HeaderText="In Budget" SortExpression="IN_BUDGET" />
                        <asp:BoundField DataField="CRITICALITY" HeaderText="Criticality" SortExpression="CRITICALITY" />
                        <asp:BoundField DataField="PSPNR" HeaderText="Project Code" SortExpression="PSPNR" />
                        <asp:BoundField DataField="BNFPO" HeaderText="Capex" SortExpression="BNFPO" />


                        <asp:TemplateField HeaderText="Total Amount" SortExpression="UNIT_PRICE">
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#  Convert.ToDouble(Eval("UNIT_PRICE")).ToString("#,##0.00") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="WAERS" HeaderText="Currency" SortExpression="WAERS" />
                        <asp:TemplateField HeaderText="Total Amount (INR)" SortExpression="TOTALAMT">
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%#  Convert.ToDouble(Eval("TAINRAmt")).ToString("#,##0.00") %>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="CREATED_ON1" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="CREATED_ON1" HeaderStyle-Width="30%" />
                        <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS" />


                        <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnEmpPurchaseItemView" runat="server" CausesValidation="False" CommandName="Copy" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><%# Eval("STATUS").ToString() == "Saved" ? "Edit" : "Copy" %></asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CommandName="Cancel" OnClientClick="return confirm('Do you want to Cancel this PR?');" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><%# Eval("STATUS").ToString() == "Saved" ? "Cancel" : "" %></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle BackColor="Silver" />
                </asp:GridView>

            </div>

            <%--<asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>--%>



            <!-- Tab Panel Start / -->
            <div class="col-xl-12 m-t-20" id="divNewPR" runat="server">
               <%--  visible="false"--%>
                <div class="tab-content m-0 p-0 ">
                 
                    <!-- Sub Tab Panel -->
                    <div class="card text-dark table-responsive card-box col-30">
                        <div class="card-body border-bottom table-responsive card-box">
                            <div class="card-widgets">
                                <%--<a data-toggle="collapse" href="#cardCollpase7" role="button" aria-expanded="false" aria-controls="cardCollpase2" class="collapsed bg-warning btn-rounded">
                                        <i class="mdi mdi-minus font-20 text-white"></i></a>--%>
                                <asp:LinkButton runat="server" ID="Tab1" CssClass=" bg-warning btn-rounded">
                                    <i id="icollapse" runat="server" class="mdi mdi-minus font-20 text-white"></i>
                                </asp:LinkButton>
                                <Ajx:CollapsiblePanelExtender ID="cpe" runat="Server"
                                    TargetControlID="pnlcardCollpase7"
                                    CollapsedSize="3"
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
                            <h4 class="card-title mb-0">PR Header
                                   
                                        <span style="float: right; font-size: small !important; margin-right: 10px; margin-top: 5px"><code>*</code> are mandatory fields</span></h4>
                        </div>
                        <div class="card-body mt-n2 alert-success">

                            <table class="table table-sm table-borderless font-16 mt-n2 mb-n2 mb-0 mt-0 text-dark  table_font_sm  ">
                                <tbody>
                                    <tr>
                                        <td class="col-md-1"><b>PR No. </b></td>
                                        <td class="col-md-1">
                                            <asp:Literal ID="ltPRnum" runat="server" Text="NA" /></td>
                                        <td class="col-md-1"><b>Indentor</b></td>
                                        <td class="col-md-1">
                                            <asp:Literal ID="ltIndentorHeader" runat="server" Text="NA" /></td>
                                        <td class="col-md-1"><b>Requestor</b></td>
                                        <td class="col-md-1">
                                            <asp:Literal ID="ltRequestor" runat="server" Text="NA" /></td>
                                        <td class="col-md-1"><b>Project</b></td>
                                        <td class="col-md-1">
                                            <asp:Literal ID="ltProject" runat="server" Text="NA" /></td>
                                        <td class="col-md-1"><b>Total Amt</b></td>
                                        <td class="col-md-1">
                                            <asp:Literal ID="ltTotalAmount" runat="server" Text="NA" /></td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>
                        <%--  <div class="table-responsive card-box" id="dvlineitem" runat="server" visible="false">
                                    <asp:GridView ID="grd_ItemInfo" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" DataKeyNames="id,BANFN_EXT,MTART,NO_OF_UNITS,UNIT_PRICE,TAXABLE,WAERS,MEINS,ITEM_NOTE,MATNR,TXZ01" Width="99%"
                                        OnRowCommand="grd_ItemInfo_RowCommand" OnRowDeleting="grd_ItemInfo_RowDeleting" TabIndex="46" ShowFooter="true">
                                        <Columns>


                                            <asp:TemplateField HeaderText="Item No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:BoundField DataField="MTART" HeaderText="Category" />

                                            <asp:BoundField DataField="NO_OF_UNITS" HeaderText="No. of Units" />
                                            <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price" />
                                            <asp:BoundField DataField="TAXABLE" HeaderText="Taxable" />
                                            <asp:BoundField DataField="WAERS" HeaderText="Currency" />

                                            <asp:BoundField DataField="MEINS" HeaderText="Unit of Measurements" />
                                            <asp:TemplateField HeaderText="Total Price">
                                                <ItemTemplate>

                                                    <%# ((Convert.ToDecimal(Eval("UNIT_PRICE")))*(Convert.ToDecimal(Eval("NO_OF_UNITS")))).ToString("#,##0.00") %>    <%# Eval("WAERS") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnDeleteExp" runat="server" CausesValidation="false" CssClass="btn btn-danger btn-xs waves-effect waves-light" Text="Delete" OnClientClick="javascript:return confirm('Do you want to delete this item?')"
                                                        CommandName="DELETE" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                                    <asp:LinkButton ID="LbtnEditExptype" runat="server" CausesValidation="false" CssClass="btn btn-warning btn-xs waves-effect waves-light" Text="EDIT"
                                                        CommandName="EDITITEMS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>--%>

                        <asp:Panel ID="pnlcardCollpase7" runat="server">
                            <div class="table-responsive card-box">
                                <table class="table table-borderless table_font_sm" id="tblPRRQ">
                                    <thead>
                                        <%-- <thead class="border-bottom">--%>
                                        <tr>
                                            <%-- <tr class="border-bottom">--%>
                                            <th colspan="2">
                                                <h4 class="card-title mb-0">

                                                    <asp:Literal ID="ltIndentor" runat="server" Visible="false"></asp:Literal></h4>
                                                <asp:TextBox ID="txtIndentor" runat="server" TabIndex="6" ValidationGroup="VG1" Enabled="false" Visible="false"></asp:TextBox></th>
                                            <th colspan="2" class="text-right" style="display: none;"><code>*</code> are mandatory fields</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Requestor <code>*</code></td>
                                            <td>
                                                <asp:TextBox ID="txtRequester" runat="server" CssClass="form-control-file" OnTextChanged="txtRequester_TextChanged" AutoPostBack="true" TabIndex="7" ValidationGroup="VG1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFV_txtRequester" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtRequester" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                                <Ajx:AutoCompleteExtender ID="EmpNameAutoCompleteExtender" runat="server" TargetControlID="txtRequester" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="5" CompletionInterval="1" FirstRowSelected="True"
                                                    ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/UI/Benefits_Payment/EmployeeNames.asmx"
                                                    CompletionListCssClass="completionList"
                                                    CompletionListHighlightedItemCssClass="itemHighlighted"
                                                    CompletionListItemCssClass="listItem">
                                                </Ajx:AutoCompleteExtender>
                                            </td>
                                            <td>In Budget </td>
                                            <td>
                                                <div class="radio">
                                                    <asp:RadioButton ID="rbtnBudgetYes" runat="server" GroupName="Budget" Text="Yes" TabIndex="19" AutoPostBack="True" OnCheckedChanged="rbtnBudgetYes_CheckedChanged" />&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rbtnBudgetNo" runat="server" GroupName="Budget" Text="No" Checked="True" TabIndex="20" AutoPostBack="True" OnCheckedChanged="rbtnBudgetNo_CheckedChanged" />
                                                    <asp:TextBox ID="txtWillthisItembeCapitalized" runat="server" CssClass="ml-2" Enabled="False" TabIndex="21" ValidationGroup="VG1" placeholder="Enter Budget line item" Visible="False"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="RFVWillthisItembeCapitalized" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtWillthisItembeCapitalized" ValidationGroup="VG1" Enabled="false" Visible="false"></asp:RequiredFieldValidator>
                                                    <Ajx:FilteredTextBoxExtender ID="FTb_txtWillthisItembeCapitalized" runat="server" TargetControlID="txtWillthisItembeCapitalized" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ./[]()&,\-@:;"></Ajx:FilteredTextBoxExtender>
                                                </div>

                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Main Function <code>*</code></td>
                                            <td>
                                                <asp:TextBox ID="txtMainFunction" runat="server" CssClass="form-control-file" TabIndex="8" ValidationGroup="VG1" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>Capitalized ?  <code>*</code></td>
                                            <td>
                                                <div class="radio">
                                                    <asp:RadioButton ID="rbtnCapitalizedYes" runat="server" GroupName="Capitalized" Text="Yes" AutoPostBack="true" OnCheckedChanged="rbtnCapitalizedYes_CheckedChanged" TabIndex="22" />&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rbtnCapitalizedNo" runat="server" GroupName="Capitalized" Text="No" Checked="True" AutoPostBack="true" OnCheckedChanged="rbtnCapitalizedNo_CheckedChanged1" TabIndex="23" />
                                                    <%-- <asp:TextBox ID="txtWillthisItembeCapitalized" runat="server" CssClass="ml-2" Enabled="False" TabIndex="23" ValidationGroup="VG1" placeholder="Enter Budget line item"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="RFVWillthisItembeCapitalized" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtWillthisItembeCapitalized" ValidationGroup="VG1" Enabled="false" Visible="false"></asp:RequiredFieldValidator>
                                                    <Ajx:FilteredTextBoxExtender ID="FTb_txtWillthisItembeCapitalized" runat="server" TargetControlID="txtWillthisItembeCapitalized" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ./[]()&,\-@:;"></Ajx:FilteredTextBoxExtender>--%>
                                                </div>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>Sub Function <code>*</code></td>
                                            <td>
                                                <asp:TextBox ID="txtSubFunction" runat="server" CssClass="form-control-file" TabIndex="9" ValidationGroup="VG1" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>Managed Service/Bureau?</td>
                                            <td>
                                                <div class="radio">
                                                    <asp:RadioButton ID="rbtnServiceYes" runat="server" GroupName="Service" Text="Yes" TabIndex="24" />&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rbtnServiceNo" runat="server" GroupName="Service" Text="No" Checked="True" TabIndex="25" />
                                                </div>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>MIS Group C <code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="ddlMISGroupC" runat="server" CssClass="form-control-file" AutoPostBack="true" OnSelectedIndexChanged="ddlMISGroupC_SelectedIndexChanged" TabIndex="10" ValidationGroup="VG1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVMISGroupC" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlMISGroupC" ValidationGroup="VG1" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>

                                            <td>Criticality <code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="ddlCriticality" runat="server" CssClass="form-control-file" TabIndex="26" ValidationGroup="VG1">
                                                    <asp:ListItem Selected="True" Value="0">- SELECT -</asp:ListItem>
                                                    <asp:ListItem Value="High">High</asp:ListItem>
                                                    <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                                    <asp:ListItem Value="Low">Low</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVCriticality" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlCriticality" ValidationGroup="VG1" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>MIS Group A </td>
                                            <td>
                                                <asp:TextBox ID="txtMISGroupA" runat="server" CssClass="form-control-file" TabIndex="11" ValidationGroup="VG1" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>ERP Code <code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="ddlERPProjectCode" runat="server" CssClass="form-control-file" OnSelectedIndexChanged="ddlERPProjectCode_SelectedIndexChanged" AutoPostBack="true" TabIndex="27" ValidationGroup="VG1"></asp:DropDownList>
                                                <Ajx:ListSearchExtender ID="LSE_ddlERPProjectCode" TargetControlID="ddlERPProjectCode" PromptText="Search Project" PromptPosition="Top" QueryPattern="Contains" IsSorted="true" PromptCssClass="PromptCSSClass" runat="server">
                                                </Ajx:ListSearchExtender>
                                              
                                                <asp:RequiredFieldValidator ID="RFVERPProjectCode" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlERPProjectCode" ValidationGroup="VG1" InitialValue="0"></asp:RequiredFieldValidator>

                                                <br />
                                                <small class="text-blue">( Note: External Projects can be added only by the PM of the project. )</small>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>MIS Group B </td>
                                            <td>
                                                <asp:TextBox ID="txtMISGroupB" runat="server" CssClass="form-control-file" TabIndex="12" ValidationGroup="VG1" Enabled="false"></asp:TextBox>
                                            </td>
                                            <td>Project Manager Name/ID </td>
                                            <td>
                                                <asp:TextBox ID="ddlProjectDeliveryHeadName" runat="server" CssClass="form-control-file" TabIndex="28" Enabled="False"></asp:TextBox>
                                                <asp:TextBox ID="txtProjectDeliveryHeadID" runat="server" CssClass="form-control-file" Enabled="False" TabIndex="29"></asp:TextBox></td>
                                            <%-- <td>Employee ID</td>--%>
                                        </tr>

                                        <tr>
                                            <td>Reqtr Region <code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="ddlRequesterRegion" runat="server" CssClass="form-control-file" TabIndex="13" ValidationGroup="VG1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVRequesterRegion" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRequesterRegion" InitialValue="0" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>Is this Billable ?</td>
                                            <td>
                                                <div class="radio">
                                                    <asp:RadioButtonList ID="RbtnBillable" runat="server" TabIndex="30" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="true" OnSelectedIndexChanged="RbtnBillable_SelectedIndexChanged">
                                                        <asp:ListItem Text="Yes " Value="1" style="margin-right: 10px"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="2" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>Bill To Address <code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="ddlBillToAddress" runat="server" CssClass="form-control-file" TabIndex="14" ValidationGroup="VG1" AutoPostBack="true" OnSelectedIndexChanged="ddlBillToAddress_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVBillToAddress" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlBillToAddress" InitialValue="0" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>Business Unit <code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="ddlBusinessUnit" runat="server" CssClass="form-control-file" TabIndex="31" ValidationGroup="VG1"></asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RFVBusinessUnit" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlBusinessUnit" InitialValue="-1" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                                <br />
                                                <small class="text-blue">( Note: Select Bill To Address to load Business Unit. )</small>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>Ship To Address  <code>*</code></td>
                                            <td>
                                                <asp:Label ID="txtShipToAddress" runat="server" TabIndex="15" ValidationGroup="VG1"></asp:Label>
                                                <asp:DropDownList ID="ddlShipToAddress" runat="server" CssClass="form-control-file" TabIndex="15" ValidationGroup="VG1" Enabled="False" Visible="false"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVBillToAddress0" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlShipToAddress" InitialValue="0" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>Region <code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control-file" TabIndex="32" ValidationGroup="VG1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFVRegion" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlRegion" InitialValue="0" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>Suggested Supplier  <code>*</code></td>
                                            <td>
                                                <asp:TextBox ID="txtSuggestedSupplier" runat="server" CssClass="form-control-file" TabIndex="16" ValidationGroup="VG1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFVSuggestedSupplier" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSuggestedSupplier" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                                <Ajx:FilteredTextBoxExtender ID="FTB_txtSuggestedSupplier" runat="server" TargetControlID="txtSuggestedSupplier" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars="./[](),\-@:; "></Ajx:FilteredTextBoxExtender>
                                            </td>
                                            <td>Justification <code>*</code></td>
                                            <td>
                                                <asp:TextBox ID="txtJustification" runat="server" CssClass="form-control-file" TextMode="MultiLine" TabIndex="33" ValidationGroup="VG1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFVJustification" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtJustification" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                                <Ajx:FilteredTextBoxExtender ID="FTB_txtJustification" runat="server" TargetControlID="txtJustification" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ./[](),\-@:;"></Ajx:FilteredTextBoxExtender>
                                            </td>

                                        </tr>

                                        <tr>
                                            <td>Supplier Address</td>
                                            <td>
                                                <asp:TextBox ID="txtSuggestedAddress" runat="server" CssClass="form-control-file" TextMode="MultiLine" TabIndex="17" ValidationGroup="VG1"></asp:TextBox></td>
                                            <Ajx:FilteredTextBoxExtender ID="FTB_txtSuggestedAddress" runat="server" TargetControlID="txtSuggestedAddress" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars="./[](),\-@:; "></Ajx:FilteredTextBoxExtender>

                                            </td>
                                                    <td>Special Notes </td>
                                            <td>
                                                <asp:TextBox ID="txtSpecialNotes" runat="server" CssClass="form-control-file" TextMode="MultiLine" TabIndex="34" ValidationGroup="VG1"></asp:TextBox></td>
                                            <Ajx:FilteredTextBoxExtender ID="FTB_txtSpecialNotes" runat="server" TargetControlID="txtSpecialNotes" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ./[](),\-@:;"></Ajx:FilteredTextBoxExtender>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>Supplier Phone</td>
                                            <td>
                                                <asp:TextBox ID="txtSupplierPhone" runat="server" CssClass="form-control-file" TabIndex="18" MaxLength="15" ValidationGroup="VG1"></asp:TextBox></td>
                                            <Ajx:FilteredTextBoxExtender ID="FTB_txtSupplierPhone" runat="server" TargetControlID="txtSupplierPhone" FilterMode="ValidChars" FilterType="Custom,Numbers" ValidChars="+-;"></Ajx:FilteredTextBoxExtender>

                                            </td>
                                                   
                                                   
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </asp:Panel>

                        <div class="table-responsive card-box">
                            <table class="table table-borderless table_font_sm" id="tblPRRQ2">
                                <thead class="border-bottom" runat="server">
                                    <tr class="border-bottom">
                                        <th colspan="2">Add Indent Item Details</th>
                                        <%--<asp:Label ID="lblIndent" runat="server" CssClass="msgboard"></asp:Label>--%>
                                        <th colspan="2" class="text-right"><code>*</code> are mandatory fields</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Category  <code>*</code></td>
                                        <td>
                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control-file" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true" TabIndex="35" ValidationGroup="VG2">
                                                <asp:ListItem Value="0" Selected="True" Enabled="false">- SELECT -</asp:ListItem>

                                                <asp:ListItem Value="AMC" Enabled="false">AMC</asp:ListItem>
                                                <asp:ListItem Value="Asset" Enabled="false">Asset</asp:ListItem>
                                                <asp:ListItem Value="Product" Enabled="false">Product</asp:ListItem>
                                                <asp:ListItem Value="Project" Enabled="false">Project</asp:ListItem>
                                                <asp:ListItem Value="Service" Enabled="false">Service</asp:ListItem>

                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RFV_ddlCategory" runat="server" ErrorMessage="Please select Category" ForeColor="Red" ControlToValidate="ddlCategory" ValidationGroup="VG2" InitialValue="0" Text="*"></asp:RequiredFieldValidator>
                                            <%--<Ajx:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RFV_ddlCategory" HighlightCssClass="validatorCalloutHighlight"></Ajx:ValidatorCalloutExtender>--%>
                                            <td>Currency <code>*</code></td>
                                            <td>
                                                <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="form-control-file " TabIndex="41" ValidationGroup="VG2">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlCurrency" ErrorMessage="Please select Currency" Text="*" ForeColor="Red" InitialValue="0" ValidationGroup="VG2"></asp:RequiredFieldValidator>
                                                <%--<Ajx:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator16" HighlightCssClass="validatorCalloutHighlight"></Ajx:ValidatorCalloutExtender>--%>
                                            </td>


                                        </td>

                                    </tr>

                                    <tr>
                                        <td>Sub Category <code>*</code></td>
                                        <td>
                                            <asp:DropDownList ID="ddlItemDescription" runat="server" AutoPostBack="true" CssClass="form-control-file" OnSelectedIndexChanged="ddlItemDescription_SelectedIndexChanged" TabIndex="36" ValidationGroup="VG2">
                                            </asp:DropDownList>
                                            <Ajx:ListSearchExtender ID="LSE_ddlItemDescription" runat="server" IsSorted="true" PromptCssClass="PromptCSSClass" PromptPosition="Top" PromptText="Search Item Desc" QueryPattern="Contains" TargetControlID="ddlItemDescription">
                                            </Ajx:ListSearchExtender>
                                            <asp:TextBox ID="txtItemDesc" runat="server" CssClass="form-control-file" Enabled="false" MaxLength="40" placeholder="Maximum upto 40 Characters" TabIndex="37" ValidationGroup="VG2" Visible="false"></asp:TextBox>
                                            <br />
                                            <asp:RequiredFieldValidator ID="RFV_ddlItemDescription" runat="server" ControlToValidate="ddlItemDescription" ErrorMessage="Please select Item Description" Text="*" ForeColor="Red" InitialValue="0" ValidationGroup="VG2"></asp:RequiredFieldValidator>
                                            <%--<Ajx:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RFV_ddlItemDescription" HighlightCssClass="validatorCalloutHighlight"></Ajx:ValidatorCalloutExtender>--%>
                                            <asp:RequiredFieldValidator ID="RFV_txtItemDesc" runat="server" ControlToValidate="txtItemDesc" Enabled="false" ErrorMessage="Please enter Item Description" Text="*" ForeColor="Red" ValidationGroup="VG2"></asp:RequiredFieldValidator>
                                            <%--<Ajx:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RFV_txtItemDesc" HighlightCssClass="validatorCalloutHighlight"></Ajx:ValidatorCalloutExtender>--%>
                                            <Ajx:FilteredTextBoxExtender ID="FTB_txtItemDesc" runat="server" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" TargetControlID="txtItemDesc" ValidChars=" ./[]()amp;,\-@:;">
                                            </Ajx:FilteredTextBoxExtender>

                                        </td>
                                        <td>Per Unit Price(Exclusive of Taxes)</td>
                                        <td>
                                            <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control-file" MaxLength="12" TabIndex="42" ValidationGroup="VG2"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtUnitPrice" ErrorMessage="Please enter Unit Price" Text="*" ForeColor="Red" ValidationGroup="VG2"></asp:RequiredFieldValidator>
                                            <%--<Ajx:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator15" HighlightCssClass="validatorCalloutHighlight"></Ajx:ValidatorCalloutExtender>--%>
                                            <asp:RegularExpressionValidator ID="REVtxtUnitPrice" runat="server" ControlToValidate="txtUnitPrice" ErrorMessage="Please enter greater than zero" Text="*" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$" ValidationGroup="VG2"></asp:RegularExpressionValidator>
                                            <%--<Ajx:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="REVtxtUnitPrice" HighlightCssClass="validatorCalloutHighlight"></Ajx:ValidatorCalloutExtender>--%>
                                            <Ajx:FilteredTextBoxExtender ID="FTB_txtUnitPrice" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers" TargetControlID="txtUnitPrice" ValidChars=".">
                                            </Ajx:FilteredTextBoxExtender>
                                        </td>

                                        </td>
                                    </tr>

                                    <tr>
                                        <td>UOM <code>*</code></td>
                                        <td>
                                            <asp:DropDownList ID="ddlUnitOfMeasurement" runat="server" CssClass="form-control-file" TabIndex="38" ValidationGroup="VG2">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RFV_ddlUnitOfMeasurement" runat="server" ControlToValidate="ddlUnitOfMeasurement" InitialValue="0" ErrorMessage="Please select Unit" Text="*" ForeColor="Red" ValidationGroup="VG2"></asp:RequiredFieldValidator>
                                            <%--<Ajx:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RFV_ddlUnitOfMeasurement" HighlightCssClass="validatorCalloutHighlight"></Ajx:ValidatorCalloutExtender>--%>
                                        </td>
                                        <td>Taxable (%) </td>
                                        <td>
                                            <asp:TextBox ID="txtTaxable" runat="server" CssClass="form-control-file" TabIndex="43" MaxLength="12"></asp:TextBox>
                                            <Ajx:FilteredTextBoxExtender ID="FTB_txtTaxable" runat="server" TargetControlID="txtTaxable" FilterMode="ValidChars" FilterType="Numbers,Custom" ValidChars="."></Ajx:FilteredTextBoxExtender>
                                        </td>
                                        <%-- <td></td>--%>
                                    </tr>

                                    <tr>
                                        <td>Quantity  <code>*</code></td>
                                        <td>
                                            <asp:TextBox ID="txtNoOfUnits" runat="server" CssClass="form-control-file" TabIndex="39" MaxLength="6" ValidationGroup="VG2"></asp:TextBox>
                                            <Ajx:FilteredTextBoxExtender ID="FTB_txtNoOfUnits" runat="server" TargetControlID="txtNoOfUnits" FilterMode="ValidChars" FilterType="Numbers"></Ajx:FilteredTextBoxExtender>

                                            <asp:RequiredFieldValidator ID="RFV_txtNoOfUnits" runat="server" ControlToValidate="txtNoOfUnits" ErrorMessage="Please enter Quantity" Text="*" ForeColor="Red" ValidationGroup="VG2"></asp:RequiredFieldValidator>
                                            <%--<Ajx:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RFV_txtNoOfUnits" HighlightCssClass="validatorCalloutHighlight"></Ajx:ValidatorCalloutExtender>--%>
                                            <asp:RegularExpressionValidator ID="RE_txtNoOfUnits" runat="server" ControlToValidate="txtNoOfUnits" ErrorMessage="Please enter greater than zero" Text="*" ForeColor="Red" ValidationGroup="VG2" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"></asp:RegularExpressionValidator>
                                            <%--<Ajx:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RE_txtNoOfUnits" HighlightCssClass="validatorCalloutHighlight"></Ajx:ValidatorCalloutExtender>--%>
                                    </tr>

                                    <tr>
                                        <td>Item Description  <code>*</code></td>
                                        <td>
                                            <%--<asp:TextBox ID="txtPartNo" runat="server" CssClass="form-control-file" TabIndex="51" Visible="false"></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="FTB_txtPartNo" runat="server" TargetControlID="txtPartNo" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ./\-"></Ajx:FilteredTextBoxExtender>--%>
                                            <asp:TextBox ID="txtItemNote" runat="server" CssClass="form-control-file" TextMode="MultiLine" TabIndex="40"></asp:TextBox>
                                            <Ajx:FilteredTextBoxExtender ID="FTB_txtItemNote" runat="server" TargetControlID="txtItemNote" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ./[](),\-@:;"></Ajx:FilteredTextBoxExtender>

                                            <asp:RequiredFieldValidator ID="RFV_txtItemNote" runat="server" ControlToValidate="txtItemNote" ErrorMessage="Please enter Item Description" Text="*" ForeColor="Red" ValidationGroup="VG2"></asp:RequiredFieldValidator>
                                            <%--<Ajx:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" TargetControlID="RFV_txtItemNote" HighlightCssClass="validatorCalloutHighlight"></Ajx:ValidatorCalloutExtender>--%>
                                        </td>
                                        <%--<td>Item Description  <code>*</code></td>--%>
                                        <%--                                                    <td></td>
                                                    <td>--%>
                                        <%--<asp:TextBox ID="txtItemNote" runat="server" CssClass="form-control-file" TextMode="MultiLine" TabIndex="40"></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="FTB_txtItemNote" runat="server" TargetControlID="txtItemNote" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ./[](),\-@:;"></Ajx:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RFV_txtItemNote" runat="server" ControlToValidate="txtItemNote" ErrorMessage="*" ForeColor="Red" ValidationGroup="VG2"></asp:RequiredFieldValidator>--%>
                                        <td>
                                            <asp:TextBox ID="txtPartNo" runat="server" CssClass="form-control-file" TabIndex="51" Visible="false"></asp:TextBox>
                                            <Ajx:FilteredTextBoxExtender ID="FTB_txtPartNo" runat="server" TargetControlID="txtPartNo" FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters" ValidChars=" ./\-"></Ajx:FilteredTextBoxExtender>
                                        </td>
                                        <td colspan="2">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnItemAdd" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" Text="Add Items" TabIndex="44" ValidationGroup="VG2" OnClick="btnItemAdd_Click" />
                                            <asp:Button ID="btnUpdateItems" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" Text="Update" TabIndex="45" Visible="false" ValidationGroup="VG2" OnClick="btnUpdateItems_Click" />
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

                        <%-- </asp:Panel>--%>
                    </div>
                    <%--<div class="mb-3 float-left">
                                <asp:Button ID="btnItemAdd" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" Text="Add Items" TabIndex="44" ValidationGroup="VG2" OnClick="btnItemAdd_Click" />
                                <asp:Button ID="btnUpdateItems" runat="server" CssClass="btn bg-dark waves-effect waves-light btn-std" Text="Update" TabIndex="45" Visible="false" ValidationGroup="VG2" OnClick="btnUpdateItems_Click" />
                            </div>--%>

                    <%-- <div class="alert alert-warning float-left ml-2 small" role="alert">
                                <i class="mdi mdi-alert-circle-outline mr-2"></i><b>In order to create, you have to add at least one line item</b>
                            </div>--%>
                    <%-- <div class="clearfix"></div>--%>

                    <!-- ////// Purchase Request Declaration //// -->

                    <div class="table-responsive card-box" id="dvlineitem" runat="server" visible="false">
                        <asp:GridView ID="grd_ItemInfo" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" DataKeyNames="id,BANFN_EXT,MTART,NO_OF_UNITS,UNIT_PRICE,TAXABLE,WAERS,MEINS,ITEM_NOTE,MATNR,TXZ01" Width="99%"
                            OnRowCommand="grd_ItemInfo_RowCommand" OnRowDeleting="grd_ItemInfo_RowDeleting" TabIndex="46" ShowFooter="true">
                            <%-- OnRowDataBound="grd_ItemInfo_RowDataBound">--%>
                            <Columns>




                                <asp:TemplateField HeaderText="Item No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>



                                </asp:TemplateField>



                                <asp:BoundField DataField="MTART" HeaderText="Category" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                                <%--<asp:BoundField DataField="TXZ01" HeaderText="Sub Category" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />--%>
                                <asp:TemplateField HeaderText="Sub Category" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <%--<%# returnSubTypeTxt("MATNR") %>--%>
                                        <%# Eval("MTART").ToString().Trim()=="Product"?returnSubTypeTxt(Eval("MATNR").ToString().Trim()) :Eval("TXZ01") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="NO_OF_UNITS" HeaderText="Quantity" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%--<%# ((Convert.ToDecimal(Eval("UNIT_PRICE")))*(Convert.ToDecimal(Eval("NO_OF_UNITS")))).ToString("#,##0.00") %>    <%# Eval("WAERS") %>--%>
                                        <%# ((Convert.ToDecimal(Eval("UNIT_PRICE")))).ToString("#,##0.00") %>    <%# Eval("WAERS") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price" />--%>
                                <asp:BoundField DataField="TAXABLE" HeaderText="Taxable" HeaderStyle-CssClass="right" ItemStyle-HorizontalAlign="right" />
                                <asp:BoundField DataField="WAERS" HeaderText="Currency" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />



                                <%--<asp:BoundField DataField="MEINS" HeaderText="UOM" />--%>
                                <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <%# Eval("MEINS") %>
                                    </ItemTemplate>
                                    <%--  <FooterTemplate>
                                                <asp:Label ID="lblTotaltxt" runat="server" />
                                            </FooterTemplate>--%>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Price" FooterStyle-CssClass="right" HeaderStyle-CssClass="right">
                                    <ItemTemplate>



                                        <%# ((Convert.ToDecimal(Eval("UNIT_PRICE")))*(Convert.ToDecimal(Eval("NO_OF_UNITS")))).ToString("#,##0.00") %>    <%# Eval("WAERS") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <%-- <FooterTemplate>
                                                <asp:Label ID="lblTotalAmt" runat="server" />
                                            </FooterTemplate>--%>
                                </asp:TemplateField>




                                <asp:TemplateField HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                         <asp:LinkButton ID="LbtnEditExptype" runat="server" CausesValidation="false" CssClass="btn btn-xs btn-warning " Title="EDIT"
                                            CommandName="EDITITEMS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Edit</asp:LinkButton>
                                        <asp:LinkButton ID="LbtnDeleteExp" runat="server" CausesValidation="false" CssClass="btn btn-xs btn-danger " Title="Delete" OnClientClick="javascript:return confirm('Do you want to delete this item?')"
                                            CommandName="DELETE" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Delete</asp:LinkButton>



                                        <%-- <asp:LinkButton ID="LbtnEditExptype" runat="server" CausesValidation="false" CssClass="btn btn-warning btn-xs waves-effect waves-light" Text="EDIT"
                                                    CommandName="EDITITEMS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>--%>
                                        <%--  </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                    </HeaderTemplate>
                                    <ItemTemplate>--%>
                                        <%--  <asp:LinkButton ID="LbtnDeleteExp" runat="server" CausesValidation="false" CssClass="btn btn-danger btn-xs waves-effect waves-light" Text="Delete" OnClientClick="javascript:return confirm('Do you want to delete this item?')"
                                                    CommandName="DELETE" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>--%>



                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <%-- <div class="table-responsive card-box" id="dvlineitem" runat="server" visible="false">
                                <asp:GridView ID="grd_ItemInfo" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" DataKeyNames="id,BANFN_EXT,MTART,NO_OF_UNITS,UNIT_PRICE,TAXABLE,WAERS,MEINS,ITEM_NOTE,MATNR,TXZ01" Width="99%"
                                    OnRowCommand="grd_ItemInfo_RowCommand" OnRowDeleting="grd_ItemInfo_RowDeleting" TabIndex="46" ShowFooter="true" >
                                    <Columns>


                                        <asp:TemplateField HeaderText="Item No">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:BoundField DataField="MTART" HeaderText="Category" />

                                        <asp:BoundField DataField="NO_OF_UNITS" HeaderText="No. of Units" />
                                        <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price" />
                                        <asp:BoundField DataField="TAXABLE" HeaderText="Taxable" />
                                        <asp:BoundField DataField="WAERS" HeaderText="Currency" />

                                        <asp:BoundField DataField="MEINS" HeaderText="Unit of Measurements" />
                                        <asp:TemplateField HeaderText="Total Price">
                                            <ItemTemplate>
                                               
                                                <%# ((Convert.ToDecimal(Eval("UNIT_PRICE")))*(Convert.ToDecimal(Eval("NO_OF_UNITS")))).ToString("#,##0.00") %>    <%# Eval("WAERS") %>
                                              
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                      
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnDeleteExp" runat="server" CausesValidation="false" CssClass="btn btn-danger btn-xs waves-effect waves-light" Text="Delete" OnClientClick="javascript:return confirm('Do you want to delete this item?')"
                                                    CommandName="DELETE" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                                <asp:LinkButton ID="LbtnEditExptype" runat="server" CausesValidation="false" CssClass="btn btn-warning btn-xs waves-effect waves-light" Text="EDIT"
                                                    CommandName="EDITITEMS" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>--%>
                    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.min.js"></script>
                    <script type="text/javascript" src="../../Scripts/JqblockUI.js"></script>
                    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>


                    <script type="text/javascript">



                        $(document).ready(function () {
                            Page = Sys.WebForms.PageRequestManager.getInstance();
                            Page.add_beginRequest(OnBeginRequest);
                            Page.add_endRequest(endRequest);

                            function OnBeginRequest(sender, args) {
                                // $.blockUI();
                                //$("select").searchable();
                            }
                            function endRequest(sender, args) {
                                //  $.unblockUI();
                                //$("select").searchable();
                            }

                        });



                        //$(document).ready(function () {
                        //    $("select").searchable();

                        //});





                        var ddlText, ddlValue, ddl, lblMesg;

                        function CacheItems() {
                            ddlText = new Array();
                            ddlValue = new Array();
                            ddl = document.getElementById("<%=ddlERPProjectCode.ClientID %>");

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



                    </script>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="Attachments" runat="server" visible="false" class="table-responsive card-box">
                                <span style="color: #00529b; font-style: italic">( Note: Please add all Indented Rows, before adding any associated attachments. )</span>
                                <table class="Tblcls01" id="PRAttchs">


                                    <asp:Panel ID="PnlBillable" runat="server" Visible="true" Enabled="true">
                                        <tr>
                                           <%-- <td colspan="2">Attachments--%>
                                             <td>Attachments
                                            </td>

                                            <td id="tdwarning" runat="server" visible="true">
                                                <div class="alert alert-warning float-left ml-2 small" role="alert">
                                                    <%-- id="dvlialert" runat="server" visible="false">--%>
                                                    <i class="mdi mdi-alert-circle-outline mr-2"></i><b>Please click on file name to download, verify and upload the same.</b>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>



                                            <td>Proposal</td>
                                            <td>
                                                <asp:RadioButtonList ID="RbtnListProposal" runat="server" CssClass="rbtnspc" RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="47" AutoPostBack="false" OnSelectedIndexChanged="RbtnListProposal_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>



                                                &nbsp;&nbsp;
               
                                                <asp:FileUpload ID="fuProposal" runat="server" TabIndex="48" />



                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                <asp:LinkButton ID="LbtnProposal" runat="server" OnClick="LbtnProposal_Click" />


                                                <asp:RequiredFieldValidator ID="RFV_fuProposal" runat="server" ControlToValidate="fuProposal" ErrorMessage="Please Select Proposal Document" Enabled="false" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>



                                        <tr>
                                            <td>Agreement</td>
                                            <td>
                                                <asp:RadioButtonList ID="RbtnListAgreement" runat="server" CssClass="rbtnspc" RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="49" AutoPostBack="false" OnSelectedIndexChanged="RbtnListAgreement_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>



                                                &nbsp;&nbsp;
               
                                                <asp:FileUpload ID="fuAgreement" runat="server" TabIndex="50" />




                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                                <asp:LinkButton ID="LbtnAgreement" runat="server" OnClick="LbtnAgreement_Click" />


                                                <asp:RequiredFieldValidator ID="RFV_fuAgreement" runat="server" ControlToValidate="fuAgreement" ErrorMessage="Please Select Agreement Document" Enabled="false" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>



                                        <tr>



                                            <td>Email Communication</td>
                                            <td>
                                                <asp:RadioButtonList ID="RbtnListEmailCommunication" runat="server" CssClass="rbtnspc" RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="51" AutoPostBack="false" OnSelectedIndexChanged="RbtnListEmailCommunication_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>



                                                &nbsp;&nbsp;
               
                                                <asp:FileUpload ID="fuEmailCommunication" runat="server" TabIndex="52" />
                                                <asp:Label ID="Label3" runat="server"></asp:Label>
                                                <asp:LinkButton ID="LbtnEmailCommunication" runat="server" OnClick="LbtnEmailCommunication_Click" />


                                                &nbsp;
               
                                                <asp:RequiredFieldValidator ID="RFV_fuEmailCommunication" runat="server" ControlToValidate="fuEmailCommunication" ErrorMessage="Please Select Email Communication Document" Enabled="false" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Invoice</td>
                                            <td>
                                                <asp:RadioButtonList ID="RbtnListInvoice" runat="server" CssClass="rbtnspc" RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="53" AutoPostBack="false" OnSelectedIndexChanged="RbtnListInvoice_SelectedIndexChanged">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>



                                                &nbsp;&nbsp;
               
                                                <asp:FileUpload ID="fuInvoice" runat="server" TabIndex="54" />



                                                <asp:Label ID="Label4" runat="server"></asp:Label>
                                                <asp:LinkButton ID="LbtnInvoice" runat="server" OnClick="LbtnInvoice_Click" />


                                                &nbsp;
             
                                                <asp:RequiredFieldValidator ID="RFV_fuInvoice" runat="server" ControlToValidate="fuInvoice" ErrorMessage="Please Select Invoice Document" Enabled="false" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>



                                    </asp:Panel>
                                </table>
                            </div>

                            <div class="mb-3">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" Text="Save" TabIndex="56" ValidationGroup="VG1" OnClick="btnSave_Click" />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn bg-brand-btn waves-effect waves-light" Text="Send for Approval" TabIndex="55" ValidationGroup="VG1" OnClick="btnSubmit_Click" />
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <%-- <asp:PostBackTrigger ControlID="fuProposal" />
                            <asp:PostBackTrigger ControlID="fuAgreement" />
                            <asp:PostBackTrigger ControlID="fuEmailCommunication" />
                            <asp:PostBackTrigger ControlID="fuInvoice" />--%>
                            <asp:PostBackTrigger ControlID="LbtnProposal" />
                            <asp:PostBackTrigger ControlID="LbtnAgreement" />
                            <asp:PostBackTrigger ControlID="LbtnEmailCommunication" />
                            <asp:PostBackTrigger ControlID="LbtnInvoice" />
                            <asp:PostBackTrigger ControlID="btnSave" />
                            <asp:PostBackTrigger ControlID="btnSubmit" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                    
                <!-- end Tab Panel-->

            </div>
            <!-- end row -->



        </asp:Panel>

        <%-- </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
                <asp:PostBackTrigger ControlID="btnSubmit" />
                <asp:PostBackTrigger ControlID="btnItemAdd" />
                <asp:PostBackTrigger ControlID="btnUpdateItems" />
                <asp:PostBackTrigger ControlID="grdPurchaseItemDetails" />
                <asp:PostBackTrigger ControlID="LbtnProposal" />
                <asp:PostBackTrigger ControlID="LbtnAgreement" />
                <asp:PostBackTrigger ControlID="LbtnEmailCommunication" />
                <asp:PostBackTrigger ControlID="LbtnInvoice" />
            </Triggers>
        </asp:UpdatePanel>--%>
    </div>
    <!-- container -->
  
</asp:Content>



