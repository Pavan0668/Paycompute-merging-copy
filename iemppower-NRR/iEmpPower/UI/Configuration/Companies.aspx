<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Companies.aspx.cs"  MaintainScrollPositionOnPostback="true"
     Inherits="iEmpPower.UI.Configuration.Companies" Theme="SkinFile" Culture="en-GB" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">View Company's Details</li>
                    </ol>
                </div>
                <h4 class="page-title">View Company's Details</h4>
            </div>
        </div>
    </div>

        

     <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">
                <div id="divbrdr" class="divfr">

                <h5>All Company Details</h5>
                <asp:GridView ID="GV_Comp" runat="server" AutoGenerateColumns="false" Width="100%" OnRowCommand="GV_Comp_RowCommand" DataKeyNames="Company_Code">
                    <Columns>
                        <asp:BoundField HeaderText="Company Code" DataField="Company_Code" />
                        <asp:BoundField HeaderText="Name" DataField="Company_Name" />
                        <asp:BoundField HeaderText="Company Type" DataField="Company_Type_Txt" />
                        <asp:BoundField HeaderText="Address" DataField="Company_Address" />
                        <asp:BoundField HeaderText="Country" DataField="CountryTxt" />
                        <asp:BoundField HeaderText="State" DataField="StateTxt" />
                        <asp:BoundField HeaderText="District" DataField="District" />
                        <asp:BoundField HeaderText="Pincode" DataField="Pincode" />
                        <asp:BoundField HeaderText="Mail ID" DataField="Company_MailID" />
                        <asp:BoundField HeaderText="Contact Number" DataField="Company_ContactNum" />
                        <asp:BoundField HeaderText="Created On" DataField="Created_On" />
                        <asp:TemplateField HeaderText="Is Locked">
                            <ItemTemplate>
                                <%#Eval("IsLocked").ToString()=="true"?"Yes":"No" %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkGenerate" runat="server" CommandName="Generate" Text="Generate Emp Details" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <hr />
                <asp:Panel ID="pnlempdtls" runat="server">
                    <div class="respovrflw">
                        <asp:GridView ID="gvEMpDtls" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Sl No." DataField="_1" />
                                <asp:BoundField HeaderText="Emp ID." DataField="_2" />
                                <asp:BoundField HeaderText="Salutation" DataField="_3" />
                                <asp:BoundField HeaderText="First Name" DataField="_4" />
                                <asp:BoundField HeaderText="Middle Name" DataField="_5" />
                                <asp:BoundField HeaderText="Last Name" DataField="_6" />
                                <asp:BoundField HeaderText="Short Name" DataField="_7" />
                                <asp:BoundField HeaderText="Fathers Name" DataField="_8" />
                                <asp:BoundField HeaderText="Mothers Name" DataField="_9" />
                                <asp:BoundField HeaderText="Date of Birth" DataField="_10" />
                                <asp:BoundField HeaderText="Gender" DataField="_11" />
                                <asp:BoundField HeaderText="Marital Status" DataField="_12" />
                                <asp:BoundField HeaderText="Spouse Name" DataField="_13" />
                                <asp:BoundField HeaderText="Designation" DataField="_14" />
                                <asp:BoundField HeaderText="Occupation" DataField="_15" />
                                <asp:BoundField HeaderText="Department" DataField="_16" />
                                <asp:BoundField HeaderText="Grade" DataField="_17" />
                                <asp:BoundField HeaderText="Branch" DataField="_18" />
                                <asp:BoundField HeaderText="Division" DataField="_19" />
                                <asp:BoundField HeaderText="Bank Account No." DataField="_20" />
                                <asp:BoundField HeaderText="Bank Name" DataField="_21" />
                                <asp:BoundField HeaderText="Sal Structure" DataField="_22" />
                                <asp:BoundField HeaderText="Attendance" DataField="_23" />
                                <asp:BoundField HeaderText="Res. No." DataField="_24" />
                                <asp:BoundField HeaderText="Res. Name" DataField="_25" />
                                <asp:BoundField HeaderText="Road/Street" DataField="_26" />
                                <asp:BoundField HeaderText="Locality/Area" DataField="_27" />
                                <asp:BoundField HeaderText="City/District" DataField="_28" />
                                <asp:BoundField HeaderText="State" DataField="_29" />
                                <asp:BoundField HeaderText="Pincode" DataField="_30" />
                                <asp:BoundField HeaderText="Res. No." DataField="_31" />
                                <asp:BoundField HeaderText="Res. Name" DataField="_32" />
                                <asp:BoundField HeaderText="Road/Street" DataField="_33" />
                                <asp:BoundField HeaderText="Locality/Area" DataField="_34" />
                                <asp:BoundField HeaderText="City/District" DataField="_35" />
                                <asp:BoundField HeaderText="State" DataField="_36" />
                                <asp:BoundField HeaderText="Pincode" DataField="_37" />
                                <asp:BoundField HeaderText="E - Mail ID" DataField="_38" />
                                <asp:BoundField HeaderText="STD Code" DataField="_39" />
                                <asp:BoundField HeaderText="Phone" DataField="_40" />
                                <asp:BoundField HeaderText="Mobile" DataField="_41" />
                                <asp:BoundField HeaderText="Date of Joining" DataField="_42" />
                                <asp:BoundField HeaderText="Salary calculate from" DataField="_43" />
                                <asp:BoundField HeaderText="Date of leaving" DataField="_44" />
                                <asp:BoundField HeaderText="Reason for leaving" DataField="_45" />
                                <asp:BoundField HeaderText="ESI Applicable" DataField="_46" />
                                <asp:BoundField HeaderText="ESI No" DataField="_47" />
                                <asp:BoundField HeaderText="ESI Dispensary" DataField="_48" />
                                <asp:BoundField HeaderText="PF Applicable" DataField="_49" />
                                <asp:BoundField HeaderText="PF No" DataField="_50" />
                                <asp:BoundField HeaderText="PF No for Dept File" DataField="_51" />
                                <asp:BoundField HeaderText="Restrict PF" DataField="_52" />
                                <asp:BoundField HeaderText="Zero Pension" DataField="_53" />
                                <asp:BoundField HeaderText="Zero PT" DataField="_54" />
                                <asp:BoundField HeaderText="PAN" DataField="_55" />
                                <asp:BoundField HeaderText="Ward/Circle" DataField="_56" />
                                <asp:BoundField HeaderText="Director" DataField="_57" />
                                <asp:BoundField HeaderText="UAN NO" DataField="_58" />
                                <asp:BoundField HeaderText="IFSC Code" DataField="_59" />
                                <asp:BoundField HeaderText="Aadhar No." DataField="_60" />
                                <asp:BoundField HeaderText="Remarks" DataField="_61" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div>
                        <br />
                        <asp:UpdatePanel ID="UPD_Admin_salary_upload" runat="server">
                        <ContentTemplate>
                        <asp:Button ID="BtnExporttoXl" runat="server" Width="80px" Text="Export" OnClick="BtnExporttoXl_Click" CausesValidation="false" TabIndex="5" />
                             </ContentTemplate>
                            <Triggers>
                              <asp:PostBackTrigger ControlID="BtnExporttoXl" />
                                </Triggers>
                                 </asp:UpdatePanel>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>
