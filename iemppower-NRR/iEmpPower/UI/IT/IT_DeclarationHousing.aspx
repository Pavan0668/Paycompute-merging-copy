<%@ Page Title="" Language="C#" MasterPageFile="~/UI/SubSiteMaster.master" AutoEventWireup="true" CodeBehind="IT_DeclarationHousing.aspx.cs"
    Inherits="iEmpPower.UI.IT.IT_DeclarationHousing" Theme="SkinFile" EnableEventValidation="false" Culture="en-GB"
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<%--<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .TblCls
        {
            border-collapse: collapse;
        }

        .Td01
        {
            /*color: #004080;*/
            font-size: 13px;
            width: 160px;
            height: 28px;
            padding: 3px;
            text-align: justify !important;
        }

        .Td02
        {
            /*color: #004080;*/
            font-size: 13px;
            width: 10px;
            height: 28px;
            padding: 8px;
            text-align: center;
            line-height: 12px !important;
        }

        .Td03
        {
            width: 235px;
            height: 28px;
            padding: 3px;
        }

        .auto-style1
        {
            width: 200px;
        }
    </style>
</asp:Content>--%>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="LblLockSts" runat="server" CssClass="msgboard"></asp:Label>
    <div id="DivHosuing" runat="server">
        <h2>Housing Details : From April 1<sup>st</sup>
            <asp:Label ID="LblFromDate" runat="server"></asp:Label>
            To March 31<sup>st</sup>
            <asp:Label ID="LblToDate" runat="server"></asp:Label></h2>

        <asp:Label ID="lblMessageBoard" runat="server" CssClass="msgboard"></asp:Label>

        <div>
            <div style="width: 70%">
                <fieldset style="border-color: white; border-style: none;">

                    <table class="TblCls">
                        <%--<tr>
                            <td colspan="4" style="text-align:right; height: 20px;">
                                <asp:Button id="BtnEDIT" runat="server" Text="EDIT" OnClick="BtnEDIT_Click"/>
                            </td>
                        </tr>--%>
                        <%-- <tr>
                            <td class="Td01">Accomodation Type
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:DropDownList ID="DDl_AccomTyp" runat="server" Font-Size="12px" CssClass="textbox" OnSelectedIndexChanged="DDl_AccomTyp_SelectedIndexChanged" AutoPostBack="true" TabIndex="1">
                                    <asp:ListItem Text="SELECT" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Rented" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Own" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RFV_DDl_AccomTyp" runat="server" ErrorMessage="Please Select Accomodation Type" ForeColor="Red"
                                    ControlToValidate="DDl_AccomTyp" ValidationGroup="VG1" InitialValue="0"></asp:RequiredFieldValidator>
                            </td>

                        </tr>--%>
                        <tr>
                            <td class="Td01">City Category
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:DropDownList ID="DDL_CityCat" runat="server" Font-Size="12px" CssClass="textbox" TabIndex="2">
                                    <asp:ListItem Text="SELECT" Value="-1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Metro" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Non-Metro" Value="0"></asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RF_DDL_CityCat" runat="server" ErrorMessage="Please Select City Category" ForeColor="Red"
                                    ControlToValidate="DDL_CityCat" ValidationGroup="VG1" InitialValue="-1"></asp:RequiredFieldValidator>
                            </td>


                        </tr>
                    </table>
                </fieldset>
            </div>

            <b>HRA-RENT PAYMENT DETAILS</b>
            <br />
            <br />
            <asp:GridView ID="gvHRA" runat="server" CssClass="gridview" AutoGenerateColumns="false" DataKeyNames="ID"
            EmptyDataText="No records has been added."  OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit"
            OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting">
                <Columns>
                    <asp:TemplateField HeaderText="From Date" ItemStyle-Width="80">
                        <ItemTemplate>
                            <asp:Label ID="lblFromDate" runat="server" Text='<%# Eval("BEGDA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFromDate" runat="server" Text='<%# Eval("BEGDA", "{0:dd/MM/yyyy}") %>' Width="80"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To Date" ItemStyle-Width="80">
                        <ItemTemplate>
                            <asp:Label ID="lblToDate" runat="server" Text='<%# Eval("ENDDA", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtToDate" runat="server" Text='<%# Eval("ENDDA", "{0:dd/MM/yyyy}") %>' Width="80"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rent Per Month" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblRentPerMonth" runat="server" Text='<%# Eval("RTAMT") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRentPerMonth" runat="server" Text='<%# Eval("RTAMT") %>'  Width="80"  OnTextChanged = "OnTextChanged" AutoPostBack = "true"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Period Rent" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Right" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPeriodRent" runat="server" Text=""  Width="80"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Address" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'  Width="140"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtAddress" runat="server" Text='<%# Eval("Address") %>' TextMode="MultiLine" Width="140"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="State" ItemStyle-Width="130">
                        <ItemTemplate>
                            <asp:Label ID="lblState" runat="server" Text='<%# Eval("BEZEI") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <%--<asp:TextBox ID="txtState" runat="server" Text='<%# Eval("State") %>'></asp:TextBox>--%>
                            <asp:DropDownList ID="drpdwnState" runat="server" Width="130px" CssClass="textbox"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFVdrpdwnState" runat="server" ErrorMessage="Please select State" ForeColor="Red" ControlToValidate="drpdwnState" InitialValue="0"></asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="City" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCity" runat="server" Text='<%# Eval("City") %>'  Width="100"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LandLord's Name" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblLandLordsName" runat="server" Text='<%# Eval("LDNAM") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLandLordsName" runat="server" Text='<%# Eval("LDNAM") %>'  Width="100"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LandLord's Address" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="lblLandLordsAddress" runat="server" Text='<%# Eval("LDAD1") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLandLordsAddress" runat="server" Text='<%# Eval("LDAD1") %>' Width="140" TextMode="MultiLine"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PAN of LandLord" ItemStyle-Width="80">
                        <ItemTemplate>
                            <asp:Label ID="lblPANofLandLord" runat="server" Text='<%# Eval("LDAID") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPANofLandLord" runat="server" Text='<%# Eval("LDAID") %>'  Width="80"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true" ItemStyle-Width="80" ValidationGroup="VGEdit"/>
                </Columns>
            </asp:GridView>
            <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse;margin-left:8px">
                <tr>
                    <td style="width: 80px">From Date:<br />
                        <asp:TextBox ID="txtFromDate" runat="server" Width="80"  ValidationGroup="VGAdd"/>
                        <cc1:MaskedEditExtender ID="MEE_txtFromDate" runat="server" AcceptNegative="Left"
                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtFromDate" />
                        <cc1:CalendarExtender ID="CE_txtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtFromDate">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RFV_txtFromDate" runat="server" ControlToValidate="txtFromDate" ValidationGroup="VGAdd" ErrorMessage="Please select From Date"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RV_txtFromDate" runat="server" ControlToValidate="txtFromDate" Display="Dynamic"
                            ErrorMessage="" ValidationGroup="VGAdd"
                            Type="Date" ForeColor="Red"></asp:RangeValidator>
                    </td>
                    <td style="width: 80px">To Date:<br />
                        <asp:TextBox ID="txtToDate" runat="server" Width="80" ValidationGroup="VGAdd"/>
                        <cc1:MaskedEditExtender ID="MEE_txtToDate" runat="server" AcceptNegative="Left"
                            CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                            MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                            OnInvalidCssClass="MaskedEditError" TargetControlID="txtToDate" />
                        <cc1:CalendarExtender ID="CE_txtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                            TargetControlID="txtToDate">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RFV_txtToDate" runat="server" ControlToValidate="txtToDate" ValidationGroup="VGAdd" ErrorMessage="Please select To Date"
                            ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="RV_txtToDate" runat="server" ControlToValidate="txtToDate" Display="Dynamic"
                            ErrorMessage="" ValidationGroup="VGAdd"
                            Type="Date" ForeColor="Red"></asp:RangeValidator>
                    </td>
                    <td style="width: 80px">Rent Per Month:<br />
                        <asp:TextBox ID="TXT_ActAmount" runat="server" Width="80" AutoPostBack="True" OnTextChanged="TXT_ActAmount_TextChanged" ValidationGroup="VGAdd"/>
                        <asp:RequiredFieldValidator ID="RFV_TXT_ActAmount" runat="server" ErrorMessage="Please Enter Rent"
                            ForeColor="Red" ControlToValidate="TXT_ActAmount" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                        <cc1:FilteredTextBoxExtender ID="FTB_TXT_ActAmount" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                            TargetControlID="TXT_ActAmount" ValidChars=".">
                        </cc1:FilteredTextBoxExtender>

                        <asp:RegularExpressionValidator ID="REVTXT_ActAmount" runat="server" ControlToValidate="TXT_ActAmount"
                            ErrorMessage="> 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                            ValidationGroup="VGAdd"></asp:RegularExpressionValidator>
                    </td>
                    <td style="width: 80px;display:none;">Period Rent:<br />
                        <asp:Label ID="lblPeriodRent" runat="server" Width="80" />
                        <br />
                        <br />
                    </td>
                    <td style="width: 150px">Address:<br />
                        <asp:TextBox ID="txtAddress" runat="server" Width="140" TextMode="MultiLine" ValidationGroup="VGAdd"/>
                        <asp:RequiredFieldValidator ID="RFVtxtAddress" runat="server" ErrorMessage="Please Enter Address"
                            ForeColor="Red" ControlToValidate="txtAddress" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                    </td>
                    <td style="width: 130px">State:<br />
                        <%--<asp:TextBox ID="txtState" runat="server" Width="140" />--%>
                        <asp:DropDownList ID="drpdwnState" runat="server" Width="130px" CssClass="textbox" ValidationGroup="VGAdd"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVdrpdwnState" runat="server" ErrorMessage="Please select State" ForeColor="Red" ControlToValidate="drpdwnState" InitialValue="0" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                    </td>
                    <td style="width: 150px">City:<br />
                        <asp:TextBox ID="txtCity" runat="server" Width="140" ValidationGroup="vg1"/>
                        <asp:RequiredFieldValidator ID="RFVCity" runat="server" ErrorMessage="Please Enter City"
                            ForeColor="Red" ControlToValidate="txtCity" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                        <br />
                        <br />
                    </td>
                    <td style="width: 150px">LandLord's Name:<br />
                        <asp:TextBox ID="TXTLandLordName" runat="server" Width="140" ValidationGroup="VGAdd"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter Name"
                            ForeColor="Red" ControlToValidate="TXTLandLordName" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 150px">LandLord's Address:<br />
                        <asp:TextBox ID="TXTLandLordAddr" runat="server" Width="140" TextMode="MultiLine" ValidationGroup="VGAdd"/>
                        <asp:RequiredFieldValidator ID="RFV_TXTLandLordAddr" runat="server" ErrorMessage="Please Enter Address"
                            ForeColor="Red" ControlToValidate="TXTLandLordAddr" ValidationGroup="VGAdd"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 80px">PAN of LandLord:<br />
                        <asp:TextBox ID="TXTPANLAndLord" runat="server" Width="80" ValidationGroup="VGAdd"/>
                        <asp:RequiredFieldValidator ID="RFV_TXTPANLAndLord" runat="server" ErrorMessage="Please Enter PAN"
                            ForeColor="Red" ControlToValidate="TXTPANLAndLord" ValidationGroup="VGAdd" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 50px" align="center">
                        <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="Insert" ValidationGroup="VGAdd"/>
                    </td>
                </tr>
            </table>




            <%--Old Code Starts
            <div style="width: 70%" id="HousingTab" runat="server">
                <fieldset>
                    <legend style="font-size: 17px;"><b>&nbsp;Housing Details&nbsp;</b></legend>
                    <table class="TblCls" id="Tab1" runat="server">


                        <tr>
                            <td class="Td01">From Date</td>
                            <td class="Td02"><b>:</b> </td>
                            <td class="Td03">
                                <asp:TextBox ID="txtFromDate" runat="server" ValidationGroup="vg2" Style="text-align: center"></asp:TextBox>

                            </td>
                            <td>
                                <cc1:MaskedEditExtender ID="MEE_txtFromDate" runat="server" AcceptNegative="Left"
                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtFromDate" />
                                <cc1:CalendarExtender ID="CE_txtFromDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtFromDate">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RFV_txtFromDate" runat="server" ControlToValidate="txtFromDate" ValidationGroup="vg1" ErrorMessage="*"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RV_txtFromDate" runat="server" ControlToValidate="txtFromDate" Display="Dynamic"
                                    ErrorMessage="" ValidationGroup="vg1"
                                    Type="Date" ForeColor="Red"></asp:RangeValidator>
                            </td>


                        </tr>
                        <tr>
                            <td class="Td01">To Date</td>
                            <td class="Td02"><b>:</b> </td>
                            <td class="Td03">
                                <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="vg2" Style="text-align: center"></asp:TextBox>

                            </td>
                            <td>
                                <cc1:MaskedEditExtender ID="MEE_txtToDate" runat="server" AcceptNegative="Left"
                                    CultureName="en-GB" DisplayMoney="Left" ErrorTooltipEnabled="true" Mask="99/99/9999"
                                    MaskType="Date" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
                                    OnInvalidCssClass="MaskedEditError" TargetControlID="txtToDate" />
                                <cc1:CalendarExtender ID="CE_txtToDate" runat="server" Enabled="True" Format="dd/MM/yyyy"
                                    TargetControlID="txtToDate">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RFV_txtToDate" runat="server" ControlToValidate="txtToDate" ValidationGroup="vg1" ErrorMessage="*"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RangeValidator ID="RV_txtToDate" runat="server" ControlToValidate="txtToDate" Display="Dynamic"
                                    ErrorMessage="" ValidationGroup="vg1"
                                    Type="Date" ForeColor="Red"></asp:RangeValidator>
                            </td>


                        </tr>
                        <tr>
                            <td class="Td01">Actual Amount</td>
                            <td class="Td02"><b>:</b> </td>
                            <td class="Td03">
                                <asp:TextBox ID="TXT_ActAmount" runat="server" CssClass="textbox" TabIndex="3"></asp:TextBox>&nbsp;INR
                                            
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RFV_TXT_ActAmount" runat="server" ErrorMessage="Please Enter Actual Amount"
                                    ForeColor="Red" ControlToValidate="TXT_ActAmount" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FTB_TXT_ActAmount" runat="server" FilterMode="ValidChars" FilterType="Custom,Numbers"
                                    TargetControlID="TXT_ActAmount" ValidChars=".">
                                </cc1:FilteredTextBoxExtender>

                                <asp:RegularExpressionValidator ID="REVTXT_ActAmount" runat="server" ControlToValidate="TXT_ActAmount"
                                    ErrorMessage="Must be greater than 0" ForeColor="Red" ValidationExpression="^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"
                                    ValidationGroup="VG1"></asp:RegularExpressionValidator>
                            </td>


                        </tr>
                        <tr>
                            <td colspan="3" style="font-size: 13px; height: 40px; padding: 3px; text-align: justify !important;">
                                <asp:CheckBox ID="CB_ConsAct" runat="server" Font-Size="13px" Text="HRA to be Excempt" TabIndex="4" />
                            </td>


                        </tr>
                    </table>

                </fieldset>

            </div>

            <div style="width: 70%" id="LandLordsTab" runat="server">
                <fieldset>
                    <legend style="font-size: 17px;"><b>&nbsp;LandLord's Details&nbsp;</b></legend>
                    <table class="TblCls" id="Tab2" runat="server">
                        <tr>
                            <td class="Td01">LandLord's Name
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:TextBox ID="TXTLandLordName" runat="server" CssClass="textbox" Columns="9" TabIndex="5"></asp:TextBox>

                            </td>
                            <td class="auto-style1">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Enter LandLord's Name"
                                    ForeColor="Red" ControlToValidate="TXTLandLordName" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                            </td>

                        </tr>

                        <tr>
                            <td class="Td01">LandLord's Address
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:TextBox ID="TXTLandLordAddr" runat="server" CssClass="textbox" TextMode="MultiLine"
                                    Columns="9" TabIndex="5"></asp:TextBox>

                            </td>
                            <td class="auto-style1">
                                <asp:RequiredFieldValidator ID="RFV_TXTLandLordAddr" runat="server" ErrorMessage="Please Enter LandLord's Address"
                                    ForeColor="Red" ControlToValidate="TXTLandLordAddr" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                            </td>

                        </tr>
                        <tr>
                            <td class="Td01">PAN of LandLord</td>
                            <td class="Td02"><b>:</b> </td>
                            <td class="Td03">
                                <asp:TextBox ID="TXTPANLAndLord" runat="server" CssClass="textbox" TabIndex="6"></asp:TextBox>

                            </td>

                            <td class="auto-style1">
                                <asp:RequiredFieldValidator ID="RFV_TXTPANLAndLord" runat="server" ErrorMessage="Please Enter PAN of LandLord's"
                                    ForeColor="Red" ControlToValidate="TXTPANLAndLord" ValidationGroup="VG1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>

                            <td colspan="3" style="font-size: 13px; height: 40px; padding: 3px; text-align: justify !important;">
                                <asp:CheckBox ID="CHK_LLDECLARATION" runat="server" Font-Size="13px" Text="Declaration Provided by LandLord" TabIndex="7" />
                            </td>

                        </tr>
                    </table>
                </fieldset>
            </div>

            <div style="width: 70%">
                <fieldset style="border-color: white; border-style: none;">


                    <table class="TblCls">

                        <tr>
                            <td class="Td01">Comments
                            </td>
                            <td class="Td02">
                                <b>:</b></td>
                            <td class="Td03">
                                <asp:TextBox ID="TXTCOMMENTS" runat="server" CssClass="textbox" TextMode="MultiLine"
                                    Columns="9" TabIndex="8"></asp:TextBox>
                            </td>
                            <td></td>

                        </tr>

                    </table>
                </fieldset>
            </div>
       
Old Code Ends--%>
        </div>
        <br />
        <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="VG1" CausesValidation="true" TabIndex="9" />
        <asp:Button ID="BtnEDIT" runat="server" Text="Edit" TabIndex="10" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="VG1" CausesValidation="true" TabIndex="11" />
        <asp:Button ID="BtnCancel" runat="server" Text="Cancel" TabIndex="12" />--%>

    </div>
</asp:Content>
