<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="FBP_PendingClaims.aspx.cs"
    Inherits="iEmpPower.UI.FBP.FBP_PendingClaims" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
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

    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item"><a href="FBP.aspx?PC=H">FBP Claims History</a></li>
                        <li class="breadcrumb-item active">FBP Claims Details</li>
                    </ol>
                </div>
                <h4 class="page-title">FBP Claims Details</h4>
                <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
                <asp:Label ID="LblMsg" runat="server" CssClass="msgboard"></asp:Label>
            </div>
        </div>
    </div>
    <!-- end page title -->


    <div class="row">
        <div class="col-lg-12">
            <div class="tab-content m-0 p-0">
                <div class="card-box">
                    <div class="responsive-table">
                        <table class="table table-sm table_font_sm">
                            <thead>
                                <tr class="bg-light">
                                    <th colspan="16">
                                        <h4>FBP Claims Details</h4>
                                    </th>
                                </tr>
                            </thead>
                        </table>

                        <asp:GridView ID="grd_FbpClaims_History" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None" Width="99%"
                            OnRowCommand="grd_FbpClaims_History_RowCommand" DataKeyNames="FBPC_IC,LGART,STATUS,BETRG,ALLOWANCETEXT,OVERRIDE_AMT,CREATED_ON,APPROVEDON,REMARKS"
                            AllowPaging="true" AllowSorting="true" PageSize="10" OnSorting="grd_FbpClaims_History_Sorting" OnPageIndexChanging="grd_FbpClaims_History_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl. No." ControlStyle-CssClass="col-center">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                    <ItemStyle CssClass="col-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FBP Claim Id" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                    <ItemTemplate>
                                        <%# Eval("FBPC_IC") %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Allowance" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                    <ItemTemplate>
                                        <%--<%# Eval("LGART") %> - <%# Eval("ALLOWANCETEXT") %>--%>
                                        <%# Eval("ALLOWANCETEXT") %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Date" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" SortExpression="BEGDA">
                        <ItemTemplate>
                            <%--    <%# Eval("BEGDA") %>
                            <%# Eval("BEGDA", "{0:dd-MM-yyyy}") %>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Claimed Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("BETRG")).ToString("#,##0.00") %>
                                        <%--   <%# Eval("BETRG") %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Override Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" Visible="false">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("OVERRIDE_AMT")).ToString("#,##0.00") %>
                                        <%--<%# Eval("OVERRIDE_AMT") %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("APPAMT")).ToString("#,##0.00") %>
                                        <%--  <%# Eval("APPAMT") %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Created on" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                    <ItemTemplate>
                                        <%--    <%# Eval("CREATED_ON") %>--%>
                                        <%# Eval("CREATED_ON", "{0:dd-MM-yyyy}") %>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                    <ItemTemplate>

                                        <%# Eval("REMARKS") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                       <%-- <%# Eval("STATUS") %>--%>
                                         <%--<asp:LinkButton ID="lbtnStatus" runat="server" CssClass='<%#Eval("STATUS").ToString() == "Approved"? "btn btn-xs btn-success waves-effect waves-light" :Eval("Status").ToString() == "Rejected" ?"btn btn-xs btn-danger waves-effect waves-light btn-block": "btn btn-xs btn-blue waves-effect waves-light" %>' CausesValidation="False" Text='<%#  Eval("STATUS").ToString() %>'></asp:LinkButton>--%>
                                             <asp:LinkButton ID="lbtnStatus" Enabled="false" Width="100px" ForeColor="White" runat="server" CssClass='<%#Eval("STATUS").ToString() == "Approved"? "btn btn-xs btn-success waves-light" :Eval("Status").ToString() == "Rejected" ?"btn btn-xs btn-danger waves-light btn-block": "btn btn-xs btn-blue waves-light" %>' CausesValidation="False" Text='<%#  Eval("STATUS").ToString() %>'></asp:LinkButton>

                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved on" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center">
                                    <%--SortExpression="APPROVEDON"--%>
                                    <ItemTemplate>
                                        <%--    <%# Eval("APPROVED_ON") %>--%>
                                        <%#(Eval("APPROVEDON","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVEDON","{0:dd-MM-yyyy}")%>
                                        <%--   <%# Eval("APPROVEDON", "{0:dd-MM-yyyy}") %>--%>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnFbpclaimsView" runat="server" CausesValidation="False" CommandName="VIEW" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CssClass="btn-xs btn-warning" ForeColor="White" title="View Details"><i class="fe-eye"></i></asp:LinkButton>

                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                            <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                            <SelectedRowStyle BackColor="Silver" />
                        </asp:GridView>

                        <div class="DivSpacer01">
                            <br />
                        </div>
                    </div>
                    <div id="divitems" runat="server">

                        <asp:GridView ID="grd_CalimsItems" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None">
                            <Columns>


                                <%--  <asp:BoundField HeaderText="Items" DataField="ALLOWANCETEXT" ItemStyle-Width="45%" />--%>
                                <asp:TemplateField HeaderText="Allowance" ItemStyle-Width="45%">
                                    <ItemTemplate>
                                        <%-- <%# Eval("LGART") %> - <%# Eval("ALLOWANCETEXT") %>--%>
                                        <%# Eval("ALLOWANCETEXT") %>
                                    </ItemTemplate>

                                </asp:TemplateField>

                                <%-- <asp:BoundField HeaderText="Entitlement" DataField="ANNUAL" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify"> /--%>
                                <asp:TemplateField HeaderText="Annual Entitlement" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("ANNUAL")).ToString("#,##0.00") %>
                                        <%--   <%# Eval("ANNUAL") %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:BoundField HeaderText="Claims Paid" DataField="BETRG" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                <asp:TemplateField HeaderText="Claims Paid" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("BETRG")).ToString("#,##0.00") %>
                                        <%--   <%# Eval("BETRG") %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%--<asp:BoundField HeaderText="Claims Pending" DataField="PENDINGAMT" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                <asp:TemplateField HeaderText="Claims Pending" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("PENDINGAMT")).ToString("#,##0.00") %>
                                        <%--   <%# Eval("PENDINGAMT") %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:BoundField HeaderText="Balance" DataField="BALANCE" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>
                                <asp:TemplateField HeaderText="Balance" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                    <ItemTemplate>
                                        <%# Convert.ToDouble(Eval("BALANCE")).ToString("#,##0.00") %>
                                        <%--   <%# Eval("BALANCE") %>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>


                        </asp:GridView>

                        <br />
                        <h4 class="header-title">Bill Details</h4>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="gridviewNew" GridLines="None"
                            OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl No." ControlStyle-CssClass="col-center" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                    <ItemStyle CssClass="col-center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Bill No." DataField="BILL_NO" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" />

                                <asp:BoundField HeaderText="Bill Date" DataField="BILL_DATE" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" DataFormatString="{0:dd-MM-yyyy}" />

                                <asp:BoundField HeaderText="Remarks" DataField="RELATIONSHIP" ControlStyle-CssClass="col-center" ItemStyle-CssClass="col-center" />

                                <%--   <asp:BoundField HeaderText="Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" DataField="BILL_AMT" />--%>

                                <asp:TemplateField HeaderText="Claimed Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
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
                                        <%-- <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />--%>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="Lbtndownload" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                        <div class="DivSpacer01"></div>
                        <div class="buttonrow" id="divbutton" runat="server" visible="false" style="margin-top: 5px;">
                            <table>
                                <tr>

                                    <td>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine" Placeholder="Remarks" TabIndex="6" ValidationGroup="vg1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFV_txtRemarks" runat="server" ControlToValidate="txtRemarks" ErrorMessage="Please enter Remarks" ForeColor="Red" ValidationGroup="vg1"></asp:RequiredFieldValidator>
                                    </td>

                                    <td>
                                        <asp:Button ID="btnWithdraw" runat="server" Text="Withdraw" OnClick="btnWithdraw_Click" ValidationGroup="vg1" TabIndex="7" CssClass="btn btn-xs btn-secondary" />
                                    </td>
                                </tr>
                            </table>





                        </div>

                    </div>
                    <div class="DivSpacer01">
                        <br />
                    </div>


                    <asp:HiddenField ID="viewcheck" runat="server" />
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UPFbp" runat="server">
            <ContentTemplate>
                <div id="Exportbtn" class="form-group" runat="server">
                    <div class="col-sm-12">
                        <asp:Button ID="BtnExporttoXl" runat="server" Text="Export To Excel" OnClientClick='exportTableToExcel("ContentPlaceHolder1_MainContent_exportFormat", "FBP_Claim")' CausesValidation="false" TabIndex="6" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                        &nbsp;&nbsp;
                    <asp:Button ID="BtnExporttoPDF" runat="server" Text="Export To PDF" OnClientClick="createPDF();" TabIndex="7" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                        <%--    <asp:Button ID="BtnExporttoXl" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" CausesValidation="false" TabIndex="6" CssClass="btn bg-dark waves-effect waves-light btn-std"/>
        &nbsp;&nbsp;
                    <asp:Button ID="BtnExporttoPDF" runat="server" Text="Export To PDF" OnClick="ExportToPDF_Click" TabIndex="7" CssClass="btn bg-dark waves-effect waves-light btn-std"/>--%>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="BtnExporttoPDF" />
                <asp:PostBackTrigger ControlID="BtnExporttoXl" />
            </Triggers>
        </asp:UpdatePanel>
        <div id="maingrid" runat="server" visible="false">
            <div>
                <span id="bold" runat="server" style="font-weight: bold"></span>
            </div>
            <h2>FBP Claims History</h2>
            <%--<asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>--%>
            <br />
            <div style="width: 49%; float: left">
                <fieldset style="float: left">
                    <legend><b>&nbsp;Search FBP claims&nbsp;</b></legend>
                    <table>

                        <tr>
                            <td>&nbsp;Search&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="textbox" TabIndex="1">
                                    <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="FBP Claim ID" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Allowance ID" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Status" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </td>


                            <td>
                                <asp:TextBox ID="txtsearch" runat="server" CssClass="textbox" placeholder="Enter Text" TabIndex="2"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;Created On&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="txtCreatedOn" runat="server" TabIndex="3" placeholder="Select Date" Width="199px" Style="letter-spacing: 1px; background: #ffffff url('../../images/CalenderIMG.png') no-repeat 99% 55% !important; border: 1px solid #666666; margin-bottom: 4px; margin-left: 2px; padding: 2px;"></asp:TextBox>
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
            </div>

            <div class="DivSpacer01"></div>
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
                               <%-- <td style="text-align: left">Date :</td>
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
                     <%-- <td style="width: 70%; text-align: left">Employee Signature:</td>
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
                <%--<h5 style="text-decoration: underline">Notes :
                </h5>
                <p>
                    1. List all supporting in the given format totals must agree for each individual expenses claimed.<br />
                    2. Attach all supportings. Each bill must be serially numbered on the top right hand corner.<br />
                    3. Bills must have the subexian name. Bill date must pertain to the current financial year (Apr to Mar)
                </p>--%>
            </div>
            <div>
                <br />
               <%-- <%= DateTime.Now.ToString("dd - MMMM - yyyy HH:mm:sss") %>--%>
            </div>

        </div>

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
</asp:Content>
