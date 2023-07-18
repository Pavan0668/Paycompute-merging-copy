<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.Master" AutoEventWireup="true" CodeBehind="addData.aspx.cs" Inherits="iEmpPower.UI.Configuration.addData" 
    MaintainScrollPositionOnPostback="true" Theme="SkinFile" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>
<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
 .HrCls
        {
            width: 100%;
            border: 0;
            height: 1px;
            background: #333;
            background-image: linear-gradient(to right, #333, #333, #ccc);
            padding: 0;
            margin: 3px 0;
        }
        .ccode {
            display:none;
        }
 
    </style>
    
     <script>
         function myFunction() {
             document.getElementById("MainContent_divForm").reset() ;
         }
    </script>

    <script>
        function myFunction() {
            document.getElementById("MainContent_divForm").reset();
        }
    </script>

    <script type ="text/javascript">

        var validFilesTypes = ["pdf", "jpeg", "jpg", "png"];

        function ValidateFile() {

            var file = document.getElementById("<%=file_docupload.ClientID%>");

            var label = document.getElementById("<%=lbldocupmssg.ClientID%>");

            var path = file.value;

            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();

            var isValidFile = false;

            for (var i = 0; i < validFilesTypes.length; i++) {

                if (ext == validFilesTypes[i]) {

                    isValidFile = true;

                    break;

                }

            }

            if (!isValidFile) {

                label.style.color = "red";

                label.innerHTML = "Invalid File. Please upload a file with pdf and image formats format";

            }

            return isValidFile;

        }

</script>
    

    <div class="row">
            <div class="col-12">
                <div class="page-title-box">
                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Add User's Details</li>
                        </ol>
                    </div>
                    <h4 class="page-title">Add User's Details
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                    </h4>
                </div>
            </div>
        </div>
        <div class="header">
            <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
        </div>

    <div class="card-box">
            <div id="real_time_chart" class="row">
                <div style="width: 99%; margin: 0 auto; padding: 5px 0 40px 0;">
                     <div class="col-sm-12"  style="width:100%">
                    <div  style="width:100%">
                        <ul class="nav nav-pills navtab-bg" >
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab1" class="nav-link p-2" OnClick="Tab1_Click" CausesValidation="false"><i class="fe-arrow-up-circle" ></i>
                            Excel Upload</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab2" class="nav-link  p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-file-text"></i>
                            Form Upload</asp:LinkButton></li>
                            <%--<li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab3" class="nav-link  p-2" OnClick="Tab3_Click" CausesValidation="false"><i class="fe-eye"></i>
                            View/Edit Employee</asp:LinkButton></li> --%>                          
                   </ul>
                    <div class="tabcontents">
                        <div id="view1" runat="server" visible="false"  style="width:100%">
                            <br />
                          <div class="header-title">&nbsp;&nbsp;Add Employees</div>
                                 <hr class="HrCls"/>
                            <br />
                                <div class="row">
                                    <div class="md-2">&nbsp;&nbsp;&nbsp;&nbsp; Upload Employee Details</div>
                                    <div class="col-md-3">
                                        <asp:FileUpload ID="uflEmpData" runat="server" AllowMultiple="false" />
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button ID="btnUploadEmpData" runat="server" Text="Check" OnClick="btnUploadEmpData_Click" />
                                        <asp:Button ID="btnSave" runat="server" Visible="false" Text="Upload" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnClear" runat="server" Visible="false" Text="Cancel" OnClick="btnClear_Click" />
                                    </div>
                                    <div class="col-md-3" style="text-align: right">
                                 <asp:UpdatePanel ID="b" runat="server">
                                 <ContentTemplate>
                                        <asp:LinkButton ID="lnkTemplDwnld" runat="server" Text="Download Template" OnClick="lnkTemplDwnld_Click"></asp:LinkButton>
                                      </ContentTemplate>
                                      <Triggers>
                                          <asp:PostBackTrigger ControlID="btnUploadEmpData" />
                                           <asp:PostBackTrigger ControlID="btnSave" />
                                         <asp:PostBackTrigger ControlID="lnkTemplDwnld" />
                                       </Triggers>
                                  </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12" id="divgrds" runat="server" visible="false">
                                        <h4>Employee Personal Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_EmpInfo" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee_ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Saluation" DataField="Salutation" />
                                                    <asp:BoundField HeaderText="First Name" DataField="First_Name" />
                                                    <asp:BoundField HeaderText="Middle Name" DataField="Middle_Name" />
                                                    <asp:BoundField HeaderText="Last Name" DataField="Last_Name" />
                                                    <asp:BoundField HeaderText="Gender" DataField="Gender" />
                                                    <asp:BoundField HeaderText="Date of Birth" DataField="Date_of_Birth" />
                                                    <asp:BoundField HeaderText="Marital Status" DataField="Marital_Status" />
                                                    <asp:BoundField HeaderText="Father Name" DataField="Father_Name" />
                                                    <asp:BoundField HeaderText="Mother Name" DataField="Mother_Name" />
                                                    <asp:BoundField HeaderText="Spouse Name" DataField="Spouse_Name" />

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Department Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="gv_dept" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee_ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Department" DataField="Employee_Department" />
                                                    <asp:BoundField HeaderText="Grade" DataField="Grade" />
                                                    <asp:BoundField HeaderText="Branch" DataField="Branch" />
                                                    <asp:BoundField HeaderText="Division" DataField="Division" />
                                                    <asp:BoundField HeaderText="Date of Joining" DataField="Date_of_Joining" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Bank Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_BankInfo" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee_ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Bank Name" DataField="Bank_Name" />
                                                    <asp:BoundField HeaderText="Account Number" DataField="Account_Number" />
                                                    <asp:BoundField HeaderText="IFSC Code" DataField="IFSC_Code" />
                                                    <asp:BoundField HeaderText="Bank Branch" DataField="Bank_Branch" />
                                                    <asp:BoundField HeaderText="Bank District" DataField="Bank_District" />
                                                    <asp:BoundField HeaderText="Branch Country" DataField="Branch_Country" />
                                                    <asp:BoundField HeaderText="Branch State" DataField="Branch_State" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Address Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_AddressInfo" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee_ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Address Type" DataField="Address_Type" />
                                                    <asp:BoundField HeaderText="Valid From" DataField="Start_Date" />
                                                    <asp:BoundField HeaderText="To Date" DataField="End_Date" />
                                                    <asp:BoundField HeaderText="Residence Number" DataField="Residence_Number" />
                                                    <asp:BoundField HeaderText="Street" DataField="Street" />
                                                    <asp:BoundField HeaderText="Locality" DataField="Locality" />
                                                    <asp:BoundField HeaderText="District" DataField="District" />
                                                    <asp:BoundField HeaderText="Country" DataField="Country" />
                                                    <asp:BoundField HeaderText="State" DataField="State" />
                                                    <asp:BoundField HeaderText="Pincode" DataField="Pincode" />
                                                    <asp:BoundField HeaderText="STD_Code" DataField="STD_Code" />
                                                    <asp:BoundField HeaderText="Ward_Number" DataField="Ward_Number" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Contact Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_ContInfo" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Contact_Type" DataField="Contact_Type" />
                                                    <asp:BoundField HeaderText="Contact_Type_ID" DataField="Contact_Type_ID" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <h4>Document Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_DocInfo" runat="server" AutoGenerateColumns="false" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="Document_Type" DataField="Document_Type" />
                                                    <asp:BoundField HeaderText="Document_Type_ID" DataField="Document_Type_ID" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <h4>Benefits Information</h4>
                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_Benefits" runat="server" AutoGenerateColumns="false" CssClass="Grid respovrflw" GridLines="Both" PagerStyle-CssClass="cssPager" PageSize="5" Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID" />
                                                    <asp:BoundField HeaderText="ESI_Applicable" DataField="ESI_Applicable" />
                                                    <asp:BoundField HeaderText="ESI_Number" DataField="ESI_Number" />
                                                    <asp:BoundField HeaderText="ESI_Dispencary" DataField="ESI_Dispencary" />
                                                    <asp:BoundField HeaderText="PF_Applicable" DataField="PF_Applicable" />
                                                    <asp:BoundField HeaderText="PF_Number" DataField="PF_Number" />
                                                    <asp:BoundField HeaderText="PF_Number_Dept_File" DataField="PF_Number_Dept_File" />
                                                    <asp:BoundField HeaderText="Restrict_PF" DataField="Restrict_PF" />
                                                    <asp:BoundField HeaderText="Zero_Pension" DataField="Zero_Pension" />
                                                    <asp:BoundField HeaderText="Zero_PT" DataField="Zero_PT" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                        </div>

                        <div id="view2" runat="server" visible="false">
                            <br />
                            <div style="width: 99%; margin: 0 auto; padding: 5px 0 40px 0;">
                                <%-- <ul class="nav nav-pills navtab-bg" >

                           <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Infotab1" class="nav-link p-2" OnClick="Infotab1_Click" CausesValidation="false"><i class="fe-book" ></i>
                            Personal Info</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Infotab2" class="nav-link  p-2" OnClick="Infotab2_Click" CausesValidation="false"><i class="fe-credit-card"></i>
                            Bank Info</asp:LinkButton></li>  
                                       <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Infotab3" class="nav-link  p-2" OnClick="Infotab3_Click" CausesValidation="false"><i class="fe-home"></i>
                            Address Info</asp:LinkButton></li>  
                                       <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Infotab4" class="nav-link  p-2" OnClick="Infotab4_Click" CausesValidation="false"><i class="fe-layers"></i>
                            Benefits Info</asp:LinkButton></li>  
                                       <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Infotab5" class="nav-link  p-2" OnClick="Infotab5_Click" CausesValidation="false"><i class="fe-phone"></i>
                            Communication Info</asp:LinkButton></li> 
                                      <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Infotab6" class="nav-link  p-2" OnClick="Infotab6_Click" CausesValidation="false"><i class="fe-folder-plus"></i>
                            Document Info</asp:LinkButton></li>                           
                   </ul>
                    <div class="tabcontents">
                        <div id="info1" runat="server" visible="false"  style="width:100%">
                            <br />--%>
                                <div class="row">
                                <div class="col-md-2"> <div class="header-title">&nbsp;&nbsp;Personal Information</div></div>
                                  <div class="col-md-10"><div  style="margin-left:720px"><asp:Label ID="lblcompCode" runat="server" CssClass="ccode"></asp:Label><span style="color:red;">*</span>
                                   <asp:TextBox ID="txtEmpID" runat="server" AutoComplete="off" CssClass="txtDropDownwidth" Placeholder="Employee ID" TabIndex="1" MaxLength="15"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="REQ_txtEmpID" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info1" ControlToValidate="txtEmpID" ForeColor="Red" ErrorMessage="*">
                                     </asp:RequiredFieldValidator>
                                     </div>
                                                    </div>
                                                </div>
                               
                                 <hr class="HrCls"/>
                                    <br />
                                        <div class="form-group">
                                            <div class="">
                                                <asp:Label ID="lblmssgpi" runat="server" Text="" ForeColor="Red"></asp:Label>                                                
                                            </div>
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:red">*</span>Name :</div>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList ID="DDL_Salutation" CssClass="txtDropDownwidth" runat="server" TabIndex="2"></asp:DropDownList>
                                                            &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtFName" AutoComplete="off" placeholder="First Name" CssClass="txtDropDownwidth" runat="server" TabIndex="3"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="REQ_txtFName" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info1" ControlToValidate="txtFName" ForeColor="Red" ErrorMessage="*">
                                                        </asp:RequiredFieldValidator>&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtMName" AutoComplete="off" placeholder="Middle Name" CssClass="txtDropDownwidth" runat="server" TabIndex="4"></asp:TextBox>
                                                        &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtLName" AutoComplete="off" placeholder="Last Name" CssClass="txtDropDownwidth" runat="server" TabIndex="5"></asp:TextBox>
                                                        
                                                        <Ajx:FilteredTextBoxExtender ID="FLTR_Noleave" runat="server" FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtFName"></Ajx:FilteredTextBoxExtender>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtMName"></Ajx:FilteredTextBoxExtender>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="UppercaseLetters,LowercaseLetters" TargetControlID="txtLName"></Ajx:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>Gender :</div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="DDL_Gender" CssClass="txtDropDownwidth" runat="server" TabIndex="6">
                                                            <asp:ListItem Value="1" Selected="True">Male</asp:ListItem>
                                                            <asp:ListItem Value="2">Female</asp:ListItem>
                                                            <asp:ListItem Value="3">Others</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    </div>
                                            <div class="row">
                                                    <div class="col-md-2"><span style="color:red">*</span>Birth Date :</div>
                                                    <div class="col-md-10">
                                                        <asp:TextBox ID="txtDob" AutoComplete="off" CssClass="txtDropDownwidth" runat="server" Placeholder="Select Date" TabIndex="7"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RFVtxtDob" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info1" ControlToValidate="txtFName" ForeColor="Red" ErrorMessage="*">
                                                        </asp:RequiredFieldValidator>
                                                        <Ajx:CalendarExtender ID="CE_TxtFromDate" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                            TargetControlID="txtDob" PopupButtonID="txtDob">
                                                        </Ajx:CalendarExtender>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" FilterType="Numbers,Custom" FilterMode="ValidChars" ValidChars="-" TargetControlID="txtDob"></Ajx:FilteredTextBoxExtender>
                                                        
                                                    </div>
                                                </div>

                                            <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>Parents Name :</div>
                                                    <div class="col-md-10">

                                                        <asp:TextBox ID="txtFatherName" AutoComplete="off" placeholder="Father's Name" CssClass="txtDropDownwidth" runat="server" TabIndex="8"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters" ValidChars=". " FilterMode="ValidChars" TargetControlID="txtFatherName"></Ajx:FilteredTextBoxExtender>
                                                         &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtMotherName" AutoComplete="off" CssClass="txtDropDownwidth" Placeholder="Mother's Name" runat="server" TabIndex="9"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters" ValidChars=". " FilterMode="ValidChars" TargetControlID="txtMotherName"></Ajx:FilteredTextBoxExtender>
                                                   
                                                        
                                                        
                                                    </div>
                                                    
                                                </div>
                                           
                                            
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>Marital Status :</div>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList ID="ddl_materlSatuts" CssClass="txtDropDownwidth" runat="server" TabIndex="10" AutoPostBack="true" OnSelectedIndexChanged="ddl_materlSatuts_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtSpouseName" AutoComplete="off" CssClass="txtDropDownwidth" runat="server" Visible="false" placeholder="Spouse Name" TabIndex="11"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters" ValidChars=". " FilterMode="ValidChars" TargetControlID="txtSpouseName"></Ajx:FilteredTextBoxExtender>
                                                    </div>

                                                   
                                                </div>
                                   
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>Department :</div>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList ID="ddleDept" runat="server" CssClass="txtDropDownwidth" TabIndex="12"></asp:DropDownList>
                                                        &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtaddpt" AutoComplete="off" CssClass="txtDropDownwidth" runat="server" Placeholder="Add New Department"></asp:TextBox>
                                                          <asp:RequiredFieldValidator ID="REQ_addept" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="dept" ControlToValidate="txtaddpt" ForeColor="Red" ErrorMessage="*">
                                                        </asp:RequiredFieldValidator>
                                                          &nbsp;&nbsp;
                                                          <asp:Button ID="btnadddept" Text="Add" ValidationGroup="dept" runat="server" OnClick="btnadddept_Click" />
                                                    </div>
                                                    </div>
                                             <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>Grade :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txteGrade" AutoComplete="off" CssClass="txtDropDownwidth" runat="server" Placeholder="Grade" TabIndex="13"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>Branch :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txteBranch" AutoComplete="off" CssClass="txtDropDownwidth" runat="server" TabIndex="14" Placeholder="Branch"></asp:TextBox>
                                                    </div>
                                                    </div>
                                            <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>Division :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtedivision" AutoComplete="off" CssClass="txtDropDownwidth" runat="server" TabIndex="15" Placeholder="Division"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:red">*</span>Date of Joining :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txteDOJ" AutoComplete="off" CssClass="txtDropDownwidth" runat="server" TabIndex="16" Placeholder="Select Date"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info1" ControlToValidate="txteDOJ" ForeColor="Red" ErrorMessage="*">
                                                        </asp:RequiredFieldValidator>
                                                        <Ajx:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                                            TargetControlID="txteDOJ" PopupButtonID="txteDOJ">
                                                        </Ajx:CalendarExtender>
                                                        <Ajx:FilteredTextBoxExtender ID="FLTR_txtDOJ" runat="server" FilterType="Custom,Numbers" TargetControlID="txteDOJ" ValidChars=" -"></Ajx:FilteredTextBoxExtender>
                                                    </div>

                                                </div>
                                           
                                        </div>
                                        <div class="row">
                                          <div class="col-md-6">
                                        <asp:Button ID="btnSubmit" Text="Submit" ValidationGroup="info1" Width="60px" runat="server" OnClick="btnSubmit_Click" TabIndex="17"/>
                                        <asp:Button ID="btnClearAll" Text="Clear" runat="server" Width="60px" OnClick="btnClearAll_Click" CausesValidation="false" />
                                              </div>
                                            </div>
                                   <%-- </div>

                                    <div id="info2" runat="server" visible="false"  style="width:100%"> --%>
                                     <br />
                                <br />
                              <div class="header-title">&nbsp;&nbsp;Bank Information</div>
                                 <hr class="HrCls"/>
                                    <br />
                                        <div class="form-group">                                            
                                                <asp:Label ID="lblbif" runat="server" Text="" ForeColor="Red"></asp:Label>                                               
                                           
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:red">*</span>Account Number :</div>
                                                    <div class="col-md-10">
                                                       <asp:TextBox ID="txtAccNum" CssClass="txtDropDownwidth" AutoComplete="off" runat="server" TabIndex="18" Placeholder="Account Number"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers" TargetControlID="txtAccNum"></Ajx:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info2" ControlToValidate="txtAccNum" ForeColor="Red" ErrorMessage="*">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    </div>
                                            
                                                    <div class="row">
                                                    <div class="col-md-2"><span style="color:red">*</span>Bank Name :</div>
                                                    <div class="col-md-10">
                                                        <asp:TextBox ID="txtbankName" CssClass="txtDropDownwidth" runat="server" TabIndex="19" Placeholder="Bank Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info2" ControlToValidate="txtbankName" ForeColor="Red" ErrorMessage="*">
                                                        </asp:RequiredFieldValidator>                                                
                                                        
                                                    </div>
                                                </div>
                                            
                                            
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:red">*</span>IFSC Code :</div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtIFSC" CssClass="txtDropDownwidth" AutoComplete="off" runat="server" TabIndex="20" Placeholder="IFSC Code"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info2" ControlToValidate="txtIFSC" ForeColor="Red" ErrorMessage="*">
                                                            <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Numbers" TargetControlID="txtIFSC"></Ajx:FilteredTextBoxExtender>
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    </div>

                                            <div class="row">
                                                    <div class="col-md-2"><span style="color:red">*</span>Bank Address :</div>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList ID="ddlBnkContry" CssClass="txtDropDownwidth" runat="server" TabIndex="21" OnSelectedIndexChanged="ddlBnkContry_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                   <asp:RequiredFieldValidator ID="RQF_bnkcntry" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info2" ControlToValidate="ddlBnkContry" InitialValue="0" ForeColor="Red" ErrorMessage="*">
                                                    </asp:RequiredFieldValidator>
                                                        &nbsp;&nbsp;
                                                        <asp:DropDownList ID="ddl_Bnkstate" CssClass="txtDropDownwidth" runat="server" TabIndex="22">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RFV_bnkstate" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info2" ControlToValidate="ddl_Bnkstate" InitialValue="0" ForeColor="Red" ErrorMessage="*">
                                                    </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>


                                            <div class="row">
                                              <div class="col-md-2"></div>
                                                <div class="col-md-10">
                                                  <asp:TextBox ID="txtBranch" AutoComplete="off" CssClass="txtDropDownwidth" runat="server" Placeholder="Bank Branch" TabIndex="23"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info2" ControlToValidate="txtBranch" ForeColor="Red" ErrorMessage="*">
                                                    </asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;
                                                     <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom"  ValidChars="- ," TargetControlID="txtBranch"></Ajx:FilteredTextBoxExtender>
                                                     <asp:TextBox ID="txtDistrict"   AutoComplete="off"  CssClass="txtDropDownwidth" runat="server" Placeholder="Bank District" TabIndex="24"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters" FilterMode="ValidChars" ValidChars=". " TargetControlID="txtDistrict"></Ajx:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                           
                                            
                                                
                                            <div class="row">
                                              <div class="col-md-6">
                                        <asp:Button ID="btnBankinfosubmit" Text="Submit" ValidationGroup="info2" Width="60px" runat="server" OnClick="btnBankinfosubmit_Click" TabIndex="25"/>
                                        <asp:Button ID="btninfo2" Text="Clear" runat="server" OnClick="btnClearAll_Click" Width="60px" CausesValidation="false" />
                                                  </div>
                                                </div>
                                            </div>
                                            <br />
                                            
                                    <%--</div>

                                    <div id="info3" runat="server" visible="false"  style="width:100%">--%>
                                         <div class="header-title">&nbsp;&nbsp;Address Information</div>
                                 <hr class="HrCls"/>
                                    <br />
                                        <div class="form-group">
                                                <asp:Label ID="lblainfo" runat="server" Text="" ForeColor="Red"></asp:Label>
                                               
                                           
                                           
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:red">*</span>Address Type :</div>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList ID="ddlAddType" CssClass="txtDropDownwidth" runat="server" TabIndex="26">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RQF_addtyp" runat="server" ControlToValidate="ddlAddType" ErrorMessage="*" ForeColor="Red" InitialValue="0" SetFocusOnError="true" Display="Dynamic" ValidationGroup="info3"></asp:RequiredFieldValidator>
                                                   
                                                    </div>
                                                </div>
                                           
                                            
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:red">*</span>Address Details :</div>
                                                    <div class="col-md-10">
                                                        <asp:TextBox ID="txtAddline1" MaxLength="190" AutoComplete="off" TextMode="MultiLine" CssClass="txtDropDownwidth" runat="server" placeholder="Address Line1" TabIndex="27"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom,Numbers" ValidChars=" #.,/" TargetControlID="txtAddline1"></Ajx:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info3" ControlToValidate="txtAddline1" ForeColor="Red" ErrorMessage="*">
                                                        </asp:RequiredFieldValidator>
                                                   &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtAddline2" MaxLength="190"  AutoComplete="off" TextMode="MultiLine" CssClass="txtDropDownwidth" runat="server" placeholder="Address Line2" TabIndex="28"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="UppercaseLetters,LowercaseLetters,Custom,Numbers" ValidChars=" #.,/" TargetControlID="txtAddline2"></Ajx:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                           
                                             <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-10">
                                                        <asp:TextBox ID="txtAddDistrict" CssClass="txtDropDownwidth"  AutoComplete="off"  runat="server" Placeholder="District" TabIndex="29"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters" FilterMode="ValidChars" ValidChars=".,/ " TargetControlID="txtAddDistrict"></Ajx:FilteredTextBoxExtender>
                                                    &nbsp;&nbsp;
                                                         <asp:TextBox ID="txtLocality" CssClass="txtDropDownwidth" AutoComplete="off"  runat="server" Placeholder="Locality" TabIndex="30"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom,UppercaseLetters,LowercaseLetters" FilterMode="ValidChars" ValidChars=". " TargetControlID="txtLocality"></Ajx:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="info3" ControlToValidate="txtLocality" ForeColor="Red" ErrorMessage="*">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>   
                                            
                                                <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-10">
                                                        <asp:DropDownList ID="ddlAddCountry" CssClass="txtDropDownwidth" runat="server" OnSelectedIndexChanged="ddlAddCountry_SelectedIndexChanged" TabIndex="31" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RQF_addcnty" ValidationGroup="info3" runat="server" ControlToValidate="ddlAddCountry" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                   &nbsp;&nbsp;
                                                        <asp:DropDownList ID="ddlAddState" CssClass="txtDropDownwidth" runat="server" TabIndex="32">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RQF_addstate" runat="server" ValidationGroup="info3" ControlToValidate="ddlAddState" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                           
                                            
                                                <div class="row">
                                                    <div class="col-md-2"></div>
                                                    <div class="col-md-10">
                                                    <asp:TextBox ID="txtWardNum" CssClass="txtDropDownwidth" runat="server" Placeholder="Ward Number" TabIndex="33"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="Numbers" ValidChars=" #.,/" TargetControlID="txtWardNum"></Ajx:FilteredTextBoxExtender>
                                                   &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtAddStd" CssClass="txtDropDownwidth" runat="server" Placeholder="STD Code" TabIndex="34"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Numbers" ValidChars=" #.,/" TargetControlID="txtAddStd"></Ajx:FilteredTextBoxExtender>
                                                        &nbsp;&nbsp;
                                                   <asp:TextBox ID="txtAddPincode" CssClass="txtDropDownwidth" runat="server" Placeholder="Pin Code" TabIndex="35"></asp:TextBox>
                                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Numbers" ValidChars=" #.,/" TargetControlID="txtAddPincode"></Ajx:FilteredTextBoxExtender>
                                                 
                                                </div>
                                                    </div>


                                             <div class="row">
                                                    <div class="col-md-2"><span style="color:red">*</span>Residence Period :</div>
                                                    <div class="col-md-10">
                                                        <asp:TextBox ID="txtaddstdate" runat="server" CssClass="txtDropDownwidth" AutoComplete="off" Placeholder="Select Date" TabIndex="36"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="REQ_addsrtdate" runat="server" ValidationGroup="info3" ControlToValidate="txtaddstdate" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                        <Ajx:CalendarExtender ID="CE_addsrtdate" runat="server" Format="yyyy-MM-dd" PopupButtonID="txtaddstdate" TargetControlID="txtaddstdate" />
                                                       <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" FilterType="Custom,Numbers" TargetControlID="txtaddstdate" ValidChars=" -"></Ajx:FilteredTextBoxExtender>
                                                       &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtaddenddate" runat="server" CssClass="txtDropDownwidth" AutoComplete="off" Placeholder="Select Date" TabIndex="37"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="REQ_addenddate" runat="server" ValidationGroup="info3" ControlToValidate="txtaddenddate" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator>
                                                        <Ajx:CalendarExtender ID="CE_addenddate" runat="server" Format="yyyy-MM-dd" PopupButtonID="txtaddenddate" TargetControlID="txtaddenddate" />
                                                       <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" FilterType="Custom,Numbers" TargetControlID="txtaddenddate" ValidChars=" -"></Ajx:FilteredTextBoxExtender>
                                                        &nbsp;&nbsp;
                                                        <asp:CompareValidator ID="CFV_wbsenddate" ValidationGroup="Date" Font-Size="Medium" ForeColor="Red" runat="server" ControlToValidate="txtaddstdate" ControlToCompare="txtaddenddate" Operator="LessThan" Type="Date" ErrorMessage="From date must be less than To date." Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>
                                                    </div>
                                                </div>

                                                      </div>
                                        <div class="row">
                                         <div class="col-md-6">
                                        <asp:Button ID="btnAddressinfoSubmit" Text="Submit" ValidationGroup="info3" Width="60px" runat="server" OnClick="btnAddressinfoSubmit_Click" TabIndex="38"/>
                                        <asp:Button ID="btninfo3" Text="Clear" runat="server" OnClick="btnClearAll_Click" Width="60px" CausesValidation="false" />
                                             </div>
                                            </div>
                                            </div>
                                   <%-- </div>
                                       
                                   
                                    <div id="info4" runat="server" visible="false"  style="width:100%">--%>
                                        <div class="header-title">&nbsp;&nbsp;Benefits Information</div>
                                 <hr class="HrCls"/>
                                    <br />
                                        <div class="form-group">
                                                <asp:Label ID="lblbinfo" runat="server" Text="" ForeColor="Red"></asp:Label>
                                               
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>ESI Applicable :</div>
                                                    <div class="col-md-2">
                                                        <asp:RadioButtonList ID="rbtnESI" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" TabIndex="39" OnSelectedIndexChanged="rbtnESI_SelectedIndexChanged">
                                                            <asp:ListItem Value="Yes" Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem Value="No">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-md-8" id="esidv" runat="server">
                                                        <asp:TextBox ID="txtESINum" AutoComplete="off" CssClass="txtDropDownwidth" runat="server" Placeholder="ESI Number" TabIndex="40"></asp:TextBox>
                                                          <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" FilterType="Numbers" ValidChars=" #.,/" TargetControlID="txtESINum"></Ajx:FilteredTextBoxExtender>
                                                        &nbsp;&nbsp;
                                                      <asp:TextBox ID="txtESIDisp" CssClass="txtDropDownwidth" runat="server" Placeholder="ESI Dispensary"></asp:TextBox>
                                                   
                                                </div>
                                            </div>

                                            
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>PF Applicable :</div>
                                                    <div class="col-md-2">
                                                        <asp:RadioButtonList ID="rbnPFApp" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" TabIndex="41" OnSelectedIndexChanged="rbnPFApp_SelectedIndexChanged">
                                                            <asp:ListItem Value="Yes" Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem Value="No">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        </div>
                                                   <div class="col-md-8" id="pfdv" runat="server">
                                                        <asp:TextBox ID="txtPFNumer" CssClass="txtDropDownwidth" runat="server" Placeholder="PF Number" TabIndex="42"></asp:TextBox>
                                                          <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" FilterType="Numbers" ValidChars=" #.,/" TargetControlID="txtPFNumer"></Ajx:FilteredTextBoxExtender>
                                                       &nbsp;&nbsp;
                                                        <asp:CheckBox ID="chkrespf" Text="&nbsp;&nbsp;Restrict PF" runat="server" TabIndex="43"/>                                                      
                                                    </div>
                                                        
                                                </div>
                                           
                                           
                                           
                                                <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>Zero Pension :</div>
                                                    <div class="col-md-10">
                                                        <asp:RadioButtonList ID="rbtnZeroPens" runat="server" RepeatDirection="Horizontal" TabIndex="44">
                                                            <asp:ListItem Value="Yes" Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem Value="No">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        </div>
                                                    </div>

                                                   <div class="row">
                                                    <div class="col-md-2"><span style="color:white">*</span>Zero PT :</div>
                                                    <div class="col-md-10">
                                                        <asp:RadioButtonList ID="rbtnZeroPT" runat="server" RepeatDirection="Horizontal" TabIndex="45">
                                                            <asp:ListItem Value="Yes" Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem Value="No">No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            
                                        <div class="row">
                                          <div class="col-md-2">
                                        <asp:Button ID="btnBeniftsinfosubmit" Text="Submit" ValidationGroup="info4" runat="server" Width="60px" OnClick="btnBeniftsinfosubmit_Click" TabIndex="46"/>
                                        <asp:Button ID="btninfo4" Text="Clear" runat="server" OnClick="btnClearAll_Click" Width="60px" CausesValidation="false" />
                                              </div>
                                            </div>
                                            </div>
                            <br />
                                   <%-- </div>

                                    <div id="info5" runat="server" visible="false"  style="width:100%">--%>
                                         <div class="header-title">&nbsp;&nbsp;Communication Information</div>
                                 <hr class="HrCls"/>
                                    <br />
                                        <div class="form-group">
                                               
                                                <div class="row">
                                                     <div class="col-sm-2">Contact Details:</div>
                                                     <div class="col-md-10">
                                                        <asp:DropDownList ID="ddlContType" runat="server" CssClass="txtDropDownwidth" TabIndex="47"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RQF_cncttype" runat="server" ControlToValidate="ddlContType" InitialValue="0" ErrorMessage="*" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ValidationGroup="addcntct"></asp:RequiredFieldValidator>
                                                   &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtCoTypeID" AutoComplete="off" CssClass="txtDropDownwidth" runat="server" Placeholder="Contact Type ID" TabIndex="48"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RQF_cntctdescp" runat="server" ControlToValidate="txtCoTypeID" Display="Dynamic" ForeColor="Red" ErrorMessage="*" SetFocusOnError="true" ValidationGroup="addcntct"></asp:RequiredFieldValidator>
                                                       
                                                  &nbsp;&nbsp;
                                                         <asp:Button ID="btnCumminfosubmit" Text="Submit"  ValidationGroup="addcntct" TabIndex="49" runat="server" Width="60px" CausesValidation="false" OnClick="btnCumminfosubmit_Click"/>
                                        <asp:Button ID="btninfo5" Text="Clear" runat="server" OnClick="btnClearAll_Click" Width="60px" CausesValidation="false" />
                                             
                                                    </div>
                                                    </div>
                                            </div>
                                               <div id="contact" runat="server" visible="false">
                                            <div>
                                                    <asp:GridView ID="gv_tempConttypes" runat="server" Visible="false" AutoGenerateColumns="false" AllowPaging="True" PageSize="10" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID" />
                                                            <asp:BoundField HeaderText="Contact_Type" DataField="Contact_Type" />
                                                            <asp:BoundField HeaderText="Contact_Type_ID" DataField="Contact_Type_ID" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                           <asp:Button ID="btnContAdd" runat="server" Text="Add" OnClick="btnContAdd_Click" Visible="false"/></div>
                                                      
                            <br />
                                    <%--</div>ddlContType.SelectedItem

                                    <div id="info6" runat="server" visible="false"  style="width:100%">--%>
                                        <div class="header-title">&nbsp;&nbsp;Document Information</div>
                                 <hr class="HrCls"/>
                                    <br />
                                        <div class="form-group">
                                            <asp:Label ID="lbldocupmssg" runat="server"></asp:Label>

                                                <div class="row">
                                                    <div class="col-sm-2"><span style="color:red">*</span>Document Details :</div>
                                                    <div class="col-sm-10">
                                                        <asp:DropDownList ID="ddlDocType" runat="server" CssClass="txtDropDownwidth" TabIndex="50" OnSelectedIndexChanged="ddlDocType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RQF_doctyps" runat="server" ControlToValidate="ddlDocType" SetFocusOnError="true" InitialValue="0" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="info6"></asp:RequiredFieldValidator>
                                                    &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtDocTypeID" CssClass="txtDropDownwidth" runat="server" AutoComplete="off" TabIndex="51" Placeholder="Document Type ID"></asp:TextBox>
                                                        <asp:RequiredFieldValidator runat="server" ID="RQF_docdescrpn" ErrorMessage="*"  Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDocTypeID" ForeColor="Red" ValidationGroup="info6"></asp:RequiredFieldValidator>
                                                        &nbsp;&nbsp;
                                                                                                                
                                                         <asp:FileUpload ID="file_docupload" runat="server" AllowMultiple="false"/> 
                                                           
                                                        <asp:Button ID="btnDocuntinfosubmit" Text="Submit" OnClientClick = "return ValidateFile()" ValidationGroup="info6" Width="60px" TabIndex="52" runat="server" OnClick="btnDocuntinfosubmit_Click" />
                                                        <asp:Button ID="btninfo6" Text="Clear" runat="server" OnClick="btnClearAll_Click" Width="60px" CausesValidation="false" />
                                                        
                                                   </div>
                                                    </div>
                                            <asp:UpdatePanel ID="docup" runat="server">
                                                             <ContentTemplate>                                                            
                                                                  </ContentTemplate>
                                                             <Triggers>
                                                                 <asp:PostBackTrigger ControlID="btnDocuntinfosubmit"/>
                                                             </Triggers>
                                                         </asp:UpdatePanel>
                                            </div>
                                                    <div id="divdocs" runat="server" visible="false">
                                                    <asp:GridView ID="gv_tempDocTypes" runat="server" AutoGenerateColumns="false" Visible="false" AllowPaging="True" PageSize="10" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID" />
                                                            <asp:BoundField HeaderText="Document_Type" DataField="Document_Type" />
                                                            <asp:BoundField HeaderText="Document_Type_ID" DataField="Document_Type_ID" />
                                                        </Columns>
                                                    </asp:GridView>
                                                        <asp:Button ID="btnDocType" runat="server" Text="Add" OnClick="btnDocType_Click" Visible="false" />
                                                </div>
                                           
                                    </div>
                        
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
        </div>

</asp:Content>
