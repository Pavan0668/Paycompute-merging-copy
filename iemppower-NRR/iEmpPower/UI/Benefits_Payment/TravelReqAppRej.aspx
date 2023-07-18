<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="TravelReqAppRej.aspx.cs"
    Inherits="iEmpPower.UI.Benefits_Payment.TravelReqAppRej" EnableEventValidation="false" Culture="en-GB" Theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h4> Travel Request Approve / Reject</h4>
        <div class="DivMsg">
            <asp:Label ID="LblMsg" runat="server" CssClass="msgboard"></asp:Label>
        </div>
        <div>
            <table class="table table-hover table-sm mb-0 table_font_sm">
                <tr>
                    <td>
                        <asp:GridView ID="GV_TravelReqAppRej" runat="server" AutoGenerateColumns="False" DataKeyNames="REINR,KZREA,KUNDE,ZORT1,ZLAND,DATV1,DATB1,WBS_ELEMT,SUM_ADVANC,ADDIT_AMNT,CURRENCY,CREATED_BY"
                             CssClass="table table-hover table-sm mb-0 table_font_sm"
                            Width="100%" AllowPaging="True" AllowSorting="True" PageSize="3" OnPageIndexChanging="GV_TravelReqAppRej_PageIndexChanging"
                            OnRowCancelingEdit="GV_TravelReqAppRej_RowCancelingEdit" OnRowCommand="GV_TravelReqAppRej_RowCommand" OnRowDeleting="GV_TravelReqAppRej_RowDeleting"
                            OnRowEditing="GV_TravelReqAppRej_RowEditing" OnRowUpdating="GV_TravelReqAppRej_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="Trip No & PERNR">
                                    <ItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01"><%#Eval("REINR") %></li>
                                            <li class="Li01"><%#Eval("PERNR") %></li>
                                        </ul>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Trip From & To">
                                    <ItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <label class="Lbl01 W01">Trip Type </label>
                                                : <%# Eval("KZREA") %></li>
                                            <li class="Li01">
                                                <label class="Lbl01 W01">From </label>
                                                : <%# Eval("KUNDE") %></li>
                                            <li class="Li01">
                                                <label class="Lbl01 W01">To </label>
                                                : <%# Eval("ZORT1") %></li>
                                            <li class="Li01">
                                                <label class="Lbl01 W01">Country </label>
                                                : <%# Eval("ZLAND") %></li>
                                        </ul>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Trip From & To Date">
                                    <ItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <label class="Lbl01 W01">From Date </label>
                                                : <%# Eval("DATV1") %></li>
                                            <li class="Li01">
                                                <label class="Lbl01 W01">To Date </label>
                                                : <%# Eval("DATB1") %></li>
                                            <li class="Li01">
                                                <label class="Lbl01 W01">Project</label>
                                                : <%# Eval("WBS_ELEMT") %></li>
                                        </ul>
                                    </ItemTemplate>
                                    
                                    <ItemStyle Width="26%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Additional Advance & Currency">
                                    <ItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Additional Advance </label>
                                                : <%# Eval("ADDIT_AMNT") %></li>
                                          <%--  <li class="Li01">
                                                <label class="Lbl01 W02">Total Advance </label>
                                                : <%# Eval("SUM_ADVANC") %></li>--%>
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Currency </label>
                                                : <%# Eval("CURRENCY") %></li>
                                        </ul>
                                    </ItemTemplate>                                    
                                    <ItemStyle Width="24%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <asp:LinkButton ID="GVLbtnApp" runat="server" Text="Approve" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="APPROVE" 
                                                    CausesValidation="false" OnClientClick="javascript:confirm('Do you want to Approve this travel request !')"></asp:LinkButton>
                                            </li>
                                            <li class="">
                                                <asp:LinkButton ID="GVLbtnRej" runat="server" Text="Reject" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="REJECT" 
                                                    CausesValidation="false" OnClientClick="javascript:confirm('Do you want to Reject this travel request !')"></asp:LinkButton>
                                            </li>
                                        </ul>
                                    </ItemTemplate>                                    
                                    <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
