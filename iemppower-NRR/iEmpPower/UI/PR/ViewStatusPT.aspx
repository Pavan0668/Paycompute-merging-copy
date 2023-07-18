<%@ Page Title="PR Status for PT" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="ViewStatusPT.aspx.cs"
    Inherits="iEmpPower.UI.PR.ViewStatusPT" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlcontent" runat="server" DefaultButton="btnsearch">
        <!-- start page title -->
        <div class="row">
            <div class="col-12">
                <div class="page-title-box">
                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Purchase Requisitions Details<</li>
                        </ol>
                    </div>
                    <h4 class="page-title">Purchase Requisitions Details</h4>
                </div>
            </div>
        </div>
        <!-- end page title -->

        <div class="table-responsive card-box">
            <h4>Purchase Requisitions Details</h4>
            <br />
            <asp:Label ID="LblMsg" runat="server"></asp:Label>
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
            <asp:GridView ID="grdPurchaseItemDeatils" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%"
                OnRowCommand="grdPurchaseItemDeatils_RowCommand" DataKeyNames="PRID" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPurchaseItemDeatils_PageIndexChanging"
                AllowSorting="true" OnSorting="grdPurchaseItemDeatils_Sorting">
                <Columns>
                    <asp:BoundField DataField="PRID" HeaderText="PR No."/>
                    <asp:BoundField DataField="IPERNR" HeaderText="Indentor"/>
                    <asp:BoundField DataField="RPERNR" HeaderText="Requestor"/>
                    <asp:BoundField DataField="SUG_SUPP" HeaderText="Supplier"/>
                    <asp:BoundField DataField="IN_BUDGET" HeaderText="In Budget"/>
                    <asp:BoundField DataField="CRITICALITY" HeaderText="Criticality"/>
                    <asp:BoundField DataField="PSPNR" HeaderText="Project Code"/>
                    <asp:BoundField DataField="BNFPO" HeaderText="Capex"/>
                    <%--<asp:BoundField DataField="UNIT_PRICE" HeaderText="Total Amount" SortExpression="UNIT_PRICE" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"/>--%>
                    <asp:TemplateField HeaderText="Total Amount">
                        <EditItemTemplate>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%#  Convert.ToDouble(Eval("UNIT_PRICE")).ToString("#,##0.00") %>
                            <%-- <%# Eval("UNIT_PRICE") %>--%>
                            <%--   ( <%# Eval("WAERS") %>)--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="WAERS" HeaderText="Currency"/>
                    <asp:TemplateField HeaderText="Total Amount (INR)">
                        <EditItemTemplate>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%#  Convert.ToDouble(Eval("TAINRAmt")).ToString("#,##0.00") %>
                            <%-- <%# Eval("TAINRAmt") %> --%>
                            <%--  ( <%# Eval("INRCURR") %>)--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <%--<asp:BoundField DataField="TAmountRs" HeaderText="Total Amount(in Rs.)" />--%>
                    <asp:BoundField DataField="CREATED_ON1" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}"/>
                    <asp:BoundField DataField="STATUS" HeaderText="Status"/>


                    <%-- <asp:BoundField DataField="IndentStatus" HeaderText="Indent Status" />--%>

                    <%--<asp:BoundField DataField="" HeaderText="INdent Details">
                     </asp:BoundField>--%>

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
            <h2>Requisition Details</h2>
            <Ajx:TabContainer ID="DisplayPRInfo" runat="server" Width="100%" ActiveTabIndex="0" TabIndex="6">
                <Ajx:TabPanel runat="server" HeaderText="&nbsp;&nbsp;&nbsp;Indent&nbsp;Details&nbsp;&nbsp;&nbsp;" ID="IndentDetailsTabPanel">
                    <ContentTemplate>

                        <fieldset>

       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>

                            <asp:FormView ID="FV_EmpPRInfoDisplay" runat="server" OnItemCommand="FV_PRInfoDisplay_ItemCommand" CssClass="gridviewNew" GridLines="None">
                                <ItemTemplate>
                                    <div id="div1" style="text-align: left;">
                                        <table style="border-collapse: collapse; width: 100%;">
                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>PR No</b></td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("BANFN_EXT") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Requestor</b></td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("RPERNR") %> <%#Eval("ENAME") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Main Function</b></td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("PFUNC_AREA") %>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Sub Function</b></td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("BTEXT") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>MIS Group C</b></td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("MIS_GRPC") %>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>MIS Group A</b></td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("MIS_GRPA") %>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>MIS Group B</b></td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("MIS_GRPB") %>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Requestor Region</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("EKGRP") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Bill to address</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("BWERKS") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Ship to Address</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("SWERKS") %>
                                                </td>

                                            </tr>


                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Suggested Supplier</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("SUG_SUPP") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Supplier Address</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("SUP_ADDRESS") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Supplier Phone No</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("SUP_PHONE") %>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>In Budget</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("IN_BUDGET") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Capitalization</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("CAPITALIZED") %>
                                                </td>

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
                                                <td><%#Eval("SERVICE_BUREA") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Criticality</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("CRITICALITY") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>ERP Project Code</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("PSPNR") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Billable</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("BILLABLE") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Proposal</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#Eval("PFID") %>' Font-Bold="True" CommandName="downloadp" CommandArgument='<%# Eval("PFPATH") %>' CausesValidation="false" /></td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Agreement</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" Text='<%#Eval("AFID") %>' Font-Bold="True" CommandName="downloada" CommandArgument='<%# Eval("AFPATH") %>' CausesValidation="false" /></td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Email</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" Text='<%#Eval("EFID") %>' Font-Bold="True" CommandName="downloade" CommandArgument='<%# Eval("EFPATH") %>' CausesValidation="false" /></td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Invoice</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" Text='<%#Eval("IFID") %>' Font-Bold="True" CommandName="downloadi" CommandArgument='<%# Eval("IFPATH") %>' CausesValidation="false" /></td>

                                            </tr>


                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Business Unit</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("SPART") %>
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
                                                <td><%#Eval("JUSTIFICATION") %>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Special Notes</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("SPL_NOTES") %>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Total Amount</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%#Eval("TOTAL") %>(<%#Eval("WAERS") %>)
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="Td01"></td>
                                                <td class="Fnt04 Td02"><b>Submit Date</b> </td>
                                                <td class="Td01"><b>:</b></td>
                                                <td><%# Eval("CREATED_ON1", "{0:dd/MM/yyyy}") %>
                                                </td>

                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:FormView>
                                    </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="FV_EmpPRInfoDisplay" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </fieldset>
                    </ContentTemplate>
                </Ajx:TabPanel>

                <Ajx:TabPanel ID="IndentItems" runat="server" HeaderText="&nbsp;&nbsp;&nbsp;Indent&nbsp;Items&nbsp;&nbsp;&nbsp;">
                    <ContentTemplate>

                        <fieldset class="FldSetCls">
                            <%-- <legend class="LegendCls"><b>Indent&nbsp; Details</b></legend>--%>
                            <%-- <asp:GridView ID="GV_EmpPrItems" runat="server" AutoGenerateColumns="False" CssClass="gridview">
                            <Columns>
                                <asp:BoundField DataField="BNFPO" HeaderText="Item No" />
                                <asp:BoundField DataField="MTART" HeaderText="Category" />
                                <asp:BoundField DataField="NO_OF_UNITS" HeaderText="No. of Units" />
                                <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price" />
                                <asp:BoundField DataField="TAXABLE" HeaderText="Taxable" />
                                <asp:BoundField DataField="WAERS" HeaderText="Currency" />
                                <asp:BoundField DataField="ITEM_NOTE" HeaderText="Item Note" />
                                <asp:BoundField DataField="TXZ01" HeaderText="Item Description" />
                                <asp:BoundField DataField="PART_NO" HeaderText="Part No" />
                                <asp:BoundField DataField="MEINS" HeaderText="Unit of Measurements" />
                            </Columns>
                            
                        </asp:GridView>--%>
                            <asp:GridView ID="GV_EmpPrItems" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="BNFPO" HeaderText="Item No" />
                                    <asp:BoundField DataField="TXZ01" HeaderText="Item Description" />
                                    <asp:BoundField DataField="NO_OF_UNITS" HeaderText="No. of Units" />
                                    <asp:BoundField DataField="MEINS" HeaderText="Unit of Measurements" />
                                    <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="WAERS" HeaderText="Currency" />
                                    <asp:BoundField DataField="ITEM_NOTE" HeaderText="Item Note" />
                                    <%--  <asp:BoundField DataField="PART_NO" HeaderText="Part No" />--%>
                                    <asp:BoundField DataField="MTART" HeaderText="Category" />
                                    <asp:BoundField DataField="TAXABLE" HeaderText="Taxable(%)" />

                                </Columns>
                            </asp:GridView>
                        </fieldset>
                    </ContentTemplate>
                </Ajx:TabPanel>

                <Ajx:TabPanel ID="AppHistory" runat="server" HeaderText="&nbsp;&nbsp;&nbsp;Approval&nbsp;History&nbsp;&nbsp;&nbsp;">
                    <ContentTemplate>

                        <fieldset class="FldSetCls">
                            <asp:GridView ID="grdEmpAppHistory" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" DataKeyNames="APPROVEDBY1,APPROVEDBY2,APPROVEDBY3,APPROVEDBY4,APPROVEDBY5,APPROVEDBY6"
                                OnRowDataBound="grdEmpAppHistory_RowDataBound" ShowHeader="false">
                                <%--<Columns>
                                   
                                    <asp:TemplateField HeaderText="Approver-1">

                                        <ItemTemplate>
                                            <%# Eval("APPROVEDBY1") %> <%# Eval("APPROVEDBY1N") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved On1">

                                        <ItemTemplate>
                                             
                                            <%#(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="COMMENTS1" HeaderText="Comments1" />


                                 
                                    <asp:TemplateField HeaderText="Approver-2">

                                        <ItemTemplate>
                                            <%# Eval("APPROVEDBY2") %> <%# Eval("APPROVEDBY2N") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved On2">

                                        <ItemTemplate>
 
                                            <%#(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  
                                    <asp:BoundField DataField="COMMENTS2" HeaderText="Comments2" />

                                   
                                    <asp:TemplateField HeaderText="Approver-3">

                                        <ItemTemplate>
                                            <%# Eval("APPROVEDBY3") %> <%# Eval("APPROVEDBY3N") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved On3">

                                        <ItemTemplate>
                                          
                                            <%#(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="COMMENTS3" HeaderText="Comments3" />
 
                                    <asp:TemplateField HeaderText="Approver-4">

                                        <ItemTemplate>
                                            <%# Eval("APPROVEDBY4") %> <%# Eval("APPROVEDBY4N") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved On4">

                                        <ItemTemplate>
                                           
                                            <%#(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                 
                                    <asp:BoundField DataField="COMMENTS4" HeaderText="Comments4" />

                                    
                                    <asp:TemplateField HeaderText="Approver-5">

                                        <ItemTemplate>
                                            <%# Eval("APPROVEDBY5") %> <%# Eval("APPROVEDBY5N") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved On5">

                                        <ItemTemplate>
                                           
                                            <%#(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="COMMENTS5" HeaderText="Comments5" />

                                    
                                    <asp:TemplateField HeaderText="Approver-6">

                                        <ItemTemplate>
                                            <%# Eval("APPROVEDBY6") %> <%# Eval("APPROVEDBY6N") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved On6">

                                        <ItemTemplate>
                                          
                                            <%#(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="COMMENTS6" HeaderText="Comments6" />

                                    <asp:BoundField DataField="Status" HeaderText="Status" />
                                </Columns>--%>

                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table class="TblCls">
                                                <tr>
                                                    <th class="Tblth">Id</th>
                                                    <th class="Tblth">Name</th>
                                                    <th class="Tblth">Action</th>
                                                    <th class="Tblth">Action Date</th>
                                                    <th class="Tblth">Comments</th>
                                                </tr>
                                                <tr>
                                                    <td class="Tbltd"><%# Eval("APPROVEDBY1") %> </td>
                                                    <td class="Tbltd"><%# Eval("APPROVEDBY1N") %></td>
                                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":(Eval("STATUS").ToString()=="HOLD1")?"Hold":(Eval("STATUS").ToString()=="RELEASED1")?"Released":"Approved"%> </td>
                                                    <%--   <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": ""%>--%>
                                                    <%-- <td class="Tbltd"><%#(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%></td>--%>

                                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved1")?Eval("APP_ON1","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD1")?Eval("HOLD_ON1","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED1")?Eval("RELEASED_ON1","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected1")?Eval("APP_ON1","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%> </td>

                                                    <td class="Tbltd"><%# Eval("COMMENTS1") %></td>
                                                </tr>
                                                <asp:Panel ID="pnlAPPROVEDBY2" runat="server" Visible='<%# (Eval("APPROVEDBY2")).ToString()==""?false:true %>'>
                                                    <tr>
                                                        <td class="Tbltd"><%# Eval("APPROVEDBY2") %></td>
                                                        <td class="Tbltd"><%# Eval("APPROVEDBY2N") %></td>
                                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Approved2")?"Approved":(Eval("STATUS").ToString()=="HOLD2")?"Hold":(Eval("STATUS").ToString()=="RELEASED2")?"Released":"Approved"%> </td>
                                                        <%-- <td class="Tbltd"><%#(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%></td>--%>


                                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved2")?Eval("APP_ON2","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD2")?Eval("HOLD_ON2","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED2")?Eval("RELEASED_ON2","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected2")?Eval("APP_ON2","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%> </td>

                                                        <td class="Tbltd"><%# Eval("COMMENTS2") %></td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel1" runat="server" Visible='<%# (Eval("APPROVEDBY3")).ToString()==""?false:true %>'>
                                                    <tr>
                                                        <td class="Tbltd"><%# Eval("APPROVEDBY3") %></td>
                                                        <td class="Tbltd"><%# Eval("APPROVEDBY3N") %></td>
                                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected3")?"Rejected": (Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved3")?"Approved":(Eval("STATUS").ToString()=="HOLD3")?"Hold":(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED3")?"Released":(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":"Approved"%> </td>
                                                        <%-- <td class="Tbltd"><%#(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%></td>--%>
                                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved3")?Eval("APP_ON3","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD3")?Eval("HOLD_ON3","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED3")?Eval("RELEASED_ON3","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected3")?Eval("APP_ON3","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%> </td>


                                                        <td class="Tbltd"><%# Eval("COMMENTS3") %></td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel2" runat="server" Visible='<%# (Eval("APPROVEDBY4")).ToString()==""?false:true %>'>
                                                    <tr>
                                                        <td class="Tbltd"><%# Eval("APPROVEDBY4") %></td>
                                                        <td class="Tbltd"><%# Eval("APPROVEDBY4N") %></td>
                                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected4")?"Rejected": (Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved4")?"Approved":(Eval("STATUS").ToString()=="HOLD4")?"Hold":(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED4")?"Released":(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":"Approved"%> </td>
                                                        <%-- <td class="Tbltd"><%#(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%></td>--%>
                                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved4")?Eval("APP_ON4","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD4")?Eval("HOLD_ON4","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED4")?Eval("RELEASED_ON4","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected4")?Eval("APP_ON4","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%> </td>

                                                        <td class="Tbltd"><%# Eval("COMMENTS4") %></td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel3" runat="server" Visible='<%# (Eval("APPROVEDBY5")).ToString()==""?false:true %>'>
                                                    <tr>
                                                        <td class="Tbltd"><%# Eval("APPROVEDBY5") %></td>
                                                        <td class="Tbltd"><%# Eval("APPROVEDBY5N") %></td>
                                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected5")?"Rejected": (Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved5")?"Approved":(Eval("STATUS").ToString()=="HOLD5")?"Hold":(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED5")?"Released":(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":"Approved"%> </td>
                                                        <%--<td class="Tbltd"><%#(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%></td>--%>
                                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved5")?Eval("APP_ON5","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD5")?Eval("HOLD_ON5","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED5")?Eval("RELEASED_ON5","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected5")?Eval("APP_ON5","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%> </td>

                                                        <td class="Tbltd"><%# Eval("COMMENTS5") %></td>
                                                    </tr>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel4" runat="server" Visible='<%# (Eval("APPROVEDBY6")).ToString()==""?false:true %>'>
                                                    <tr>
                                                        <td class="Tbltd"><%# Eval("APPROVEDBY6") %></td>
                                                        <td class="Tbltd"><%# Eval("APPROVEDBY6N") %></td>
                                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":(Eval("STATUS").ToString()=="HOLD6")?"Hold":(Eval("STATUS").ToString()=="HOLD5")||(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED6")?"Released":(Eval("STATUS").ToString()=="RELEASED5")||(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":"Approved"%> </td>
                                                        <%--  <td class="Tbltd"><%#(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%></td>--%>
                                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>

                                                        <td class="Tbltd"><%# Eval("COMMENTS6") %></td>
                                                    </tr>
                                                </asp:Panel>

                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </fieldset>

                    </ContentTemplate>
                </Ajx:TabPanel>
            </Ajx:TabContainer>
        </div>

        <asp:UpdatePanel ID="UPExport" runat="server">
            <ContentTemplate>

                <div class="mb-3">
                    <div id="Exportbtn" runat="server">
                        
                         <asp:Button ID="BtnExporttoXl" runat="server" Text="Export To Excel" OnClick="BtnExporttoXl_Click" CausesValidation="false" CssClass="btn btn-dark waves-effect waves-light btn-std"/>
                        <asp:Button ID="BtnExporttoPDF" runat="server" Text="Export To PDF" OnClick="BtnExporttoPDF_Click" CssClass="btn btn-dark waves-effect waves-light btn-std" />
                        <asp:Button ID="BtnExportHeadertoXl" runat="server" Text="Header To XL" OnClick="BtnExportHeadertoXl_Click" CausesValidation="false" CssClass="btn btn-dark waves-effect waves-light btn-std"/>
                        <asp:Button ID="BtnExportLineitemtoXl" runat="server" Text="Lineitem To XL" OnClick="BtnExportLineitemtoXl_Click" CausesValidation="false" CssClass="btn btn-dark waves-effect waves-light btn-std"/>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnExporttoPDF" />
                <asp:PostBackTrigger ControlID="BtnExporttoXl" />
                <asp:PostBackTrigger ControlID="BtnExportHeadertoXl" />
                <asp:PostBackTrigger ControlID="BtnExportLineitemtoXl" />
            </Triggers>
        </asp:UpdatePanel>
    </asp:Panel>
</asp:Content>
