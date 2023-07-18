<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="FBP_AppRejClaims.aspx.cs"
    Inherits="iEmpPower.UI.FBP.FBP_AppRejClaims" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">
        
        .Divh {
            background-color: #3470A7;
             color: #FFFFFF;
        }

    </style>
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <%--	<li class="breadcrumb-item"><a href="index.html">Home</a></li>
	<li class="breadcrumb-item"><a href="iExpense_Request.html">iExpense Request</a></li>
	<li class="breadcrumb-item active">iExpense View</li>--%>


                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item"><a href="FBP.aspx">FBP</a></li>
                        <li class="breadcrumb-item active">My Pending Approval Requisitions</li>

                    </ol>
                </div>
                <h4 class="page-title">My Pending Approval Requisitions</h4>
            </div>
        </div>
    </div>
    <!-- end page title -->
    <div class="row">
        <div class="col-lg-12">
            <div class="tab-content m-0 p-0">
                <div class="card-box">
                    <div class="responsive-table">
                        <div>
                            <span id="bold" runat="server" style="font-weight: bold"></span>
                        </div>
                        <%--<h4>Approve / Reject FBP Claims</h4>--%>

                        <asp:Label ID="lblmsg" runat="server"></asp:Label><br />
                        <div class="row margin5rem">
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="form-control-file" TabIndex="1">
                                    <asp:ListItem Text="-SELECT-" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="FBP Claim ID" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Allowance ID" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Employee ID" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Employee Name" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="Status" Value="5"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="col-sm-2">
                                <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" TabIndex="2" placeholder="Enter Text"></asp:TextBox>

                            </div>

                            <div class="col-sm-4">
                                <asp:Button ID="btnsearch" runat="server" CssClass="btn-xs btn-secondary" Text="Show Items" TabIndex="3" OnClick="btnsearch_Click" />
                                <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="btn-xs btn-secondary" TabIndex="4" OnClick="btnclear_Click" />
                            </div>
                            <div class="col-sm-4 text-right" id="divcnt" runat="server">
                            </div>

                        </div>
                        <asp:GridView ID="grdFBP_claims" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" HeaderStyle-CssClass="Divh"
                            AllowPaging="true" AllowSorting="true" PageSize="5" OnSorting="grdFBPclaims_Sorting" OnPageIndexChanging="grdFBPclaims_PageIndexChanging"
                            OnRowCommand="grdFBPclaims_RowCommand"
                            DataKeyNames="FBPC_IC,LGART,STATUS,BETRG">
                            <Columns>
                                <asp:BoundField HeaderText="FBP Claim ID" DataField="FBPC_IC" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" HeaderStyle-Width="10%" />
                                <asp:BoundField HeaderText="Employee ID" DataField="CREATED_BY" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" HeaderStyle-Width="10%" />
                                <asp:BoundField HeaderText="Employee Name" DataField="ENAME" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" HeaderStyle-Width="10%" />
                                <%-- <asp:BoundField HeaderText="Entitlement"  DataField="LGART" SortExpression="LGART"   ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center"/>
                                --%>
                                <asp:TemplateField HeaderText="Allowance" >
                                    <ItemTemplate>
                                        <%--<%# Eval("LGART") %> ---%> <%# Eval("ALLOWANCETEXT") %>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <%--<asp:BoundField HeaderText="Amount"  DataField="BETRG" SortExpression="BETRG" DataFormatString="{0:#,##0.00}"  ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"/>--%>

                                <asp:TemplateField HeaderText="Claimed Amount"  HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("BETRG")).ToString("#,##0.00") %>
                                        <%--   <%# Eval("BETRG") %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:BoundField  HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" DataField="BEGDA" SortExpression="BEGDA"  ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center"/>--%>
                                <asp:BoundField HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" DataField="CREATED_ON" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" />
                                <%--<asp:BoundField HeaderText="Status" DataField="STATUS" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" />--%>

                               <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%-- <%# Eval("STATUS") %>--%>
                                        <asp:LinkButton ID="lbtnStatus" Enabled="false" Width="100px" ForeColor="White" runat="server" CssClass='<%#Eval("STATUS").ToString() == "Approved"? "btn btn-xs btn-success  waves-light" :Eval("Status").ToString() == "Rejected" ?"btn btn-xs btn-warning  waves-light btn-block": "btn btn-xs btn-blue  waves-light" %>' CausesValidation="False" Text='<%#  Eval("STATUS").ToString() %>'></asp:LinkButton>
                                    </ItemTemplate>

 

                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnFbpClaimsView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>


                                    </ItemTemplate>
                                    <ItemStyle CssClass="col-center" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                            <SelectedRowStyle BackColor="Silver" />
                        </asp:GridView>

                        <div id="divclaims" runat="server" visible="false">
                            <h4>Claim for the reimbursement :
                                <asp:Label runat="server" ID="LblReimbursement"></asp:Label></h4>
                            <%--<asp:GridView ID="grd_CalimsDetails" runat="server" AutoGenerateColumns="false" CssClass="gridview" HeaderStyle-CssClass="Divh" Width="70%">
        <Columns>
             <asp:BoundField HeaderText="Claim Date" DataField="Claim_Date" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center"/>
                
             <asp:BoundField HeaderText="Claim No" DataField="Claim_No" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center"/>
                
               <asp:BoundField HeaderText="Claims Amount" DataField="Claims_Amnt" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"/>
                
        <asp:BoundField HeaderText="Remarks" DataField="Remarks" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center"/>
          
        </Columns>
    </asp:GridView>

    <br />--%>

                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None"
                                OnRowCommand="GridView1_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Bill No." DataField="BILL_NO" />

                                    <asp:BoundField HeaderText="Bill Date" DataField="BILL_DATE" DataFormatString="{0:dd-MM-yyyy}" />

                                    <asp:BoundField HeaderText="Remarks" DataField="RELATIONSHIP" />

                                    <%--   <asp:BoundField HeaderText="Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" DataField="BILL_AMT" />--%>

                                    <asp:TemplateField HeaderText="Claimed Amount">
                                        <ItemTemplate>
                                            <%# Convert.ToDouble(Eval("BILL_AMT")).ToString("0.00") %>
                                            <%--   <%# Eval("BILL_AMT") %>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Attachments
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />--%>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="Lbtndownload" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>

                            <br />
                            <table class="tablebody" border="0" cellpadding="1" cellspacing="1" id="tblamtremarks" runat="server">

                                <tr>
                                    <td class="form-control-file">Approved Amount : </td>
                                    <td>
                                        <asp:TextBox ID="txtAmnt" runat="server" CssClass="form-control-file" TabIndex="5"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FTB_txtAmnt" runat="server" TargetControlID="txtAmnt" FilterMode="ValidChars" FilterType="Custom,Numbers" ValidChars="."></cc1:FilteredTextBoxExtender>
                                        <asp:RegularExpressionValidator ID="REVtxtAmounte" runat="server" ControlToValidate="txtAmnt"
                                            ErrorMessage="Please enter the amount greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                            ValidationGroup="vg1"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="form-control-file">Remarks : </td>
                                    <td>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control-file" TextMode="MultiLine" TabIndex="6" ValidationGroup="vg1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFV_txtRemarks" runat="server" ControlToValidate="txtRemarks" ErrorMessage="Please enter Remarks" ForeColor="Red" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>

                            <div class="buttonrow" id="divbutton" runat="server">
                                <asp:HiddenField ID="HF_FBPID" runat="server" />
                                <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" TabIndex="7" ValidationGroup="vg1" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" TabIndex="8" ValidationGroup="vg1" CssClass="btn bg-danger waves-effect waves-light btn-std" />


                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="viewcheck" runat="server" />
</asp:Content>
