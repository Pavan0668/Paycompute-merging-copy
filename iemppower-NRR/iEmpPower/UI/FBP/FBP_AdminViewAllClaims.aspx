<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true"
    CodeBehind="FBP_AdminViewAllClaims.aspx.cs" Inherits="iEmpPower.UI.FBP.FBP_AdminViewAllClaims"
    Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
      <style type="text/css">
        
        .Divh {
            background-color: #3470A7;
             color: #FFFFFF;
        }

    </style>
</asp:Content>--%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .hidden
        {
            display: none;
        }
    </style>
    <!-- start page title -->
    <asp:HiddenField ID="HFselec" runat="server" />
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
                        <li class="breadcrumb-item active">View All FBP Claims</li>

                    </ol>
                </div>
                <h4 class="page-title">View All FBP Claims</h4>
            </div>
        </div>
    </div>
    <!-- end page title -->
    <div class="row">
        <div class="col-lg-12">
            <div class="tab-content m-0 p-0">
                <div class="card-box">
                    <div class="responsive-table">
                        <div>
                            <span id="bold" runat="server" style="font-weight: bold"></span>
                        </div>
                        <h4>FBP Claims History</h4>
                        <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label><br />

                        <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="textbox" TabIndex="1">
                            <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Emp ID" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Emp Name" Value="2"></asp:ListItem>
                            <asp:ListItem Text="FBP Claim ID" Value="3"></asp:ListItem>
                           <%-- <asp:ListItem Text="Allowance" Value="4"></asp:ListItem>--%>
                            <asp:ListItem Text="Status" Value="5"></asp:ListItem>
                        </asp:DropDownList>

                        <asp:TextBox ID="txtsearch" runat="server" CssClass="textbox" AutoPostBack="true" OnTextChanged="txtsearch_TextChanged" placeholder="Enter Text" TabIndex="2"></asp:TextBox>

                        <asp:TextBox ID="txtCreatedOn" runat="server" TabIndex="3" placeholder="Select From Date" Width="199px" Style="letter-spacing: 1px; background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px;" AutoPostBack="true" OnTextChanged="txtCreatedOn_TextChanged"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MEE_txtStartDate" runat="server" AcceptNegative="Left"
                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtCreatedOn" />
                        <cc1:CalendarExtender ID="CE_txtCreatedOn" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtCreatedOn">
                        </cc1:CalendarExtender>

                        <asp:TextBox ID="txtCreatedOnto" runat="server" TabIndex="3" placeholder="Select To Date" Width="199px" Style="letter-spacing: 1px; background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px;" AutoPostBack="true" OnTextChanged="txtCreatedOnto_TextChanged"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MEE_txtCreatedOnto" runat="server" AcceptNegative="Left"
                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtCreatedOnto" />
                        <cc1:CalendarExtender ID="CE_txtCreatedOnto" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtCreatedOnto">
                        </cc1:CalendarExtender>
                        <div class="btn-group float-right">

                            <asp:Button ID="btnCY" Text="Current Year" runat="server" CssClass="btn btn-xs btn-secondary" OnClick="btnCY_Click" />
                            <asp:Button ID="btnLY" Text="Last Year" runat="server" CssClass="btn btn-xs btn-light" OnClick="btnLY_Click" />
                            <asp:Button ID="btnAll" Text="All" runat="server" CssClass="btn btn-xs btn-light" OnClick="btnAll_Click" />
                        </div>
                        <%-- <asp:Button ID="btnsearch" runat="server" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="4" Text="Search" />
                        &nbsp;&nbsp;
                            <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" TabIndex="5" Text="Clear" />--%>


                        <%-- <div style="width: 49%; float: left">
            <fieldset style="float: left"">
                <legend><b>&nbsp;Search FBP claims&nbsp;</b></legend>
                <table>

                    <tr>
                        <td>&nbsp;Search&nbsp;
            </td>
                        <td>
                            <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="textbox" TabIndex="1">
                                <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                  <asp:ListItem Text="Emp ID" Value="1"></asp:ListItem>
                                 <asp:ListItem Text="Emp Name" Value="2"></asp:ListItem>
                                <asp:ListItem Text="FBP Claim ID" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Allowance ID" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Status" Value="5"></asp:ListItem>
                            </asp:DropDownList>
                        </td>


                        <td>
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="textbox" placeholder="Enter Text" TabIndex="2"></asp:TextBox>
                        </td></tr>
                        <tr>
                            <td>&nbsp;Created On&nbsp;
            </td>
            <td>
                <asp:TextBox ID="txtCreatedOn" runat="server" TabIndex="3"  placeholder="Select Date" Width="199px" Style="letter-spacing: 1px; background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px;"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MEE_txtStartDate" runat="server" AcceptNegative="Left"
                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtCreatedOn" />
                <cc1:CalendarExtender ID="CE_txtCreatedOn" runat="server" Enabled="True" Format="dd/MM/yyyy"
                    TargetControlID="txtCreatedOn">
                </cc1:CalendarExtender>
            </td>
                        <td>
                            <asp:Button ID="btnsearch" runat="server" CausesValidation="false" OnClick="btnsearch_Click" TabIndex="4" Text="Search" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" TabIndex="5" Text="Clear" />
                        </td>



                    </tr>
                </table>
            </fieldset>
            </div>--%>

                        <div class="DivSpacer01"></div>

                        <asp:GridView ID="grd_FbpClaims_History" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" Width="100%"
                            OnRowCommand="grd_FbpClaims_History_RowCommand" DataKeyNames="FBPC_IC,LGART,STATUS,CREATED_BY,ENAME,BETRG,ALLOWANCETEXT,OVERRIDE_AMT,CREATED_ON,APPROVEDON,REMARKS"
                            AllowPaging="true" AllowSorting="true" PageSize="5" OnSorting="grd_FbpClaims_History_Sorting" OnPageIndexChanging="grd_FbpClaims_History_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No.">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FBP Claim Id">
                                    <ItemTemplate>
                                        <%# Eval("FBPC_IC") %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Employee ID" DataField="CREATED_BY" />
                                <asp:BoundField HeaderText="Employee Name" DataField="ENAME" />
                                <asp:TemplateField HeaderText="Allowance">
                                    <ItemTemplate>
                                        <%--<%# Eval("LGART") %> ---%> <%# Eval("ALLOWANCETEXT") %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Date" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" SortExpression="BEGDA">
                        <ItemTemplate>
                            <%--    <%# Eval("BEGDA") %>
                            <%# Eval("BEGDA", "{0:dd-MM-yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Claimed Amount"  HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("BETRG")).ToString("#,##0.00") %>
                                        <%--   <%# Eval("BETRG") %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Override Amount"  HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%--      <%# Convert.ToDouble(Eval("OVERRIDE_AMT")).ToString("#,##0.00") %>--%>
                                        <%# Eval("OVERRIDE_AMT") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved Amount"  HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("APPAMT")).ToString("#,##0.00") %>
                                        <%--  <%# Eval("APPAMT") %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Submitted On">
                                    <ItemTemplate>
                                        <%--    <%# Eval("CREATED_ON") %>--%>
                                        <%# Eval("CREATED_ON", "{0:dd-MM-yyyy}") %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>

                                        <%# Eval("REMARKS") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <%# Eval("STATUS") %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved on">
                                    <ItemTemplate>
                                        <%--    <%# Eval("APPROVED_ON") %>--%>
                                        <%-- <%# Eval("APPROVEDON", "{0:dd-MM-yyyy}") %>--%>
                                        <%#(Eval("APPROVEDON","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVEDON","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnFbpclaimsView" runat="server" CausesValidation="False" CommandName="VIEW" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn-xs btn-warning" ForeColor="White"><i class="fe-eye" ></i></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                            <SelectedRowStyle BackColor="Silver" />
                        </asp:GridView>

                        <br />
                        <div id="divitems" runat="server">
                            <asp:GridView ID="grd_CalimsItems" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" >
                                <Columns>


                                    <%--  <asp:BoundField HeaderText="Items" DataField="ALLOWANCETEXT" ItemStyle-Width="45%" />--%>
                                    <asp:TemplateField HeaderText="Allowance">
                                        <ItemTemplate>
                                            <%-- <%# Eval("LGART") %> ---%> <%# Eval("ALLOWANCETEXT") %>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <%-- <asp:BoundField HeaderText="Entitlement" DataField="ANNUAL" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"> /--%>
                                    <asp:TemplateField HeaderText="Annual Entitlement"  HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                        <ItemTemplate>
                                            <%# Convert.ToDouble(Eval("ANNUAL")).ToString("#,##0.00") %>
                                            <%--   <%# Eval("ANNUAL") %>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:BoundField HeaderText="Claims Paid" DataField="BETRG" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                    <asp:TemplateField HeaderText="Claims Paid"  HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                        <ItemTemplate>
                                            <%# Convert.ToDouble(Eval("BETRG")).ToString("#,##0.00") %>
                                            <%--   <%# Eval("BETRG") %>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--<asp:BoundField HeaderText="Claims Pending" DataField="PENDINGAMT" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                    <asp:TemplateField HeaderText="Claims Pending"  HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                        <ItemTemplate>
                                            <%# Convert.ToDouble(Eval("PENDINGAMT")).ToString("#,##0.00") %>
                                            <%--   <%# Eval("PENDINGAMT") %>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- <asp:BoundField HeaderText="Balance" DataField="BALANCE" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                    <asp:TemplateField HeaderText="Balance"  HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                        <ItemTemplate>
                                            <%# Convert.ToDouble(Eval("BALANCE")).ToString("#,##0.00") %>
                                            <%--   <%# Eval("BALANCE") %>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>


                            </asp:GridView>

                            <br />
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Bill No." DataField="BILL_NO" />

                                    <asp:BoundField HeaderText="Bill Date" DataField="BILL_DATE" DataFormatString="{0:dd-MM-yyyy}" />

                                    <asp:BoundField HeaderText="Relationship" DataField="RELATIONSHIP" />

                                    <%--   <asp:BoundField HeaderText="Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" DataField="BILL_AMT" />--%>

                                    <asp:TemplateField HeaderText="Mobile No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobNo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Claimed Amount"  HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                        <ItemTemplate>
                                            <%# Convert.ToDouble(Eval("BILL_AMT")).ToString("#,##0.00") %>
                                            <%--   <%# Eval("BILL_AMT") %>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Attachments
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="Lbtndownload" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                        <HeaderTemplate>
                                            <a data-toggle="tooltip" title="View Details"></a>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="btnFbp " runat="server" CausesValidation="false"  CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning"><i class="fe-eye"></i></asp:LinkButton>--%>
                                            <%--<asp:LinkButton ID="lnkbtnFbp" runat="server" CausesValidation="false" OnClick="lnkbtnFbp_Click"  CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning" ><i class="fe-eye" ></i> </asp:LinkButton>--%>
                                            <asp:LinkButton ID="LnkbtnFbp" runat="server" OnClick="LnkbtnFbp_Click" CausesValidation="false" CommandName="VIEW" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="btn btn-xs btn-warning" ><i class="fe-eye" ></i> </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                 
                                    <asp:BoundField DataField="FBPC_IC" HeaderText="FBP ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />

                                </Columns>
                            </asp:GridView>


                        </div>
                    </div>
                </div>
            </div>

            <div class="DivSpacer01"></div>
            <div id="Exportbtn" runat="server">

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                           <asp:Button ID="BtnExporttoXl" runat="server" Text="Export To Excel" OnClientClick='exportTableToExcel("ContentPlaceHolder1_MainContent_exportFormat", "FBP_Claim")' CausesValidation="false" TabIndex="6" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                        &nbsp;&nbsp;
                    <asp:Button ID="BtnExporttoPDF" runat="server" Text="Export To PDF" OnClientClick="createPDF();" TabIndex="7" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                        <asp:Button ID="BtnExportAlltoXl" runat="server" Text="Export All To Excel" OnClick="btnExportToExcel_Click" CausesValidation="false" TabIndex="6" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                       <%-- <asp:Button ID="BtnExporttoXl" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" CausesValidation="false" TabIndex="6" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                        &nbsp;&nbsp;
                    <asp:Button ID="BtnExporttoPDF" runat="server" Text="Export To PDF" OnClick="ExportToPDF_Click" TabIndex="7" CssClass="btn bg-dark waves-effect waves-light btn-std" />--%>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BtnExporttoXl" />
                        <asp:PostBackTrigger ControlID="BtnExporttoPDF" />
                        <asp:PostBackTrigger ControlID="BtnExportAlltoXl" />
                    </Triggers>

                </asp:UpdatePanel>



            </div>
            
        <div id="exportFormat" runat="server" style="display: none">



            <table>
                <tr>
                    <th style="text-align: left">
                        <img src="../../NewAssets/images/subex.png" width="140" height="50" /></th>
                    <th style="text-align: left" colspan="3">Subex
                    <br />
                       <%-- RMZ  Eco World, Outer Ring Road, Devarabisanahalli, Bangalore - 560103</th>--%>
                        Pritech Park SEZ,Block 09,4th Floor,B Wing,
                        Survey No. 51 to 64/4, Outer Ring Rd, Bellandur Village Varthur Hobli, Bangalore,KA-560103</th>
                </tr>
            </table>
            <br />
            <table style="border: solid 1px black; color: black; min-width: 100%;">
                <tr>
                    <td colspan="2">
                        <table style="border: solid 1px black; width: 98%; margin: 10px;">
                            <tr>
                                <td colspan="5" align="center" style="padding: 5px 0 5px 0">Reimbursement Claim Form</td>
                            </tr>
                            <tr style="border: solid 1px black; text-align: left">
                                <td colspan="3"></td>
                          <%--      <td style="text-align: left">Date :</td>
                                <td style="text-align: left"><%= DateTime.Now.ToString("dd - MMMM - yyyy") %></td>--%>
                                <td style="text-align: left"></td>
                                <td style="text-align: left"></td>
                            </tr>
                            <tr style="border: solid 1px black; text-align: left">
                                <td style="text-align: left">Name :</td>
                                <td id="EmpName" runat="server" style="text-align: left"></td>
                                <td></td>
                                <td style="text-align: left">Employee No :</td>
                                <td id="EmpPERNR" runat="server" style="text-align: left"></td>
                            </tr>
                            <tr style="border: solid 1px black; text-align: left">
                                <td style="text-align: left">Claim Date :</td>
                                <td id="ClaimDate" runat="server" style="text-align: left"></td>
                                <td></td>
                                <td style="text-align: left">Claim No :</td>
                                <td id="ClaimID" runat="server" style="text-align: left"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div>
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" Width="100%" GridLines="Both" ShowFooter="true" FooterStyle-BorderStyle="None" FooterStyle-BorderColor="White">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No." ControlStyle-CssClass="text-left">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-left" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Item" ControlStyle-CssClass="text-left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblItem" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-left" />
                                    </asp:TemplateField>



                                    <asp:BoundField HeaderText="Bill No." DataField="BILL_NO" ControlStyle-CssClass="text-left" ItemStyle-CssClass="text-left" />



                                    <%--  <asp:BoundField HeaderText="Bill Date" DataField="BILL_DATE" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" DataFormatString="{0:dd-MM-yyyy}" />--%>
                                    <asp:TemplateField HeaderText="Bill Date" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                                        <ItemTemplate>
                                            <%# Eval("BILL_DATE", "{0:dd-MM-yyyy}") %>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Total :
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" BorderColor="White" BorderStyle="None" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Amount" ControlStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblamt" runat="server" Text=' <%# Convert.ToDouble(Eval("BILL_AMT")).ToString("0.00") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" BorderColor="White" BorderStyle="None" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr style="border-bottom: 1px solid black">
                    <td id="tdRemarks" runat="server" colspan="2" style="text-align: left"></td>



                </tr>
                <tr style="border-top: 1px solid black; padding: 10px 0 10px 0">
                  <%--  <td style="width: 70%; text-align: left">Employee Signature:</td>
                    <td style="text-align: left">Date:</td>--%>
                      <td style="width: 70%; text-align: left"></td>
                    <td style="text-align: left"></td>
                </tr>
            </table>
            <br />
            <div>
                <h5 style="text-decoration: underline">Declaration :
                </h5>
                <p>
                    1. I hereby declare that reimbursement is as per rules of the company.<br />
                    2. Any discrepancy in the claim is my personal liability.
                </p>
            </div>
            <br />
            <div>
               <%-- <h5 style="text-decoration: underline">Notes :
                </h5>
                <p>
                    1. List all supporting in the given format totals must agree for each individual expenses claimed.<br />
                    2. Attach all supportings. Each bill must be serially numbered on the top right hand corner.<br />
                    3. Bills must have the subexian name. Bill date must pertain to the current financial year (Apr to Mar)
                </p>--%>
            </div>
            <div>
                <br />
             <%--   <%= DateTime.Now.ToString("dd - MMMM - yyyy HH:mm:sss") %>--%>
            </div>

        </div>
            <br/> <br/>

            <div id="divLTA" runat="server" style="border: 1px solid; padding: 3px"  visible="false">





                                    <h5 class="header-title text-center margin5rem">LTA Form</h5>


                                    <h5 class="header-title margin5rem">Relations</h5>
                                   <%-- <asp:GridView ID="grdLTARelps" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FAMTX_text" HeaderText="Relationship Type" />
                                            <asp:BoundField DataField="FAMTX" HeaderText="Relationship Type" Visible="false" />
                                            <asp:BoundField DataField="FCNAM" HeaderText="Name" />
                                            <asp:BoundField DataField="FGBDT" HeaderText="Date of Birth" DataFormatString="{0:dd/MM/yyyy}" />
                                            <%--<asp:BoundField DataField="FASEX" HeaderText="Gender" />--%>
                                       <%--     <asp:TemplateField HeaderText="Gender">
                                                <ItemTemplate>
                                                    <%#Eval("FASEX").ToString() == "1" ? "Male" : "Female" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dependent?">
                                                <ItemTemplate>
                                                    <%#Eval("DEPDT").ToString() == "n" ? "No" : "Yes" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="DEPDT" HeaderText="Dependent?" DataFormatString="{0:dd/MM/yyyy}" />--%>
                                    
                                            <%--<asp:BoundField DataField="ID" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />--%>
                                        <%--</Columns>
                                    </asp:GridView>--%>
                                  <%--  <table class="gridviewNew  table-responsive" id="tblLTAReL" runat="server">
                                        <tr>
                                            <th>Relationship Type</th>
                                            <th>Name
                                            </th>
                                            <th>Date of Birth
                                            </th>
                                           <%-- <th>Gender
                                            </th>
                                            <th>Dependent?
                                            </th>--%>
                                            <%--<th></th>
                                        </tr>
                                        </table>--%>
                                           <asp:GridView ID="Grdbind" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew"  DataKeyNames="FBPC_ID" Visible="false">
                                        <Columns>
                                           <%-- <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:BoundField DataField="ID" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />--%>
                                            <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"  />
                                           <asp:BoundField DataField="LID" HeaderText="LID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                                             <asp:BoundField DataField="FBPC_ID" HeaderText="FBP ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="FAMTX" HeaderText="Relationship Type" />
                                          <%--  <asp:BoundField DataField="FAMTX" HeaderText="Relationship Type" Visible="false" />--%>
                                          <asp:BoundField DataField="FCNAM" HeaderText="Name" />
                                            <asp:BoundField DataField="FGBDT" HeaderText="Date of Birth" DataFormatString="{0:dd/MM/yyyy}" />
                                                 </Columns>
                                    </asp:GridView>

                                           

                                      <%--  <tr>
                                            <td>

                                                <%--<asp:Label ID="lbldropdowname" runat="server" Text=""></asp:Label>
                                          <%--  </td>--%>
                                            <%--<td>--%>
                                             <%--  <asp:Label ID="lblrelname" runat="server" Text=""></asp:Label>--%>
                                            <%--</td>--%>

                                           <%-- <td>--%>
                                              <%--<asp:Label ID="Lbaltadob" runat="server" Text=""></asp:Label>--%>
                                          <%--  </td>
                                            <td>--%>
                                                <%-- <asp:Label ID="Lblltagender" runat="server" Text=""></asp:Label>--%>
                                             <%--    </td>
                                            <td>--%>
                                              <%--  <asp:CheckBox ID="chkLTARelDepend" CssClass="form-control-file" runat="server" Text=" " />--%>
                                           <%-- </td>
                                            <td>
                                              
                                            </td>
                                        </tr>
                                    </table>--%>


                                        <div>

                                    <h5 class="header-title margin5rem">Travel Details</h5>
                                    <%--<table>
                                        <tr>

                                            <td style="width: 20%">Journey Start Date from Destination :</td>
                                            <td class="">
                                               <%-- <asp:TextBox ID="txtLTAHeadJStartdt" runat="server" CssClass="form-control" Enabled="false" MaxLength="10"></asp:TextBox></td>--%>
                                                <%--   <asp:Label ID="Lblltastartdate" runat="server" Text=""></asp:Label>
                                                </td>
                                            <td class="">Journey End Date to Destination :
                                            </td>--%>
                                          <%--  <td class="">
                                             <%--   <asp:TextBox ID="txtLTAHeadJEnddt" runat="server" CssClass="form-control" Enabled="false" MaxLength="10"></asp:TextBox>--%>
                                                  <%--<asp:Label ID="LblLTAHeadJEnddt" runat="server" Text=""></asp:Label>
                                            </td>--%>
                                     <%--   </tr>
                                        <tr>
                                            <td class="">Mode of Travel :</td>
                                            <td class="">
                                               <%-- <asp:TextBox ID="txtLTAHeadModeofTrvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLTAHeadModeofTrvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>--%>
                                                 <%--<asp:Label ID="LblLTAHeadModeofTrvl" runat="server" Text=""></asp:Label>
                                            </td>--%>
                                           <%-- <td class="">Class of Travel :
                                            </td>--%>
                                           <%-- <td class="">
                                               <%-- <asp:TextBox ID="txtLTAHeadClasTrvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAHeadClasTrvl" runat="server" ControlToValidate="txtLTAHeadClasTrvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>--%>
                                                 <%--<asp:Label ID="LblLTAHeadClasTrvl" runat="server" Text=""></asp:Label>
                                            </td>--%>
                                     <%--   </tr>
                                        <tr>
                                            <td class="">Place of Departure :</td>
                                            <td class="">
                                              <%--  <asp:TextBox ID="txtLTAHeadPD" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAHeadPD" runat="server" ControlToValidate="txtLTAHeadPD" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>--%>
                                                 <%--<asp:Label ID="LblLTAHeadPD" runat="server" Text=""></asp:Label>
                                            </td>--%>
                                          <%--  <td class="">Place Arrival :
                                            </td>
                                            <td class="">--%>
                                              <%--  <asp:TextBox ID="txtLTAHEADPA" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAHEADPA" runat="server" ControlToValidate="txtLTAHEADPA" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>--%>
                                                 <%--<asp:Label ID="LblLTAHEADPA" runat="server" Text=""></asp:Label>
                                            </td>--%>
                                      <%--  </tr>
                                        <tr>--%>
                                            <%--<td class="">Are you part of entire Joureny?</td>
                                            <td>--%>
                                               <%-- <asp:RadioButtonList ID="rbtnLTAPartofJ" runat="server" CssClass="form-control-file" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                              <%--  <asp:Label ID="LblLTAPartofJ" runat="server" Text=""></asp:Label>

                                            </td>
                                        </tr>--%>


                                  <%--  </table>--%>
                                            <asp:GridView ID="Grdload" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew"  DataKeyNames="FBPC_ID" >
                                        <Columns>

                                           <asp:BoundField DataField="FBPC_ID" HeaderText="FBP ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="MTRVL" HeaderText="Mode of Travel" />
                                            <asp:BoundField DataField="CTRVL" HeaderText="Class of Travel" />
                                            <asp:BoundField DataField="JBGDT" HeaderText="Date of  Departure" />
                                            <asp:BoundField DataField="STPNT" HeaderText="Place of Departure" />
                                            <asp:BoundField DataField="JENDT" HeaderText="Date of Arriaval" />
                                            <asp:BoundField DataField="DESTN" HeaderText="Place Arrival" />

                                            </Columns>
                                                 </asp:GridView>


                                    <asp:GridView ID="grdLTATrvlDetails" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle" >
                                        <Columns>
                                           <%-- <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="MTRVL" HeaderText="Mode of Travel" />
                                            <asp:BoundField DataField="CTRVL" HeaderText="Class of Travel" />
                                            <asp:BoundField DataField="JBGDT" HeaderText="Date of  Departure" />
                                            <asp:BoundField DataField="STPNT" HeaderText="Place of Departure" />
                                            <asp:BoundField DataField="JENDT" HeaderText="Date of Arriaval" />
                                            <asp:BoundField DataField="DESTN" HeaderText="Place Arrival" />
                                            <%--<asp:BoundField DataField="KM_TRVLD" HeaderText="KM Travelled" />
                                            <asp:BoundField DataField="AMOUNTLTA" HeaderText="Amount As per ticket" />
                                            <asp:BoundField DataField="TKTNO" HeaderText="Ticket Number" />--%>




                                             


                                            <%--<asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="false" title="Edit Details" CssClass="fe-edit-1"
                                                        CommandName="EDITROW" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                    &nbsp; &nbsp;
                                                    <asp:LinkButton ID="LbtnDeleteFile" runat="server" CausesValidation="false" title="Delete" CssClass="fe-trash-2"
                                                        CommandName="DELETEROW" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:BoundField DataField="ID" HeaderText="" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                        </Columns>
                                    </asp:GridView>
                                    <%--<table class="gridviewNew margin5rem table-responsive" id="tblLTATRvl" runat="server">
                                        <tr>
                                            <th>Mode of Travel
                                            </th>
                                            <th>Class of Travel
                                            </th>
                                            <th>Date of  Departure</th>

                                            <th>Place of Departure
                                            </th>
                                            <th>Date of Arriaval
                                            </th>
                                            <th>Place Arrival
                                            </th>
                                            <th>KM Travelled
                                            </th>
                                            <th>Amount As per ticket
                                            </th>
                                            <th>Ticket Number
                                            </th>
                                            <th></th>
                                        </tr>
                                        <tr>
                                        


