<%@ Page Title="Reset Employee Password" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true"
    Inherits="Account_ResetEmployeePassword" CodeBehind="ResetEmployeePassword.aspx.cs"
    EnableEventValidation="false" Theme="SkinFile" Culture="en-GB" UICulture="auto" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Reset Employee Password</li>
                    </ol>
                </div>
                <h4 class="page-title">Reset Employee Password
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

    <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">
                <div class="divfr" id="divbrdr">

                    <asp:Label ID="LblMsg" runat="server" CssClass="lblMsg"></asp:Label>
                   
                    <div class="search-section">

                        <div class="row">
                    <div class="col-sm-2 htCr">Employee ID&nbsp;<b>:</b></div>
                           <div class="col-sm-2">
                                <asp:TextBox ID="TxtEmpId" placeholder="Enter Emp ID" CssClass="txtDropDownwidth" runat="server" MaxLength="8" ValidationGroup="Vg"></asp:TextBox>
                                <Ajx:FilteredTextBoxExtender ID="FTB_TxtEmpId" runat="server" TargetControlID="TxtEmpId" FilterType="Numbers"></Ajx:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RFV_TxtEmpId" CssClass="lblValidation" runat="server" ControlToValidate="TxtEmpId" ValidationGroup="Vg" ErrorMessage="Enter Employee ID" Display="Dynamic" ForeColor="Red"> </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RGV_TxtEmpId" runat="server" CssClass="lblValidation" ControlToValidate="TxtEmpId" Display="Dynamic" ErrorMessage="Invalid PERNR [8 digit required] !"
                                    ValidationExpression="^[0-9]{8}" ValidationGroup="Vg" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-3">
                                <asp:Button ID="BtnReset" runat="server" Text="Reset" Width="80px" ValidationGroup="Vg" OnClick="BtnReset_Click" />
                            </div>
                        </div>
                    </div>

                <br />
                <div class="row">
                    <div class="col-sm-2 htCr">Search Employee ID&nbsp;<b>:</b></div>
                            <div class="col-md-2">
                                <asp:TextBox ID="TxtEmpIdSearch" runat="server" placeholder="Enter Emp ID" CssClass="txtDropDownwidth" MaxLength="8" ValidationGroup="Vg1"></asp:TextBox>                                
                                <asp:RequiredFieldValidator ID="RFV_TxtEmpIdSearch" runat="server" ControlToValidate="TxtEmpIdSearch" ValidationGroup="Vg1" ErrorMessage="Enter Employee ID" Display="Dynamic" ForeColor="Red"> </asp:RequiredFieldValidator>                                
                            </div>
                             <div class="col-sm-3">
                                <asp:Button ID="BtnEmpSearch" runat="server" Text="Search" ValidationGroup="Vg1" OnClick="BtnEmpSearch_Click" Width="80px" />
                            </div>
                        </div>
                    </div>

                <br />
                <br />
                    <div class="respovrflw">
                        <asp:GridView ID="GV_Users" runat="server" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="UserID,UserName" PageSize="5" Width="95%"                         
                            OnRowCommand="GV_Users_RowCommand" OnRowDeleting="GV_Users_RowDeleting" OnRowEditing="GV_Users_RowEditing" OnRowUpdating="GV_Users_RowUpdating">
                            <Columns>
                                <asp:BoundField DataField="UserName" HeaderText="Employee ID" SortExpression="UserName">
                                </asp:BoundField>
                                <asp:BoundField DataField="Email" HeaderText="Email Address" SortExpression="Email">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Send Email">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChkSendEmail" CssClass="chkboxStyl1" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New Password">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TxtUserNewPassword" runat="server" MaxLength="30" ValidationGroup="GvVg"></asp:TextBox>
                                        <br />
                                        <asp:RequiredFieldValidator ID="RFV_TxtUserNewPassword" runat="server" ControlToValidate="TxtUserNewPassword"
                                            Text="" ErrorMessage="Enter Password" ValidationGroup="GvVg" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="GVViewAddressInfoDelete" runat="server" CommandName="RESET" ValidationGroup="GvVg"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="Reset"
                                            OnClientClick="if (!confirm('Do you want to reset the password ?')) return false;"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
        </div>
    </div>
    <script type="text/javascript">
        function Validate() {
            if (Page_ClientValidate())
                return confirm('Do you want to reset the password ?');
        }
    </script>

</asp:Content>

