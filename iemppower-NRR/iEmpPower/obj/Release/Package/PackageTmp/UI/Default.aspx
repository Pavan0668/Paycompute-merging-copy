<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.Master" AutoEventWireup="true" Culture="auto" EnableEventValidation="false" CodeBehind="Default.aspx.cs" 
    UICulture="auto" Theme="SkinFile" Inherits="iEmpPower.UI.Default"  MaintainScrollPositionOnPostback="true"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">
        function ShowHideDiv(cbUser) {
            var dvUser = document.getElementById("dvUser");
            dvUser.style.display = cbUser.checked ? "block" : "none";

            var dvApprover = document.getElementById("dvApprover");
            dvApprover.style.display = cbUser.checked ? "none" : "block";
        }
        function favrte(a) {
            //alert(a);
            document.getElementById("<%=HF_FavData.ClientID%>").value = a;
            //alert(document.getElementById("<%=HF_FavData.ClientID%>").value);
            document.getElementById("<%=btnFavData.ClientID%>").click();
        }
    </script>




    <style>
        .btn-group a span
        {
            padding-top: 7px!important;
            vertical-align: middle !important;
        }

        .dispnone
        {
            display: none;
        }

        .tilehover p
        {
            color: #1bc4f7 !important;
        }



        .tilehover:hover p
        {
            transform: scale(1.05,1.05);
            color: #3827c1 !important;
        }

        .close
        {
            background-color: white !important;
            border: none !important;
            font-size: small;
        }




        .centerprogress img
        {
            /*height: 40px;*/
            /*width: 128px;*/
        }

        .popUpStyle
        {
            /*font: normal 11px auto "Trebuchet MS", Verdana;*/
            background-color: #000000;
            /*color: #4f6b72;*/
            /*padding: 6px;*/
            filter: alpha(opacity=80);
            opacity: 0.15;
        }


        .repeater
        {
            margin-top: 5px;
            float: right;
        }


        .grida td:nth-child(1)
        {
            width: 2%;
        }




        .grida td:nth-child(2)
        {
            width: 5%;
        }




        /*.grida td:nth-child(3)
        {
            width: 20%;
        }
*/



        .grida td:nth-child(4)
        {
            width: 20% !important;
        }




        .grida td:nth-child(5)
        {
            width: 25% !important;
        }




        .grida td:nth-child(6)
        {
            width: 5%;
        }




        .grida td:nth-child(7)
        {
            width: 5% !important;
            /*text-align:center;*/
        }



        .grida td:nth-child(8)
        {
            text-align: center;
            /*width: 5%;*/
        }





        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }


        .modalPopupDefault
        {
            text-align: left;
            background-color: #FFFFFF;
            /* border-width: 3px;
            border-style: solid;
            border-color: black;
            padding: 10px;*/
            /*padding-left: 10px;*/
            width: auto;
            height: 80vh;
            overflow: auto;
        }
    </style>


    <div class="row">
        <div class="col-6">
            <div class="page-title-box">
                <h4 class="page-title">Dashboard</h4>
            </div>
        </div>
        <div class="col-6" id="togglemgr" runat="server">
            <div class="page-title-box page-title text-left">

                <label class='toggle-label'>
                    <asp:CheckBox ID="cbUser1" runat="server" AutoPostBack="true" OnCheckedChanged="cbUser1_CheckedChanged" />
                    <span class='back'>
                        <span class='toggle'></span>
                        <span class='label on'>Me as User</span>
                        <span class='label off'>Me as Approver</span>
                    </span>
                </label>

            </div>
        </div>
    </div>

    <asp:HiddenField ID="HFmethdLoader" runat="server" />
    <asp:HiddenField ID="HFsortId" runat="server" />
    <asp:HiddenField ID="HF_FavData" runat="server" />
    <div class="hidden">
        <asp:Button ID="btnFavData" runat="server" OnClick="btnFavData_Click" />
    </div>

    <div id="dvUser" runat="server">
        <div id="plTile1" runat="server" class="rmvpaddng">
        </div>
        <!-- More bottun -->
        <div class="text-right" id="divMore" runat="server">
            <a class="waves-effect waves-light text-blue collapsed" data-toggle="collapse" href="#collapseExample" aria-expanded="false" aria-controls="collapseExample">
                <i class="fe-more-horizontal"></i>More </a>
        </div>

        <div class="row">
            <div class="col-xl-12">

                <div class="collapse" id="collapseExample" style="">
                    <div id="pnlTileEmpCols" runat="server" class="rmvpaddng">
                    </div>
                </div>

            </div>
        </div>

        <div class="row">

            <ul id="Ul1" class="nav nav-pills navtab-bg" style="margin-left: 6px;" runat="server">
                <li class="nav-item font-16">
                    <asp:LinkButton runat="server" ID="LbtTabPending" class="nav-link active p-2" OnClick="LbtTabPending_Click"><i class="fe-alert-circle"></i>
   Pending </asp:LinkButton></li>
                <li class="nav-item font-16">
                    <asp:LinkButton runat="server" ID="LbtTabComplt" class="nav-link  p-2" OnClick="LbtTabComplt_Click"><i class="fe-check-circle"></i>
   Completed </asp:LinkButton></li>
            </ul>


            <div class="col-xl-12 m-t-20">
                <div class="card-box" id="cbox" runat="server">
                    <div class="tab-content p-0">

                        <!-- Pendding panel Start -->
                        <div class="tab-pane show active" id="pending" runat="server">
                            <asp:HiddenField runat="server" ID="HF_TBLTYPE" />
                            <asp:HiddenField runat="server" ID="HF_ID" />
                            <asp:Label ID="lblMsgPending" runat="server" ForeColor="Red"></asp:Label>

                            <div class="row">
                                <h4 class="header-title mb-3 col-sm-6">The below shown request's need approval by your supervisor</h4>
                            </div>
                            <div class="row">
                                <div class=" col-sm-1 ">
                                    <asp:DropDownList runat="server" ID="DDLYear" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="offset-7 col-sm-4 text-right" id="divsortfilterUPend" runat="server">

                                    <div class="btn-group mb-2">
                                        <h5 class="mr-3">Created On :</h5>

                                        <asp:LinkButton ID="lnkbtnTodayUPpending" runat="server" class="btn btn-xs btn-secondary" OnClick="btnTodayUPpending_Click"><span>Today</span></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnYesterdayUPpending" runat="server" class="btn btn-xs btn-light" OnClick="btnYesterdayUPpending_Click"><span>Yesterday</span></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtnTwoDaysUPpending" runat="server" class="btn btn-xs btn-light" OnClick="btnTwoDaysUPpending_Click"><span>Older</span></asp:LinkButton>
                                    </div>
                                </div>

                            </div>


                            <div class="respovrflw">

                                <asp:GridView ID="grdPending" runat="server" DataKeyNames="PERNR,PKEY,CHANGE_APPROVAL,REVIEW,LAST_ACTIVITY_DATE,ID,TableTyp"
                                    AllowPaging="True" OnPageIndexChanging="grdPending_PageIndexChanging"
                                    OnSorting="grdPending_Sorting" AutoGenerateColumns="False"
                                    AllowSorting="True" PageSize="10"
                                    OnRowCommand="grdPending_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="RowNumber" HeaderText="Sl No." ReadOnly="True" />
                                        <asp:BoundField DataField="CHANGE_APPROVAL" HeaderText="Type" ReadOnly="True" />
                                        <asp:BoundField DataField="Subtype" HeaderText="Subtype" ReadOnly="True"
                                            SortExpression="Subtype" />
                                        <asp:TemplateField HeaderText="Pending With">
                                            <ItemTemplate> <%--<%#Eval("AppPERNR") %> -   --%>
                                               <%#Eval("AppByName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="LAST_ACTIVITY_DATE" HeaderText="Last Activity Date" ReadOnly="True"
                                            SortExpression="LAST_ACTIVITY_DATE" meta:resourcekey="BoundFieldResource6" />
                                        <asp:BoundField DataField="REVIEW" HeaderText="Status" ReadOnly="True"
                                            SortExpression="REVIEW" meta:resourcekey="BoundFieldResource5" />
                                        <asp:TemplateField HeaderText="Quick View">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="grdPendingView" runat="server" CausesValidation="false" CommandName="VIEW"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>
                                                &nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <div class="row col-md-12" runat="server" id="divpageNcnt">
                                    <div class="col-md-3" style="margin-top: 5px" id="divpendingrecordcount" runat="server"></div>
                                    <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                        <asp:Repeater ID="RptrPendingPager" runat="server">
                                            <ItemTemplate>
                                                <ul class="pagination pagination-rounded" style="display: inline-block">
                                                    <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                        <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Click" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>


                            </div>

                        </div>
                        <!-- end Pending panel -->

                        <!-- Start completed panel -->
                        <div class="tab-pane" id="completed" runat="server">

                            <div class="row">
                                <h4 class="header-title mb-3 col-sm-6">The below shown request's are approved by your supervisor recently</h4>
                            </div>
                            <div class="row">
                                <div class=" col-sm-1">
                                    <asp:DropDownList runat="server" ID="DDLYearcomp" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDLYearcomp_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="offset-7 col-sm-4 text-right" id="divsortfilterUcompd" runat="server">
                                    <div class="btn-group mb-2">
                                        <h5 class="mr-3">Created On :</h5>

                                        <asp:LinkButton ID="lnkbtncompToday" runat="server" class="btn btn-xs btn-secondary" OnClick="btnTodayUPpending_Click"><span>Today</span></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtncompyYestrdy" runat="server" class="btn btn-xs btn-light" OnClick="btnYesterdayUPpending_Click"><span>Yesterday</span></asp:LinkButton>
                                        <asp:LinkButton ID="lnkbtncompTwodayb4" runat="server" class="btn btn-xs btn-light" OnClick="btnTwoDaysUPpending_Click"><span>Older</span></asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="table-responsive">

                                <asp:Label ID="lblComplrec" runat="server" ForeColor="Red"></asp:Label>

                                <asp:GridView ID="grdCompleted" runat="server" AutoGenerateColumns="False" DataKeyNames="PERNR,PKEY,CHANGE_APPROVAL,REVIEW,LAST_ACTIVITY_DATE,ID,TableTyp,MODIFIEDON"
                                    OnPageIndexChanging="grdCompleted_PageIndexChanging"
                                    OnSorting="grdCompleted_Sorting" AllowSorting="True" AllowPaging="True" PageSize="10"
                                    OnRowCommand="grdCompleted_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="RowNumber" HeaderText="Sl No." ReadOnly="True" />
                                        <asp:BoundField DataField="CHANGE_APPROVAL" HeaderText="Type" ReadOnly="True" SortExpression="CHANGE_APPROVAL" />
                                        <asp:BoundField DataField="Subtype" HeaderText="Subtype" ReadOnly="True" SortExpression="Subtype" />
                                        <asp:TemplateField HeaderText="Approved By">
                                            <ItemTemplate><%-- <%#Eval("AppPERNR") %> -  --%>
                                                <%#Eval("AppByName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="LAST_ACTIVITY_DATE" HeaderText="Last Activity Date" ReadOnly="True"
                                            SortExpression="LAST_ACTIVITY_DATE" meta:resourcekey="BoundFieldResource12" />

                                        <asp:BoundField DataField="REVIEW" HeaderText="Status" ReadOnly="True"
                                            SortExpression="REVIEW" meta:resourcekey="BoundFieldResource11" />

                                        <asp:TemplateField HeaderText="Quick View">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="grdCompletedView" runat="server" CausesValidation="false" CommandName="VIEW"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                    </Columns>


                                </asp:GridView>

                                <div class="row col-md-12" runat="server" id="divpageNcntcomp">
                                    <div class="col-md-3" style="margin-top: 5px" id="divUcompledCount" runat="server"></div>
                                    <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                        <asp:Repeater ID="RptrCompletedPager" runat="server">
                                            <ItemTemplate>
                                                <ul class="pagination pagination-rounded" style="display: inline-block">
                                                    <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                        <asp:LinkButton ID="lnkPage_Ucompletd" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="lnkPage_Ucompletd_Click" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>

                                <cc1:ModalPopupExtender BackgroundCssClass="popUpStyle" BehaviorID="ViewDetails1" ID="MPE_Compltd"
                                    runat="server" PopupControlID="divpopupcomp" TargetControlID="btnpopupcomp">
                                </cc1:ModalPopupExtender>

                                <button style="display: none;" id="btnpopupcomp" runat="server"></button>
                                <div id="divpopupcomp" runat="server" class="modalPopupDefault" align="center">
                                    <div class="modal-dialog modal-lg">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h4 class="modal-title" id="H2">Request Details </h4>
                                                <asp:Button ID="btncloseComp" class="close" data-dismiss="modal" aria-hidden="true" runat="server" Text="X" />
                                            </div>
                                            <div class="modal-body">
                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="Button3" class="close btn btn-light waves-effect" data-dismiss="modal" aria-hidden="true" runat="server" Text="Close" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <cc1:ModalPopupExtender BackgroundCssClass="popUpStyle" BehaviorID="ViewDetails" ID="MPE_Pend"
                            runat="server" PopupControlID="divpopup" TargetControlID="abc">
                        </cc1:ModalPopupExtender>

                        <button style="display: none;" id="abc" runat="server"></button>
                        <div id="divpopup" runat="server" class="modalPopupDefault" align="center">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title" id="H1">Request Details </h4>
                                        <asp:LinkButton ID="lnkbtnClose" runat="server" Text="X" CssClass="close" data-dismiss="modal" aria-hidden="true" CausesValidation="false" />
                                    </div>
                                    <div class="modal-body">

                                        <div class="respovrflw">
                                            <asp:GridView ID="GridViewDetails" runat="server" DataKeyNames="PKEY,ID,CHANGE_APPROVAL,STATUS" Visible="false" AutoGenerateColumns="false"
                                              OnRowCommand="GridViewDetails_RowCommand" OnRowDataBound="GridViewDetails_RowDataBound">
                                                <Columns>
                                                     <asp:BoundField HeaderText="Employee ID" DataField=""/>
                                                    <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />
                                                    <asp:BoundField DataField="PARTICULARS" HeaderText="Particulars" />
                                                    <asp:BoundField DataField="MODIFIEDON" HeaderText="Modified On" ReadOnly="True" />
                                                    <asp:BoundField DataField="STATUS" HeaderText="Status" ReadOnly="True" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="GridViewDetailsView" runat="server" CausesValidation="false" CommandName="VIEW"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="LBL_empid" runat="server" Text='<%# Eval("CREATED_BY") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField> 
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="respovrflw">
                                            <asp:GridView ID="GV_DashboardDetails" runat="server" AutoGenerateColumns="false" ShowHeader="false" AllowPaging="false" AllowSorting="false" OnRowDataBound="GV_DashboardDetails_RowDataBound">
                                                <Columns>

                                                    <asp:BoundField DataField="TEXT" HeaderText="Particulars" ItemStyle-Width="150" ItemStyle-Font-Bold="true" />
                                                    <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="60">
                                                        <ItemTemplate>
                                                            <b>:</b>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VALUES" HeaderText="Details" />

                                                </Columns>

                                            </asp:GridView>
                                        </div>

                                        <asp:Panel ID="pnlRecordWorking" runat="server">
                                            <div class="respovrflw">
                                                <asp:GridView ID="grdRecordTime" runat="server" ShowFooter="True" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Project">
                                                            <ItemTemplate>
                                                                <asp:Label ID="drpdwnCostCenter" runat="server"> 
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                             <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                             <ItemStyle Width="50px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WBS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="drpdwnOrder" runat="server"> 
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                            <ItemStyle Width="30px"></ItemStyle>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Activity" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtStaffGrade" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                            <ItemStyle Width="30px"></ItemStyle>
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblStaffGrade" runat="server" Text="Hours" CssClass="label" Width="40px"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>

                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attendance Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="drpdwnAttabsType" runat="server"> 
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>

                                                        </asp:TemplateField>


                                                        
                                                        <asp:TemplateField HeaderText="Hours" HeaderStyle-CssClass="hd-small"
                                                            HeaderStyle-Width="30">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTotal" runat="server" Width="30px" ReadOnly="True"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblHours" runat="server" CssClass="label" Width="30px" Style="text-align: center;"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>

                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SUN" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtSUN" runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblSun" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>

                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="MON" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtMON" runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblMon" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>

                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TUE" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTUE" runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblTues" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>

                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WED" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtWED" runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblWed" runat="server" CssClass="label" ></asp:Label>
                                                                </div>
                                                            </FooterTemplate>

                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="THU" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTHU" runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblThu" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>

                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FRI" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtFRI" runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblFri" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SAT" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtSAT" runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblSAt" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>

                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="REMARKS" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtREMARKS" runat="server"></asp:Label>

                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblREMARKS" runat="server" CssClass="label" Visible="false"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>

                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <br />
                                            <table id="tblRWT" runat="server">
                                                <tr>
                                                    <td>Remarks
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblValidateRWCommnets" runat="server" Text=""
                                                            CssClass="label"></asp:Label></td>
                                                </tr>
                                            </table>

                                        </asp:Panel>


                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end card-box-->
            </div>
            <!-- end row -->
        </div>
    </div>
    <%-- User Tab End--%>

    <%-- Approvar Tab Start--%>

    <div id="dvApprover" runat="server" visible="false">
        <div id="divTileApprv" runat="server" class="rmvpaddng">
        </div>

        <!-- More bottun -->
        <div class="text-right" id="divMoreApp" runat="server">
            <a class="waves-effect waves-light text-blue collapsed" data-toggle="collapse" href="#collapseExample1" aria-expanded="false" aria-controls="collapseExample">
                <i class="fe-more-horizontal"></i>More </a>
        </div>

        <div class="row">
            <div class="col-xl-12">

                <div class="collapse" id="collapseExample1" style="">
                    <div id="divTileCollApprv" runat="server" class="rmvpaddng">
                    </div>
                </div>

            </div>
        </div>
        <div class="row">
            <ul id="Ul2" class="nav nav-pills navtab-bg" style="margin-left: 6px;" runat="server">
                <li class="nav-item font-16">
                    <asp:LinkButton runat="server" ID="lnkbtnAppPending" class="nav-link active p-2" OnClick="lnkbtnAppPending_Click"><i class="fe-alert-circle"></i>
   Pending </asp:LinkButton></li>
                <li class="nav-item font-16">
                    <asp:LinkButton runat="server" ID="lnkbtnAppComplt" class="nav-link  p-2" OnClick="lnkbtnAppComplt_Click"><i class="fe-check-circle"></i>
   Completed </asp:LinkButton></li>
            </ul>

            <div class="col-xl-12 m-t-20">

                <div class="card-box">
                    <div class="tab-content p-0">

                        <!-- Pendding panel Start -->
                         <div class="tab-pane show active" id="pendingApp" runat="server">
                           <%-- <asp:UpdatePanel runat="server" ID="a">
                                <ContentTemplate>--%>
                                    <div class="row" style="margin-bottom:5px">
                                        <div class=" col-sm-6 text-left">
                                            <h4 class="header-title ">The below shown request's need your approval</h4>
                                        </div>
                                        <div class="offset-4 col-sm-2 text-right" >
                                            <asp:Button ID="btnviewRepts" runat="server" Text="View Reportees" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-1">
                                    <asp:DropDownList runat="server" ID="DDL_YearAppP" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDL_YearAppP_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="DDL_AppHLP" CssClass="txtDropDownwidth" runat="server" OnSelectedIndexChanged="DDL_AppHLP_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="offset-5 col-sm-4 text-right" id="filsortappP" runat="server">
                                            <div class="btn-group mb-2">
                                        <h5 class="mr-3">Created On :</h5>
                                        <asp:LinkButton ID="btnAppPendToday" runat="server" class="btn btn-xs btn-secondary" Text="Today" OnClick="btnTodayUPpending_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="btnAppPendYsterday" runat="server" class="btn btn-xs btn-light" Text="Yesterday" OnClick="btnYesterdayUPpending_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="btnAppPendTwodayb4" runat="server" class="btn btn-xs btn-light" Text="Older" OnClick="btnTwoDaysUPpending_Click"></asp:LinkButton>
                                    </div>

                                </div>
                            </div>



                            <div class="table-responsive">

                                <asp:HiddenField ID="HFCID" runat="server" />

                                <asp:Label ID="lblAppPending" runat="server" ForeColor="Red"></asp:Label>

                                <div class="respovrflw">
                                    <asp:GridView ID="grdAppPending" runat="server" DataKeyNames="PERNR,PKEY,ENAME,PLSXT,ID,TableTyp,REVIEW,CHANGE_APPROVAL,LAST_ACTIVITY_DATE,MMODON" 
                                        OnPageIndexChanging="grdAppPending_PageIndexChanging" AutoGenerateColumns="False" OnRowDataBound="grdAppPending_RowDataBound"
                                        OnRowCommand="grdAppPending_RowCommand" OnSorting="grdAppPending_Sorting" AllowSorting="true">
                                        <Columns>
                                             
                                            <asp:BoundField DataField="RowNumber" HeaderText="Sl No." />
                                            <asp:BoundField DataField="" HeaderText="Employee ID" ReadOnly="True" SortExpression="PERNR" />
                                            <asp:BoundField DataField="ENAME" HeaderText="Employee Name"
                                                SortExpression="ENAME" />
                                            <asp:BoundField DataField="CHANGE_APPROVAL" HeaderText="Type of Request" ReadOnly="True"
                                                SortExpression="CHANGE_APPROVAL" />
                                            <asp:BoundField DataField="Subtype" HeaderText="Request Subtype" ReadOnly="True"
                                                SortExpression="Subtype" />
                                            <asp:BoundField DataField="REVIEW" HeaderText="Review" ReadOnly="True"
                                                SortExpression="REVIEW" meta:resourcekey="BoundFieldResource5" />
                                            <asp:BoundField DataField="LAST_ACTIVITY_DATE" HeaderText="Last Activity Date" ReadOnly="True"
                                                SortExpression="LAST_ACTIVITY_DATE" meta:resourcekey="BoundFieldResource6" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <a data-toggle="tooltip" title="View Details">Action</a>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="grdPendingView" runat="server" CausesValidation="false" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="" Visible="false">
                                              <ItemTemplate>
                                                <asp:Label ID="LBL_empid" runat="server" Text='<%# Eval("PERNR") %>'></asp:Label>
                                                  </ItemTemplate>
                                                </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>



                                </div>

                                <div class="row col-md-12" id="divcountgridpnl" runat="server">
                                    <div class="col-md-3" style="margin-top: 5px" id="divcountgridApppend" runat="server"></div>
                                    <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                        <asp:Repeater ID="repeAppPending" runat="server">
                                            <ItemTemplate>
                                                <ul class="pagination pagination-rounded" style="display: inline-block">
                                                    <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>

                                                        <asp:LinkButton ID="lnk_repeAppPending" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="lnk_repeAppPending_Click" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>


                                    <cc1:ModalPopupExtender BackgroundCssClass="popUpStyle" BehaviorID="ViewDetails2" ID="ModalPopupExtender1"
                                        runat="server" PopupControlID="divviewRepts" TargetControlID="btnviewRepts" DropShadow="false">
                                    </cc1:ModalPopupExtender>

                                    <button style="display: none;" id="Button1" runat="server"></button>
                                    <div id="divviewRepts" runat="server" class="modalPopupDefault" align="center">
                                        <div class="modal-dialog modal-lg">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h4 class="modal-title" id="H4">Direct Reportees </h4>
                                                    <%--<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>--%>
                                                    <asp:LinkButton ID="LinkButton1" class="close" data-dismiss="modal" aria-hidden="true" runat="server" Text="X" />
                                                </div>
                                                <div class="modal-body">

                                                    <div class="respovrflw">
                                                        <asp:GridView ID="grdEmployeeSubOrdinates" runat="server" 
                                                            AllowPaging="false" AutoGenerateColumns="False" OnRowDataBound="grdEmployeeSubOrdinates_RowDataBound"
                                                            AllowSorting="True">
                                                            <Columns>
                                                                 <asp:TemplateField HeaderText="Sl NO.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                                <asp:BoundField DataField="" HeaderText="Employee ID" ReadOnly="True" SortExpression="PERNR" />
                                                                <asp:BoundField DataField="ENAME" HeaderText="Employee Name" ReadOnly="True"
                                                                    SortExpression="ENAME" />
                                                                <asp:BoundField DataField="PLSXT" HeaderText="Designation" ReadOnly="True" />
                                                                <asp:BoundField DataField="USRID" HeaderText="Email ID" ReadOnly="True"
                                                                    SortExpression="USRID" />
                                                                <asp:BoundField DataField="PHN" HeaderText="Phone No." ReadOnly="True" />
                                                                 
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
                                        </div>
                                    </div>

                                <%--</ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="DDL_YearAppP" />
                                    <asp:PostBackTrigger ControlID="DDL_AppHLP" />
                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </div>
                        <!-- end Pendding panel -->

                        <div class="tab-pane" id="completedApp" runat="server">
                            <%--<asp:UpdatePanel ID="b" runat="server">
                                <ContentTemplate>--%>
                            <div class="row">
                                <h4 class="header-title mb-3 col-sm-6">The below shown request's are approved by you recently</h4>
                            </div>
                            <div class="row">
                                <div class="col-sm-1">
                                    <asp:DropDownList runat="server" ID="DDL_APPYear" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDL_APPYear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:DropDownList ID="DDL_HRTabSel" CssClass="txtDropDownwidth" runat="server" OnSelectedIndexChanged="DDL_HRTabSel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                               <div class="offset-5 col-sm-4 text-right" id="filerSrtAppCompd" runat="server">
                                    <div class="btn-group mb-2">
                                        <h5 class="mr-3">Created On :</h5>
                                        <asp:LinkButton ID="btnAppCmpToday" runat="server" class="btn btn-xs btn-secondary" Text="Today" OnClick="btnTodayUPpending_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="btnAppCmpYesterday" runat="server" class="btn btn-xs btn-light" Text="Yesterday" OnClick="btnYesterdayUPpending_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="btnAppCmpTwodayb4" runat="server" class="btn btn-xs btn-light" Text="Older" OnClick="btnTwoDaysUPpending_Click"></asp:LinkButton>
                                    </div>

                                </div>
                            </div>


                            <div class="table-responsive">
                                <asp:Label ID="lblAppCompl" runat="server" ForeColor="Red"></asp:Label>

                            </div>
                            <div class="respovrflw">
                                <asp:GridView ID="grdAppCompleted" runat="server" AutoGenerateColumns="False" DataKeyNames="PERNR,PKEY,USRID,ENAME,PLSXT,ID,TableTyp,REVIEW,LAST_ACTIVITY_DATE,MODIFIEDON,CHANGE_APPROVAL"
                                    OnPageIndexChanging="grdAppCompleted_PageIndexChanging" AllowSorting="true" OnRowDataBound="grdAppCompleted_RowDataBound"
                                    AllowPaging="True" OnRowCommand="grdAppCompleted_RowCommand" PageSize="10" OnSorting="grdAppCompleted_Sorting">
                                    <Columns>
                                        <asp:BoundField DataField="RowNumber" HeaderText="Sl No." />
                                        <asp:BoundField DataField="" HeaderText="Employee ID" ReadOnly="true" SortExpression="PERNR" />
                                        <asp:BoundField DataField="ENAME" HeaderText="Employee Name" SortExpression="ENAME" />
                                        <asp:BoundField DataField="CHANGE_APPROVAL" HeaderText="Type of Request" ReadOnly="True" SortExpression="CHANGE_APPROVAL" />
                                        <asp:BoundField DataField="Subtype" HeaderText="Request Subtype" ReadOnly="True" SortExpression="Subtype" />
                                        <asp:BoundField DataField="REVIEW" HeaderText="Review" ReadOnly="True" SortExpression="REVIEW" meta:resourcekey="BoundFieldResource11" />
                                        <asp:BoundField DataField="AppByName" HeaderText="Approved By" ReadOnly="true" SortExpression="AppByName" />
                                        <asp:BoundField DataField="LAST_ACTIVITY_DATE" HeaderText="Last Activity Date" ReadOnly="True" SortExpression="LAST_ACTIVITY_DATE" meta:resourcekey="BoundFieldResource12" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <a data-toggle="tooltip" title="View Details">Action</a>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="grdCompletedView" runat="server" CausesValidation="false" CommandName="VIEW"
                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>
                                                &nbsp;
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="LBL_empid" runat="server" Text='<%# Eval("PERNR") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>
                                


                            </div>
                            <div class="row col-md-12" id="divNoofEntriesappCompltedrow" runat="server">
                                <div class="col-md-3" style="margin-top: 5px" id="divNoofEntriesappComplted" runat="server"></div>
                                <div class="col-md-9 DivSpacer01 Div02 repeater text-xl-right">
                                    <asp:Repeater ID="repeAppCompl" runat="server">
                                        <ItemTemplate>
                                            <ul class="pagination pagination-rounded" style="display: inline-block">
                                                <li class='paginate_button page-item <%# Convert.ToBoolean(Eval("Enabled")) ? "" : "active" %>'>
                                                    <asp:LinkButton ID="lnk_repeAppCompl" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="lnk_repeAppCompl_Click" CssClass='page-link <%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                                </li>
                                                    </ul>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                    </div>
                               <%-- </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="DDL_APPYear" />
                                    <asp:PostBackTrigger ControlID="DDL_HRTabSel" />

                                </Triggers>
                            </asp:UpdatePanel>--%>
                        </div>

                        <cc1:ModalPopupExtender BackgroundCssClass="popUpStyle" BehaviorID="ViewDetails2" ID="MPE_AppPending"
                            runat="server" PopupControlID="divAppPending" TargetControlID="btnAppPending" DropShadow="false">
                        </cc1:ModalPopupExtender>

                        <button style="display: none;" id="btnAppPending" runat="server"></button>
                        <div id="divAppPending" runat="server" class="modalPopupDefault" align="center">
                            <div class="modal-dialog modal-lg">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h4 class="modal-title" id="H3">Request Details </h4>
                                        <asp:LinkButton ID="btnAppPendingclose" class="close" data-dismiss="modal" aria-hidden="true" runat="server" Text="X" />
                                    </div>
                                    <div class="modal-body">

                                        <div>

                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:GridView ID="grdappRecordTime" runat="server" ShowFooter="True" AutoGenerateColumns="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Project">
                                                            <ItemTemplate>
                                                                <asp:Label ID="drpdwnCostCenter" runat="server"> 
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                             <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                            <ItemStyle Width="50px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WBS" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="drpdwnOrder" runat="server"> 
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                            <ItemStyle Width="30px"></ItemStyle>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Activity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtStaffGrade" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                            <ItemStyle Width="30px"></ItemStyle>
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblStaffGrade" runat="server" Text="Hours" CssClass="label" Width="40px"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Attendance Type" ItemStyle-Width="30" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="drpdwnAttabsType" runat="server"> 
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                            <ItemStyle Width="30px"></ItemStyle>
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="Hours" HeaderStyle-CssClass="hd-small" HeaderStyle-Width="30">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTotal" runat="server" Width="30px" ReadOnly="True"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <FooterStyle HorizontalAlign="Center" />
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblHours" runat="server" CssClass="label" Width="40px" Style="text-align: center;"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SUN" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtSUN" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblSun" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MON" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtMON" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblMon" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TUE">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTUE" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblTues" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WED">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtWED" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblWed" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="THU">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtTHU" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblThu" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="FRI" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtFRI" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblFri" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SAT" HeaderStyle-CssClass="hd-small">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtSAT" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblSAt" runat="server" CssClass="label"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="REMARKS">
                                                            <ItemTemplate>
                                                                <asp:Label ID="txtREMARKS" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div>
                                                                    <asp:Label ID="lblREMARKS" runat="server" CssClass="label" Visible="false"></asp:Label>
                                                                </div>
                                                            </FooterTemplate>
                                                            <HeaderStyle CssClass="hd-small"></HeaderStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />


                                                <div class="form-group" id="RWTdiv" runat="server">

                                                    <div class="row">
                                                        <div class="col-sm-1"><span style="color:red">*</span>Remarks&nbsp;<b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtRWComments" runat="server" CssClass="txtDropDownwidth" TextMode="MultiLine"></asp:TextBox>

                                                            <asp:Label ID="Label1" runat="server" Text="" CssClass="lblValidation"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <table id="TblRemarks" runat="server">
                                                    <tr>
                                                        <td>&nbsp;Remarks&nbsp;:&nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="lblRemarksRWT" runat="server" Text="" CssClass="lblValidation"></asp:Label></td>
                                                    </tr>
                                                </table>

                                                <div class="row" id="RWTdivbtn" runat="server">
                                                    <div class="col-sm-4">
                                                        <asp:Button ID="btnRWApprove" runat="server" Text="Approve" Width="80px" OnClick="btnRWApprove_Click" />
                                                   
                                                        <asp:Button ID="btnRWReject" runat="server" Text="Reject" Width="80px" OnClick="btnRWReject_Click" />
                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </div>


                                        <asp:HiddenField runat="server" ID="HiddenField1" />
                                        <asp:HiddenField runat="server" ID="HiddenField2" />
                                        <asp:HiddenField runat="server" ID="HF_PKEY" />
                                        <asp:HiddenField runat="server" ID="HF_STS" />
                                         
                                        <div class="respovrflw">
                                            <asp:GridView ID="GridViewAppDetails" runat="server" DataKeyNames="PKEY,ID,CHANGE_APPROVAL,STATUS,MODON,MODIFIEDON,CREATED_BY" Visible="false" AutoGenerateColumns="false" 
                                                OnRowCommand="GridViewAppDetails_RowCommand" OnRowDataBound="GridViewAppDetails_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="" HeaderText="Employee ID" ReadOnly="True" />
                                                    <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />

                                                    <asp:BoundField DataField="PARTICULARS" HeaderText="Particulars" />
                                                    <asp:BoundField DataField="MODIFIEDON" HeaderText="Modified On" ReadOnly="True" />
                                                    <asp:BoundField DataField="STATUS" HeaderText="Status" ReadOnly="True" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="GridViewDetailsView" runat="server" CausesValidation="false" CommandName="VIEW"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>
                                                            &nbsp;
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                 <asp:Label ID="LBL_empid" runat="server" Text='<%# Eval("CREATED_BY") %>'></asp:Label>
                                                 </ItemTemplate>
                                         </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                         
                                        
                                         <div class="respovrflw">

                                            <div>
                                                <asp:GridView ID="GV_AppDashboardDetails" runat="server" AutoGenerateColumns="false" ShowHeader="false" AllowPaging="false"
                                                    ShowFooter="true" Visible="false"
                                                    OnRowCommand="GV_AppDashboardDetails_RowCommand" OnRowCancelingEdit="GV_AppDashboardDetails_RowCancelingEdit"
                                                    OnRowDataBound="GV_AppDashboardDetails_RowDataBound" OnRowDeleting="GV_AppDashboardDetails_RowDeleting"
                                                    OnRowEditing="GV_AppDashboardDetails_RowEditing" OnRowUpdating="GV_AppDashboardDetails_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particulars">
                                                            <ItemTemplate>
                                                                <%# Eval("TEXT") %>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Approver Remarks
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <b>:</b>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <b>:</b>
                                                            </FooterTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details">
                                                            <ItemTemplate>

                                                                <div class=""><%# Eval("VALUES") %></div>

                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <div class="form-group">
                                                                    <div><span style="color:red">*</span>
                                                                        <asp:TextBox ID="TxtGvRemarks" runat="server" Width="180px" CssClass="" TextMode="MultiLine" Columns="6" MaxLength="150" autocomplete="off" placeholder="Enter Remarks"
                                                                            Style="resize: none;"></asp:TextBox>

                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-3">
                                                                            <asp:Button ID="BtnGvReqApprove" runat="server" Width="70px" Text="Approve" CommandName="APPROVE"/>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                            <asp:Button ID="BtnGvReqReject" runat="server" Width="70px" Text="Reject" CommandName="REJECT"/>
                                                                        </div>
                                                                    </div>


                                                                </div>

                                                            </FooterTemplate>

                                                            <ItemStyle />
                                                            <FooterStyle VerticalAlign="Top" ForeColor="#333333" Font-Bold="false" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                               
                                            </div>
                                            <div style="margin: 20px 0 0 0;">
                                                <asp:GridView ID="GV_DashboardCompleatedDetails" runat="server" AutoGenerateColumns="false" ShowHeader="false" AllowPaging="false"
                                                    ShowFooter="false" AllowSorting="false"
                                                    OnRowCommand="GV_DashboardCompleatedDetails_RowCommand" OnRowCancelingEdit="GV_DashboardCompleatedDetails_RowCancelingEdit"
                                                    OnRowDataBound="GV_DashboardCompleatedDetails_RowDataBound" OnRowDeleting="GV_DashboardCompleatedDetails_RowDeleting"
                                                    OnRowEditing="GV_DashboardCompleatedDetails_RowEditing" OnRowUpdating="GV_DashboardCompleatedDetails_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Particulars">
                                                            <ItemTemplate>
                                                                <%# Eval("TEXT") %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" ItemStyle-Width="60">
                                                            <ItemTemplate>
                                                                <b>:</b>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Details">
                                                            <ItemTemplate>
                                                                <%# Eval("VALUES") %>
                                                            </ItemTemplate>
                                                            <ItemStyle />
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
                <!-- end card-box-->
            </div>
        </div>



    </div>
</asp:Content>








