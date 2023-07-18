<%@ Page Title="Family Member Info" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" EnableEventValidation="false" Inherits="UI_Personal_Information_family_members"
    Theme="SkinFile" Culture="en-GB" CodeBehind="family_members.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .Td06
        {
            color: #004080;
            font-size: 13px;
            width: 190px;
            padding: 3px;
            text-align: justify !important;
            line-height: 18px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="DivSpacer01">

        <div class="header">
            <div class="row clearfix">
                <div class="col-xs-12">

                    <span class="HeadFontSize">Family&nbsp;Members / Dependent&nbsp;Information&nbsp;</span><br />

                </div>
            </div>
            <asp:Label ID="LblMsg" runat="server" CssClass="lblMsg"></asp:Label>
        </div>
        <div class="body">
            <div id="real_time_chart" class="dashboard-flot-chart">

                <div id="divbrdr" class="divfr">
                    <div class="search-section">

                        <asp:MultiView ID="MV_FamilyInfo" runat="server">
                            <asp:View ID="V_ViewFamilyInfo" runat="server">
                                <div class="DivSpacer01">
                                    &nbsp;<asp:LinkButton ID="LblAddFamilyInfo" runat="server" CausesValidation="false" Text="Add New Family Info" CssClass="Fr linkbtn Fnt01" OnClick="LblAddFamilyInfo_Click"></asp:LinkButton>
                                </div>
                                <div class="DivSpacer01 respovrflw" >
                                    <asp:GridView ID="GV_FamilyInfo" runat="server" AutoGenerateColumns="False" AllowPaging="false" DataKeyNames="ID,STEXT" CssClass="Grid" GridLines="Both" PagerStyle-CssClass="cssPager"
                                        PageSize="5" Width="100%" OnRowCommand="GV_FamilyInfo_RowCommand" OnRowDeleting="GV_FamilyInfo_RowDeleting" OnRowEditing="GV_FamilyInfo_RowEditing" OnRowUpdating="GV_FamilyInfo_RowUpdating">
                                        <Columns>
                                            <asp:BoundField DataField="RowNumber" HeaderText="Slno" SortExpression="RowNumber">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="35px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="STEXT" HeaderText="Member Type" SortExpression="STEXT">
                                                <ItemStyle />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FAVOR" HeaderText="First Name" SortExpression="FAVOR">
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FANAM" HeaderText="Last Name" SortExpression="FANAM">
                                                <ItemStyle Width="20%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FASEX_Name" HeaderText="Gender" SortExpression="FASEX_Name">
                                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FGBDT" HeaderText="DOB" DataFormatString="{0:dd-MMM-yyyy}" SortExpression="FGBDT">
                                                <ItemStyle Width="11%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AGE" HeaderText="Age" SortExpression="AGE">
                                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="DATE_FROM" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="From"
                                    SortExpression="DATE_FROM">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DATE_TO" DataFormatString="{0:dd-MMM-yyyy}" HeaderText="To"
                                    SortExpression="DATE_TO">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                            <%--   <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS">
                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="GVViewFamilyInfo" runat="server" CausesValidation="false" CommandName="VIEW"
                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="linkbtn Fnt02" Text="View"></asp:LinkButton>
                                                    &nbsp; 
                                        <%-- <asp:LinkButton ID="GVViewFamilyInfoDelete" runat="server" CommandName="DELETE" CausesValidation="true"
                                             CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CssClass="Fnt02" Text="Delete"
                                             OnClientClick="return confirm('Do you want to delete this Family member info ?')"></asp:LinkButton>--%>
                                                </ItemTemplate>
                                                <ItemStyle Width="7%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="DivSpacer01 Div03">
                                    <asp:Repeater ID="RptrFamilyInfoPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>' OnClick="RptrLeaveOverviewPagerPage_Changed" CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="DivSpacer01"></div>
                            </asp:View>
                            <asp:View ID="V_AddEditFamilyInfo" runat="server">
                                <div class="DivSpacer01">
                                    <div style="width: 90%; float: left"></div>
                                    <div style="width: 10%; float: right">
                                        <asp:LinkButton ID="LbtnBackFamilyInfoView" runat="server"  CausesValidation="false" Text="Back" CssClass="Fr linkbtn Fnt01" OnClick="LbtnBackFamilyInfoView_Click"></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="DivSpacer01">
                                    <asp:FormView ID="FV_FamilyInfo" runat="server" DataKeyNames="ID" OnItemCommand="FV_FamilyInfo_ItemCommand" OnModeChanging="FV_FamilyInfo_ModeChanging">
                                        <%--<ItemTemplate>
                                            <div>
                                                <b>View Family member information - [<span style="text-transform: uppercase;"><%# Eval("STEXT")%></span>]</b>
                                                <%C--  <hr class="HrCls" />--C%>
                                                <div class="DivSpacer01"></div>
                                            </div>
                                            <div class="DivSpacer03 Cb">
                                                <table class="TblCls Cb Fl">
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06">Family Type</td>
                                                        <td class="Td07"><b>:</b> </td>
                                                        <td style="text-transform: uppercase;">
                                                            <%# Eval("STEXT")%>
                                                        </td>
                                                        <td colspan="2"></td>
                                                        <td>&nbsp; </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06">First Name
                                                        </td>
                                                        <td class="Td07">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FAVOR")%>
                                                        </td>
                                                        <td colspan="2"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06">Last Name
                                                        </td>
                                                        <td class="Td07">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FANAM")%>

                                                        </td>
                                                        <td colspan="2"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06 Td08">Date Of Birth
                                                        </td>
                                                        <td class="Td07 Td08">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FGBDT", "{0:dd/MM/yyyy}")%>
                                                        </td>
                                                        <td colspan="2"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06 Td08">Gender
                                                        </td>
                                                        <td class="Td07 Td08">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <%# Eval("FASEX_Name")%>                              
                                                        </td>
                                                        <td colspan="2"></td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <asp:Panel runat="server" ID="pnlchilditem" Visible='<%# (Eval("FAMSA").ToString()=="2") ? true :false   %>'>
                                                        <tr class='Cls02 <%# string.Format("{0}",Eval("FAMSA").ToString()=="2"?"":"DispNone")%>'>
                                                            <td class="TdMandate"></td>
                                                            <td class="Td06">Other allowances
                                                            </td>
                                                            <td class="Td07">
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <%# Eval("KDBSL")%>
                                                            </td>
                                                            <td colspan="2"></td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr class='Cls02 <%# string.Format("{0}",Eval("FAMSA").ToString()=="2"?"":"DispNone")%>'>
                                                            <td class="TdMandate"></td>
                                                            <td class="Td06">Child Hostel allowances
                                                            </td>
                                                            <td class="Td07">
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <%# Eval("KDBGR")%>
                                                            </td>
                                                            <td colspan="2"></td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr class='Cls02 <%# string.Format("{0}",Eval("FAMSA").ToString()=="2"?"":"DispNone")%>'>
                                                            <td class="TdMandate"></td>
                                                            <td class="Td06">Child Educational allowances
                                                            </td>
                                                            <td class="Td07">
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <%# Eval("KDZUL")%>
                                                            </td>
                                                            <td colspan="2"></td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <tr>

                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="BtnSubmit" runat="server" Text="Edit" CausesValidation="false" CommandName="EDITFAMILY" TabIndex="1"
                                                                Enabled='<%# (Eval("TRANSSTATUS").ToString()=="Updated") ? false : true  %>'
                                                                Visible='<%# (Eval("TRANSSTATUS").ToString()=="Updated") ? false : true  %>'
                                                                OnClientClick="validate('Add');" />&nbsp;                                               
                                                        </td>
                                                        <td colspan="3">
                                                            <div class="Fr" style="width: 99%; color: Red;">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="DivSpacer01">
                                            </div>
                                        </ItemTemplate>--%>

                                         <ItemTemplate>
                                            <div>
                                                <b>View Family member information - [<span style="text-transform: uppercase;"><%# Eval("STEXT")%></span>]</b>
                                                <%--  <hr class="HrCls" />--%>
                                                <div class="DivSpacer01"></div>
                                            </div>
                                            <div class="DivSpacer03 Cb">

                                                <div class="form-inline">
                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> Family Type&nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("STEXT")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> First Name&nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("FAVOR")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> Last Name&nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("FANAM")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> Date Of Birth&nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("FGBDT", "{0:dd/MM/yyyy}")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> Gender&nbsp;<b>:</b></div>
                                                        <div class="col-sm-6"><%# Eval("FASEX_Name")%> </div>
                                                    </div>

                                                    <asp:Panel runat="server" ID="Panel1" Visible='<%# (Eval("FAMSA").ToString()=="2") ? true :false   %>'>
                                                        <div class='form-group <%# string.Format("{0}",Eval("FAMSA").ToString()=="2"?"":"DispNone")%>'>
                                                            <div class="col-sm-2 htCr">Other allowances &nbsp;<b>:</b></div>
                                                            <div class="col-sm-6"><%# Eval("KDBSL")%></div>
                                                        </div>

                                                        <div class='form-group <%# string.Format("{0}",Eval("FAMSA").ToString()=="2"?"":"DispNone")%>'>
                                                            <div class="col-sm-2 htCr">Child Hostel allowances&nbsp;<b>:</b></div>
                                                            <div class="col-sm-6"><%# Eval("KDBGR")%></div>
                                                        </div>

                                                        <div class='form-group <%# string.Format("{0}",Eval("FAMSA").ToString()=="2"?"":"DispNone")%>'>
                                                            <div class="col-sm-2 htCr">Child Educational allowances&nbsp;<b>:</b></div>
                                                            <div class="col-sm-6"><%# Eval("KDZUL")%></div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                                <div class="btn-group-sm">
                                                    <div class="col-sm-2">
                                                        <asp:Button ID="Button1" runat="server" Text="Edit" CausesValidation="false" CommandName="EDITFAMILY" TabIndex="1"
                                                            Enabled='<%# (Eval("TRANSSTATUS").ToString()=="Updated") ? false : true  %>'
                                                            Visible='<%# (Eval("TRANSSTATUS").ToString()=="Updated") ? false : true  %>'
                                                            OnClientClick="validate('Add');" />
                                                    </div>
                                                </div>

                                                
                                            </div>
                                            <div class="DivSpacer01">
                                            </div>
                                        </ItemTemplate>

                                        <%--<EditItemTemplate>
                                            <div>
                                                <b>Edit Family member information - [<span style="text-transform: uppercase;"><%# Eval("STEXT")%></span>]</b>
                                                <%C--   <hr class="HrCls" />--C%>
                                                <div class="DivSpacer01"></div>
                                            </div>
                                            <div class="DivSpacer03 Cb">
                                                <table class="TblCls Cb Fl">
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06">Family Type</td>
                                                        <td class="Td07"><b>:</b> </td>
                                                        <td style="text-transform: uppercase;">
                                                            <%# Eval("STEXT")%>
                                                        </td>
                                                        <td colspan="2"></td>
                                                        <td>&nbsp; </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06">First Name
                                                        </td>
                                                        <td class="Td07">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtEditFirstName" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                MaxLength="40" TabIndex="2" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" ValidationGroup="UpdateFamilyInfoVG" Text=' <%# Bind("FAVOR")%>'></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="FTB_TxtEditFirstName" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                                ValidChars=" ." TargetControlID="TxtEditFirstName">
                                                            </Ajx:FilteredTextBoxExtender>

                                                        </td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="RFV_TxtEditFirstName" runat="server" ControlToValidate="TxtEditFirstName" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter First Name" ValidationGroup="UpdateFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06">Last Name
                                                        </td>
                                                        <td class="Td07">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtEditLastName" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                MaxLength="40" TabIndex="3" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" ValidationGroup="UpdateFamilyInfoVG" Text='<%# Bind("FANAM")%>'></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="FTB_TxtEditLastName" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                                ValidChars=" ." TargetControlID="TxtEditLastName">
                                                            </Ajx:FilteredTextBoxExtender>

                                                        </td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="RFV_TxtEditLastName" runat="server" ControlToValidate="TxtEditLastName" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter Last name" ValidationGroup="UpdateFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06 Td08">Date Of Birth</td>
                                                        <td class="Td07 Td08">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtEditDOB" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                TabIndex="4" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" ValidationGroup="UpdateFamilyInfoVG" Text='<%# Eval("FGBDT", "{0:dd/MM/yyyy}")%>'></asp:TextBox>
                                                            <Ajx:MaskedEditExtender ID="MEE_TxtEditDOB" runat="server" AcceptNegative="Left"
                                                                CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError" TargetControlID="TxtEditDOB" AcceptAMPM="false" ClearTextOnInvalid="true"
                                                                UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour" />
                                                            <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="TxtEditDOB" PopupButtonID="TxtEditDOB">
                                                            </Ajx:CalendarExtender>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="RFV_TxtEditDOB" runat="server" ControlToValidate="TxtEditDOB" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter Date Of Birth" ValidationGroup="UpdateFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <asp:RangeValidator ID="RV_TxtEditDOB" runat="server" ControlToValidate="TxtEditDOB" ForeColor="Red" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Invalid DOB [Future Dates not allowed]" MaximumValue='<%# DateTime.Now.ToString("dd/MM/yyyy") %>' MinimumValue='<%# DateTime.Now.AddYears(-100).ToString("dd/MM/yyyy") %>'
                                                                Type="Date" SetFocusOnError="True" ValidationGroup="UpdateFamilyInfoVG"></asp:RangeValidator>
                                                            <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic" CssClass="lblValidation"
                                                                ErrorMessage="Invalid Date" ControlToValidate="TxtEditDOB" ValidationGroup="UpdateFamilyInfoVG"
                                                                SetFocusOnError="True" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06 Td08">Gender
                                                        </td>
                                                        <td class="Td07 Td08">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="RbtnEditGender" runat="server" ValidationGroup="UpdateFamilyInfoVG" TabIndex="5" RepeatDirection="Horizontal"
                                                                SelectedValue='<%# Bind("FASEX") %>' onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'">
                                                                <asp:ListItem Value="1" Text="Male" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Female"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="RFV_RbtnEditGender" runat="server" ControlToValidate="RbtnEditGender" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please select Gender" ValidationGroup="UpdateFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <asp:Panel runat="server" ID="pnlchildEdit" Visible='<%# (Eval("FAMSA").ToString()=="2") ? true :false   %>'>
                                                        <tr>
                                                            <td class="TdMandate"></td>
                                                            <td class="Td06">Other allowances
                                                            </td>
                                                            <td class="Td07">
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="ChkEditOtherAllowances" TabIndex="6" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("KDBSL").ToString() == "Yes"  ? "true" : "false") %>' />
                                                            </td>
                                                            <td colspan="2"></td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TdMandate"></td>
                                                            <td class="Td06">Child Hostel allowances
                                                            </td>
                                                            <td class="Td07">
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="ChkEditChildHostelAllowances" TabIndex="7" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("KDBGR").ToString() == "Yes"  ? "true" : "false") %>' />
                                                            </td>
                                                            <td colspan="2"></td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TdMandate"></td>
                                                            <td class="Td06">Child Educational allowances
                                                            </td>
                                                            <td class="Td07">
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="ChkEditChildEducationalAllowances" TabIndex="8" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" Checked='<%# Convert.ToBoolean( Eval("KDZUL").ToString() == "Yes" ? "true" : "false") %>' />
                                                            </td>
                                                            <td colspan="2"></td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <tr>

                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="BtnSubmit" runat="server" Text="Update" TabIndex="9" ValidationGroup="UpdateFamilyInfoVG" CommandName="UPDATEFAMILY"
                                                                OnClientClick="validate('Update');" />&nbsp;
                                                 <asp:Button ID="BtnCancel" runat="server" Text="Cancel" TabIndex="10" CommandName="CANCEL" />
                                                        </td>
                                                        <td colspan="3">
                                                            <div class="Fr" style="width: 99%; color: Red;">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="DivSpacer01">
                                            </div>
                                        </EditItemTemplate>--%>

                                         <EditItemTemplate>
                                            <div>
                                                <b>Edit Family member information - [<span style="text-transform: uppercase;"><%# Eval("STEXT")%></span>]</b>
                                                <%--   <hr class="HrCls" />--%>
                                                <div class="DivSpacer01"></div>
                                            </div>
                                            <div class="DivSpacer03 Cb">

                                                <div class="form-inline">
                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> Family Type &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6" style="text-transform: uppercase"><%# Eval("STEXT")%></div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> First Name &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="TxtEditFirstName" runat="server" autocomplete="off" CssClass="txtDropDownwidth" placeholder="Enter First Name"
                                                                MaxLength="40" TabIndex="2" ValidationGroup="UpdateFamilyInfoVG" Text=' <%# Bind("FAVOR")%>'></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="FTB_TxtEditFirstName" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                                ValidChars=" ." TargetControlID="TxtEditFirstName">
                                                            </Ajx:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator ID="RFV_TxtEditFirstName" runat="server" ControlToValidate="TxtEditFirstName" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter First Name" ValidationGroup="UpdateFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span>  Last Name&nbsp;<b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="TxtEditLastName" runat="server" autocomplete="off" CssClass="txtDropDownwidth" placeholder="Enter Last Name"
                                                                MaxLength="40" TabIndex="3" ValidationGroup="UpdateFamilyInfoVG" Text='<%# Bind("FANAM")%>'></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="FTB_TxtEditLastName" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                                ValidChars=" ." TargetControlID="TxtEditLastName">
                                                            </Ajx:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator ID="RFV_TxtEditLastName" runat="server" ControlToValidate="TxtEditLastName" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter Last name" ValidationGroup="UpdateFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span>  Date Of Birth&nbsp;<b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="TxtEditDOB" runat="server" autocomplete="off" CssClass="txtDropDownwidth" placeholder="Enter Date Of Birth"
                                                                TabIndex="4" ValidationGroup="UpdateFamilyInfoVG" Text='<%# Eval("FGBDT", "{0:dd/MM/yyyy}")%>'></asp:TextBox>
                                                            <Ajx:MaskedEditExtender ID="MEE_TxtEditDOB" runat="server" AcceptNegative="Left"
                                                                CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError" TargetControlID="TxtEditDOB" AcceptAMPM="false" ClearTextOnInvalid="true"
                                                                UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour" />
                                                            <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="TxtEditDOB" PopupButtonID="TxtEditDOB">
                                                            </Ajx:CalendarExtender>

                                                            <asp:RequiredFieldValidator ID="RFV_TxtEditDOB" runat="server" ControlToValidate="TxtEditDOB" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter Date Of Birth" ValidationGroup="UpdateFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            <asp:RangeValidator ID="RV_TxtEditDOB" runat="server" ControlToValidate="TxtEditDOB" ForeColor="Red" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Invalid DOB [Future Dates not allowed]" MaximumValue='<%# DateTime.Now.ToString("dd/MM/yyyy") %>' MinimumValue='<%# DateTime.Now.AddYears(-100).ToString("dd/MM/yyyy") %>'
                                                                Type="Date" SetFocusOnError="True" ValidationGroup="UpdateFamilyInfoVG"></asp:RangeValidator>
                                                            <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic" CssClass="lblValidation"
                                                                ErrorMessage="Invalid Date" ControlToValidate="TxtEditDOB" ValidationGroup="UpdateFamilyInfoVG"
                                                                SetFocusOnError="True" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" ForeColor="Red"></asp:RegularExpressionValidator>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> Gender &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <asp:RadioButtonList ID="RbtnEditGender" runat="server" ValidationGroup="UpdateFamilyInfoVG" TabIndex="5" RepeatDirection="Horizontal"
                                                                SelectedValue='<%# Bind("FASEX") %>' placeholder="Gender">
                                                                <asp:ListItem Value="1" Text="Male" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Female"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                            <asp:RequiredFieldValidator ID="RFV_RbtnEditGender" runat="server" ControlToValidate="RbtnEditGender" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please select Gender" ValidationGroup="UpdateFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <asp:Panel runat="server" ID="pnlchildEdit" Visible='<%# (Eval("FAMSA").ToString()=="2") ? true :false   %>'>
                                                        <div class="form-group">
                                                            <div class="col-sm-2 htCr">Other allowances&nbsp;<b>:</b></div>
                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="ChkEditOtherAllowances" TabIndex="6" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("KDBSL").ToString() == "Yes"  ? "true" : "false") %>' />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-sm-2 htCr">Child Hostel allowances&nbsp;<b>:</b></div>
                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="ChkEditChildHostelAllowances" TabIndex="7" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" Checked='<%# Convert.ToBoolean(Eval("KDBGR").ToString() == "Yes"  ? "true" : "false") %>' />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-sm-2 htCr">Child Educational allowances&nbsp;<b>:</b></div>
                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="ChkEditChildEducationalAllowances" TabIndex="8" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" Checked='<%# Convert.ToBoolean( Eval("KDZUL").ToString() == "Yes" ? "true" : "false") %>' />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <div class="btn-group-sm">
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="BtnSubmit" runat="server" Text="Update" TabIndex="9" ValidationGroup="UpdateFamilyInfoVG" CommandName="UPDATEFAMILY"
                                                            OnClientClick="validate('Update');" />
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" TabIndex="10" CommandName="CANCEL" />
                                                    </div>
                                                </div>

                                                
                                            </div>
                                            <div class="DivSpacer01">
                                            </div>
                                        </EditItemTemplate>

                                        <%--<InsertItemTemplate>
                                            <div>
                                                <b>Add new Family information</b>
                                                <%C--  <hr class="HrCls" />--C%>
                                                <div class="DivSpacer01"></div>
                                            </div>
                                            <div class="DivSpacer03 Cb">
                                                <table class="TblCls Cb Fl">
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06">Family Type</td>
                                                        <td class="Td07"><b>:</b> </td>
                                                        <td>
                                                            <div style="width: 210px; overflow: hidden;">
                                                                <asp:DropDownList ID="DDL_FamilyTypes" runat="server" CssClass="txtDropDownwidth" OnSelectedIndexChanged="DDL_FamilyTypes_SelectedIndexChanged" AutoPostBack="true"
                                                                    TabIndex="11" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" ValidationGroup="AddFamilyInfoVG">
                                                                </asp:DropDownList>
                                                                <Ajx:CascadingDropDown ID="CCD_DDL_FamilyTypes" runat="server" Category="DDL_FamilyTypes" EmptyText="- SELECT FAMILY TYPE -" EmptyValue="0"
                                                                    LoadingText="[LOADING FAMILY TYPES....]" PromptText="- SELECT FAMILY TYPE -" PromptValue="0" TargetControlID="DDL_FamilyTypes"
                                                                    ServicePath="~/WebService/Service.asmx" ServiceMethod="GetFamilyMemTypes">
                                                                </Ajx:CascadingDropDown>
                                                            </div>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="RFV_DDL_FamilyTypes" runat="server" ControlToValidate="DDL_FamilyTypes" Display="Dynamic" CssClass="lblValidation"
                                                                ErrorMessage="Please select Family type" ForeColor="Red" InitialValue="0" ValidationGroup="AddFamilyInfoVG"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp; </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06">First Name
                                                        </td>
                                                        <td class="Td07">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtFirstName" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                MaxLength="40" TabIndex="12" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" ValidationGroup="AddFamilyInfoVG"></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="FTB_TxtFirstName" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                                ValidChars=" ." TargetControlID="TxtFirstName">
                                                            </Ajx:FilteredTextBoxExtender>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="RFV_TxtFirstName" runat="server" ControlToValidate="TxtFirstName" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter First Name" ValidationGroup="AddFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06">Last Name
                                                        </td>
                                                        <td class="Td07">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtLastName" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                MaxLength="40" TabIndex="13" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" ValidationGroup="AddFamilyInfoVG"></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="FTB_TxtLastName" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                                ValidChars=" ." TargetControlID="TxtLastName">
                                                            </Ajx:FilteredTextBoxExtender>

                                                        </td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="RFV_TxtLastName" runat="server" ControlToValidate="TxtLastName" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter Last name" ValidationGroup="AddFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="TdMandate"></td>
                                                        <td class="Td06 Td08">Date Of Birth
                                                        </td>
                                                        <td class="Td07 Td08">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtDOB" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                TabIndex="14" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'"></asp:TextBox>
                                                            <%-- ValidationGroup="AddFamilyInfoVG"--C%>
                                                            <Ajx:MaskedEditExtender ID="MEE_TxtDOB" runat="server" AcceptNegative="Left"
                                                                CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError" TargetControlID="TxtDOB" AcceptAMPM="false" ClearTextOnInvalid="true"
                                                                UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour" />
                                                            <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="TxtDOB" PopupButtonID="TxtDOB">
                                                            </Ajx:CalendarExtender>
                                                        </td>
                                                        <td colspan="2">
                                                            <%C--  <asp:RequiredFieldValidator ID="RFV_TxtDOB" runat="server" ControlToValidate="TxtDOB" CssClass="RvCls"
                                                    Display="Dynamic" ErrorMessage="Please enter Date Of Birth" ValidationGroup="AddFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                            --C%>
                                                            <asp:RangeValidator ID="RV_TxtDOB" runat="server" ControlToValidate="TxtDOB" ForeColor="Red" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Invalid DOB [Future Dates not allowed]" MaximumValue='<%# DateTime.Now.ToString("dd/MM/yyyy") %>'
                                                                MinimumValue='<%# DateTime.Now.AddYears(-100).ToString("dd/MM/yyyy") %>'
                                                                Type="Date" SetFocusOnError="True" ValidationGroup="AddFamilyInfoVG"></asp:RangeValidator>
                                                            <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic" CssClass="lblValidation"
                                                                ErrorMessage="Invalid Date" ControlToValidate="TxtDOB" ValidationGroup="AddFamilyInfoVG"
                                                                SetFocusOnError="True" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" ForeColor="Red"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td class="TdMandate"><span class="rcls">*</span></td>
                                                        <td class="Td06 Td08">Gender
                                                        </td>
                                                        <td class="Td07 Td08">
                                                            <b>:</b>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="RbtnGender" TabIndex="15" runat="server" ValidationGroup="AddFamilyInfoVG" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1" Text="Male" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Female"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:RequiredFieldValidator ID="RFV_RbtnGender" runat="server" ControlToValidate="RbtnGender" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please select Gender" ValidationGroup="AddFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <asp:Panel runat="server" ID="pnlChild" Visible="false">
                                                        <tr class="Cls01">
                                                            <td class="TdMandate"></td>
                                                            <td class="Td06">Other allowances
                                                            </td>
                                                            <td class="Td07">
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="ChkOtherAllowances" TabIndex="16" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" />
                                                            </td>
                                                            <td colspan="2"></td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr class="Cls01">
                                                            <td class="TdMandate"></td>
                                                            <td class="Td06">Child Hostel allowances
                                                            </td>
                                                            <td class="Td07">
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="ChkChildHostelAllowances" TabIndex="17" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" />
                                                            </td>
                                                            <td colspan="2"></td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr class="Cls01">
                                                            <td class="TdMandate"></td>
                                                            <td class="Td06">Child Educational allowances
                                                            </td>
                                                            <td class="Td07">
                                                                <b>:</b>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="ChkChildEducationalAllowances" TabIndex="18" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" />
                                                            </td>
                                                            <td colspan="2"></td>
                                                            <td>&nbsp;
                                                            </td>
                                                        </tr>
                                                    </asp:Panel>

                                                    <tr>

                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Button ID="BtnSubmit" runat="server" TabIndex="19" Text="Submit" ValidationGroup="AddFamilyInfoVG" CommandName="ADDFAMILY"
                                                                OnClientClick="return  validate('Add');" />
                                                            &nbsp;
                                                 <input type="button" id="clear-form" tabindex="20" value="Clear" class="btnStyle" />
                                                            &nbsp;
                                                <asp:Button ID="btnCancel" runat="server" TabIndex="21" Text="Cancel" CommandName="CANCEL" />

                                                        </td>
                                                        <td colspan="3">
                                                            <div class="Fr" style="width: 99%; color: Red;">
                                                                <asp:Literal ID="LtrMsg" runat="server"></asp:Literal>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="DivSpacer01">
                                            </div>
                                        </InsertItemTemplate>--%>

                                          <InsertItemTemplate>
                                            <div>
                                                <b>Add new Family information</b>
                                                <%--  <hr class="HrCls" />--%>
                                                <div class="DivSpacer01"></div>
                                            </div>
                                            <div class="DivSpacer03 Cb">

                                                <div class="form-inline">
                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span>  Family Type&nbsp;<b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="DDL_FamilyTypes" runat="server" CssClass="txtDropDownwidth"  AutoPostBack="false"
                                                                TabIndex="11" ValidationGroup="AddFamilyInfoVG"><%--OnSelectedIndexChanged="DDL_FamilyTypes_SelectedIndexChanged"--%>
                                                            </asp:DropDownList>
                                                            <Ajx:CascadingDropDown ID="CCD_DDL_FamilyTypes" runat="server" Category="DDL_FamilyTypes" EmptyText="- SELECT FAMILY TYPE -" EmptyValue="0"
                                                                LoadingText="[LOADING FAMILY TYPES....]" PromptText="- SELECT FAMILY TYPE -" PromptValue="0" TargetControlID="DDL_FamilyTypes"
                                                                ServicePath="~/WebService/Service.asmx" ServiceMethod="GetFamilyMemTypes">
                                                            </Ajx:CascadingDropDown>

                                                            <asp:RequiredFieldValidator ID="RFV_DDL_FamilyTypes" runat="server" ControlToValidate="DDL_FamilyTypes" Display="Dynamic" CssClass="lblValidation"
                                                                ErrorMessage="Please select Family type" ForeColor="Red" InitialValue="0" ValidationGroup="AddFamilyInfoVG"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> First Name &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="TxtFirstName" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                MaxLength="40" TabIndex="12" placeholder="Enter First Name" ValidationGroup="AddFamilyInfoVG"></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="FTB_TxtFirstName" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                                ValidChars=" ." TargetControlID="TxtFirstName">
                                                            </Ajx:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator ID="RFV_TxtFirstName" runat="server" ControlToValidate="TxtFirstName" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter First Name" ValidationGroup="AddFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> Last Name &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="TxtLastName" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                MaxLength="40" TabIndex="13" placeholder="Enter Last Name" ValidationGroup="AddFamilyInfoVG"></asp:TextBox>
                                                            <Ajx:FilteredTextBoxExtender ID="FTB_TxtLastName" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                                ValidChars=" ." TargetControlID="TxtLastName">
                                                            </Ajx:FilteredTextBoxExtender>

                                                            <asp:RequiredFieldValidator ID="RFV_TxtLastName" runat="server" ControlToValidate="TxtLastName" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please enter Last name" ValidationGroup="AddFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls"></span> Date Of Birth &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="TxtDOB" runat="server" autocomplete="off" CssClass="txtDropDownwidth"
                                                                TabIndex="14" placeholder="DD/MM/YYYY"></asp:TextBox>

                                                            <Ajx:MaskedEditExtender ID="MEE_TxtDOB" runat="server" AcceptNegative="Left"
                                                                CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                                                MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                                                OnInvalidCssClass="MaskedEditError" TargetControlID="TxtDOB" AcceptAMPM="false" ClearTextOnInvalid="true"
                                                                UserDateFormat="DayMonthYear" UserTimeFormat="TwentyFourHour" />
                                                            <Ajx:CalendarExtender ID="CE_TxtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                                                TargetControlID="TxtDOB" PopupButtonID="TxtDOB">
                                                            </Ajx:CalendarExtender>

                                                            <asp:RangeValidator ID="RV_TxtDOB" runat="server" ControlToValidate="TxtDOB" ForeColor="Red" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Invalid DOB [Future Dates not allowed]" MaximumValue='<%# DateTime.Now.ToString("dd/MM/yyyy") %>'
                                                                MinimumValue='<%# DateTime.Now.AddYears(-100).ToString("dd/MM/yyyy") %>'
                                                                Type="Date" SetFocusOnError="True" ValidationGroup="AddFamilyInfoVG"></asp:RangeValidator>
                                                            <asp:RegularExpressionValidator ID="REV_TxtToDate" runat="server" Display="Dynamic" CssClass="lblValidation"
                                                                ErrorMessage="Invalid Date" ControlToValidate="TxtDOB" ValidationGroup="AddFamilyInfoVG"
                                                                SetFocusOnError="True" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$" ForeColor="Red"></asp:RegularExpressionValidator>

                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <div class="col-sm-2 htCr"><span class="rcls">*</span> Gender &nbsp;<b>:</b></div>
                                                        <div class="col-sm-6">
                                                            <asp:RadioButtonList ID="RbtnGender" TabIndex="15" runat="server" ValidationGroup="AddFamilyInfoVG" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" RepeatDirection="Horizontal">
                                                                <asp:ListItem Value="1" Text="Male" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Female"></asp:ListItem>
                                                            </asp:RadioButtonList>

                                                            <asp:RequiredFieldValidator ID="RFV_RbtnGender" runat="server" ControlToValidate="RbtnGender" CssClass="lblValidation"
                                                                Display="Dynamic" ErrorMessage="Please select Gender" ValidationGroup="AddFamilyInfoVG" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <asp:Panel runat="server" ID="pnlChild" Visible="false">
                                                        <div class="form-group">
                                                            <div class="col-sm-3 htCr"><span class="rcls"></span> Other allowances &nbsp;<b>:</b></div>
                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="ChkOtherAllowances" TabIndex="16" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-sm-3 htCr"><span class="rcls"></span> Child Hostel allowances &nbsp;<b>:</b></div>
                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="ChkChildHostelAllowances" TabIndex="17" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" />
                                                            </div>
                                                        </div>

                                                        <div class="form-group">
                                                            <div class="col-sm-3 htCr"><span class="rcls"></span>  Child Educational allowances&nbsp;<b>:</b></div>
                                                            <div class="col-sm-6">
                                                                <asp:CheckBox ID="ChkChildEducationalAllowances" TabIndex="18" onfocus="this.style.backgroundColor='lightgrey'" onblur="this.style.backgroundColor='white'" runat="server" Text="" />
                                                            </div>
                                                        </div>
                                                    </asp:Panel>
                                                </div>

                                                <div class="btn-group-sm">
                                                    <div class="col-sm-1" style="width:85px;">
                                                        <asp:Button ID="BtnSubmit" Width="80px" runat="server" TabIndex="19" Text="Submit" ValidationGroup="AddFamilyInfoVG" CommandName="ADDFAMILY"
                                                            OnClientClick="return  validate('Add');" />
                                                    </div>
                                                    <div class="col-sm-1" style="width:85px;">
                                                        <input type="button" style="width:80px" id="clear-form" tabindex="20" value="Clear" class="btnStyle" onclick="Clear();"/>
                                                        <%--<asp:Button ID="btnClear" runat="server" TabIndex="21" Text="Clear" CommandName="CLEAR" />--%>
                                                    </div>
                                                    <div class="col-sm-1">
                                                        <asp:Button ID="btnCancel" Width="80px" runat="server" TabIndex="22" Text="Cancel" CommandName="CANCEL" />
                                                    </div>
                                                </div>
                                                <div class="Fr" style="width: 99%; color: Red;">
                                                    <asp:Literal ID="LtrMsg" runat="server"></asp:Literal>
                                                </div>

                                                
                                            </div>
                                            <div class="DivSpacer01">
                                            </div>
                                        </InsertItemTemplate>

                                    </asp:FormView>
                                </div>


                            </asp:View>
                        </asp:MultiView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<script src="../../Scripts/jquery-3.1.1.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
      <%--  $(document).ready(function () {
            $('#clear-form').on('click', function () {
                $('#FrmID')[0].reset();
                $("input[type='text'], textarea, input[type='password']").val('');
                $('.RvCls').css('display', 'none');
                $('.Cls01').addClass('DispNone')
                $('#<%= LblMsg.ClientID%>').html('')
                return false;
            });
        });



        function Expand_DDL(element) {
            element.style.width = 'auto';
            element.style.backgroundColor = 'white';
        }

        function Compress_DDL(element, width) {
            element.style.width = '' + width + 'px';
            element.style.backgroundColor = 'white';
        }--%>

        function validate(Msg) {
            if (Page_ClientValidate())
                return confirm('Do you want to ' + Msg + ' this Family member details ?');
        }

        function Clear() {
            document.getElementById("MainContent_FV_FamilyInfo_TxtFirstName").value = "";
            document.getElementById("MainContent_FV_FamilyInfo_TxtLastName").value = "";
            document.getElementById("MainContent_FV_FamilyInfo_TxtDOB").value = "";
            document.getElementById("MainContent_FV_FamilyInfo_DDL_FamilyTypes").selectedIndex = "";
            document.getElementById("MainContent_FV_FamilyInfo_RbtnGender_0").checked = true;
        }


        //$(document).ready(function () {
        //    $('.Cls01').addClass('DispNone')

        //});
    </script>
</asp:Content>

