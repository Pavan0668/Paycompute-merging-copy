<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="FBP.aspx.cs" Inherits="iEmpPower.UI.FBP.FBP" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <style type="text/css">
        .close {
            background-color: white !important;
            border: none !important;
            font-size: small;
        }

        .popUpStyle {
            /*font: normal 11px auto "Trebuchet MS", Verdana;*/
            background-color: #000000;
            /*color: #4f6b72;*/
            /*padding: 6px;*/
            filter: alpha(opacity=80);
            opacity: 0.15;
        }

        .right {
            text-align: right !important;
        }

        .btn-block {
            color: white !important;
        }

        #ContentPlaceHolder1_MainContent_grd_FbpClaims_History tr td:nth-child(8) a
        {
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
                        <li class="breadcrumb-item active">FBP</li>
                    </ol>
                </div>
                <h4 class="page-title">FBP
                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <!-- end page title -->

    <div class="row">
        <%-- <ul class="nav nav-pills navtab-bg" style="margin-left: 6px;">
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab1" class="nav-link active p-2" OnClick="Tab1_Click"><i class="fe-bell noti-icon"></i>
   FBP Declaration </asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab2" class="nav-link p-2" OnClick="Tab2_Click"><i class="fe-bell noti-icon"></i>
   My FBP Claims</asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab3" class="nav-link p-2" OnClick="Tab3_Click"><i class="fe-file-text"></i>
   FBP - Claims History</asp:LinkButton></li>
        </ul>--%>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <ul class="nav nav-pills navtab-bg" style="margin-left: 6px;">
                    <li class="nav-item font-16">
                        <asp:LinkButton runat="server" ID="Tab1" class="nav-link active p-2" OnClick="Tab1_Click"><i class="fe-bell noti-icon"></i>
   FBP Declaration </asp:LinkButton></li>
                    <li class="nav-item font-16">
                        <asp:LinkButton runat="server" ID="Tab2" class="nav-link p-2" OnClick="Tab2_Click"><i class="fe-bell noti-icon"></i>
   My FBP Claims</asp:LinkButton></li>
                    <li class="nav-item font-16">
                        <asp:LinkButton runat="server" ID="Tab3" class="nav-link p-2" OnClick="Tab3_Click"><i class="fe-file-text"></i>
   FBP - Claims History</asp:LinkButton></li>
                    <%--            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab3" class="nav-link p-2" OnClick="Tab3_Click"><i class="fe-file-text"></i>
   FBP - Pending My Action</asp:LinkButton></li>
            <li class="nav-item font-16">
                <asp:LinkButton runat="server" ID="Tab4" class="nav-link p-2" OnClick="Tab4_Click"><i class="fe-file-text"></i>
   FBP - Completed My Action</asp:LinkButton></li>--%>
                </ul>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="Tab1" />
                <asp:PostBackTrigger ControlID="Tab2" />
                <asp:PostBackTrigger ControlID="Tab3" />
            </Triggers>
        </asp:UpdatePanel>


        <!-- Tab Panel Start / -->
        <div class="col-xl-12 m-t-20">
            <div class="tab-content m-0 p-0">
                <asp:MultiView ID="MainView" runat="server">
                    <asp:View ID="View1" runat="server">

                        <%--   <div class="tab-pane active" id="Tab-1">--%>
                        <div id="Tab-1">
                            <!-- Tab Panel Start / -->

                            <div class="tab-pane active" id="FBP">
                                <div class="tab-content m-0 p-0">
                                    <div class="card-box" id="divview" runat="server" visible="true">
                                        <h4 class="header-title">Flexible Benefit Plan Declaration:April 1<sup>st</sup>
                                            <asp:Label ID="LblFromDate" runat="server"></asp:Label>
                                            To March 31<sup>st</sup>
                                            <asp:Label ID="LblToDate" runat="server"></asp:Label>
                                        </h4>


                                        <%--</div>--%>
                                        <%--<div id="divview" runat="server" visible="true">--%>
                                        <div class="alert-info">
                                            <table class="table table-borderless">
                                                <thead>
                                                    <tr class="font-16 text-dark">
                                                        <th>Plan:</th>
                                                        <th>
                                                            <asp:Label ID="lblPlanDate" runat="server" CssClass="label"></asp:Label></th>
                                                        <th>Basket Total:</th>
                                                        <th>
                                                            <asp:Label ID="lblBasketTotalAmount" runat="server" CssClass="hidden"></asp:Label>
                                                            <asp:Label ID="lblBasketTotalAmount2" runat="server" Text='<%#Eval(lblBasketTotalAmount.Text.ToString()) %>'></asp:Label>
                                                        </th>
                                                        <th class="text-right table_font_sm">
                                                         <span class="fe-info"></span>   <asp:Label ID="Label1" CssClass= "alert-warning" Font-Size="Small" runat="server" Text="Please click on '+' icon and update requested details, before updating Annual allocation amount."></asp:Label>
                                                        </th>
                                                    </tr>
                                                </thead>
                                            </table>
                                        </div>

                                        <div class="table-responsive">
                                            <%--  <table class="table table-sm mb-0 table_font_sm">
                                                        <thead>
                                                            <tr class="text-dark text-right">
                                                                <th class="text-left">#</th>
                                                                <th class="text-left">Heads of Allowances</th>
                                                                <th>Annual Entitlement</th>
                                                                <th>Monthly Allocation</th>
                                                                <th>Annual Allocation</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <th scope="row">1200</th>
                                                                <td>Meal Voucher</td>
                                                                <td class="text-right">26400.0</td>
                                                                <td class="text-right">0.00</td>
                                                                <td>
                                                                    <input value="0.0" class="form-control-file text-right"></td>
                                                            </tr>
                                                            <tr>
                                                                <th scope="row">1205</th>
                                                                <td>Medical</td>
                                                                <td class="text-right">15000.0</td>
                                                                <td class="text-right">1250.00</td>
                                                                <td>
                                                                    <input value="15000.0" class="form-control-file text-right"></td>
                                                            </tr>
                                                            <tr>
                                                                <th scope="row">1215</th>
                                                                <td>LTA</td>
                                                                <td class="text-right">75000.0</td>
                                                                <td class="text-right">0.00</td>
                                                                <td>
                                                                    <input value="0.0" class="form-control-file text-right"></td>
                                                            </tr>
                                                            <tr>
                                                                <th scope="row">1255</th>
                                                                <td>Mobile & Telephone Reimbursement</td>
                                                                <td class="text-right">36000.0</td>
                                                                <td class="text-right">0.00</td>
                                                                <td>
                                                                    <input value="0.0" class="form-control-file text-right"></td>
                                                            </tr>
                                                            <tr>
                                                                <th scope="row">1276</th>
                                                                <td>Mobile Purchase</td>
                                                                <td class="text-right">15000.0</td>
                                                                <td class="text-right">0.00</td>
                                                                <td>
                                                                    <input value="0.0" class="form-control-file text-right"></td>
                                                            </tr>
                                                            <tr class="text-dark text-right">
                                                                <th colspan="3"></th>
                                                                <th>Total Allocated</th>
                                                                <th>15000.00</th>
                                                            </tr>
                                                            <tr class="text-dark text-right">
                                                                <th colspan="3"></th>
                                                                <th>FBP Special Allowance</th>
                                                                <th>55542.00</th>
                                                            </tr>
                                                        </tbody>
                                                    </table>--%>
                                            <asp:GridView ID="GV_FBPDeclare" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" DataKeyNames="ALLOWANCEID" ShowFooter="true"
                                                FooterStyle-HorizontalAlign="Right" BorderStyle="None" OnRowDataBound="GV_FBPDeclare_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="ALLOWANCEID" HeaderText="Id" HeaderStyle-Width="10%"></asp:BoundField>
                                                    <%--  <asp:TemplateField HeaderText="Sl.No." HeaderStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="col-center" />
                                                    </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="ALLOWANCETEXT" HeaderText="Heads of Allowances" HeaderStyle-Width="23%">
                                                        <ItemStyle HorizontalAlign="left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AMOUNT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Annual Entitlement" ItemStyle-CssClass="right" HeaderStyle-Width="23%" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <%# Convert.ToDouble(Eval("AMOUNT")).ToString("#,##0.00") %>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            Total Allocated<br />
                                                            FBP Special Allowance
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Monthly Allocation" ItemStyle-CssClass="right" FooterStyle-CssClass="right" HeaderStyle-Width="22%" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMonthlyAllocation" runat="server" Text='<%#Convert.ToDecimal(Eval("MONTHLY")).ToString("#,##0.00") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblAnnualAllocationTotal" CssClass="right" runat="server" /><br />
                                                            <asp:Label ID="lblFBPSpecialAllowance" runat="server" CssClass="right" />
                                                            <%-- <asp:CompareValidator ID="CompareValidator1" runat="server"
                                                                ControlToValidate="lblFBPSpecialAllowance"
                                                                Operator="GreaterThanEqual"
                                                                ValueToCompare="0"
                                                                Type="Integer"
                                                                ErrorMessage="Special Allowance cannot be negative"
                                                                Display="Dynamic"
                                                                Text="*" />--%>
                                                            <asp:HiddenField ID="HFAllocationTotal" runat="server" />

                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Annual Allocation" HeaderStyle-Width="18%" HeaderStyle-CssClass="right">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAnnualAllocation" runat="server" CssClass="form-control right" Style="text-align: right" onkeyup="calculateGrandTotal();" ValidationGroup="VG1" Text='<%# Eval("ANNUAL") %>' OnTextChanged="txtAnnualAllocation_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <%--  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtAnnualAllocation">
                                                            </cc1:FilteredTextBoxExtender>
                                                            --%>
                                                            <asp:Label ID="lblAnnualAllocation" runat="server" Text="0.0" Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="1%">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hyp_redirct"
                                                                runat="server" Visible="false"><i class="fe-plus-square"></i></asp:HyperLink>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" FilterType="Custom, Numbers" ValidChars="." runat="server" Enabled="True" TargetControlID="txtAnnualAllocation">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="18%">
                                                        <ItemStyle />
                                                        <ItemTemplate>
                                                            <asp:RangeValidator ID="RVtxtAnnualAllocation" runat="server" ErrorMessage="Cannot declare more than eligibility" ControlToValidate="txtAnnualAllocation"
                                                                MinimumValue="0" MaximumValue='<%# Eval("AMOUNT") %>' Type="Double" ForeColor="Red" Display="Dynamic" ValidationGroup="VG1" SetFocusOnError="true"></asp:RangeValidator>
                                                            <asp:CompareValidator ID="CVClaimexists" runat="server"
                                                                ControlToValidate="txtAnnualAllocation"
                                                                Operator="GreaterThanEqual"
                                                                ValueToCompare="0"
                                                                Type="Double"
                                                                ErrorMessage="Cannot be zero when there are claims exists"
                                                                ForeColor="Red"
                                                                Display="Dynamic"
                                                                ValidationGroup="VG1"
                                                                SetFocusOnError="true"
                                                                Enabled="false" /><%--Operator="NotEqual"--%>

                                                            <asp:RangeValidator ID="rgtxtAnnualAllocation" runat="server" ControlToValidate="txtAnnualAllocation" ValidationGroup="VG1" Enabled="false" MaximumValue="0.0" MinimumValue=".0" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Incorrect Entry!" ForeColor="Red" Type="Double"></asp:RangeValidator>

                                                            <asp:CustomValidator ID="custmtxtAnnualAllocation" ForeColor="Red" runat="server" ValidationGroup="VG1" OnServerValidate="custmtxtAnnualAllocation_ServerValidate" ControlToValidate="txtAnnualAllocation" SetFocusOnError="true" Display="Dynamic"></asp:CustomValidator>
                                                              
                                                            
                                                            <asp:Label ID="lblError" ForeColor="Red" runat="server" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <%--<HeaderStyle CssClass="borderhide" />
                                                                <ItemStyle CssClass="borderhide" />
                                                                <FooterStyle CssClass="borderhide" />--%>
                                                    </asp:TemplateField>
                                                </Columns>

                                            </asp:GridView>

                                        </div>
                                    </div>

                                    <div class="mb-3">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" OnClick="btnSubmit_Click" OnClientClick="return validate('Submit');" ValidationGroup="VG1" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" OnClick="btnCancel_Click" OnClientClick="javascript:return confirm('Do you want to Cancel ?')" CausesValidation="False" />
                                    </div>

                                    <div class="card-box bg-brand">
                                        <div class="table-responsive">
                                            <table class="table table-sm text-white mb-0  table_font_sm">
                                                <thead>
                                                    <tr>
                                                        <th>Joining Date</th>
                                                        <th>Effective Date</th>
                                                        <th>Last Updated Date</th>
                                                        <th>Exit Date</th>
                                                        <%-- <th>Pay Date</th>--%>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblJoiningDate" runat="server" Enabled="False"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblEffectiveDate" runat="server" Enabled="False"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblLastUpdatedDate" runat="server" Enabled="False"></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblPayDate" runat="server" Enabled="False"></asp:Label></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>


                                </div>

                                <div class="card-box border-0" role="alert">
                                    <div class="table-responsive">
                                        <h4>Declaration of Current Financial Year</h4>
                                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                        <%-- <table class="table table-sm mb-0 table_font_sm">
                                                        <thead>
                                                            <tr class="text-dark">
                                                                <th>Sl.No.</th>
                                                                <th>Month</th>
                                                                <th>Medical</th>
                                                                <th>LTA</th>
                                                                <th>Meal Voucher</th>
                                                                <th>Mobile & Telephone Reimbursement</th>
                                                                <th>Mobile Purchase</th>
                                                                <th>Created on</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr>
                                                                <td>1</td>
                                                                <td>01 - 08 - 2019 </td>
                                                                <td>15000.0</td>
                                                                <td>30000.0</td>
                                                                <td>.0</td>
                                                                <td>.0</td>
                                                                <td>.0</td>
                                                                <td>05 - 08 - 2019</td>
                                                            </tr>
                                                        </tbody>
                                                    </table>--%>
                                        <asp:GridView ID="grdFbpDeclaration" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="false" Width="100%" CellPadding="4"
                                            CellSpacing="1"
                                            OnRowDataBound="grdFbpDeclaration_RowDataBound" DataKeyNames="AA_AMT01,AA_AMT02,AA_AMT03,AA_AMT04,AA_AMT05,AA_AMT06,AA_AMT07,AA_AMT08,AA_AMT09,AA_AMT10,AA_AMT11,AA_AMT12">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>

                                                    <ItemStyle CssClass="col-center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Month" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                                    <ItemTemplate>
                                                        <%-- <%# Eval("BEGDA") %>--%>
                                                        <%# Eval("BEGDA", "{0:dd-MM-yyyy}") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Medical" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                                    <ItemTemplate>
                                                        <%# Eval("AA_AMT01") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="LTA" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                    <ItemTemplate>
                                                        <%# Convert.ToDecimal(Eval("AA_AMT02")).ToString("#,##0.00") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Meal Voucher" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                    <ItemTemplate>
                                                        <%# Convert.ToDecimal(Eval("AA_AMT03")).ToString("#,##0.00") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Car EMI" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                    <ItemTemplate>
                                                        <%#  Convert.ToDecimal(Eval("AA_AMT04")).ToString("#,##0.00") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Fuel" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT05") %>
                        </ItemTemplate>

                    </asp:TemplateField>--%>
                                                <%--<asp:TemplateField HeaderText="Driver's Salary" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT06") %>
                        </ItemTemplate>

                    </asp:TemplateField>--%>
                                                <%--<asp:TemplateField HeaderText="Car Insurance & Maintenance" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("AA_AMT07") %>
                        </ItemTemplate>

                    </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Mobile & Telephone Reimbursement" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                    <ItemTemplate>
                                                        <%#  Convert.ToDecimal(Eval("AA_AMT08")).ToString("#,##0.00") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Car Fuel Reimbursment-Self" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                    <ItemTemplate>
                                                        <%# Convert.ToDecimal(Eval("AA_AMT09")).ToString("#,##0.00") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile Purchase" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                    <ItemTemplate>
                                                        <%# Convert.ToDecimal(Eval("AA_AMT10")).ToString("#,##0.00") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Car Related Reimbursement" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                    <ItemTemplate>
                                                        <%# Convert.ToDecimal(Eval("AA_AMT11")).ToString("#,##0.00") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Education" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                    <ItemTemplate>
                                                        <%# Convert.ToDecimal(Eval("AA_AMT12")).ToString("#,##0.00") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Created on" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                    <ItemTemplate>
                                                        <%--    <%# Eval("CREATED_ON") %>--%>
                                                        <%# Eval("CREATED_ON", "{0:dd-MM-yyyy}") %>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </div>

                                <asp:UpdatePanel ID="UPFbp" runat="server">
                                    <ContentTemplate>

                                        <div class="mb-3 hidden">
                                            <div id="Exportbtn" runat="server">
                                                <asp:Button ID="BtnExporttoXl" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" CausesValidation="false" CssClass="btn btn-dark waves-effect waves-light btn-std" TabIndex="5" />
                                                <asp:Button ID="BtnExporttoPDF" runat="server" Text="Export To PDF" OnClick="ExportToPDF_Click" TabIndex="6" CssClass="btn btn-dark waves-effect waves-light btn-std" />
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="BtnExporttoPDF" />
                                        <asp:PostBackTrigger ControlID="BtnExporttoXl" />
                                    </Triggers>
                                </asp:UpdatePanel>

                            </div>

                            <!-- end Tab Panel-->

                        </div>
                        <!-- end row -->

                    </asp:View>


                    <!-- =========== Tab-2 Tab Panel ==================-->
                    <asp:View ID="View2" runat="server">
                        <div id="Tab-2">




                            <div class="table-responsive card-box">

                                <table class="table table-sm table-borderless mb-0 table_font_sm">
                                    <tbody>
                                        <tr>
                                            <td colspan="4">
                                                <h5 class="header-title">My FBP Claims</h5>
                                                <asp:Label ID="LblDate" runat="server" Text="Jan 2016"></asp:Label>
                                            </td>
                                            <td colspan="4">
                                                <%--<a href="Other_Reimbursements.aspx">
                                                    <button type="button" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right">
                                                        <i class="mdi mdi-plus"></i>Add New iExpense Request</button></a>--%>
                                            </td>
                                        </tr>

                                        <%--                                        <tr class="border-top">
                                            <td width="60">Show</td>
                                            <td width="60">
                                                <select name="" aria-controls="" class="">
                                                    <option value="10">10</option>
                                                    <option value="25">25</option>
                                                    <option value="50">50</option>
                                                    <option value="100">100</option>
                                                </select>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td width="104">Tickets Visibility</td>
                                            <td width="250">
                                                <div class="btn-group mb-2">
                                                    <button type="button" class="btn btn-xs btn-secondary">All </button>
                                                    <button type="button" class="btn btn-xs btn-light">Current Month</button>
                                                    <button type="button" class="btn btn-xs btn-light">Last Month</button>
                                                </div>
                                            </td>
                                            <td width="50" align="right">Search:</td>
                                            <td width="300">
                                                <input type="text" class="form-control-file" placeholder="" aria-controls=""></td>
                                        </tr>--%>
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



                                <asp:GridView ID="grd_CalimsItems" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew hidden" GridLines="None" HeaderStyle-CssClass="Divh" Visible="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No." HeaderStyle-Width="10%">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="col-center" />
                                        </asp:TemplateField>
                                        <%--<asp:BoundField  HeaderText="Items" DataField="ALLOWANCETEXT" ItemStyle-Width="45%"/>--%>
                                        <asp:TemplateField HeaderText="Allowance" ItemStyle-Width="45%">
                                            <ItemTemplate>

                                                <%--<%# Eval("LGART") %> - <%# Eval("ALLOWANCETEXT") %>--%>
                                                <%# Eval("ALLOWANCETEXT") %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Annual Entitlement" DataField="ANNUAL" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"/>--%>
                                        <asp:TemplateField HeaderText="Annual Entitlement" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" HeaderStyle-Width="15%">
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("ANNUAL")).ToString("0.00") %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Accrued as on current month" DataField="ACCRUED" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                        <asp:TemplateField HeaderText="Accrued as on current month" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" HeaderStyle-Width="15%">
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("ACCRUED")).ToString("0.00") %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:BoundField HeaderText="Claims Paid" DataField="BETRG" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />

                                        <asp:BoundField HeaderText="Claims Pending" DataField="PENDINGAMT" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />

                                        <asp:BoundField HeaderText="Balance" DataField="BALANCE" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />

                                    </Columns>
                                </asp:GridView>
                                <br />
                                <%-- <p style="color: #00529b">Note: Balance = Entitlement - Claims Paid - Claims Pending</p>--%>
                                <p style="color: #00529b">Note: Balance =  Accrued - Claims Paid - Claims Pending</p>

                            </div>
                            <div class="mb-3">
                                <%--<p style="color: #00529b">Note: Balance = Entitlement - Claims Paid - Claims Pending</p>
                                <br />--%>
                                <%--Claim option has been disabled--%>
                                <asp:Button ID="btnApplyView" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" runat="server" Text="Apply/View Claim" OnClick="btnApplyView_Click" TabIndex="1" Width="180"/>
                                 <%--<asp:Button ID="btnApplyView" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" runat="server" Text="Claims has been disabled" OnClick="btnApplyView_Click" TabIndex="1" Width="200" Enabled="false"/>--%>
                            </div>



                            <asp:Button runat="server" ID="btnShowModalPopup" Style="display: none" />
                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
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
                                                <h5 class="header-title">Flexible Benefit Plan Claims History</h5>
                                            </td>
                                            <!--td colspan="4" align="right">Indentor : Shifaz K Mohammed</td-->
                                        </tr>

                                        <tr class="border-top">
                                            <td width="60">Show</td>
                                            <td width="60">
                                                <%--   <select name="" aria-controls="" class="">
                                                    <option value="10">10</option>
                                                    <option value="25">25</option>
                                                    <option value="50">50</option>
                                                    <option value="100">100</option>
                                                </select>--%>
                                                <asp:DropDownList ID="ddlPagesizeEmp" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPagesizeEmp_SelectedIndexChanged">
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
                                            <%--<td width="50" align="right">Search:</td>--%>
                                            <td width="300">
                                                <%-- <input type="text" class="form-control-file" placeholder="" aria-controls="">--%>
                                                <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control" placeholder="FBP Claim Id" AutoPostBack="True" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtsearch"></cc1:FilteredTextBoxExtender>
                                            </td>
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


                                <%-- <asp:GridView ID="grdAppRejTravelMP" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" OnRowCommand="grdAppRejTravelMP_RowCommand"
                            DataKeyNames="CID,CREATED_BY,REINR,WBS_ELEMT,ACTIVITY,RE_AMT,RCURR" AllowPaging="true" AllowSorting="true" OnSorting="grdAppRejTravelMP_Sorting" OnPageIndexChanging="grdAppRejTravelMP_PageIndexChanging" PageSize="5">
                            <Columns>
                                <asp:BoundField DataField="CID" HeaderText="Claim Id" />

                                <asp:BoundField DataField="REINR" HeaderText="Trip No" />
                                <asp:BoundField DataField="DATV1" HeaderText="Trip From" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="7%" />
                                <asp:BoundField DataField="DATB1" HeaderText="Trip To" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="7%" />

                                <asp:BoundField DataField="CREATED_BY" HeaderText="Employee ID" />
                                <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />

                                <asp:BoundField DataField="WBS_ELEMT" HeaderText="Project" />

                                <asp:BoundField DataField="ACTIVITY" HeaderText="Task" />


                                <asp:TemplateField HeaderText="Total Reimbursement Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>

                                        <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:BoundField DataField="RCURR" HeaderText="Reimbursement Currency" />

                                <asp:BoundField DataField="CREATED_ON" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" />

                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved" ? "btn btn-xs btn-blue waves-effect waves-light" :Eval("Status").ToString() == "Saved" ?"btn btn-xs btn-warning waves-effect waves-light btn-block": "btn btn-xs btn-success waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved" ? "Closed" : Eval("Status").ToString() == "Saved" ? "Saved" :" Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <i class="dripicons-article" data-toggle="tooltip" title="View Details"></i>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnIExpenseView" runat="server" CssClass="dripicons-article" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                            <SelectedRowStyle BackColor="Silver" />
                        </asp:GridView>--%>

                                <asp:GridView ID="grd_FbpClaims_History" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" Width="99%"
                                    OnRowCommand="grd_FbpClaims_History_RowCommand" DataKeyNames="FBPC_IC,LGART,STATUS,BETRG,ALLOWANCETEXT,OVERRIDE_AMT,CREATED_ON,APPROVEDON"
                                    AllowPaging="true" AllowSorting="true" PageSize="15" OnSorting="grd_FbpClaims_History_Sorting" OnPageIndexChanging="grd_FbpClaims_History_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl. No." ControlStyle-CssClass="col-center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>

                                            <ItemStyle CssClass="col-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FBP Claim Id" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                            <ItemTemplate>
                                                <%# Eval("FBPC_IC") %>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Allowance" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                            <ItemTemplate>
                                                <%-- <%# Eval("LGART") %> - <%# Eval("ALLOWANCETEXT") %>--%>
                                                <%# Eval("ALLOWANCETEXT") %>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Date" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" SortExpression="BEGDA">
                        <ItemTemplate>
                            <%--    <%# Eval("BEGDA") %>
                            <%# Eval("BEGDA", "{0:dd-MM-yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Claimed Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("BETRG")).ToString("#,##0.00") %>
                                                <%--   <%# Eval("BETRG") %>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Override Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" Visible="false">
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("OVERRIDE_AMT")).ToString("#,##0.00") %>
                                                <%--   <%# Eval("OVERRIDE_AMT") %>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("APPAMT")).ToString("#,##0.00") %>
                                                <%--  <%# Eval("APPAMT") %>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Submitted On" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                            <ItemTemplate>
                                                <%--    <%# Eval("CREATED_ON") %>--%>
                                                <%# Eval("CREATED_ON", "{0:dd-MM-yyyy}") %>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                            <ItemTemplate>

                                                <%# Eval("REMARKS") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <%--<%# Eval("STATUS") %>--%>
                                                <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%#Eval("STATUS").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected" ?"btn btn-xs btn-danger waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%#  Eval("STATUS").ToString() %>' Enabled="false" ForeColor="White"></asp:LinkButton>
                                            </ItemTemplate>

 

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved on" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                            <%--SortExpression="APPROVEDON"--%>
                                            <ItemTemplate>
                                                <%--    <%# Eval("APPROVED_ON") %>--%>
                                                <%#(Eval("APPROVEDON","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVEDON","{0:dd-MM-yyyy}")%>
                                                <%--   <%# Eval("APPROVEDON", "{0:dd-MM-yyyy}") %>--%>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnFbpclaimsView" runat="server" CausesValidation="False" CommandName="VIEW" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                    <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                    <SelectedRowStyle BackColor="Silver" />
                                </asp:GridView>

                                <asp:Button runat="server" ID="btnShowModalPopup3" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server"
                                    TargetControlID="btnShowModalPopup3"
                                    PopupControlID="dvAppHistory"
                                    BackgroundCssClass="popUpStyle"
                                    DropShadow="false" />
                                <!-- end table-responsive-->

                            </div>
                        </div>

                    </asp:View>


                    <!-- =========== Tab-4 Tab Panel ==================-->
                    <asp:View ID="View4" runat="server">

                        <div id="Tab-4">
                            <div class="table-responsive card-box">

                                <table class="table table-sm table-borderless mb-0 table_font_sm">
                                    <tbody>
                                        <tr>
                                            <td colspan="5">
                                                <h5 class="header-title">Below FBP Requisitions are completed your level of action</h5>
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
                                                 
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td width="104">Tickets Visibility</td>
                                            <td width="250">
                                                <div class="btn-group mb-2">
                                                    <%--<button type="button" class="btn btn-xs btn-secondary">All </button>
                                                    <button type="button" class="btn btn-xs btn-light">Current Month</button>
                                                    <button type="button" class="btn btn-xs btn-light">Last Month</button>--%>
                                                </div>
                                            </td>
                                            <td width="50" align="right">Search:</td>
                                            <td width="300">
                                                <input type="text" class="form-control-file" placeholder="" aria-controls=""></td>
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

                                <%-- <asp:GridView ID="grdAppRejTravelMC" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" OnRowCommand="grdAppRejTravelMC_RowCommand"
                            DataKeyNames="CID,CREATED_BY,REINR,WBS_ELEMT,ACTIVITY,RE_AMT,RCURR" AllowPaging="true" AllowSorting="true" OnSorting="grdAppRejTravelMC_Sorting" OnPageIndexChanging="grdAppRejTravelMC_PageIndexChanging" PageSize="5">
                            <Columns>
                                <asp:BoundField DataField="CID" HeaderText="Claim Id" />

                                <asp:BoundField DataField="REINR" HeaderText="Trip No" />
                                <asp:BoundField DataField="DATV1" HeaderText="Trip From" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="7%" />
                                <asp:BoundField DataField="DATB1" HeaderText="Trip To" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="7%" />

                                <asp:BoundField DataField="CREATED_BY" HeaderText="Employee ID" />
                                <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />

                                <asp:BoundField DataField="WBS_ELEMT" HeaderText="Project" />

                                <asp:BoundField DataField="ACTIVITY" HeaderText="Task" />


                                <asp:TemplateField HeaderText="Total Reimbursement Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                                    <EditItemTemplate>
                                    </EditItemTemplate>
                                    <ItemTemplate>

                                        <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="RCURR" HeaderText="Reimbursement Currency" />

                                <asp:BoundField DataField="CREATED_ON" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" />
                                <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%# Eval("Status").ToString() == "Approved" ? "btn btn-xs btn-blue waves-effect waves-light" :Eval("Status").ToString() == "Saved" ?"btn btn-xs btn-warning waves-effect waves-light btn-block": "btn btn-xs btn-success waves-effect waves-light" %>' CausesValidation="False" Text='<%# Eval("Status").ToString() == "Approved" ? "Closed" : Eval("Status").ToString() == "Saved" ? "Saved" :" Open "%>' CommandName="Status" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <i class="dripicons-article" data-toggle="tooltip" title="View Details"></i>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnIExpenseView" runat="server" CssClass="dripicons-article" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                            <SelectedRowStyle BackColor="Silver" />
                        </asp:GridView>--%>
                                <asp:Button runat="server" ID="btnShowModalPopup4" Style="display: none" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server"
                                    TargetControlID="btnShowModalPopup4"
                                    PopupControlID="dvAppHistory"
                                    BackgroundCssClass="popUpStyle"
                                    DropShadow="false" />
                                <!-- end table-responsive-->

                            </div>
                        </div>

                    </asp:View>
                </asp:MultiView>
            </div>
        </div>
        <!-- end Tab Panel-->

    </div>
    <!-- end row -->


    <!--  Modal content for the above example -->
    <div id="dvAppHistory" runat="server" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" style="display: none;" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myLargeModalLabel">iExpense Approval History </h4>
                    <%--  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>--%>
                    <asp:Button ID="btnClose" runat="server" Text="x" CssClass="close" data-dismiss="modal" aria-hidden="true" CausesValidation="false" />
                </div>
                <div class="modal-body">
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
                                            <th>Approved</th>
                                        </tr>
                                        <asp:Panel ID="pnlAPPROVEDBY1" runat="server" Visible='<%# (Eval("APPROVED_BY1")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY1") %> </td>
                                                <td><%#(Eval("APPROVED_BY1").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY1N")%></td>

                                                <%--   <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": ""%>--%>
                                                <%-- <td class="Tbltd"><%#(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%></td>--%>

                                                <td><%#(Eval("APPROVED_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON1","{0:dd-MM-yyyy}")%></td>

                                                <td><%# Eval("REMARKS1") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":(Eval("STATUS").ToString()=="HOLD1")?"Hold":(Eval("STATUS").ToString()=="RELEASED1")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span></td>
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
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Approved2")?"Approved":(Eval("STATUS").ToString()=="HOLD2")?"Hold":(Eval("STATUS").ToString()=="RELEASED2")?"Released":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel1" runat="server" Visible='<%# (Eval("APPROVED_BY3")).ToString()==""?false:true %>'>

                                            <tr>
                                                <td><%# Eval("APPROVED_BY3") %></td>
                                                <td><%# (Eval("APPROVED_BY3").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY3N") %></td>

                                                <%-- <td class="Tbltd"><%#(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%></td>--%>
                                                <td><%#(Eval("APPROVED_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON3","{0:dd-MM-yyyy}")%></td>


                                                <td><%# Eval("REMARKS3") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected3")?"Rejected": (Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved3")?"Approved":(Eval("STATUS").ToString()=="HOLD3")?"Hold":(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED3")?"Released":(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel2" runat="server" Visible='<%# (Eval("APPROVED_BY4")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY4") %></td>
                                                <td><%# (Eval("APPROVED_BY4").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY4N") %></td>

                                                <%-- <td class="Tbltd"><%#(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%></td>--%>
                                                <td><%#(Eval("APPROVED_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON4","{0:dd-MM-yyyy}")%></td>

                                                <td><%# Eval("REMARKS4") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected4")?"Rejected": (Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved4")?"Approved":(Eval("STATUS").ToString()=="HOLD4")?"Hold":(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED4")?"Released":(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel3" runat="server" Visible='<%# (Eval("APPROVED_BY5")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY5") %></td>
                                                <td><%# (Eval("APPROVED_BY5").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY5N") %></td>

                                                <%--<td class="Tbltd"><%#(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%></td>--%>
                                                <td><%#(Eval("APPROVED_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON5","{0:dd-MM-yyyy}")%></td>

                                                <td><%# Eval("REMARKS5") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected5")?"Rejected": (Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved5")?"Approved":(Eval("STATUS").ToString()=="HOLD5")?"Hold":(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED5")?"Released":(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel4" runat="server" Visible='<%# (Eval("APPROVED_BY6")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY6") %></td>
                                                <td><%# (Eval("APPROVED_BY6").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY6N") %></td>

                                                <%--  <td class="Tbltd"><%#(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%></td>--%>
                                                <td><%#(Eval("APPROVED_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON6","{0:dd-MM-yyyy}")%></td>

                                                <td><%# Eval("REMARKS6") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":(Eval("STATUS").ToString()=="HOLD6")?"Hold":(Eval("STATUS").ToString()=="HOLD5")||(Eval("STATUS").ToString()=="HOLD4")||(Eval("STATUS").ToString()=="HOLD3")||(Eval("STATUS").ToString()=="HOLD2")?"":(Eval("STATUS").ToString()=="RELEASED6")?"Released":(Eval("STATUS").ToString()=="RELEASED5")||(Eval("STATUS").ToString()=="RELEASED4")||(Eval("STATUS").ToString()=="RELEASED3")||(Eval("STATUS").ToString()=="RELEASED2")||(Eval("STATUS").ToString()=="RELEASED1")?"":(Eval("STATUS").ToString()=="Cancelled")?"Cancelled":(Eval("STATUS").ToString()=="Saved")?"Saved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel5" runat="server" Visible='<%# (Eval("APPROVED_BY7")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY7") %></td>
                                                <td><%# (Eval("APPROVED_BY7").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY7N") %></td>

                                                <td><%#(Eval("APPROVED_ON7","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON7","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td><%# Eval("REMARKS7") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"||Eval("STATUS").ToString()=="Approved6"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel6" runat="server" Visible='<%# (Eval("APPROVED_BY8")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY8") %></td>
                                                <td><%# (Eval("APPROVED_BY8").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY8") %></td>

                                                <td><%#(Eval("APPROVED_ON8","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON8","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td><%# Eval("REMARKS8") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"||Eval("STATUS").ToString()=="Approved6"||Eval("STATUS").ToString()=="Approved7"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%></span> </td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel7" runat="server" Visible='<%# (Eval("APPROVED_BY9")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%# Eval("APPROVED_BY9") %></td>
                                                <td><%# (Eval("APPROVED_BY9").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY9") %></td>

                                                <td><%#(Eval("APPROVED_ON9","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON9","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td><%# Eval("REMARKS9") %></td>
                                                <td><span class='<%# Eval("STATUS").ToString()=="Requested"||Eval("STATUS").ToString()=="Approved1"||Eval("STATUS").ToString()=="Approved2"||Eval("STATUS").ToString()=="Approved3"||Eval("STATUS").ToString()=="Approved4"||Eval("STATUS").ToString()=="Approved5"||Eval("STATUS").ToString()=="Approved6"||Eval("STATUS").ToString()=="Approved7"||Eval("STATUS").ToString()=="Approved8"?"badge badge-warning":"badge badge-success" %>'><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%></span> </td>
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
                <div class="modal-footer">
                    <%--<button type="button" class="btn btn-light waves-effect" data-dismiss="modal">Close</button>--%>
                    <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btn btn-light waves-effect" data-dismiss="modal" CausesValidation="false" />
                </div>
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
    <!-- container -->


    <script src="http://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function calculateGrandTotal() {
            $("[id*=txtAnnualAllocation], input[type=text]").on("keyup keydown change", function () {
                var TotalWorkHrs = 0;
                $("[id*=txtAnnualAllocation]").each(function () {
                    if (!jQuery.trim($(this).val()) == '') {
                        if (!isNaN(parseFloat($(this).val() == '' ? 0.00 : $(this).val()))) {
                            //alert($(this).val() == '' ? 0 : $(this).val());
                            TotalWorkHrs = TotalWorkHrs + parseFloat($(this).val() == '' ? 0.00 : $(this).val());

                            var row = $(this).closest("tr");
                            $("[id*=lblMonthlyAllocation]", row).html((parseFloat($(this).val()) / 12).toFixed(2).toString());
                        }
                    }
                });
                $("[id*=lblAnnualAllocationTotal]").html(TotalWorkHrs.toFixed(2).toString());
                $("[id*=HFAllocationTotal]").val(TotalWorkHrs.toFixed(2).toString());


                var sallow = 0;
                sallow = parseFloat($("[id*=lblBasketTotalAmount]").html().replace(',', '')) - parseFloat($("[id*=lblAnnualAllocationTotal]").html());
                $("[id*=lblFBPSpecialAllowance]").html(sallow.toFixed(2).toString());
            });
        }

        ////$(document).ready(function () {
        ////    var parameter = Sys.WebForms.PageRequestManager.getInstance();
        ////    parameter.add_endRequest(function () {
        ////        var TotalWorkHrs = 0;
        ////        $("[id*=txtAnnualAllocation]").each(function () {
        ////            if (!jQuery.trim($(this).val()) == '') {
        ////                if (!isNaN(parseFloat($(this).val() == '' ? 0.00 : $(this).val()))) {
        ////                    //alert($(this).val() == '' ? 0 : $(this).val());
        ////                    TotalWorkHrs = TotalWorkHrs + parseFloat($(this).val() == '' ? 0.00 : $(this).val());



        ////                    var row = $(this).closest("tr");
        ////                    $("[id*=lblMonthlyAllocation]", row).html((parseFloat($(this).val()) / 12).toFixed(2).toString());
        ////                }
        ////            }
        ////        });
        ////        $("[id*=lblAnnualAllocationTotal]").html(TotalWorkHrs.toFixed(2).toString());
        ////        $("[id*=HFAllocationTotal]").val(TotalWorkHrs.toFixed(2).toString());



        ////        var sallow = 0;
        ////        sallow = parseFloat($("[id*=lblBasketTotalAmount]").html().replace(',', '')) - parseFloat($("[id*=lblAnnualAllocationTotal]").html());
        ////        $("[id*=lblFBPSpecialAllowance]").html(sallow.toFixed(2).toString());
        ////    });
        ////});



        function validate(Msg) {
            if (Page_ClientValidate())
                return confirm('Do you want to ' + Msg + ' this FBP declaration ?');
        }



    </script>
    <asp:PlaceHolder ID="PlaceScript" runat="server" Visible="false">
        <script>
            $(document).ready(function () {
                var parameter = Sys.WebForms.PageRequestManager.getInstance();
                parameter.add_endRequest(function () {
                    var TotalWorkHrs = 0;
                    $("[id*=txtAnnualAllocation]").each(function () {
                        if (!jQuery.trim($(this).val()) == '') {
                            if (!isNaN(parseFloat($(this).val() == '' ? 0.00 : $(this).val()))) {
                                //alert($(this).val() == '' ? 0 : $(this).val());
                                TotalWorkHrs = TotalWorkHrs + parseFloat($(this).val() == '' ? 0.00 : $(this).val());





                                var row = $(this).closest("tr");
                                $("[id*=lblMonthlyAllocation]", row).html((parseFloat($(this).val()) / 12).toFixed(2).toString());
                            }
                        }
                    });
                    $("[id*=lblAnnualAllocationTotal]").html(TotalWorkHrs.toFixed(2).toString());
                    $("[id*=HFAllocationTotal]").val(TotalWorkHrs.toFixed(2).toString());





                    var sallow = 0;
                    sallow = parseFloat($("[id*=lblBasketTotalAmount]").html().replace(',', '')) - parseFloat($("[id*=lblAnnualAllocationTotal]").html());
                    $("[id*=lblFBPSpecialAllowance]").html(sallow.toFixed(2).toString());
                });
            });

        </script>
    </asp:PlaceHolder>

</asp:Content>
