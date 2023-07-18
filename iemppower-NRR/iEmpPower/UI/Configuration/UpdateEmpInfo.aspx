<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.Master" AutoEventWireup="true" CodeBehind="UpdateEmpInfo.aspx.cs"  MaintainScrollPositionOnPostback="true"
    Inherits="iEmpPower.UI.Configuration.UpdateEmpInfo" 
    UICulture="auto" Theme="SkinFile" EnableEventValidation="false"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>    
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <%--<script type ="text/javascript">

        var validFilesTypes = ["pdf"];

        function ValidateFile() {

            var file = document.getElementById("<%=file_updtdoccopy.ClientID%>");

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

                label.innerHTML = "Invalid File. Please upload a file with pdf format";

            }

            return isValidFile;

        }

</script>--%>

    <style>
         .gridviewNew td a {
            font-size: 16px;
            font-weight: 600;
        }

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

    </style>

    <div class="row">
            <div class="col-12">
                <div class="page-title-box">
                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Update/View Employee's Info</li>
                        </ol>
                    </div>
                    <h4 class="page-title">Update/View Employee's Info
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
                        <%--<ul class="nav nav-pills navtab-bg" >
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab1" class="nav-link p-2" OnClick="Tab1_Click" CausesValidation="false"><i class="fe-check-square" ></i>
                            Update Employee's Info</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab2" class="nav-link  p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-eye"></i>
                            View Employee's Info</asp:LinkButton></li>                           
                   </ul>--%>
                    <div class="tabcontents">
                        <div id="view1" runat="server" visible="false"  style="width:100%">
                            <br />
                          <div class="header-title">&nbsp;&nbsp;Update Employee Department Details</div>
                                 <hr class="HrCls"/>
                            <br />
                                <div class="row">
                                    <div class="col-md-4">
                                    <asp:TextBox ID="txtEmpID" runat="server" placeHolder="Enter Emp ID" CssClass="txtDropDownwidth"></asp:TextBox>
                                    <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" />
                                    <asp:Button ID="btnSearchClear" runat="server" Text="Clear" OnClick="btncancel_Click" />
                                </div>
                                     </div>
                                <br />
                                    <br />
                                <asp:GridView ID="gv_dept" runat="server" AutoGenerateColumns="false"
                                    PageSize="10" Width="100%" OnRowCommand="gv_dept_RowCommand" AllowPaging="true" 
                                    DataKeyNames="Employee_ID,FullName,EDEPT,EGRADE,EBRANCH,EDIVISION,EDOJ,depitid" OnPageIndexChanging="gv_dept_PageIndexChanging" OnRowDataBound="gv_dept_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Employee ID" DataField="" />
                                        <asp:BoundField HeaderText="Employee Name" DataField="FullName" />
                                        <asp:BoundField HeaderText="Department" DataField="EDEPT" />
                                        <asp:BoundField HeaderText="Grade" DataField="EGRADE" />
                                        <asp:BoundField HeaderText="Branch" DataField="EBRANCH" />
                                        <asp:BoundField HeaderText="Division" DataField="EDIVISION" />
                                        <asp:BoundField HeaderText="Date of Joining" DataField="EDOJ" />
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkGenertelogin" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false"
                                                     CommandName="View" CssClass="fe-edit-2"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server" Text='<%#Eval("Employee_ID") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                           

                            <asp:Panel ID="pnlCntrls" runat="server" Visible="false">
                                <hr />
                                <h5>Update Joining Process</h5>
                                <div class="form-group">
                                    <div class="">
                                        <asp:Label ID="lblEmpID" runat="server"></asp:Label>
                                        <div class="row">
                                            <div class="col-md-2">Department :</div>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddleDept" runat="server" CssClass="txtDropDownwidth"></asp:DropDownList>
                                            </div>
                                            <div class="col-md-2">Grade :</div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txteGrade" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">Branch :</div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txteBranch" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2">Division :</div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtedivision" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-2">Date of Joining :</div>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txteDOJ" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="Csave" ControlToValidate="txteDOJ" ForeColor="Red" ErrorMessage="*">
                                                </asp:RequiredFieldValidator>
                                                <Ajx:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MM-yyyy"
                                                    TargetControlID="txteDOJ" PopupButtonID="txteDOJ">
                                                </Ajx:CalendarExtender>
                                            </div>

                                        </div>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" alidationGroup="Csave" OnClick="btnUpdate_Click" />
                                        <asp:Button ID="btncancel" CausesValidation="false" runat="server" Text="Cancel" OnClick="btncancel_Click" />
                                    </div>
                                </div>
                            </asp:Panel>
                            </div>
                            </div>
                            </div>


                        <div id="view2" runat="server" style="width:100%">
                            <br />
                          <div class="header-title">&nbsp;&nbsp;View Employees Details</div>
                                 <hr class="HrCls"/>
                            <br />
                            <div class="row" id="srch" runat="server" visible="false">
                                <div class="col-sm-8">                                   
                                    <asp:DropDownList ID="DDL_srchvalupdt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_srchvalupdt_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:Button ID="btn_restupdt" runat="server" Visible="false" Text="Reset" OnClick="btn_restupdt_Click"/>
                                </div>
                                 <div class="col-md-4 text-right" id="divcnt" runat="server"></div>
                            </div>
                                   <br />
                                        <div>                                        
                                            <asp:GridView ID="GV_viewemp_details" runat="server" AutoGenerateColumns="false" 
                                               PageSize="10" OnRowDataBound="GV_viewemp_details_RowDataBound" OnRowCommand="GV_viewemp_details_RowCommand" AllowPaging="true"
                                               OnPageIndexChanging="GV_viewemp_details_PageIndexChanging" DataKeyNames="Employee_ID,EDIVISION,_2,_1">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl NO.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                                    <asp:BoundField HeaderText="Employee ID" DataField=""/>
                                                    <asp:BoundField HeaderText="Employee Name" DataField="EDIVISION"/>
                                                    <asp:TemplateField HeaderText="Joining Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljod" runat="server" Text='<%# Eval("_2")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Separation Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_exitdt" runat="server" Text='<%# (Eval("_1")).ToString() == "01-Jan-1900" ? " " : Eval("_1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LK_Viewemp" ToolTip="View" runat="server" CommandName="VIEWEMP" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                                CausesValidation="false"  CssClass="fe-eye"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_viewempid" runat="server" Text='<%#Eval("Employee_ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                           
                                            <br />                                                                                       
                                            <asp:Panel ID="PNL_allemp_details" runat="server">
                                        
                                            <div id="divPI" runat="server" visible="false">
                                                <b>Employee's Personal Details</b>
                                            </div>
                                            <asp:GridView ID="GV_empPI" runat="server" AutoGenerateColumns="false" OnRowCommand="GV_empPI_RowCommand" OnRowDataBound="GV_empPI_RowDataBound"
                                             DataKeyNames="ID,EMPID,desig,WK,DDLTYPETEXT,Created_By" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                    <%--<asp:TemplateField HeaderText="Photo" meta:resourcekey="TemplateFieldResource1">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("EMPLOYEE_PHOTO_PATH") %>' alt="Image"
                                                        Height="35px" Width="35px" class="rounded-circle setdatainput" />
                                                    </ItemTemplate>
                                                    <ItemStyle Wrap="False" />
                                                    </asp:TemplateField>--%>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="EMPID"/>
                                                    <asp:BoundField HeaderText="Employee Name" DataField="NAME"/>
                                                     <asp:TemplateField HeaderText="Employee DOB">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_pidob" runat="server" Text='<%# Eval("dt1", "{0:dd/MM/yyyy}") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_pidob" runat="server" Text='<%# Eval("dt1","{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                                           <Ajx:CalendarExtender ID="CE_pidob" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_pidob" PopupButtonID="txt_pidob">
                                                        </Ajx:CalendarExtender>
                                                            <Ajx:FilteredTextBoxExtender ID="FLT_pidob" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_pidob"></Ajx:FilteredTextBoxExtender>                                          
                                                        </ItemTemplate>
                                                         </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Father's Name">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_pifname" runat="server" Text='<%# Eval("IMAP_SERVER") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_pifname" runat="server" Text='<%# Eval("IMAP_SERVER") %>'></asp:TextBox>
                                                           <Ajx:FilteredTextBoxExtender ID="FLT_pifname" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ." FilterMode="ValidChars" TargetControlID="txt_pifname"></Ajx:FilteredTextBoxExtender>                                          
                                                           </ItemTemplate>
                                                         </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Mother's Name">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_pimname" runat="server" Text='<%# Eval("Company_Type_Txt") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_pipimname" runat="server" Text='<%# Eval("Company_Type_Txt") %>'></asp:TextBox>
                                                           <Ajx:FilteredTextBoxExtender ID="FLT_pipimname" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ." FilterMode="ValidChars" TargetControlID="txt_pipimname"></Ajx:FilteredTextBoxExtender>                                          
                                                           </ItemTemplate>
                                                         </asp:TemplateField>
                                                  
                                                    <asp:TemplateField HeaderText="Marital Status">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_pimsts" runat="server" Text='<%# Eval("deptid") %>'></asp:Label>

                                                           <asp:DropDownList ID="DDL_pists" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                         </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Spouse Name">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_pispname" runat="server" Text='<%# Eval("Company_Name") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_pispname" runat="server" Text='<%# Eval("Company_Name") %>'></asp:TextBox>
                                                           <Ajx:FilteredTextBoxExtender ID="FLT_pispname" runat="server" FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ." FilterMode="ValidChars" TargetControlID="txt_pispname"></Ajx:FilteredTextBoxExtender>                                          
                                                           </ItemTemplate>
                                                         </asp:TemplateField>

                                                    <asp:BoundField HeaderText="Created On" DataField="Created_On" DataFormatString="{0:dd/MM/yyyy}"/>
                                                     <asp:TemplateField HeaderText="Action">  
                                                     <ItemTemplate>
                                                     <asp:LinkButton ID="LK_PIedit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                      CommandName="editpi" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                                                    <asp:LinkButton ID="LK_piupdte" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    CommandName="updatepi"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                                    <asp:LinkButton ID="LK_picancel" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    CommandName="cancelpi" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                                     </ItemTemplate>
                                                     </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <br />

                                                <div id="divDI" runat="server" visible="false">
                                                <b>Employee's Department Info</b>
                                            </div>
                                            <asp:GridView ID="GV_empdept" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_empdept_RowDataBound" OnRowCommand="GV_empdept_RowCommand"
                                                Width="100%" DataKeyNames="ID,EMPID,desig" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                     <asp:BoundField HeaderText="Employee ID" DataField="EMPID"/>
                                                     <asp:BoundField HeaderText="Employee Name" DataField="NAME"/>
                                                     <asp:TemplateField HeaderText="Department">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_deptpi" runat="server" Text='<%# Eval("Company_Address") %>'></asp:Label>

                                                           <asp:DropDownList ID="DDL_deptpi" runat="server"></asp:DropDownList>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Grade">  
                                                       <ItemTemplate>
                                                            <asp:Label ID="lbl_grdepi" runat="server" Text='<%# Eval("AWART") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_grdepi" runat="server" Text='<%# Eval("AWART") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Branch">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_brnchpi" runat="server" Text='<%# Eval("ATEXT_STRING") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_bechpi" runat="server" Text='<%# Eval("ATEXT_STRING") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Division">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_dicsnpi" runat="server" Text='<%# Eval("ATEXT") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_dvsnpi" runat="server" Text='<%# Eval("ATEXT") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="DOJ">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_dojpi" runat="server" Text='<%# Eval("Date","{0:dd/MM/yyyy}") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_dojpi" runat="server" Text='<%# Eval("Date","{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                                            <Ajx:CalendarExtender ID="CE_dojpi" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_dojpi" PopupButtonID="txt_dojpi">
                                                            </Ajx:CalendarExtender>
                                                           <Ajx:FilteredTextBoxExtender ID="FLT_dojpi" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_dojpi"></Ajx:FilteredTextBoxExtender>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:BoundField HeaderText="Created On" DataField="Created_On" DataFormatString="{0:dd/MM/yyyy}"/>

                                                    <asp:TemplateField HeaderText="Action">  
                                                       <ItemTemplate>
                                                           <asp:LinkButton ID="LK_deptedt" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="editdpt" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="LK_deptupt" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="upddept"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="LK_deptcncl" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="cncldpt" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>
                                                    
                                                </Columns>
                                            </asp:GridView>
                                           <br />
                                            <br />

                                                <div id="divdesig" runat="server" visible="false">
                                                <b>Employee's Designation Info</b>
                                            </div>
                                                <asp:GridView ID="GV_empdesightry" runat="server" DataKeyNames="ID,CRYFWRD" OnRowDataBound="GV_empdesightry_RowDataBound">
                                                    <Columns>
                                                       <asp:BoundField DataField="EMPID" HeaderText="Employee ID" />
                                                        <asp:BoundField DataField="DESCRIPTION" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="desigTEXT" HeaderText="Designation" />
                                                        <asp:BoundField DataField="Date" HeaderText="Valid From" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                        <asp:BoundField DataField="enddate" HeaderText="Valid To" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                        <asp:BoundField DataField="Created_On" HeaderText="Updated On" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                    </Columns>
                                                </asp:GridView>
                                                 <br />
                                                <br />

                                                <div id="dvmgrlst" runat="server" visible="false">
                                                <b>Employee's Manager Info</b>
                                            </div>

                                                 <asp:GridView ID="GV_empmgr" runat="server" DataKeyNames="ID" OnRowDataBound="GV_empmgr_RowDataBound">
                                                    <Columns>
                                                        <asp:BoundField DataField="EMPID" HeaderText="Employee ID" />
                                                        <asp:BoundField DataField="DESCRIPTION" HeaderText="Employee Name" />
                                                        <asp:BoundField DataField="desigTEXT" HeaderText="Manager ID" />
                                                        <asp:BoundField DataField="deptid" HeaderText="Manager Name" />
                                                        <asp:BoundField DataField="Date" HeaderText="Valid From" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                        <asp:BoundField DataField="enddate" HeaderText="Valid To" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                         <asp:BoundField DataField="Created_On" HeaderText="Updated On" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <br />

                                                 <div id="divAI" runat="server" visible="false">
                                                <b>Employee's Address Info</b>
                                            </div>
                                            <asp:GridView ID="GV_empAI" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_empAI_RowDataBound" OnRowCommand="GV_empAI_RowCommand"
                                                 Width="100%" DataKeyNames="ID,EMPID,desig,AWART,ATEXT" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="EMPID"/>
                                                    <asp:BoundField HeaderText="Employee Name" DataField="NAME"/>
                                                    <asp:BoundField HeaderText="Address Type" DataField="DESCRIPTION"/>

                                                     <asp:TemplateField HeaderText="Address Line1">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_addline1" runat="server" Text='<%# Eval("DDLTYPETEXT") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_addln1" TextMode="MultiLine" runat="server" Text='<%# Eval("DDLTYPETEXT") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Address Line2">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_addln2" runat="server" Text='<%# Eval("Company_Type_Txt") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_addln2" runat="server" TextMode="MultiLine"  Text='<%# Eval("Company_Type_Txt") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Locality">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_locality" runat="server" Text='<%# Eval("Company_Name") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_locality" runat="server" Text='<%# Eval("Company_Name") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="District">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_dstrct" runat="server" Text='<%# Eval("Company_Address") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_dstrct" runat="server" Text='<%# Eval("Company_Address") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Country">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_addcntry" runat="server" Text='<%# Eval("ATEXT_STRING") %>'></asp:Label>

                                                           <asp:DropDownList ID="DDL_addcntry" runat="server" OnSelectedIndexChanged="DDL_addcntry_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="State">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_addste" runat="server" Text='<%# Eval("CLOGO") %>'></asp:Label>

                                                           <asp:DropDownList ID="DDL_addstate" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Pincode">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_addpncode" runat="server" Text='<%# Eval("TYPE") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_addpncode" runat="server" Text='<%# Eval("TYPE") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Valid From">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_addfrmdt" runat="server" Text='<%# Eval("dt1","{0:dd/MM/yyyy}") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_addfrmdate" runat="server" Text='<%# Eval("dt1","{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                                           <Ajx:CalendarExtender ID="CE_addbegda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_addfrmdate" PopupButtonID="txt_addfrmdate">
                                                            </Ajx:CalendarExtender>
                                                           <Ajx:FilteredTextBoxExtender ID="FLT_addbegda" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_addfrmdate"></Ajx:FilteredTextBoxExtender>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Valid To">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_addtodate" runat="server" Text='<%# Eval("enddate","{0:dd/MM/yyyy}") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_addtodate" runat="server" Text='<%# Eval("enddate","{0:yyyy-MM-dd}") %>'></asp:TextBox>
                                                           <Ajx:CalendarExtender ID="CE_addendda" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_addtodate" PopupButtonID="txt_addtodate">
                                                            </Ajx:CalendarExtender>
                                                           <Ajx:FilteredTextBoxExtender ID="FLT_addendda" runat="server" FilterType="Custom,Numbers" ValidChars="-" FilterMode="ValidChars" TargetControlID="txt_addtodate"></Ajx:FilteredTextBoxExtender>

                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">  
                                                       <ItemTemplate>
                                                           <asp:LinkButton ID="LK_addedt" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        CommandName="addedit" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                                                        <asp:LinkButton ID="LK_addupdt" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="addupdt"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                                        <asp:LinkButton ID="LK_addcncl" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                        CommandName="addcncl" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <br />

                                            <div id="divCI" runat="server" visible="false">
                                                <b>Employee's Communication Info</b>
                                            </div>
                                            <asp:GridView ID="GV_empCI" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_empCI_RowDataBound" OnRowCommand="GV_empCI_RowCommand"
                                                Width="100%" DataKeyNames="ID,EMPID,desig" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                     <asp:BoundField HeaderText="Employee ID" DataField="EMPID"/>
                                                     <asp:BoundField HeaderText="Employee Name" DataField="NAME"/>
                                                    <asp:BoundField HeaderText="Contact Type" DataField="DESCRIPTION"/>

                                                     <asp:TemplateField HeaderText="Contact Type ID">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_citype" runat="server" Text='<%# Eval("DDLTYPETEXT") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_cityp" runat="server" Text='<%# Eval("DDLTYPETEXT") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>

                                                     <asp:BoundField HeaderText="Created On" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                                    
                                                     <asp:TemplateField HeaderText="Action">  
                                                       <ItemTemplate>
                                                    <asp:LinkButton ID="LK_CIedt" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                     CommandName="ciedit" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                                                    <asp:LinkButton ID="LK_ciupdate" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    CommandName="ciupdate"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                                    <asp:LinkButton ID="LK_cicncl" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                    CommandName="cicancl" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <br />

                                                <div id="divDCI" runat="server" visible="false">
                                                <b>Employee's Documents Info</b>
                                            </div>
                                             <asp:GridView ID="GV_docinfo" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_docinfo_RowDataBound" OnRowCommand="GV_docinfo_RowCommand"
                                                Width="100%" DataKeyNames="ID,EMPID,desig,CLOGO,DESCRIPTION" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false"> 
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="EMPID"/>
                                                    <asp:BoundField HeaderText="Employee Name" DataField="NAME"/>
                                                    <asp:BoundField HeaderText="Document Type" DataField="DESCRIPTION"/>

                                                     <asp:TemplateField HeaderText="Document Type ID">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_doctype" runat="server" Text='<%# Eval("DDLTYPETEXT") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_doctype" runat="server" Text='<%# Eval("DDLTYPETEXT") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Document Copy">  
                                                       <ItemTemplate>
                                                            <asp:FileUpload ID="file_updtdoccopy" runat="server"/>                                                              
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:BoundField HeaderText="Created On" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>

                                                    <asp:TemplateField HeaderText="Action">  
                                                       <ItemTemplate>
                                                            <asp:LinkButton ID="LK_docdownload" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="docdown" ToolTip="Download"><i class="fe-download"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="LK_docedit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="dcedit" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="LK_docupdt" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="dcupdt"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="LK_doccncl" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="dccncl" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <br />

                                           
                                            <div id="divBI" runat="server" visible="false">
                                                <b>Employee's Bank Info</b>
                                            </div>
                                            <asp:GridView ID="GV_empBI" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_empBI_RowDataBound" OnRowCommand="GV_empBI_RowCommand"
                                                Width="100%" DataKeyNames="ID,EMPID,Company_Name,Company_Address" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false"> 
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="EMPID"/>
                                                    <asp:BoundField HeaderText="Employee Name" DataField="NAME"/>
                                                     <asp:TemplateField HeaderText="Bank">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_bkname" runat="server" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_bkname" runat="server" Text='<%# Eval("DESCRIPTION") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Account NO.">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_accnum" runat="server" Text='<%# Eval("deptid") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_accnum" runat="server" Text='<%# Eval("deptid") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="IFSC Code">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_bkifsccode" runat="server" Text='<%# Eval("DDLTYPETEXT") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_bkifsc" runat="server" Text='<%# Eval("DDLTYPETEXT") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Bank Branch">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_bkbrnch" runat="server" Text='<%# Eval("H_type") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_bkbrnch" runat="server" Text='<%# Eval("H_type") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bank District">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_bkdstrct" runat="server" Text='<%# Eval("ATEXT_STRING") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_bkdstrct" runat="server" Text='<%# Eval("ATEXT_STRING") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Country">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_bkcntry" runat="server" Text='<%# Eval("Company_Type_Txt") %>'></asp:Label>

                                                           <asp:DropDownList ID="DDL_bkcntry" runat="server" OnSelectedIndexChanged="DDL_bkcntry_SelectedIndexChanged"></asp:DropDownList>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="State">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_bkstste" runat="server" Text='<%# Eval("IMAP_SERVER") %>'></asp:Label>

                                                           <asp:DropDownList ID="DDL_bkstate" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:BoundField HeaderText="Created On" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>

                                                    <asp:TemplateField HeaderText="Action">  
                                                       <ItemTemplate>
                                                            <asp:LinkButton ID="LK_bkedit" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="bkedit" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="LK_bkupdt" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="bkupdate"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="LK_bkcncl" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="bkcncl" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                                            </ItemTemplate>
                                                     </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                            <br />
                                            <div id="divBNF" runat="server" visible="false">
                                                <b>Employee's Benefits Info</b>
                                            </div>
                                            <asp:GridView ID="GV_empBENI" runat="server" AutoGenerateColumns="false" OnRowDataBound="GV_empBENI_RowDataBound" OnRowCommand="GV_empBENI_RowCommand"
                                                Width="100%" DataKeyNames="ID,EMPID" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Employee ID" DataField="EMPID" />
                                                    <asp:BoundField HeaderText="Employee Name" DataField="NAME" />

                                                     <asp:TemplateField HeaderText="ESI Applicable">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_esiapplicabe" runat="server" Text='<%# Eval("DESCRIPTION") %>'></asp:Label>

                                                           <asp:CheckBox ID="CHK_chkesiappli" runat="server" AutoPostBack="true" OnCheckedChanged="CHK_chkesiappli_CheckedChanged"/>
                                                            </ItemTemplate>
                                                         <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ESI Number">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_esinum" runat="server" Text='<%# Eval("DDLTYPETEXT").ToString() =="0"? "" : Eval("DDLTYPETEXT") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_esinum" runat="server" Text='<%# Eval("DDLTYPETEXT").ToString() =="0"? "" : Eval("DDLTYPETEXT") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ESI Dispensary">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_esidisp" runat="server" Text='<%# Eval("H_type") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_esidispen" runat="server" Text='<%# Eval("H_type") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PF Applicable">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_pfappli" runat="server" Text='<%# Eval("ATEXT_STRING") %>'></asp:Label>

                                                           <asp:CheckBox ID="chk_pfappli" runat="server" AutoPostBack="true" OnCheckedChanged="chk_pfappli_CheckedChanged"/>
                                                           </ItemTemplate>
                                                         <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PF Number">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_pfnum" runat="server" Text='<%# Eval("Company_Name") %>'></asp:Label>

                                                           <asp:TextBox ID="txt_pfnum" runat="server" Text='<%# Eval("Company_Name") %>'></asp:TextBox>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="PF Restrict">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_pfres" runat="server" Text='<%# Eval("Company_Address") %>'></asp:Label>

                                                           <asp:CheckBox ID="chk_pfres" runat="server" />
                                                           </ItemTemplate>
                                                          <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Zero Pension">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_zeropens" runat="server" Text='<%# Eval("IMAP_SERVER") %>'></asp:Label>

                                                           <asp:CheckBox ID="chk_zeropen" runat="server" />
                                                            </ItemTemplate>
                                                         <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                     </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Zero PT">  
                                                       <ItemTemplate>
                                                           <asp:Label ID="lbl_zeropt" runat="server" Text='<%# Eval("AWART") %>'></asp:Label>

                                                           <asp:CheckBox ID="chk_zeropt" runat="server" />
                                                           </ItemTemplate>
                                                         <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                     </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Created On" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>

                                                    <asp:TemplateField HeaderText="Action">  
                                                       <ItemTemplate>
                                                           <asp:LinkButton ID="LK_editben" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                            CommandName="editbene" ToolTip="Edit"><i class="fe-edit-1"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="LK_updtben" runat="server" ToolTip="Update" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="updtbene"><i class="fe-arrow-up-circle"></i></asp:LinkButton>

                                                            <asp:LinkButton ID="LK_cnclben" runat="server" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                CommandName="cnclbene" ToolTip="Cancel"><i class="fe-x-circle"></i></asp:LinkButton>
                                                           </ItemTemplate>
                                                     </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                                <br />
                                                 <asp:Button ID="btn_exprt_empinfo" runat="server" Text="Export to Excel" Visible="false" OnClick="btn_exprt_empinfo_Click"/>
                                                </asp:Panel>




                                            <asp:Panel ID="pnlexprt" runat="server" Visible="false">
                                                <div class="row col-sm-12 Grid" id="emprowtoexprt" visible="false" runat="server" style="background-color:#ffe6ba;height:50px;margin-left:3px">
                                        <div class="col-sm-3" style="padding-top:15px">Employee ID : <asp:Label ID="lbl_empid" runat="server"></asp:Label></div>
                                         <div class="col-sm-3" style="padding-top:15px">Employee Name : <asp:Label ID="lblename" runat="server"></asp:Label></div>
                                         <div class="col-sm-3" style="padding-top:15px">Date of Joining : <asp:Label ID="lbl_JOD" runat="server"></asp:Label></div>
                                          <div class="col-sm-3" style="padding-top:15px">Separation Date : <asp:Label ID="lbl_exitdate" runat="server"></asp:Label></div>
                                        </div>

                                                 <div>
                                                <b>Employee's Personal Details</b>
                                            </div>
                                            <asp:GridView ID="gv_exp1" runat="server" AutoGenerateColumns="false" Width="100%" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                   
                                                    <asp:BoundField HeaderText="Employee DOB" DataField="dt1"/>
                                                    <asp:BoundField HeaderText="Father's Name" DataField="IMAP_SERVER"/>
                                                     <asp:BoundField HeaderText="Mother's Name" DataField="Company_Type_Txt"/>
                                                     <asp:BoundField HeaderText="Marital Status" DataField="deptid"/>
                                                    <asp:BoundField HeaderText="Spouse Name" DataField="Company_Name"/>
                                                    <asp:BoundField HeaderText="Created On" DataField="Created_On" DataFormatString="{0:dd/MM/yyyy}"/>                                                   
                                                </Columns>
                                            </asp:GridView>

                                                <br />

                                             <div>
                                                <b>Employee's Department Info</b>
                                            </div>
                                                 <asp:GridView ID="gv_expty2" runat="server" AutoGenerateColumns="false" Width="100%" 
                                            EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                   
                                                    <asp:BoundField HeaderText="Department" DataField="Company_Address"/>
                                                    <asp:BoundField HeaderText="Grade" DataField="AWART"/>
                                                     <asp:BoundField HeaderText="Branch" DataField="ATEXT_STRING"/>
                                                     <asp:BoundField HeaderText="Division" DataField="ATEXT"/>
                                                     <asp:BoundField HeaderText="DOJ" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                                    <asp:BoundField HeaderText="Created On" DataField="Created_On" DataFormatString="{0:dd/MM/yyyy}"/>
                                                </Columns>
                                            </asp:GridView>
                                                <br />
                                                <div>
                                                <b>Employee's Designation Info</b>
                                            </div>
                                                <asp:GridView ID="gv_exprt3" runat="server"  AutoGenerateColumns="false"  Width="100%" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="desigTEXT" HeaderText="Designation" />
                                                        <asp:BoundField DataField="Date" HeaderText="Valid From" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                        <asp:BoundField DataField="enddate" HeaderText="Valid To" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                        <asp:BoundField DataField="Created_On" HeaderText="Updated On" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                    </Columns>
                                                </asp:GridView>
                                                 <br />
                                               
                                                <div>
                                                <b>Employee's Manager Info</b>
                                            </div>

                                                 <asp:GridView ID="gv_expt4" runat="server" Width="100%"  AutoGenerateColumns="false" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="desigTEXT" HeaderText="Manager ID" />
                                                        <asp:BoundField DataField="deptid" HeaderText="Manager Name" />
                                                        <asp:BoundField DataField="Date" HeaderText="Valid From" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                        <asp:BoundField DataField="enddate" HeaderText="Valid To" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                         <asp:BoundField DataField="Created_On" HeaderText="Updated On" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                
                                            <div>
                                                <b>Employee's Address Info</b>
                                            </div>
                                            <asp:GridView ID="gv_exprt5" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found..!" Width="100%" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Address Type" DataField="DESCRIPTION"/>
                                                    <asp:BoundField HeaderText="Address Line1" DataField="DDLTYPETEXT"/>
                                                    <asp:BoundField HeaderText="Address Line2" DataField="Company_Type_Txt"/>
                                                    <asp:BoundField HeaderText="Locality" DataField="Company_Name"/>
                                                    <asp:BoundField HeaderText="District" DataField="Company_Address"/>
                                                    <asp:BoundField HeaderText="Country" DataField="ATEXT_STRING"/>
                                                    <asp:BoundField HeaderText="State" DataField="CLOGO"/>
                                                    <asp:BoundField HeaderText="Pincode" DataField="TYPE"/>
                                                    <asp:BoundField HeaderText="Valid From" DataField="dt1" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                    <asp:BoundField HeaderText="Valid To" DataField="enddate" DataFormatString= "{0:dd/MM/yyyy}"/>
                                                </Columns>
                                            </asp:GridView>
                                                <br />
                                                <div>
                                                <b>Employee's Communication Info</b>
                                            </div>
                                            <asp:GridView ID="gv_exprt6" runat="server" AutoGenerateColumns="false" Width="100%" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                     <asp:BoundField HeaderText="Contact Type" DataField="DESCRIPTION"/>
                                                     <asp:BoundField HeaderText="Contact Type ID" DataField="DDLTYPETEXT"/>
                                                     <asp:BoundField HeaderText="Created On" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                                    
                                                </Columns>
                                            </asp:GridView>
                                            <br />

                                                <div>
                                                <b>Employee's Documents Info</b>
                                            </div>
                                            <asp:GridView ID="gv_exprt7" runat="server" AutoGenerateColumns="false" 
                                                Width="100%" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false"> 
                                                <Columns>
                                                    <asp:BoundField HeaderText="Document Type" DataField="DESCRIPTION"/>
                                                    <asp:BoundField HeaderText="Document Type ID" DataField="DDLTYPETEXT"/>
                                                    <asp:BoundField HeaderText="Created On" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                                </Columns>
                                            </asp:GridView>
                                                <br />
                                                <div>
                                                <b>Employee's Bank Info</b>
                                            </div>
                                            <asp:GridView ID="gv_exprt8" runat="server" AutoGenerateColumns="false" Width="100%" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false"> 
                                                <Columns>
                                                    <asp:BoundField HeaderText="Bank" DataField="DESCRIPTION"/>
                                                    <asp:BoundField HeaderText="Account NO." DataField="deptid"/>
                                                    <asp:BoundField HeaderText="IFSC Code" DataField="DDLTYPETEXT"/>
                                                    <asp:BoundField HeaderText="Bank Branch" DataField="H_type"/>
                                                    <asp:BoundField HeaderText="Bank District" DataField="ATEXT_STRING"/>
                                                    <asp:BoundField HeaderText="Country" DataField="Company_Type_Txt"/>
                                                    <asp:BoundField HeaderText="State" DataField="IMAP_SERVER"/>                                                    
                                                    <asp:BoundField HeaderText="Created On" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>

                                                </Columns>
                                            </asp:GridView>
                                                <br />

                                                <div>
                                                <b>Employee's Benefits Info</b>
                                            </div>
                                            <asp:GridView ID="gv_exprt9" runat="server" AutoGenerateColumns="false" Width="100%" EmptyDataText="No Records Found..!" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                    <asp:BoundField HeaderText="ESI Applicable" DataField="DESCRIPTION" />
                                                    <asp:BoundField HeaderText="ESI Number" DataField="DDLTYPETEXT" />
                                                    <asp:BoundField HeaderText="ESI Dispensary" DataField="H_type" />
                                                    <asp:BoundField HeaderText="PF Applicable" DataField="ATEXT_STRING" />
                                                    <asp:BoundField HeaderText="PF Number" DataField="Company_Name" />
                                                     <asp:BoundField HeaderText="PF Restrict" DataField="Company_Address" />
                                                     <asp:BoundField HeaderText="Zero Pension" DataField="IMAP_SERVER" />
                                                    <asp:BoundField HeaderText="Zero PT" DataField="AWART" />
                                                    <asp:BoundField HeaderText="Created On" DataField="Date" DataFormatString="{0:dd/MM/yyyy}"/>
                                                </Columns>
                                            </asp:GridView> 
                                            </asp:Panel>

                                            <br />
                                            <asp:UpdatePanel ID="b" runat="server">
                                            <ContentTemplate>
                                           
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btn_exprt_empinfo"/>
                                                 <asp:PostBackTrigger ControlID="GV_viewemp_details" />
                                                     <asp:PostBackTrigger ControlID="GV_empPI" />
                                                     <asp:PostBackTrigger ControlID="GV_empdept" />
                                                     <asp:PostBackTrigger ControlID="GV_empAI" />
                                                     <asp:PostBackTrigger ControlID="GV_empCI" />
                                                     <asp:PostBackTrigger ControlID="GV_docinfo" />
                                                     <asp:PostBackTrigger ControlID="GV_empBI" />
                                                     <asp:PostBackTrigger ControlID="GV_empBENI" />
                                            </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        </div>
                                    

                        </div>
                        </div>
                    </div>
                </div>
                    
</asp:Content>
 
