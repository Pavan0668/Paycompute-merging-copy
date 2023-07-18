<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RRF_App_rejt.aspx.cs" Inherits="iEmpPower.UI.RRF.RRF_App_rejt"
    Culture="en-GB" UICulture="auto" Theme="SkinFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .txtcolor
        {
            color: #00617C !important;
        }

        .pager table td
        {
            color: #00617C !important;
            border: solid 1px red;
            padding: 0 2px;
        }

            .pager table td a
            {
                color: #00617C !important;
            }

                .pager table td:hover, .pager table td a:hover
                {
                    color: black !important;
                }

        .form-group div
        {
            color: black;
        }

        .Cntrlwidth
        {
            color: #00617C !important;
            max-width: 260px;
        }

        .Apprvrclass
        {
            /*border-style: Inset;*/
            border-color: #00617C;
            padding: 25px;
            color: #00617C !important;
        }

            .Apprvrclass textarea
            {
                /*background-color: transparent;
                color: white;*/
                margin-bottom: 5px;
            }

        .pager table td
        {
            border: solid 1px;
            padding: 0 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="header">
        <div class="row clearfix">
            <div class="col-xs-12 col-sm-6">
                <span class="HeadFontSize">&nbsp;R.R.F. Approve / Reject</span><br />
            </div>
        </div>

    </div>
    <div class="body">
        <div class="divfr" id="divbrdr">

            <asp:UpdatePanel runat="server" ID="upapp_rej">
                <ContentTemplate>
                    <div class="form-inline">
                        <div class="form-group">
                            <div class="col-sm-8">
                                <div class="col-sm-2 htCr Cntrlwidth">Search :</div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="DDL_search_RRF" CssClass="txtDropDownwidth" runat="server">
                                        <asp:ListItem Selected="True" Value="0">- Select Search Type -</asp:ListItem>
                                        <asp:ListItem Value="1">RRF ID</asp:ListItem>
                                        <asp:ListItem Value="2">Indentor Name</asp:ListItem>
                                        <asp:ListItem Value="3">Requestor Name</asp:ListItem>
                                        <asp:ListItem Value="4">Reqd. Designation</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                    <asp:TextBox ID="txtsearch_rrf" AutoPostBack="true" OnTextChanged="txtsearch_rrf_TextChanged" CssClass="txtDropDownwidth" runat="server"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btnSearchClear" runat="server" Text="Clear" OnClick="btnSearchClear_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Label ID="lblMessageBoard" runat="server" CssClass="lblMsg"></asp:Label>
                    <div class="respovrflw">
                        <asp:GridView ID="GRD_RRF" runat="server" CssClass="Grid"
                            AllowPaging="True" AutoGenerateColumns="False"
                            AllowSorting="True" Width="99%" PageSize="5"
                            OnPageIndexChanging="GRD_RRF_PageIndexChanging"
                            OnSorting="GRD_RRF_Sorting" DataKeyNames="ID,STATUS" OnRowCommand="GRD_RRF_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID"
                                    SortExpression="ID" />
                                <asp:BoundField DataField="IND_ENAME" HeaderText="Indentor"
                                    SortExpression="IND_ENAME" />
                                <asp:BoundField DataField="REQT_ENAME" HeaderText="Requestor"
                                    SortExpression="REQT_ENAME" />
                                <asp:BoundField DataField="DESRTEXT" HeaderText="Designation"
                                    SortExpression="DESRTEXT" />
                                <asp:BoundField DataField="POS_REPT_TO_ID_ENAME" HeaderText="Reporting To"
                                    SortExpression="POS_REPT_TO_ID_ENAME" />
                                <asp:BoundField DataField="STATUS" HeaderText="Status"
                                    SortExpression="STATUS" />

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRRFView" runat="server" CausesValidation="false" CommandName="VIEW"
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="View"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />



                    <asp:FormView ID="FV_RRF_MyReq" PagerStyle-CssClass="pager" runat="server" AllowPaging="true" ForeColor="WhiteSmoke" OnPageIndexChanging="FV_RRF_MyReq_PageIndexChanging" OnItemCommand="FV_RRF_MyReq_ItemCommand"
                        DataKeyNames="ID, INDNTR_NAME,REQTR_NAME,DES_RECUTD,REP_EXT_EMP,REP_EXT_EMP_ID,REQ_POS_BUDGT,REQ_POS_BUDGT_FRM_MONTH,
                REQ_POS_BUDGT_COST,PURPS_HIRNG,PURPS_HIRNG_LOC,PURPS_HIRNG_PROJ,POS_REPT_TO_ID,
      MIN_EDU_QLAFTN,MIN_CERTIFNTN,TOT_EXP,TOT_DOMAIN_EXP,AREA_EXPRTSE,OTHER_SPC_REQ,JOB_DISP,DISP_FILE,
                TENTTIVE_DATE,NORESOURCE,CREATED_ON,STATUS">
                        <ItemTemplate>

                            <div style="color: #00617C">
                                <b>Request Details :</b><hr />
                            </div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">1. Request ID :</div>
                                    <div class="col-sm-8 formtext_">
                                        <asp:Label ID="lblRRfID" Text='<%# Eval("ID")%>' runat="server"></asp:Label>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">2. Indentor Name :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("IND_ENAME")%>
                                    </div>
                                </div>

                                <%--<div class="DivSpacer03"></div>--%>
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">3. <span class="rcls"></span>Requestor Name :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("REQT_ENAME")%>
                                    </div>
                                </div>
                                <%--  <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">3. <span class="rcls">*</span>Requestor Designation :</div>
                            <div class="col-sm-8">
                                <%# Eval("")%>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-4 htCr Cntrlwidth">4. <span class="rcls">*</span>Department/Profit Centre :</div>
                            <div class="col-sm-8">
                                <%# Eval("")%>
                            </div>
                        </div>--%>
                            </div>
                            <div class="DivSpacer03"></div>

                            <div class="form-inline">
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">4. <span class="rcls"></span>Designation to be recruited :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("DESRTEXT")%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth" style="max-width: 260px;">5. Replacement of existing candidate? :</div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <div class="col-sm-2 formtext_">
                                                <%# Eval("REP_EXT_EMP").ToString()=="True"?"Yes":"No"%>
                                            </div>
                                            <div class="col-sm-10">
                                                <asp:Panel ID="Panel1" runat="server" Visible='<%# Eval("REP_EXT_EMP").ToString()=="True"?true:false%>'>
                                                    <div class="">
                                                        <div class="col-sm-4 htCr txtcolor">5.1 Replace to :</div>
                                                        <div class="col-sm-8 formtext_">
                                                            <%# Eval("REP_EXT_EMP_ENAME")%>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="">
                                    <div class="form-group">
                                        <div class="col-sm-4 htCr Cntrlwidth" style="max-width: 260px;">6. Required Position is Budgeted? :</div>
                                        <div class="col-sm-8">
                                            <div class="form-group">
                                                <div class="col-sm-2 formtext_">
                                                    <%# Eval("REQ_POS_BUDGT").ToString()=="True"?"Yes":"No"%>
                                                </div>

                                                <asp:Panel ID="Panel2" runat="server" Visible='<%# Eval("REQ_POS_BUDGT").ToString()=="True"?true:false%>'>
                                                    <div class="col-sm-10">
                                                        <div class="">
                                                            <div class="col-sm-4 htCr txtcolor">6.1 From month :</div>
                                                            <div class="col-sm-2 formtext_">
                                                                <%# Eval("REQ_POS_BUDGT_FRM_MONTH")%>
                                                            </div>
                                                            <div class="col-sm-4 htCr txtcolor">6.2 Budgt Cost :</div>
                                                            <div class="col-sm-2 formtext_">
                                                                <%# Eval("REQ_POS_BUDGT_COST")%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth" style="max-width: 260px;">7. Purpose of Hiring:</div>
                                    <div class="col-sm-8">
                                        <div class="form-group">
                                            <div class="col-sm-2 formtext_">
                                                <%# Eval("PURPS_HIRNG").ToString()=="SF"?"Staffing":"Internal Resource"%>
                                            </div>
                                            <div class="col-sm-10">
                                                <asp:Panel ID="Panel3" runat="server" Visible="true">
                                                    <div class="col-sm-3 htCr txtcolor">7.1 Location:</div>
                                                    <div class="col-sm-3 formtext_">
                                                        <%# Eval("PURPS_HIRNG_LOC_TEXT")%>
                                                    </div>

                                                </asp:Panel>
                                                <asp:Panel ID="Panel4" runat="server" Visible='<%# Eval("PURPS_HIRNG").ToString()=="SF"?true:false%>'>
                                                    <div class="col-sm-3 htCr txtcolor">7.2 Project :</div>
                                                    <div class="col-sm-3 formtext_">
                                                        <%# Eval("PURPS_HIRNG_PROJ_TEXT")%>
                                                    </div>

                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">8. <span class="rcls"></span>Position Reporting to :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("POS_REPT_TO_ID_ENAME")%>
                                    </div>
                                </div>
                            </div>

                            <div class="DivSpacer03"></div>
                            <div class="form-inline">
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">9. <span class="rcls"></span>Min. Educational Qualification :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("EDU_QLATEXT")%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">10. <span class="rcls"></span>Min. Certifications :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("MIN_CERTIFNTN")%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">11. <span class="rcls"></span>Min. Total Experience :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("TOT_EXP")%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">12. <span class="rcls"></span>Min. Domain Experience :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("TOT_DOMAIN_EXP")%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">13. <span class="rcls"></span>Areas of Expertise Required :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("AREA_EXPRTSE")%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">14. <span class="rcls"></span>Other Specific Requirement :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("OTHER_SPC_REQ")%>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">15. <span class="rcls"></span>Job Description :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("JOB_DISP")%>
                                        <asp:HiddenField ID="HF_jobDisp" Value='<%# Eval("DISP_FILE")%>' runat="server" />
                                        &nbsp;(<asp:LinkButton ID="lnkDwnldjobDisp" CommandName="DOWNLOAD" runat="server" CommandArgument='<%#Eval("DISP_FILE") %>' Text="Download" Visible='<%#Eval("DISP_FILE").ToString()==""?false:true %>'></asp:LinkButton>)
                                    </div>

                                </div>

                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">16. <span class="rcls"></span>Tentative Date on Board :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("TENTTIVE_DATE")%>
                                    </div>
                                </div>


                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">17. <span class="rcls"></span>No. of Resourses :</div>
                                    <div class="col-sm-8 formtext_">
                                        <%# Eval("NORESOURCE")%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 htCr Cntrlwidth">Status :</div>
                                    <div class="col-sm-8 formtext_">
                                        <b>
                                            <asp:Label ID="lblStatus" Text='<%# Eval("STATUS")%>' runat="server"></asp:Label></b>
                                    </div>
                                </div>
                            </div>
                            <div class="respovrflw" style="padding: 10px 0">
                                <div class="col-sm-8" style="">
                                    <table class="gridview respovrflw" style="border: inset 2px WhiteSmoke; color: #00617C !important; overflow: scroll;">
                                        <tr style="text-underline-position: below">
                                            <th>Approver ID</th>
                                            <th>Approver Name</th>
                                            <th>Approved Date</th>
                                            <th>Approver Comment</th>
                                        </tr>
                                        <asp:Panel ID="pnlapp1" runat="server" Visible='<%#Eval("APPROVED1_ID").ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%#Eval("APPROVED1_ID") %></td>
                                                <td><%#Eval("APPROVED1_ID_ENAME")%></td>
                                                <td><%#Eval("APPROVED1_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900"?" - ":Eval("APPROVED1_ON")%></td>
                                                <td><%#Eval("APPROVED1_REMARKS")%></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlapp2" runat="server" Visible='<%#Eval("APPROVED2_ID").ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%#Eval("APPROVED2_ID") %></td>
                                                <td><%#Eval("APPROVED2_ID_ENAME")%></td>
                                                <td><%#Eval("APPROVED2_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900"?" - ":Eval("APPROVED2_ON")%></td>
                                                <td><%#Eval("APPROVED2_REMARKS")%></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlapp3" runat="server" Visible='<%#Eval("APPROVED3_ID").ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%#Eval("APPROVED3_ID") %></td>
                                                <td><%#Eval("APPROVED3_ID_ENAME")%></td>
                                                <td><%#Eval("APPROVED3_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900"?" - ":Eval("APPROVED3_ON")%></td>
                                                <td><%#Eval("APPROVED3_REMARKS")%></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlapp4" runat="server" Visible='<%#Eval("APPROVED4_ID").ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%#Eval("APPROVED4_ID") %></td>
                                                <td><%#Eval("APPROVED4_ID_ENAME")%></td>
                                                <td><%#Eval("APPROVED4_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900"?" - ":Eval("APPROVED4_ON")%></td>
                                                <td><%#Eval("APPROVED4_REMARKS")%></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlapp5" runat="server" Visible='<%#Eval("APPROVED5_ID").ToString()==""?false:true %>'>
                                            <tr>
                                                <td><%#Eval("APPROVED5_ID") %></td>
                                                <td><%#Eval("APPROVED5_ID_ENAME")%></td>
                                                <td><%#Eval("APPROVED5_ON","{0:dd-MM-yyyy}").ToString()=="01-01-1900"?" - ":Eval("APPROVED5_ON")%></td>
                                                <td><%#Eval("APPROVED5_REMARKS")%></td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                </div>
                            </div>
                            <div class="">
                                <asp:Panel ID="divAppRej" runat="server" class="col-sm-6">
                                    <div class="Apprvrclass">
                                        <b>Request Approve / Reject : </b>
                                        <asp:TextBox ID="txtApprRemarks" TextMode="MultiLine" PlaceHolder="Enter Remarks" runat="server" ValidationGroup="rem" CssClass="txtDropDownwidth"></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Enter Remarks..!" ForeColor="Red" ID="RF_txtApprRemarks" ValidationGroup="rem" CssClass="lblValidation" ControlToValidate="txtApprRemarks" SetFocusOnError="true" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <br />
                                        <asp:Button ID="btnApprove" Width="80px" runat="server" OnClick="btnApprove_Click" Text="Approve" ValidationGroup="rem" />
                                        <asp:Button ID="btnReject" Width="80px" runat="server" OnClick="btnReject_Click" Text="Reject" ValidationGroup="rem" />
                                    </div>
                                </asp:Panel>
                            </div>

                            <div>
                                <%--  <asp:LinkButton ID="FV_myReqEdit" runat="server" CausesValidation="false" CommandName="EDIT"
                                                                 CssClass="Fnt02" Text="Edit"></asp:LinkButton>--%>
                            </div>
                        </ItemTemplate>
                    </asp:FormView>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="FV_RRF_MyReq" />
                    <asp:AsyncPostBackTrigger ControlID="GRD_RRF" />
                    <asp:AsyncPostBackTrigger ControlID="txtsearch_rrf" />
                    <asp:AsyncPostBackTrigger ControlID="btnSearchClear" />

                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
