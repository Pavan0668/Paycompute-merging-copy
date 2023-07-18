<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="FBP_Declarations.aspx.cs" Inherits="iEmpPower.UI.FBP.FBP_Declarations" EnableEventValidation="false"  Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        #ContentPlaceHolder1_MainContent_grdFbpDeclaration {
            position: relative !important;
            max-height: 70vh !important;
        }

 

            #ContentPlaceHolder1_MainContent_grdFbpDeclaration tr th {
                position: sticky !important;
                top: 0 !important;
                background-color: white !important;
            }

 

            #ContentPlaceHolder1_MainContent_grdFbpDeclaration tr:last-child {
                position: sticky !important;
                bottom: 0 !important;
                background-color: white !important;
            }
    </style>
    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item active">FBP Declarations</li>
                    </ol>
                </div>
                <h4 class="page-title">FBP Declarations
                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>
                </h4>
            </div>
        </div>
    </div>
    <!-- end page title -->
    <div class="row card-box">
        <div class="col-xl-12 m-t-20">
            <asp:Label ID="lblmsg" runat="server"></asp:Label>
            <table class="table table-sm table-borderless mb-0 table_font_sm">
                <tbody>
                    <tr>
                        <td colspan="4">
                            <h5 class="header-title">FBP Declarations</h5>
                        </td>
                        <td colspan="4">
                            <%--<a href="Other_Reimbursements.aspx">
                                                    <button type="button" class="btn btn-sm bg-brand-btn waves-effect waves-light float-right">
                                                        <i class="mdi mdi-plus"></i>Add New iExpense Request</button></a>--%>
                        </td>
                    </tr>

                    <tr class="border-top">
                        <td width="60">Show</td>
                        <td width="60">
                            <%--<select name="" aria-controls="" class="">
                                                    <option value="10">10</option>
                                                    <option value="25">25</option>
                                                    <option value="50">50</option>
                                                    <option value="100">100</option>
                                                </select>--%>
                            <asp:DropDownList ID="ddlPagesizeEmp" CssClass="hidden" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPagesizeEmp_SelectedIndexChanged">
                                <asp:ListItem Selected="True">10</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnLastestRec" Text="Last Declaration Done" runat="server" CssClass="btn btn-xs btn-secondary" OnClick="btnLastestRec_Click" />
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
                            </div>
                        </td>
                        <%--<td width="50" align="right">Search:</td>
                        <td width="300">
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" placeholder="Employee ID" AutoPostBack="True" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>
                        </td>--%>

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
                    </tr>
                </tbody>
            </table>
    
