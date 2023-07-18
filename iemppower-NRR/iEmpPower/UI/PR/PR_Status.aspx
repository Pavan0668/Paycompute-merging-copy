<%@ Page Title="PR Status." Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="PR_Status.aspx.cs" Inherits="iEmpPower.UI.PR.PR_Status" Theme="SkinFile" EnableEventValidation="true" Culture="en-GB" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #formTable tr {
            border-bottom: 1px solid #dee2e6;
            padding: .6rem;
            vertical-align: top;
        }



        .tdwidth1 {
            padding: .3rem;
            width: 15% !important;
        }



        .tdwidth2 {
            padding: .3rem;
            width: 2% !important;
        }

        .right {
            text-align: right !important;
        }

        .tdwidth3 {
            padding: .3rem;
            width: 33% !important;
        }
    </style>
    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item"><a href="Purchase_Requisitions.aspx">Purchase Requisitions</a></li>
                        <li class="breadcrumb-item active">View PR Details</li>
                    </ol>
                </div>
                <h4 class="page-title">PR Requisition Details&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>
                        <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1" Font-Size="Medium"></asp:Label>
                    </span></h4>
                <asp:LinkButton runat="server" ID="lbtCopyPR" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right" OnClick="lbtCopyPR_Click"><i class="mdi mdi-Copy"></i>
                Copy as Template </asp:LinkButton>
            </div>
        </div>
    </div>
    <!-- end page title -->



    <div id="dvPRView" class="table-responsive card-box" runat="server">
        <h4>My Pending and Saved Requisitions</h4>
        <br />
        <table>
            <tr>
                <td align="right">Select:</td>
                <td>
                    <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="form-control-file" TabIndex="1">
                        <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Purchase No" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Originator" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Status" Value="3"></asp:ListItem>

                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" TabIndex="2" placeholder="Enter Text"></asp:TextBox>
                </td>

                <td align="right">Created On</td>
                <td style="margin-left: 80px">
                    <asp:TextBox ID="txtCreatedOn" runat="server" TabIndex="3" placeholder="Select Date"></asp:TextBox>
                    <Ajx:MaskedEditExtender ID="MEE_txtStartDate" runat="server" AcceptNegative="Left"
                        CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                        MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                        OnInvalidCssClass="MaskedEditError" TargetControlID="txtCreatedOn" />
                    <Ajx:CalendarExtender ID="CE_txtCreatedOn" runat="server" Enabled="True" Format="dd/MM/yyyy"
                        TargetControlID="txtCreatedOn">
                    </Ajx:CalendarExtender>
                </td>
                <td>
                    <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" TabIndex="4" CssClass="btn btn-xs btn-secondary" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnclear" runat="server" Text="Clear" OnClick="btnclear_Click" TabIndex="5" CssClass="btn btn-xs btn-secondary" /></td>
            </tr>

            <tr>
                <td></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <asp:GridView ID="grdPurchaseItemDetails" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" PagerStyle-CssClass="pagination" Width="99%" DataKeyNames="PRID"
            OnRowCommand="grdPurchaseItemDetails_RowCommand" AllowPaging="true" PageSize="5" OnPageIndexChanging="grdPurchaseItemDeatils_PageIndexChanging"
            AllowSorting="true" OnSorting="grdPurchaseItemDeatils_Sorting">
            <Columns>
                <asp:BoundField DataField="PRID" HeaderText="PR No." />
                <asp:BoundField DataField="IPERNR" HeaderText="Indentor" />
                <asp:BoundField DataField="RPERNR" HeaderText="Requestor" />
                <asp:BoundField DataField="SUG_SUPP" HeaderText="Supplier" />
                <asp:BoundField DataField="IN_BUDGET" HeaderText="In Budget" />
                <asp:BoundField DataField="CRITICALITY" HeaderText="Criticality" />
                <asp:BoundField DataField="PSPNR" HeaderText="Project Code" />
                <asp:BoundField DataField="BNFPO" HeaderText="Capex" />
                <%-- <asp:BoundField DataField="UNIT_PRICE" HeaderText="Total Amount" SortExpression="UNIT_PRICE" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"/>
                --%>

                <asp:TemplateField HeaderText="Total Amount">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#  Convert.ToDouble(Eval("UNIT_PRICE")).ToString("#,##0.00") %>
                        <%-- <%# Eval("UNIT_PRICE") %> --%>
                        <%--  ( <%# Eval("WAERS") %>)--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:BoundField DataField="WAERS" HeaderText="Currency" />
                <asp:TemplateField HeaderText="Total Amount (INR)">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#  Convert.ToDouble(Eval("TAINRAmt")).ToString("#,##0.00") %>
                        <%-- <%# Eval("TAINRAmt") %>--%>
                        <%-- ( <%# Eval("INRCURR") %>)--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <%--<asp:BoundField DataField="TAmountRs" HeaderText="Total Amount(in Rs.)" />--%>
                <asp:BoundField DataField="CREATED_ON1" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="STATUS" HeaderText="Status" />


                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LbtnEmpPurchaseItemView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>View</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
            <SelectedRowStyle BackColor="Silver" />
        </asp:GridView>
        <br />
        <br />
    </div>


    <div id="ViewPRIfo" runat="server" visible="false" class="table-responsive card-box">
        <%--  <h4>View PR Requisition Details</h4>--%>
        <asp:Label ID="LblMsg" runat="server"></asp:Label>
        <%--   <Ajx:TabContainer ID="DisplayPRInfo" runat="server" Width="100%" ActiveTabIndex="1" TabIndex="6">
            <Ajx:TabPanel runat="server" HeaderText="&nbsp;&nbsp;&nbsp;Indent.&nbsp;Details&nbsp;&nbsp;&nbsp;" ID="IndentDetailsTabPanel">
                <ContentTemplate>

                    <fieldset>--%>
        <div class="bg-light" style="padding-top: 10px; padding-left: 5px; padding-bottom: 10px; border-bottom: 1.5px solid #dee2e6; border-top: 1.5px solid #dee2e6">
            <h4>PR Header Details</h4>
        </div>
        <asp:FormView ID="FV_EmpPRInfoDisplay" runat="server" OnItemCommand="FV_PRInfoDisplay_ItemCommand" GridLines="None" Width="100%">
            <ItemTemplate>
                <div id="div1" style="text-align: left;">
                    <table style="border-collapse: collapse; width: 100%;" id="formTable">
                        <tr>
                            <td class="tdwidth1"><b>PR No</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><b><%#Eval("BANFN_EXT") %></b>
                            </td>

                            <td class=" tdwidth1"><b>Capitalization</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("CAPITALIZED") %>
                            </td>
                        </tr>

                        <tr>
                            <td class="tdwidth1"><b>Requestor</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("RPERNR") %>-<%#Eval("ENAME") %>
                            </td>

                            <td class=" tdwidth1"><b>Managed Service</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SERVICE_BUREA") %>
                            </td>
                        </tr>

                        <tr>
                            <td class="tdwidth1"><b>Main Function</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("PFUNC_AREA") %>
                            </td>

                            <td class=" tdwidth1"><b>Criticality</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("CRITICALITY") %>
                            </td>

                        </tr>
                        <tr>

                            <td class=" tdwidth1"><b>Sub Function</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("BTEXT") %>
                            </td>

                            <td class=" tdwidth1"><b>ERP Project Code</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("PSPNR") %>
                            </td>
                        </tr>

                        <tr>

                            <td class=" tdwidth1"><b>MIS Group C</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("MIS_GRPC") %>
                            </td>

                            <td class=" tdwidth1"><b>Billable</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("BILLABLE") %>
                            </td>

                        </tr>
                        <tr>

                            <td class=" tdwidth1"><b>MIS Group A</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("MIS_GRPA") %>
                            </td>

                            <td class=" tdwidth1"><b>Business Unit</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SPART") %>
                            </td>
                        </tr>
                        <tr>

                            <td class=" tdwidth1"><b>MIS Group B</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("MIS_GRPB") %>
                            </td>

                            <td class=" tdwidth1"><b>Region</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("REGIONTXT") %>
                            </td>
                        </tr>
                        <tr>

                            <td class=" tdwidth1"><b>Requestor Region</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("EKGRP") %>
                            </td>

                            <td class=" tdwidth1"><b>Justification</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("JUSTIFICATION") %>
                            </td>
                        </tr>

                        <tr>

                            <td class=" tdwidth1"><b>Bill to address</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("BWERKS") %>
                            </td>

                            <td class=" tdwidth1"><b>Special Notes</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SPL_NOTES") %>
                            </td>
                        </tr>

                        <tr>

                            <td class=" tdwidth1"><b>Ship to Address</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SWERKS") %>
                            </td>

                            <td class=" tdwidth1"><b>Proposal</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("PFID") %>' Font-Bold="True" CommandName="downloadp" CommandArgument='<%# Eval("PFPATH") %>' CausesValidation="false" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="LinkButton1" />
                                    </Triggers>

                                </asp:UpdatePanel>
                            </td>

                        </tr>


                        <tr>

                            <td class=" tdwidth1"><b>Suggested Supplier</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SUG_SUPP") %>
                            </td>

                            <td class=" tdwidth1"><b>Agreement</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text='<%#Eval("AFID") %>' Font-Bold="True" CommandName="downloada" CommandArgument='<%# Eval("AFPATH") %>' CausesValidation="false" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="LinkButton2" />
                                    </Triggers>

                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr>

                            <td class=" tdwidth1"><b>Supplier Address</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SUP_ADDRESS") %>
                            </td>


                            <td class=" tdwidth1"><b>Invoice</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton4" runat="server" Text='<%#Eval("IFID") %>' Font-Bold="True" CommandName="downloadi" CommandArgument='<%# Eval("IFPATH") %>' CausesValidation="false" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="LinkButton4" />
                                    </Triggers>

                                </asp:UpdatePanel>
                            </td>

                        </tr>

                        <tr>

                            <td class=" tdwidth1"><b>Supplier Phone No</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SUP_PHONE") %>
                            </td>

                            <td class=" tdwidth1"><b>Email</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" Text='<%#Eval("EFID") %>' Font-Bold="True" CommandName="downloade" CommandArgument='<%# Eval("EFPATH") %>' CausesValidation="false" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="LinkButton3" />
                                    </Triggers>

                                </asp:UpdatePanel>

                            </td>
                        </tr>

                        <tr>
                            <td class=" tdwidth1"><b>In Budget</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("IN_BUDGET") %>
                            </td>


                            <td class=" tdwidth1"><b>Total Amount</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><b><%# Convert.ToDouble(Eval("TOTAL")).ToString("N2") %>(<%#Eval("WAERS") %>)</b>
                            </td>
                        </tr>
                        <tr>
                            <td class=" tdwidth1"><b>Budget line item</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("CAP_TEXT") %>
                            </td>


                            <td class=" tdwidth1"><b>Submit Date</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%# Eval("CREATED_ON1", "{0:dd/MM/yyyy}") %>
                            </td>
                        </tr>

                    </table>
                </div>
            </ItemTemplate>
        </asp:FormView>
        <%-- </fieldset>
                </ContentTemplate>
            </Ajx:TabPanel>--%>

        <%-- <Ajx:TabPanel ID="IndentItems" runat="server" HeaderText="&nbsp;&nbsp;&nbsp;Indent.&nbsp;Items&nbsp;&nbsp;&nbsp;">
                <ContentTemplate>

                    <fieldset class="FldSetCls">--%>
        <div class="DivSpacer01">
            <br />
            <br />
        </div>
        <div class="bg-light" style="padding-top: 10px; padding-left: 5px; padding-bottom: 10px; border-bottom: 1.5px solid #dee2e6; border-top: 1.5px solid #dee2e6">
            <h4>PR Item Details</h4>
        </div>
        <asp:GridView ID="GV_EmpPrItems" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None">
            <Columns>

                <asp:BoundField DataField="BNFPO" HeaderText="Item No" />
                <asp:BoundField DataField="TXZ01" HeaderText="Item Description" />
                <asp:BoundField DataField="NO_OF_UNITS" HeaderText="No. of Units" />
                <asp:BoundField DataField="MEINS" HeaderText="Unit of Measurements" />
                <%-- <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price" ItemStyle-HorizontalAlign="Right" />--%>
                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-CssClass="right">
                    <ItemTemplate>
                        <%# Convert.ToDouble(Eval("UNIT_PRICE")).ToString("#,##0.00") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:BoundField DataField="WAERS" HeaderText="Currency" />
                <asp:BoundField DataField="ITEM_NOTE" HeaderText="Item Note" />
                <%--  <asp:BoundField DataField="PART_NO" HeaderText="Part No" />--%>

                <asp:BoundField DataField="MTART" HeaderText="Category" />
                <asp:BoundField DataField="TAXABLE" HeaderText="Taxable(%)" />
            </Columns>
            <HeaderStyle CssClass="table th" />

        </asp:GridView>

        <%-- <br></br>

                        <br></br>

                    </fieldset>
                </ContentTemplate>
            </Ajx:TabPanel>--%>

        <%--    <Ajx:TabPanel ID="AppHistory" runat="server" HeaderText="&nbsp;&nbsp;&nbsp;Approval&nbsp;History&nbsp;&nbsp;&nbsp;" Visible="false">
                <ContentTemplate>

                    <fieldset class="FldSetCls">--%>
        <div class="DivSpacer01"></div>
        <br />


        <asp:GridView ID="grdEmpAppHistory" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" DataKeyNames="APPROVEDBY1,APPROVEDBY2,APPROVEDBY3,APPROVEDBY4,APPROVEDBY5,APPROVEDBY6"
            OnRowDataBound="grdEmpAppHistory_RowDataBound" ShowHeader="False" Visible="false">

            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table>
                            <tr>
                                <th>Id</th>
                                <th>Name</th>
                                <th>Action</th>
                                <th>Action Date</th>
                                <th>Comments</th>
                            </tr>
                            <tr>
                                <td><%# Eval("APPROVEDBY1") %> </td>
                                <td><%# Eval("APPROVEDBY1N") %></td>
                                <td><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":(Eval("STATUS").ToString()=="HOLD1")?"Hold":(Eval("STATUS").ToString()=="RELEASED1")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%> </td>
                                <%--   <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": ""%>--%>
                                <%-- <td class="Tbltd"><%#(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%></td>--%>

                                <td><%# (Eval("STATUS").ToString()=="Approved1")?Eval("APP_ON1","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD1")?Eval("HOLD_ON1","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED1")?Eval("RELEASED_ON1","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected1")?Eval("APP_ON1","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%> </td>

                                <td><%# Eval("COMMENTS1") %></td>
                            </tr>
                            <asp:Panel ID="pnlAPPROVEDBY2" runat="server" Visible='<%# (Eval("APPROVEDBY2")).ToString()==""?false:true %>'>
                                <tr>
                                    <td><%# Eval("APPROVEDBY2") %></td>
                                    <td><%# Eval("APPROVEDBY2N") %></td>
                                    <td><%# (Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Approved2")?"Approved":(Eval("STATUS").ToString()=="HOLD2")?"Hold":(Eval("STATUS").ToString()=="RELEASED2")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%> </td>
                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%></td>--%>


                                    <td><%# (Eval("STATUS").ToString()=="Approved2")?Eval("APP_ON2","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD2")?Eval("HOLD_ON2","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED2")?Eval("RELEASED_ON2","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected2")?Eval("APP_ON2","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%> </td>

                                    <td><%# Eval("COMMENTS2") %></td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="Panel1" runat="server" Visible='<%# (Eval("APPROVEDBY3")).ToString()==""?false:true %>'>
                                <tr>
                                    <td><%# Eval("APPROVEDBY3") %></td>
                                    <td><%# Eval("APPROVEDBY3N") %></td>
                                    <td><%# (Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected3")?"Rejected": (Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved3")?"Approved":(Eval("STATUS").ToString()=="HOLD3")?"Hold":(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED3")?"Released":(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%> </td>
                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%></td>--%>
                                    <td><%# (Eval("STATUS").ToString()=="Approved3")?Eval("APP_ON3","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD3")?Eval("HOLD_ON3","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED3")?Eval("RELEASED_ON3","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected3")?Eval("APP_ON3","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%> </td>


                                    <td><%# Eval("COMMENTS3") %></td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="Panel2" runat="server" Visible='<%# (Eval("APPROVEDBY4")).ToString()==""?false:true %>'>
                                <tr>
                                    <td><%# Eval("APPROVEDBY4") %></td>
                                    <td><%# Eval("APPROVEDBY4N") %></td>
                                    <td><%# (Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected4")?"Rejected": (Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved4")?"Approved":(Eval("STATUS").ToString()=="HOLD4")?"Hold":(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED4")?"Released":(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%> </td>
                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%></td>--%>
                                    <td><%# (Eval("STATUS").ToString()=="Approved4")?Eval("APP_ON4","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD4")?Eval("HOLD_ON4","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED4")?Eval("RELEASED_ON4","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected4")?Eval("APP_ON4","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%> </td>

                                    <td class="Tbltd"><%# Eval("COMMENTS4") %></td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="Panel3" runat="server" Visible='<%# (Eval("APPROVEDBY5")).ToString()==""?false:true %>'>
                                <tr>
                                    <td><%# Eval("APPROVEDBY5") %></td>
                                    <td><%# Eval("APPROVEDBY5N") %></td>
                                    <td><%# (Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected5")?"Rejected": (Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved5")?"Approved":(Eval("STATUS").ToString()=="HOLD5")?"Hold":(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED5")?"Released":(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%> </td>
                                    <%--<td class="Tbltd"><%#(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%></td>--%>
                                    <td><%# (Eval("STATUS").ToString()=="Approved5")?Eval("APP_ON5","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD5")?Eval("HOLD_ON5","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED5")?Eval("RELEASED_ON5","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected5")?Eval("APP_ON5","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%> </td>

                                    <td><%# Eval("COMMENTS5") %></td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="Panel4" runat="server" Visible='<%# (Eval("APPROVEDBY6")).ToString()==""?false:true %>'>
                                <tr>
                                    <td><%# Eval("APPROVEDBY6") %></td>
                                    <td><%# Eval("APPROVEDBY6N") %></td>
                                    <td><%# (Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":(Eval("STATUS").ToString()=="HOLD6")?"Hold":(Eval("STATUS").ToString()=="HOLD5")||(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED6")?"Released":(Eval("STATUS").ToString()=="RELEASED5")||(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%> </td>
                                    <%--  <td class="Tbltd"><%#(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%></td>--%>
                                    <td><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>

                                    <td><%# Eval("COMMENTS6") %></td>
                                </tr>
                            </asp:Panel>

                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>


        <%--  <br>
                            <br></br>
                            <br>
                                <br></br>
                                <br>
                                    <br></br>
                                    <br>
                                        <br></br>
                                    </br>
                                </br>
                            </br>
                        </br>

                    </fieldset>
                </ContentTemplate>
            </Ajx:TabPanel>
        </Ajx:TabContainer>--%>
    </div>

</asp:Content>

