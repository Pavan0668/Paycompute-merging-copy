<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="FBP_Balance.aspx.cs" Inherits="iEmpPower.UI.FBP.FBP_Balance"  EnableEventValidation="false" Culture="en-GB"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        table
        {
            position: relative !important;
            max-height: 50vh !important;
        }

        tr th
        {
            position: sticky !important;
            top: 0 !important;
            background-color: white !important;
            /*position:sticky !important;*/
        }



        #grdHRA {

            font-family: Arial, Helvetica, sans-serif;

            border-collapse: collapse;

        }

            #grdHRA td, #studentTable th {

                border: 1px solid #ddd;

                padding: 8px;

            }

            #grdHRA th {

                padding-top: 6px;

                padding-bottom: 6px;

                text-align: left;

                background-color: #4CAF50;

                color: white;

            }






    </style>
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Reports</li>
                    </ol>
                </div>
                <h4 class="page-title">Reports
                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <div class="row card-box">
        <div class="col-xl-12 m-t-20">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">

                        <div class="col-sm-1">Select Type </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control-file" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="0">-Select Report-</asp:ListItem>
                                <asp:ListItem Value="1">FBP Balance</asp:ListItem>
<%--                                <asp:ListItem Value="2">FBP Claims</asp:ListItem>
                                <asp:ListItem Value="3">Sec 80</asp:ListItem>
                                <asp:ListItem Value="4">Sec 80C</asp:ListItem>
                                <asp:ListItem Value="9">Sec 80 Actuals</asp:ListItem>
                                <asp:ListItem Value="10">Sec 80C Actuals</asp:ListItem>
                                <asp:ListItem Value="5">HRA</asp:ListItem>
                                <asp:ListItem Value="6">Prev. Employment</asp:ListItem>
                                <asp:ListItem Value="7">Income from Other Sources-Self Occupied</asp:ListItem>
                                <asp:ListItem Value="8">Income from Other Sources-Letout</asp:ListItem>
                                <asp:ListItem Value="11">IT Consolidated</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-9 text-right">
                            <h5 class="text-warning">Export process may take few minutes, depending on number of records!</h5>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddlType" />
                </Triggers>
            </asp:UpdatePanel>

            <table class="table table-sm table-borderless mb-0 table_font_sm">
                <tbody>
                    <tr>
                        <td colspan="4">
                            <%--<a href="Other_Reimbursements.aspx">
                                                    <button type="button" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right">
                                                        <i class="mdi mdi-plus"></i>Add New iExpense Request</button></a>--%>
                        </td>
                    </tr>

                    <tr class="border-top">
                        <td width="60" class="hidden">Show</td>
                        <td width="60" class="hidden">

                            <asp:DropDownList ID="ddlPagesizeEmp" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPagesizeEmp_SelectedIndexChanged">
                                <asp:ListItem Selected="True">10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="row" style="margin-top: 10px">
                                        <div class="col-sm-2">
                                            <asp:Button ID="btnExpXL" runat="server" OnClick="BtnExporttoXl_Click" Text="Export to Excel" CausesValidation="false" CssClass="btn btn-dark waves-effect waves-light btn-std" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExpXL" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <%--  <asp:Button ID="btnLastestRec" Text="Last Declaration Done" runat="server" CssClass="btn btn-xs btn-secondary" OnClick="" />--%>
                            <asp:HiddenField ID="HFselec" runat="server" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtfrmdt" runat="server" CssClass="form-control-file" placeholder="From Date" AutoPostBack="True" OnTextChanged="txtfrmdt_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFV_txtfrmdt" Display="Dynamic" Enabled="false" ForeColor="Red" runat="server" ControlToValidate="txtfrmdt" ErrorMessage="*"></asp:RequiredFieldValidator><br />
                            <br />
                            <asp:TextBox ID="txttodt" runat="server" CssClass="form-control-file" placeholder="To Date" AutoPostBack="True" OnTextChanged="txttodt_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFV_txttodt" Display="Dynamic" Enabled="false" ForeColor="Red" ControlToValidate="txttodt" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <cc1:CalendarExtender ID="CE_txtfrmdt" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                TargetControlID="txtfrmdt">
                            </cc1:CalendarExtender>
                            <cc1:CalendarExtender ID="CE_txttodt" runat="server" Enabled="True" Format="yyyy-MM-dd"
                                TargetControlID="txttodt">
                            </cc1:CalendarExtender>
                        </td>
                        <td width="104">Tickets Visibility</td>
                        <td width="250">
                            <div class="btn-group mb-2">

                                <asp:Button ID="btnCY" Text="Current Year" runat="server" CssClass="btn btn-xs btn-secondary" OnClick="btnCY_Click" />
                                <asp:Button ID="btnLY" Text="Last Year" runat="server" CssClass="btn btn-xs btn-light" OnClick="btnLY_Click" />
                                <asp:Button ID="btnAll" Text="All" runat="server" CssClass="btn btn-xs btn-light" OnClick="btnAll_Click" />
                                <div style="float: left">
                                    <asp:CheckBox ID="chkDeclared" Visible="false" AutoPostBack="true" runat="server" Text="Not Declared" OnCheckedChanged="chkDeclared_CheckedChanged" />
                                </div>
                            </div>
                        </td>
                        <td>
                            <div class="row">
                                <div class="col-sm-4">Search:</div>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" placeholder="Employee ID" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row margin5reml">
                                <div class="col-sm-4" style="margin-top: 5px">Emp Status:</div>
                                <div class="col-sm-8 margin5rem">
                                    <asp:DropDownList ID="ddlEmpSts" runat="server" CssClass="form-control-file" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpSts_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="1">Active</asp:ListItem>
                                        <asp:ListItem Value="2">Inactive</asp:ListItem>
                                        <asp:ListItem Value="3">All</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </td>
                        <%--<td width="300">--%>
                        <%-- <input type="text" class="form-control-file" placeholder="" aria-controls="">--%>


                        <%--  </td>--%>
                    </tr>
                </tbody>
            </table>
            <div id="div">
                <asp:GridView ID="grdFbpBalance" runat="server" CssClass="gridviewNew" GridLines="None" AllowPaging="false" OnPageIndexChanging="grdFbpBalance_PageIndexChanging"
                    ShowFooter="True" PageSize="10" FooterStyle-CssClass="foo01" OnRowDataBound="grdFbpBalance_RowDataBound" Visible="false">
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Left" />
                    <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                </asp:GridView>
        </div>
    </div>



    <script type="text/javascript">

        function ExportXL(grd, flnm) {

            $('#' + grd).table2excel({
                filename: flnm + ".xls",
                fileext: ".xls",
                preserveColors: true

            });

        }

    </script>

</asp:Content>
