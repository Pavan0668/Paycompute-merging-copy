<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.Master" AutoEventWireup="true" CodeBehind="Salary_Details.aspx.cs" Inherits="iEmpPower.UI.SPaycompute.Salary_Details"
    Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" Theme="SkinFile" EnableEventValidation="false"  MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
   <script type ="text/javascript">

       var validFilesTypes = ["xls", "xlsx"];

       function ValidateFile() {

           var file = document.getElementById("<%=flup_slrydata_mstrs.ClientID%>");

        var label = document.getElementById("<%=lbl_uploadxl_mssg.ClientID%>");

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

            label.innerHTML = "Invalid File. Please upload a file with provided format";

        }

        return isValidFile;

    }

</script>
    <script>

        function call(eventElement) {

            var target = eventElement.target;

            switch (target.mode) {

                case "month":

                    var cal = $find("CE_BE_txtBudgFrmMonth");

                    cal._visibleDate = target.date;

                    cal.set_selectedDate(target.date);

                    cal._switchMonth(target.date);

                    cal._blur.post(true);

                    cal.raiseDateSelectionChanged();

                    break;

            }

        }

        function onCalendarShown() {

            var cal = $find("CE_BE_txtBudgFrmMonth");

            //Setting the default mode to month

            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it

            if (cal._monthsBody) {

                for (var i = 0; i < cal._monthsBody.rows.length; i++) {

                    var row = cal._monthsBody.rows[i];

                    for (var j = 0; j < row.cells.length; j++) {

                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);

                    }

                }

            }

        }

        function onCalendarHidden() {

            var cal = $find("CE_BE_txtBudgFrmMonth");

            //Iterate every month Item and remove click event from it

            if (cal._monthsBody) {

                for (var i = 0; i < cal._monthsBody.rows.length; i++) {

                    var row = cal._monthsBody.rows[i];

                    for (var j = 0; j < row.cells.length; j++) {

                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);

                    }

                }

            }

        }





        function Srch_call(eventElement) {

            var Src_target = eventElement.target;

            switch (Src_target.mode) {

                case "month":

                    var cald = $find("Srch_cldr_behve");

                    cald._visibleDate = Src_target.date;

                    cald.set_selectedDate(Src_target.date);

                    cald._switchMonth(Src_target.date);

                    cald._blur.post(true);

                    cald.raiseDateSelectionChanged();

                    break;

            }

        }

        function Srch_Cal_Show() {

            var cald = $find("Srch_cldr_behve");

            //Setting the default mode to month

            cald._switchMode("months", true);

            //Iterate every month Item and attach click event to it

            if (cald._monthsBody) {

                for (var i = 0; i < cald._monthsBody.rows.length; i++) {

                    var row = cald._monthsBody.rows[i];

                    for (var j = 0; j < row.cells.length; j++) {

                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", Srch_call);

                    }

                }

            }

        }

        function Srch_Cal_hide() {

            var cald = $find("Srch_cldr_behve");

            //Iterate every month Item and remove click event from it

            if (cald._monthsBody) {

                for (var i = 0; i < cal._monthsBody.rows.length; i++) {

                    var row = cald._monthsBody.rows[i];

                    for (var j = 0; j < row.cells.length; j++) {

                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", Srch_call);

                    }

                }

            }

        }






        function update_call(eventElement) {

            var Src_target = eventElement.target;

            switch (Src_target.mode) {

                case "month":

                    var cald = $find("Update_cldr_behve");

                    cald._visibleDate = Src_target.date;

                    cald.set_selectedDate(Src_target.date);

                    cald._switchMonth(Src_target.date);

                    cald._blur.post(true);

                    cald.raiseDateSelectionChanged();

                    break;

            }

        }

        function update_Cal_Show() {

            var cald = $find("Update_cldr_behve");

            //Setting the default mode to month

            cald._switchMode("months", true);

            //Iterate every month Item and attach click event to it

            if (cald._monthsBody) {

                for (var i = 0; i < cald._monthsBody.rows.length; i++) {

                    var row = cald._monthsBody.rows[i];

                    for (var j = 0; j < row.cells.length; j++) {

                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", update_call);

                    }

                }

            }

        }

        function update_Cal_hide() {

            var cald = $find("Update_cldr_behve");

            //Iterate every month Item and remove click event from it

            if (cald._monthsBody) {

                for (var i = 0; i < cal._monthsBody.rows.length; i++) {

                    var row = cald._monthsBody.rows[i];

                    for (var j = 0; j < row.cells.length; j++) {

                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", update_call);

                    }

                }

            }

        }
     </script>

     <div class="row">
            <div class="col-12">
                <div class="page-title-box">
                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Upload/Download Employee Salary Details</li>
                        </ol>
                    </div>
                    <h4 class="page-title">Upload/Download Employee Salary Details
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
                            Upload Salary Excel</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab2" class="nav-link  p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-edit-2"></i>
                            Edit Salary Details</asp:LinkButton></li>
                             <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab3" class="nav-link  p-2" OnClick="Tab3_Click" CausesValidation="false"><i class="fe-database"></i>
                            Upload History</asp:LinkButton></li>
                            </ul>

                    <div class="tabcontents">
                        <div id="view1" runat="server" visible="false"  style="width:100%">
                            <br />
                             <div class="header-title">&nbsp;&nbsp;Upload Employees Salary Through Excel</div>
                               <hr class="HrCls"/>
                                  <br />
                            <asp:Label ID="LBL_bindprev_mnth" runat="server"></asp:Label>
                             <div class="row">
                                    <div class="md-2">&nbsp;&nbsp;&nbsp;&nbsp; Upload Salary Details</div>
                                    <div class="col-md-3">
                                        <asp:FileUpload ID="flup_slrydata_mstrs" runat="server" />
                                    </div>

                                    <div class="col-md-4">
                                         <asp:TextBox ID="txt_salary_formonth" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>                                   
                                     <Ajx:CalendarExtender ID="CE_txtBudgFrmMonth" runat="server" BehaviorID="CE_BE_txtBudgFrmMonth" Enabled="True" Format="MM-yyyy"
                                             TargetControlID="txt_salary_formonth" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" PopupButtonID="txt_salary_formonth" 
                                                   DefaultView="Months">  </Ajx:CalendarExtender>
                                           <asp:RequiredFieldValidator ID="Req_mnthto_crtsalary" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txt_salary_formonth" SetFocusOnError="true" Display="Dynamic" ValidationGroup="createsalary"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:Button ID="btn_upld_slrytemplt" runat="server" Text="Upload Template" ValidationGroup="createsalary" OnClientClick = "return ValidateFile()"  OnClick="btn_upld_slrytemplt_Click"/>
                                    </div>
                                  <div class="col-md-3" style="text-align: right">
                                 <asp:UpdatePanel ID="b" runat="server">
                                 <ContentTemplate>
                                   
                                        <asp:LinkButton ID="LK_dwnld_slrytemplt" runat="server" Text="Download Template" OnClick="LK_dwnld_slrytemplt_Click"></asp:LinkButton>
                                        </ContentTemplate>
                                         <Triggers>
                                         <asp:PostBackTrigger ControlID="LK_dwnld_slrytemplt" />
                                              <asp:PostBackTrigger ControlID="btn_upld_slrytemplt" />
                                             <asp:PostBackTrigger ControlID="btn_Save_SalarytoDB" />
                                       </Triggers>
                                  </asp:UpdatePanel>
                                    </div>
                                </div>
                <div>
                    <asp:Label ID="lbl_uploadxl_mssg" runat="server" ForeColor="Red"></asp:Label>
                </div>

                            <br />

                    <asp:GridView ID="GV_upload_slry_details" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="GV_upload_slry_details_PageIndexChanging">
                    <Columns>
                        <asp:BoundField HeaderText="SL_NO" DataField="SL_NO"/>
                        <asp:BoundField HeaderText="Employee ID" DataField="Employee_ID"/>
                        <asp:BoundField HeaderText="Employee Name" DataField="Employee_Name"/>
                        <asp:BoundField HeaderText="Salary Components" DataField="Salary_Component"/>
                        <asp:BoundField HeaderText="Amount(INR)" DataField="Salary_Rate"/>


                    </Columns>
                </asp:GridView>
                         <br />
                        
                         <div>
                              <asp:Button ID="btn_Save_SalarytoDB" runat="server" Text="Save" Width="70px" OnClick="btn_Save_SalarytoDB_Click"/>
                             <asp:Button ID="btn_cancel_ssave" runat="server" Text="Cancel" Width="70px" OnClick="btn_cancel_ssave_Click"/>
                         </div>

                   
                </div>
      </div>




                        <div id="view2" runat="server" visible="false"  style="width:100%">
                             <br />
                             <div class="header-title">&nbsp;&nbsp;Upload Employees Salary Through Existing Data</div>
                               <hr class="HrCls"/>
                                  <br />
                             <div class="row" id="searchdiv" runat="server">
                                 <div class="col-sm-10 htCr Cntrlwidth"><span style="color:red">*</span>
                                     <asp:DropDownList ID="DDL_editemp_slry" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RQD_editemp_slry" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="DDL_editemp_slry" Display="Dynamic" SetFocusOnError="true" InitialValue="0" ValidationGroup="slrysrch"></asp:RequiredFieldValidator>
                                 <%--<asp:TextBox ID="txt_srchtoedit" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>--%>
                                     &nbsp;&nbsp;
                                     <asp:Button ID="btnsearch" runat="server" Text="Search" Width="60px" ValidationGroup="slrysrch" OnClick="btnsearch_Click"/>
                                     <asp:Button ID="btnreset" runat="server" Text="Reset" Width="60px" OnClick="btnreset_Click"/>
                                     <asp:Button ID="btnedit" runat="server" Text="Edit" Width="60px" OnClick="btnedit_Click"/>
                                     </div>
                             </div>
                             <br />
                     <div class="form-group" id="disp_mnth" runat="server">
                                     <div class="col-sm-6" style="margin-left:-13px">Salary details for the month of :
                                        <asp:Label ID="LBL_bind_Smonth" runat="server"></asp:Label>
                                        </div>
                          <div class="col-sm-3 htCr Cntrlwidth" id="monttxt" runat="server">Select Month to upload salary :</div>
                         <div class="col-sm-2">
                            <asp:TextBox ID="txtslry_saveexisting_mnth" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                        <Ajx:CalendarExtender ID="CE_Update_salarydtls" runat="server"
                                            BehaviorID="Update_cldr_behve" Enabled="true" Format="MM-yyyy"
                                        TargetControlID="txtslry_saveexisting_mnth" OnClientHidden="update_Cal_hide" OnClientShown="update_Cal_Show" PopupButtonID="txtslry_saveexisting_mnth"
                                         DefaultView="Months"></Ajx:CalendarExtender>  
                             <asp:RequiredFieldValidator ID="REQ_Runpayroll" runat="server" ErrorMessage="*" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" ControlToValidate="txtslry_saveexisting_mnth" ValidationGroup="runpay"></asp:RequiredFieldValidator>                                   
                             </div>
                                         </div>
                             <br />
                             <div style="float:right" id="errormssg_srtpayrol" runat="server">
                             <asp:Label ID="lblerrormssg" runat="server" ForeColor="Red"></asp:Label>
                               </div>      
                      <br />
                      <div>
                          <div class="col-md-12 text-right" id="divcnt" runat="server"></div>
                          <asp:GridView ID="GV_loademp_slry" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnRowDataBound="GV_loademp_slry_RowDataBound"
                            DataKeyNames="col1,TXT" OnPageIndexChanging="GV_loademp_slry_PageIndexChanging" OnRowCommand="GV_loademp_slry_RowCommand">
                              <Columns>
                                  <asp:TemplateField HeaderText="Slno">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                  <asp:BoundField DataField="" HeaderText="Employee Id"/>
                                 <asp:TemplateField HeaderText="Employee Name">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_emname" runat="server" Text='<%# Eval("col1") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Action">
                                             <ItemTemplate>

                                                 <asp:LinkButton ID="LNK_View" runat="server" ToolTip="View" CommandName="VIEWSALARY" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="fe-eye"></asp:LinkButton>&nbsp;&nbsp;&nbsp;

                                                 <asp:LinkButton ID="LNK_edit" runat="server" ToolTip="Edit" CommandName="EDITSALARY" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="fe-edit-1"></asp:LinkButton>

                                                 </ItemTemplate>
                                       </asp:TemplateField>

                                   <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="LBL_gvempid" runat="server" Text='<%# Eval("TXT") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField> 
                              </Columns>
                          </asp:GridView>
                      </div>
                     <div>

                         <div class="col-md-12 text-right" id="dvcntedt" runat="server"></div>
                          <asp:GridView ID="GV_enableedit_purpose" runat="server" AutoGenerateColumns="false" 
                           AllowPaging="true" PageSize="10" OnPageIndexChanging="GV_enableedit_purpose_PageIndexChanging" DataKeyNames="TXT,col1,CCD,col10,id1" 
                              OnRowDataBound="GV_enableedit_purpose_RowDataBound" ShowHeaderWhenEmpty="false">

                              <Columns>

                                         <asp:TemplateField HeaderText="Slno">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbledtRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                   <asp:BoundField HeaderText="Employee ID" DataField="" />                                   

                                   <asp:TemplateField HeaderText="Employee Name">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_name" runat="server" Text='<%# Eval("col1") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Salary Components">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_salary_components" runat="server" Text='<%# Eval("CCD") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                 
                                         <asp:TemplateField HeaderText="Amount(INR)">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_Salaryrate" runat="server" Text='<%# Eval("col10") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>  
                                  <asp:BoundField DataField="col10" HeaderText="Salary Rate" />                                       
                                        
                                    <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="LBL_empid" runat="server" Text='<%# Eval("TXT") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField> 
                                        
                                     </Columns>

                             
                </asp:GridView>
                         <br />
                         <br />
                          <div id="buttonset" runat="server">
                              <asp:Button ID="btn_Saveexisting_slry" runat="server" Text="Start Payroll" Width="100px"  ValidationGroup="runpay" OnClick="btn_Saveexisting_slry_Click"/>
                             <%--<asp:Button ID="btn_clrexisting_slry" runat="server" Text="Cancel" Width="70px" OnClick="btn_clrexisting_slry_Click"/>--%>
                         </div>

                         </div>
                            
                             <asp:GridView ID="GV_viewsalaryindetail" runat="server" AutoGenerateColumns="false" AllowPaging="false" 
                                 DataKeyNames="TXT,col1,CCD,col10,id1" OnRowDataBound="GV_viewsalaryindetail_RowDataBound" ShowFooter="true">
                                 <Columns>
                                    <%-- <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                               
                                      <asp:BoundField HeaderText="Employee ID" DataField=""/>
                                   <asp:TemplateField HeaderText="Employee Name">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_namesrch" runat="server" Text='<%# Eval("col1") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Earnings">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_salary_componentssrch" runat="server" Text='<%# Eval("CCD") %>'></asp:Label>
                                                 </ItemTemplate>
                                       <FooterTemplate >
                                            <div style="color:#00617c">
                                             Gross Salary&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: <br />
                                                </div>
                                           </FooterTemplate>
                                         </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Amount(INR)">
                                             <ItemTemplate>
                                                 <asp:Label ID="lbl_allowance" runat="server" Text='<%# Eval("col10") %>'></asp:Label>

                                                  </ItemTemplate>
                                       <FooterTemplate >
                                                    <div>
                                              <asp:Label ID="lbl_grosstotal" runat="server" Text="" ForeColor="#00617c"></asp:Label>
                                                        </div>
                                               </FooterTemplate>
                                         </asp:TemplateField>
                                                                              
                                     <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="lbl_DBID" runat="server" Text='<%# Eval("id1") %>'></asp:Label>
                                                 </ItemTemplate>
                                         
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="LBL_empidsrch" runat="server" Text='<%# Eval("TXT") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField> 
                                 </Columns>
                             </asp:GridView>                            
                            <asp:GridView ID="GV_viewsaldedtn" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                 DataKeyNames="TXT,col1,CCD,col10,id1" OnRowDataBound="GV_viewsaldedtn_RowDataBound" ShowFooter="true">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Slno">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                    <asp:BoundField HeaderText="" DataField="" />
                                    <asp:TemplateField HeaderText="">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_namedetn" runat="server" Text='<%# Eval("col1") %>'></asp:Label>
                                                 </ItemTemplate>
                                        </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Deductions">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_componendtn" runat="server" Text='<%# Eval("CCD") %>'></asp:Label>
                                                 </ItemTemplate>
                                         <FooterTemplate >
                                        <div style="color:#00617c">
                                             Net Salary&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: <br />
                                                </div>
                                             </FooterTemplate>
                                         </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Amount(INR)">
                                             <ItemTemplate>
                                                  <asp:Label ID="lbl_deductions" runat="server" Text='<%# Eval("col10") %>'></asp:Label> 
                                                 </ItemTemplate>
                                         <FooterTemplate >
                                                    <div>
                                              <asp:Label ID="lbl_nettotal" runat="server" Text="" ForeColor="#00617c"></asp:Label>
                                                        </div>
                                               </FooterTemplate>
                                         </asp:TemplateField>

                                     <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="lbl_DBIDdtn" runat="server" Text='<%# Eval("id1") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="LBL_empiddtn" runat="server" Text='<%# Eval("TXT") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField> 
                                </Columns>
                            </asp:GridView>
                               

                             <asp:GridView ID="GV_search" runat="server" AutoGenerateColumns="false" AllowPaging="false"
                                 DataKeyNames="TXT,col1,CCD,col10,id1" OnRowDataBound="GV_search_RowDataBound">
                                 <Columns>
                                     <asp:TemplateField HeaderText="Slno">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                               
                                      <asp:BoundField HeaderText="Employee ID" DataField=""/>
                                   <asp:TemplateField HeaderText="Employee Name">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_namesrch" runat="server" Text='<%# Eval("col1") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Salary Components">
                                             <ItemTemplate>
                                                 <asp:Label ID="LBL_salary_componentssrch" runat="server" Text='<%# Eval("CCD") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                 


                                         <asp:TemplateField HeaderText="Amount(INR)">
                                             <ItemTemplate>                                      
                                                 <asp:TextBox ID="txt_Salaryratesrch" runat="server" Text='<%# Eval("col10") %>'></asp:TextBox>
                                                 </ItemTemplate>
                                         </asp:TemplateField>  
                                  <%--<asp:BoundField DataField="col10" HeaderText="Salary Rate"/>--%>                                       
                                        
                                         
                                     <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="lbl_DBID" runat="server" Text='<%# Eval("id1") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>

                                         <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="LBL_empidsrch" runat="server" Text='<%# Eval("TXT") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField> 
                                 </Columns>
                             </asp:GridView>

                             <br />
                             <div>
                                 <asp:Button ID="btn_updateslry" runat="server" Text="Update" Width="70px" OnClick="btn_updateslry_Click"/>
                                 <asp:Button ID="btn_slryupdate_cncl" runat="server" Width="70px" Text="Cancel" OnClick="btn_slryupdate_cncl_Click"/>
                             </div>
                             <br />
                            </div>
                          




                        <div id="view3" runat="server" visible="false"  style="width:100%">
                             <br />
                             <div class="header-title">&nbsp;&nbsp;View Upload Salary Details</div>
                               <hr class="HrCls"/>
                                  <br />
                      <div class="form-group">
                          <div class="row">
                              <div class="col-sm-6">
                              <asp:DropDownList ID="DDL_View_salaryrts" runat="server" CssClass="txtDropDownwidth"  AutoPostBack="true" OnSelectedIndexChanged="DDL_View_salaryrts_SelectedIndexChanged">
                                   <asp:ListItem Value="1">Month</asp:ListItem>
                                   <asp:ListItem Value="3">Employee</asp:ListItem>
                              </asp:DropDownList>

                              <asp:TextBox ID="txt_sech_Salary_bymnth" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                              <Ajx:CalendarExtender ID="CE_Viewhistory" runat="server" BehaviorID="Srch_cldr_behve" Enabled="true" Format="MM-yyyy" TargetControlID="txt_sech_Salary_bymnth"
                                  OnClientHidden="Srch_Cal_hide" OnClientShown="Srch_Cal_Show" PopupButtonID="txt_sech_Salary_bymnth"  DefaultView="Months"></Ajx:CalendarExtender>
                              
                              <asp:DropDownList ID="DDL_userIDs" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                              <asp:Button ID="btn_salary_history" runat="server" Text="Search" Width="60px" OnClick="btn_salary_history_Click"/>
                              <asp:Button ID="btn_salary_viewclr" runat="server" Text="Reset" Width="60px" OnClick="btn_salary_viewclr_Click"/>
                          </div>
                            </div>
                          <br />
                          <br />

                          <div>
                            
                              <div class="col-md-12 text-right" id="dvcnthsrty" runat="server"></div>
                              <asp:GridView ID="GV_Salaruy_uploadhstry" runat="server"
                             AutoGenerateColumns="false"
                           AllowPaging="true" PageSize="10" OnPageIndexChanging="GV_Salaruy_uploadhstry_PageIndexChanging"  OnRowDataBound="GV_Salaruy_uploadhstry_RowDataBound" EmptyDataText="Records Not Found..!" ShowHeaderWhenEmpty="false">

                                  <Columns>
                                       <asp:TemplateField  HeaderText="Sl No.">
                                   <ItemTemplate>
                                          <asp:Label ID="lblhstryRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>

                                      <asp:BoundField HeaderText="Employee ID"  DataField="TXT"/>
                                       <asp:BoundField HeaderText="Employee Name"  DataField="col1"/>
                                      <asp:BoundField HeaderText="Salary Components"  DataField="CCD"/>
                                      <asp:BoundField HeaderText="Amount(INR)"  DataField="col10"/>
                                      <asp:BoundField HeaderText="Salary Month"  DataField="MNTH"/>
                                      <%--<asp:BoundField HeaderText="Uploaded By"  DataField="Created_BY"/>--%>
                                      <asp:BoundField HeaderText="Uploaded On"  DataField="begda" DataFormatString="{0:dd/MM/yyyy}"/>
                                     
                                  </Columns>
                              </asp:GridView>

                         <br />
                          <div>
                              <asp:UpdatePanel ID="c" runat="server">
                                 <ContentTemplate>
                              <asp:Button ID="btn_export_slryhsry" runat="server" Text="Export to Excel" OnClick="btn_export_slryhsry_Click"/>
                               </ContentTemplate>
                                <Triggers>
                                <asp:PostBackTrigger ControlID="btn_export_slryhsry" />
                                 </Triggers>
                                  </asp:UpdatePanel>
                              </div>
                          </div>
                          </div>
    </div>
        </div>
                        </div>
                    </div>
                 </div>
            </div>

</asp:Content>
