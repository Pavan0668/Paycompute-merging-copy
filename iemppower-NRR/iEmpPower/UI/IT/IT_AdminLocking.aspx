<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="IT_AdminLocking.aspx.cs"
    Inherits="iEmpPower.UI.IT.IT_AdminLocking" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
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

        #MainContent_PnlIT
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
    <style>
        body.enlarged {
            min-height: unset !important;
            max-height: 100vh !important;
        }

        table {
            position: relative !important;
            max-height: 50vh !important;
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
                        <li class="breadcrumb-item"><a href="Income_Tax.aspx">IT</a></li>
                        <li class="breadcrumb-item active">IT Lock</li>

                    </ol>
                </div>
                <h4 class="page-title">IT Lock</h4>
            </div>
        </div>
    </div>
    <!-- end page title -->
    <div class="row">
        <div class="col-lg-12">
            <div class="tab-content m-0 p-0">
                <div class="card-box">
                    <div class="responsive-table">
                        <%-- <div>
                            <asp:Panel ID="pnl" runat="server" DefaultButton="btnsearch">--%>
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="row margin5rem">

                                    <div class="col-sm-5">
                                        <asp:CheckBox ID="cb_SearchEnable" runat="server" ToolTip="Select to enable search"
                                            Checked="false" AutoPostBack="true" OnCheckedChanged="cb_SearchEnable_CheckedChanged" Enabled="false" />
                                        <asp:TextBox ID="txtsearch" runat="server" CssClass="textbox" Width="150px" placeholder="Enter Emp ID" TabIndex="1" Enabled="false"></asp:TextBox>
                                        <asp:Button ID="btnsearch" runat="server" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="2" Text="Search" CssClass="btn-xs btn-secondary" Enabled="false" />
                                        <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" TabIndex="3" Text="Reload" CssClass="btn-xs btn-secondary" />
                                        <cc1:FilteredTextBoxExtender ID="txtsearch_FilteredTextBoxExtender" runat="server" Enabled="True" TargetControlID="txtsearch"
                                            FilterType="Numbers">
                                        </cc1:FilteredTextBoxExtender>
                                        <asp:Button ID="btnexpXL" OnClick="btnexpXL_Click" runat="server" Text="Export to Excel" TabIndex="6" CssClass="btn-xs btn-secondary" />
                                    </div>
                                    <div class="col-sm-7 text-right">
                                        <asp:LinkButton runat="server" ToolTip="Download Excel Template" ID="lnkExcelTepDwnld" OnClick="lnkExcelTepDwnld_Click">&nbsp;&nbsp;&nbsp;<i class="dripicons-download"></i></asp:LinkButton>
                                        <asp:FileUpload ID="uflEmpData" runat="server" AllowMultiple="false" />
                                        <asp:Button ID="btnExcelProceed" runat="server" Text="Upload Excel" ToolTip="Upload the excel file, click save to confirm the changes" OnClick="btnExcelProceed_Click" TabIndex="7" CssClass="btn-xs btn-secondary" />
                                        <%--<asp:Button ID="btnMark" runat="server" Text="Mark All" ToolTip="Lock all Employees, click save to confirm the changes" OnClick="btnMark_Click" TabIndex="4" CssClass="btn-xs btn-secondary" />
                                                <asp:Button ID="btnUnmark" runat="server" Text="UnMark All" ToolTip="Unlock all Employees, click save to confirm the changes" OnClick="btnUnmark_Click" TabIndex="5" CssClass="btn-xs btn-secondary" />
                                        --%>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Save" ToolTip="Confirm the changes" OnClick="btnSubmit_Click" TabIndex="6" CssClass="btn-xs btn-secondary" />

                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnExcelProceed" />
                                <asp:PostBackTrigger ControlID="btnexpXL" />
                                <asp:PostBackTrigger ControlID="lnkExcelTepDwnld" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <%--  </asp:Panel>
                        </div>--%>
                        <div>
                            <asp:Label ID="LblMsg" runat="server"></asp:Label>
                        </div>
                        <div class="DivSpacer01"></div>
                        <asp:Panel ID="PnlIT" runat="server">
                            <%--<asp:GridView ID="grdLocking" runat="server" AutoGenerateColumns="false"  CssClass="gridviewNew" GridLines="None" HeaderStyle-CssClass="Divh">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="col-center" />
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Emp ID" DataField="PERNR" ControlStyle-CssClass="col-center" />

                                    <asp:BoundField HeaderText="Emp Name" DataField="ENAME" />


                                    <asp:TemplateField HeaderText="Lock" ControlStyle-CssClass="col-center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="CB_Lock" runat="server"
                                                Checked='<%# Bind("ITLOCK") %>' />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="col-center" />
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>--%>


                            <asp:GridView ID="grdLocking" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" HeaderStyle-CssClass="Divh"
                                DataKeyNames="PERNR" OnRowDataBound="grd_Locking_RowDataBound" OnRowCommand="grdLocking_RowCommand">


                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No." ControlStyle-CssClass="col-center">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                            <%--       <%# Eval("SLNO") %>--%>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="col-center" />
                                    </asp:TemplateField>


                                    <asp:BoundField HeaderText="Emp ID" DataField="PERNR" ControlStyle-CssClass="col-center" />


                                    <asp:BoundField HeaderText="Emp Name" DataField="ENAME" />



                                    <asp:TemplateField ControlStyle-CssClass="col-center">
                                        <HeaderTemplate>
                                            <%--<asp:CheckBox ID="CB_LockHeader" runat="server" ToolTip="Lock IT for all Employees, click save to confirm the changes"
                                                Checked="false" AutoPostBack="true" OnCheckedChanged="CB_LockHeader_CheckedChanged" />--%>
                                            <asp:CheckBox ID="CB_LockHeader" runat="server" ToolTip="Lock IT for all Employees, click save to confirm the changes"
                                                Checked="false" />
                                            Lock IT
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<asp:CheckBox ID="CB_Lock" runat="server" Checked='<%# Bind("ITLOCK") %>' AutoPostBack="true" OnCheckedChanged="CB_Lock_CheckedChanged" />--%>
                                            <asp:CheckBox ID="CB_Lock" runat="server" CssClass="CssItLock" Checked='<%# Bind("ITLOCK") %>' />
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
                                    <asp:TemplateField HeaderText="" >
                                        <HeaderTemplate>
                                            <%--  <asp:CheckBox ID="CB_l80Header" runat="server" ToolTip="Lock Sec 80 for all Employees, click save to confirm the changes"
                                                Checked="false" AutoPostBack="true" OnCheckedChanged="CB_l80Header_CheckedChanged" />--%>
                                            <asp:CheckBox ID="CB_l80Header" runat="server" ToolTip="Lock Sec 80 for all Employees, click save to confirm the changes"
                                                Checked="false" />
                                            Consider Actuals Sec 80
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<asp:CheckBox ID="CB_CA80" runat="server" Checked='<%# Bind("CA80") %>' AutoPostBack="true" OnCheckedChanged="CB_CA80_CheckedChanged" />--%>
                                            <asp:CheckBox ID="CB_CA80" runat="server" Checked='<%# Bind("CA80") %>'  />
                                        </ItemTemplate>
                                        <%-- <ItemStyle CssClass="col-center" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Consider Actuals Sec 80C" ControlStyle-CssClass="col-center">
                                        <HeaderTemplate>
                                           <%-- <asp:CheckBox ID="CB_l80cHeader" runat="server" ToolTip="Lock Sec 80C for all Employees, click save to confirm the changes"
                                                Checked="false" AutoPostBack="true" OnCheckedChanged="CB_l80cHeader_CheckedChanged" />--%>
                                             <asp:CheckBox ID="CB_l80cHeader" runat="server" ToolTip="Lock Sec 80C for all Employees, click save to confirm the changes"
                                                Checked="false"  />
                                            Consider Actuals Sec 80C
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<asp:CheckBox ID="CB_CA80c" runat="server" Checked='<%# Bind("CA80C") %>' AutoPostBack="true" OnCheckedChanged="CB_CA80c_CheckedChanged" />--%>
                                            <asp:CheckBox ID="CB_CA80c" runat="server" Checked='<%# Bind("CA80C") %>' />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="col-center" />
                                    </asp:TemplateField>
                                </Columns>


                            </asp:GridView>








                        </asp:Panel>

                        <br />
                    </div>
                </div>
            </div>


            <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
            <script type="text/javascript">
                $("[id*=grdLocking] input[type=checkbox]").click(function () {

                    var _Row = $(this).closest('tr');
                    if (!$(_Row).first('th').hasClass('Divh')) {
                        var _EmpID, _LockIT, _Sec80, _Sec80C;
                        _EmpID = $("td", _Row).eq(1).html();

                        _LockIT = $("td", _Row).eq(3).find('input[type=checkbox]').is(":checked");
                        _Sec80 = $("td", _Row).eq(5).find('input[type=checkbox]').is(":checked");
                        _Sec80C = $("td", _Row).eq(6).find('input[type=checkbox]').is(":checked");
                        //alert(_EmpID +'-'+_LockIT + '-' + _Sec80 + '-' + _Sec80C);
                        $.ajax({
                            type: "POST",
                            url: 'IT_AdminLocking.aspx/UpdateUserLock',
                            data: '{UserID:"' + _EmpID + '",LockIT:"' + _LockIT + '",Sec80:"' + _Sec80 + '",Sec80C:"' + _Sec80C + '"}',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (response) {
                                alert(' IT Lock Updated successfully !! \n Employee Id : ' + _EmpID);
                            },
                            failure: function (response) {
                                //alert('hi fail');
                            },
                            error: function (response) {
                                //alert('hi error - ' + response.d);
                            }

                        });
                    }
                });

                //------------------------------------------------------             
                $("[id*=CB_LockHeader]").live("click", function () {
                    var ChkHeader = $(this);
                    var grid = $(this).closest("table");
                    $("tr td:nth-child(4) input[type=checkbox]", grid).each(function () {
                        //alert($(this).attr('id'));
                        $(this).prop("checked", $(ChkHeader).prop("checked"))
                    });
                });
                //------------------------------------------------------
                $("[id*=CB_l80Header]").live("click", function () {
                    var ChkHeader = $(this);
                    var grid = $(this).closest("table");
                    $("tr td:nth-child(6) input[type=checkbox]", grid).each(function () {
                        //alert($(this).attr('id'));
                        $(this).prop("checked", $(ChkHeader).prop("checked"))
                    });
                });
                //------------------------------------------------------
                $("[id*=CB_l80cHeader]").live("click", function () {
                    var ChkHeader = $(this);
                    var grid = $(this).closest("table");
                    $("tr td:nth-child(7) input[type=checkbox]", grid).each(function () {
                        //alert($(this).attr('id'));
                        $(this).prop("checked", $(ChkHeader).prop("checked"))
                    });
                });
            </script>
</asp:Content>
