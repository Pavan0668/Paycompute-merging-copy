<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.Master" AutoEventWireup="true" CodeBehind="iExpense_Request.aspx.cs" Inherits="iEmpPower.UI.Benefits_Payment.iExpense_Request" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style type="text/css">
        .btn-block {
            color: white !important;
        }

        #ContentPlaceHolder1_MainContent_grdIexpdetails tr td:nth-child(8) a {
            width: 80px !important;
        }

        #ContentPlaceHolder1_MainContent_grdAppRejIexp tr td:nth-child(11) a {
            width: 85px !important;
        }

        #ContentPlaceHolder1_MainContent_grdAppRejIexpC tr td:nth-child(11) a {
            width: 85px !important;
        }
    </style>
    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">iExpense Request</li>
                    </ol>
                </div>
                <h4 class="page-title">iExpense&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>
                        <asp:Label ID="LblMsg" runat="server" Font-Size="Medium"></asp:Label>
                        <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" Font-Size="Medium"></asp:Label>
                    </span></h4>
            </div>
        </div>
    </div>
    <!-- end page title -->
    <div class="row">
        <%--        <ul class="nav nav-pills navtab-bg" style="margin-left: 6px;">
            <li class="nav-item font-16"><a href="#Tab-1" data-toggle="tab" aria-expanded="false" class="nav-link active p-2">
                <i class="fe-bell noti-icon"></i>My PR Tickets </a></li>
            <li class="nav-item font-16"><a href="#Tab-2" data-toggle="tab" aria-expanded="true" class="nav-link p-2">
                <i class="fe-file-text"></i>PR - Pending My Action  </a></li>
            <li class="nav-item font-16"><a href="#Tab-3" data-toggle="tab" aria-expanded="true" class="nav-link p-2">
                <i class="fe-eye"></i>PR - Completed My Action </a></li>
        </ul>--%>

        <ul class="nav nav-pills navtab-bg" style="margin-left: 6px;">
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab1" class="nav-link active p-2" OnClick="Tab1_Click"><i class="fe-bell noti-icon"></i>
    My iExpense Requests </asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab2" class="nav-link p-2" OnClick="Tab2_Click"><i class="fe-file-text"></i>
   iExpense - Pending My Action </asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab3" class="nav-link p-2" OnClick="Tab3_Click"><i class="fe-eye"></i>
   iExpense - Completed My Action </asp:LinkButton></li>
        </ul>

        <!-- Tab Panel Start / -->
        <div class="col-xl-12 m-t-20">
            <div class="tab-content m-0 p-0">
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">

                        <%--   <div class="tab-pane active" id="Tab-1">--%>
                        <div id="Tab-1">
                            <div class="table-responsive card-box">

                                <table class="table table-sm table-borderless mb-0 table_font_sm">
                                    <tbody>
                                        <tr>
                                            <td colspan="4">
                                                <h5 class="header-title">My iExpense Requests</h5>
                                            </td>
                                            <td colspan="4">
                                                <a href="Other_Reimbursements.aspx">
                                                    <button type="button" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right">
                                                        <i class="mdi mdi-plus"></i>Add New iExpense Request</button></a>
                                                <%--<asp:Button ID="btnNewPR" runat="server"  class="btn btn-sm bg-brand-btn waves-effect waves-light float-right" Text="+ Add New Purchase Request" OnClick="btnNewPR_Click" />--%>
                                            </td>
                                        </tr>

                                        <tr class="border-top">
                                            <td width="60">Show</td>
                                            <td width="60">
                                                <%--<select name="" aria-controls="" class="">
                                                    <option value="10">10</option>
                                                    <option value="25">25</option>
                                                    <option value="50">50</option>
                                                    <option value="100">100</option>
                                                </select>--%>
                                                <asp:DropDownList ID="ddlPagesizeEmp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPagesizeEmp_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">10</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td width="104">Tickets Visibility</td>
                                            <td width="250">
                                                <div class="btn-group mb-2">
                                                    <%-- <button type="button" class="btn btn-xs btn-secondary">All </button>
                                                    <button type="button" class="btn btn-xs btn-light">Current Month</button>
                                                    <button type="button" class="btn btn-xs btn-light">Last Month</button>--%>
                                                    <asp:Button ID="btnAll" Text="All" runat="server" CssClass="btn btn-xs btn-secondary" OnClick="btnAll_Click" />
                                                    <asp:Button ID="btnCurrentMonth" Text="Current Month" runat="server" CssClass="btn btn-xs btn-light" OnClick="btnCurrentMonth_Click" />
                                                    <asp:Button ID="btnLastMonth" Text="Last Month" runat="server" CssClass="btn btn-xs btn-light" OnClick="btnLastMonth_Click" />
                                                </div>
                                            </td>
                                            <td width="50" align="right">Search:</td>
                                            <td width="300">
                                                <%-- <input type="text" class="form-control-file" placeholder="" aria-controls="">--%>
                                                <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" placeholder="Expense Id" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtsearch"></Ajx:FilteredTextBoxExtender>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--	<table class="table table-striped table-sm mb-0 table_font_sm">
	<thead>
	<tr>
	<th>#</th>	
	<th>PR No</th>
	<th>Indentor</th>
	<!--th>Supplier</th-->
	<th>In Budget</th>
	<th>Criticality</th>
	<th>Project Code</th>
	<th>Capex</th>
	<th class="text-right">Total Amount</th>
	<th>Currency</th>
	<th class="text-right">Total Amount (INR)</th>
	<th class="text-right">Submitted On</th>
	<th class="text-right">Status</th>		
	<th class="text-right"><i class="dripicons-copy" data-toggle="tooltip" title="Copy as Template"></i></th>
	<th class="text-right"><i class="dripicons-article" data-toggle="tooltip" title="View Details"></i></th>
	</tr>
	</thead>
	<tbody>
	<tr>
	<td>1</td>	
	<th scope="row">634</th>
	<td>Shifaz K Mohammed</td>
	<!--td>Addteq</td-->
	<td>YES</td>
	<td>Medium</td>
	<td>I/10007-10</td>
	<td>NO</td>
	<td align="right">1,260.00</td>
	<td>USD</td>
	<td align="right">83,934.90</td>
	<td align="right">24-Aug-2016</td>
	<td align="right"><button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
	<td align="right"><a href="Purchase_Request_Form_Copy.html"><i class="dripicons-copy"></i></a></td>
	<td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
	</tr>
	<tr>	
	<td>2</td>	
	<th scope="row">634</th>
	<td>Shifaz K Mohammed</td>
	<!--td>Addteq</td-->
	<td>YES</td>
	<td>Medium</td>
	<td>I/10007-10</td>
	<td>NO</td>
	<td align="right">1,260.00</td>
	<td>USD</td>
	<td align="right">83,934.90</td>
	<td align="right">24-Aug-2016</td>
	<td align="right"><button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
	<td align="right"><a href="Purchase_Request_Form_Copy.html"><i class="dripicons-copy" ></i></a></td>
	<td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
	</tr>
	<tr>	
	<td>3</td>	
	<th scope="row">634</th>
	<td>Shifaz K Mohammed</td>
	<!--td>Addteq</td-->
	<td>YES</td>
	<td>Medium</td>
	<td>I/10007-10</td>
	<td>NO</td>
	<td align="right">1,260.00</td>
	<td>USD</td>
	<td align="right">83,934.90</td>
	<td align="right">24-Aug-2016</td>
	<td align="right"><button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
	<td align="right"><a href="Purchase_Request_Form_Copy.html"><i class="dripicons-copy" ></i></a></td>
	<td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
	</tr>
	<tr>	
	<td>4</td>	
	<th scope="row">634</th>
	<td>Shifaz K Mohammed</td>
	<!--td>Addteq</td-->
	<td>YES</td>
	<td>Medium</td>
	<td>I/10007-10</td>
	<td>NO</td>
	<td align="right">1,260.00</td>
	<td>USD</td>
	<td align="right">83,934.90</td>
	<td align="right">24-Aug-2016</td>
	<td align="right"><button type="button" class="btn btn-xs btn-blue waves-effect waves-light" data-toggle="modal" data-target=".Approved">Closed</button></td>
	<td align="right"><a href="Purchase_Request_Form_Copy.html"><i class="dripicons-copy" ></i></a></td>
	<td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
	</tr>	
	<tr>	
	<td>5</td>	
	<th scope="row">634</th>
	<td>Shifaz K Mohammed</td>
	<!--td>Addteq</td-->
	<td>YES</td>
	<td>Medium</td>
	<td>I/10007-10</td>
	<td>NO</td>
	<td align="right">1,260.00</td>
	<td>USD</td>
	<td align="right">83,934.90</td>
	<td align="right">24-Aug-2016</td>
	<td align="right"><button type="button" class="btn btn-xs btn-blue waves-effect waves-light" data-toggle="modal" data-target=".Approved">Closed</button></td>
	<td align="right"><a href="Purchase_Request_Form_Copy.html"><i class="dripicons-copy" ></i></a></td>
	<td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
	</tr>
	<tr>	
	<td>7</td>	
	<th scope="row">634</th>
	<td>Shifaz K Mohammed</td>
	<!--td>Addteq</td-->
	<td>YES</td>
	<td>Medium</td>
	<td>I/10007-10</td>
	<td>NO</td>
	<td align="right">1,260.00</td>
	<td>USD</td>
	<td align="right">83,934.90</td>
	<td align="right">24-Aug-2016</td>
	<td align="right"><button type="button" class="btn btn-xs btn-blue waves-effect waves-light" data-toggle="modal" data-target=".Approved">Closed</button></td>
	<td align="right"><a href="Purchase_Request_Form_Copy.html"><i class="dripicons-copy" ></i></a></td>
	<td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
	</tr>
	<tr>	
	<td>8</td>	
	<th scope="row">634</th>
	<td>Shifaz K Mohammed</td>
	<!--td>Addteq</td-->
	<td>YES</td>
	<td>Medium</td>
	<td>I/10007-10</td>
	<td>NO</td>
	<td align="right">1,260.00</td>
	<td>USD</td>
	<td align="right">83,934.90</td>
	<td align="right">24-Aug-2016</td>
	<td align="right"><button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
	<td align="right"><a href="Purchase_Request_Form_Copy.html"><i class="dripicons-copy" ></i></a></td>
	<td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
	</tr>	
	<tr>	
	<td>9</td>	
	<th scope="row">634</th>
	<td>Shifaz K Mohammed</td>
	<!--td>Addteq</td-->
	<td>YES</td>
	<td>Medium</td>
	<td>I/10007-10</td>
	<td>NO</td>
	<td align="right">1,260.00</td>
	<td>USD</td>
	<td align="right">83,934.90</td>
	<td align="right">24-Aug-2016</td>
	<td align="right"><button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
	<td align="right"><a href="Purchase_Request_Form_Copy.html"><i class="dripicons-copy" ></i></a></td>
	<td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
	</tr>
	<tr>	
	<td>10</td>	
	<th scope="row">634</th>
	<td>Shifaz K Mohammed</td>
	<!--td>Addteq</td-->
	<td>YES</td>
	<td>Medium</td>
	<td>I/10007-10</td>
	<td>NO</td>
	<td align="right">1,260.00</td>
	<td>USD</td>
	<td align="right">83,934.90</td>
	<td align="right">24-Aug-2016</td>
	<td align="right"><button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
	<td align="right"><a href="Purchase_Request_Form_Copy.html"><i class="dripicons-copy" ></i></a></td>
	<td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
	</tr>	
	</tbody>
	</table>	
		
	<div class="clearfix">&nbsp;</div>	
	<table class="table table-borderless table-sm mb-0 table_font_sm">
	<tbody>
	<tr>
	<td>Showing 1 to 10 of 15 entries</td>
	<td>
		<ul class="pagination pagination-rounded float-right">
			<li class="paginate_button page-item previous disabled"><a href="#" class="page-link"><i class="mdi mdi-chevron-left"></i></a></li>
			<li class="paginate_button page-item active"><a href="#" class="page-link">1</a></li>
			<li class="paginate_button page-item "><a href="#" class="page-link">2</a></li>
			<li class="paginate_button page-item next"><a href="#" class="page-link"><i class="mdi mdi-chevron-right"></i></a></li>
		</ul>
	</td>
	</tr>
	</tbody>
	</table>	--%>

                                <asp:GridView ID="grdIexpdetails" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" OnRowCommand="grdIexpdetails_RowCommand"
                                    DataKeyNames="IEXP_ID,PURPOSE,CREATED_BY,ENAME,TASK,POST1,RE_AMT,RCURR,CREATED_ON" AllowPaging="true" AllowSorting="true" OnSorting="grdIexpdetails_Sorting"
                                    OnPageIndexChanging="grdIexpdetails_PageIndexChanging" Width="99%" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%--<%# Container.DataItemIndex + 1 %>--%>
                                                <%# Eval("RwNum") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IEXP_ID" HeaderText="Expense Id" HeaderStyle-Width="110" />
                                        <asp:BoundField DataField="POST1" HeaderText="Project" HeaderStyle-Width="250" />
                                        <%--   <asp:BoundField DataField="TASK" HeaderText="Task" SortExpression="TASK" />--%>

                                        <asp:TemplateField HeaderText="Task" HeaderStyle-Width="150">
                                            <ItemTemplate>

                                                <%#(Eval("TASK").ToString().Trim()=="B") ? "Billable" : "Non-Billable"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="RE_AMT" HeaderText="Total Reimbursement Amount" SortExpression="RE_AMT" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                        <asp:TemplateField HeaderText="Total Reimbursement Amount" HeaderStyle-Width="210" ItemStyle-CssClass="right" HeaderStyle-CssClass="right">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>

                                                <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>
                                                <%-- ( <%# Eval("WAERS") %>)--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="RCURR" HeaderText="Reimbursement Currency" HeaderStyle-Width="210" />
                                        <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" HeaderStyle-Width="130" />
                                        <%-- <asp:BoundField DataField="STATUS" HeaderText="Status" />--%>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved" ? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Saved" ?"btn btn-xs btn-warning waves-effect waves-light btn-block":Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6"||Eval("Status").ToString() == "Rejected7"||Eval("Status").ToString() == "Rejected8"||Eval("Status").ToString() == "Rejected9" ? "btn btn-xs btn-danger waves-effect waves-light" : "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved" ? "Closed" : Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected6")||(Eval("Status").ToString() == "Rejected7")||(Eval("Status").ToString() == "Rejected8")||(Eval("Status").ToString() == "Rejected9"))?"Rejected":" Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>
                                                <%--CssClass='<%# Eval("Status").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6" ? "btn btn-xs btn-danger waves-effect waves-light" :Eval("Status").ToString() == "Cancelled" ? "btn btn-xs btn-blue waves-effect waves-light" :Eval("Status").ToString() == "Saved" ||Eval("Status").ToString() == "HOLD1"||Eval("Status").ToString() == "HOLD2"||Eval("Status").ToString() == "HOLD3"||Eval("Status").ToString() == "HOLD4"||Eval("Status").ToString() == "HOLD5"||Eval("Status").ToString() == "HOLD6" ?"btn btn-xs btn-warning waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>'--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <i class="fe-edit-1" data-toggle="tooltip" title="Edit"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnEmpPurchaseItemEdit" runat="server" CssClass="fe-edit-1" CausesValidation="False" CommandName="Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Visible='<%# Eval("Status").ToString() == "Saved" ? true : false %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i class="dripicons-copy marminus3lefts" data-toggle="tooltip" title="Copy as Template"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%-- <asp:LinkButton ID="LbtnEmpPurchaseItemCopy" runat="server" CssClass="dripicons-copy" CausesValidation="False" CommandName="Copy" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><%# Eval("STATUS").ToString() == "Saved" ? "Edit" : "Copy" %></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="dripicons-cancel" CommandName="Cancel" OnClientClick="return confirm('Do you want to Cancel this PR?');" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><%# Eval("STATUS").ToString() == "Saved" ? "Cancel" : "" %></asp:LinkButton>--%>

                                                <asp:LinkButton ID="LbtnEmpPurchaseItemCopy" runat="server" CssClass='<%# Eval("Status").ToString() == "Saved" ? "" : "dripicons-copy" %>' data-toggle="tooltip" title='<%# Eval("Status").ToString() == "Saved" ? "" : "Copy as Template" %>' CausesValidation="False" CommandName="Copy" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>' Text='<%# Eval("Status").ToString() == "Saved" ? "NA" : "" %>'></asp:LinkButton>
                                                <%--<asp:LinkButton ID="LbtnEmpPurchaseItemCopy1" runat="server" CssClass="dripicons-copy"  data-toggle="tooltip" title="Copy as Template" CausesValidation="False" CommandName="Copy" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i class="fe-edit-1 marminus3lefts" data-toggle="tooltip" title="Edit"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnEmpPurchaseItemEdit" runat="server" CssClass='<%# Eval("Status").ToString() == "Saved"||Eval("Status").ToString() == "Requested" ? "fe-edit-1" : "" %>' data-toggle="tooltip" title="Edit" CausesValidation="False" CommandName="Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved"||Eval("Status").ToString() == "Requested" ? true : false %>' Text='<%# Eval("Status").ToString() == "Saved"||Eval("Status").ToString() == "Requested" ? "" : "NA" %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i class="dripicons-article marminus3left" data-toggle="tooltip" title="View Details"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnIExpenseView" runat="server" CssClass="dripicons-article" data-toggle="tooltip" title="View Details" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                    <SelectedRowStyle BackColor="Silver" />
                                </asp:GridView>
                                <div class="row col-md-12">
                                    <div class="col-md-3" style="margin-top: 5px" id="divpendingrecordcount" runat="server"></div>
                                    <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                        <asp:Repeater ID="RptrPendingPager" runat="server">
                                            <ItemTemplate>
                                                <ul class="pagination pagination-rounded" style="display: inline-block">
                                                    <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <asp:Button runat="server" ID="btnShowModalPopup" Style="display: none" />
                                <Ajx:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                                    TargetControlID="btnShowModalPopup"
                                    PopupControlID="dvAppHistory"
                                    BackgroundCssClass="popUpStyle"
                                    DropShadow="false" />

                                <div id="divPopUp" class="modal fade bs-example-modal-lg" style="display: none;">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h4 class="modal-title" id="H1">PR Approve / Reject Request View </h4>
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">X</button>
                                            </div>
                                            <div class="modal-body">

                                                <%-- <asp:Button ID="btnClose" runat="server" Text="Close" />--%>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="close btn btn-light waves-effect" data-dismiss="modal">Close</button>
                                            </div>
                                        </div>
                                        <!-- /.modal-content -->
                                    </div>
                                    <!-- /.modal-dialog -->
                                </div>
                                <!-- end table-responsive-->

                            </div>
                        </div>

                    </asp:View>
                    <!-- =========== Tab-2 Tab Panel ==================-->
                    <asp:View ID="View2" runat="server">
                        <div id="Tab-2">
                            <div class="table-responsive card-box">

                                <table class="table table-sm table-borderless mb-0 table_font_sm">
                                    <tbody>
                                        <tr>
                                            <td colspan="5">
                                                <h5 class="header-title">Below iExpense Requisition's required your action to proceed</h5>
                                            </td>
                                            <!--td colspan="4" align="right">Indentor : Shifaz K Mohammed</td-->
                                        </tr>

                                        <tr class="border-top">
                                            <td width="60">Show</td>
                                            <td width="60">
                                                <%--<select name="" aria-controls="" class="">
                                                    <option value="10">10</option>
                                                    <option value="25">25</option>
                                                    <option value="50">50</option>
                                                    <option value="100">100</option>
                                                </select>--%>
                                                <asp:DropDownList ID="ddlPagesizeMgrP" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPagesizeMgrP_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">10</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td width="104">Tickets Visibility</td>
                                            <td width="250">
                                                <div class="btn-group mb-2">
                                                    <%--<button type="button" class="btn btn-xs btn-secondary">All </button>
                                                    <button type="button" class="btn btn-xs btn-light">Current Month</button>
                                                    <button type="button" class="btn btn-xs btn-light">Last Month</button>--%>
                                                    <asp:Button ID="btnAllMP" Text="All" runat="server" CssClass="btn btn-xs btn-secondary" OnClick="btnAllMP_Click" />
                                                    <asp:Button ID="btnCurrentMonthMP" Text="Current Month" runat="server" CssClass="btn btn-xs btn-light" OnClick="btnCurrentMonthMP_Click" />
                                                    <asp:Button ID="btnLastMonthMP" Text="Last Month" runat="server" CssClass="btn btn-xs btn-light" OnClick="btnLastMonthMP_Click" />
                                                </div>
                                            </td>
                                            <td width="50" align="right">Search:</td>
                                            <td width="300">
                                                <%--<input type="text" class="form-control-file" placeholder="" aria-controls="">--%>
                                                <asp:TextBox ID="txtSearchMP" runat="server" CssClass="form-control-file" placeholder="Expense Id" AutoPostBack="True" OnTextChanged="txtSearchMP_TextChanged"></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtSearchMP"></Ajx:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                                <%--        <table class="table table-striped table-sm mb-0 table_font_sm">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>PR No</th>
                                    <th>Requestor</th>
                                    <!--th>Supplier</th-->
                                    <th>In Budget</th>
                                    <th>Criticality</th>
                                    <th>Project Code</th>
                                    <th>Capex</th>
                                    <th>Total Amount</th>
                                    <th>Currency</th>
                                    <th class="text-right">Total Amount (INR)</th>
                                    <th class="text-right">Submitted On</th>
                                    <th class="text-right">Status</th>
                                    <th class="text-right"><i class="dripicons-article" data-toggle="tooltip" title="View Details"></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <th scope="row">634</th>
                                    <td>Shifaz K Mohammed</td>
                                    <!--td>Addteq</td-->
                                    <td>YES</td>
                                    <td>Medium</td>
                                    <td>I/10007-10</td>
                                    <td>NO</td>
                                    <td align="right">1,260.00</td>
                                    <td>USD</td>
                                    <td align="right">83,934.90</td>
                                    <td align="right">24-Aug-2016</td>
                                    <td align="right">
                                        <button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
                                    <td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <th scope="row">634</th>
                                    <td>Ajith T Abraham</td>
                                    <!--td>Addteq</td-->
                                    <td>YES</td>
                                    <td>Medium</td>
                                    <td>I/10007-10</td>
                                    <td>NO</td>
                                    <td align="right">1,260.00</td>
                                    <td>USD</td>
                                    <td align="right">83,934.90</td>
                                    <td align="right">24-Aug-2016</td>
                                    <td align="right">
                                        <button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
                                    <td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <th scope="row">634</th>
                                    <td>Ajith T Abraham</td>
                                    <!--td>Addteq</td-->
                                    <td>YES</td>
                                    <td>Medium</td>
                                    <td>I/10007-10</td>
                                    <td>NO</td>
                                    <td align="right">1,260.00</td>
                                    <td>USD</td>
                                    <td align="right">83,934.90</td>
                                    <td align="right">24-Aug-2016</td>
                                    <td align="right">
                                        <button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
                                    <td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <th scope="row">634</th>
                                    <td>Ajith T Abraham</td>
                                    <!--td>Addteq</td-->
                                    <td>YES</td>
                                    <td>Medium</td>
                                    <td>I/10007-10</td>
                                    <td>NO</td>
                                    <td align="right">1,260.00</td>
                                    <td>USD</td>
                                    <td align="right">83,934.90</td>
                                    <td align="right">24-Aug-2016</td>
                                    <td align="right">
                                        <button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
                                    <td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
                                </tr>
                                <tr>
                                    <td>5</td>
                                    <th scope="row">634</th>
                                    <td>Ajith T Abraham</td>
                                    <!--td>Addteq</td-->
                                    <td>YES</td>
                                    <td>Medium</td>
                                    <td>I/10007-10</td>
                                    <td>NO</td>
                                    <td align="right">1,260.00</td>
                                    <td>USD</td>
                                    <td align="right">83,934.90</td>
                                    <td align="right">24-Aug-2016</td>
                                    <td align="right">
                                        <button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
                                    <td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
                                </tr>
                                <tr>
                                    <td>6</td>
                                    <th scope="row">634</th>
                                    <td>Shifaz K Mohammed</td>
                                    <!--td>Addteq</td-->
                                    <td>YES</td>
                                    <td>Medium</td>
                                    <td>I/10007-10</td>
                                    <td>NO</td>
                                    <td align="right">1,260.00</td>
                                    <td>USD</td>
                                    <td align="right">83,934.90</td>
                                    <td align="right">24-Aug-2016</td>
                                    <td align="right">
                                        <button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
                                    <td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
                                </tr>
                            </tbody>
                        </table>

                        <div class="clearfix">&nbsp;</div>
                        <table class="table table-borderless table-sm mb-0 table_font_sm">
                            <tbody>
                                <tr>
                                    <td>Showing 1 to 10 of 15 entries</td>
                                    <td>
                                        <ul class="pagination pagination-rounded float-right">
                                            <li class="paginate_button page-item previous disabled"><a href="#" class="page-link"><i class="mdi mdi-chevron-left"></i></a></li>
                                            <li class="paginate_button page-item active"><a href="#" class="page-link">1</a></li>
                                            <li class="paginate_button page-item "><a href="#" class="page-link">2</a></li>
                                            <li class="paginate_button page-item next"><a href="#" class="page-link"><i class="mdi mdi-chevron-right"></i></a></li>
                                        </ul>
                                    </td>
                                </tr>
                            </tbody>
                        </table>--%>

                                <asp:GridView ID="grdAppRejIexp" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" OnRowCommand="grdAppRejIexp_RowCommand"
                                    DataKeyNames="IEXP_ID,CREATED_BY,POST1,TASK,RE_AMT,RCURR" AllowPaging="true" AllowSorting="true"
                                    OnSorting="grdAppRejIexp_Sorting" OnPageIndexChanging="grdAppRejIexp_PageIndexChanging" OnRowDataBound="grdAppRejIexp_RowDataBound" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%-- <%# Container.DataItemIndex + 1 %>--%>
                                                <%# Eval("RwNum") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IEXP_ID" HeaderText="Expense Id" HeaderStyle-Width="110" />
                                        <asp:BoundField DataField="ENTITY" HeaderText="Employee Entity" />
                                        <asp:BoundField DataField="CREATED_BY" HeaderText="Employee ID" />
                                        <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="POST1" HeaderText="Project" HeaderStyle-Width="250" />

                                        <asp:TemplateField HeaderText="Task">
                                            <ItemTemplate>

                                                <%-- <%#(Eval("TASK").ToString().Trim()=="B") ? "Billable" : "Non-Billable"%>--%>
                                                <asp:Label ID="grdlblTask" runat="server" Text='<%#(Eval("TASK").ToString().Trim()=="B") ? "Billable" : "Non-Billable"%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Reimbursement Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>

                                                <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="RCURR" HeaderText="Reimbursement Currency" />


                                        <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:TemplateField HeaderText="Posting Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtStartDate" runat="server" Visible="false" ValidationGroup="vg3" Width="100px" Style="letter-spacing: 1px; background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px;"></asp:TextBox>
                                                <br />
                                                <Ajx:MaskedEditExtender ID="MEE_txtStartDate" runat="server" AcceptNegative="Left"
                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtStartDate" />
                                                <Ajx:CalendarExtender ID="CE_txtStartDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtStartDate" SelectedDate='<%#Bind("P_DATE") %>'>
                                                </Ajx:CalendarExtender>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i data-toggle="tooltip" title="Update"></i><%--class="fe-edit-1" --%>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnUpdatePdate" runat="server" Visible="false" CausesValidation="false" CommandName="UpdatePdate" ValidationGroup="vg3" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Text="Update"></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="STATUS" HeaderText="Status" />--%>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved" ? "btn btn-xs btn-blue waves-effect waves-light" : "btn btn-xs btn-success waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved" ? "Closed" : " Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved" ? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Saved" ?"btn btn-xs btn-warning waves-effect waves-light btn-block":Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6"||Eval("Status").ToString() == "Rejected7"||Eval("Status").ToString() == "Rejected8"||Eval("Status").ToString() == "Rejected9" ? "btn btn-xs btn-danger waves-effect waves-light" : "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved" ? "Closed" : Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected6")||(Eval("Status").ToString() == "Rejected7")||(Eval("Status").ToString() == "Rejected8")||(Eval("Status").ToString() == "Rejected9"))?"Rejected":" Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>
                                                <%--Text='<%# Eval("Status").ToString() == "Approved" ? "Closed" : Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected6")||(Eval("Status").ToString() == "Rejected7")||(Eval("Status").ToString() == "Rejected8")||(Eval("Status").ToString() == "Rejected9"))?"Rejected":" Open "%>'--%>
                                                <%-- CssClass='<%# Eval("Status").ToString() == "Approved" ? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Saved" ?"btn btn-xs btn-warning waves-effect waves-light btn-block":Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6"||Eval("Status").ToString() == "Rejected7"||Eval("Status").ToString() == "Rejected8"||Eval("Status").ToString() == "Rejected9" ? "btn btn-xs btn-danger waves-effect waves-light" : "btn btn-xs btn-blue waves-effect waves-light" %>'--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i class="dripicons-article marminus3left" data-toggle="tooltip" title="View Details"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnIExpenseView" runat="server" CssClass="dripicons-article" data-toggle="tooltip" title="View Details" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-CssClass="marminus3left" ItemStyle-HorizontalAlign="Left">
                                            <HeaderTemplate>
                                                <asp:CheckBox runat="server" ID="masschkhead" Text=" " CssClass="marminus3left" OnCheckedChanged="masschkhead_CheckedChanged" AutoPostBack="true" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="masschkrow" Text=" " CssClass="" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                    <SelectedRowStyle BackColor="Silver" />
                                </asp:GridView>
                                <div class="row col-md-12">
                                    <div class="col-md-3" style="margin-top: 5px" id="div1" runat="server"></div>
                                    <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                        <asp:Repeater ID="RepeatrPRAppPending" runat="server">
                                            <ItemTemplate>
                                                <ul class="pagination pagination-rounded" style="display: inline-block">
                                                    <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                        <asp:LinkButton ID="LnkPgPRAppPending" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="LnkPgPRAppPending_Click" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <asp:Panel ID="pnlmassbtn" runat="server">
                                    <asp:Button ID="btnMassApprove" runat="server" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" Text="Approve" Width="120px" TabIndex="14" OnClick="btnMassApprove_Click" ValidationGroup="vg2" />
                                    <asp:Button ID="btnMassReject" runat="server" CssClass="btn bg-danger waves-effect waves-light btn-std" Text="Reject" Width="120px" TabIndex="15" OnClick="btnMassReject_Click" ValidationGroup="vg2" />




                                </asp:Panel>
                                <asp:Button runat="server" ID="btnShowModalPopup2" Style="display: none" />
                                <Ajx:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                                    TargetControlID="btnShowModalPopup2"
                                    PopupControlID="dvAppHistory"
                                    BackgroundCssClass="popUpStyle"
                                    DropShadow="false" />
                                <!-- end table-responsive-->
                                <asp:HiddenField ID="HFExpID" runat="server" />
                                <asp:HiddenField ID="HF_Pdate" runat="server" />
                            </div>
                        </div>

                    </asp:View>
                    <!-- =========== end Payslip Tab Panel ===========-->

                    <!-- =========== Tab-3 Tab Panel ==================-->
                    <asp:View ID="View3" runat="server">

                        <div id="Tab-3">
                            <div class="table-responsive card-box">

                                <table class="table table-sm table-borderless mb-0 table_font_sm">
                                    <tbody>
                                        <tr>
                                            <td colspan="5">
                                                <h5 class="header-title">Below iExpense Requisitions are completed your level of action</h5>
                                            </td>
                                            <!--td colspan="4" align="right">Indentor : Shifaz K Mohammed</td-->
                                        </tr>

                                        <tr class="border-top">
                                            <td width="60">Show</td>
                                            <td width="60">
                                                <%--<select name="" aria-controls="" class="">
                                                    <option value="10">10</option>
                                                    <option value="25">25</option>
                                                    <option value="50">50</option>
                                                    <option value="100">100</option>
                                                </select>--%>
                                                <asp:DropDownList ID="ddlPagesizeMgrC" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPagesizeMgrC_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True">10</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>100</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td width="104">Tickets Visibility</td>
                                            <td width="250">
                                                <div class="btn-group mb-2">
                                                    <%--<button type="button" class="btn btn-xs btn-secondary">All </button>
                                                    <button type="button" class="btn btn-xs btn-light">Current Month</button>
                                                    <button type="button" class="btn btn-xs btn-light">Last Month</button>--%>
                                                    <asp:Button ID="btnAllMC" Text="All" runat="server" CssClass="btn btn-xs btn-secondary" OnClick="btnAllMC_Click" />
                                                    <asp:Button ID="btnCurrentMonthMC" Text="Current Month" runat="server" CssClass="btn btn-xs btn-light" OnClick="btnCurrentMonthMC_Click" />
                                                    <asp:Button ID="btnLastMonthMC" Text="Last Month" runat="server" CssClass="btn btn-xs btn-light" OnClick="btnLastMonthMC_Click" />
                                                </div>
                                            </td>
                                            <td width="50" align="right">Search:</td>
                                            <td width="300">
                                                <%--<input type="text" class="form-control-file" placeholder="" aria-controls="">--%>
                                                <asp:TextBox ID="txtSearchMC" runat="server" CssClass="form-control-file" placeholder="Expense Id" AutoPostBack="True" OnTextChanged="txtSearchMC_TextChanged"></asp:TextBox>
                                                 <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtSearchMC"></Ajx:FilteredTextBoxExtender>
                                        </tr>
                                    </tbody>
                                </table>

                                <%-- <table class="table table-striped table-sm mb-0 table_font_sm">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>PR No</th>
                                    <th>Requestor</th>
                                    <!--th>Supplier</th-->
                                    <th>In Budget</th>
                                    <th>Criticality</th>
                                    <th>Project Code</th>
                                    <th>Capex</th>
                                    <th>Total Amount</th>
                                    <th>Currency</th>
                                    <th class="text-right">Total Amount (INR)</th>
                                    <th class="text-right">Submitted On</th>
                                    <th class="text-right">Status</th>
                                    >
	<th><i class="dripicons-article" data-toggle="tooltip" title="View Details"></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>1</td>
                                    <th scope="row">634</th>
                                    <td>Shifaz K Mohammed</td>
                                    <!--td>Addteq</td-->
                                    <td>YES</td>
                                    <td>Medium</td>
                                    <td>I/10007-10</td>
                                    <td>NO</td>
                                    <td align="right">1,260.00</td>
                                    <td>USD</td>
                                    <td align="right">83,934.90</td>
                                    <td align="right">24-Aug-2016</td>
                                    <td align="right">
                                        <button type="button" class="btn btn-xs btn-secondary waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">Closed</button></td>
                                    <td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <th scope="row">634</th>
                                    <td>Nagnath Tonne</td>
                                    <!--td>Addteq</td-->
                                    <td>YES</td>
                                    <td>Medium</td>
                                    <td>I/10007-10</td>
                                    <td>NO</td>
                                    <td align="right">1,260.00</td>
                                    <td>USD</td>
                                    <td align="right">83,934.90</td>
                                    <td align="right">24-Aug-2016</td>
                                    <td align="right">
                                        <button type="button" class="btn btn-xs btn-success waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">&nbsp;Open&nbsp;</button></td>
                                    <td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <th scope="row">634</th>
                                    <td>Nagnath Tonne</td>
                                    <!--td>Addteq</td-->
                                    <td>YES</td>
                                    <td>Medium</td>
                                    <td>I/10007-10</td>
                                    <td>NO</td>
                                    <td align="right">1,260.00</td>
                                    <td>USD</td>
                                    <td align="right">83,934.90</td>
                                    <td align="right">24-Aug-2016</td>
                                    <td align="right">
                                        <button type="button" class="btn btn-xs btn-secondary waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">Closed</button></td>
                                    <td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
                                </tr>
                                <tr>
                                    <td>4</td>
                                    <th scope="row">634</th>
                                    <td>Ganganga Gourda L N</td>
                                    <!--td>Addteq</td-->
                                    <td>YES</td>
                                    <td>Medium</td>
                                    <td>I/10007-10</td>
                                    <td>NO</td>
                                    <td align="right">1,260.00</td>
                                    <td>USD</td>
                                    <td align="right">83,934.90</td>
                                    <td align="right">24-Aug-2016</td>
                                    <td align="right">
                                        <button type="button" class="btn btn-xs btn-secondary waves-effect waves-light" data-toggle="modal" data-target=".bs-example-modal-lg">Closed</button></td>
                                    <td align="right"><a href="PR_View.html"><i class="dripicons-article"></i></a></td>
                                </tr>
                            </tbody>
                        </table>

                        <div class="clearfix">&nbsp;</div>
                        <table class="table table-borderless table-sm mb-0 table_font_sm">
                            <tbody>
                                <tr>
                                    <td>Showing 1 to 10 of 15 entries</td>
                                    <td>
                                        <ul class="pagination pagination-rounded float-right">
                                            <li class="paginate_button page-item previous disabled"><a href="#" class="page-link"><i class="mdi mdi-chevron-left"></i></a></li>
                                            <li class="paginate_button page-item active"><a href="#" class="page-link">1</a></li>
                                            <li class="paginate_button page-item "><a href="#" class="page-link">2</a></li>
                                            <li class="paginate_button page-item next"><a href="#" class="page-link"><i class="mdi mdi-chevron-right"></i></a></li>
                                        </ul>
                                    </td>
                                </tr>
                            </tbody>
                        </table>--%>

                                <asp:GridView ID="grdAppRejIexpC" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%"
                                    DataKeyNames="IEXP_ID,CREATED_BY,POST1,TASK,RE_AMT,RCURR" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="grdAppRejIexpC_PageIndexChanging" OnRowCommand="grdAppRejIexpC_RowCommand" OnRowDataBound="grdAppRejIexpC_RowDataBound" OnSorting="grdAppRejIexpC_Sorting" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%-- <%# Container.DataItemIndex + 1 %>--%>
                                                <%# Eval("RwNum") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IEXP_ID" HeaderText="Expense Id" />
                                        <asp:BoundField DataField="ENTITY" HeaderText="Employee Entity" />
                                        <asp:BoundField DataField="CREATED_BY" HeaderText="Employee ID" />
                                        <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />
                                        <asp:BoundField DataField="POST1" HeaderText="Project" />

                                        <asp:TemplateField HeaderText="Task">
                                            <ItemTemplate>

                                                <%#(Eval("TASK").ToString().Trim()=="B") ? "Billable" : "Non-Billable"%>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Total Reimbursement Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>

                                                <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="RCURR" HeaderText="Reimbursement Currency" />


                                        <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />

                                        <asp:TemplateField HeaderText="Posting Date">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtStartDate" runat="server" Visible="false" Enabled="false" ValidationGroup="vg3" Width="100px" Style="letter-spacing: 1px; background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px;"></asp:TextBox>
                                                <br />
                                                <Ajx:MaskedEditExtender ID="MEE_txtStartDate" runat="server" AcceptNegative="Left"
                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtStartDate" />
                                                <Ajx:CalendarExtender ID="CE_txtStartDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtStartDate" SelectedDate='<%#Bind("P_DATE") %>'>
                                                </Ajx:CalendarExtender>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField DataField="STATUS" HeaderText="Status" />--%>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%#  Eval("Status").ToString() == "Approved" ||Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6"||Eval("Status").ToString() == "Rejected7"||Eval("Status").ToString() == "Rejected8"||Eval("Status").ToString() == "Rejected9" ? "btn btn-xs btn-blue waves-effect waves-light" : "btn btn-xs btn-success waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved" ||Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6"||Eval("Status").ToString() == "Rejected7"||Eval("Status").ToString() == "Rejected8"||Eval("Status").ToString() == "Rejected9"  ? "Closed" : " Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved" ? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Saved" ?"btn btn-xs btn-warning waves-effect waves-light btn-block":Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6"||Eval("Status").ToString() == "Rejected7"||Eval("Status").ToString() == "Rejected8"||Eval("Status").ToString() == "Rejected9" ? "btn btn-xs btn-danger waves-effect waves-light" : "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved" ? "Closed" : Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected6")||(Eval("Status").ToString() == "Rejected7")||(Eval("Status").ToString() == "Rejected8")||(Eval("Status").ToString() == "Rejected9"))?"Rejected":" Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>
                                                <%--Text='<%# Eval("Status").ToString() == "Approved" ? "Closed" : Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected6")||(Eval("Status").ToString() == "Rejected7")||(Eval("Status").ToString() == "Rejected8")||(Eval("Status").ToString() == "Rejected9"))?"Rejected":" Open "%>'--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnUpdatePdate" runat="server" Visible="false" CausesValidation="false" CommandName="UpdatePdate" ValidationGroup="vg3" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Update</asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i style="margin-left: -3px" class="dripicons-article" data-toggle="tooltip" title="View Details"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnIExpenseView" runat="server" CssClass="dripicons-article" data-toggle="tooltip" title="View Details" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                    <SelectedRowStyle BackColor="Silver" />
                                </asp:GridView>
                                <div class="row col-md-12">
                                    <div class="col-md-3" style="margin-top: 5px" id="div2" runat="server"></div>
                                    <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                        <asp:Repeater ID="RepetrCompl" runat="server">
                                            <ItemTemplate>
                                                <ul class="pagination pagination-rounded" style="display: inline-block">
                                                    <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                        <asp:LinkButton ID="LnkPgPRAppCompl" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="LnkPgPRAppCompl_Click" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                                <asp:Button runat="server" ID="btnShowModalPopup3" Style="display: none" />
                                <Ajx:ModalPopupExtender ID="ModalPopupExtender3" runat="server"
                                    TargetControlID="btnShowModalPopup3"
                                    PopupControlID="dvAppHistory"
                                    BackgroundCssClass="popUpStyle"
                                    DropShadow="false" />
                                <!-- end table-responsive-->

                            </div>
                        </div>

                    </asp:View>
                </asp:MultiView>
            </div>
            <!-- end Tab Panel-->

        </div>
        <!-- end row -->


        <!--  Modal content for the above example -->
        <div id="dvAppHistory" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" style="display: none;" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="myLargeModalLabel">iExpense #<asp:Literal ID="ltClaimid" runat="server" Text="NA" />-Track Status</h4>
                        <%--  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>--%>
                        <asp:UpdatePanel ID="upclose" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnClose" runat="server" Text="x" CssClass="close" data-dismiss="modal" aria-hidden="true" CausesValidation="false" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div>
                        <%--	<table class="table table-sm table-bordered table_font_sm">
	<thead>
	<tr>
	<th>#</th>
	<th>Approver</th>
	<th>Date</th>	
	<th>Comments</th>
	<th>Approved</th>	
	</tr>
	</thead>	
	<tbody>
	<tr>
	<td>2</td>
	<td>00002632 - Shifaz K Mohammed </td>
	<td>24-08-2016 </td>	
	<td>OK</td>
	<td><span class="badge badge-success">Approved</span></td>
	</tr>
	<tr>
	<td>2</td>
	<td>00002632 - Shifaz K Mohammed </td>
	<td>24-08-2016 </td>	
	<td>OK</td>
	<td><span class="badge badge-success">Approved</span></td>
	</tr>	
	<tr>
	<td>3</td>
	<td>00002632 - Neville Collins </td>
	<td>24-08-2016 </td>	
	<td>OK</td>
	<td><span class="badge badge-warning">Pending</span></td>
	</tr>
	</tbody>
	</table>--%>
                        <asp:GridView ID="grdAppRejHistory" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" ShowHeader="False">

                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table class="table table-sm table-bordered table_font_sm">
                                            <tr>
                                                <th>Id</th>
                                                <th>Approver</th>

                                                <th>Date</th>
                                                <th>Comments</th>
                                                <th>Status</th>
                                            </tr>
                                            <asp:Panel ID="pnlAPPROVEDBY1" runat="server" Visible='<%# (Eval("APPROVED_BY1")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVED_BY1") %> </td>
                                                    <td><%#(Eval("APPROVED_BY1").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY1N")%></td>

                                                    <%--   <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": ""%>--%>
                                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%></td>--%>

                                                    <td><%#(Eval("APPROVED_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON1","{0:dd-MM-yyyy}")%></td>

                                                    <td><%# Eval("REMARKS1") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected1"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":(Eval("STATUS").ToString()=="HOLD1")?"Hold":(Eval("STATUS").ToString()=="RELEASED1")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span></td>
                                                    <%--<td><span class="{!IF(<%# Eval("STATUS").ToString()=="Approved1")%>,"badge badge-warning","badge badge-success")}"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":(Eval("STATUS").ToString()=="HOLD1")?"Hold":(Eval("STATUS").ToString()=="RELEASED1")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>--%>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlAPPROVEDBY2" runat="server" Visible='<%# (Eval("APPROVED_BY2")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVED_BY2") %></td>
                                                    <td><%#(Eval("APPROVED_BY2").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY2N") %></td>

                                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%></td>--%>


                                                    <td><%#(Eval("APPROVED_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON2","{0:dd-MM-yyyy}")%></td>

                                                    <td><%# Eval("REMARKS2") %></td>
                                                    <%--<td><span class="badge badge-success"><%# (Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Approved2")?"Approved":(Eval("STATUS").ToString()=="HOLD2")?"Hold":(Eval("STATUS").ToString()=="RELEASED2")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>--%>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected2"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"":(Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Approved2")?"Approved":(Eval("STATUS").ToString()=="HOLD2")?"Hold":(Eval("STATUS").ToString()=="RELEASED2")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel1" runat="server" Visible='<%# (Eval("APPROVED_BY3")).ToString()==""?false:true %>'>

                                                <tr>
                                                    <td><%# Eval("APPROVED_BY3") %></td>
                                                    <td><%# (Eval("APPROVED_BY3").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY3N") %></td>

                                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%></td>--%>
                                                    <td><%#(Eval("APPROVED_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON3","{0:dd-MM-yyyy}")%></td>


                                                    <td><%# Eval("REMARKS3") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected3"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved1")||(Eval("STATUS").ToString()=="Approved2")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"" :(Eval("STATUS").ToString()=="Rejected3")?"Rejected": (Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved3")?"Approved":(Eval("STATUS").ToString()=="HOLD3")?"Hold":(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED3")?"Released":(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel2" runat="server" Visible='<%# (Eval("APPROVED_BY4")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVED_BY4") %></td>
                                                    <td><%# (Eval("APPROVED_BY4").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY4N") %></td>

                                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%></td>--%>
                                                    <td><%#(Eval("APPROVED_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON4","{0:dd-MM-yyyy}")%></td>

                                                    <td><%# Eval("REMARKS4") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected4"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")||(Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected3")?"": (Eval("STATUS").ToString()=="Rejected4")?"Rejected": (Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved4")?"Approved":(Eval("STATUS").ToString()=="HOLD4")?"Hold":(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED4")?"Released":(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel3" runat="server" Visible='<%# (Eval("APPROVED_BY5")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVED_BY5") %></td>
                                                    <td><%# (Eval("APPROVED_BY5").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY5N") %></td>

                                                    <%--<td class="Tbltd"><%#(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%></td>--%>
                                                    <td><%#(Eval("APPROVED_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON5","{0:dd-MM-yyyy}")%></td>

                                                    <td><%# Eval("REMARKS5") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected5"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")||(Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected4")?"": (Eval("STATUS").ToString()=="Rejected5")?"Rejected": (Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved5")?"Approved":(Eval("STATUS").ToString()=="HOLD5")?"Hold":(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED5")?"Released":(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel4" runat="server" Visible='<%# (Eval("APPROVED_BY6")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVED_BY6") %></td>
                                                    <td><%# (Eval("APPROVED_BY6").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY6N") %></td>

                                                    <%--  <td class="Tbltd"><%#(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%></td>--%>
                                                    <td><%#(Eval("APPROVED_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON6","{0:dd-MM-yyyy}")%></td>

                                                    <td><%# Eval("REMARKS6") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected6"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")||(Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected5")?"": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":(Eval("STATUS").ToString()=="HOLD6")?"Hold":(Eval("STATUS").ToString()=="HOLD5")||(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED6")?"Released":(Eval("STATUS").ToString()=="RELEASED5")||(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel5" runat="server" Visible='<%# (Eval("APPROVED_BY7")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVED_BY7") %></td>
                                                    <td><%# (Eval("APPROVED_BY7").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY7N") %></td>

                                                    <td><%#(Eval("APPROVED_ON7","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON7","{0:dd-MM-yyyy}")%></td>
                                                    <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                    <td><%# Eval("REMARKS7") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"||Eval("STATUS").ToString()=="Approved6"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected7"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")||(Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel6" runat="server" Visible='<%# (Eval("APPROVED_BY8")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVED_BY8") %></td>
                                                    <td><%# (Eval("APPROVED_BY8").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY8") %></td>

                                                    <td><%#(Eval("APPROVED_ON8","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON8","{0:dd-MM-yyyy}")%></td>
                                                    <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                    <td><%# Eval("REMARKS8") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"||Eval("STATUS").ToString()=="Approved6"||Eval("STATUS").ToString()=="Approved7"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected8"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")||(Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel7" runat="server" Visible='<%# (Eval("APPROVED_BY9")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVED_BY9") %></td>
                                                    <td><%# (Eval("APPROVED_BY9").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY9") %></td>

                                                    <td><%#(Eval("APPROVED_ON9","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON9","{0:dd-MM-yyyy}")%></td>
                                                    <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                    <td><%# Eval("REMARKS9") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"||Eval("STATUS").ToString()=="Approved6"||Eval("STATUS").ToString()=="Approved7"||Eval("STATUS").ToString()=="Approved8"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected9"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")||(Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="APPROVEDBY1" HeaderText="Id" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Name" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Action" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Action Date" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Comments" />--%>
                            </Columns>

                        </asp:GridView>
                        <asp:GridView ID="grdApprovalHistory" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" ShowHeader="False">

                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <th>Id</th>
                                                <th>Approver</th>

                                                <th>Date</th>
                                                <th>Comments</th>
                                                <th>Approved</th>
                                            </tr>
                                            <asp:Panel ID="pnlAPPROVEDBY1" runat="server" Visible='<%# (Eval("APPROVED_BY")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVED_BY") %> </td>
                                                    <td><%#(Eval("APPROVED_BY").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY")%></td>

                                                    <%--   <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": ""%>--%>
                                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%></td>--%>

                                                    <td><%#(Eval("APPROVED_ON","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON","{0:dd-MM-yyyy}")%></td>

                                                    <td></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Saved"?"badge badge-warning":Eval("STATUS").ToString()=="Requested"?"badge badge-warning":Eval("STATUS").ToString()=="REJECT"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Saved")?"Saved":(Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected%")?"Rejected": (Eval("STATUS").ToString()=="Approved")?"Approved":""%></span></td>
                                                    <%--<td><span class="{!IF(<%# Eval("STATUS").ToString()=="Approved1")%>,"badge badge-warning","badge badge-success")}"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":(Eval("STATUS").ToString()=="HOLD1")?"Hold":(Eval("STATUS").ToString()=="RELEASED1")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>--%>
                                                </tr>
                                            </asp:Panel>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField DataField="APPROVEDBY1" HeaderText="Id" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Name" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Action" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Action Date" />
                                    <asp:BoundField DataField="APPROVEDBY1" HeaderText="Comments" />--%>
                            </Columns>

                        </asp:GridView>
                    </div>
                    <%-- <div class="modal-footer">
                        <asp:Button ID="Button1" runat="server" Text="Close" CssClass="close btn btn-light waves-effect" data-dismiss="modal" CausesValidation="false" />
                    </div>--%>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <!-- /.modal -->

        <!--  Modal content for the above example -->
        <div class="modal fade Approved" tabindex="-1" role="dialog" aria-labelledby=".Approved" style="display: none;" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title" id="Approved">PR Approved View </h4>
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    </div>
                    <div class="modal-body">
                        <table class="table table-sm table-bordered table_font_sm">
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Approver</th>
                                    <th>Date</th>
                                    <th>Comments</th>
                                    <th>Approved</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>2</td>
                                    <td>00002632 - Shifaz K Mohammed </td>
                                    <td>24-08-2016 </td>
                                    <td>OK</td>
                                    <td><span class="badge badge-success">Approved</span></td>
                                </tr>
                                <tr>
                                    <td>2</td>
                                    <td>00002632 - Shifaz K Mohammed </td>
                                    <td>24-08-2016 </td>
                                    <td>OK</td>
                                    <td><span class="badge badge-success">Approved</span></td>
                                </tr>
                                <tr>
                                    <td>3</td>
                                    <td>00002632 - Neville Collins </td>
                                    <td>24-08-2016 </td>
                                    <td>OK</td>
                                    <td><span class="badge badge-success">Approved</span></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="close btn btn-light waves-effect" data-dismiss="modal">Close</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <!-- /.modal -->



    </div>
    <asp:Panel ID="pnlhide" runat="server" Visible="true">
        <asp:GridView ID="grdIexpDetails1" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" DataKeyNames="ID,IEXP_ID,TASK">
            <Columns>
                <asp:TemplateField HeaderText="Sl No">
                    <EditItemTemplate>
                        <%# Eval("ID") %>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("ID") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expense Id">
                    <EditItemTemplate>
                        <%# Eval("IEXP_ID") %>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("IEXP_ID") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Entity">
                    <EditItemTemplate>
                        <%# Eval("ENTITY") %>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("ENTITY") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expense Type">
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="textbox">
                        </asp:DropDownList>
                        <Ajx:CascadingDropDown ID="CCD_ddlExpenseType" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Expensetype" SelectedValue='<%# Bind("EXP_TYPE") %>'
                            TargetControlID="ddlExpenseType" PromptText="SELECT " PromptValue="0" ServiceMethod="GetExpenseTypeIexpense" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("TASK") %>'>
                        </Ajx:CascadingDropDown>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("LGTXT") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expense Date">
                    <EditItemTemplate>
                        <%# Eval("S_DATE","{0:dd-MM-yyyy}") %>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("S_DATE","{0:dd-MM-yyyy}") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expenditure Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                    <EditItemTemplate>
                        <%# Convert.ToDouble(Eval("EXPT_AMT")).ToString("#,##0.00") %> ( <%# Eval("EXPT_CURR") %>)
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Convert.ToDouble(Eval("EXPT_AMT")).ToString("#,##0.00") %> ( <%# Eval("EXPT_CURR") %>)
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Exchange Rate" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                    <EditItemTemplate>
                        <%# Eval("EXC_RATE") %>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("EXC_RATE") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reimbursable Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                    <EditItemTemplate>
                        <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>  ( <%# Eval("RCURR") %>)
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>  ( <%# Eval("RCURR") %>)
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Justification">
                    <EditItemTemplate>
                        <%# Eval("JUSTIFY") %>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("JUSTIFY") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Original Receipt Missing">
                    <EditItemTemplate>
                        <%# Eval("RECEIPT_FILE") %>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("RECEIPT_FILE") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Attachments
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>' />

                        <asp:CheckBox ID="cb" runat="server" Text="Original Receipt Missing" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>' /><br />
                        <asp:FileUpload ID="fuAttachments" runat="server" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>' /><br />
                        <asp:Label ID="fuAttachmentsfname" runat="server" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:Label>
                        <asp:LinkButton ID="LbtnUpload" runat="server" Text="Upload" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            CommandName="Upload" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:LinkButton>
                        <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            CommandName="Delete" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateField>
                <asp:TemplateField Visible="false">

                    <EditItemTemplate>
                        <asp:LinkButton ID="LbtnUpdateIexp" runat="server" Text="Update Expense Tpye" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            CommandName="UPDATE IEXPENSE" CausesValidation="false"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>

                        <asp:LinkButton ID="LbtnEDIT" runat="server" Text="Edit Expense Type" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                            CommandName="EDIT" CausesValidation="false" Visible="true"></asp:LinkButton>

                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateField>

            </Columns>
            <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
            <SelectedRowStyle BackColor="Silver" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>
