<%@ Page Title="TimeSheet Overview" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true"
    CodeBehind="TimeSheetReview_Employees.aspx.cs" Inherits="iEmpPower.UI.Manager_Self_Service.TimeSheetReview_Employees"
    EnableEventValidation="false" Culture="en-GB" Theme="SkinFile" %>


<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls" TagPrefix="BDP" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Time Sheet Overview</li>
                    </ol>
                </div>
                <h4 class="page-title">Time Sheet Overview
                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg"></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <div class="header">
        <asp:Label ID="LblMsg" runat="server" CssClass="lblValidation"></asp:Label>
    </div>

    <div class="card-box">
        <div id="real_time_chart" class="dashboard-flot-chart">

            <div id="divbrdr" class="divfr">
                <div class="search-section">

                    <asp:Panel ID="pnlTimeSheetReview" runat="server">
                        <div>
                                <div class="form-group">
                                    <div class="row">                                        
                                        <div class="col-sm-12">    
                         <asp:TextBox ID="TxtFromDate" runat="server" CssClass="txtDropDownwidth" placeholder="From Date" ValidationGroup="vg1" TabIndex="1"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RFV_TxtFromDate" runat="server" ControlToValidate="TxtFromDate" SetFocusOnError="true" ValidationGroup="vg1" ErrorMessage="*" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                         <Ajx:MaskedEditExtender ID="MEE_TxtFromDate" runat="server" AcceptNegative="Left" CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                         MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtFromDate" />
                         <Ajx:CalendarExtender ID="CE_TxtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="TxtFromDate"> </Ajx:CalendarExtender>                           
                          <%-- <asp:RegularExpressionValidator ID="REV_TxtFromDate" runat="server" Display="Dynamic" CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtFromDate" ValidationGroup="vg1"
                            SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                            &nbsp;&nbsp;
                            <asp:TextBox ID="TxtToDate" runat="server" CssClass="txtDropDownwidth" placeholder="To Date" ValidationGroup="vg1" TabIndex="2"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RFV_TxtToDate" runat="server" ControlToValidate="TxtToDate" ValidationGroup="vg1" ErrorMessage="*"
                             Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                             <Ajx:MaskedEditExtender ID="MEE_TxtToDate" runat="server" AcceptNegative="Left" CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                              MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" TargetControlID="TxtToDate" />
                              <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="TxtToDate"></Ajx:CalendarExtender>
                             <%-- <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic" CssClass="lblValidation" ErrorMessage="Invalid Date" ControlToValidate="TxtToDate" ValidationGroup="vg1"
                               SetFocusOnError="True" ValidationExpression="^((((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00)))))$" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                  &nbsp;&nbsp;
                                 <asp:DropDownList ID="DDL_srchtyp" runat="server" OnSelectedIndexChanged="DDL_srchtyp_SelectedIndexChanged" AutoPostBack="true">
                                     <asp:ListItem value="0" Selected="True">All Records</asp:ListItem>
                                     <asp:ListItem value="1">Projects</asp:ListItem>
                                     <asp:ListItem value="2">WBS</asp:ListItem>
                                     <asp:ListItem value="3">Activity</asp:ListItem>
                                     <asp:ListItem value="4">Attendance</asp:ListItem>
                                 </asp:DropDownList>
                                 &nbsp;&nbsp;
                                 <asp:DropDownList ID="DDL_srchtyps" runat="server" Visible="false"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="REQ_ddlsrchtyps" runat="server" ControlToValidate="DDL_srchtyps" ValidationGroup="vg1" InitialValue="0" ErrorMessage="*"
                             Display="Dynamic" ForeColor="Red" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                 &nbsp;&nbsp;
                                 <asp:Button ID="btnDisplay" Width="80px" runat="server" TabIndex="3" Text="Display" OnClick="btnDisplay_Click" ValidationGroup="vg1" />
                                  <asp:Button ID="BtnClear"  Width="80px" runat="server" Text="Clear" CausesValidation="false" OnClick="BtnClear_Click" TabIndex="4" />
                                  <asp:Button ID="btnExcel" Width="80px" runat="server" TabIndex="5" Text="Export" Visible="false" OnClick="btnExcel_Click" />                                               
                               </div>
                                        <br />
                               <asp:CompareValidator ID="CV_TxtToDate" runat="server" ControlToCompare="TxtFromDate" ValidationGroup="vg1" CssClass="lblValidation" ControlToValidate="TxtToDate" Display="Dynamic" ErrorMessage="From date should be less than to date"
                               Operator="GreaterThanEqual" Type="Date" ForeColor="Red"></asp:CompareValidator>
                                            
                                       
                                    </div>

                                    <div class="col-md-12 text-right" id="divcnt" runat="server"></div>
                                </div> 
                            <asp:UpdatePanel ID="up" runat="server">
                             <ContentTemplate></ContentTemplate>
                              <Triggers>
                               <asp:PostBackTrigger ControlID="btnExcel" />
                               </Triggers>
                               </asp:UpdatePanel>
                           
                                <div class="respovrflw">
                                            <asp:GridView ID="grdTimeSheetReview" runat="server" AutoGenerateColumns="False" Width="100%" 
                                                OnRowDataBound="grdTimeSheetReview_RowDataBound" DataKeyNames="PERNR" ShowFooter="true" ShowHeaderWhenEmpty="false">
                                                <Columns>
                                                     <asp:TemplateField HeaderText="Slno">
                                                <ItemTemplate>
                                                   <asp:Label ID="lblRowNumber" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                    <asp:BoundField  HeaderText="Employee ID" DataField=""/>
                                                    <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />
                                                     <asp:BoundField DataField="PROJ" HeaderText="Project" />
                                                    <asp:BoundField DataField="WBS" HeaderText="Work Breakdown Structure(WBS)" />
                                                    <asp:BoundField DataField="Activity" HeaderText="Activity" />
                                                    <asp:BoundField DataField="Attd" HeaderText="Attendance Type" />
                                                    <asp:BoundField DataField="WORKINGDATE" HeaderText="Working Dates" />
                                                   <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />
                                                    <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                    <asp:Label ID="LBL_mysts" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate >
                                                    <div style="color:#00617c">
                                                        Total Hours&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: <br />
                                                    </div>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Hours">
                                             <ItemTemplate>
                                                 <asp:Label ID="lbl_hrs" runat="server" Text='<%# Eval("CATSHOURS") %>'></asp:Label>

                                                  </ItemTemplate>
                                       <FooterTemplate >
                                                    <div>
                                              <asp:Label ID="lbl_totalhrs" runat="server" Text="" ForeColor="#00617c"></asp:Label>
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

                    </asp:Panel>

                </div>
            </div>
        </div>
    </div>

           

</asp:Content>
