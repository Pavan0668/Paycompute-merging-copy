<%@ Page Title="TimeSheet Review" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="TimeSheetReview.aspx.cs" EnableEventValidation="false"
    Inherits="iEmpPower.UI.Manager_Self_Service.TimeSheetReview" Culture="en-GB" Theme="SkinFile" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    </style>


    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Time Sheet Review</li>
                    </ol>
                </div>
                <h4 class="page-title">Time Sheet Review
                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg"></asp:Label>
                </h4>
            </div>
        </div>
    </div>

     <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
    </div>

   <div class=" card-box">
             <div id="real_time_chart" class="row">
                <div style="width: 99%; margin: 0 auto; padding: 5px 0 40px 0;">
                     <div class="col-sm-12"  style="width:100%">
                    <div  style="width:100%">
                        <ul class="nav nav-pills navtab-bg" >
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab1" class="nav-link p-2" OnClick="Tab1_Click" CausesValidation="false"><i class="fe-arrow-up-circle" ></i>
                            Timesheet Review</asp:LinkButton></li>
                            <li class="nav-item font-12">
                                <asp:LinkButton runat="server" ID="Tab2" class="nav-link  p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-file-text"></i>
                            Leave/Attd. Review</asp:LinkButton></li>
                                                
                   </ul>
                    <div class="tabcontents">
                        <div id="view1" runat="server" visible="false"  style="width:100%">
                             <br />
                                <div class="header-title">&nbsp;&nbsp;Timesheet Review</div>
                                <hr class="HrCls" />
                                <br />
                <div>                 
                    <div class="form-group">
                 <div class="row">
                    <div class="col-sm-12">
                    <asp:DropDownList ID="DDLEmpList" runat="server" CssClass="txtDropDownwidth" TabIndex="1"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFV_DDLEmpList" runat="server" ControlToValidate="DDLEmpList" Display="Dynamic" CssClass="lblValidation"
                    ErrorMessage="Please select data for type" ForeColor="Red" InitialValue="0" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                   &nbsp;&nbsp;
                        <asp:TextBox ID="TxtFromDate" runat="server" CssClass="txtDropDownwidth"  ValidationGroup="vg1" TabIndex="2" Placeholder="From Date"></asp:TextBox>
                         <Ajx:MaskedEditExtender ID="MEE_TxtFromDate" runat="server" AcceptNegative="Left" CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                          MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtFromDate" />
                          <Ajx:CalendarExtender ID="CE_TxtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="TxtFromDate"> </Ajx:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RFV_TxtFromDate" runat="server" ControlToValidate="TxtFromDate" ValidationGroup="vg1"
                            CssClass="lblValidation" Display="Dynamic" ErrorMessage="Please select from date" ForeColor="Red"></asp:RequiredFieldValidator>
                            <%--<asp:RegularExpressionValidator ID="REV_TxtFromDate" runat="server" Display="Dynamic" CssClass="lblValidation"
                             ErrorMessage="Invalid Date" ControlToValidate="TxtFromDate" ValidationGroup="vg1"
                             SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                        &nbsp;&nbsp;
                               <asp:TextBox ID="TxtToDate" runat="server" CssClass="txtDropDownwidth" ValidationGroup="vg1" TabIndex="3" Placeholder="From Date"></asp:TextBox>
                                <Ajx:MaskedEditExtender ID="MEE_TxtToDate" runat="server" AcceptNegative="Left" CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                 MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtToDate" />
                                 <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="TxtToDate"> </Ajx:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RFV_TxtToDate" runat="server" ControlToValidate="TxtToDate" ValidationGroup="vg1" CssClass="lblValidation" Display="Dynamic" ErrorMessage="Please select to date" ForeColor="Red"></asp:RequiredFieldValidator>
                                    
                                     <%-- <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic" CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtToDate" ValidationGroup="vg1"
                                      SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                    &nbsp;&nbsp;
                                <asp:DropDownList ID="DDL_mgrsrchtyp" runat="server" OnSelectedIndexChanged="DDL_mgrsrchtyp_SelectedIndexChanged" AutoPostBack="true">
                                     <asp:ListItem value="0" Selected="True">All Records</asp:ListItem>
                                     <asp:ListItem value="1">Projects</asp:ListItem>
                                     <asp:ListItem value="2">WBS</asp:ListItem>
                                     <asp:ListItem value="3">Activity</asp:ListItem>
                                     <asp:ListItem value="4">Attendance</asp:ListItem>
                                 </asp:DropDownList>
                                 &nbsp;&nbsp;
                                    <asp:DropDownList ID="DDL_mgrbysrchtyps" runat="server" Visible="false"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="REQ_ddlsrchbymgr" runat="server" ControlToValidate="DDL_mgrbysrchtyps" ValidationGroup="vg1" InitialValue="0" ErrorMessage="*"
                             Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>                              
                        </div>
                     </div>  
                         <div class="row">
                    <div class="col-sm-6">                     
                     <asp:Button ID="btnDisplay" Width="80px" runat="server" Text="Display" ValidationGroup="vg1" CausesValidation="true" OnClick="btnDisplay_Click" TabIndex="4" />
                        &nbsp;
                      <asp:Button ID="BtnClear" Width="80px" runat="server" Text="Clear" CausesValidation="false" OnClick="BtnClear_Click" TabIndex="5" />                       
                        &nbsp;
                       <asp:Button ID="btnExcel" Width="80px" runat="server" Text="Export" CausesValidation="false" Visible="false" OnClick="btnExcel_Click" TabIndex="6" />
                         &nbsp;
                        <asp:CompareValidator ID="CV_TxtToDate" runat="server" ControlToCompare="TxtFromDate" ValidationGroup="vg1" CssClass="lblValidation" ControlToValidate="TxtToDate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                                      Operator="GreaterThanEqual" Type="Date" ForeColor="Red"></asp:CompareValidator>
                        </div>
                              <div class="col-md-6 text-right" id="divcnt" runat="server"></div>
                        </div>
                         
                        </div>
                        <asp:UpdatePanel runat="server" ID="a">
                          <ContentTemplate>
                                    </ContentTemplate>
                             <Triggers>
                                 <asp:PostBackTrigger ControlID="btnExcel"/>
                                 
                             </Triggers>
                             </asp:UpdatePanel>
                         
                              <div class="respovrflw">
                               <asp:GridView ID="grdTimeSheetReview" runat="server" AutoGenerateColumns="False" OnRowDataBound="grdTimeSheetReview_RowDataBound" ShowFooter="true" DataKeyNames="PERNR">
                                <Columns>
                                 <asp:TemplateField HeaderText="Slno">
                                 <ItemTemplate>
                                   <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                  </ItemTemplate>
                                  </asp:TemplateField>
                                   <asp:BoundField DataField="" HeaderText="Employee ID" />
                                    <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />
                                    <asp:BoundField DataField="EMP_DEPT" HeaderText="Department" />
                                    <asp:BoundField DataField="KTEXT" HeaderText="Project" />
                                    <asp:BoundField DataField="POST1" HeaderText="Work Breakdown Structure(WBS)" />
                                    <asp:BoundField DataField="Activity" HeaderText="Activity" />
                                    <asp:BoundField DataField="ATEXT" HeaderText="Attendance Type" />
                                     <asp:BoundField DataField="WORKINGDATE" HeaderText="Working Dates" />
                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />

                                    <asp:TemplateField HeaderText="Status">
                                     <ItemTemplate>
                                      <asp:Label ID="LBL_empsts" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                       </ItemTemplate>
                                        <FooterTemplate >
                                          <div style="color:#00617c">
                                           Total Hours&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: <br />
                                           </div>
                                          </FooterTemplate>
                                           </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Hours">
                                             <ItemTemplate>
                                                 <asp:Label ID="lbl_emphrs" runat="server" Text='<%# Eval("CATSHOURS") %>'></asp:Label>

                                                  </ItemTemplate>
                                       <FooterTemplate >
                                                    <div>
                                              <asp:Label ID="lbl_emptotalhrs" runat="server" Text="" ForeColor="#00617c"></asp:Label>
                                                        </div>
                                               </FooterTemplate>
                                         </asp:TemplateField>
                                      
                                        <asp:TemplateField HeaderText="" Visible="false">
                                   <ItemTemplate>
                                     <asp:Label ID="LBL_empid" runat="server" Text='<%# Eval("PERNR") %>'></asp:Label>
                                     </ItemTemplate>
                                         </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                </div>

                        <div id="view2" runat="server" visible="false" style="width: 100%">
                            <br />
                                <div class="header-title">&nbsp;&nbsp;Leave / Attd. Review</div>
                                <hr class="HrCls" />
                                <br />
                            <div>

                                <div class="form-group">
                 <div class="row">
                    <div class="col-sm-12">
                    <asp:DropDownList ID="DDL_lvemp" runat="server" CssClass="txtDropDownwidth" TabIndex="1"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFV_lvemp" runat="server" ControlToValidate="DDL_lvemp" Display="Dynamic" CssClass="lblValidation"
                    ErrorMessage="Please select data for type" ForeColor="Red" InitialValue="0" ValidationGroup="lvvalid"></asp:RequiredFieldValidator>
                   &nbsp;&nbsp;
                        <asp:TextBox ID="txt_lvfrmdate" autocompletetext="off" runat="server" CssClass="txtDropDownwidth"  ValidationGroup="lvvalid" TabIndex="2" Placeholder="From Date"></asp:TextBox>
                        
                          <Ajx:CalendarExtender ID="CE_lvfrmdate" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_lvfrmdate"> </Ajx:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RFV_lvfrmdate" runat="server" ControlToValidate="txt_lvfrmdate" ValidationGroup="lvvalid"
                            CssClass="lblValidation" Display="Dynamic" ErrorMessage="Please select from date" ForeColor="Red"></asp:RequiredFieldValidator>
                           
                        &nbsp;&nbsp;
                               <asp:TextBox ID="txt_lvtodate" autocompletetext="off" runat="server" CssClass="txtDropDownwidth" ValidationGroup="lvvalid" TabIndex="3" Placeholder="From Date"></asp:TextBox>
                               
                                 <Ajx:CalendarExtender ID="CE_lvtodate" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txt_lvtodate"> </Ajx:CalendarExtender>
                                    <asp:RequiredFieldValidator ID="RFV_lvtodate" runat="server" ControlToValidate="txt_lvtodate" ValidationGroup="lvvalid" CssClass="lblValidation" Display="Dynamic" ErrorMessage="Please select to date" ForeColor="Red"></asp:RequiredFieldValidator>
                                     &nbsp;&nbsp;

                         <asp:Button ID="btnlvview" Width="80px" runat="server" Text="Display" ValidationGroup="lvvalid" CausesValidation="true" OnClick="btnlvview_Click" TabIndex="4" />
                        &nbsp;
                      <asp:Button ID="btnlvclick" Width="80px" runat="server" Text="Clear" CausesValidation="false" OnClick="btnlvclick_Click" TabIndex="5" />                       
                        &nbsp;
                       <asp:Button ID="btnlvexprt" Width="80px" runat="server" Text="Export" CausesValidation="false" Visible="false" OnClick="btnlvexprt_Click" TabIndex="6" />
                                                            
                        </div>
                     <asp:UpdatePanel ID="uplv" runat="server">
                         <ContentTemplate></ContentTemplate>
                         <Triggers>
                             <asp:PostBackTrigger ControlID="btnlvexprt"/>
                         </Triggers>
                     </asp:UpdatePanel>
                     </div>  
                         <div class="row">
                    <div class="col-sm-6">                     
                    
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txt_lvfrmdate" ValidationGroup="lvvalid" CssClass="lblValidation" ControlToValidate="txt_lvtodate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                                      Operator="GreaterThanEqual" Type="Date" ForeColor="Red"></asp:CompareValidator>
                        </div>
                              <div class="col-md-6 text-right" id="divlv" runat="server"></div>
                        </div>
                         
                        </div>

                                <asp:GridView ID="GV_leaveattd_review" runat="server" OnPageIndexChanging="GV_leaveattd_review_PageIndexChanging" 
                                    AllowPaging="true" PageSize="25" DataKeyNames="rid,PKEY,PERNR" OnRowDataBound="GV_leaveattd_review_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Slno">
                                 <ItemTemplate>
                                   <asp:Label ID="lbllvRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                  </ItemTemplate>
                                  </asp:TemplateField>
                                        <asp:BoundField HeaderText="Employee ID" DataField="PERNR" />
                                        <asp:BoundField HeaderText="Employee Name" DataField="ENAME" />
                                        <asp:BoundField HeaderText="Request Type" DataField="TableTyp" />
                                        <asp:BoundField HeaderText="Request Sub Type" DataField="Subtype" />
                                        <asp:BoundField HeaderText="Start Date" DataField="MODON" DataFormatString="{0:dd-MMM-yyyy ddd}"/>
                                        <asp:BoundField HeaderText="End Date" DataField="MMODON" DataFormatString="{0:dd-MMM-yyyy ddd}"/>
                                        <asp:BoundField HeaderText="From Time" DataField="stime" />
                                        <asp:BoundField HeaderText="To Time" DataField="etime" />
                                        <asp:BoundField HeaderText="Total Days" DataField="ttl" />
                                        <asp:BoundField HeaderText="Status" DataField="S_NAME" />
                                        <asp:BoundField HeaderText="Employee Remarks" DataField="COMMENTS" />                                        
                                        <asp:BoundField HeaderText="Applied On" DataField="createdon" DataFormatString="{0:dd-MMM-yyyy hh:mm/ddd}"/>
                                         <asp:TemplateField HeaderText="" Visible="false">
                                   <ItemTemplate>
                                     <asp:Label ID="LBL_lvempid" runat="server" Text='<%# Eval("PERNR") %>'></asp:Label>
                                     </ItemTemplate>
                                         </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>


                            </div>

                            </div>


                        </div>
                       </div>
                      </div>
                    </div>
                 </div>
                </div>
    

                               
</asp:Content>