<asp:GridView ID="grdFbpDeclaration" runat="server" CssClass="gridviewNew" GridLines="None" AutoGenerateColumns="false" Width="100%" CellPadding="4" AllowPaging="false" OnPageIndexChanging="grdFbpDeclaration_PageIndexChanging" PageSize="10"
                CellSpacing="1"
                EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle"
                ShowFooter="True" FooterStyle-CssClass="foo01" DataKeyNames="AA_AMT01,AA_AMT02,AA_AMT03,AA_AMT04,AA_AMT05,AA_AMT06,AA_AMT07,AA_AMT08,AA_AMT09,AA_AMT10,AA_AMT11,AA_AMT12">
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No." ControlStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                        </ItemTemplate>


                        <ItemStyle CssClass="col-center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Entity">
                        <EditItemTemplate>
                            <%# Eval("ENTITY") %>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <%# Eval("ENTITY") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PERNR" HeaderText="Emp ID" />
                    
                    <asp:BoundField DataField="SAP_ID" HeaderText=" SAP ID" />
                   

                    <asp:TemplateField HeaderText="Emp Name" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%# Eval("ENAME", "") %>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="DOJ" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                            <%--<%# Eval("BEGDA", "{​​​​​​​0:dd-MM-yyyy}​​​​​​​") %>--%>
                            <%-- <%# (String.IsNullOrEmpty(Eval("DOJ").ToString())) ? "" : DateTime.Parse(Eval("BEGDA").ToString()).ToString("dd-MM-yyyy​​​​​​") %>--%>
                            <%# (String.IsNullOrEmpty(Eval("DOJ").ToString())) ? "" : Eval("DOJ").ToString().Substring(0,10) %>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="DOL" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                           <%-- <%# Eval("BEGDA", "{​​​​​​​0:dd-MM-yyyy}​​​​​​​") %>--%>
                           <%--  <%# (String.IsNullOrEmpty(Eval("DOL").ToString())) ? "" : DateTime.Parse(Eval("BEGDA").ToString()).ToString("dd-MM-yyyy​​​​​​") %>--%>
                            <%# (String.IsNullOrEmpty(Eval("DOL").ToString())) ? "" : Eval("DOL").ToString().Substring(0,10) %>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Effective Month" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                        <ItemTemplate>
                           <%-- <%# Eval("BEGDA", "{​​​​​​​0:dd-MM-yyyy}​​​​​​​") %>--%>
                             <%--<%# (String.IsNullOrEmpty(Eval("BEGDA").ToString())) ? "" : DateTime.Parse(Eval("BEGDA").ToString()).ToString("dd-MM-yyyy​​​​​​") %>--%>
                            <%# (String.IsNullOrEmpty(Eval("BEGDA").ToString())) ? "" : Eval("BEGDA").ToString().Substring(0,10) %>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Created on" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" SortExpression="CREATED_ON">
                        <ItemTemplate>
                          <%--  <%# Eval("CREATED_ON", "{​​​​​​​0:dd-MM-yyyy}​​​​​​​") %>--%>
                            <%# (String.IsNullOrEmpty(Eval("CREATED_ON").ToString())) ? "" : DateTime.Parse(Eval("CREATED_ON").ToString()).ToString("dd-MM-yyyy") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Basket Total" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />


                    <asp:BoundField HeaderText="LTA" DataField="AA_AMT02" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />
                    <asp:BoundField HeaderText="Meal Voucher" DataField="AA_AMT03" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />
                    <asp:BoundField HeaderText="Car EMI" DataField="AA_AMT04" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />
                    <asp:BoundField HeaderText="Mobile & Telephone Reimbursement" DataField="AA_AMT08" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />
                    <asp:BoundField HeaderText="Car Fuel Reimbursment-Self" DataField="AA_AMT09" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />
                    <asp:BoundField HeaderText="Mobile Purchase" DataField="AA_AMT10" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />
                    <asp:BoundField HeaderText="Car Related Reimbursement" DataField="AA_AMT11" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />
                    <asp:BoundField HeaderText="Education" DataField="AA_AMT12" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />



                    <asp:BoundField HeaderText="Total" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />


                    <asp:BoundField HeaderText="Special Allowance" HeaderStyle-CssClass="right" ItemStyle-CssClass="right" FooterStyle-CssClass="right" />
                </Columns>
                <PagerStyle CssClass="GridPager" HorizontalAlign="Left" />
                <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
            </asp:GridView>


            <br />
        </div>

        <asp:UpdatePanel ID="UPFbp" runat="server">
            <ContentTemplate>

                <div class="mb-3">
                    <div id="Exportbtn" runat="server">
                        
                         <asp:Button ID="BtnExporttoXl" runat="server" Text="Export To Excel" OnClick="BtnExporttoXl_Click" CausesValidation="false" CssClass="btn btn-dark waves-effect waves-light btn-std" TabIndex="5" />
                        <asp:Button ID="BtnExporttoPDF" runat="server" Text="Export To PDF" OnClick="BtnExporttoPDF_Click" TabIndex="6" CssClass="btn btn-dark waves-effect waves-light btn-std" />
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnExporttoPDF" />
                <asp:PostBackTrigger ControlID="BtnExporttoXl" />
            </Triggers>
        </asp:UpdatePanel>
    </div>


   

</asp:Content>
