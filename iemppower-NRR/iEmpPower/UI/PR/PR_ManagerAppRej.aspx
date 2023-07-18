<%@ Page Title="PR Approval" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="PR_ManagerAppRej.aspx.cs" Inherits="iEmpPower.UI.PR.PR_ManagerAppRej"
    Theme="SkinFile" EnableEventValidation="false" Culture="en-GB" MaintainScrollPositionOnPostback="true" %>

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
                        <li class="breadcrumb-item active">My Pending Approval Requisitions</li>
                    </ol>
                </div>
                <h4 class="page-title">PR Requisition Details
                 <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <!-- end page title -->
    <div class="table-responsive card-box" runat="server" visible="false">
        <h4>My Pending Approval Requisitions</h4>

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

                <td align="right">Created On </td>
                <td style="margin-left: 80px">
                    <asp:TextBox ID="txtCreatedOn" runat="server" TabIndex="3" placeholder="Select Date" CssClass="form-control-file"></asp:TextBox>
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
        <asp:GridView ID="grdPRAppRej" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" OnRowCommand="grdPRAppRej_RowCommand"
            DataKeyNames="PRID,IN_BUDGET,MIS_GRPC,MIS_GRPA,MIS_GRPB,BWERKS,SWERKS,CAPITALIZED,CAP_TEXT,CREATEDBY,STATUS" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPRAppRej_PageIndexChanging"
            AllowSorting="true" OnSorting="grdPRAppRej_Sorting">
            <Columns>
                <asp:BoundField DataField="PRID" HeaderText="PR No." />
                <asp:BoundField DataField="IPERNR" HeaderText="Indentor" />
                <asp:BoundField DataField="RPERNR" HeaderText="Requestor" />
                <asp:BoundField DataField="SUG_SUPP" HeaderText="Originator" />
                <asp:BoundField DataField="IN_BUDGET" HeaderText="In Budget" />
                <asp:BoundField DataField="CRITICALITY" HeaderText="Criticality" />
                <asp:BoundField DataField="BNFPO" HeaderText="Capex" />
                <asp:TemplateField HeaderText="Total Amount">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Convert.ToDouble(Eval("UNIT_PRICE")).ToString("#,##0.00") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:BoundField DataField="WAERS" HeaderText="Currency" />
                <asp:TemplateField HeaderText="Total Amount (INR)">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#  Convert.ToDouble(Eval("TAINRAmt")).ToString("#,##0.00") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Right" />
                </asp:TemplateField>
                <asp:BoundField DataField="CREATED_ON1" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="STATUS" HeaderText="Status" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LbtnPurchaseItemView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>View</asp:LinkButton>
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

    <div id="ViewPRIfo" runat="server" class="table-responsive card-box">
        <%-- visible="false">--%>
        <%--<h4>Requisition Details</h4>--%>
        <%-- <Ajx:TabContainer ID="DisplayPRInfo" runat="server" Width="100%" ActiveTabIndex="1" TabIndex="6">
            <Ajx:TabPanel runat="server" HeaderText="&nbsp;&nbsp;&nbsp;Indent.&nbsp;Details&nbsp;&nbsp;&nbsp;" ID="IndentDetailsTabPanel">
                <ContentTemplate>

                    <fieldset>--%>
        <div class="bg-light" style="padding-top: 10px; padding-left: 5px; padding-bottom: 10px; border-bottom: 1.5px solid #dee2e6; border-top: 1.5px solid #dee2e6">
          <div class="row">
                <div class="col-sm-6">
                    <h4>PR Header Details</h4>
                </div>
                <div class="col-sm-6 text-right">
                    <asp:LinkButton ID="LbtnEdit" runat="server" Text="Edit" CssClass="btn btn-warning"
                        CommandName="Edit" CausesValidation="False" OnClick="LbtnEdit_Click" Visible="false"></asp:LinkButton>

 

                    <asp:LinkButton ID="LbtnSave" runat="server" Text="Save" CssClass="btn btn-secondary"
                        CommandName="Save" CausesValidation="true" OnClick="LbtnSave_Click" Visible="false" ValidationGroup="capitalixed"></asp:LinkButton>
                </div>
            </div>
        </div>
      <%--  <div class="DivSpacer01">
            <asp:LinkButton ID="LbtnEdit" runat="server" Text="Edit"
                CommandName="Edit" CausesValidation="False" OnClick="LbtnEdit_Click" Visible="false"></asp:LinkButton>
            &nbsp;&nbsp;&nbsp;
                                       <asp:LinkButton ID="LbtnSave" runat="server" Text="Save"
                                           CommandName="Save" CausesValidation="true" OnClick="LbtnSave_Click" Visible="false" ValidationGroup="capitalixed"></asp:LinkButton>


        </div>--%>

        <asp:FormView ID="FV_PRInfoDisplay" runat="server" Width="99%" OnItemCommand="FV_PRInfoDisplay_ItemCommand" GridLines="None">
            <ItemTemplate>
                <div id="div1" style="text-align: left;">
                    <table id="formTable" style="border-collapse: collapse; width: 100%; color: #555555;">
                        <tr>
                            <td class="tdwidth1"><b>PR No</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><b><%#Eval("BANFN_EXT") %></b></td>

                            <td class="tdwidth1"><b>Capitalization</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("CAPITALIZED") %>

                                <asp:RadioButton ID="rbtnCapitalizedYes" runat="server" GroupName="Capitalized" TabIndex="12" Text="Yes" Visible="false" AutoPostBack="true" OnCheckedChanged="rbtnCapitalizedYes_CheckedChanged" />
                                <asp:RadioButton ID="rbtnCapitalizedNo" runat="server" GroupName="Capitalized" TabIndex="13" Text="No" Visible="false" AutoPostBack="true" OnCheckedChanged="rbtnCapitalizedNo_CheckedChanged1" />
                                &nbsp;&nbsp;
                <asp:TextBox ID="txtWillthisItembeCapitalized" runat="server" CssClass="textbox" TabIndex="14" Enabled="False" Visible="false"></asp:TextBox>

                                <asp:RequiredFieldValidator ID="RFVWillthisItembeCapitalized" runat="server" ErrorMessage="Select Item to be Capitalized" ForeColor="Red" ControlToValidate="txtWillthisItembeCapitalized" Enabled="false" Visible="false"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="REV_txtCapitalized" runat="server" ValidationGroup="capitalixed"
                                    ControlToValidate="txtWillthisItembeCapitalized" ErrorMessage="Single qoutes not allowed" ForeColor="Red"
                                    ValidationExpression="^[^']+$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdwidth1"><b>Requestor</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("RPERNR") %>-<%#Eval("ENAME") %>
                            </td>

                            <td class="tdwidth1"><b>Managed Service</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SERVICE_BUREA") %>
                            </td>
                        </tr>
                        <tr>

                            <td class="tdwidth1"><b>Main Function</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("PFUNC_AREA") %></td>

                            <td class="tdwidth1"><b>Criticality</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("CRITICALITY") %>
                            </td>

                        </tr>
                        <tr>
                            <td class="tdwidth1"><b>Sub Function</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("BTEXT") %>  </td>

                            <td class="tdwidth1"><b>ERP Project Code</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("PSPNR") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="tdwidth1"><b>MIS Group C</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("MIS_GRPC") %>
                                <asp:DropDownList ID="ddlMISGroupC" runat="server" Width="150px" CssClass="textbox" AutoPostBack="true" OnSelectedIndexChanged="ddlMISGroupC_SelectedIndexChanged" Visible="false" TabIndex="7"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select MIS Group C" ForeColor="Red" ControlToValidate="ddlMISGroupC" InitialValue="0"></asp:RequiredFieldValidator>

                            </td>

                            <td class="tdwidth1"><b>Billable</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("BILLABLE") %>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdwidth1"><b>MIS Group A</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("MIS_GRPA") %>
                                <asp:TextBox ID="txtMISGroupA" runat="server" CssClass="textbox" Visible="false" Enabled="false"></asp:TextBox>
                            </td>

                            <td class="tdwidth1"><b>Business Unit</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SPART") %>
                            </td>
                        </tr>

                        <tr>

                            <td class="tdwidth1"><b>MIS Group B</b></td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("MIS_GRPB") %>
                                <asp:TextBox ID="txtMISGroupB" runat="server" CssClass="textbox" Visible="false" Enabled="false"></asp:TextBox></td>

                            <td class="tdwidth1"><b>Region</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("REGIONTXT") %>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdwidth1"><b>Requestor Region</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("EKGRP") %>
                            </td>

                            <td class="tdwidth1"><b>Justification</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("JUSTIFICATION") %>
                            </td>
                        </tr>

                        <tr>

                            <td class="tdwidth1"><b>Bill to address</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("BWERKS") %>

                                <asp:DropDownList ID="ddlBillToAddress" runat="server" Width="150px" CssClass="textbox" Visible="false" TabIndex="8"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVBillToAddress" runat="server" ErrorMessage="Select Bill To Address" ForeColor="Red" ControlToValidate="ddlBillToAddress" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>

                            <td class="tdwidth1"><b>Special Notes</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SPL_NOTES") %>
                            </td>

                        </tr>

                        <tr>
                            <td class="tdwidth1"><b>Ship to Address</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SWERKS") %>
                                <asp:DropDownList ID="ddlShipToAddress" runat="server" Width="150px" CssClass="textbox" Visible="false" TabIndex="9"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVBillToAddress0" runat="server" ErrorMessage="Select Ship To Address" ForeColor="Red" ControlToValidate="ddlBillToAddress" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>

                            <td class="tdwidth1"><b>Proposal</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#(Eval("PROPOSAL").ToString()=="YES") ? Eval("PFID") : "<span>NA</span>" %>' Font-Bold="True" CommandName="downloadp" CommandArgument='<%# Eval("PFPATH") %>' CausesValidation="false" Enabled='<%# bool.Parse(string.Format("{0}", Eval("PROPOSAL").ToString()=="YES" ? "true" : "false"))%>' />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="LinkButton1" />
                                    </Triggers>

                                </asp:UpdatePanel>
                            </td>


                        </tr>




                        <tr>

                            <td class="tdwidth1"><b>Suggested Supplier</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SUG_SUPP") %>
                            </td>

                            <td class="tdwidth1"><b>Agreement</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" Text='<%#(Eval("AGREEMENT").ToString()=="YES") ? Eval("AFID") : "<span>NA</span>" %>' Font-Bold="True" CommandName="downloada" CommandArgument='<%# Eval("AFPATH") %>' CausesValidation="false" Enabled='<%# bool.Parse(string.Format("{0}", Eval("AGREEMENT").ToString()=="YES" ? "true" : "false"))%>' />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="LinkButton2" />
                                    </Triggers>

                                </asp:UpdatePanel>
                            </td>

                        </tr>
                        <tr>
                            <td class="tdwidth1"><b>Supplier Address</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SUP_ADDRESS") %>
                            </td>

                            <td class="tdwidth1"><b>Invoice</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton4" runat="server" Text='<%#(Eval("INVOICE").ToString()=="YES") ? Eval("IFID") : "<span>NA</span>" %>' Font-Bold="True" CommandName="downloadi" CommandArgument='<%# Eval("IFPATH") %>' CausesValidation="false" Enabled='<%# bool.Parse(string.Format("{0}", Eval("INVOICE").ToString()=="YES" ? "true" : "false"))%>' />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="LinkButton4" />
                                    </Triggers>

                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        <tr>

                            <td class="tdwidth1"><b>Supplier Phone No</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("SUP_PHONE") %>
                            </td>

                            <td class="tdwidth1"><b>Email</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" Text='<%#(Eval("EMAIL_COM").ToString()=="YES") ? Eval("EFID") : "<span>NA</span>" %>' Font-Bold="True" CommandName="downloade" CommandArgument='<%# Eval("EFPATH") %>' CausesValidation="false" Enabled='<%# bool.Parse(string.Format("{0}", Eval("EMAIL_COM").ToString()=="YES" ? "true" : "false"))%>' />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="LinkButton3" />
                                    </Triggers>

                                </asp:UpdatePanel>
                            </td>

                        </tr>

                        <tr>
                            <td class="tdwidth1"><b>Budget line item</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("CAP_TEXT") %>
                            </td>

                            <td class="tdwidth1"><b>Total Amount</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><b><%# Convert.ToDouble(Eval("TOTAL")).ToString("N2") %>(<%#Eval("WAERS") %>)</b>
                            </td>
                        </tr>

                        <tr>
                            <td class="tdwidth1"><b>In Budget</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%#Eval("IN_BUDGET") %>

                                <asp:RadioButton ID="rbtnBudgetYes" runat="server" GroupName="Budget" Text="Yes" Visible="false" TabIndex="10" />
                                <asp:RadioButton ID="rbtnBudgetNo" runat="server" GroupName="Budget" Text="No" Visible="false" TabIndex="11" />
                            </td>

                            <td class="tdwidth1"><b>Submit Date</b> </td>
                            <td class="tdwidth2"><b>:</b></td>
                            <td class="tdwidth3"><%# Eval("CREATED_ON1", "{0:dd/MM/yyyy}") %>
                            </td>

                        </tr>
                    </table>
                </div>
                <br />
                <br />
            </ItemTemplate>
        </asp:FormView>
        <%-- </fieldset>
                </ContentTemplate>
            </Ajx:TabPanel>--%>

        <%--  <Ajx:TabPanel ID="IndentItems" runat="server" HeaderText="&nbsp;&nbsp;&nbsp;Indent.&nbsp;Items&nbsp;&nbsp;&nbsp;">

                <ContentTemplate>

                    <fieldset class="FldSetCls">--%>
        <div class="DivSpacer01"></div>
        <br />
        <br />
        <div class="bg-light" style="padding-top: 10px; padding-left: 5px; padding-bottom: 10px; border-bottom: 1.5px solid #dee2e6; border-top: 1.5px solid #dee2e6">
            <h4>PR Item Details</h4>
        </div>
        <asp:GridView ID="GV_PrItems" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" DataKeyNames="BNFPO,PRID">
            <Columns>
                <asp:BoundField DataField="BNFPO" HeaderText="Item No" />
                <asp:BoundField DataField="TXZ01" HeaderText="Item Description" />
                <asp:BoundField DataField="NO_OF_UNITS" HeaderText="No. of Units" />
                <asp:BoundField DataField="MEINS" HeaderText="Unit of Measurements" />
                <%-- <asp:BoundField DataField="UNIT_PRICE" HeaderText="Unit Price" ItemStyle-HorizontalAlign="Right" />--%>
                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                    <ItemTemplate>
                        <%# Convert.ToDouble(Eval("UNIT_PRICE")).ToString("#,##0.00") %>
                    </ItemTemplate>
                </asp:TemplateField>
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

        <%-- <br>
            <br>
                <br></br>
                <br></br>
            </br>
        </br>--%>

        <%-- </fieldset>
                </ContentTemplate>
            </Ajx:TabPanel>--%>

        <%--  <Ajx:TabPanel ID="AppHistory" runat="server" HeaderText="&nbsp;&nbsp;&nbsp;Approval&nbsp;Status&nbsp;&nbsp;&nbsp;" Visible="false">
                <ContentTemplate>

                    <fieldset class="FldSetCls">--%>
        <div class="DivSpacer01"></div>
        <br />


        <asp:GridView ID="grdAppHistory" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" DataKeyNames="APPROVEDBY1,APPROVEDBY2,APPROVEDBY3,APPROVEDBY4,APPROVEDBY5,APPROVEDBY6"
            OnRowDataBound="grdAppHistory_RowDataBound" ShowHeader="false" Visible="false">
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
                                <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved1")?Eval("APP_ON1","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD1")?Eval("HOLD_ON1","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED1")?Eval("RELEASED_ON1","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected1")?Eval("APP_ON1","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%> </td>

                                <td class="Tbltd"><%# Eval("COMMENTS1") %></td>
                            </tr>
                            <asp:Panel ID="pnlAPPROVEDBY2" runat="server" Visible='<%# (Eval("APPROVEDBY2")).ToString()==""?false:true %>'>
                                <tr>
                                    <td class="Tbltd"><%# Eval("APPROVEDBY2") %></td>
                                    <td class="Tbltd"><%# Eval("APPROVEDBY2N") %></td>
                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Approved2")?"Approved":(Eval("STATUS").ToString()=="HOLD2")?"Hold":(Eval("STATUS").ToString()=="RELEASED2")?"Released":"Approved"%> </td>


                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved2")?Eval("APP_ON2","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD2")?Eval("HOLD_ON2","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED2")?Eval("RELEASED_ON2","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected2")?Eval("APP_ON2","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%> </td>

                                    <td class="Tbltd"><%# Eval("COMMENTS2") %></td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="Panel1" runat="server" Visible='<%# (Eval("APPROVEDBY3")).ToString()==""?false:true %>'>
                                <tr>
                                    <td class="Tbltd"><%# Eval("APPROVEDBY3") %></td>
                                    <td class="Tbltd"><%# Eval("APPROVEDBY3N") %></td>
                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected3")?"Rejected": (Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved3")?"Approved":(Eval("STATUS").ToString()=="HOLD3")?"Hold":(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED3")?"Released":(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":"Approved"%> </td>

                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved3")?Eval("APP_ON3","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD3")?Eval("HOLD_ON3","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED3")?Eval("RELEASED_ON3","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected3")?Eval("APP_ON3","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%> </td>


                                    <td class="Tbltd"><%# Eval("COMMENTS3") %></td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="Panel2" runat="server" Visible='<%# (Eval("APPROVEDBY4")).ToString()==""?false:true %>'>
                                <tr>
                                    <td class="Tbltd"><%# Eval("APPROVEDBY4") %></td>
                                    <td class="Tbltd"><%# Eval("APPROVEDBY4N") %></td>
                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected4")?"Rejected": (Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved4")?"Approved":(Eval("STATUS").ToString()=="HOLD4")?"Hold":(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED4")?"Released":(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":"Approved"%> </td>

                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved4")?Eval("APP_ON4","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD4")?Eval("HOLD_ON4","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED4")?Eval("RELEASED_ON4","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected4")?Eval("APP_ON4","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%> </td>

                                    <td class="Tbltd"><%# Eval("COMMENTS4") %></td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="Panel3" runat="server" Visible='<%# (Eval("APPROVEDBY5")).ToString()==""?false:true %>'>
                                <tr>
                                    <td class="Tbltd"><%# Eval("APPROVEDBY5") %></td>
                                    <td class="Tbltd"><%# Eval("APPROVEDBY5N") %></td>
                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected5")?"Rejected": (Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved5")?"Approved":(Eval("STATUS").ToString()=="HOLD5")?"Hold":(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED5")?"Released":(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":"Approved"%> </td>

                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved5")?Eval("APP_ON5","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD5")?Eval("HOLD_ON5","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED5")?Eval("RELEASED_ON5","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected5")?Eval("APP_ON5","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%> </td>

                                    <td class="Tbltd"><%# Eval("COMMENTS5") %></td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="Panel4" runat="server" Visible='<%# (Eval("APPROVEDBY6")).ToString()==""?false:true %>'>
                                <tr>
                                    <td class="Tbltd"><%# Eval("APPROVEDBY6") %></td>
                                    <td class="Tbltd"><%# Eval("APPROVEDBY6N") %></td>
                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":(Eval("STATUS").ToString()=="HOLD6")?"Hold":(Eval("STATUS").ToString()=="HOLD5")||(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED6")?"Released":(Eval("STATUS").ToString()=="RELEASED5")||(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":"Approved"%> </td>

                                    <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>

                                    <td class="Tbltd"><%# Eval("COMMENTS6") %></td>
                                </tr>
                            </asp:Panel>

                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>




        <%--    <br></br>
                        <br></br>
                        <br></br>
                        <br></br>
                        <br></br>
                        <br></br>
                        <br></br>

                    </fieldset>
                </ContentTemplate>
            </Ajx:TabPanel>
        </Ajx:TabContainer>--%>

        <br />
        <asp:Label ID="Label22" runat="server" Text="*Remarks" CssClass="label"></asp:Label>
        <asp:TextBox ID="TxtRemarks" runat="server" CssClass="textbox" TextMode="MultiLine" TabIndex="16" Text="APPROVED"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RFV_TxtRemarks" runat="server" ControlToValidate="TxtRemarks" ErrorMessage="Please enter the Remarks." ForeColor="Red" ValidationGroup="V1"></asp:RequiredFieldValidator>

        <br />
        <br />
        <div class="mb-3">
            &nbsp;<asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" TabIndex="17" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" ValidationGroup="V1" TabIndex="18" CssClass="btn bg-danger waves-effect waves-light btn-std" />&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnHold" runat="server" Text="Hold" OnClick="btnHold_Click" ValidationGroup="V1" TabIndex="19" CssClass="btn bg-danger waves-effect waves-light btn-std" />&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnRelease" runat="server" Text="Release" OnClick="btnRelease_Click" ValidationGroup="V1" TabIndex="20" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
        </div>

    </div>
    <asp:HiddenField ID="HF_PRID" runat="server" />
    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var allCheckBoxSelector = '#<%=grdPRAppRej.ClientID%> input[id*="chkAll"]:checkbox';
        var checkBoxSelector = '#<%=grdPRAppRej.ClientID%> input[id*="chkSelected"]:checkbox';

        function ToggleCheckUncheckAllOptionAsNeeded() {
            var totalCheckboxes = $(checkBoxSelector),
                checkedCheckboxes = totalCheckboxes.filter(":checked"),
                noCheckboxesAreChecked = (checkedCheckboxes.length === 0),
                allCheckboxesAreChecked = (totalCheckboxes.length === checkedCheckboxes.length);

            $(allCheckBoxSelector).attr('checked', allCheckboxesAreChecked);
        }

        $(document).ready(function () {
            $(allCheckBoxSelector).live('click', function () {
                $(checkBoxSelector).attr('checked', $(this).is(':checked'));

                ToggleCheckUncheckAllOptionAsNeeded();
            });

            $(checkBoxSelector).live('click', ToggleCheckUncheckAllOptionAsNeeded);

            ToggleCheckUncheckAllOptionAsNeeded();
        });
    </script>

</asp:Content>
