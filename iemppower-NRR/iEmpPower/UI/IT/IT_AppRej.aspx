﻿<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="IT_AppRej.aspx.cs"
    Inherits="iEmpPower.UI.IT.IT_AppRej" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        .Initial {
            display: inline;
            padding: 7px 16px;
            margin: 0;
            margin-right: 3px;
            border-radius: 3px 3px 0 0;
            outline: none;
            color: black;
        }

        .Clicked {
            display: inline;
            padding: 7px 16px;
            margin: 0;
            margin-right: 3px;
            border-radius: 3px 3px 0 0;
            outline: none;
            background: white;
            color: black;
        }
    </style>

</asp:Content>--%>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .Initial
        {
            display: inline;
            padding: 7px 16px;
            margin: 0;
            margin-right: 3px;
            border-radius: 3px 3px 0 0;
            outline: none;
            color: black;
        }

        .Clicked
        {
            display: inline;
            padding: 7px 16px;
            margin: 0;
            margin-right: 3px;
            border-radius: 3px 3px 0 0;
            outline: none;
            /*background: white;*/
            /*color: black;*/
            background: rgb(0,125,233);
            background: linear-gradient(90deg, rgba(0,125,233,1) 0%, rgba(0,212,197,1) 100%);
            color: #fff;
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
                        <li class="breadcrumb-item active">My Pending Approval Requisitions</li>

                    </ol>
                </div>
                <h4 class="page-title">My Pending Approval Requisitions</h4>

            </div>
        </div>
    </div>
    <!-- end page title -->
    <div class="row">
        <div class="col-lg-12">
            <div class="tab-content m-0 p-0">
                <div class="card-box">
                    <div class="responsive-table">


                        <h4 class="header-title">Income Tax April 1<sup>st</sup>
                            <asp:Label ID="LblFromDate" runat="server"></asp:Label>
                            To March 31<sup>st</sup>
                            <asp:Label ID="LblToDate" runat="server"></asp:Label>
                            for Approval </h4>
                        <%--from April 1<sup>st</sup> <asp:Label ID="LblFromDate" runat="server"></asp:Label> To March 31<sup>st</sup> <asp:Label ID="LblToDate" runat="server"></asp:Label></h2>
                        --%>

                        <%--<div style="width: 99%" class="cb">
                            <asp:Button Text="Section 80" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
                                OnClick="Tab1_Click" />
                            <asp:Button Text="Section 80C" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                                OnClick="Tab2_Click" />
                            <asp:Button Text="Housing" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
                                OnClick="Tab3_Click" />
                            <asp:Button Text="Other Sources" BorderStyle="None" ID="Tab4" CssClass="Initial" runat="server"
                                OnClick="Tab4_Click" />
                            <asp:Button Text="Previous Employment Income" BorderStyle="None" CssClass="Initial" runat="server"
                                OnClick="Tab5_Click" ID="Tab5" />
                        </div>--%>
                        <div style="width: 99%" class="cb">
                            <asp:Button Text="Section 80C" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
                                OnClick="Tab2_Click" />
                            <asp:Button Text="Section 80" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
                                OnClick="Tab1_Click" />
                            <asp:Button Text="Housing" BorderStyle="None" ID="Tab3" CssClass="Initial" runat="server"
                                OnClick="Tab3_Click" />
                            <asp:Button Text="Previous Employment Income" BorderStyle="None" CssClass="Initial" runat="server"
                                OnClick="Tab5_Click" ID="Tab5" />
                            <asp:Button Text="Income from Other Sources" BorderStyle="None" ID="Tab4" CssClass="Initial" runat="server"
                                OnClick="Tab4_Click" />



                        </div>
                        <br />
                        <div class="row">
                            <div class="col-sm-2">
                                <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="form-control-file" TabIndex="1">
                                    <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Emp ID" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Emp Name" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="IT ID" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Status" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-2">
                                <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" placeholder="Enter Text" TabIndex="2"></asp:TextBox>
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnsearch" runat="server" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="3" Text="Search" CssClass="btn-xs btn-secondary" />

                                <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" TabIndex="4" Text="Clear" CssClass="btn-xs btn-secondary" />
                            </div>
                            <asp:HiddenField ID="HFTabID" runat="server" />
                            <div class="col-sm-4"></div>
                            <div class="col-sm-2 text-right">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:LinkButton ID="lbtnDownldall" runat="server" CssClass="btn btn-xs btn-secondary" ToolTip="Download All Attachments" OnClick="lbtnDownldall_Click"><i class="fe-download"></i></asp:LinkButton>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="lbtnDownldall" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>

                        </div>
                        <br />

                        <div class="DivSpacer01"></div>

                        <div class="cb">
                            <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
                            <asp:MultiView ID="MainView" runat="server">
                                <asp:View ID="View1" runat="server">



                                    <asp:GridView ID="GVSec80Header" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="ID,CREATED_BY" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle"
                                        Width="85%" OnRowCommand="GVSec80Header_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="GVSec80Header_PageIndexChanging"
                                        AllowSorting="true" OnSorting="GVSec80Header_Sorting" TabIndex="5">




                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                            <asp:BoundField HeaderText="Emp ID" DataField="CREATED_BY"></asp:BoundField>
                                            <asp:BoundField HeaderText="Emp Name" DataField="ENAME"></asp:BoundField>
                                            <asp:TemplateField HeaderText="IT Type">
                                                <ItemTemplate>
                                                    Section 80 
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <%-- <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals" />--%>

                                            <asp:TemplateField HeaderText="Consider Actuals">
                                                <ItemTemplate>

                                                    <%#(Eval("CONACTPROP").ToString().Trim()=="0") ? "No" : (Eval("CONACTPROP").ToString().Trim()=="1")? "Yes": "-"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField HeaderText="Status" DataField="STATUS"></asp:BoundField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnSec80View" CssClass="btn btn-xs btn-warning" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><i class="fe-eye"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                        <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                        <SelectedRowStyle BackColor="Silver" />
                                    </asp:GridView>

                                    <br />

                                    <asp:GridView ID="GVITSec80" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="99%" Visible="false" TabIndex="6" CssClass="gridviewNew" GridLines="None" DataKeyNames="ID,LID,SBSEC,SBDIV" OnRowUpdating="GVITSec80_RowUpdating">
                                        <Columns>
                                            <%--//select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                                            <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                            <asp:BoundField HeaderText="ID" DataField="ID">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Contribution" DataField="SBDDS">
                                                <ItemStyle HorizontalAlign="left" Width="450px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Limit" DataField="SDVLT">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField HeaderText="Tax EXEM%" DataField="TXEXM">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>--%>

                                            <asp:BoundField HeaderStyle-CssClass="right" ItemStyle-CssClass="right" HeaderText="Prop. Contr. (INR)" DataField="PROPCONTR" DataFormatString="{0:#,##0.00}"></asp:BoundField>
                                            <%--<asp:BoundField HeaderStyle-CssClass="right" ItemStyle-CssClass="right" HeaderText="Act. Contr. (INR)" DataField="ACTCONTR" DataFormatString="{0:#,##0.00}"></asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Act. Contr. (INR)" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtActContr" runat="server" CssClass="form-control text-right" Text='<%# Eval("ACTCONTR") %>'></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTB_txtActContr" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                        TargetControlID="txtActContr" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Curr" DataField="CURR" Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Attachments" DataField="RECEIPT_FID">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnUpdateExpGrid" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light" CausesValidation="false" Text="Update"
                                                        CommandName="Update" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>

                                    <br />

                                    <table border="0" cellpadding="1" cellspacing="1" id="tblsSec80" runat="server" visible="false">

                                        <tr>
                                            <td>Remarks</td>
                                            <td>
                                                <asp:TextBox ID="txtRemarksSec80" runat="server" CssClass="textbox" TextMode="MultiLine" TabIndex="7" ValidationGroup="vg1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFV_txtRemarks" runat="server" ControlToValidate="txtRemarksSec80" ErrorMessage="Please enter Remarks" ForeColor="Red" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>

                                    <div id="divbuttonSec80" runat="server" visible="false">
                                        <asp:HiddenField ID="HF_Sec80ID" runat="server" />
                                        <asp:HiddenField ID="HF_CreatedBySec80" runat="server" />
                                        <asp:Button ID="btnApproveSec80" runat="server" Text="Approve" OnClick="btnApproveSec80_Click" TabIndex="8" ValidationGroup="vg1" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnRejectSec80" runat="server" Text="Reject" OnClick="btnRejectSec80_Click" TabIndex="9" ValidationGroup="vg1" CssClass="btn bg-danger waves-effect waves-light btn-std" />


                                    </div>


                                </asp:View>

                                <asp:View ID="View2" runat="server">
                                    <asp:Label ID="LblSec80c" runat="server" CssClass="msgboard"></asp:Label>
                                    <asp:GridView ID="GVSec80CHeader" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="ID,CREATED_BY" Width="85%"
                                        OnRowCommand="GVSec80CHeader_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="GVSec80CHeader_PageIndexChanging" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle"
                                        AllowSorting="true" OnSorting="GVSec80CHeader_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                            <asp:BoundField HeaderText="Emp ID" DataField="CREATED_BY"></asp:BoundField>
                                            <asp:BoundField HeaderText="Emp Name" DataField="ENAME"></asp:BoundField>
                                            <asp:TemplateField HeaderText="IT Type">
                                                <ItemTemplate>
                                                    Section 80 C
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <%-- <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals" />--%>

                                            <asp:TemplateField HeaderText="Consider Actuals">
                                                <ItemTemplate>

                                                    <%#(Eval("CONACTPROP").ToString().Trim()=="0") ? "No" : (Eval("CONACTPROP").ToString().Trim()=="1")? "Yes": "-"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField HeaderText="Status" DataField="STATUS"></asp:BoundField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnSec80CView" runat="server" CssClass="btn btn-xs btn-warning" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><i class="fe-eye"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                        <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                        <SelectedRowStyle BackColor="Silver" />
                                    </asp:GridView>

                                    <br />

                                    <asp:GridView ID="GVITSec80C" runat="server" AutoGenerateColumns="False" BorderStyle="None" Width="99%"  CssClass="gridviewNew" GridLines="None" DataKeyNames="ID,LID,ICODE"  OnRowUpdating="GVITSec80C_RowUpdating">
                                        <Columns>
                                            <%-- //select t8.SBSEC,t8.SBDIV,t8.SBDDS, t9.SDVLT,t9.TXEXM--%>
                                            <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                            <asp:BoundField HeaderText="ID" DataField="ID">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Investment / Contribution" DataField="ITTXT">
                                                <ItemStyle HorizontalAlign="left" Width="450px" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Limit" DataField="ITLMT">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Prop. Invst. (INR)" DataField="PROPINVST" DataFormatString="{0:#,##0.00}" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField HeaderText="Act. Invst. (INR)" DataField="ACTINVST" DataFormatString="{0:#,##0.00}" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Act. Invst. (INR)" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtACTINVST" runat="server" CssClass="form-control text-right" Text='<%# Eval("ACTINVST") %>'></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTB_txtActContr" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                        TargetControlID="txtACTINVST" ValidChars=".">
                                                    </cc1:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Curr" DataField="CURR" Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Attachments" DataField="RECEIPT_FID">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnUpdateExpGrid" runat="server" CssClass="btn btn-warning btn-xs waves-effect waves-light" CausesValidation="false" Text="Update"
                                                        CommandName="Update" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>


                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>


                                    <br />
                                    <table border="0" cellpadding="1" cellspacing="1" id="TblSec80C" runat="server" visible="false">

                                        <tr>
                                            <td>Remarks</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RemarksSec80C" runat="server" CssClass="textbox" TextMode="MultiLine" TabIndex="10" ValidationGroup="vg2"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFV_TXT_RemarksSec80C" runat="server" ControlToValidate="TXT_RemarksSec80C" ErrorMessage="Please enter Remarks" ForeColor="Red" ValidationGroup="vg2"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>

                                    <div id="DivBtnsSec80C" runat="server" visible="false">

                                        <asp:HiddenField ID="HF_Sec80CID" runat="server" />
                                        <asp:HiddenField ID="HF_CreatedBySec80C" runat="server" />
                                        <asp:Button ID="BtnAppSec80C" runat="server" Text="Approve" OnClick="BtnAppSec80C_Click" TabIndex="11" ValidationGroup="vg2" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="BtnRejSec80C" runat="server" Text="Reject" OnClick="BtnRejSec80C_Click" TabIndex="12" ValidationGroup="vg2" CssClass="btn bg-danger waves-effect waves-light btn-std" />


                                    </div>

                                </asp:View>

                                <asp:View ID="View3" runat="server">
                                    <asp:Label ID="LblHousing" runat="server" CssClass="msgboard"></asp:Label>
                                    <asp:GridView ID="GVHousingHeader" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" Width="85%" DataKeyNames="ID,CREATED_BY"
                                        OnRowCommand="GVHousingHeader_RowCommand" AllowPaging="true" PageSize="10" OnPageIndexChanging="GVHousingHeader_PageIndexChanging" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle"
                                        AllowSorting="true" OnSorting="GVHousingHeader_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                            <asp:BoundField HeaderText="Emp ID" DataField="CREATED_BY"></asp:BoundField>
                                            <asp:BoundField HeaderText="Emp Name" DataField="ENAME"></asp:BoundField>
                                            <asp:TemplateField HeaderText="IT Type">
                                                <ItemTemplate>
                                                    Housing
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <%--<asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals"/>--%>

                                            <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField HeaderText="Status" DataField="STATUS"></asp:BoundField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnHousingView" runat="server" CssClass="btn btn-xs btn-warning" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><i class="fe-eye"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                        <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                        <SelectedRowStyle BackColor="Silver" />
                                    </asp:GridView>

                                    <br />

                                    <asp:GridView ID="GVHousing" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" Width="99%">
                                        <Columns>
                                            <%--  //ACCOM,METRO,RTAMT,HRTXE,LDAD1,LDAID,LDADE,EMPCOMMENTS,STATUS--%>
                                            <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                            <asp:BoundField HeaderText="ID" DataField="ID">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField HeaderText="Accomodation Type" DataField="">
                        <ItemStyle HorizontalAlign="left" Width="100px"/>
                    </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Accomodation Type">
                                                <ItemTemplate>

                                                    <%#(Eval("ACCOM").ToString().Trim()=="1") ? "Rentded" : (Eval("ACCOM").ToString().Trim()=="4")? "Own": "-"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField HeaderText="City Category"  DataField="">
                        <ItemStyle HorizontalAlign="left" />
                    </asp:BoundField>--%>

                                            <asp:TemplateField HeaderText="City Category">
                                                <ItemTemplate>

                                                    <%#(Eval("METRO").ToString().Trim()=="1") ? "Metro" : (Eval("METRO").ToString().Trim()=="0")? "Non-Metro": "-"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Actual Amount" DataField="RTAMT" DataFormatString="{0:#,##0.00}" HeaderStyle-CssClass="right" ItemStyle-CssClass="right"></asp:BoundField>
                                            <%-- <asp:BoundField HeaderText="HRA to be Excempt"  DataField="">
                        <ItemStyle HorizontalAlign="left" />
                    </asp:BoundField>--%>

                                            <asp:TemplateField HeaderText="HRA to be Excempt">
                                                <ItemTemplate>

                                                    <%#(Eval("HRTXE").ToString().Trim()=="1") ? "Yes" : (Eval("HRTXE").ToString().Trim()=="0")? "No" : "-"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:BoundField HeaderText="LandLord's Address" DataField="LDAD1">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="PAN of LandLord" DataField="LDAID">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>

                                            <%--<asp:BoundField HeaderText="Declaration Provided by LandLord"  DataField="">
                        <ItemStyle HorizontalAlign="left"/>
                    </asp:BoundField>--%>

                                            <asp:TemplateField HeaderText="Declaration Provided by LandLord">
                                                <ItemTemplate>

                                                    <%#(Eval("LDADE").ToString().Trim()=="1") ? "Yes" : (Eval("LDADE").ToString().Trim()=="0")? "No" : "-"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                        </Columns>

                                    </asp:GridView>

                                    <table border="0" cellpadding="1" cellspacing="1" id="TblHousing" runat="server" visible="false">

                                        <tr>
                                            <td>Remarks</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RemarksHousing" runat="server" CssClass="textbox" TextMode="MultiLine" TabIndex="13" ValidationGroup="vg3"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFV_TXT_RemarksHousing" runat="server" ControlToValidate="TXT_RemarksHousing" ErrorMessage="Please enter Remarks" ForeColor="Red" ValidationGroup="vg3"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>

                                    <div id="DivBtnsHousing" runat="server" visible="false">
                                        <asp:HiddenField ID="HFHousingID" runat="server" />
                                        <asp:HiddenField ID="HFCreatedByHousing" runat="server" />
                                        <asp:Button ID="BtnAppHousing" runat="server" Text="Approve" OnClick="BtnAppHousing_Click" TabIndex="14" ValidationGroup="vg3" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="BtnRejHousing" runat="server" Text="Reject" OnClick="BtnRejHousing_Click" TabIndex="15" ValidationGroup="vg3" CssClass="btn bg-danger waves-effect waves-light btn-std" />


                                    </div>
                                </asp:View>

                                <asp:View ID="View4" runat="server">
                                    <asp:Label ID="LblOthers" runat="server" CssClass="msgboard"></asp:Label>
                                    <asp:GridView ID="GVOthersHeader" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" DataKeyNames="ID,ITTYP,CREATED_BY,CONACTPROP"
                                        OnRowCommand="GVOthersHeader_RowCommand" Width="85%" AllowPaging="true" PageSize="10" OnPageIndexChanging="GVOthersHeader_PageIndexChanging" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle"
                                        AllowSorting="true" OnSorting="GVOthersHeader_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>

                                            <asp:BoundField HeaderText="Emp ID" DataField="CREATED_BY"></asp:BoundField>
                                            <asp:BoundField HeaderText="Emp Name" DataField="ENAME"></asp:BoundField>
                                            <%--   <asp:TemplateField HeaderText="IT Type">
                                <ItemTemplate>
                                    Other Sources
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                            <%-- <asp:BoundField HeaderText="IT Type" DataField="ITTYP"></asp:BoundField>--%>

                                            <asp:TemplateField HeaderText="IT Type">
                                                <ItemTemplate>

                                                    <%#(Eval("ITTYP").ToString().Trim()=="1") ? "Housing Property" :  "Other Sources"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <%--          <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals"/>--%>

                                            <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />

                                            <asp:BoundField HeaderText="Status" DataField="STATUS"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnOthersView" runat="server" CssClass="btn btn-xs btn-warning" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><i class="fe-eye"> </i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                        <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                        <SelectedRowStyle BackColor="Silver" />
                                    </asp:GridView>

                                    <br />
                                    <asp:GridView ID="grdSelfOccDetails" runat="server" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle"
                                        OnRowDataBound="grdSelfOccDetails_RowDataBound" DataKeyNames="ID"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="gridviewNew">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Led. Name">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Led. Addr.">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Led. PAN">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Addr. of Property">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="City">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan Sanc. Date">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purpose of Housing loan">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Carpet Area in Sq. Ft.">
                                                <ItemTemplate>
                                                    <%#Eval("TDSAT") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan Sanc. Amt." HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemTemplate>
                                                    <%#Eval("INT24","{0:#,##0.00}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value of the Property" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemTemplate>
                                                    <%#Eval("REP24","{0:#,##0.00}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stamp Duty/Reg. charge" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemTemplate>
                                                    <%#Eval("OTH24","{0:#,##0.00}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stamp Duty/Reg. charge date">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Any other house Property">
                                                <ItemTemplate>
                                                    <%#Eval("LETVL").ToString()=="0.00"?"No":"Yes" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loan Type">
                                                <ItemTemplate>
                                                    <%#Eval("BSPFT").ToString()=="1.00"?"Single":"Joint" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Interest Paid" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemTemplate>
                                                    <%#Eval("CPGLN","{0:#,##0.00}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Principal Paid " HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemTemplate>
                                                    <%#Eval("CPGLS","{0:#,##0.00}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Interest" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemTemplate>
                                                    <%#Eval("CPGSS","{0:#,##0.00}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Principal" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                                <ItemTemplate>
                                                    <%#Eval("INTRS","{0:#,##0.00}") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Benefit Under Section 80EE">
                                                <ItemTemplate>
                                                    <%#Eval("DVDND").ToString()=="0.00"?"No":"Yes" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>



                                        </Columns>
                                    </asp:GridView>

                                    <asp:FormView ID="frmSelfOccDetails" runat="server">
                                        <ItemTemplate>
                                            <table class="table" id="Table1" runat="server">
                                                <%-- <tr>
                                                    <td style="width: 20%">Lenders Name </td>
                                                    <td style="width: 30%">
                                                        <%# Eval("LENDNAME") %></td>
                                                    <td style="width: 20%">Address of Property for which loan is taken		
                                                    </td>
                                                    <td style="width: 30%">
                                                        <%# Eval("ADDPROPTY") %></td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Lenders Address </td>
                                                    <td>
                                                           <%# Eval("LENDADD") %></td>
                                                    <td>State </td>
                                                    <td>
                                                           <%# Eval("State") %></td>
                                                </tr>

                                                <tr>
                                                    <td>Pan of the Lender		
                                                    </td>
                                                    <td>
                                                          <%# Eval("LENDPAN") %>
                                                    </td>

                                                    <td>City 		
                                                    </td>
                                                    <td>
                                                           <%# Eval("City") %></td>
                                                </tr>
                                                <tr>
                                                    <td>Loan Sanction Date </td>
                                                    <td>
                                                          <%# Eval("ADDPROPTY") %>
                                                    </td>
                                                    <td>Purpose of Housing loan 		
                                                    </td>
                                                    <td>
                                                          <%# Eval("ADDPROPTY") %></td>
                                                </tr>--%>
                                                <tr>
                                                    <td>Loan Sanctioned Amount </td>
                                                    <td>
                                                        <%#Eval("INT24","{0:#,##0.00}") %>
                                                    </td>
                                                    <td>Carpet Area in Sq. Ft. </td>
                                                    <td>
                                                        <%#Eval("TDSAT") %>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>Stamp Duty/Registration charge if paid in current Financial year  		
                                                    </td>
                                                    <td>
                                                        <%#Eval("OTH24","{0:#,##0.00}") %>
                                                    </td>
                                                    <td>Value of the Property </td>
                                                    <td>
                                                        <%#Eval("REP24","{0:#,##0.00}") %>
                                                    </td>

                                                </tr>
                                                <tr>

                                                    <td>Do you own any other house Property 		
                                                    </td>
                                                    <td>
                                                        <%#Eval("LETVL").ToString()=="0.00"?"No":"Yes" %>

                                                    </td>
                                                    <td>Stamp Duty/Registration charge date </td>
                                                    <td>
                                                        <%# Eval("ADDPROPTY") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Loan Type </td>
                                                    <td>
                                                        <%#Eval("BSPFT").ToString()=="1.00"?"Single":"Joint" %>
                                                        <%--    <td colspan="2">--%>


                                                        <%--     <table class="gridviewNew">
                                                            <tr>

                                                                <th>Borrowers Name</th>
                                                                <th>Percentange % of Contribution</th>

                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                       <%# Eval("ADDPROPTY") %>
                                                                </td>
                                                                <td>
                                                                     <%# Eval("ADDPROPTY") %>
                                                                </td>
                                                                <td>
                                                                   </td>
                                                            </tr>
                                                        </table>--%>


                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Total Interest Paid	 		
                                                    </td>
                                                    <td>
                                                        <%#Eval("CPGLN","{0:#,##0.00}") %>
                                                    </td>
                                                    <td>Total Principal Paid </td>
                                                    <td>
                                                        <%#Eval("CPGLS","{0:#,##0.00}") %>
                                                    </td>

                                                </tr>
                                                <tr class="text-center font-weight-bold">
                                                    <td colspan="4">
                                                        <h5>Housing Loan Interest & Principal considered for Exemption</h5>
                                                    </td>
                                                </tr>
                                                <tr>

                                                    <td>
                                                        <div runat="server" id="DIVins1">Total Interest	 </div>
                                                    </td>
                                                    <td>
                                                        <div runat="server" id="DIVins">
                                                            <%#Eval("CPGSS","{0:#,##0.00}") %>
                                                        </div>
                                                    </td>
                                                    <td></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>Total Principal		 		
                                                    </td>
                                                    <td>
                                                        <%#Eval("INTRS","{0:#,##0.00}") %>
                                                    </td>
                                                    <td>Benefit Under Section 80 EEA </td>
                                                    <td>
                                                        <%#Eval("DVDND").ToString()=="0.00"?"No":"Yes" %>
                                                    </td>

                                                </tr>

                                            </table>
                                        </ItemTemplate>
                                    </asp:FormView>
                                    <br />
                                    <asp:GridView ID="GVOthers1" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" Width="85%">
                                        <Columns>
                                            <%--    //ID,PROPTYP,RENTO,INT24,LETVL,REP24,OTH24,TDSOT,
                // BSPFT,CPGLN,CPGLS,CPGNS,CPGSS,DVDND,INTRS,UNSPI,TDSAT, EMPCOMMENTS,EMPCOMMENTS2,STATUS  -%>
                   <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                            <asp:BoundField HeaderText="ID" DataField="ID">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>
                                            <%-- <asp:BoundField HeaderText="Accomodation Type" DataField="">
                        <ItemStyle HorizontalAlign="left" Width="100px"/>
                    </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Income Type">
                                                <ItemTemplate>

                                                    <%#(Eval("PROPTYP").ToString().Trim()=="1") ? "House Property" : (Eval("PROPTYP").ToString().Trim()=="2")? "Other Sources": "-"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Property Type">
                                                <ItemTemplate>

                                                    <%#(Eval("RENTO").ToString().Trim()=="1") ? "Self-Occupied/Deemed Self Occupied House Property" : (Eval("RENTO").ToString().Trim()=="2")? "Partly Let Out House Property": (Eval("RENTO").ToString().Trim()=="3")? "Wholly Let Out House Property": "-"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField HeaderText="Deduce - Interest u/s 24" DataField="INT24" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"></asp:BoundField>
                                            <asp:BoundField HeaderText="Final Lettable Value" DataField="LETVL" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"   Visible="false"></asp:BoundField>

                                            <%--  <asp:BoundField HeaderText="Deduce - Interest u/s 24" DataField="LETVL">
                            <ItemStyle HorizontalAlign="right" />
                        </asp:BoundField>--%>

                                            <asp:BoundField HeaderText="TDS On Other Income" DataField="TDSOT" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"   Visible="false"></asp:BoundField>

                                            <asp:BoundField HeaderText="Deduce - Repair u/s 24" DataField="REP24" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"   Visible="false"></asp:BoundField>

                                            <asp:BoundField HeaderText="Deduce - Others u/s 24 " DataField="OTH24" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}"   Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>

                                            <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                        </Columns>

                                    </asp:GridView>
                                    <br />
                                    <asp:GridView ID="grdBorwDetails" runat="server" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle" DataKeyNames="ID" AutoGenerateColumns="false" GridLines="None" CssClass="gridviewNew">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl. No." ControlStyle-CssClass="col-center">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Borrowers Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdBorwName" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                    <%-- <asp:TextBox ID="txtGrdBorwName" runat="server" CssClass="form-control-file"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtGrdBorwName" runat="server" FilterMode="ValidChars" FilterType="LowercaseLetters,Custom,UppercaseLetters"
                                                                                            TargetControlID="txtGrdBorwName" ValidChars=".">
                                                                                        </cc1:FilteredTextBoxExtender>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Percentange(%)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGrdBorwPernct" runat="server" Text='<%#Eval("PERCNT") %>'></asp:Label>
                                                    <%-- <asp:TextBox ID="txtGrdBorwPernct" runat="server" CssClass="form-control-file"></asp:TextBox>
                                                                                        <cc1:FilteredTextBoxExtender ID="FTBEtxtGrdBorwPernct" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                                                                            TargetControlID="txtGrdBorwPernct" ValidChars=".">
                                                                                        </cc1:FilteredTextBoxExtender>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>

                                    <asp:GridView ID="GVOthers2" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False" BorderStyle="None" Width="85%">
                                        <Columns>
                                            <%--    //ID,PROPTYP,RENTO,INT24,LETVL,REP24,OTH24,TDSOT,
                // BSPFT,CPGLN,CPGLS,CPGNS,CPGSS,DVDND,INTRS,UNSPI,TDSAT, EMPCOMMENTS,EMPCOMMENTS2,STATUS  -%>
                   <%-- <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                            <asp:BoundField HeaderText="ID" DataField="ID">
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Income Type">
                                                <ItemTemplate>

                                                    <%#(Eval("PROPTYP").ToString().Trim()=="1") ? "House Property" : (Eval("PROPTYP").ToString().Trim()=="2")? "Other Sources": "-"%>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:BoundField HeaderText="Business Profits" DataField="BSPFT" Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Long-Term Capital Gains (Normal Rate)" DataField="CPGLN" Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Long-Term Capital Gains (Special Rate)" DataField="CPGLS" Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Short-Term Capital Gains" DataField="CPGNS" Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Short-Term Capital Gains (Listed Securities)" DataField="CPGSS" Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Income From Dividend" DataField="DVDND" Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Income From Interest" DataField="INTRS">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Other Income Unspecified" DataField="UNSPI" Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="TDS On Other Income" DataField="TDSAT" Visible="false">
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:BoundField>


                                            <asp:BoundField HeaderText="Comments" DataField="EMPCOMMENTS2">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>



                                        </Columns>

                                    </asp:GridView>






                                    <table border="0" cellpadding="1" cellspacing="1" id="TblOthers" runat="server" visible="false">

                                        <tr>
                                            <td>Remarks</td>
                                            <td>
                                                <asp:TextBox ID="TXT_RemarksOthers" runat="server" CssClass="textbox" TextMode="MultiLine" TabIndex="16" ValidationGroup="vg4"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFV_TXT_RemarksOthers" runat="server" ControlToValidate="TXT_RemarksOthers" ErrorMessage="Please enter Remarks" ForeColor="Red" ValidationGroup="vg4"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>

                                    <div id="DivBtnsOthers" runat="server" visible="false">
                                        <asp:HiddenField ID="HFOthersID" runat="server" />
                                        <asp:HiddenField ID="HFPROPTYP" runat="server" />
                                        <asp:HiddenField ID="HFCreatedByOthers" runat="server" />
                                        <asp:Button ID="BtnAppOthers" runat="server" Text="Approve" OnClick="BtnAppOthers_Click" TabIndex="17" ValidationGroup="vg4" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="BtnRejOthers" runat="server" Text="Reject" OnClick="BtnRejOthers_Click" TabIndex="18" ValidationGroup="vg4" CssClass="btn bg-danger waves-effect waves-light btn-std" />


                                    </div>

                                </asp:View>

                                <asp:View ID="View5" runat="server">
                                    <asp:Label ID="lblPreE" runat="server"></asp:Label>
                                    <asp:GridView ID="grdPreEmptIncHead" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False"
                                        BorderStyle="None" DataKeyNames="ID,BEGDA,ENDDA,STATUS" Width="85%" AllowPaging="true" PageSize="10" OnRowCommand="grdPreEmptIncHead_RowCommand"
                                        OnPageIndexChanging="grdPreEmptIncHead_PageIndexChanging">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="ID" DataField="ID"></asp:BoundField>
                                            <asp:BoundField HeaderText="Emp ID" DataField="CREATED_BY"></asp:BoundField>
                                            <asp:BoundField HeaderText="Emp Name" DataField="ENAME"></asp:BoundField>
                                            <%--   <asp:TemplateField HeaderText="IT Type">
                                <ItemTemplate>
                                    Other Sources
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"/>
                    </asp:TemplateField>--%>
                                            <%-- <asp:BoundField HeaderText="IT Type" DataField="ITTYP"></asp:BoundField>--%>

                                            <asp:TemplateField HeaderText="IT Type">
                                                <ItemTemplate>
                                                    Previous Employment Income
                                                           <%-- <%#(Eval("ITTYP").ToString().Trim()=="1") ? "Housing Property" :  "Other Sources"%>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BEGDA" HeaderText="From Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="ENDDA" HeaderText="To Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <%--          <asp:BoundField DataField="CONACTPROP" HeaderText="Consider Actuals"/>--%>

                                            <asp:BoundField DataField="CREATED_ON" HeaderText="Submitted On" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="APPROVEDON" HeaderText="Approved On" DataFormatString="{0:dd-MMM-yyyy}" />
                                            <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />

                                            <asp:BoundField HeaderText="Status" DataField="STATUS"></asp:BoundField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LbtnOthersView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn-xs btn-warning" ForeColor="White"><i class="fe-eye"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                                        <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                                        <SelectedRowStyle BackColor="Silver" />
                                    </asp:GridView>

                                    <div id="divPreEmp" runat="server" visible="false">
                                        <asp:GridView ID="grdPreEmptInc" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="False"
                                            BorderStyle="None">
                                            <Columns>
                                                <asp:BoundField HeaderText="Employer" DataField="PreEmpr" />
                                                <asp:BoundField HeaderText="PAN" DataField="PreEmprPAN" />
                                                <asp:BoundField HeaderText="TAN" DataField="PreEmprTAN" />
                                                <asp:BoundField HeaderText="From Date" DataField="BEGDA" DataFormatString="{0:dd-MMM-yyyy}" />
                                                <asp:BoundField HeaderText="To Date" DataField="ENDDA" DataFormatString="{0:dd-MMM-yyyy}" />
                                                <asp:BoundField HeaderText="Salary Income After Exemptions" DataField="GRSAL" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                <%-- <asp:BoundField HeaderText="Value of Perquisites u/s 17(2)" DataField="VPRQS" />
                                                <asp:BoundField HeaderText="Profits in lieu of salary u/s 17(3)" DataField="PRSAL" />
                                                <asp:BoundField HeaderText="Exemptions under Section (10)" DataField="EXS10" />--%>
                                                <asp:BoundField HeaderText="PT deducted" DataField="PRTAX" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                <asp:BoundField HeaderText="PF deducted" DataField="PRFND" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                <asp:BoundField HeaderText="IT Deducted" DataField="TXDED" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                <asp:BoundField HeaderText="Surcharge Deducted" DataField="SCDED" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                                <asp:BoundField HeaderText="Cess Deducted" DataField="ECDED" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" DataFormatString="{0:#,##0.00}" />
                                            </Columns>
                                        </asp:GridView>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <div class="col-sm-6">Remarks</div>
                                                <div class="col-sm-6">
                                                    <asp:TextBox ID="txtPreEmpRemks" runat="server" CssClass="form-control-file" TextMode="MultiLine" TabIndex="16" ValidationGroup="vg5"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TXT_RemarksOthers" ErrorMessage="Please enter Remarks" ForeColor="Red" ValidationGroup="vg5"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">

                                                <asp:Button ID="btnPreEmApp" runat="server" Text="Approve" OnClick="btnPreEmApp_Click" TabIndex="17" ValidationGroup="vg5" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" />
                                                <asp:Button ID="btnPreEmRej" runat="server" Text="Reject" OnClick="btnPreEmRej_Click" TabIndex="18" ValidationGroup="vg5" CssClass="btn bg-danger waves-effect waves-light btn-std" />
                                            </div>
                                        </div>

                                    </div>
                                </asp:View>
                            </asp:MultiView>

                        </div>

                    </div>
                </div>
            </div>
</asp:Content>
