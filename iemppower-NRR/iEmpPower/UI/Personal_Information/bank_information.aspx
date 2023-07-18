<%@ Page Title="Bank Information" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" 
    Inherits="iEmpPower.UI_Employee_Performance_bank_information" MaintainScrollPositionOnPostback="true"
    Theme="SkinFile" EnableEventValidation="false" Culture="en-Gb" CodeBehind="bank_information.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
   <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Bank Info.</li>
                    </ol>
                </div>
                <h4 class="page-title">Bank Information
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

        <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="Fl lblMsg" Font-Size="11px"></asp:Label>
    </div>

        <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">

                <div id="divbrdr" class="divfr">
                    <div class="search-section">

                        <asp:MultiView ID="MV_BankInfo" runat="server">
                            <asp:View ID="V_ViewBankInfo" runat="server">
                                <div class="DivSpacer01">
                                    &nbsp;<asp:LinkButton runat="server" CausesValidation="false" Text="Add New Bank Info" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right"
                                    OnClick="LbtnAddBankInfo_Click" Visible="false"><i class="mdi mdi-plus"></i>Add New Bank Info</asp:LinkButton>
                                </div>
                                <div class="DivSpacer01 respovrflw">
                                    <asp:GridView ID="GV_BankInfo" runat="server" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ID"
                                        PageSize="5" Width="100%" OnRowCommand="GV_BankInfo_RowCommand" OnRowDeleting="GV_BankInfo_RowDeleting" OnRowEditing="GV_BankInfo_RowEditing"
                                        OnRowUpdating="GV_BankInfo_RowUpdating">
                                        <Columns>
                                            <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name">
                                                <ItemStyle Width="35px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="F_Name" HeaderText="Account Number">
                                                <ItemStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="IFSC_Code" HeaderText="IFSC Code">
                                                <ItemStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Bank_Branch" HeaderText="Bank Branch">
                                                <ItemStyle />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="District" HeaderText="District">
                                                <ItemStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="StateTxt" HeaderText="State">
                                                <ItemStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CountryTxt" HeaderText="Country">
                                                <ItemStyle />
                                            </asp:BoundField>
                                           
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="DivSpacer01 Div03">
                                    <asp:Repeater ID="RptrBankInfoPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="RptrLeaveOverviewPagerPage_Changed" CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="DivSpacer01"></div>
                            </asp:View>
                            <asp:View ID="V_AddEditBankInfo" runat="server">
                                <div class="DivSpacer01">
                                    <div style="width: 90%; float: left"></div>
                                    <div style="width: 10%; float: right">
                                        <asp:LinkButton ID="LbtnBackBankInfoView" runat="server" CausesValidation="false" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right" OnClick="LbtnBackBankInfoView_Click"><i class="mdi mdi-step-backward"></i>  Back</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="DivSpacer01">
                                    <asp:FormView ID="FV_BankInfo" runat="server" DataKeyNames="ID" CssClass="DivSpacer01" OnItemCommand="FV_BankInfo_ItemCommand" OnModeChanging="FV_BankInfo_ModeChanging">                                        

                                        <ItemTemplate>
                                            <div>
                                                <b>Bank information</b>
                                                <div class="DivSpacer01"></div>
                                            </div>
                                            <div class="DivSpacer03 Cb">

                                                <div class="form-inline">
                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr">Bank Type &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("BANK_TYPE_NAME")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr">Bank Key  &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("BANK_KEY") %></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr">Bank County &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("COUNTRY_NAME") %></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr">Bank Account &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("BANK_ACCOUNT")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr">Payment Method &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("PAYMENT_METHOD_NAME")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr">Payment Currency&nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("PAYMENT_CURRENCY_NAME")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr1">Payee  &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("PAYEE") %></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr1">City Name &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("CITY")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr1">PIN Code &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("POSTAL_CODE")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr1">Purpose  &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("PURPOSE")%></div>
                                                    </div>
                                                </div>



                                            </div>
                                            <div class="DivSpacer01">
                                            </div>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <InsertItemTemplate>
                                        </InsertItemTemplate>
                                    </asp:FormView>
                                </div>
                            </asp:View>
                        </asp:MultiView>

                    </div>
                </div>
            </div>
        </div>
</asp:Content>

