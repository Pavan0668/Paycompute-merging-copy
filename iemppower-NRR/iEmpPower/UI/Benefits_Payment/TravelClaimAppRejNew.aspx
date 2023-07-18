<%@ Page Title="Travel Claim Approve / Reject" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="TravelClaimAppRejNew.aspx.cs"
    Inherits="iEmpPower.UI.Benefits_Payment.TravelClaimAppRejNew" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- start page title -->
            <div class="row">
                <div class="col-12">
                    <div class="page-title-box">
                        <div class="page-title-right">
                            <ol class="breadcrumb m-0">
                                <li class="breadcrumb-item"><a href="../Default.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="Travel_Requests.aspx">Travel Requests</a></li>
                                <li class="breadcrumb-item active">Travel Claim Requests For Approval</li>
                            </ol>
                        </div>
                        <h4 class="page-title">Travel Claim Requests For Approval&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <span>
                        <asp:Label ID="Label2" runat="server" CssClass="msgboard" meta:resourcekey="lblMessageBoardResource1" Font-Size="Medium"></asp:Label>
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
                                            <th colspan="16">Indent Item Details
                                            </th>
                                            <th width="10%" id="thHigltr" runat="server" style="background-color: #FFF9AE" visible="false">
                                                <asp:Label ID="lbltask" BackColor="Yellow" CssClass="float-right" runat="server"></asp:Label></th>
                                        </tr>
                                    </thead>
                                </table>


                                <asp:GridView ID="grdClaimDetails" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" DataKeyNames="CID,NO_DAYS,LID,EXPID,EXP_TYPE,CountryID,RegoinID,DAILY_RATE,DEVIATION_AMT,DEVIATION_CURR,EXPT_AMT,EXPT_CURR,ZLAND,ZORT1,DAILY_CURR"
                                    OnRowCommand="grdClaimDetails_RowCommand" OnRowDeleting="grdClaimDetails_RowDeleting"
                                    OnRowDataBound="grdClaimDetails_RowDataBound" OnRowEditing="grdClaimDetails_RowEditing" ShowFooter="True" FooterStyle-CssClass="foo01">
                                    <Columns>

                                        <asp:BoundField DataField="LID" HeaderText="Sl No" />
                                        <asp:BoundField DataField="CID" HeaderText="Claim Id" />
                                        <asp:BoundField DataField="EXP_TYPE" HeaderText="Expense Type" />

                                        <asp:BoundField DataField="S_DATE" HeaderText="Expense Date" DataFormatString="{0:dd-MMM-yyyy}" />
                                        <asp:BoundField DataField="NO_DAYS" HeaderText="No of Days" />

                                        <asp:TemplateField HeaderText="Daily Rate">
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
                                                <%# Convert.ToDouble(Eval("EXPT_AMT")).ToString("#,##0.00") %>    ( <%# Eval("EXPT_CURR") %>)
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="EXC_RATE" HeaderText="Exchange Rate" ControlStyle-CssClass="rightJustify" ItemStyle-CssClass="rightJustify" />
                                        <asp:TemplateField HeaderText="Reimbursable Amount" FooterStyle-CssClass="right" HeaderStyle-CssClass="right" ItemStyle-CssClass="right">
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
                                                <%#   (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Convert.ToDouble(Eval("DEVIATION_AMT")).ToString("#,##0.00")%>
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
                                                        <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>' />
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="Lbtndownload" />
                                                    </Triggers>

                                                </asp:UpdatePanel>
                                                <asp:CheckBox ID="cb" runat="server" Text="Original Receipt Missing" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>' /><br />
                                                <asp:FileUpload ID="fuAttachments" runat="server" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>' /><br />
                                                <asp:Label ID="fuAttachmentsfname" runat="server" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:Label>
                                                <asp:LinkButton ID="LbtnUpload" runat="server" Text="Upload" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                    CommandName="Upload" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:LinkButton>
                                                <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                    CommandName="Delete" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

                                            </ItemTemplate>
                                            <ItemStyle Width="100" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>


                                            <ItemTemplate>

                                                <asp:LinkButton ID="LbtnEDIT" runat="server" Text="Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                                    CommandName="EDITEXPTYPE" CausesValidation="false" Visible="false" CssClass="btn btn-xs btn-warning"></asp:LinkButton>

                                            </ItemTemplate>
                                            <ItemStyle Width="100" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="foo01" ForeColor="Black"></FooterStyle>
                                </asp:GridView>

                                <br />

                                <div id="ExpEDIT" runat="server" visible="false">

                                    <table id="table2" class="TblCls" runat="server">

                                        <tr>
                                            <td>Expense Type&nbsp;
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLExpenseType" runat="server" CssClass="textbox" Font-Size="12px" TabIndex="5" AutoPostBack="true" OnSelectedIndexChanged="DDLExpenseType_SelectedIndexChanged">
                                                </asp:DropDownList>

                                                <Ajx:ListSearchExtender ID="lseexptype" runat="server" TargetControlID="DDLExpenseType"></Ajx:ListSearchExtender>

                                                <asp:RequiredFieldValidator ID="RFV_ddlExpenseType" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlExpenseType" InitialValue="0"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Expenditure Amount&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblExpenditureAmount" runat="server"></asp:Label>

                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Expenditure Currency&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="LblExptCurr" runat="server"></asp:Label>


                                            </td>
                                        </tr>


                                        <tr>
                                            <td>Country</td>
                                            <td>

                                                <asp:Label ID="LblCountry" runat="server"></asp:Label>


                                            </td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>Region</td>
                                            <td>
                                                <asp:Label ID="LblRegion" runat="server"></asp:Label>



                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Daily Rate</td>
                                            <td>
                                                <asp:Label ID="LblDailyRate" runat="server"></asp:Label>&nbsp;&nbsp;
                
                    <asp:Label ID="LblCurrency" runat="server"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>

                                        </tr>
                                        <tr>
                                            <td>Deviation</td>
                                            <td>
                                                <asp:Label ID="LblDeviation" runat="server"></asp:Label>&nbsp;&nbsp;
                     <asp:Label ID="LblCurrency1" runat="server"></asp:Label>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td><span id="Sp01" style="color: white;"></span></td>
                                        </tr>

                                        <tr>
                                            <td></td>
                                            <td>
                                                <asp:LinkButton ID="LbtnUpdateExpTyp" runat="server" Text="Update" Visible="true" OnClick="LbtnUpdateExpTyp_Click" TabIndex="6"></asp:LinkButton>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:HiddenField ID="HF_DailyRate" runat="server" />
                                                <asp:HiddenField ID="HF_Deviation" runat="server" />
                                                <asp:HiddenField ID="HF_DeCurr" runat="server" />
                                                <asp:HiddenField ID="HF_LblRegion" runat="server" />
                                                <asp:HiddenField ID="HF_LblCountry" runat="server" />
                                                <asp:HiddenField ID="HF_LblExptCurr" runat="server" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    </table>
                                </div>
                                <br />

                                <asp:GridView ID="grdAppRejHistory" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" OnRowEditing="grdAppRejHistory_RowEditing"
                                    DataKeyNames="APPROVED_BY1,APPROVED_BY2,APPROVED_BY3,APPROVED_BY4,APPROVED_BY5,APPROVED_BY6,APPROVED_BY7,APPROVED_BY8,APPROVED_BY9"
                                    OnRowUpdating="grdAppRejHistory_RowUpdating" OnRowCommand="grdAppRejHistory_RowCommand" OnRowCancelingEdit="grdAppRejHistory_RowCancelingEdit"
                                    OnRowDataBound="grdAppRejHistory_RowDataBound">
                                    <Columns>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:LinkButton ID="GVLbtnEdit" runat="server" Text="Redirect" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Edit" CausesValidation="false"></asp:LinkButton>



                                            </ItemTemplate>
                                            <EditItemTemplate>

                                                <asp:LinkButton ID="GVLbtnUpdate" runat="server" Text="Update" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Update" CausesValidation="false"> </asp:LinkButton>
                                                <br />
                                                <br />
                                                <asp:LinkButton ID="GVLbtnCancel" runat="server" Text="Cancel" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Cancel" CausesValidation="false"></asp:LinkButton>

                                            </EditItemTemplate>
                                            <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approver-1">
                                            <ItemTemplate>

                                                <%#Eval("APPROVED_BY1")==null?"":(Eval("APPROVED_BY1").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY1N")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>

                                                <asp:DropDownList ID="DDLApprover1" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                                </asp:DropDownList>

                                                <Ajx:CascadingDropDown ID="CCD_DDLApprover1" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY1") %>'
                                                    TargetControlID="DDLApprover1" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                                </Ajx:CascadingDropDown>
                                                <asp:Label ID="LblApprover1" runat="server" Text=' <%#Eval("APPROVED_BY1")==null?"":(Eval("APPROVED_BY1").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY1N")%>' Enabled="false" Visible="false"></asp:Label>


                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved On">
                                            <ItemTemplate>

                                                <%#(Eval("APPROVED_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON1","{0:dd-MM-yyyy}")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#(Eval("APPROVED_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON1","{0:dd-MM-yyyy}")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>

                                                <%# Eval("REMARKS1")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("REMARKS1")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approver-2">
                                            <ItemTemplate>

                                                <%#Eval("APPROVED_BY2")==null?"":(Eval("APPROVED_BY2").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY2N")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DDLApprover2" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                                </asp:DropDownList>

                                                <Ajx:CascadingDropDown ID="CCD_DDLApprover2" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY2") %>'
                                                    TargetControlID="DDLApprover2" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                                </Ajx:CascadingDropDown>
                                                <asp:Label ID="LblApprover2" runat="server" Text=' <%#Eval("APPROVED_BY2")==null?"":(Eval("APPROVED_BY2").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY2N")%>' Enabled="false" Visible="false"></asp:Label>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved On">
                                            <ItemTemplate>

                                                <%#(Eval("APPROVED_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON2","{0:dd-MM-yyyy}")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#(Eval("APPROVED_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON2","{0:dd-MM-yyyy}")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <%# Eval("REMARKS2")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("REMARKS2")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Approver-3">
                                            <ItemTemplate>
                                                <%#Eval("APPROVED_BY3")==null?"":(Eval("APPROVED_BY3").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY3N")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DDLApprover3" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                                </asp:DropDownList>

                                                <Ajx:CascadingDropDown ID="CCD_DDLApprover3" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY3") %>'
                                                    TargetControlID="DDLApprover3" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                                </Ajx:CascadingDropDown>
                                                <asp:Label ID="LblApprover3" runat="server" Text=' <%#Eval("APPROVED_BY3")==null?"":(Eval("APPROVED_BY3").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY3N")%>' Enabled="false" Visible="false"></asp:Label>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved On">
                                            <ItemTemplate>

                                                <%#(Eval("APPROVED_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON3","{0:dd-MM-yyyy}")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#(Eval("APPROVED_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON3","{0:dd-MM-yyyy}")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <%# Eval("REMARKS3")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("REMARKS3")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approver-4">
                                            <ItemTemplate>
                                                <%#Eval("APPROVED_BY4")==null?"":(Eval("APPROVED_BY4").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY4N")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DDLApprover4" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                                </asp:DropDownList>

                                                <Ajx:CascadingDropDown ID="CCD_DDLApprover4" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY4") %>'
                                                    TargetControlID="DDLApprover4" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                                </Ajx:CascadingDropDown>
                                                <asp:Label ID="LblApprover4" runat="server" Text=' <%#Eval("APPROVED_BY4")==null?"":(Eval("APPROVED_BY4").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY4N")%>' Enabled="false" Visible="false"></asp:Label>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved On">
                                            <ItemTemplate>

                                                <%#(Eval("APPROVED_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON4","{0:dd-MM-yyyy}")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#(Eval("APPROVED_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON4","{0:dd-MM-yyyy}")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <%# Eval("REMARKS4")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("REMARKS4")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approver-5">
                                            <ItemTemplate>

                                                <%#Eval("APPROVED_BY5")==null?"":(Eval("APPROVED_BY5").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY5N")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DDLApprover5" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                                </asp:DropDownList>

                                                <Ajx:CascadingDropDown ID="CCD_DDLApprover5" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY5") %>'
                                                    TargetControlID="DDLApprover5" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                                </Ajx:CascadingDropDown>
                                                <asp:Label ID="LblApprover5" runat="server" Text=' <%#Eval("APPROVED_BY5")==null?"":(Eval("APPROVED_BY5").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY5N")%>' Enabled="false" Visible="false"></asp:Label>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved On">
                                            <ItemTemplate>

                                                <%#(Eval("APPROVED_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON5","{0:dd-MM-yyyy}")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#(Eval("APPROVED_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON5","{0:dd-MM-yyyy}")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <%# Eval("REMARKS5")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("REMARKS5")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Approver-6">
                                            <ItemTemplate>
                                                <%#Eval("APPROVED_BY6")==null?"":(Eval("APPROVED_BY6").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY6N")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DDLApprover6" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                                </asp:DropDownList>

                                                <Ajx:CascadingDropDown ID="CCD_DDLApprover6" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY6") %>'
                                                    TargetControlID="DDLApprover6" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                                </Ajx:CascadingDropDown>
                                                <asp:Label ID="LblApprover6" runat="server" Text=' <%#Eval("APPROVED_BY6")==null?"":(Eval("APPROVED_BY6").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY6N")%>' Enabled="false" Visible="false"></asp:Label>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved On">
                                            <ItemTemplate>

                                                <%#(Eval("APPROVED_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON6","{0:dd-MM-yyyy}")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#(Eval("APPROVED_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON6","{0:dd-MM-yyyy}")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <%# Eval("REMARKS6")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("REMARKS6")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approver-7">

                                            <ItemTemplate>

                                                <%#Eval("APPROVED_BY7")==null?"":(Eval("APPROVED_BY7").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY7N")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DDLApprover7" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                                </asp:DropDownList>

                                                <Ajx:CascadingDropDown ID="CCD_DDLApprover7" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY7") %>'
                                                    TargetControlID="DDLApprover7" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                                </Ajx:CascadingDropDown>
                                                <asp:Label ID="LblApprover7" runat="server" Text=' <%#Eval("APPROVED_BY7")==null?"":(Eval("APPROVED_BY7").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY7N")%>' Enabled="false" Visible="false"></asp:Label>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved On">
                                            <ItemTemplate>

                                                <%#(Eval("APPROVED_ON7","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON7","{0:dd-MM-yyyy}")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#(Eval("APPROVED_ON7","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON7","{0:dd-MM-yyyy}")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <%# Eval("REMARKS7")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("REMARKS7")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Approver-8">
                                            <ItemTemplate>
                                                <%#Eval("APPROVED_BY8")==null?"":(Eval("APPROVED_BY8").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY8")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DDLApprover8" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                                </asp:DropDownList>

                                                <Ajx:CascadingDropDown ID="CCD_DDLApprover8" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY8") %>'
                                                    TargetControlID="DDLApprover8" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                                </Ajx:CascadingDropDown>
                                                <asp:Label ID="LblApprover8" runat="server" Text=' <%#Eval("APPROVED_BY8")==null?"":(Eval("APPROVED_BY8").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY8")%>' Enabled="false" Visible="false"></asp:Label>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved On">
                                            <ItemTemplate>

                                                <%#(Eval("APPROVED_ON8","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON8","{0:dd-MM-yyyy}")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#(Eval("APPROVED_ON8","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON8","{0:dd-MM-yyyy}")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <%# Eval("REMARKS8")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("REMARKS8")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Approver-9">
                                            <ItemTemplate>
                                                <%#Eval("APPROVED_BY9")==null?"":(Eval("APPROVED_BY9").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY9")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DDLApprover9" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                                </asp:DropDownList>

                                                <Ajx:CascadingDropDown ID="CCD_DDLApprover9" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY9") %>'
                                                    TargetControlID="DDLApprover9" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                                </Ajx:CascadingDropDown>
                                                <asp:Label ID="LblApprover9" runat="server" Text='<%#Eval("APPROVED_BY9")==null?"":(Eval("APPROVED_BY9").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY9")%>' Enabled="false" Visible="false"></asp:Label>

                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved On">
                                            <ItemTemplate>

                                                <%#(Eval("APPROVED_ON9","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON9","{0:dd-MM-yyyy}")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%#(Eval("APPROVED_ON9","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON9","{0:dd-MM-yyyy}")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <%# Eval("REMARKS9")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("REMARKS9")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Comments">
                                            <ItemTemplate>
                                                <%# Eval("STATUS")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <%# Eval("STATUS")%>
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <br />
                                <asp:Label ID="lblRemarks" runat="server" Text="*Remarks" CssClass="label"></asp:Label>
                                <asp:TextBox ID="TxtRemarks" runat="server" CssClass="textbox" TextMode="MultiLine" TabIndex="7" Text="APPROVED"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFV_TxtRemarks" runat="server" ControlToValidate="TxtRemarks" ErrorMessage="Mandatory" ForeColor="Red" ValidationGroup="vg1"></asp:RequiredFieldValidator>

                                <br />
                                <br />
                                <div id="AppRejButton" class="buttonrow">
                                    &nbsp;<asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" TabIndex="8" CssClass="btn bg-brand-btn waves-effect waves-light btn-std" Visible="false" />&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" TabIndex="9" ValidationGroup="vg1" CssClass="btn bg-danger waves-effect waves-light btn-std" Visible="false" />
                                </div>
                                <asp:HiddenField ID="HFCID" runat="server" />
                            </div>
                        </div>
                    </div>


                    <div class="table-responsive card-box" runat="server" visible="false">
                        <h4>Travel Claim Requests For Approval</h4>

                        <br />
                        <table>
                            <tr>
                                <td align="right">Select:</td>
                                </td>
                    <td>
                        <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="form-control-file" TabIndex="1">
                            <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Claim ID" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Trip No" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Employee Name" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                                <td>
                                    <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" TabIndex="2"></asp:TextBox>
                                </td>
                                <td>&nbsp;&nbsp;
                        <asp:Button ID="btnsearch" CssClass="btn btn-xs btn-secondary" runat="server" Text="Search" OnClick="btnsearch_Click" CausesValidation="false" TabIndex="3" />
                                    &nbsp;&nbsp;
                    <asp:Button ID="btnclear" CssClass="btn btn-xs btn-secondary" runat="server" Text="Clear" OnClick="btnclear_Click" TabIndex="4" /></td>
                            </tr>

                            <tr>
                                <td></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <asp:GridView ID="grdAppRejTravel" runat="server" AutoGenerateColumns="False" CssClass="gridviewNew" GridLines="None" Width="99%" OnRowCommand="grdAppRejTravel_RowCommand"
                            DataKeyNames="CID,CREATED_BY,REINR,WBS_ELEMT,ACTIVITY,RE_AMT,RCURR" AllowPaging="true" AllowSorting="true" OnSorting="grdAppRejTravel_Sorting" OnPageIndexChanging="grdAppRejTravel_PageIndexChanging" PageSize="5">
                            <Columns>
                                <asp:BoundField DataField="CID" HeaderText="Claim Id" SortExpression="CID" />

                                <asp:BoundField DataField="REINR" HeaderText="Trip No" SortExpression="REINR" />
                                <asp:BoundField DataField="DATV1" HeaderText="Trip From" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="7%" SortExpression="DATV1" />
                                <asp:BoundField DataField="DATB1" HeaderText="Trip To" DataFormatString="{0:dd-MMM-yyyy}" ItemStyle-Width="7%" SortExpression="DATB1" />

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
                    <asp:Panel ID="PnlIExpDetalsView" runat="server" Visible="false" DefaultButton="btnApprove">
                        <%-- <asp:GridView ID="grdAppRejHistory" runat="server" AutoGenerateColumns="False" CssClass="table-responsive card-box" OnRowEditing="grdAppRejHistory_RowEditing"
                            DataKeyNames="APPROVED_BY1,APPROVED_BY2,APPROVED_BY3,APPROVED_BY4,APPROVED_BY5,APPROVED_BY6,APPROVED_BY7,APPROVED_BY8,APPROVED_BY9"
                            OnRowUpdating="grdAppRejHistory_RowUpdating" OnRowCommand="grdAppRejHistory_RowCommand" OnRowCancelingEdit="grdAppRejHistory_RowCancelingEdit"
                            OnRowDataBound="grdAppRejHistory_RowDataBound">
                            <Columns>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="GVLbtnEdit" runat="server" Text="Redirect" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Edit" CausesValidation="false"></asp:LinkButton>



                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:LinkButton ID="GVLbtnUpdate" runat="server" Text="Update" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Update" CausesValidation="false"> </asp:LinkButton>
                                        <br />
                                        <br />
                                        <asp:LinkButton ID="GVLbtnCancel" runat="server" Text="Cancel" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Cancel" CausesValidation="false"></asp:LinkButton>

                                    </EditItemTemplate>
                                    <ItemStyle Width="5%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approver-1">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_BY1").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY1N")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:DropDownList ID="DDLApprover1" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                        </asp:DropDownList>

                                        <Ajx:CascadingDropDown ID="CCD_DDLApprover1" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY1") %>'
                                            TargetControlID="DDLApprover1" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                        </Ajx:CascadingDropDown>
                                        <asp:Label ID="LblApprover1" runat="server" Text=' <%#(Eval("APPROVED_BY1").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY1N")%>' Enabled="false" Visible="false"></asp:Label>


                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON1","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%#(Eval("APPROVED_ON1","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON1","{0:dd-MM-yyyy}")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>

                                        <%# Eval("REMARKS1")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("REMARKS1")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approver-2">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_BY2").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY2N")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DDLApprover2" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                        </asp:DropDownList>

                                        <Ajx:CascadingDropDown ID="CCD_DDLApprover2" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY2") %>'
                                            TargetControlID="DDLApprover2" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                        </Ajx:CascadingDropDown>
                                        <asp:Label ID="LblApprover2" runat="server" Text=' <%#(Eval("APPROVED_BY2").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY2N")%>' Enabled="false" Visible="false"></asp:Label>

                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON2","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%#(Eval("APPROVED_ON2","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON2","{0:dd-MM-yyyy}")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS2")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("REMARKS2")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Approver-3">
                                    <ItemTemplate>
                                        <%#(Eval("APPROVED_BY3").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY3N")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DDLApprover3" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                        </asp:DropDownList>

                                        <Ajx:CascadingDropDown ID="CCD_DDLApprover3" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY3") %>'
                                            TargetControlID="DDLApprover3" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                        </Ajx:CascadingDropDown>
                                        <asp:Label ID="LblApprover3" runat="server" Text=' <%#(Eval("APPROVED_BY3").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY3N")%>' Enabled="false" Visible="false"></asp:Label>

                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON3","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%#(Eval("APPROVED_ON3","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON3","{0:dd-MM-yyyy}")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS3")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("REMARKS3")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approver-4">
                                    <ItemTemplate>
                                        <%#(Eval("APPROVED_BY4").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY4N")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DDLApprover4" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                        </asp:DropDownList>

                                        <Ajx:CascadingDropDown ID="CCD_DDLApprover4" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY4") %>'
                                            TargetControlID="DDLApprover4" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                        </Ajx:CascadingDropDown>
                                        <asp:Label ID="LblApprover4" runat="server" Text=' <%#(Eval("APPROVED_BY4").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY4N")%>' Enabled="false" Visible="false"></asp:Label>

                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON4","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%#(Eval("APPROVED_ON4","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON4","{0:dd-MM-yyyy}")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS4")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("REMARKS4")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approver-5">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_BY5").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY5N")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DDLApprover5" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                        </asp:DropDownList>

                                        <Ajx:CascadingDropDown ID="CCD_DDLApprover5" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY5") %>'
                                            TargetControlID="DDLApprover5" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                        </Ajx:CascadingDropDown>
                                        <asp:Label ID="LblApprover5" runat="server" Text=' <%#(Eval("APPROVED_BY5").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY5N")%>' Enabled="false" Visible="false"></asp:Label>

                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON5","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%#(Eval("APPROVED_ON5","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON5","{0:dd-MM-yyyy}")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS5")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("REMARKS5")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Approver-6">
                                    <ItemTemplate>
                                        <%#(Eval("APPROVED_BY6").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY6N")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DDLApprover6" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                        </asp:DropDownList>

                                        <Ajx:CascadingDropDown ID="CCD_DDLApprover6" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY6") %>'
                                            TargetControlID="DDLApprover6" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                        </Ajx:CascadingDropDown>
                                        <asp:Label ID="LblApprover6" runat="server" Text=' <%#(Eval("APPROVED_BY6").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY6N")%>' Enabled="false" Visible="false"></asp:Label>

                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON6","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%#(Eval("APPROVED_ON6","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON6","{0:dd-MM-yyyy}")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS6")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("REMARKS6")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approver-7">

                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_BY7").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY7N")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DDLApprover7" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                        </asp:DropDownList>

                                        <Ajx:CascadingDropDown ID="CCD_DDLApprover7" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY7") %>'
                                            TargetControlID="DDLApprover7" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                        </Ajx:CascadingDropDown>
                                        <asp:Label ID="LblApprover7" runat="server" Text=' <%#(Eval("APPROVED_BY7").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY7N")%>' Enabled="false" Visible="false"></asp:Label>

                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_ON7","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON7","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%#(Eval("APPROVED_ON7","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON7","{0:dd-MM-yyyy}")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS7")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("REMARKS7")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Approver-8">
                                    <ItemTemplate>
                                        <%#(Eval("APPROVED_BY8").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY8")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DDLApprover8" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                        </asp:DropDownList>

                                        <Ajx:CascadingDropDown ID="CCD_DDLApprover8" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY8") %>'
                                            TargetControlID="DDLApprover8" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                        </Ajx:CascadingDropDown>
                                        <asp:Label ID="LblApprover8" runat="server" Text=' <%#(Eval("APPROVED_BY8").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY8")%>' Enabled="false" Visible="false"></asp:Label>

                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_ON8","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON8","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%#(Eval("APPROVED_ON8","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON8","{0:dd-MM-yyyy}")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS8")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("REMARKS8")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Approver-9">
                                    <ItemTemplate>
                                        <%#(Eval("APPROVED_BY9").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY9")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DDLApprover9" runat="server" CssClass="textbox" TabIndex="11" Font-Size="12px" Enabled="false" Visible="false">
                                        </asp:DropDownList>

                                        <Ajx:CascadingDropDown ID="CCD_DDLApprover9" runat="server" EmptyText="SELECT " EmptyValue="0" LoadingText="[Loading..]" Category="Approvers" SelectedValue='<%# Bind("APPROVED_BY9") %>'
                                            TargetControlID="DDLApprover9" PromptText="SELECT " PromptValue="0" ServiceMethod="GetEmployeeNamesAndId" ServicePath="~/WebService/Service.asmx" UseContextKey="true" ContextKey='<%#Bind("BUKRS") %>'>
                                        </Ajx:CascadingDropDown>
                                        <asp:Label ID="LblApprover9" runat="server" Text='<%#(Eval("APPROVED_BY9").ToString().StartsWith("fiad")) ? "Finance" : Eval("APPROVED_BY9")%>' Enabled="false" Visible="false"></asp:Label>

                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Approved On">
                                    <ItemTemplate>

                                        <%#(Eval("APPROVED_ON9","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON9","{0:dd-MM-yyyy}")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%#(Eval("APPROVED_ON9","{0:dd-MM-yyyy}").ToString()=="01-01-0001") ? "" : Eval("APPROVED_ON9","{0:dd-MM-yyyy}")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <%# Eval("REMARKS9")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("REMARKS9")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Comments">
                                    <ItemTemplate>
                                        <%# Eval("STATUS")%>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <%# Eval("STATUS")%>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>--%>

                        <%--                <br />
                <br />

                <asp:GridView ID="grdClaimDetails" runat="server" AutoGenerateColumns="False" CssClass="gridview" Width="99%" DataKeyNames="CID,NO_DAYS,LID,EXPID,EXP_TYPE,CountryID,RegoinID,DAILY_RATE,DEVIATION_AMT,DEVIATION_CURR,EXPT_AMT,EXPT_CURR,ZLAND,ZORT1,DAILY_CURR"
                    OnRowCommand="grdClaimDetails_RowCommand" OnRowDeleting="grdClaimDetails_RowDeleting"
                    OnRowDataBound="grdClaimDetails_RowDataBound" OnRowEditing="grdClaimDetails_RowEditing" ShowFooter="True" FooterStyle-CssClass="foo01">
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
                                <%#   (Eval("DEVIATION_AMT").ToString()=="0.000") ? "" : Convert.ToDouble(Eval("DEVIATION_AMT")).ToString("#,##0.00")%>
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

                                <asp:LinkButton ID="Lbtndownload" runat="server" Text='<%#Eval("RECEIPT_FID") %>' Font-Bold="True" CommandName="download" CommandArgument='<%# Eval("RECEIPT_FPATH") %>' CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>' />

                                <asp:CheckBox ID="cb" runat="server" Text="Original Receipt Missing" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>' /><br />
                                <asp:FileUpload ID="fuAttachments" runat="server" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>' /><br />
                                <asp:Label ID="fuAttachmentsfname" runat="server" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:Label>
                                <asp:LinkButton ID="LbtnUpload" runat="server" Text="Upload" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                    CommandName="Upload" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "true" : "false"))%>'></asp:LinkButton>
                                <asp:LinkButton ID="LbtnDelete" runat="server" Text="Delete" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                    CommandName="Delete" CausesValidation="false" Visible='<%# bool.Parse(string.Format("{0}", Eval("RECEIPT_FID").ToString()=="" ? "false" : "true"))%>'></asp:LinkButton>

                            </ItemTemplate>
                            <ItemStyle Width="100" />
                        </asp:TemplateField>
                        <asp:TemplateField>


                            <ItemTemplate>

                                <asp:LinkButton ID="LbtnEDIT" runat="server" Text="Edit Expense Type" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>'
                                    CommandName="EDITEXPTYPE" CausesValidation="false" Visible="false"></asp:LinkButton>

                            </ItemTemplate>
                            <ItemStyle Width="100" />
                        </asp:TemplateField>

                    </Columns>
                    <FooterStyle CssClass="foo01" ForeColor="Black"></FooterStyle>
                </asp:GridView>

                <br />

                <div id="ExpEDIT" runat="server" visible="false">

                    <table id="table2" class="TblCls" runat="server">

                        <tr>
                            <td>Expense Type&nbsp;
                            </td>
                            <td>
                                <asp:DropDownList ID="DDLExpenseType" runat="server" CssClass="textbox" Font-Size="12px" TabIndex="5" AutoPostBack="true" OnSelectedIndexChanged="DDLExpenseType_SelectedIndexChanged">
                                </asp:DropDownList>

                                <Ajx:ListSearchExtender ID="lseexptype" runat="server" TargetControlID="DDLExpenseType"></Ajx:ListSearchExtender>

                                <asp:RequiredFieldValidator ID="RFV_ddlExpenseType" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="ddlExpenseType" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Expenditure Amount&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="LblExpenditureAmount" runat="server"></asp:Label>

                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Expenditure Currency&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="LblExptCurr" runat="server"></asp:Label>


                            </td>
                        </tr>


                        <tr>
                            <td>Country</td>
                            <td>

                                <asp:Label ID="LblCountry" runat="server"></asp:Label>

                   
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>Region</td>
                            <td>
                                <asp:Label ID="LblRegion" runat="server"></asp:Label>

                    

                            </td>
                        </tr>
                        <tr>
                            <td>Daily Rate</td>
                            <td>
                                <asp:Label ID="LblDailyRate" runat="server"></asp:Label>&nbsp;&nbsp;
                
                    <asp:Label ID="LblCurrency" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>

                        </tr>
                        <tr>
                            <td>Deviation</td>
                            <td>
                                <asp:Label ID="LblDeviation" runat="server"></asp:Label>&nbsp;&nbsp;
                     <asp:Label ID="LblCurrency1" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td><span id="Sp01" style="color: white;"></span></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:LinkButton ID="LbtnUpdateExpTyp" runat="server" Text="Update" Visible="true" OnClick="LbtnUpdateExpTyp_Click" TabIndex="6"></asp:LinkButton>

                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:HiddenField ID="HF_DailyRate" runat="server" />
                                <asp:HiddenField ID="HF_Deviation" runat="server" />
                                <asp:HiddenField ID="HF_DeCurr" runat="server" />
                                <asp:HiddenField ID="HF_LblRegion" runat="server" />
                                <asp:HiddenField ID="HF_LblCountry" runat="server" />
                                <asp:HiddenField ID="HF_LblExptCurr" runat="server" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>

                <asp:Label ID="lblRemarks" runat="server" Text="*Remarks" CssClass="label"></asp:Label>
                <asp:TextBox ID="TxtRemarks" runat="server" CssClass="textbox" TextMode="MultiLine" TabIndex="7" Text="APPROVED"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFV_TxtRemarks" runat="server" ControlToValidate="TxtRemarks" ErrorMessage="Mandatory" ForeColor="Red" ValidationGroup="vg1"></asp:RequiredFieldValidator>

                <br />

                <div id="AppRejButton" class="buttonrow">
                    &nbsp;<asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" TabIndex="8" />&nbsp;&nbsp;&nbsp;
          <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click" TabIndex="9" ValidationGroup="vg1" />
                </div>--%>
                    </asp:Panel>
        </ContentTemplate>

    </asp:UpdatePanel>

    <Ajx:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lblID" DropShadow="True" PopupControlID="pnlConfirmation" CancelControlID="btncancel">
    </Ajx:ModalPopupExtender>

    <asp:Panel ID="pnlConfirmation" runat="server" Style="background-color: #D4D0C8; border: 2px solid black; width: 300px; height: 100px;">
        <asp:Label ID="lblID" runat="server"></asp:Label>
        <table cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td>This claim is having a deviation amount! Do you still want to approve?
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnOK1" runat="server" Text="OK" OnClick="btnOK1_Click" />
                    <asp:Button ID="btncancel" runat="server" Text="Cancel" />
                    <%--OnClick="btncancel_Click" />--%>
                </td>
            </tr>
        </table>
    </asp:Panel>




    <script src="../../Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //  var allCheckBoxSelector = '#<%=grdAppRejTravel.ClientID%> input[id*="chkAll"]:checkbox';
        //  var checkBoxSelector = '#<%=grdAppRejTravel.ClientID%> input[id*="chkSelected"]:checkbox';

        // function ToggleCheckUncheckAllOptionAsNeeded() {
        //  var totalCheckboxes = $(checkBoxSelector),
        //  checkedCheckboxes = totalCheckboxes.filter(":checked"),
        //   noCheckboxesAreChecked = (checkedCheckboxes.length === 0),
        //  allCheckboxesAreChecked = (totalCheckboxes.length === checkedCheckboxes.length);

        // $(allCheckBoxSelector).attr('checked', allCheckboxesAreChecked);
        //}

        //  $(document).ready(function () {
        // $(allCheckBoxSelector).live('click', function () {
        //    $(checkBoxSelector).attr('checked', $(this).is(':checked'));

        //   ToggleCheckUncheckAllOptionAsNeeded();
        //  });

        // $(checkBoxSelector).live('click', ToggleCheckUncheckAllOptionAsNeeded);

        // ToggleCheckUncheckAllOptionAsNeeded();
        //  });
    </script>
</asp:Content>
