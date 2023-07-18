<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="FBP_Apply_ViewClaimsNew.aspx.cs"
    Inherits="iEmpPower.UI.FBP.FBP_Apply_ViewClaimsNew" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        .Divh
        {
            background-color: #3470A7;
            color: #FFFFFF;
        }
    </style>
</asp:Content>--%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .hidden {
            display: none;
        }

        .linetext {
            margin-left: 2px;
            margin-right: 2px;
            width: 50px;
            background: transparent !important;
            border: none !important;
            border-bottom: 1px solid #000000 !important;
        }
    </style>

    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item"><a href="FBP.aspx">FBP</a></li>
                        <li class="breadcrumb-item active">Reimbursement List</li>
                    </ol>
                </div>
                <h4 class="page-title">Reimbursement List</h4>
            </div>
        </div>
    </div>
    <!-- end page title -->

    <div class="row">
        <div class="table-responsive card-box">
            <h5 class="header-title">Reimbursement List</h5>
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <%--div class="DivSpacer01"></div>--%>
            <!-- Tab Panel Start / -->

            <div class="col-xl-12 m-t-20">
                <div class="tab-content m-0 p-0">
                    <asp:MultiView ID="MV_Fbp" runat="server">
                        <asp:View ID="View1" runat="server">

                            <div class="row  text-right">
                                <div class="col-sm-12 text-right">
                                    <asp:LinkButton ID="lbtn_View1Back" runat="server" Text="Back" OnClick="lbtn_View1Back_Click" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right"></asp:LinkButton>
                                    <asp:Button ID="btnNewFbpclaims" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right" runat="server" Text="New Claims" OnClick="btnNewFbpclaims_Click" TabIndex="5" />

                                </div>
                            </div>

                            <div class="row border-top ">
                                <div class="col-sm-2  margin5rem">
                                    <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="form-control" TabIndex="1">
                                        <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="FBP Claim ID" Value="1"></asp:ListItem>
                                        <%--<asp:ListItem Text="Allowance ID" Value="2"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2 margin5rem">
                                    <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control" placeholder="Enter Text" TabIndex="2"></asp:TextBox>
                                </div>
                                <div class="col-sm-4 margin5rem">
                                    <asp:Button ID="btnsearch" runat="server" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="3" Text="Search" CssClass="btn btn-xs btn-secondary" />

                                    <asp:Button ID="btnclear" CssClass="btn btn-xs btn-secondary" runat="server" OnClick="btnclear_Click" TabIndex="4" Text="Clear" />
                                    <%--<fieldset style="float: left">
                                    <legend><b><h4>Search for saved claims</h4></b></legend>
                                    <table>

                                        <tr>

                                            <td>
                                                <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="textbox" TabIndex="1">
                                                    <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="FBP Claim ID" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Allowance ID" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>


                                            <td>
                                                <asp:TextBox ID="txtsearch" runat="server" CssClass="textbox" placeholder="Enter Text" TabIndex="2"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnsearch" runat="server" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="3" Text="Search" />
                                                &nbsp;&nbsp;
                            <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" TabIndex="4" Text="Clear" />
                                            </td>



                                        </tr>
                                    </table>
                                </fieldset>--%>
                                </div>

                            </div>

                            <%--<fieldset style="float: left">
                                    <legend><b><h4>Search for saved claims</h4></b></legend>
                                    <table>

                                        <tr>

                                            <td>
                                                <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="textbox" TabIndex="1">
                                                    <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="FBP Claim ID" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Allowance ID" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>


                                            <td>
                                                <asp:TextBox ID="txtsearch" runat="server" CssClass="textbox" placeholder="Enter Text" TabIndex="2"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnsearch" runat="server" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="3" Text="Search" />
                                                &nbsp;&nbsp;
                            <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" TabIndex="4" Text="Clear" />
                                            </td>



                                        </tr>
                                    </table>
                                </fieldset>--%>


                            <%-- <div style="width: 49%; float: right">

                                <fieldset style="width: 27%; float: left">
                                    <legend><b>&nbsp;New FBP Cliams&nbsp;</b></legend>
                                    <table>

                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:Button ID="btnNewFbpclaims" runat="server" Text="New Claims" OnClick="btnNewFbpclaims_Click" TabIndex="5" />
                                            </td>

                                        </tr>
                                    </table>
                                </fieldset>






                            </div>--%>
                            <%-- <div class="DivSpacer01"></div>

                            <div style="width: 68%; text-align: right;">--%>

                            <%--</div>--%>
                            <div class="DivSpacer01"></div>
                            <asp:GridView ID="grd_SavedFbpClaims" runat="server" AutoGenerateColumns="false"  CssClass="gridviewNew" GridLines="None"
                                OnRowCommand="grd_SavedFbpClaims_RowCommand" DataKeyNames="FBPC_IC,LGART" TabIndex="2"
                                AllowPaging="true" AllowSorting="true" PageSize="10" OnSorting="grdSavedFBPclaims_Sorting" OnPageIndexChanging="grdSavedFBPclaims_PageIndexChanging" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
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
                                    <asp:TemplateField HeaderText="Allowance">
                                        <ItemTemplate>
                                            <%--   <%# Eval("LGART") %> - <%# Eval("ALLOWANCETEXT") %>--%>
                                            <%# Eval("ALLOWANCETEXT") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Reimbursement" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" SortExpression="LGART">
                        <ItemTemplate>
                            <%# Eval("LGART") %>
                        </ItemTemplate>

                    </asp:TemplateField>--%>
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
                                    <asp:TemplateField HeaderText="Created on" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="col-center" >
                                        <ItemTemplate>
                                            <%--    <%# Eval("CREATED_ON") %>--%>
                                            <%# Eval("CREATED_ON", "{0:dd-MM-yyyy}") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="col-center" >
                                        <ItemTemplate>
                                            <%# Eval("STATUS") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <i class="fe-edit-1" title="Edit Details"></i>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnFbpclaimsView" runat="server" CausesValidation="False" CommandName="EDITFBP" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="fe-edit-1"></asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <br />
                        </asp:View>


                        <asp:View ID="View2" runat="server">

                            <table id="tblreimbursement" runat="server" visible="false" style="width: 100%">

                                <tr>

                                    <td style="width: 20%">
                                        <asp:DropDownList ID="ddlPlan" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPlan_SelectedIndexChanged" AutoPostBack="true" TabIndex="6">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFVddlPlan" runat="server" ErrorMessage="Please Select Reimbursements" ForeColor="Red" ControlToValidate="ddlPlan" InitialValue="0"></asp:RequiredFieldValidator>

                                    </td>

                                    <td class="text-right">
                                        <asp:LinkButton ID="btnback" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right" runat="server" Text="Back" OnClick="btnBack_Click" Visible="false" Font-Size="14px"></asp:LinkButton></td>



                                </tr>
                            </table>
                            <br />
                            <%--   <table id="Table1" class="tablebody" border="0" cellpadding="1" cellspacing="1" runat="server" style="float: right; width: 34%">

                                <tr>
                                    <td>&nbsp;   &nbsp;
                <asp:LinkButton ID="btnback" runat="server" Text="Back" OnClick="btnBack_Click" Visible="false" Font-Size="14px"></asp:LinkButton>
                                    </td>


                                </tr>
                            </table>--%>

                            <div class="DivSpacer01"></div>

                            <div id="DivClaims" runat="server" visible="false">
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                <asp:GridView ID="grd_CalimsItems" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" HeaderStyle-CssClass="Divh">
                                    <Columns>


                                        <%-- <asp:BoundField HeaderText="Items" DataField="ALLOWANCETEXT" ItemStyle-Width="45%" />--%>
                                        <asp:TemplateField HeaderText="Allowance" ItemStyle-Width="60%">
                                            <ItemTemplate>
                                                <%-- <%# Eval("LGART") %> ---%> <%# Eval("ALLOWANCETEXT") %>
                                                <%-- <%# Eval("ALLOWANCETEXT") %>--%>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <%-- <asp:BoundField HeaderText="Entitlement" DataField="ANNUAL" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"> /--%>
                                        <asp:TemplateField HeaderText="Annual Entitlement" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" HeaderStyle-Width="15%">
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("ANNUAL")).ToString("#,##0.00") %>
                                                <%--   <%# Eval("ANNUAL") %>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Accrued as on current month" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" HeaderStyle-Width="20%">
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("ACCRUED")).ToString("#,##0.00") %>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Claims Paid" DataField="BETRG" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                        <asp:TemplateField HeaderText="Claims Paid" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("BETRG")).ToString("#,##0.00") %>
                                                <%--   <%# Eval("BETRG") %>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%--<asp:BoundField HeaderText="Claims Pending" DataField="PENDINGAMT" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                        <asp:TemplateField HeaderText="Claims Pending" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("PENDINGAMT")).ToString("#,##0.00") %>
                                                <%--   <%# Eval("PENDINGAMT") %>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <%-- <asp:BoundField HeaderText="Balance" DataField="BALANCE" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                        <asp:TemplateField HeaderText="Balance" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(Eval("BALANCE")).ToString("#,##0.00") %>
                                                <%--   <%# Eval("BALANCE") %>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>


                                </asp:GridView>

                                <br />

                                <%--  -----------------------%>
                                <div id="divLTA" runat="server" style="border: 1px solid; padding: 3px">





                                    <h5 class="header-title text-center margin5rem">LTA Form</h5>


                                    <h5 class="header-title margin5rem">Relations</h5>
                                    <asp:GridView ID="grdLTARelps" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle" OnRowCommand="grdLTARelps_RowCommand" DataKeyNames="ID,FAMTX,FCNAM,FGBDT,FASEX,DEPDT">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FAMTX_text" HeaderText="Relationship Type" />
                                            <asp:BoundField DataField="FAMTX" HeaderText="Relationship Type" Visible="false" />
                                            <asp:BoundField DataField="FCNAM" HeaderText="Name" />
                                            <asp:BoundField DataField="FGBDT" HeaderText="Date of Birth" DataFormatString="{0:dd/MM/yyyy}" />
                                            <%--<asp:BoundField DataField="FASEX" HeaderText="Gender" />--%>
                                            <asp:TemplateField HeaderText="Gender">
                                                <ItemTemplate>
                                                    <%#Eval("FASEX").ToString() == "1" ? "Male" : "Female" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dependent?">
                                                <ItemTemplate>
                                                    <%#Eval("DEPDT").ToString() == "n" ? "No" : "Yes" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="DEPDT" HeaderText="Dependent?" DataFormatString="{0:dd/MM/yyyy}" />--%>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="false" title="Edit Details" CssClass="fe-edit-1"
                                                        CommandName="EDITROW" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                    &nbsp; &nbsp;
                                                    <asp:LinkButton ID="LbtnDeleteFile" runat="server" CausesValidation="false" title="Delete" CssClass="fe-trash-2"
                                                        CommandName="DELETEROW" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        </Columns>
                                    </asp:GridView>
                                    <table class="gridviewNew  table-responsive" id="tblLTAReL" runat="server">
                                        <tr>
                                            <th>Relationship Type</th>
                                            <th>Name
                                            </th>
                                            <th>Date of Birth
                                            </th>
                                            <th>Gender
                                            </th>
                                            <th>Dependent?
                                            </th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlLTArelTypes" runat="server" CssClass="form-control" RepeatDirection="Horizontal"></asp:DropDownList>
                                                <%--  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtLTARelType" runat="server" ValidChars="." FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters" TargetControlID="txtLTARelType"></cc1:FilteredTextBoxExtender>--%>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLTARelName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTARelName" runat="server" ControlToValidate="txtLTARelName" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTARel"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtLTARelName" runat="server" ValidChars="." FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters" TargetControlID="txtLTARelName"></cc1:FilteredTextBoxExtender>
                                            </td>

                                            <td>
                                                <asp:TextBox ID="txtLTARelDob" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTARelDob" runat="server" ControlToValidate="txtLTARelDob" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTARel"></asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="MEE_txtLTARelDob" runat="server" AcceptNegative="Left"
                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtLTARelDob" />
                                                <cc1:CalendarExtender ID="CE_txtLTARelDob" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtLTARelDob">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlLTARelGender" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="1" Selected="True">Male</asp:ListItem>
                                                    <asp:ListItem Value="2">Female</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td>
                                                <asp:CheckBox ID="chkLTARelDepend" CssClass="form-control-file" runat="server" Text=" " />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbtnLTAAddRel" runat="server" CssClass="btn-xs  btn-warning" OnClick="lbtnLTAAddRel_Click" ValidationGroup="LTARel"><i class="fe-plus" ></i></asp:LinkButton>
                                                <asp:LinkButton ID="LBTNLTAUPDATEREL" runat="server" CssClass="btn-xs  btn-warning" OnClick="LBTNLTAUPDATEREL_Click" ValidationGroup="LTARel" Visible="false"><i class="fe-check" ></i></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnLTACNCLRel" runat="server" CssClass="btn-xs  btn-warning" OnClick="lbtnLTACNCLRel_Click" ValidationGroup="LTARel" Visible="false" CausesValidation="false"><i class="fe-delete"></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>

                                    <h5 class="header-title margin5rem">Travel Details</h5>
                                    <table>
                                        <tr>

                                            <td style="width: 20%">Journey Start Date from Destination :</td>
                                            <td class="">
                                                <asp:TextBox ID="txtLTAHeadJStartdt" runat="server" CssClass="form-control" Enabled="false" MaxLength="10"></asp:TextBox></td>


                                            <td class="">Journey End Date to Destination :
                                            </td>
                                            <td class="">
                                                <asp:TextBox ID="txtLTAHeadJEnddt" runat="server" CssClass="form-control" Enabled="false" MaxLength="10"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="">Mode of Travel :</td>
                                            <td class="">
                                                <asp:TextBox ID="txtLTAHeadModeofTrvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLTAHeadModeofTrvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="">Class of Travel :
                                            </td>
                                            <td class="">
                                                <asp:TextBox ID="txtLTAHeadClasTrvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAHeadClasTrvl" runat="server" ControlToValidate="txtLTAHeadClasTrvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="">Place of Departure :</td>
                                            <td class="">
                                                <asp:TextBox ID="txtLTAHeadPD" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAHeadPD" runat="server" ControlToValidate="txtLTAHeadPD" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>
                                            </td>
                                            <td class="">Place Arrival :
                                            </td>
                                            <td class="">
                                                <asp:TextBox ID="txtLTAHEADPA" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAHEADPA" runat="server" ControlToValidate="txtLTAHEADPA" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="">Are you part of entire Joureny?</td>
                                            <td>
                                                <asp:RadioButtonList ID="rbtnLTAPartofJ" runat="server" CssClass="form-control-file" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>


                                    </table>
                                    <asp:GridView ID="grdLTATrvlDetails" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle" OnRowCommand="grdLTATrvlDetails_RowCommand" DataKeyNames="ID,MTRVL,CTRVL,JBGDT,STPNT,JENDT,DESTN,KM_TRVLD,AMOUNTLTA,TKTNO">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MTRVL" HeaderText="Mode of Travel" />
                                            <asp:BoundField DataField="CTRVL" HeaderText="Class of Travel" />
                                            <asp:BoundField DataField="JBGDT" HeaderText="Date of  Departure" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="STPNT" HeaderText="Place of Departure" />
                                            <asp:BoundField DataField="JENDT" HeaderText="Date of Arriaval" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="DESTN" HeaderText="Place Arrival" />
                                            <asp:BoundField DataField="KM_TRVLD" HeaderText="KM Travelled" />
                                            <asp:BoundField DataField="AMOUNTLTA" HeaderText="Amount As per ticket" />
                                            <asp:BoundField DataField="TKTNO" HeaderText="Ticket Number" />

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="false" title="Edit Details" CssClass="fe-edit-1"
                                                        CommandName="EDITROW" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                    &nbsp; &nbsp;
                                                    <asp:LinkButton ID="LbtnDeleteFile" runat="server" CausesValidation="false" title="Delete" CssClass="fe-trash-2"
                                                        CommandName="DELETEROW" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ID" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        </Columns>
                                    </asp:GridView>
                                    <table class="gridviewNew margin5rem table-responsive" id="tblLTATRvl" runat="server">
                                        <tr>
                                            <th>Mode of Travel
                                            </th>
                                            <th>Class of Travel
                                            </th>
                                            <th>Date of  Departure</th>

                                            <th>Place of Departure
                                            </th>
                                            <th>Date of Arriaval
                                            </th>
                                            <th>Place Arrival
                                            </th>
                                            <th>KM Travelled
                                            </th>
                                            <th>Amount As per ticket
                                            </th>
                                            <th>Ticket Number
                                            </th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtLTAModeTrvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAModeTrvl" runat="server" ControlToValidate="txtLTAModeTrvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FTBE_txtLTAModeTrvl" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters" TargetControlID="txtLTAModeTrvl"></cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLTAClsTrvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAClsTrvl" runat="server" ControlToValidate="txtLTAClsTrvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtLTAClsTrvl" runat="server" ValidChars=" " FilterMode="ValidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txtLTAClsTrvl"></cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLTADtofDep" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTADtofDep" runat="server" ControlToValidate="txtLTADtofDep" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="MEE_txtLTADtofDep" runat="server" AcceptNegative="Left"
                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtLTADtofDep" />
                                                <cc1:CalendarExtender ID="CE_txtLTADtofDep" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtLTADtofDep">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLTAPlaceDep" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAPlaceDep" runat="server" ControlToValidate="txtLTAPlaceDep" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLTADtArvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTADtArvl" runat="server" ControlToValidate="txtLTADtArvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="MEE_txtLTADtArvl" runat="server" AcceptNegative="Left"
                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtLTADtArvl" />
                                                <cc1:CalendarExtender ID="CE_txtLTADtArvl" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtLTADtArvl">
                                                </cc1:CalendarExtender>
                                                <asp:CompareValidator ID="cvtxtStartDate" runat="server"
                                                    ControlToCompare="txtLTADtofDep" CultureInvariantValues="true"
                                                    Display="Dynamic" EnableClientScript="true"
                                                    ControlToValidate="txtLTADtArvl"
                                                    ErrorMessage="Invalid!"
                                                    Type="Date" SetFocusOnError="true" Operator="GreaterThanEqual"
                                                    Text="Invalid!" ForeColor="Red" ValidationGroup="LTATRVL"></asp:CompareValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLTAPlaceArvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAPlaceArvl" runat="server" ControlToValidate="txtLTAPlaceArvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLTAKMTvld" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FTBE_txtLTAKMTvld" runat="server" ValidChars="." FilterMode="ValidChars" FilterType="Numbers,Custom" TargetControlID="txtLTAKMTvld"></cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAKMTvld" runat="server" ControlToValidate="txtLTAKMTvld" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLTAAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FTBE_txtLTAAmount" runat="server" ValidChars="." FilterMode="ValidChars" FilterType="Numbers,Custom" TargetControlID="txtLTAAmount"></cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1_txtLTAAmount" runat="server" ControlToValidate="txtLTAAmount" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtLTATicketNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FTBE_txtLTATicketNo" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="LowercaseLetters,Numbers,UppercaseLetters" TargetControlID="txtLTATicketNo"></cc1:FilteredTextBoxExtender>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lbtnAddLTATrvl" runat="server" CssClass="btn-xs  btn-warning" ValidationGroup="LTATRVL" OnClick="lbtnAddLTATrvl_Click"><i class="fe-plus"></i></asp:LinkButton>
                                                <asp:LinkButton ID="LBTNADDUPDATETRVL" runat="server" ValidationGroup="LTATRVL" CssClass="btn-xs  btn-warning"  Visible="false"><i class="fe-check" ></i></asp:LinkButton>
                                                <asp:LinkButton ID="LBTNCNCLLTATRVL" runat="server" CssClass="btn-xs  btn-warning" CausesValidation="false"  ValidationGroup="LTATRVL" Visible="false"><i class="fe-x" ></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="Note" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h5 class="header-title">Declaration</h5>
                                                <ul>
                                                    <li>The block period of four years i.e, (<asp:TextBox ID="txtLTAblky1" runat="server" CssClass="linetext" MaxLength="4" OnTextChanged="txtLTAblky1_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLTAblky1" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTAblky2" CssClass="linetext" runat="server" MaxLength="4"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLTAblky2" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTAblky3" CssClass="linetext" runat="server" MaxLength="4"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLTAblky3" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTAblky4" CssClass="linetext" runat="server" MaxLength="4"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLTAblky4" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>) and
                                                    </li>
                                                    <li>that I have not availed LTA exemption till date during the above block period.
                                                    </li>
                                                    <li>that I have claimed LTA exemption once during the year
                                                    <asp:TextBox ID="txtLTACLkyear" runat="server" MaxLength="4" CssClass="linetext"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLTACLkyear" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>
                                                        falling in the block period of four year i.e, (<asp:TextBox ID="txtLTACL1" runat="server" CssClass="linetext" MaxLength="4" OnTextChanged="txtLTACL1_TextChanged" AutoPostBack="true"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLTACL1" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTACL2" runat="server" MaxLength="4" CssClass="linetext"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLTACL2" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTACL3" runat="server" MaxLength="4" CssClass="linetext"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtLTACL3" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTACL4" runat="server" MaxLength="4" CssClass="linetext"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtLTACL4" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>).</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h5 class="header-title">Note</h5>
                                                <ul>
                                                    <li>If the journey is performed by air: actual air fare incurred subject to the max, of economy class of a standard carrier.<br />
                                                        Boarding pass has to be enclosed along with original ticket.
                                                    </li>
                                                    <li>If the journey is performed by rail: actual rail fare incurred subject to the max, of First AC rail fare.
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtLTAblky1"></cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtLTAblky2"></cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtLTAblky3"></cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtLTAblky4"></cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtLTACLkyear"></cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtLTACL1"></cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtLTACL2"></cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtLTACL3"></cc1:FilteredTextBoxExtender>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="Numbers" TargetControlID="txtLTACL4"></cc1:FilteredTextBoxExtender>
                                    </div>
                                    <div class="row" id="divLTAAction" runat="server">
                                        <div class="col-sm-6">
                                            <asp:Button ID="btnLTASubmit" CssClass="btn btn-xs btn-secondary" ValidationGroup="LTA" runat="server" Text="Submit" OnClick="btnLTASubmit_Click" Visible="false" TabIndex="8" />
                                            <asp:Button ID="btnLTACancel" runat="server" CausesValidation="false" CssClass="btn btn-xs btn-secondary" Text="Cancel" OnClick="btnLTACancel_Click" Visible="false" TabIndex="9" />
                                            <asp:Button ID="btnLTAUpdate" runat="server" CausesValidation="false" CssClass="btn btn-xs btn-secondary" Text="Update" OnClick="btnLTAUpdate_Click" Visible="false" TabIndex="9" ValidationGroup="LTA" />
                                            <%--  <asp:Button ID="btnLTAXl" runat="server" CausesValidation="false" CssClass="btn btn-xs btn-secondary" Text="Export to Excel"
                                                OnClick="btnLTAXl_Click" Visible="false" TabIndex="9" OnClientClick='exportTableToExcel("ContentPlaceHolder1_MainContent_divLTA", "FBP_Claim")' />
                                            <asp:Button ID="btnLTAPDF" runat="server" CausesValidation="false" CssClass="btn btn-xs btn-secondary" Text="Export to PDF" Visible="false" TabIndex="9" OnClientClick="createPDF();" />--%>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <asp:Button ID="BtnExporttoPDF" runat="server" Text="Export To PDF" OnClientClick="createPDF();" TabIndex="7" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                                <br />

                                <%-- -----------------------%>

                                <p style="color: #00529b;" id="pNote" runat="server">Note: Fields marked by an asterisk (*) are required fields</p>
                                <asp:Label ID="lblitemsMsg" runat="server"></asp:Label>
                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" OnRowCommand="GridView1_RowCommand"
                                    OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" ShowFooter="true"
                                    DataKeyNames="BILL_NO,ID,FBPC_IC,BILL_AMT" OnRowCancelingEdit="GridView1_RowCancelingEdit" TabIndex="4" OnRowDataBound="GridView1_RowDataBound" OnDataBound="GridView1_DataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField><%-- HeaderText="Bill No.">--%>
                                            <HeaderTemplate>
                                                Bill No. <span style="color: red">*</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Eval("BILL_NO") %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBillNoe" runat="server" CssClass="form-control" Text='<%# Eval("BILL_NO") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFV_txtBillNoe" runat="server" ControlToValidate="txtBillNoe" ValidationGroup="vg2" ErrorMessage="*"
                                                    ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>

                                            </EditItemTemplate>

                                            <FooterTemplate>
                                                <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFV_txtBillNo" runat="server" ControlToValidate="txtBillNo" ValidationGroup="vg1" ErrorMessage="*"
                                                    ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>


                                            </FooterTemplate>


                                        </asp:TemplateField>
                                        <asp:TemplateField><%-- HeaderText="Bill Date">--%>
                                            <HeaderTemplate>
                                                Bill Date <span style="color: red">*</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--  <%# Eval("BILL_DATE") %>--%>
                                                <%# Eval("BILL_DATE") %>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBillDatee" runat="server" ValidationGroup="vg2"
                                                    Text='<%# Eval("BILL_DATE") %>' CssClass="form-control"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MEE_txtBillDatee" runat="server" AcceptNegative="Left"
                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtBillDatee" />
                                                <cc1:CalendarExtender ID="CE_txtBillDatee" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtBillDatee">
                                                </cc1:CalendarExtender>

                                                <asp:RequiredFieldValidator ID="RFV_txtBillDatee" runat="server" ControlToValidate="txtBillDatee" ValidationGroup="vg2" ErrorMessage="*"
                                                    ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RV_txtBillDatee" runat="server" ControlToValidate="txtBillDatee" Display="Dynamic"
                                                    ErrorMessage="" ValidationGroup="vg2"
                                                    Type="Date" ForeColor="Red" SetFocusOnError="true"></asp:RangeValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtBillDate" runat="server" ValidationGroup="vg2" CssClass="form-control"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MEE_txtBillDate" runat="server" AcceptNegative="Left"
                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtBillDate" />
                                                <cc1:CalendarExtender ID="CE_txtBillDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtBillDate">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="RFV_txtBillDate" runat="server" ControlToValidate="txtBillDate" ValidationGroup="vg1" ErrorMessage="*"
                                                    ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RV_txtBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic"
                                                    ErrorMessage="" ValidationGroup="vg1"
                                                    Type="Date" ForeColor="Red" SetFocusOnError="true"></asp:RangeValidator>
                                            </FooterTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="right" ItemStyle-CssClass="right"><%-- HeaderText="Amount">--%>
                                            <HeaderTemplate>
                                                Amount <span style="color: red">*</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                               <%-- <%# Eval("BILL_AMT") %>--%>
                                                <asp:Label ID="lblBillAmount" runat="server" Text='<%# Eval("BILL_AMT")%>' /> 
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAmounte" runat="server" CssClass="form-control text-right" Text='<%# Eval("BILL_AMT") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFV_txtAmounte" runat="server" ControlToValidate="txtAmounte" ValidationGroup="vg2" ErrorMessage="*"
                                                    ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>

                                                <cc1:FilteredTextBoxExtender ID="FTB_txtAmounte" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                    TargetControlID="txtAmounte" ValidChars=".">
                                                </cc1:FilteredTextBoxExtender>

                                                <asp:RegularExpressionValidator ID="REVtxtAmounte" runat="server" ControlToValidate="txtAmounte"
                                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                                    ValidationGroup="vg2" SetFocusOnError="true" Display="Dynamic"></asp:RegularExpressionValidator>

                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control right"></asp:TextBox>

                                                <asp:RequiredFieldValidator ID="RFV_txtAmount" runat="server" ControlToValidate="txtAmount" ValidationGroup="vg1" ErrorMessage="*"
                                                    ForeColor="Red" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>

                                                <cc1:FilteredTextBoxExtender ID="FTB_txtAmount" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                    TargetControlID="txtAmount" ValidChars=".">
                                                </cc1:FilteredTextBoxExtender>

                                                <asp:RegularExpressionValidator ID="REVtxtAmount" runat="server" ControlToValidate="txtAmount"
                                                    ErrorMessage="Must be > 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                                    ValidationGroup="vg1" SetFocusOnError="true" Display="Dynamic"></asp:RegularExpressionValidator>
                                                <asp:Label ID="lblTotalAmount" runat="server" />
                                            </FooterTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks"><%--Relationship--%>
                                            <ItemTemplate>
                                                <%# Eval("RELATIONSHIP") %>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtRelationshipe" runat="server" CssClass="form-control" Text='<%# Eval("RELATIONSHIP") %>'></asp:TextBox>
                                                <%--  <asp:RequiredFieldValidator ID="RFV_txtRelationshipe" runat="server" ControlToValidate="txtRelationshipe" ValidationGroup="vg2" ErrorMessage="Please enter Relationship" 
                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtRelationship" runat="server" CssClass="form-control"></asp:TextBox>
                                                <br />
                                                <br />
                                                <%--   <asp:RequiredFieldValidator ID="RFV_txtRelationship" runat="server" ControlToValidate="txtRelationship" ValidationGroup="vg1" ErrorMessage="Please enter Relationship" 
                    ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                            </FooterTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Mobile No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobNo" runat="server"></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlEmpMobNoe" CssClass="form-control" runat="server" Visible="true"></asp:DropDownList>
                                                <%--<asp:TextBox ID="txtEmpMobNoe" runat="server" MaxLength="13" Text='<%# Bind("BILL_AMT") %> ' Visible ="true"></asp:TextBox>--%>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddlEmpMobNo" CssClass="form-control" runat="server"></asp:DropDownList>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                       <asp:TemplateField><%-- HeaderText="Attachments">--%>
                                            <HeaderTemplate>
                                                Attachments <span style="color: red">*</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="DOWNLOAD" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="Lbtndownload" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

 

                                            </ItemTemplate>
                                            <EditItemTemplate>

 

                                                <div id="divFilee" runat="server" style="overflow: hidden">
                                                    <asp:Label ID="fuAttachmentsfnamee" runat="server" Text='<%#Eval("RECEIPT_FID") %>'></asp:Label>
                                                    <asp:LinkButton ID="LbtnDeleteFile" runat="server" CausesValidation="false" title="Delete File" Font-Bold="false" CssClass="fe-trash-2"
                                                        CommandName="DELETEFILE" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' Height="20" Width="20"
                                                        Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

 

                                                    <asp:FileUpload ID="fuAttachmentse" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="rfvFileupload" runat="server" ValidationGroup="vg2" ErrorMessage="*"
                                                        ForeColor="Red" Display="Dynamic" SetFocusOnError="true"
                                                        ControlToValidate="fuAttachmentse"></asp:RequiredFieldValidator><br />

 

                                                </div>

 


                                                <%--<asp:LinkButton ID="LbtnUploade" runat="server" Text="Upload" Visible="false"></asp:LinkButton>
                    <asp:LinkButton ID="LbtnDeletee" runat="server" Text="Delete" Visible="false"></asp:LinkButton>--%>
                                            </EditItemTemplate>

 

                                            <FooterTemplate>
                                                <div id="divFile" runat="server" style="width: 250px; overflow: hidden">
                                                    <asp:FileUpload ID="fuAttachments" runat="server" ForeColor="Black" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="rfvFileupload" runat="server" ValidationGroup="vg1" ErrorMessage="*"
                                                        ForeColor="Red" Display="Dynamic" SetFocusOnError="true"
                                                        ControlToValidate="fuAttachments"></asp:RequiredFieldValidator><br />
                                                    <asp:Label ID="fuAttachmentsfname" runat="server"></asp:Label>

 


                                                </div>
                                                <%--<asp:LinkButton ID="LbtnUpload" runat="server" Text="Upload" Visible="false"></asp:LinkButton>
                    <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete" Visible="false"></asp:LinkButton>--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LBtn_Edit" runat="server" CausesValidation="False" CommandName="EDIT"
                                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="fe-edit-1" title="Edit"></asp:LinkButton>&nbsp;&nbsp;
                                                <asp:LinkButton ID="LBtn_Delete" runat="server" CausesValidation="False" CommandName="DELETE" title="Delete"
                                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="fe-trash-2"></asp:LinkButton>&nbsp;&nbsp;
                
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <%--  <asp:LinkButton ID="LBtn_Update" runat="server" CommandName="UPDATE" ValidationGroup="vg2"
                                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Update</asp:LinkButton>&nbsp;&nbsp;
                    <asp:LinkButton ID="LBtn_Cancel" runat="server" CausesValidation="False" CommandName="CANCEL"
                        CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Cancel</asp:LinkButton>--%>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="LBtn_Update" runat="server" CommandName="UPDATE" ValidationGroup="vg2"
                                                            CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="fe-refresh-ccw" title="Update"></asp:LinkButton>&nbsp;&nbsp;
                    <asp:LinkButton ID="LBtn_Cancel" runat="server" CausesValidation="False" CommandName="CANCEL"
                        CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="dripicons-cross" title="Cancel"></asp:LinkButton>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="LBtn_Update" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </EditItemTemplate>

                                            <FooterTemplate>

                                                <%--<asp:LinkButton ID="LbtnFbpClaimsViewADD" runat="server" CommandName="ADD" ValidationGroup="vg1"
                                                    CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>Add</asp:LinkButton>--%>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <asp:LinkButton ID="LbtnFbpClaimsViewADD" runat="server" CommandName="ADD" ValidationGroup="vg1" title="Add"
                                                            CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn-xs btn-warning"><i class="fe-plus"></i></asp:LinkButton>



                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="LbtnFbpClaimsViewADD" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:HiddenField ID="HF_Fid" runat="server" />
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="false" />

                                        </asp:TemplateField>

                                        <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="FBPC_IC" HeaderText="FBP ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                    </Columns>

                                </asp:GridView>

                            </div>
                            <%--Total:<asp:label ID="lblTotalAmount" runat="server" Style="text-align: right" Width="80px"></asp:label><br />--%>
                            <asp:Label ID="lblTotalAmount" runat="server" Style="text-align: right" Width="80px" Visible="false"></asp:Label><br />

                            <div class="buttonrow">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-xs btn-secondary" Text="Save" OnClick="btnSave_Click" Visible="false" TabIndex="10" OnClientClick="javascript:return confirm('Do you want to save this Claim?')" />
                                <asp:Button ID="btnSubmitClaims" CssClass="btn btn-xs btn-secondary" runat="server" Text="Submit" OnClick="btnSubmitClaims_Click" Visible="false" TabIndex="8" OnClientClick="javascript:return confirm('Do you want to submit this Claim?')" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-xs btn-secondary" Text="Delete" OnClick="btnCancel_Click" Visible="false" TabIndex="9" OnClientClick="javascript:return confirm('Do you want to delete this Claim?')" />
                            </div>

                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
            <!-- end Tab Panel-->
        </div>
    </div>
    <!-- end row -->


    <script type="text/javascript" src="../../Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../../Scripts/JqblockUI.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            Page = Sys.WebForms.PageRequestManager.getInstance();
            Page.add_beginRequest(OnBeginRequest);
            Page.add_endRequest(endRequest);

            function OnBeginRequest(sender, args) {

            }
            function endRequest(sender, args) {

            }

        });
    </script>
    <div id="editor"></div>





    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.22/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
    <script type="text/javascript">

        $(window).on('load', function () {
            var doc = new jsPDF();
            var specialElementHandlers = {
                '#editor': function (element, renderer) {
                    return true;
                }
            };



            $('#btnLTAPDF').click(function () {
                doc.fromHTML($('#ContentPlaceHolder1_MainContent_divLTA').html(), 15, 15, {
                    'width': 170,
                    'elementHandlers': specialElementHandlers
                });
                doc.save('sample-file.pdf');
            });
        });
        function exportTableToExcel(tableID, filename = '') {
            var downloadLink;
            var dataType = 'application/vnd.ms-excel';
            var tableSelect = document.getElementById(tableID);
            var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

            // Specify file name
            filename = filename ? filename + '.xls' : 'excel_data.xls';

            // Create download link element
            downloadLink = document.createElement("a");

            document.body.appendChild(downloadLink);

            if (navigator.msSaveOrOpenBlob) {
                var blob = new Blob(['\ufeff', tableHTML], {
                    type: dataType
                });
                navigator.msSaveOrOpenBlob(blob, filename);
            } else {
                // Create a link to the file
                downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

                // Setting the file name
                downloadLink.download = filename;

                //triggering the function
                downloadLink.click();
            }
        }




            function createPDF() {
                var sTable = document.getElementById('ContentPlaceHolder1_MainContent_divLTA').innerHTML;



                var style = "<style>";
                style = style + "table {width: 100%;font: 17px Calibri;}";
                style = style + "table, th, td {border: solid 1px #DDD; border-collapse: collapse;";
                style = style + "padding: 2px 3px;text-align: center;}";
                style = style + "#ContentPlaceHolder1_MainContent_Note input[type='text']{width:30px;}";
                style = style + "input[type='text']{border:none}";
                style = style + "#ContentPlaceHolder1_MainContent_tblLTAReL, #ContentPlaceHolder1_MainContent_tblLTATRvl, #ContentPlaceHolder1_MainContent_divLTAAction, .hidden {display:none;}";
                style = style + "</style>";



                // CREATE A WINDOW OBJECT.
                var win = window.open('', '', 'height=700,width=700');



                win.document.write('<html><head>');
                win.document.write('<title>FBP</title>');   // <title> FOR PDF HEADER.
                win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
                win.document.write('</head>');
                win.document.write('<body>');
                win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
                win.document.write('</body></html>');



                win.document.close(); // CLOSE THE CURRENT WINDOW.



                win.print();    // PRINT THE CONTENTS.



                //var doc = new jsPDF();
                //doc.addHTML('#ContentPlaceHolder1_MainContent_exportFormat', function () {
                //    doc.save('html.pdf');
                //});
            }}
    </script>
</asp:Content>
