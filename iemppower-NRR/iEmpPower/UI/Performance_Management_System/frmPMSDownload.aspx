<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="frmPMSDownload.aspx.cs"
    Inherits="iEmpPower.UI.Performance_Management_System.frmPMSDownload" Theme="SkinFile" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../../Scripts/tabcontent.js" type="text/javascript"></script>
    <style type="text/css">
        .TblCls {
            border-collapse: collapse;
        }

        .TblWidth {
            width: 100%;
        }

        .Fnt01 {
            font: normal normal normal 11px/22px Verdana,Arial,Helvetica,sans-serif;
            color: #474747;
        }

        .Td01 {
            width: 20%;
            padding: 2px 0 2px 5px;
        }

        .Td02 {
            width: 18px;
            text-align: center;
        }
    </style>
    <div>
        <div class="row">
            <div class="col-12">
                <div class="page-title-box">
                    <div class="page-title-right">
                        <ol class="breadcrumb m-0">
                            <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                            <li class="breadcrumb-item active">Employee &nbsp;Performance</li>
                        </ol>
                    </div>
                    <h4 class="page-title">Employee &nbsp;Performance 
                    <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
                    </h4>
                </div>
            </div>
        </div>

        <div class="row">
            <ul class="nav nav-pills navtab-bg" style="margin-left: 12px;">
                <li class="nav-item font-16">
                    <asp:LinkButton runat="server" ID="Tab1" class="nav-link p-2" OnClick="Tab1_Click" CausesValidation="false"><i class="fe-arrow-up-circle" ></i>
  Self Appraisal </asp:LinkButton></li>

                <li class="nav-item font-16">
                    <asp:LinkButton runat="server" ID="Tab2" class="nav-link  p-2" OnClick="Tab2_Click" CausesValidation="false"><i class="fe-file-text"></i>
    Manager&#39;s Review </asp:LinkButton></li>
                <li class="nav-item font-16">
                    <asp:LinkButton runat="server" ID="Tab3" class="nav-link p-2" OnClick="Tab3_Click" CausesValidation="false"><i class="fe-arrow-down-circle" ></i>
  Status </asp:LinkButton></li>
            </ul>

        </div>
        <br />

        <div class="card-box">
            <div class="divfr">
                <asp:Label ID="LblErrMsg" runat="server" Text=""></asp:Label>
                <div style="float: right">
                    <a href="../../UI/Performance_Management_System/EmailTemplates/Employee_End-User_Document.pdf" target="_blank">Help?</a>
                </div>



                <div id="view1" runat="server" visible="false">
                    <br />

                    <asp:Label ID="lblpanellock" runat="server" ForeColor="red"></asp:Label>
                    <asp:Panel ID="pnllock" runat="server">
                        <asp:Label ID="lblRatingValues" runat="server" Text="Rating Values: 1 - Unacceptable; 2 - Needs Improvement; 3 - Meets Expectation; 4 - Exceeds Expectations; 5 - Outstanding" Font-Bold="true" ForeColor="Black" BackColor="#ffccff"></asp:Label>
                        <br />
                        <br />

                        <h5 class="header-title">Self Appraisal</h5>
                        <asp:Label ID="LblEmpApprisalCnt" runat="server" Text="" CssClass="Fl"></asp:Label>


                        <asp:Label ID="lblloadgrid" runat="server" ForeColor="Red"></asp:Label>
                        <br />
                        <br />


                        <table border="1">
                            <tr>

                                <td>
                                    <asp:Label ID="lblname" runat="server" Text="Name"></asp:Label></td>

                                <td>
                                    <asp:TextBox ID="name" runat="server" ReadOnly="True"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Total Experience"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="totalexperience" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Relevant Experience"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="relevantexperience" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Date Of Joining"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="doj" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Module"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="module" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label8" runat="server" Text="Grade"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="grade" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Certified"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="certified" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" runat="server" Text="Next Review"></asp:Label></td>
                                <td>
                                    <asp:TextBox ID="nextreview" runat="server" ReadOnly="True"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />

                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <asp:GridView ID="grdrate" runat="server" AutoGenerateColumns="False" AllowSorting="True" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField DataField="SINO" HeaderText="Kpi id" SortExpression="SINO" />
                                        <%--<asp:BoundField DataField="PERNR" HeaderText="Employee ID" 
                        SortExpression="PERNR" />--%>
                                        <asp:BoundField DataField="ZKPI11" HeaderText="KPI"
                                            SortExpression="ZKPI11" />
                                        <asp:BoundField DataField="WVALUE" HeaderText="Weightage value"
                                            SortExpression="WVALUE" />
                                        <%-- <asp:BoundField DataField="Weightage_Rating" HeaderText="weightage_rating" 
                        SortExpression="weighted_rating" />--%>

                                        <asp:TemplateField HeaderText="Self rating(1-5)">

                                            <ItemTemplate>


                                                <%--                                        <asp:UpdatePanel ID="updatepnl" runat="server">
         <ContentTemplate>
             
             <cc1:Rating ID="Rating1" runat="server" AutoPostBack="true"
                 starcssclass="starrating"
                 filledstarcssclass="filledstar"
                 emptystarcssclass="emptystar"
                 waitingstarcssclass="waitingstar"
                
                 currentrating="1"
                 maximumrating="5" >


            
                
                  
