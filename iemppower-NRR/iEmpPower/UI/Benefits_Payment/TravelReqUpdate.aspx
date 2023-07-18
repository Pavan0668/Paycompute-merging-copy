<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="TravelReqUpdate.aspx.cs"
    Inherits="iEmpPower.UI.Benefits_Payment.TravelReqUpdate" EnableEventValidation="false" Culture="en-GB" Theme="SkinFile" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<div>
        <h2>Travel Request Update</h2>
        <div class="DivMsg">
            <asp:Label ID="LblMsg" runat="server" CssClass="msgboard"></asp:Label>
        </div>
        <div>
            <table class="TblCls">
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="GV_TravelReqUpdate" runat="server" AutoGenerateColumns="False" DataKeyNames="REINR,KZREA,KUNDE,ZORT1,ZLAND,WBS_ELEMT,SUM_ADVANC" CssClass="GvCls"
                            Width="100%" AllowPaging="True" AllowSorting="True" PageSize="4" OnPageIndexChanging="GV_TravelReqUpdate_PageIndexChanging"
                            OnRowCancelingEdit="GV_TravelReqUpdate_RowCancelingEdit" OnRowCommand="GV_TravelReqUpdate_RowCommand" OnRowDeleting="GV_TravelReqUpdate_RowDeleting"
                            OnRowEditing="GV_TravelReqUpdate_RowEditing"
                            OnRowUpdating="GV_TravelReqUpdate_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="Trip No">
                                    <ItemTemplate>
                                        <%#Eval("REINR") %>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Trip From & To">
                                    <ItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <label class="Lbl01 W01">Trip Type </label>
                                                : <asp:Label id="lblKZREA" runat="server"  Text = '<%# Eval("KZREA") %>' />
                                                </li>
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
                                    <EditItemTemplate>
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
                                    </EditItemTemplate>
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
                                    <EditItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <label class="Lbl01 W01">From Date </label>
                                                :
                                                <asp:TextBox ID="TxtGvFrmDate" runat="server" Width="49%" MaxLength="18" Text='<%#Bind("DATV1") %>' ValidationGroup="TrvlUpdateVG"></asp:TextBox>
                                                <Ajx:CalendarExtender ID="CE_TxtGvFrmDate" runat="server" Format="dd/MM/yyyy HH:mm:ss" PopupButtonID="TxtGvFrmDate"
                                                    PopupPosition="BottomLeft" TargetControlID="TxtGvFrmDate" />
                                               
                                                 <Ajx:MaskedEditExtender ID="MEE_TxtGvFrmDate" runat="server" ErrorTooltipEnabled="true" Mask="99/99/9999 99:99:99" AutoCompleteValue="00/00/0000 00:00:00"
                                                    MaskType="DateTime" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" UserDateFormat="DayMonthYear" AcceptAMPM="false"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="TxtGvFrmDate" CultureName="en-GB" />
                                                <asp:RequiredFieldValidator ID="RFV_TxtGvFrmDate" runat="server" ErrorMessage="Enter from date !" ControlToValidate="TxtGvFrmDate"
                                                    ValidationGroup="TrvlUpdateVG" Display="None" CssClass="Fnt03"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="REV_TxtGvFrmDate" runat="server" Display="None" ErrorMessage="Invalid Date (eg : DD/MM/YYYY HH:MM:SS)" ControlToValidate="TxtGvFrmDate" CssClass="Fnt03"
                                                    ValidationGroup="TrvlUpdateVG" SetFocusOnError="True" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))\s((?:[01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9])$">
                                                </asp:RegularExpressionValidator>
                                            </li>
                                            <li class="Li01">
                                                <label class="Lbl01 W01">To Date </label>
                                                :
                                                <asp:TextBox ID="TxtGvToDate" runat="server" Width="49%" MaxLength="18" Text='<%#Bind("DATB1") %>' ValidationGroup="TrvlUpdateVG"></asp:TextBox>
                                                <Ajx:CalendarExtender ID="CE_TxtGvToDate" runat="server" Format="dd/MM/yyyy HH:mm:ss" PopupButtonID="TxtGvToDate"
                                                    PopupPosition="BottomLeft" TargetControlID="TxtGvToDate" />
                                                <Ajx:MaskedEditExtender ID="MEE_TxtGvToDate" runat="server" ErrorTooltipEnabled="true" Mask="99/99/9999 99:99:99" AutoCompleteValue="00/00/0000 00:00:00"
                                                    MaskType="DateTime" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" UserDateFormat="DayMonthYear" AcceptAMPM="false"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="TxtGvToDate" CultureName="en-GB" />
                                                <asp:RequiredFieldValidator ID="RFV_TxtGvToDate" runat="server" ErrorMessage="Enter to date !" ControlToValidate="TxtGvToDate"
                                                    ValidationGroup="TrvlUpdateVG" Display="None" CssClass="Fnt03"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="REV_TxtGvToDate" runat="server" Display="None" ErrorMessage="Invalid Date (eg : DD/MM/YYYY HH:MM:SS)" ControlToValidate="TxtGvToDate" CssClass="Fnt03"
                                                    ValidationGroup="TrvlUpdateVG" SetFocusOnError="True" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))\s((?:[01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9])$">
                                                </asp:RegularExpressionValidator>
                                            </li>
                                            <li class="Li01">
                                                <label class="Lbl01 W01">Project</label>
                                                : <%# Eval("WBS_ELEMT") %></li>
                                        </ul>
                                    </EditItemTemplate>
                                    <ItemStyle Width="26%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Additional Amount & Advance">
                                    <ItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Additional Advance </label>
                                                : <%# Eval("ADDIT_AMNT") %></li>
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Total Advance </label>
                                                : <%# Eval("SUM_ADVANC") %></li>
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Currency </label>
                                                : <%# Eval("CURRENCY") %></li>
                                        </ul>
                                    </ItemTemplate>
                                   <EditItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Additional Advance </label>
                                                :
                                                <asp:TextBox ID="TxtGvAdditionalAdvance" runat="server" Width="35%" MaxLength="9" Text='<%#Bind("ADDIT_AMNT") %>' ValidationGroup="TrvlUpdateVG" Enabled='<%# Eval("COMP_CODE").ToString().Equals("2021") %>' Visible='<%# Eval("COMP_CODE").ToString().Equals("2021") %>' ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFV_TxtGvAdditionalAdvance" runat="server" ErrorMessage="Enter Additional advance amount" ControlToValidate="TxtGvAdditionalAdvance"
                                                    ValidationGroup="TrvlUpdateVG" Display="None" CssClass="Fnt03"></asp:RequiredFieldValidator>
                                                <Ajx:FilteredTextBoxExtender ID="FTB_TxtGvAdditionalAdvance" runat="server" TargetControlID="TxtGvAdditionalAdvance" ValidChars="." FilterType="Custom,Numbers"></Ajx:FilteredTextBoxExtender>
                                                <asp:Label ID="LblAddAdvance" runat="server" Text='<%# Bind("ADDIT_AMNT") %>' Enabled='<%# !Eval("COMP_CODE").ToString().Equals("2021") %>' Visible='<%# !Eval("COMP_CODE").ToString().Equals("2021") %>'></asp:Label>
                                            </li>
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Total Advance </label>
                                                : <%# Eval("SUM_ADVANC") %></li>
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Currency </label>
                                                :
                                                <div style="width: 45%; overflow: hidden; float: right;">
                                                    <asp:DropDownList ID="DDLCurrency" runat="server" onBlur="javascript:Compress_DDL(this,90%)" Enabled='<%# Eval("COMP_CODE").ToString().Equals("2021") %>' Visible='<%# Eval("COMP_CODE").ToString().Equals("2021") %>'
                                                        onChange="javascript:Compress_DDL(this,90%)" onMouseDown="javascript:Expand_DDL(this)" Font-Size="11px">
                                                    </asp:DropDownList>
                                                    <Ajx:CascadingDropDown ID="CCD_DDLCurrency" runat="server" EmptyText="SELECT CURRENCY" EmptyValue="0" LoadingText="[Loading..]" Category="Currency"
                                                        TargetControlID="DDLCurrency" PromptText="SELECT CURRENCY" PromptValue="0" ServiceMethod="GetCurrencyTypes" ServicePath="TravelReqUpdate.aspx" SelectedValue='<%# Bind("CURRENCY") %>'>
                                                    </Ajx:CascadingDropDown>
                                                </div>
                                                <asp:Label ID="LblGVTCurrency" runat="server" Text='<%# Bind("CURRENCY") %>' Enabled='<%# !Eval("COMP_CODE").ToString().Equals("2021") %>' Visible='<%# !Eval("COMP_CODE").ToString().Equals("2021") %>' ></asp:Label>
                                            </li>
                                        </ul>
                                    </EditItemTemplate>
                                    <ItemStyle Width="24%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <asp:LinkButton ID="GVLbtnEdit" runat="server" Text="Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Edit" CausesValidation="false"></asp:LinkButton>
                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <asp:LinkButton ID="GVLbtnUpdate" runat="server" Text="Update" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Update" CausesValidation="true" ValidationGroup="TrvlUpdateVG"> </asp:LinkButton>
                                                <asp:ValidationSummary ID="VS_TrvlUpdate" runat="server" ValidationGroup="TrvlUpdateVG" ShowMessageBox="true" ShowSummary="false" />
                                            </li>
                                            <li class="Li01">
                                                <asp:LinkButton ID="GVLbtnCancel" runat="server" Text="Cancel" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Cancel" CausesValidation="false"></asp:LinkButton>
                                            </li>
                                        </ul>
                                    </EditItemTemplate>
                                    <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>--%>


    <div class="table-responsive card-box">
        <h4>Travel Request Update</h4>
        <div class="DivMsg">
            <asp:Label ID="LblMsg" runat="server" CssClass="msgboard"></asp:Label>
        </div>

        <table>
            <tr>
                <td align="right">Select:</td>
                <td>
                    <asp:DropDownList ID="ddlSeachSelect" runat="server" CssClass="form-control-file" TabIndex="1">
                        <asp:ListItem Text="-SELECT-" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Trip No" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Trip Type" Value="2"></asp:ListItem>

                    </asp:DropDownList>
                </td>
                <td>
                    <asp:TextBox ID="txtsearch" runat="server" CssClass="form-control-file" TabIndex="2"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnsearch" runat="server" Text="Search" OnClick="btnsearch_Click" TabIndex="3" CssClass="btn btn-xs btn-secondary" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnclear" runat="server" Text="Clear" OnClick="btnclear_Click" TabIndex="4" CssClass="btn btn-xs btn-secondary" /></td>

            </tr>
            <tr>
                <td></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>



        <div>
            <table class="table table-hover table-sm mb-0 table_font_sm">
                <tr>
                    <td>
                        <asp:GridView ID="GV_TravelReqUpdate" runat="server" AutoGenerateColumns="False" DataKeyNames="REINR,KZREA,KUNDE,ZORT1,ZLAND,WBS_ELEMT,SUM_ADVANC,DATV1,DATB1,ADDIT_AMNT,CURRENCY"
                            Width="100%" AllowPaging="True" AllowSorting="True" PageSize="4" OnPageIndexChanging="GV_TravelReqUpdate_PageIndexChanging"
                            OnRowCancelingEdit="GV_TravelReqUpdate_RowCancelingEdit" OnRowCommand="GV_TravelReqUpdate_RowCommand" OnRowDeleting="GV_TravelReqUpdate_RowDeleting"
                            OnRowEditing="GV_TravelReqUpdate_RowEditing"
                            OnRowUpdating="GV_TravelReqUpdate_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="Trip No">
                                    <ItemTemplate>
                                        <%#Eval("REINR") %>
                                    </ItemTemplate>
                                    <ItemStyle Width="50px" VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Trip From & To">
                                    <ItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <label class="Lbl01 W01">Trip Type </label>
                                                :
                                                <asp:Label ID="lblKZREA" runat="server" Text='<%# Eval("KZREA") %>' />
                                            </li>
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
                                    <EditItemTemplate>
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
                                    </EditItemTemplate>
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
                                    <EditItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <label class="Lbl01 W01">From Date </label>
                                                :
                                                <asp:TextBox ID="TxtGvFrmDate" runat="server" Width="49%" MaxLength="18" Text='<%#Bind("DATV1") %>' ValidationGroup="TrvlUpdateVG"></asp:TextBox>
                                                <Ajx:CalendarExtender ID="CE_TxtGvFrmDate" runat="server" Format="dd/MM/yyyy HH:mm:ss" PopupButtonID="TxtGvFrmDate"
                                                    PopupPosition="BottomLeft" TargetControlID="TxtGvFrmDate" />

                                                <Ajx:MaskedEditExtender ID="MEE_TxtGvFrmDate" runat="server" ErrorTooltipEnabled="true" Mask="99/99/9999 99:99:99" AutoCompleteValue="00/00/0000 00:00:00"
                                                    MaskType="DateTime" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" UserDateFormat="DayMonthYear" AcceptAMPM="false"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="TxtGvFrmDate" CultureName="en-GB" />
                                                <asp:RequiredFieldValidator ID="RFV_TxtGvFrmDate" runat="server" ErrorMessage="Enter from date !" ControlToValidate="TxtGvFrmDate"
                                                    ValidationGroup="TrvlUpdateVG" Display="None" CssClass="Fnt03"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="REV_TxtGvFrmDate" runat="server" Display="None" ErrorMessage="Invalid Date (eg : DD/MM/YYYY HH:MM:SS)" ControlToValidate="TxtGvFrmDate" CssClass="Fnt03"
                                                    ValidationGroup="TrvlUpdateVG" SetFocusOnError="True" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))\s((?:[01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9])$">
                                                </asp:RegularExpressionValidator>
                                            </li>
                                            <li class="Li01">
                                                <label class="Lbl01 W01">To Date </label>
                                                :
                                                <asp:TextBox ID="TxtGvToDate" runat="server" Width="49%" MaxLength="18" Text='<%#Bind("DATB1") %>' ValidationGroup="TrvlUpdateVG"></asp:TextBox>
                                                <Ajx:CalendarExtender ID="CE_TxtGvToDate" runat="server" Format="dd/MM/yyyy HH:mm:ss" PopupButtonID="TxtGvToDate"
                                                    PopupPosition="BottomLeft" TargetControlID="TxtGvToDate" />
                                                <Ajx:MaskedEditExtender ID="MEE_TxtGvToDate" runat="server" ErrorTooltipEnabled="true" Mask="99/99/9999 99:99:99" AutoCompleteValue="00/00/0000 00:00:00"
                                                    MaskType="DateTime" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" UserDateFormat="DayMonthYear" AcceptAMPM="false"
                                                    OnInvalidCssClass="MaskedEditError" TargetControlID="TxtGvToDate" CultureName="en-GB" />
                                                <asp:RequiredFieldValidator ID="RFV_TxtGvToDate" runat="server" ErrorMessage="Enter to date !" ControlToValidate="TxtGvToDate"
                                                    ValidationGroup="TrvlUpdateVG" Display="None" CssClass="Fnt03"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="REV_TxtGvToDate" runat="server" Display="None" ErrorMessage="Invalid Date (eg : DD/MM/YYYY HH:MM:SS)" ControlToValidate="TxtGvToDate" CssClass="Fnt03"
                                                    ValidationGroup="TrvlUpdateVG" SetFocusOnError="True" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))\s((?:[01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9])$">
                                                </asp:RegularExpressionValidator>
                                            </li>
                                            <li class="Li01">
                                                <label class="Lbl01 W01">Project</label>
                                                : <%# Eval("WBS_ELEMT") %></li>
                                        </ul>
                                    </EditItemTemplate>
                                    <ItemStyle Width="26%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Additional Amount & Advance">
                                    <ItemTemplate>
                                        <ul class="UlCls01">

                                            <li class="Li01">
                                                <label class="Lbl01 W02">Advance Details </label>
                                                :
                                        
                                                 <asp:LinkButton ID="GVLbtnView" runat="server" Text="Click here" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="View" CausesValidation="true"> </asp:LinkButton>


                                            </li>
                                            <%-- <li class="Li01">--%>
                                            <%-- <label class="Lbl01 W02">Purpose </label>
                                                : <%# Eval("PURPOSE") %></li>--%>
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Additional Advance </label>
                                                : <%# Eval("ADDIT_AMNT") %>
                                                 
                                            </li>
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Currency </label>
                                                : <%# Eval("CURRENCY") %></li>


                                            <%-- <li class="Li01">
                                                <label class="Lbl01 W02">Total Advance </label>
                                                : <%# Eval("SUM_ADVANC") %></li>--%>
                                            <%-- <li class="Li01">
                                                <label class="Lbl01 W02">Settled </label>
                                                : <%# Eval("SETTLED") %></li>--%>
                                        </ul>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Additional Advance </label>
                                                :
                                                <asp:TextBox ID="TxtGvAdditionalAdvance" runat="server" Width="35%" MaxLength="9" Text='<%#Bind("ADDIT_AMNT") %>' ValidationGroup="TrvlUpdateVG" Enabled='<%# Eval("COMP_CODE").ToString().Equals("2021") %>' Visible='<%# Eval("COMP_CODE").ToString().Equals("2021") %>'></asp:TextBox>

                                                <%--<asp:TextBox ID="TxtGvAdditionalAdvance" runat="server" Width="35%" MaxLength="9" Text="" ValidationGroup="TrvlUpdateVG" Enabled='<%# Eval("COMP_CODE").ToString().Equals("2021") %>' Visible='<%# Eval("COMP_CODE").ToString().Equals("2021") %>'></asp:TextBox>
                                                --%>
                                                <asp:RequiredFieldValidator ID="RFV_TxtGvAdditionalAdvance" runat="server" ErrorMessage="Enter Additional advance amount" ControlToValidate="TxtGvAdditionalAdvance"
                                                    ValidationGroup="TrvlUpdateVG" Display="None" CssClass="Fnt03"></asp:RequiredFieldValidator>
                                                <Ajx:FilteredTextBoxExtender ID="FTB_TxtGvAdditionalAdvance" runat="server" TargetControlID="TxtGvAdditionalAdvance" ValidChars="." FilterType="Custom,Numbers"></Ajx:FilteredTextBoxExtender>
                                                <asp:Label ID="LblAddAdvance" runat="server" Text='<%# Bind("ADDIT_AMNT") %>' Enabled='<%# !Eval("COMP_CODE").ToString().Equals("2021") %>' Visible='<%# !Eval("COMP_CODE").ToString().Equals("2021") %>'></asp:Label>
                                            </li>







                                            <%--                                            <li class="Li01">
                                                <label class="Lbl01 W02">Total Advance </label>
                                                : <%# Eval("SUM_ADVANC") %></li>--%>
                                            <li class="Li01">
                                                <label class="Lbl01 W02">Currency </label>
                                                :
                                                <div style="width: 45%; overflow: hidden; float: right;">
                                                    <asp:DropDownList ID="DDLCurrency" runat="server" onBlur="javascript:Compress_DDL(this,90%)" Enabled='<%# Eval("COMP_CODE").ToString().Equals("2021") %>' Visible='<%# Eval("COMP_CODE").ToString().Equals("2021") %>'
                                                        onChange="javascript:Compress_DDL(this,90%)" onMouseDown="javascript:Expand_DDL(this)" Font-Size="11px">
                                                    </asp:DropDownList>
                                                    <Ajx:CascadingDropDown ID="CCD_DDLCurrency" runat="server" EmptyText="SELECT CURRENCY" EmptyValue="0" LoadingText="[Loading..]" Category="Currency"
                                                        TargetControlID="DDLCurrency" PromptText="SELECT CURRENCY" PromptValue="0" ServiceMethod="GetCurrencyTypes" ServicePath="TravelReqUpdate.aspx" SelectedValue='<%# Bind("CURRENCY") %>'>
                                                    </Ajx:CascadingDropDown>
                                                </div>
                                                <asp:Label ID="LblGVTCurrency" runat="server" Text='<%# Bind("CURRENCY") %>' Enabled='<%# !Eval("COMP_CODE").ToString().Equals("2021") %>' Visible='<%# !Eval("COMP_CODE").ToString().Equals("2021") %>'></asp:Label>
                                            </li>


                                        </ul>
                                    </EditItemTemplate>
                                    <ItemStyle Width="24%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <asp:LinkButton ID="GVLbtnEdit" runat="server" Text="Edit" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Edit" CausesValidation="false"></asp:LinkButton>


                                            </li>
                                        </ul>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <ul class="UlCls01">
                                            <li class="Li01">
                                                <asp:LinkButton ID="GVLbtnUpdate" runat="server" Text="Update" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Update" CausesValidation="true" ValidationGroup="TrvlUpdateVG"> </asp:LinkButton>
                                                <asp:ValidationSummary ID="VS_TrvlUpdate" runat="server" ValidationGroup="TrvlUpdateVG" ShowMessageBox="true" ShowSummary="false" />
                                            </li>
                                            <li class="Li01">
                                                <asp:LinkButton ID="GVLbtnCancel" runat="server" Text="Cancel" CommandArgument='<%# ((GridViewRow) Container).RowIndex %>' CommandName="Cancel" CausesValidation="false"></asp:LinkButton>
                                            </li>
                                        </ul>
                                    </EditItemTemplate>
                                    <ItemStyle Width="4%" VerticalAlign="Middle" HorizontalAlign="Center" />
                                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="grdAdvance" runat="server" CssClass="gridview"
                            AllowPaging="True" AutoGenerateColumns="False"
                            AllowSorting="True" Width="100%"
                            PageSize="10">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="ADV_AMOUNT" HeaderText="ADVANCE AMOUNT" />
                                <asp:BoundField DataField="ADV_CURR" HeaderText="ADVANCE CURRENCY" />

                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />

                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="12pt" Font-Names="verdana" />

                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />

                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />

                            <RowStyle BackColor="#EFF3FB" />

                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
