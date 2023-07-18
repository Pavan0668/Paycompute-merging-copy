<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="iEmpPower.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $('.count').each(function () {
            $(this).prop('Counter', 0).animate({
                Counter: $(this).text()
            }, {
                duration: 4000,
                easing: 'swing',
                step: function (now) {
                    $(this).text(Math.ceil(now));
                }
            });
        });
    </script>

    <style type="text/css">
        .c1
        {
            background-color: #ef4437 !important;
            color: #fff !important;
        }

        .c2
        {
            background-color: #e71e63 !important;
            color: #fff !important;
        }

        .c3
        {
            background-color: #8f3e97 !important;
            color: #fff !important;
        }

        .c4
        {
            background-color: #65499d !important;
            color: #fff !important;
        }

        .c5
        {
            background-color: #4554a4 !important;
            color: #fff !important;
        }

        .c6
        {
            background-color: #1f83c5 !important;
            color: #fff !important;
        }

        .c7
        {
            background-color: #35a4dc !important;
            color: #fff !important;
        }

        .c8
        {
            background-color: #07bcd3 !important;
            color: #fff !important;
        }

        .c9
        {
            background-color: #009688 !important;
            color: #fff !important;
        }

        .c10
        {
            background-color: #43a047 !important;
            color: #fff !important;
        }

        .c11
        {
            background-color: #8bc34a !important;
            color: #fff !important;
        }

        .c12
        {
            background-color: rgba(0,0,0,0.8) !important;
            color: #fff !important;
        }

        .c13
        {
            background-color: #f8971d !important;
            color: #fff !important;
        }

        .c14
        {
            background-color: #B03A2E !important;
            color: #fff !important;
        }

        .c15
        {
            background-color: #f06291 !important;
            color: #fff !important;
        }

        .c16
        {
            background-color: #656464 !important;
            color: #fff !important;
        }

        .c17
        {
            background-color: #9A7D0A !important;
            color: #fff !important;
        }

        .c18
        {
            background-color: #ff6347 !important;
            color: #fff !important;
        }

        .c19
        {
            background-color: #e6c542 !important;
            color: #fff !important;
        }

        .c20
        {
            background-color: #c21807 !important;
            color: #fff !important;
        }

        .c21
        {
            background-color: #FF8880 !important;
            color: #fff !important;
        }

        .c22
        {
            background-color: #A9A9A9 !important;
            color: #fff !important;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="CompBlock" runat="server">
        <div class="block-header">
            <h2><b>COMPANY CONFIG</b></h2>
        </div>
        <div class="row clearfix">           

            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Configuration/addData.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c1">
                            <i class="material-icons">group_add</i>
                        </div>
                        <div class="content">
                            <div class="text">Add </div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Users</div>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Configuration/lock_lockuser.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c10">
                            <i class="material-icons">perm_data_setting</i>
                        </div>
                        <div class="content">
                            <div class="text">Company </div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Configuartion</div>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Configuration/UpdateEmpInfo.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c22">
                            <i class="material-icons">edit</i>
                        </div>
                        <div class="content">
                            <div class="text">Update User's  </div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Info</div>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/SPaycompute/Salary_Details.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c9">
                            <i class="material-icons">publish</i>
                        </div>
                        <div class="content">
                            <div class="text">Salary Details </div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Upload</div>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>
            </div>
        </div>

        <div class="row clearfix">

             <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/SPaycompute/Payc_Masters.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c3">
                            <i class="material-icons">note_add</i>
                        </div>
                        <div class="content">
                            <div class="text">Salary </div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Add Componenet</div>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/SPaycompute/Payc_Masters.aspx?timesheetmstrs=3">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c13">
                            <i class="material-icons">playlist_add</i>
                        </div>
                        <div class="content">
                            <div class="text">Timesheet </div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Add Masters</div>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>
            </div>


            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Payroll_Management/SalaryRegister.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c21">
                            <i class="material-icons">payment</i>
                        </div>
                        <div class="content">
                            <div class="text">Salary Register</div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">View</div>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Payroll_Management/EmployeesPayslip.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c8">
                            <i class="material-icons">account_balance_wallet</i>
                        </div>
                        <div class="content">
                            <div class="text">Employee's Payslip</div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">View</div>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>
            </div>

           
        </div>

    </asp:Panel>

    <asp:Panel ID="EmpBlock" runat="server">
        <div class="block-header">
            <h2><b>MY REQUESTS</b></h2>
        </div>
        <!-- Counter Examples -->
        <div class="row clearfix">
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Working_Time/leaverequest_new.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c1">
                            <i class="material-icons">access_alarm</i>
                        </div>
                        <div class="content">
                            <div class="text">Leave</div>
                            <%-- <a href="../../UI/Working_Time/leaverequest_new.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="125" data-speed="1000" data-fresh-interval="20">Apply</div>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>

            </div>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Working_Time/recordworking_time.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c2">
                            <i class="material-icons">access_time</i>
                        </div>
                        <div class="content">
                            <div class="text">Timesheet</div>
                            <%-- <a href="../../UI/PR/Purchase_Request.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="257" data-speed="1000" data-fresh-interval="20">Create</div>
                            <%-- </a>--%>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Manager_Self_Service/TimeSheetReview_Employees.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c3">
                            <i class="material-icons">history</i>
                        </div>
                        <div class="content">
                            <div class="text">Timesheet</div>
                            <div class="number count-to" data-from="0" data-to="1432" data-speed="1500" data-fresh-interval="20">Overview</div>
                        </div>
                    </div>
                </a>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Payroll_Management/Payslip_Emp.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c20">
                            <i class="material-icons">account_balance_wallet</i>
                        </div>
                        <div class="content">
                            <div class="text">Payslip</div>
                            <%--<a href="../../UI/Benefits_Payment/TravelClaimReq1.aspx">--%>
                            <div class="number count-to" data-from="0" data-to="1432" data-speed="1500" data-fresh-interval="20">View</div>
                            <%--</a>--%>
                        </div>
                    </div>
                </a>
            </div>

        </div>
    </asp:Panel>

    <asp:Panel ID="pnlApprvr" runat="server" Visible="false">
        <div class="block-header">
            <h2><b>APPROVE REQUESTS</b></h2>
        </div>
        <div class="row clearfix">
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Manager_Self_Service/assignedtome.aspx?id=Leave">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c8">
                            <i class="material-icons">alarm_on</i>
                        </div>
                        <div class="content">
                            <div class="text">Leave</div>
                            <%-- <a href="../../UI/Manager_Self_Service/assignedtome.aspx">--%>
                            <div id="LEAVE" runat="server">Action</div>
                            <%-- </a>class="numberPending"--%>
                        </div>
                    </div>
                </a>

            </div>
            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Manager_Self_Service/assignedtome.aspx?id=RWT">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c9">
                            <i class="material-icons">watch_later</i>
                        </div>
                        <div class="content">
                            <div class="text">Timesheet</div>
                            <%-- <a href="../../UI/PR/PR_ManagerAppRej.aspxschedule">--%>
                            <div id="RWT" runat="server">Action</div>
                            <%--</a>class="numberPending"--%>
                        </div>
                    </div>
                </a>
            </div>


            <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                <a href="../../UI/Manager_Self_Service/TimeSheetReview.aspx">
                    <div class="info-box hover-zoom-effect PointerHover">
                        <div class="icon c11">
                            <i class="material-icons">format_list_bulleted</i>
                        </div>
                        <div class="content">
                            <div class="text">EMP Timesheet</div>
                            <%-- <a href="../../UI/Benefits_Payment/TravelClaimAppRejNew.aspx">--%>
                            <%--  <div class="numberPending" id="TRAVEL" runat="server"></div>--%>
                            <div class="number count-to" data-from="0" data-to="1432" data-speed="1500" data-fresh-interval="20">View</div>
                            <%--  </a>--%>
                        </div>
                    </div>
                </a>
            </div>
        </div>
    </asp:Panel>


    <div class="block-header">
        <h2><b>MY ACCOUNT</b></h2>
    </div>
    <div class="row clearfix">
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" id="PernlInfo" runat="server">
            <a href="../../UI/Personal_Information/personal_data.aspx">
                <div class="info-box hover-zoom-effect PointerHover">
                    <div class="icon c14">
                        <i class="material-icons">face</i>
                    </div>
                    <div class="content">
                        <div class="text">Personal Info</div>
                        <%--    <a href="../../UI/Personal_Information/personal_data.aspx">--%>
                        <div class="number">View</div>
                        <%-- </a>--%>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <a href="../../UI/UserAccount/changepassword.aspx">
                <div class="info-box hover-zoom-effect PointerHover">
                    <div class="icon c15">
                        <i class="material-icons">settings</i>
                    </div>
                    <div class="content">
                        <div class="text">Change PWD.</div>
                        <%--<a href="../../UI/UserAccount/changepassword.aspx">--%>
                        <div class="number">Change</div>
                        <%-- </a>--%>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <a href="../../UI/UserAccount/createquestionandanswer.aspx">
                <div class="info-box hover-zoom-effect PointerHover">
                    <div class="icon c16">
                        <i class="material-icons">lock_open</i>
                    </div>
                    <div class="content">
                        <div class="text">Security Q & A</div>
                        <%--<a href="../../UI/UserAccount/createquestionandanswer.aspx">--%>
                        <div class="number">Set</div>
                        <%--  </a>--%>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12" id="dirct" runat="server">
            <a href="../../UI/Manager_Self_Service/who'swho.aspx">
                <div class="info-box hover-zoom-effect PointerHover">
                    <div class="icon c17">
                        <i class="material-icons">people</i>
                    </div>
                    <div class="content">
                        <div class="text">Directory</div>
                        <%-- <a href="../../UI/Manager_Self_Service/who'swho.aspx">--%>
                        <div class="number">Search</div>
                        <%--</a>--%>
                    </div>
                </div>
            </a>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
            <a href="../../UI/Document Management System/UserManual.aspx">
                <div class="info-box hover-zoom-effect PointerHover">
                    <div class="icon c18">
                        <i class="material-icons">description</i>
                    </div>
                    <div class="content">
                        <div class="text">iEmp User Manual</div>
                        <%-- <a href="../../UI/Manager_Self_Service/who'swho.aspx">--%>
                        <div class="number">View</div>
                        <%--</a>--%>
                    </div>
                </div>
            </a>
        </div>

    </div>

</asp:Content>