<
                 </cc1:Rating>

             

                        </ContentTemplate>  
                   </asp:UpdatePanel>--%>

                                                <asp:TextBox ID="txtself" runat="server" AutoPostBack="true" OnTextChanged="txtself_TextChanged" MaxLength="3" BorderColor="Black" Enabled="true"></asp:TextBox>





                                                <%--<Ajx:FilteredTextBoxExtender ID="txtPhone_FilteredTextBoxExtender"
                    runat="server" Enabled="True" TargetControlID="txtself" FilterType="Custom" FilterMode="ValidChars" ValidChars="0123456789.">
                </Ajx:FilteredTextBoxExtender>
                                                --%>


                                                <asp:RequiredFieldValidator ID="rfvtxtself" runat="server" ErrorMessage="enter the required field" ControlToValidate="txtself" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>

                                                <%--  <asp:RegularExpressionValidator ID="revself" runat="server" ControlToValidate="txtself" ValidationExpression=" \d+\.\d[0-5]" SetFocusOnError="true" Display="Dynamic" ErrorMessage="Enter Rating Between 1 to5" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Please Enter Rating Between 1 to5" ControlToValidate="txtself" SetFocusOnError="True" MaximumValue="5.0" MinimumValue="1.0" Type="double" ForeColor="Red" Display="Dynamic"></asp:RangeValidator>

                                                <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers, Custom"
                                                    ValidChars="." TargetControlID="txtself">
                                                </Ajx:FilteredTextBoxExtender>


                                            </ItemTemplate>

                                        </asp:TemplateField>









                                        <asp:TemplateField HeaderText="Remarks">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtremark" runat="server" MaxLength="250" BorderColor="Black"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtmremarks" runat="server" ErrorMessage="enter the required field" ControlToValidate="txtremark" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                                                <Ajx:FilteredTextBoxExtender ID="FTB_txtREMARKS" runat="server" TargetControlID="txtremark"
                                                    FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters"
                                                    ValidChars="./[]()&,\-@:;+=%$* ">
                                                </Ajx:FilteredTextBoxExtender>



                                            </ItemTemplate>
                                            

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Weighted rating">
                                            <ItemTemplate>
                                                <%--    <asp:TextBox ID="txtweightedrating" runat="server" BorderColor="White" ReadOnly="true"></asp:TextBox>--%>
                                                <asp:Label ID="lblrating" runat="server"></asp:Label>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotal" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>










                                        <%--                   <asp:BoundField DataField="WEIGHTED_RATING" HeaderText="WEIGHTED RATING" 
                        SortExpression="WEIGHTED_RATING" />--%>

                                        <%--  <asp:TemplateField HeaderText="Manager Rating" Visible="false">--%>

                                        <%--  <ItemTemplate>--%>
                                        <%--  <asp:TextBox ID="txtManage" runat="server" Visible="false"></asp:TextBox>--%>
                                        <%-- <asp:RequiredFieldValidator ID="rfvtxtrating" runat="server" ErrorMessage="enter the required field" ControlToValidate="txtManage" SetFocusOnError="true" ForeColor="Red" ></asp:RequiredFieldValidator>


                                        <asp:RangeValidator ID="Rangerating" runat="server" ErrorMessage="Please Enter Rating Between 1 to5" ControlToValidate="txtManage" SetFocusOnError="True"  MaximumValue="5.0" MinimumValue="1.0" Type="Double" ForeColor="Red" Display="Dynamic"></asp:RangeValidator>--%>
                                        <%--   </ItemTemplate>--%>
                                        <%--  </asp:TemplateField>--%>
                                        <%-- <asp:TemplateField HeaderText="Manager Remarks" Visible="false">--%>

                                        <%--  <ItemTemplate>--%>
                                        <%--  <asp:TextBox ID="txtmremarks" runat="server" Visible="false"></asp:TextBox>--%>

                                        <%-- <asp:RequiredFieldValidator ID="rfvtxtrating" runat="server" ErrorMessage="enter the required field" ControlToValidate="txtmremarks" SetFocusOnError="true" ForeColor="Red" ></asp:RequiredFieldValidator>--%>
                                        <%-- </ItemTemplate>--%>
                                        <%--  </asp:TemplateField>--%>
                                    </Columns>

                                </asp:GridView>


                                <br />




                                <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" OnClientClick="return confirm('Are you sure? Do you want to submit?' )" />

                                <asp:Label ID="lblsucess" runat="server" ForeColor="Red"></asp:Label>
                                <asp:Label ID="lblfailure" runat="server" ForeColor="Red"></asp:Label>



                                <%--  <div>
              <h5>Supervisor Comments</h5>
              <asp:TextBox ID="txtReviewer_Comments" TextMode="MultiLine" runat="server"></asp:TextBox>
          </div><br />
          <div>
              <h5>Reviewer Comments</h5>
              <asp:TextBox ID="TextBox1" TextMode="MultiLine" runat="server"></asp:TextBox>
                  </div>--%>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="Submit" />
                                <%--<asp:PostBackTrigger ControlID="txtself" ></asp:PostBackTrigger>--%>
                                <%-- <asp:PostBackTrigger ControlID="txtself" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>



                <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                <div id="view2" runat="server" visible="false">
                    <br />
                    <h5 class="header-title">Manager&#39;s Review</h5>
                    <br />
                    <br />
                    <asp:Label ID="lblinfo" runat="server" Text="Rating Values: 1 - Unacceptable; 2 - Needs Improvement; 3 - Meets Expectation; 4 - Exceeds Erxpectations; 5 - Outstanding" Font-Bold="true" ForeColor="Black" BackColor="#ffccff"></asp:Label>
                    <br />
                    <br />

                    <asp:Label ID="LblMngrApprisalCnt" runat="server" Text="" CssClass="Fl"></asp:Label>





                    <asp:GridView ID="Grdmngrating" runat="server" AutoGenerateColumns="False" AllowSorting="True" DataKeyNames="id,PERNR">


                        <Columns>

                            <asp:BoundField DataField="Employee_Name" HeaderText="EmployeeName"
                                SortExpression="Employee_Name" />

                            <asp:BoundField DataField="submitted_on" HeaderText="Submitted on"
                                SortExpression="submitted_on" />
                            <asp:BoundField DataField="status1" HeaderText="Status"
                                SortExpression="status" />

                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                <HeaderTemplate>
                                    <a data-toggle="tooltip" title="View Details">Action</a>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnPendingView" runat="server" OnClick="btnPendingView_Click" CausesValidation="false" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    <asp:Label ID="lblloadmanager" runat="server" ForeColor="Red"></asp:Label>
                    <br />





                    <div>
                        <asp:GridView ID="Grd_mng" runat="server" ShowFooter="true" DataKeyNames="SINO,id">

                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="true" SortExpression="id" />

                                <asp:BoundField DataField="SINO" HeaderText="Kpi id" SortExpression="SINO" />
                                <asp:BoundField DataField="ZKPI11" HeaderText="KPI"
                                    SortExpression="ZKPI11" />
                                <asp:BoundField DataField="WVALUE" HeaderText="Weightage value"
                                    SortExpression="WVALUE" />

                                <asp:BoundField DataField="SELF_RATING" HeaderText="Self rating(1-5)"
                                    SortExpression="SELF_RATING" />
                                <asp:BoundField DataField="SELF_REMARKS" HeaderText="Self remarks"
                                    SortExpression="SELF_REMARKS" />



                                <asp:TemplateField HeaderText="Manager rating(1-5)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtManage" runat="server" OnTextChanged="txtManage_TextChanged" AutoPostBack="true" Visible="true" MaxLength="3" BorderColor="Black"></asp:TextBox>


                                        <asp:RequiredFieldValidator ID="rfvtxtManage" runat="server" ErrorMessage="enter the required field" ControlToValidate="txtManage" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>


                                        <asp:RangeValidator ID="rvtxtManage" runat="server" ErrorMessage="Please Enter Rating Between 1 to5" ControlToValidate="txtManage" ForeColor="Red" SetFocusOnError="true" Display="Dynamic" MinimumValue="1.0" MaximumValue="5.0" Type="Double"></asp:RangeValidator>
                                 
                                 
                                        <Ajx:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers, Custom"
                                            ValidChars="." TargetControlID="txtManage">
                                        </Ajx:FilteredTextBoxExtender>
                                   </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manager remarks">
                                    <ItemTemplate>


                                        <asp:TextBox ID="txtmremarks" runat="server" Visible="true" MaxLength="250" BorderColor="Black"></asp:TextBox>
   <asp:RequiredFieldValidator ID="rfvtxtmremarks" runat="server" ErrorMessage="enter the required field" ControlToValidate="txtmremarks" SetFocusOnError="true" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>

                                        <%-- <asp:RegularExpressionValidator ID="Rgxmanager" runat="server" ErrorMessage="Pleae Enter Valid Text" ControlToValidate="txtmremarks" SetFocusOnError="True"  forecolor="Red" validationexpression="([A-Za-z])+( [A-Za-z]+)*"></asp:RegularExpressionValidator>
                                    
                                        --%>
                                
                                   
                                        <Ajx:FilteredTextBoxExtender ID="FTB_txtREMARKS" runat="server" TargetControlID="txtmremarks"
                                            FilterMode="ValidChars" FilterType="Numbers,Custom,UppercaseLetters,LowercaseLetters"
                                            ValidChars="./[]()&,\-@:;+=%$* ">
                                        </Ajx:FilteredTextBoxExtender>

                                        </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Weighted rating">
                                    <ItemTemplate>

                                        <asp:Label ID="lblweighrating" runat="server" Text=""></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>




                            </Columns>

                        </asp:GridView>
                        <br />

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server"  UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="managerupdate" Text="Submit" runat="server" visible="false" OnClick="managerupdate_Click" OnClientClick=" return confirm('Are you sure? Do you want to submit?' )"  />
                                <asp:Label ID="lblupdate" runat="server" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="managerupdate" />
                                <%--<asp:PostBackTrigger ControlID="txtself" ></asp:PostBackTrigger>--%>
                                <%-- <asp:PostBackTrigger ControlID="txtself" />--%>
                            </Triggers>
                        </asp:UpdatePanel>


                    </div>
                </div>

                <div id="view3" runat="server" visible="false">
                    <br />
                    <h5 class="header-title">Status</h5>
                    <asp:Label ID="status" runat="server" Text="" CssClass="Fl"></asp:Label>
                    <br />
                    <br />


                    <asp:UpdatePanel ID="pnlParent" runat="server">
                        <ContentTemplate>



                            <asp:DropDownList ID="ddlstatus" runat="server" CssClass="txtDropDownwidth" TabIndex="1">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFV_DDLEmpList" runat="server" ControlToValidate="ddlstatus" Display="Dynamic" CssClass="lblValidation"
                                ErrorMessage="Please select data for type" ForeColor="Red" InitialValue="---Select---" ValidationGroup="vg1"></asp:RequiredFieldValidator>

                            <asp:Button ID="Display" runat="server" Text="Display" ValidationGroup="vg1" OnClick="Display_Click" />


                            <asp:Button ID="btnExport" runat="server" Text="Export to Excel"
                                OnClick="btnExport_Click" />
                            <asp:Button ID="btnexportall" runat="server" Text="Export all Employee"
                                OnClick="btnexportall_Click" />
                           



                            <br />
                            <br />
                            <br />

                            <asp:GridView ID="GV_Report" runat="server" DataKeyNames="id">

                                <Columns>

                                    <asp:BoundField DataField="PERNR" HeaderText="Employee ID"
                                        SortExpression="PERNR" />
                                    <asp:BoundField DataField="Employee_Name" HeaderText="Employee Name"
                                        SortExpression="Employee_Name" />

                                    <asp:BoundField DataField="submitted_on" HeaderText="Submitted On"
                                        SortExpression="submitted_on" />
                                    <asp:BoundField DataField="status1" HeaderText="Status"
                                        SortExpression="status" />
                                    <asp:BoundField DataField="Approved_on" HeaderText="Approved On"
                                        SortExpression="Approved_on" />

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <a data-toggle="tooltip" title="View Details">Action</a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnStatus" runat="server" OnClick="btnStatus_Click" CausesValidation="false" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>

                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />

                            <div>
                                <asp:GridView ID="Gridstatus" runat="server" DataKeyNames="SINO,id" ShowFooter="true">
                                    <%-- <asp:GridView ID="Grd_mng" runat="server">--%>
                                    <Columns>
                                        <asp:BoundField DataField="PERNR" HeaderText="Employee Id"
                                            SortExpression="PERNR" />
                                        <asp:BoundField DataField="Employee_Name" HeaderText="Employee Name"
                                            SortExpression="Employee_Name" />
                                        <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="true" SortExpression="id" />

                                        <asp:BoundField DataField="SINO" HeaderText="Kpi id" SortExpression="SINO" />
                                        <asp:BoundField DataField="ZKPI11" HeaderText="KPI"
                                            SortExpression="ZKPI11" />

                                        <asp:BoundField DataField="SELF_RATING" HeaderText="Self rating"
                                            SortExpression="SELF_RATING" />
                                        <asp:BoundField DataField="SELF_REMARKS" HeaderText="Self remarks"
                                            SortExpression="SELF_REMARKS" />
                                        <asp:BoundField DataField="WVALUE" HeaderText="Weightage value"
                                            SortExpression="WVALUE" />

                                        <asp:BoundField DataField="MANAGER_RATING" HeaderText="Manager rating"
                                            SortExpression="MANAGER_RATING" />

                                        <asp:BoundField DataField="MANAGER_REMARKS" HeaderText="manager remarks"
                                            SortExpression="MANAGER_REMARKS" />
                                        <asp:BoundField DataField="WEIGHTED_RATING" HeaderText="Weighted rating"
                                            SortExpression="WEIGHTED_RATING" />

                                    </Columns>

                                </asp:GridView>
                                <br />

                            </div>
                            <asp:GridView ID="excelgrid" runat="server" DataKeyNames="SINO,id" ShowFooter="true">
                                <%-- <asp:GridView ID="Grd_mng" runat="server">--%>
                                <Columns>
                                    <asp:BoundField DataField="PERNR" HeaderText="Employee Id" ReadOnly="true" SortExpression="PERNR" />
                                    <asp:BoundField DataField="Employee_Name" HeaderText="Employee Name"
                                        SortExpression="Employee_Name" />
                                    <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="true" SortExpression="id" />



                                    <asp:BoundField DataField="SINO" HeaderText="Kpi id" SortExpression="SINO" />
                                    <asp:BoundField DataField="ZKPI11" HeaderText="KPI"
                                        SortExpression="ZKPI11" />




                                    <asp:BoundField DataField="SELF_RATING" HeaderText="Self rating"
                                        SortExpression="SELF_RATING" />
                                    <asp:BoundField DataField="SELF_REMARKS" HeaderText="Self remarks"
                                        SortExpression="SELF_REMARKS" />
                                    <asp:BoundField DataField="WVALUE" HeaderText="Weightage value"
                                        SortExpression="WVALUE" />



                                    <asp:BoundField DataField="MANAGER_RATING" HeaderText="Manager rating"
                                        SortExpression="MANAGER_RATING" />



                                    <asp:BoundField DataField="MANAGER_REMARKS" HeaderText="manager remarks"
                                        SortExpression="MANAGER_REMARKS" />
                                    <asp:BoundField DataField="WEIGHTED_RATING" HeaderText="Weighted rating"
                                        SortExpression="WEIGHTED_RATING" />



                                </Columns>



                            </asp:GridView>

                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnExport" />
                            <asp:PostBackTrigger ControlID="btnexportall" />
                           

                        </Triggers>
                    </asp:UpdatePanel>

                </div>

            </div>











            <div class="DivSpacer01">
            </div>
        </div>
    </div>






    <%--<script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>--%>
    </div>



</asp:Content>
