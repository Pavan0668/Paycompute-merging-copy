<%@ Page Title="Personal Data" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" Inherits="UI_Personal_Information_personal_file"
    Theme="SkinFile" Culture="en-GB" CodeBehind="personal_data.aspx.cs" EnableEventValidation="false"  MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
   

         <div class="row">
            <div class="col-12">
                <div class="page-title-box">
                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Personal Data</li>
                        </ol>
                    </div>
                    <h4 class="page-title">Personal&nbsp;Data&nbsp;Information&nbsp;                 
                    </h4>
                </div>
            </div>
        </div>

     <div class="header">           
            <asp:Label ID="LblMsg" runat="server" CssClass="lblMsg"></asp:Label>
        </div>


        <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">
                <div id="divbrdr" class="divfr">
                    <div class="search-section">
                        <asp:FormView ID="FV_PersonalDataInfo" runat="server" Width="99%" DataKeyNames="ID" OnItemCommand="FV_PersonalDataInfo_ItemCommand" OnModeChanging="FV_PersonalDataInfo_ModeChanging" >


                            <ItemTemplate>
                                <div class="DivSpacer01">
                                    <asp:LinkButton ID="LbtnEditPerInfo" runat="server" TabIndex="1"
                                        CausesValidation="false" CommandName="EDITPERINFO" CssClass="btn btn-sm bg-brand-btn waves-effect waves-light float-right"><i class="fe-edit-1"></i> Edit</asp:LinkButton>
                                </div>
                                <div class="DivSpacer03 Cb">
                                    <div class="row">
                                        <div class="col-sm-9 Fl">

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-3 htCr">Title&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6"><%# Eval("Created_By")%></div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 htCr">First Name&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6"><%# Eval("F_Name") %></div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 htCr">Middle Name&nbsp;<b>:</b></div>
                                                     <div class="col-sm-6"><%# (Eval("L_Name").ToString()=="")?"-":Eval("L_Name")%></div>
                                                    
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 htCr">Last Name&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6"><%# (Eval("M_Name").ToString()=="")?"-":Eval("M_Name")%></div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 htCr">Gender&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6"><%# Eval("Gender").ToString()=="1"?"Male":Eval("Gender").ToString()=="2"?"Female":"Others"%></div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 htCr">Date of Birth&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6"><%# Eval("DOB", "{0:dd/MM/yyyy}") %></div>
                                                </div>

                                                <div class="row">
                                        <div class="col-sm-3 htCr">Father Name &nbsp;<b>:</b> </div>
                                        <div class="col-sm-6"><%# (Eval("Father_Name").ToString()=="")?"-":Eval("Father_Name")%></div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-3 htCr">Mother Name&nbsp;<b>:</b> </div>
                                        <div class="col-sm-6"><%# (Eval("Mother_Name").ToString()=="")?"-":Eval("Mother_Name")%></div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-3 htCr">Spouse Name&nbsp;<b>:</b> </div>
                                        <div class="col-sm-6"><%#(Eval("Spouse_Name").ToString()=="")?"-":Eval("Spouse_Name")%></div>
                                    </div>

                                    <div class="row">
                                        <div class="col-sm-3 htCr">Maritial Status &nbsp;<b>:</b></div>
                                        <div class="col-sm-6"><%# Eval("Material_StatusTxt")%></div>
                                    </div>
                                            </div>

                                        </div>
                                        <div class="col-sm-3 Fr">
                                            <asp:UpdatePanel runat="server" ID="a">
                                             <ContentTemplate>                       
                                            <table class="TblCls Cb Fl">
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="ImgEmpPhoto" runat="server" CssClass="ImgCls" AlternateText="Img" Height="45px" Width="45px" ImageUrl='<%#Bind("Emp_Photo") %>' /></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:FileUpload ID="FU_ProfImg" runat="server" ForeColor="#F44336" CssClass="Fnt02 Fl" onchange="showimagepreview(this)" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;
                                             <asp:RequiredFieldValidator ID="RFV_FU_ProfImg" runat="server" ControlToValidate="FU_ProfImg" ForeColor="Red"
                                                 ValidationGroup="VGProfImg" Display="Dynamic" ErrorMessage="Please select image" CssClass="Fnt02"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REV_FU_ProfImg" runat="server" ControlToValidate="FU_ProfImg" ValidationGroup="VGProfImg"
                                                            Display="Dynamic" ErrorMessage="Please select only image file (.doc or .docx)" CssClass="Fnt02" ForeColor="Red"
                                                            ValidationExpression="^.+(.jpg|.JPG|.gif|.GIF|.png|.PNG|.jpeg|.JPEG)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="LbtnUploadImg" runat="server" Text="&#171;&nbsp;Upload"  CausesValidation="true" ValidationGroup="VGProfImg"
                                                                    CommandName="UPLOADPROFIMAGE" CssClass="btn btn-xs btn-secondary"> <i class="fe-upload" ></i> </asp:LinkButton>
                                                        <asp:LinkButton ID="LbtnImageDelete" runat="server" CausesValidation="false" ValidationGroup="VGProfImg"
                                                                    CommandName="DELETPROFIMAGE" CssClass="btn btn-xs btn-secondary" OnClientClick="javascript:return confirm('Do you want to delete profile image ?')"> <i class="fe-trash-2" ></i> </asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                                        </ContentTemplate> 
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="LbtnUploadImg"/>
                                                    <asp:PostBackTrigger ControlID="LbtnImageDelete"/>
                                                </Triggers>
                                                </asp:UpdatePanel>

                                        </div>
                                    </div>
                                </div>
                               
                            </ItemTemplate>


                            <EditItemTemplate>
                                <div class="DivSpacer01">
                                    <asp:LinkButton ID="LbtnCancelPerInfo" TabIndex="19" runat="server" Text="&#171;&nbsp;Cancel" CausesValidation="false" CommandName="CANCELPERSINFO" CssClass="Fr Fnt02 linkbtn Td10"> </asp:LinkButton>
                                    <asp:LinkButton ID="LbtnUpdatePerInfo" TabIndex="18" runat="server" Text="&#171;&nbsp;Update" CausesValidation="true" CommandName="UPDATEPERSINFO" CssClass="Fr Fnt02 linkbtn Td10" ValidationGroup="UpdatePIInfoVG"> </asp:LinkButton>
                                </div>
                                <br />
                                <div class="">
                                    <b class="Td09">Personal Information</b>
                                    <div class="DivSpacer01"></div>
                                </div>
                                <div class="DivSpacer03 Cb">
                                    <div class="row">
                                        <div class="col-sm-9 Fl">

                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-3 htCr"><span style="color:white">*</span>Title&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6"><%# Eval("Created_By")%></div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 htCr"><span style="color:white">*</span>First Name&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6"><%# Eval("F_Name") %></div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 htCr"><span style="color:white">*</span>Middle Name&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6"><%# Eval("L_Name") %></div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 htCr"><span style="color:white">*</span>Last Name&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6"><%# Eval("M_Name") %></div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 htCr"><span style="color:white">*</span>Gender&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6">
                                                        <asp:RadioButtonList ID="RbtnGender" runat="server" TabIndex="3" ValidationGroup="UpdatePIInfoVG" RepeatDirection="Horizontal"
                                                            SelectedValue='<%# Bind("Gender") %>'>
                                                            <asp:ListItem Value="1" Text="Male"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Female"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Others"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        <asp:RequiredFieldValidator ID="RFV_RbtnGender" runat="server" ControlToValidate="RbtnGender" CssClass="lblValidation"
                                                            Display="Dynamic" ErrorMessage="Please select Gender" ValidationGroup="UpdatePIInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-sm-3 htCr"><span style="color:red">*</span>Date of birth&nbsp;<b>:</b></div>
                                                    <div class="col-sm-6">
                                                        <asp:TextBox ID="TxtDOB" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                            TabIndex="4" ValidationGroup="UpdatePIInfoVG" Text='<%# Eval("DOB", "{0:dd/MM/yyyy}")%>'></asp:TextBox>
                                                        <Ajx:MaskedEditExtender ID="MEE_TxtDOB" runat="server" AcceptNegative="Left"
                                                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                            OnInvalidCssClass="MaskedEditError" TargetControlID="TxtDOB" AcceptAMPM="false" ClearTextOnInvalid="true"
                                                            UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour" />
                                                        <Ajx:CalendarExtender ID="CE_TxtDOB" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                            TargetControlID="TxtDOB" PopupButtonID="TxtDOB">
                                                        </Ajx:CalendarExtender>

                                                        <asp:RequiredFieldValidator ID="RFV_TxtDOB" runat="server" ControlToValidate="TxtDOB" CssClass="lblValidation"
                                                            Display="Dynamic" ErrorMessage="Please enter Date Of Birth" ValidationGroup="UpdatePIInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>

                                                        <asp:RangeValidator ID="RV_TxtDOB" runat="server" ControlToValidate="TxtDOB" ForeColor="Red" CssClass="lblValidation"
                                                            Display="Dynamic" ErrorMessage="Invalid DOB [Age must be between :18 to 50 Year(s)]" MaximumValue='<%# DateTime.Now.AddYears(-18).ToString("dd/MM/yyyy") %>' MinimumValue='<%# DateTime.Now.AddYears(-50).ToString("dd/MM/yyyy") %>'
                                                            Type="Date" SetFocusOnError="True" ValidationGroup="UpdatePIInfoVG"></asp:RangeValidator>
                                                        <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic" CssClass="lblValidation"
                                                            ErrorMessage="Invalid Date" ControlToValidate="TxtDOB" ValidationGroup="UpdatePIInfoVG"
                                                            SetFocusOnError="True" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" ForeColor="Red"></asp:RegularExpressionValidator>

                                                    </div>
                                                </div>

                                                     <div class="row">
                                            <div class="col-sm-3 htCr"><span style="color:red">*</span>Maritial Status&nbsp;<b>:</b></div>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="DDLMaritialStatus" runat="server" CssClass="txtDropDownwidth" 
                                                    TabIndex="15">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFV_DDLMaritialStatus" runat="server" ControlToValidate="DDLMaritialStatus" Display="Dynamic" CssClass="lblValidation"
                                                    ErrorMessage="Please select Maritial Status" ForeColor="Red" InitialValue="00" ValidationGroup="UpdatePIInfoVG"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-3 htCr"><span style="color:white">*</span>Father Name&nbsp;<b>:</b></div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtFathrName" Text='<%# Eval("Father_Name")%>' runat="server" CssClass="txtDropDownwidth" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 htCr"><span style="color:white">*</span>Mother Name&nbsp;<b>:</b></div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtMthrName" Text='<%# Eval("Mother_Name")%>' runat="server" CssClass="txtDropDownwidth" ></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-3 htCr"><span style="color:white">*</span>Spouse Name&nbsp;<b>:</b></div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtSpousrName" Text='<%# Eval("Spouse_Name")%>' CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                            </div>


                                        </div>
                                        <div class="col-sm-3 Fr">
                                            <table class="TblCls Cb Fl">
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Image ID="ImgEmpPhoto" runat="server" CssClass="ImgCls" AlternateText="Img" Height="45px" Width="45px" ImageUrl='<%#Bind("Emp_Photo") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:FileUpload ID="FU_ProfImg" runat="server" CssClass="Fnt02 Fl" onchange="showimagepreview(this)" TabIndex="5" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;
                                             <asp:RequiredFieldValidator ID="RFV_FU_ProfImg" runat="server" ControlToValidate="FU_ProfImg" ForeColor="Red"
                                                 ValidationGroup="VGProfImg" Display="Dynamic" ErrorMessage="Please select image" CssClass="lblValidation"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="REV_FU_ProfImg" runat="server" ControlToValidate="FU_ProfImg" ValidationGroup="VGProfImg"
                                                            Display="Dynamic" ErrorMessage="Select only image file (.doc or .docx)" CssClass="lblValidation" ForeColor="Red"
                                                            ValidationExpression="^.+(.jpg|.JPG|.gif|.GIF|.png|.PNG|.jpeg|.JPEG)$"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:LinkButton ID="LbtnUploadImg" runat="server" TabIndex="6" Text="&#171;&nbsp;Upload" CausesValidation="true" ValidationGroup="VGProfImg"
                                                            CommandName="UPLOADPROFIMAGE" CssClass="btn btn-xs btn-secondary"> <i class="fe-upload" ></i> </asp:LinkButton>
                                                        <asp:LinkButton ID="LbtnImageDelete" runat="server" TabIndex="7" Text="&#171;&nbsp;Delete" CausesValidation="false" ValidationGroup="VGProfImg"
                                                            CommandName="DELETPROFIMAGE" CssClass="btn btn-xs btn-secondary" OnClientClick="javascript:return confirm('Do you want to delete profile image ?')"> <i class="fe-trash-2" ></i></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                                </div>
                              
                                </div>
                            </EditItemTemplate>

                        </asp:FormView>
                    </div>
                </div>
            </div>
        </div>
    <script type="text/javascript">
        function showimagepreview(input) {
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('.ImgCls').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }        
    </script>
</asp:Content>

