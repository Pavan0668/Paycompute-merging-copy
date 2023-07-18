<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Purchase_Requisitions.aspx.cs" Inherits="iEmpPower.UI.PR.Purchase_Requisitions" Culture="en-GB" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .dvAppHistory{
            border: solid;
        }
        .btn-block {
            color: white !important;
        }

        #ContentPlaceHolder1_MainContent_grdPurchaseItemDetails tr td:nth-child(12) a {
            width: 85px !important;
        }

        #ContentPlaceHolder1_MainContent_grdPRAppRej tr td:nth-child(13) a {
            width: 85px !important;
        }

        #ContentPlaceHolder1_MainContent_grdPRAppRejC tr td:nth-child(13) a {
            width: 85px !important;
        }
    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.0/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.0/jquery.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
<script src="https://code.jquery.com/jquery-migrate-1.4.1.min.js"></script>

    <script>    
        // Get the close button element by its ID
        var closeButton = document.getElementById('<%= btnClose.ClientID %>');

// Add click event listener to the close button
closeButton.addEventListener('click', function() {
  // Get the modal element by its ID
    var modal = document.getElementById('<%= dvAppHistory.ClientID %>');

    // Hide the modal by removing the "show" class
    modal.classList.remove('show');

    // Update the aria-hidden attribute
    modal.setAttribute('aria-hidden', 'true');
});


    </script>

    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Purchase Requisitions</li>
                    </ol>
                </div>
                <h4 class="page-title">Purchase Request&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
    My PR Tickets </asp:LinkButton></li>

            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab2" class="nav-link p-2" OnClick="Tab2_Click"><i class="fe-file-text"></i>
   PR - Pending My Action </asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab3" class="nav-link p-2" OnClick="Tab3_Click"><i class="fe-eye"></i>
   PR - Completed My Action </asp:LinkButton></li>
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
                                                <h5 class="header-title">My Purchase Requisitions</h5>
                                            </td>
                                            <td colspan="4">
                                                <a href="Purchase_Request.aspx">
                                                    <button type="button" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right">
                                                        <i class="mdi mdi-plus"></i>Add New Purchase Request</button></a>
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
                                                    <%--<button type="button" class="btn btn-xs btn-secondary">All </button>
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
                                                <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" placeholder="PR No." AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtsearch"></Ajx:FilteredTextBoxExtender>
                                            </td>
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

                                <asp:GridView ID="grdPurchaseItemDetails" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" DataKeyNames="PRID" OnRowCommand="grdPurchaseItemDetails_RowCommand"
                                    AllowPaging="true" OnPageIndexChanging="grdPurchaseItemDeatils_PageIndexChanging"
                                    AllowSorting="true" OnSorting="grdPurchaseItemDeatils_Sorting" OnRowCancelingEdit="grdPurchaseItemDetails_RowCancelingEdit" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%--    <%# Container.DataItemIndex + 1 %>--%>
                                                <%#Eval("RowNum")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PRID" HeaderText="PR No." />
                                        <asp:BoundField DataField="IPERNR" HeaderText="Indentor" />
                                        <asp:BoundField DataField="RPERNR" HeaderText="Requestor" />
                                        <asp:BoundField DataField="SUG_SUPP" HeaderText="Supplier" Visible="false" />
                                        <asp:BoundField DataField="IN_BUDGET" HeaderText="In Budget" />
                                        <asp:BoundField DataField="CRITICALITY" HeaderText="Criticality" Visible="false" />
                                        <asp:BoundField DataField="PSPNR" HeaderText="Project Code" />
                                        <asp:BoundField DataField="BNFPO" HeaderText="Capex" />


                                        <asp:TemplateField HeaderText="Total Amount" HeaderStyle-CssClass="right">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#  Convert.ToDouble(Eval("UNIT_PRICE")).ToString("#,##0.00") %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="WAERS" HeaderText="Currency" />
                                        <asp:TemplateField HeaderText="Total Amount (INR)" HeaderStyle-CssClass="right">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#  Convert.ToDouble(Eval("TAINRAmt")).ToString("#,##0.00") %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="CREATED_ON1" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" HeaderStyle-Width="10%" />
                                        <%--<asp:BoundField DataField="STATUS" HeaderText="Status" />--%>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>

                                                <%--<asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved"||Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6" ? "btn btn-xs btn-blue waves-effect waves-light" :Eval("Status").ToString() == "Cancelled" ? "btn btn-xs btn-blue waves-effect waves-light" :Eval("Status").ToString() == "Saved" ?"btn btn-xs btn-warning waves-effect waves-light btn-block": "btn btn-xs btn-success waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved"?"Approved" : Eval("Status").ToString() == "Cancelled" ? "Closed" :  Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected"))?"Rejected":" Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'  Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6" ? "btn btn-xs btn-danger waves-effect waves-light" :Eval("Status").ToString() == "Cancelled" ? "btn btn-xs btn-blue waves-effect waves-light" :Eval("Status").ToString() == "Saved" ||Eval("Status").ToString() == "HOLD1"||Eval("Status").ToString() == "HOLD2"||Eval("Status").ToString() == "HOLD3"||Eval("Status").ToString() == "HOLD4"||Eval("Status").ToString() == "HOLD5"||Eval("Status").ToString() == "HOLD6" ?"btn btn-xs btn-warning waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved"?"Approved" : Eval("Status").ToString() == "Cancelled" ? "Cancelled" :  Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected"))?"Rejected":((Eval("Status").ToString() == "HOLD1") ||(Eval("Status").ToString() == "HOLD2") || (Eval("Status").ToString() == "HOLD3") ||(Eval("Status").ToString() == "HOLD4") ||(Eval("Status").ToString() == "HOLD5") ||(Eval("Status").ToString() == "HOLD"))?"Hold":((Eval("Status").ToString() == "RELEASED1") ||(Eval("Status").ToString() == "RELEASED2") || (Eval("Status").ToString() == "RELEASED3") ||(Eval("Status").ToString() == "RELEASED4") ||(Eval("Status").ToString() == "RELEASED5") ||(Eval("Status").ToString() == "RELEASED"))?"Released":" Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i class="dripicons-copy marminus3left" data-toggle="tooltip" title="Copy as Template"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%-- <asp:LinkButton ID="LbtnEmpPurchaseItemCopy" runat="server" CssClass="dripicons-copy" CausesValidation="False" CommandName="Copy" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><%# Eval("STATUS").ToString() == "Saved" ? "Edit" : "Copy" %></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="dripicons-cancel" CommandName="Cancel" OnClientClick="return confirm('Do you want to Cancel this PR?');" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><%# Eval("STATUS").ToString() == "Saved" ? "Cancel" : "" %></asp:LinkButton>--%>
                                                <asp:LinkButton ID="LbtnEmpPurchaseItemCopy" runat="server" CssClass='<%# Eval("Status").ToString() == "Saved" ? "" : "dripicons-copy" %>' data-toggle="tooltip" title='<%# Eval("Status").ToString() == "Saved" ? "" : "Copy as Template" %>' CausesValidation="False" CommandName="Copy" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>' Text='<%# Eval("Status").ToString() == "Saved" ? "NA" : "" %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i class="dripicons-article marminus3left" data-toggle="tooltip" title="View Details"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnEmpPurchaseItemView" runat="server" CssClass="dripicons-article" data-toggle="tooltip" title="View Details" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <i class="fe-edit-1" data-toggle="tooltip" title="Edit"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnEmpPurchaseItemEdit" runat="server" CssClass="fe-edit-1" CausesValidation="False" CommandName="Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Visible='<%# Eval("Status").ToString() == "Saved" ? true : false %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <i class="dripicons-cross" data-toggle="tooltip" title="Cancel"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnEmpPurchaseItemCancel" runat="server" CssClass="dripicons-cross" CausesValidation="False" CommandName="Cancel" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Visible='<%# Eval("Status").ToString() == "Saved" ? true : false %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i class="fe-edit-1 marminus3left" data-toggle="tooltip" title="Edit"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnEmpPurchaseItemEdit" runat="server" CssClass='<%# Eval("Status").ToString() == "Saved" ? "fe-edit-1" : "" %>' data-toggle="tooltip" title='<%# Eval("Status").ToString() == "Saved" ? "Edit" : "" %>' CausesValidation="False" CommandName="Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? true : false %>' Text='<%# Eval("Status").ToString() == "Saved" ? "" : "NA" %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i class="dripicons-cross marminus3left" data-toggle="tooltip" title="Cancel"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnEmpPurchaseItemCancel" runat="server" CssClass='<%# Eval("Status").ToString() == "Saved" ? "dripicons-cross" : "" %>' data-toggle="tooltip" title='<%# Eval("Status").ToString() == "Saved" ? "Cancel" : "" %>' CausesValidation="False" CommandName="Cancel" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? true : false %>' Text='<%# Eval("Status").ToString() == "Saved" ? "" : "NA" %>'></asp:LinkButton>
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
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                            </div>
                                            <div class="modal-body">

                                                <%-- <asp:Button ID="btnClose" runat="server" Text="Close" />--%>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-light waves-effect" data-dismiss="modal">Close</button>
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
                                                <h5 class="header-title">Below Purchase Requisition's required your action to proceed</h5>
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
                                                <asp:TextBox ID="txtSearchMP" runat="server" CssClass="form-control-file" placeholder="PR No." AutoPostBack="True" OnTextChanged="txtSearchMP_TextChanged"></asp:TextBox>
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

                                <asp:GridView ID="grdPRAppRej" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" OnRowCommand="grdPRAppRej_RowCommand"
                                    DataKeyNames="PRID,IN_BUDGET,MIS_GRPC,MIS_GRPA,MIS_GRPB,BWERKS,SWERKS,CAPITALIZED,CAP_TEXT,CREATEDBY,STATUS" AllowPaging="true" OnPageIndexChanging="grdPRAppRej_PageIndexChanging"
                                    AllowSorting="true" OnSorting="grdPRAppRej_Sorting" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%-- <%# Container.DataItemIndex + 1 %>--%>
                                                <%#Eval("RowNum")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PRID" HeaderText="PR No." />
                                        <asp:BoundField DataField="IPERNR" HeaderText="Indentor" />
                                        <asp:BoundField DataField="RPERNR" HeaderText="Requestor" />
                                        <asp:BoundField DataField="SUG_SUPP" HeaderText="Originator" />
                                        <asp:BoundField DataField="IN_BUDGET" HeaderText="In Budget" />
                                        <asp:BoundField DataField="CRITICALITY" HeaderText="Criticality" />
                                        <asp:BoundField DataField="BNFPO" HeaderText="Capex" />
                                        <asp:TemplateField HeaderText="Total Amount" HeaderStyle-CssClass="right">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("UNIT_PRICE")).ToString("#,##0.00") %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="WAERS" HeaderText="Currency" />
                                        <asp:TemplateField HeaderText="Total Amount (INR)" HeaderStyle-CssClass="right">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#  Convert.ToDouble(Eval("TAINRAmt")).ToString("#,##0.00") %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CREATED_ON1" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" HeaderStyle-Width="10%" />
                                        <%-- <asp:BoundField DataField="STATUS" HeaderText="Status" />--%>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%-- <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved"||Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6" ? "btn btn-xs btn-blue waves-effect waves-light" : "btn btn-xs btn-success waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved" ? "Closed" : " Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6" ? "btn btn-xs btn-danger waves-effect waves-light" :Eval("Status").ToString() == "Cancelled" ? "btn btn-xs btn-blue waves-effect waves-light" :Eval("Status").ToString() == "Saved" ||Eval("Status").ToString() == "HOLD1"||Eval("Status").ToString() == "HOLD2"||Eval("Status").ToString() == "HOLD3"||Eval("Status").ToString() == "HOLD4"||Eval("Status").ToString() == "HOLD5"||Eval("Status").ToString() == "HOLD6" ?"btn btn-xs btn-warning waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved"?"Approved" : Eval("Status").ToString() == "Cancelled" ? "Cancelled" :  Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected"))?"Rejected":((Eval("Status").ToString() == "HOLD1") ||(Eval("Status").ToString() == "HOLD2") || (Eval("Status").ToString() == "HOLD3") ||(Eval("Status").ToString() == "HOLD4") ||(Eval("Status").ToString() == "HOLD5") ||(Eval("Status").ToString() == "HOLD"))?"Hold":((Eval("Status").ToString() == "RELEASED1") ||(Eval("Status").ToString() == "RELEASED2") || (Eval("Status").ToString() == "RELEASED3") ||(Eval("Status").ToString() == "RELEASED4") ||(Eval("Status").ToString() == "RELEASED5") ||(Eval("Status").ToString() == "RELEASED"))?"Released":" Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>
                                                <%--CssClass='<%# Eval("Status").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6" ? "btn btn-xs btn-danger waves-effect waves-light" :Eval("Status").ToString() == "Cancelled" ? "btn btn-xs btn-blue waves-effect waves-light" :Eval("Status").ToString() == "Saved" ||Eval("Status").ToString() == "HOLD1"||Eval("Status").ToString() == "HOLD2"||Eval("Status").ToString() == "HOLD3"||Eval("Status").ToString() == "HOLD4"||Eval("Status").ToString() == "HOLD5"||Eval("Status").ToString() == "HOLD6" ?"btn btn-xs btn-warning waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>'--%>
                                                <%--  Text='<%# Eval("Status").ToString() == "Approved"?"Approved" : Eval("Status").ToString() == "Cancelled" ? "Cancelled" :  Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected"))?"Rejected":((Eval("Status").ToString() == "HOLD1") ||(Eval("Status").ToString() == "HOLD2") || (Eval("Status").ToString() == "HOLD3") ||(Eval("Status").ToString() == "HOLD4") ||(Eval("Status").ToString() == "HOLD5") ||(Eval("Status").ToString() == "HOLD"))?"Hold":((Eval("Status").ToString() == "RELEASED1") ||(Eval("Status").ToString() == "RELEASED2") || (Eval("Status").ToString() == "RELEASED3") ||(Eval("Status").ToString() == "RELEASED4") ||(Eval("Status").ToString() == "RELEASED5") ||(Eval("Status").ToString() == "RELEASED"))?"Released":" Open "%>'--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i class="dripicons-article marminus3left" data-toggle="tooltip" title="View Details"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnPurchaseItemView" runat="server" CssClass="dripicons-article" data-toggle="tooltip" title="View Details" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
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
                                                <h5 class="header-title">Below Purchase Requisitions are completed your level of action</h5>
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
                                                <asp:TextBox ID="txtSearchMC" runat="server" CssClass="form-control-file" placeholder="PR No." AutoPostBack="True" OnTextChanged="txtSearchMC_TextChanged"></asp:TextBox>
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

                                <asp:GridView ID="grdPRAppRejC" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" OnRowCommand="grdPRAppRejC_RowCommand"
                                    DataKeyNames="PRID,IN_BUDGET,MIS_GRPC,MIS_GRPA,MIS_GRPB,BWERKS,SWERKS,CAPITALIZED,CAP_TEXT,CREATEDBY,STATUS" AllowPaging="true" OnPageIndexChanging="grdPRAppRejC_PageIndexChanging"
                                    AllowSorting="true" OnSorting="grdPRAppRejC_Sorting" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <%--<%# Container.DataItemIndex + 1 %>--%>
                                                <%#Eval("RowNum")%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PRID" HeaderText="PR No." />
                                        <asp:BoundField DataField="IPERNR" HeaderText="Indentor" />
                                        <asp:BoundField DataField="RPERNR" HeaderText="Requestor" />
                                        <asp:BoundField DataField="SUG_SUPP" HeaderText="Originator" />
                                        <asp:BoundField DataField="IN_BUDGET" HeaderText="In Budget" />
                                        <asp:BoundField DataField="CRITICALITY" HeaderText="Criticality" />
                                        <asp:BoundField DataField="BNFPO" HeaderText="Capex" />
                                        <asp:TemplateField HeaderText="Total Amount" HeaderStyle-CssClass="right">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("UNIT_PRICE")).ToString("#,##0.00") %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="WAERS" HeaderText="Currency" />
                                        <asp:TemplateField HeaderText="Total Amount (INR)" HeaderStyle-CssClass="right">
                                            <EditItemTemplate>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <%#  Convert.ToDouble(Eval("TAINRAmt")).ToString("#,##0.00") %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CREATED_ON1" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" HeaderStyle-Width="10%" />
                                        <%-- <asp:BoundField DataField="STATUS" HeaderText="Status" />--%>
                                        <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%-- <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved" ||Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6" ? "btn btn-xs btn-blue waves-effect waves-light" : "btn btn-xs btn-success waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved" ||Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6" ? "Closed" : " Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>--%>
                                                <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected1"||Eval("Status").ToString() == "Rejected2"||Eval("Status").ToString() == "Rejected3"||Eval("Status").ToString() == "Rejected4"||Eval("Status").ToString() == "Rejected5"||Eval("Status").ToString() == "Rejected6" ? "btn btn-xs btn-danger waves-effect waves-light" :Eval("Status").ToString() == "Cancelled" ? "btn btn-xs btn-blue waves-effect waves-light" :Eval("Status").ToString() == "Saved" ||Eval("Status").ToString() == "HOLD1"||Eval("Status").ToString() == "HOLD2"||Eval("Status").ToString() == "HOLD3"||Eval("Status").ToString() == "HOLD4"||Eval("Status").ToString() == "HOLD5"||Eval("Status").ToString() == "HOLD6" ?"btn btn-xs btn-warning waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved"?"Approved" : Eval("Status").ToString() == "Cancelled" ? "Cancelled" :  Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected"))?"Rejected":((Eval("Status").ToString() == "HOLD1") ||(Eval("Status").ToString() == "HOLD2") || (Eval("Status").ToString() == "HOLD3") ||(Eval("Status").ToString() == "HOLD4") ||(Eval("Status").ToString() == "HOLD5") ||(Eval("Status").ToString() == "HOLD"))?"Hold":((Eval("Status").ToString() == "RELEASED1") ||(Eval("Status").ToString() == "RELEASED2") || (Eval("Status").ToString() == "RELEASED3") ||(Eval("Status").ToString() == "RELEASED4") ||(Eval("Status").ToString() == "RELEASED5") ||(Eval("Status").ToString() == "RELEASED"))?"Released":" Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Enabled='<%# Eval("Status").ToString() == "Saved" ? false : true %>'></asp:LinkButton>
                                                <%--  Text='<%# Eval("Status").ToString() == "Approved"?"Approved" : Eval("Status").ToString() == "Cancelled" ? "Cancelled" :  Eval("Status").ToString() == "Saved" ? "Saved" :((Eval("Status").ToString() == "Rejected1") ||(Eval("Status").ToString() == "Rejected2") || (Eval("Status").ToString() == "Rejected3") ||(Eval("Status").ToString() == "Rejected4") ||(Eval("Status").ToString() == "Rejected5") ||(Eval("Status").ToString() == "Rejected"))?"Rejected":((Eval("Status").ToString() == "HOLD1") ||(Eval("Status").ToString() == "HOLD2") || (Eval("Status").ToString() == "HOLD3") ||(Eval("Status").ToString() == "HOLD4") ||(Eval("Status").ToString() == "HOLD5") ||(Eval("Status").ToString() == "HOLD"))?"Hold":((Eval("Status").ToString() == "RELEASED1") ||(Eval("Status").ToString() == "RELEASED2") || (Eval("Status").ToString() == "RELEASED3") ||(Eval("Status").ToString() == "RELEASED4") ||(Eval("Status").ToString() == "RELEASED5") ||(Eval("Status").ToString() == "RELEASED"))?"Released":" Open "%>'--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <i class="dripicons-article marminus3left" data-toggle="tooltip" title="View Details"></i>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnPurchaseItemView" runat="server" CssClass="dripicons-article" data-toggle="tooltip" title="View Details" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
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
                        <h4 class="modal-title" id="myLargeModalLabel">PR #
                            <asp:Literal ID="ltPRid" runat="server" Text="NA" />-Track Status</h4>
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
                        <asp:GridView ID="grdEmpAppHistory" runat="server" AutoGenerateColumns="False" CssClass="" GridLines="None" DataKeyNames="APPROVEDBY1,APPROVEDBY2,APPROVEDBY3,APPROVEDBY4,APPROVEDBY5,APPROVEDBY6"
                            ShowHeader="False">

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
                                            <tr>
                                                <td><%# Eval("APPROVEDBY1") %> </td>
                                                <td><%# Eval("APPROVEDBY1N") %></td>

                                                <%--   <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": ""%>--%>
                                                <%-- <td class="Tbltd"><%#(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%></td>--%>

                                                <td><%# (Eval("STATUS").ToString()=="Approved1")?Eval("APP_ON1","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD1")?Eval("HOLD_ON1","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED1")?Eval("RELEASED_ON1","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected1")?Eval("APP_ON1","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%> </td>

                                                <td><%# Eval("COMMENTS1") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected1"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":(Eval("STATUS").ToString()=="HOLD1")?"Hold":(Eval("STATUS").ToString()=="RELEASED1")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span></td>
                                                <%--<td><span class="{!IF(<%# Eval("STATUS").ToString()=="Approved1")%>,"badge badge-warning","badge badge-success")}"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":(Eval("STATUS").ToString()=="HOLD1")?"Hold":(Eval("STATUS").ToString()=="RELEASED1")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>--%>
                                            </tr>
                                            <asp:Panel ID="pnlAPPROVEDBY2" runat="server" Visible='<%# (Eval("APPROVEDBY2")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVEDBY2") %></td>
                                                    <td><%# Eval("APPROVEDBY2N") %></td>

                                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%></td>--%>


                                                    <td><%# (Eval("STATUS").ToString()=="Approved2")?Eval("APP_ON2","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD2")?Eval("HOLD_ON2","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED2")?Eval("RELEASED_ON2","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected2")?Eval("APP_ON2","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%> </td>

                                                    <td><%# Eval("COMMENTS2") %></td>
                                                    <%--<td><span class="badge badge-success"><%# (Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Approved2")?"Approved":(Eval("STATUS").ToString()=="HOLD2")?"Hold":(Eval("STATUS").ToString()=="RELEASED2")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>--%>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected2"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Approved2")?"Approved":(Eval("STATUS").ToString()=="HOLD2")?"Hold":(Eval("STATUS").ToString()=="RELEASED2")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel1" runat="server" Visible='<%# (Eval("APPROVEDBY3")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVEDBY3") %></td>
                                                    <td><%# Eval("APPROVEDBY3N") %></td>

                                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%></td>--%>
                                                    <td><%# (Eval("STATUS").ToString()=="Approved3")?Eval("APP_ON3","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD3")?Eval("HOLD_ON3","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED3")?Eval("RELEASED_ON3","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected3")?Eval("APP_ON3","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%> </td>


                                                    <td><%# Eval("COMMENTS3") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected3"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected3")?"Rejected": (Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved3")?"Approved":(Eval("STATUS").ToString()=="HOLD3")?"Hold":(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED3")?"Released":(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel2" runat="server" Visible='<%# (Eval("APPROVEDBY4")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVEDBY4") %></td>
                                                    <td><%# Eval("APPROVEDBY4N") %></td>

                                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%></td>--%>
                                                    <td><%# (Eval("STATUS").ToString()=="Approved4")?Eval("APP_ON4","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD4")?Eval("HOLD_ON4","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED4")?Eval("RELEASED_ON4","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected4")?Eval("APP_ON4","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%> </td>

                                                    <td class="Tbltd"><%# Eval("COMMENTS4") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected4"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected4")?"Rejected": (Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved4")?"Approved":(Eval("STATUS").ToString()=="HOLD4")?"Hold":(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED4")?"Released":(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel3" runat="server" Visible='<%# (Eval("APPROVEDBY5")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVEDBY5") %></td>
                                                    <td><%# Eval("APPROVEDBY5N") %></td>

                                                    <%--<td class="Tbltd"><%#(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%></td>--%>
                                                    <td><%# (Eval("STATUS").ToString()=="Approved5")?Eval("APP_ON5","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD5")?Eval("HOLD_ON5","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED5")?Eval("RELEASED_ON5","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected5")?Eval("APP_ON5","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%> </td>

                                                    <td><%# Eval("COMMENTS5") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected5"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected5")?"Rejected": (Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved5")?"Approved":(Eval("STATUS").ToString()=="HOLD5")?"Hold":(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED5")?"Released":(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                                </tr>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel4" runat="server" Visible='<%# (Eval("APPROVEDBY6")).ToString()==""?false:true %>'>
                                                <tr>
                                                    <td><%# Eval("APPROVEDBY6") %></td>
                                                    <td><%# Eval("APPROVEDBY6N") %></td>

                                                    <%--  <td class="Tbltd"><%#(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%></td>--%>
                                                    <td><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>

                                                    <td><%# Eval("COMMENTS6") %></td>
                                                    <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"?"badge badge-warning":Eval("STATUS").ToString()=="Rejected6"?"badge badge-danger":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":(Eval("STATUS").ToString()=="HOLD6")?"Hold":(Eval("STATUS").ToString()=="HOLD5")||(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED6")?"Released":(Eval("STATUS").ToString()=="RELEASED5")||(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
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
                        <button type="button" class="btn btn-light waves-effect" data-dismiss="modal">Close</button>
                    </div>
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>

        <!-- /.modal -->



    </div>
    <asp:Panel ID="pnlHide" runat="server" Visible="false">
        <asp:FormView ID="FV_PRInfoDisplay" runat="server" Width="99%" CssClass="gridviewNew" GridLines="None">
            <ItemTemplate>
                <div id="div1" style="text-align: left;">
                    <table style="border-collapse: collapse; width: 100%; color: #555555;">
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>PR No</b></td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("BANFN_EXT") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Requestor</b></td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("RPERNR") %>  <%#Eval("ENAME") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Main Function</b></td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("PFUNC_AREA") %>
                            </td>

                        </tr>
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Sub Function</b></td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("BTEXT") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>MIS Group C</b></td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("MIS_GRPC") %>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMISGroupC" runat="server" Width="150px" CssClass="textbox" AutoPostBack="true" Visible="false" TabIndex="7"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVMISGroupC" runat="server" ErrorMessage="Select MIS Group C" ForeColor="Red" ControlToValidate="ddlMISGroupC" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>MIS Group A</b></td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("MIS_GRPA") %>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMISGroupA" runat="server" CssClass="textbox" Visible="false" Enabled="false"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>MIS Group B</b></td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("MIS_GRPB") %>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMISGroupB" runat="server" CssClass="textbox" Visible="false" Enabled="false"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Requestor Region</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("EKGRP") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Bill to address</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("BWERKS") %>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBillToAddress" runat="server" Width="150px" CssClass="textbox" Visible="false" TabIndex="8"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVBillToAddress" runat="server" ErrorMessage="Select Bill To Address" ForeColor="Red" ControlToValidate="ddlBillToAddress" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Ship to Address</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("SWERKS") %>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlShipToAddress" runat="server" Width="150px" CssClass="textbox" Visible="false" TabIndex="9"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVBillToAddress0" runat="server" ErrorMessage="Select Ship To Address" ForeColor="Red" ControlToValidate="ddlBillToAddress" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>




                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Suggested Supplier</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("SUG_SUPP") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Supplier Address</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("SUP_ADDRESS") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Supplier Phone No</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("SUP_PHONE") %>
                            </td>

                        </tr>
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>In Budget</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("IN_BUDGET") %>
                            </td>
                            <td>
                                <asp:RadioButton ID="rbtnBudgetYes" runat="server" GroupName="Budget" Text="Yes" Visible="false" TabIndex="10" />
                                <asp:RadioButton ID="rbtnBudgetNo" runat="server" GroupName="Budget" Text="No" Visible="false" TabIndex="11" />

                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Capitalization</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("CAPITALIZED") %>
                            </td>
                            <td>
                                <asp:RadioButton ID="rbtnCapitalizedYes" runat="server" GroupName="Capitalized" TabIndex="12" Text="Yes" Visible="false" AutoPostBack="true" />
                                <asp:RadioButton ID="rbtnCapitalizedNo" runat="server" GroupName="Capitalized" TabIndex="13" Text="No" Visible="false" AutoPostBack="true" />
                                &nbsp;&nbsp;
                <asp:TextBox ID="txtWillthisItembeCapitalized" runat="server" CssClass="textbox" TabIndex="14" Enabled="False" Visible="false"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="RFVWillthisItembeCapitalized" runat="server" ErrorMessage="Select Item to be Capitalized" ForeColor="Red" ControlToValidate="txtWillthisItembeCapitalized" Enabled="false" Visible="false"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REV_txtCapitalized" runat="server" ValidationGroup="capitalixed"
                                    ControlToValidate="txtWillthisItembeCapitalized" ErrorMessage="Single qoutes not allowed" ForeColor="Red"
                                    ValidationExpression="^[^']+$"></asp:RegularExpressionValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Budget line item</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("CAP_TEXT") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Managed Service</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("SERVICE_BUREA") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Criticality</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("CRITICALITY") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>ERP Project Code</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("PSPNR") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Billable</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("BILLABLE") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Proposal</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd">
                                <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#(Eval("PROPOSAL").ToString()=="YES") ? Eval("PFID") : "<span>NA</span>" %>' Font-Bold="True" CommandName="downloadp" CommandArgument='<%# Eval("PFPATH") %>' CausesValidation="false" Enabled='<%# bool.Parse(string.Format("{0}", Eval("PROPOSAL").ToString()=="YES" ? "true" : "false"))%>' /></td>
                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Agreement</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd">
                                <asp:LinkButton ID="LinkButton2" runat="server" Text='<%#(Eval("AGREEMENT").ToString()=="YES") ? Eval("AFID") : "<span>NA</span>" %>' Font-Bold="True" CommandName="downloada" CommandArgument='<%# Eval("AFPATH") %>' CausesValidation="false" Enabled='<%# bool.Parse(string.Format("{0}", Eval("AGREEMENT").ToString()=="YES" ? "true" : "false"))%>' /></td>
                            <td></td>
                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Email</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd">
                                <asp:LinkButton ID="LinkButton3" runat="server" Text='<%#(Eval("EMAIL_COM").ToString()=="YES") ? Eval("EFID") : "<span>NA</span>" %>' Font-Bold="True" CommandName="downloade" CommandArgument='<%# Eval("EFPATH") %>' CausesValidation="false" Enabled='<%# bool.Parse(string.Format("{0}", Eval("EMAIL_COM").ToString()=="YES" ? "true" : "false"))%>' /></td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Invoice</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd">
                                <asp:LinkButton ID="LinkButton4" runat="server" Text='<%#(Eval("INVOICE").ToString()=="YES") ? Eval("IFID") : "<span>NA</span>" %>' Font-Bold="True" CommandName="downloadi" CommandArgument='<%# Eval("IFPATH") %>' CausesValidation="false" Enabled='<%# bool.Parse(string.Format("{0}", Eval("INVOICE").ToString()=="YES" ? "true" : "false"))%>' /></td>
                        </tr>


                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Business Unit</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("SPART") %>
                            </td>

                        </tr>
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Region</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td><%#Eval("REGIONTXT") %>
                            </td>

                        </tr>
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Justification</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("JUSTIFICATION") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Special Notes</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("SPL_NOTES") %>
                            </td>

                        </tr>
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Total Amount</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%#Eval("TOTAL") %>(<%#Eval("WAERS") %>)
                            </td>

                        </tr>
                        <tr>
                            <td class="Td01"></td>
                            <td class="Fnt04 Td02"><b>Submit Date</b> </td>
                            <td class="Td01"><b>:</b></td>
                            <td class="formtd"><%# Eval("CREATED_ON1", "{0:dd/MM/yyyy}") %>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
                <br />
                <br />
            </ItemTemplate>
        </asp:FormView>
        <asp:GridView ID="GV_PrItems" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" DataKeyNames="BNFPO,PRID">
            <Columns>
                <asp:BoundField DataField="BNFPO" HeaderText="Item No" />
                <asp:BoundField DataField="TXZ01" HeaderText="Item Description" />
                <asp:BoundField DataField="NO_OF_UNITS" HeaderText="No. of Units" />
                <asp:BoundField DataField="MEINS" HeaderText="Unit of Measurements" />
                <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price" ItemStyle-HorizontalAlign="Right" />
                <asp:BoundField DataField="WAERS" HeaderText="Currency" />
                <asp:BoundField DataField="ITEM_NOTE" HeaderText="Item Note" />
                <asp:BoundField DataField="MTART" HeaderText="Category" />
                <asp:BoundField DataField="TAXABLE" HeaderText="Taxable(%)" />
                <asp:TemplateField HeaderText="GL Account" Visible="False">

                    <ItemTemplate>
                        <asp:DropDownList ID="ddlGLAcc" runat="server" Width="150px" TabIndex="15" CssClass="textbox" Visible="true"></asp:DropDownList>
                        <Ajx:CascadingDropDown ID="CCD_ddlGLAcc" runat="server" Enabled="true" TargetControlID="ddlGLAcc" PromptText="- SELECT GL ACCOUNT -"
                            PromptValue="0" LoadingText="[Loading GL Account...]" Category="GL" SelectedValue='<%#Bind("SAKNR") %>' ServiceMethod="GetGL_Account"
                            ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("RPERNR") %>'>
                        </Ajx:CascadingDropDown>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

    </asp:Panel>
</asp:Content>
