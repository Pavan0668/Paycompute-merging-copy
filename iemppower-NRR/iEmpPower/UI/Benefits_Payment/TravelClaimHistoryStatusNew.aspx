<%@ Page Title="Travel Claim History" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="TravelClaimHistoryStatusNew.aspx.cs"
    Inherits="iEmpPower.UI.Benefits_Payment.TravelClaimHistoryStatusNew" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box">
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                        <li class="breadcrumb-item"><a href="Travel_Requests.aspx">Travel Requests</a></li>
                        <li class="breadcrumb-item active">Travel View</li>
                    </ol>
                </div>
                <h4 class="page-title">Travel Claim History&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>
                <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1" Font-Size="Medium"></asp:Label>
                    </span></h4>
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
                                    <th colspan="16">Indent Item Details</th>
                                </tr>
                            </thead>
                        </table>
                        <asp:GridView ID="grdClaimDetails" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" DataKeyNames="CID"
            OnRowCommand="grdClaimDetails_RowCommand" ShowFooter="True" FooterStyle-CssClass="foo01">
            <Columns>
                <asp:BoundField DataField="LID" HeaderText="Sl No" />
                <asp:BoundField DataField="CID" HeaderText="Claim Id" />
                <asp:BoundField DataField="EXP_TYPE" HeaderText="Expense Type" />

                <asp:BoundField DataField="S_DATE" HeaderText="Expense Date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="NO_DAYS" HeaderText="No of Days" />
                <asp:TemplateField HeaderText="Daily Rate" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("DAILY_RATE") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expenditure Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Convert.ToDouble(Eval("EXPT_AMT")).ToString("#,##0.00") %> ( <%# Eval("EXPT_CURR") %>)
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />
                <asp:TemplateField HeaderText="Reimbursable Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>  ( <%# Eval("RCURR") %>)
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ZLAND" HeaderText="Country" />
                <asp:BoundField DataField="ZORT1" HeaderText="Region" />
                <asp:TemplateField HeaderText="Deviation Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#   (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" :  Convert.ToDouble(Eval("DEVIATION_AMT")).ToString("#,##0.00") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Deviation Currency">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#   (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Eval("DEVIATION_CURR") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="JUSTIFY" HeaderText="Justification" />
                <asp:BoundField DataField="RECEIPT_FILE" HeaderText="Original Receipt Missing" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        Attachments
                    </HeaderTemplate>
                    <ItemTemplate>
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                        <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />
                      </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="Lbtndownload" />
                                    </Triggers>

                                </asp:UpdatePanel>
                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
                    </div>
                </div>
            </div>
    <div class="table-responsive card-box" runat="server" visible="false" >
        <h4>Travel Claim History</h4>

        <br />


        <table>
            <tr>
                <td align="right">Select:</td>
                <td>
                    <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="form-control-file" TabIndex="1">
                        <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Claim ID" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Trip No" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Status" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" TabIndex="2"></asp:TextBox>
                </td>

                <td>
                    <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CausesValidation="false" TabIndex="3" CssClass="btn btn-xs btn-secondary" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnclear" runat="server" Text="Clear" OnClick="btnclear_Click" TabIndex="4" CssClass="btn btn-xs btn-secondary" /></td>
            </tr>

        </table>
        <br />
        <asp:GridView ID="grdAppRejTravel" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" OnRowCommand="grdAppRejTravel_RowCommand"
            DataKeyNames="CID,REINR,CREATED_BY,ENAME,WBS_ELEMT,ACTIVITY,RE_AMT,RCURR"
            AllowPaging="true" AllowSorting="true" OnSorting="grdAppRejTravel_Sorting" OnPageIndexChanging="grdAppRejTravel_PageIndexChanging" PageSize="5">

            <Columns>
                <asp:BoundField DataField="CID" HeaderText="Claim Id" SortExpression="CID" />
                <asp:BoundField DataField="REINR" HeaderText="Trip No" SortExpression="REINR" />


                <asp:BoundField DataField="CREATED_BY" HeaderText="Employee ID" SortExpression="CREATED_BY" />
                <asp:BoundField DataField="ENAME" HeaderText="Employee Name" SortExpression="ENAME" />

                <asp:BoundField DataField="WBS_ELEMT" HeaderText="Project" SortExpression="WBS_ELEMT" />

                <asp:BoundField DataField="ACTIVITY" HeaderText="Task" SortExpression="ACTIVITY" />


                <asp:TemplateField HeaderText="Total Reimbursement Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" SortExpression="RE_AMT">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>

                        <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="RCURR" HeaderText="Reimbursement Currency" SortExpression="RCURR" />

                <asp:BoundField DataField="CREATED_ON" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="CREATED_ON" />
                <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS" />


               
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="LbtnIExpenseView" runat="server" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'>View</asp:LinkButton>

                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <br />
        <br />
    </div>
    <asp:Panel ID="PnlIExpDetalsView" runat="server" CssClass="table-responsive card-box" Visible="false">
       <%-- <asp:GridView ID="grdClaimDetails" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" DataKeyNames="CID"
            OnRowCommand="grdClaimDetails_RowCommand" ShowFooter="True" FooterStyle-CssClass="foo01">
            <Columns>
                <asp:BoundField DataField="LID" HeaderText="Sl No" />
                <asp:BoundField DataField="CID" HeaderText="Claim Id" />
                <asp:BoundField DataField="EXP_TYPE" HeaderText="Expense Type" />

                <asp:BoundField DataField="S_DATE" HeaderText="Expense Date" DataFormatString="{0:dd-MMM-yyyy}" />
                <asp:BoundField DataField="NO_DAYS" HeaderText="No of Days" />
                <asp:TemplateField HeaderText="Daily Rate" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Eval("DAILY_RATE") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expenditure Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Convert.ToDouble(Eval("EXPT_AMT")).ToString("#,##0.00") %> ( <%# Eval("EXPT_CURR") %>)
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />
                <asp:TemplateField HeaderText="Reimbursable Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>  ( <%# Eval("RCURR") %>)
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ZLAND" HeaderText="Country" />
                <asp:BoundField DataField="ZORT1" HeaderText="Region" />
                <asp:TemplateField HeaderText="Deviation Amount" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#   (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" :  Convert.ToDouble(Eval("DEVIATION_AMT")).ToString("#,##0.00") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Deviation Currency">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <%#   (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Eval("DEVIATION_CURR") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="JUSTIFY" HeaderText="Justification" />
                <asp:BoundField DataField="RECEIPT_FILE" HeaderText="Original Receipt Missing" />
                <asp:TemplateField>
                    <HeaderTemplate>
                        Attachments
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" />
                    </ItemTemplate>
                    <ItemStyle Width="100" />
                </asp:TemplateField>

            </Columns>
        </asp:GridView>--%>

        <br />
        <br />
        <asp:GridView ID="grdAppRejHistory" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" ShowHeader="false" Visible="false">

            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <table class="TblCls">

                            <asp:Panel ID="Panel8" runat="server" Visible='<%# (Eval("STATUS")).ToString()=="Saved"?false:true %>'>

                                <tr>
                                    <th class="Tblth">Employee ID</th>
                                    <th class="Tblth">Employee Name</th>
                                    <th class="Tblth">Action</th>
                                    <th class="Tblth">Action Date</th>
                                    <th class="Tblth">Comments</th>
                                </tr>
                                <asp:Panel ID="pnlAPPROVEDBY1" runat="server" Visible='<%# (Eval("APPROVED_BY1")).ToString()==""?false:true %>'>
                                    <tr>
                                        <td class="Tbltd"><%# Eval("APPROVED_BY1") %> </td>
                                        <td class="Tbltd"><%#(Eval("APPROVED_BY1").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY1N")%></td>
                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": (Eval("STATUS").ToString()=="Rejected1")?"Rejected": (Eval("STATUS").ToString()=="Approved1")?"Approved":"Approved"%> </td>
                                        
                                        <td class="Tbltd"><%#(Eval("APPROVED_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON1","{0:dd-MM-yyyy}")%></td>

                                       

                                        <td class="Tbltd"><%# Eval("REMARKS1") %></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="pnlAPPROVEDBY2" runat="server" Visible='<%# (Eval("APPROVED_BY2")).ToString()==""?false:true %>'>
                                    <tr>
                                        <td class="Tbltd"><%# Eval("APPROVED_BY2") %></td>
                                        <td class="Tbltd"><%#(Eval("APPROVED_BY2").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY2N") %></td>
                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved2")?"Approved":"Approved"%> </td>
                                        <td class="Tbltd"><%#(Eval("APPROVED_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON2","{0:dd-MM-yyyy}")%></td>


                                       

                                        <td class="Tbltd"><%# Eval("REMARKS2") %></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="Panel1" runat="server" Visible='<%# (Eval("APPROVED_BY3")).ToString()==""?false:true %>'>
                                    <tr>
                                        <td class="Tbltd"><%# Eval("APPROVED_BY3") %></td>
                                        <td class="Tbltd"><%# (Eval("APPROVED_BY3").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY3N") %></td>
                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved1")||(Eval("STATUS").ToString()=="Approved2")?"Pending": (Eval("STATUS").ToString()=="Rejected3")?"Rejected":(Eval("STATUS").ToString()=="Rejected2")|| (Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved3")?"Approved":"Approved"%> </td>
                                        <td class="Tbltd"><%#(Eval("APPROVED_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON3","{0:dd-MM-yyyy}")%></td>
                                        


                                        <td class="Tbltd"><%# Eval("REMARKS3") %></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="Panel2" runat="server" Visible='<%# (Eval("APPROVED_BY4")).ToString()==""?false:true %>'>
                                    <tr>
                                        <td class="Tbltd"><%# Eval("APPROVED_BY4") %></td>
                                        <td class="Tbltd"><%# (Eval("APPROVED_BY4").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY4N") %></td>
                                        <td class="Tbltd"><%#(Eval("STATUS").ToString()=="Requested")|| (Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected4")?"Rejected": (Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved4")?"Approved":"Approved"%> </td>
                                        <td class="Tbltd"><%#(Eval("APPROVED_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON4","{0:dd-MM-yyyy}")%></td>
                                       

                                        <td class="Tbltd"><%# Eval("REMARKS4") %></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="Panel3" runat="server" Visible='<%# (Eval("APPROVED_BY5")).ToString()==""?false:true %>'>
                                    <tr>
                                        <td class="Tbltd"><%# Eval("APPROVED_BY5") %></td>
                                        <td class="Tbltd"><%# (Eval("APPROVED_BY5").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY5N") %></td>
                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected5")?"Rejected": (Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved5")?"Approved":"Approved"%> </td>
                                        <td class="Tbltd"><%#(Eval("APPROVED_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON5","{0:dd-MM-yyyy}")%></td>
                                        

                                        <td class="Tbltd"><%# Eval("REMARKS5") %></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="Panel4" runat="server" Visible='<%# (Eval("APPROVED_BY6")).ToString()==""?false:true %>'>
                                    <tr>
                                        <td class="Tbltd"><%# Eval("APPROVED_BY6") %></td>
                                        <td class="Tbltd"><%# (Eval("APPROVED_BY6").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY6N") %></td>
                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%> </td>
                                        <td class="Tbltd"><%#(Eval("APPROVED_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON6","{0:dd-MM-yyyy}")%></td>
                                        

                                        <td class="Tbltd"><%# Eval("REMARKS6") %></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="Panel5" runat="server" Visible='<%# (Eval("APPROVED_BY7")).ToString()==""?false:true %>'>
                                    <tr>
                                        <td class="Tbltd"><%# Eval("APPROVED_BY7") %></td>
                                        <td class="Tbltd"><%# (Eval("APPROVED_BY7").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY7N") %></td>
                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%> </td>
                                        <td class="Tbltd"><%#(Eval("APPROVED_ON7","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON7","{0:dd-MM-yyyy}")%></td>
                                        

                                        <td class="Tbltd"><%# Eval("REMARKS7") %></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="Panel6" runat="server" Visible='<%# (Eval("APPROVED_BY8")).ToString()==""?false:true %>'>
                                    <tr>
                                        <td class="Tbltd"><%# Eval("APPROVED_BY8") %></td>
                                        <td class="Tbltd"><%# (Eval("APPROVED_BY8").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY8") %></td>
                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%> </td>
                                        <td class="Tbltd"><%#(Eval("APPROVED_ON8","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON8","{0:dd-MM-yyyy}")%></td>
                                       

                                        <td class="Tbltd"><%# Eval("REMARKS8") %></td>
                                    </tr>
                                </asp:Panel>
                                <asp:Panel ID="Panel7" runat="server" Visible='<%# (Eval("APPROVED_BY9")).ToString()==""?false:true %>'>
                                    <tr>
                                        <td class="Tbltd"><%# Eval("APPROVED_BY9") %></td>
                                        <td class="Tbltd"><%# (Eval("APPROVED_BY9").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY9") %></td>
                                        <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%> </td>
                                        <td class="Tbltd"><%#(Eval("APPROVED_ON9","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON9","{0:dd-MM-yyyy}")%></td>
                                       

                                        <td class="Tbltd"><%# Eval("REMARKS9") %></td>
                                    </tr>
                                </asp:Panel>
                            </asp:Panel>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>


        </asp:GridView>


        <br />



    </asp:Panel>
    <div id="Exportbtn" runat="server" visible="false">
        <asp:Button ID="BtnExporttoXl" runat="server" Text="ExportToExcel" OnClick="btnExportToExcel_Click" CausesValidation="false" TabIndex="5" CssClass="btn bg-dark waves-effect waves-light btn-std" />
        &nbsp;&nbsp;
                    <asp:Button ID="BtnExporttoPDF" runat="server" Text="ExportToPDF" OnClick="ExportToPDF_Click" TabIndex="6" CssClass="btn bg-dark waves-effect waves-light btn-std" />

    </div>
          </div>

    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
</asp:Content>
