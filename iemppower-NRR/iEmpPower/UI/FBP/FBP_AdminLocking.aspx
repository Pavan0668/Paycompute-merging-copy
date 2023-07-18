<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="FBP_AdminLocking.aspx.cs"
    Inherits="iEmpPower.UI.FBP.FBP_AdminLocking" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
         #MainContent_grdLocking
        {
             
            /*overflow: scroll !important;*/
            overflow-y:scroll !important;
            overflow-x:hidden !important;
        }

        #MainContent_PnlFBP
        {
            overflow-y:scroll !important;
            overflow-x:hidden !important;
            /*overflow: scroll !important;*/
        }

        .DivSpacer01
        {
            width: 100% !important;
        }

      
    </style>
</asp:Content>--%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .popUpStyle1 {
            /*font: normal 11px auto "Trebuchet MS", Verdana;*/
            background-color: #000000;
            /*color: #4f6b72;*/
            /*padding: 6px;*/
            filter: alpha(opacity=80);
            opacity: 0.15;
        }


        .modalPopupDefault3 {
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



        .close {
            background-color: white !important;
            border: none !important;
            font-size: small;
        }

        body.enlarged {
            min-height: unset !important;
            max-height: 150vh !important;
        }

        table {
            position: relative !important;
            max-height: 80vh !important;
            overflow-x: hidden !important;
        }

        tr th {
            position: sticky !important;
            top: 0 !important;
            background-color: white !important;
            /*position:sticky !important;*/
        }
    </style>
    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <%--	<li class="breadcrumb-item"><a href="index.html">Home</a></li>
	<li class="breadcrumb-item"><a href="iExpense_Request.html">iExpense Request</a></li>
	<li class="breadcrumb-item active">iExpense View</li>--%>


                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <%--<li class="breadcrumb-item"><a href="FBP.aspx">FBP</a></li>--%>
                        <li class="breadcrumb-item active">FBP Lock</li>

                    </ol>
                </div>
                <h4 class="page-title">FBP Lock</h4>
            </div>
        </div>
    </div>
    <!-- end page title -->
    <div class="row">
        <div class="col-lg-12">
            <div class="tab-content m-0 p-0">
                <div class="card-box">
                    <div class="responsive-table">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>


                                <div class="row margin5rem">

                                    <%-- <div class="col-sm-2">
                                       
                                        <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" placeholder="Enter Emp ID" TabIndex="1" Enabled="false"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtsearch_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtsearch"
                                            FilterType="Numbers">
                                        </cc1:FilteredTextBoxExtender>



                                    </div>--%>
                                    <div class="col-sm-6">
                                        <asp:CheckBox ID="cb_SearchEnable" runat="server" ToolTip="Select to enable search"
                                            Checked="false" AutoPostBack="true" OnCheckedChanged="cb_SearchEnable_CheckedChanged" Enabled="false" />

                                        <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" Width="150px" placeholder="Enter Emp ID" TabIndex="1" Enabled="false"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="txtsearch_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtsearch"
                                            FilterType="Numbers">
                                        </cc1:FilteredTextBoxExtender>



                                        <asp:Button ID="btnsearch" ToolTip="Search for Employee based on ID" CssClass="btn-xs btn-secondary" runat="server" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="2" Text="Search" Enabled="false" />
                                        <asp:Button ID="btnclear" ToolTip="Reload" runat="server" CssClass="btn-xs btn-secondary" OnClick="btnclear_Click" TabIndex="3" Text="Reload" />
                                        <asp:Button ID="btnCheck" runat="server" ToolTip="Check Employees who have not declared FBP for current FY" Text="Not Declared" OnClick="btnCheck_Click" TabIndex="7" CssClass="btn-xs btn-secondary" />
                                        <asp:Button ID="btnexpXL" runat="server" Text="Export" TabIndex="6" CssClass="btn-xs btn-secondary" OnClick="btnexpXL_Click" />
                                        <%--<button id="BulkLockUnlock" name="BulkLockUnlock" type="button" class="btn-xs btn-secondary">Lock / Unlock</button>--%>


                                    </div>
                                    <%--<div class="col-sm-5 text-right">--%>
                                    <div class="col-sm-6 text-right">
                                        <asp:LinkButton runat="server" ToolTip="Download Excel Template" ID="lnkExcelTepDwnld" OnClick="lnkExcelTepDwnld_Click"><i class="dripicons-download"></i></asp:LinkButton>
                                        <asp:FileUpload ID="uflEmpData" runat="server" AllowMultiple="false" />
                                        <asp:Button ID="btnExcelProceed" runat="server" Text="Upload Excel" ToolTip="Upload the excel file, click save to confirm the changes" OnClick="btnExcelProceed_Click" TabIndex="7" CssClass="btn-xs btn-secondary" />
                                        <%--<asp:Button ID="btnMark" runat="server" ToolTip="Lock all Employees, click save to confirm the changes" Text="Mark All" OnClick="btnMark_Click" TabIndex="4" CssClass="btn-xs btn-secondary" />
                                        <asp:Button ID="btnUnmark" runat="server" ToolTip="Unlock all Employees, click save to confirm the changes" Text="UnMark All" OnClick="btnUnmark_Click" TabIndex="5" CssClass="btn-xs btn-secondary" />
                                        --%>
                                        <asp:Button ID="btnSubmit" runat="server" ToolTip="Confirm the changes" Text="Save" OnClick="btnSubmit_Click" TabIndex="6" CssClass="btn-xs btn-secondary" />


                                    </div>


                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExcelProceed" />
                                <asp:PostBackTrigger ControlID="lnkExcelTepDwnld" />
                                <asp:PostBackTrigger ControlID="btnexpXL" />

                            </Triggers>
                        </asp:UpdatePanel>
                        <div>
                            <asp:Label ID="LblMsg" runat="server"></asp:Label>
                        </div>
                        <div class="DivSpacer01"></div>
                        <%--  <h5 style="text-align: left; color: black;">
                         <asp:CheckBox ID="CB_ConsAct2" runat="server" Text="Consider "  TabIndex="1" CssClass="checkbox checkbox-info" /></h5>--%>

                        <asp:Panel ID="PnlFBP" runat="server">
                            <asp:GridView ID="grdLocking" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" HeaderStyle-CssClass="Divh"
                                DataKeyNames="PERNR" OnRowDataBound="grd_Locking_RowDataBound" OnRowCommand="grdLocking_RowCommand">
                                <%-- ShowFooter="true" FooterStyle-CssClass="foo01" PageSize="15" AllowPaging="true" 
        OnPageIndexChanging="grd_Locking_PageIndexChanging">--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No." ControlStyle-CssClass="col-center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                            <%--       <%# Eval("SLNO") %>--%>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Emp ID" DataField="PERNR" />

                                    <asp:BoundField HeaderText="Emp Name" DataField="ENAME" />


                                    <asp:TemplateField HeaderText="Lock" ControlStyle-CssClass="col-center" HeaderStyle-Width="10%">
                                        <HeaderTemplate>
                                            <%--<asp:CheckBox ID="CB_LockHeader" runat="server" ToolTip="Lock FBP for all Employees, click save to confirm the changes"
                                                Checked="false" AutoPostBack="true" OnCheckedChanged="CB_LockHeader_CheckedChanged" />--%>
                                            <asp:CheckBox ID="CB_LockHeader" runat="server" ToolTip="Lock FBP for all Employees, click save to confirm the changes"
                                                Checked="false" />
                                            Lock FBP
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%-- <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                            <asp:CheckBox ID="CB_Lock" runat="server" Text=" " CssClass="" OnCheckedChanged="CB_Lock_CheckedChanged"
                                                Checked='<%# Bind("FBPLOCK") %>' AutoPostBack="true" />--%>
                                            <%--   </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="CB_Lock" />
                                                </Triggers>
                                            </asp:UpdatePanel>--%>

                                            <asp:CheckBox ID="CB_Lock" runat="server" Text=" " CssClass=""
                                                Checked='<%# Bind("FBPLOCK") %>' onclick='<%# string.Format("javascript: UpdateUserLock(\"{0}\",this.checked, this);",Eval("PERNR")) %>' />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="col-center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Slab Reset" ControlStyle-CssClass="col-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                            <asp:LinkButton ID="linkresetslab" runat="server" CommandName="RESET"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">(Reset)</asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="col-center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Mobile Info Reset">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobstatus" runat="server"></asp:Label>
                                            <div id="divMob" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        Mobile No. 1 :
                                                        <asp:Label ID="lblMob1" runat="server"></asp:Label>
                                                    </div>

                                                </div>
                                                <div class="row">

                                                    <div class="col-sm-12">
                                                        Mobile No. 2 :
                                                        <asp:Label ID="lblMob2" runat="server"></asp:Label>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-9 text-left">
                                                        <asp:LinkButton ID="linkresetMob" runat="server" CommandName="MOBRESET"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Reset</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="25%" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Car CC Info Reset" ControlStyle-CssClass="col-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCCstatus" runat="server"></asp:Label>
                                            <div id="divCC" runat="server">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        Vehicle CC :
                                                        <asp:Label ID="lvlVCC" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        RC Number :
                                                        <asp:Label ID="lblRCNum" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        Name (As on RC) :
                                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        Registered Date :
                                                        <asp:Label ID="lblRdt" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <asp:UpdatePanel runat="server">
                                                        <ContentTemplate>
                                                            <div class="col-sm-12">
                                                                Rc File :
                                                        <asp:Label ID="lblRDwnld" runat="server" CssClass="hidden"></asp:Label>

                                                                <asp:LinkButton ID="lnkRDwnld" runat="server" CommandName="CCDWLD"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fe-download"></i></asp:LinkButton>

                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="lnkRDwnld" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="row ">
                                                    <div class="col-sm-9 text-left">
                                                        <asp:LinkButton ID="linkresetCC" runat="server" CommandName="CCRESET"
                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">Reset</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="25%" />
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </asp:Panel>
                        <%-- <div class="DivSpacer01" id="LeavePager" runat="server">
            <asp:Repeater ID="RptrPager" runat="server">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="Page_Changed" CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:Repeater>
             
        </div>--%>
                        <br />
                    </div>
                </div>
            </div>







            <cc1:ModalPopupExtender BackgroundCssClass="popUpStyle1" BehaviorID="ViewDetails" ID="MPE_Pend"
                runat="server" PopupControlID="divpopup" TargetControlID="abc">
            </cc1:ModalPopupExtender>



            <button style="display: none;" id="abc" runat="server"></button>
            <div id="divpopup" runat="server" class="modalPopupDefault3" align="center" style="width: 40% !important; height: 75% !important; overflow-y: hidden">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header" style="padding-top: 2px !important">
                            <h4 class="modal-title" id="H1">FBP Not Declared Employees </h4>
                            <asp:UpdatePanel ID="upclose" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btnClose" runat="server" Text="x" CssClass="close" data-dismiss="modal" aria-hidden="true" CausesValidation="false" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-body" style="width: 100% !important">
                            <div class="row" style="width: 100% !important">



                                <asp:GridView ID="grd_Notdeclared" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" HeaderStyle-CssClass="Divh" DataKeyNames="PERNR">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="col-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Employee ID" DataField="PERNR" />
                                        <asp:BoundField HeaderText="Employee Name" DataField="ENAME" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            function UpdateUserLock(UserID, ChkBx, ChkBxID) {
                $.ajax({
                    type: "POST",
                    url: 'FBP_AdminLocking.aspx/UpdateUserLock',
                    data: '{UserID:"' + UserID + '",Status:"' + ChkBx + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        alert(ChkBx == true ? 'Locked Successfully!!' : 'UnLocked Successfully!!');

                        if (ChkBx) {
                            $(ChkBxID).attr("checked", "checked");
                        }
                        else {
                            $(ChkBxID).removeAttr("checked");
                        }
                    },
                    failure: function (response) {
                        //alert('hi fail');
                    },
                    error: function (response) {
                        //alert('hi error - ' + response.d);
                    }
                });
            }

            $("[id*=CB_LockHeader]").live("click", function () {
                var chkHeader = $(this);
                var grid = $(this).closest("table");
                $("input[type=checkbox]", grid).each(function () {

                    if (chkHeader.is(":checked")) {
                        $(this).prop("checked", true);
                    } else {
                        $(this).prop("checked", false);
                    }
                });
            });

            $("[id*=BulkLockUnlock]").click(function () {
                if ($("[id*=CB_LockHeader]").is(":checked")) {


                    var message = '';
                    $("[id*=grdLocking] input[type=checkbox]").each(function () {
                        var row = $(this).closest("tr")[0];
                        if (!$(row).hasClass('Divh')) {
                            message += row.cells[1].innerHTML + '~' + $(this).is(":checked") + ';';
                        }

                    });
                    alert(message);
                }
                return false;
            });
        </script>
</asp:Content>
