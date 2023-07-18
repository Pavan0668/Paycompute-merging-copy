<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="Admin_salary.aspx.cs" 
    Inherits="iEmpPower.UI.SPaycompute.Admin_salary" UICulture="auto" Theme="SkinFile" MaintainScrollPositionOnPostback="true"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    </script>


    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Run Payroll</li>
                    </ol>
                </div>
                <h4 class="page-title">Run Payroll</h4>
            </div>
        </div>
    </div>

    <div class=" card-box">
            <div id="real_time_chart" class="dashboard-flot-chart">
                <div id="divbrdr" class="divfr">

   
            <div>

                <asp:Panel ID="PNL_upload_excel" runat="server">
                    <div class="header">
                        <div class="row clearfix">
                            <div class="col-xs-12 col-sm-12">
                                <span style="font-size: 25px">Generate Salary Details</span>

                            </div>
                        </div>
                    </div>




                    <div class="body">
                        <div class="row " style="padding-top: 10px; padding-bottom: 0; margin-bottom: 0; margin-left: 10px; margin-right: 7px">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="">
                                <div class="info-box" style="margin-bottom: 10px !important; margin-right: 10px !important; height: 50px;">
                                    <div class="content" style="margin-top: 10px !important;">
                                        <div class="" style="color: #555;">
                                            <div class="form-inline">
                                                <div class="form-group" style="align-content:center">

                                                    <div class="col-sm-4" style="width: 180px;">
                                                        <h5 style="margin-top: 2px !important;">Select Company Code: </h5>
                                                    </div>

                                                    <div class="col-sm-3" style="width: 210px;">
                                                        <asp:DropDownList ID="DDL_ADcompcode" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3" style="width: 210px;">
                                                        <asp:TextBox ID="txt_ADsalary_month" CssClass="txtDropDownwidth" Width="100%" runat="server"></asp:TextBox>
                                                        <%--<asp:RangeValidator ID="RNG_Smonth" runat="server" ControlToValidate="txt_salary_formonth" ValidationGroup="Upload" ForeColor="Red"></asp:RangeValidator>--%>
                                                        <Ajx:CalendarExtender ID="CE_txtBudgFrmMonth" runat="server" BehaviorID="CE_BE_txtBudgFrmMonth" Enabled="True" Format="MM-yyyy"
                                                            TargetControlID="txt_ADsalary_month" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown" PopupButtonID="txt_ADsalary_month"
                                                            DefaultView="Months">
                                                        </Ajx:CalendarExtender>
                                                        <asp:RequiredFieldValidator ID="REQ_ADmin_slrymonth" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="month" ControlToValidate="txt_ADsalary_month"></asp:RequiredFieldValidator>
                                                    </div>
                                                     <asp:UpdatePanel ID="UPD_Admin_salary_upload" runat="server">
                                                     <ContentTemplate>
                                                    <div class="col-sm-3" style="width: 210px;">
                                                        <asp:Button ID="btn_ADview_todownld" runat="server" Text="Generate" ValidationGroup="month" OnClick="btn_ADview_todownld_Click" />
                                                    </div>
                                                         </ContentTemplate>
                                                         <Triggers>
                                                             <asp:PostBackTrigger ControlID="btn_ADview_todownld" />
                                                         </Triggers>
                                                         </asp:UpdatePanel>

                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" id="disp_mnth" runat="server">
                            <div class="col-sm-3 htCr Cntrlwidth">Salary details for the month of :</div>
                            <div class="col-sm-4">
                                <asp:Label ID="LBL_ADbind_Smonth" runat="server"></asp:Label>
                            </div>
                        </div>


                        <div>
                            <asp:GridView ID="GV_Admin_Export" runat="server" AutoGenerateColumns="true" Width="100%"
                                CssClass="Grid" GridLines="Both" OnRowDataBound="GV_Admin_Export_RowDataBound">
                                <Columns>
                                   <%-- <asp:BoundField HeaderText="Emp ID/Ref No."/>
                                    <asp:BoundField HeaderText="Name"/>
                                    <asp:BoundField HeaderText="Col 1"/>
                                    <asp:BoundField HeaderText="Col 2"/>
                                    <asp:BoundField HeaderText="Col 3"/>
                                    <asp:BoundField HeaderText="Col 4"/>
                                    <asp:BoundField HeaderText="Col 5"/>
                                    <asp:BoundField HeaderText="Col 6"/>
                                    <asp:BoundField HeaderText="Col 7"/>
                                    <asp:BoundField HeaderText="Col 8"/>
                                    <asp:BoundField HeaderText="Col 9"/>
                                    <asp:BoundField HeaderText="Col 10"/>
                                    <asp:BoundField HeaderText="Col 11"/>
                                    <asp:BoundField HeaderText="Col 12"/>
                                    <asp:BoundField HeaderText="Col 13"/>
                                    <asp:BoundField HeaderText="Col 14"/>
                                    <asp:BoundField HeaderText="Col 15"/>
                                    <asp:BoundField HeaderText="Col 16"/>
                                    <asp:BoundField HeaderText="Col 17"/>
                                    <asp:BoundField HeaderText="Col 18"/>
                                    <asp:BoundField HeaderText="Col 19"/>
                                    <asp:BoundField HeaderText="Col 20"/>
                                    <asp:BoundField HeaderText="Remarks"/>--%>


                                </Columns>
                            </asp:GridView>
                            <br />
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                              <ContentTemplate>
                            <div>
                                <asp:Button ID="btn_exprt_slry" runat="server" Text="Export" Width="70px" OnClick="btn_exprt_slry_Click"/>
                                <asp:Button ID="btn_cncl_exprt" runat="server" Text="Cancel" Width="70px" OnClick="btn_cncl_exprt_Click"/>
                            </div>
                                 </ContentTemplate>
                                   <Triggers>
                                    <asp:PostBackTrigger ControlID="btn_exprt_slry" />
                                    <asp:PostBackTrigger ControlID="btn_cncl_exprt" />
                                     </Triggers>
                                      </asp:UpdatePanel>
                        </div>
                    </div>
                </asp:Panel>



                <asp:Panel ID="Admin_attendance" runat="server">
                    <div class="header">
                        <div class="row clearfix">
                            <div class="col-xs-12 col-sm-12">
                                <span style="font-size: 25px">Generate Monthly Attendance</span>

                            </div>
                        </div>
                    </div>



                        <div class="row " style="padding-top: 10px; padding-bottom: 0; margin-bottom: 0; margin-left: 10px; margin-right: 7px">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="">
                                <div class="info-box" style="margin-bottom: 10px !important; margin-right: 10px !important; height: 60px;">
                                    <div class="content" style="margin-top: 10px !important;">
                                        <div class="" style="color: #555;">
                                            <div class="form-inline">
                                                <div class="form-group" style="align-content:center">

                                                    <div class="col-sm-3" style="width: 180px;">
                                                        <h5 style="margin-top: 2px !important;">Select Company Code: </h5>
                                                    </div>

                                                    <div class="col-sm-3" style="width: 180px;">
                                                        <asp:DropDownList ID="DDL_Admin_attnds" CssClass="txtDropDownwidth" runat="server"></asp:DropDownList>
                                                    </div>

                                                      <div class="col-sm-3" style="width: 180px;">
                                    <asp:TextBox ID="txt_adattence_frmdate" CssClass="txtDropDownwidth"   Width="90%" runat="server"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CE_Frmdate_ADmi" PopupButtonID="txt_adattence_frmdate" runat="server" TargetControlID="txt_adattence_frmdate" Format="yyyy-MM-dd"/>                                   
                                                          <asp:RequiredFieldValidator ID="REQ_From_attdc" runat="server" Width="10%" ErrorMessage="*" ValidationGroup = "Date" ForeColor="Red" ControlToValidate="txt_adattence_frmdate" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                    </div>

                                                        <div class="col-sm-3" style="width: 180px;">
                                    <asp:TextBox ID="txt_attdn_todate" CssClass="txtDropDownwidth"   Width="90%" runat="server"></asp:TextBox> 
                                        <cc1:CalendarExtender ID="CE_Todate_Adm" PopupButtonID="txt_attdn_todate" runat="server" TargetControlID="txt_attdn_todate" Format="yyyy-MM-dd"/> 
                                         <asp:RequiredFieldValidator ID="REQ_Todate_attdce" Width="10%" runat="server" ErrorMessage="*" ValidationGroup = "Date" ForeColor="Red" ControlToValidate="txt_attdn_todate" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>                                  
                                     </div>
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                    <div class="col-sm-3" style="width: 120px;">
                                                        <asp:Button ID="btn_generate_attnd" runat="server" Text="Generate" ValidationGroup = "Date" OnClick="btn_generate_attnd_Click" />
                                                    </div>
                                                        </ContentTemplate>
                                                <Triggers>
                                                <asp:PostBackTrigger ControlID="btn_generate_attnd" />
                                                </Triggers>
                                                </asp:UpdatePanel>

                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div>
                             <asp:CompareValidator ID="cpre_attancedate_valid" ValidationGroup = "Date" ForeColor = "Red" runat="server" ControlToValidate = "txt_adattence_frmdate" ControlToCompare = "txt_attdn_todate" Operator = "LessThan" Type = "Date" ErrorMessage="From date must be less than To date." Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>

                        </div>
                        <div>
                            <div>
                            <asp:GridView ID="GV_viewattnce" runat="server" CssClass="Grid" AllowPaging="true" PageSize="10" GridLines="Both" AutoGenerateColumns="false" Width="100%" 
                               OnRowDataBound="GV_viewattnce_RowDataBound" OnPageIndexChanging="GV_viewattnce_PageIndexChanging">
                                <Columns>
                                     <asp:TemplateField HeaderText="Slno" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                    <asp:BoundField HeaderText="Emp ID/Ref No." DataField="EID" />
                                    <asp:BoundField HeaderText="Employee Name" DataField="TXT" />
                                    <asp:BoundField HeaderText="Number of days" DataField="ID" />
                                    <asp:BoundField HeaderText="Pay days" DataField="col6" />
                                </Columns>
                            </asp:GridView>
                            </div>

                            <asp:GridView ID="GV_admn_attence" runat="server" AutoGenerateColumns="false" Width="100%"
                                CssClass="Grid" AllowPaging="true" PageSize="10" GridLines="Both" OnPageIndexChanging="GV_admn_attence_PageIndexChanging" OnDataBound="GV_admn_attence_DataBound" OnRowDataBound="GV_admn_attence_RowDataBound">
                                <Columns>

                                    <asp:BoundField HeaderText="Emp ID/Ref No." DataField="EID" />
                                    <asp:BoundField HeaderText="Employee Name" DataField="TXT" />
                                    <asp:BoundField HeaderText="Col 1" DataField="ID" />
                                    <asp:BoundField HeaderText="Col 2" DataField="col6" />
                                    <asp:BoundField HeaderText="Col 3" />
                                    <asp:BoundField HeaderText="Col 4" />
                                    <asp:BoundField HeaderText="Col 5" />
                                    <asp:BoundField HeaderText="Col 6" />
                                    <asp:BoundField HeaderText="Col 7" />
                                    <asp:BoundField HeaderText="Col 8" />
                                    <asp:BoundField HeaderText="Col 9" />
                                    <asp:BoundField HeaderText="Col 10" />

                                </Columns>
                            </asp:GridView>
                            <br />
                            <br />
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                             <ContentTemplate>
                            <div>
                                <asp:Button ID="btn_exprt_excel_attnce" runat="server" Text="Export" Width="70px" OnClick="btn_exprt_excel_attnce_Click" />
                                <asp:Button ID="btn_cnclexprt_excel_attnce" runat="server" Text="Cancel" Width="70px" OnClick="btn_cnclexprt_excel_attnce_Click" />
                            </div>
                                 </ContentTemplate>
                                 <Triggers>
                                   <asp:PostBackTrigger ControlID="btn_exprt_excel_attnce" />
                                     <asp:PostBackTrigger ControlID="btn_cnclexprt_excel_attnce" />
                                   </Triggers>
                                   </asp:UpdatePanel>
                        </div>
                </asp:Panel>

            </div>
       
</asp:Content>
