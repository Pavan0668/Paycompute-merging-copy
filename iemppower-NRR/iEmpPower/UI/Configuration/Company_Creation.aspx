<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Company_Creation.aspx.cs"  MaintainScrollPositionOnPostback="true"
    Inherits="iEmpPower.UI.Configuration.Company_Creation" Culture="en-GB" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Company Creation</li>
                    </ol>
                </div>
                <h4 class="page-title">Company Creation</h4>
            </div>
        </div>
    </div>


        <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">
                <div id="divbrdr" class="divfr">
                            <div class="row">
                                       <div class="col-sm-2 htCr">1. Company Name  :</div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txt_Cname" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="REQ_Cname" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txt_Cname" ForeColor="Red" ErrorMessage="Please Enter Company Name">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                    <br />
                                <div class="row">
                                  <div class="col-sm-2 htCr">2. Company Code :</div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txt_Ccode" CssClass="txtDropDownwidth" runat="server" MaxLength="8"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="REQ_Ccode" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txt_Ccode" ForeColor="Red" ErrorMessage="Please Enter Company Code">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                    <br />
                                <div class="row">
                                  <div class="col-sm-2 htCr">3. Company Type :</div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="DDL_Ctype" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                    <br />

                                <div class="row">
                                  <div class="col-sm-2 htCr">4. Official E-Mail :</div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txt_Cemail" CssClass="txtDropDownwidth" runat="server" MaxLength="99"> </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="REQ_Cmail" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txt_Cemail" ForeColor="Red" ErrorMessage="Please Enter E-Mail ID">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="REL_Cemail" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txt_Cemail"
                                            ErrorMessage="Please enter valid email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">  
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                    <br />
                               <div class="row">
                                  <div class="col-sm-2 htCr">5. Official Contact No. :</div>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" CssClass="txtDropDownwidth" ID="txt_Ccontctno" MaxLength="12"></asp:TextBox>
                                        <Ajx:FilteredTextBoxExtender ID="Filteredtxt_Ccontctno" runat="server"
                                            TargetControlID="txt_Ccontctno" FilterType="Numbers">
                                        </Ajx:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="REQ_Ccontact" runat="server" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txt_Ccontctno" ValidationGroup="Csave" ForeColor="Red" ErrorMessage="Please Enter Contact NO">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                    <br />
                                <div class="row">
                                  <div class="col-sm-2 htCr">6. Company Address :</div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txt_Caddress" CssClass="txtDropDownwidth" runat="server" TextMode="MultiLine" MaxLength="249"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="REQ_Cadress" runat="server" ValidationGroup="Csave" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txt_Caddress" ForeColor="Red" ErrorMessage="Please Enter Company Address">
                                        </asp:RequiredFieldValidator>
                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxtxt_Caddress" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" TargetControlID="txt_Caddress" ValidChars=" .#/," FilterMode="ValidChars" />
                                    </div>
                                </div>
                    <br />
                               <div class="row">
                                  <div class="col-sm-2 htCr">7. District :</div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtDist" CssClass="txtDropDownwidth" runat="server" MaxLength="39"></asp:TextBox>
                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxtxtDist" runat="server" FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtDist" />
                                        <asp:RequiredFieldValidator ID="RFV_txtDist" runat="server" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDist" ValidationGroup="Csave" ForeColor="Red" ErrorMessage="Please Enter District">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                    <br />
                                <div class="row">
                                  <div class="col-sm-2 htCr">8. Country :</div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="DDL_Ccountry" CssClass="txtDropDownwidth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Ccountry_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                    <br />
                                <div class="row">
                                  <div class="col-sm-2 htCr">9. State :</div>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="DDL_Cstate" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                    <br />
                                <div class="row">
                                  <div class="col-sm-2 htCr">10. Pincode :</div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txt_Cpincode" CssClass="txtDropDownwidth" runat="server" MaxLength="10"></asp:TextBox>
                                        <Ajx:FilteredTextBoxExtender ID="FLT_Cpincode" runat="server" FilterType="Numbers" TargetControlID="txt_Cpincode" />
                                        <asp:RequiredFieldValidator ID="REQ_Cpincode" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txt_Cpincode" ForeColor="Red" ErrorMessage="Please Enter Pincode">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                    <br />
                                <div class="row">
                                  <div class="col-sm-2 htCr">11. Logo :</div>
                                    <div class="col-sm-6">
                                        <asp:FileUpload ID="flLogo" runat="server"></asp:FileUpload>

                                        <asp:RegularExpressionValidator ID="regexValidator" runat="server"
                                            ControlToValidate="flLogo"
                                            ErrorMessage="Only PNG images are allowed"
                                            ValidationExpression="(.*\.([Pp][Nn][Gg])|.*\.([Pp][Nn][Gg])$)" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave">
                                        </asp:RegularExpressionValidator>
                                    </div>
                                </div>
                    <br />
                   
                                <asp:UpdatePanel ID="upnl_company" runat="server">
                                <ContentTemplate>
                                     <div class="row">
                                  <div class="col-sm-6 htCr">
                                    <asp:Button ID="btn_Csave" runat="server" Text="Create" ValidationGroup="Csave" OnClick="btn_Csave_Click" />
                                    <asp:Button ID="btn_Ccancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btn_Ccancel_Click" />
                                      </div>
                            </div>
                                </ContentTemplate>
                             <Triggers>
                                    <asp:PostBackTrigger ControlID="btn_Csave" />
                                 <asp:PostBackTrigger ControlID="btn_Ccancel" />
                                </Triggers>
                            </asp:UpdatePanel>
                                    

                                
                    
                </div>
            </div>
        </div>
 
</asp:Content>