--%>

                                            <br/> <br/>

  <asp:GridView ID="Grdltadetails" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew"  DataKeyNames="FBPC_ID" >
                                        <Columns>

                                           <asp:BoundField DataField="FBPC_ID" HeaderText="FBP ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="MTRVL" HeaderText="Mode of Travel" />
                                            <asp:BoundField DataField="CTRVL" HeaderText="Class of Travel" />
                                            <asp:BoundField DataField="JBGDT" HeaderText="Date of  Departure" />
                                            <asp:BoundField DataField="STPNT" HeaderText="Place of Departure" />
                                            <asp:BoundField DataField="JENDT" HeaderText="Date of Arriaval" />
                                            <asp:BoundField DataField="DESTN" HeaderText="Place Arrival" />
                                             <asp:BoundField DataField="KM_TRVLD" HeaderText="KM Travelled" />
                                             <asp:BoundField DataField="AMOUNT" HeaderText="Amount As per ticket" />
                                             <asp:BoundField DataField="TKTNO" HeaderText="Ticket Number" />

                                            </Columns>
                                                 </asp:GridView>


 <%-- <asp:GridView ID="Grdtraveldetails" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" EmptyDataText="No Record Found!" EmptyDataRowStyle-CssClass="emptygridstyle" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="MTRVL" HeaderText="Mode of Travel" />
                                            <asp:BoundField DataField="CTRVL" HeaderText="Class of Travel" />
                                            <asp:BoundField DataField="JBGDT" HeaderText="Date of  Departure" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="STPNT" HeaderText="Place of Departure" />
                                            <asp:BoundField DataField="JENDT" HeaderText="Date of Arriaval" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="DESTN" HeaderText="Place Arrival" />
                                            <asp:BoundField DataField="KM_TRVLD" HeaderText="KM Travelled" />
                                            <asp:BoundField DataField="AMOUNTLTA" HeaderText="Amount As per ticket" />
                                            <asp:BoundField DataField="TKTNO" HeaderText="Ticket Number" />

                                              </Columns>
      </asp:GridView>--%>

                                            <td>
                                              <%--  <asp:TextBox ID="txtLTAModeTrvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAModeTrvl" runat="server" ControlToValidate="txtLTAModeTrvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FTBE_txtLTAModeTrvl" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="LowercaseLetters,UppercaseLetters" TargetControlID="txtLTAModeTrvl"></cc1:FilteredTextBoxExtender>--%>
                                                 <asp:Label ID="LblLTAModeTrvl" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                              <%--  <asp:TextBox ID="txtLTAClsTrvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAClsTrvl" runat="server" ControlToValidate="txtLTAClsTrvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtLTAClsTrvl" runat="server" ValidChars=" " FilterMode="ValidChars" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" TargetControlID="txtLTAClsTrvl"></cc1:FilteredTextBoxExtender>--%>
                                                 <asp:Label ID="LblLTAClsTrvl" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                               <%-- <asp:TextBox ID="txtLTADtofDep" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTADtofDep" runat="server" ControlToValidate="txtLTADtofDep" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="MEE_txtLTADtofDep" runat="server" AcceptNegative="Left"
                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtLTADtofDep" />
                                                <cc1:CalendarExtender ID="CE_txtLTADtofDep" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtLTADtofDep">
                                                </cc1:CalendarExtender>--%>

                                                 <asp:Label ID="LblLTADtofDep" runat="server" Text=""></asp:Label>

                                            </td>
                                            <td>
                                                <%--<asp:TextBox ID="txtLTAPlaceDep" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAPlaceDep" runat="server" ControlToValidate="txtLTAPlaceDep" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>--%>
                                                  <asp:Label ID="LblLTAPlaceDep" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                              <%--  <asp:TextBox ID="txtLTADtArvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTADtArvl" runat="server" ControlToValidate="txtLTADtArvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="MEE_txtLTADtArvl" runat="server" AcceptNegative="Left"
                                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtLTADtArvl" />
                                                <cc1:CalendarExtender ID="CE_txtLTADtArvl" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                    TargetControlID="txtLTADtArvl">
                                                </cc1:CalendarExtender>
                                                <asp:CompareValidator ID="cvtxtStartDate" runat="server"
                                                    ControlToCompare="txtLTADtofDep" CultureInvariantValues="true"
                                                    Display="Dynamic" EnableClientScript="true"
                                                    ControlToValidate="txtLTADtArvl"
                                                    ErrorMessage="Invalid!"
                                                    Type="Date" SetFocusOnError="true" Operator="GreaterThanEqual"
                                                    Text="Invalid!" ForeColor="Red" ValidationGroup="LTATRVL"></asp:CompareValidator>--%>
                                                <asp:Label ID="LblLTADtArvl" runat="server" Text=""></asp:Label>

                                            </td>
                                            <td>
                                               <%-- <asp:TextBox ID="txtLTAPlaceArvl" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAPlaceArvl" runat="server" ControlToValidate="txtLTAPlaceArvl" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>--%>
                                                 <asp:Label ID="LblLTAPlaceArvl" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <%--<asp:TextBox ID="txtLTAKMTvld" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FTBE_txtLTAKMTvld" runat="server" ValidChars="." FilterMode="ValidChars" FilterType="Numbers,Custom" TargetControlID="txtLTAKMTvld"></cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_txtLTAKMTvld" runat="server" ControlToValidate="txtLTAKMTvld" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>--%>
                                                  <asp:Label ID="LblLTAKMTvld" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <%--<asp:TextBox ID="txtLTAAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FTBE_txtLTAAmount" runat="server" ValidChars="." FilterMode="ValidChars" FilterType="Numbers,Custom" TargetControlID="txtLTAAmount"></cc1:FilteredTextBoxExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1_txtLTAAmount" runat="server" ControlToValidate="txtLTAAmount" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTATRVL"></asp:RequiredFieldValidator>--%>
                                                <asp:Label ID="LblLTAAmount" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <%--<asp:TextBox ID="txtLTATicketNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                <cc1:FilteredTextBoxExtender ID="FTBE_txtLTATicketNo" runat="server" ValidChars="" FilterMode="ValidChars" FilterType="LowercaseLetters,Numbers,UppercaseLetters" TargetControlID="txtLTATicketNo"></cc1:FilteredTextBoxExtender>--%>
                                                 <asp:Label ID="LblLTATicketNo" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                               
                                            </td>
                                        </tr>
                                    </table>
                                  <%--  <div id="Note" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h5 class="header-title">Declaration</h5>
                                                <ul>
                                                    <li>The block period of four years i.e, (<asp:TextBox ID="txtLTAblky1" runat="server" CssClass="linetext" MaxLength="4" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLTAblky1" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTAblky2" CssClass="linetext" runat="server" MaxLength="4"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLTAblky2" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTAblky3" CssClass="linetext" runat="server" MaxLength="4"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLTAblky3" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTAblky4" CssClass="linetext" runat="server" MaxLength="4"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLTAblky4" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>) and
                                                    </li>
                                                    <li>that I have not availed LTA exemption till date during the above block period.
                                                    </li>
                                                    <li>that I have claimed LTA exemption once during the year
                                                    <asp:TextBox ID="txtLTACLkyear" runat="server" MaxLength="4" CssClass="linetext"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLTACLkyear" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>
                                                        falling in the block period of four year i.e, (<asp:TextBox ID="txtLTACL1" runat="server" CssClass="linetext" MaxLength="4" AutoPostBack="true"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLTACL1" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTACL2" runat="server" MaxLength="4" CssClass="linetext"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtLTACL2" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTACL3" runat="server" MaxLength="4" CssClass="linetext"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtLTACL3" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>,
                                                        <asp:TextBox ID="txtLTACL4" runat="server" MaxLength="4" CssClass="linetext"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtLTACL4" ErrorMessage="*" ForeColor="Red" Display="Dynamic" SetFocusOnError="true" ValidationGroup="LTA"></asp:RequiredFieldValidator>).</li>
                                                </ul>
                                            </div>
                                        </div>
                                        </div>--%>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h5 class="header-title">Note</h5>
                                                <ul>
                                                    <li>If the journey is performed by air: actual air fare incurred subject to the max, of economy class of a standard carrier.<br />
                                                        Boarding pass has to be enclosed along with original ticket.
                                                    </li>
                                                    <li>If the journey is performed by rail: actual rail fare incurred subject to the max, of First AC rail fare.
                                                    </li>
                                                </ul>
                                            </div>
                                            </div>
    </div>
                                        </div></div></div>





            




        <div id="editor"></div>



        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.22/pdfmake.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
        <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.5.3/jspdf.min.js"></script>
        <script type="text/javascript">
            //$("body").on("click", "#btnExport", function () {

            //    $("#exportFormat").css("display", "block");
            //    alert("a");
            //    html2canvas($('#ContentPlaceHolder1_MainContent_exportFormat')[0], {
            //        onrendered: function (canvas) {
            //            var data = canvas.toDataURL();
            //            var docDefinition = {
            //                content: [{
            //                    image: data,
            //                    width: 500
            //                }]
            //            };
            //            pdfMake.createPdf(docDefinition).download("Table.pdf");
            //        }
            //    });
            //    $("#exportFormat").css("display", "none");
            //});
            $(window).on('load', function () {
                var doc = new jsPDF();
                var specialElementHandlers = {
                    '#editor': function (element, renderer) {
                        return true;
                    }
                };

                $('#btnExport').click(function () {
                    doc.fromHTML($('#ContentPlaceHolder1_MainContent_exportFormat').html(), 15, 15, {
                        'width': 170,
                        'elementHandlers': specialElementHandlers
                    });
                    doc.save('sample-file.pdf');
                });
            });
            function exportTableToExcel(tableID, filename = '') {
                var downloadLink;
                var dataType = 'application/vnd.ms-excel';
                var tableSelect = document.getElementById(tableID);
                var tableHTML = tableSelect.outerHTML.replace(/ /g, '%20');

                // Specify file name
                filename = filename ? filename + '.xls' : 'excel_data.xls';

                // Create download link element
                downloadLink = document.createElement("a");

                document.body.appendChild(downloadLink);

                if (navigator.msSaveOrOpenBlob) {
                    var blob = new Blob(['\ufeff', tableHTML], {
                        type: dataType
                    });
                    navigator.msSaveOrOpenBlob(blob, filename);
                } else {
                    // Create a link to the file
                    downloadLink.href = 'data:' + dataType + ', ' + tableHTML;

                    // Setting the file name
                    downloadLink.download = filename;

                    //triggering the function
                    downloadLink.click();
                }
            }


                function createPDF() {
                    var sTable = document.getElementById('ContentPlaceHolder1_MainContent_exportFormat').innerHTML;

                    var style = "<style>";
                    style = style + "table {width: 100%;font: 17px Calibri;}";
                    style = style + "table, th, td {border: solid 1px #DDD; border-collapse: collapse;";
                    style = style + "padding: 2px 3px;text-align: center;}";
                    style = style + "</style>";

                    // CREATE A WINDOW OBJECT.
                    var win = window.open('', '', 'height=700,width=700');

                    win.document.write('<html><head>');
                    win.document.write('<title>FBP</title>');   // <title> FOR PDF HEADER.
                    win.document.write(style);          // ADD STYLE INSIDE THE HEAD TAG.
                    win.document.write('</head>');
                    win.document.write('<body>');
                    win.document.write(sTable);         // THE TABLE CONTENTS INSIDE THE BODY TAG.
                    win.document.write('</body></html>');

                    win.document.close(); // CLOSE THE CURRENT WINDOW.

                    win.print();    // PRINT THE CONTENTS.

                    //var doc = new jsPDF();
                    //doc.addHTML('#ContentPlaceHolder1_MainContent_exportFormat', function () {
                    //    doc.save('html.pdf');
                    //});
                }
        </script>
            <asp:HiddenField ID="viewcheck" runat="server" />
</asp:Content>
