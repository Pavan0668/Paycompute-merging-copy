<%@ Page Title="Finance View Iexpense Status" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="FinanceViewAllIexpenseStatus.aspx.cs"
    Inherits="iEmpPower.Old_App_Code.iEmpPowerDAL.Benefits_Payment.FinanceViewAllIexpenseStatus" Theme="SkinFile"
    EnableEventValidation="false" Culture="en-GB" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        /*#MainContent_grdAppRejHistory
        {
            overflow: scroll !important;
        }

        #MainContent_PnlIExpDetalsView
        {
            overflow: scroll !important;
        }*/




        .TblCls
        {
            border-collapse: collapse;
            width: 100%;
        }

        .Tblth
        {
            border: 1px solid lightgrey;
            padding: 3px;
            background-color: #3470A7;
            color: white;
        }

        .Tbltd
        {
            border: 1px solid lightgrey;
            padding: 3px;
        }

        .UlCls01
        {
            list-style: none;
            margin: 0;
            padding: 0;
        }

        .Li01
        {
            padding: 2px 3px 2px 5px;
        }

        .Lbl01
        {
            padding: 2px 4px 2px 5px;
            font-size: 11px;
            font-weight: bold;
            display: inline-block;
            color: #004080;
            text-shadow: 1px 1px 1px #ddd;
        }

        .GvCls
        {
            font: normal normal normal 12px/20px "Segoe UI", Verdana,'Helvetica Neue';
            text-decoration: none;
        }

        .W01
        {
            width: 90px;
        }

        .W02
        {
            width: 138px;
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
                        <li class="breadcrumb-item active">Finance View iExpense status</li>
                    </ol>
                </div>
                <h4 class="page-title">Finance View iExpense status</h4>
                <asp:Label ID="LblMsg" runat="server"></asp:Label>
                <asp:Label ID="Label1" runat="server" CssClass="msgboard"></asp:Label>
            </div>
        </div>
    </div>
    <!-- end page title -->
    <div class="row">
        <div class="table-responsive card-box">
            <asp:Panel ID="pnlcontent" runat="server" DefaultButton="btnsearch">
                <h4>IExpense Details</h4>
                <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1"></asp:Label>
                <br />


                <div class="form-inline">
                    <div class="row">
                        <div class="col-sm-1">
                            &nbsp;Select&nbsp;
                        </div>
                        <div class="col-sm-2">
                            <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="textbox" TabIndex="1">
                                <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Expense ID" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Task" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Employee Name" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Status" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-2">
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" TabIndex="2" placeholder="Enter Text"></asp:TextBox>
                        </div>

                        <div class="col-sm-7"> 
                            <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" CausesValidation="false" TabIndex="3" CssClass="btn btn-xs btn-secondary" />
                            &nbsp;&nbsp;
                    <asp:Button ID="btnclear" runat="server" Text="Clear" OnClick="btnclear_Click" TabIndex="4" CssClass="btn btn-xs btn-secondary" />
                            &nbsp;&nbsp;
                    <asp:Button ID="PendingMnrApp" runat="server" TabIndex="5" Text="Pending Manager Approval" OnClick="btnPendingMnrApp_Click" Width="180px" CssClass="btn btn-xs btn-secondary" />
                            &nbsp;&nbsp; 
                    <asp:Button ID="PendingFinApp" runat="server" TabIndex="6" Text="Pending Finance Approval" OnClick="btnPendingFinApp_Click" Width="180px" CssClass="btn btn-xs btn-secondary" />
                        </div>
                    </div>


                </div>
                <br />
                <asp:GridView ID="grdIexpdetails" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" OnRowCommand="grdIexpdetails_RowCommand"
                    DataKeyNames="IEXP_ID,PURPOSE,CREATED_BY,ENAME,TASK,POST1,RE_AMT,RCURR,CREATED_ON" AllowPaging="true" AllowSorting="true" OnSorting="grdIexpdetails_Sorting" OnPageIndexChanging="grdIexpdetails_PageIndexChanging" PageSize="10">
                    <Columns>
                        <asp:BoundField DataField="IEXP_ID" HeaderText="Expense Id" />
                        <asp:BoundField DataField="POST1" HeaderText="Project" />
                        <%-- <asp:BoundField DataField="TASK" HeaderText="Task" SortExpression="TASK" />--%>
                        <asp:TemplateField HeaderText="Task">
                            <ItemTemplate>

                                <%#(Eval("TASK").ToString().Trim()=="B") ? "Billable" : "Non-Billable"%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Entity">
                            <ItemTemplate>
                                <%#(Eval("CREATED_BY").ToString().Split('-').First())%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee ID">
                            <ItemTemplate>
                                <%#(Eval("CREATED_BY").ToString().Split('-').Last())%>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%--       <asp:BoundField DataField="CREATED_BY" HeaderText="Employee ID" SortExpression="CREATED_BY" />--%>
                        <asp:BoundField DataField="ENAME" HeaderText="Employee Name" />
                        <%--<asp:BoundField DataField="RE_AMT" HeaderText="Total Reimbursement Amount" SortExpression="RE_AMT" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />--%>

                        <asp:TemplateField HeaderText="Total Reimbursement Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                            <EditItemTemplate>
                            </EditItemTemplate>
                            <ItemTemplate>

                                <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>
                                <%-- ( <%# Eval("WAERS") %>)--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RCURR" HeaderText="Reimbursement Currency" />
                        <asp:BoundField DataField="CREATED_ON" HeaderText="Created On" DataFormatString="{0:dd-MMM-yyyy}" />
                        <asp:BoundField DataField="STATUS" HeaderText="Status" />


                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnIExpenseView" runat="server" CssClass="btn btn-xs btn-warning" CausesValidation="False" CommandName="View" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'><i class="fe-eye"></i></asp:LinkButton>

                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="GridPager" HorizontalAlign="Right" />
                    <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" />
                    <SelectedRowStyle BackColor="Silver" />
                </asp:GridView>

                <br />
                <br />

                <asp:Panel ID="PnlIExpDetalsView" runat="server" Visible="false">
                    <asp:GridView ID="grdIexpDetailsView" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" DataKeyNames="ID"
                        OnRowCommand="grdIexpDetailsView_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="Sl No" />
                            <asp:BoundField DataField="IEXP_ID" HeaderText="Expense Id" />

                            <asp:BoundField DataField="LGTXT" HeaderText="Expense Type" />
                            <asp:BoundField DataField="S_DATE" HeaderText="Expense Date" DataFormatString="{0:dd-MMM-yyyy}" />
                            <asp:TemplateField HeaderText="Expenditure Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <%--  <%# Eval("EXPT_AMT") %> ( <%# Eval("EXPT_CURR") %>)--%>
                                    <%# Convert.ToDouble(Eval("EXPT_AMT")).ToString("#,##0.00") %> ( <%# Eval("EXPT_CURR") %>)
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" />
                            <asp:TemplateField HeaderText="Reimbursable Amount" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <%--   <%# Eval("RE_AMT") %> ( <%# Eval("RCURR") %>)--%>
                                    <%# Convert.ToDouble(Eval("RE_AMT")).ToString("#,##0.00") %>  ( <%# Eval("RCURR") %>)
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
                    <br />
                    <br />
                    <asp:GridView ID="grdAppRejHistory" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" ShowHeader="false">
                        <%--<Columns> 
                             
                                <asp:TemplateField HeaderText="Approver-1">
                                   <ItemTemplate>
                                     
                                      <%#(Eval("APPROVED_BY1").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY1N")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                 <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON1","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="REMARKS1" HeaderText="Comments" />


                               
                                <asp:TemplateField HeaderText="Approver-2">
                                   <ItemTemplate>
                                
                                    <%#(Eval("APPROVED_BY2").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY2N")%>    
                                    </ItemTemplate>
                                 </asp:TemplateField>
                               
                                 <asp:TemplateField HeaderText="Approved On">
                                   <ItemTemplate>
                              
                                 <%#(Eval("APPROVED_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON2","{0:dd-MM-yyyy}")%>
                                   </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="REMARKS2" HeaderText="Comments" />


                              
                                <asp:TemplateField HeaderText="Approver-3">
                                   <ItemTemplate>
                                      <%#(Eval("APPROVED_BY3").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY3N")%>
                                     
                                     </ItemTemplate>
                                </asp:TemplateField>
                             
                                 <asp:TemplateField HeaderText="Approved On">
                                   <ItemTemplate>
                                    
                                        <%#(Eval("APPROVED_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON3","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="REMARKS3" HeaderText="Comments" />


                          
                                <asp:TemplateField HeaderText="Approver-4">
                                   <ItemTemplate>
                                         <%#(Eval("APPROVED_BY4").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY4N")%>
                                        
                                   </ItemTemplate>
                                 </asp:TemplateField>
                               
                                 <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>
                                   
                                        <%#(Eval("APPROVED_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON4","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="REMARKS4" HeaderText="Comments" />


                               
                                <asp:TemplateField HeaderText="Approver-5">
                                    <ItemTemplate>
                                    
                                      <%#(Eval("APPROVED_BY5").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY5N")%> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                 <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>
                                    
                                        <%#(Eval("APPROVED_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON5","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="REMARKS6" HeaderText="Comments" />


                              
                                <asp:TemplateField HeaderText="Approver-6">
                                    <ItemTemplate>
                                       
                                       <%#(Eval("APPROVED_BY6").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY6N")%> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                 <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>
                                       
                                        <%#(Eval("APPROVED_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON6","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="REMARKS6" HeaderText="Comments" />


                            
                                <asp:TemplateField HeaderText="Approver-7">
                                   
                                    <ItemTemplate>
                 
                                      <%#(Eval("APPROVED_BY7").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY7N")%>  
                                    </ItemTemplate>
                                </asp:TemplateField>
                           
                                 <asp:TemplateField HeaderText="Approved On">
                                   <ItemTemplate>
                                      
                                        <%#(Eval("APPROVED_ON7","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON7","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="REMARKS7" HeaderText="Comments" />


                                   <asp:TemplateField HeaderText="Approver-8">
                                   <ItemTemplate>
                                         <%#(Eval("APPROVED_BY8").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY8")%>
                                    
                                   </ItemTemplate>
                                </asp:TemplateField>
                          
                                 <asp:TemplateField HeaderText="Approved On">
                                   <ItemTemplate>
                                       
                                        <%#(Eval("APPROVED_ON8","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON8","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="REMARKS8" HeaderText="Comments" />


                                 <asp:TemplateField HeaderText="Approver-9">
                                   <ItemTemplate>
                                         <%#(Eval("APPROVED_BY9").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY9")%>
                                   
                                   </ItemTemplate>
                                </asp:TemplateField>
                         
                                 <asp:TemplateField HeaderText="Approved On">
                                   <ItemTemplate>
                                      
                                        <%#(Eval("APPROVED_ON9","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON9","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="REMARKS9" HeaderText="Comments" />

                                <asp:BoundField DataField="STATUS" HeaderText="Status" />
                            </Columns>--%>


                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table class="TblCls">
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
                                                <%--   <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")?"Pending": ""%>--%>
                                                <td class="Tbltd"><%#(Eval("APPROVED_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON1","{0:dd-MM-yyyy}")%></td>

                                                <%-- <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved1")?Eval("APP_ON1","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD1")?Eval("HOLD_ON1","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED1")?Eval("RELEASED_ON1","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected1")?Eval("APP_ON1","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON1","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td class="Tbltd"><%# Eval("REMARKS1") %></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlAPPROVEDBY2" runat="server" Visible='<%# (Eval("APPROVED_BY2")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td class="Tbltd"><%# Eval("APPROVED_BY2") %></td>
                                                <td class="Tbltd"><%#(Eval("APPROVED_BY2").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY2N") %></td>
                                                <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected2")?"Rejected": (Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved2")?"Approved":"Approved"%> </td>
                                                <td class="Tbltd"><%#(Eval("APPROVED_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON2","{0:dd-MM-yyyy}")%></td>


                                                <%--  <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved2")?Eval("APP_ON2","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD2")?Eval("HOLD_ON2","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED2")?Eval("RELEASED_ON2","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected2")?Eval("APP_ON2","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON2","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td class="Tbltd"><%# Eval("REMARKS2") %></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel1" runat="server" Visible='<%# (Eval("APPROVED_BY3")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td class="Tbltd"><%# Eval("APPROVED_BY3") %></td>
                                                <td class="Tbltd"><%# (Eval("APPROVED_BY3").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY3N") %></td>
                                                <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved1")||(Eval("STATUS").ToString()=="Approved2")?"Pending": (Eval("STATUS").ToString()=="Rejected3")?"Rejected":(Eval("STATUS").ToString()=="Rejected2")|| (Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved3")?"Approved":"Approved"%> </td>
                                                <td class="Tbltd"><%#(Eval("APPROVED_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON3","{0:dd-MM-yyyy}")%></td>
                                                <%-- <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved3")?Eval("APP_ON3","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD3")?Eval("HOLD_ON3","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED3")?Eval("RELEASED_ON3","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected3")?Eval("APP_ON3","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON3","{0:dd-MM-yyyy}")%> </td>--%>


                                                <td class="Tbltd"><%# Eval("REMARKS3") %></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel2" runat="server" Visible='<%# (Eval("APPROVED_BY4")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td class="Tbltd"><%# Eval("APPROVED_BY4") %></td>
                                                <td class="Tbltd"><%# (Eval("APPROVED_BY4").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY4N") %></td>
                                                <td class="Tbltd"><%#(Eval("STATUS").ToString()=="Requested")|| (Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected4")?"Rejected": (Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved4")?"Approved":"Approved"%> </td>
                                                <td class="Tbltd"><%#(Eval("APPROVED_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON4","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved4")?Eval("APP_ON4","{0:dd-MM-yyyy}"): (Eval("STATUS").ToString()=="HOLD4")?Eval("HOLD_ON4","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED4")?Eval("RELEASED_ON4","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected4")?Eval("APP_ON4","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON4","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td class="Tbltd"><%# Eval("REMARKS4") %></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel3" runat="server" Visible='<%# (Eval("APPROVED_BY5")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td class="Tbltd"><%# Eval("APPROVED_BY5") %></td>
                                                <td class="Tbltd"><%# (Eval("APPROVED_BY5").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY5N") %></td>
                                                <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected5")?"Rejected": (Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2"||(Eval("STATUS").ToString()=="Rejected1"))?"": (Eval("STATUS").ToString()=="Approved5")?"Approved":"Approved"%> </td>
                                                <td class="Tbltd"><%#(Eval("APPROVED_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON5","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved5")?Eval("APP_ON5","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD5")?Eval("HOLD_ON5","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED5")?Eval("RELEASED_ON5","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected5")?Eval("APP_ON5","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON5","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td class="Tbltd"><%# Eval("REMARKS5") %></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel4" runat="server" Visible='<%# (Eval("APPROVED_BY6")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td class="Tbltd"><%# Eval("APPROVED_BY6") %></td>
                                                <td class="Tbltd"><%# (Eval("APPROVED_BY6").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY6N") %></td>
                                                <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%> </td>
                                                <td class="Tbltd"><%#(Eval("APPROVED_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON6","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td class="Tbltd"><%# Eval("REMARKS6") %></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel5" runat="server" Visible='<%# (Eval("APPROVED_BY7")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td class="Tbltd"><%# Eval("APPROVED_BY7") %></td>
                                                <td class="Tbltd"><%# (Eval("APPROVED_BY7").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY7N") %></td>
                                                <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%> </td>
                                                <td class="Tbltd"><%#(Eval("APPROVED_ON7","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON7","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td class="Tbltd"><%# Eval("REMARKS7") %></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel6" runat="server" Visible='<%# (Eval("APPROVED_BY8")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td class="Tbltd"><%# Eval("APPROVED_BY8") %></td>
                                                <td class="Tbltd"><%# (Eval("APPROVED_BY8").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY8") %></td>
                                                <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%> </td>
                                                <td class="Tbltd"><%#(Eval("APPROVED_ON8","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON8","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td class="Tbltd"><%# Eval("REMARKS8") %></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel7" runat="server" Visible='<%# (Eval("APPROVED_BY9")).ToString()==""?false:true %>'>
                                            <tr>
                                                <td class="Tbltd"><%# Eval("APPROVED_BY9") %></td>
                                                <td class="Tbltd"><%# (Eval("APPROVED_BY9").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY9") %></td>
                                                <td class="Tbltd"><%# (Eval("STATUS").ToString()=="Requested")||(Eval("STATUS").ToString()=="Approved5")||(Eval("STATUS").ToString()=="Approved4")||(Eval("STATUS").ToString()=="Approved3")||(Eval("STATUS").ToString()=="Approved2")||(Eval("STATUS").ToString()=="Approved1")?"Pending": (Eval("STATUS").ToString()=="Rejected6")?"Rejected": (Eval("STATUS").ToString()=="Rejected5")||(Eval("STATUS").ToString()=="Rejected4")||(Eval("STATUS").ToString()=="Rejected3")||(Eval("STATUS").ToString()=="Rejected2")||(Eval("STATUS").ToString()=="Rejected1")?"": (Eval("STATUS").ToString()=="Approved6")?"Approved":"Approved"%> </td>
                                                <td class="Tbltd"><%#(Eval("APPROVED_ON9","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON9","{0:dd-MM-yyyy}")%></td>
                                                <%--<td class="Tbltd"><%# (Eval("STATUS").ToString()=="Approved6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="HOLD6")?Eval("HOLD_ON6","{0:dd-MM-yyyy}").ToString(): (Eval("STATUS").ToString()=="RELEASED6")?Eval("RELEASED_ON6","{0:dd-MM-yyyy}").ToString():(Eval("STATUS").ToString()=="Rejected6")?Eval("APP_ON6","{0:dd-MM-yyyy}").ToString():(Eval("APP_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APP_ON6","{0:dd-MM-yyyy}")%> </td>--%>

                                                <td class="Tbltd"><%# Eval("REMARKS9") %></td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>



                    </asp:GridView>



                    <br />
                </asp:Panel>
                <asp:UpdatePanel ID="a" runat="server">
                    <ContentTemplate>
                        <div id="Exportbtn" runat="server">
                            <asp:Button ID="BtnExporttoXl" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" CausesValidation="false" TabIndex="7" CssClass="btn bg-dark waves-effect waves-light btn-std" />
                            &nbsp;&nbsp;
                            <asp:Button ID="BtnExporttoPDF" runat="server" Text="Export To PDF" OnClick="ExportToPDF_Click" TabIndex="8" CssClass="btn bg-dark waves-effect waves-light btn-std" />

                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BtnExporttoPDF" />
                        <asp:PostBackTrigger ControlID="BtnExporttoXl" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:HiddenField ID="viewcheck" runat="server" />
            </asp:Panel>
        </div>
    </div>
</asp:Content>
