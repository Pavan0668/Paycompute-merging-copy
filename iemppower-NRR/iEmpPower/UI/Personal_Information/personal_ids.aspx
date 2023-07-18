<%@ Page Title="Personal IDs" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" EnableEventValidation="false"
    Inherits="UI_Personal_Information_personal_ids" Culture="en-GB" Theme="SkinFile" CodeBehind="personal_ids.aspx.cs" MaintainScrollPositionOnPostback="true" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
     <script type="text/javascript">
         function validate(Msg) {
             if (Page_ClientValidate())
                 return confirm('Do you want to ' + Msg + ' this Personal Id details ?');
         }

    </script>
    <style>
        .HrCls {
            width: 100%;
            border: 0;
            height: 1px;
            background: #333;
            background-image: linear-gradient(to right, #333, #333, #ccc);
            padding: 0;
            margin: 3px 0;
        }

        .updatedoctext {
            display: none;
            color: transparent;
        }
    </style>

    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Personal Id Information</li>
                    </ol>
                </div>
                <h4 class="page-title">Personal Id Information
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

    <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
    </div>


    <div class=" card-box">
        <div class="dashboard-flot-chart">
                <div class="search-section">

                    <div id="D_ViewPersonalIdInfo" runat="server">

                        <div class="DivSpacer01">
                            &nbsp;<asp:LinkButton ID="LblAddPersonalIdInfo" CausesValidation="false" OnClick="LblAddPersonalIdInfo_Click" runat="server" Text="Add New PersonalId Info" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right"><i class="mdi mdi-plus"></i> Add New Personal Id Info</asp:LinkButton>
                        </div>

                        <div id="gvidesi" runat="server">
                            <div class="DivSpacer01 respovrflw">
                                <asp:GridView ID="GV_PersonalIdInfo" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                                    DataKeyNames="ID,Doc_Type_ID,Doc_Type_TXT,docpath" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager"
                                    PageSize="10" Width="100%" OnRowCommand="GV_PersonalIdInfo_RowCommand" OnRowDeleting="GV_PersonalIdInfo_RowDeleting"
                                    OnRowEditing="GV_PersonalIdInfo_RowEditing" OnRowUpdating="GV_PersonalIdInfo_RowUpdating" ShowHeaderWhenEmpty="false">
                                    <Columns>
                                        <asp:BoundField DataField="Doc_Type_TXT" HeaderText="ID type">
                                            <ItemStyle />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Doc_Type_ID" HeaderText="ID Number / Details"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LK_docdownload" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    CommandName="docdown" ToolTip="Download"><i class="fe-download"></i></asp:LinkButton>
                                                &nbsp;
                                                    <asp:LinkButton ID="GVEditPersonalIdInfo" ToolTip="Edit" runat="server" CausesValidation="false" CommandName="EDITID"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="fe-edit-1"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />

                        <div class="DivSpacer01 respovrflw">
                            <div class="form-group">
                                <div class="row">
                                    <asp:GridView ID="GV_ESIpf_benifits" runat="server" AutoGenerateColumns="False" AllowPaging="false">
                                        <Columns>
                                            <asp:BoundField HeaderText="ID Type" DataField="ICTYPE" />
                                            <asp:BoundField HeaderText="ID Details" DataField="ID_TYPE_TEXT" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="DivSpacer01 Div03">
                            <asp:Repeater ID="RptrPersonalIdInfoPager" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="RptrLeaveOverviewPagerPage_Changed" CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="DivSpacer01"></div>
                    </div>


                     <asp:UpdatePanel ID="uppi" runat="server" UpdateMode="Conditional">
                      <ContentTemplate>
                    <div id="D_AddEditPersonalIdInfo" runat="server">

                        <div style="width: 10%; float: right">
                            <asp:LinkButton ID="LbtnBackPersonalIdInfoView" runat="server" CausesValidation="false" OnClick="LbtnBackPersonalIdInfoView_Click" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right"><i class="mdi mdi-step-backward"></i>  Back</asp:LinkButton>
                        </div>
                        <br />

                            <div id="dvcreatePI" runat="server">
                                <div class="header-title">Add New Personal Id Information</div>
                                <hr class="HrCls" />
                                <br />

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-2">
                                                <span style="color: red">*</span>PersonalId Type <b>:</b>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:DropDownList ID="DDL_PersonalIdTypes" runat="server" CssClass="txtDropDownwidth"
                                                    TabIndex="4" ValidationGroup="AddPersonalIdInfoVG" OnSelectedIndexChanged="DDL_PersonalIdTypes_SelectedIndexChanged" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFV_DDL_PersonalIdTypes" runat="server" ControlToValidate="DDL_PersonalIdTypes" Display="Dynamic" CssClass="lblValidation"
                                                    ErrorMessage="Please select Personal Id type" ForeColor="Red" InitialValue="0" ValidationGroup="AddPersonalIdInfoVG"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-4 text-right">
                                                [NOTE : Use PDF documents to upload attachment file]
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2 htCr">
                                                <span style="color: red">*</span>ID number<b>:</b>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:TextBox ID="TxtIdNumber" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                    MaxLength="30" placeholder="Enter ID number" TabIndex="5" ValidationGroup="AddPersonalIdInfoVG"></asp:TextBox>
                                                <Ajx:FilteredTextBoxExtender ID="FTB_TxtIdNumber" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                    ValidChars=" .-/,()[]_&" TargetControlID="TxtIdNumber">
                                                </Ajx:FilteredTextBoxExtender>

                                                <asp:RequiredFieldValidator ID="RFV_TxtIdNumber" runat="server" ControlToValidate="TxtIdNumber" CssClass="lblValidation"
                                                    Display="Dynamic" ErrorMessage="Please enter ID details" ValidationGroup="AddPersonalIdInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2 htCr">
                                                <span style="color: white">*</span>Document Attachment<b>:</b>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:FileUpload ID="file_docupload" runat="server" />
                                            </div>
                                        </div>
                                   
                                    <div class="row">
                                        <div class="col-md-6">

                                            <asp:Button ID="BtnSave" runat="server" Text="Submit" ValidationGroup="AddPersonalIdInfoVG"
                                                TabIndex="6" OnClientClick="validate('Add');" OnClick="BtnSave_Click" />
                                            <asp:Button ID="btnCancelcrt" runat="server" Text="Cancel" OnClick="btnCancelcrt_Click" TabIndex="8" />

                                        </div>
                                    </div>
                                         </div>
                               
                            </div>



                        <div id="dvupdatePI" runat="server">

                               
                                        <div class="header-title">
                                            <b>Update PersonalId information - [<span style="text-transform: uppercase;"><asp:Label ID="lblidtext" runat="server"></asp:Label></span>]</b>
                                        </div>
                                        <hr class="HrCls" />
                                        <br />


                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <span style="color: white">*</span>PersonalId Type&nbsp; <b>:</b>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:Label ID="lblidtextin" runat="server"></asp:Label>

                                                    <asp:Label ID="lbl_typtext" CssClass="updatedoctext" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-md-4 text-right">
                                                    [NOTE : Use PDF documents to upload attachment file]
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <span style="color: red">*</span>ID number&nbsp; <b>:</b>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:TextBox ID="TxtEditIdNumber" runat="server" autocomplete="off" CssClass="txtDropDownwidth" placeholder="Enter ID number"
                                                        MaxLength="30" TabIndex="1" ValidationGroup="UpdatePersonalIdInfoVG"></asp:TextBox>
                                                    <Ajx:FilteredTextBoxExtender ID="FTB_TxtEditIdNumber" runat="server" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                        ValidChars=" .-/,()[]_&" TargetControlID="TxtEditIdNumber">
                                                    </Ajx:FilteredTextBoxExtender>

                                                    <asp:RequiredFieldValidator ID="RFV_TxtEditIdNumber" runat="server" ControlToValidate="TxtEditIdNumber" CssClass="lblValidation"
                                                        Display="Dynamic" ErrorMessage="Please enter ID details" ValidationGroup="UpdatePersonalIdInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <span style="color: white">*</span>Document Attchment&nbsp; <b>:</b>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:FileUpload ID="FileUpload1" runat="server" />
                                                </div>
                                            </div>

                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Button ID="btn_updatePIdoc" runat="server" ValidationGroup="UpdatePersonalIdInfoVG" Text="Update"  OnClientClick="validate('Update');"
                                                    TabIndex="2" OnClick="btn_updatePIdoc_Click" />
                                                <asp:Button ID="BtnCancel" runat="server" Text="Cancel" TabIndex="3" 
                                                    CausesValidation="false" OnClick="BtnCancel_Click" />


                                            </div>

                                        </div>
                                    </div>
                                   
                            </div>
                        </div>
                     </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="GV_PersonalIdInfo" />
                                        <asp:PostBackTrigger ControlID="BtnSave" />
                                        <asp:PostBackTrigger ControlID="btn_updatePIdoc" />
                                        <asp:PostBackTrigger ControlID="LblAddPersonalIdInfo" />
                                    </Triggers>
                                </asp:UpdatePanel>


                    </div>
            </div>
        </div>
    </asp:Content>
